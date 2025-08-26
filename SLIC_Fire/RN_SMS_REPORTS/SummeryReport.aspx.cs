using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SLIC_Fire_RN_SMS_REPORTS_SummeryReport : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    ORCL_Connection orcl_con = new ORCL_Connection();
    ExcuteOraSide ora_side = new ExcuteOraSide();
    Execute_sql _sql = new Execute_sql();
    string Auth = string.Empty;
    public double Cal_NBTTemp, Cal_VATTemp, Cal_TotalTemp = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        lblAlertMessage.Text = "";
        lblAlertMessage.Visible = false;
        //btn_download.Visible = false;

        if (!Page.IsPostBack)
        {
            try
            {
                //Auth = Session["AccessAdmin"].ToString();
                Auth = "admin";

                if (Auth == "admin")
                {
                    reportCat.Visible = true;
                    reportType.Visible = true;
                    reportType.Visible = true;

                    if (Session["UserId"].ToString() != "")
                    {

                        UserId.Value = Session["UserId"].ToString();
                        brCode.Value = Session["brcode"].ToString();

                        //this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
                        //this.InitializedListBranch(ddl_branch, "BRNAM", "BRCOD", this._sql.GetBranch(Convert.ToInt32(brCode.Value)), "'BRCOD'");

                    }

                    else
                    {
                        var endc = new EncryptDecrypt();
                        Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("SE".ToString()));
                    }

                }
                else
                {
                    string msg = "You are not authorized to access this system.";


                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()));

                }


            }
            catch (Exception ex)
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()));

            }

        }
        else
        {

            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
        }
    }

    protected void InitializedListBranch(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {

        DataTable getrecordBranch = new DataTable();
        try
        {

            getrecordBranch = orcle_trans.GetRows(executor, getrecordBranch);

            if (orcle_trans.Trans_Sucess_State == true)
            {
                if (getrecordBranch.Rows.Count > 1)
                {
                    target_list.DataSource = getrecordBranch;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                    // ddl_make.Items.Insert(0, new ListItem("--All--", "0"));
                }

                else if (getrecordBranch.Rows.Count == 1)
                {

                    target_list.DataSource = getrecordBranch;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                }

                else
                {

                    // CleareDropDownList(ddlBranch);

                    var endc = new EncryptDecrypt();
                    string msg = "Error : Sorry " + target_desc + "can't initialized. Contact system administrator. Dated On :" + System.DateTime.Now.ToString();
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
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.IniGridview();
        ddl_RepType.SelectedValue = "N";
        txt_to_date.Text = string.Empty;
        txt_start_date.Text = string.Empty;

    }

    protected void ddlCate_SelectedIndexChanged(object sender, EventArgs e)
    {
        //btn_download.Visible = false;
        Grid_Details.Visible = false;

        Grid_Details.DataSource = null;
        Grid_Details.DataBind();

        if (ddl_category.SelectedValue == "SUM_N_CHANGED")
        {
            // Find the "Intermediate Status List" item (Value = "I_Status") in ddl_RepType
            ListItem itemToRemove = ddl_RepType.Items.FindByValue("I_Status");

            if (itemToRemove != null)
            {
                ddl_RepType.Items.Remove(itemToRemove);
            }
        }
        else
        {
            // Reload the original list if needed (optional)

            if (ddl_RepType.Items.FindByValue("I_Status") == null)
            {
                ddl_RepType.Items.Add(new ListItem("Intermediate Status List", "I_Status"));
            }
        }
    }

    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        //btn_download.Visible = false;
        Grid_Details.Visible = false;

        Grid_Details.DataSource = null;
        Grid_Details.DataBind();

    }

    protected void IniGridview()
    {
        Grid_Details.DataSource = null;
        Grid_Details.DataBind();
    }
    protected void btn_find_Click1(object sender, EventArgs e)
    {
        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_RepType.SelectedValue.ToString().Trim(), ddl_category.SelectedValue.ToString().Trim(), "", txtPolNo.Text.ToString().Trim());

    }

    protected void GetDetails(string start_date, string end_date, string reportType, string category, string branch, string polNo)
    {

        DataTable sentSmsList = new DataTable();
        try
        {
            string bank_val, branch_val, propTerm, propType = string.Empty;

            sentSmsList = orcle_trans.GetRows(this._sql.GetFireRenewalSMSReports(start_date, end_date, reportType, category, branch, polNo), sentSmsList);


            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (sentSmsList.Rows.Count > 0)
                {
                    Grid_Details.Visible = true;

                    // Store the full data in Session
                    Session["FullData"] = sentSmsList;

                    Grid_Details.DataSource = sentSmsList;
                    Grid_Details.DataBind();
                    //btn_download.Visible = true;

                }
                else
                {

                    lblAlertMessage.Text = "Data not found.";
                    lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
                    lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
                    lblAlertMessage.Attributes.Add("data-alert-type", "Alert");
                    lblAlertMessage.Visible = true;

                    ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('" + lblAlertMessage.Text + "', 'Alert');", true);

                    // Clear alert message immediately after showing it
                    lblAlertMessage.Text = "";
                    lblAlertMessage.Visible = false;

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



    protected void clientFunctionValidation(object sender, EventArgs e)
    {

    }

    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Grid_Details.PageIndex = e.NewPageIndex;

        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_category.SelectedValue.ToString().ToUpper().Trim(), ddl_category.SelectedValue.ToString().Trim(),"", txtPolNo.Text.ToString().Trim());
        //btn_PremCal_Click1(null, null);
    }

    protected void Grid_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string smsStatusVal = "";
            // Assuming you have a column `is_locked` in the data source.
            string smsStatus = e.Row.Cells[12].Text.ToString();

            if (smsStatus == "N")
            {
                smsStatusVal = "Not Sent";
            }
            else if (smsStatus == "I")
            {
                smsStatusVal = "Intermediate Level";
            }
            else if (smsStatus == "A")
            {
                smsStatusVal = "SMS Sent";
            }
            else
            {
                smsStatusVal = "";
            }

            // Set the text in the Lock Status column
            e.Row.Cells[12].Text = smsStatusVal;


        }
    }

    protected void Grid_Details_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }



}