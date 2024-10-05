using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeva;

namespace BasicProgramming2
{
   class Program
   {
      static void Main(string[] args)
      {
         using (var scanner = new ZevaScanner())
         {
            scanner.Initialize(2 ^ 22, 2 ^ 7);

            var n = scanner.NextUInt();
            var t = scanner.NextUInt();

            var result = false;
            var intResult = -1;
            var medianResults = new Tuple<int, int>(-1,-1);
            switch (t)
            {
               case 1:
                  var data = new HashSet<long>();

                  for(int i=0;i<n;i++)
                  {
                     var next = (i == 0) ? scanner.NextULong(true) : scanner.NextULong();
                     var nap = 7777 - next;
                     if(data.Contains(next))
                     {
                        result = true;
                     }
                     else
                     {
                        data.Add(nap);
                     }
                  }

                  scanner.streamWriter.WriteLine(result ? "Yes" : "No");
                  break;
               case 2:
                  var unique = new HashSet<int>();
                  for (int i = 0; i < n; i++)
                  {
                     var next = (i == 0) ? scanner.NextUInt(true) : scanner.NextUInt();
                     if (unique.Contains(next))
                     {
                        result = true;
                     }
                     else
                     {
                        unique.Add(next);
                     }
                  }
                  scanner.streamWriter.WriteLine(result ? "Contains duplicate" : "Unique");
                  break;
               case 3:
                  var level = n / 2;
                  var levelData = new Dictionary<int, int>();
                  int max = 1;
                  for (int i = 0; i < n; i++)
                  {
                     var next = (i == 0) ? scanner.NextUInt(true) : scanner.NextUInt();
                     if(levelData.ContainsKey(next))
                     {
                        levelData[next]++;
                        if(levelData[next]>=max)
                        {
                           intResult = next;
                           max = levelData[next];
                        }
                     }
                     else
                     {
                        levelData.Add(next, 1);
                     }
                  }
                  if(levelData[intResult]<=level)
                  {
                     intResult = -1;
                  }

                  scanner.streamWriter.WriteLine(intResult);
                  break;

               case 4:
                  var maxHeap = new MaxIntHeap<HeapIntValue>();
                  var minHeap = new MinIntHeap<HeapIntValue>();
                  int median = -1;
                  for (int i = 0; i < n; i++)
                  {
                     var next = (i == 0) ? scanner.NextUInt(true) : scanner.NextUInt();
                     var heapValue = new HeapIntValue { Value = next };
                     if (i == 0)
                     {
                        median = next;
                        medianResults = new Tuple<int, int>(next, -1);
                        continue;
                     }
                     var medianValue = new HeapIntValue { Value = median };
                     if (i == 1)
                     {
                        
                        var maxElement = new HeapIntValue() { Value = Math.Max(median, next) };
                        var minElement = new HeapIntValue() { Value = Math.Min(median, next) };
                        medianResults = new Tuple<int, int>(minElement.Value,maxElement.Value);
                        maxHeap.Push(minElement);
                        minHeap.Push(maxElement);
                        median = -1;
                        continue;
                     }
                     var left = maxHeap.Peek();
                     var right = minHeap.Peek();
                     if (median == -1 && next < left.Value)
                     {
                        median = maxHeap.Pop().Value;
                        maxHeap.Push(heapValue);
                        medianResults = new Tuple<int, int>(median,-1);
                        continue;
                     }
                     if (median == -1 && next >= left.Value && next <= right.Value)
                     {
                        median = next;
                        medianResults = new Tuple<int, int>(median, -1);
                        continue;
                     }
                     if (median == -1 && next > right.Value)
                     {
                        median = minHeap.Pop().Value;
                        minHeap.Push(heapValue);
                        medianResults = new Tuple<int, int>(median, -1);
                        continue;
                     }
                     if (next < left.Value)
                     {
                        medianResults = new Tuple<int, int>(left.Value, median);
                        minHeap.Push(new HeapIntValue() { Value = median });
                        median = -1;
                        maxHeap.Push(heapValue);
                        continue;
                     }
                     if (next >= left.Value && next <= right.Value)
                     {
                        medianResults = new Tuple<int, int>(Math.Min(next,median),Math.Max(next, median));
                        if (next <= median)
                        {
                           maxHeap.Push(heapValue);
                           minHeap.Push(medianValue);
                        }
                        else
                        {
                           minHeap.Push(heapValue);
                           maxHeap.Push(medianValue);
                        }
                        median = -1;
                        continue;
                     }
                     medianResults = new Tuple<int, int>(median,right.Value);
                     maxHeap.Push(medianValue);
                     median = -1;
                     minHeap.Push(heapValue);
                  }
                  scanner.streamWriter.WriteLine(medianResults.Item1.ToString() + ((medianResults.Item2 >= 0) ? $" {medianResults.Item2}" : ""));
                  break;
               case 5:
                  var range = new int[900];
                  for (int i = 0; i < n; i++)
                  {
                     var next = (i == 0) ? scanner.NextUInt(true) : scanner.NextUInt();
                     var d = next - 100;
                     if(d>0 && d<900)
                     {
                        range[d]++;
                     }
                  }
                  var first = true;
                  for(int i=0;i<900; i++)
                  {
                     while(range[i]>0)
                     {
                        scanner.streamWriter.Write(first ? (i+100).ToString() : $" {i+100}");
                        first = false;
                        range[i]--;
                     }
                  }
                  break;
            }
            scanner.streamWriter.Flush();
         }
         //Console.ReadKey();
      }

      public class HeapIntValue : IGetIntValue<HeapIntValue>
      {
         public int Value { get; set; }

         public int GetValue(HeapIntValue item) => item.Value;
      }
   }
}
