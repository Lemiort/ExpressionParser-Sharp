using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
{
    public class Expression
    {
        Dictionary<string, double> arguments;
        Dictionary<string,string> variables;
        List<string> expression;
        public int VariablesCount
        {
            get;
            private set;
        }

        /// <summary>
        /// К
        /// </summary>
        /// <param name="source">строка для апрсинга</param>
        public Expression(string source)
        {
            //Исходная запись в польской нотации
            List<string> tokens = Parser.StringToTokens(source);
            List<string> polska = Parser.TokensToPolskaInvers(tokens);

            expression = polska;
            arguments = new Dictionary<string,double>();
            variables = new Dictionary<string,string>();
            double value=0.0;
            string name="";
            foreach (string str in polska)
            {
                //одна из констант
                if (Parser.IsConstant(str))
                {
                    if (str.Equals("Pi"))
                    {
                        name = str;
                        value = Math.PI;
                    }
                    else
                    {
                        name = str;
                        value = Math.E;
                    }
                }
                //число и не функция
                else if (!Parser.IsOperator(str) && !Parser.IsFunction(str))
                {
                    try
                    {
                        value = Double.Parse(str);
                        name = str;
                    }
                    catch (FormatException)
                    {
                        value = 0;
                        name = str;
                        if(!variables.ContainsKey(str))
                            variables.Add(str,str);
                    }
                }
                if(!arguments.ContainsKey(name))
                    arguments.Add(name, value);
            }
            VariablesCount = variables.Count;
        }
       /* public void SetVariables(List<double> values)
        {
            int i = 0;
            foreach(KeyValuePair<string,string> kvp  in variables)
            {
                arguments[kvp.Key] = values[i];
                i++;
            }
        }*/
        public double Calculate(List<double> values)
        {
            int i = 0;
            foreach (KeyValuePair<string, string> kvp in variables)
            {
                arguments[kvp.Key] = values[i];
                i++;
            }
            Stack<double> stack = new Stack<double>();
            for (int j = 0; j < expression.Count; j++)
            {
                //обработка входного символа
                //операнд
                if (!Parser.IsOperator(expression[j]) && !Parser.IsFunction(expression[j]))
                {
                    //помещаем операнд в стек
                    stack.Push(arguments[expression[j]]);
                }
                //функция
                else if (Parser.IsFunction(expression[j]))
                {
                    //левый операнд
                    double d = stack.Pop();
                    if (expression[j].Equals("cos"))
                        stack.Push(Math.Cos(d));
                    else if (expression[j].Equals("sin"))
                        stack.Push(Math.Sin(d));
                    else if (expression[j].Equals("tan"))
                        stack.Push(Math.Tan(d));
                    else if (expression[j].Equals("atan"))
                        stack.Push(Math.Atan(d));
                    else if (expression[j].Equals("acos"))
                        stack.Push(Math.Acos(d));
                    else if (expression[j].Equals("asin"))
                        stack.Push(Math.Asin(d));
                    else if (expression[j].Equals("log"))
                        stack.Push(Math.Log(d));
                    else if (expression[j].Equals("log10"))
                        stack.Push(Math.Log10(d));
                }
                //знак операции
                else
                {
                    //правый операнд
                    double d1 = stack.Pop();
                    //левый операнд
                    double d2 = stack.Pop();
                    if (expression[j].Equals("+"))
                        stack.Push(d2 + d1);
                    else if (expression[j].Equals("-"))
                        stack.Push(d2 - d1);
                    else if (expression[j].Equals("*"))
                        stack.Push(d2 * d1);
                    else if (expression[j].Equals("/"))
                        stack.Push(d2 / d1);
                    else if (expression[j].Equals("^"))
                        stack.Push(Math.Pow(d2, d1));
                }

            }
            return stack.Pop();
        }
    }
}
