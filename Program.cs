using System;

class SeidelMethod
{
    static void Main()
    {
        double[,] coefficients = {
            { 4, -1,  1,  8 },
            { 2,  7, -2, -5 },
            { 1, -3,  6,  9 }
        };

        double[] initialGuess = { 0, 0, 0 };
        double[] solution = SolveSeidelMethod(coefficients, initialGuess, 0.0001);

        Console.WriteLine("Solution:");
        for (int i = 0; i < solution.Length; i++)
        {
            Console.WriteLine($"x{i + 1} = {solution[i]}");
        }

        double[] residual = CalculateResidual(coefficients, solution);
        Console.WriteLine("\nResidual:");
        for (int i = 0; i < residual.Length; i++)
        {
            Console.WriteLine($"r{i + 1} = {residual[i]}");
        }

        Console.ReadLine();
    }

    static double[] SolveSeidelMethod(double[,] coefficients, double[] initialGuess, double epsilon)
    {
        int n = initialGuess.Length;
        double[] previousSolution = new double[n];
        double[] solution = (double[])initialGuess.Clone();
        double error = epsilon + 1;
        int iterations = 0;

        while (error > epsilon)
        {
            iterations++;
            for (int i = 0; i < n; i++)
            {
                previousSolution[i] = solution[i];
            }

            for (int i = 0; i < n; i++)
            {
                double sum1 = 0;
                double sum2 = 0;

                for (int j = 0; j < i; j++)
                {
                    sum1 += coefficients[i, j] * solution[j];
                }

                for (int j = i + 1; j < n; j++)
                {
                    sum2 += coefficients[i, j] * previousSolution[j];
                }

                solution[i] = (coefficients[i, n] - sum1 - sum2) / coefficients[i, i];
            }

            error = CalculateError(solution, previousSolution);
        }

        Console.WriteLine($"\nNumber of iterations: {iterations}");
        return solution;
    }

    static double[] CalculateResidual(double[,] coefficients, double[] solution)
    {
        int n = solution.Length;
        double[] residual = new double[n];

        for (int i = 0; i < n; i++)
        {
            double sum = 0;

            for (int j = 0; j < n; j++)
            {
                sum += coefficients[i, j] * solution[j];
            }

            residual[i] = coefficients[i, n] - sum;
        }

        return residual;
    }

    static double CalculateError(double[] currentSolution, double[] previousSolution)
    {
        double maxError = 0;

        for (int i = 0; i < currentSolution.Length; i++)
        {
            double error = Math.Abs(currentSolution[i] - previousSolution[i]);
            if (error > maxError)
            {
                maxError = error;
            }
        }

        return maxError;
    }
}
