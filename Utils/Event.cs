using System.Collections.Generic;

namespace PageFaults.Utils
{
    public class Event
    {
        public IList<int> Pages { get; set; }
        public int Faults { get; set; }
    }
}