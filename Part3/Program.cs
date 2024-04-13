using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Part3
{
    class Program
    {
        static void Main(string[] args)
        {
            Kvadrat kvadrat = new Kvadrat();
            int[] MyArr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };
            IEnumerable<int> result = kvadrat.Getnum(MyArr);
            foreach (int i in result)
            {
                Console.WriteLine(i);
            }
        }
    }
}
