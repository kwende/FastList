using DontPanic.CV.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            int size = 512 * 424 * 1000; 

            IntList list = new IntList(size);
            Stopwatch sw = new Stopwatch();
            sw.Start(); 
            for(int c=0;c< size; c++)
            {
                list.Add(c);
            }
            sw.Stop();
            Console.WriteLine("Total time: " + sw.ElapsedMilliseconds + " ms");
            Console.WriteLine(list[100]);

            List<int> list2 = new List<int>(size);
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            for (int c = 0; c < size; c++)
            {
                list2.Add(c);
            }
            sw2.Stop();
            Console.WriteLine("Total time: " + sw2.ElapsedMilliseconds + "ms");

            Console.ReadLine();
        }
    }
}
