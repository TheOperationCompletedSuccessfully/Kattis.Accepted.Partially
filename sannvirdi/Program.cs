using System;
using System.Collections.Generic;
using System.Linq;
using Zeva;

namespace Sannvirdi
{
    class Program
    {
        static void Main(string[] args)
        {
            using var scanner = new ZevaScanner(2 ^ 23, 2 ^ 21);

                var n = scanner.NextUInt();
                var contestants = new Contestant[n];
                var minGuess = Int32.MaxValue;
                var maxGuess = 0;
                for (int i = 0; i < n; i++)
                {
                    var nextName = scanner.NextString(65);
                    var nextGuess = scanner.NextUInt();
                    var newContestant = new Contestant { Name = nextName, Guess = nextGuess };
                    contestants[i] = newContestant;
                    minGuess = Math.Min(minGuess, nextGuess);
                    maxGuess = Math.Max(maxGuess, nextGuess);
                }
                var ordered = contestants.OrderBy(x => x.Guess);
                contestants = [.. ordered];
                var q = scanner.NextUInt(true);
                for (int i = 0; i < q; i++)
                {
                    var next = scanner.NextUInt(true);
                    if (next < minGuess)
                    {
                        scanner.streamWriter.WriteLine(":(");
                        continue;
                    }
                    if (next >= maxGuess)
                    {
                        scanner.streamWriter.WriteLine(contestants[^1].Name);
                        continue;
                    }
                    string result = ordered.MaxBy(el => el.Guess <= next ? el.Guess : 0)?.Name ?? ":(";
                    /*
                    var prevIndex = 0;
                    var nextIndex = contestants.Length / 2;
                    bool found = false;

                    while(Math.Abs(prevIndex-nextIndex)>1&&!found)
                    {
                        prevIndex = nextIndex;
                        if (contestants[nextIndex].Guess==next)
                        {
                            found = true;
                            result = contestants[nextIndex].Name;
                            continue;
                        }
                        if (contestants[nextIndex].Guess<next)
                        {
                            nextIndex += (contestants.Length - nextIndex) / 2;
                            continue;
                        }
                        nextIndex -= nextIndex / 2;
                    }
                    if(!found)
                    {
                        result = contestants[nextIndex].Guess <= next ? contestants[nextIndex].Name : contestants[nextIndex-1].Name;
                    }
                    */
                    scanner.streamWriter.WriteLine(result);
                }
                scanner.streamWriter.Flush();
        }
    }

    class Contestant
    {
        public string Name { get; set; }
        public int Guess { get; set; }
    }
}