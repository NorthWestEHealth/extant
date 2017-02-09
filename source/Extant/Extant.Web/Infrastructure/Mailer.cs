//-----------------------------------------------------------------------
// <copyright file="Mailer.cs" company="North West e-Health">
// Copyright (c) North West e-Health 2011. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
using System;
using System.Net.Mail;
using System.Web.Configuration;
using System.Net.Configuration;
using log4net;

namespace Extant.Web.Infrastructure
{
    public interface IMailer
    {
        bool Send(string to, string subject, string body);
    }

    public class Mailer : IMailer
    {
        protected readonly static ILog log = log4net.LogManager.GetLogger(typeof(Mailer));

        private readonly SmtpClient MailClient;
        private readonly string EmailFrom;

        public Mailer()
        {
            MailClient = new SmtpClient();
            EmailFrom = (WebConfigurationManager.GetSection("system.net/mailSettings/smtp") as SmtpSection).From;
        }

        public bool Send(string to, string subject, string body)
        {
            try
            {
                var msg = new MailMessage(EmailFrom, to, subject, body);
                if (null != MailClient) MailClient.Send(msg);
                return true;
            }
            catch (Exception ex)
            {
                log.Error("Error when trying to send email", ex);
                return false;
            }
        }
    }
}