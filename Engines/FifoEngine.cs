using System.Collections.Generic;
using PageFaults.Utils;

namespace PageFaults.Engines
{
    public class FifoEngine : IEngine
    {
        private Queue<int> _queue { get; set; }
        private IList<int> _pages { get; set; }
        private IList<Event> _events { get; set; }
        private int _faults { get; set; }

        public FifoEngine()
        {
            _queue = new Queue<int>();
            _queue.Enqueue(0);
            _queue.Enqueue(1);
            _queue.Enqueue(2);
            _events = new List<Event>();
            _pages = new List<int>();
            _faults = 0;
        }
        public IList<Event> Process(List<int> sequence)
        {
            if (sequence == null) return null;

            for (int i = 0; i < sequence.Count; i++)
            {
                var val = sequence[i];
                if (_pages.Count < 3)
                {
                    var position = NextQueue();
                    _pages.Insert(position, val);
                    AddEvent();
                    continue;
                }

                if (!_pages.Contains(val))
                {
                    var position = NextQueue();
                    _pages[position] = val;
                    _faults += 1;
                }
                AddEvent();
            }
            return _events;
        }

        private void AddEvent()
        {
            _events.Add(new Event()
            {
                Pages = new List<int>(_pages),
                Faults = _faults
            });
        }

        private int NextQueue()
        {
            var val = _queue.Dequeue();
            _queue.Enqueue(val);

            return val;
        }
    }
}