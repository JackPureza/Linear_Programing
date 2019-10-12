using System;
using System.Collections.Generic;
using System.Text;

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
            Console.WriteLine("Digite a expressão de Z:");
            var Z = Console.ReadLine();
            return Z;
        }

        public static void SetLimitantExpressions()
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

        }
    }
}
