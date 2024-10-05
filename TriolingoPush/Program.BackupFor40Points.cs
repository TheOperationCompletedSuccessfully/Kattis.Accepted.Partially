using System;
using Zeva;

namespace TriolingoPush
{
   class Program
   {
      public static long[] FibNumbers = new long[1000001];
      public static long modulo = 1000000000 + 7;

      static void Main(string[] args)
      {
         using (var scanner = new ZevaScanner())
         {
            scanner.Initialize(2 ^ 7, 2 ^ 7);

            var n = scanner.NextUInt();
            FibNumbers[1] = 1;
            FibNumbers[2] = 2;

            var result = Fib(n);
            scanner.streamWriter.WriteLine(result);
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
         FibNumbers[index] = (result +1) % modulo;
         return FibNumbers[index];
      }
   }
}
