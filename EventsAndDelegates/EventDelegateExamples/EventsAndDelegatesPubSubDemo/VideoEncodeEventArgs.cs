using System;

namespace EventsAndDelegatesPubSubDemo
{
    public class VideoEncodeEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }
}