using System;
using System.Collections.Generic;
using Zeva;

namespace TriolingoPush
{
   class Program
   {
      public static long[] FibNumbers = new long[1000001];
      public static long modulo = 1000000000 + 7;

      public static Dictionary<long, long> SavedResults = new Dictionary<long, long>();
      public static Dictionary<long, long> periodResults = new Dictionary<long, long>();

      public static HashSet<long> allResults = new HashSet<long>();

      static void Main(string[] args)
      {
         using (var scanner = new ZevaScanner())
         {
            scanner.Initialize(2 ^ 7, 2 ^ 7);

            var n = scanner.NextUInt();
            FibNumbers[1] = 1;
            FibNumbers[2] = 2;
            FibNumbers[3] = 4;
            var d1 = FibNumbers[1];
            var d2 = FibNumbers[2];
            var d3 = FibNumbers[3];
            for (long i = 4; i <= n; i++)
            {
               d1 = d2;
               d2 = d3;
               var key = d2 * modulo + d1;
               if (SavedResults.ContainsKey(key))
               {
                  d3 = SavedResults[key];
                  var period = i - periodResults[key];
                  var periods = (n - i) / period;
                  i += periods * period;
                  continue;
               }

               d3 = (d2 % modulo + d1 % modulo + 1) % modulo;
               SavedResults.Add(key, d3);
               periodResults.Add(key, i);
            }

            if (n == 1)
            {
               scanner.streamWriter.WriteLine($"{d1}");
            }
            else if (n == 2)
            {
               scanner.streamWriter.WriteLine($"{d2}");
            }
            else
            {
               scanner.streamWriter.WriteLine($"{d3}");
            }
            scanner.streamWriter.Flush();
         }
         Console.ReadKey();
      }
   }
}
