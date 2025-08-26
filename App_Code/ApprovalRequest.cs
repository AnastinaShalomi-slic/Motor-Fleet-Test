using System;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using System.Collections.Generic;

/// <summary>
/// Summary description for ApprovalRequest
/// </summary>
public class ApprovalRequest
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]);
    public ApprovalRequest()
    {

    }

    public bool quoationAvailibility(string quotNo)
    {
        bool IsAvailable = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlCountQout = "";

                sqlCountQout = "SELECT COUNT(*) FROM quotation.issued_quotations qut WHERE  qut.qref_no = :txtQuotNo ";
                //+
                //                    "AND qut.qmake is not null AND qut.qfuel is not null AND (qut.qyear is not null OR qut.qyear = '0') " +
                //                    " AND qut.qcateg !='PRIVATE LORRY' AND qut.qcateg !='THREE WHEELER PRIVATE USE' ";

                OracleParameter para1 = new OracleParameter();
                para1.Value = quotNo.ToUpper().Trim();
                para1.ParameterName = "txtQuotNo";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountQout;

                int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countQuot == 1)
                {
                    IsAvailable = true;

                }
            }
        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }

        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return IsAvailable;
    }

    public bool PushAprrovalRequest(string reqTyp, string Req_id, int fileuploadcount, string quotNo, string requestedEpf, string requestReason, bool fileUpload, string docURL, string recepientEmail, string ccEmails, int brCode, string bank_user, 
        string v_make, string v_model, out string outRequestID, out int emailMgs)
    {
        outRequestID = ""; emailMgs = 0;
        string requestID = "";
        var _DeviceFinder = new DeviceFinder();
        bool rtn = false;
        DateTime now = DateTime.Now;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                if (reqTyp == "N")
                {
                    if (quoationAvailibility(quotNo.Trim().ToUpper()))
                    {
                        if (oconn.State != ConnectionState.Open)
                        {
                            oconn.Open();
                        }
                       

                            if (fileuploadcount == 1)
                            {



                            string updateQuery = "UPDATE QUOTATION.BANK_REQ_ENTRY_DETAILS " +
                                                 "SET QUO_NO = :txtQuoNo, FLAG= :flag, EMAIL_SEND_BY = :appOfficer, EMAIL_SEND_ON = :sendDate " +
                                                 "WHERE REQ_ID = :txtReqId";
                                    cmd.CommandText = updateQuery;

                                    OracleParameter quo_no = new OracleParameter();
                                    quo_no.Value = quotNo;
                                    quo_no.ParameterName = "txtQuoNo";
                                    cmd.Parameters.Add(quo_no);

                            OracleParameter FLAG1 = new OracleParameter();
                            FLAG1.Value = "C";
                            FLAG1.ParameterName = "flag";
                            cmd.Parameters.Add(FLAG1);

                            OracleParameter officer = new OracleParameter();
                            officer.Value = bank_user;
                            officer.ParameterName = "appOfficer";
                            cmd.Parameters.Add(officer);

                            OracleParameter sendDate = new OracleParameter();
                            sendDate.Value = now;
                            sendDate.ParameterName = "sendDate";
                            cmd.Parameters.Add(sendDate);

                           
                            OracleParameter reqId = new OracleParameter();
                                    reqId.Value = Req_id;
                                    reqId.ParameterName = "txtReqId";
                                    cmd.Parameters.Add(reqId);

                                    
                                    bool IsRequestNEWSuccess = Convert.ToBoolean(cmd.ExecuteNonQuery());

                                    cmd.Parameters.Clear();

                                    if (IsRequestNEWSuccess)
                                    {
                                        var _ApprovalRequestEmails = new ApprovalRequestEmails();

                                        //send approval email 

                                        if (_ApprovalRequestEmails.sendAprovalEmail("NR", brCode, requestedEpf, recepientEmail, ccEmails, Req_id, requestReason.Trim(), quotNo.ToUpper().Trim(), requestedEpf, "", "", "A",v_make,v_model))
                                        {
                                            emailMgs = 1;
                                            outRequestID = requestID;
                                    rtn = true;
                                        }
                                        else
                                        {
                                          emailMgs = 0;
                                          outRequestID = requestID;
                                        }

                                       
                                    }                                                        

                            }                         
                      
                    }

                    //outRequestID = getReuestCount();
                }
               

            }
        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
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

    public bool getRequestedData(string reqID, out string Qref, out string reason, out DateTime reqDate)
    {
        bool mgs = false;
        Qref = ""; reason = ""; reqDate = DateTime.Now;


        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlCountQout = "SELECT COUNT(*) FROM quotation_approval_request qpr WHERE qpr.activeflag='Y' AND  qpr.request_id=:txtreqID";

                OracleParameter para1 = new OracleParameter();
                para1.Value = reqID.ToUpper().Trim();
                para1.ParameterName = "txtreqID";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountQout;

                int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countQuot == 1)
                {
                    string sqlGetQoutData = "SELECT qpr.qref_no,qpr.request_reason, qpr.datestamp, qpr.updated_date " +
                          "FROM quotation_approval_request qpr " +
                          "WHERE qpr.activeflag='Y' AND qpr.request_id = :txtreqID";

                    OracleParameter para2 = new OracleParameter();
                    para2.Value = reqID.ToUpper().Trim();
                    para2.ParameterName = "txtreqID";
                    cmd.Parameters.Add(para2);

                    cmd.CommandText = sqlGetQoutData;

                    //cmd.Parameters.Clear();

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            //if (!reader.IsDBNull(0))
                            //{
                            if (!reader.IsDBNull(0))
                            {
                                Qref = reader.GetString(0).ToString();
                            }
                            if (!reader.IsDBNull(1))
                            {
                                reason = reader.GetString(1);
                            }

                            if (!reader.IsDBNull(3))
                            {
                                reqDate = reader.GetDateTime(3);
                                //reqDate = reader.GetDateTime(2).ToString("dd MMM yyyy hh:mm ss tt");
                            }
                            else
                            {
                                if (!reader.IsDBNull(2))
                                {
                                    reqDate = reader.GetDateTime(2);
                                    //reqDate = reader.GetDateTime(2).ToString("dd MMM yyyy hh:mm ss tt");
                                }
                            }

                        }

                    }

                    mgs = true;
                }


            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }


        }

        return mgs;
    }

    public DataTable SuccessTableViewGV(string ReqID)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        try
        {
            string sqlSelectNotice = "SELECT * FROM quotation_documents qd WHERE qd.Activeflag='Y' AND qd.request_id = '" + ReqID.Trim() + "' ORDER BY qd.datestamp DESC";


            using (OracleDataAdapter oda = new OracleDataAdapter(sqlSelectNotice, oconn))
            {

                ds.Clear();

                oda.Fill(ds);

                if (ds != null)
                {
                    dt = ds.Tables[0];
                    //dt = ds.Tables[1];
                }

            }
        }
        catch (Exception)
        {
            dt = null;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return dt;
    }

    public DataTable PreviousRecTableViewGV(string RefNo)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        try
        {
            string sqlSelectDoc = "SELECT * FROM quotation_documents qd, quotation_approval_request qar " +
                                      "WHERE qar.qref_no ='" + RefNo.Trim() + "' AND qar.request_id = qd.request_id AND qd.activeflag='Y' ORDER BY qd.datestamp DESC";


            using (OracleDataAdapter oda = new OracleDataAdapter(sqlSelectDoc, oconn))
            {

                ds.Clear();

                oda.Fill(ds);

                if (ds != null)
                {
                    dt = ds.Tables[0];
                    //dt = ds.Tables[1];
                }

            }
        }
        catch (Exception)
        {
            dt = null;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return dt;
    }

    public bool quoationReqAvailibility(string quotNo, out string mgs)
    {
        bool IsAvailable = false;
        mgs = "";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                if (quoationAvailibility(quotNo.Trim().ToUpper()))
                {
                    if (oconn.State != ConnectionState.Open)
                    {
                        oconn.Open();
                    }
                    string result = "";

                    string sqlCountQout = "SELECT COUNT(*) FROM quotation.quotation_approval_request q WHERE q.activeflag='Y' AND q.qref_no = :txtQuotNo";

                    OracleParameter para1 = new OracleParameter();
                    para1.Value = quotNo.ToUpper().Trim();
                    para1.ParameterName = "txtQuotNo";
                    cmd.Parameters.Add(para1);


                    cmd.CommandText = sqlCountQout;

                    int countReq = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    cmd.Parameters.Clear();

                    if (countReq == 0)
                    {
                        mgs = "A";
                        IsAvailable = true;


                    }
                    else if (countReq > 0)
                    {
                        string sqlCountApproved = "SELECT COUNT(*) FROM quotation.quotation_approval_request q WHERE q.activeflag='Y' AND q.approved_status='Y' AND " +
                                                  " q.qref_no = :txtQuotNo";

                        OracleParameter paraQuotNo = new OracleParameter();
                        paraQuotNo.Value = quotNo.ToUpper().Trim();
                        paraQuotNo.ParameterName = "txtQuotNo";
                        cmd.Parameters.Add(paraQuotNo);


                        cmd.CommandText = sqlCountApproved;

                        int countReqApproved = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                        cmd.Parameters.Clear();

                        if (countReqApproved > 0)
                        {
                            mgs = "The quotation (" + quotNo + ") has already been approved..!";
                            IsAvailable = false;
                        }
                        else
                        {
                            string sqlGetReqQout = "SELECT q.approved_status FROM quotation.quotation_approval_request q WHERE q.activeflag='Y' AND q.qref_no = :txtQuotNo";

                            OracleParameter para2 = new OracleParameter();
                            para2.Value = quotNo.ToUpper().Trim();
                            para2.ParameterName = "txtQuotNo";
                            cmd.Parameters.Add(para2);

                            cmd.CommandText = sqlGetReqQout;

                            result = cmd.ExecuteScalar().ToString();

                            cmd.Parameters.Clear();

                            if (result == "N")
                            {
                                mgs = result;
                                IsAvailable = true;
                            }
                            else if (result == "R")
                            {
                                string sqlCountReqRej = "SELECT COUNT(*) FROM quotation.quotation_approval_request q WHERE q.activeflag='Y'  AND q.approved_status='R' AND q.qref_no = :txtQuotNo";

                                OracleParameter paraReqRej = new OracleParameter();
                                paraReqRej.Value = quotNo.ToUpper().Trim();
                                paraReqRej.ParameterName = "txtQuotNo";
                                cmd.Parameters.Add(paraReqRej);

                                cmd.CommandText = sqlCountReqRej;

                                int ReqRej = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                                cmd.Parameters.Clear();

                                string sqlCountReqPen = "SELECT COUNT(*) FROM quotation.quotation_approval_request q WHERE q.activeflag='Y'  AND q.approved_status='N' AND q.qref_no = :txtQuotNo";

                                OracleParameter paraReqPen = new OracleParameter();
                                paraReqPen.Value = quotNo.ToUpper().Trim();
                                paraReqPen.ParameterName = "txtQuotNo";
                                cmd.Parameters.Add(paraReqPen);

                                cmd.CommandText = sqlCountReqPen;

                                int ReqPen = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                                cmd.Parameters.Clear();

                                mgs = result;
                                IsAvailable = true;
                            }
                        }

                    }
                    else
                    {
                        mgs = "A";
                        IsAvailable = true;
                    }




                }
                else
                {
                    mgs = "The reference number not found...!!";
                    IsAvailable = false;
                }

            }
        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }

        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return IsAvailable;
    }

    public string getUpdateDatawithPreClm(string quotNo, out string preClm, out string ccEmail)
    {
        string rtnMgs = ""; preClm = ""; ccEmail = "";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlCountQout = "SELECT COUNT(*) FROM quotation_approval_request qpr WHERE qpr.activeflag='Y' AND qpr.approved_status ='N' " +
                                      "AND qpr.qref_no=:txtQuotNo";


                OracleParameter para1 = new OracleParameter();
                para1.Value = quotNo.ToUpper().Trim();
                para1.ParameterName = "txtQuotNo";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountQout;

                int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countQuot == 1)
                {

                    string sqlGetReqReason = "SELECT qpr.request_reason, qpr.previous_claim_history ,e.cc_email_addresses " +
                                             "FROM quotation_approval_request qpr " +
                                             "INNER JOIN quotation_email_history e " +
                                             "ON e.request_id = qpr.request_id " +
                                             "WHERE qpr.activeflag='Y' AND qpr.approved_status ='N' " +
                                             "AND qpr.qref_no=:txtQuotNo";

                    OracleParameter para2 = new OracleParameter();
                    para2.Value = quotNo.ToUpper().Trim();
                    para2.ParameterName = "txtQuotNo";
                    cmd.Parameters.Add(para2);


                    cmd.CommandText = sqlGetReqReason;

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                rtnMgs = reader.GetString(0).ToUpper();
                            }
                            if (!reader.IsDBNull(1))
                            {
                                preClm = reader.GetString(1).Trim();
                            }
                            if (!reader.IsDBNull(2))
                            {
                                ccEmail = reader.GetString(2).Trim();
                            }
                        }
                    }

                    //rtnMgs = cmd.ExecuteScalar().ToString();

                    //cmd.Parameters.Clear();

                }
            }
        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }

        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return rtnMgs;
    }

    public string getUpdateData(string quotNo)
    {
        string rtnMgs = "";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlCountQout = "SELECT COUNT(*) FROM quotation_approval_request qpr WHERE qpr.activeflag='Y' AND qpr.approved_status ='N' " +
                                      "AND qpr.qref_no=:txtQuotNo";


                OracleParameter para1 = new OracleParameter();
                para1.Value = quotNo.ToUpper().Trim();
                para1.ParameterName = "txtQuotNo";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountQout;

                int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countQuot == 1)
                {

                    string sqlGetReqReason = "SELECT qpr.request_reason FROM quotation_approval_request qpr WHERE qpr.activeflag='Y' AND qpr.approved_status ='N' " +
                                          "AND qpr.qref_no=:txtQuotNo";

                    OracleParameter para2 = new OracleParameter();
                    para2.Value = quotNo.ToUpper().Trim();
                    para2.ParameterName = "txtQuotNo";
                    cmd.Parameters.Add(para2);


                    cmd.CommandText = sqlGetReqReason;

                    rtnMgs = cmd.ExecuteScalar().ToString();

                    cmd.Parameters.Clear();

                }
            }
        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }

        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return rtnMgs;
    }

    public string doCountRejectionPending(string quotNo, out bool cancellBtn)
    {
        string result = "";
        cancellBtn = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                if (quoationAvailibility(quotNo.Trim().ToUpper()))
                {
                    if (oconn.State != ConnectionState.Open)
                    {
                        oconn.Open();
                    }

                    string sqlCountReqRej = "SELECT COUNT(*) FROM quotation.quotation_approval_request q WHERE q.activeflag='Y'  AND q.approved_status='R' AND q.qref_no = :txtQuotNo";

                    OracleParameter paraReqRej = new OracleParameter();
                    paraReqRej.Value = quotNo.ToUpper().Trim();
                    paraReqRej.ParameterName = "txtQuotNo";
                    cmd.Parameters.Add(paraReqRej);

                    cmd.CommandText = sqlCountReqRej;

                    int ReqRej = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    cmd.Parameters.Clear();

                    string sqlCountReqPen = "SELECT COUNT(*) FROM quotation.quotation_approval_request q WHERE q.activeflag='Y'  AND q.approved_status='N' AND q.qref_no = :txtQuotNo";

                    OracleParameter paraReqPen = new OracleParameter();
                    paraReqPen.Value = quotNo.ToUpper().Trim();
                    paraReqPen.ParameterName = "txtQuotNo";
                    cmd.Parameters.Add(paraReqPen);

                    cmd.CommandText = sqlCountReqPen;

                    int ReqPen = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    cmd.Parameters.Clear();

                    var endc = new EncryptDecrypt();
                    string Rejectedhistory = "javascript:window.open('./Rejectedhistory/Rejectedhistory.aspx?_=" + endc.Encrypt(quotNo.ToUpper().Trim()) +
                                "','newwindow','width=570, height=435, toolbar=no, location=no, directories=no," +
                                "status=no, menubar=no, scrollbars=yes, resizable=yes, copyhistory=no'); return false;";

                    if (ReqRej > 1 && ReqPen == 0)
                    {
                        result = "<div style='width:100%;padding-right: 5px;float:right;margin-bottom: 5px;text-align:center;font-size: 15px;font-family: Oxygen,Arial, Helvetica, sans-serif;color: #d47801;text-align:right'>" +
                                 "<a style='color:#d47801;cursor: pointer'  onclick=\"" + Rejectedhistory + "\">View rejected request  (" + ReqRej + " times) !</a></div>";
                    }
                    else if (ReqRej > 1 && ReqPen > 0)
                    {
                        cancellBtn = true;
                        result = "<div style='width:100%;padding-right: 5px;float:right;margin-bottom: 5px;text-align:center;font-size: 15px;font-family: Oxygen,Arial, Helvetica, sans-serif;color: #027f96;text-align:right'>" +
                                 "<a style='color:#d47801;cursor: pointer'  onclick=\"" + Rejectedhistory + "\">View rejected request  (" + ReqRej + " times) !</a></div>";
                    }
                    else if (ReqRej == 1 && ReqPen > 0)
                    {
                        cancellBtn = true;
                        result = "<div style='width:100%;padding-right: 5px;float:right;margin-bottom: 5px;text-align:center;font-size: 15px;font-family: Oxygen,Arial, Helvetica, sans-serif;color: #027f96;text-align:right'>" +
                                 "<b style=';color: #d47801'>" +
                                 "<a style='color:#d47801;cursor: pointer'  onclick=\"" + Rejectedhistory + "\" >View rejected request !</a></b></div>";
                    }
                    else if (ReqRej == 1)
                    {
                        result = "<div style='width:100%;padding-right: 5px;float:right;margin-bottom: 5px;text-align:center;font-size: 15px;font-family: Oxygen,Arial, Helvetica, sans-serif;color: #d47801;text-align:right'>" +
                                  "<a style='color:#d47801;cursor: pointer'  onclick=\"" + Rejectedhistory + "\" >View rejected request !</a></div>";
                    }
                    else if (ReqPen == 1)
                    {
                        cancellBtn = true;
                        result = "<div style='width:100%;padding-right: 5px;float:right;margin-bottom: 5px;text-align:center;font-size: 15px;font-family: Oxygen,Arial, Helvetica, sans-serif;color: #027f96;text-align:right'>" +
                                 "The reference's been awaiting an approval..!</div>";
                    }
                    //else
                    //{

                    //}
                }
            }
        }

        catch (Exception ex)
        {
            string exe = ex.ToString();
        }

        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;

    }

    public string newID(string idTyp, string queryTyp)
    {
        string newIDYear = "";
        string idPrefix = "";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                //getting server year

                string sqlGetServerYear = "SELECT SYSDATE FROM DUAL";

                cmd.CommandText = sqlGetServerYear;

                var serverCurrentYear = Convert.ToDateTime(cmd.ExecuteScalar().ToString());

                //var serverCurrentYear = Convert.ToDateTime("12/01/2020".ToString());

                if (idTyp == "ARID")
                {
                    idPrefix = "REQ";
                    #region getting REUEST ID year

                    string sqlGetARIDYearCount = "SELECT COUNT(b.datestamp) FROM quotation_approval_request b";
                    cmd.CommandText = sqlGetARIDYearCount;
                    int sqlGetARIDYearCountResult = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    if (sqlGetARIDYearCountResult > 0)
                    {
                        string sqlGetARIDYear = "SELECT MAX(b.datestamp) FROM quotation_approval_request b";

                        cmd.CommandText = sqlGetARIDYear;

                        var ARIDCurrentYear = Convert.ToDateTime(cmd.ExecuteScalar().ToString());

                        if (ARIDCurrentYear.Year == serverCurrentYear.Year)
                        {

                            if (queryTyp == "EXIST")
                            {
                                newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp)).ToString("000");
                            }
                            else if (queryTyp == "NEW")
                            {
                                newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp) + 1).ToString("000");
                            }
                        }
                        else if (serverCurrentYear > ARIDCurrentYear)
                        {
                            if (queryTyp == "EXIST")
                            {
                                newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp)).ToString("000");
                            }
                            else if (queryTyp == "NEW")
                            {
                                newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp) + 1).ToString("000");
                            }
                        }
                    }
                    else
                    {
                        if (queryTyp == "EXIST")
                        {
                            newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp)).ToString("000");
                        }
                        else if (queryTyp == "NEW")
                        {
                            newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp) + 1).ToString("000");
                        }

                    }

                    #endregion
                }
                else if (idTyp == "DocID")
                {
                    idPrefix = "DOC";
                    #region getting doc ID year

                    string sqlGetDocIDYearCount = "SELECT COUNT(c.DATE_TIME) FROM QUOTATION.BANK_QUOTATION_DOCUMENTS c";
                    cmd.CommandText = sqlGetDocIDYearCount;
                    int sqlGetDocIDYearCountResult = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    if (sqlGetDocIDYearCountResult > 0)
                    {
                        string sqlGetDocIDYear = "SELECT MAX(c.DATE_TIME) FROM QUOTATION.BANK_QUOTATION_DOCUMENTS c";

                        cmd.CommandText = sqlGetDocIDYear;

                        var DocIDCurrentYear = Convert.ToDateTime(cmd.ExecuteScalar().ToString());

                        if (DocIDCurrentYear.Year == serverCurrentYear.Year)
                        {

                            if (queryTyp == "EXIST")
                            {
                                newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp)).ToString("000");
                            }
                            else if (queryTyp == "NEW")
                            {
                                newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp) + 1).ToString("000");
                            }
                        }
                        else if (serverCurrentYear > DocIDCurrentYear)
                        {
                            if (queryTyp == "EXIST")
                            {
                                newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp)).ToString("000");
                            }
                            else if (queryTyp == "NEW")
                            {
                                newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp) + 1).ToString("000");
                            }
                        }
                    }
                    else
                    {
                        if (queryTyp == "EXIST")
                        {
                            newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp)).ToString("000");
                        }
                        else if (queryTyp == "NEW")
                        {
                            newIDYear = idPrefix + serverCurrentYear.Year.ToString() + Convert.ToInt32(getIDsCount(serverCurrentYear, idTyp) + 1).ToString("000");
                        }
                    }

                    #endregion
                }
            }
        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }

        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return newIDYear;
    }

    public int getIDsCount(DateTime serverTime, string idTyp)
    {

        int requestID = 0;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {

                if (oconn.State != ConnectionState.Open)
                {
                    oconn.Open();
                }

                string sqlCountIds = "";

                if (idTyp == "ARID")
                {
                    sqlCountIds = "SELECT COUNT(*) FROM QUOTATION.BANK_QUOTATION_DOCUMENTS c WHERE TO_CHAR(c.DATE_TIME,'yyyy') = TO_CHAR(:txtServerTime,'yyyy')";
                }
                else if (idTyp == "DocID")
                {
                    sqlCountIds = "SELECT COUNT(*) FROM QUOTATION.BANK_QUOTATION_DOCUMENTS c WHERE TO_CHAR(c.DATE_TIME,'yyyy') = TO_CHAR(:txtServerTime,'yyyy')";
                }

                cmd.CommandText = sqlCountIds;

                OracleParameter para1 = new OracleParameter();
                para1.Value = serverTime;
                para1.ParameterName = "txtServerTime";
                para1.DbType = DbType.DateTime;
                cmd.Parameters.Add(para1);

                requestID = Convert.ToInt32(cmd.ExecuteScalar().ToString());

            }
        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }

        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }
        return requestID;
    }

    public DataTable RejectedReqTableViewGV(string RefNo)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        try
        {
            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {
                string sqlCountRejection = "SELECT COUNT(*) FROM quotation_approval_request q " +
                                            "WHERE q.activeflag = 'Y' AND q.approved_status='R' AND q.qref_no= :txtRefNo";

                cmd.CommandText = sqlCountRejection;

                OracleParameter para1 = new OracleParameter();
                para1.Value = RefNo.ToUpper().Trim();
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);

                int countResult = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countResult > 0)
                {
                    string sqlSelectRejection = "SELECT * FROM quotation_approval_request q " +
                                         "WHERE q.activeflag = 'Y' AND q.approved_status='R' AND " +
                                         "q.qref_no='" + RefNo.Trim().ToUpper() + "' ORDER BY q.approved_date DESC";


                    using (OracleDataAdapter oda = new OracleDataAdapter(sqlSelectRejection, oconn))
                    {

                        ds.Clear();

                        oda.Fill(ds);

                        if (ds != null)
                        {
                            dt = ds.Tables[0];
                            //dt = ds.Tables[1];
                        }

                    }
                }


            }

        }
        catch (Exception)
        {
            dt = null;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return dt;
    }

    public DataTable ApprovalResponedDocTableViewGV(string ReqId)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        try
        {
            string sqlSelectDoc = "SELECT * FROM quotation_documents qd " +
                                      "WHERE qd.request_id ='" + ReqId + "' AND  qd.activeflag='Y' AND qd.approved_status !='N' ORDER BY qd.datestamp DESC";


            using (OracleDataAdapter oda = new OracleDataAdapter(sqlSelectDoc, oconn))
            {

                ds.Clear();

                oda.Fill(ds);

                if (ds != null)
                {
                    dt = ds.Tables[0];
                    //dt = ds.Tables[1];
                }

            }
        }
        catch (Exception)
        {
            dt = null;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return dt;
    }

    public bool ApprovalResponedReqDataByReqId(string ReqId, out string reqEPF, out string respEpf,
                                                    out string reqIP, out string respIP,
                                                    out string reqDate, out string respDate,
                                                    out string reqReason, out string respRemark, out DateTime reqTimeTooltip,
                                                    out DateTime RespTimeTooltip, out string apprStatus, out string NCBvalue, out string NCBPremium,
                                                    out string MRvalue, out string MRpremium, out string speDiscount, out string clmHistory, out string approver)
    {
        reqEPF = ""; respEpf = ""; reqIP = ""; respIP = ""; reqDate = ""; respDate = ""; reqReason = ""; respRemark = ""; apprStatus = ""; NCBPremium = "";
        NCBvalue = ""; MRvalue = ""; MRpremium = ""; speDiscount = ""; clmHistory = ""; approver = "";
        bool rtn = false;
        reqTimeTooltip = DateTime.Now; RespTimeTooltip = DateTime.Now;

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        try
        {
            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {
                string sqlCountRejection = "SELECT COUNT(*) FROM quotation_approval_request q " +
                                            "WHERE q.activeflag = 'Y' AND q.approved_status !='N' AND q.request_id= :txtReqID";

                cmd.CommandText = sqlCountRejection;

                OracleParameter para1 = new OracleParameter();
                para1.Value = ReqId.ToUpper().Trim();
                para1.ParameterName = "txtReqID";
                cmd.Parameters.Add(para1);

                int countResult = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countResult > 0)
                {
                    string sqlGetRejectionData = "SELECT " +
                                                "q.requested_epf, q.approved_epf, " +
                                                "q.requested_ip, q.approved_ip, " +
                                                "q.DATESTAMP, q.approved_date, " +
                                                "q.request_reason, q.remarks, q.approved_status, " +
                                                "d.qncb_vl, d.qncb_dis, d.qmr_vl, d.qmr_dis, d.qdis_vl, d.qbtype, q.previous_claim_history,q.approver_epf " +
                                                " FROM quotation_approval_request q,  issued_quotations d " +
                                                " WHERE d.qref_no = q.qref_no AND q.activeflag = 'Y' AND q.approved_status !='N' AND q.request_id= :txtreqID " +
                                                " ORDER BY q.approved_date DESC";



                    OracleParameter para2 = new OracleParameter();
                    para2.Value = ReqId.ToUpper().Trim();
                    para2.ParameterName = "txtreqID";
                    cmd.Parameters.Add(para2);

                    cmd.CommandText = sqlGetRejectionData;

                    //cmd.Parameters.Clear();

                    OracleDataReader reader = cmd.ExecuteReader();
                    cmd.Parameters.Clear();
                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            //if (!reader.IsDBNull(0))
                            //{
                            if (!reader.IsDBNull(0))
                            {
                                reqEPF = reader.GetString(0).ToString();
                            }
                            if (!reader.IsDBNull(1))
                            {
                                respEpf = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                reqIP = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                respIP = reader.GetString(3);
                            }

                            var _TimeCalcuator = new TimeCalcuator();
                            if (!reader.IsDBNull(4))
                            {
                                reqTimeTooltip = reader.GetDateTime(4);
                                reqDate = _TimeCalcuator.UpdatedTimeCalculator(Convert.ToDateTime(reader.GetDateTime(4).ToString()));

                            }
                            if (!reader.IsDBNull(5))
                            {
                                RespTimeTooltip = reader.GetDateTime(5);
                                respDate = _TimeCalcuator.UpdatedTimeCalculator(Convert.ToDateTime(reader.GetDateTime(5).ToString()));

                            }
                            if (!reader.IsDBNull(6))
                            {
                                reqReason = reader.GetString(6);
                            }
                            if (!reader.IsDBNull(7))
                            {
                                respRemark = reader.GetString(7);
                            }
                            if (!reader.IsDBNull(8))
                            {
                                apprStatus = reader.GetString(8);
                            }
                            if (!reader.IsDBNull(9))
                            {
                                NCBvalue = reader.GetString(9);
                                NCBPremium = reader.GetDouble(10).ToString("###,###.00");
                            }
                            if (!reader.IsDBNull(11))
                            {
                                MRvalue = reader.GetDouble(11).ToString();
                                MRpremium = reader.GetDouble(12).ToString("###,###.00");
                            }

                            if (!reader.IsDBNull(13))
                            {
                                speDiscount = reader.GetString(13).ToUpper() + " - " + reader.GetDouble(14).ToString() + "%";

                            }
                            else
                            {
                                speDiscount = "<i>Not available</i>";
                            }

                            if (!reader.IsDBNull(15))
                            {
                                clmHistory = reader.GetString(15);

                            }
                            else
                            {
                                clmHistory = "<i>Not available</i>";
                            }

                            if (!reader.IsDBNull(16))
                            {
                                approver = reader.GetString(16);
                            }
                            rtn = true;
                        }

                    }
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

    public DataTable CurrentReqTableViewGV(string RefNo)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();



        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        try
        {
            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {
                string sqlDocCount = "SELECT COUNT(*) FROM quotation_documents d, quotation_approval_request r " +
                                    " WHERE d.request_id = r.request_id AND r.qref_no=:txtRefNo AND r.activeflag='Y' AND d.approved_status = 'N'";

                cmd.CommandText = sqlDocCount;

                OracleParameter para1 = new OracleParameter();
                para1.Value = RefNo;
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);

                int DocCount = Convert.ToInt32(cmd.ExecuteOracleScalar().ToString());

                string sqlSelectDoc = "";
                if (DocCount > 0)
                {
                    sqlSelectDoc = "SELECT d.approved_status, d.request_id, d.datestamp, d.document_url,d.activeflag  FROM quotation_documents d, quotation_approval_request r " +
                                      "  WHERE d.request_id = r.request_id AND r.qref_no='" + RefNo + "' AND r.activeflag='Y' AND d.approved_status = 'N' ORDER BY d.datestamp DESC";


                }
                else
                {
                    sqlSelectDoc = "SELECT ar.approved_status, ar.request_id, ar.datestamp,'' AS document_url,ar.activeflag  " +
                                    " FROM quotation_approval_request ar" +
                                     " WHERE ar.qref_no='" + RefNo + "' AND ar.activeflag='Y' AND ar.approved_status ='N' ORDER BY ar.datestamp DESC";


                }

                using (OracleDataAdapter oda = new OracleDataAdapter(sqlSelectDoc, oconn))
                {

                    ds.Clear();

                    oda.Fill(ds);

                    if (ds != null)
                    {
                        dt = ds.Tables[0];
                        //dt = ds.Tables[1];
                    }

                }
            }

        }
        catch (Exception exe)
        {
            dt = null;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return dt;
    }

    public DataTable PreviousReqTableViewGV(string RefNo)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();



        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        try
        {
            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {
                string sqlDocCount = "SELECT COUNT(*) FROM quotation_documents d, quotation_approval_request r " +
                                    " WHERE d.request_id = r.request_id AND r.qref_no=:txtRefNo AND r.activeflag='Y' AND d.approved_status = 'R'";

                cmd.CommandText = sqlDocCount;

                OracleParameter para1 = new OracleParameter();
                para1.Value = RefNo;
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);

                int DocCount = Convert.ToInt32(cmd.ExecuteOracleScalar().ToString());

                string sqlSelectDoc = "";
                if (DocCount > 0)
                {
                    sqlSelectDoc = "SELECT d.approved_status, d.request_id,  r.approved_date AS datestamp, d.document_url,d.activeflag  FROM quotation_documents d, quotation_approval_request r " +
                                      "  WHERE d.request_id = r.request_id AND r.qref_no='" + RefNo + "' AND r.activeflag='Y' AND d.approved_status = 'R' ORDER BY   d.datestamp DESC";


                }
                else
                {
                    sqlSelectDoc = "SELECT ar.approved_status, ar.request_id, ar.approved_date AS datestamp,'' AS document_url,ar.activeflag  " +
                                    " FROM quotation_approval_request ar" +
                                     " WHERE ar.qref_no='" + RefNo + "' AND ar.activeflag='Y' AND ar.approved_status ='R' ORDER BY ar.approved_date DESC";


                }

                using (OracleDataAdapter oda = new OracleDataAdapter(sqlSelectDoc, oconn))
                {

                    ds.Clear();

                    oda.Fill(ds);

                    if (ds != null)
                    {
                        dt = ds.Tables[0];
                        //dt = ds.Tables[1];
                    }

                }
            }

        }
        catch (Exception exe)
        {
            dt = null;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return dt;
    }
    public bool cancelRequest(string refNo, string cancelledEpf, out int falseRtn, out string outReqId)
    {
        bool rtnResult = false;
        falseRtn = 0;
        outReqId = "";

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

                string sqlCountVaildRef = "SELECT COUNT(*) FROM quotation_approval_request q " +
                                          "WHERE q.qref_no= :txtRefNo AND q.activeflag='Y' AND q.approved_status = 'N'";

                cmd.CommandText = sqlCountVaildRef;

                OracleParameter para1 = new OracleParameter();
                para1.Value = refNo.Trim().ToUpper();
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);

                int CountVaildRef = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (CountVaildRef == 1)
                {
                    string sqlGetReqID = "SELECT q.request_id  FROM quotation_approval_request q " +
                                         "WHERE q.qref_no= :txtRefNo AND q.activeflag='Y' AND q.approved_status = 'N'";

                    cmd.CommandText = sqlGetReqID;

                    OracleParameter para2 = new OracleParameter();
                    para2.Value = refNo.Trim().ToUpper();
                    para2.ParameterName = "txtRefNo";
                    cmd.Parameters.Add(para2);

                    reqId = cmd.ExecuteScalar().ToString();

                    outReqId = reqId;
                    cmd.Parameters.Clear();

                    string CancelReqUpdate = "UPDATE quotation_approval_request u SET " +
                                             " u.activeflag='N' , u.cancelled_epf=:txtCancelEPF , u.cancelled_ip=:txtIPAddress, u.cancelled_date=SYSDATE " +

                                             " WHERE u.request_id=:txtreqId  AND u.activeflag='Y' AND u.approved_status = 'N'";

                    cmd.CommandText = CancelReqUpdate;




                    OracleParameter para3 = new OracleParameter();
                    para3.Value = reqId;
                    para3.ParameterName = "txtreqId";
                    cmd.Parameters.Add(para3);


                    OracleParameter para4 = new OracleParameter();
                    para4.Value = cancelledEpf.Trim();
                    para4.ParameterName = "txtCancelEPF";
                    cmd.Parameters.Add(para4);

                    var _DeviceFinder = new DeviceFinder();

                    OracleParameter para5 = new OracleParameter();
                    para5.Value = _DeviceFinder.GetDeviceIPAddress();
                    para5.ParameterName = "txtIPAddress";
                    cmd.Parameters.Add(para5);

                    int exce = Convert.ToInt32(cmd.ExecuteNonQuery());

                    cmd.Parameters.Clear();

                    if (exce > 0)
                    {
                        string sqlCountDocAvail = "SELECT COUNT(*) FROM quotation_documents d " +
                                                  "WHERE d.request_id=:txtreqId AND d.activeflag='Y' AND d.approved_status='N'";

                        cmd.CommandText = sqlCountDocAvail;

                        OracleParameter para6 = new OracleParameter();
                        para6.Value = reqId;
                        para6.ParameterName = "txtreqId";
                        cmd.Parameters.Add(para6);

                        int DocAvail = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                        if (DocAvail > 0)
                        {
                            string sqlUpdateDocAvail = "UPDATE quotation_documents d SET d.activeflag='N' " +
                                                       " WHERE d.request_id =:txtreqId AND d.activeflag='Y' AND d.approved_status = 'N'";

                            cmd.CommandText = sqlUpdateDocAvail;

                            OracleParameter para7 = new OracleParameter();
                            para7.Value = reqId;
                            para7.ParameterName = "txtreqId";
                            cmd.Parameters.Add(para7);

                            int docExce = Convert.ToInt32(cmd.ExecuteNonQuery());

                            cmd.Parameters.Clear();

                            if (docExce > 0)
                            {
                                rtnResult = true;
                            }
                        }
                        else
                        {
                            rtnResult = true;
                        }
                    }
                    else
                    {
                        rtnResult = false;
                    }

                }
                else
                {
                    string sqlRefApprovalStatus = "SELECT COUNT(*) FROM quotation_approval_request q " +
                                        "WHERE q.qref_no= :txtRefNo AND q.activeflag='Y' AND q.approved_status = 'Y'";

                    cmd.CommandText = sqlRefApprovalStatus;

                    OracleParameter para8 = new OracleParameter();
                    para8.Value = refNo.Trim().ToUpper();
                    para8.ParameterName = "txtRefNo";
                    cmd.Parameters.Add(para8);

                    int approved = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    if (approved > 0)
                    {
                        falseRtn = 1;// already approved
                    }
                    else
                    {
                        string sqlRefCancelStatus = "SELECT COUNT(*) FROM quotation_approval_request q " +
                                         "WHERE q.qref_no= :txtRefNo AND q.activeflag='N' AND q.approved_status = 'N'";

                        cmd.CommandText = sqlRefCancelStatus;

                        OracleParameter para9 = new OracleParameter();
                        para9.Value = refNo.Trim().ToUpper();
                        para9.ParameterName = "txtRefNo";
                        cmd.Parameters.Add(para9);

                        int Cancelled = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                        if (Cancelled > 0)
                        {
                            falseRtn = -2;// already cancelled
                        }
                        else
                        {
                            falseRtn = -1;// already rejected
                        }
                    }
                    rtnResult = false;
                }



            }
        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }

        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return rtnResult;

    }

    public DataTable CancellationSuccessTableViewGV(string ReqID)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        try
        {
            string sqlSelectNotice = "SELECT * FROM quotation_documents qd WHERE qd.Activeflag='N' AND qd.request_id = '" + ReqID.Trim() + "' ORDER BY qd.datestamp DESC";


            using (OracleDataAdapter oda = new OracleDataAdapter(sqlSelectNotice, oconn))
            {

                ds.Clear();

                oda.Fill(ds);

                if (ds != null)
                {
                    dt = ds.Tables[0];
                    //dt = ds.Tables[1];
                }

            }
        }
        catch (Exception)
        {
            dt = null;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return dt;
    }

    public string getCancelledRequestedData(string reqID, out string Qref, out string reason, out DateTime CancelledDate)
    {
        string mgs = ""; Qref = ""; reason = ""; CancelledDate = DateTime.Now;


        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlCountQout = "SELECT COUNT(*) FROM quotation_approval_request qpr WHERE qpr.activeflag='N' AND  qpr.request_id=:txtreqID";

                OracleParameter para1 = new OracleParameter();
                para1.Value = reqID.ToUpper().Trim();
                para1.ParameterName = "txtreqID";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountQout;

                int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countQuot == 1)
                {
                    string sqlGetQoutData = "SELECT c.qref_no,c.request_reason, c.cancelled_date  " +
                          "FROM quotation_approval_request c " +
                          "WHERE c.activeflag='N' AND c.request_id = :txtreqID";

                    OracleParameter para2 = new OracleParameter();
                    para2.Value = reqID.ToUpper().Trim();
                    para2.ParameterName = "txtreqID";
                    cmd.Parameters.Add(para2);

                    cmd.CommandText = sqlGetQoutData;

                    //cmd.Parameters.Clear();

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            //if (!reader.IsDBNull(0))
                            //{
                            if (!reader.IsDBNull(0))
                            {
                                Qref = reader.GetString(0).ToString();
                            }
                            if (!reader.IsDBNull(1))
                            {
                                reason = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                CancelledDate = reader.GetDateTime(2);
                                //reqDate = reader.GetDateTime(2).ToString("dd MMM yyyy hh:mm ss tt");
                            }

                        }

                    }


                }

            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }


        }

        return mgs;
    }

    public bool getCountRequesteCancel(string Qref)
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

                string sqlCountQout = "SELECT COUNT(*) FROM quotation_approval_request qpr WHERE qpr.activeflag='Y'  AND qpr.approved_status='N' AND  qpr.qref_no=:txtRefNo";

                OracleParameter para1 = new OracleParameter();
                para1.Value = Qref.ToUpper().Trim();
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountQout;

                int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countQuot > 0)
                {
                    rtn = true;

                }

            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
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

    public bool getDataForRequestCancel(string Qref, out string ReqId, out string ReqTime, out string ReqReason, out string ReqEpf, out string ReqIp, out string appStatus, out DateTime ActualTime)
    {
        bool rtn = false;
        ReqId = ""; ReqTime = ""; ReqEpf = ""; ReqIp = ""; ReqReason = ""; appStatus = ""; ActualTime = DateTime.Now;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlCountQout = "SELECT COUNT(*) FROM quotation_approval_request qpr WHERE qpr.activeflag='Y'  AND qpr.approved_status='N' AND  qpr.qref_no=:txtRefNo";

                OracleParameter para1 = new OracleParameter();
                para1.Value = Qref.ToUpper().Trim();
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountQout;

                int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countQuot == 1)
                {
                    string sqlGetDataQout = "SELECT qpr.request_id, qpr.datestamp,  " +
                                            "qpr.request_reason, qpr.requested_epf, " +
                                            "qpr.requested_ip , qpr.approved_status " +
                                            "FROM quotation_approval_request qpr " +
                                            "WHERE qpr.activeflag='Y'  AND qpr.approved_status='N' AND  qpr.qref_no=:txtRefNo";

                    OracleParameter para2 = new OracleParameter();
                    para2.Value = Qref.ToUpper().Trim();
                    para2.ParameterName = "txtRefNo";
                    cmd.Parameters.Add(para2);

                    cmd.CommandText = sqlGetDataQout;

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                ReqId = reader.GetString(0);
                            }

                            if (!reader.IsDBNull(1))
                            {
                                ActualTime = reader.GetDateTime(1);
                                var _TimeCalcuator = new TimeCalcuator();

                                ReqTime = _TimeCalcuator.UpdatedTimeCalculator(Convert.ToDateTime(reader.GetDateTime(1).ToString()));

                            }

                            if (!reader.IsDBNull(2))
                            {
                                ReqReason = reader.GetString(2);
                            }

                            if (!reader.IsDBNull(3))
                            {
                                ReqEpf = reader.GetString(3);
                            }

                            if (!reader.IsDBNull(4))
                            {
                                ReqIp = reader.GetString(4);
                            }
                            if (!reader.IsDBNull(5))
                            {
                                if (reader.GetString(5) == "N")
                                {
                                    appStatus = "<span style='font-weight:bold;font-size:18px;color: #e29104'>Pending</sapn>";
                                }
                                else if (reader.GetString(5) == "Y")
                                {
                                    appStatus = "<span style='font-weight:bold;font-size:18px;color: #5fb601'>Approved</sapn>";
                                }
                                else if (reader.GetString(5) == "R")
                                {
                                    appStatus = "<span style='font-weight:bold;font-size:18px;color: #e29104'>Rejected</sapn>";
                                }

                            }
                            rtn = true;
                        }
                        reader.Close();

                    }

                }

            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
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

    public DataTable cancelRequestDocTableViewGV(string RefNo)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();



        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        try
        {
            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {
                string sqlDocCount = "SELECT COUNT(d.request_id) FROM quotation_documents d, quotation_approval_request r " +
                                    " WHERE d.request_id = r.request_id AND r.qref_no=:txtRefNo AND r.activeflag='Y' AND d.approved_status = 'N'";

                cmd.CommandText = sqlDocCount;

                OracleParameter para1 = new OracleParameter();
                para1.Value = RefNo;
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);

                int DocCount = Convert.ToInt32(cmd.ExecuteOracleScalar().ToString());

                cmd.Parameters.Clear();

                if (DocCount > 0)
                {
                    string sqlSelectID = "SELECT d.request_id FROM quotation_documents d, quotation_approval_request r " +
                                    " WHERE d.request_id = r.request_id AND r.qref_no=:txtRefNo AND r.activeflag='Y' AND d.approved_status = 'N'";

                    cmd.CommandText = sqlSelectID;

                    OracleParameter para2 = new OracleParameter();
                    para2.Value = RefNo;
                    para2.ParameterName = "txtRefNo";
                    cmd.Parameters.Add(para2);

                    string reqId = cmd.ExecuteScalar().ToString();

                    cmd.Parameters.Clear();

                    string sqlSelectDoc = "SELECT * FROM quotation_documents d " +
                                            "WHERE d.activeflag='Y' AND d.request_id ='" + reqId + "'";

                    using (OracleDataAdapter oda = new OracleDataAdapter(sqlSelectDoc, oconn))
                    {

                        ds.Clear();

                        oda.Fill(ds);

                        if (ds != null)
                        {
                            dt = ds.Tables[0];
                            //dt = ds.Tables[1];
                        }

                    }

                }




            }

        }
        catch (Exception exe)
        {
            dt = null;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return dt;
    }

    public bool UndocancelRequest(string refNo, string reqId, string undoEpf)
    {
        bool rtnResult = false;



        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {
                string sqlCountCancel = "SELECT COUNT(*) FROM quotation_approval_request c " +
                                        " WHERE  c.request_id=:txtReqId  AND c.activeflag='N' AND c.approved_status = 'N' AND c.qref_no=:txtRefNo ";

                cmd.CommandText = sqlCountCancel;

                OracleParameter para1 = new OracleParameter();
                para1.Value = refNo;
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);

                OracleParameter para2 = new OracleParameter();
                para2.Value = reqId;
                para2.ParameterName = "txtReqId";
                cmd.Parameters.Add(para2);

                int count = Convert.ToInt32(cmd.ExecuteOracleScalar().ToString());

                cmd.Parameters.Clear();

                if (count == 1)
                {
                    string UndoCancelReqUpdate = "UPDATE quotation_approval_request u SET " +
                                          " u.activeflag='Y' , u.cancel_undo_epf=:txtUndoEpf , " +
                                          " u.cancel_undo_ip=:txtIPAddress, u.cancel_undo_date=SYSDATE " +

                                          " WHERE u.request_id=:txtReqId  AND u.activeflag='N' AND u.approved_status = 'N' AND u.qref_no=:txtRefNo";

                    cmd.CommandText = UndoCancelReqUpdate;



                    OracleParameter para3 = new OracleParameter();
                    para3.Value = refNo;
                    para3.ParameterName = "txtRefNo";
                    cmd.Parameters.Add(para3);

                    OracleParameter para4 = new OracleParameter();
                    para4.Value = reqId;
                    para4.ParameterName = "txtReqId";
                    cmd.Parameters.Add(para4);

                    OracleParameter para5 = new OracleParameter();
                    para5.Value = undoEpf;
                    para5.ParameterName = "txtUndoEpf";
                    cmd.Parameters.Add(para5);

                    var _DeviceFinder = new DeviceFinder();

                    OracleParameter para8 = new OracleParameter();
                    para8.Value = _DeviceFinder.GetDeviceIPAddress();
                    para8.ParameterName = "txtIPAddress";
                    cmd.Parameters.Add(para8);

                    int updateRtn = Convert.ToInt32(cmd.ExecuteNonQuery());

                    cmd.Parameters.Clear();

                    if (updateRtn > 0)
                    {
                        string sqlCountDocAvail = "SELECT COUNT(*) FROM quotation_documents d " +
                                                 "WHERE d.request_id=:txtreqId AND d.activeflag='N' AND d.approved_status='N'";

                        cmd.CommandText = sqlCountDocAvail;

                        OracleParameter para6 = new OracleParameter();
                        para6.Value = reqId;
                        para6.ParameterName = "txtreqId";
                        cmd.Parameters.Add(para6);

                        int DocAvail = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                        if (DocAvail > 0)
                        {
                            string sqlUpdateDocAvail = "UPDATE quotation_documents d SET d.activeflag='Y' " +
                                                       " WHERE d.request_id =:txtreqId AND d.activeflag='N' AND d.approved_status = 'N'";

                            cmd.CommandText = sqlUpdateDocAvail;

                            OracleParameter para7 = new OracleParameter();
                            para7.Value = reqId;
                            para7.ParameterName = "txtreqId";
                            cmd.Parameters.Add(para7);

                            int docExce = Convert.ToInt32(cmd.ExecuteNonQuery());

                            cmd.Parameters.Clear();

                            if (docExce > 0)
                            {
                                rtnResult = true;
                            }
                        }
                        else
                        {
                            rtnResult = true;
                        }
                    }
                }

            }
        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }

        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return rtnResult;

    }

    public bool bracnchLevelQuoationAvailibility(string quotNo, int Userbrcode)
    {
        bool IsAvailable = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlCountQout = "";


                //if (Userbrcode == 10)
                //{
                //    sqlCountQout = "SELECT COUNT(*) FROM quotation.issued_quotations qut " +
                //                   "INNER JOIN genpay.branch br " +
                //                   "ON qut.qbranch = br.brcod " +
                //                   "WHERE br.brcod IS NOT NULL " +
                //                   "AND  qut.qref_no =  :txtQuotNo " +
                //                   "AND qut.qmake IS NOT NULL AND qut.qfuel IS NOT NULL AND (qut.qyear IS NOT NULL OR qut.qyear = '0')  " +
                //                   "AND qut.qcateg !='PRIVATE LORRY' AND qut.qcateg !='THREE WHEELER PRIVATE USE'  ";

                //}
                //else
                //{
                if (Userbrcode != 0)
                {
                    sqlCountQout = "SELECT COUNT(*) FROM quotation.issued_quotations qut " +
                             "INNER JOIN genpay.branch br " +
                             "ON qut.qbranch = br.brcod " +
                             "WHERE br.brcod IS NOT NULL " +
                             "AND  qut.qref_no =  :txtQuotNo " +
                             "AND qut.qmake IS NOT NULL AND qut.qfuel IS NOT NULL AND (qut.qyear IS NOT NULL OR qut.qyear = '0')  " +
                             "AND qut.qcateg !='PRIVATE LORRY' AND qut.qcateg !='THREE WHEELER PRIVATE USE'  ";

                    sqlCountQout += " AND  br.brcod=:txtUserbrcode ";

                    OracleParameter para2 = new OracleParameter();
                    para2.DbType = DbType.Int32;
                    para2.Value = Userbrcode;
                    para2.ParameterName = "txtUserbrcode";
                    cmd.Parameters.Add(para2);

                }
                //}


                OracleParameter para1 = new OracleParameter();
                para1.Value = quotNo.ToUpper().Trim();
                para1.ParameterName = "txtQuotNo";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountQout;

                int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countQuot == 1)
                {
                    IsAvailable = true;

                }
            }
        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }

        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return IsAvailable;
    }


}
