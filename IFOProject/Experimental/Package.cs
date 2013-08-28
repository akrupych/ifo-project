using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using IFOProject.DataStructures;
using System.IO;
using System.Linq;

namespace IFOProject.Experimental
{
    public class Package
    {
        /// <summary>
        /// Package name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Interference patterns list
        /// </summary>
        public List<Pattern> Patterns { get; set; }

        /// <summary>
        /// Gets or sets selected pattern index
        /// </summary>
        public int CurrentIndex { get; set; }

        /// <summary>
        /// Average phase for every calculation row
        /// </summary>
        public double[] AveragePhase { get; private set; }

        /// <summary>
        /// Gets or sets currently selected pattern
        /// </summary>
        public Pattern CurrentPattern
        {
            get
            {
                if (Patterns.Count == 0) return null;
                return Patterns[CurrentIndex];
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public Package()
        {
            Patterns = new List<Pattern>();
        }

        /// <summary>
        /// Adds pattern from image file and sets focus on it
        /// </summary>
        /// <param name="fileName">Full path to image file</param>
        public void Add(string fileName)
        {
            string[] parts = Path.GetFileName(fileName).Split(new char[1] { '_' });
            Name = parts[0];
            Patterns.Add(new Pattern(fileName));
            CurrentIndex = Patterns.Count - 1;
        }

        /// <summary>
        /// Adds multiple patterns from image files
        /// </summary>
        /// <param name="fileNames">Full pathes to image files</param>
        public void Add(string[] fileNames)
        {
            foreach (string name in fileNames) Add(name);
            CurrentIndex = 0;
        }

        /// <summary>
        /// Removes current pattern from package
        /// </summary>
        public bool Remove()
        {
            if (Patterns.Count == 0) return false;
            else Patterns.RemoveAt(CurrentIndex);
            if (CurrentIndex >= Patterns.Count)
                CurrentIndex = Patterns.Count - 1;
            return true;
        }

        /// <summary>
        /// Removes all patterns from package
        /// </summary>
        public bool Clear()
        {
            if (Patterns.Count == 0) return false;
            else Patterns.Clear();
            CurrentIndex = 0;
            return true;
        }

        /// <summary>
        /// Calculates all patterns in the package
        /// </summary>
        public void Calculate()
        {
            Calculate(null, null);
        }

        /// <summary>
        /// Calculates all patterns in the package, sending progress updates 
        /// </summary>
        /// <param name="listener">Listener to publish progress</param>
        public void Calculate(IProgressUpdater listener)
        {
            Calculate(listener, null);
        }

        /// <summary>
        /// Calculates package with custom approximation starting with a given pattern
        /// </summary>
        /// <param name="patternIndex">Starting pattern index</param>
        /// <param name="approximation">Initial approximation for the first row</param>
        public void Calculate(int patternIndex, Coefficients approximation)
        {
            Patterns[patternIndex].Calculate(approximation);
            for (int i = patternIndex + 1; i < Patterns.Count; i++)
                Patterns[i].Calculate(Patterns[i - 1].Calculations[0].Final);
        }

        /// <summary>
        /// Calculates package with custom approximation for first pattern
        /// </summary>
        /// <param name="listener">Listener to publish progress</param>
        /// <param name="approximation">Initial approximation for the first row
        /// of the first pattern</param>
        public void Calculate(IProgressUpdater listener, Coefficients approximation)
        {
            for (int i = 0; i < Patterns.Count; i++)
            {
                Pattern current = Patterns[i];
                if (current != CurrentPattern)
                {
                    current.Selection = CurrentPattern.Selection;
                    current.SelectionRowStep = CurrentPattern.SelectionRowStep;
                    if (CurrentPattern.UseSmoothing)
                        current.Smooth(CurrentPattern.SmoothingRadius);
                    current.UseSmoothing = CurrentPattern.UseSmoothing;
                }
                if (i > 0)
                {
                    // get approximation from last pattern results
                    current.Calculate(Patterns[i - 1].Calculations[0].Final);
                }
                else if (approximation != null)
                {
                    // calculate with custom approximation
                    current.Calculate(approximation);
                }
                else
                {
                    // with default approximation
                    current.Calculate();
                }
                if (i == Patterns.Count - 1)
                {
                    // create and fill AveragePhase array
                    int rows = current.Calculations.Length;
                    int images = Patterns.Count;
                    AveragePhase = new double[rows];
                    double sum = 0;
                    for (int row = 0; row < rows; row++, sum = 0)
                    {
                        foreach (Pattern pattern in Patterns)
                            sum += pattern.Calculations[row].Phase;
                        AveragePhase[row] = sum / images;
                    }
                }
                if (listener != null)
                {
                    // send update signal
                    listener.SetProgress((i + 1) * 100 / Patterns.Count);
                }
            }
        }

        /// <summary>
        /// Saves average phase values to file for comparing with next package
        /// </summary>
        public void SaveResults(string fileName)
        {
            // write to file line by line
            File.WriteAllLines(fileName, AveragePhase.Select(item => item.ToString()));
        }

        /// <summary>
        /// Loads saved results from file
        /// </summary>
        /// <returns>Returns saved average phase values</returns>
        public static double[] LoadResults(string fileName)
        {
            return File.ReadAllLines(fileName).Select(item => Double.Parse(item)).ToArray();
        }
    }
}
