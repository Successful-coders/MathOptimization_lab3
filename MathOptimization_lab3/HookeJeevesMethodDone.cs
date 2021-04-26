using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathOptimization_lab3
{
    class HookeJeevesMethod
    {
        private static bool MakeExploratorySearch(Function2 function, double[] startX, double[] delta, out double[] resultX)
        {
            double[] trialStep = new double[2];
            startX.CopyTo(trialStep, 0);

            bool isStepMade = false;

            for (int i = 0; i < startX.Length; i++)
            {
                trialStep[i] = startX[i] + delta[i];

                if (function(trialStep[0], trialStep[1]) > function(startX[0], startX[1]))
                {
                    trialStep[i] = startX[i] - delta[i];

                    if (function(trialStep[0], trialStep[1]) > function(startX[0], startX[1]))
                    {
                        //keep startX[0]
                    }
                    else
                    {
                        startX[i] = trialStep[i];

                        isStepMade = true;
                    }
                }
                else
                {
                    startX[i] = trialStep[i];

                    isStepMade = true;
                }
            }

            resultX = new double[2] { startX[0], startX[1]};

            return isStepMade;
        }
        public static double[] FindMin(Function2 function, double[] startX, double[] delta, double step, double error, bool isPrint = false)
        {
            double[] x = new double[2];
            startX.CopyTo(x, 0);

            for (int iterationIndex = 0; iterationIndex < 15; iterationIndex++)
            {
                double functionValue = function(x[0], x[1]);

                while (!MakeExploratorySearch(function, startX, delta, out x))
                {
                    for (int i = 0; i < delta.Length; i++)
                    {
                        delta[i] /= step;
                    }
                }

                if (isPrint)
                {
                    Console.WriteLine($"{iterationIndex}. f({x[0]}; {x[1]}) = {function(x[0], x[1])}");
                }

                if (Math.Abs(function(x[0], x[1]) - functionValue) < error)
                {
                    return x;
                }
            }

            return x;
        }
    }
}
