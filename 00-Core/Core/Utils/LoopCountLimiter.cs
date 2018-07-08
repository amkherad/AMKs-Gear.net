using System;
using System.Threading;
using System.Threading.Tasks;

namespace AMKsGear.Core.Utils
{
    public delegate Task LoopCountLimiterAsyncDelay(double delay);
    
    public class LoopCountLimiter
    {
        public static readonly Action<double> Delay = val =>
        {
            var x = (int)val;
            if (x != 0) // to prevent yielding the current thread.
                Thread.Sleep(x); // we need at least x milliseconds (doesnt matter if gets more...)
        };
        public static readonly LoopCountLimiterAsyncDelay DelayAsync = async val =>
        {
            var x = (int)val;
            if (x != 0) // to prevent yielding the current thread.
                await Task.Delay(x); // we need at least x milliseconds (doesnt matter if gets more...)
        };

        double _delay = 0;
        int _frames = 0;
        DateTime _lastPin;
        DateTime _lastUpdate;
        double _interval = 0;

        public double DesiredInterval
        {
            get { return _interval; }
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException();
                _interval = value;
            }
        }

        public double Interval { get; private set; }
        public Action<double> Callback { get; }

        public LoopCountLimiter(int interval, Action<double> callback)
        {
            Interval = 0;
            DesiredInterval = interval;
            Callback = callback;
            _lastUpdate = DateTime.Now;
        }
        public LoopCountLimiter(int interval)
        {
            Interval = 0;
            DesiredInterval = interval;
            Callback = Delay;
            _lastUpdate = DateTime.Now;
        }

        public void Count() { Count(Callback); }
        public void Count(Action<double> callback)
        {
            if (callback == null) throw new ArgumentNullException(nameof(callback));
            _frames++;
            if (_interval > 0)
            {
                var desiredDiff = (1000d / _interval);
                var cDt = DateTime.Now;
                var diff = (cDt - _lastUpdate).TotalMilliseconds - _delay;

                if (diff >= desiredDiff)
                    _delay = 0;
                else
                    _delay = desiredDiff - diff;
                
                _lastUpdate = cDt;

                if ((cDt - _lastPin).TotalMilliseconds >= 1000)
                {
                    Interval = _frames;
                    _frames = 0;
                    _lastPin = cDt;
                }
            }
            else
            {
                _delay = 0;
            }
            callback(_delay);
        }
        
        public async Task CountAsync()
        {
            _frames++;
            if (_interval > 0)
            {
                var desiredDiff = (1000d / _interval);
                var cDt = DateTime.Now;
                var diff = (cDt - _lastUpdate).TotalMilliseconds - _delay;

                if (diff >= desiredDiff)
                    _delay = 0;
                else
                    _delay = desiredDiff - diff;
                
                _lastUpdate = cDt;

                if ((cDt - _lastPin).TotalMilliseconds >= 1000)
                {
                    Interval = _frames;
                    _frames = 0;
                    _lastPin = cDt;
                }
            }
            else
            {
                _delay = 0;
            }
            await DelayAsync((int)_delay);
        }
    }
}