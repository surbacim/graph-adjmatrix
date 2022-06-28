using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdjmGraph
{
    class Graph
    {
        public static bool BFS(int[,] adj, int src, int dest, int[] pred, int[] dist) //в качестве аргументов передаются матрица смежности, 
                                                                                        // стартовая вершина, вершина назначения, массив предшественников и массив для дистанции между заданными вершинами
        {

            Queue<int> queue = new Queue<int>(); //очередь для добавления всех соседей вершины
            bool[] visited = new bool[adj.GetLength(0)]; // массив для хранения посещенных вершин
            for (int i = 0; i < adj.GetLength(0); i++)
            {
                visited[i] = false;      //изначально все вершины не посещены               
                dist[i] = int.MaxValue;  //все расстояния имеют максимальное значение
                pred[i] = -1;            //предшественники отсутствуют
            }
            visited[src] = true;    //стартовая вершина посещена
            dist[src] = 0;
            queue.Enqueue(src);     //добавляем стартовую вершину в очередь
            while (queue.Count != 0)
            {
                int u = queue.Dequeue(); // возвращаем элемент из очереди
                for (int i = 0; i < adj.GetLength(0); i++)
                {
                    if (visited[i] == false && adj[u, i] != 0) // если вершина i еще не посещена и является соседом вершины u
                    {
                        visited[i] = true;                     // то отмечаем вершину i посещенной
                        dist[i] = dist[u] + 1;                 // считаем расстояние между вершинами 
                        pred[i] = u;                           // вершина u становится предком вершины i
                        queue.Enqueue(i);                      // добавляем вершину i в очередь
                    }
                    if (u == dest)                             // если от стартовой вершины до пункта назначения есть путь
                        return true;
                }
                visited[u] = true;                             // отмечаем обработанную вершину посещенной
            }
            return false;                                      // если нет пути
        }
        public static void PrintShort(int[,] adj, int s, int dest) // метод печати кратчайшего пути
        {
            int[] pred = new int[adj.GetLength(0)];
            int[] dist = new int[adj.GetLength(0)];

            if (BFS(adj, s, dest, pred, dist) == false)                 // если результат BFS показал, что путь отсутствует
            {
                Console.WriteLine("Данные вершины не соединены");
                return;
            }
            List<int> path = new List<int>();                          // список для хранения путей
            int end = dest;                                            // вспомогательная переменная, которая изначальна имеет значение вершины назначения
            path.Add(end);                                             // добавляем в список пути эту переменную
            while (pred[end] != -1)                                    // пока есть предшественники
            {
                path.Add(pred[end]);                                   // добавляем в список пути предыдущую вершину
                end = pred[end];                                       // запоминание текущей вершины, как "предыдущая"
            }
            Console.WriteLine("Длина кратчайшего пути: " + dist[dest]);
            Console.WriteLine("Кратчайший путь от {0} ({1}) до {2} ({3}):", (char)(s+65), s,(char) (dest+65), dest);
            for (int i = path.Count - 1; i >= 0; i--)
            {
                Console.Write(path[i] + i + " ");                          // вывод пути
            }
        }
        public static IList<int> ShortPath(int[,] adj, int start, int dest) // метод для поиска кратчайшего пути через матрицу весов
        {
            int[] minPath = Array.Empty<int>();             // самый короткий путь среди уже найденных
            int minLenght = int.MaxValue;                   // его длина   
            HashSet<int> path = new HashSet<int>() { };     // множество точек текщего пути

            if (start != dest)                              // если начало и конец пути разные вершины,
                SearchingPath(start);                       // то производится рекурсивный поис всех возможных путей

            return minPath;


            void SearchingPath(int vertex)                  // рекурсивный метод построения всех возможных путей
            {
                if (path.Add(vertex))                       // проверка текущий путь проходил через вершину vertex или нет
                {                                           //обработка вершины происходит только если путь через неё ещё не проходил
                    int lastLengh = GetLength(path);        // получение длины текущего пути
                    if (lastLengh < minLenght)              // если  длина пути меньше самого короткого из ранее найденных, то выполняется блок if
                    {

                        if (vertex == dest)                  // если вершина это заданный конец пути, 
                        {                                    
                            minLenght = lastLengh;          //то запоминается этот путь и его длина,
                            minPath = path.ToArray();       // как самый короткий из уже найденных
                        }
                        else
                        {                                                   //если это не конец пути, то происходит перебор всех вершин с которыми есть соединение
                            for (int i = 0; i < adj.GetLength(0); i++)
                            {
                                if (i != vertex && adj[vertex, i] > 0)     //пропускается текущая вершина и вершины не имеющие соединения
                                {
                                    SearchingPath(i);       // рекурсивный вызов метода для всех вершин с которыми есть соединение
                                }
                            }
                        }
                    }

                    path.Remove(vertex);                // "откат" пути на предыдущую вершину
                }

                int GetLength(IEnumerable<int> vertexes)            // метод подсчёта длины пути
                {
                    int prev = vertexes.First();                    // получение начала пути
                    int length = 0;                                 // переменная для подсчёта длины пути
                    foreach (var item in vertexes.Skip(1))          // перебор всех вершин с пропуском первой
                    {
                        length += adj[prev, item];                  // добавление к длине пути веса по индексу [предыдущая вершина, текущая вершина]
                        prev = item;                                // запоминание текущей вершины, как "предыдущая"
                    }
                    return length;
                }
            }

        }
        public static void PrintMatrix(int[,] matrix)
        {
            Console.Write("    ");
            for (int i = 0; i < matrix.GetLength(0); i++)
                Console.Write((char)(i+65) + "   ");
            Console.WriteLine();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                Console.Write((char)(i + 65) + " | ");

                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    Console.Write(matrix[i, j] + " | ");
                }
                Console.WriteLine();
            }
        }
    }
}
