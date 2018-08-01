using System;

namespace AMKsGear.AppLayer.Core.Localization.Persian.PersianDateTime
{
    public class InvalidPersianDateFormatException : Exception
    {
        public InvalidPersianDateFormatException(string message)
            : base(message)
        {
        }

        public InvalidPersianDateFormatException()
            : base()
        { 
        }
    }
}
