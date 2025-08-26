using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Web.Services;
using System.Collections.Generic;
using System.Data.OracleClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

public partial class SLIC_Side_ViewPageSecond : System.Web.UI.Page
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
                if (Session["UserId"] != null)
                {

                    UserId.Value = Session["UserId"].ToString();
                    brCode.Value = Session["brcode"].ToString();
                    user_epf.Value = Session["EPFNum"].ToString();

                    Req_id.Focus();
                    var en = new EncryptDecrypt();
                  

                    if (Request.QueryString["REQ_ID"] != "")
                    {
                         x = en.Decrypt(Request.QueryString["REQ_ID"]);
                        
                        mainAlert.Visible = false;
                        sucsessAlert.Visible = false;
                        errorAlert.Visible = false;
                        warningAlert.Visible = false;
                        infoAlert.Visible = false;

                        if (string.IsNullOrEmpty(x) || x == "#" ) //|| string.IsNullOrEmpty(app) || app == "#" || string.IsNullOrEmpty(requ) || requ == "#"
                        {

                            // ShowPopupMessage("***************",PopupMessageType.Message);
                            var endc = new EncryptDecrypt();
                            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("URL".ToString()));
                        }
                        else
                        {
                            var endc = new EncryptDecrypt();
                            Req_id.Text = en.Decrypt(Request.QueryString["REQ_ID"]);
                            flag = en.Decrypt(Request.QueryString["V_FLAG"]);
                            quo_no_temp= en.Decrypt(Request.QueryString["V_QUO"]);
                            tempStatus.Value = flag;
                            //ddlRecepientEPF.Text = Session["bancass_email"].ToString().Trim();
                            refNo.Value = Req_id.Text.ToString();
                            BindGrid();
                            if (flag == "Completed")
                            {

                                refNo.Disabled = true;
                                Button1.Enabled = false;
                                FileUpload1.Enabled = false;
                                btnUpload.Enabled = false;

                                ddlStatus.Items.FindByValue("D").Attributes.Add("Disabled", "Disabled");
                                ddlStatus.Items.FindByValue("M").Attributes.Add("Disabled", "Disabled");
                                //ddlStatus.Items.FindByValue("RI").Attributes.Add("Disabled", "Disabled");
                                //ddlStatus.Items.FindByValue("RM").Attributes.Add("Disabled", "Disabled");
                                txtStatOne.InnerHtml = "Completed";
                                txtStatTwo.Visible = false;
                                txtStatThree.Visible = false;
                                txtStatFour.Visible = false;

                            }

                            else if (flag == "Pending")
                            {
                                //statusView1.Visible = false;
                                //statusView3.Visible = false;
                                //statusView2.InnerHtml = "Quotation is in pending status.";
                                //BindGrid();
                                FileUpload1.Enabled = true;

                                //     var items1 = new List<ListItem>()
                                //     {
                                //         new ListItem("Select Option"),
                                //         new ListItem("Test 1"),
                                //         new ListItem("Test 2"),
                                //         new ListItem("Test 3")
                                //      };

                                //ddlStatus.DataSource = items1;
                                //ddlStatus.DataBind();

                                txtStatOne.Visible =false;
                                txtStatTwo.InnerHtml = "Pending";
                                txtStatThree.Visible = false;
                                txtStatFour.Visible = false;

                            }

                            else if (flag == "Cancel")
                            {
                             

                                ddlStatus.Items.FindByValue("C").Attributes.Add("Disabled", "Disabled");
                                txtStatOne.Visible = false;
                                txtStatTwo.Visible = false;
                                txtStatThree.InnerHtml = "Cancelled";
                                txtStatFour.Visible = false;

                            }
                            else if (flag == "Need more Info.")
                            {
                                txtStatOne.Visible = false;
                                txtStatTwo.Visible = false;
                                txtStatThree.InnerHtml = "Need more Info.";
                                txtStatFour.Visible = false;

                            }

                            else if (flag == "Forward to Risk Managment")
                            {
                                //ddlStatus.Items.FindByValue("RM").Attributes.Add("Disabled", "Disabled");

                            }
                            else if (flag == "Forward to Reinsurance")
                            {

                                //ddlStatus.Items.FindByValue("RI").Attributes.Add("Disabled", "Disabled");
                            }
                            else
                            {
                                refNo.Disabled = true;
                                Button1.Enabled = false;
                                FileUpload1.Enabled = false;
                                btnUpload.Enabled = false;
                                ddlStatus.Enabled = false;
                                txtremark.Disabled = true;

                                txtStatOne.Visible = false;
                                txtStatTwo.Visible = false;
                                txtStatThree.Visible = false;
                                txtStatFour.InnerHtml="Rejected";
                            }

                            this.GetDetails(x);
                            // this.attachment();
                            this.GetDetailOfficers();

                        }


                    }

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
        }


    }


    [WebMethod]
    public static string[] Get_Ref_No(string prefix)
    {
        List<string> ref_no = new List<string>();
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["CONN_STRING_ORCL"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = "select qref_no from QUOTATION.issued_quotations where qref_no like '%" + prefix.ToUpper() + "%' AND ROWNUM <= 10 order by QENT_DATE desc";
                cmd.Connection = conn;
                conn.Open();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        ref_no.Add(string.Format("{0}", sdr["qref_no"]));
                    }
                }
                conn.Close();
            }
            return ref_no.ToArray();
        }
    }


   
    protected void GetDetails(string req_id)
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetReqDetails(null,null, req_id, null,null,null,null), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {


                    v_type.Text = details.Rows[0]["V_TYPE"].ToString();
                    yom.Text = details.Rows[0]["YOM"].ToString();

                    int number = Convert.ToInt32( details.Rows[0]["SUM_INSU"].ToString());
                    txtsumInsu.Text = Convert.ToDecimal(number).ToString("#,##0.00");

                    //txtsumInsu.Text = details.Rows[0]["SUM_INSU"].ToString();

                    txtMake.Text = details.Rows[0]["vh_make"].ToString();
                    txtmodel.Text = details.Rows[0]["model_name"].ToString();
                    txtpurpose.Text = details.Rows[0]["PURPOSE"].ToString();

                    txtV_reg.Text = details.Rows[0]["V_REG_NO"].ToString();
                    txtcusname.Text = details.Rows[0]["CUS_NAME"].ToString();
                    txtCusCon.Text = details.Rows[0]["CUS_PHONE"].ToString();

                    txtfuel.Text = details.Rows[0]["V_FUEL"].ToString();
                    txtcusEmail.Text = details.Rows[0]["email"].ToString();

                    statusHidden.Value = details.Rows[0]["FLAG"].ToString();
                    reqDate.Value = details.Rows[0]["ENTERED_ON"].ToString();
                    bankEnmailId.Value = details.Rows[0]["banc_email"].ToString();
                    bank_code.Value = details.Rows[0]["BANK_CODE"].ToString();
                    statusId.InnerHtml = details.Rows[0]["SLIC_REMARK"].ToString();
                    //Grid_Details.DataBind();
                    

                }
                else
                {

                    string msg = "Message : No Records found. Dated on " + System.DateTime.Now.ToString();
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()));
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
                    for (int i=0; i < details.Rows.Count; i++)
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
        if (tempStatus.Value == "Completed")
        {

            refNo.Disabled = true;
            Button1.Enabled = false;
            FileUpload1.Enabled = false;
            btnUpload.Enabled = false;

            ddlStatus.Items.FindByValue("D").Attributes.Add("Disabled", "Disabled");
            ddlStatus.Items.FindByValue("M").Attributes.Add("Disabled", "Disabled");
            //ddlStatus.Items.FindByValue("RI").Attributes.Add("Disabled", "Disabled");
            //ddlStatus.Items.FindByValue("RM").Attributes.Add("Disabled", "Disabled");
          

        }

        else if (tempStatus.Value == "Pending")
        {

        }

        else if (tempStatus.Value == "Cancel")
        {


            ddlStatus.Items.FindByValue("C").Attributes.Add("Disabled", "Disabled");

        }
        else if (tempStatus.Value == "Need more Info.")
        {


        }

        //else if (tempStatus.Value == "Forward to Risk Managment")
        //{
        //    ddlStatus.Items.FindByValue("RM").Attributes.Add("Disabled", "Disabled");

        //}
        //else if (tempStatus.Value == "Forward to Reinsurance")
        //{

        //    ddlStatus.Items.FindByValue("RI").Attributes.Add("Disabled", "Disabled");
        //}
        else
        {
            refNo.Disabled = true;
            Button1.Enabled = false;
            FileUpload1.Enabled = false;
            btnUpload.Enabled = false;
            ddlStatus.Enabled = false;
            txtremark.Disabled = true;
        }
        //disable fields according to selection---------------------> 
        if (ddlStatus.SelectedValue == "D")
        {
            refNo.Disabled = true;
            Button1.Enabled = true;
            FileUpload1.Enabled = true;
            btnUpload.Enabled = true;
            //ddlStatus.Enabled = false;
            txtremark.Disabled = false;



            mainAlert.Visible = false;
            sucsessAlert.Visible = false;
            errorAlert.Visible = false;
            warningAlert.Visible = false;
            infoAlert.Visible = false;

        }


        else if (ddlStatus.SelectedValue == "M")
        {
            refNo.Disabled = true;
            Button1.Enabled = false;
            FileUpload1.Enabled = false;
            btnUpload.Enabled = false;
            //ddlStatus.Enabled = false;
            txtremark.Disabled = false;

            mainAlert.Visible = false;
        }
        else if (ddlStatus.SelectedValue == "C")
        {
            refNo.Disabled = true;
            Button1.Enabled = false;
            FileUpload1.Enabled = false;
            btnUpload.Enabled = false;
            //ddlStatus.Enabled = false;
            txtremark.Disabled = false;
            mainAlert.Visible = false;

        }
        else if (ddlStatus.SelectedValue == "R")
        {
            refNo.Disabled = true;
            Button1.Enabled = false;
            FileUpload1.Enabled = false;
            btnUpload.Enabled = false;
            //ddlStatus.Enabled = false;
            txtremark.Disabled = false;
            mainAlert.Visible = false;
        }


        //else if (ddlStatus.SelectedValue == "RM")
        //{
        //    refNo.Disabled = true;
        //    Button1.Enabled = false;
        //    FileUpload1.Enabled = false;
        //    btnUpload.Enabled = false;
        //    //ddlStatus.Enabled = false;
        //    txtremark.Disabled = false;

        //    mainAlert.Visible = false;
        //}
        //else if (ddlStatus.SelectedValue == "RI")
        //{
        //    refNo.Disabled = true;
        //    Button1.Enabled = false;
        //    FileUpload1.Enabled = false;
        //    btnUpload.Enabled = false;
        //    //ddlStatus.Enabled = false;
        //    txtremark.Disabled = false;

        //    mainAlert.Visible = false;
        //}
        else
        {
            refNo.Disabled = true;
            Button1.Enabled = false;
            FileUpload1.Enabled = false;
            btnUpload.Enabled = false;
            //ddlStatus.Enabled = false;
            txtremark.Disabled = false;
            mainAlert.Visible = false;
        }

    }

    private void BindGrid()
    {
        GetQuoDetails(refNo.Value.ToString());
        GetRemarksfromAll(refNo.Value.ToString());
        
    }


    protected void GetQuoDetails(string Ref_No)
    {
        DataTable details = new DataTable();
        try
        {

            details = orcle_trans.GetRows(this._sql.GetUploadQuotaions(Ref_No.ToString()), details);

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
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()),false);

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()),false);

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
            if (ddlStatus.SelectedValue == "D")
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
                                                string req_val = "select QUOTATION.bacassu_motoquo_seq.NEXTVAL from dual";
                                                int req_id = Convert.ToInt32(orcle_trans.GetString(req_val));

                                                string query = "insert into QUOTATION.BANCASSU_UPMOTO_QUOTATIONS(Q_NO,Q_REF,Q_NAME,Q_CONTENT,Q_DATA,CREATED_BY,CREATED_ON,Q_FLAG,P_ACTIVE) values (:id,:F_ref,:Name, :ContentType, :Data, :create_by, :create_on, :active, :P_ACTIVE)";
                                                using (OracleCommand cmd = new OracleCommand(query))
                                                {
                                                    cmd.Connection = connection;
                                                    cmd.Parameters.AddWithValue(":id", req_id);
                                                    cmd.Parameters.AddWithValue(":F_ref", refNo.Value.ToString());
                                                    cmd.Parameters.AddWithValue(":Name", filename);
                                                    cmd.Parameters.AddWithValue(":ContentType", contentType);
                                                    cmd.Parameters.AddWithValue(":Data", bytes);
                                                    cmd.Parameters.AddWithValue(":create_by", UserId.Value.ToString());
                                                    cmd.Parameters.AddWithValue(":create_on", System.DateTime.Now);
                                                    cmd.Parameters.AddWithValue(":active", "D");
                                                    cmd.Parameters.AddWithValue(":P_ACTIVE", "Y");

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
                cmd.CommandText = "select Q_NAME, Q_DATA, Q_CONTENT from QUOTATION.BANCASSU_UPMOTO_QUOTATIONS where Q_NO=:Id";
                cmd.Parameters.AddWithValue(":Id", id);
                cmd.Connection = connection;
                connection.Open();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    sdr.Read();

                    bytes = (byte[])sdr["Q_DATA"];
                    contentType = sdr["Q_CONTENT"].ToString();
                    fileName = sdr["Q_NAME"].ToString();
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
        rtn = exe_up.update_doc_table(REQ_ID, UserId.Value.Trim());
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


    protected void GetRemarksfromAll(string refNo)
    {
       
        //int rtnCount = 0;
        DataTable Remarksdetails = new DataTable();
       
        try
        {
            /// second  table for CC user emails

            Remarksdetails = orcle_trans.GetRows(this._sql.GetMotorRemarks(refNo.ToString().Trim()), Remarksdetails);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (Remarksdetails.Rows.Count > 0)
                {
                    for (int i = 0; i < Remarksdetails.Rows.Count; i++)
                    {
                        slicRemarks.Add((i+1) +". "+Remarksdetails.Rows[i]["r_slic"].ToString());
                        bankRemarks.Add((i+1) +". " + Remarksdetails.Rows[i]["r_bank"].ToString());
                    }

                }
                else
                {
                    

                }



                string outputSlic = string.Join("<br/>", slicRemarks.ToArray());
                string outputBank = string.Join("<br/>", bankRemarks.ToArray());
                txtSlicRemarks.InnerHtml = outputSlic;
                txtBankRemarks.InnerHtml = outputBank;


            }

            else
            {
                var endc = new EncryptDecrypt();
                string msg = orcle_trans.Error_Message.ToString();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()), false);

            }

        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = orcle_trans.Error_Message.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()), false);

        }
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
       
        string txt_body = "Motor quotation requested process. Ref. ID: " + refNo + "<br/>Status: " + status+ "<br/>Please check the system to get quotations.";

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
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()), false);

            }

        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = orcle_trans.Error_Message.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()), false);

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

                        string txt_body = "Motor quotation requested process " + sataus + " Ref. No - " + ref_no + ". Plz check the system.";

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
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()), false);

            }



        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()), false);

        }

    }

    protected void btfinished_Click(object sender, EventArgs e)
    {
        try
        {
            bool result1 = false;
          
            if (ddlStatus.SelectedValue == "D")
            {
               
                result1 = exe_up.update_motorQuotationTable(refNo.Value.ToString().Trim(), "D", "", UserId.Value.ToString(), txtremark.Value);
                this.sendSMStoAgent("D", bank_code.Value.Trim(), refNo.Value.ToString().Trim(), "");
                this.sendEmailToSLICOfficer(refNo.Value.ToString().Trim(), bank_code.Value.Trim(), "D");
            }

            else if (ddlStatus.SelectedValue == "M")
            {

                result1 = exe_up.update_motorQuotationTable(refNo.Value.ToString().Trim(), "M", "", UserId.Value.ToString(), txtremark.Value);

                bool result2 = exe_in.insertMotorRemarksToWays(refNo.Value.ToString().Trim(), "A", UserId.Value.ToString(), txtremark.Value);
                this.sendSMStoAgent("M", bank_code.Value.Trim(), refNo.Value.ToString().Trim(), "");
                this.sendEmailToSLICOfficer(refNo.Value.ToString().Trim(), bank_code.Value.Trim(), "M");

            }

            else if (ddlStatus.SelectedValue == "C")
            {

                result1 = exe_up.update_motorQuotationTable(refNo.Value.ToString().Trim(), "C", "", UserId.Value.ToString(), txtremark.Value);
                this.sendSMStoAgent("C", bank_code.Value.Trim(), refNo.Value.ToString().Trim(), "");
                this.sendEmailToSLICOfficer(refNo.Value.ToString().Trim(), bank_code.Value.Trim(), "C");

            }

            else if (ddlStatus.SelectedValue == "R")
            {

                result1 = exe_up.update_motorQuotationTable(refNo.Value.ToString().Trim(), "R", "", UserId.Value.ToString(), txtremark.Value);
                this.sendSMStoAgent("R", bank_code.Value.Trim(), refNo.Value.ToString().Trim(), "");
                this.sendEmailToSLICOfficer(refNo.Value.ToString().Trim(), bank_code.Value.Trim(), "R");
            }

            /*if (result1)*/
            if (result1)
            {
                System.Threading.Thread.Sleep(3000);
            
                var endc = new EncryptDecrypt();
                string msg = "Successfully saved and email sent to bank.";
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()), false);
               
            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = "Syetem Error".ToString();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()), false);

            }
        }

        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()), false);
        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(1000);
        IniGridview();
        BindGrid();
    }


}