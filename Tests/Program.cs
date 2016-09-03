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
        static void ReallocationTiming()
        {
            int size = 512 * 424 * 1000;

            IntList list = new IntList(size / 1000);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int c = 0; c < size; c++)
            {
                list.Add(c);
            }
            sw.Stop();
            Console.WriteLine("IntList Reallocation Time: " + sw.ElapsedMilliseconds + " ms");

            List<int> list2 = new List<int>(size / 1000);
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            for (int c = 0; c < size; c++)
            {
                list2.Add(c);
            }
            sw2.Stop();
            Console.WriteLine("List<int> Reallocation Time: " + sw2.ElapsedMilliseconds + "ms");

            //Console.ReadLine();
        }

        static void NoReallocationTime()
        {
            int size = 512 * 424 * 1000;

            IntList list = new IntList(size);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int c = 0; c < size; c++)
            {
                list.UnsafeAdd(c);
            }
            sw.Stop();
            Console.WriteLine("IntList Time: " + sw.ElapsedMilliseconds + " ms");

            List<int> list2 = new List<int>(size);
            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            for (int c = 0; c < size; c++)
            {
                list2.Add(c);
            }
            sw2.Stop();
            Console.WriteLine("List<int> Time: " + sw2.ElapsedMilliseconds + "ms");
        }

        static void Main(string[] args)
        {
            for(int c=0;c<100;c++)
            {
                NoReallocationTime();
                ReallocationTiming();
            }

            Console.ReadLine();    
        }
    }
}
