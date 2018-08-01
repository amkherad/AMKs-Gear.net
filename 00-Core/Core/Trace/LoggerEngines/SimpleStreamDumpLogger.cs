using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class SimpleStreamDumpLogger : ILogChannel
    {
        public static readonly Func<string, DateTime, string> DefaultFormatter =
            (msg, date) =>
                string.Format("{0} - {1}", date.ToString("G"), msg);

        private readonly string _path;
        private readonly Encoding _encoding;

        private object _lock = new object();

        public Func<string, DateTime, string> Formatter { get; }
        public Func<string, Stream> StreamCreator { get; }
        public bool DisposeStream { get; set; }

        public SimpleStreamDumpLogger(string path, Func<string, Stream> streamCreator, Encoding encoding,
            Func<string, DateTime, string> formatter = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (streamCreator == null) throw new ArgumentNullException(nameof(streamCreator));
            _path = path;
            StreamCreator = streamCreator;
            _encoding = encoding;
            Formatter = formatter ?? DefaultFormatter;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Write(
            string @string,
            ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            if (_encoding != null)
            {
                var unicode = Encoding.Unicode;
                var bytes = unicode.GetBytes(@string);
                @string = _encoding.GetString(Encoding.Convert(unicode, _encoding, bytes));
            }

            lock (_lock)
            {
                var stream = StreamCreator(_path);
                using (var writer = new StreamWriter(stream))
                    writer.Write(Formatter(@string, DateTime.Now) + Environment.NewLine
                                                                  + new string('-', 80) + Environment.NewLine);
                if (DisposeStream)
                    stream.Dispose();
            }
        }

        public void Dispose()
        {
        }

        public void LogString(string @string, ILoggingContext context, string callerMemberName = null,
            int callerLineNumber = 0,
            string callerFilePath = null)
        {
            Write(@string, context, callerMemberName, callerLineNumber, callerFilePath);
        }

        public void LogException(Exception exception, ILoggingContext context, string callerMemberName = null,
            int callerLineNumber = 0, string callerFilePath = null)
        {
            Write(exception.ToString(), context, callerMemberName, callerLineNumber, callerFilePath);
        }
    }
}