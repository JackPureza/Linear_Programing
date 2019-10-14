using System;
using System.Collections.Generic;
using TrabalhoMarcia.src;

namespace TrabalhoMarcia
{
    public class Program
    {
        static void Main(string[] args)
        {
            var typeZ = Operations.GetTypeZ(); //return z type in a string that contains a number 1 or 2
            Operations.SetNumberOfRestrictions();
            Operations.SetNumberOfVariables();
            int[] Z = Operations.GetZ(typeZ); //returns a array with the values of the Z expression and the result after the check of the Z type

            int[,] variables = Operations.GetMatrixOfVaribles(); // return a matrix of all the variable's values 

            int?[,] restrictions = Operations.GetMatrixOfRestrictions(); // returns a matrix of all restriciton's values

            int?[,] mergedMatrices = Operations.MergeMatrices(variables, restrictions); //return the matrices together 

            int?[,] CompleteMatrix = Operations.FinalMatrix(mergedMatrices, Z); //puts te Z and result on the matrix 

            Matrix.PrintMatrix(CompleteMatrix); //prints any matrices

            //bool isSimplex = Matrix.isSimplex(typeZ);

            Matrix.SimplexResolve(CompleteMatrix);
        }
    }
}
