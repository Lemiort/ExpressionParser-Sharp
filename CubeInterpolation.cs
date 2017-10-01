using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser
{
    public class CubeInterpolation
    {
        static double h;
        static double eps;
        public delegate double funcDelegate(double x);

        static private void swann1(funcDelegate y, double x0, out double a, out double b)
        {
            int k = 1;

            double alpha = 0.01;
            double x=x0;

            //устанавливаем направление функции
            if (y(x + alpha) > y(x))
                alpha = -alpha;


            while (k < 30)
            {
                if ( y(x+alpha) > y(x) )
                {
                    break;
                }
                else
                {
                    x += alpha;
                    alpha *= 2.0;
                    k++;
                }
            }

            a = x-alpha/2;
            b = x+alpha;

            if (a > b)
            {
                double t = a;
                a = b;
                b = t;
            }
        }

        static public double CubeMin(funcDelegate y, double x0, double _eps, out int k)
        {
            k = 1;
            eps = _eps;
            h = 0.01*x0;
            if (h == 0)
                h = 0.01;
            double a, b;
            swann1(y, x0,out a,out b);
            double x1 = x0;
            double x = x0;

            for(int i=0; i< 100; i++)
            {
                //основной этап
                //шаг 1
                //1)
                double z = dy(a, y) + dy(b, y) + 3 * (y(a) - y(b)) / (b - a);
                double omega = Math.Pow((z * z - dy(a, y) * dy(b, y)), 0.5);
                double gamma = (z + omega - dy(a, y)) / (dy(b, y) - dy(a, y) + 2 * omega);
            
                //2)
                if ((gamma <= 1) && ( gamma >= 0))
                {
                    x = a + gamma * (b - a);
                }
                else if(gamma <0)
                    x =a;
                else 
                    x =b;
                //шаг 2
                if(Math.Abs(dy(x,y)) < eps  || x ==a || x==b)
                    break;
                else if(dy(x,y)>0) 
                    b =x;
                else 
                    a = x;
                k++;
            }
            return x;
        }
        static private double dy(double x,funcDelegate y )
        {

            return (y(x + eps) - y(x - eps)) / (2 * eps);
        }
    }
}
