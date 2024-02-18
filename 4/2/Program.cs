using System;
using System.Collections.Generic;
using System.Linq;

class Graph
{
    private Dictionary<int, List<int>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<int, List<int>>();
    }

    public void Add(int start, int end)
    {
        if (!adjacencyList.ContainsKey(start))
            adjacencyList[start] = new List<int>();

        if (!adjacencyList.ContainsKey(end))
            adjacencyList[end] = new List<int>();

        adjacencyList[start].Add(end);
        adjacencyList[end].Add(start);
    }

    public List<List<int>> FindShortestPaths(int start, int destination)
    {
        List<List<int>> result = new List<List<int>>();
        Queue<List<int>> queue = new Queue<List<int>>();
        int shortestPathLength = -1;

        queue.Enqueue(new List<int> { start });

        while (queue.Count > 0)
        {
            List<int> path = queue.Dequeue();
            int node = path.Last();

            if (node == destination)
            {
                if (shortestPathLength == -1 || path.Count == shortestPathLength)
                {
                    shortestPathLength = path.Count;
                    result.Add(path);
                }
                else
                {
                    // All paths with the same length have been found
                    break;
                }
            }
            else
            {
                if (!adjacencyList.ContainsKey(node)) return result;
                foreach (int neighbor in adjacencyList[node])
                {
                    if (!path.Contains(neighbor))
                    {
                        List<int> newPath = new List<int>(path);
                        newPath.Add(neighbor);
                        queue.Enqueue(newPath);
                    }
                }
            }
        }

        return result;
    }

    static void Main()
    {
        var input = ParseInput("input.txt");
        List<string> output = new List<string>();

        for (int i = 0; i < input.graphs.Length; i++)
        {
            System.Console.WriteLine($"New Task {i}/{input.graphs.Length - 1}");

            Graph graph = input.graphs[i];
            int n = input.ns[i];

            Dictionary<(int, int), (List<int>, List<int>)> edges = input.edgesList[i];

            List<List<int>> shortestPaths = graph.FindShortestPaths(0, n - 1);

            int bestTemp = -100000;
            bool outcome = false;
            List<int> shortestPath = new List<int>();
            foreach (List<int> path in shortestPaths)
            {
                List<int> pathFormated;
                List<(int, int)> options;
                int tempTotal;
                (pathFormated, options, tempTotal) = formatPaths(path, edges);

                var (pathOptined, temp) = applyOptions(pathFormated, options, tempTotal);
                if (Math.Abs(temp) < Math.Abs(bestTemp))
                {
                    bestTemp = temp;
                    shortestPath = pathOptined;
                    outcome = true;
                }
            }

            if (!outcome)
            {
                System.Console.WriteLine("ajajaj");
                output.Add("ajajaj");
            }
            else
            {
                if (shortestPath.Count == 0) { output.Add("ajajaj"); }
                else
                {
                    System.Console.WriteLine($"pohoda {bestTemp} {shortestPath.Count} {string.Join(" ", shortestPath)}");
                    output.Add($"pohoda {bestTemp} {shortestPath.Count} {string.Join(" ", shortestPath)}");
                }
            }
            File.WriteAllLines("output.txt", output);
        }
    }

    static (List<int> pathOptined, int temp) applyOptions(List<int> path, List<(int, int)> options, int temp)
    {
        List<int> pathOptined = new List<int>(path);
        for (int i = 0; i < options.Count; i++)
        {
            if (temp < 2) break;
            var repPair = options[i];
            if (pathOptined.Contains(repPair.Item1))
            {
                int index = path.FindIndex(a => a == repPair.Item1);
                pathOptined[index] = repPair.Item2;
                temp -= 2;
            }
            else if (pathOptined.Contains(repPair.Item2))
            {
                int index = path.FindIndex(a => a == repPair.Item2);
                pathOptined[index] = repPair.Item1;
                temp -= 2;

            }
        }
        return (pathOptined, temp);
    }

    static (List<int> pathFormated, List<(int, int)> options, int tempTotal) formatPaths(List<int> path, Dictionary<(int, int), (List<int> Idents, List<int> Temps)> edges)
    {
        List<int> pathFormated = new List<int>();
        List<(int, int)> options = new List<(int, int)>();
        int tempTotal = 0;
        for (int i = 1; i < path.Count; i++)
        {
            var key = (path[i - 1], path[i]);

            if (!edges.ContainsKey(key)) key = (path[i], path[i - 1]);
            List<int> idents = edges[key].Idents;
            List<int> temps = edges[key].Temps;

            if (idents.Count > 1)
            {
                var index = 0;
                bool found = false;
                bool addedNegative = false;
                for (int l = 0; l < idents.Count; l++)
                {
                    if (found && !addedNegative && temps[l] == -1)
                    {
                        options.Add((idents[index], idents[l]));
                        addedNegative = true;
                    }
                    if (temps[l] == 1 && !found)
                    {
                        index = l;
                        found = true;
                    }
                }

                pathFormated.Add(idents[index]);
                tempTotal += temps[index];

            }
            else
            {
                pathFormated.Add(idents[0]);
                tempTotal += temps[0];
            }

        }

        return (pathFormated, options, tempTotal);
    }

    static (Graph[] graphs, int[] ns, List<Dictionary<(int, int), (List<int>, List<int>)>> edgesList) ParseInput(string path)
    {
        string[] input = File.ReadAllLines(path);
        int tMax = int.Parse(input[0]);
        List<Graph> graphs = new List<Graph>();
        List<Dictionary<(int, int), (List<int> idents, List<int> temps)>> edgesList = new List<Dictionary<(int, int), (List<int>, List<int>)>>();
        List<int> ns = new List<int>();
        int lineCounter = 1;
        for (int t = 0; t < tMax; t++)
        {
            string[] taskValues = input[lineCounter].Split(" ");
            System.Console.WriteLine(t + 1 + " - " + (lineCounter + 1));
            System.Console.WriteLine(string.Join(", ", taskValues));
            lineCounter++;

            int n = int.Parse(taskValues[0]);
            int mMax = int.Parse(taskValues[1]);

            Graph graph = new Graph();
            Dictionary<(int, int), (List<int> Idents, List<int> Temps)> edges = new Dictionary<(int, int), (List<int>, List<int>)>();


            for (int m = 0; m < mMax; m++)
            {
                string[] edge = input[lineCounter].Split(" ");
                lineCounter++;
                int startNode = int.Parse(edge[0]);
                int endNode = int.Parse(edge[1]);

                graph.Add(startNode, endNode);

                int temperature = edge[2] == "ohniva" ? 1 : -1;
                if (edges.ContainsKey((startNode, endNode)) || edges.ContainsKey((endNode, startNode)))
                {
                    var keyUsed = (startNode, endNode);
                    if (edges.ContainsKey((endNode, startNode)))
                    {
                        keyUsed = (endNode, startNode);
                    }
                    List<int> idents = edges[keyUsed].Idents;
                    List<int> temps = edges[keyUsed].Temps;

                    idents.Add(m);
                    temps.Add(temperature);

                    edges[keyUsed] = (idents, temps);
                }
                else
                {
                    List<int> idents = new List<int>();
                    List<int> temps = new List<int>();

                    idents.Add(m);
                    temps.Add(temperature);

                    edges[(startNode, endNode)] = (idents, temps);
                }

            }

            foreach (var key in edges.Keys)
            {
                var values = edges[key];
                var idens = values.Idents;
                var teps = values.Temps;
            }
            graphs.Add(graph);
            ns.Add(n);
            edgesList.Add(edges);

        }



        return (graphs.ToArray(), ns.ToArray(), edgesList);
    }
}
