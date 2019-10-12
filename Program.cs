using System;
using System.Collections.Generic;

namespace TrabalhoMarcia
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] expressao = new string[50];

            Console.WriteLine("Para Zmax, digite 1, para Zmin digite 2:");
            var Ztipo = Console.ReadLine();

            Console.WriteLine("Digite a expressão de Z:");
            var Z = Console.ReadLine();

            for (int i = 0; i <= expressao.Length; i++)
            {
                Console.WriteLine("Digite expressão limitante:");
                expressao[i] = Console.ReadLine();
                Console.WriteLine(expressao[i]);

                Console.WriteLine("Se não quiser escrever outra expressao, digite falso, caso queira digite qualquer valor:");
                var aux = Console.ReadLine();
                if
                    (aux == "falso")
                {
                    break;
                }
            }
        }
    }
}
