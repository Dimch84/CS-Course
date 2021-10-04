using System;

namespace EventsAndDelegatesPubSubDemo
{
    public class VideoEncoder
    {
        // Define Delegate
        public delegate void VideoEncodedEventHandler(object source, VideoEncodeEventArgs e);

        // Define Event using Delegate
        public event VideoEncodedEventHandler VideoEncoded;

        public void Encode(Video video)
        {
            System.Console.WriteLine("Encoding Video "+video.Title);
            OnVideoEncoded(video);
        }

        private void OnVideoEncoded(Video video)
        {
            // Raise an Event
            if (VideoEncoded !=null)
            {
                VideoEncoded(this, new VideoEncodeEventArgs { Video = video });
            }

        }
    }
}