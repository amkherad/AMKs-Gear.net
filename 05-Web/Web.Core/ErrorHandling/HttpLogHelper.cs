using System;
using System.Linq;
using System.Text;
using AMKsGear.Architecture.Trace;
using AMKsGear.Core.Trace;
using AMKsGear.Web.Core.MvcPatternAbstractApi;

namespace AMKsGear.Web.Core.ErrorHandling
{
    public static class HttpLogHelper
    {
        public static void OnActionExecuting(this LocalLogger loggerEngine, string category, LogLevel level, IActionContext actionContext)
        {
            loggerEngine.Write(log: GetLogString(
                "Executing '{0}' with folowing information:",
                level,
                actionContext,
                actionContext.ActionDescriptor,
                false
            ), category: category);
        }
        public static void OnActionExecuted(this LocalLogger loggerEngine, string category, LogLevel level, IActionContext actionContext)
        {
            loggerEngine.Write(log: GetLogString(
                "Action '{0}' execution has done with folowing information:",
                level,
                actionContext,
                actionContext.ActionDescriptor,
                true
            ), category: category);
        }

        public static string GetLogString(string header, LogLevel level, IActionContext actionContext, IActionDescriptor actionDescriptor, bool logResponse)
        {
            var sb = new StringBuilder();

            sb.Append(string.Format(header, actionDescriptor.ActionName)).AppendLine();

            var separator = ',' + Environment.NewLine;
            
            if (level >= LogLevel.Track)
            {
                sb.AppendLine("[Raw RawUrl]: ");
                sb.AppendLine(actionContext.Request.RawUrl);
                sb.AppendLine();
                // GET / HTTP/1.1
                //sb.Append($"{actionDescriptor.}");
                // write request query string.
            }

            if (level >= LogLevel.Debug)
            {
                sb.Append($"Action: {actionDescriptor.ActionName}(");
                sb.AppendLine();
                sb.Append(string.Join(separator, actionDescriptor.GetParameters().Select(x => $"{x.Type.Name} {x.Name}=")));
                sb.AppendLine();
                sb.Append(')');
            }
            else if (level >= LogLevel.Track)
            {
                sb.Append("Action: ");
                sb.Append(actionDescriptor.ActionName);
                sb.Append('(');
                sb.Append(string.Join(", ", actionDescriptor.GetParameters().Select(x => $"{x.Type.Name} {x.Name}")));
                sb.Append(')');
                sb.AppendLine();
            }

            if (level >= LogLevel.Track)
            {
                sb.AppendLine("--== Request ::Begin ==--");
            }

            if (level >= LogLevel.Track)
            {
                sb.AppendLine("[Raw Query Parameters]: ");
                sb.AppendLine(string.Join("," + Environment.NewLine, actionContext.Request.QueryString.ToKeyValuePairs().Select(x => $"{x.Key}: {x.Value}")));
                sb.AppendLine();

                sb.AppendLine("[Raw Form Data]: ");
                sb.AppendLine(string.Join("," + Environment.NewLine, actionContext.Request.Form.ToKeyValuePairs().Select(x => $"{x.Key}: {x.Value}")));
                sb.AppendLine();
            }

            if (level >= LogLevel.Track)
            {
                sb.AppendLine("[Raw Headers]: ");
                sb.AppendLine(string.Join("," + Environment.NewLine, actionContext.Request.Headers.ToKeyValuePairs().Select(x => $"{x.Key}: {x.Value}")));
                sb.AppendLine();
            }

            if (level >= LogLevel.Track)
            {
                sb.AppendLine("[Raw Body]: ");
                sb.AppendLine(actionContext.Request.GetRawBody());
                sb.AppendLine();
            }

            if (level >= LogLevel.Track)
            {
                sb.AppendLine("--== Request ::End ==--");
            }

            if (logResponse)
            {
                if (level >= LogLevel.Track)
                {
                    sb.AppendLine("--== Response ::Begin ==--");
                }

                if (level >= LogLevel.Track)
                {
                    sb.AppendLine("[Raw Headers]:");
                    sb.AppendLine(string.Join("," + Environment.NewLine, actionContext.Response.Headers.ToKeyValuePairs().Select(x => $"{x.Key}: {x.Value}")));
                    sb.AppendLine();
                }

                if (level >= LogLevel.Debug)
                {
                    //sb.Append("[Raw Body]:");
                    //sb.AppendLine(actionContext.Response);
                }

                if (level >= LogLevel.Track)
                {
                    sb.AppendLine("--== Response ::End ==--");
                }
            }

            return sb.ToString();
        }
    }
}