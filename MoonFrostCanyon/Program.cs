using System;
using System.Collections.Generic;
using Zeva;

namespace MoonFrostCanyon
{
    class Program
    {
        static void Main(string[] args)
        {
            var map1 = new Dictionary<int, char>();

            map1.Add(0, '1');
            map1.Add(1, '1');
            map1.Add(2, '1');
            map1.Add(3, '3');
            map1.Add(4, '3');
            map1.Add(5, '3');
            var map2 = new Dictionary<int, char>();

            map2.Add(0, '2');
            map2.Add(1, '2');
            map2.Add(2, '2');
            map2.Add(3, '4');
            map2.Add(4, '4');
            map2.Add(5, '4');
            var queues = new Queue<char>[]{
                new Queue<char>(new[] { '1', '1', '1', '2', '2', '2' }),
                new Queue<char>(new[] { '1', '1', '1', '2', '2', '2' }),
                new Queue<char>(new[] { '1', '1', '1', '2', '2', '2' }),
                new Queue<char>(new[] { '3', '3', '3', '4', '4', '4' }),
                new Queue<char>(new[] { '3', '3', '3', '4', '4', '4' }),
                new Queue<char>(new[] { '3', '3', '3', '4', '4', '4' })};
            using (var scanner = new ZevaScanner(2^20,2^20))
            {
                var rows = scanner.NextUInt();
                var cols = scanner.NextUInt();

                var map = new int[][] { new []{ 1, 1, 1, 2, 2, 2 }, new[] { 1, 1, 1, 2, 2, 2 }, new[] { 1, 1, 1, 2, 2, 2 }, new[]{ 3, 3, 3, 4, 4, 4 }, new[] { 3, 3, 3, 4, 4, 4 }, new[] { 3, 3, 3, 4, 4, 4 } };
                var queueIndex = 0;
                var metInRow = new HashSet<int>();
                for(int row = 0;row<rows; row++)
                {
                    var metInCol = new HashSet<int>();
                    for (int col = 0;col<cols; col++)
                    {
                        var next = scanner.NextChar(42);
                        if(next ==42)
                        {
                            scanner.streamWriter.Write((char)next);
                        }
                        else
                        {
                            var colIndex = metInCol.Count % 6;
                            var rowIndex = metInRow.Count % 6;

                            var toPrint = map[rowIndex][colIndex];
                            
                            scanner.streamWriter.Write(toPrint);
                            
                            metInCol.Add(col);
                        }
                    }
                    if(metInCol.Count > 0)
                    {
                        metInRow.Add(row);
                    }
                    scanner.streamWriter.WriteLine();
                }
                scanner.streamWriter.Flush();
            }
        }
    }
}