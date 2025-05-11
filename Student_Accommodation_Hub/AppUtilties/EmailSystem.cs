using Student_Accommodation_Hub.Models;
using System;
using System.Net;
using System.Net.Mail;

public class EmailSystem
{
    private static readonly string DefaultSenderEmail = "jarslanjutt570@gmail.com";
    private static readonly string DefaultSenderPassword = "mderhdjbnkcmdwxd";
    private static readonly string SmtpServer = "smtp.gmail.com";
    private static readonly int SmtpPort = 587;

    public static bool SendOTPEmail(EmailSystemModel emailModel)
    {
        try
        {
            string senderEmail = string.IsNullOrEmpty(emailModel.FromEmail)
                ? DefaultSenderEmail
                : emailModel.FromEmail;

            MailMessage mail = new MailMessage
            {
                From = new MailAddress(senderEmail, emailModel.FromName ?? "StudentAccommodationHub"),
                Subject = emailModel.Subject,
                Body = emailModel.Body
                 .Replace("{OTP}", emailModel.OTP)
                 .Replace("{ToName}", emailModel.ToName),
                IsBodyHtml = emailModel.IsHtml
            };


            mail.To.Add(emailModel.ToEmail);

            using (SmtpClient smtpClient = new SmtpClient(SmtpServer, SmtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(DefaultSenderEmail, DefaultSenderPassword);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mail);
            }

            return true;
        }
        catch (Exception ex)
        {
            // Optional: log error
            Console.WriteLine("Error sending OTP: " + ex.Message);
            return false;
        }
    }
}
