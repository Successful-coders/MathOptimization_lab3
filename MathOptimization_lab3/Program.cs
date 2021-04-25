using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathOptimization_lab3
{
    class Program
    {
        private static double CalculateF(double x, double y)
        {
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
            return Math.Pow(x + y, 2) + 4 * Math.Pow(y, 2);
        }
        private static double CalculateRestrictionG1(double x, double y)
        {
            return -x - y + 5.0d;
        }
        private static double CalculateRestrictionH1(double x, double y)
        {
            return x - y + 2;
        }


        static void Main(string[] args)
        {
            //double[] resulVector2 = PenaltyMethod.FindMin(CalculateF, CalculateRestrictionG, CalculateRestrictionH,
            //    0.01d, new double[2] { 1.1d, 1.1d},
            //    PenaltyMethod.CalculatePenaltyFunctionG2, PenaltyMethod.CalculatePenaltyFunctionH2);

            double[] resulVector2 = PenaltyMethod.FindMin(CalculateF1, CalculateRestrictionG1, CalculateRestrictionH1,
                1.0E-7d, new double[2] { -10.0d, 10.0d },
                PenaltyMethod.CalculatePenaltyFunctionG1, PenaltyMethod.CalculatePenaltyFunctionH1,
                2);

            Console.WriteLine($"result = ({resulVector2[0]}; {resulVector2[1]})");
        }
    }
}
