using System;
using System.Collections.Generic;

public class FloydWarshallWithGraph
{
    public static int[,] SolveFloydGraph(Graph graph)
    {
        int n = graph.VerticesCount;
        int[,] dist = new int[n, n];

        // Initialize distances based on the graph
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (i == j)
                    dist[i, j] = 0;
                else
                {
                    int edgeWeight = graph.GetEdgeWeight(graph.GetVertexLabel(i), graph.GetVertexLabel(j));
                    dist[i, j] = edgeWeight != -1 ? edgeWeight : int.MaxValue;
                }
            }
        }

        // Floyd-Warshall algorithm
        for (int k = 0; k < n; k++)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (dist[i, k] != int.MaxValue && dist[k, j] != int.MaxValue &&
                        dist[i, k] + dist[k, j] < dist[i, j])
                    {
                        dist[i, j] = dist[i, k] + dist[k, j];
                    }
                }
            }
        }

        return dist;
    }
}
public class Graph
{
    private Dictionary<string, int> vertexIndices;
    private List<string> vertices;
    private List<List<int>> adjacencyMatrix;

    public int VerticesCount => vertices.Count;

    public Graph()
    {
        vertexIndices = new Dictionary<string, int>();
        vertices = new List<string>();
        adjacencyMatrix = new List<List<int>>();
    }

    public void AddEdge(string source, string destination, int weight)
    {
        if (!vertexIndices.ContainsKey(source))
        {
            vertexIndices[source] = vertices.Count;
            vertices.Add(source);

            // Add a new row to the adjacencyMatrix for the new vertex
            adjacencyMatrix.Add(new List<int>(new int[vertices.Count - 1]));

            // Add a new column to each existing row in the adjacencyMatrix
            foreach (var row in adjacencyMatrix)
                row.Add(int.MaxValue);
        }

        if (!vertexIndices.ContainsKey(destination))
        {
            vertexIndices[destination] = vertices.Count;
            vertices.Add(destination);

            // Add a new row to the adjacencyMatrix for the new vertex
            adjacencyMatrix.Add(new List<int>(new int[vertices.Count - 1]));

            // Add a new column to each existing row in the adjacencyMatrix
            foreach (var row in adjacencyMatrix)
                row.Add(int.MaxValue);
        }

        int sourceIndex = vertexIndices[source];
        int destinationIndex = vertexIndices[destination];

        adjacencyMatrix[sourceIndex][destinationIndex] = weight;
    }


    public int GetEdgeWeight(string source, string destination)
    {
        int sourceIndex, destinationIndex;
        if (vertexIndices.TryGetValue(source, out sourceIndex) && vertexIndices.TryGetValue(destination, out destinationIndex))
        {
            return adjacencyMatrix[sourceIndex][destinationIndex];
        }

        return -1; // Edge does not exist
    }

    public int GetVertexIndex(string vertexLabel)
    {
        return vertexIndices.ContainsKey(vertexLabel) ? vertexIndices[vertexLabel] : -1;
    }

    public string GetVertexLabel(int vertexIndex)
    {
        return vertexIndex >= 0 && vertexIndex < vertices.Count ? vertices[vertexIndex] : null;
    }
}
