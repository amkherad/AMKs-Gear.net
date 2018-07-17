using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AMKsGear.Core.Trace;

namespace AMKsGear.Core.TraceTools.TestBuilderPattern
{
    public static class TestBuilderExtensions
    {
        #region Behavior
        public static TestBuilderContext SetPrivateLogger(this TestBuilderContext context,
            LocalLogger localLogger)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (localLogger == null) throw new ArgumentNullException(nameof(localLogger));
            context.LocalLogger = localLogger; // Can be null.
            return context;
        }
        public static TestBuilderContext RegisterAssertionResult(this TestBuilderContext context,
            string name, bool result)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            context.AssertionResults.Add(name, result);

            var log = (string)Localization.Format<ITestingLocalization, DefaultTestingLocalization>(
                x => x.AssertionDoneWithResultOf, name, result.ToString());

            var style = result ? Logger.SuccessStyle : Logger.ErrorStyle;
            context.LocalLogger?.Write(log, style);
            Test.Logger?.Write(log, style);

            return context;
        }

        public static bool IsTestSuccessfull(this TestBuilderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            return context.AssertionResults.All(x => x.Value);
        }
        public static bool IsTestSuccessfull(this TestBuilderContext context, out IEnumerable<string> errors)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            var errorAsserts = context.AssertionResults.Where(x => !x.Value).ToArray();

            if (!errorAsserts.Any())
            {
                errors = new string[0];
                return true;
            }

            errors = errorAsserts.Select(x => x.Key);
            return false;
        }
        #endregion

        #region Actions

        private static T _do<T>(T context, string name, Action<T> action)
            where T : TestBuilderContext
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (action == null) throw new ArgumentNullException(nameof(action));
            var sw = new Stopwatch();
            sw.Start();
            action(context);
            sw.Stop();
            context.ActionHistory.Add(name);

            var log = $"Action '{name}' done, duration(ticks): {sw.ElapsedTicks}, duration(mil.): {sw.ElapsedMilliseconds}";

            context.LocalLogger?.Write(log);
            Test.Logger?.Write(log);

            return context;
        }

        public static TestBuilderContext Do(this TestBuilderContext context,
            string name, Action<TestBuilderContext> action)
                => _do(context, name, action);
        public static TestBuilderContext<TArg1> Do<TArg1>(
            this TestBuilderContext<TArg1> context,
            string name, Action<TestBuilderContext<TArg1>> action)
                => _do(context, name, action);
        public static TestBuilderContext<TArg1, TArg2> Do<TArg1, TArg2>(
            this TestBuilderContext<TArg1, TArg2> context,
            string name, Action<TestBuilderContext<TArg1, TArg2>> action)
                => _do(context, name, action);
        public static TestBuilderContext<TArg1, TArg2, TArg3> Do<TArg1, TArg2, TArg3>(
            this TestBuilderContext<TArg1, TArg2, TArg3> context,
            string name, Action<TestBuilderContext<TArg1, TArg2, TArg3>> action)
                => _do(context, name, action);
        public static TestBuilderContext<TArg1, TArg2, TArg3, TArg4> Do<TArg1, TArg2, TArg3, TArg4>(
            this TestBuilderContext<TArg1, TArg2, TArg3, TArg4> context,
            string name, Action<TestBuilderContext<TArg1, TArg2, TArg3, TArg4>> action)
                => _do(context, name, action);
        #endregion

        #region Conditions
        public static TestBuilderContext TestTag(this TestBuilderContext context,
            string name, object value, IEqualityComparer comparer = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (comparer == null)
                context.RegisterAssertionResult(name, context.Tag?.Equals(value) ?? value == null);
            else
                context.RegisterAssertionResult(name, comparer.Equals(context.Tag, value));

            context.ActionHistory.Add(name);
            return context;
        }
        public static TestBuilderContext<TArg> TestArg1<TArg>(this TestBuilderContext<TArg> context,
            string name, object value, IEqualityComparer comparer = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (comparer == null)
                context.RegisterAssertionResult(name, context.Arg1?.Equals(value) ?? value == null);
            else
                context.RegisterAssertionResult(name, comparer.Equals(context.Arg1, value));

            context.ActionHistory.Add(name);
            return context;
        }
        public static TestBuilderContext<TArg1, TArg2> TestArg2<TArg1, TArg2>(this TestBuilderContext<TArg1, TArg2> context,
            string name, object value, IEqualityComparer comparer = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (comparer == null)
                context.RegisterAssertionResult(name, context.Arg2?.Equals(value) ?? value == null);
            else
                context.RegisterAssertionResult(name, comparer.Equals(context.Arg2, value));

            context.ActionHistory.Add(name);
            return context;
        }
        public static TestBuilderContext<TArg1, TArg2, TArg3> TestArg3<TArg1, TArg2, TArg3>(this TestBuilderContext<TArg1, TArg2, TArg3> context,
            string name, object value, IEqualityComparer comparer = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (comparer == null)
                context.RegisterAssertionResult(name, context.Arg3?.Equals(value) ?? value == null);
            else
                context.RegisterAssertionResult(name, comparer.Equals(context.Arg3, value));

            context.ActionHistory.Add(name);
            return context;
        }
        public static TestBuilderContext<TArg1, TArg2, TArg3, TArg4> TestArg4<TArg1, TArg2, TArg3, TArg4>(this TestBuilderContext<TArg1, TArg2, TArg3, TArg4> context,
            string name, object value, IEqualityComparer comparer = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (comparer == null)
                context.RegisterAssertionResult(name, context.Arg4?.Equals(value) ?? value == null);
            else
                context.RegisterAssertionResult(name, comparer.Equals(context.Arg4, value));

            context.ActionHistory.Add(name);
            return context;
        }
        #endregion

        #region Conditions 1
        public static TestBuilderContext<TArg> TestArg1<TArg>(this TestBuilderContext<TArg> context,
            string name,
            Func<TArg, object> argValue,
            object value, IEqualityComparer comparer = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (argValue == null) throw new ArgumentNullException(nameof(argValue));

            if (comparer == null)
                context.RegisterAssertionResult(name, argValue(context.Arg1)?.Equals(value) ?? value == null);
            else
                context.RegisterAssertionResult(name, comparer.Equals(argValue(context.Arg1), value));

            context.ActionHistory.Add(name);
            return context;
        }
        public static TestBuilderContext<TArg1, TArg2> TestArg2<TArg1, TArg2>(this TestBuilderContext<TArg1, TArg2> context,
            string name,
            Func<TArg2, object> argValue,
            object value, IEqualityComparer comparer = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (comparer == null)
                context.RegisterAssertionResult(name, argValue(context.Arg2)?.Equals(value) ?? value == null);
            else
                context.RegisterAssertionResult(name, comparer.Equals(argValue(context.Arg2), value));

            context.ActionHistory.Add(name);
            return context;
        }
        public static TestBuilderContext<TArg1, TArg2, TArg3> TestArg3<TArg1, TArg2, TArg3>(this TestBuilderContext<TArg1, TArg2, TArg3> context,
            string name,
            Func<TArg3, object> argValue,
            object value, IEqualityComparer comparer = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (comparer == null)
                context.RegisterAssertionResult(name, argValue(context.Arg3)?.Equals(value) ?? value == null);
            else
                context.RegisterAssertionResult(name, comparer.Equals(argValue(context.Arg3), value));

            context.ActionHistory.Add(name);
            return context;
        }
        public static TestBuilderContext<TArg1, TArg2, TArg3, TArg4> TestArg4<TArg1, TArg2, TArg3, TArg4>(this TestBuilderContext<TArg1, TArg2, TArg3, TArg4> context,
            string name,
            Func<TArg4, object> argValue,
            object value, IEqualityComparer comparer = null)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            if (comparer == null)
                context.RegisterAssertionResult(name, argValue(context.Arg4)?.Equals(value) ?? value == null);
            else
                context.RegisterAssertionResult(name, comparer.Equals(argValue(context.Arg4), value));

            context.ActionHistory.Add(name);
            return context;
        }
        #endregion

        #region Test Controllers
        public static bool Done(this TestBuilderContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var result = context.AssertionResults.All(x => x.Value);
            
            var resultStr = Localization.Format<ITestingLocalization, DefaultTestingLocalization>(
                result ? (Func<ITestingLocalization, string>)(x => x.Successful) : (x => x.Failed));
            
            var log = Localization.Format<ITestingLocalization, DefaultTestingLocalization>(
                x => x.TestDoneWithResultOf, context.Name, (string)resultStr);

            context.LocalLogger?.Write(log, Logger.BoldStyle);
            Test.Logger?.Write(log, Logger.BoldStyle);
            
            return result;
        }
        #endregion
    }
}