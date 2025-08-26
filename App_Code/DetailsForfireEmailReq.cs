using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DetailsForfireEmailReq
/// </summary>
public class DetailsForfireEmailReq
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

    public DetailsForfireEmailReq()
    {
       
    }

    public bool fireRequestDetails(string reqType, string bankCode,string toEmail, string ccEmailIDs,string qRefNo,  string BANK_NAME,string branch_name,string fromEmail)
    {
        bool emailBody = false;
        bool rtn = false;
        string subject = "", emailmgs = "";
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
                if (reqType == "NR")
                {


                    if (!string.IsNullOrEmpty(ccEmailIDs))
                    {
                        pushCCEmailIDs = ccEmailIDs;
                    }
                    else { pushCCEmailIDs = ""; }

                    //senderNameStr = "BANCASSURANCE USER".ToUpper();
                    senderNameStr = (BANK_NAME + " - " + branch_name).ToUpper(); 

                    subject = "Approval Request from " + BANK_NAME;


                    emailmgs = "<center><div style='width: 95%;text-align:left;box-shadow: 0 0 1.5% #dfdad6;font-family:arial'> " +
                                    "<div style='padding-top:0%;font-size: 17px;text-transform: none;text-align:left;color:#000;font-family:arial;font-weight:normal'> " +
                                    "Dear All,<p style='padding-left: 0%;font-size: 16px;text-transform: none;line-height: 25px'>Fire Policy Approval Request from " + BANK_NAME + " - " + branch_name + ". Reference ID  <b>" + qRefNo + "</b>.</p> " +

                                    "<br /><div style='color: #000;font-size: 17px;text-align: left;line-height: 20px;padding-left: 0%;padding-right: 1%'> " +
                                    "Thank you.<br />Yours Sincerely,<br />" + senderNameStr + ".<hr /><br /><i style='Color: #00bab8;font-size:12px'> " +
                                    "<a href='http://insurance-app/Slicgeneral/Bancassu_Sys/Login.aspx'>Log-in</a></i> " +
                                    "</div></div></div></center> ";
                    emailBody = true;



                }
                else if (reqType == "SLICR")
                {


                    if (!string.IsNullOrEmpty(ccEmailIDs))
                    {
                        pushCCEmailIDs = ccEmailIDs;
                    }
                    else { pushCCEmailIDs = ""; }

                    senderNameStr = "SLIC BANCASSURANCE UNIT".ToUpper();
                    //senderNameStr = (BANK_NAME + " - " + branch_name).ToUpper();

                    subject = "Current status of the requested policy (Ref. ID " + qRefNo+")";


                    emailmgs = "<center><div style='width: 95%;text-align:left;box-shadow: 0 0 1.5% #dfdad6;font-family:arial'> " +
                                    "<div style='padding-top:0%;font-size: 17px;text-transform: none;text-align:left;color:#000;font-family:arial;font-weight:normal'> " +
                                    "Dear All,<p style='padding-left: 0%;font-size: 16px;text-transform: none;line-height: 25px'>" + BANK_NAME + ".</p> " +

                                    "<br /><div style='color: #000;font-size: 17px;text-align: left;line-height: 20px;padding-left: 0%;padding-right: 1%'> " +
                                    "Thank you.<br />Yours Sincerely,<br />" + senderNameStr + ".<hr /><br /><i style='Color: #00bab8;font-size:12px'> " +
                                    "<a href='http://insurance-app/Slicgeneral/Bancassu_Sys/Login.aspx'>Log-in</a></i> " +
                                    "</div></div></div></center> ";
                    emailBody = true;



                }

                else if (reqType == "SLIMO")
                {


                    if (!string.IsNullOrEmpty(ccEmailIDs))
                    {
                        pushCCEmailIDs = ccEmailIDs;
                    }
                    else { pushCCEmailIDs = ""; }

                    senderNameStr = "SLIC BANCASSURANCE UNIT".ToUpper();
                    //senderNameStr = (BANK_NAME + " - " + branch_name).ToUpper();

                    subject = "Current status of the requested motor quotation (Ref. ID " + qRefNo + ")";


                    emailmgs = "<center><div style='width: 95%;text-align:left;box-shadow: 0 0 1.5% #dfdad6;font-family:arial'> " +
                                    "<div style='padding-top:0%;font-size: 17px;text-transform: none;text-align:left;color:#000;font-family:arial;font-weight:normal'> " +
                                    "Dear All,<p style='padding-left: 0%;font-size: 16px;text-transform: none;line-height: 25px'>" + BANK_NAME + ".</p> " +

                                    "<br /><div style='color: #000;font-size: 17px;text-align: left;line-height: 20px;padding-left: 0%;padding-right: 1%'> " +
                                    "Thank you.<br />Yours Sincerely,<br />" + senderNameStr + ".<hr /><br /><i style='Color: #00bab8;font-size:12px'> " +
                                    "<a href='http://insurance-app/Slicgeneral/Bancassu_Sys/Login.aspx'>Log-in</a></i> " +
                                    "</div></div></div></center> ";
                    emailBody = true;



                }

                else if (reqType == "BANKINQ")
                {


                    if (!string.IsNullOrEmpty(ccEmailIDs))
                    {
                        pushCCEmailIDs = ccEmailIDs;
                    }
                    else { pushCCEmailIDs = ""; }

                    //senderNameStr = "SLIC BANCASSURANCE UNIT".ToUpper();
                    senderNameStr = (BANK_NAME + " - " + branch_name).ToUpper();
                    subject = "Inquiry Request from " + BANK_NAME + " - " + branch_name+"(Ref.ID " + qRefNo + ")";
                    


                    emailmgs = "<center><div style='width: 95%;text-align:left;box-shadow: 0 0 1.5% #dfdad6;font-family:arial'> " +
                                    "<div style='padding-top:0%;font-size: 17px;text-transform: none;text-align:left;color:#000;font-family:arial;font-weight:normal'> " +
                                    "Dear All,<p style='padding-left: 0%;font-size: 16px;text-transform: none;line-height: 25px'>" + bankCode + ".</p> " +

                                    "<br /><div style='color: #000;font-size: 17px;text-align: left;line-height: 20px;padding-left: 0%;padding-right: 1%'> " +
                                    "Thank you.<br />Yours Sincerely,<br />" + senderNameStr + ".<hr /><br /><i style='Color: #00bab8;font-size:12px'> " +
                                    "<a href='http://insurance-app/Slicgeneral/Bancassu_Sys/Login.aspx'>Log-in</a></i> " +
                                    "</div></div></div></center> ";
                    emailBody = true;



                }

                else if (reqType == "SLIINQ")
                {


                    if (!string.IsNullOrEmpty(ccEmailIDs))
                    {
                        pushCCEmailIDs = ccEmailIDs;
                    }
                    else { pushCCEmailIDs = ""; }

                    senderNameStr = "SLIC BANCASSURANCE UNIT".ToUpper();
                    //senderNameStr = (BANK_NAME + " - " + branch_name).ToUpper();

                    subject = "Current status of the requested inquiry  (Ref. ID " + qRefNo + ")";


                    emailmgs = "<center><div style='width: 95%;text-align:left;box-shadow: 0 0 1.5% #dfdad6;font-family:arial'> " +
                                    "<div style='padding-top:0%;font-size: 17px;text-transform: none;text-align:left;color:#000;font-family:arial;font-weight:normal'> " +
                                    "Dear All,<p style='padding-left: 0%;font-size: 16px;text-transform: none;line-height: 25px'>" + BANK_NAME + ".</p> " +

                                    "<br /><div style='color: #000;font-size: 17px;text-align: left;line-height: 20px;padding-left: 0%;padding-right: 1%'> " +
                                    "Thank you.<br />Yours Sincerely,<br />" + senderNameStr + ".<hr /><br /><i style='Color: #00bab8;font-size:12px'> " +
                                    "<a href='http://insurance-app/Slicgeneral/Bancassu_Sys/Login.aspx'>Log-in</a></i> " +
                                    "</div></div></div></center> ";
                    emailBody = true;



                }
                else { }


                #region get stuff to send email
                //receiverEmailId = "mrjanuplus@gmail.com";
                //toEmail = Convert.ToString(recipientEmailId);
                toEmail = Convert.ToString(toEmail);
                EmailMsg = Convert.ToString(emailmgs);
                EmailSubj = Convert.ToString(subject);

                var ms = new MemoryStream();
                var _printEmailPDF = new printEmailPDF();

                string isAttachment = "";
                string attachmentName = "";
               
                    isAttachment = "N";
                    attachmentName = "";

                FromEmailid = fromEmail;

                if (emailBody)
                {
                    if (_sendEmail.Email_With_Attachment(FromEmailid, FromEmailPasscode, senderNameStr, toEmail, recipientName, pushCCEmailIDs, SenderEmailId, EmailSubj, EmailMsg, ms, attachmentName, isAttachment))
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