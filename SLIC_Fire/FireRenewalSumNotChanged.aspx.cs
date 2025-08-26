using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SLIC_Fire_FireRenewalSumNotChanged : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    ORCL_Connection orcl_con = new ORCL_Connection();
    ExcuteOraSide ora_side = new ExcuteOraSide();
    Execute_sql _sql = new Execute_sql();
    string Auth = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        Panel1.Visible = false;
        lblAlertMessage.Text = "";
        lblAlertMessage.Visible = false;

        if (!Page.IsPostBack)
        {
            Session["UpdatedPolicyFee"] = null;



            try
            {
                Auth = "admin";

                if (Auth == "admin")
                {

                    if (Session["UserId"].ToString() != "")
                    {

                        UserId.Value = Session["UserId"].ToString();
                        brCode.Value = Session["brcode"].ToString();

                        //this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
                        this.InitializedListBranch(ddl_branch, "BRNAM", "BRCOD", this._sql.GetBranch(Convert.ToInt32(brCode.Value)), "'BRCOD'");
                        if (brCode.Value == "10")
                        {
                            divBudiUnit.Visible = true;
                        }
                        else
                        {
                            divBudiUnit.Visible = false;
                            //ddlBusUnit.SelectedValue = "0";
                        }

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
            if (brCode.Value == "10")
            {
                divBudiUnit.Visible = true;
            }
            else
            {
                divBudiUnit.Visible = false;
                //ddlBusUnit.SelectedValue = "0";
            }
        }
    }

    protected void InitializedListBranch(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {
        string selectedBranchCode = Session["brcode"].ToString();

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

                    if (selectedBranchCode == "10")
                    {
                        if (!string.IsNullOrEmpty(selectedBranchCode) && target_list.Items.FindByValue(selectedBranchCode) != null)
                        {
                            target_list.SelectedValue = selectedBranchCode;
                        }
                    }

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

    //protected void InitializedListBranch(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    //{

    //    DataTable getrecordBranch = new DataTable();
    //    try
    //    {

    //        getrecordBranch = orcle_trans.GetRows(executor, getrecordBranch);

    //        if (orcle_trans.Trans_Sucess_State == true)
    //        {
    //            if (getrecordBranch.Rows.Count > 1)
    //            {
    //                target_list.DataSource = getrecordBranch;
    //                target_list.DataTextField = target_datafield;
    //                target_list.DataValueField = target_value;
    //                target_list.DataBind();
    //                // ddl_make.Items.Insert(0, new ListItem("--All--", "0"));
    //            }

    //            else if (getrecordBranch.Rows.Count == 1)
    //            {

    //                target_list.DataSource = getrecordBranch;
    //                target_list.DataTextField = target_datafield;
    //                target_list.DataValueField = target_value;
    //                target_list.DataBind();
    //            }

    //            else
    //            {

    //                // CleareDropDownList(ddlBranch);

    //                var endc = new EncryptDecrypt();
    //                string msg = "Error : Sorry " + target_desc + "can't initialized. Contact system administrator. Dated On :" + System.DateTime.Now.ToString();
    //                Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

    //            }



    //        }
    //        else
    //        {
    //            var endc = new EncryptDecrypt();
    //            string msg = orcle_trans.Error_Message.ToString();
    //            Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        var endc = new EncryptDecrypt();
    //        string msg = ex.ToString();
    //        Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

    //    }
    //}

    protected void ddlsubDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSubDept.SelectedValue == "EN")
        {
            divEngineering.Visible = true;
            divFire.Visible = false;
            ddlFirePro.SelectedValue = "N";
        }
        else if (ddlSubDept.SelectedValue == "FI")
        {
            divEngineering.Visible = false;
            divFire.Visible = true;
            ddlEngineering.SelectedValue = "N";
        }
        else
        {
            divEngineering.Visible = false;
            divFire.Visible = false;
        }


    }
    protected void GetDetails(string start_date, string end_date, string app_status, string policyNo, string branch, string subDpt, string engProd, string fireProd, string busiUnit)
    {

        DataTable RenwalSMSdetails = new DataTable();
        try
        {
            string bank_val, branch_val, propTerm, propType = string.Empty;

            string department = ddl_polType.SelectedValue.ToString();

            if(Session["AccessAdmin"].ToString() == "Y")
            {
                RenwalSMSdetails = orcle_trans.GetRows(this._sql.GetFireRenewalSMSDetails(start_date, end_date, app_status, department, policyNo, branch, subDpt, engProd, fireProd, busiUnit), RenwalSMSdetails);
            }
            else
            {
                RenwalSMSdetails = orcle_trans.GetRows(this._sql.GetFireRenewalSMSDetails(start_date, end_date, app_status, department, policyNo, Session["brcode"].ToString(), subDpt, engProd, fireProd, busiUnit), RenwalSMSdetails);
            }
            

            if (app_status == "SUM_N_CHANGED")
            {
                if (orcle_trans.Trans_Sucess_State == true)
                {

                    if (RenwalSMSdetails.Rows.Count > 0)
                    {

                        DivSumInsuNotCha.Visible = true;
                        // Store the full data in Session
                        Session["FullData"] = RenwalSMSdetails;

                        SunInsuNotChangedGrid.DataSource = RenwalSMSdetails;
                        SunInsuNotChangedGrid.DataBind();
                        btnSmsSent.Visible = true;
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

                        SunInsuNotChangedGrid.DataSource = null;
                        SunInsuNotChangedGrid.DataBind();


                    }
                }
                else
                {
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

                }
            }            
            

        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }
    }

    protected void txtCusPhone_TextChanged2(object sender, EventArgs e)
    {
        TextBox txtCusPhone = (TextBox)sender;
        GridViewRow row = (GridViewRow)txtCusPhone.NamingContainer;

        CustomValidator cvPhone = (CustomValidator)row.FindControl("cvPhone");

        string phoneNumber = txtCusPhone.Text.Trim();
        string policyNo = SunInsuNotChangedGrid.DataKeys[row.RowIndex].Value.ToString();

        // Example: Validate if it's a Sri Lankan number starting with 94 and 11 digits total
        if (!System.Text.RegularExpressions.Regex.IsMatch(phoneNumber, @"^94\d{9}$"))
        {
            cvPhone.IsValid = false; // This will show the error message

        }
        else
        {
            cvPhone.IsValid = true;
            phoneNumber = phoneNumber.Substring(2);
            int upResult = _sql.updateCusMobNo(phoneNumber, policyNo);
        }

    }

    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSmsSent.Visible = false;

    }

    protected void btn_find_Click1(object sender, EventArgs e)
    {
        string subDpt = ddlSubDept.SelectedValue;
        string engProd = ddlEngineering.SelectedValue;
        string fireProd = ddlFirePro.SelectedValue;
        string busiUnit = ddlBusUnit.SelectedValue;

        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.ToString().ToUpper().Trim(), txt_pol_no.Text.Trim(), ddl_branch.SelectedValue.ToString(), subDpt, engProd, fireProd, busiUnit);
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }

    protected void ClearText()
    {
        this.IniGridview();
        ddl_status.SelectedValue = "SUM_N_CHANGED";
        txt_to_date.Text = string.Empty;
        txt_start_date.Text = string.Empty;
        ddlTerm.SelectedIndex = 0;
        ddlPropType.SelectedIndex = 0;
    }

    protected void IniGridview()
    {
        SunInsuNotChangedGrid.DataSource = null;
        SunInsuNotChangedGrid.DataBind();
    }

    //functon for send temp table to sum not chnaged list of records
    protected void btnSentSMS_Click(object sender, EventArgs e)
    {
        List<FireRenewalMast.FireRenewalMastClass> details = new List<FireRenewalMast.FireRenewalMastClass>();

        foreach (GridViewRow row in SunInsuNotChangedGrid.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                string policyNo = SunInsuNotChangedGrid.DataKeys[row.RowIndex].Value.ToString();
                string rndept = "F";
                string rnType = "FD_RENEWAL";


                TextBox txtCusPhone = (TextBox)row.FindControl("txtCusPhone");
                string cusCoNo = txtCusPhone.Text;

                Label lblYear = (Label)row.FindControl("lblYear");
                int rnYear = Int32.Parse(lblYear.Text);

                Label lblRnMonth = (Label)row.FindControl("lblMonth");
                int rnMonth = Int32.Parse(lblRnMonth.Text);


                details.Add(new FireRenewalMast.FireRenewalMastClass
                {
                    RNDEPT = rndept,
                    RNPTP = rnType,
                    RNPOL = policyNo,
                    RNCNT = cusCoNo,
                    RNMNTH = rnMonth,
                    RNYR = rnYear

                });
            }
        }

        bool saversult = _sql.SendSMS_SumNotChangedList(details, Session["UserId"].ToString(), Session["branch_code"].ToString());

        if (saversult)
        {

            lblAlertMessage.Text = "SMS sent for all selected records.";
            lblAlertMessage.Attributes.Add("data-alert-title", "Success");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Visible = true;
            // Trigger the JavaScript function manually after setting the message
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data successfully sent for approval process.', 'Success');", true);

        }
        else
        {

            lblAlertMessage.Text = "SMS not sent. ";
            lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data not sent for approval process.', 'Success');", true);


        }
    }


}