using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TrabalhoMarcia.src;

namespace TrabalhoMarcia.src
{
    public class Matrix
    {
        public static int[] artificialVariableColumn = Operations.GetColumnPositions();
        public static int[] artificialAllVariableColumn = Operations.GetAllColumnPositions();
        public static bool simplex = true;
        public static bool infinity = true;

        public static void isInfinity(double?[,] matrix, int chosenColumn)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (matrix[i,chosenColumn] > 0)
                {
                    infinity = false;
                }
            }
        }

        public static bool getInfinity()
        {
            return infinity;
        }

        public static bool isSimplex(string typez)
        {
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
                isInfinity(matrix, chosenColumn);
                if (!infinity)
                {
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
                                    matrixAux[i, j] = matrix[i, j] - (matrix[i, chosenColumn] * matrixAux[chosenLine, j]) ?? default(int);
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
                else
                {
                    verification = false;
                }
            }
            Verify(matrix, artificialAllVariableColumn);
            return matrix;
        }

        public static int? ProductionProcess(double?[,] matrix)
        {
            int chosenColumn = GetChosenColumn(matrix);
            int? line = null;
            double? chosenLine = 999999999;
            double? comp;
            int s = 1;

            if(!simplex)
            {
                s = 2;
            }


            for (int i = 0; i < matrix.GetLength(0) - s; i++)
            {
                if (matrix[i, chosenColumn] == 0)
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
                    if(i != matrix.GetLength(1) - 1)
                    {
                        chosenColumn = i;
                    }
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

        public static void TwoPhasesResolve(double?[,] matrix)
        {
            int?[] artificialVariableLines = Operations.GetLinePositions();
            double[] zLinha = new double[50];
            int avl = 0;

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                while (artificialVariableLines[avl] != null)
                {
                    for (int j = 0; j < matrix.GetLength(0) - 1; j++)
                    {
                        if (j == artificialVariableLines[avl])
                            zLinha[i] = zLinha[i] + matrix[j, i] ?? default(double);
                    }
                    avl++;
                }
                avl = 0;
            }

            for (int i = 0; i < zLinha.Length; i++)
            {
                if (zLinha[i] != 0)
                {
                    zLinha[i] = zLinha[i] * -1;
                }
            }

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if(artificialVariableColumn[i] != 0)
                    zLinha[artificialVariableColumn[i]] = 0;
            }

            double?[,] bigMatrix = new double?[matrix.GetLength(0) + 1, matrix.GetLength(1)];

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    bigMatrix[i, j] = matrix[i, j];
                }
            }

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                bigMatrix[bigMatrix.GetLength(0) - 1, i] = zLinha[i];
            }

            PrintMatrix(bigMatrix);
            double?[,] resolvedMatrix = SimplexResolve(bigMatrix);

            simplex = true;
            double?[,] simplexMatrix = new double?[resolvedMatrix.GetLength(0) - 1, resolvedMatrix.GetLength(1) - (Operations.ColumnCount - 1)];

            for (int i = 0; i < simplexMatrix.GetLength(0); i++)
            {
                int cont = 0;
                for (int j = 0; j < resolvedMatrix.GetLength(1); j++)
                {
                    int test = 0;
                    for (int x = 0; x < Operations.ColumnCount - 1; x++)
                    {
                        if (j != artificialVariableColumn[x])
                        {
                            test++;
                        }
                    }
                    if (test == (Operations.ColumnCount - 1))
                    {
                        simplexMatrix[i, j - cont] = resolvedMatrix[i, j];
                    }
                    else
                    {
                        cont++;
                    }
                }
            }

            if (!infinity)
            {
                SimplexResolve(simplexMatrix);
            }
            double? [,] ToResolveMatrix = SimplexResolve(simplexMatrix);
            Verify(ToResolveMatrix, artificialVariableColumn);
        }

        public static void Verify(double?[,] matrix, int[] columns) 
        {
            int c = 0;
            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    if (i == columns[x])
                    {
                        if (matrix[matrix.GetLength(0) - 1, i] == 0)
                        {
                            for (int j = 0; j < matrix.GetLength(0); j++)
                            {
                                if (matrix[j, i] > 0)
                                {
                                    c++;
                                }
                            }
                        }
                    }
                }
            }
            if(c > 0)
            {
                Console.WriteLine("Matrix possui múltiplas soluções otimas.");
            }
        }
    }
}
