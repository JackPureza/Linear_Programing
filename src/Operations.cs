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

        public static string GetZ()
        {

            string x1 = "";
            string x2 = "";
            Console.WriteLine("Digite a expressão de Z:");
            var Z = Console.ReadLine();
            Console.WriteLine($"Z = {Z}");
            if (Z.Contains("x1") && Z.Contains("x2"))
            {
                if (Z.Substring(0, Z.IndexOf("x1")) == "")
                {
                    x1 = "1";
                    var pos = Z.IndexOf("x1");
                    Z = Z.Remove(0, pos + 3);
                }
                else 
                {
                    x1 = Z.Substring(0, Z.IndexOf("x1"));
                    var pos = Z.IndexOf("x1");
                    Z = Z.Remove(0,pos+3);
                }
                if(Z.Substring(0, Z.IndexOf("x2")) == "") 
                {
                    x2 = "1";
                }
                else
                {
                    x2 = Z.Substring(0, Z.IndexOf("x2"));
                }

                string newZ = $"{x1}|{x2}";

                return newZ;
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
    }
}
