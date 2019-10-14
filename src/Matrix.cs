using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TrabalhoMarcia.src;

namespace TrabalhoMarcia.src
{
    public class Matrix
    {
        public static bool isSimplex(string typez)
        {
            bool simplex = true;
            int[] restrictionsSignal = Operations.GetRestrictionsSignal();

            if (typez == "1")
            {
                foreach (int signal in restrictionsSignal)
                {
                    if (signal != 1)
                    {
                        simplex = false;
                    }
                }
            }
            else
            {
                simplex = false;
            }
            return simplex;
        }

        public static void PrintMatrix(double?[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.WriteLine("-----------------------------------");
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"| {matrix[i, j]} |");
                }
                Console.WriteLine();
                if (i == matrix.GetLength(0) - 1)
                    Console.WriteLine("-----------------------------------");

            }
        }

        public static double?[,] SimplexResolve(double?[,] matrix)
        {
            bool verification = true;

            while (verification)
            {
                int chosenColumn = GetChosenColumn(matrix);
                int? chosenL = ProductionProcess(matrix);
                if (chosenL != null)
                {
                    int chosenLine = Convert.ToInt32(chosenL);
                    double[,] matrixAux = new double[matrix.GetLength(0), matrix.GetLength(1)];


                    double pivo = matrix[chosenLine, chosenColumn] / matrix[chosenLine, chosenColumn] ?? default(int);

                    matrixAux[chosenLine, chosenColumn] = pivo;

                    for (int i = 0; i < matrix.GetLength(1); i++)
                    {
                        if (i != chosenColumn)
                        {
                            matrixAux[chosenLine, i] = matrix[chosenLine, i] / matrix[chosenLine, chosenColumn] ?? default(int);
                        }
                    }

                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        if (i != chosenLine)
                        {
                            for (int j = 0; j < matrix.GetLength(1); j++)
                            {
                                matrixAux[i, j] = matrix[i, j] - (matrix[i, chosenColumn] * matrix[chosenLine, j]) ?? default(int);
                            }
                        }
                    }

                    for (int i = 0; i < matrix.GetLength(0); i++)
                    {
                        for (int j = 0; j < matrix.GetLength(1); j++)
                        {
                            matrix[i, j] = matrixAux[i, j];
                        }

                    }
                    PrintMatrix(matrix);
                    verification = VerifyLastLine(matrix);
                }
                else
                {
                    verification = false;
                }
            }
            return matrix;
        }

        public static int? ProductionProcess(double?[,] matrix)
        {
            int chosenColumn = GetChosenColumn(matrix);
            int? line = null;
            double? chosenLine = 999999999;
            double? comp;
            for (int i = 0; i < matrix.GetLength(0) - 1; i++)
            {
                if (matrix[i,chosenColumn] == 0)
                {
                    comp = 999999999;
                }
                else
                {
                    comp = matrix[i, matrix.GetLength(1) - 1] / matrix[i, chosenColumn];
                }

                if (comp < chosenLine && comp >= 0)
                {
                    chosenLine = comp;
                    line = i;
                }
            }

            return line;
        }

        public static int GetChosenColumn(double?[,] matrix)
        {
            double? comparator = 999999;
            int chosenColumn = -1;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[matrix.GetLength(0) - 1, i] < comparator)
                {
                    comparator = matrix[matrix.GetLength(0) - 1, i];
                    chosenColumn = i;
                }
            }
            return chosenColumn;
        }

        public static bool VerifyLastLine(double?[,] matrix)
        {
            bool verify = false;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[matrix.GetLength(0) - 1, i] < 0)
                    verify = true;
            }

            return verify;
        }
    }
}
