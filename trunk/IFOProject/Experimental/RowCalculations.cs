using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IFOProject.DataStructures;

namespace IFOProject.Experimental
{
    public class RowCalculations
    {
        /// <summary>
        /// Row index in pattern
        /// </summary>
        public int Index { get; set; }
        /// <summary>
        /// Actual data bytes
        /// </summary>
        public byte[] Data { get; set; }
        /// <summary>
        /// Selection left edge
        /// </summary>
        public int Begin { get; set; }
        /// <summary>
        /// Selection right edge
        /// </summary>
        public int End { get; set; }
        /// <summary>
        /// Initial approximation
        /// </summary>
        public Coefficients Approximation { get; set; }
        /// <summary>
        /// Final coefficients
        /// </summary>
        public Coefficients Final { get; set; }

        public RowCalculations(int row, byte[] data, int begin, int end)
            : this(row, data, begin, end, DefaultApproximation(data, begin, end)) { }

        public RowCalculations(int row, byte[] data, int begin, int end, Coefficients approximation)
        {
            Index = row;
            Data = data;
            Begin = begin;
            End = end;
            Approximation = approximation;
            Final = Calculate();
        }

        public void Recalculate(Coefficients approximation)
        {
            Approximation = approximation;
            Final = Calculate();
        }

        private Coefficients Calculate()
        {
            const double eps = 0.0001;
            byte[] I = Data.Skip(Begin).Take(End - Begin).ToArray();

            Coefficients next = new Coefficients(Approximation);
            Coefficients prev;

            double f = FunctionF(next, I);
            double mult = 1;

            do
            {
                prev = new Coefficients(next);
                double[,] der2Matrix = FunctionF_der2(prev, I, eps);
                double[,] invDer2Matr = Mathematics.InverseMatrix(der2Matrix);
                double[] der1Vector = FunctionF_der1(prev, I, eps);
                double[] change = Mathematics.MatrVecMult(4, invDer2Matr, der1Vector);
                next = prev - new Coefficients(change) * mult;
                double newF = FunctionF(next, I);
                if (double.IsNaN(newF) || newF > f)
                {
                    if (mult > eps) mult /= 2;
                    else break;
                    Coefficients temp = prev;
                    prev = next;
                    next = temp;
                }
                else f = newF;
            }
            while (!Converged(prev, next, eps, I));

            return next;
        }

        public Double Phase
        {
            get { return (360 * ((Begin + End) / 2.0 - Final.InitialPhase) / Final.Period); }
        }
        
        // перевірка умови збіжності
        bool Converged(Coefficients first, Coefficients second, double eps, byte[] I)
        {
            /*double sum = 0;
            for (int i = 0; i < 4; i++)
                sum += (first[i] - second[i]) * (first[i] - second[i]);
            return Math.Sqrt(sum) < (eps * 0.1);*/

            return (Math.Abs(FunctionF(first, I) - FunctionF(second, I)) < eps);
        }

        /// <summary>
        /// Обраховує матрицю других похідних цільової функції
        /// </summary>
        /// <param name="a">4 коефіцієнти функції</param>
        /// <param name="I">Інтенсивності в кожній точці виділеного рядка</param>
        /// <param name="eps">Точність</param>
        /// <returns>Матриця других похідних цільової функції</returns>
        double[,] FunctionF_der2(Coefficients a, byte[] I, double eps)
        {
            double[,] result = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = i; j < 4; j++)
                {
                    double[] b = a.ToArray();
                    b[j] += eps;
                    result[i, j] = (FunctionF_der1(new Coefficients(b), I, eps)[i] - FunctionF_der1(a, I, eps)[i]) / eps;
                    result[j, i] = result[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// Обраховує вектор перших похідних цільової функції
        /// </summary>
        /// <param name="a">4 коефіцієнти функції</param>
        /// <param name="I">Інтенсивності в кожній точці виділеного рядка</param>
        /// <param name="eps">Точність</param>
        /// <returns>Вектор перших похідних цільової функції</returns>
        double[] FunctionF_der1(Coefficients a, byte[] I, double eps)
        {
            double[] result = new double[4];
            for (int i = 0; i < 4; i++)
            {
                double[] b = a.ToArray();
                b[i] += eps;
                result[i] = (FunctionF(new Coefficients(b), I) - FunctionF(a, I)) / eps;
            }
            return result;
        }

        /// <summary>
        /// Обраховує значення суми квадратів відхилень
        /// </summary>
        /// <param name="a">4 коефіцієнти функції</param>
        /// <param name="I">Інтенсивності в кожній точці виділеного рядка</param>
        /// <returns>Значення суми квадратів відхилень</returns>
        public double FunctionF(Coefficients a, byte[] I)
        {
            double result = 0;
            for (int i = 0; i < I.Length; i++)
            {
                double val = (I[i] - FunctionI(a, Begin + i));
                result += val * val;
            }
            return result;
        }

        /// <summary>
        /// Обраховує інтенсивність
        /// </summary>
        /// <param name="a">4 коефіцієнти функції</param>
        /// <param name="x">Координата Х у виділеному рядку</param>
        /// <returns>Значення інтенсивності</returns>
        public static double FunctionI(Coefficients a, int x)
        {
            return a.MeanLevel + a.Amplitude * Math.Cos(2 * Math.PI * (x - a.InitialPhase) / a.Period);
        }

        public static Coefficients DefaultApproximation(byte[] data, int begin, int end)
        {
            const int step = 5;
            const int smoothRadius = 20;

            int size = 1 + (end - begin) / step;
            int[] smoothed = new int[size];
            int min = int.MaxValue;
            int max = int.MinValue;

            List<int> minima = new List<int>();
            List<int> minI = new List<int>();
            List<int> maxima = new List<int>();
            List<int> maxI = new List<int>();

            // заповнення зрідженого масиву інтенсивностей, умовних масивів максимумів та мінімумів
            for (int i = 0; i < size; i++)
            {
                smoothed[i] = PointSmoothing(data, begin + i * step, smoothRadius);
                if (i > 1 && smoothed[i - 1] <= smoothed[i - 2] && smoothed[i - 1] < smoothed[i])
                {
                    minI.Add(smoothed[i - 1]);
                    minima.Add(begin + (i - 1) * step);
                }
                if (i > 1 && smoothed[i - 1] >= smoothed[i - 2] && smoothed[i - 1] > smoothed[i])
                {
                    maxI.Add(smoothed[i - 1]);
                    maxima.Add(begin + (i - 1) * step);
                }
                if (smoothed[i] < min) min = smoothed[i];
                if (smoothed[i] > max) max = smoothed[i];
            }

            // формування початкових наближень
            Coefficients result = new Coefficients();
            result.MeanLevel = smoothed.Average();
            result.Amplitude = (max - min) * 0.5;

            int middle = (begin + end) / 2;
            if (maxima.Count > 1)
            {
                result.InitialPhase = maxima[0];
                int distance = Math.Abs(middle - maxima[0]);
                foreach (int current in maxima)
                {
                    if (Math.Abs(middle - current) < distance)
                    {
                        result.InitialPhase = current;
                        distance = Math.Abs(middle - current);
                    }
                }
                result.Period = (maxima[maxima.Count - 1] - maxima[0]) / (double)(maxima.Count - 1);
            }
            else if (minima.Count > 1)
            {
                result.InitialPhase = maxima[0];
                result.Period = (minima[minima.Count - 1] - minima[0]) / (double)(minima.Count - 1);
            }
            else if (maxima.Count > 0 && minima.Count > 0)
            {
                result.InitialPhase = maxima[0];
                result.Period = Math.Abs(maxima[0] - minima[0]) * 2;
            }
            else
            {
                return new Coefficients();
            }

            result.Amplitude = 2 * Math.PI * smoothRadius * result.Amplitude / (result.Period * Math.Sin(2 * Math.PI * smoothRadius / result.Period));

            /*if (smoothRadius * everyNPixel >= result[2]) return InitialValues(row, begin, end, everyNPixel, (int)(result[2] / (8 * everyNPixel)));
            if (smoothRadius == 0) return InitialValues(row, begin, end, everyNPixel - 1, smoothRadius);*/

            return result;
        }

        private static byte PointSmoothing(byte[] data, int position, int radius)
        {
            List<int> selected = new List<int>();
            for (int x = position - radius; x <= position + radius; x++)
            {
                if (x < 0 || x >= data.Length) radius--;
                else selected.Add(data[x]);
            }
            return Convert.ToByte(selected.Average());
        }

        /// <summary>
        /// Discrepancy between the data and an estimation model
        /// </summary>
        /// <param name="setup">Coefficients to compare with actual data</param>
        /// <returns>Sum of squares of residuals divided by their number</returns>
        public double ResidualSquareSum(Coefficients setup)
        {
            byte[] I = Data.Skip(Begin).Take(End - Begin).ToArray();
            return FunctionF(setup, I) / I.Length;
        }
    }
}
