using System;
using System.Collections.Generic;
using System.Text0;
using System.Text.RegularExpressions;
using TrabalhoMarcia.src;

namespace TrabalhoMarcia.src
{
    public class Matrix
    {
        public static void resolveMatrix()
        {
            while (!this.isResolved())
            {
                if (this.isSimplex())
                {
                    this.simplex();
                }
                else
                {
                    this.twoFases();
                }
            }
        }

        public bool isSimplex()
        {
            bool simplex = true;
            int[] restrictionsSignal = Operations.GetRestrictionsSignal();

            foreach (int signal in restrictionsSignal)
            {
                    if (signal != 1)
                    {
                        simplex = false;
                    }
            }

            return simplex;
        }

        public bool isResolved()
        {
            bool resolved;
            return resolved;
        }

        public int[,] simplex(int[,] matrix)
        {
            return matrix;
        }

        public int[,] twoFases(int[,] matrix)
        {
            return matrix;
        }
    }
}
