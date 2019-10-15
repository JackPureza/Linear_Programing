using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TrabalhoMarcia.src
{
    public class Operations
    {
        public static int numberOfVariables = 0;
        public static int numberOfRestrictions = 0;
        public static int[] restrictionsSignal;
        public static int?[] linePosition = new int?[50];
        public static int[] columnPosition = new int[50];
        public static int artificialLineCount = 1;
        public static int ColumnCount = 1;
        public static string GetTypeZ()
        {
            Console.WriteLine("Para Zmax, digite 1, para Zmin digite 2:");
            var Ztipo = Console.ReadLine();
            return Ztipo;
        }

        public static void SetNumberOfVariables()
        {
            Console.WriteLine("Digite a quantidade de variaveis que será inserida");
            numberOfVariables = int.Parse(Console.ReadLine());
        }

        public static int GetNumberOfVariables()
        {
            return numberOfVariables;
        }

        public static void SetNumberOfRestrictions()
        {
            Console.WriteLine("Digite a quantidade de restrições que será inserida");
            numberOfRestrictions = int.Parse(Console.ReadLine());
        }

        public static int GetNumberOfRestrictions()
        {
            return numberOfRestrictions;
        }

        public static void SetRestrictionsSignal()
        {
            restrictionsSignal = new int[numberOfRestrictions];
            for (int i = 0; i < numberOfRestrictions; i++)
            {
                Console.WriteLine($"Digite o sinal da {i + 1}ª restrição:");
                string sinal = Console.ReadLine();

                if (sinal == "<=")
                {
                    restrictionsSignal[i] = 1;
                }
                if (sinal == "=")
                {
                    restrictionsSignal[i] = 2;
                }
                if (sinal == ">=")
                {
                    restrictionsSignal[i] = 3;
                }
            }
        }

        public static int[] GetRestrictionsSignal()
        {
            return restrictionsSignal;
        }

        public static double[] GetZ(string typeZ)
        {
            int numberofVariables = GetNumberOfVariables();
            int numberofRestrictions = GetNumberOfRestrictions();
            double[] z = new double[numberofVariables + numberofRestrictions + 1];
            for (int i = 0; i < numberofVariables; i++)
            {
                Console.WriteLine($"Digite o valor para x{i + 1} de Z:");
                double number = double.Parse(Console.ReadLine());
                z[i] = number;
            }

            //Console.WriteLine("Digite o valor do resultado de Z");
            //int result = int.Parse(Console.ReadLine());

            if (typeZ == "1")
            {
                for (int i = 0; i < numberofVariables; i++)
                {
                    z[i] = z[i] * -1;
                }
                //  result = result * -1;
            }

            //z[z.Length] = result;
            return z;
        }

        public static double[,] GetMatrixOfVaribles()
        {
            int numberOfRestrictions = GetNumberOfRestrictions();
            int numberOfVariables = GetNumberOfVariables();
            double[,] expression = new double[numberOfRestrictions, numberOfVariables];

            for (int i = 0; i < numberOfRestrictions; i++)
            {
                for (int j = 0; j < numberOfVariables; j++)
                {
                    Console.WriteLine($"Digite o numero da {j + 1}ª variavel da {i + 1}ª expressão:");
                    expression[i, j] = double.Parse(Console.ReadLine());
                }
            }

            return expression;
        }

        public static double?[,] GetMatrixOfRestrictions()
        {
            int count = 0;
            int countBiggerThen = 0;
            string[] restricao = new string[numberOfRestrictions];
            SetRestrictionsSignal();
            int[] restrictionsSignal = GetRestrictionsSignal();
            for (int i = 0; i < numberOfRestrictions; i++)
            {
                if (restrictionsSignal[i] == 1)
                {
                    count++;
                    restricao[i] = $"1f{i}";
                }
                if (restrictionsSignal[i] == 2)
                {
                    count++;
                    restricao[i] = $"1a{i}";
                }
                if (restrictionsSignal[i] == 3)
                {
                    count = count + 2;
                    restricao[i] = $"-1f{i}+1a{i}";
                }
            }

            double?[,] matrix = new double?[numberOfRestrictions, count];

            for (int i = 0; i < numberOfRestrictions; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i == j)
                    {
                        switch (restrictionsSignal[i])
                        {
                            case 1:
                                if (countBiggerThen != 0)
                                {
                                    matrix[i, j] = 0;
                                    matrix[i, j + countBiggerThen] = double.Parse(restricao[i].Substring(0, restricao[i].IndexOf("f")));
                                }
                                else
                                {
                                    matrix[i, j] = double.Parse(restricao[i].Substring(0, restricao[i].IndexOf("f")));
                                }
                                break;
                            case 2:
                                if (countBiggerThen != 0)
                                {
                                    matrix[i, j] = 0;
                                    matrix[i, j + countBiggerThen] = double.Parse(restricao[i].Substring(0, restricao[i].IndexOf("a")));
                                    SetColumnPosition(j+countBiggerThen);
                                    SetLinePosition(i);
                                }
                                else
                                {
                                    matrix[i, j] = double.Parse(restricao[i].Substring(0, restricao[i].IndexOf("a")));
                                    SetColumnPosition(j);
                                    SetLinePosition(i);
                                }
                                break;
                            case 3:
                                if (countBiggerThen != 0)
                                {
                                    matrix[i, j] = 0;
                                    matrix[i, j + countBiggerThen] = double.Parse(restricao[i].Substring(0, restricao[i].IndexOf("f")));
                                    restricao[i] = restricao[i].Remove(0, restricao[i].IndexOf("+"));
                                    matrix[i, j + countBiggerThen + 1] = double.Parse(restricao[i].Substring(0, restricao[i].IndexOf("a")));
                                    SetColumnPosition(j + countBiggerThen+1);
                                    SetLinePosition(i);
                                }
                                else
                                {
                                    matrix[i, j] = double.Parse(restricao[i].Substring(0, restricao[i].IndexOf("f")));
                                    restricao[i] = restricao[i].Remove(0, restricao[i].IndexOf("+"));
                                    matrix[i, j + 1] = double.Parse(restricao[i].Substring(0, restricao[i].IndexOf("a")));
                                    SetColumnPosition(j + 1);
                                    SetLinePosition(i);
                                }
                                countBiggerThen++;
                                break;
                        }
                    }
                    else if (matrix[i, j] == null)
                    {
                        matrix[i, j] = 0;
                    }
                }
            }
            return matrix;
        }

        public static double?[,] MergeMatrices(double[,] matrixVariables, double?[,] matrixRestrictions)
        {
            double?[,] newMatrix = new double?[matrixRestrictions.GetLength(0), matrixVariables.GetLength(1) + matrixRestrictions.GetLength(1)];

            for (int i = 0; i < matrixVariables.GetLength(0); i++)
            {
                for (int j = 0; j < matrixVariables.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrixVariables[i, j];
                }
            }
            for (int i = 0; i < matrixRestrictions.GetLength(0); i++)
            {
                for (int j = 0; j < matrixRestrictions.GetLength(1); j++)
                {
                    newMatrix[i, matrixVariables.GetLength(1) + j] = matrixRestrictions[i, j];
                }
            }
            return newMatrix;
        }

        public static double?[,] FinalMatrix(double?[,] matrix, double[] z)
        {
            double?[,] newMatrix = new double?[matrix.GetLength(0) + 1, matrix.GetLength(1) + 1];

            double[] result = GetResult(newMatrix.GetLength(0) - 1);

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                newMatrix[i, matrix.GetLength(1)] = result[i];
            }

            for (int i = 0; i < result.Length; i++)
            {
                newMatrix[newMatrix.GetLength(0) - 1, i] = z[i];
            }
            for (int i = result.Length; i < newMatrix.GetLength(1); i++)
            {
                newMatrix[newMatrix.GetLength(0) - 1, i] = 0;
            }
            return newMatrix;
        }

        public static double[] GetResult(int height)
        {
            double[] results = new double[height];
            for (int i = 0; i < height; i++)
            {
                Console.WriteLine($"Digite o resultado da {i + 1}ª limitante:");
                results[i] = double.Parse(Console.ReadLine());

            }

            return results;
        }

        public static void SetLinePosition(int pos)
        {
            for (int i = 0; i < artificialLineCount; i++)
            {
                if (linePosition[i] == null)
                    linePosition[i] = pos;
            }
            artificialLineCount++;
        }

        public static int?[] GetLinePositions()
        {
            return linePosition;
        }

        public static void SetColumnPosition(int pos)
        {
            
            for (int i = 0; i < ColumnCount; i++)
            {
                if (columnPosition[i] == 0)
                    columnPosition[i] = pos + numberOfVariables;
            }
            ColumnCount++;
        }

        public static int[] GetColumnPositions()
        {
            return columnPosition;
        }
    }
}
