using System;
using System.Collections.Generic;
using Zeva;

namespace TheGiant
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var scanner = new ZevaScanner())
            {
                scanner.Initialize(2^7,2^7);
                var n = scanner.NextUInt();
                var m = scanner.NextUInt();
                var x1 = scanner.NextUInt(true);
                var y1 = scanner.NextUInt();
                var x2 = scanner.NextUInt();
                var y2 = scanner.NextUInt();

                var usedPoints = new HashSet<long>();

                var middleX = (x1 + x2) / 2;
                var middleY = (y1 + y2) / 2;

                var realMiddleX = (x1 + x2) / 2.0;
                var realMiddleY = (y1 + y2) / 2.0;
                var radio2 = (x1 - realMiddleX)/1000.0 * (x1 - realMiddleX)/1000.0 + (y1 - realMiddleY)/1000.0 * (y1 - realMiddleY)/1000.0;
                double len = (x1 - x2)/1000.0 * (x1 - x2)/1000.0 + (y1 - y2)/1000.0 * (y1 - y2)/1000.0;
                var queue = new Queue<Tuple<int, int>>();
                queue.Enqueue(new Tuple<int, int>(middleX, middleY));
                queue.Enqueue(new Tuple<int, int>(x1 + 1, y1));
                queue.Enqueue(new Tuple<int, int>(x1 - 1, y1));
                queue.Enqueue(new Tuple<int, int>(x1, y1 + 1));
                queue.Enqueue(new Tuple<int, int>(x1, y1 - 1));
                queue.Enqueue(new Tuple<int, int>(x2 + 1, y2));
                queue.Enqueue(new Tuple<int, int>(x2 - 1, y2));
                queue.Enqueue(new Tuple<int, int>(x2, y2 + 1));
                queue.Enqueue(new Tuple<int, int>(x2, y2 - 1));

                var result = FindPoint(x1, y1, x2, y2, n,m,queue, usedPoints,radio2, len);
                scanner.streamWriter.WriteLine($"{result.Item1} {result.Item2}");
                scanner.streamWriter.Flush();
                //Console.ReadKey();
               
            }
        }

        private static Tuple<int,int> FindPoint(int x1, int y1, int x2, int y2, int n,int m, Queue<Tuple<int,int>> queue, HashSet<long> visitedPoints, double radio, double len)
        {
            Tuple<int, int> result = null;
            while (result == null)
            {
                var item = queue.Dequeue();
                var candidateX = item.Item1;
                var candidateY = item.Item2;
                //if(x1==candidateX&&x2==candidateX||y1==candidateY&&y2==candidateY)
                //{
                //    visitedPoints.Add(candidateX * 100000000L + candidateY);
                //}
                if (!visitedPoints.Contains(candidateX * 100000000L + candidateY))
                {
                    double distanceX1 = ((x1 - candidateX) / 1000d) * ((x1 - candidateX) / 1000d) + ((y1 - candidateY) / 1000d )* (y1 - candidateY) / 1000d;
                    double distanceX2 = ((x2 - candidateX) / 1000d) * (x2 - candidateX) / 1000d + ((y2 - candidateY) / 1000d) * (y2 - candidateY) / 1000d;
                    var maxLen = Math.Max(Math.Max(distanceX1, distanceX2), len);
                    if (Math.Abs(distanceX1 - radio) > 0.0000002 && Math.Abs(distanceX2 - radio) > 0.0000002 && candidateX < n && candidateY < m && candidateX >=0 && candidateY>=0
                        && (distanceX1 + distanceX2 < len || len + distanceX2 < distanceX1 || distanceX1 + len < distanceX2) && Math.Sqrt(distanceX1)+Math.Sqrt(distanceX2)+Math.Sqrt(len)-2*Math.Sqrt(maxLen) > 0.00000025)
                    {
                        result = new Tuple<int, int>(candidateX, candidateY);
                    }
                    visitedPoints.Add(candidateX * 100000000L + candidateY);
                    if (candidateX < n)
                    {
                        queue.Enqueue(new Tuple<int, int>(candidateX + 1, candidateY));
                    }
                    if (candidateX > 0)
                    {
                        queue.Enqueue(new Tuple<int, int>(candidateX - 1, candidateY));
                    }
                    if (candidateY < m)
                    {
                        queue.Enqueue(new Tuple<int, int>(candidateX, candidateY + 1));
                    }
                    if (candidateY > 0)
                    {
                        queue.Enqueue(new Tuple<int, int>(candidateX, candidateY - 1));
                    }
                }
            }
            return result;
        }
    }
}
