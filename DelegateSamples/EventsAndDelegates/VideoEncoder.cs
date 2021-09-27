using System;
using System.Threading;

namespace EventsAndDelegates
{
    public class VideoEventArgs : EventArgs
    {
        public Video Video { get; set; }
    }

    public class VideoEncoder
    {
        public event EventHandler<VideoEventArgs> VideoEncoded;

        public void Encode(Video video)
        {
            Console.WriteLine("Encoding Video");
            Thread.Sleep(3000);
            Console.WriteLine("Encoded Video...");

            OnVideoEncoded(video);
        }

        //MSDN Convention, should be protected, virtual and void, and should start with the word on and then the name of the event
        protected virtual void OnVideoEncoded(Video video)
        {
            if(VideoEncoded != null)
            {
                //VideoEncoded(this, EventArgs.Empty);
                VideoEncoded(this, new VideoEventArgs() { Video = video });
            }
        }
    }
}
