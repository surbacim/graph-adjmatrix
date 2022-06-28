using System;
using System.Collections.Generic;

namespace AdjmGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] weightmatr =
            {
            { 0, 4, 3, 7, 0},
            { 4, 0, 2, 0, 0},
            { 3, 2, 0, 3, 0},
            { 7, 0, 3, 0, 5},
            { 0, 0, 0, 5, 0},
            };
            int[,] matrix =
            {
            { 0, 1, 1, 1, 0},
            { 1, 0, 1, 0, 0},
            { 1, 1, 0, 1, 0},
            { 1, 0, 1, 0, 1},
            { 0, 0, 0, 1, 0},
            };
            int start = 3, dest = 0;
            Console.WriteLine("Матрица смежности: ");
            Graph.PrintMatrix(matrix);
            Console.WriteLine("\n");
            Graph.PrintShort(matrix, start, dest);
            Console.WriteLine("\n");
            Console.WriteLine("Матрица весов: ");
            Graph.PrintMatrix(weightmatr);
            Console.WriteLine("\n");
            Console.WriteLine("Кратчайший путь от {0} ({1}) до {2} ({3}): ", (char)(start + 65), start, (char)(dest + 65), dest);
            var path = Graph.ShortPath(weightmatr, start, dest);
            Console.WriteLine(string.Join(" - ", path));
            Console.WriteLine();
        }
    }
}
