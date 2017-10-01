using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
{
    public static class Parser
    {
        
       // static Stack<Operator> operators;
       // static Stack<Expression> expression;
        static private int GetOperatorPriority(string str)
        {
            int priority=0;
            if(str.Equals("+"))
            {
                priority = 1;
            }
            else if( str.Equals("-"))
            {
                priority = 1;
            }
            else if(str.Equals("*"))
            {
                priority = 2;
            }
            else if (str.Equals("/"))
            {
                priority = 2;
            }
            else if (str.Equals("^"))
            {
                priority = 3;
            }
            return priority;
        }
        public static bool IsOperator(string str)
        {
            if (str.Equals("+") || str.Equals("-") || str.Equals("*") ||
                    str.Equals("/") || str.Equals("^"))
                return true;
            else
                return false;
        }
        /// <summary>
        /// Унарные функции
        /// </summary>
        /// <param name="str">строка</param>
        /// <returns></returns>
        public static bool IsFunction(string str)
        {
            if (str.Equals("log") || str.Equals("log10") || 
                   str.Equals("sin") || str.Equals("cos") 
                   || str.Equals("tan")|| str.Equals("atan")
                   || str.Equals("acos") || str.Equals("asin") )
                return true;
            else
                return false;
        }
        public static bool IsConstant(string str)
        {
            if (str.Equals("e") || str.Equals("Pi"))
                return true;
            else
                return false;
        }
        public static List<string> StringToTokens(string str)
        {
            List<string> result = new List<string>();
            string temp ="";
            for (int i = 0; i < str.Length; i++)
            {
                switch(str[i])
                {
                    case '(':
                        result.Add(temp);
                        temp =  "";
                        temp += str[i];
                        result.Add(temp);
                        temp = "";
                        break;
                    case ')':
                        result.Add(temp);
                        temp =  "";
                        temp += str[i];
                        result.Add(temp);
                        temp = "";
                        break;
                    case '+':
                        result.Add(temp);
                        temp =  "";
                        temp += str[i];
                        result.Add(temp);
                        temp = "";
                        break;
                    case '-':
                        result.Add(temp);
                       /* //если это первый символ - добавляем 0 в начало
                        if (result.Count == 0)
                            result.Add("0");
                        //не первый символ - сохраняем всё
                        else
                            result.Add(temp);
                        //после скобки - добавляем 0
                        if (result.Last().Equals("("))
                            result.Add("0");*/
                        temp =  "";
                        temp += str[i];
                        result.Add(temp);
                        temp = "";
                        break;
                    case '/':
                        result.Add(temp);
                        temp =  "";
                        temp += str[i];
                        result.Add(temp);
                        temp = "";
                        break;
                    case '*':
                        result.Add(temp);
                        temp =  "";
                        temp += str[i];
                        result.Add(temp);
                        temp = "";
                        break;
                    case '^':
                        result.Add(temp);
                        temp =  "";
                        temp += str[i];
                        result.Add(temp);
                        temp = "";
                        break;
                    case '\0':
                        result.Add(temp);
                        temp =  "";
                        temp += str[i];
                        result.Add(temp);
                        temp = "";
                        break;
                    default:
                        temp += str[i];
                        break;
                }
            }

            //обработка одного символа
            if (result.Count != 0)
            {
                if (!result.Last().Equals(temp))
                {
                    result.Add(temp);
                }
            }
            else
                result.Add(temp);

            List<string> result2 = new List<string>();
            string prev="start";
            foreach (string str2 in result)
            {
                if (!str2.Equals(""))
                    result2.Add(str2);
                else if (prev.Equals("(") || prev.Equals("start"))
                {
                    result2.Add("0");
                }
                prev = str2;
            }
            return result2;
        }
        public static List<string> TokensToPolskaInvers(List<string> source)
        {
            Stack<string> stack = new Stack<string>();
            List<string> result = new List<string>();
            foreach (string str in source)
            {
                //символ является открывающей скобкой
                if (str.Equals("("))
                    //помещаем в стек
                    stack.Push(str);
                //символ является закрывающей скобкой
                else if(str.Equals(")"))
                {
                    //пока открывающим элементом не станет открывающая скобка
                    while(!stack.Peek().Equals("("))
                    {
                        //выталкиваем элементы из стека в выходную строку.
                        result.Add(stack.Pop());
                    }
                    //выталкиваем открывающую скобку
                    stack.Pop();
                }
                //символ является функцией
                else if (IsFunction(str))
                {
                    //помещаем в стек
                    stack.Push(str);
                }
                //символ явлется левоассоциативным оператором
                else if (IsOperator(str))
                {

                    //пока приоритет оператора
                    //меньше либо равен приоритету оператора,
                    //находящегося на вершине стека
                    if (stack.Count != 0)
                        while (GetOperatorPriority(str) <= GetOperatorPriority(stack.Peek()))
                        {
                            //выталкиваем верхние элементы стека в выходную строку;
                            result.Add(stack.Pop());
                            //если пуст, выйти
                            if (stack.Count == 0)
                                break;
                        }

                    //помещаем оператор в стек
                    stack.Push(str);
                }
                //число
                else
                {
                    result.Add(str);
                }
            }
            //выталкиваем всё оставлшееся
            while (true)
            {
                if (stack.Count == 0)
                    break;
                else
                {
                    result.Add(stack.Pop());
                }
            }
            //result.Add(stack.Peek());
            return result;
        }
    }
}
