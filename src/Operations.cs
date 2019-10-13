﻿using System;
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
                Console.WriteLine($"Digite o sinal da {i}ª restrição:");
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

        public static int[] GetZ(string typeZ)
        {
            int numberofVariables = GetNumberOfVariables();
            int[] z = new int[numberofVariables + 1];
            for (int i = 0; i < numberofVariables; i++)
            {
                Console.WriteLine($"Digite o valor para x{i} de Z:");
                int number = int.Parse(Console.ReadLine());
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

        public static int[,] GetMatrixOfVaribles()
        {
            int numberOfRestrictions = GetNumberOfRestrictions();
            int numberOfVariables = GetNumberOfVariables();
            int[,] expression = new int[numberOfRestrictions, numberOfVariables];

            for (int i = 0; i < numberOfRestrictions; i++)
            {
                for (int j = 0; j < numberOfVariables; j++)
                {
                    Console.WriteLine($"Digite o numero da {j}ª variavel da {i}ª expressão:");
                    expression[i, j] = int.Parse(Console.ReadLine());
                }
            }

            return expression;
        }

        public static int?[,] GetMatrixOfRestrictions()
        {

            int count = 0;
            string[] restricao = new string[numberOfRestrictions];
            SetRestrictionsSignal();
            int[] restrictionsSignal = GetRestrictionsSignal();
            for (int i = 0; i < numberOfRestrictions; i++)
            {
                if (restrictionsSignal == 1)
                {
                    count++;
                    restricao[i] = $"1f{i}";
                }
                if (sinal == 2)
                {
                    count++;
                    restricao[i] = $"1a{i}";
                }
                if (sinal == 3)
                {
                    count = count + 2;
                    restricao[i] = $"-1f{i}+1a{i}";
                }
            }

            int?[,] matrix = new int?[numberOfRestrictions, count];

            for (int i = 0; i < numberOfRestrictions; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i == j)
                    {
                        switch (qualsinal[i])
                        {
                            case 1:
                                matrix[i, j] = int.Parse(restricao[i].Substring(0, restricao[i].IndexOf("f")));
                                break;
                            case 2:
                                matrix[i, j] = int.Parse(restricao[i].Substring(0, restricao[i].IndexOf("a")));
                                break;
                            case 3:
                                matrix[i, j] = int.Parse(restricao[i].Substring(0, restricao[i].IndexOf("f")));
                                restricao[i] = restricao[i].Remove(0, restricao[i].IndexOf("+"));
                                matrix[i, j + 1] = int.Parse(restricao[i].Substring(0, restricao[i].IndexOf("a")));
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

        public static int?[,] MergeMatrices(int[,] matrixVariables, int?[,] matrixRestrictions)
        {
            int?[,] newMatrix = new int?[matrixRestrictions.GetLength(0), matrixVariables.GetLength(1) + matrixRestrictions.GetLength(1)];

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

        public static int?[,] FinalMatrix(int?[,] matrix, int[] z)
        {
            int?[,] newMatrix = new int?[matrix.GetLength(0) + 1, matrix.GetLength(1) + 1];

            int[] result = GetResult(newMatrix.GetLength(0) - 1);

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
                newMatrix[newMatrix.GetLength(0)-1, i] = z[i];
            }
            for (int i = result.Length; i < newMatrix.GetLength(1); i++)
            {
                newMatrix[newMatrix.GetLength(0)-1, i] = 0;
            }
            return newMatrix;
        }

        public static int[] GetResult(int height)
        {
            int[] results = new int[height];
            for (int i = 0; i < height; i++)
            {
                Console.WriteLine($"Digite o resultado da {i}ª limitante:");
                results[i] = int.Parse(Console.ReadLine());

            }

            return results;
        }
    }
}
