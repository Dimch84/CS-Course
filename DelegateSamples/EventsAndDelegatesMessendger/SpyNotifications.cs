using System;

namespace EventsAndDelegatesMessendger
{
    public class SpyNotifications
    {
        public Account Observable { get; set; }

        public SpyNotifications(Account observable)
        {
            Observable = observable;
            Observable.OnSend += Detector;
        }

        public void Detector(object sender, OnSendEventArgs e)
        {
            Console.WriteLine($"Detected message sending...\n From: {e.From}\n To: {e.To}\n Text: {e.Text}", ConsoleColor.Red);
        }
    }
}
