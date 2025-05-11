using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Student_Accommodation_Hub.Models
{
    public class EmailSystemModel
    {
        public string ToEmail { get; set; }

        // Optional: Name of the recipient
        public string ToName { get; set; }

        // Required: Subject line of the email
        public string Subject { get; set; }

        // Required: Body content of the email (can be HTML or plain text)
        public string Body { get; set; }

        // Required: OTP code being sent
        public string OTP { get; set; }

        // Optional: Sender email (if dynamic); default can be configured in app settings
        public string FromEmail { get; set; }

        // Optional: Sender display name
        public string FromName { get; set; }

        public bool IsHtml { get; set; } = false;

    }
}