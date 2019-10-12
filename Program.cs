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
            var Z =  Operations.GetZ(); //returns the number inside the Z expression

            Operations.SetLimitantExpressions(); // Set all limitant expressions

        }
    }
}
