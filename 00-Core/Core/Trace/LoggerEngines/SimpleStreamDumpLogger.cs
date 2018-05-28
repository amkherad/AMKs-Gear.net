using System;
using System.IO;
using System.Text;
using AMKsGear.Architecture.Trace;

namespace AMKsGear.Core.Trace.LoggerEngines
{
    public class SimpleStreamDumpLogger : ILoggerEngine
    {
        public static readonly Func<string, string, DateTime, string> DefaultFormatter =
            (msg, style, date) =>
                string.Format("{0} - {1}", date.ToString("G"), msg, style);

        private readonly string _path;
        private readonly Encoding _encoding;

        public Func<string, string, DateTime, string> Formatter { get; }
        public Func<string, Stream> StreamCreator { get; }
        public bool DisposeStream { get; set; }

        public SimpleStreamDumpLogger(string path, Func<string, Stream> streamCreator, Encoding encoding, Func<string, string, DateTime, string> formatter = null)
        {
            if (path == null) throw new ArgumentNullException(nameof(path));
            if (streamCreator == null) throw new ArgumentNullException(nameof(streamCreator));
            _path = path;
            StreamCreator = streamCreator;
            _encoding = encoding ?? Encoding.Unicode;
            Formatter = formatter ?? DefaultFormatter;
        }

        public void Write(string @string, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            var stream = StreamCreator(_path);
            using (var writer = new StreamWriter(stream))
                writer.Write(Formatter(@string, styles, DateTime.Now) + Environment.NewLine
                    + new string('-', 80) + Environment.NewLine);
            if (DisposeStream)
                stream.Dispose();
        }
        public void Write(Exception exception, string styles, ILoggingContext context,
            string callerMemberName,
            int callerLineNumber,
            string callerFilePath)
        {
            Write(exception + Environment.NewLine + new string('-', 80) + Environment.NewLine, styles, context, callerMemberName, callerLineNumber, callerFilePath);
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