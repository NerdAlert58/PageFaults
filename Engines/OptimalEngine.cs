using System;
using System.Collections.Generic;
using PageFaults.Utils;

namespace PageFaults.Engines
{
    public class OptimalEngine : IEngine
    {
        private IList<int> _queue { get; set; }
        private IList<int> _pages { get; set; }
        private IList<Event> _events { get; set; }
        private IList<int> _sequence { get; set; }
        private int _faults { get; set; }

        public OptimalEngine()
        {
            _queue = new List<int>();
            _queue.Add(0);
            _queue.Add(1);
            _queue.Add(2);
            _events = new List<Event>();
            _pages = new List<int>();
            _faults = 0;
        }
        public IList<Event> Process(List<int> sequence)
        {
            _sequence = new List<int>(sequence);
            while (_sequence.Count > 0)
            {
                var val = _sequence[0];
                _sequence.RemoveAt(0);
                if (_pages.Contains(val))
                {
                    AddEvent();
                    continue;
                }
                if (_pages.Count < 3)
                {
                    _pages.Add(val);
                    AddEvent();
                    continue;
                }
                var item = LocateFarthest();
                _pages[_pages.IndexOf(item)] = val;
                _faults += 1;
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

        private int LocateFarthest()
        {
            if (_pages.Count > 3)
            {
                throw new Exception("Impossible to have more than 3 pages.");
            }
            if (_pages.Count < 3)
            {
                return _pages.Count;
            }

            var val0 = _pages[0];
            var val1 = _pages[1];
            var val2 = _pages[2];

            var dist0 = _sequence.IndexOf(val0);
            if (dist0 == -1)
            {
                return val0;
            }
            var dist1 = _sequence.IndexOf(val1);
            if (dist1 == -1)
            {
                return val1;
            }
            var dist2 = _sequence.IndexOf(val2);
            if (dist2 == -1)
            {
                return val2;
            }

            if (dist0 > dist1)
            {
                if (dist0 > dist2)
                {
                    return val0;
                }
                else
                {
                    return val2;
                }
            }
            else
            {
                if (dist1 > dist2)
                {
                    return val1;
                }
                else
                {
                    return val2;
                }
            }
        }
    }
}