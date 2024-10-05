using System;
using System.Collections.Generic;
using System.Linq;
using Zeva;

namespace KannaFriendship
{
   class Program
   {
      static void Main(string[] args)
      {
         using (var scanner = new ZevaScanner())
         {
            scanner.Initialize(2 ^ 35, 2 ^ 34);

            var n = scanner.NextUInt();
            var q = scanner.NextUInt();
            var used = new HashSet<int>();
            for(int i=0;i<q;i++)
            {
               var nextQType = scanner.NextUInt(true);
               switch(nextQType)
               {
                  case 1:
                     var first = scanner.NextUInt();
                     var second = scanner.NextUInt();
                     var data = Enumerable.Range(first, second+1-first);
                     foreach(var d in data)
                     {
                        used.Add(d);
                     }
                     break;
                  case 2:
                     scanner.streamWriter.WriteLine(used.Count);
                     break;
               }
            }
            scanner.streamWriter.Flush();
         }
         //Console.ReadKey();
      }
   }
}
