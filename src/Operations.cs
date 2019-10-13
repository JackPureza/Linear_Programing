using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace TrabalhoMarcia.src
{
    public class Operations
    {
        public static string GetTypeZ()
        {
            Console.WriteLine("Para Zmax, digite 1, para Zmin digite 2:");
            var Ztipo = Console.ReadLine();
            return Ztipo;
        }

        public static string GetZ(string typeZ)
        {

            string x1 = "";
            string x2 = "";
            string result = "";
            Console.WriteLine("Digite a expressão de Z:");
            var Z = Console.ReadLine();
            if (Z.Contains("x1") && Z.Contains("x2"))
            {
                if (Z.Substring(0, Z.IndexOf("x1")) == "")
                {
                    x1 = "1";
                    var pos = Z.IndexOf("x1");
                    Z = Z.Remove(0, pos + 2);
                }
                else
                {
                    x1 = Z.Substring(0, Z.IndexOf("x1"));
                    var pos = Z.IndexOf("x1");
                    Z = Z.Remove(0, pos + 2);
                }

                if (Z.Substring(0, Z.IndexOf("x2")) == "")
                {
                    x2 = "1";
                    var pos = Z.IndexOf("x2");
                    Z = Z.Remove(0, pos + 2);
                }
                else
                {
                    x2 = Z.Substring(0, Z.IndexOf("x2"));
                    var pos = Z.IndexOf("x2");
                    Z = Z.Remove(0, pos + 2);
                }

                Z = Z.Replace("=", "");
                int newX1 = int.Parse(x1);
                int newX2 = int.Parse(x2);
                int newZ = int.Parse(Z);

                if (typeZ == "1")
                {
                    newX1 = newX1 * -1;
                    newX2 = newX2 * -1;
                    newZ = newZ * -1;
                }

                Console.WriteLine($"Z = {newX1}{newX2}{newZ}");
                result = $"{newX1}|{newX2}|{newZ}";
                return result;
            }
            else
            {
                Console.WriteLine("You must put x1 and x2");
                return null;
            }
        }

        public static string[] SetLimitantExpressions()
        {
            string[] expressao = new string[50];

            for (int i = 0; i <= expressao.Length; i++)
            {
                Console.WriteLine("Digite expressão limitante:");
                expressao[i] = Console.ReadLine();

                Console.WriteLine("Se não quiser escrever outra expressao, digite falso, caso queira digite qualquer valor:");
                var aux = Console.ReadLine();
                if (aux == "falso")
                {
                    break;
                }
            }

            return expressao;
        }

        public static string TreatmentOFArtificialVariablesAndRespites(string expression, int i)
        {
            if (expression.Contains("<="))
            {
                return expression.Replace("<=", $"+f{i}=");
            }
            if (expression.Contains(">="))
            {
                return expression.Replace(">=", $"-f{i}+a{i}=");
            }
            return expression.Replace("=", $"+a{i}=");
        }

        public static string GetNumbersInVariables(string expression)
        {
            string x1 = "";
            string x2 = "";

            if (expression.Substring(0, expression.IndexOf("x1")) == "")
            {
                x1 = "1";
                var pos = expression.IndexOf("x1");
                expression = expression.Remove(0, pos + 2);
            }
            else
            {
                x1 = expression.Substring(0, expression.IndexOf("x1"));
                var pos = expression.IndexOf("x1");
                expression = expression.Remove(0, pos + 2);
            }

            if (expression.Substring(0, expression.IndexOf("x2")) == "")
            {
                x1 = "1";
                var pos = expression.IndexOf("x2");
                expression = expression.Remove(0, pos + 2);
            }
            else
            {
                x2 = expression.Substring(0, expression.IndexOf("x2"));
                var pos = expression.IndexOf("x2");
                expression = expression.Remove(0, pos + 2);
            }

            if (expression.Contains("+f"))
            {
                var pos = expression.IndexOf("=");
                expression = expression.Remove(0, pos + 1);
                return $"{x1}|{x2}|1|{expression}";
            }
            if (expression.Contains("-f"))
            {
                var pos = expression.IndexOf("=");
                expression = expression.Remove(0, pos + 1);
                return $"{x1}|{x2}|-1|1|{expression}";
            }
            if (expression.Contains("+a"))
            {
                var pos = expression.IndexOf("=");
                expression = expression.Remove(0, pos + 1);
                return $"{x1}|{x2}|1|{expression}";
            }
            return "Error";
        }

        public static int[] PopulateAndOrganizeMatrix(string[] expressions) 
        {
            int[] lines = new int[expressions.Length]; 
            for (int i = expressions.Length; i > 0 ; i--)
            {
               
            }
        }
    }
}
