using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace EventTracker.BLL.Entities
{
    public class MailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public static implicit operator MailRequest(MailRequest v)
        {
            throw new NotImplementedException();
        }
    }
}