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
        static void Main(string[] args)
        {
            object lockConsole = new object();
            byte[] buffer = new byte[Console.WindowWidth];
            object lockLineLock = new object();

            Line line = new Line(6, 6, lockConsole, ref buffer, lockLineLock);
            Line line2 = new Line(7, 12, lockConsole, ref buffer, lockLineLock);
            Thread thread3 = new Thread(line.Draw);
            Thread thread4 = new Thread(line2.Draw);
            Thread.Sleep(1000);

            thread3.Start();
            Thread.Sleep(200);
            thread4.Start();
            //Random rnd2 = new Random();
            //int num1 = rnd2.Next(1, 20);
            //Line[] line3 = new Line[6];
            //Thread[] threads = new Thread[line3.Length];



            //for (int i = 0; i < 6; i++)
            //{
              
            //       int row = i;
                    
            //        threads[i] = new Thread(() =>
            //        {
            //            line3[row] = new Line(num1+ i ,10, lockConsole, ref buffer, lockLineLock);
                        
            //        });
            //        threads[i].Start();

             

            //}
            //foreach (var thread in threads)
            //{
            //    thread.Join();
            //    Console.Write("   ");
            //}


        }

    }
}




        
        