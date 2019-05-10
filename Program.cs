using System;
using System.Collections.Generic;
using System.Text;
using PageFaults.Engines;
using PageFaults.Utils;

namespace PageFaults
{
    class Program
    {
        static void Main(string[] args)
        {
            var sequence = new List<int>() { 1, 2, 3, 4, 2, 1, 5, 6, 2, 1, 2, 3, 7, 6, 3, 2, 1, 2, 3, 6 };
            // var sequence = new List<int>() { 7, 0, 1, 2, 0, 3, 0, 4, 2, 3, 0, 3, 2, 1, 2, 0, 1, 7, 0, 1 };
            // var sequence = new List<int>() { 7, 0, 1, 2, 0, 3, 0, 4, 2, 3, 0, 3, 2, 1, 2, 0, 1, 7, 0, 1, 1, 2, 3, 4, 2, 1, 5, 6, 2, 1, 2, 3, 7, 6, 3, 2, 1, 2, 3, 6 };
            var lru = new LruEngine();
            var fifo = new FifoEngine();
            var opt = new OptimalEngine();

            var LRU_events = lru.Process(sequence);
            var FIFO_events = fifo.Process(sequence);
            var OPT_events = opt.Process(sequence);

            DisplayEvents("LRU", LRU_events);
            DisplayEvents("FIFO", FIFO_events);
            DisplayEvents("OPTIMAL", OPT_events);
        }

        static void DisplayEvents(string name, IList<Event> events)
        {
            var faults = 0;
            System.Console.WriteLine("***********************************************");
            System.Console.WriteLine(name);
            System.Console.WriteLine("***********************************************");
            for (int i = 0; i < 3; i++)
            {
                var sb = new StringBuilder();
                sb.Append("| ");
                for (int j = 0; j < events.Count; j++)
                {
                    var e = events[j];
                    if (e.Pages.Count > i)
                    {
                        sb.Append(events[j].Pages[i]);
                    }
                    else
                    {
                        sb.Append(" ");
                    }
                    if (j < events.Count - 1)
                    {
                        sb.Append(" | ");
                    }
                    else
                    {
                        sb.Append(" |");
                    }
                    faults = e.Faults;
                }
                System.Console.WriteLine(sb.ToString());
                sb.Clear();
            }
            System.Console.WriteLine("***********************************************");
            System.Console.WriteLine($"{name} Algorithm resulted in {faults} faults.");
            System.Console.WriteLine("***********************************************");
            System.Console.WriteLine(" ");
            System.Console.WriteLine(" ");
        }
    }
}
// Implement LRU, FIFO, Optimal  page replacement algorithm.
// 1.Consider the following page reference string:1, 2, 3, 4, 2, 1, 5, 6, 2, 1, 2, 3, 7, 6, 3, 2, 1, 2, 3, 6
// Assuming demand paging with fourframes, how many page faultswould occur for the following replacement algorithms?
// LRU replacement
// FIFO replacement
// Optimal replacement