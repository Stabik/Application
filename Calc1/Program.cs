using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Calc1
{
    class Program
    {

        static void Main(string[] args)
        {
            string s = "y";
                while (s != "n")
            {
                try
                {
                    Console.WriteLine("-------------------------------------------------");
                   
                    Console.WriteLine("Арифметические знаки, используемые в программе");
                    Console.WriteLine("     *   |   Умножение");
                    Console.WriteLine("     /   |   Деление");
                    Console.WriteLine("     +   |   Сложение");
                    Console.WriteLine("     -   |   Вычитание");
                    Console.WriteLine("Для написания десятичного числа используйте ',' ");
                    Console.WriteLine("Пример:   (-16-2,8)+(4,4-33)-(5+3)*2");
                    Console.WriteLine("Для вычисления выражения нажмите 'ENTER'");
                    
                    Console.WriteLine("-------------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("Введите выражение для вычисления");
                    //"1+2*(3+4/2-(1+2))*2+1";//"(-16-2,8)+(4,4-33)-(5+3)*2";

                    string a = Console.ReadLine();
                    Calc calc = new Calc();
                    Console.WriteLine(calc.ReadStringAndBuildNumbers(a));
                   
                }
                catch
                {
                    Console.WriteLine();
                    Console.WriteLine("Вводите выражение согласно инструкции");
                    Console.WriteLine();
                }
                
                    Console.WriteLine("Для выхода из программы введите 'n' затем нажмите 'ENTER'");
                s = Console.ReadLine();

            }
        }
    }
}

