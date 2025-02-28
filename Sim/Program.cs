using System;
using System.Collections.Generic;
using System.Linq;
using Zeva;


namespace Sim
{
    class Program
    {
        static void Main(string[] args)
        {
            using var scanner = new ZevaScanner(2 ^ 20, 2 ^ 20);
            var cases = scanner.NextUInt();
            for (int i = 0; i < cases; i++)
            {
                var data = new LinkedList<byte>();
                var next = scanner.NextChar(14);

                LinkedListNode<byte> currentNode = null;
                while (next > 13)
                {
                    switch (next)
                    {
                        case 60:
                            if (currentNode != null)
                            {
                                var newCurrent = currentNode.Previous;
                                data.Remove(currentNode);
                                currentNode = newCurrent;

                            }
                            break;
                        case 91:
                            if (currentNode != null && data.Count > 0)
                            {
                                currentNode = null;
                            }
                            break;
                        case 93:
                            if (currentNode != null && data.Count > 0)
                            {
                                currentNode = data.Last;
                            }
                            break;
                        default:
                            //var newNode = new LinkedListNode<byte>((byte)next);
                            if (currentNode != null)
                            {
                                currentNode = data.AddAfter(currentNode, (byte)next);
                            }
                            else
                            {
                                currentNode = data.AddFirst((byte)next);
                            }
                            break;
                    }
                    next = scanner.NextByte();
                }
                while (next != 10)
                {
                    next = scanner.NextByte();
                }
                LinkedListNode<byte> pointer = data.First;
                while (pointer != null)
                {
                    scanner.streamWriter.Write((char)pointer.Value);
                    pointer = pointer.Next;
                }
                scanner.streamWriter.WriteLine();
            }
            scanner.streamWriter.Flush();

        }
    }
}