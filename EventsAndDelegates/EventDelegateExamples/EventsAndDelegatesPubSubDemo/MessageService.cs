using System;

namespace EventsAndDelegatesPubSubDemo
{
    public class MessageService
    {
        public void Text(object source, VideoEncodeEventArgs e)
        {
            Console.WriteLine("Sending Text.. for "+e.Video.Title);
        }
    }
}