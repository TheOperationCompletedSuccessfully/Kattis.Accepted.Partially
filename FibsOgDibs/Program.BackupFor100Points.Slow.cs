using System;
using System.Collections.Generic;
using Zeva;

namespace FibsOgDibs
{
   class Program
   {

      public static long[] FibNumbers = new long[4];
      //still works but on the brink
      //public static long[] FibNumbers = new long[20000005];

      public static long modulo = 1000000000 + 7;

      public static Dictionary<Pair, long> SavedResults = new Dictionary<Pair, long>();
      
      public static Dictionary<Pair, long> periodResults = new Dictionary<Pair, long>();
      static void Main(string[] args)
      {
         FibNumbers[1] = 1;
         FibNumbers[2] = 1;
         FibNumbers[3] = 2;
         using (var scanner = new ZevaScanner())
         {
            scanner.Initialize(2 ^ 16, 2 ^ 7);

            var a = scanner.NextUInt();
            var b = scanner.NextUInt();
            var n = scanner.NextUInt(true);
            var d1 = FibNumbers[1];
            var d2 = FibNumbers[2];
            var d3 = FibNumbers[3];
            for (long i = 3; i <= 2 * n + 1; i++)
            {
               //FibNumbers[i] = FibNumbers[i - 1] % modulo + FibNumbers[i - 2] % modulo;
               var newItem = new Pair { Arg1 = d2, Arg2 = d1 };
               if (SavedResults.ContainsKey(newItem))
               {
                  if(periodResults.ContainsKey(newItem))
                  {
                     var period = i - periodResults[newItem];
                     var periods = (2 * n + 1) / period;
                     d1 = d2;
                     d2 = d3;
                     d3 = SavedResults[newItem];
                     i += periods * period;
                     continue;
                  }
                  d1 = d2;
                  d2 = d3;
                  d3 = SavedResults[newItem];
                  continue;
               }
               d3 = d2 % modulo + d1 % modulo;
               d1 = d2;
               d2 = d3;
               SavedResults.Add(newItem, d3);
               periodResults.Add(newItem, i);
            }

            var c1 = d1;//FibNumbers[2 * n - 1];
            var c2 = d2;// FibNumbers[2 * n];
            var c3 = d3;// FibNumbers[2 * n + 1];
            var resultA = ((a * c1) % modulo + (b * c2) % modulo) % modulo;
            var resultB = ((a * c2) % modulo + (b * c3) % modulo) % modulo;
            scanner.streamWriter.WriteLine($"{resultA} {resultB}");
            scanner.streamWriter.Flush();
         }
         Console.ReadKey();
      }

      public struct Pair : IEquatable<Pair>
      {
         public long Arg1 { get; set; }
         public long Arg2 { get; set; }

         public bool Equals(Pair other)
         {
            return Arg1.Equals(other.Arg1) && Arg2.Equals(other.Arg2);
         }
      }

      public static long Fib(int index)
      {
         if (FibNumbers[index] > 0)
         {
            return FibNumbers[index];
         }
         var result = Fib(index - 1) % modulo + Fib(index - 2) % modulo;
         FibNumbers[index] = result % modulo;
         return result % modulo;
      }
   }
}
