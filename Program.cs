using System;
using System.ComponentModel;
using Projekt_MD;
public class Program
{
    public static void Main(string[] args)
    {
        int n;
        double p;
        int[,] graphMatrix = null;
        int[] vertexDegrees = null;
        double edgeCount = 0;
        double graphDensity = 0;
        int[] visited;
       
    bool exit = false;
        using (StreamWriter writer = new StreamWriter("wynik.txt"))
        {
            while (!exit)
            {
                Console.WriteLine("------ Menu ------");
                Console.WriteLine("1. Generuj macierz grafu");
                Console.WriteLine("2. Zamień macierz na listę");
                Console.WriteLine("3. Zamień macierz na zestaw krawędzi");
                Console.WriteLine("4. Oblicz sumy stopni wierzchołków");
                Console.WriteLine("5. Oblicz m");
                Console.WriteLine("6. Oblicz gęstość grafu");
                Console.WriteLine("7. Przeszukanie");
                Console.WriteLine("8. Wyjście");

                Console.Write("\nWybierz opcję: ");
                int option = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();

                switch (option)
                {
                    case 1:
                        Console.Write("Podaj wartość n: ");
                        n = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Podaj wartość p: ");
                        p = Convert.ToDouble(Console.ReadLine());
                        graphMatrix = Matrix.GenerateGraphMatrix(n, p);
                        Console.WriteLine("Macierz grafu wygenerowana.");

                        int L = graphMatrix.GetLength(0);

                        Console.WriteLine("Macierz grafu:");
                        writer?.WriteLine("Macierz grafu:");
                        for (int i = 0; i < L; i++)
                        {
                            for (int j = 0; j < L; j++)
                            {
                                Console.Write(graphMatrix[i, j] + " ");
                                writer?.Write(graphMatrix[i, j] + " ");
                            }
                            Console.WriteLine();
                            writer?.WriteLine();
                        }
                        break;

                    case 2:
                        if (graphMatrix == null)
                        {
                            Console.WriteLine("Macierz grafu nie została wygenerowana.");
                        }
                        else
                        {
                            List<List<int>> graphList = Matrix.ConvertMatrixToList(graphMatrix);
                            Console.WriteLine("Lista grafu:");
                            writer?.WriteLine("Lista grafu:");
                            PrintGraphList(graphList);
                        }
                        break;

                    case 3:
                        if (graphMatrix == null)
                        {
                            Console.WriteLine("Macierz grafu nie została wygenerowana.");
                        }
                        else
                        {
                            HashSet<Tuple<int, int>> edges = Matrix.ConvertMatrixToEdges(graphMatrix);
                            Console.WriteLine("Zestaw krawędzi grafu:");
                            writer?.WriteLine("Zestaw krawędzi grafu:");
                            PrintGraphEdges(edges);
                        }
                        break;

                    case 4:
                        if (graphMatrix == null)
                        {
                            Console.WriteLine("Macierz grafu nie została wygenerowana.");
                        }
                        else
                        {
                            vertexDegrees = Matrix.CalculateVertexDegrees(graphMatrix);
                            Console.WriteLine("Sumy stopni wierzchołków:");
                            writer?.WriteLine("Sumy stopni wierzchołków:");
                            PrintVertexDegrees(vertexDegrees);
                        }
                        break;

                    case 5:
                        if (vertexDegrees == null)
                        {
                            Console.WriteLine("Sumy stopni wierzchołków nie zostały obliczone.");
                        }
                        else
                        {
                            edgeCount = Matrix.CalculateEdgeCount(vertexDegrees);
                            Console.WriteLine("Liczba krawędzi (m): " + edgeCount);
                            writer?.WriteLine("Liczba krawędzi (m): " + edgeCount);
                        }
                        break;

                    case 6:
                        if (graphMatrix == null)
                        {
                            Console.WriteLine("Macierz grafu nie została wygenerowana.");
                        }
                        else
                        {
                            if (edgeCount == 0)
                            {
                                Console.WriteLine("Liczba krawędzi (m) nie została obliczona.");
                            }
                            else
                            {
                                graphDensity = Matrix.CalculateGraphDensity(edgeCount, graphMatrix.GetLength(0));
                                Console.WriteLine("Gęstość grafu (p): " + graphDensity);
                                writer?.WriteLine("Gęstość grafu (p): " + graphDensity);
                            }
                        }
                        break;
                    case 7:
                        if (graphMatrix != null)
                        {
                            Console.WriteLine("Podaj wierzchołek początkowy (0 - " + (graphMatrix.GetLength(0) - 1) + "):");
                            int startVertex = Convert.ToInt32(Console.ReadLine());
                            visited = new int[graphMatrix.GetLength(0)];
                            DFS(startVertex);
                        }
                        else
                        {
                            Console.WriteLine("Najpierw wygeneruj macierz grafu.");
                        }
                        break;

                    case 8:
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Nieprawidłowa opcja.");
                        break;
                }

                Console.WriteLine();
            }
            void PrintGraphList(List<List<int>> graphList)
            {
                for (int i = 0; i < graphList.Count; i++)
                {
                    Console.Write(i + ": ");
                    foreach (int neighbor in graphList[i])
                    {
                        Console.Write(neighbor + " ");
                        writer?.WriteLine(neighbor + " ");
                    }
                    Console.WriteLine();
                    writer?.WriteLine();
                }
            }

            void PrintGraphEdges(HashSet<Tuple<int, int>> edges)
            {
                foreach (Tuple<int, int> edge in edges)
                {
                    Console.WriteLine(edge.Item1 + " - " + edge.Item2);
                    writer?.WriteLine(edge.Item1 + " - " + edge.Item2);
                }
            }
            void PrintVertexDegrees(int[] vertexDegrees)
            {
                for (int i = 0; i < vertexDegrees.Length; i++)
                {
                    Console.WriteLine("Wierzchołek " + i + ": " + vertexDegrees[i]);
                    writer?.WriteLine("Wierzchołek " + i + ": " + vertexDegrees[i]);
                }
            }
            void DFS(int vertex)
            {
                Console.Write(vertex + " ");
                writer?.WriteLine("\n Przeszukanie => Krawędź "+ vertex + " ");
                visited[vertex] = 1;

                for (int i = 0; i < graphMatrix.GetLength(0); i++)
                {
                    if (graphMatrix[vertex, i] == 1 && visited[i] == 0)
                    {
                        DFS(i);
                    }
                }
            }
        }
    }
}
