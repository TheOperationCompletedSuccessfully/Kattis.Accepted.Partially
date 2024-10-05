using System;
using System.IO;
using System.Text;

namespace Zeva
{
    public class ZevaScanner : IDisposable
    {
        public int NextStreamChar { get; private set; }
        public StreamReader streamReader = null;
        public StreamWriter streamWriter = null;
        readonly StringBuilder sb = new StringBuilder();

        public void Initialize(int bufferSize = 4096, int writeBufferSize = 4096)
        {
            streamReader = new StreamReader(new BufferedStream(Console.OpenStandardInput(), bufferSize));
            streamWriter = new StreamWriter(new BufferedStream(Console.OpenStandardOutput(), writeBufferSize));
            streamWriter.AutoFlush = false;
        }

        public int NextUInt()
        {
            return NextUInt(0);
        }

        public int NextUInt(bool skipPendingInput = false)
        {
            int start = 0;
            if (skipPendingInput)
            {
                while (start - 48 < 0)
                {
                    start = streamReader.Read();
                }
                start -= 48;
            }
            return NextUInt(start);
        }

        public int NextUInt(int previous)
        {
            int data = streamReader.Read();
            if (data < 48) return previous;
            return NextUInt(previous * 10 + data - 48);
        }

        public int NextInt(bool skipPendingInput = false)
        {
            int start = 0;
            if (skipPendingInput)
            {
                while (start - 45 < 0)
                {
                    start = streamReader.Read();
                }
                if (start == 45) return -NextInt(0);
                start -= 48;
            }
            return NextInt(start);
        }

        public int NextInt(int previous)
        {
            int data = streamReader.Read();
            if (data == 45) return -NextInt(previous);
            if (data < 48) return previous;
            return NextInt(previous * 10 + data - 48);
        }

        public long NextULong(bool skipPendingInput = false)
        {
            int start = 0;
            if (skipPendingInput)
            {
                while (start - 48 < 0)
                {
                    start = streamReader.Read();
                }
                start -= 48;
            }
            return NextULong(start);
        }

        public long NextULong(long previous)
        {
            int data = streamReader.Read();
            if (data < 48) return previous;
            return NextULong(previous * 10 + data - 48);
        }

        public long NextLong(bool skipPendingInput = false)
        {
            int start = 0;
            if (skipPendingInput)
            {
                while (start - 45 < 0)
                {
                    start = streamReader.Read();
                }
                if (start == 45) return -NextLong(0);
                start -= 48;
            }
            return NextLong(start);
        }

        public long NextLong(long previous)
        {
            int data = streamReader.Read();
            if (data == 45) return -NextLong(previous);
            if (data < 48) return previous;
            return NextLong(previous * 10 + data - 48);
        }

        public double NextUDouble(bool skipPendingInput = false)
        {
            int start = 0;
            if (skipPendingInput)
            {
                while (start - 48 < 0)
                {
                    start = streamReader.Read();
                }
                start -= 48;
            }
            return NextUDouble(start, 0);
        }

        public double NextUDouble(double previous, int afterDot)
        {
            int data = streamReader.Read();
            if (data == 46) return NextUDouble(previous, 1);
            if (data < 48) return previous;
            if (afterDot == 0)
                return NextUDouble(previous * 10 + data - 48, afterDot);

            return NextUDouble(previous + (data - 48) / Math.Pow(10, afterDot), afterDot++);
        }

        public double NextDouble(bool skipPendingInput = false)
        {
            int start = 0;
            if (skipPendingInput)
            {
                while (start - 45 < 0)
                {
                    start = streamReader.Read();
                }
                if (start == 45) return -NextLong(0);
                start -= 48;
            }
            return NextDouble(start, 0);
        }

        public double NextDouble(double previous, int afterDot)
        {
            int data = streamReader.Read();
            if (data == 45) return -NextDouble(previous, afterDot);
            if (data == 46) return NextDouble(previous, 1);
            if (data < 48) return previous;
            if (afterDot == 0)
                return NextDouble(previous * 10 + data - 48, afterDot);

            return NextDouble(previous + (data - 48) / Math.Pow(10, afterDot), afterDot++);
        }

        public string NextString(int firstAllowedChar)
        {
            sb.Clear();
            int data = streamReader.Read();
            while (data < firstAllowedChar && data >= 0)
            {
                data = streamReader.Read();
            }

            while (data >= firstAllowedChar)
            {
                sb.Append((char)data);
                data = streamReader.Read();
            }
            return sb.ToString();
        }

        public int NextByte()
        {
            return streamReader.Read();
        }

        public void Dispose()
        {
            if (streamReader != null)
            {
                streamReader.Close();
            }

            if (streamWriter != null)
            {
                streamWriter.Close();
            }
        }
    }
}