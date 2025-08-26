using System;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using System.IO;
using System.Linq;

/// </summary>
public  class SendEmail
{
   
    
    private const string HostAdd = "192.168.248.3";
    private const int smtpPort = 25;

    //public  string Pass, FromEmailid, HostAdd;public static int smtpPort;

    public  bool  Email_With_Attachment(string fromEmailID,string fromEmailPasscode, string fromName,string ToEmail,string toName,string ccEmailIds,string bccemailID, string Subj, string Message , MemoryStream attachment, string attachment_name, string IsAttachment)
    {
        bool rtn = false;

        try
        {
            //creating the object of MailMessage
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromEmailID, fromName); //From Email Id
            mailMessage.Subject = Subj; //Subject of Email
            mailMessage.Body = Message; //body or message of Email

            if (IsAttachment == "Y")
            {
                mailMessage.Attachments.Add(new Attachment(attachment, attachment_name));
            }

            mailMessage.IsBodyHtml = true;
            //Adding Multiple recipient email id logic

            string[] MultiEmail = ToEmail.Split(','); //spiliting input Email id string with comma(,)
           // string[] MultiName = toName.Split(','); //spiliting input Email id string with comma(,)

            foreach (string Multiemailid in MultiEmail)
            {

                mailMessage.To.Add(new MailAddress(Multiemailid)); //adding multi reciver's Email Id
                //foreach (var MultiemailidName in MultiName)
                //{
                //    mailMessage.To.Add(new MailAddress(Multiemailid, MultiemailidName)); //adding multi reciver's Email Id
                //}

            }

            if (!string.IsNullOrEmpty(ccEmailIds))
            {
                string[] MulticcEmailId = ccEmailIds.Split(',');

                foreach (var MultieccEmails in MulticcEmailId)
                {
                    string emailIdWithhost = "";

                    if (!MultieccEmails.ToLower().Contains('@'))
                    {
                        emailIdWithhost = MultieccEmails.Substring(MultieccEmails.IndexOf('@') + 1) + "@srilankainsurance.com";
                    }
                    else
                    {
                        emailIdWithhost = MultieccEmails;
                    }

                    mailMessage.CC.Add(new MailAddress(emailIdWithhost)); //adding multi reciver's Email Id
                }
                //spiliting input Email id string with comma(,)
            }

            if(!string.IsNullOrEmpty(bccemailID))
            {
                mailMessage.Bcc.Add(new MailAddress(bccemailID));
            }
            SmtpClient smtp = new SmtpClient(); // creating object of smptpclient
            smtp.Host = HostAdd; //host of emailaddress for example smtp.gmail.com etc

            //network and security related credentials
            smtp.EnableSsl = true;
            var NetworkCred = new NetworkCredential();
            NetworkCred.UserName = mailMessage.From.Address;
            NetworkCred.Password = fromEmailPasscode;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = smtpPort;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            smtp.Send(mailMessage); //sending Email

            rtn = true;
        }
        catch(Exception ex)
        {
            rtn = false;
        }
       

        return rtn;
    }

    public bool Email_To_FireRN(string fromEmailID, string fromEmailPasscode, string fromName, string ToEmail, string toName, string bccemailID, string Subj, string Message)
    {
        bool rtn = false;

        try
        {
            //creating the object of MailMessage
            var mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(fromEmailID, fromName); //From Email Id
            mailMessage.Subject = Subj; //Subject of Email
            mailMessage.Body = Message; //body or message of Email

            

            mailMessage.IsBodyHtml = true;
            //Adding Multiple recipient email id logic

            string[] MultiEmail = ToEmail.Split(','); //spiliting input Email id string with comma(,)
                                                      // string[] MultiName = toName.Split(','); //spiliting input Email id string with comma(,)

            foreach (string Multiemailid in MultiEmail)
            {

                mailMessage.To.Add(new MailAddress(Multiemailid)); //adding multi reciver's Email Id
                //foreach (var MultiemailidName in MultiName)
                //{
                //    mailMessage.To.Add(new MailAddress(Multiemailid, MultiemailidName)); //adding multi reciver's Email Id
                //}

            }

            

            if (!string.IsNullOrEmpty(bccemailID))
            {
                mailMessage.Bcc.Add(new MailAddress(bccemailID));
            }
            SmtpClient smtp = new SmtpClient(); // creating object of smptpclient
            smtp.Host = HostAdd; //host of emailaddress for example smtp.gmail.com etc

            //network and security related credentials
            smtp.EnableSsl = true;
            var NetworkCred = new NetworkCredential();
            NetworkCred.UserName = mailMessage.From.Address;
            NetworkCred.Password = fromEmailPasscode;
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = smtpPort;

            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object s,
                    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                    System.Security.Cryptography.X509Certificates.X509Chain chain,
                    System.Net.Security.SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            smtp.Send(mailMessage); //sending Email

            rtn = true;
        }
        catch (Exception ex)
        {
            rtn = false;

            LogFile Err1 = new LogFile();
            Err1.ErrorLog(@"D:\WebLogs\FireRenewalErrorlg.txt", "Email error in SendEmail.cs page: " + ex.Message);
        }


        return rtn;
    }

}



