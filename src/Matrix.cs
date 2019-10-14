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
                    Console.Write($"|\t{matrix[i, j]}\t|");
                }
                Console.WriteLine();
                if (i == matrix.GetLength(0) - 1)
                    Console.WriteLine("-----------------------------------");
            }
        }

        public static int?[,] SimplexResolve(int?[,] matrix)
        {
            ProductionProcess(matrix);

        }

        public static int? ProductionProcess(int?[,] matrix)
        {

            int? comparator = matrix[matrix.GetLength(0), 1];
            int chosenColumn = 0;
            int? chosenLine = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[matrix.GetLength(0), i] < comparator)
                {
                    comparator = matrix[matrix.GetLength(0), i];
                    chosenColumn = i;
                }
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int? comp = matrix[i, matrix.GetLength(1)] / matrix[i, chosenColumn];

                if (comp < chosenLine)
                {
                    chosenLine = comp;
                }
            }
            return chosenLine;
        }

        public static int?[,] TwoPhasesResolve(int?[,] matrix)
        {

        }
    }
}
