namespace EventTracker.BLL.Common
{
    public class NotificationMessages
    {
        public const string Greeting = "Hello,\n";
        public const string Regards = "Best Regards,\nEvent Tracker Team";

        public const string CreatedEventSubject = "Created an event  {0}";
        public const string CreatedEventBody = "You successfully created an event {0} - {1}";

        public const string DeletedEventSubject = "Deleted event {0}";

        public const string DeletedEventBody = Greeting +
                                               "The event {0} which you are subscripted to just got deleted. \nFor more information, contact the event creator." +
                                               Regards;
    }
}
