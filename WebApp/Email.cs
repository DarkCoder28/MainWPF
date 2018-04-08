using System;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Text.RegularExpressions;

namespace WebApp
{
    class Email
    {
        

        public Email(String user, String pass, String from, String to, String smtp, String portS, Boolean ssl, String subject, String body, Boolean attachment, String[] attachNames)
        { 
            MailMessage message = new MailMessage();

            if (ssl)
            {
                if (String.IsNullOrEmpty(user))
                {
                    MessageBox.Show("Error: Please Enter Username");
                    return;
                }
                if (String.IsNullOrEmpty(pass))
                {
                    MessageBox.Show("Error: Please Enter Password");
                }
            }

            if (attachment)
            {
                for(int x = 0; x<=attachNames.Length-1; x++)
                {
                    message.Attachments.Add(new Attachment(attachNames[0]));
                }
            }

            if (isValidEmail(from))
            {
                MailAddress fromaddress = new MailAddress(from);
                message.From = fromaddress;
            }
            else
            {
                MessageBox.Show("An Error Occured: Invalid From Address");
                return;
            }
            if (isValidEmail(to))
            {
                MailAddress toaddress = new MailAddress(to);
                message.To.Add(toaddress);
            }
            else
            {
                MessageBox.Show("An Error Occured: Invalid To Address");
                return;
            }
            if (!String.IsNullOrEmpty(subject))
            {
                message.Subject = subject;
            }
            else
            {
                MessageBox.Show("Error: Subject Can Not Be Empty!");
                return;
            }
            message.Body = body;
            if (!String.IsNullOrEmpty(smtp))
            {
                if (isValidNumber(portS))
                {
                    Int16 port = Convert.ToInt16(portS);
                    SmtpClient smtpC = new SmtpClient(smtp, port)
                    {
                        Credentials = new NetworkCredential(user, pass),
                        EnableSsl = ssl
                    };
                    try
                    {
                        smtpC.Send(message);
                        MessageBox.Show("Message Sent Successfully!");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("An Error Occured:" + Environment.NewLine + e);
                    }
                }
                else
                {
                    MessageBox.Show("Error: Port must be a number!");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Error: Enter SMTP Server Address!");
            }
        }

        private Boolean isValidEmail(String m)
        {
            String pattern = "@";
            return Regex.IsMatch(m, pattern);
        }
        private Boolean isValidNumber(String m)
        {
            return Regex.IsMatch(m, "^[0-9]+$");
        }
    }
}
