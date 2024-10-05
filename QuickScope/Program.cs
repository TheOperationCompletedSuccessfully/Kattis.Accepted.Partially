using System;
using System.Collections.Generic;
using System.Linq;
using Zeva;

namespace QuickScope
{
   class Program
   {
      static void Main(string[] args)
      {
         using (var scanner = new ZevaScanner())
         {
            scanner.Initialize(2 ^ 22, 2 ^ 22);
            var data = new Dictionary<int,Dictionary<string, string>>();
            int stackCounter = 0;
            data.Add(stackCounter, new Dictionary<string, string>());
            var n = scanner.NextUInt();
            
            for (int i=0;i<n;i++)
            {
               var command = scanner.NextString(65);
               
               switch(command)
               {
                  case "TYPEOF":
                     var varName = scanner.NextString(97);
                     if (data.Any(kvp => kvp.Value.ContainsKey(varName)))
                     {
                        var result = data.Last(kvp => kvp.Value.ContainsKey(varName));
                        scanner.streamWriter.WriteLine(result.Value[varName]);
                     }
                     else
                     {
                        scanner.streamWriter.WriteLine("UNDECLARED");
                     }
                     break;
                  case "DECLARE":
                     var variableName = scanner.NextString(97);
                     var typeName = scanner.NextString(97);
                     if (data[stackCounter].ContainsKey(variableName))
                     {
                        scanner.streamWriter.WriteLine("MULTIPLE DECLARATION");
                        //scanner.streamReader.ReadToEnd();
                        i = n;
                     }
                     else
                     {
                        data[stackCounter].Add(variableName, typeName);
                     }
                        break;
                  case "{":
                     data.Add(++stackCounter, new Dictionary<string, string>());
                     break;
                  case "}":
                     data.Remove(stackCounter);
                     stackCounter--;
                     break;
               }
            }
            scanner.streamWriter.Flush();
         }
         //Console.ReadKey();
      }
   }
}
