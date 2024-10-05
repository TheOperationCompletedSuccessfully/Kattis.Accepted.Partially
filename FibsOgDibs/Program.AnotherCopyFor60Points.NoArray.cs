using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeva;

namespace FibsOgDibs
{
   class Program
   {

      public static long[] FibNumbers = new long[20005];
      //still works but on the brink
      //public static long[] FibNumbers = new long[20000005];

      public static long modulo = 1000000000 + 7;

      public static HashSet<long> allResults = new HashSet<long>();
      static void Main(string[] args)
      {
         FibNumbers[1] = 1;
         FibNumbers[2] = 1;
         FibNumbers[3] = 2;
         var d1 = FibNumbers[1];
         var d2 = FibNumbers[2];
         var d3 = FibNumbers[3];
         using (var scanner = new ZevaScanner())
         {
            scanner.Initialize(2 ^ 16, 2 ^ 7);

            var a = scanner.NextUInt();
            var b = scanner.NextUInt();
            var n = scanner.NextUInt(true);
            for (int i = 4; i <= 2 * n + 1; i++)
            {
               //if(allResults.Contains(FibNumbers[i - 2] % modulo)&&allResults.Contains(FibNumbers[i - 1] % modulo))
               //FibNumbers[i] = (FibNumbers[i - 1] % modulo + FibNumbers[i - 2] % modulo)%modulo;
               d1 = d2;
               d2 = d3;
               d3 = (d2 % modulo + d1 % modulo)%modulo;
            }

            var c1 = d1;// FibNumbers[2 * n - 1];
            var c2 = d2;// FibNumbers[2 * n];
            var c3 = d3;// FibNumbers[2 * n + 1];
            var resultA = ((a * c1) % modulo + (b * c2) % modulo) % modulo;
            var resultB = ((a * c2) % modulo + (b * c3) % modulo) % modulo;
            scanner.streamWriter.WriteLine($"{resultA} {resultB}");
            scanner.streamWriter.Flush();
         }
         Console.ReadKey();
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
