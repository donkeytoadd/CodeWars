using System;
using System.Collections.Generic;
using System.Linq;

class Project3
{
    // Define the activity structure
    struct Activity
    {
        public int Duration;
        public int EarliestStart;
        public int LatestStart;
        public int Slack;
    }

    // Define the graph structure
    class Graph
    {
        public Dictionary<string, Activity> Activities = new Dictionary<string, Activity>();
        public Dictionary<string, List<string>> Dependencies = new Dictionary<string, List<string>>();
    }

    // Calculate earliest start time for activities in the graph
    static void CalculateEarliestStart(Graph graph)
    {
        foreach (var vertex in graph.Dependencies.Keys)
        {
            int earliestStart = 0;

            foreach (var dependency in graph.Dependencies[vertex])
            {
                int dependencyFinishTime = graph.Activities[dependency].EarliestStart + graph.Activities[dependency].Duration;
                earliestStart = Math.Max(earliestStart, dependencyFinishTime);
            }

            Activity graphActivity = graph.Activities[vertex];
            graphActivity.EarliestStart = earliestStart;
        }
    }

    // Calculate latest start time and slack for activities in the graph
    static void CalculateLatestStart(Graph graph, int projectDuration)
    {
        foreach (var vertex in graph.Dependencies.Keys)
        {
            int latestStart = projectDuration - graph.Activities[vertex].Duration;

            foreach (var dependent in graph.Dependencies[vertex])
            {
                int dependentStart = graph.Activities[dependent].EarliestStart;
                latestStart = Math.Min(latestStart, dependentStart - graph.Activities[vertex].Duration);
            }

            // Make a copy of the original activity
            Activity modifiedActivity = graph.Activities[vertex];

            // Modify the copy
            modifiedActivity.LatestStart = latestStart;
            modifiedActivity.Slack = latestStart - modifiedActivity.EarliestStart;

            // Assign the modified copy back to the dictionary entry
            graph.Activities[vertex] = modifiedActivity;
        }
    }

    // Identify critical paths based on slack time
    static List<List<string>> GetCriticalPaths(Graph graph)
    {
        var criticalPaths = new List<List<string>>();
        var endVertices = graph.Activities.Where(activity => !graph.Dependencies.ContainsKey(activity.Key)).Select(activity => activity.Key);

        foreach (var endVertex in endVertices)
        {
            var path = new List<string> { endVertex };
            GetCriticalPathsRecursive(graph, endVertex, path, criticalPaths);
        }

        return criticalPaths;
    }

    static void GetCriticalPathsRecursive(Graph graph, string vertex, List<string> path, List<List<string>> criticalPaths)
    {
        if (!graph.Dependencies.ContainsKey(vertex))
        {
            criticalPaths.Add(new List<string>(path));
            return;
        }

        foreach (var dependency in graph.Dependencies[vertex])
        {
            path.Insert(0, dependency);
            GetCriticalPathsRecursive(graph, dependency, path, criticalPaths);
            path.RemoveAt(0);
        }
    }

    static void Main1()
    {
        // Create the project graph
        Graph projectGraph = new Graph();

// Add activities and dependencies
        projectGraph.Activities["V1..V2"] = new Activity { Duration = 6 };
        projectGraph.Activities["V1..V3"] = new Activity { Duration = 4 };
        projectGraph.Activities["V1..V4"] = new Activity { Duration = 2 };
        projectGraph.Activities["V2..V5"] = new Activity { Duration = 3 };
        projectGraph.Activities["V3..V5"] = new Activity { Duration = 5 };
        projectGraph.Activities["V4..V6"] = new Activity { Duration = 3 };
        projectGraph.Activities["V5..V7"] = new Activity { Duration = 2 };
        projectGraph.Activities["V5..V8"] = new Activity { Duration = 4 };
        projectGraph.Activities["V6..V8"] = new Activity { Duration = 3 };
        projectGraph.Activities["V7..V9"] = new Activity { Duration = 2 };
        projectGraph.Activities["V8..V9"] = new Activity { Duration = 3 };

        projectGraph.Dependencies["V1..V2"] = new List<string> { };
        projectGraph.Dependencies["V1..V3"] = new List<string> { "V1..V2" };
        projectGraph.Dependencies["V1..V4"] = new List<string> { "V1..V2" };
        projectGraph.Dependencies["V2..V5"] = new List<string> { "V1..V3" };
        projectGraph.Dependencies["V3..V5"] = new List<string> { "V1..V3" };
        projectGraph.Dependencies["V4..V6"] = new List<string> { "V1..V4" };
        projectGraph.Dependencies["V5..V7"] = new List<string> { "V2..V5", "V3..V5" };
        projectGraph.Dependencies["V5..V8"] = new List<string> { "V2..V5", "V3..V5" };
        projectGraph.Dependencies["V6..V8"] = new List<string> { "V4..V6" };
        projectGraph.Dependencies["V7..V9"] = new List<string> { "V5..V7" };
        projectGraph.Dependencies["V8..V9"] = new List<string> { "V5..V8", "V6..V8" };

        // Calculate earliest start time
        CalculateEarliestStart(projectGraph);

        // Calculate project duration
        int projectDuration = projectGraph.Activities.Values.Max(a => a.EarliestStart + a.Duration);

        // Calculate latest start time and slack
        CalculateLatestStart(projectGraph, projectDuration);

        // Identify critical paths
        List<List<string>> criticalPaths = GetCriticalPaths(projectGraph);

        // Display results
        Console.WriteLine("Project Duration: " + projectDuration);

        foreach (var vertex in projectGraph.Dependencies.Keys)
        {
            Console.WriteLine($"{vertex}: " +
                              $"Earliest Start: {projectGraph.Activities[vertex].EarliestStart}, " +
                              $"Latest Start: {projectGraph.Activities[vertex].LatestStart}, " +
                              $"Slack: {projectGraph.Activities[vertex].Slack}");

            if (criticalPaths.Any(path => path.Contains(vertex)))
            {
                Console.WriteLine($"   *** This activity is part of a critical path ***");
            }
        }
    }
}
