namespace EventTracker.BLL.Common
{
    public class NotificationMessages
    {
        public const string Greeting = "Hello,\n";
        public const string Regards = "Best Regards,\nEvent Tracker Team";

        public const string DeletedEventSubject = "Deleted event {0}";
        public const string DeletedEventBody = 
            Greeting +
            "We regret to inform you that the event {0} (Held at {1}; {2} - {3}) which you are subscripted to just got deleted.\n" +
            Regards;

        public const string SubscribedToEventSubject = "Event {0} subscription";
        public const string SubscribedToEventBody =
            Greeting +
            "Congratulations! You successfully subscribed to event {0}.\n Event Details:\nLocation: {1}\nTime slot:{2}-{3}\n"
            + Regards;
    }
}