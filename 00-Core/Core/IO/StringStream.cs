//using System;
//using System.IO;
//using System.Runtime;
//using System.Text;
//
//namespace AMKsGear.Core.IO
//{
//    public class StringStream : Stream
//    {
//        private readonly StringBuilder _stringBuilder;
//        private readonly bool _writable = true;
//        private int _pos = 0;
//        
//        
//        public Encoding Encoding { get; }
//        
//
//        public bool Writable => _writable;
//
//
//        public StringStream()
//        {
//            _stringBuilder = new StringBuilder();
//            
//            Encoding = Encoding.Default;
//        }
//
//        //[TargetedPatchingOptOut("")]
//        public StringStream(string buffer)
//        {
//            _stringBuilder = new StringBuilder(buffer);
//            
//            Encoding = Encoding.Default;
//        }
//
//        //[TargetedPatchingOptOut("")]
//        public StringStream(StringBuilder source)
//        {
//            if (source == null) throw new ArgumentNullException(nameof(source));
//            _stringBuilder = source;
//            
//            Encoding = Encoding.Default;
//        }
//
//        public StringStream(Encoding encoding)
//        {
//            Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
//            
//            _stringBuilder = new StringBuilder();
//        }
//
//        //[TargetedPatchingOptOut("")]
//        public StringStream(int capacity)
//        {
//            if (capacity < 0) throw new Exception(@"Negative capacity value determined.");
//            _stringBuilder = new StringBuilder(capacity);
//            
//            Encoding = Encoding.Default;
//        }
//
//        public StringStream(string buffer, bool writable)
//        {
//            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
//            _stringBuilder = new StringBuilder(buffer);
//            _writable = writable;
//        }
//
//        public StringStream(StringBuilder buffer, bool writable)
//        {
//            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
//            _stringBuilder = buffer;
//            _writable = writable;
//        }
//
//        public StringStream(string buffer, int index, int count)
//        {
//            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
//            _stringBuilder = new StringBuilder(buffer.Substring(index, count));
//            
//            Encoding = Encoding.Default;
//        }
//
//        public StringStream(StringBuilder buffer, int index, int count)
//        {
//            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
//            _stringBuilder = new StringBuilder(buffer.ToString().Substring(index, count));
//            
//            Encoding = Encoding.Default;
//        }
//
//        public StringStream(string buffer, int index, int count, bool writable)
//        {
//            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
//            _stringBuilder = new StringBuilder(buffer.Substring(index, count));
//            _writable = writable;
//            
//            Encoding = Encoding.Default;
//        }
//
//        public StringStream(StringBuilder buffer, int index, int count, bool writable)
//        {
//            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
//            _stringBuilder = new StringBuilder(buffer.ToString().Substring(index, count));
//            _writable = writable;
//            
//            Encoding = Encoding.Default;
//        }
//
//        public StringStream(string buffer, int index, int count, bool writable, Encoding encoding)
//        {
//            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
//            _stringBuilder = new StringBuilder(buffer.Substring(index, count));
//            _writable = writable;
//            
//            Encoding = encoding;
//        }
//
//
//        public override string ToString()
//        {
//            return _stringBuilder.ToString();
//        }
//
//        protected override void Dispose(bool disposing)
//        {
//            Dispose();
//        }
//
//        public override int GetHashCode()
//        {
//            return _stringBuilder.GetHashCode();
//        }
//
//
//        public override void Flush()
//        {
//            //
//        }
//
//        public override long Seek(long offset, SeekOrigin origin)
//        {
//            int offs;
//            checked
//            {
//                offs = (int) offset;
//            }
//
//            switch (origin)
//            {
//                case SeekOrigin.Begin:
//                    _pos = offs;
//                    break;
//                case SeekOrigin.End:
//                    _pos = _stringBuilder.Length - offs;
//                    break;
//                case SeekOrigin.Current:
//                default:
//                    _pos += offs;
//                    break;
//            }
//
//            return _pos;
//        }
//
//        public override void SetLength(long value)
//        {
//            checked
//            {
//                _stringBuilder.Length = (int) value;
//            }
//        }
//
//        public override int Read(byte[] buffer, int offset, int count)
//        {
//            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
//            if (count == 0) return 0;
//            if (offset < 0 || count < 0 || (offset + count) >= _stringBuilder.Length) throw new IndexOutOfRangeException();
//            
//            for (int i = 0; i < count; i += 2)
//            {
//                var @char = _stringBuilder[i];
//                buffer[i] = (byte) (@char & 0xff);
//                buffer[i + 1] = (byte) (@char & 0xff00 >> 0x100);
//            }
//
//            return count;
//        }
//
//        public override void Write(byte[] buffer, int offset, int count)
//        {
//            if (buffer == null) throw new ArgumentNullException(nameof(buffer));
//            if (offset < 0 || count < 0 || buffer.Length <= (offset + count)) throw new IndexOutOfRangeException();
//            if (count == 0) return;
//            var binaryBuffer = new byte[count];
//            for (int i = 0; i < count; i++)
//                binaryBuffer[i] = buffer[offset + i];
//            var buf = Encoding.Unicode.GetChars(binaryBuffer);
//            _stringBuilder.Insert(_pos, buf);
//            _pos += count;
//        }
//
//        public override bool CanRead
//        {
//            get { return true; }
//        }
//
//        public override bool CanSeek
//        {
//            get { return true; }
//        }
//
//        public override bool CanWrite
//        {
//            get { return _writable; }
//        }
//
//        public override long Length
//        {
//            get { return _stringBuilder.Length; }
//        }
//
//        public override long Position
//        {
//            get { return _pos; }
//            set
//            {
//                checked
//                {
//                    _pos = (int) value;
//                }
//            }
//        }
//    }
//}