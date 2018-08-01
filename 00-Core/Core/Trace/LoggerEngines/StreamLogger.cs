using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class StreamLogger : ILogChannel
    {
        private readonly Stream _stream;
        private readonly StreamWriter _writer;
        private readonly Encoding _encoding;

        private object _lock = new object();

        public Func<string, DateTime, string> Formatter { get; }

        public StreamLogger(Stream stream, Encoding encoding, Func<string, DateTime, string> formatter = null)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            _encoding = encoding;
            _stream = stream;
            _writer = new StreamWriter(stream, _encoding);
            Formatter = formatter ??
                ((msg, date) =>
                    string.Format("{0} - {1}", date, msg));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Write(string @string, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            byte[] log;
            if (_encoding == null)
            {
                log = Encoding.Unicode.GetBytes(@string);
            }
            else
            {
                var unicode = Encoding.Unicode;
                var bytes = unicode.GetBytes(@string);
                log = Encoding.Convert(unicode, _encoding, bytes);
            }

            lock (_lock)
            {
                _writer.Write(log);
            }
        }
        
        public void Dispose()
        {

        }

        public void LogString(
            string @string,
            ILoggingContext context,
            string callerMemberName = null,
            int callerLineNumber = 0,
            string callerFilePath = null)
        {
            Write(@string, context, callerMemberName, callerLineNumber, callerFilePath);
        }

        public void LogException(
            Exception exception,
            ILoggingContext context,
            string callerMemberName = null,
            int callerLineNumber = 0,
            string callerFilePath = null)
        {
            Write(exception.ToString(), context, callerMemberName, callerLineNumber, callerFilePath);
        }
    }
}