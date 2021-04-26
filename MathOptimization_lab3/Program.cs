using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathOptimization_lab3
{
    class Program
    {
        public static int functionCallCount;


        private static double CalculateF(double x, double y)
        {
            functionCallCount++;

            return 5 * Math.Pow(x + y, 2) + Math.Pow(x - 2, 2);
        }
        private static double CalculateRestrictionG(double x, double y)
        {
            return -x - y + 1.0d;
        }
        private static double CalculateRestrictionH(double x, double y)
        {
            return x - y;
        }

        private static double CalculateF1(double x, double y)
        {
            functionCallCount++;

            return Math.Pow(x + y, 2) + 4 * Math.Pow(y, 2);
        }
        private static double CalculateRestrictionG1(double x, double y)
        {
            return -x - y + 5.0d;
        }
        private static double CalculateRestrictionH1(double x, double y)
        {
            return y - x - 2;
        }


        static void Main(string[] args)
        {
            functionCallCount = 0;

            double[] resulVector2 = PenaltyMethod.FindMinPenalty(CalculateF, CalculateRestrictionG, CalculateRestrictionH,
                1.0E-7d, new double[2] { 0, 0 },
                PenaltyMethod.CalculatePenaltyFunctionG1, PenaltyMethod.CalculatePenaltyFunctionH2,
                2);

            //double[] resulVector2 = PenaltyMethod.FindMinBarrier(CalculateF, CalculateRestrictionG,
            //    1.0E-6d, new double[2] { 1.1d, 1.2d },
            //    PenaltyMethod.CalculateBarrierFunctionG2,
            //    1000, 0.5d);

            Console.WriteLine($"\nresult = f({resulVector2[0].ToString("e5")}; {resulVector2[1].ToString("e5")}) = {CalculateF(resulVector2[0], resulVector2[1]).ToString("e5")}");
            Console.WriteLine("Function call count = " + functionCallCount);
        }
    }
}
