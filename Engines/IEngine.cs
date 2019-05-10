using System.Collections.Generic;
using PageFaults.Utils;

namespace PageFaults.Engines
{
    public interface IEngine
    {
        IList<Event> Process(List<int> sequence);
    }
}