using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FireRNSMSEmail
/// </summary>
public class FireRNSMSEmail
{
    private string FromEmailid = ConfigurationManager.AppSettings["senderEmailId"];
    private string FromEmailPasscode = ConfigurationManager.AppSettings["senderEmailPasscode"];
    string toEmail, EmailSubj, EmailMsg, emailUrl;
    string SenderEmailId = "", SenderName = "";
    string recipientEmailId = "", recipientName = "", previousClaimHistory = "";
    string pushCCEmailIDs = "";
    string senderNameStr = "";
    string brName = "", specialDiscount = "", busType = "";

    string isDirectOrAgent;
    string VehicleType, make, sumInsured, vechileChassNo, yOm, fuelType, totalPre, agentName, insurredName, NCBval, NCBpre, MRval, MRpre, model;
    int agentCode;
    bool IsVechicleNo;
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]);

    public FireRNSMSEmail()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool sendRenewalSMSMail(string emailMessage, string RecepientEmail, string policyNo, double rnTot)
    {
        bool emailBody = false;
        bool rtn = false;
        string subject = "Fire Renewal Notice", emailmgs = "";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            var _sendEmail = new SendEmail();



            // get recepient email id here 

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {



                    senderNameStr = "SLIC_Fire Department".ToUpper();

   
                        emailmgs = "<center><div style='width: 95%;text-align:left;box-shadow: 0 0 1.5% #dfdad6;font-family:arial'> " +
                                    "<div style='padding-top:0%;font-size: 17px;text-transform: none;text-align:left;color:#000;font-family:arial;font-weight:normal'> " +
                                    "Dear Sir/Madam,<br /><p style='padding-left: 0%;font-size: 16px;text-transform: none;line-height: 25px'> "+ emailMessage + "</p> " +
                                    
                                    "Thank you.<br />Yours Sincerely,<br />" + senderNameStr + ".<hr /><br /><i style='Color: #00bab8;font-size:12px'> " +
                                    "</div></div></div></center> ";
                        emailBody = true;
                    



                #region get stuff to send email
                //receiverEmailId = "mrjanuplus@gmail.com";
                //toEmail = Convert.ToString(recipientEmailId);
                toEmail = Convert.ToString(RecepientEmail);
                EmailMsg = Convert.ToString(emailmgs);
                EmailSubj = Convert.ToString(subject);

                var ms = new MemoryStream();


                if (emailBody)
                {
                    if (_sendEmail.Email_To_FireRN(FromEmailid, FromEmailPasscode, senderNameStr, toEmail, recipientName, pushCCEmailIDs, EmailSubj, EmailMsg))
                    {
                        //if(InsertEmailHistory(requestID, reqType,recipientEmailId,recipientName,SenderEmailId,SenderName,pushCCEmailIDs, EmailSubj, EmailMsg,"Y", brName))
                        //{
                        rtn = true;
                        //}

                    }
                    else
                    {
                        //if (InsertEmailHistory(requestID, reqType, recipientEmailId, recipientName, SenderEmailId, SenderName, pushCCEmailIDs, EmailSubj, EmailMsg, "N", brName))
                        //{
                        rtn = false;
                        //}

                    }
                }
                else
                {
                    rtn = false;
                }
                #endregion
            }



        }
        catch (Exception ex)
        {
            string x = ex.ToString();

            LogFile Err1 = new LogFile();
            Err1.ErrorLog(@"D:\WebLogs\FireRenewalErrorlg.txt", "Email error in fireRNSMSEmaii.cs : " + ex.Message);

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return rtn;
    }
}