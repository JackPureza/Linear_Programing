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
            var Z = Operations.GetZ(typeZ); //returns the number inside the Z expression

            string[] expressions = Operations.SetLimitantExpressions(); // return a array with all the string expressions

            string[] lines = new string[expressions.Length];
            //treatment of expressions to put artifical variables and respites, them returns the numbers inside the expressions
            for (int i = 0; i < expressions.Length; i++)
            {
                expressions[i] = Operations.TreatmentOFArtificialVariablesAndRespites(expressions[i], i);
                Console.WriteLine(expressions[i]);
                lines[i] = Operations.GetNumbersInVariables(expressions[i]);
            }

            Operations.PopulateAndOrganizeMatrix(expressions);
        }
    }
}
