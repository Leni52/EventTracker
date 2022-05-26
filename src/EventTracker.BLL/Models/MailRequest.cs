using System;
using System.Collections.Generic;

namespace EventTracker.BLL.Models
{
    public class MailRequest
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Recipients { get; set; }
    }
}