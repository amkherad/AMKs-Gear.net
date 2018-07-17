using System.Threading;

namespace AMKsGear.Core.Automation
{
	public class SpinCountExtender : SpinCountExtender<object>
    {
		public SpinCountExtender() { }
		public SpinCountExtender(object value)
        {
            Value = value;
        }
		public SpinCountExtender(object value, int initialCount)
			: base(value, initialCount)
        {
        }
    }
	public class SpinCountExtender<TValue>
    {
		private volatile int _spinCount;

		public SpinCountExtender() { }
		public SpinCountExtender(TValue value)
        {
            Value = value;
        }
		public SpinCountExtender(TValue value, int initialCount)
        {
            Value = value;
			_spinCount = initialCount;
        }

		public int Increment() { return Interlocked.Increment (ref _spinCount); }
		public int Increment(int count) { return Interlocked.Add (ref _spinCount, count); }

		public int Decrement() { return Interlocked.Decrement (ref _spinCount); }
		public int Decrement(int count) { return Interlocked.Add (ref _spinCount, -count); }

        public TValue Value { get; set; }
		public int SpinCount { get { return _spinCount; } }
    }
}
