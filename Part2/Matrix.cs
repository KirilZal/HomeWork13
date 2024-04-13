using System.Data.Common;
using System.Runtime.InteropServices;

namespace Part2
{
    class Martix2
    {

        public class Line
        {
            private readonly int length;
            private readonly int colomn;
            private readonly int width;
            private readonly static string letters = "QWERTYUIOPASDFGHJKLZXCVBNM<>?}{12345678990!@#$%^&*()";
            private LinkedList<char> line;
            private readonly object lockConsole;
            private readonly byte[] lockLine;

            public Line(int length, int colomn, object lockConsole, ref byte[] lockLine,  int width)
            {
                Thread thread = new Thread(Draw);
                this.length = length;
                this.colomn = colomn;
                this.width = width;
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
                    
                        if (a < line.Count)
                        {
                            
                            for (int j = 0; j < Console.WindowWidth; j++)
                            {
                                if (a < Console.WindowHeight)
                                {
                                    Console.SetCursorPosition(colomn + j, a);  
                                    Console.ForegroundColor = ConsoleColor.White;
                                    line.AddFirst(GetNewLetter());
                                    Console.Write(line.First.Value);
                                }

                                if (line.Count >= width && a - 1 < Console.WindowHeight)
                                {
                                    Console.SetCursorPosition(colomn + j, a - 1);
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.Write(line.First.Next.Value);
                                }
                                if (line.Count >=width && a - 2 < Console.WindowHeight)
                                {
                                    Console.SetCursorPosition(colomn, (a * 2 + j) - 2);
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
                    }
                    lockLine[colomn]--;
                }
            }
           
            

            private static char GetNewLetter()
            {
                var rnd = new Random();
                return letters[rnd.Next(0, letters.Length)];

            }
            static void Main(string[] args)
            {
                object lockConsole = new object();
                byte[] buffer = new byte[Console.WindowHeight];
                int withd = 5;

                Line line = new Line(6, 6, lockConsole, ref buffer, withd);


            }
        }
    }
}
       

