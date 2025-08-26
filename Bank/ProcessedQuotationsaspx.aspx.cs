using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Bank_ProcessedQuotationsaspx : System.Web.UI.Page
{
    public string x = string.Empty;
    public string flag = string.Empty;
    public string ref_ID = string.Empty;
    public string bank_code = string.Empty;
    public string pro_id = string.Empty;
    public static string customerRef = string.Empty;
    public string responce_date, creadted_date = string.Empty;

    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Update_class exe_up = new Update_class();
    Ip_Class get_ip = new Ip_Class();
    EncryptDecrypt dc = new EncryptDecrypt();
    DeviceFinder findDev = new DeviceFinder();
    DeviceFinder df = new DeviceFinder();
    ORCL_Connection orcl_con = new ORCL_Connection();
    ExcuteOraSide ExSql = new ExcuteOraSide();
    List<String> bankRemarks = new List<String>();
    List<String> slicRemarks = new List<String>();
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        string Auth = string.Empty;


        if (!Page.IsPostBack)
        {
            try
            {
                Auth = "admin";//Session["Auth_Code"].ToString();

                if (Auth == "admin")
                {

                    if (Session["bank_code"].ToString() != "")
                    {
                        var en = new EncryptDecrypt();


                        if (Request.QueryString["REQ_ID"] != null)
                        {
                            x = en.Decrypt(Request.QueryString["REQ_ID"]);


                            if (string.IsNullOrEmpty(x) || x == "#")
                            {

                                var endc = new EncryptDecrypt();
                                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("URL".ToString()), false);
                            }
                            else
                            {
                                var endc = new EncryptDecrypt();
                                ref_ID = en.Decrypt(Request.QueryString["REQ_ID"]).ToString();
                                Req_id.Text = ref_ID;
                                flag = en.Decrypt(Request.QueryString["V_FLAG"]);

                                bank_code = en.Decrypt(Request.QueryString["Bank_code"]);

                                GetDetails(ref_ID);


                                if (flag == "Completed")
                                {

                                    bankResp.Visible = false;
                                    statusView2.Visible = false;
                                    statusView3.Visible = false;
                                    statusView1.InnerHtml = "Quotation is completed and attached below.";
                                    BindGrid();

                                }

                                else if (flag == "Pending")
                                {
                                    statusView1.Visible = false;
                                    statusView3.Visible = false;
                                    statusView2.InnerHtml = "Quotation is in pending status.";
                                    BindGrid();
                                    bankResp.Visible = false;

                                }

                                else if (flag == "Cancel")
                                {
                                    statusView1.Visible = false;
                                    statusView3.Visible = false;
                                    statusView2.InnerHtml = "Quotation canceled.";
                                    BindGrid();
                                    bankResp.Visible = false;

                                }
                                else if (flag == "Need more Info.")
                                {
                                    statusView1.Visible = false;
                                    statusView3.Visible = false;
                                    statusView2.InnerHtml = "Need more information.";
                                    BindGrid();
                                    bankResp.Visible = true;

                                }


                                else if (flag == "Forward to risk managment")
                                {
                                    statusView1.Visible = false;
                                    statusView3.Visible = false;
                                    statusView2.InnerHtml = "Forward to Risk Managment.";
                                    BindGrid();
                                    bankResp.Visible = false;
                                }

                                else if (flag == "Forward to Reinsurance")
                                {
                                    statusView1.Visible = false;
                                    statusView3.Visible = false;
                                    statusView2.InnerHtml = "Forward to Reinsurance.";
                                    BindGrid();
                                    bankResp.Visible = false;
                                }

                                else
                                {
                                    statusView2.Visible = false;
                                    statusView1.Visible = false;
                                    statusView3.InnerHtml = "Quotation rejected.";
                                    BindGrid();
                                    bankResp.Visible = false;
                                    rejectDiv.Visible = true;

                                }


                            }


                        }






                    }

                    else
                    {
                        var endc = new EncryptDecrypt();
                        string msg = "Session expired. Please login again.";
                        Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("SE".ToString()), false);

                    }

                }
                else
                {

                    string msg = "You are not authorized to access this system.";
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("1".ToString()), false);


                }

            }
            catch (Exception ex)
            {
                var endc = new EncryptDecrypt();
                string msg = ex.ToString();
                Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("1".ToString()), false);

            }


        }
        else
        {

            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
        }


    }


    protected void GetTimeDurations(string temp_Time)
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetTimeDuration(temp_Time), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    string day, hour, minute, second = string.Empty;

                    day = details.Rows[0]["days"].ToString();
                    hour = details.Rows[0]["hours"].ToString();
                    minute = details.Rows[0]["minutes"].ToString();
                    second = details.Rows[0]["seconds"].ToString();

                    string tempDay, tempHour, tempminute = string.Empty;

                    if (!string.IsNullOrEmpty(day) && day == "1") { tempDay = day + " Day"; }
                    else if (!string.IsNullOrEmpty(day) && day != "1") { tempDay = day + " Days"; }
                    else { tempDay = ""; }

                    if (!string.IsNullOrEmpty(hour) && hour == "1") { tempHour = hour + " Hour"; }
                    else if (!string.IsNullOrEmpty(hour) && hour != "1") { tempHour = hour + " Hours"; }
                    else { tempHour = ""; }

                    if (flag == "Pending")
                    {

                    }
                    else
                    {

                    }

                }
                else
                {
                    var endc = new EncryptDecrypt();
                    string msg = "Error : Sorry can't initialized. Contact system administrator. Dated On :" + System.DateTime.Now.ToString();
                    Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = orcle_trans.Error_Message.ToString();
                Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

        }
    }


    protected void GetDetails(string req_id)
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetReqDetails(null, null, req_id, null, null, null, null), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {


                    v_type.Text = details.Rows[0]["V_TYPE"].ToString();
                    yom.Text = details.Rows[0]["YOM"].ToString();

                    int number = Convert.ToInt32(details.Rows[0]["SUM_INSU"].ToString());
                    txtsumInsu.Text = Convert.ToDecimal(number).ToString("#,##0.00");


                    txtMake.Text = details.Rows[0]["vh_make"].ToString();
                    txtmodel.Text = details.Rows[0]["model_name"].ToString();
                    txtpurpose.Text = details.Rows[0]["PURPOSE"].ToString();

                    txtV_reg.Text = details.Rows[0]["V_REG_NO"].ToString();
                    txtcusname.Text = details.Rows[0]["CUS_NAME"].ToString();
                    txtCusCon.Text = details.Rows[0]["CUS_PHONE"].ToString();

                    txtfuel.Text = details.Rows[0]["V_FUEL"].ToString();
                    txtcusEmail.Text = details.Rows[0]["email"].ToString();
                    statusId.InnerHtml = details.Rows[0]["SLIC_REMARK"].ToString();


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

    private void BindGrid()
    {
        attachedFiles(ref_ID.ToString());
        GetRemarksfromAll(ref_ID.ToString());
    }


    protected void attachedFiles(string Ref_No)
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

                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();

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


    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
        BindGrid(); //bindgridview will get the data source and bind it again
    }

    protected void IniGridview()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();

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


    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        var endc = new EncryptDecrypt();
        string refID = customerRef;
        string x = tempDID.Value;
        string y = tempPID.Value;

        Response.Redirect("~/ProposalForms/GenAccProposalEdit/EditGenAccNeonSign.aspx?Ref_no=" + endc.Encrypt(refID) + "&DID=" + endc.Encrypt(tempDID.Value)
                  + "&PID=" + endc.Encrypt(tempPID.Value), false);

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
                        slicRemarks.Add((i + 1) + ". " + Remarksdetails.Rows[i]["r_slic"].ToString());
                        bankRemarks.Add((i + 1) + ". " + Remarksdetails.Rows[i]["r_bank"].ToString());
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
    protected void btEdit_Click(object sender, EventArgs e)
    {
        try
        {
            bool result1, result2 = false;


            result1 = exe_up.update_motorQuotationBankRemarks(Req_id.Text.Trim(), Session["userName_code"].ToString(), txtBankRe.Value);
            result2 = exe_up.update_motorQuotationNeedMoreBank(Req_id.Text.Trim(), "P");

            /*if (result1)*/
            if (result1 && result2)
            {
                System.Threading.Thread.Sleep(3000);

                var endc = new EncryptDecrypt();
                string msg = "Response sent. SLIC will attend.";
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("13".ToString()), false);

            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = "Syetem Error".ToString();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("13".ToString()), false);

            }
        }

        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()), false);
        }
    }
}