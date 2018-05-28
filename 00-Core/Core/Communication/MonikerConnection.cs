using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AMKsGear.Architecture.Annotations;

namespace AMKsGear.Core.Communication
{
    public class MonikerConnection : IConnection
    {
        public string ConnectionString { get; private set; }
        public string Value { get; private set; }
        public string DeviceName { get; private set; }
        public Dictionary<string, string> Arguments { get; private set; }

        public MonikerConnection(IConnection connection) { Fill(connection.GetConnectionString()); }

        public MonikerConnection(string connection) { Fill(connection); }

        [Throws(typeof(ArgumentNullException))]
        private void Fill(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection)) throw new ArgumentNullException(nameof(connection));
            connection = connection.Trim();

            ConnectionString = connection;

            if (connection[0] == '@')
            {
                int parenthesisIndex = connection.IndexOf('(');
                if (parenthesisIndex < 0)
                {
                    Value = connection;
                    return;
                }
                DeviceName = connection.Substring(1, parenthesisIndex - 1);
                connection = connection.Substring(parenthesisIndex).Trim('(', ')');
                Value = connection;

                string[] args = connection.Split(',');
                if (args.Length == 0) return;
                Arguments = new Dictionary<string, string>(args.Length);
                foreach (var x in args)
                {
                    var colon = x.IndexOf(':');
                    if (colon < 0) continue;
                    var name = colon >= 0 ? x.Substring(0, colon) : string.Empty;
                    var m = Regex.Match(x, "(\".*\")");
                    if (!m.Success)
                    {
                        Arguments.Add(name, null);
                        continue;
                    }
                    Arguments.Add(name, m.Value.Trim('\"'));
                }
            }
            else
                Value = connection;

        }

        public string GetConnectionString(IFormatProvider formatProvider)
        { return GetConnectionString(); }

        public string GetConnectionString()
        {
            return ConnectionString;
        }

        public object ConnectionObject { get { return ConnectionString; } private set { ConnectionString = (string)value; } }
    }
}
