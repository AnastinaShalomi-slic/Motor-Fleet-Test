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

public partial class BankResponseTeam_BankTicketView : System.Web.UI.Page
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
                if (Session["bank_code"].ToString() != "")
                {


                    UserId.Value = Session["bank_code"].ToString();
                    brCode.Value = Session["branch_code"].ToString();
                    user_epf.Value = Session["userName_code"].ToString();

                    mainAlert.Visible = false;
                    sucsessAlert.Visible = false;
                    errorAlert.Visible = false;
                    warningAlert.Visible = false;
                    infoAlert.Visible = false;

                    divNew.Visible = false;
                    divView.Visible = false;
                    hfTicketId.Value = "";


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

            Session["bank_code"] = UserId.Value;
            Session["branch_code"] = brCode.Value;
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
        //if (tempStatus.Value == "Completed")
        //{

        //    refNo.Disabled = true;
        //    Button1.Enabled = false;
        //    FileUpload1.Enabled = false;
        //    btnUpload.Enabled = false;

        //    ddlStatus.Items.FindByValue("D").Attributes.Add("Disabled", "Disabled");
        //    ddlStatus.Items.FindByValue("M").Attributes.Add("Disabled", "Disabled");
        //    //ddlStatus.Items.FindByValue("RI").Attributes.Add("Disabled", "Disabled");
        //    //ddlStatus.Items.FindByValue("RM").Attributes.Add("Disabled", "Disabled");


        //}

        //else if (tempStatus.Value == "Pending")
        //{

        //}

        //else if (tempStatus.Value == "Cancel")
        //{


        //    ddlStatus.Items.FindByValue("C").Attributes.Add("Disabled", "Disabled");

        //}
        //else if (tempStatus.Value == "Need more Info.")
        //{


        //}

        //else
        //{
        //    refNo.Disabled = true;
        //    Button1.Enabled = false;
        //    FileUpload1.Enabled = false;
        //    btnUpload.Enabled = false;
        //    ddlStatus.Enabled = false;
        //    txtremark.Disabled = true;
        //}
        ////disable fields according to selection---------------------> 
        //if (ddlStatus.SelectedValue == "D")
        //{
        //    refNo.Disabled = true;
        //    Button1.Enabled = true;
        //    FileUpload1.Enabled = true;
        //    btnUpload.Enabled = true;
        //    //ddlStatus.Enabled = false;
        //    txtremark.Disabled = false;



        //    mainAlert.Visible = false;
        //    sucsessAlert.Visible = false;
        //    errorAlert.Visible = false;
        //    warningAlert.Visible = false;
        //    infoAlert.Visible = false;

        //}


        //else if (ddlStatus.SelectedValue == "M")
        //{
        //    refNo.Disabled = true;
        //    Button1.Enabled = false;
        //    FileUpload1.Enabled = false;
        //    btnUpload.Enabled = false;
        //    //ddlStatus.Enabled = false;
        //    txtremark.Disabled = false;

        //    mainAlert.Visible = false;
        //}
        //else if (ddlStatus.SelectedValue == "C")
        //{
        //    refNo.Disabled = true;
        //    Button1.Enabled = false;
        //    FileUpload1.Enabled = false;
        //    btnUpload.Enabled = false;
        //    //ddlStatus.Enabled = false;
        //    txtremark.Disabled = false;
        //    mainAlert.Visible = false;

        //}
        //else if (ddlStatus.SelectedValue == "R")
        //{
        //    refNo.Disabled = true;
        //    Button1.Enabled = false;
        //    FileUpload1.Enabled = false;
        //    btnUpload.Enabled = false;
        //    //ddlStatus.Enabled = false;
        //    txtremark.Disabled = false;
        //    mainAlert.Visible = false;
        //}


        ///
        //else
        //{
        //    refNo.Disabled = true;
        //    Button1.Enabled = false;
        //    FileUpload1.Enabled = false;
        //    btnUpload.Enabled = false;
        //    //ddlStatus.Enabled = false;
        //    txtremark.Disabled = false;
        //    mainAlert.Visible = false;
        //}

    }

    private void BindGrid()
    {
        GetQuoDetails(txtRefNo.Text.ToString());

    }


    protected void GetQuoDetails(string Ref_No)
    {
        DataTable details = new DataTable();
        try
        {

            details = orcle_trans.GetRows(this._sql.GetUploadBankTickets(Ref_No.ToString()), details);

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
                                                string req_val = "select QUOTATION.bacassu_bank_ticket_seq.NEXTVAL from dual";
                                                int req_id = Convert.ToInt32(orcle_trans.GetString(req_val));

                                                string query = "insert into QUOTATION.BANCASSU_BANK_ITUPDOC (T_NO,T_REF,T_NAME,T_CONTENT,T_DATA,CREATED_BY,CREATED_ON,T_FLAG,T_ACTIVE) values (:id,:T_ref,:Name, :ContentType, :Data, :create_by, :create_on, :active, :T_ACTIVE)";
                                                using (OracleCommand cmd = new OracleCommand(query))
                                                {
                                                    cmd.Connection = connection;
                                                    cmd.Parameters.AddWithValue(":id", req_id);
                                                    cmd.Parameters.AddWithValue(":T_ref", txtRefNo.Text.ToString());
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

    protected void RemoveFile(object sender, EventArgs e)
    {
        bool rtn = false;
        ImageButton lnkRemove = sender as ImageButton;

        string[] arg = new string[4];
        arg = lnkRemove.CommandArgument.ToString().Split(';');
        string REQ_ID = arg[0].ToString().Trim();
        rtn = exe_up.update_Bankdoc_table_ticket(REQ_ID, Session["userName_code"].ToString().Trim());
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

        string txt_body = "Quick Response Team. Bank Ref. No - " + refNo + "<br/>Please check the system and attend the ticket.";

        string ccEmails = string.Empty;
        string toEmails = string.Empty;


        ccEmails = String.Join(", ", emailIDForCC);
        //toEmails = String.Join(", ", emailID);
        toEmails = bankEnmailId.Value.Trim().ToLower();

        emailSend.fireRequestDetails("SLIMO", "", toEmails, ccEmails, refNo.ToString().Trim().ToString(), txt_body, "", FromEmailid);

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

            detailsforCC = orcle_trans.GetRows(this._sql.GetOfficerEmailAll(bank_code.ToString().Trim()), detailsforCC);

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

                        string txt_body = "Quick Response Team. Bank Ref. No - " + ref_no + ". Plz check the system.";

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
            //--comment in 26042023
            //result1 = exe_in.insert_ticket_details(txtRefNo.Text.ToString(), txtremark.Value, Session["bank_code"].ToString(), Session["branch_code"].ToString(), Session["userName_code"].ToString(), ddlStatus.SelectedValue.ToString().Trim(), "P", out Gen_t_id);

            //this.sendSMStoAgent("D", Session["bank_code"].ToString(), txtRefNo.Text.ToString().Trim(), "");
            //this.sendEmailToSLICOfficer(txtRefNo.Text.ToString().Trim(), Session["bank_code"].ToString(), "D");



            /*if (result1)*/
            if (result1)
            {
                System.Threading.Thread.Sleep(3000);

                var endc = new EncryptDecrypt();
                string msg = "Successfully saved and email sent to bank.";
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("14".ToString()), false);

            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = "Syetem Error".ToString();
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

        this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(),
            ddlTicketStatus.SelectedValue.ToString().ToUpper().Trim(), Session["bank_code"].ToString().Trim(), Session["branch_code"].ToString().Trim(), Session["userName_code"].ToString());

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
             ddlTicketStatus.SelectedValue.ToString().ToUpper().Trim(), Session["bank_code"].ToString().Trim(), Session["branch_code"].ToString().Trim(), Session["userName_code"].ToString());
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

    }


    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }
    protected void btn_find_Click1(object sender, EventArgs e)
    {
        this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(),
             ddlTicketStatus.SelectedValue.ToString().ToUpper().Trim(), Session["bank_code"].ToString().Trim(), Session["branch_code"].ToString().Trim(), Session["userName_code"].ToString());

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
            string op_ststus = e.Row.Cells[5].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (op_ststus.Equals("Pending"))
                {
                    //cell.BackColor = Color.FromName("#F4F996"); //
                    e.Row.Cells[5].BackColor = Color.FromName("#F4F996");
                }

                else if (op_ststus.Equals("Complete"))
                {
                    //cell.BackColor = Color.FromName("#cffae2"); //#cffae2
                    e.Row.Cells[5].BackColor = Color.FromName("#cffae2");
                }

                else if (op_ststus.Equals("Rejected"))
                {
                    //cell.BackColor = Color.FromName("#ffcccc"); //#cffae2
                    e.Row.Cells[5].BackColor = Color.FromName("#ffcccc");

                }
            }

        }


    }

    protected void SelectRecord_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btndetails = sender as ImageButton;

            string[] arg = new string[3];
            arg = btndetails.CommandArgument.ToString().Split(';');
            string Ref_no = arg[0].ToString().Trim();
            string status = arg[1].ToString().Trim();

            var en = new EncryptDecrypt(); //imgbtn_select          

            //if (string.IsNullOrEmpty(pol_no))
            //{
            //    string fd_ref = "pending";
            //    //string fd_flag = "Y";
            //    string fd_flag = Final_flag;
            //    string fd_sum = sumInsu;

            //    Response.Redirect("~/Bank_Fire/PrintSchedule.aspx?Ref_no=" + en.Encrypt(fd_ref) + "&Flag=" + en.Encrypt(fd_flag) + "&sumInsu=" + en.Encrypt(fd_sum.ToString()), false);

            //}
            //else
            //{

            //    string fd_ref = Ref_no;
            //    string fd_flag = "Y";
            //    string fd_sum = sumInsu;

            //    Response.Redirect("~/Bank_Fire/PrintSchedule.aspx?Ref_no=" + en.Encrypt(fd_ref) + "&Flag=" + en.Encrypt(fd_flag) + "&sumInsu=" + en.Encrypt(fd_sum.ToString()), false);
            //}

        }

        catch (Exception ex)
        {

            string msg = "Message : File not found.!";
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("14".ToString()));


        }
    }

}