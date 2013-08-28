using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IFOProject.Experimental
{
    class Mathematics
    {
        private static double Determinant(
            double a11, double a12, double a13,
            double a21, double a22, double a23,
            double a31, double a32, double a33)
        {
            return a11 * (a22 * a33 - a23 * a32) -
                a12 * (a21 * a33 - a23 * a31) +
                a13 * (a21 * a32 - a22 * a31);
        }

        public static double[,] InverseMatrix4x4m(double[,] matrix)
        {
            double det = matrix[0, 0] * Determinant(matrix[1, 1], matrix[1, 2], matrix[1, 3], matrix[2, 1], matrix[2, 2], matrix[2, 3], matrix[3, 1], matrix[3, 2], matrix[3, 3]) -
                  matrix[0, 1] * Determinant(matrix[1, 0], matrix[1, 2], matrix[1, 3], matrix[2, 0], matrix[2, 2], matrix[2, 3], matrix[3, 0], matrix[3, 2], matrix[3, 3]) +
                  matrix[0, 2] * Determinant(matrix[1, 0], matrix[1, 1], matrix[1, 3], matrix[2, 0], matrix[2, 1], matrix[2, 3], matrix[3, 0], matrix[3, 1], matrix[3, 3]) -
                  matrix[0, 3] * Determinant(matrix[1, 0], matrix[1, 1], matrix[1, 2], matrix[2, 0], matrix[2, 1], matrix[2, 2], matrix[3, 0], matrix[3, 1], matrix[3, 2]);

            double[,] inversed = new double[4, 4];

            inversed[0, 0] = Determinant(matrix[1, 1], matrix[1, 2], matrix[1, 3], matrix[2, 1], matrix[2, 2], matrix[2, 3], matrix[3, 1], matrix[3, 2], matrix[3, 3]) / det;
            inversed[0, 1] = -Determinant(matrix[1, 0], matrix[1, 2], matrix[1, 3], matrix[2, 0], matrix[2, 2], matrix[2, 3], matrix[3, 0], matrix[3, 2], matrix[3, 3]) / det;
            inversed[0, 2] = Determinant(matrix[1, 0], matrix[1, 1], matrix[1, 3], matrix[2, 0], matrix[2, 1], matrix[2, 3], matrix[3, 0], matrix[3, 1], matrix[3, 3]) / det;
            inversed[0, 3] = -Determinant(matrix[1, 0], matrix[1, 1], matrix[1, 2], matrix[2, 0], matrix[2, 1], matrix[2, 2], matrix[3, 0], matrix[3, 1], matrix[3, 2]) / det;

            inversed[1, 0] = -Determinant(matrix[0, 1], matrix[0, 2], matrix[0, 3], matrix[2, 1], matrix[2, 2], matrix[2, 3], matrix[3, 1], matrix[3, 2], matrix[3, 3]) / det;
            inversed[1, 1] = Determinant(matrix[0, 0], matrix[0, 2], matrix[0, 3], matrix[2, 0], matrix[2, 2], matrix[2, 3], matrix[3, 0], matrix[3, 2], matrix[3, 3]) / det;
            inversed[1, 2] = -Determinant(matrix[0, 0], matrix[0, 1], matrix[0, 3], matrix[2, 0], matrix[2, 1], matrix[2, 3], matrix[3, 0], matrix[3, 1], matrix[3, 3]) / det;
            inversed[1, 3] = Determinant(matrix[0, 0], matrix[0, 1], matrix[0, 2], matrix[2, 0], matrix[2, 1], matrix[2, 2], matrix[3, 0], matrix[3, 1], matrix[3, 2]) / det;

            inversed[2, 0] = Determinant(matrix[0, 1], matrix[0, 2], matrix[0, 3], matrix[1, 1], matrix[1, 2], matrix[1, 3], matrix[3, 1], matrix[3, 2], matrix[3, 3]) / det;
            inversed[2, 1] = -Determinant(matrix[0, 0], matrix[0, 2], matrix[0, 3], matrix[1, 0], matrix[1, 2], matrix[1, 3], matrix[3, 0], matrix[3, 2], matrix[3, 3]) / det;
            inversed[2, 2] = Determinant(matrix[0, 0], matrix[0, 1], matrix[0, 3], matrix[1, 0], matrix[1, 1], matrix[1, 3], matrix[3, 0], matrix[3, 1], matrix[3, 3]) / det;
            inversed[2, 3] = -Determinant(matrix[0, 0], matrix[0, 1], matrix[0, 2], matrix[1, 0], matrix[1, 1], matrix[1, 2], matrix[3, 0], matrix[3, 1], matrix[3, 2]) / det;

            inversed[3, 0] = -Determinant(matrix[0, 1], matrix[0, 2], matrix[0, 3], matrix[1, 1], matrix[1, 2], matrix[1, 3], matrix[2, 1], matrix[2, 2], matrix[2, 3]) / det;
            inversed[3, 1] = Determinant(matrix[0, 0], matrix[0, 2], matrix[0, 3], matrix[1, 0], matrix[1, 2], matrix[1, 3], matrix[2, 0], matrix[2, 2], matrix[2, 3]) / det;
            inversed[3, 2] = -Determinant(matrix[0, 0], matrix[0, 1], matrix[0, 3], matrix[1, 0], matrix[1, 1], matrix[1, 3], matrix[2, 0], matrix[2, 1], matrix[2, 3]) / det;
            inversed[3, 3] = Determinant(matrix[0, 0], matrix[0, 1], matrix[0, 2], matrix[1, 0], matrix[1, 1], matrix[1, 2], matrix[2, 0], matrix[2, 1], matrix[2, 2]) / det;

            return inversed;
        }

        public static double[,] InverseMatrix4x4(double[,] matrix)
        {
            double a11 = matrix[0, 0];
            double a12 = matrix[0, 1];
            double a13 = matrix[0, 2];
            double a14 = matrix[0, 3];
            double a21 = matrix[1, 0];
            double a22 = matrix[1, 1];
            double a23 = matrix[1, 2];
            double a24 = matrix[1, 3];
            double a31 = matrix[2, 0];
            double a32 = matrix[2, 1];
            double a33 = matrix[2, 2];
            double a34 = matrix[2, 3];
            double a41 = matrix[3, 0];
            double a42 = matrix[3, 1];
            double a43 = matrix[3, 2];
            double a44 = matrix[3, 3];

            double det = a11 * Determinant(a22, a23, a24, a32, a33, a34, a42, a43, a44) -
                  a12 * Determinant(a21, a23, a24, a31, a33, a34, a41, a43, a44) +
                  a13 * Determinant(a21, a22, a24, a31, a32, a34, a41, a42, a44) -
                  a14 * Determinant(a21, a22, a23, a31, a32, a33, a41, a42, a43);

            double[,] inversed = new double[4, 4];
        
            inversed[0, 0] = Determinant(a22, a23, a24, a32, a33, a34, a42, a43, a44) / det;
            inversed[0, 1] = -Determinant(a21, a23, a24, a31, a33, a34, a41, a43, a44) / det;
            inversed[0, 2] = Determinant(a21, a22, a24, a31, a32, a34, a41, a42, a44) / det;
            inversed[0, 3] = -Determinant(a21, a22, a23, a31, a32, a33, a41, a42, a43) / det;

            inversed[1, 0] = -Determinant(a12, a13, a14, a32, a33, a34, a42, a43, a44) / det;
            inversed[1, 1] = Determinant(a11, a13, a14, a31, a33, a34, a41, a43, a44) / det;
            inversed[1, 2] = -Determinant(a11, a12, a14, a31, a32, a34, a41, a42, a44) / det;
            inversed[1, 3] = Determinant(a11, a12, a13, a31, a32, a33, a41, a42, a43) / det;

            inversed[2, 0] = Determinant(a12, a13, a14, a22, a23, a24, a42, a43, a44) / det;
            inversed[2, 1] = -Determinant(a11, a13, a14, a21, a23, a24, a41, a43, a44) / det;
            inversed[2, 2] = Determinant(a11, a12, a14, a21, a22, a24, a41, a42, a44) / det;
            inversed[2, 3] = -Determinant(a11, a12, a13, a21, a22, a23, a41, a42, a43) / det;

            inversed[3, 0] = -Determinant(a12, a13, a14, a22, a23, a24, a32, a33, a34) / det;
            inversed[3, 1] = Determinant(a11, a13, a14, a21, a23, a24, a31, a33, a34) / det;
            inversed[3, 2] = -Determinant(a11, a12, a14, a21, a22, a24, a31, a32, a34) / det;
            inversed[3, 3] = Determinant(a11, a12, a13, a21, a22, a23, a31, a32, a33) / det;

            return inversed;
        }

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
