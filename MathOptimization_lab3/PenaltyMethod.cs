using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathOptimization_lab3
{
    class PenaltyMethod
    {
        public delegate double PenaltyFunction(Function2 function, double x, double y);
        public delegate double BarrierFunction(Function2 function, double x, double y);


        public static double[] FindMinPenalty(Function2 function, Function2 restrictionFunctionG, Function2 restrictionFunctionH,
            double error,  double[] x0, 
            PenaltyFunction penaltyFunctionG, PenaltyFunction penaltyFunctionH,
            double r = 1.0d, double rFactor = 2.0d)
        {
            int iterationIndex = 1;

            do
            {
                if (iterationIndex > 1)
                {
                    //r *= rFactor;
                    r *= r;
                }

                Console.WriteLine("\nk = " + iterationIndex);

                x0 = HookeJeevesMethod.FindMin((x, y) =>
                {
                    return function(x, y) + r * (penaltyFunctionH(restrictionFunctionH, x, y) + penaltyFunctionG(restrictionFunctionG, x, y));
                }, x0, new double[] { 1.0d, 1.0d }, 2, error);

                iterationIndex++;

                Console.WriteLine($"f({x0[0]}; {x0[1]}) = {function(x0[0], x0[1])}");
                Console.WriteLine($"g({x0[0]}; {x0[1]}) = {restrictionFunctionG(x0[0], x0[1])}");
                Console.WriteLine($"h({x0[0]}; {x0[1]}) = {restrictionFunctionH(x0[0], x0[1])}");
            }
            while (r * (penaltyFunctionH(restrictionFunctionH, x0[0], x0[1]) + penaltyFunctionG(restrictionFunctionG, x0[0], x0[1])) > error);

            return x0;
        }
        public static double[] FindMinBarrier(Function2 function, Function2 restrictionFunctionG,
            double error, double[] x0,
            BarrierFunction barrierFunction,
            double r = 1.0d, double rFactor = 2.0d)
        {
            int iterationIndex = 1;

            do
            {
                if (iterationIndex > 1)
                {
                    r *= rFactor;
                    //r *= r;
                }

                Console.WriteLine("\nk = " + iterationIndex);

                x0 = HookeJeevesMethod.FindMin((x, y) =>
                {
                    return function(x, y) + r * barrierFunction(restrictionFunctionG, x, y);
                }, x0, new double[] { 1.0d, 1.0d }, 2, error);

                iterationIndex++;

                Console.WriteLine($"f({x0[0]}; {x0[1]}) = {function(x0[0], x0[1])}");
                Console.WriteLine($"g({x0[0]}; {x0[1]}) = {restrictionFunctionG(x0[0], x0[1])}");
            }
            while (r * barrierFunction(restrictionFunctionG, x0[0], x0[1]) > error);
            //while (Math.Abs(restrictionFunctionG(x0[0], x0[1])) > error);

            return x0;
        }

        public static double CalculatePenaltyFunctionG1(Function2 function, double x, double y)
        {
            return 0.5d * (function(x, y) + Math.Abs(function(x, y)));
        }
        public static double CalculatePenaltyFunctionG2(Function2 function, double x, double y)
        {
            return Math.Pow(0.5d * (function(x, y) + Math.Abs(function(x, y))), 2);
        }
        public static double CalculatePenaltyFunctionG10(Function2 function, double x, double y)
        {
            return Math.Pow(0.5d * (function(x, y) + Math.Abs(function(x, y))), 10);
        }

        public static double CalculatePenaltyFunctionH1(Function2 function, double x, double y)
        {
            return Math.Abs(function(x, y));
        }
        public static double CalculatePenaltyFunctionH2(Function2 function, double x, double y)
        {
            return Math.Pow(Math.Abs(function(x, y)), 2);
        }
        public static double CalculatePenaltyFunctionH10(Function2 function, double x, double y)
        {
            return Math.Pow(Math.Abs(function(x, y)), 10);
        }

        public static double CalculateBarrierFunctionG1(Function2 function, double x, double y)
        {
            return -1 / function(x, y);
        }
        public static double CalculateBarrierFunctionG2(Function2 function, double x, double y)
        {
            return -Math.Log(-function(x, y));
        }
    }
}
