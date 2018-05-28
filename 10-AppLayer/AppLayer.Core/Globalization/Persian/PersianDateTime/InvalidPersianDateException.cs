﻿using System;

namespace AMKsGear.AppLayer.Core.Globalization.Persian.PersianDateTime
{
    public class InvalidPersianDateException : Exception
    {
        public InvalidPersianDateException()
            : base("Invalid persian datetime format.")
        {
        }

        public InvalidPersianDateException(string message)
            : base(message)
        {
        }
    }
}
