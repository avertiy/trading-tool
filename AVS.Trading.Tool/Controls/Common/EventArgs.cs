using System;

namespace AVS.Trading.Tool.Controls.Common
{
    public class EventArgs<T> : EventArgs
    {
        public T Value { get; set; }

        public EventArgs()
        {
        }

        public EventArgs(T value)
        {
            Value = value;
        }
    }
}