using System;

namespace EventsAndDelegatesPubSubDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Publisher
            VideoEncoder videoEncoder = new VideoEncoder();

            // Subscriber
            MailService mailService = new MailService();

            // Another MessageService
            MessageService messageService = new MessageService();

            // Register for Event listener
            videoEncoder.VideoEncoded += mailService.Email;
            videoEncoder.VideoEncoded += messageService.Text;

            Video video = new Video { Id = 1, Title = "Melody", Description = "Melody forever" };

            // Fire the Event
            videoEncoder.Encode(video);



            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
