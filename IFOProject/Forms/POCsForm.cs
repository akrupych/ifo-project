using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

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
            /// Should this parameter be saved?
            /// Added for distinguishing current calculation parameters
            /// from static interferometry setup constants.
            /// </summary>
            public bool IsSaved { get; set; }

            /// <summary>
            /// All-args constructor
            /// </summary>
            public Parameter(string name, double value, bool editable,
                bool visible, Calculate function, bool isSaved)
            {
                Name = name;
                Value = value;
                Editable = editable;
                Visible = visible;
                Function = function;
                IsSaved = isSaved;
            }

            /// <summary>
            /// Creates hidden constant-parameter
            /// </summary>
            public static Parameter CreateHiddenConstant(string name, double value)
            {
                return new Parameter(name, value, false, false, null, true);
            }

            /// <summary>
            /// Creates visible editable parameter
            /// </summary>
            public static Parameter CreateVisibleVariable(string name, double value)
            {
                return new Parameter(name, value, true, true, null, true);
            }

            /// <summary>
            /// Creates function parameter
            /// </summary>
            public static Parameter CreateFunction(string name, double value, bool editable,
                bool visible, Calculate function)
            {
                return new Parameter(name, value, editable, visible, function, false);
            }

            /// <summary>
            /// Creates not-to-save constant (result of current calculations)
            /// </summary>
            public static Parameter CreateInstantConstant(string name, double value, bool visible)
            {
                return new Parameter(name, value, false, visible, null, false);
            }

            /// <summary>
            /// Adapted for adding to DataGridView as a row values
            /// </summary>
            /// <returns></returns>
            public object[] ToArray()
            {
                return new object[] { Name, string.Format("{0:F6}", Value) };
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
            public ParametersList()
            {
                Parameters = new List<Parameter>() {
                    Parameter.CreateInstantConstant("Loading mass difference, kg", 0, false),
                    Parameter.CreateInstantConstant("Line slope, deg", 0, true),
                    Parameter.CreateInstantConstant("Standard error, deg/mm", 0, true),
                    Parameter.CreateHiddenConstant("Gravitational Acceleration, m/s^2", 9.80954),
                    Parameter.CreateHiddenConstant("Refractive index of environment", 1.00027),
                    Parameter.CreateHiddenConstant("Wavelength, nm", 632.8),
                    Parameter.CreateVisibleVariable("Lever Amplification (mechanical advantage)", 8.31),
                    Parameter.CreateVisibleVariable("Sample's height, mm", 3.026),
                    Parameter.CreateVisibleVariable("Distance between loading edges, mm", 4),
                    Parameter.CreateVisibleVariable("Elastic Complianse Coefficient, Brewsters", -0.801),
                    Parameter.CreateVisibleVariable("Initial refractive index", 2.286),
                    Parameter.CreateFunction("Loading force, P", 0, false, false, delegate()
                        {
                            return this["Loading mass difference, kg"] *
                                this["Gravitational Acceleration, m/s^2"] *
                                this["Lever Amplification (mechanical advantage)"];
                        }),
                    Parameter.CreateFunction("Factor", 0, false, false, delegate()
                        {
                            return (this["Wavelength, nm"] * Math.Pow(this["Sample's height, mm"], 3)) /
                                (12 * 180 * this["Loading force, P"] * this["Distance between loading edges, mm"]);
                        }),
                    Parameter.CreateFunction("Effective piezooptical coefficient, Brewsters", 0, false, true, delegate()
                        {
                            return this["Factor"] * this["Line slope, deg"];
                        }),
                    Parameter.CreateFunction("EPOC Error", 0, false, true, delegate()
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
                    Parameter.CreateFunction("Elastic term, Brewsters", 0, false, false, delegate()
                        {
                            return (this["Initial refractive index"] -
                                this["Refractive index of environment"]) *
                                this["Elastic Complianse Coefficient, Brewsters"];
                        }),
                    Parameter.CreateFunction("Stress-optic coefficient, Brewsters", 0, false, true, delegate()
                        {
                            return this["Effective piezooptical coefficient, Brewsters"] -
                                this["Elastic term, Brewsters"];
                        }),
                    Parameter.CreateFunction("SOC error", 0, false, true, delegate()
                        {
                            return this["EPOC Error"] + 0.001 *
                                (Math.Abs(this["Initial refractive index"]) + 
                                Math.Abs(this["Stress-optic coefficient, Brewsters"]));
                        }),
                    Parameter.CreateFunction("Piezooptic coefficient, Brewsters", 0, false, true, delegate()
                        {
                            return -2 * this["Stress-optic coefficient, Brewsters"] /
                                Math.Pow(this["Initial refractive index"], 3);
                        }),
                    Parameter.CreateFunction("POC Error", 0, false, true, delegate()
                        {
                            double a = Math.Abs(this["Piezooptic coefficient, Brewsters"]);
                            double b = Math.Abs(this["SOC error"] /
                                this["Stress-optic coefficient, Brewsters"]);
                            double c = Math.Abs(0.003 * Math.Pow(this["Initial refractive index"], 2));
                            return a * (b + c) / (1 - c);
                        })
                };
                Recalculate();
            }

            /// <summary>
            /// Returns copy of the list for adding to the DataGridView
            /// </summary>
            public List<Parameter> GetCopy()
            {
                return new List<Parameter>(Parameters);
            }

            /// <summary>
            /// Safe setter method.
            /// Allows modification only for editable fields.
            /// Recalculates dependent parameters.
            /// </summary>
            /// <param name="name">Parameter name</param>
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

            /// <summary>
            /// Non-safe parameter setter. Doesn't care if parameter is editable.
            /// </summary>
            /// <param name="name">Parameter name</param>
            /// <param name="value">New parameter's value</param>
            public void SetValueAnyway(string name, double value)
            {
                FindParameter(name).Value = value;
                Recalculate();
            }

            /// <summary>
            /// Recalculates all function parameters
            /// </summary>
            private void Recalculate()
            {
                foreach (Parameter param in Parameters)
                    if (param.Function != null)
                        param.Value = param.Function();
            }

            /// <summary>
            /// Returns parameter with a given name
            /// </summary>
            private Parameter FindParameter(string name)
            {
                foreach (Parameter param in Parameters)
                    if (param.Name.Equals(name))
                        return param;
                return null;
            }

            /// <summary>
            /// Returns parameter value
            /// </summary>
            /// <param name="name">Parameter name</param>
            public double this[string name]
            {
                get { return FindParameter(name).Value; }
            }

            /// <summary>
            /// Saves parameters to .prm file
            /// </summary>
            /// <param name="fileName">Full file path</param>
            public void Save(string fileName)
            {
                List<string> lines = new List<string>();
                foreach (var param in Parameters)
                    if (param.IsSaved)
                        lines.Add(param.Name + ":" + param.Value);
                File.WriteAllLines(fileName, lines.ToArray());
            }

            /// <summary>
            /// Loads parameters from .prm file
            /// </summary>
            /// <param name="fileName">Full file path</param>
            public void Load(string fileName)
            {
                string[] lines = File.ReadAllLines(fileName);
                foreach (var line in lines)
                {
                    string[] splitted = line.Split(new char[] { ':' });
                    FindParameter(splitted[0]).Value = double.Parse(splitted[1]);
                }
            }
        }

        private ParametersList Parameters { get; set; }

        public POCsForm(double loadingMassDifference,
            double lineSlope, double standardError)
        {
            InitializeComponent();
            Parameters = new ParametersList();
            Parameters.SetValueAnyway("Loading mass difference, kg", loadingMassDifference);
            Parameters.SetValueAnyway("Line slope, deg", lineSlope);
            Parameters.SetValueAnyway("Standard error, deg/mm", standardError);
            UpdateTable();
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
                dataGridView["Value", row].Value = string.Format("{0:F6}",
                    Parameters[dataGridView["Key", row].Value.ToString()]);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Parameters files (*.prm)|*.prm";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Parameters.Load(dialog.FileName);
                UpdateTable();
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Parameters files (*.prm)|*.prm";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Parameters.Save(dialog.FileName);
            }
        }
    }
}
