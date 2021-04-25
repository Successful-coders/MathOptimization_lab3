using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathOptimization_lab3
{
    public class HookeJeevesMethod
    {
        double f; // функция f(x1,x2)
        double x1, x2, xt1, xt2; // точки
        double vpx1, vpx2; // вектор приращения
        double step; // коэффициент уменьшения шага
        double acc; // точность
        double[] fun; //массив хранящий значения функций
        int count; //счетчик
        Boolean xflag; //переменная отражает успех или неудачу исследующего поиска x1,x2
        double f_old; // переменная нужна для сравнения новой функции
        double fkm1; // значение предыдущей функции
        Boolean iflag; // флаг отражает был ли весь поиск успешным или нет
        double[] mainx1, mainx2; //массивы хранящие базовые точки
        double kp1, kp2, kp1_old, kp2_old; // точки, построенные при движении по образцу
        int proverka; // счетчик для предотвращения зацикливания
        int iterationLimit = 1000;

        private Function2 Function;


        private double CalculateF(double x, double y)
        {
            return 5 * Math.Pow(x + y, 2) + Math.Pow(x - 2, 2);
        }


        public double[] Start(Function2 Function, double x1, double x2, double deltaX1, double deltaX2, double step, double acc)
        {
            // параметры формы
            try
            {
                this.Function = Function;

                this.x1 = x1;
                this.x2 = x2;
                this.vpx1 = deltaX1;
                this.vpx2 = deltaX2;
                this.step = step;
                this.acc = acc;

                fun = new double[1000];
                mainx1 = new double[1000];
                mainx2 = new double[1000];

                if (step <= 1)
                {
                    //Console.WriteLine("Step should be > 1");

                    return new double[2] { x1, x2 };
                }
                if (acc >= 1 || acc < 0)
                {
                    //Console.WriteLine("Acc should be < 1 & > 0");

                    return new double[2] { x1, x2 };
                }

                return start();
            }
            catch (FormatException)
            {
                //Console.WriteLine("Incorrect fields");
            }

            return new double[2] { x1, x2 };
        }

        private Boolean uspex(double a, double b)
        {
            if (a < b)
                return true;
            else
                return false;
        }
        private void ipoisk(double xodin, double xdva, int p)
        {
            f = Function(xodin, xdva);
            xflag = uspex(f, f_old);
            //if (p == 0)
                //Console.WriteLine("Hold x" + (p + 1) + "=" + xdva + ", give for x");
            //else
                //Console.WriteLine("hold x" + (p + 1) + "=" + xodin + ", give for x");
            //Console.WriteLine("f(" + xodin + "," + xdva + ")= " + f + " < " + f_old + " ? " + xflag);
            if (xflag == true)
            {
                if (p == 0)
                    x1 = xt1;
                else
                    x2 = xt2;
                // f_old = f;
                iflag = true;
            }
        }
        private double[] start()
        {
            // Шаг 1. Инициализация
            count = 0;
            fun[count] = Function(x1, x2);
            //Console.WriteLine("f(" + x1 + " ; " + x2 + ") = " + fun[count]);
            mainx1[0] = x1;
            mainx2[0] = x2;
            //Console.WriteLine("-----------------------------------------------------------------------------");
            proverka = 1;
            f_old = fun[count];
        // Шаг 2. Исследующий поиск
        shag2:
            //Console.WriteLine("Exploratory search: ");
            iflag = false;
            proverka = proverka + 1;
            // исследующий поиск x1
            xt1 = x1 + vpx1;
            ipoisk(xt1, x2, 0);
            // исследующий поиск x2
            xt2 = x2 + vpx2;
            ipoisk(x1, xt2, 1);
            if (iflag == false)
            {   // исследующий поиск x1 в противоположном направлении 
                xt1 = x1 - vpx1;
                ipoisk(xt1, x2, 0);
                // исследующий поиск x2 в противоположном направлении 
                xt2 = x2 - vpx2;
                ipoisk(x1, xt2, 1);
            }
            // Шаг 3. Проверка успеха исследующего поиска
            if (iflag == true)
                goto predshag5;
            else
                goto shag4;
            predshag5:
            count = count + 1;
            mainx1[count] = x1;
            mainx2[count] = x2;
            fun[count] = Function(mainx1[count], mainx2[count]);
            //Console.WriteLine("x" + count + "=(" + x1 + "," + x2 + ")=" + fun[count]);
            //Console.WriteLine("-----------------------------------------------------------------------------");
            // Шаг 5. Поиск по образцу
            kp1_old = mainx1[count - 1];
            kp2_old = mainx2[count - 1];
        shag5:
            kp1 = mainx1[count] + (mainx1[count] - kp1_old);
            kp2 = mainx2[count] + (mainx2[count] - kp2_old);
            f = Function(kp1, kp2);
            proverka = proverka + 1;
            if (proverka > iterationLimit)
                goto konec2;

            /*if (f >= fun[count])
            {
                kp1 = kp1_old;
                kp2 = kp2_old;
                f = regexpr(kp1, kp2);
            }*/

            //Console.WriteLine("pattern-mattching search:");
            //Console.WriteLine("p(" + kp1 + "," + kp2 + ")=" + f);

            // Шаг 6. Исследующий поиск после поиска по образцу
            //Console.WriteLine("Exploratory search afer pattern-mattching search: ");
            f_old = f;
            iflag = false;
            x1 = kp1;
            x2 = kp2;
            // исследующий поиск x1
            xt1 = x1 + vpx1;
            ipoisk(xt1, x2, 0);
            // исследующий поиск x2
            xt2 = x2 + vpx2;
            ipoisk(x1, xt2, 1);
            if (iflag == false)
            {   // исследующий поиск x1 в противоположном направлении 
                xt1 = x1 - vpx1;
                ipoisk(xt1, x2, 0);
                // исследующий поиск x2 в противоположном направлении 
                xt2 = x2 - vpx2;
                ipoisk(x1, xt2, 1);
            }
            if (iflag == false) goto shag4;
            //Console.WriteLine("f^k+1 = " + f);
            //Console.WriteLine("f^k = " + fun[count]);

            // Шаг 7. Выполняется ли неравенство f(x^(k+1))<f(x^k)?

            if (f < fun[count])
            {
                kp1_old = kp1;
                kp2_old = kp2;
                fkm1 = Function(kp1, kp2);
                count++;
                mainx1[count] = x1;
                mainx2[count] = x2;
                fun[count] = Function(mainx1[count], mainx2[count]);
                //Console.WriteLine("x^k-1(" + kp1_old + "," + kp2_old + ")=" + fkm1);
                //Console.WriteLine("---- x" + count + "=(" + x1 + "," + x2 + ")=" + fun[count] + "----");
                //Console.WriteLine("-----------------------------------------------------------------------------");
                goto shag5;
            }
            else
            {
                x1 = mainx1[count];
                x2 = mainx2[count];
                goto shag4;
            }
        // Шаг 4. Проверка на окончание поиска
        shag4:

            if (Math.Sqrt(vpx1 * vpx1 + vpx2 * vpx2) <= acc)
            {

                //Console.WriteLine("Inequaliy holds: " + Math.Sqrt(vpx1 * vpx1 + vpx2 * vpx2) + "<=" + acc);
                goto konec;
            }
            else
            {
                vpx1 = vpx1 / step;
                vpx2 = vpx2 / step;
                //Console.WriteLine("dX1=" + vpx1 + " dX2=" + vpx2);
                goto shag2;
            }
        konec:
            Console.WriteLine("Answer: x(" + kp1 + ";" + kp2 + "),f(x)=" + f_old);
            return new double[2] { kp1, kp2 };
        konec2:
            Console.WriteLine("Right solution hasn't been found");

            return new double[2] { x1, x2 };
        }
    }
}
