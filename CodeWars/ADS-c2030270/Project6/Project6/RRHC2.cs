using System;
namespace Project6;

public class HillClimbing
{
    public static Dictionary<int, decimal> weights = new Dictionary<int, decimal>();
    private static Random rand = new Random();

    public static List<int[]> Rrhc(int iterations, int restarts)
    {
        List<int[]> solutions = new List<int[]>();

        for (int i = 1; i <= restarts; i++)
        {
            var s = Rmhc(iterations / restarts);
            solutions.Add(s);
            //double fitness = Fitness(s);
            /*
            Console.WriteLine($"Solution {i}: {PrintSolution(s)}, Fitness: {fitness}");
            Console.WriteLine(" ");
            */
            
        }

        return solutions;
    }

    private static int[] Rmhc(int iterations)
    {
        int[] s = RandomSolution();
        double f = Fitness(s);

        for (int i = 0; i < iterations; i++)
        {
            int[] neighbour = RandomLorryReassignment(s);
            double neighbourFitness = Fitness(neighbour);

            if (neighbourFitness > f)
            {
                s = neighbour;
                f = neighbourFitness;
            }
        }

        return s;
    }

    private static int[] RandomSolution()
    {
        int[] solution = new int[30];
        /*
        for (int i = 0; i < solution.Length; i++)
        {
            solution[i] = rand.Next(1,4);
        }
        */
        
        for (int i = 0; i < solution.Length; i++)
        {
            solution[i] = i % 3 + 1; // Assign lorries in a cyclic pattern
        }
        
        return solution;
    }

    private static int[] RandomNeighbour(int[] currentSolution)
    {
        int[] neighbour = (int[])currentSolution.Clone();
        int[] neighbour2 = new int[neighbour.Length];
        
        //Array.Copy(neighbour, neighbour2, neighbour.Length);
        
        

        int randomBricks = rand.Next(0, 20);

        neighbour[randomBricks] = rand.Next(1, 4);

        return neighbour;
    }

    private static int[] RandomNeighbourSwap(int[] currentSolution)
    {
        int[] neighbour = (int[])currentSolution.Clone();

        int brick1 = rand.Next(0, 20);
        int brick2 = rand.Next(0, 20);

        // Swap lorry assignments between two randomly chosen bricks
        int temp = neighbour[brick1];
        neighbour[brick1] = neighbour[brick2];
        neighbour[brick2] = temp;

        return neighbour;
    }

    private static int[] RandomNeighbourMultiple(int[] currentSolution)
    {
        int[] neighbour = (int[])currentSolution.Clone();

        // Define how many bricks to reassign (adjust as needed)
        int numBricksToReassign = rand.Next(1, 5);

        for (int i = 0; i < numBricksToReassign; i++)
        {
            int randomBrick = rand.Next(0, 20);
            neighbour[randomBrick] = rand.Next(1, 4);
        }

        return neighbour;
    }
    
    private static int[] RandomLorryReassignment(int[] currentSolution)
    {
        int[] neighbour = (int[])currentSolution.Clone();

        int randomLorry = rand.Next(1, 4);

        for (int i = 0; i < neighbour.Length; i++)
        {
            if (neighbour[i] == randomLorry)
                neighbour[i] = rand.Next(1, 4);
        }

        return neighbour;
    }
    
    private static int[] SwapBricksFromEachLorry(int[] currentSolution)
    {
        int[] neighbour = (int[])currentSolution.Clone();

        for (int lorry = 1; lorry <= 3; lorry++)
        {
            // Find a random brick assigned to the current lorry
            int currentBrick = rand.Next(0, 20);

            // Find the lorry with the fewest bricks and assign the brick to it
            int minBricksLorry = FindLorryWithMinBricks(neighbour);
            neighbour[currentBrick] = minBricksLorry;
        }

        return neighbour;
    }

    private static int FindLorryWithMinBricks(int[] solution)
    {
        int[] lorryBricksCount = new int[3];

        for (int i = 0; i < solution.Length; i++)
        {
            lorryBricksCount[solution[i] - 1]++;
        }

        int minBricks = lorryBricksCount.Min();

        for (int lorry = 1; lorry <= 3; lorry++)
        {
            if (lorryBricksCount[lorry - 1] == minBricks)
            {
                return lorry;
            }
        }

        return 1; // Default to lorry 1
    }

    
    private static int[] SwapNext(int[] currentSolution)
    {
        int[] neighbour = (int[])currentSolution.Clone();

        for (int lorry = 1; lorry <= 3; lorry++)
        {
            // Find a random brick assigned to the current lorry
            int currentBrick = rand.Next(0, 20);

            // Find a random lorry other than the current one
            int newLorry = lorry + 1;
            
            if (newLorry == 3)
            {
                newLorry = 1;
            }
            

            // Swap the brick between lorries
            neighbour[currentBrick] = newLorry;
        }

        return neighbour;
    }


    private static decimal LoadDifference(int[] solution)
    {
        decimal[] lorryLoad = new decimal[3];
        
        /*
        for (int i = 0; i < solution.Length; i++)
        {
            lorryLoad[solution[i] - 1] += weights[i];
        }
        */
        
        for (int i = 0; i < solution.Length; i++)
        {
            int dictionaryKey = solution[i] - 1;

            if (weights.ContainsKey(dictionaryKey))
            {
                lorryLoad[solution[i] - 1] += weights[dictionaryKey];
            }
        }
        
        decimal maxLoad = lorryLoad.Max();
        decimal minLoad = lorryLoad.Min();
        
        /*
        decimal totalLoad1 = lorryLoad[0];
        decimal totalLoad2 = lorryLoad[1];
        decimal totalLoad3 = lorryLoad[2];

        // Find the heaviest and lightest lorries
        decimal maxLoad = Math.Max(Math.Max(totalLoad1, totalLoad2), totalLoad3);
        decimal minLoad = Math.Min(Math.Min(totalLoad1, totalLoad2), totalLoad3);
        */
        
        decimal loadDifference = maxLoad - minLoad;

        return loadDifference;
    }
    
    private static void NormalizeWeights()
    {
        decimal maxWeight = weights.Max(kvp => kvp.Value);
        decimal minWeight = weights.Min(kvp => kvp.Value);

        foreach (var kvp in weights)
        {
            weights[kvp.Key] = (kvp.Value - minWeight) / (maxWeight - minWeight);
        }
    }

    public static double Fitness(int[] solution)
    {
        decimal difference = LoadDifference(solution);
        var denominator = (difference)+1;
        double fitness = 1.0 / ((double)denominator);
       

        return fitness;
    }

    // Function to print the solution assignment
    public static string PrintSolution(int[] solution)
    {
        return "[" + string.Join(", ", solution) + "]";
    }

    public static void WeightFile()
    {
        string fileName = "/Users/jordanhanson/RiderProjects/Project6/Project6/dataset1.txt";
        
        string[] lines = File.ReadAllLines(fileName);

        for (int i = 0; i < lines.Length; i++)
        {
            decimal weight = decimal.Parse(lines[i]);
            weights.Add(i, weight);
        }
        /*
        foreach (var kvp in weights)
        {
            Console.WriteLine($"Index: {kvp.Key}, Weight: {kvp.Value}");
        }
        */
    }
    
    public static void WeightFile2()
    {
        string fileName = "/Users/jordanhanson/RiderProjects/Project6/Project6/dataset1.txt";
    
        string[] lines = File.ReadAllLines(fileName);

        // Read weights from file
        for (int i = 0; i < lines.Length; i++)
        {
            decimal weight = decimal.Parse(lines[i]);
            weights.Add(i, weight);
        }

        // Normalize weights
        NormalizeWeights();
    
        /*
        foreach (var kvp in weights)
        {
            Console.WriteLine($"Index: {kvp.Key}, Weight: {kvp.Value}");
        }
        */
    }
}

