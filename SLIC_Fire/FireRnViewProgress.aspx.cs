using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SLIC_Fire_FireRnViewProgress : System.Web.UI.Page
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

        Panel1.Visible = false;
        //btn_calPremium.Visible = true;

        lblAlertMessage.Text = "";
        lblAlertMessage.Visible = false;
        pnlPopup.Visible = false;
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

                        //this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.ToString().ToUpper().Trim(), txt_pol_no.Text.Trim(), ddl_branch.SelectedValue.ToString());

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

    protected void GetDetails(string start_date, string end_date, string app_status, string policyNo, string branch)
    {

        DataTable RenwalSMSdetails = new DataTable();
        try
        {
            string bank_val, branch_val, propTerm, propType = string.Empty;

            string department = ddl_polType.SelectedValue.ToString();

            if (Session["AccessAdmin"].ToString() == "Y")
            {
                RenwalSMSdetails = orcle_trans.GetRows(this._sql.GetFireRenewalRejectedList(start_date, end_date, app_status, department, policyNo, branch), RenwalSMSdetails);
            }
            else
            {
                RenwalSMSdetails = orcle_trans.GetRows(this._sql.GetFireRenewalRejectedList(start_date, end_date, app_status, department, policyNo, Session["brcode"].ToString()), RenwalSMSdetails);
            }
               

            if (app_status == "SUM_N_CHANGED")
            {
                if (orcle_trans.Trans_Sucess_State == true)
                {

                    if (RenwalSMSdetails.Rows.Count > 0)
                    {
                        Grid_Details.Visible = false;
                        DivSumInsuNotCha.Visible = true;

                        // Store the full data in Session
                        Session["FullData"] = RenwalSMSdetails;

                        SunInsuNotChangedGrid.DataSource = RenwalSMSdetails;
                        SunInsuNotChangedGrid.DataBind();
                        //btn_calPremium.Visible = true;
                        btnSentAppro.Visible = false;
                        //btnSmsSent.Visible = true;
                    }
                    else
                    {

                        lblAlertMessage.Text = "Data not found.";
                        lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
                        lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
                        lblAlertMessage.Attributes.Add("data-alert-type", "Alert");
                        lblAlertMessage.Attributes["data-alert-method"] = "1";
                        lblAlertMessage.Visible = true;

                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('" + lblAlertMessage.Text + "', 'Alert');", true);

                        // Clear alert message immediately after showing it
                        lblAlertMessage.Text = "";
                        lblAlertMessage.Visible = false;

                        SunInsuNotChangedGrid.DataSource = null;
                        SunInsuNotChangedGrid.DataBind();

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
            else if (app_status == "WC")
            {
                if (orcle_trans.Trans_Sucess_State == true)
                {

                    if (RenwalSMSdetails.Rows.Count > 0)
                    {
                        Grid_Details.Visible = false;
                        DivSumInsuNotCha.Visible = false;
                        WithClaimGrid.Visible = true;

                        // Store the full data in Session
                        Session["FullData"] = RenwalSMSdetails;

                        WithClaimGrid.DataSource = RenwalSMSdetails;
                        WithClaimGrid.DataBind();
                        //btn_calPremium.Visible = false;
                        btnSentAppro.Visible = false;
                        //btnSmsSent.Visible = false;
                    }
                    else
                    {

                        lblAlertMessage.Text = "Data not found.";
                        lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
                        lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
                        lblAlertMessage.Attributes.Add("data-alert-type", "Alert");
                        lblAlertMessage.Attributes["data-alert-method"] = "1";
                        lblAlertMessage.Visible = true;

                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('" + lblAlertMessage.Text + "', 'Alert');", true);

                        // Clear alert message immediately after showing it
                        lblAlertMessage.Text = "";
                        lblAlertMessage.Visible = false;

                        SunInsuNotChangedGrid.DataSource = null;
                        SunInsuNotChangedGrid.DataBind();

                        Grid_Details.DataSource = null;
                        Grid_Details.DataBind();

                        WithClaimGrid.DataSource = null;
                        WithClaimGrid.DataBind();

                    }
                }
                else
                {
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

                }
            }
            else
            {
                if (orcle_trans.Trans_Sucess_State == true)
                {

                    if (RenwalSMSdetails.Rows.Count > 0)
                    {
                        Grid_Details.Visible = true;
                        // Store the full data in Session
                        Session["FullData"] = RenwalSMSdetails;

                        Grid_Details.DataSource = RenwalSMSdetails;
                        Grid_Details.DataBind();
                        //btn_calPremium.Visible = true;
                        btnSentAppro.Visible = true;
                        //btnSmsSent.Visible = false;
                        btnSentAppro.Enabled = false;
                        DivSumInsuNotCha.Visible = false;
                    }
                    else
                    {

                        lblAlertMessage.Text = "Data not found.";
                        lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
                        lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
                        lblAlertMessage.Attributes.Add("data-alert-type", "Alert");
                        lblAlertMessage.Attributes["data-alert-method"] = "1";
                        lblAlertMessage.Visible = true;

                        ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('" + lblAlertMessage.Text + "', 'Alert');", true);

                        // Clear alert message immediately after showing it
                        lblAlertMessage.Text = "";
                        lblAlertMessage.Visible = false;
                        WithClaimGrid.Visible = false;
                        Grid_Details.DataSource = null;
                        Grid_Details.DataBind();

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

    protected void txtPolicyFee_TextChanged(object sender, EventArgs e)
    {
        // Get the new value from the TextBox that was changed
        TextBox txtChangedPolicyFee = (TextBox)sender;
        string newPolicyFeeValue = txtChangedPolicyFee.Text;

        // Store the new value in a session
        Session["UpdatedPolicyFee"] = newPolicyFeeValue;

        // Loop through all rows in the GridView and update the "Policy Fee" column
        foreach (GridViewRow row in Grid_Details.Rows)
        {
            Label txtPolFee = row.FindControl("lblPolicyFee") as Label;

            if (txtPolFee != null)
            {
                // Set the value of the "Policy Fee" TextBox to the new value
                txtPolFee.Text = string.IsNullOrEmpty(txtPolFee.Text) ? "0" : (txtPolFee.Text);
            }
        }

       // btn_calPremium.Visible = true;

    }


    protected void Grid_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // Find the TextBox for Policy Fee
            Label txtPolicyFee = (Label)e.Row.FindControl("lblPolicyFee");

            // Check if the session has a new policy fee value
            if (Session["UpdatedPolicyFee"] != null)
            {
                // Set the TextBox value to the updated fee from session
                txtPolicyFee.Text = Session["UpdatedPolicyFee"].ToString();
            }
        }
    }



    protected void IniGridview()
    {
        Grid_Details.DataSource = null;
        Grid_Details.DataBind();
    }

    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Grid_Details.PageIndex = e.NewPageIndex;

        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.ToString().ToUpper().Trim(), txt_pol_no.Text.Trim(), ddl_branch.SelectedValue.ToString());
        //btn_PremCal_Click1(null, null);
    }

    protected void btn_PremCal_Click1(object sender, EventArgs e)
    {
        //double VATPercent = _sql.Get_VAT_Precentage();
        foreach (GridViewRow row in Grid_Details.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {

                double netPremium = Convert.ToDouble(row.Cells[7].Text);  // BASIC_PREMIUM
                double srcc = Convert.ToDouble(row.Cells[8].Text);        // RCC
                double tc = Convert.ToDouble(row.Cells[9].Text);          // TC
                double adminFeeprecentage = Convert.ToDouble(row.Cells[10].Text); // adminfee

                Label txtPolFee = row.FindControl("lblPolicyFee") as Label;
                double policyFee = string.IsNullOrEmpty(txtPolFee.Text) ? 0 : Convert.ToDouble(txtPolFee.Text);

                //TextBox txtPolFee = row.FindControl("txtPolicyFee") as TextBox;
                //double policyFee = string.IsNullOrEmpty(txtPolFee.Text) ? 0 : Convert.ToDouble(txtPolFee.Text);

                double Cal_ADMIN_FEETemp = ((netPremium + srcc + tc) * adminFeeprecentage) / 100;

                string todayStr = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime date = DateTime.ParseExact(todayStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                int date2 = Convert.ToInt32(date.ToString("yyyyMMdd"));
                double sumForTaxVat = 0;
                sumForTaxVat = netPremium + srcc + tc + policyFee + Cal_ADMIN_FEETemp;

                using (OracleConnection conn = orcl_con.GetConnection())
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT_DATE", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("taxLiableAmount", sumForTaxVat);
                        cmd.Parameters.AddWithValue("requestDate", date2);
                        cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                        cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                        OracleDataReader dr = cmd.ExecuteReader();

                        Cal_NBTTemp = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                        Cal_VATTemp = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                        dr.Close();

                    }
                    conn.Close();
                }

                Cal_TotalTemp = sumForTaxVat + Cal_NBTTemp + Cal_VATTemp;

                // Set the value to the label
                Label lblTotalPremium = row.FindControl("lblTotalPremium") as Label;
                if (lblTotalPremium != null)
                {
                    lblTotalPremium.Text = Cal_TotalTemp.ToString("N2");
                }

                Label lblNbt = row.FindControl("lblNbt") as Label;
                lblNbt.Text = Cal_NBTTemp.ToString("N2");

                Label lblVat = row.FindControl("lblVat") as Label;
                lblVat.Text = Cal_VATTemp.ToString("N2");

                Label AdminFeeVal = row.FindControl("lblAdminFeeVal") as Label;
                AdminFeeVal.Text = Cal_ADMIN_FEETemp.ToString("N2");

            }
        }

        btnSentAppro.Visible = true;
    }

    //end of calculation

    protected void btn_PremCal_Click2(object sender, EventArgs e)
    {
        //double VATPercent = _sql.Get_VAT_Precentage();

        double netPremium = Convert.ToDouble(txtNetPremium.Text);  // BASIC_PREMIUM
        double srcc = string.IsNullOrEmpty(txtRcc.Text) ? 0 : Convert.ToDouble(txtRcc.Text);        // RCC
        double tc = string.IsNullOrEmpty(txtTr.Text) ? 0 : Convert.ToDouble(txtTr.Text);         // TC
        double adminFeeprecentage = Convert.ToDouble(txtAdminFee.Text); // adminfee

        double policyFee = string.IsNullOrEmpty(txtPolicyFee1.Text) ? 0 : Convert.ToDouble(txtPolicyFee1.Text);

        double Cal_ADMIN_FEETemp = ((netPremium + srcc + tc) * adminFeeprecentage) / 100;

        string todayStr = DateTime.Now.ToString("dd/MM/yyyy");
        DateTime date = DateTime.ParseExact(todayStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        int date2 = Convert.ToInt32(date.ToString("yyyyMMdd"));
        double sumForTaxVat = 0;
        sumForTaxVat = netPremium + srcc + tc + policyFee + Cal_ADMIN_FEETemp;

        using (OracleConnection conn = orcl_con.GetConnection())
        {
            conn.Open();
            using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT_DATE", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("taxLiableAmount", sumForTaxVat);
                cmd.Parameters.AddWithValue("requestDate", date2);
                cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                OracleDataReader dr = cmd.ExecuteReader();

                Cal_NBTTemp = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                Cal_VATTemp = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                dr.Close();

            }
            conn.Close();
        }

        Cal_TotalTemp = sumForTaxVat + Cal_NBTTemp + Cal_VATTemp;

        // Set the value to the label
        //Label lblTotalPremium = row.FindControl("lblTotalPremium") as Label;
        //if (lblTotalPremium != null)
        //{
        //    lblTotalPremium.Text = Cal_TotalTemp.ToString("N2");
        //}

        txtTotalPremium.Text = Cal_TotalTemp.ToString("N2");

        //Label lblNbt = row.FindControl("lblNbt") as Label;
        //lblNbt.Text = Cal_NBTTemp.ToString("N2");
        txtNbt.Text = Cal_NBTTemp.ToString("N2"); ;

        //Label lblVat = row.FindControl("lblVat") as Label;
        //lblVat.Text = Cal_VATTemp.ToString("N2");
        txtVat.Text = Cal_VATTemp.ToString("N2"); ;

        //Label AdminFeeVal = row.FindControl("lblAdminFeeVal") as Label;
        txtAdminFee2.Text = Cal_ADMIN_FEETemp.ToString("N2");


        Panel1.Visible = true;
        btnSendToApp.Visible = true;
    }

    // Helper to get double values from BoundFields
    private double GetDoubleValue(GridViewRow row, string dataFieldName)
    {
        object dataItem = DataBinder.GetDataItem(row);
        if (dataItem != null)
        {
            object value = DataBinder.Eval(dataItem, dataFieldName);
            return value != null ? Convert.ToDouble(value) : 0;
        }
        return 0;
    }

    protected void txtCusPhone_TextChanged(object sender, EventArgs e)
    {
        TextBox txtCusPhone = (TextBox)sender;
        GridViewRow row = (GridViewRow)txtCusPhone.NamingContainer;

        CustomValidator cvPhone = (CustomValidator)row.FindControl("cvPhone");

        string phoneNumber = txtCusPhone.Text.Trim();
        string policyNo = Grid_Details.DataKeys[row.RowIndex].Value.ToString();

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


    protected void ClearText()
    {
        this.IniGridview();
        ddl_status.SelectedValue = "N";
        txt_to_date.Text = string.Empty;
        txt_start_date.Text = string.Empty;

    }


    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }
    protected void btn_find_Click1(object sender, EventArgs e)
    {

        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.ToString().ToUpper().Trim(), txt_pol_no.Text.Trim(), ddl_branch.SelectedValue.ToString());
    }

    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        //btn_calPremium.Visible = false;
        btnSentAppro.Visible = false;
        //btnSmsSent.Visible = false;

    }




    protected void btnSentAprr_Click(object sender, EventArgs e)
    {

        List<FireRenewalMast.FireRenewalMastClass> details = new List<FireRenewalMast.FireRenewalMastClass>();

        foreach (GridViewRow row in Grid_Details.Rows)
        {
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            if (chkSelect != null && chkSelect.Checked)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string policyNo = Grid_Details.DataKeys[row.RowIndex].Value.ToString();
                    string rndept = "F";
                    string rnType = "FD_RENEWAL";
                    string eDate = row.Cells[2].Text;

                    //string eDateString = row.Cells[2].Text;

                    // Try parsing the string into a DateTime
                    DateTime endDate;
                    string oneYearBackString = string.Empty;


                    if (DateTime.TryParseExact(eDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                    {
                        DateTime oneYearBack = endDate.AddYears(-1);
                        oneYearBackString = oneYearBack.ToString("dd/MM/yyyy"); // Output will be correct: 04/07/2024
                    }
                    else
                    {
                        // Log or show alert
                        Console.WriteLine("Invalid date format: " + eDate);
                    }

                    //if (DateTime.TryParse(eDate, out endDate))
                    //{
                    //    DateTime oneYearBack = endDate.AddYears(-1);
                    //    oneYearBackString = oneYearBack.ToString("dd/MM/yyyy"); // Or any format you need

                    //}
                    //else
                    //{
                    //    // Handle invalid date
                    //    // e.g. log error or show alert
                    //}



                    double netPremium = Convert.ToDouble(row.Cells[7].Text);  // BASIC_PREMIUM
                    double srcc = Convert.ToDouble(row.Cells[8].Text);        // RCC
                    double tc = Convert.ToDouble(row.Cells[9].Text);          // TC

                   // Label lbladminfee = row.FindControl("lblAdminFeeVal") as Label;
                    Label lbladminfee = (Label)row.FindControl("lblAdminFeeVal");
                    double adminfee = 0;
                    if (lbladminfee != null && !string.IsNullOrWhiteSpace(lbladminfee.Text))
                    {
                        adminfee = Convert.ToDouble(lbladminfee.Text);
                    }

                    //Label lblnbtVal = row.FindControl("lblNbt") as Label;
                    Label lblnbtVal = (Label)row.FindControl("lblNbt");
                    double nbtVal = 0;
                    if (lblnbtVal != null && !string.IsNullOrWhiteSpace(lblnbtVal.Text))
                    {
                        nbtVal = Convert.ToDouble(lblnbtVal.Text);
                    }

                    //Label lbllblVat = row.FindControl("lblVat") as Label;
                    Label lbllblVat = (Label)row.FindControl("lblVat");
                    double vatVal = 0;
                    if (lbllblVat != null && !string.IsNullOrWhiteSpace(lbllblVat.Text))
                    {
                        vatVal = Convert.ToDouble(lbllblVat.Text);

                    }


                    string cusName = row.Cells[4].Text.Trim().Replace((char)160, ' '); ;
                    //string cusCoNo = row.Cells[6].Text;

                    TextBox txtCusPhone = (TextBox)row.FindControl("txtCusPhone");
                    string cusCoNo = txtCusPhone.Text;

                    Label txtPolFee = row.FindControl("lblPolicyFee") as Label;
                    double policyFee = string.IsNullOrEmpty(txtPolFee.Text) ? 0 : Convert.ToDouble(txtPolFee.Text);

                    //TextBox txtPolFee = row.FindControl("txtPolicyFee") as TextBox;
                    //double policyFee = string.IsNullOrEmpty(txtPolFee.Text) ? 0 : Convert.ToDouble(txtPolFee.Text);

                    Label lblTotalPremium1 = row.FindControl("lblTotalPremium") as Label;

                    double totalPremium = 0;

                    if (lblTotalPremium1 != null && !string.IsNullOrWhiteSpace(lblTotalPremium1.Text))
                    {
                        double.TryParse(lblTotalPremium1.Text.Trim(), out totalPremium);
                    }

                    Label lblYear = (Label)row.FindControl("lblYear");
                    int rnYear = Int32.Parse(lblYear.Text);

                    Label lblRnMonth = (Label)row.FindControl("lblMonth");
                    int rnMonth = Int32.Parse(lblRnMonth.Text);

                    Label lblCusNic = (Label)row.FindControl("lblCusNic");
                    string cusNic = (lblCusNic.Text);

                    Label lblAgency = (Label)row.FindControl("lblAgntCode");
                    string agencyCd = (lblAgency.Text);


                    Label lblSumInsu = (Label)row.FindControl("lblSunInsu");
                    string sunInsured = (lblSumInsu.Text);

                    double sunInsuredVal = 0;
                    if (lblSumInsu != null && !string.IsNullOrWhiteSpace(lblSumInsu.Text))
                    {
                        double.TryParse(lblSumInsu.Text.Trim(), out sunInsuredVal);
                    }

                    double sumInsured = Convert.ToDouble(row.Cells[6].Text);

                    string sDate = string.Empty;
                    string enDate = string.Empty;
                    string add1 = string.Empty;
                    string add2 = string.Empty;
                    string add3 = string.Empty;
                    string add4 = string.Empty;

                    //for get policy start date and end date, sum isured 
                    ora_side.GetPayFileDetails(policyNo, out sDate, out enDate, out add1, out add2, out add3, out add4);


                    //create renewal reference no
                    //string refNo = _sql.GetRefnoProcedure(policyNo, sDate, enDate, "", "F", "R");
                    string refNo = _sql.GetRefnoProcedure(policyNo, oneYearBackString, eDate, "", "F", "R");




                    details.Add(new FireRenewalMast.FireRenewalMastClass
                    {
                        RNDEPT = rndept,
                        RNPTP = rnType,
                        RNPOL = policyNo,
                        RNYR = rnYear,
                        RNMNTH = rnMonth,
                        RNSTDT = oneYearBackString,
                        //RNSTDT = DateTime.ParseExact(sDate, "yyyy-MM-dd", null).ToString("dd/MM/yyyy"),
                        RNENDT = eDate,
                        RNAGCD = Convert.ToInt32(agencyCd),
                        RNNET = netPremium,
                        RNRCC = srcc,
                        RNTC = tc,
                        RNPOLFEE = policyFee,
                        RNVAT = vatVal,
                        RNNBT = nbtVal,
                        RNTOT = totalPremium,
                        RNNAM = cusName,
                        RNADD1 = add1.Replace((char)160, ' '),
                        RNADD2 = add2.Replace((char)160, ' '),
                        RNADD3 = add3.Replace((char)160, ' '),
                        RNNIC = cusNic,
                        RNCNT = cusCoNo,
                        RNREF = refNo,
                        RN_ADMINFEE = adminfee,
                        RNSUMINSUR = sunInsuredVal,
                        SUMNOTCHASTATUS = "N"

                    });
                }

            }

        }

        //bool saversult = _sql.InsertIntoRenwalMasterTemp(details, Session["UserId"].ToString(), Session["branch_code"].ToString());
        bool saversult = _sql.UpdateIntoRenwalMasterTemp(details, Session["UserId"].ToString(), Session["branch_code"].ToString());

        if (saversult)
        {
            

            lblAlertMessage.Text = "Data successfully sent for approval process.";
            lblAlertMessage.Attributes.Add("data-alert-title", "Success");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "1";
            lblAlertMessage.Visible = true;
            // Trigger the JavaScript function manually after setting the message
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data successfully sent for approval process.', 'Success');", true);

        }
        else
        {
            

            lblAlertMessage.Text = "Data not sent for approval process. ";
            lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "1";
            lblAlertMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data not sent for approval process.', 'Success');", true);


        }

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
            lblAlertMessage.Attributes["data-alert-method"] = "1";
            lblAlertMessage.Visible = true;
            // Trigger the JavaScript function manually after setting the message
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data successfully sent for approval process.', 'Success');", true);

        }
        else
        {

            lblAlertMessage.Text = "SMS not sent. ";
            lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "1";
            lblAlertMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data not sent for approval process.', 'Success');", true);


        }
    }

    //with claim grid row click
    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        Grid_Details.Visible = false;
        WithClaimGrid.Visible = false;
        Panel1.Visible = true;
        //btn_calPremium.Visible = false;

        // Get the clicked button reference
        Button btn = (Button)sender;

        // Find the row that contains the clicked button
        GridViewRow row = (GridViewRow)btn.NamingContainer;



        string policyNo = GetSafeCellText(row.Cells[1]);
        string polStartDate = string.Empty;
        string polEndDate = GetSafeCellText(row.Cells[2]);

        DateTime endDate;
        string oneYearBackString = string.Empty;
        //if (DateTime.TryParse(polEndDate, out endDate))
        //{
        //    DateTime oneYearBack = endDate.AddYears(-1);
        //    polStartDate = oneYearBack.ToString("dd/MM/yyyy"); // Or any format you need

        //}
        //else
        //{

        //}

        if (DateTime.TryParseExact(polEndDate, "dd/MM/yyyy",
                           System.Globalization.CultureInfo.InvariantCulture,
                           System.Globalization.DateTimeStyles.None, out endDate))
        {
            DateTime oneYearBack = endDate.AddYears(-1);
            polStartDate = oneYearBack.ToString("dd/MM/yyyy");
        }
        else
        {
            // Handle invalid date case
        }

        // string bank = GetSafeCellText(row.Cells[4]);
        string branch = GetSafeCellText(row.Cells[3]);

        //string custName = GetSafeCellText(row.Cells[4]);

        string custName = GetSafeCellText(row.Cells[4]);
        if (custName.Contains("&#160;"))
        {
            custName = custName.Replace("&#160;", " ");
        }

        if (custName.Contains("&amp;"))
        {
            custName = custName.Replace("&amp;", "");
        }

        Label lblCusNic = (Label)row.FindControl("lblCusNic");
        string cusNic = (lblCusNic.Text);
        string custNic = cusNic;

        string custPhone = GetSafeCellText(row.Cells[5]);

        string sDate = string.Empty;
        string enDate = string.Empty;
        string add1 = string.Empty;
        string add2 = string.Empty;
        string add3 = string.Empty;
        string add4 = string.Empty;

        //for get policy start date and end date, sum isured 
        ora_side.GetPayFileDetails(policyNo, out sDate, out enDate, out add1, out add2, out add3, out add4);

        string sumInsured = GetSafeCellText(row.Cells[6]);
        string sumInsured2 = GetSafeCellText(row.Cells[7]);
        string sumInsured3 = GetSafeCellText(row.Cells[8]);
        string totalPremium = string.Empty;
        string netPremium = GetSafeCellText(row.Cells[9]);
        string rcc = GetSafeCellText(row.Cells[10]);
        string tr = GetSafeCellText(row.Cells[11]);
        string adminfee = GetSafeCellText(row.Cells[12]);
        string policyfee = string.Empty;
        string nbt = string.Empty;
        string vat = string.Empty;

        Label lblAgency = (Label)row.FindControl("lblAgntCode");
        string agency = (lblAgency.Text);

        Label lblrnyear = (Label)row.FindControl("lblYear");
        string rnYearVal = (lblrnyear.Text);

        Label lblrnMonth = (Label)row.FindControl("lblMonth");
        string rnMonthVal = (lblrnMonth.Text);



        txtPolicyNo.Text = policyNo;
        txtPolicyStartDate.Text = polStartDate;
        txtPolicyEndDate.Text = polEndDate;
        //txtBank.Text = bank;
        txtBranch.Text = branch;
        txtCustomerName.Text = custName;
        txtCustomerNIC.Text = custNic;
        txtCustomerPhone.Text = custPhone;

        txtAdd1.Text = add1;
        txtAdd2.Text = add2;
        txtAdd3.Text = add3;
        txtAdd4.Text = add4;
        // txtSumInsured.Text = sumInsured;  // Uncomment if needed
        txtTotalPremium.Text = totalPremium;
        txtNetPremium.Text = netPremium;
        txtRcc.Text = rcc;
        txtTr.Text = tr;
        txtAdminFee.Text = adminfee;
        //txtPolicyFee1.Text = policyfee;
        txtPolicyFee1.Text = "100.00";
        txtNbt.Text = nbt;
        txtVat.Text = vat;
        txtAgency.Text = agency;
        rnYear.Value = rnYearVal;
        rnMonth.Value = rnMonthVal;


        if (!string.IsNullOrWhiteSpace(sumInsured))
            txtSumInsured1.Text = Convert.ToDouble(sumInsured).ToString("N2");

        if (!string.IsNullOrWhiteSpace(sumInsured2))
            txtSumInsured2.Text = Convert.ToDouble(sumInsured2).ToString("N2");

        if (!string.IsNullOrWhiteSpace(sumInsured3))
            txtSumInsured3.Text = Convert.ToDouble(sumInsured3).ToString("N2");

        DataTable details = new DataTable();
        details = orcle_trans.GetRows(_sql.GetFireRenewalRemarlData(policyNo), details);

        DataTable extraExcessDetail = new DataTable();
        extraExcessDetail = orcle_trans.GetRows(_sql.GetFireRenewalExtraAccess(policyNo), extraExcessDetail);

        if (orcle_trans.Trans_Sucess_State == true)
        {

            if (details.Rows.Count > 0)
            {

                txtPercentage1.Text = details.Rows[0]["EXCESS_PRE"] != DBNull.Value ? details.Rows[0]["EXCESS_PRE"].ToString() : string.Empty;
                txtPercentage2.Text = details.Rows[0]["EXCESS_PRE2"] != DBNull.Value ? details.Rows[0]["EXCESS_PRE2"].ToString() : string.Empty;
                txtAmount1.Text = details.Rows[0]["EXCESS_AMO"] != DBNull.Value ? details.Rows[0]["EXCESS_AMO"].ToString() : string.Empty;
                txtAmount2.Text = details.Rows[0]["EXCESS_AMO2"] != DBNull.Value ? details.Rows[0]["EXCESS_AMO2"].ToString() : string.Empty;
                txtRemark.Text = details.Rows[0]["REMARK"] != DBNull.Value ? details.Rows[0]["REMARK"].ToString() : string.Empty;

                // Load dynamic excess rows
                List<ExcessDetail> excessList = new List<ExcessDetail>();
                for (int i = 0; i < extraExcessDetail.Rows.Count; i++)
                {
                    string excessId = extraExcessDetail.Rows[i]["ID"] != DBNull.Value ? extraExcessDetail.Rows[i]["ID"].ToString() : string.Empty;
                    string name = extraExcessDetail.Rows[i]["EXCESS_DESCRIPTION"] != DBNull.Value ? extraExcessDetail.Rows[i]["EXCESS_DESCRIPTION"].ToString() : string.Empty;
                    string perc = extraExcessDetail.Rows[i]["EXCESS_PRECENTAGE"] != DBNull.Value ? extraExcessDetail.Rows[i]["EXCESS_PRECENTAGE"].ToString() : string.Empty;
                    string amount = extraExcessDetail.Rows[i]["EXCESS_AMOUNT"] != DBNull.Value ? extraExcessDetail.Rows[i]["EXCESS_AMOUNT"].ToString() : string.Empty;

                    if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(perc) || !string.IsNullOrWhiteSpace(amount))
                    {
                        excessList.Add(new ExcessDetail
                        {
                            ExcessId = excessId,
                            ExcessName = name,
                            Percentage = perc,
                            Amount = amount
                        });
                    }
                }

                if (excessList != null && excessList.Count > 0)
                {
                    // Store in ViewState with a unique key (good practice)
                    ViewState["Excess_" + policyNo] = ConvertToDataTable(excessList);

                    // Bind data to the Repeater
                    rptAdditionalExcess.DataSource = excessList;
                    rptAdditionalExcess.DataBind();
                }
                else
                {
                    // Clear the repeater if no data
                    rptAdditionalExcess.DataSource = null;
                    rptAdditionalExcess.DataBind();
                }


            }
            else
            {
                txtPercentage1.Text = string.Empty;
                txtPercentage2.Text = string.Empty;
                txtAmount1.Text = string.Empty;
                txtAmount2.Text = string.Empty;
                txtRemark.Text = string.Empty;
                rptAdditionalExcess.DataSource = null;
                rptAdditionalExcess.DataBind();
            }
        }

        //load exctra aceess row
        LoadPremium(policyNo);

    }

    private void LoadPremium(string policyNo)
    {
        // Load fixed values here like txtNetPremPop, txtRCCPop etc. from DB

        if (ViewState["Excess_" + policyNo] != null)
        {
            DataTable dt = (DataTable)ViewState["Excess_" + policyNo];
            rptAdditionalExcess.DataSource = dt;
            rptAdditionalExcess.DataBind();
        }
        else
        {
            DataTable dt = CreateExcessDataTable();
            ViewState["Excess_" + policyNo] = dt;
            rptAdditionalExcess.DataSource = dt;
            rptAdditionalExcess.DataBind();
        }

        HiddenFieldSelectedPolicy.Value = policyNo; // for reuse in add row
    }

    private DataTable CreateExcessDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ExcessId");
        dt.Columns.Add("ExcessName");
        dt.Columns.Add("Percentage");
        dt.Columns.Add("Amount");
        return dt;
    }

    private DataTable ConvertToDataTable(List<ExcessDetail> list)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ExcessId");
        dt.Columns.Add("ExcessName");
        dt.Columns.Add("Percentage");
        dt.Columns.Add("Amount");

        foreach (var item in list)
        {
            dt.Rows.Add(item.ExcessId, item.ExcessName, item.Percentage, item.Amount);
        }

        return dt;
    }


    public class ExcessDetail
    {
        public string ExcessId { get; set; }
        public string ExcessName { get; set; }
        public string Percentage { get; set; }
        public string Amount { get; set; }
    }
    private string GetSafeCellText(TableCell cell)
    {
        if (cell == null || string.IsNullOrEmpty(cell.Text) || cell.Text.Trim() == "" || cell.Text.Trim() == "&nbsp;")
        {
            return "";
        }
        return cell.Text.Trim();
    }

    protected void btn_sentToApp_Click(object sender, EventArgs e)
    {

        List<FireRenewalMast.FireRenewalMastClass> details = new List<FireRenewalMast.FireRenewalMastClass>();


        string policyNo = txtPolicyNo.Text.Trim();
        string rndept = "F";
        string rnType = "FD_RENEWAL";
        string eDate = txtPolicyEndDate.Text.Trim();

        string oneYearBackString = txtPolicyStartDate.Text.Trim();
        


        double netPremium = Convert.ToDouble(txtNetPremium.Text);  // BASIC_PREMIUM
        double srcc = Convert.ToDouble(txtRcc.Text);        // RCC
        double tc = Convert.ToDouble(txtTr.Text);          // TC
        double adminfee = Convert.ToDouble(txtAdminFee2.Text);

        double nbtVal = Convert.ToDouble(txtNbt.Text);
        double vatVal = Convert.ToDouble(txtVat.Text);


        string cusName = txtCustomerName.Text;

        string cusCoNo = txtCustomerPhone.Text;
        if (cusCoNo.StartsWith("0"))
        {
            cusCoNo = cusCoNo.Substring(1);
        }


        double policyFee = Convert.ToDouble(txtPolicyFee1.Text);


        double totalPremium = Convert.ToDouble(txtTotalPremium.Text);



        int rnYearV = Int32.Parse(rnYear.Value);
        int rnMonthV = Int32.Parse(rnMonth.Value);

        string cusNic = (txtCustomerNIC.Text);

        string agencyCd = string.Empty;
        if (!string.IsNullOrEmpty(txtAgency.Text))
        {
            agencyCd = txtAgency.Text;
        }


        double sumInsured = Convert.ToDouble(txtSumInsured1.Text);

        string add1 = txtAdd1.Text;
        string add2 = txtAdd2.Text;
        string add3 = txtAdd3.Text;
        string add4 = txtAdd4.Text;

        string remark = txtRemark.Text;

        double excessPre = 0;
        if (!string.IsNullOrEmpty(txtPercentage1.Text))
        {
            excessPre = Convert.ToDouble(txtPercentage1.Text);
        }
        else
        {
            excessPre = 0;
        }

        double excessAmo = 0;
        if (!string.IsNullOrEmpty(txtAmount1.Text))
        {
            excessAmo = Convert.ToDouble(txtAmount1.Text);
        }
        else
        {
            excessAmo = 0;
        }

        double excessPre2 = 0;
        if (!string.IsNullOrEmpty(txtPercentage2.Text))
        {
            excessPre2 = Convert.ToDouble(txtPercentage2.Text);
        }
        else
        {
            excessPre2 = 0;
        }

        double excessAmo2 = 0;
        if (!string.IsNullOrEmpty(txtAmount2.Text))
        {
            excessAmo2 = Convert.ToDouble(txtAmount2.Text);
        }
        else
        {
            excessAmo2 = 0;
        }

        //create renewal reference no
        //string refNo = _sql.GetRefnoProcedure(policyNo, sDate, enDate, "", "F", "R");
        string refNo = _sql.GetRefnoProcedure(policyNo, oneYearBackString, eDate, "", "F", "R");




        details.Add(new FireRenewalMast.FireRenewalMastClass
        {
            RNDEPT = rndept,
            RNPTP = rnType,
            RNPOL = policyNo,
            RNYR = rnYearV,
            RNMNTH = rnMonthV,
            RNSTDT = oneYearBackString,
            //RNSTDT = DateTime.ParseExact(sDate, "yyyy-MM-dd", null).ToString("dd/MM/yyyy"),
            RNENDT = eDate,
            RNAGCD = Convert.ToInt32(agencyCd),
            RNNET = netPremium,
            RNRCC = srcc,
            RNTC = tc,
            RNPOLFEE = policyFee,
            RNVAT = vatVal,
            RNNBT = nbtVal,
            RNTOT = totalPremium,
            RNNAM = cusName,
            RNADD1 = add1.Replace((char)160, ' '),
            RNADD2 = add2.Replace((char)160, ' '),
            RNADD3 = add3.Replace((char)160, ' '),
            RNNIC = cusNic,
            RNCNT = cusCoNo,
            RNREF = refNo,
            RN_ADMINFEE = adminfee,
            RNSUMINSUR = sumInsured,
            SUMNOTCHASTATUS = "N",
            EXCESSPRE = excessPre,
            EXCESSAMO = excessAmo,
            EXCESSPRE2 = excessPre2,
            EXCESSAMO2 = excessAmo2,
            REMARK = remark

        });



        bool saversult = _sql.InsertIntoRenwalMasterTemp_WithCla(details, Session["UserId"].ToString(), Session["branch_code"].ToString());      

        if (saversult)
        {
            InserExtraExcess(policyNo);

            string policyNo2 = HiddenFieldSelectedPolicy.Value;
            ViewState["Excess_" + policyNo2] = null;

            lblAlertMessage.Text = "Data successfully sent for approval process.";
            lblAlertMessage.Attributes.Add("data-alert-title", "Success");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "1";
            lblAlertMessage.Visible = true;
            // Trigger the JavaScript function manually after setting the message
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data successfully sent for approval process.', 'Success');", true);

        }
        else
        {
            string policyNo2 = HiddenFieldSelectedPolicy.Value;
            ViewState["Excess_" + policyNo2] = null;

            lblAlertMessage.Text = "Data not sent for approval process. ";
            lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "1";
            lblAlertMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data not sent for approval process.', 'Success');", true);


        }

    }

    private void InserExtraExcess(string policyNo)
    {
        bool extrExcessRes = true;
        DataTable dt = ViewState["Excess_" + policyNo] as DataTable;

        // Read current values from Repeater before inserting
        foreach (RepeaterItem item in rptAdditionalExcess.Items)
        {
            TextBox txtExcessId = (TextBox)item.FindControl("txtExcessID");
            TextBox txtExcessName = (TextBox)item.FindControl("txtExcessName");
            TextBox txtPercentage = (TextBox)item.FindControl("txtPercentage");
            TextBox txtAmount = (TextBox)item.FindControl("txtAmount");

            string excessName = txtExcessName.Text.Trim();
            double percentage = Convert.ToDouble(txtPercentage.Text.Trim());
            double amount = Convert.ToDouble(txtAmount.Text.Trim());
            string excessIdVal = txtExcessId.Text.Trim();

            int excessId = 0;

            if (excessIdVal != null && !string.IsNullOrEmpty(excessIdVal))
            {
                excessId = Convert.ToInt32(excessIdVal);
            }

            // Now insert into your database
            bool result = _sql.InsertUpdateExtExcess(policyNo, excessName, percentage, amount, excessId);

            // Now insert into your database
            //bool result = _sql.InsertExcessRecord(excessName, percentage, amount, policyNo);
        }
    }
    protected void Grid_Details_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = Grid_Details.Rows[rowIndex];

            string id = row.Cells[0].Text;
            string netPre = row.Cells[7].Text;
            string rcc = row.Cells[8].Text;
            string tr = row.Cells[9].Text;
            string policyNo = row.Cells[1].Text;

            Label lblrnyear = (Label)row.FindControl("lblYear");
            string rnYearVal = (lblrnyear.Text);

            Label lblrnMonth = (Label)row.FindControl("lblMonth");
            string rnMonthVal = (lblrnMonth.Text);

            Label lblAdminFeePre = (Label)row.FindControl("lblAminPRe");
            string adminFeePre = string.Empty;
            if (lblAdminFeePre != null)
            {
                adminFeePre = lblAdminFeePre.Text;
            }

            // string policyFee = row.Cells[11].Text;
            Label lblpolicyFee = (Label)row.FindControl("lblPolicyFee");
            string policyFee = string.Empty;
            if (lblpolicyFee != null)
            {
                policyFee = lblpolicyFee.Text;
            }

            txtNetPremPop.Text = netPre;
            txtRCCPop.Text = rcc;
            txtTRPop.Text = tr;
            txtPolNoPop.Text = policyNo;

            txtPolFee.Text = Convert.ToDouble(policyFee).ToString("N2");
            txtAdminFeePre.Text = Convert.ToDouble(adminFeePre).ToString("N2");

            txtTotPre.Text = string.Empty;
            txtNbtPop.Text = string.Empty;
            txtVatValue.Text = string.Empty;
            txtAdminFeeVal.Text = string.Empty;

            HiddenRNyearPop.Value = rnYearVal;
            HiddenRnMonthPop.Value = rnMonthVal;

            DataTable details = new DataTable();
            details = orcle_trans.GetRows(_sql.GetFireRenewalRemarlData(policyNo), details);

            DataTable extraExccessDetails = new DataTable();
            extraExccessDetails = orcle_trans.GetRows(_sql.GetFireRenewalExtraAccess(policyNo), extraExccessDetails);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {

                    txtExcePrePop1.Text = details.Rows[0]["EXCESS_PRE"] != DBNull.Value ? details.Rows[0]["EXCESS_PRE"].ToString() : string.Empty;
                    txtExcePrePop2.Text = details.Rows[0]["EXCESS_PRE2"] != DBNull.Value ? details.Rows[0]["EXCESS_PRE2"].ToString() : string.Empty;
                    txtExceAmoPo1.Text = details.Rows[0]["EXCESS_AMO"] != DBNull.Value ? details.Rows[0]["EXCESS_AMO"].ToString() : string.Empty;
                    txtExceAmoPo2.Text = details.Rows[0]["EXCESS_AMO2"] != DBNull.Value ? details.Rows[0]["EXCESS_AMO2"].ToString() : string.Empty;
                    txtRemrPop.Text = details.Rows[0]["REMARK"] != DBNull.Value ? details.Rows[0]["REMARK"].ToString() : string.Empty;

                    // Load dynamic excess rows
                    List<ExcessDetail> excessList = new List<ExcessDetail>();
                    for (int i = 0; i < extraExccessDetails.Rows.Count; i++)
                    {
                        string excessIdVal = extraExccessDetails.Rows[i]["EXCESS_ID"] != DBNull.Value ? extraExccessDetails.Rows[i]["EXCESS_ID"].ToString() : string.Empty;
                        string name = extraExccessDetails.Rows[i]["EXCESS_DESCRIPTION"] != DBNull.Value ? extraExccessDetails.Rows[i]["EXCESS_DESCRIPTION"].ToString() : string.Empty;
                        string perc = extraExccessDetails.Rows[i]["EXCESS_PRECENTAGE"] != DBNull.Value ? extraExccessDetails.Rows[i]["EXCESS_PRECENTAGE"].ToString() : string.Empty;
                        string amount = extraExccessDetails.Rows[i]["EXCESS_AMOUNT"] != DBNull.Value ? extraExccessDetails.Rows[i]["EXCESS_AMOUNT"].ToString() : string.Empty;

                        if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(perc) || !string.IsNullOrWhiteSpace(amount))
                        {
                            excessList.Add(new ExcessDetail
                            {
                                ExcessId = excessIdVal,
                                ExcessName = name,
                                Percentage = perc,
                                Amount = amount
                            });
                        }
                    }

                    if (excessList != null && excessList.Count > 0)
                    {
                        // Store in ViewState with a unique key (good practice)
                        ViewState["Excess_" + policyNo] = ConvertToDataTable(excessList);

                        // Bind data to the Repeater
                        rptAdditionalExcessPop.DataSource = excessList;
                        rptAdditionalExcessPop.DataBind();
                    }
                    else
                    {
                        // Clear the repeater if no data
                        rptAdditionalExcessPop.DataSource = null;
                        rptAdditionalExcessPop.DataBind();
                    }



                }
                else
                {
                    txtExcePrePop1.Text = string.Empty;
                    txtExcePrePop2.Text = string.Empty;
                    txtExceAmoPo1.Text = string.Empty;
                    txtExceAmoPo2.Text = string.Empty;
                    txtRemrPop.Text = string.Empty;

                    ViewState["Excess_" + policyNo] = null;
                    rptAdditionalExcessPop.DataSource = null;
                    rptAdditionalExcessPop.DataBind();
                }
            }

            //load selected data row including dynamic excess
            LoadPremiumPopup(policyNo);

            pnlPopup.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showPopup();", true);

        }
    }

    private void LoadPremiumPopup(string policyNo)
    {
        // Load fixed values here like txtNetPremPop, txtRCCPop etc. from DB

        if (ViewState["Excess_" + policyNo] != null)
        {
            DataTable dt = (DataTable)ViewState["Excess_" + policyNo];
            rptAdditionalExcessPop.DataSource = dt;
            rptAdditionalExcessPop.DataBind();
        }
        else
        {
            DataTable dt = CreateExcessDataTablePopup();
            ViewState["Excess_" + policyNo] = dt;
            rptAdditionalExcessPop.DataSource = dt;
            rptAdditionalExcessPop.DataBind();
        }

        HiddenFieldSelectedPolicy.Value = policyNo; // for reuse in add row
    }

    private DataTable CreateExcessDataTablePopup()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ExcessId");
        dt.Columns.Add("ExcessName");
        dt.Columns.Add("Percentage");
        dt.Columns.Add("Amount");
        return dt;
    }
    protected void btnEditSave_Click(object sender, EventArgs e)
    {
        List<FireRenewalMast.FireRenewalMastClass> details = new List<FireRenewalMast.FireRenewalMastClass>();

        string policyNo = txtPolNoPop.Text.Trim();
        double netPremPop = 0;
        if (!string.IsNullOrEmpty(txtNetPremPop.Text))
        {
            netPremPop = Convert.ToDouble(txtNetPremPop.Text);
        }
        else
        {
            netPremPop = 0;
        }

        double RCCPop = 0;
        if (!string.IsNullOrEmpty(txtRCCPop.Text))
        {
            RCCPop = Convert.ToDouble(txtRCCPop.Text);
        }
        else
        {
            RCCPop = 0;
        }

        double TRPop = 0;
        if (!string.IsNullOrEmpty(txtTRPop.Text))
        {
            TRPop = Convert.ToDouble(txtTRPop.Text);
        }
        else
        {
            TRPop = 0;
        }

        string remark = string.Empty;
        if (!string.IsNullOrEmpty(txtTRPop.Text))
        {
            remark = txtRemrPop.Text.Trim();
        }
        else
        {
            remark = "";
        }


        double excessPre1 = 0;
        if (!string.IsNullOrEmpty(txtExcePrePop1.Text))
        {
            excessPre1 = Convert.ToDouble(txtExcePrePop1.Text);
        }
        else
        {
            excessPre1 = 0;
        }

        double excessVal1 = 0;
        if (!string.IsNullOrEmpty(txtExceAmoPo1.Text))
        {
            excessVal1 = Convert.ToDouble(txtExceAmoPo1.Text);
        }
        else
        {
            excessVal1 = 0;
        }

        double excessPre2 = 0;
        if (!string.IsNullOrEmpty(txtExcePrePop2.Text))
        {
            excessPre2 = Convert.ToDouble(txtExcePrePop2.Text);
        }
        else
        {
            excessPre2 = 0;
        }

        double excessVal2 = 0;
        if (!string.IsNullOrEmpty(txtExceAmoPo2.Text))
        {
            excessVal2 = Convert.ToDouble(txtExceAmoPo2.Text);
        }
        else
        {
            excessVal2 = 0;
        }

        double nbtVal = 0;
        if (!string.IsNullOrEmpty(txtNbtPop.Text))
        {
            nbtVal = Convert.ToDouble(txtNbtPop.Text);
        }
        else
        {
            nbtVal = 0;
        }

        double vatVal = 0;
        if (!string.IsNullOrEmpty(txtVatValue.Text))
        {
            vatVal = Convert.ToDouble(txtVatValue.Text);
        }
        else
        {
            vatVal = 0;
        }

        double adminFeeVal = 0;
        if (!string.IsNullOrEmpty(txtAdminFeeVal.Text))
        {
            adminFeeVal = Convert.ToDouble(txtAdminFeeVal.Text);
        }
        else
        {
            adminFeeVal = 0;
        }

        double finalPayPRem = 0;
        if (!string.IsNullOrEmpty(txtTotPre.Text))
        {
            finalPayPRem = Convert.ToDouble(txtTotPre.Text);
        }
        else
        {
            finalPayPRem = 0;
        }

        int year = Convert.ToInt32(HiddenRNyearPop.Value);
        int month = Convert.ToInt32(HiddenRnMonthPop.Value);



        details.Add(new FireRenewalMast.FireRenewalMastClass
        {

            RNPOL = policyNo,
            RNNET = netPremPop,
            RNRCC = RCCPop,
            RNTC = TRPop,
            EXCESSPRE = excessPre1,
            EXCESSAMO = excessVal1,
            EXCESSPRE2 = excessPre2,
            EXCESSAMO2 = excessVal2,
            REMARK = remark,
            RNYR = year,
            RNMNTH = month,
            RNNBT = nbtVal,
            RNVAT = vatVal,
            RN_ADMINFEE = adminFeeVal,
            RNTOT = finalPayPRem

        });



        //bool saversult = _sql.UpdateIntoRenwalMasterTemp_WithCla(details, Session["UserId"].ToString(), Session["branch_code"].ToString());
        bool saversult = _sql.UpdateIntoRenwalMasterTemp_InPregress(details, Session["UserId"].ToString(), Session["branch_code"].ToString());

        if (saversult)
        {
            string policyNo2 = HiddenFieldSelectedPolicy.Value;

            // add extra excess
            InserOrUpdateExtraExcess(policyNo2);


            ViewState["Excess_" + policyNo2] = null;
            pnlPopup.Visible = false;

            this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.ToString().ToUpper().Trim(), txt_pol_no.Text.Trim(), ddl_branch.SelectedValue.ToString());

            lblAlertMessage.Text = "Data successfully Edited.";
            lblAlertMessage.Attributes.Add("data-alert-title", "Success");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "2";
            lblAlertMessage.Visible = true;
            // Trigger the JavaScript function manually after setting the message
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert2('Data successfully Edited.', 'Success');", true);

        }
        else
        {
            string policyNo2 = HiddenFieldSelectedPolicy.Value;
            ViewState["Excess_" + policyNo2] = null;

            lblAlertMessage.Text = "Update failed";
            lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "2";
            lblAlertMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert2('Update failed', 'Success');", true);

        }

    }

    //update or inert extra excess
    private void InserOrUpdateExtraExcess(string policyNo)
    {
        bool extrExcessRes = true;
        DataTable dt = ViewState["Excess_" + policyNo] as DataTable;

        // Read current values from Repeater before inserting
        foreach (RepeaterItem item in rptAdditionalExcessPop.Items)
        {
            TextBox txtExcessID = (TextBox)item.FindControl("txtExcessId");
            TextBox txtExcessName = (TextBox)item.FindControl("txtExcessName");
            TextBox txtPercentage = (TextBox)item.FindControl("txtPercentage");
            TextBox txtAmount = (TextBox)item.FindControl("txtAmount");


            string excessName = txtExcessName.Text.Trim();
            double percentage = Convert.ToDouble(txtPercentage.Text.Trim());
            double amount = Convert.ToDouble(txtAmount.Text.Trim());
            string excessIDVal = txtExcessID.Text.Trim();
            int excessId = 0;

            if (excessIDVal != null && !string.IsNullOrEmpty(excessIDVal))
            {
                excessId = Convert.ToInt32(excessIDVal);
            }

            // Now insert into your database
            bool result = _sql.InsertUpdateExtExcess(policyNo, excessName, percentage, amount, excessId);
        }
    }
    //end of function

    protected void btnAddRow_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;


        string policyNo = HiddenFieldSelectedPolicy.Value;
        string key = "Excess_" + policyNo;

        DataTable dt = ViewState[key] as DataTable ?? CreateExcessDataTable();

        // Save existing repeater values
        for (int i = 0; i < rptAdditionalExcess.Items.Count; i++)
        {
            RepeaterItem item = rptAdditionalExcess.Items[i];
            TextBox txtExcessId = (TextBox)item.FindControl("txtExcessId");
            TextBox txtExcessName = (TextBox)item.FindControl("txtExcessName");
            TextBox txtPercentage = (TextBox)item.FindControl("txtPercentage");
            TextBox txtAmount = (TextBox)item.FindControl("txtAmount");

            if (i < dt.Rows.Count)
            {
                dt.Rows[i]["ExcessId"] = txtExcessId.Text;
                dt.Rows[i]["ExcessName"] = txtExcessName.Text;
                dt.Rows[i]["Percentage"] = txtPercentage.Text;
                dt.Rows[i]["Amount"] = txtAmount.Text;
            }
        }

        // Add new empty row
        dt.Rows.Add("", "", "");

        ViewState[key] = dt;

        rptAdditionalExcess.DataSource = dt;
        rptAdditionalExcess.DataBind();
    }

    protected void btnRemoveRow_Click1(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        //renewGridViewSection.Visible = true;

        string policyNo = HiddenFieldSelectedPolicy.Value;
        string key = "Excess_" + policyNo;

        DataTable dt = ViewState[key] as DataTable ?? CreateExcessDataTable();

        // Save existing data from repeater
        for (int i = 0; i < rptAdditionalExcess.Items.Count; i++)
        {
            RepeaterItem item = rptAdditionalExcess.Items[i];
            TextBox txtExcessId = (TextBox)item.FindControl("txtExcessId");
            TextBox txtExcessName = (TextBox)item.FindControl("txtExcessName");
            TextBox txtPercentage = (TextBox)item.FindControl("txtPercentage");
            TextBox txtAmount = (TextBox)item.FindControl("txtAmount");

            if (i < dt.Rows.Count)
            {
                dt.Rows[i]["ExcessId"] = txtExcessId.Text;
                dt.Rows[i]["ExcessName"] = txtExcessName.Text;
                dt.Rows[i]["Percentage"] = txtPercentage.Text;
                dt.Rows[i]["Amount"] = txtAmount.Text;
            }
        }

        // Remove last row if any
        //if (dt.Rows.Count > 0)
        //{
        //    dt.Rows.RemoveAt(dt.Rows.Count - 1);
        //}
        if (dt.Rows.Count > 0)
        {
            DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
            string excessId = lastRow["ExcessId"].ToString();

            if (!string.IsNullOrWhiteSpace(excessId))
            {
                bool result = _sql.DelteFromExtraExcess(txtPolicyNo.Text, Convert.ToInt32(excessId));
            }

            // Then remove the row from the table
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
        }

        ViewState[key] = dt;

        rptAdditionalExcess.DataSource = dt;
        rptAdditionalExcess.DataBind();
    }

    protected void btnAddRow_Click2(object sender, EventArgs e)
    {
        pnlPopup.Visible = true;
        renewGridViewSection.Visible = true;

        string policyNo = HiddenFieldSelectedPolicy.Value;
        string key = "Excess_" + policyNo;

        DataTable dt = ViewState[key] as DataTable ?? CreateExcessDataTablePopup();

        // Save existing repeater values
        for (int i = 0; i < rptAdditionalExcessPop.Items.Count; i++)
        {
            RepeaterItem item = rptAdditionalExcessPop.Items[i];
            TextBox txtExcessId = (TextBox)item.FindControl("txtExcessId");
            TextBox txtExcessName = (TextBox)item.FindControl("txtExcessName");
            TextBox txtPercentage = (TextBox)item.FindControl("txtPercentage");
            TextBox txtAmount = (TextBox)item.FindControl("txtAmount");

            if (i < dt.Rows.Count)
            {
                dt.Rows[i]["ExcessId"] = txtExcessId.Text;
                dt.Rows[i]["ExcessName"] = txtExcessName.Text;
                dt.Rows[i]["Percentage"] = txtPercentage.Text;
                dt.Rows[i]["Amount"] = txtAmount.Text;
            }
        }

        // Add new empty row
        dt.Rows.Add("", "", "");

        ViewState[key] = dt;

        rptAdditionalExcessPop.DataSource = dt;
        rptAdditionalExcessPop.DataBind();
    }

    // Optional: clear on save or close
    protected void btnClose_Click(object sender, EventArgs e)
    {
        string policyNo = HiddenFieldSelectedPolicy.Value;
        ViewState["Excess_" + policyNo] = null;
        pnlPopup.Visible = false;
    }

    protected void btnClose_Click2(object sender, EventArgs e)
    {
        string policyNo = HiddenFieldSelectedPolicy.Value;
        ViewState["Excess_" + policyNo] = null;

        Panel1.Visible = false;
        WithClaimGrid.Visible = true;
    }

    protected void btnRemoveRow_Click2(object sender, EventArgs e)
    {
        pnlPopup.Visible = true;
        renewGridViewSection.Visible = true;

        string policyNo = HiddenFieldSelectedPolicy.Value;
        string key = "Excess_" + policyNo;

        DataTable dt = ViewState[key] as DataTable ?? CreateExcessDataTablePopup();

        // Save existing data from repeater
        for (int i = 0; i < rptAdditionalExcessPop.Items.Count; i++)
        {
            RepeaterItem item = rptAdditionalExcessPop.Items[i];
            TextBox txtExcessId = (TextBox)item.FindControl("txtExcessId");
            TextBox txtExcessName = (TextBox)item.FindControl("txtExcessName");
            TextBox txtPercentage = (TextBox)item.FindControl("txtPercentage");
            TextBox txtAmount = (TextBox)item.FindControl("txtAmount");

            if (i < dt.Rows.Count)
            {
                dt.Rows[i]["ExcessId"] = txtExcessId.Text;
                dt.Rows[i]["ExcessName"] = txtExcessName.Text;
                dt.Rows[i]["Percentage"] = txtPercentage.Text;
                dt.Rows[i]["Amount"] = txtAmount.Text;
            }
        }

        // Remove last row if any
        if (dt.Rows.Count > 0)
        {
            DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
            string excessId = lastRow["ExcessId"].ToString();

            if (!string.IsNullOrWhiteSpace(excessId))
            {
                bool result = _sql.DelteFromExtraExcess(txtPolNoPop.Text, Convert.ToInt32(excessId));
            }

            // Then remove the row from the table
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
        }

        ViewState[key] = dt;

        rptAdditionalExcessPop.DataSource = dt;
        rptAdditionalExcessPop.DataBind();
    }

    private double ConvertToDouble(object value)
    {
        if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            return 0.0;

        double result;
        if (double.TryParse(value.ToString(), out result))
            return result;

        return 0.0;
    }

    //cal premium and other values base on admin edited values
    protected void btnPrmCal_Click(object sender, EventArgs e)
    {
        double VATPercent = _sql.Get_VAT_Precentage();


        double netPremium = ConvertToDouble(txtNetPremPop.Text);  // BASIC_PREMIUM
        double srcc = ConvertToDouble(txtRCCPop.Text);      // RCC
        double tc = ConvertToDouble(txtTRPop.Text);         // TC
        double adminFeeprecentage = ConvertToDouble(txtAdminFeePre.Text); // adminfee


        double policyFee = ConvertToDouble(txtPolFee.Text); // policy fee

        double Cal_ADMIN_FEETemp = ((netPremium + srcc + tc) * adminFeeprecentage) / 100;

        string todayStr = DateTime.Now.ToString("dd/MM/yyyy");
        DateTime date = DateTime.ParseExact(todayStr, "dd/MM/yyyy", CultureInfo.InvariantCulture);

        int date2 = Convert.ToInt32(date.ToString("yyyyMMdd"));
        double sumForTaxVat = 0;
        sumForTaxVat = netPremium + srcc + tc + policyFee + Cal_ADMIN_FEETemp;
        double Cal_NBTTemp, Cal_VATTemp, Cal_TotalTemp = 0;
        using (OracleConnection conn = orcl_con.GetConnection())
        {
            conn.Open();
            using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT_DATE", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("taxLiableAmount", sumForTaxVat);
                cmd.Parameters.AddWithValue("requestDate", date2);
                cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                OracleDataReader dr = cmd.ExecuteReader();

                Cal_NBTTemp = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                Cal_VATTemp = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                dr.Close();

            }
            conn.Close();
        }

        Cal_TotalTemp = sumForTaxVat + Cal_NBTTemp + Cal_VATTemp;

        txtTotPre.Text = Cal_TotalTemp.ToString("N2");
        txtNbtPop.Text = Cal_NBTTemp.ToString("N2");
        txtVatValue.Text = Cal_VATTemp.ToString("N2");
        txtAdminFeeVal.Text = Cal_ADMIN_FEETemp.ToString("N2");

        btnSave.Enabled = true;
        pnlPopup.Visible = true;
    }

    protected void SunInsuNotChangedGrid_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}