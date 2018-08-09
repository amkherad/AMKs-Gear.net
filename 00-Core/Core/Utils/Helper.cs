using System;
using AMKsGear.Architecture;
using AMKsGear.Architecture.Data.Types;

namespace AMKsGear.Core.Utils
{
    public static class Helper
    {
        public const int KiB = 1024;
        public const int MiB = 1024 * KiB;
        public const int GiB = 1024 * MiB;
        public const long TiB = 1024 * (long)GiB;
        
        #region StringComparer
        public static StringComparer StringComparerFromStringComparision(StringComparison comparision)
        {
            switch (comparision)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase;
                default:
                    throw new ArgumentOutOfRangeException(nameof(comparision), comparision, null);
            }
        }
        public static StringComparer StringComparerFromString(string comparer, StringComparer unspecifiedValue = null)
        {
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            var str = comparer.ToLower();
            switch (str)
            {
                case "default":
                case "current":
                case "currentculture":
                case "current_culture":
                case "null":
                case "sensetive":
                case "casesensetive":
                    return StringComparer.CurrentCulture;
                case "ignore":
                case "ignorecase":
                case "currentignore":
                case "caseinsensetive":
                case "insensetive":
                    return StringComparer.CurrentCultureIgnoreCase;
                case "ordinal":
                    return StringComparer.Ordinal;
                case "ordinalignore":
                case "ordinalignorecase":
                    return StringComparer.OrdinalIgnoreCase;
                default:
                    int intVal;
                    if (int.TryParse(str, out intVal))
                    {
                        switch (intVal)
                        {
                            case (int)StringComparison.CurrentCulture: return StringComparer.CurrentCulture;
                            case (int)StringComparison.CurrentCultureIgnoreCase: return StringComparer.CurrentCultureIgnoreCase;
                            case (int)StringComparison.Ordinal: return StringComparer.Ordinal;
                            case (int)StringComparison.OrdinalIgnoreCase: return StringComparer.OrdinalIgnoreCase;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                    return unspecifiedValue;
            }
        }
        #endregion
        #region Gender
        public static Gender GenderFromString(string genderString, Gender? unspecifiedValue = null)
        {
            if (genderString == null) throw new ArgumentNullException(nameof(genderString));
            var str = genderString.ToLower();
            switch (str)
            {
                case "male":
                case "m":
                case "ml":
                case "man":
                case "boy":
                case "men":
                case "gentleman":
                    return Gender.Male;
                case "female":
                case "f":
                case "fml":
                case "fl":
                case "girl":
                case "woman":
                case "women":
                case "lady":
                    return Gender.Female;
                default:
                    int intVal;
                    if (int.TryParse(str, out intVal))
                    {
                        switch (intVal)
                        {
                            case (int)Gender.Male:
                                return Gender.Male;
                            case (int)Gender.Female:
                                return Gender.Female;
                            default:
                                return unspecifiedValue ?? Gender.Unspecified;
                        }
                    }
                    return unspecifiedValue ?? Gender.Unspecified;
            }
        }
        #endregion
        #region SortingOrder
        public static SortingOrder SortingOrderFromString(string sortingOrderString, SortingOrder? unspecifiedValue = null)
        {
            if (sortingOrderString == null) throw new ArgumentNullException(nameof(sortingOrderString));
            var str = sortingOrderString.ToLower();
            switch (str)
            {
                case "as":
                case "ass":
                case "asc":
                case "ascending":
                case "ascend":
                case "a":
                    return SortingOrder.Ascending;
                case "des":
                case "dess":
                case "desc":
                case "descend":
                case "descending":
                case "d":
                    return SortingOrder.Descending;
                default:
                    int intVal;
                    if (int.TryParse(str, out intVal))
                    {
                        switch (intVal)
                        {
                            case (int)SortingOrder.Ascending:
                                return SortingOrder.Ascending;
                            case (int)SortingOrder.Descending:
                                return SortingOrder.Descending;
                            default:
                                return unspecifiedValue ?? SortingOrder.Unspecified;
                        }
                    }
                    return unspecifiedValue ?? SortingOrder.Unspecified;
            }
        }
        #endregion
        #region Free Helpers
        public static StringComparer GetStringComparerFromEnum(StringComparison mode)
        {
            switch (mode)
            {
                case StringComparison.CurrentCulture:
                    return StringComparer.CurrentCulture;
                case StringComparison.CurrentCultureIgnoreCase:
                    return StringComparer.CurrentCultureIgnoreCase;
                case StringComparison.Ordinal:
                    return StringComparer.Ordinal;
                case StringComparison.OrdinalIgnoreCase:
                    return StringComparer.OrdinalIgnoreCase;
                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }

        public static TResult RecursiveJoin<T, TResult>(this T state, Func<T, T> innerValue, Func<T, TResult, TResult> aggregateFunc)
        {
            if (state == null) throw new ArgumentNullException(nameof(state));
            if (innerValue == null) throw new ArgumentNullException(nameof(innerValue));
            if (aggregateFunc == null) throw new ArgumentNullException(nameof(aggregateFunc));

            var aggregate = aggregateFunc(state, default(TResult));

            var inner = state;
            while (inner != null)
            {
                state = inner;
                aggregate = aggregateFunc(state, aggregate);
                inner = innerValue(state);
            }
            return aggregate;
        }

        #endregion
    }
}