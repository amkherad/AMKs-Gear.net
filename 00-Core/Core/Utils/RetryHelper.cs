using System;

namespace AMKsGear.Core.Utils
{
    public enum RetryHelperStates
    {
        Done,
        UnDone,
        DoneContinue,
        
        Failed = DoneContinue,
    }
    public class RetryHelper
    {
        public int MaxTries { get; protected set; }
        public int Tries { get; protected set; }

        public RetryHelperStates State { get; protected set; } = RetryHelperStates.UnDone;
        public Exception LastException { get; protected set; }

        private readonly bool _continueOnDone = false;

        public RetryHelper(int maxRetries = -1)
        {
            MaxTries = maxRetries;
        }
        public RetryHelper(bool continueOnDone, int maxRetries = -1)
        {
            MaxTries = maxRetries;
            _continueOnDone = continueOnDone;
        }

        public void Done(bool @continue = false)
        {
            State = @continue && _continueOnDone ? RetryHelperStates.DoneContinue : RetryHelperStates.Done;

            Tries += 1;
        }
        public void Catch(Exception ex)
        {
            if (ex == null) throw new ArgumentNullException(nameof(ex));
            LastException = ex;

            State = RetryHelperStates.Failed;
            Tries += 1;
        }
        public void Fail()
        {
            State = RetryHelperStates.Failed;
            Tries += 1;
        }

        public bool IsDone()
        {
            return
                State == RetryHelperStates.Done ||
                (/*MaxTries == -1 || */Tries >= MaxTries);
        }
    }
}