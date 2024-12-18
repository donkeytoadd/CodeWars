/*
namespace Project6;

public class HillCLimbing1
{
    public static Dictionary<int, decimal> weights = new Dictionary<int, decimal>();
    

    
    public static List<int[]> Rrhc(int iterations, int restarts)
    {
        List<int[]> solutions = new List<int[]>();

        for (int i = 1; i <= restarts; i++)
        {
            var s = Rmhc(iterations / restarts);
            solutions.Add(s);
            double fitness = Fitness(s);
            Console.WriteLine($"Solution {i}: {PrintSolution(s)}, Fitness: {fitness}");
        }

        return solutions;
    }

    private static int[] Rmhc(int iterations)
    {
        int[] s = RandomSolution();
        double f = Fitness(s);

        for (int i = 0; i < iterations; i++)
        {
            int[] neighbour = RandomNeighbourSwap(s);
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
        Random rand = new Random();
        int[] solution = new int[20];
        
        // Initialize each dimension with a random value
        for (int i = 0; i < solution.Length; i++)
        {
            solution[i] = rand.Next(1,4);
        }

        return solution;
    }

    private static int[] RandomNeighbour(int[] currentSolution)
    {
        Random random = new Random();
        int [] neighbour = (int[])currentSolution.Clone();

        int randomBricks = random.Next(0, 20);

        neighbour[randomBricks] = random.Next(1, 4);

        return neighbour;
    }
    
    private static int[] RandomNeighbourSwap(int[] currentSolution)
    {
        Random random = new Random();
        int[] neighbour = (int[])currentSolution.Clone();

        int brick1 = random.Next(0, 20);
        int brick2 = random.Next(0, 20);

        // Swap lorry assignments between two randomly chosen bricks
        int temp = neighbour[brick1];
        neighbour[brick1] = neighbour[brick2];
        neighbour[brick2] = temp;

        return neighbour;
    }

    
    private static int[] RandomNeighbourMultiple(int[] currentSolution)
    {
        Random random = new Random();
        int[] neighbour = (int[])currentSolution.Clone();

        // Define how many bricks to reassign (adjust as needed)
        int numBricksToReassign = random.Next(1, 5);

        for (int i = 0; i < numBricksToReassign; i++)
        {
            int randomBrick = random.Next(0, 20);
            neighbour[randomBrick] = random.Next(1, 4);
        }

        return neighbour;
    }


    private static decimal[] LoadDifference(int[] solution)
    {
        decimal[] lorryLoad = new decimal[3];

        for (int i = 0; i < solution.Length; i++)
        {
            lorryLoad[solution[i] - 1] += weights[i + 1];
        }

        decimal maxLoad = lorryLoad.Max();
        decimal minLoad = lorryLoad.Min();

        return new decimal[] {maxLoad, minLoad};
    }

    private static double Fitness(int[] solution)
    {
        int difference = LoadDifference(solution);

        double fitness = 1.0 / (difference + 1);

        return fitness;
    }
    
    // Function to print the solution assignment
    public static string PrintSolution(int[] solution)
    {
        return "[" + string.Join(", ", solution) + "]";
    }
    
    public static Dictionary<int, decimal> weightFile()
    {
        string fileName = "/Users/jordanhanson/RiderProjects/Project6/Project6/dataset1.txt";


        string[] lines = File.ReadAllLines(fileName);

        for (int i = 0; i < lines.Length; i++)
        {
            int weight = int.Parse(lines[i]);
            weights.Add(i + 1, weight);  // Assuming weights are 1-indexed
        }

        return weights;
    }



}
*/