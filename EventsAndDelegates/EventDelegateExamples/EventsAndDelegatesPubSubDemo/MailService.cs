using System;

namespace EventsAndDelegatesPubSubDemo
{
    public class MailService
    {
        public void Email(object source, VideoEncodeEventArgs args)
        {
            Console.WriteLine("Sending Email...for "+args.Video.Title);
        }
    }
}