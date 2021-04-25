using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathOptimization_lab3
{
    class PowellMethod
    {
        static double Y(double x)
        {
            double y = Math.Pow(x, 3) + 8 * Math.Pow(x, 2) + x + 5;

            return y;
        }
        public static void FindMin(double x1, double dx)
        {
            Console.WriteLine("----------------------");
            int count = 0;
            double x2, xmin, xopt, x3, a1, a2, eps1;
            double eps = 0.01;
            x2 = x1 + dx;
            xmin = x1;
            xopt = x2;
            eps1 = (xmin - xopt) / xmin;
            Console.WriteLine("x1= " + x1);
            Console.WriteLine("x2 =  " + x2);
            Console.WriteLine("Y(x1)=  " + Y(x1));
            Console.WriteLine("Y(x2)=" + Y(x2) + "\n--------------------");


            while (eps1 <= eps)
            {
                if (Y(x1) > Y(x2))
                {
                    x3 = x1 + 2 * dx;
                }
                else
                {
                    x3 = x1 - dx;
                }

                a1 = (Y(x2) - Y(x1)) / (x2 - x1);
                a2 = 1 / (x3 - x2) * (((Y(x3) - Y(x1)) / (x3 - x1)) - ((Y(x2) - Y(x1)) / (x2 - x1)));
                xopt = (x2 + x1) / (2 - (a1 / 2 * a2));

                if (x1 < x2 && x1 < x3)
                {
                    xmin = x1;
                }
                if (x2 < x1 && x2 < x3)
                {
                    xmin = x2;
                }
                if (x3 < x1 && x3 < x1)
                {
                    xmin = x3;
                }

                if (xmin < xopt)
                {
                    x1 = xmin;
                    x2 = xopt;
                }
                eps1 = (xmin - xopt) / xmin;

                Console.WriteLine("a1 =  " + a1);
                Console.WriteLine("a2 =  " + a2);
                Console.WriteLine("xopt =  " + xopt);
                Console.WriteLine("xmin =  " + xmin);

            }

            Console.WriteLine("Решение:" + xopt);
            Console.WriteLine("Количество итераций:" + count);
            Console.ReadLine();
        }
    }
}
