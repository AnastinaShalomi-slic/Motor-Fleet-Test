using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ApprovalRequestEmails
/// </summary>
public class ApprovalRequestEmails
{
    private string FromEmailid = ConfigurationManager.AppSettings["senderEmailId"];
    private string FromEmailPasscode = ConfigurationManager.AppSettings["senderEmailPasscode"];
    string toEmail, EmailSubj, EmailMsg, emailUrl;
    string SenderEmailId = "", SenderName = "";
    string recipientEmailId = "", recipientName = "", previousClaimHistory="";
    string pushCCEmailIDs = "";
    string senderNameStr = "";
    string brName = "",specialDiscount="", busType="";

    string isDirectOrAgent;
    string VehicleType, make, sumInsured, vechileChassNo, yOm, fuelType, totalPre, agentName, insurredName, NCBval, NCBpre, MRval, MRpre,model;
    int agentCode;
    bool IsVechicleNo;
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]);

    public ApprovalRequestEmails()
    {

    }

    public bool sendAprovalEmail(string reqType, int brCode,string senderEPF, string RecepientEPF, string ccEmailIDs, string requestID,
        string reqReason, string qRefNo, string userID,string requestStatus,string remark,string reqEmailType, string v_make, string v_model)
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

                    senderNameStr = "SLIC BANCASSURANCE".ToUpper(); 

                    string reqEmailTypeForSubject = "";
                    if(reqEmailType == "A")
                    {
                        reqEmailTypeForSubject = "  (Attachment)".ToUpper();
                    }
                    

                        if (getQuotationDataForRequestEmailBody(qRefNo, "N", out VehicleType, out make, out sumInsured, out vechileChassNo, out yOm, out fuelType,
                         out totalPre, out agentCode, out agentName, out insurredName, out IsVechicleNo, out NCBval, out NCBpre, out MRval, out MRpre,out model,out specialDiscount,out busType,out previousClaimHistory))
                    {
                        subject = "SLIC Motor Quotation / " +v_make+" - "+v_model+" / "+ requestID + reqEmailTypeForSubject;

                        if (agentCode == 0)
                        {
                            isDirectOrAgent = "Yes";
                        }
                        else
                        {
                            isDirectOrAgent = "No";
                        }

                        if (string.IsNullOrEmpty(NCBval))
                        {
                            NCBval = "NCB not available";
                        }

                        if (string.IsNullOrEmpty(MRval))
                        {
                            MRval = "MR not available";
                        }
                        else
                        {
                            MRval = MRval + "%";
                        }
                        string preClmCss = "";
                        if(previousClaimHistory == "No")
                        {
                            preClmCss = "background:#dffdc2;text-align:center;padding: 5px";
                        }
                        else
                        {
                            preClmCss = "background:#fdc2c2;text-align:center;padding: 5px";
                        }
                        emailmgs = "<center><div style='width: 95%;text-align:left;box-shadow: 0 0 1.5% #dfdad6;font-family:arial'> " +
                                    "<div style='padding-top:0%;font-size: 17px;text-transform: none;text-align:left;color:#000;font-family:arial;font-weight:normal'> " +
                                    "Dear Sir/Madam,<p style='padding-left: 0%;font-size: 16px;text-transform: none;line-height: 25px'>Quotation referenced  <b>" + qRefNo + "</b> has attached above.</p> " +
                                    "<table style='width: 100%'><tr style='text-align:center;color:#fff;background: #6d6d6d;'> " +
                                    "<td colspan='2' style='padding: 5px;font-size: 20px'> Quotation Summary</td></tr> " +
                                    
                                   
                                    "<tr style='text-align:left;color:#fff;background: #a3a3a3'> " + //br name or user name
                                    "<td colspan='2' style='padding: 3px;font-size: 18px;color:#bf3f07'>Insured Details</td></tr> " +
                                    "<tr style='background:#f1eeeb'><td style='width: 50%'><table style='width: 100%;text-align:left;padding:5px'> " +
                                    "<tr><td style='width: 160px'>Insured Name</td><td style='font-weight:bold'> " +
                                    "" + insurredName + "</td></tr></table></td> " + // insured name
                                    "<td style='width: 50%'><table style='width: 100%;padding:5px'> " +
                                    "<tr><td style='width: 170px'>Vehicle / Chassis No.</td>" +
                                    "<td style='font-weight:bold;text-align:left'>" + vechileChassNo + "</td></tr></table></td> " + // vehicle or chassis 
                                    "</tr><tr style='background:#dfdad6'> " +
                                    "<td><table style='width: 100%;text-align:left;padding:5px'> " +
                                    "<tr><td style='width: 160px'>Type of Fuel</td> " +
                                    "<td style='font-weight:bold'>" + fuelType + "</td> " + // fuel type
                                    "</tr></table></td><td><table style='width: 100%;padding:5px'> " +
                                    "<tr> <td style='width: 170px'>Make</td> " +
                                    "<td style='font-weight:bold;text-align:left'> " +
                                    "" + make + "</td></tr></table></td></tr> " + // make
                                    "<tr style='background:#f1eeeb'><td> " +
                                    "<table style='width: 100%;text-align:left;padding:5px'> " +
                                    "<tr><td style='width: 160px'>Model</td> " +
                                    "<td style='font-weight:bold'>"+model+"</td></tr></table></td> " +  // model
                                    "<td><table style='width: 100%;padding:5px'><tr><td style='width: 170px'>Vehicle Type " +
                                    "</td><td style='font-weight:bold;text-align:left'>" + VehicleType + "</td></tr></table></td></tr> " + // Vehicle Type
                                    "<tr style='background:#dfdad6'><td><table style='width: 100%;text-align:left;padding:5px'> " +
                                    "<tr><td style='width: 160px'>Year of Manufacture</td> " +
                                    "<td style='font-weight:bold'>" + yOm + "</td></tr></table></td> " +   // YOM
                                    "<td><table style='width: 100%;padding:5px'><tr><td style='width: 170px'> " +
                                    "Sum Assured</td><td style='font-weight:bold;text-align:left'> " +
                                    "LKR " + sumInsured + "</td></tr></table></td></tr> " +                  // sum
                                    "<tr style='text-align:left;color:#fff;background: #a3a3a3'> " +
                                    "<td colspan='2' style='padding: 3px;font-size: 18px;color:#bf3f07'> " +
                                    "Business Details</td></tr><tr style='background:#f1eeeb'><td> " +
                                    "<table style='width: 100%;text-align:left;padding:5px'><tr> " +
                                    "<td style='width: 160px'>Business Type</td> " +
                                    "<td colspan='2' style='font-weight:bold'>"+busType+"</td></tr></table></td><td> " +
                                    " <table style='width: 100%;padding:5px'><tr><td style='width: 170px'> " +
                                    " Direct Business</td><td style='font-weight:bold;text-align:left'> " + // direct business
                                    "" + isDirectOrAgent + "</td></tr></table></td></tr><tr style='background:#dfdad6'><td> " +
                                    "<table style='width: 100%;text-align:left;padding:5px'><tr><td style='width: 160px'> " +
                                    "Agent Name</td><td style='font-weight:bold'>" + agentName + " " + // agent name
                                    "</td></tr></table></td><td><table style='width: 100%;padding:5px'><tr> " +
                                    "<td style='width: 170px'>Agent Code</td><td style='font-weight:bold;text-align:left'> " +
                                    "" + agentCode.ToString() + "</td></tr></table></td></tr> " +
									"<tr style='background:#f1eeeb'><td><tr style='padding: 5px'><td style='"+ preClmCss + "'>Any previous claim history : <b> " + previousClaimHistory + "</b></td>" +//previous claim history
                                    "<td style='background:#d7cb08;text-align:center;padding: 5px'>Total premium if expected discount offered : <b>LKR " + totalPre + "</b></td></tr> " + //discounted Premium
                                    "<tr style='text-align:left;color:#fff;background: #a3a3a3'><td colspan='2' style='padding: 3px;font-size: 18px;color:#bf3f07'> " +
                                    "Remarks</td></tr><tr style='background:#f1eeeb;padding:5px'><td colspan='2'style='font-weight:bold;text-align:left'> " + reqReason + " </td></tr></table> " +  //request reason
                                    "<br /><div style='color: #000;font-size: 17px;text-align: left;line-height: 20px;padding-left: 1%;padding-right: 1%'> " +
                                    "Thank you.<br />Yours Sincerely,<br />" + senderNameStr + ".<hr /><br /><i style='Color: #00bab8;font-size:12px'> " +
                                    "<a href='http://insurance-app/Slicgeneral/Bancassu_Sys/Login.aspx'>Log-in</a></i> " +
                                    "</div></div></div></center> ";
                        emailBody = true;
                    }
                    else
                    {
                        emailBody = false;
                    }

                   
                }
               


                #region get stuff to send email
                //receiverEmailId = "mrjanuplus@gmail.com";
                //toEmail = Convert.ToString(recipientEmailId);
                toEmail = Convert.ToString(RecepientEPF);
                EmailMsg = Convert.ToString(emailmgs);
                EmailSubj = Convert.ToString(subject);

                var ms = new MemoryStream();
                var _printEmailPDF = new printEmailPDF();

                string isAttachment = "";
                string attachmentName = "";
                if (_printEmailPDF.getPDFQuotationAttachment(qRefNo, userID.Trim().ToUpper(), out ms))
                {
                    isAttachment = "Y";
                    attachmentName = qRefNo + ".pdf";
                }
                else
                {
                    isAttachment = "N";
                    attachmentName = "";
                }

               
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

    public bool checkCCemailIds(string getCCemailIds)
    {
        bool rtn = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlCountCCemailId = "SELECT COUNT(*) FROM SLICCOMMON.EMAIL_ADDRESS_DATA bre WHERE TRIM(UPPER(bre.e_mail_address))=TRIM(UPPER(:txtCCemailIds)) AND (bre.motor_quotation = 'Y' OR bre.motor_quotation = 'YS')";

                cmd.CommandText = sqlCountCCemailId;

                OracleParameter para1 = new OracleParameter();
                para1.Value = getCCemailIds;
                para1.ParameterName = "txtCCemailIds";
                cmd.Parameters.Add(para1);

                int resultCountCCemailId = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (resultCountCCemailId == 1)
                {
                    rtn = true;
                }
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


    public bool getQuotationDataForRequestEmailBody(string quotNo,string reqStatus, out string vehicleType, out string makeModel, out string sumInsured,
                                                      out string vehicleChasNo, out string yOm, out string fuelType, out string totalPre,
                                                      out int epfAgentCode, out string agentName, out string insuredName, out bool IsVechicleNo, out string NCBvalue, out string NCBPremium,
                                                      out string MRvalue, out string MRpremium,out string model,out string speDiscount,out string busType,out string preClm)
    {
        bool rtn = false;
        vehicleType = ""; makeModel = ""; sumInsured = ""; vehicleChasNo = ""; yOm = ""; fuelType = ""; model = ""; speDiscount = ""; busType = ""; preClm = "";
            totalPre = ""; epfAgentCode = 0;
        insuredName = ""; IsVechicleNo = false;
        NCBPremium = ""; agentName = "";
        NCBvalue = ""; MRvalue = ""; MRpremium = "";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlCountQout = "SELECT COUNT(*) FROM quotation.issued_quotations qut WHERE qut.qref_no = :txtQuotNo";

                OracleParameter para1 = new OracleParameter();
                para1.Value = quotNo.ToUpper().Trim();
                para1.ParameterName = "txtQuotNo";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountQout;

                int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countQuot == 1)
                {



        string sqlGetQoutData = "SELECT qut.qcateg, qut.qmake, qut.qval, qut.qprov, qut.qvno1, qut.qvno2, qut.qchno, qut.qyear, qut.qfuel, " +
        "qut.qtotal_prm, qut.qagents, " +
        "qut.qpdf_user_id, qut.qpdf_user_id, qut.qstat, qut.qname,  d.qncb_vl, d.qncb_dis, d.qmr_vl, d.qmr_dis, " +
        "qut.Model, qut.qdis_type, qut.qdis_vl, qut.qbtype, " +
        "qut.qbranch " +
        "FROM quotation.issued_quotations qut, " +
        "quotation.issued_quotations d " +
        "WHERE qut.qref_no = d.qref_no " +
        "and qut.qref_no = :txtQuotNo2";

                    //string sqlGetQoutData = "SELECT " +
                    //      "qut.qcateg, qut.qmake, " +
                    //      "qut.qval, " +
                    //      "qut.qprov, qut.qvno1, qut.qvno2, qut.qchno, qut.qyear, qut.qfuel, " +
                    //      "qut.qtotal_prm, " +
                    //      "qut.qagents, a.int, a.name, " +
                    //      "qut.qstat, qut.qname,  " +
                    //      "d.qncb_vl, d.qncb_dis, d.qmr_vl, d.qmr_dis, qut.Model, qut.qdis_type, qut.qdis_vl, qut.qbtype, " +
                    //      "q.previous_claim_history " +
                    //      "FROM quotation.issued_quotations qut ,  quotation.issued_quotations d, agent.agent a, quotation.quotation_approval_request q " +
                    //      "WHERE a.agency = qut.qagents  AND qut.qref_no = d.qref_no AND qut.qref_no = :txtQuotNo2 "+
                    //      "AND q.qref_no=qut.qref_no AND q.activeflag='Y' AND q.approved_status=:txtreqStatus";

                    OracleParameter para2 = new OracleParameter();
                    para2.Value = quotNo.ToUpper().Trim();
                    para2.ParameterName = "txtQuotNo2";
                    cmd.Parameters.Add(para2);

                    //OracleParameter para3 = new OracleParameter();
                    //para3.Value = reqStatus.ToUpper().Trim();
                    //para3.ParameterName = "txtreqStatus";
                    //cmd.Parameters.Add(para3);

                    cmd.CommandText = sqlGetQoutData;

                    //cmd.Parameters.Clear();

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {

                            if (!reader.IsDBNull(0))
                            {
                                vehicleType = reader.GetString(0).ToUpper();
                            }
                            if (!reader.IsDBNull(1))
                            {
                                makeModel = reader.GetString(1).ToUpper();
                            }
                            if (!reader.IsDBNull(2))
                            {
                                sumInsured = reader.GetDouble(2).ToString("###,###,###0.00");
                            }


                            string veh1 = "", veh2 = "", veh3 = "";

                            if (!reader.IsDBNull(3))
                            {
                                veh1 = reader.GetString(3) + " ";
                            }
                            if (!reader.IsDBNull(4))
                            {
                                veh2 = reader.GetString(4) + " ";

                            }

                            if (!reader.IsDBNull(5))
                            {
                                veh3 = reader.GetString(5);

                            }

                            if (!string.IsNullOrEmpty(veh1) && !string.IsNullOrEmpty(veh2) && !string.IsNullOrEmpty(veh3))
                            {
                                vehicleChasNo = veh1.ToUpper() + veh2.ToUpper() + veh3.ToUpper();
                                IsVechicleNo = true;
                            }
                            else
                            {
                                if (!reader.IsDBNull(6))
                                {

                                    vehicleChasNo = reader.GetString(6).ToUpper();
                                    IsVechicleNo = false;

                                }
                                else
                                {
                                    vehicleChasNo = "Data not available";
                                    IsVechicleNo = true;
                                }

                            }

                            if (!reader.IsDBNull(7))
                            {

                                yOm = reader.GetString(7);
                                if (yOm == "0")
                                {
                                    yOm = "";
                                }
                            }
                            if (!reader.IsDBNull(8))
                            {
                                fuelType = reader.GetString(8).ToUpper();

                            }

                            if (!reader.IsDBNull(9))
                            {
                                totalPre = reader.GetDouble(9).ToString("###,###,###0.00");

                            }
                            else
                            {
                                totalPre = "0.00";
                            }




                            if (!reader.IsDBNull(10))
                            {
                                epfAgentCode = Convert.ToInt32(reader.GetString(10));

                            }

                            if (!reader.IsDBNull(12))
                            {
                                if (!reader.IsDBNull(11))
                                {
                                    agentName = reader.GetString(11).ToUpper() + " " + reader.GetString(12).ToUpper();
                                }
                                else
                                {
                                    if (!reader.IsDBNull(12))
                                    {
                                        agentName = reader.GetString(12).ToUpper();
                                    }
                                }

                            }
                            else
                            {
                                agentName = "";
                            }



                            if (!reader.IsDBNull(14))
                            {
                                if (!reader.IsDBNull(13))
                                {
                                    insuredName = reader.GetString(13).ToUpper() + " " + reader.GetString(14).ToUpper();
                                }
                                else
                                {
                                    insuredName = reader.GetString(14).ToUpper();
                                }


                            }
                            else
                            {
                                insuredName = "UNKNOWN";
                            }

                            if (!reader.IsDBNull(15))
                            {
                                NCBvalue = reader.GetString(15);
                                NCBPremium = reader.GetDouble(16).ToString("###,###.00");
                            }
                            if (!reader.IsDBNull(17))
                            {
                                MRvalue = reader.GetDouble(17).ToString();
                                MRpremium = reader.GetDouble(18).ToString("###,###.00");
                            }


                            if (!reader.IsDBNull(19))
                            {
                                model = reader.GetString(19).ToUpper();
                                
                            }

                            if (!reader.IsDBNull(20))
                            {
                                speDiscount = reader.GetString(20).ToUpper() + " - " + reader.GetDouble(21).ToString()+"%";

                            }
                            else
                            {
                                speDiscount = "<i>Not available</i>";
                            }

                            if (!reader.IsDBNull(22))
                            {
                               
                                if(reader.GetString(22) == "N")
                                {
                                    busType = "New Business".ToUpper();
                                }
                                else if(reader.GetString(22) == "R")
                                {
                                    busType = "Renewal".ToUpper();
                                }
                              

                            }
                            if (!reader.IsDBNull(23))
                            {

                                //preClm = reader.GetString(23);

                                preClm = "N";
                            }

                            rtn = true;

                        }


                    }


                }

            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
            rtn = false;
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


    public bool InsertEmailHistory(string reqId,string emailType,string recipientEmailAddress,string recipientEmailName, string bccEmailAddress, string bccName,string ccEmailAddress,string emailSubject, string emailBody,string emailDelivery,string brName)
    {
        bool rtn = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlReqIdCheck = "SELECT COUNT(*) FROM quotation.quotation_email_history e WHERE e.email_type=:txtEmailType AND e.request_id=:txtReqIdPara AND e.activity_flag='Y'";

                cmd.CommandText = sqlReqIdCheck;


                OracleParameter reqIdPara = new OracleParameter();
                reqIdPara.Value = reqId;
                reqIdPara.ParameterName = "txtReqIdPara";
                cmd.Parameters.Add(reqIdPara);

                OracleParameter EmailTypepara = new OracleParameter();
                EmailTypepara.Value = emailType;
                EmailTypepara.ParameterName = "txtEmailType";
                cmd.Parameters.Add(EmailTypepara);


               

                int ReqIdChecIs = Convert.ToInt32(cmd.ExecuteScalar().ToString());

              
                cmd.Parameters.Clear();
                if(ReqIdChecIs> 0)
                {
                    string updateEmailHistory = "UPDATE quotation.quotation_email_history e SET " +
                                                "e.recipient_email_address=:txtrecipientEmailAddress,e.recipient_name=:txtrecipientName, " +
                                                "e.cc_email_addresses=:txtccEmailAddress,e.bcc_email_address=:txtbccEmailAddress, "+
                                                "e.bcc_name=:txtbccName, e.datestamp=SYSDATE, e.branch_name=:txtbrName, " +
                                                "e.email_subject=:txtemailSubject, e.email_body=:txtemailBody, e.email_delivery=:txtemailDelivery "+
                                                "WHERE e.email_type=:txtEmailType AND e.Request_Id=:txtReqId AND e.activity_flag='Y'";

                    cmd.CommandText = updateEmailHistory;

                    OracleParameter para4 = new OracleParameter();
                    para4.Value = reqId;
                    para4.ParameterName = "txtReqId";
                    cmd.Parameters.Add(para4);

                    OracleParameter para5 = new OracleParameter();
                    para5.Value = emailType;
                    para5.ParameterName = "txtEmailType";
                    cmd.Parameters.Add(para5);

                    OracleParameter para6 = new OracleParameter();
                    para6.Value = brName;
                    para6.ParameterName = "txtbrName";
                    cmd.Parameters.Add(para6);

                    OracleParameter para7 = new OracleParameter();
                    para7.Value = recipientEmailAddress;
                    para7.ParameterName = "txtrecipientEmailAddress";
                    cmd.Parameters.Add(para7);

                    OracleParameter para8 = new OracleParameter();
                    para8.Value = recipientEmailName;
                    para8.ParameterName = "txtrecipientName";
                    cmd.Parameters.Add(para8);

                    OracleParameter para9 = new OracleParameter();
                    para9.Value = ccEmailAddress;
                    para9.ParameterName = "txtccEmailAddress";
                    cmd.Parameters.Add(para9);

                    OracleParameter para10 = new OracleParameter();
                    para10.Value = bccEmailAddress;
                    para10.ParameterName = "txtbccEmailAddress";
                    cmd.Parameters.Add(para10);


                    OracleParameter para11 = new OracleParameter();
                    para11.Value = emailSubject;
                    para11.ParameterName = "txtemailSubject";
                    cmd.Parameters.Add(para11);

                    OracleParameter para12 = new OracleParameter();
                    para12.Value = bccName;
                    para12.ParameterName = "txtbccName";
                    cmd.Parameters.Add(para12);


                    if (emailBody.Length > 3999)
                    {
                        OracleParameter para14 = new OracleParameter();
                        para14.Value = "ORA-01461: can bind a LONG value only for insert into a LONG column";
                        para14.ParameterName = "txtemailBody";
                        cmd.Parameters.Add(para14);
                    }
                    else
                    {
                        OracleParameter para14 = new OracleParameter();
                        para14.Value = emailBody;
                        para14.ParameterName = "txtemailBody";
                        cmd.Parameters.Add(para14);
                    }



                    OracleParameter para13 = new OracleParameter();
                    para13.Value = emailDelivery;
                    para13.ParameterName = "txtemailDelivery";
                    cmd.Parameters.Add(para13);



                    rtn = Convert.ToBoolean(cmd.ExecuteNonQuery());

                    cmd.Parameters.Clear();
                }
                else
                {
                    string setNextEmailId = "";

                    #region getEmailId

                    string sqlGetServerYear = "SELECT SYSDATE FROM DUAL";

                    cmd.CommandText = sqlGetServerYear;

                    var serverCurrentYear = Convert.ToDateTime(cmd.ExecuteScalar().ToString());

                    string sqlCountEmailIds = "SELECT COUNT(*) FROM quotation_email_history e WHERE TO_CHAR(e.datestamp,'yyyy') = TO_CHAR(:txtServerTime,'yyyy')";

                    cmd.CommandText = sqlCountEmailIds;

                    OracleParameter para1 = new OracleParameter();
                    para1.Value = serverCurrentYear;
                    para1.ParameterName = "txtServerTime";
                    para1.DbType = DbType.DateTime;
                    cmd.Parameters.Add(para1);

                    int CountEmailIds = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    cmd.Parameters.Clear();



                    if (CountEmailIds > 0)
                    {


                        setNextEmailId = serverCurrentYear.Year.ToString() + Convert.ToInt32(CountEmailIds + 1).ToString("000");

                    }
                    else
                    {
                        setNextEmailId = serverCurrentYear.Year.ToString() + Convert.ToInt32(CountEmailIds + 1).ToString("000");
                    }

                    #endregion

                    #region insertInto Email History table

                    string sqlInsertInto = "INSERT INTO quotation_email_history(email_id, request_id, email_type, recipient_email_address, recipient_name, bcc_email_address, bcc_name, cc_email_addresses, email_subject, email_body, email_delivery, branch_name) " +
                                                                       "VALUES (:txtEmailID,:txtReqId,:txtEmailType,:txtrecipientEmailAddress,:txtrecipientName,:txtbccEmailAddress,:txtbccName,:txtccEmailAddress,:txtemailSubject,:txtemailBody,:txtemailDelivery,:txtbrName)";

                    cmd.CommandText = sqlInsertInto;


                    OracleParameter para4 = new OracleParameter();
                    para4.Value = reqId;
                    para4.ParameterName = "txtReqId";
                    cmd.Parameters.Add(para4);

                    OracleParameter para5 = new OracleParameter();
                    para5.Value = emailType;
                    para5.ParameterName = "txtEmailType";
                    cmd.Parameters.Add(para5);

                    OracleParameter para6 = new OracleParameter();
                    para6.Value = setNextEmailId;
                    para6.ParameterName = "txtEmailID";
                    cmd.Parameters.Add(para6);

                    OracleParameter para7 = new OracleParameter();
                    para7.Value = recipientEmailAddress;
                    para7.ParameterName = "txtrecipientEmailAddress";
                    cmd.Parameters.Add(para7);

                    OracleParameter para8 = new OracleParameter();
                    para8.Value = recipientEmailName;
                    para8.ParameterName = "txtrecipientName";
                    cmd.Parameters.Add(para8);

                    OracleParameter para9 = new OracleParameter();
                    para9.Value = ccEmailAddress;
                    para9.ParameterName = "txtccEmailAddress";
                    cmd.Parameters.Add(para9);

                    OracleParameter para10 = new OracleParameter();
                    para10.Value = bccEmailAddress;
                    para10.ParameterName = "txtbccEmailAddress";
                    cmd.Parameters.Add(para10);


                    OracleParameter para11 = new OracleParameter();
                    para11.Value = emailSubject;
                    para11.ParameterName = "txtemailSubject";
                    cmd.Parameters.Add(para11);

                    OracleParameter para12 = new OracleParameter();
                    para12.Value = bccName;
                    para12.ParameterName = "txtbccName";
                    cmd.Parameters.Add(para12);


                    if (emailBody.Length > 3999)
                    {
                        OracleParameter para14 = new OracleParameter();
                        para14.Value = "ORA-01461: can bind a LONG value only for insert into a LONG column";
                        para14.ParameterName = "txtemailBody";
                        cmd.Parameters.Add(para14);
                    }
                    else
                    {
                        OracleParameter para14 = new OracleParameter();
                        para14.Value = emailBody;
                        para14.ParameterName = "txtemailBody";
                        cmd.Parameters.Add(para14);
                    }



                    OracleParameter para13 = new OracleParameter();
                    para13.Value = emailDelivery;
                    para13.ParameterName = "txtemailDelivery";
                    cmd.Parameters.Add(para13);

                    OracleParameter para15 = new OracleParameter();
                    para15.Value = brName;
                    para15.ParameterName = "txtbrName";
                    cmd.Parameters.Add(para15);

                    rtn = Convert.ToBoolean(cmd.ExecuteNonQuery());


                    #endregion
                }


            }
        }
        catch (Exception ex)
        {

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


    public string getPreviousReceipientEpf(string qRefNo)
    {
        string GetPreviousRecepientIs = "";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {
                string reqId = "";

                string sqlCountQref = "SELECT COUNT(q.request_id) FROM quotation_approval_request q "+
                                      "WHERE q.qref_no=:txtqRefNo AND q.activeflag ='Y' AND q.approved_status='N' AND " +
                                      "q.datestamp IN (SELECT MAX(i.datestamp) FROM quotation_approval_request i WHERE i.qref_no=:txtqRefNo) ";

                cmd.CommandText = sqlCountQref;

                OracleParameter para3 = new OracleParameter();
                para3.Value = qRefNo;
                para3.ParameterName = "txtqRefNo";
                cmd.Parameters.Add(para3);

                int CountQrefIs = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if(CountQrefIs>0)
                {
                    string sqGetReqId = "SELECT q.request_id FROM quotation_approval_request q " +
                                     "WHERE q.qref_no=:txtqRefNo AND q.activeflag ='Y' AND q.approved_status='N' AND " +
                                     "q.datestamp IN (SELECT MAX(i.datestamp) FROM quotation_approval_request i WHERE i.qref_no=:txtqRefNo) ";

                    cmd.CommandText = sqGetReqId;

                    OracleParameter para4 = new OracleParameter();
                    para4.Value = qRefNo;
                    para4.ParameterName = "txtqRefNo";
                    cmd.Parameters.Add(para4);

                    reqId =cmd.ExecuteScalar().ToString();

                    cmd.Parameters.Clear();


                    string sqlCountPreviousRecepient = "SELECT COUNT(bre.epf) FROM SLICCOMMON.EMAIL_ADDRESS_DATA bre, quotation_email_history e " +
                                                        "WHERE e.recipient_email_address = bre.e_mail_address  AND e.ACTIVITY_FLAG='Y' AND " +
                                                        "bre.epf IS NOT NULL AND bre.motor_quotation = 'Y' AND e.request_id=:txtReqId AND e.Datestamp IN " +
                                                        "(SELECT  MAX(D.Datestamp) FROM quotation_email_history d WHERE d.request_id=:txtReqId)  GROUP BY bre.epf ";

                    cmd.CommandText = sqlCountPreviousRecepient;

                    OracleParameter para1 = new OracleParameter();
                    para1.Value = reqId;
                    para1.ParameterName = "txtReqId";
                    cmd.Parameters.Add(para1);

                    int CountPreviousRecepientIs = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    if (CountPreviousRecepientIs == 1)
                    {
                        string sqlGetPreviousRecepient = "SELECT bre.epf FROM SLICCOMMON.EMAIL_ADDRESS_DATA bre, quotation_email_history e " +
                                                        "WHERE e.recipient_email_address = bre.e_mail_address  AND e.ACTIVITY_FLAG='Y' AND " +
                                                        "bre.epf IS NOT NULL AND bre.motor_quotation = 'Y' AND e.request_id=:txtReqId AND e.Datestamp IN " +
                                                        "(SELECT  MAX(D.Datestamp) FROM quotation_email_history d WHERE d.request_id=:txtReqId)  GROUP BY bre.epf ";

                        cmd.CommandText = sqlGetPreviousRecepient;

                        OracleParameter para2 = new OracleParameter();
                        para2.Value = reqId;
                        para2.ParameterName = "txtReqId";
                        cmd.Parameters.Add(para2);

                        GetPreviousRecepientIs = cmd.ExecuteScalar().ToString();
                    }
                }


            }
        }
        catch(Exception ex)
        {

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return GetPreviousRecepientIs;

    }
}