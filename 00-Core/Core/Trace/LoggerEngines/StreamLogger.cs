using System;
using System.IO;
using System.Text;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class StreamLogger : ILoggerEngine
    {
        private readonly Stream _stream;
        private readonly StreamWriter _writer;
        private readonly Encoding _encoding;

        public Func<string, string, DateTime, string> Formatter { get; }

        public StreamLogger(Stream stream, Encoding encoding, Func<string, string, DateTime, string> formatter = null)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            _encoding = encoding ?? Encoding.Unicode;
            _stream = stream;
            _writer = new StreamWriter(stream, _encoding);
            Formatter = formatter ??
                ((msg, style, date) =>
                    string.Format("{0} - {1}", date, msg, style));
        }

        public void Write(string @string, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            var serialized = _encoding.GetBytes(Formatter(@string, styles, DateTime.Now));
            _writer.Write(serialized);
        }
        public void Write(Exception exception, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            Write(exception.ToString(), styles, context, callerMemberName, callerLineNumber, callerFilePath);
        }
        public void Feed(int repeat)
        {
            Write(new StringBuilder(repeat).Insert(0, Environment.NewLine, repeat).ToString(), string.Empty, null, null, 0, null);
        }

        public void Dispose()
        {

        }
    }
}