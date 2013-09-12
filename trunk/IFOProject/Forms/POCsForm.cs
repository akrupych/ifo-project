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
            public static Parameter CreateFunction(string name, bool visible, Calculate function)
            {
                return new Parameter(name, 0, false, visible, function, false);
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
                    Parameter.CreateFunction("Line slope Relative error", false, delegate()
                    {
                        return this["Standard error, deg/mm"] / this["Line slope, deg"];
                    }),
                    Parameter.CreateHiddenConstant("Gravitational Acceleration, m/s^2", 9.80954),
                    Parameter.CreateHiddenConstant("Refractive index of environment", 1.00027),
                    Parameter.CreateHiddenConstant("Wavelength, nm", 632.8),
                    Parameter.CreateVisibleVariable("Lever Amplification (mechanical advantage)", 8.31),
                    Parameter.CreateVisibleVariable("Sample's height, mm", 3.026),
                    Parameter.CreateVisibleVariable("Distance between loading edges, mm", 4),
                    Parameter.CreateVisibleVariable("Elastic Complianse Coefficient, Brewsters", -0.801),
                    Parameter.CreateVisibleVariable("Initial refractive index", 2.286),
                    Parameter.CreateFunction("Loading force, P", false, delegate()
                    {
                        return this["Loading mass difference, kg"] *
                            this["Gravitational Acceleration, m/s^2"] *
                            this["Lever Amplification (mechanical advantage)"];
                    }),
                    Parameter.CreateFunction("Factor", false, delegate()
                    {
                        return (this["Wavelength, nm"] * Math.Pow(this["Sample's height, mm"], 3)) /
                            (12 * 180 * this["Loading force, P"] * this["Distance between loading edges, mm"]);
                    }),
                    Parameter.CreateFunction("Effective piezooptical coefficient, Brewsters", true, delegate()
                    {
                        return this["Factor"] * this["Line slope, deg"];
                    }),
                    Parameter.CreateFunction("EPOC Relative error", false, delegate()
                    {
                        double delta_h = 0.01 / this["Sample's height, mm"];
                        double delta_a = 0.05 / this["Distance between loading edges, mm"];
                        double delta_m = 0.001 / this["Loading mass difference, kg"];
                        double delta_L = 0.0014;
                        double delta_l = 0.0119;
                        double delta_P_squared = Math.Pow(delta_m, 2) +
                            Math.Pow(delta_L, 2) + Math.Pow(delta_l, 2);
                        double delta_A = this["Line slope Relative error"];
                        return Math.Sqrt(Math.Pow(3 * delta_h, 2) + delta_P_squared +
                            Math.Pow(delta_a, 2) + Math.Pow(delta_A, 2));
                    }),
                    Parameter.CreateFunction("EPOC Error", true, delegate()
                    {
                        return this["Effective piezooptical coefficient, Brewsters"] *
                            this["EPOC Relative error"];
                        //double deltaP = this["Gravitational Acceleration, m/s^2"] *
                        //    (0.001 * this["Lever Amplification (mechanical advantage)"] +
                        //    0.01 * this["Loading mass difference, kg"]);
                        //double deltaD = 12 * 180 * (0.01 * this["Loading force, P"] +
                        //    deltaP * this["Distance between loading edges, mm"]);
                        //double D = 12 * 180 * this["Loading force, P"] *
                        //    this["Distance between loading edges, mm"];
                        //double deltaN = 0.015 * this["Wavelength, nm"] *
                        //    Math.Pow(this["Sample's height, mm"], 2);
                        //double N = this["Wavelength, nm"] *
                        //    Math.Pow(this["Sample's height, mm"], 3);
                        //double deltaF = (N / D) * ((Math.Abs(deltaN / N) +
                        //    Math.Abs(deltaD / D)) / (1 - Math.Abs(deltaD / D)));
                        //return this["Line slope, deg"] * deltaF +
                        //    this["Standard error, deg/mm"] + this["Factor"];
                    }),
                    Parameter.CreateFunction("Elastic term, Brewsters", false, delegate()
                    {
                        return (this["Initial refractive index"] -
                            this["Refractive index of environment"]) *
                            this["Elastic Complianse Coefficient, Brewsters"];
                    }),
                    Parameter.CreateFunction("Elastic term Relative error", false, delegate()
                    {
                        double delta_n = 0.001 / this["Initial refractive index"];
                        double delta_S = 0.003 / this["Elastic Complianse Coefficient, Brewsters"];
                        return Math.Sqrt(Math.Pow(delta_S, 2) + Math.Pow(delta_n, 2));
                    }),
                    Parameter.CreateFunction("Elastic term Error", false, delegate()
                    {
                        return this["Elastic term, Brewsters"] * this["Elastic term Relative error"];
                    }),
                    Parameter.CreateFunction("Stress-optic coefficient, Brewsters", true, delegate()
                    {
                        return this["Effective piezooptical coefficient, Brewsters"] -
                            this["Elastic term, Brewsters"];
                    }),
                    Parameter.CreateFunction("SOC Error", true, delegate()
                    {
                        return Math.Sqrt(Math.Pow(this["EPOC Error"], 2) +
                            Math.Pow(this["Elastic term Error"], 2));
                        //return this["EPOC Error"] + 0.001 *
                        //    (Math.Abs(this["Initial refractive index"]) + 
                        //    Math.Abs(this["Stress-optic coefficient, Brewsters"]));
                    }),
                    Parameter.CreateFunction("SOC Relative error", false, delegate()
                    {
                        return this["SOC Error"] / this["Stress-optic coefficient, Brewsters"];
                    }),
                    Parameter.CreateFunction("Piezooptic coefficient, Brewsters", true, delegate()
                    {
                        return -2 * this["Stress-optic coefficient, Brewsters"] /
                            Math.Pow(this["Initial refractive index"], 3);
                    }),
                    Parameter.CreateFunction("POC Relative error", false, delegate()
                    {
                        double delta_n = 0.001 / this["Initial refractive index"];
                        return Math.Sqrt(Math.Pow(this["SOC Relative error"], 2) +
                            Math.Pow(3 * delta_n, 2));
                    }),
                    Parameter.CreateFunction("POC Error", true, delegate()
                    {
                        return this["Piezooptic coefficient, Brewsters"] * this["POC Relative error"];
                        //double a = Math.Abs(this["Piezooptic coefficient, Brewsters"]);
                        //double b = Math.Abs(this["SOC error"] /
                        //    this["Stress-optic coefficient, Brewsters"]);
                        //double c = Math.Abs(0.003 * Math.Pow(this["Initial refractive index"], 2));
                        //return a * (b + c) / (1 - c);
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
                Recalculate();
            }

            /// <summary>
            /// Exports calculation results to a file
            /// </summary>
            /// <param name="fileName">Full file path</param>
            public void Export(string fileName, string crystal, string cut, string loadDirection)
            {
                StringBuilder contents = new StringBuilder();
                contents.AppendLine(DateTime.Now.ToString());
                contents.AppendLine("Institute of Physical Optics, Lviv");
                contents.AppendLine("Determination of POCs on the Digital Interferometer " +
                    "by four-points bending method");
                contents.AppendLine("Setup parameters:");
                contents.AppendFormat("\tx = {0} nm - Wavelength",
                    this["Wavelength, nm"]).AppendLine();
                contents.AppendFormat("\ta = {0} mm - Distance between loading edges",
                    this["Distance between loading edges, mm"]).AppendLine();
                contents.AppendFormat("\th = {0} - Lever Amplification (mechanical advantage)",
                    this["Lever Amplification (mechanical advantage)"]).AppendLine();
                contents.AppendFormat("\tPolarization: {0}",
                    Program.Package.Name.ToLower().StartsWith("h") ?
                    "horizontal" : "vertical").AppendLine();
                contents.AppendFormat("Sample: crystal {0} , cut {1}", crystal, cut).AppendLine();
                contents.AppendFormat("\tn = {0} - Initial refractive index",
                    this["Initial refractive index"]).AppendLine();
                contents.AppendFormat("\tS = {0} Brewsters - Elastic term",
                    this["Elastic term, Brewsters"]).AppendLine();
                contents.AppendFormat("\th = {0} mm - Sample's height",
                    this["Sample's height, mm"]).AppendLine();
                contents.AppendFormat("\tLoad direction: {0}", loadDirection).AppendLine();
                contents.AppendLine("Results:");
                contents.AppendFormat("\tA = {0} ± {1} deg - Line slope",
                    this["Line slope, deg"], this["Standard error, deg/mm"]).AppendLine();
                contents.AppendFormat("\tC = {0} ± {1} Brewsters - Effective piezooptical coefficient",
                    this["Effective piezooptical coefficient, Brewsters"], this["EPOC Error"])
                    .AppendLine();
                contents.AppendFormat("\tK = {0} ± {1} Brewsters - Stress-optic coefficient",
                    this["Stress-optic coefficient, Brewsters"], this["SOC Error"]).AppendLine();
                contents.AppendFormat("\tπ = {0} ± {1} Brewsters - Piezooptic coefficient",
                    this["Piezooptic coefficient, Brewsters"], this["POC Error"]).AppendLine();
                File.WriteAllText(fileName, contents.ToString());
            }

            /// <summary>
            /// Reads parameters from package directory.
            /// Returns loaded parameters list or
            /// null if file params.prm not found
            /// </summary>
            /// <param name="directoryPath">Full package directory path</param>
            public static ParametersList FromDirectory(string directoryPath)
            {
                string filePath = Path.Combine(directoryPath, "params.prm");
                if (!File.Exists(filePath)) return null;
                ParametersList list = new ParametersList();
                list.Load(filePath);
                return list;
            }

            /// <summary>
            /// Reads parameters from current directory.
            /// Returns loaded parameters list or
            /// null if file params.prm not found
            /// </summary>
            public static ParametersList FromCurrentDirectory()
            {
                if (!File.Exists("params.prm")) return null;
                ParametersList list = new ParametersList();
                list.Load("params.prm");
                return list;
            }
        }

        /// <summary>
        /// POCs with parameters
        /// </summary>
        private ParametersList Parameters { get; set; }

        /// <summary>
        /// Creates form with table of POCs with parameters
        /// </summary>
        public POCsForm(double loadingMassDifference, double lineSlope, double standardError)
        {
            InitializeComponent();
            // try to read from package folder
            Parameters = ParametersList.FromDirectory(Program.Package.DirectoryPath);
            // if not found, try to read from current folder
            if (Parameters == null) Parameters = ParametersList.FromCurrentDirectory();
            // if not found again, create default values
            if (Parameters == null) Parameters = new ParametersList();
            // set instant values
            Parameters.SetValueAnyway("Loading mass difference, kg", loadingMassDifference);
            Parameters.SetValueAnyway("Line slope, deg", lineSlope);
            Parameters.SetValueAnyway("Standard error, deg/mm", standardError);
            // add all rows
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
            // refresh values
            UpdateTable();
        }

        /// <summary>
        /// Set value if possible and recalculate functions
        /// </summary>
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

        /// <summary>
        /// Refresh table values
        /// </summary>
        private void UpdateTable()
        {
            for (int row = 0; row < dataGridView.Rows.Count; row++)
                dataGridView["Value", row].Value = string.Format("{0:F6}",
                    Parameters[dataGridView["Key", row].Value.ToString()]);
        }

        /// <summary>
        /// Load parameters from file
        /// </summary>
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

        /// <summary>
        /// Save parameters to file
        /// </summary>
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Parameters files (*.prm)|*.prm";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                Parameters.Save(dialog.FileName);
        }

        /// <summary>
        /// Export results to file
        /// </summary>
        private void buttonExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog1 = new SaveFileDialog();
            dialog1.Filter = "Text files (*.txt)|*.txt";
            InfoRequestDialog dialog2 = new InfoRequestDialog();
            if (dialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK &&
                dialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Parameters.Export(dialog1.FileName,
                    dialog2.CrystalMaterial, dialog2.Cut, dialog2.LoadDirection);
                MessageBox.Show("Saved to " + Path.GetFileName(dialog1.FileName), "Success");
            }
        }

        /// <summary>
        /// Save parameters to params.prm for next run
        /// </summary>
        private void POCsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Parameters.Save("params.prm");
        }
    }
}
