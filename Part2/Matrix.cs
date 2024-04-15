using System;
using System.Collections.Generic;
using System.Threading;




namespace Part2
{
    public class Line
    {
        private readonly int length;
        private readonly int colomn;
        private readonly static string letters = "QWERTYUIOPASDFGHJKLZXCVBNM<>?}{12345678990!@#$%^&*()";
        private LinkedList<char> line;
        private readonly object lockConsole;
        private readonly byte[] lockLine;

        public Line(int length, int colomn, object lockConsole, ref byte[] lockLine)
        {
            Thread thread = new Thread(Draw);
            this.length = length;
            this.colomn = colomn;
            this.lockConsole = lockConsole;
            this.lockLine = lockLine;
            line = new LinkedList<char>();
            thread.Start();
        }

        private void Draw()
        {
            lockLine[colomn]++;
            for (int a = 0; a < Console.WindowHeight + length; a++)
            {
                Thread.Sleep(400);
                lock (lockConsole)
                {
                    if (a < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(colomn, a);
                        Console.ForegroundColor = ConsoleColor.White;
                        line.AddFirst(GetNewLetter());
                        Console.Write(line.First.Value);
                    }
                    if (line.Count >= 2 && a - 1 < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(colomn, a - 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(line.First.Next.Value);
                    }
                    if (line.Count >= 3 && a - 2 < Console.WindowHeight)
                    {
                        Console.SetCursorPosition(colomn, a - 2);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(line.Last.Value);
                        line.RemoveLast();
                    }
                    if (length <= a)
                    {
                        Console.ResetColor();
                        Console.SetCursorPosition(colomn, a - length);
                        Console.Write(' ');
                    }
                }
            }
            lockLine[colomn]--;
        }

        private static char GetNewLetter()
        {
            var rnd = new Random();
            return letters[rnd.Next(0, letters.Length)];

        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            byte[] lockLine = new byte[Console.WindowWidth];
            object lockConsole = new object();
            var rnd = new Random();
            while (true)
            {
                Thread.Sleep(30);
                int i = rnd.Next(0, lockLine.Length);
                if (lockLine[i] == 1)
                {
                    continue;
                }
                new Line(rnd.Next(4, 10), i, lockConsole, ref lockLine);
            }
        }
    }
}
