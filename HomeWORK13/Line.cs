using System;
using System.Runtime.Serialization.Formatters;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Task1
{
    public class Line
    {
        private readonly int length;
        private readonly int colomn;
        private readonly static string letters = "QWERTYUIOPASDFGHJKLZXCVBNM<>?}{12345678990!@#$%^&*()";
        private readonly static string letters1 = "QWERTYUIOPASDFGHJKLZXCVBNM<>?}{12345678990!@#$%^&*()";
        private LinkedList<char> line;
        private LinkedList<char> line1;
        private readonly object lockConsole;
        private readonly byte[] lockLine;
        private readonly object lockLineLock;


        public Line(int length, int colomn, object lockConsole, ref byte[] lockLine, object lockLineLock)

        {
            Thread thread = new Thread(Draw);
            this.length = length;
            this.colomn = colomn;
            this.lockConsole = lockConsole;
            this.lockLine = lockLine;
            line = new LinkedList<char>();
            line1 = new LinkedList<char>();
            this.lockLineLock = new object();
            thread.Start();


        }

        private void Draw()
        {
            lock (lockLineLock)
            {
                lockLine[colomn]++;
            }
            for (int a = 0; a < Console.WindowHeight + length; a++)
            {
                Thread.Sleep(400);
                lock (lockConsole)
                {
                    if (a >= 0 && a < Console.WindowHeight)
                    {
                        if (a == 0)
                        {
                            a++;
                        }
                        Console.SetCursorPosition(colomn, a);
                        Console.ForegroundColor = ConsoleColor.White;
                        line.AddFirst(GetNewLetter());
                        line1.AddFirst(GetNewLetter1());

                        Console.Write(line.First.Value);
                        Console.Write(line1.First.Value);

                    }
                    if (line.Count >= 2 && a - 1 < Console.WindowHeight)
                    {

                        Console.SetCursorPosition(colomn, a - 1);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(line.First.Next.Value);
                        Console.Write(line.First.Value);
                    }
                    if (line.Count >= 3 && a - 2 < Console.WindowHeight)
                    {
                        if(a == 0)
                        {
                            a++;
                        }
                        else if(a== 1)
                        {
                            a++;
                        }
                        Console.SetCursorPosition(colomn, a - 2);
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.Write(line.Last.Value);
                        Console.Write(line1.Last.Value);

                        line.RemoveLast();
                        line1.RemoveLast();
                    }
                    if (length <= a)
                    {
                        Console.ResetColor();
                        Console.SetCursorPosition(colomn, a - length);
                        Console.Write(' ');
                        Console.Write(' ');
                    }

                }
            }
            lockLine[colomn]--;

        }
        private static Random rnd = new Random();
        private static char GetNewLetter()
        {
            lock (rnd)
            {
                return letters[rnd.Next(0, letters.Length)];
            }


        }
        private static char GetNewLetter1()
        {
            lock (rnd)
            {
                return letters1[rnd.Next(0, letters1.Length)];
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
                object lockLineLock=new object();

                while (true)
                {
                    Thread.Sleep(30);
                    int i = rnd.Next(0, lockLine.Length);
                    if (lockLine[i] == 1)
                    {
                        continue;
                    }
                    new Line(rnd.Next(4, 10), i, lockConsole, ref lockLine, lockLineLock);
                }
            }
        }

    }
}




        
        