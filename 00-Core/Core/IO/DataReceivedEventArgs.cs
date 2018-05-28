using System;

namespace AMKsGear.Core.IO
{
    public delegate void DataReceivedEventHandler<TDataType>(object sender, DataReceivedEventArgs<TDataType> eventArgs);

    public class DataReceivedEventArgs<TDataType> : EventArgs
    {
        public TDataType Buffer { get; private set; }

        public DataReceivedEventArgs(TDataType buffer)
        {
            Buffer = buffer;
        }
    }
    public class BytesReceivedEventArgs : DataReceivedEventArgs<byte[]>
    {
        public BytesReceivedEventArgs(byte[] buffer) : base(buffer) { }
    }
}