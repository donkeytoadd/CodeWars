using System;

namespace Project6
{
    class Program
    {
        private static void Main(string[] args)
        {
            /*
            int restarts = 10;
            int totalIterations = 1000;

            var weight = HillClimbing.WeightFile();

            var total = HillClimbing.weights.Sum(x => x.Value);
            Console.WriteLine($"Total weight of bricks: {total}");

            var solutions = HillClimbing.Rrhc(restarts, totalIterations);
            */
            
            int restarts = 10; // Number of restarts
            int totalIterations = 1000; // Total number of iterations across all restarts

            List<int[]> bestSolutions = new List<int[]>();
            HillClimbing.WeightFile();

            var total = HillClimbing.weights.Sum(x => x.Value);
            Console.WriteLine($"Total weight of bricks: {total}");
            Console.WriteLine("Optimal weight per lorry: " + (total/3));
            Console.WriteLine(" ");
            

            for (int i = 1; i <= 10; i++)
            {
                List<int[]> solutions = HillClimbing.Rrhc(totalIterations, restarts);

                // Find the best solution from the current run
                int[] bestSolution = solutions.OrderByDescending(s => HillClimbing.Fitness(s)).First();
                bestSolutions.Add(bestSolution);

                Console.WriteLine($"Run {i}: Best Solution: {HillClimbing.PrintSolution(bestSolution)}, Fitness: {HillClimbing.Fitness(bestSolution)}");
            }
            
            /*
            // Display the best solutions from each run
            Console.WriteLine(" ");
            for (int j = 0; j < bestSolutions.Count; j++)
            {
                Console.WriteLine($"Best Solution {j + 1}: {HillClimbing.PrintSolution(bestSolutions[j])}, Fitness: {HillClimbing.Fitness(bestSolutions[j])}");
            }
            */


        }
    }
}