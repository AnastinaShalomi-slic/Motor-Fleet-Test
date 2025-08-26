using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Web.Services;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using ClosedXML.Excel;

public partial class SLICResponseTeam_SLICTicket : System.Web.UI.Page
{
    private string FromEmailid = ConfigurationManager.AppSettings["senderEmailId"];
    private string FromEmailPasscode = ConfigurationManager.AppSettings["senderEmailPasscode"];
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    ORCL_Connection orcl_con = new ORCL_Connection();
    Execute_sql _sql = new Execute_sql();
    Update_class exe_up = new Update_class();
    Ip_Class get_ip = new Ip_Class();
    EncryptDecrypt dc = new EncryptDecrypt();
    DeviceFinder findDev = new DeviceFinder();
    DeviceFinder df = new DeviceFinder();
    Insert_class exe_in = new Insert_class();
    List<String> emailIDForCC = new List<String>();

    List<String> bankRemarks = new List<String>();
    List<String> slicRemarks = new List<String>();

    DetailsForfireEmailReq emailSend = new DetailsForfireEmailReq();
    string shortDes = string.Empty;
    string days = string.Empty;
    string hours = string.Empty;
    string minutes = string.Empty;
    string seconds = string.Empty;

    string day_msg = string.Empty;
    string hours_msg = string.Empty;
    string minutes_msg = string.Empty;
    public string x = string.Empty;
    public string flag = string.Empty;
    public string quo_no_temp = string.Empty;


    string qrefNo, UsrId = "", reqTyp = "", mgs = "";
    string rID = "";
    string filePath = "", ext = "";
    bool cancelBtn = false;
    List<string> optionList;
    private string SessionUserId = "";
    private string SessionbrCode = "";
    int emailRtn = 0;
    string PreviousClaim = "", ccEmailIds = "";

    protected void Page_Load(object sender, EventArgs e)
    {


        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);


        if (!Page.IsPostBack)
        {
            try
            {
                if (Session["UserId"].ToString() != "")
                {


                    UserId.Value = Session["UserId"].ToString();
                    brCode.Value = Session["brcode"].ToString();
                    user_epf.Value = Session["EPFNum"].ToString();

                    mainAlert.Visible = false;
                    sucsessAlert.Visible = false;
                    errorAlert.Visible = false;
                    warningAlert.Visible = false;
                    infoAlert.Visible = false;

                    divNew.Visible = false;
                    divView.Visible = false;
                    hfTicketId.Value = "";

                    moreInfo.Visible = false;
                    this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
                    //this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(),
                    //ddlTicketStatus.SelectedValue.ToString().ToUpper().Trim(), ddl_bank.SelectedValue.ToString(), ddl_branch.SelectedValue.ToString(), null);

                }

                else
                {
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("SE".ToString()));
                    //Response.Redirect("~/ErrorPage/session_expired.aspx");
                }
            }
            catch (Exception ex)
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
            }

        }
        else
        {
            /* IF PAGE.POSTBACK ASSIGHEN SESSION VARIABLE */

            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
            hfTicketId.Value = "";
        }


    }


    [WebMethod]
    public static string[] Get_Ticket_No(string prefix)
    {
        List<string> ref_no = new List<string>();
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["CONN_STRING_ORCL"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = "select T_REF from QUOTATION.BANCASSU_INQU_TICKETS where T_REF like '%" + prefix.ToUpper() + "%' AND ROWNUM <= 10 order by T_CREATED_ON desc";
                cmd.Connection = conn;
                conn.Open();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ref_no.Add(string.Format("{0}", sdr["T_REF"]));
                    }
                }
                conn.Close();
            }
            return ref_no.ToArray();
        }
    }


    protected void GetDetailOfficers()
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetOfficer(bank_code.Value.ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    for (int i = 0; i < details.Rows.Count; i++)
                    {

                        //emailArea.Value = emailArea.Value+details.Rows[i]["EMAIL"].ToString()+" ";
                    }

                }
                else
                {
                    //emailArea.Value = "";

                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }
    }

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        

    }

    private void BindGrid()
    {
        GetQuoDetails(refNo.Value.ToString());

    }


    protected void GetQuoDetails(string Ref_No)
    {
        DataTable details = new DataTable();
        try
        {

            details = orcle_trans.GetRows(this._sql.GetUploadSLICTickets(Ref_No.ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    GridView1.DataSource = details;
                    GridView1.DataBind();

                    mainAlert.Visible = true;
                    sucsessAlert.Visible = false;
                    errorAlert.Visible = false;
                    infoAlert.Visible = false;
                    warningAlert.Visible = false;
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();


                    mainAlert.Visible = true;
                    sucsessAlert.Visible = false;
                    errorAlert.Visible = false;
                    infoAlert.Visible = false;
                    warningAlert.Visible = true;
                    warningContent.InnerText = "No Records to display!.";


                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = orcle_trans.Error_Message.ToString();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()), false);

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()), false);

        }
    }


    protected void IniGridview()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
    }

    protected void Upload(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        int i = 0;
        bool validate = false;
        int filecount = 0;
        int fileuploadcount = 0;
        try
        {

            //----upload start------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            if (!string.IsNullOrEmpty(ddlStatus.SelectedValue))
            {

                // Check File Prasent or not  
                if (FileUpload1.HasFiles)
                {
                    //int filecount = 0;
                    //int fileuploadcount = 0;
                    //check No of Files Selected  
                    filecount = FileUpload1.PostedFiles.Count();
                    if (filecount <= 10)
                    {
                        foreach (HttpPostedFile postfiles in FileUpload1.PostedFiles)
                        {
                            //Get The File Extension  
                            string filetype = Path.GetExtension(postfiles.FileName);
                            if (filetype.ToLower() == ".docx" || filetype.ToLower() == ".pdf" ||
                                filetype.ToLower() == ".txt" || filetype.ToLower() == ".doc" ||
                                filetype.ToLower() == ".jpg" || filetype.ToLower() == ".xlsx" ||
                                filetype.ToLower() == ".xls" || filetype.ToLower() == ".csv")
                            {
                                //Get The File Size In Bite  
                                double filesize = postfiles.ContentLength;
                                if (filesize < (1048576))
                                {
                                    fileuploadcount++;

                                    string filename = Path.GetFileName(postfiles.FileName);
                                    string contentType = postfiles.ContentType;
                                    using (Stream fs = postfiles.InputStream)
                                    {
                                        using (BinaryReader br = new BinaryReader(fs))
                                        {
                                            byte[] bytes = br.ReadBytes((Int32)fs.Length);
                                            i = fileuploadcount;

                                            using (OracleConnection connection = orcl_con.GetConnection())
                                            {
                                                string req_val = "select QUOTATION.bacassu_slic_ticket_seq.NEXTVAL from dual";
                                                int req_id = Convert.ToInt32(orcle_trans.GetString(req_val));

                                                string query = "insert into QUOTATION.BANCASSU_SLIC_ITUPDOC (T_NO,T_REF,T_NAME,T_CONTENT,T_DATA,CREATED_BY,CREATED_ON,T_FLAG,T_ACTIVE) values (:id,:T_ref,:Name, :ContentType, :Data, :create_by, :create_on, :active, :T_ACTIVE)";
                                                using (OracleCommand cmd = new OracleCommand(query))
                                                {
                                                    cmd.Connection = connection;
                                                    cmd.Parameters.AddWithValue(":id", req_id);
                                                    cmd.Parameters.AddWithValue(":T_ref", refNo.Value.ToString());
                                                    cmd.Parameters.AddWithValue(":Name", filename);
                                                    cmd.Parameters.AddWithValue(":ContentType", contentType);
                                                    cmd.Parameters.AddWithValue(":Data", bytes);
                                                    cmd.Parameters.AddWithValue(":create_by", user_epf.Value.ToString());
                                                    cmd.Parameters.AddWithValue(":create_on", System.DateTime.Now);
                                                    cmd.Parameters.AddWithValue(":active", "D"); //done R for remove
                                                    cmd.Parameters.AddWithValue(":T_ACTIVE", "Y");

                                                    connection.Open();
                                                    cmd.ExecuteNonQuery();
                                                    connection.Close();
                                                }
                                            }
                                        }

                                    }
                                    validate = true;
                                    //.Value = "1";





                                }
                                else
                                {
                                    mainAlert.Visible = true;
                                    sucsessAlert.Visible = false;
                                    warningAlert.Visible = false;
                                    infoAlert.Visible = false;
                                    errorAlert.Visible = true;
                                    errorContent.InnerText = postfiles.FileName + "- files not uploded size is greater then(1)MB.";
                                    errorContent2.InnerText = "Your File Size is(" + (filesize / (1024 * 1034)) + ") MB.";
                                    validate = false;
                                }
                            }
                            else
                            {
                                mainAlert.Visible = true;
                                sucsessAlert.Visible = false;
                                errorAlert.Visible = false;
                                infoAlert.Visible = false;
                                warningAlert.Visible = true;
                                warningContent.InnerText = postfiles.FileName + "- file type must be Word, Pdf, Excel or Jpg.";
                                validate = false;
                            }
                        }

                    }
                    else
                    {
                        mainAlert.Visible = true;
                        sucsessAlert.Visible = false;
                        errorAlert.Visible = false;
                        warningAlert.Visible = false;
                        infoAlert.Visible = false;

                        warningContent.InnerText = "you are select(" + filecount + ")files";
                        warningContent.InnerText = "Please select Maximum five(10) files !!!";
                        validate = false;
                    }

                }
                else
                {
                    mainAlert.Visible = true;
                    sucsessAlert.Visible = false;
                    errorAlert.Visible = false;
                    infoAlert.Visible = false;
                    warningAlert.Visible = true;
                    warningContent.InnerText = "Please select the file for upload !!!";
                    validate = false;
                }

                //Response.Redirect(Request.Url.AbsoluteUri);
                if (validate)
                {
                    mainAlert.Visible = true;
                    errorAlert.Visible = false;
                    warningAlert.Visible = false;
                    infoAlert.Visible = false;
                    sucsessAlert.Visible = true;
                    sucsessContent.InnerHtml = "Total File =(" + filecount + ")<br/> Uploded file =(" + fileuploadcount + ")<br/> Not Uploaded=(" + (filecount - fileuploadcount) + ")";
                    BindGrid();

                    isUploaded.Value = "Y";
                }

                else
                {

                }
            }
            //----upload end------------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

        }
        catch (Exception ex)
        {
            mainAlert.Visible = true;
            sucsessAlert.Visible = false;
            errorAlert.Visible = false;
            infoAlert.Visible = false;
            warningAlert.Visible = true;
            warningContent.InnerHtml = ex.Message;
            validate = false;
        }

    }



    protected void DownloadFile(object sender, EventArgs e)
    {

        ImageButton lnkDownload = sender as ImageButton;
        string[] arg = new string[4];
        arg = lnkDownload.CommandArgument.ToString().Split(';');
        int id = int.Parse(arg[0].ToString().Trim());

        byte[] bytes;
        string fileName, contentType;

        using (OracleConnection connection = orcl_con.GetConnection())
        {
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = "select T_NAME, T_DATA, T_CONTENT from QUOTATION.BANCASSU_BANK_ITUPDOC where T_NO=:Id";
                cmd.Parameters.AddWithValue(":Id", id);
                cmd.Connection = connection;
                connection.Open();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();

                    bytes = (byte[])sdr["T_DATA"];
                    contentType = sdr["T_CONTENT"].ToString();
                    fileName = sdr["T_NAME"].ToString();
                }
                connection.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
    protected void DownloadFileSLIC(object sender, EventArgs e)
    {

        ImageButton lnkDownload = sender as ImageButton;
        string[] arg = new string[4];
        arg = lnkDownload.CommandArgument.ToString().Split(';');
        int id = int.Parse(arg[0].ToString().Trim());

        byte[] bytes;
        string fileName, contentType;

        using (OracleConnection connection = orcl_con.GetConnection())
        {
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = "select T_NAME, T_DATA, T_CONTENT from QUOTATION.BANCASSU_SLIC_ITUPDOC where T_NO=:Id";
                cmd.Parameters.AddWithValue(":Id", id);
                cmd.Connection = connection;
                connection.Open();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();

                    bytes = (byte[])sdr["T_DATA"];
                    contentType = sdr["T_CONTENT"].ToString();
                    fileName = sdr["T_NAME"].ToString();
                }
                connection.Close();
            }
        }
        Response.Clear();
        Response.Buffer = true;
        Response.Charset = "";
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = contentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
        Response.BinaryWrite(bytes);
        Response.Flush();
        Response.End();
    }
    protected void RemoveFile(object sender, EventArgs e)
    {
        bool rtn = false;
        ImageButton lnkRemove = sender as ImageButton;

        string[] arg = new string[4];
        arg = lnkRemove.CommandArgument.ToString().Split(';');
        string REQ_ID = arg[0].ToString().Trim();
        rtn = exe_up.update_SLICdoc_table_ticket(REQ_ID, Session["UserId"].ToString().Trim());
        BindGrid();

    }
    protected void btnBack_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("index.html");

    }



    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        mainAlert.Visible = false;
        GridView1.PageIndex = e.NewPageIndex;
        BindGrid(); //bindgridview will get the data source and bind it again
    }

    protected void sendEmailToSLICOfficer(string refNo, string bankCode, string req_type)
    {
        this.GetEmailsrofOfficers(bankCode.Trim());
        string status = string.Empty;
        if (req_type == "D")
        {
            status = "Completed.";

        }

        else if (req_type == "R") { status = "Rejected."; }
        else if (req_type == "C") { status = "Canceled."; }
        else if (req_type == "M") { status = "Need more information."; }
        else if (req_type == "RM") { status = "Forward to Risk Managment."; }
        else if (req_type == "RI") { status = "Forward to Reinsurance."; }
        else { }
        //Getas400.as400_get_userBankEmail(hfbankUN.Value.ToString().ToLower(), out bancass_email);

        string txt_body = "Quick Response Team. Bank Ref. No - " + refNo + "<br/>Please check the system and get the response sent by SLIC.";

        string ccEmails = string.Empty;
        string toEmails = string.Empty;


        ccEmails = String.Join(", ", emailIDForCC);
        //toEmails = String.Join(", ", emailID);
        toEmails = bankEnmailId.Value.Trim().ToLower();

        emailSend.fireRequestDetails("SLIINQ", "", toEmails, ccEmails, refNo.ToString().Trim().ToString(), txt_body, "", FromEmailid);

    }


    protected void GetEmailsrofOfficers(string bank_code)
    {
        string officerEmails = string.Empty;
        string officerEmailsCC = string.Empty;
        //int rtnCount = 0;
        DataTable details = new DataTable();
        DataTable detailsforCC = new DataTable();
        try
        {

            /// second  table for CC user emails

            detailsforCC = orcle_trans.GetRows(this._sql.GetOfficerEmail(bank_code.ToString().Trim()), detailsforCC);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (detailsforCC.Rows.Count > 0)
                {
                    for (int i = 0; i < detailsforCC.Rows.Count; i++)
                    {
                        emailIDForCC.Add(detailsforCC.Rows[i]["EMAIL"].ToString());
                    }

                }
                else
                {
                    officerEmailsCC = "";

                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = orcle_trans.Error_Message.ToString();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("14".ToString()), false);

            }

        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = orcle_trans.Error_Message.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("14".ToString()), false);

        }
    }
    protected void sendSMStoAgent(string req_type, string bankCode, string ref_no, string tempmsg)
    {
        int rtnCount = 0;
        DataTable details = new DataTable();
        try
        {
            string officerContact = string.Empty;
            string mobile, brName, userName, sataus = string.Empty;

            if (req_type == "D")
            {
                sataus = "Completed.";

            }

            else if (req_type == "R") { sataus = "Rejected."; }
            else if (req_type == "C") { sataus = "Canceled."; }
            else if (req_type == "M") { sataus = "Need more information."; }
            else if (req_type == "RM") { sataus = "Forward to Risk Managment."; }
            else if (req_type == "RI") { sataus = "Forward to Reinsurance."; }
            else { }

            details = orcle_trans.GetRows(this._sql.GetOfficer(bankCode.ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    for (int i = 0; i < details.Rows.Count; i++)
                    {
                        officerContact = details.Rows[i]["CONTACT_NO"].ToString().Substring(1);
                        officerContact = "94" + officerContact;

                        string txt_body = "Quick Response Team. Bank Ref. No - " + ref_no + ". Inquiry closed.";

                        exe_in.Send_sms_to_customer(officerContact, "", txt_body, out rtnCount);


                    }

                }
                else
                {
                    officerContact = "";

                }
            }

            else
            {
                var endc = new EncryptDecrypt();
                string msg = orcle_trans.Error_Message.ToString();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("14".ToString()), false);

            }



        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("14".ToString()), false);

        }

    }

    protected void btfinished_Click(object sender, EventArgs e)
    {
        try
        {
            bool result1 = false;

            string Gen_t_id;
          

            result1 = exe_up.update_table_ticket(refNo.Value.ToString(), txtremark.Value, Session["UserId"].ToString(),"D");

            this.sendSMStoAgent("D", bank_code.Value.ToString(), refNo.Value.ToString(), "");
            this.sendEmailToSLICOfficer(refNo.Value.ToString(), bank_code.Value.ToString(), "D");



            /*if (result1)*/
            if (result1)
            {
                System.Threading.Thread.Sleep(3000);

                var endc = new EncryptDecrypt();
                string msg = "Successfully saved and email sent to bank.";
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()), false);

            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = "Syetem Error".ToString();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()), false);

            }
        }

        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()), false);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        IniGridview();
        BindGrid();
    }

    protected void btNewClick(object sender, EventArgs e)
    {
        divNew.Visible = true;
        divView.Visible = false;
        idNew.Visible = true;
        idView.Visible = false;
        txtRefNo.Text = "";
        string sql = "select QUOTATION.bacassu_ticket_seq.NEXTVAL from dual";
        int ticketId = Convert.ToInt32(orcle_trans.GetString(sql));
        //hfTicketId.Value = Session["bank_name_code"].ToString()+ticketId.ToString();
        //hfTicketId.Value = "T" + ticketId.ToString();
        txtRefNo.Text = "T" + ticketId.ToString();
    }

    protected void btViewClick(object sender, EventArgs e)
    {
        divNew.Visible = false;
        divView.Visible = true;
        idNew.Visible = false;
        idView.Visible = true;
        Div2.Visible = true;
        moreInfo.Visible = false;

        this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(),
                     ddlTicketStatus.SelectedValue.ToString().ToUpper().Trim(), ddl_bank.SelectedValue.ToString(), ddl_branch.SelectedValue.ToString(), null);

    }

    //view section

    protected void GetDetails(string start_date, string end_date, string ticket_id, string reqType, string status, string bank, string branch, string user)
    {
        DataTable details = new DataTable();
        try
        {

            details = orcle_trans.GetRows(this._sql.GetBankTickets(start_date, end_date, ticket_id, reqType, status, bank, branch, user), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    Grid_Details.DataSource = details;
                    Grid_Details.DataBind();
                }
                else
                {
                    Grid_Details.DataSource = null;
                    Grid_Details.DataBind();

                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }
    }


    protected void IniGridview_View()
    {
        Grid_Details.DataSource = null;
        Grid_Details.DataBind();
    }

    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Grid_Details.PageIndex = e.NewPageIndex;
        //bindgridview will get the data source and bind it again
        this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(),
                     ddlTicketStatus.SelectedValue.ToString().ToUpper().Trim(), ddl_bank.SelectedValue.ToString(), ddl_branch.SelectedValue.ToString(), null);

    }

    protected void ClearText()
    {
        this.IniGridview_View();

        //this.InitializedListBox(ddl_branch, "BRNAM", "BRCOD", this._sql.GetBranch(Int32.Parse(brCode.Value)), "'Branch'");
        //ddl_branch.SelectedIndex = 0;
        //ddl_epf.SelectedIndex = 0;

        ddl_status.SelectedIndex = 0;
        ddlTicketStatus.SelectedIndex = 0;
        txt_end_date.Text = string.Empty;
        txt_req_id.Text = string.Empty;
        txt_start_date.Text = string.Empty;
        //txt_ref_no.Focus();
        this.CleareDropDownList(ddl_bank);
        this.CleareDropDownList(ddl_branch);
        this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");

    }


    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }
    protected void btn_find_Click1(object sender, EventArgs e)
    {
        this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(),
             ddlTicketStatus.SelectedValue.ToString().ToUpper().Trim(), ddl_bank.SelectedValue.ToString(), ddl_branch.SelectedValue.ToString(), null);

    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        for (int i = Grid_Details.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = Grid_Details.Rows[i];
            GridViewRow previousRow = Grid_Details.Rows[i - 1];

            if (row.Cells[2].Text == previousRow.Cells[2].Text)
            {
                if (previousRow.Cells[2].RowSpan == 0)
                {
                    if (row.Cells[2].RowSpan == 0)
                    {
                        previousRow.Cells[2].RowSpan += 2;
                    }
                    else
                    {
                        previousRow.Cells[2].RowSpan = row.Cells[2].RowSpan + 1;
                    }
                    row.Cells[2].Visible = false;
                }
            }
        }
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string op_ststus = e.Row.Cells[8].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (op_ststus.Equals("Pending"))
                {
                    //cell.BackColor = Color.FromName("#F4F996"); //
                    e.Row.Cells[8].BackColor = Color.FromName("#F4F996");
                }

                else if (op_ststus.Equals("Complete"))
                {
                    //cell.BackColor = Color.FromName("#cffae2"); //#cffae2
                    e.Row.Cells[8].BackColor = Color.FromName("#cffae2");
                }

                else if (op_ststus.Equals("Rejected"))
                {
                    //cell.BackColor = Color.FromName("#ffcccc"); //#cffae2
                    e.Row.Cells[8].BackColor = Color.FromName("#ffcccc");

                }
            }

        }


    }

    protected void SelectRecord_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btndetails = sender as ImageButton;

            string[] arg = new string[4];
            arg = btndetails.CommandArgument.ToString().Split(';');
            string Ref_no = arg[0].ToString().Trim();
            string status = arg[1].ToString().Trim();
            hfTicketId.Value = Ref_no;
            bankEnmailId.Value = arg[2].ToString().Trim();
            bank_code.Value = arg[3].ToString().Trim();


            moreInfo.Visible = true;
            Div2.Visible = false;
            txtSelectTicket.Text = Ref_no;
            tempRefNo.Value = txtSelectTicket.Text.ToString();

            GetDetailsOneRecodesOfTicket(null, null, Ref_no, "", "", "", "", "");
            if (status == "Pending")
            {
                GetBankUpDocs(Ref_no);
                WbankLbl.Visible = true;
                slicLbl.Visible = false;
                divNew.Visible = true;
            }

            else
            {
                GetSLICUpDocs(Ref_no);
                GetBankUpDocs(Ref_no);
                WbankLbl.Visible = true;
                slicLbl.Visible = true;
                divNew.Visible = false;
            }
            
            

        }

        catch (Exception ex)
        {

            string msg = "Message : File not found.!";
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()));


        }
    }

    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        mainAlert.Visible = false;
        GridView2.PageIndex = e.NewPageIndex;
        GetBankUpDocs(txtSelectTicket.Text.ToString().Trim()); //bindgridview will get the data source and bind it again
    }

    protected void GridView3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        mainAlert.Visible = false;
        GridView3.PageIndex = e.NewPageIndex;
        GetSLICUpDocs(txtSelectTicket.Text.ToString().Trim()); //bindgridview will get the data source and bind it again
    }

    protected void GetBankUpDocs(string Ref_No)
    {
        DataTable details = new DataTable();
        try
        {
           
            details = orcle_trans.GetRows(this._sql.GetUploadBankTickets(Ref_No.ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    GridView2.DataSource = details;
                    GridView2.DataBind();

                    mainAlert.Visible = true;
                    sucsessAlert.Visible = false;
                    errorAlert.Visible = false;
                    infoAlert.Visible = false;
                    warningAlert.Visible = false;
                    txtBankLbl.InnerHtml = "Bank Documents";
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();


                    mainAlert.Visible = true;
                    sucsessAlert.Visible = false;
                    errorAlert.Visible = false;
                    infoAlert.Visible = false;
                    warningAlert.Visible = false;
                    warningContent.InnerText = "No Records to display!.";

                    txtBankLbl.InnerHtml = "";

                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = orcle_trans.Error_Message.ToString();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()), false);

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()), false);

        }
    }
    protected void GetSLICUpDocs(string Ref_No)
    {
        DataTable details = new DataTable();
        try
        {

            details = orcle_trans.GetRows(this._sql.GetUploadSLICTickets(Ref_No.ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    GridView3.DataSource = details;
                    GridView3.DataBind();

                    mainAlert.Visible = true;
                    sucsessAlert.Visible = false;
                    errorAlert.Visible = false;
                    infoAlert.Visible = false;
                    warningAlert.Visible = false;
                    txtSlicLbl.InnerHtml = "SLIC documents";
                }
                else
                {
                    GridView3.DataSource = null;
                    GridView3.DataBind();


                    mainAlert.Visible = true;
                    sucsessAlert.Visible = false;
                    errorAlert.Visible = false;
                    infoAlert.Visible = false;
                    warningAlert.Visible = false;
                    warningContent.InnerText = "No files from SLIC.";
                    txtSlicLbl.InnerHtml = "";

                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = orcle_trans.Error_Message.ToString();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()), false);

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()), false);

        }
    }

    protected void GetDetailsOneRecodesOfTicket(string start_date, string end_date, string ticket_id, string reqType, string status, string bank, string branch, string user)
    {
        DataTable details = new DataTable();
        try
        {

            details = orcle_trans.GetRows(this._sql.GetBankTickets(start_date, end_date, ticket_id, reqType, status, bank, branch, user), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {

                    lblTicket.InnerHtml = details.Rows[0]["T_REF"].ToString();
                    lblBankRemaks.InnerHtml = details.Rows[0]["T_BANK_REMARK"].ToString();
                    //details.Rows[0]["T_FLAG"].ToString();
                    lblEnteredDate.InnerHtml = details.Rows[0]["T_CREATED_ON"].ToString();
                    lblSLICRemaks.InnerHtml = details.Rows[0]["T_SLIC_REMARK"].ToString();
                    lblReplyDate.InnerHtml = details.Rows[0]["T_UP_ON_SLIC"].ToString();
                    lblBankName.InnerHtml = details.Rows[0]["T_BANK_NAME"].ToString();
                    lblBankBr.InnerHtml = details.Rows[0]["T_BRANCH_NAME"].ToString();


                    //ddlStatus.Attributes.Add("onclick", "return false;");
                    //ddlStatus.Attributes.Add("readonly", "readonly");

                    ddlStatus.Attributes.Add("disabled", "disabled");

                    refNo.Attributes.Add("onclick", "return false;");
                    refNo.Attributes.Add("readonly", "readonly");

                    refNo.Value = details.Rows[0]["T_REF"].ToString();
                    ddlStatus.SelectedValue = details.Rows[0]["valTlag"].ToString();
                    //details.Rows[0]["T_STATUS"].ToString();
                }
                else
                {


                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }
    }

    protected void InitializedListBank(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {
        DataTable getrecord = new DataTable();
        try
        {

            getrecord = orcle_trans.GetRows(executor, getrecord);

            if (orcle_trans.Trans_Sucess_State == true)
            {
                if (getrecord.Rows.Count > 1)
                {
                    target_list.DataSource = getrecord;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                    // ddl_make.Items.Insert(0, new ListItem("--All--", "0"));
                }

                else if (getrecord.Rows.Count == 1)
                {

                    target_list.DataSource = getrecord;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                }

                else
                {
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt("Error : Sorry " + target_desc + "can't initialized. Contact system administrator. Dated On :" + System.DateTime.Now.ToString()));

                }



            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));
            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }


    protected void InitializedListBranch(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {
        DataTable getrecord = new DataTable();
        try
        {

            getrecord = orcle_trans.GetRows(executor, getrecord);

            if (orcle_trans.Trans_Sucess_State == true)
            {
                if (getrecord.Rows.Count > 1)
                {
                    target_list.DataSource = getrecord;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                    //ddl_model.Items.Insert(0, new ListItem("--All--", "0"));
                }

                else if (getrecord.Rows.Count == 1)
                {

                    target_list.DataSource = getrecord;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                }

                else
                {
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt("Error : Sorry " + target_desc + "can't initialized. Contact system administrator. Dated On :" + System.DateTime.Now.ToString()));

                }



            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));
            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }

    protected void CleareDropDownList(DropDownList ddl)
    {
        var firstitem_1 = ddl.Items[0];
        ddl.Items.Clear();
        ddl.Items.Add(firstitem_1);
    }

    protected void ddl_bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CleareDropDownList(ddl_branch);
        // ddl_model.SelectedIndex = 0;
        //this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), "");
        this.InitializedListBranch(ddl_branch, "bbrnch", "bbcode", this._sql.GetBranch(ddl_bank.SelectedValue.ToString()), "'bbrnch'");
    }


    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        if (Grid_Details.Rows.Count > 0)
        {
            ExportGridToExcel();
            string msg = "Successfully Created.!";
            var endc = new EncryptDecrypt();
        }
        else
        {
            string msg = "Message : Cannot be downloaded because no record found.";
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()));

        }
    }

    private void ExportGridToExcel()
    {
        //try
        //{
        DataTable dt = new DataTable();

        string bank_val, branch_val = string.Empty;

        bank_val = ddl_bank.SelectedValue.ToString().Trim();
        branch_val = ddl_branch.SelectedValue.ToString().Trim();

        dt = orcle_trans.GetRows(this._sql.GetBankTickets(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(),
             ddlTicketStatus.SelectedValue.ToString().ToUpper().Trim(), ddl_bank.SelectedValue.ToString(), ddl_branch.SelectedValue.ToString(), null), dt);



        //// Gridview Records to Datatable
        //// 01. Grid Column to Table
        //for (int i = 1; i < Grid_Details.Columns.Count; i++)
        //{
        //    dt.Columns.Add(Grid_Details.Columns[i].ToString());
        //}

        //// 02. Grid Rows to Table
        //foreach (GridViewRow row in Grid_Details.Rows)
        //{
        //    DataRow dr = dt.NewRow();
        //    for (int j = 1; j < Grid_Details.Columns.Count; j++)
        //    {
        //        dr[Grid_Details.Columns[j].ToString()] = row.Cells[j].Text;
        //    }

        //    dt.Rows.Add(dr);
        //}

        //-------------------------------------------------------------

        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add("Bank Tickets -" + DateTime.Now.ToString("yyyy-MM"));

            ws.Column(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column(5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column(7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Column(8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            ws.Range("B2:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Range("B2:F6").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#6790BA"));
            ws.Range("B3:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Range("B4:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Range("B5:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Range("B6:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;


            //ws.Cell(2, 2).Value = ddlTicketStatus.SelectedValue.ToString().Trim();
            //ws.Cell(2, 2).Style.Font.Bold = true;

            ws.Cell(3, 2).Value = "Report Description : Bank Tickets " + ddlTicketStatus.SelectedItem.ToString().Trim() + ".";
            ws.Cell(4, 2).Value = "Date Of Genarate : " + System.DateTime.Now;
            ws.Cell(5, 2).Value = "Genarate By  : " + Session["UsrName"].ToString().Trim() + " - " + Session["EPFNum"].ToString();
            string fromdate, toDate = string.Empty;

            if (txt_start_date.Text.ToString().Trim() == null) { fromdate = ""; }
            else { fromdate = txt_start_date.Text.ToString().Trim(); }

            if (txt_end_date.Text.ToString().Trim() == "") { toDate = ""; }
            else { toDate = txt_end_date.Text.ToString().Trim(); }

            ws.Cell(6, 2).Value = "Period : From: " + fromdate + " - To: " + toDate;

            int RowCount = dt.Rows.Count;
            int ColumnCount = dt.Columns.Count;

            string[] ColumnHead = { "NO", "Ref. No", "Bank", "Branch", "Req. Type", "Bank Remarks", "Created By", "Created Date", "SLIC Remarks", "Responded Date", "Responded By" };

            int[] ColumnSize = { 5, 25, 25, 25, 25, 25, 25, 25, 25, 25, 25 };

            for (int head = 0; head < ColumnHead.Length; head++)
            {
                ws.Cell(8, head + 2).Value = ColumnHead[head];
                ws.Cell(8, head + 2).WorksheetColumn().Width = ColumnSize[head];
                ws.Cell(8, head + 2).WorksheetRow().Height = 18;
                ws.Cell(8, head + 2).WorksheetRow().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                ws.Cell(8, head + 2).WorksheetRow().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Cell(8, head + 2).Style.Fill.BackgroundColor = XLColor.Gray;
                ws.Cell(8, head + 2).Style.Font.Bold = true;
                ws.Cell(8, head + 2).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#719AC4")); // use some unique color
                ws.Cell(8, head + 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }


            for (int rows = 0; rows < RowCount; rows++)
            {
                ws.Cell(9 + rows, 2).Value = (rows + 1).ToString();
                ws.Cell(9 + rows, 2).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 3).Value = dt.Rows[rows]["T_REF"];
                ws.Cell(9 + rows, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(9 + rows, 4).Value = dt.Rows[rows]["T_BANK_NAME"];
                ws.Cell(9 + rows, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(9 + rows, 5).Value = dt.Rows[rows]["T_BRANCH_NAME"];
                ws.Cell(9 + rows, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //ws.Cell(9 + rows, 6).Style.DateFormat.Format = "dd/MM/yyyy";
                //ws.Cell(9 + rows, 6).Value = DateTime.ParseExact(dt.Rows[rows]["Entered Date"].ToString(), "dd/MM/yyyy", null);
                //ws.Cell(9 + rows, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //ws.Cell(9 + rows, 6).Value = dt.Rows[rows]["Entered Date"];


                ws.Cell(9 + rows, 6).Value = dt.Rows[rows]["T_FLAG"];
                ws.Cell(9 + rows, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 7).Value = dt.Rows[rows]["T_BANK_REMARK"];
                ws.Cell(9 + rows, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 8).Value = dt.Rows[rows]["T_CREATED_BY"];
                ws.Cell(9 + rows, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;




                ws.Cell(9 + rows, 9).Style.DateFormat.Format = "dd/MM/yyyy";
                ws.Cell(9 + rows, 9).Value = DateTime.ParseExact(dt.Rows[rows]["T_CREATED_ON"].ToString(), "dd/MM/yyyy", null);
                ws.Cell(9 + rows, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;



                ws.Cell(9 + rows, 10).Value = dt.Rows[rows]["T_SLIC_REMARK"];
                ws.Cell(9 + rows, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 11).Value = dt.Rows[rows]["T_UP_ON_SLIC"];
                ws.Cell(9 + rows, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //ws.Cell(9 + rows, 11).Style.DateFormat.Format = "dd/MM/yyyy";
                //ws.Cell(9 + rows, 11).Value = DateTime.ParseExact(dt.Rows[rows]["T_UP_ON_SLIC"].ToString(), "dd/MM/yyyy", null);
                //ws.Cell(9 + rows, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 12).Value = dt.Rows[rows]["T_UP_BY_SLIC"];
                ws.Cell(9 + rows, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


            }


            ws.Column(2).Width = 20;
            for (int x = 3; x <= 36; x++)
            {
                ws.Column(x).AdjustToContents();

                if (x == 3 || x == 5 || x == 7 || x == 8 || x == 11 || x == 13)
                {
                    ws.Column(x).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                }

                else
                {
                    ws.Column(x).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                }
            }


            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=BankTickets.xlsx");

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);

            }


            Response.Flush();
            Response.End();
        }
        //}

        //catch (Exception ex)
        //{
        //    var endc = new EncryptDecrypt();
        //    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()),false);
        //}
    }

    protected void btexcel_Click(object sender, EventArgs e)
    {

        if (Grid_Details.Rows.Count > 0)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            //Response.AddHeader("content-disposition", "attachment;filename=FirePolicyCompleted Date From  : " + txt_start_date.Text + "  To : " + txt_to_date.Text.Trim() + ".xls");
            Response.AddHeader("content-disposition", "attachment;filename=BankTickets.xls");

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                Grid_Details.AllowPaging = false;
                this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(),
            ddlTicketStatus.SelectedValue.ToString().ToUpper().Trim(), ddl_bank.SelectedValue.ToString(), ddl_branch.SelectedValue.ToString(), null);

                //--end-->


                string wz = 
                "Report Description : Bank Tickets " + ddlTicketStatus.SelectedItem.ToString().Trim() + ".<br/>" +
                "Date From: " + txt_start_date.Text.ToString().ToUpper() + " To: " + txt_end_date.Text.ToString().ToUpper() + "<br/>" +
                "Genarate By  : " + Session["UsrName"].ToString().Trim() + " - " + Session["EPFNum"].ToString() + "<br/>" +
                "Date of Genarate : " + System.DateTime.Now;

                hw.Write("<div> <h3 align=leftss><span style=");
                hw.Write(HtmlTextWriter.DoubleQuoteChar);
                hw.Write("font-weight:bold; font-family:'Segoe UI'; color: #81040a;");
                hw.Write(HtmlTextWriter.DoubleQuoteChar);
                hw.Write(">" + wz + "</span> </h3></div>");
                hw.WriteLine();

                Grid_Details.HeaderRow.BackColor = Color.White;
                Grid_Details.Columns[14].Visible = false;
                Grid_Details.Columns[8].Visible = false;

                foreach (TableCell cell in Grid_Details.HeaderRow.Cells)
                {
                    if (cell.Text.Equals("View"))
                    {
                        cell.BackColor = Grid_Details.HeaderStyle.BackColor = Color.AliceBlue;
                        cell.CssClass = "hideColumn";
                        cell.Visible = false;

                    }
                    else
                    {
                        cell.BackColor = Grid_Details.HeaderStyle.BackColor;
                        cell.CssClass = "bhead";
                    }

                }
                foreach (GridViewRow row in Grid_Details.Rows)
                {
                    row.BackColor = Color.Beige;
                    foreach (TableCell cell in row.Cells)
                    {


                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = Grid_Details.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = Grid_Details.RowStyle.BackColor;
                        }
                        cell.CssClass = "bbody";



                    }
                }

                Grid_Details.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .bhead{padding:5px;
                 border:1px solid #fff;
                 background:#003d79;
                    color:white;
                    text-align:center; } .bbody{color:Black;
                    text-align:center;
                    background:#d7ffee;
                    padding:5px;
                    width :260px;
                    border:1px solid #fff;}
                    .hideColumn{background:#5afc03;visbility: hidden; display: none; width :0px;}
                    </style>";

                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }


        }
        else
        {
            string msg = "Message : Cannot be downloaded because no record found.";
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("15".ToString()));

        }


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
}