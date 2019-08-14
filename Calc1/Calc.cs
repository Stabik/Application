using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc1
{
    class Calc
    {
        StringBuilder sb = new StringBuilder();
        double d;
        Stack<double> stack = new Stack<double>();
        Stack<char> operation = new Stack<char>();

        void OperationWithNamber()
        {
            double res = stack.Pop();
            char b = operation.Pop();
            double c = stack.Pop();
            switch (b)
            {
                case '+': res += c; break;
                case '-': res = c - res; break;
                case '*': res *= c; break;
                case '/': res = c / res; break;
            }
            stack.Push(res);
        }


       public string ReadStringAndBuildNumbers(string s)
        {
            
            for (int i = 0; i < s.Length; i++)
            {
                if (s[0] == '-' && i == 0)// если первый знак числа является минусом
                {
                    sb.Append(s[0]);
                    continue;
                }
                if (s[i] == '/' || s[i] == '*' || s[i] == '+' || s[i] == '-' || s[i] == '(' || s[i] == ')')
                {
                    if (sb.Length == 0 && s[i] != ')') 
                    {
                        if (operation.Count > 0)
                        // проверка стека  на знак Умножения\ Деления 
                        {
                            if ((s[i] == '/' || s[i] == '*'||s[i] == '+' || s[i] == '-') && (operation.Peek() == '*' || operation.Peek() == '/'))
                            {
                                OperationWithNamber();
                                operation.Push(s[i]);
                                continue;
                            }
                            else
                            {
                                if (stack.Count == 0||operation.Peek()=='(')// создаем первое отрицательное число отрицательное число, если оно есть
                                {
                                    sb.Append(s[i]);
                                    continue;
                                }
                                else
                                {
                                    operation.Push(s[i]);
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            operation.Push(s[i]);// все операции посчитаны и их нет в стеке -добавляем
                            continue;
                        }
                    }
                    else
                    {
                        if (sb.ToString()!= "")
                        {
                            stack.Push(Double.Parse(sb.ToString()));
                            sb.Clear();
                        }
                    }
                    if (s[i] == ')')
                    {
                        if (operation.Peek()=='(')//если в стeке одно число
                        {
                            operation.Pop();
                            continue;
                        }
                        do// вычислить все оперции до открытой скобки
                        {
                            OperationWithNamber();

                        } while (operation.Peek() != '(');
                        operation.Pop();//убрать открытую скобку
                        continue;
                    }
                    if (operation.Count == 0 || operation.Peek() == '+' || operation.Peek() == '-' || operation.Peek() == '(')
                    {
                        operation.Push(s[i]);// положить в стэк умножение\деление, если в стеке операций наверху нет умножения\деления
                    }
                    else
                    {
                        OperationWithNamber();// если есть - считаем
                        operation.Push(s[i]);
                    }
                }

                else
                {
                    if (i == s.Length - 1)//добавляем последнее число в строке
                    {
                        sb.Append(s[i]);
                        d = Double.Parse(sb.ToString());
                        stack.Push(d);
                        OperationWithNamber();
                    }
                    else
                    {
                        sb.Append(s[i]);
                    }

                }

            }
            while (operation.Count > 0)// сложение всех оставшихся чисел
            {
                OperationWithNamber();
            }
            return stack.Pop().ToString();
        }
    }
}

