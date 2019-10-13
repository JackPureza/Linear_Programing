using System;
using System.Collections.Generic;
using System.Text0;
using System.Text.RegularExpressions;
using TrabalhoMarcia.src;

namespace TrabalhoMarcia.src
{
    public class Matrix
    {
        private Operations operations;

        public Matrix(Operations operations)
        {
            this.operations = operations;
        }

        public void resolveMatrix()
        {

        }

        public bool isSimplex(Operations operations, var type)
        {
            bool simplex;

            if (type == "1")
            {
                simplex == true;
            }
            else
            {
                simplex == false;
            }

            return simplex;
        }
    }
}
