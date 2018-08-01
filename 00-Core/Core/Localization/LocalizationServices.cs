using System;
using System.Linq.Expressions;
using AMKsGear.Architecture.Localization;
using AMKsGear.Core.Automation.IoC;

namespace AMKsGear.Core.Localization
{
    public static partial class LocalizationServices
    {
        internal static string LocalizeFrameworkMessage(this string message, params object[] args)
        {
            return message;
        }

        public static string Localize(this string message, params object[] args)
        {
            return message;
        }

        public static ITranslateResult Format(Type localizationType, string format)
        {
            return null;
        }

        public static ITranslateResult Format(Type localizationType, string format, params object[] args)
        {
            return null;
        }

        public static string Format<TLocalization>(
            string format, object[] args)
            where TLocalization : ILocalizationModel
        {
            return format;
        }

        public static string Format<TLocalization>(
            string format,
            IFormatProvider formatProvider,
            params object[] args)
            where TLocalization : ILocalizationModel
        {
            return format;
//            return args == null
//                ? string.Format(format, formatProvider)
//                : string.Format(format, formatProvider, args);
        }

        public static ITranslateResult LazyFormat<TLocalization>(
            string format, object[] args)
            where TLocalization : ILocalizationModel
        {
            return new TranslateRawResult(format);
        }

        public static ITranslateResult LazyFormat<TLocalization>(
            string format,
            IFormatProvider formatProvider,
            params object[] args)
            where TLocalization : ILocalizationModel
        {
            return new TranslateRawResult(format);
//            return args == null
//                ? string.Format(format, formatProvider)
//                : string.Format(format, formatProvider, args);
        }



        public static string Format<TLocalization, TDefaultLocalization>(
            Func<TLocalization, string> propertyFunc)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
        {
            var format = propertyFunc(new TDefaultLocalization());

            return format;
        }

        public static string Format<TLocalization, TDefaultLocalization>(
            Func<TLocalization, string> propertyFunc, params object[] args)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
        {
            var format = propertyFunc(new TDefaultLocalization());

            return string.Format(format, args);
        }

        public static string Format<TLocalization, TDefaultLocalization>(
            Func<TLocalization, string> propertyFunc, IFormatProvider formatProvider,
            params object[] args)
            where TLocalization : ILocalizationModel
            where TDefaultLocalization : class, TLocalization, new()
        {
            var format = propertyFunc(new TDefaultLocalization());

            return string.Format(format, args);
        }
    }
}