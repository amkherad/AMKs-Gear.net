using System;
using System.IO;
using System.Runtime;
using System.Text;

namespace AMKsGear.Core.IO
{
    public class StringStream : Stream
    {
        private readonly StringBuilder _builder;
        private readonly bool _writable = true;
        private int _pos = 0;
        private bool _publiclyVisible = false;

        public bool Writable { get { return _writable; } }


        public StringStream() { _builder = new StringBuilder(); }

        //[TargetedPatchingOptOut("")]
        public StringStream(string buffer)
        {
            _builder = new StringBuilder(buffer);
        }
        //[TargetedPatchingOptOut("")]
        public StringStream(StringBuilder source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            _builder = source;
        }

        //[TargetedPatchingOptOut("")]
        public StringStream(int capacity)
        {
            if (capacity < 0) throw new Exception(@"Negative capacity value determined.");
            _builder = new StringBuilder(capacity);
        }

        public StringStream(string buffer, bool writable)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            _builder = new StringBuilder(buffer);
            _writable = writable;
        }
        public StringStream(StringBuilder buffer, bool writable)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            _builder = buffer;
            _writable = writable;
        }

        public StringStream(string buffer, int index, int count)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            _builder = new StringBuilder(buffer.Substring(index, count));
        }
        public StringStream(StringBuilder buffer, int index, int count)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            _builder = new StringBuilder(buffer.ToString().Substring(index, count));
        }

        public StringStream(string buffer, int index, int count, bool writable)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            _builder = new StringBuilder(buffer.Substring(index, count));
            _writable = writable;
        }
        public StringStream(StringBuilder buffer, int index, int count, bool writable)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            _builder = new StringBuilder(buffer.ToString().Substring(index, count));
            _writable = writable;
        }

        public StringStream(string buffer, int index, int count, bool writable, bool publiclyVisible)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            _builder = new StringBuilder(buffer.Substring(index, count));
            _writable = writable;
            _publiclyVisible = publiclyVisible;
        }


        public override string ToString() { return _builder.ToString(); }
        protected override void Dispose(bool disposing) { Dispose(); }
        public override int GetHashCode() { return _builder.GetHashCode(); }


        public override void Flush()
        {
            //
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            int offs;
            checked { offs = (int)offset; }
            switch (origin)
            {
                case SeekOrigin.Begin:
                    _pos = offs;
                    break;
                case SeekOrigin.End:
                    _pos = _builder.Length - offs;
                    break;
                case SeekOrigin.Current:
                default:
                    _pos += offs;
                    break;
            }
            return _pos;
        }

        public override void SetLength(long value) { checked { _builder.Length = (int)value; } }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (count == 0) return 0;
            if (offset < 0 || count < 0 || (offset + count) >= _builder.Length) throw new IndexOutOfRangeException();
            for (int i = 0; i < count; i++)
            {
                buffer[i]     = (byte)((_builder[i]) & 0xff);
                buffer[i + 1] = (byte)((_builder[i]) & 0x00ff >> 0x100);
            }
            return count;
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
            if (offset < 0 || count < 0 || buffer.Length <= (offset + count)) throw new IndexOutOfRangeException();
            if (count == 0) return;
            var binaryBuffer = new byte[count];
            for (int i = 0; i < count; i++)
                binaryBuffer[i] = buffer[offset + i];
            var buf = Encoding.Unicode.GetChars(binaryBuffer);
            _builder.Insert(_pos, buf);
            _pos += count;
        }

        public override bool CanRead { get { return true; } }
        public override bool CanSeek { get { return true; } }
        public override bool CanWrite { get { return _writable; } }

        public override long Length { get { return _builder.Length; } }

        public override long Position
        {
            get { return _pos; }
            set { checked { _pos = (int)value; } }
        }
    }
}
