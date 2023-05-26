using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_MD
{
    public class Matrix
    {
        public static int[,] GenerateGraphMatrix(int n, double p)
        {
            int[,] A = new int[n, n];

            Random random = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (random.NextDouble() <= p)
                    {
                        A[i, j] = 1;
                        A[j, i] = 1;
                    }
                }
            }

            return A;
        }
        public static List<List<int>> ConvertMatrixToList(int[,] A)
        {
            List<List<int>> graphList = new List<List<int>>();

            int n = A.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                List<int> neighbors = new List<int>();
                for (int j = 0; j < n; j++)
                {
                    if (A[i, j] == 1)
                    {
                        neighbors.Add(j);
                    }
                }
                graphList.Add(neighbors);
            }

            return graphList;
        }

        public static HashSet<Tuple<int, int>> ConvertMatrixToEdges(int[,] A)
        {
            HashSet<Tuple<int, int>> edges = new HashSet<Tuple<int, int>>();

            int n = A.GetLength(0);

            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (A[i, j] == 1)
                    {
                        edges.Add(new Tuple<int, int>(i, j));
                    }
                }
            }

            return edges;
        }
        public static int[] CalculateVertexDegrees(int[,] A)
        {
            int n = A.GetLength(0);
            int[] degrees = new int[n];

            for (int i = 0; i < n; i++)
            {
                int degree = 0;
                for (int j = 0; j < n; j++)
                {
                    degree += A[i, j];
                }
                degrees[i] = degree;
            }

            return degrees;
        }

        public static double CalculateEdgeCount(int[] degrees)
        {
            int sum = degrees.Sum();
            return 0.5 * sum;
        }

        public static double CalculateGraphDensity(double edgeCount, int n)
        {
            return edgeCount / (0.5 * n * (n - 1));
        }
    }

}
