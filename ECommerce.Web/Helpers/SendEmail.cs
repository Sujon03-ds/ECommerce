using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Text;

namespace ECommerce.Web.Helpers
{
    public class SendEmail
    {
        public bool RegisterVarification(string userEmail, string varificationCode, string userName)
        {
            //try
            //{

            //    string body = string.Empty;

            //    var message = new MailMessage();
            //    message.To.Add(new MailAddress(userEmail));  // replace with valid value 
            //    message.From = new MailAddress("maruf.ahammed@datasoft-bd.com");  // replace with valid value
            //    message.Subject = "Your eCounting Account";
            //    //message.Body = string.Format("<b>Dear " + userName + "</b>, <BR/><BR/>Thank you for your registration.You requested a verification code. You can use this code to complete the process. <br/><br/> Here is your Verification code:" + varificationCode + " <br/><br/><br/> Thanks, <br/>eCounting Team<br/>DataSoft Systems Bangladesh Ltd.");
            //    message.Body = string.Format("<b>Dear " + userName + "</b>, <BR/><BR/>Thank you for your registration. Please use verification code: <b>'" + varificationCode + "'</b> to complete the process. <br/><br/><br/> Best regards, <br/><br/> <b>eCounting Team<br/>DataSoft Systems Bangladesh Ltd.</b> <br/><br/><div style='text-align:center; width:100%; color:#949494'>Don't reply to this email. It was automatically generated.</div> ");
            //    message.IsBodyHtml = true;

            //    using (var smtp = new SmtpClient())
            //    {
            //        var credential = new NetworkCredential
            //        {
            //            UserName = "hr.support@datasoft-bd.com",  // replace with valid value
            //            Password = "supportq1w2e3"  // replace with valid value

            //        };
            //        smtp.Credentials = credential;
            //        smtp.Host = "mail.datasoft-bd.com";
            //        smtp.Port = 25;
            //        smtp.EnableSsl = false;
            //        smtp.UseDefaultCredentials = false;
            //        smtp.Send(message);

            //    }

            //    return true;
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
            try
            {
                SmtpClient client = new SmtpClient("mail.datasoft-bd.com");
                //If you need to authenticate
                client.Credentials = new NetworkCredential("eCounting@datasoft-bd.com", "Dsecounting2019");
                //client.Credentials = new NetworkCredential("datasoftbd1998@gmail.com", "1998datasoftbd");

                client.EnableSsl = false;
                client.Timeout = 5000;
                client.Port = 25;

                System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                mailMessage.From = new System.Net.Mail.MailAddress("eCounting@datasoft-bd.com");

                mailMessage.To.Add(userEmail);

                mailMessage.Subject = "Your eCounting Account";

                //string.Format("<b>Dear " + userName + "</b>, <BR/><BR/>Thank you for your registration. Please use verification code: <b>'" + varificationCode + "'</b> to complete the process. <br/><br/><br/> Best regards, <br/><br/> <b>eCounting Team<br/>DataSoft Systems Bangladesh Ltd.</b> <br/><br/><div style='text-align:center; width:100%; color:#949494'>Don't reply to this email. It was automatically generated.</div> ");

                string FullEmail = "<b>Dear " + userName + "</b>, <BR/><BR/>Thank you for your registration. Please use verification code: <b>'" + varificationCode + "'</b> to complete the process. <br/><br/><br/> Best regards, <br/><br/> <b>eCounting Team<br/>DataSoft Systems Bangladesh Ltd.</b> <br/><br/><div style='text-align:center; width:100%; color:#949494'>Don't reply to this email. It was automatically generated.</div> ";


                mailMessage.Body = FullEmail;
                mailMessage.BodyEncoding = UTF8Encoding.UTF8;
                mailMessage.IsBodyHtml = true;
                client.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                return false;
                //message.custom
            }


        }
        public string RegisterVarificationMail(string userEmail, string varificationCode, string userName)
        {

            //try
            //{
            //    SmtpClient client = new SmtpClient("mail.datasoft-bd.com");
            //    //If you need to authenticate
            //    client.Credentials = new NetworkCredential("eCounting@datasoft-bd.com", "Dsecounting2019");
            //    //client.Credentials = new NetworkCredential("datasoftbd1998@gmail.com", "1998datasoftbd");

            //    client.EnableSsl = true;
            //    client.Timeout = 20000;
            //    client.Port = 25;

            //    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            //    mailMessage.From = new System.Net.Mail.MailAddress("eCounting@datasoft-bd.com");

            //    mailMessage.To.Add(userEmail);

            //    mailMessage.Subject = "Your eCounting Account";

            //    //string.Format("<b>Dear " + userName + "</b>, <BR/><BR/>Thank you for your registration. Please use verification code: <b>'" + varificationCode + "'</b> to complete the process. <br/><br/><br/> Best regards, <br/><br/> <b>eCounting Team<br/>DataSoft Systems Bangladesh Ltd.</b> <br/><br/><div style='text-align:center; width:100%; color:#949494'>Don't reply to this email. It was automatically generated.</div> ");

            //    string FullEmail = "<b>Dear " + userName + "</b>, <BR/><BR/>Thank you for your registration. Please use verification code: <b>'" + varificationCode + "'</b> to complete the process. <br/><br/><br/> Best regards, <br/><br/> <b>eCounting Team<br/>DataSoft Systems Bangladesh Ltd.</b> <br/><br/><div style='text-align:center; width:100%; color:#949494'>Don't reply to this email. It was automatically generated.</div> ";


            //    mailMessage.Body = FullEmail;
            //    mailMessage.BodyEncoding = UTF8Encoding.UTF8;
            //    mailMessage.IsBodyHtml = true;
            //    client.Send(mailMessage);

            //    return "ok";
            //}
            //catch (Exception ex)
            //{
            //    return ex.Message+"======="+ex.Source+"======"+ex.StackTrace;
            //    //message.custom
            //}
            try
            {

                string body = string.Empty;

                var message = new MailMessage();
                message.To.Add(new MailAddress(userEmail));  // replace with valid value 
                message.From = new MailAddress("eCounting@datasoft-bd.com");  // replace with valid value
                message.Subject = "Your eCounting Account";
                //message.Body = string.Format("<b>Dear " + userName + "</b>, <BR/><BR/>Thank you for your registration.You requested a verification code. You can use this code to complete the process. <br/><br/> Here is your Verification code:" + varificationCode + " <br/><br/><br/> Thanks, <br/>eCounting Team<br/>DataSoft Systems Bangladesh Ltd.");
                message.Body = string.Format("<b>Dear " + userName + "</b>, <BR/><BR/>Thank you for your registration. Please use verification code: <b>'" + varificationCode + "'</b> to complete the process. <br/><br/><br/> Best regards, <br/><br/> <b>eCounting Team<br/>DataSoft Systems Bangladesh Ltd.</b> <br/><br/><div style='text-align:center; width:100%; color:#949494'>Don't reply to this email. It was automatically generated.</div> ");
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "eCounting@datasoft-bd.com",  // replace with valid value
                        Password = "Dsecounting2019"  // replace with valid value

                    };
                    smtp.Credentials = credential;
                    smtp.Host = "mail.datasoft-bd.com";
                    smtp.Port = 25;
                    smtp.EnableSsl = false;
                    smtp.UseDefaultCredentials = false;
                    smtp.Send(message);

                }

                return "ok";
            }
            catch (Exception ex)
            {

                return ex.Message + "=======" + ex.Source + "======" + ex.StackTrace;
            }

        }
    }
}