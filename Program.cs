using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
{
    class Program
    {
         static Expression func;
         static void Prepass()
         {
             string expression;
             expression = Console.ReadLine();


             List<string> result = Parser.StringToTokens(expression);
             foreach (string str in result)
             {
                 Console.WriteLine(str);
             }
             Console.WriteLine();
             List<string> polska = Parser.TokensToPolskaInvers(result);
             foreach (string str in polska)
             {
                 Console.Write("{0}  ", str);
             }

             func = new Expression(polska);
             List<double> variables = new List<double>();

            /* Console.WriteLine();
             for (int i = 0; i < func.variablesNum; i++)
             {
                 string d = Console.ReadLine();
                 variables.Add(Double.Parse(d));
             }*/
             //Console.WriteLine("\ny={0}", func.Calculate(variables));
         }
         static double y(double x)
         {
             List<double> variables = new List<double>();

             //сосчитываем аргументы
            /* Console.WriteLine();
             for (int i = 0; i < func.variablesNum; i++)
             {
                 string d = Console.ReadLine();
                 variables.Add(Double.Parse(d));
             }*/

             variables.Add(x);
             return func.Calculate(variables);
         }
        static void Main(string[] args)
        {
           
            //func.SetVariables(variables);
            Prepass();
            Console.WriteLine("\nx0=?");
            double x0 =Double.Parse(Console.ReadLine());
            Console.WriteLine("\neps=?");
            double eps =Double.Parse(Console.ReadLine());
            int k;
            Console.WriteLine("\nx* ={0}",CubeInterpolation.CubeMin(y, x0, eps, out k));

            Console.ReadKey();
        }
    }
}
