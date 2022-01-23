using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;

namespace DickinsDev.Utilities.Smtp
{
    public class SmtpMessage
    {
        public Guid Guid { get; set; } = Guid.NewGuid();

        public List<string> ToEmail { get; set; } = new List<string>();
        public List<string> CcEmail { get; set; } = new List<string>();
        public List<string> BccEmail { get; set; } = new List<string>();

        public string Subject { get; set; } = String.Empty;
        public bool isHtml { get; set; } = false;
        public string Body { get; set; } = String.Empty;

        public List<Attachment> Attachments { get; set; } = new List<Attachment>();

        public MimeMessage GetMimeMessage(MailboxAddress SenderAddress)
        {
            var mimeMessage = new MimeMessage();

            // Add sender address
            mimeMessage.From.Add(SenderAddress);

            // Add all recipients
            foreach (var emailAddress in ToEmail)
                mimeMessage.To.Add(MailboxAddress.Parse(emailAddress));
            foreach (var emailAddress in CcEmail)
                mimeMessage.Cc.Add(MailboxAddress.Parse(emailAddress));
            foreach (var emailAddress in BccEmail)
                mimeMessage.Bcc.Add(MailboxAddress.Parse(emailAddress));

            // Add message subject
            mimeMessage.Subject = Subject;

            var builder = new BodyBuilder();
            builder.TextBody = Body;

            mimeMessage.Body = builder.ToMessageBody();


            return mimeMessage;
        }

    }
}
