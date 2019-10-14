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

        public static void PrintMatrix(int?[,] matrix)
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

        public static int?[,] SimplexResolve(int?[,] matrix)
        {
            bool verification = false;

            while (verification)
            {
                int chosenColumn = GetChosenColumn(matrix);
                int chosenLine = ProductionProcess(matrix);

                int[,] matrixAux = new int[matrix.GetLength(0), matrix.GetLength(1)];


                int pivo = matrix[chosenLine, chosenColumn] / matrix[chosenLine, chosenColumn] ?? default(int);

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
                    chosenLine++;
                    for (int j = 0; j < matrix.GetLength(1); j++)
                    {
                        matrixAux[chosenLine, j] = (matrix[chosenLine, j] - matrix[chosenLine, chosenColumn]) * pivo ?? default(int);
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
            return matrix;
        }

        public static int ProductionProcess(int?[,] matrix)
        {

            int chosenColumn = GetChosenColumn(matrix);

            int? chosenLine = 0;
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int? comp = matrix[i, matrix.GetLength(1)] / matrix[i, chosenColumn];

                if (comp < chosenLine)
                {
                    chosenLine = comp;
                }
            }
            int line = chosenLine ?? default(int);

            return line;
        }

        public static int GetChosenColumn(int?[,] matrix)
        {
            int? comparator = matrix[matrix.GetLength(0), 1];
            int chosenColumn = 0;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[matrix.GetLength(0), i] < comparator)
                {
                    comparator = matrix[matrix.GetLength(0), i];
                    chosenColumn = i;
                }
            }
            return chosenColumn;
        }

        public static bool VerifyLastLine(int?[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[matrix.GetLength(0), i] < 0)
                    return false;
                return true;
            }
            return true;
        }
    }
}
