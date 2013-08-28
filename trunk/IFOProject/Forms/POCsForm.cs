using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IFOProject.Forms
{
    public partial class POCsForm : Form
    {
        /// <summary>
        /// Simgle parameter class
        /// </summary>
        private class Parameter
        {
            /// <summary>
            /// Parameter name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Parameter value
            /// </summary>
            public double Value { get; set; }

            /// <summary>
            /// Can user change this value?
            /// Some values are calculated from anothers.
            /// </summary>
            public bool Editable { get; set; }

            /// <summary>
            /// Will the value be diplayed?
            /// Only editable params and final POCs are visible.
            /// </summary>
            public bool Visible { get; set; }

            /// <summary>
            /// If parameter is dependent from anothers,
            /// this field contains it's actual function
            /// </summary>
            public Calculate Function { get; set; }

            /// <summary>
            /// All-args constructor
            /// </summary>
            public Parameter(string name, double value, bool editable,
                bool visible, Calculate function)
            {
                Name = name;
                Value = value;
                Editable = editable;
                Visible = visible;
                Function = function;
            }

            /// <summary>
            /// Creates hidden constant-parameter
            /// </summary>
            public Parameter(string name, double value) :
                this(name, value, false, false, null) { }

            /// <summary>
            /// Adapted for adding to DataGridView as a row values
            /// </summary>
            /// <returns></returns>
            public object[] ToArray()
            {
                return new object[] { Name, Value };
            }

            public delegate double Calculate();
        }

        /// <summary>
        /// Data source for DataGridView control
        /// </summary>
        private class ParametersList
        {
            /// <summary>
            /// Backing list. It's private to avoid modification of non-editable parameters.
            /// </summary>
            private List<Parameter> Parameters { get; set; }

            /// <summary>
            /// Creates full parameters list with default values
            /// </summary>
            /// <param name="args">Calculated values</param>
            public ParametersList(params Parameter[] args)
            {
                Parameters = new List<Parameter>() {
                    new Parameter("Gravitational Acceleration, m/s^2", 9.80954),
                    new Parameter("Refractive index of environment", 1.00027),
                    new Parameter("Wavelength, nm", 632.8),
                    new Parameter("Lever Amplification (mechanical advantage)", 8.31, true, true, null),
                    new Parameter("Sample's height, mm", 3.026, true, true, null),
                    new Parameter("Distance between loading edges, mm", 4, true, true, null),
                    new Parameter("Elastic Complianse Coefficient, Brewsters", -0.801, true, true, null),
                    new Parameter("Initial refractive index", 2.286, true, true, null),
                    new Parameter("Loading force, P", 0, false, false, delegate()
                        {
                            return this["Loading mass difference, kg"] *
                                this["Gravitational Acceleration, m/s^2"] *
                                this["Lever Amplification (mechanical advantage)"];
                        }),
                    new Parameter("Factor", 0, false, false, delegate()
                        {
                            return (this["Wavelength, nm"] * Math.Pow(this["Sample's height, mm"], 3)) /
                                (12 * 180 * this["Loading force, P"] * this["Distance between loading edges, mm"]);
                        }),
                    new Parameter("Effective piezooptical coefficient, Brewsters", 0, false, true, delegate()
                        {
                            return this["Factor"] * this["Line slope, deg"];
                        }),
                    new Parameter("EPOC Error", 0, false, true, delegate()
                        {
                            double deltaP = this["Gravitational Acceleration, m/s^2"] *
                                (0.001 * this["Lever Amplification (mechanical advantage)"] +
                                0.01 * this["Loading mass difference, kg"]);
                            double deltaD = 12 * 180 * (0.01 * this["Loading force, P"] +
                                deltaP * this["Distance between loading edges, mm"]);
                            double D = 12 * 180 * this["Loading force, P"] *
                                this["Distance between loading edges, mm"];
                            double deltaN = 0.015 * this["Wavelength, nm"] *
                                Math.Pow(this["Sample's height, mm"], 2);
                            double N = this["Wavelength, nm"] *
                                Math.Pow(this["Sample's height, mm"], 3);
                            double deltaF = (N / D) * ((Math.Abs(deltaN / N) +
                                Math.Abs(deltaD / D)) / (1 - Math.Abs(deltaD / D)));
                            return this["Line slope, deg"] * deltaF +
                                this["Standard error, deg/mm"] + this["Factor"];
                        }),
                    new Parameter("Elastic term, Brewsters", 0, false, false, delegate()
                        {
                            return (this["Initial refractive index"] -
                                this["Refractive index of environment"]) *
                                this["Elastic Complianse Coefficient, Brewsters"];
                        }),
                    new Parameter("Stress-optic coefficient, Brewsters", 0, false, true, delegate()
                        {
                            return this["Effective piezooptical coefficient, Brewsters"] -
                                this["Elastic term, Brewsters"];
                        }),
                    new Parameter("SOC error", 0, false, true, delegate()
                        {
                            return this["EPOC Error"] + 0.001 *
                                (Math.Abs(this["Initial refractive index"]) + 
                                Math.Abs(this["Stress-optic coefficient, Brewsters"]));
                        }),
                    new Parameter("Piezooptic coefficient, Brewsters", 0, false, true, delegate()
                        {
                            return -2 * this["Stress-optic coefficient, Brewsters"] /
                                Math.Pow(this["Initial refractive index"], 3);
                        }),
                    new Parameter("POC Error", 0, false, true, delegate()
                        {
                            double a = Math.Abs(this["Piezooptic coefficient, Brewsters"]);
                            double b = Math.Abs(this["SOC error"] /
                                this["Stress-optic coefficient, Brewsters"]);
                            double c = Math.Abs(0.003 * Math.Pow(this["Initial refractive index"], 2));
                            return a * (b + c) / (1 - c);
                        })
                };
                Parameters.InsertRange(0, args);
                Recalculate();
            }

            /// <summary>
            /// Returns copy of the list for adding to the DataGridView
            /// </summary>
            /// <returns></returns>
            public List<Parameter> GetCopy()
            {
                return new List<Parameter>(Parameters);
            }

            /// <summary>
            /// Safe setter method.
            /// Allows modification only for editable fields.
            /// Recalculates dependent parameters.
            /// </summary>
            /// <param name="index">Index of parameter in the list</param>
            /// <param name="value">New parameter's value</param>
            public void SetValue(string name, double value)
            {
                Parameter selected = FindParameter(name);
                if (selected != null && selected.Editable)
                {
                    selected.Value = value;
                    Recalculate();
                }
            }

            private void Recalculate()
            {
                foreach (Parameter param in Parameters)
                    if (param.Function != null)
                        param.Value = param.Function();
            }

            private Parameter FindParameter(string name)
            {
                foreach (Parameter param in Parameters)
                    if (param.Name.Equals(name))
                        return param;
                return null;
            }

            public double this[string name]
            {
                get { return FindParameter(name).Value; }
            }
        }

        private ParametersList Parameters { get; set; }

        public POCsForm(double loadingMassDifference,
            double lineSlope, double standardError)
        {
            InitializeComponent();
            Parameters = new ParametersList(
                new Parameter("Loading mass difference, kg", loadingMassDifference),
                new Parameter("Line slope, deg", lineSlope, false, true, null),
                new Parameter("Standard error, deg/mm", standardError, false, true, null));
            foreach (Parameter param in Parameters.GetCopy())
            {
                if (param.Visible)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView, param.ToArray());
                    row.ReadOnly = !param.Editable;
                    dataGridView.Rows.Add(row);
                }
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            int col = e.ColumnIndex;
            if (row >= 0)
                Parameters.SetValue(
                    dataGridView[col - 1, row].Value.ToString(),
                    Convert.ToDouble(dataGridView[col, row].Value));
            UpdateTable();
        }

        private void UpdateTable()
        {
            for (int row = 0; row < dataGridView.Rows.Count; row++)
                dataGridView["Value", row].Value =
                    Parameters[dataGridView["Key", row].Value.ToString()];
        }
    }
}
