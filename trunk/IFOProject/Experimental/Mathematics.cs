using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFOProject.Experimental
{
    class Mathematics
    {
        public static double[,] InverseMatrix(double[,] matrix)
        {
            int num = Convert.ToInt32(Math.Sqrt(matrix.Length));

            double[,] result = new double[num, num];
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    if (i == j) result[i, j] = 1;
                    else result[i, j] = 0;
                }
            }

            double[,] matrixT = new double[num, num];

            // прямий хід за Гаусом

            for (int k = 0; k < num; k++)
            {
                // заповнення матриці переходу як одиничної
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        if (i == j) matrixT[i, j] = 1;
                        else matrixT[i, j] = 0;
                    }
                }

                // дозаповнення ходового стовпця матриці переходу
                for (int i = k; i < num; i++)
                {
                    if (i == k) matrixT[i, k] = 1.0 / matrix[i, k];
                    else matrixT[i, k] = -matrix[i, k] / matrix[k, k];
                }

                // множення матриці і на матрицю переходу зліва і заповнення оберненої матриці
                matrix = MatrMult(num, matrixT, matrix);
                result = MatrMult(num, matrixT, result);
            }

            // оберенений хід за Гаусом

            for (int k = num - 1; k >= 0; k--)
            {
                // заповнення матриці переходу як одиничної
                for (int i = 0; i < num; i++)
                {
                    for (int j = 0; j < num; j++)
                    {
                        if (i == j) matrixT[i, j] = 1;
                        else matrixT[i, j] = 0;
                    }
                }

                // дозаповнення ходового стовпця матриці переходу
                for (int i = k - 1; i >= 0; i--)
                {
                    if (i != k) matrixT[i, k] = -matrix[i, k];
                }

                // множення матриці і вектора на матрицю переходу зліва
                matrix = MatrMult(num, matrixT, matrix);
                result = MatrMult(num, matrixT, result);
            }

            return result;
        }

        // множення двох матриць
        public static double[,] MatrMult(int dim, double[,] first, double[,] second)
        {
            double[,] result = new double[dim, dim];
            for (int i = 0; i < dim; i++)
            {
                for (int j = 0; j < dim; j++)
                {
                    double sum = 0;
                    for (int l = 0; l < dim; l++)
                    {
                        if (((first[i, l] == double.MaxValue) && (second[l, j] == double.MaxValue)) || ((first[i, l] == double.MinValue) && (second[l, j] == double.MinValue)))
                            sum += double.MaxValue;
                        else if (((first[i, l] == double.MaxValue) && (second[l, j] == double.MinValue)) || ((first[i, l] == double.MinValue) && (second[l, j] == double.MaxValue)))
                            sum += double.MinValue;
                        else if (((first[i, l] == double.MaxValue) && (second[l, j] != 0)) || ((second[l, j] == double.MaxValue) && (first[i, l] != 0)))
                            sum += double.MaxValue;
                        else if (((first[i, l] == double.MinValue) && (second[l, j] != 0)) || ((second[l, j] == double.MinValue) && (first[i, l] != 0)))
                            sum += double.MinValue;
                        else sum += first[i, l] * second[l, j];
                    }
                    result[i, j] = sum;
                    if (result[i, j] == double.PositiveInfinity) result[i, j] = double.MaxValue;
                    if (result[i, j] == double.NegativeInfinity) result[i, j] = double.MinValue;
                }
            }
            return result;
        }

        // множення матриці на вектор
        public static double[] MatrVecMult(int dim, double[,] first, double[] second)
        {
            double[] result = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                double sum = 0;
                for (int j = 0; j < dim; j++)
                {
                    if (((first[i, j] == double.MaxValue) && (second[j] == double.MaxValue)) || ((first[i, j] == double.MinValue) && (second[j] == double.MinValue)))
                        sum += double.MaxValue;
                    else if (((first[i, j] == double.MaxValue) && (second[j] == double.MinValue)) || ((first[i, j] == double.MinValue) && (second[j] == double.MaxValue)))
                        sum += double.MinValue;
                    else if (((first[i, j] == double.MaxValue) && (second[j] != 0)) || ((second[j] == double.MaxValue) && (first[i, j] != 0)))
                        sum += double.MaxValue;
                    else if (((first[i, j] == double.MinValue) && (second[j] != 0)) || ((second[j] == double.MinValue) && (first[i, j] != 0)))
                        sum += double.MinValue;
                    else sum += first[i, j] * second[j];
                }
                result[i] = sum;
                if (result[i] == double.PositiveInfinity) result[i] = double.MaxValue;
                if (result[i] == double.NegativeInfinity) result[i] = double.MinValue;
            }
            return result;
        }
    }
}
