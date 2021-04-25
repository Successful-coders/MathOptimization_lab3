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


        public static double[] FindMin(Function2 function, Function2 restrictionFunctionG, Function2 restrictionFunctionH,
            double error,  double[] vector2x0, 
            PenaltyFunction penaltyFunctionG, PenaltyFunction penaltyFunctionH,
            double r = 1.0d, double rFactor = 2.0d)
        {
            int iterationIndex = 1;
            double[] vector2minX;

            do
            {
                if (iterationIndex > 1)
                {
                    //r *= rFactor;
                    r *= r;
                }

                Console.WriteLine("\nk = " + iterationIndex);

                HookeJeevesMethod hookeJeevesMethod = new HookeJeevesMethod();
                vector2minX = hookeJeevesMethod.Start((x, y) =>
                {
                    return function(x, y) + ( r * (penaltyFunctionH(restrictionFunctionH, x, y)) + penaltyFunctionG(restrictionFunctionG, x, y));
                }, vector2x0[0], vector2x0[1], 0.01d, 0.01d, 1000, error);

                vector2x0 = vector2minX;

                iterationIndex++;

                Console.WriteLine($"f({vector2x0[0]}; {vector2x0[1]}) = {function(vector2x0[0], vector2x0[1])}");
                Console.WriteLine($"g({vector2x0[0]}; {vector2x0[1]}) = {restrictionFunctionG(vector2x0[0], vector2x0[1])}");
                Console.WriteLine($"h({vector2x0[0]}; {vector2x0[1]}) = {restrictionFunctionH(vector2x0[0], vector2x0[1])}");
            }
            while (r * (penaltyFunctionH(restrictionFunctionH, vector2x0[0], vector2x0[1]) + penaltyFunctionG(restrictionFunctionG, vector2x0[0], vector2x0[1])) > error);

            return vector2minX;
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
    }
}
