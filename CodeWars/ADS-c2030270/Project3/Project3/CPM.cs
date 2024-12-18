using System;
using System.Collections.Generic;
using System.Linq;

class ProjectManager
{
    static void Main()
    {
        Graph projectGraph = InitializeProjectGraph();

        // 1. Calculate project duration
        int projectDuration = CalculateProjectDuration(projectGraph);
        Console.WriteLine($"Project Duration: {projectDuration} units of time");

        // 2. Calculate critical paths
        List<List<int>> criticalPaths = FindCriticalPaths(projectGraph);
        Console.WriteLine("Critical Path(s):");
        foreach (var path in criticalPaths)
        {
            Console.WriteLine(string.Join(" -> ", path));
        }

        // 3. Calculate earliest start time, latest start time, and slack time
        Dictionary<int, int> earliestStartTimes = CalculateEarliestStartTimes(projectGraph);
        Dictionary<int, int> latestStartTimes = CalculateLatestStartTimes(projectGraph, earliestStartTimes);
        Dictionary<int, int> slackTimes = CalculateSlackTimes(earliestStartTimes, latestStartTimes);

        Console.WriteLine("\nNode\tES\tLS\tSlack");
        foreach (var node in projectGraph.Nodes)
        {
            Console.WriteLine($"{node}\t{earliestStartTimes[node]}\t{latestStartTimes[node]}\t{slackTimes[node]}");
        }
    }

    static Graph InitializeProjectGraph()
    {
        // Example project graph (tasks and dependencies)
        Graph projectGraph = new Graph();
        projectGraph.AddNode(1);  // Task 1
        projectGraph.AddNode(2);  // Task 2
        projectGraph.AddNode(3);  // Task 3
        projectGraph.AddNode(4);  // Task 4
        projectGraph.AddNode(5);  // Task 5

        projectGraph.AddEdge(1, 2);  // Task 1 depends on Task 2
        projectGraph.AddEdge(1, 3);  // Task 1 depends on Task 3
        projectGraph.AddEdge(2, 4);  // Task 2 depends on Task 4
        projectGraph.AddEdge(3, 4);  // Task 3 depends on Task 4
        projectGraph.AddEdge(4, 5);  // Task 4 depends on Task 5

        return projectGraph;
    }

    static int CalculateProjectDuration(Graph graph)
    {
        // Calculate the project duration by summing up the durations of all tasks
        return graph.Nodes.Count;
    }

    static List<List<int>> FindCriticalPaths(Graph graph)
    {
        // Find critical paths using depth-first search
        List<List<int>> criticalPaths = new List<List<int>>();
        int projectDuration = CalculateProjectDuration(graph);

        void DFS(int node, List<int> currentPath)
        {
            currentPath.Add(node);

            if (node == projectDuration)
            {
                criticalPaths.Add(new List<int>(currentPath));
            }
            else
            {
                foreach (var neighbor in graph.GetNeighbors(node))
                {
                    DFS(neighbor, currentPath);
                }
            }

            currentPath.RemoveAt(currentPath.Count - 1);
        }

        DFS(1, new List<int>());
        return criticalPaths;
    }

    static Dictionary<int, int> CalculateEarliestStartTimes(Graph graph)
    {
        // Calculate earliest start times using topological sorting
        Dictionary<int, int> earliestStartTimes = new Dictionary<int, int>();

        var sortedNodes = TopologicalSort(graph);
        foreach (var node in sortedNodes)
        {
            int earliestStartTime = 0;

            foreach (var predecessor in graph.GetPredecessors(node))
            {
                earliestStartTime = Math.Max(earliestStartTime, earliestStartTimes[predecessor]);
            }

            earliestStartTimes[node] = earliestStartTime;
        }

        return earliestStartTimes;
    }

    static Dictionary<int, int> CalculateLatestStartTimes(Graph graph, Dictionary<int, int> earliestStartTimes)
    {
        // Calculate latest start times using reverse topological sorting
        Dictionary<int, int> latestStartTimes = new Dictionary<int, int>();

        var reversedSortedNodes = TopologicalSort(graph.Reverse());
        foreach (var node in reversedSortedNodes)
        {
            int latestStartTime = int.MaxValue;

            foreach (var successor in graph.GetSuccessors(node))
            {
                latestStartTime = Math.Min(latestStartTime, earliestStartTimes[successor]);
            }

            latestStartTimes[node] = latestStartTime - 1;  // Assuming tasks take one unit of time
        }

        return latestStartTimes;
    }

    static Dictionary<int, int> CalculateSlackTimes(Dictionary<int, int> earliestStartTimes, Dictionary<int, int> latestStartTimes)
    {
        // Calculate slack times as the difference between latest start time and earliest start time
        Dictionary<int, int> slackTimes = new Dictionary<int, int>();

        foreach (var node in earliestStartTimes.Keys)
        {
            slackTimes[node] = latestStartTimes[node] - earliestStartTimes[node];
        }

        return slackTimes;
    }

    static List<int> TopologicalSort(Graph graph)
    {
        // Perform topological sorting using depth-first search
        List<int> sortedNodes = new List<int>();
        HashSet<int> visited = new HashSet<int>();

        foreach (var node in graph.Nodes)
        {
            if (!visited.Contains(node))
            {
                DFS(node);
            }
        }

        Action<int> DFS = null;
        DFS = (int currentNode) =>
        {
            visited.Add(currentNode);

            foreach (var neighbor in graph.GetNeighbors(currentNode))
            {
                if (!visited.Contains(neighbor))
                {
                    DFS(neighbor);
                }
            }

            sortedNodes.Insert(0, currentNode);  // Insert at the beginning for topological sorting
        };

        return sortedNodes;
    }


}

class Graph
{
    private readonly Dictionary<int, List<int>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<int, List<int>>();
    }

    public IEnumerable<int> Nodes => adjacencyList.Keys;

    public void AddNode(int node)
    {
        if (!adjacencyList.ContainsKey(node))
        {
            adjacencyList[node] = new List<int>();
        }
    }

    public void AddEdge(int from, int to)
    {
        if (adjacencyList.ContainsKey(from))
        {
            adjacencyList[from].Add(to);
        }
    }

    public IEnumerable<int> GetNeighbors(int node)
    {
        return adjacencyList.ContainsKey(node) ? adjacencyList[node] : Enumerable.Empty<int>();
    }

    public IEnumerable<int> GetPredecessors(int node)
    {
        return adjacencyList.Where(pair => pair.Value.Contains(node)).Select(pair => pair.Key);
    }

    public IEnumerable<int> GetSuccessors(int node)
    {
        return adjacencyList.ContainsKey(node) ? adjacencyList[node] : Enumerable.Empty<int>();
    }

    public Graph Reverse()
    {
        // Create a new graph with reversed edges
        Graph reversedGraph = new Graph();

        foreach (var node in Nodes)
        {
            reversedGraph.AddNode(node);

            foreach (var successor in GetSuccessors(node))
            {
                reversedGraph.AddEdge(successor, node);
            }
        }

        return reversedGraph;
    }
}
