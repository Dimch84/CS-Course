using System;

namespace EventsAndDelegatesMessendger
{
    class Program
    {
        static void Main(string[] args)
        {
            Account a1 = new Account("Joe");
            Account a2 = new Account("Bill");

            SpyNotifications spy = new SpyNotifications(a2);

            a1.Send(a1.CreateMessage("Hi There! How are you?", a2));
            a1.Send(a1.CreateMessage("I'm Joe from SPb", a2));
            a2.Send(a2.CreateMessage("Hi Joe! I'm Bill. All is fine. Thanks!", a1));
            a1.Send(a1.CreateMessage("Hi Bill. Great!", a2));

            a1.ShowDialog(a2.Username);
        }
    }
}
