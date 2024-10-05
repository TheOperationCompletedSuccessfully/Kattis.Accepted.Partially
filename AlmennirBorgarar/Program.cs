using System;
using System.Collections.Generic;
using System.Linq;
using Zeva;

namespace ArmennirBorgarar
{
   class Program
   {
      static void Main(string[] args)
      {
         using (var scanner = new ZevaScanner())
         {
            scanner.Initialize(2 ^ 21, 2 ^ 7);

            var grillNumber = scanner.NextUInt();
            var queueLength = scanner.NextUInt();
            var grills = new Dictionary<int,int>();
            var random = new Random(DateTime.Now.Millisecond);
            var sum = 0;
            var validCount = 0;
            var workingGrills = new MinLongHeap<WorkItem>();
            var sumKeys = 0;
            for(int i=0;i<grillNumber;i++)
            {
               var next = scanner.NextUInt(i == 0);

                  if (grills.ContainsKey(next))
                  {
                     grills[next]++;
                  }
                  else
                  {
                     grills.Add(next,1);
                     sumKeys += next;
                  }
                  validCount++;
                  sum += next;

               
            }

            var coeffs = grills.Keys.ToDictionary(key => key, key => Math.Max( (double)queueLength * grills[key] / (key*1000000),0.000001));
            double total = coeffs.Values.Sum();
            foreach (var kvp in grills)
            {
               long burgersPerGrill = Math.Max((long)Math.Floor(queueLength * coeffs[kvp.Key]/(total*kvp.Value)),1);
               if(burgersPerGrill>0)
               {
                  var newWork = new WorkItem { StartTime=0, SingleGrillTime = kvp.Key, TotalTime = kvp.Key * burgersPerGrill, TotalBurgers = burgersPerGrill * kvp.Value, TotalGrills=kvp.Value, FinishTime= kvp.Key * burgersPerGrill };
                  workingGrills.Push(newWork);
               }
            }
            long done = 0;
            long time = 0;
            while(done <queueLength+1)
            {
               var nextDoneWorkItem = workingGrills.Pop();
               if (done + nextDoneWorkItem.TotalBurgers < queueLength + 1)
               {
                  time = nextDoneWorkItem.FinishTime;
               }
               else if (nextDoneWorkItem.SingleGrillTime == nextDoneWorkItem.TotalTime)
               {
                  time = nextDoneWorkItem.FinishTime;
               }
               else
               {
                  var needsToBeDone = queueLength + 1 - done;
                  var times = (long)Math.Ceiling( (double)needsToBeDone / nextDoneWorkItem.TotalGrills);
                  time = Math.Max(time,nextDoneWorkItem.StartTime+ nextDoneWorkItem.SingleGrillTime * times);
                  
               }
               done += nextDoneWorkItem.TotalBurgers;
               var nextWork = new WorkItem { StartTime=time, SingleGrillTime = nextDoneWorkItem.SingleGrillTime, TotalGrills = nextDoneWorkItem.TotalGrills, TotalBurgers = nextDoneWorkItem.TotalGrills, TotalTime = nextDoneWorkItem.SingleGrillTime, FinishTime= nextDoneWorkItem.SingleGrillTime + time };
               workingGrills.Push(nextWork);
            }

            scanner.streamWriter.WriteLine(time);
            scanner.streamWriter.Flush();
         }
         Console.ReadKey();
      }

      public class WorkItem : IGetLongValue<WorkItem>
      {
         public long TotalTime { get; set; }
         public int SingleGrillTime { get; set; }

         public long TotalBurgers { get; set; }

         public int TotalGrills { get; set; }

         public long FinishTime { get; set; }

         public long StartTime { get; set; }

         public long GetValue(WorkItem item) => item.FinishTime;
      }
   }
}
