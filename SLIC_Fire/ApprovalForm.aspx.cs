using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SLIC_Fire_ApprovalForm : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Oracle_Transaction Oracle_Trans = new Oracle_Transaction();
    ORCL_Connection orcl_con = new ORCL_Connection();
    Execute_sql _Sql = new Execute_sql();
    LogFile Err = new LogFile();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblAlertMessage.Text = "";
        lblAlertMessage.Visible = false;
        pnlPopup.Visible = false;
        RejectPanel.Visible = false;
        if (!Page.IsPostBack)
        {
            if (Session["AccessAdmin"] != null || Session["BranchAdmin"] != null)
            {
                UserId.Value = Session["UserId"].ToString();
                brCode.Value = Session["brcode"].ToString();

                if (Session["AccessAdmin"].ToString() == "Y" || Session["BranchAdmin"].ToString() == "Y")
                {
                    this.InitializedListBranch(ddl_branch, "BRNAM", "BRCOD", this._Sql.GetBranch(Convert.ToInt32(Session["brcode"].ToString())), "'BRCOD'");

                    if (brCode.Value == "10")
                    {
                        divBudiUnit.Visible = true;
                    }
                    else
                    {
                        divBudiUnit.Visible = false;
                        ddlBusUnit.SelectedValue = "0";
                    }
                }
                else
                {
                    string msg = "You are not authorized to access this page. Please contact System Administrtor.";

                    //Err.ErrorLog(Server.MapPath("Logs/ErrorLog"), msg);

                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(msg));

                    Response.Redirect("../../../Secworks/Signin.asp");
                }
            }
            else
            {
                string msg = "You are not authorized to access this page. Please contact System Administrtor.";

                //Err.ErrorLog(Server.MapPath("Logs/ErrorLog"), msg);

                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(msg));

                Response.Redirect("../../../Secworks/Signin.asp");
            }


        }
        else
        {
            if (Session["AccessAdmin"] != null || Session["BranchAdmin"] != null)
            {
                UserId.Value = Session["UserId"].ToString();
                brCode.Value = Session["brcode"].ToString();

                if (Session["AccessAdmin"].ToString() == "Y" || Session["BranchAdmin"].ToString() == "Y")
                {
                    if (brCode.Value == "10")
                    {
                        divBudiUnit.Visible = true;
                    }
                    else
                    {
                        divBudiUnit.Visible = false;
                        ddlBusUnit.SelectedValue = "0";
                    }

                }
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
    private void LoadPolicies()
    {

    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }

    protected void btn_dwn_Click(object sender, EventArgs e)
    {
        //FireRnNote_pdf pdfPrint = new FireRnNote_pdf();
        //pdfPrint.print_quotation(refId, Page.User.Identity.Name, ip, true);

        FireRnNote_Newpdf pdfPrint = new FireRnNote_Newpdf();
        pdfPrint.print_policy("FFTC20210861000008");

        //var en = new EncryptDecrypt();
        //string webLink = "http://172.24.90.100:8084/Slicgeneral/FireRenewalSMSNoticeWebLink?polno=" + en.Encrypt("FFTC20210861000008");
        //string shortUrl = UrlShortener.ShortenUrl(webLink);
    }

    protected void ClearText()
    {
        this.IniGridview();
        txt_to_date.Text = string.Empty;
        txt_start_date.Text = string.Empty;

    }

    protected void IniGridview()
    {
        Grid_Details.DataSource = null;
        Grid_Details.DataBind();
    }

    protected void btn_find_Click1(object sender, EventArgs e)
    {
        GetTempData();
        //this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.ToString().ToUpper().Trim());
    }

    protected void GetTempData()
    {
        string department = ddl_polType.SelectedValue.ToString();
        string catgory = ddl_status.SelectedValue.ToString();
        string policyNo = txt_pol_no.Text.Trim();
        string branch = ddl_branch.SelectedValue.ToString();

        string subDpt = ddlSubDept.SelectedValue;
        string busiUnit = ddlBusUnit.SelectedValue;

        DataTable ApprovalTemp = new DataTable();

        //ApprovalTemp = Oracle_Trans.GetRows(this._Sql.GetApprovalTempDataFireRNSMS(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), catgory, policyNo, Session["brcode"].ToString(), busiUnit, subDpt), ApprovalTemp);
        ApprovalTemp = Oracle_Trans.GetRows(this._Sql.GetApprovalTempDataFireRNSMS(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), catgory, policyNo, branch, busiUnit, subDpt), ApprovalTemp);


        if (ApprovalTemp.Rows.Count > 0)
        {
            Grid_Details.Visible = true;
            Grid_Details.DataSource = ApprovalTemp;
            Grid_Details.DataBind();

        }
        else
        {

            lblAlertMessage.Text = "Data not found.";
            lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "1";
            lblAlertMessage.Visible = true;
            // Trigger the JavaScript function manually after setting the message
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data not found.', 'Alert');", true);


            Grid_Details.DataSource = null;
            Grid_Details.DataBind();

        }


    }

    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    [WebMethod]
    public static object HandleApproval(string action, string policyNo)
    {

        try
        {
            Oracle_Transaction Oracle_Trans = new Oracle_Transaction();
            Execute_sql _Sql = new Execute_sql();

            Oracle_Trans.GetString(_Sql.DelteFromRenewalTemp(policyNo));

            if (action == "approve")
            {
                Oracle_Trans.ExecuteInsertQuery(_Sql.InsertIntoRenwalMaster(policyNo));
                return new { success = true, message = "Policy has been processed successfully.", data = action };
            }
            else
            {
                return new { success = true, message = "The policy has been removed from the pending approval list.", data = action };
            }


        }
        catch (Exception ex)
        {
            return new { success = false, message = "Error: " + ex.Message };
        }
    }

    protected void btnInsertSelected_Click(object sender, EventArgs e)
    {
        List<FireRenewalMast.FireRenewalMastClass> details = new List<FireRenewalMast.FireRenewalMastClass>();

        foreach (GridViewRow row in Grid_Details.Rows)
        {
            CheckBox chkSelect = (CheckBox)row.FindControl("chkSelect");
            if (chkSelect != null && chkSelect.Checked)
            {
                //string dptm = row.Cells[0].Text;
                //string rn_pol_type = row.Cells[1].Text;
                //string policyNo = row.Cells[2].Text;
                //int year = Convert.ToInt32(row.Cells[3].Text);
                //int month = Convert.ToInt32(row.Cells[4].Text);
                //string rn_st_date = row.Cells[5].Text;
                //string rn_en_date = row.Cells[6].Text;
                //string rn_agt_cd = row.Cells[7].Text;
                //double netVal = Convert.ToDouble(row.Cells[8].Text);
                //double rcc = Convert.ToDouble(row.Cells[9].Text);
                //double tc = Convert.ToDouble(row.Cells[10].Text);
                //double polFee = Convert.ToDouble(row.Cells[11].Text);
                //double vat = Convert.ToDouble(row.Cells[12].Text);
                //double nbt = Convert.ToDouble(row.Cells[13].Text);
                //double totPreVal = Convert.ToDouble(row.Cells[14].Text);
                //string cusName = row.Cells[15].Text;
                //string add1 = row.Cells[16].Text;
                //string add2 = row.Cells[17].Text;
                //string add3 = row.Cells[18].Text;
                //string add4 = row.Cells[19].Text;
                //string cusNic = row.Cells[20].Text;
                //string cusConNo = row.Cells[21].Text;
                //string refNo = row.Cells[22].Text;
                //double rn_fbr = Convert.ToDouble(row.Cells[23].Text);
                //double adminFee = Convert.ToDouble(row.Cells[24].Text);
                //string rnDate = row.Cells[25].Text;
                //string rnBy = row.Cells[26].Text;
                //string rnIp = row.Cells[27].Text;
                //string rnBrcd = row.Cells[28].Text;
                //string sunInsur = row.Cells[29].Text;

                string dptm = row.Cells[0].Text != null ? row.Cells[0].Text.Trim() : string.Empty;
                string rn_pol_type = row.Cells[1].Text != null ? row.Cells[1].Text.Trim() : string.Empty;
                string policyNo = row.Cells[2].Text != null ? row.Cells[2].Text.Trim() : string.Empty;

                int tmpYear = 0;
                int year = int.TryParse(row.Cells[3].Text, out tmpYear) ? tmpYear : 0;

                int tmpMonth = 0;
                int month = int.TryParse(row.Cells[4].Text, out tmpMonth) ? tmpMonth : 0;

                string rn_st_date = row.Cells[5].Text != null ? row.Cells[5].Text.Trim() : string.Empty;
                string rn_en_date = row.Cells[6].Text != null ? row.Cells[6].Text.Trim() : string.Empty;
                string rn_agt_cd = row.Cells[7].Text != null ? row.Cells[7].Text.Trim() : string.Empty;

                double tmpNetVal = 0;
                double netVal = double.TryParse(row.Cells[8].Text, out tmpNetVal) ? tmpNetVal : 0;

                double tmpRcc = 0;
                double rcc = double.TryParse(row.Cells[9].Text, out tmpRcc) ? tmpRcc : 0;

                double tmpTc = 0;
                double tc = double.TryParse(row.Cells[10].Text, out tmpTc) ? tmpTc : 0;

                double tmpPolFee = 0;
                double polFee = double.TryParse(row.Cells[11].Text, out tmpPolFee) ? tmpPolFee : 0;

                double tmpVat = 0;
                double vat = double.TryParse(row.Cells[12].Text, out tmpVat) ? tmpVat : 0;

                double tmpNbt = 0;
                double nbt = double.TryParse(row.Cells[13].Text, out tmpNbt) ? tmpNbt : 0;

                double tmpTotPreVal = 0;
                double totPreVal = double.TryParse(row.Cells[14].Text, out tmpTotPreVal) ? tmpTotPreVal : 0;

                string cusName = row.Cells[15].Text != null ? row.Cells[15].Text.Trim() : string.Empty;
                string add1 = row.Cells[16].Text != null ? row.Cells[16].Text.Trim() : string.Empty;
                string add2 = row.Cells[17].Text != null ? row.Cells[17].Text.Trim() : string.Empty;
                string add3 = row.Cells[18].Text != null ? row.Cells[18].Text.Trim() : string.Empty;
                string add4 = row.Cells[19].Text != null ? row.Cells[19].Text.Trim() : string.Empty;
                string cusNic = row.Cells[20].Text != null ? row.Cells[20].Text.Trim() : string.Empty;
                string cusConNo = row.Cells[21].Text != null ? row.Cells[21].Text.Trim() : string.Empty;
                string refNo = row.Cells[22].Text != null ? row.Cells[22].Text.Trim() : string.Empty;

                double tmpRnFbr = 0;
                double rn_fbr = double.TryParse(row.Cells[23].Text, out tmpRnFbr) ? tmpRnFbr : 0;

                double tmpAdminFee = 0;
                double adminFee = double.TryParse(row.Cells[24].Text, out tmpAdminFee) ? tmpAdminFee : 0;

                string rnDate = row.Cells[25].Text != null ? row.Cells[25].Text.Trim() : string.Empty;
                string rnBy = row.Cells[26].Text != null ? row.Cells[26].Text.Trim() : string.Empty;
                string rnIp = row.Cells[27].Text != null ? row.Cells[27].Text.Trim() : string.Empty;
                string rnBrcd = row.Cells[28].Text != null ? row.Cells[28].Text.Trim() : string.Empty;


                Label lblSumInsu = (Label)row.FindControl("lblSunInsu");
                string sumInsuredValue = string.Empty;
                if (lblSumInsu != null)
                {
                    sumInsuredValue = lblSumInsu.Text;

                    // Use the value
                }

                details.Add(new FireRenewalMast.FireRenewalMastClass
                {
                    RNDEPT = dptm,
                    RNPTP = rn_pol_type,
                    RNPOL = policyNo,
                    RNYR = year,
                    RNMNTH = month,
                    RNSTDT = rn_st_date,
                    //RNSTDT = DateTime.ParseExact(sDate, "yyyy-MM-dd", null).ToString("dd/MM/yyyy"),
                    RNENDT = rn_en_date,
                    RNAGCD = Convert.ToInt32(rn_agt_cd),
                    RNNET = netVal,
                    RNRCC = rcc,
                    RNTC = tc,
                    RNPOLFEE = polFee,
                    RNVAT = vat,
                    RNNBT = nbt,
                    RNTOT = totPreVal,
                    RNNAM = cusName,
                    RNADD1 = add1,
                    RNADD2 = add2,
                    RNADD3 = add3,
                    RNADD4 = add4,
                    RNNIC = cusNic,
                    RNCNT = cusConNo,
                    RNREF = refNo,
                    RNFBR = rn_fbr,
                    RN_ADMINFEE = adminFee,
                    RNDATE = rnDate,
                    RN_BY = rnBy,
                    RN_IP = rnIp,
                    RN_BRCD = rnBrcd,
                    RNSUMINSUR = Convert.ToDouble(sumInsuredValue)


                });

            }
        }

        //for live
       string saversult = _Sql.InsertIntoRenwalMaster(details, Session["UserId"].ToString(), Session["branch_code"].ToString());

        //for test
        //string link = _Sql.InsertIntoRenwalMaster(details, Session["UserId"].ToString(), Session["branch_code"].ToString());

        //if (!string.IsNullOrEmpty(link))

        if(saversult == "true")
        {
            lblAlertMessage.Text = "Send SMS to approved data list";
            lblAlertMessage.Attributes.Add("data-alert-title", "Success");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "1";
            lblAlertMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data successfully sent for approval process.', 'Success');", true);

        }
        else
        {


            lblAlertMessage.Text = "SMS not send for the approved data list";
            lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "1";
            lblAlertMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Data not sent for approval process.', 'Success');", true);


        }
    }




    //[WebMethod]
    //public static object HandleApproval(string action , string policyNo )
    //{
    //    string SQL_MASTER_TEMP = "DELETE FROM SLIC_CNOTE.RENEWAL_MASTER_TEMP WHERE RNPOL = '" + policyNo + "'";
    //    string SQL_MASTER = "";

    //    try
    //    {
    //        // Simulate approval/rejection process (Replace with actual logic)
    //        if (action == "approve")
    //        {

    //            SQL_MASTER = "INSERT INTO SLIC_CNOTE.RENEWAL_MASTER ( RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNAGCD, RNNET, RNRCC, RNTC, RNPOLFEE, RNVAT, RNNBT, RNTOT, RNNAM, RNADD1, RNADD2, RNADD3, RNADD4, RNNIC, RNCNT, RNREF, RNFBR, RN_ADMINFEE, RNDATE, RN_BY, RN_IP, RN_BRCD ) " +
    //                "SELECT RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNAGCD, RNNET, RNRCC, RNTC, RNPOLFEE, RNVAT, RNNBT, RNTOT, RNNAM, RNADD1, RNADD2, RNADD3, RNADD4, RNNIC, RNCNT, RNREF, RNFBR,   RN_ADMINFEE, RNDATE, RN_BY, RN_IP, RN_BRCDFROM SLIC_CNOTE.RENEWAL_MASTER_TEMP" +
    //                " WHERE SLIC_CNOTE.RENEWAL_MASTER_TEMP.RNPOL = 'FFPD2019200035';= ) '" + policyNo + "'";
    //        }

    //        else if (action == "reject")
    //        {

    //        }

    //        // Return a JSON response
    //        return new { success = true, message = "Policy has been  successfully." };
    //    }
    //    catch (Exception ex)
    //    {
    //        return new { success = false, message = "Error: " + ex.Message };
    //    }
    //}

    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_Details.PageIndex = e.NewPageIndex;
        // Rebind the data to refresh the GridView


        this.GetTempData();

    }
    protected void btn_renew_Click(object sender, EventArgs e)
    {

    }
    protected void btn_approve_Click(object sender, EventArgs e)
    {
        // Handle approval logic here
        Response.Write("<script>alert('Policy Approved!');</script>");
    }


    protected void btn_reject_Click(object sender, EventArgs e)
    {
        // Handle rejection logic here
        Response.Write("<script>alert('Policy Rejected!');</script>");
    }



    protected void gvPolicies_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Approve" || e.CommandName == "Reject")
        {
            string policyNumber = e.CommandArgument.ToString();
            string action = e.CommandName == "Approve" ? "approved" : "rejected";

            // Implement database logic to update approval status

            // Display confirmation message
            // ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Policy {policyNumber} has been {action}.');", true);

            // Reload policies
            LoadPolicies();
        }
    }
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        string policyNumber = hfPolicyNumber.Value;
        string action = (sender as Button).Text;

        if (!string.IsNullOrEmpty(policyNumber))
        {
            if (action == "Approve")
            {
                // Perform approval logic
                Response.Write("<script>alert('Policy " + policyNumber + " has been approved.');</script>");
            }
            else if (action == "Reject")
            {
                // Perform rejection logic
                Response.Write("<script>alert('Policy " + policyNumber + " has been rejected.');</script>");
            }
        }
    }

    public class ExcessDetail
    {
        public string ExcessId { get; set; }
        public string ExcessName { get; set; }
        public string Percentage { get; set; }
        public string Amount { get; set; }
    }

    //public void getPreClaimDetail(string policyNo)
    //{
    //    DataTable claimDetailsTbl = new DataTable();

    //    try
    //    {
    //        string bank_val, branch_val, propTerm, propType = string.Empty;

    //        string department = ddl_polType.SelectedValue.ToString();

    //        if (Session["AccessAdmin"].ToString() == "Y")
    //        {
    //            claimDetailsTbl = orcle_trans.GetRows(this._Sql.GetClaimList(policyNo), claimDetailsTbl);
    //        }
    //        else
    //        {
    //            claimDetailsTbl = orcle_trans.GetRows(this._Sql.GetClaimList(policyNo), claimDetailsTbl);
    //        }

    //        if (orcle_trans.Trans_Sucess_State == true)
    //        {

    //            if (claimDetailsTbl.Rows.Count > 0)
    //            {
    //                prevClaimDetailGrid.Visible = true;
    //                divPreClaim.Visible = true;
    //                // Store the full data in Session
    //                Session["ClaimData"] = claimDetailsTbl;

    //                prevClaimDetailGrid.DataSource = claimDetailsTbl;
    //                prevClaimDetailGrid.DataBind();
    //            }
    //            else
    //            {
    //                divPreClaim.Visible = false;
    //                prevClaimDetailGrid.DataSource = null;
    //                prevClaimDetailGrid.DataBind();

    //            }
    //        }



    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    protected void Grid_Details_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "EditRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = Grid_Details.Rows[rowIndex];

            string id = row.Cells[0].Text;
            string netPre = row.Cells[8].Text;
            string rcc = row.Cells[9].Text;
            string tr = row.Cells[10].Text;
            //string adminFeePre = row.Cells[29].Text;
            Label lblSumInsu = (Label)row.FindControl("lblSunInsu");
            string sumInsuredValue = string.Empty;
            if (lblSumInsu != null)
            {
                sumInsuredValue = lblSumInsu.Text;

                // Use the value
            }

            Label lblAdminFeePre = (Label)row.FindControl("lblAminPRe");
            string adminFeePre = string.Empty;
            if (lblAdminFeePre != null)
            {
                adminFeePre = lblAdminFeePre.Text;
            }

            txtTotPre.Text = string.Empty;
            txtNbt.Text = string.Empty;
            txtVatValue.Text = string.Empty;
            txtAdminFeeVal.Text = string.Empty;


            string policyFee = row.Cells[11].Text;
            string policyNo = row.Cells[2].Text;
            string rnYearVal = row.Cells[3].Text;
            string rnMonthVal = row.Cells[4].Text;

            //Label lblrnyear = (Label)row.FindControl("lblYear");
            //string rnYearVal = (lblrnyear.Text);

            //Label lblrnMonth = (Label)row.FindControl("lblMonth");
            //string rnMonthVal = (lblrnMonth.Text);

            txtNetPremPop.Text = netPre;
            txtRCCPop.Text = rcc;
            txtTRPop.Text = tr;
            txtPolNoPop.Text = policyNo;
            HiddenRNyearPop.Value = rnYearVal;
            HiddenRnMonthPop.Value = rnMonthVal;
            if (!string.IsNullOrEmpty(sumInsuredValue))
            {
                txtSuminsuredValPop.Text = Convert.ToDouble(sumInsuredValue).ToString("N2");
            }
            
            txtPolFee.Text = Convert.ToDouble(policyFee).ToString("N2");
            txtAdminFeePre.Text = Convert.ToDouble(adminFeePre).ToString("N2");

            //get previous claim details
            //getPreClaimDetail(policyNo);

            DataTable details = new DataTable();
            details = orcle_trans.GetRows(_Sql.GetFireRenewalRemarlData(policyNo), details);

            DataTable extraExccessDetails = new DataTable();
            extraExccessDetails = orcle_trans.GetRows(_Sql.GetFireRenewalExtraAccess(policyNo), extraExccessDetails);

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
                        string excessid = extraExccessDetails.Rows[i]["EXCESS_ID"] != DBNull.Value ? extraExccessDetails.Rows[i]["EXCESS_ID"].ToString() : string.Empty;
                        string name = extraExccessDetails.Rows[i]["EXCESS_DESCRIPTION"] != DBNull.Value ? extraExccessDetails.Rows[i]["EXCESS_DESCRIPTION"].ToString() : string.Empty;
                        string perc = extraExccessDetails.Rows[i]["EXCESS_PRECENTAGE"] != DBNull.Value ? extraExccessDetails.Rows[i]["EXCESS_PRECENTAGE"].ToString() : string.Empty;
                        string amount = extraExccessDetails.Rows[i]["EXCESS_AMOUNT"] != DBNull.Value ? extraExccessDetails.Rows[i]["EXCESS_AMOUNT"].ToString() : string.Empty;

                        if (!string.IsNullOrWhiteSpace(name) || !string.IsNullOrWhiteSpace(perc) || !string.IsNullOrWhiteSpace(amount))
                        {
                            excessList.Add(new ExcessDetail
                            {
                                ExcessId = excessid,
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

            pnlPopup.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showPopup();", true);

        }
        else if (e.CommandName == "RejectRow")
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = Grid_Details.Rows[rowIndex];

            string policyNo = row.Cells[2].Text;
            string rnYearVal = row.Cells[3].Text;
            string rnMonthVal = row.Cells[4].Text;

            txtPolNoFRej.Text = policyNo;
            HiddenRNyearPop.Value = rnYearVal;
            HiddenRnMonthPop.Value = rnMonthVal;

            RejectPanel.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Popup", "showPopup2();", true);

        }
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

    protected void btnClose_Click(object sender, EventArgs e)
    {
        string policyNo = hiddenPolicyNo.Value;
        ViewState["Excess_" + policyNo] = null;
        pnlPopup.Visible = false;
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
        double VATPercent = _Sql.Get_VAT_Precentage();


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
        txtNbt.Text = Cal_NBTTemp.ToString("N2");
        txtVatValue.Text = Cal_VATTemp.ToString("N2");
        txtAdminFeeVal.Text = Cal_ADMIN_FEETemp.ToString("N2");

        btnSave.Enabled = true;
        pnlPopup.Visible = true;
    }
    //end 

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

        double sumInsured = 0;
        if (!string.IsNullOrEmpty(txtSuminsuredValPop.Text))
        {
            sumInsured = Convert.ToDouble(txtSuminsuredValPop.Text);
        }
        else
        {
            sumInsured = 0;
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
        if (!string.IsNullOrEmpty(txtRemrPop.Text))
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
        if (!string.IsNullOrEmpty(txtNbt.Text))
        {
            nbtVal = Convert.ToDouble(txtNbt.Text);
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
            RNTOT = finalPayPRem,
            RNSUMINSUR = sumInsured

        });



        bool saversult = _Sql.UpdateIntoRenwalMasterTemp_WithCla_byAdm(details, Session["UserId"].ToString(), Session["branch_code"].ToString());

        if (saversult)
        {
            string policyNo2 = txtPolNoPop.Text.Trim();

            InserOrUpdateExtraExcess(policyNo2);

            string policyNo3 = txt_pol_no.Text.Trim();
            string department = ddl_polType.SelectedValue.ToString();
            string catgory = ddl_status.SelectedValue.ToString();
            string branch = ddl_branch.SelectedValue.ToString();
            string subDpt = ddlSubDept.SelectedValue;
            string busiUnit = ddlBusUnit.SelectedValue;

            DataTable ApprovalTemp = new DataTable();
            // ApprovalTemp = Oracle_Trans.GetRows(this._Sql.GetApprovalTempDataFireRNSMS(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), catgory, policyNo3, branch, busiUnit, subDpt), ApprovalTemp);
            ApprovalTemp = Oracle_Trans.GetRows(this._Sql.GetApprovalTempDataFireRNSMS(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), catgory, policyNo3, branch, busiUnit, subDpt), ApprovalTemp);
            if (ApprovalTemp.Rows.Count > 0)
            {
                Grid_Details.Visible = true;
                Grid_Details.DataSource = ApprovalTemp;
                Grid_Details.DataBind();

            }


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
            TextBox txtExcessId = (TextBox)item.FindControl("txtExcessId");
            TextBox txtExcessName = (TextBox)item.FindControl("txtExcessName");
            TextBox txtPercentage = (TextBox)item.FindControl("txtPercentage");
            TextBox txtAmount = (TextBox)item.FindControl("txtAmount");

            string excessIdVal = txtExcessId.Text.Trim();
            string excessName = txtExcessName.Text.Trim();
            double percentage = Convert.ToDouble(txtPercentage.Text.Trim());
            double amount = Convert.ToDouble(txtAmount.Text.Trim());

            int excessId = 0;
            if (excessIdVal != null && !string.IsNullOrEmpty(excessIdVal))
            {
                excessId = Convert.ToInt32(excessIdVal);
            }


            // Now insert into your database
            bool result = _Sql.InsertUpdateExtExcess(policyNo, excessName, percentage, amount, excessId);
        }
    }
    //end of function
    protected void btnReject_Click(object sender, EventArgs e)
    {
        string policyNo = txtPolNoFRej.Text.Trim();
        string rejectReason = txtRejReason.Text.Trim();


        int year = Convert.ToInt32(HiddenRNyearPop.Value);
        int month = Convert.ToInt32(HiddenRnMonthPop.Value);

        bool saversult = _Sql.RejectRenewalByAdmin(Session["UserId"].ToString(), Session["branch_code"].ToString(), policyNo, rejectReason, year, month);

        if (saversult)
        {
            string department = ddl_polType.SelectedValue.ToString();
            string catgory = ddl_status.SelectedValue.ToString();
            string policyNo2 = txt_pol_no.Text.Trim();
            string branch = ddl_branch.SelectedValue.ToString();
            string subDpt = ddlSubDept.SelectedValue;
            string busiUnit = ddlBusUnit.SelectedValue;

            DataTable ApprovalTemp = new DataTable();
            ApprovalTemp = Oracle_Trans.GetRows(this._Sql.GetApprovalTempDataFireRNSMS(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), catgory, policyNo2, branch, busiUnit, subDpt), ApprovalTemp);
            if (ApprovalTemp.Rows.Count > 0)
            {
                Grid_Details.Visible = true;
                Grid_Details.DataSource = ApprovalTemp;
                Grid_Details.DataBind();

            }
            else
            {
                Grid_Details.Visible = false;
                Grid_Details.DataSource = null;
                Grid_Details.DataBind();
            }


            lblAlertMessage.Text = "Selected record successfully rejected.";
            lblAlertMessage.Attributes.Add("data-alert-title", "Success");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "2";
            lblAlertMessage.Visible = true;
            // Trigger the JavaScript function manually after setting the message
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert2('Selected record successfully rejected.', 'Success');", true);

        }
        else
        {

            lblAlertMessage.Text = "Reject failed";
            lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes["data-alert-method"] = "2";
            lblAlertMessage.Visible = true;
            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert2('Reject failed', 'Success');", true);

        }
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
    protected void btnAddRow_Click2(object sender, EventArgs e)
    {
        pnlPopup.Visible = true;
        Panel2.Visible = true;
        
        //string policyNo = hiddenPolicyNo.Value;
        string policyNo = txtPolNoPop.Text;
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

    //protected void btnRemoveRow_Click2(object sender, EventArgs e)
    //{
    //    pnlPopup.Visible = true;
    //    Panel2.Visible = true;

    //    //string policyNo = hiddenPolicyNo.Value;
    //    string policyNo = txtPolNoPop.Text;
    //    string key = "Excess_" + policyNo;

    //    DataTable dt = ViewState[key] as DataTable ?? CreateExcessDataTablePopup();

    //    // Save existing data from repeater
    //    for (int i = 0; i < rptAdditionalExcessPop.Items.Count; i++)
    //    {
    //        RepeaterItem item = rptAdditionalExcessPop.Items[i];
    //        TextBox txtExcessId = (TextBox)item.FindControl("txtExcessId");
    //        TextBox txtExcessName = (TextBox)item.FindControl("txtExcessName");
    //        TextBox txtPercentage = (TextBox)item.FindControl("txtPercentage");
    //        TextBox txtAmount = (TextBox)item.FindControl("txtAmount");

    //        if (i < dt.Rows.Count)
    //        {
    //            dt.Rows[i]["ExcessId"] = txtExcessId.Text;
    //            dt.Rows[i]["ExcessName"] = txtExcessName.Text;
    //            dt.Rows[i]["Percentage"] = txtPercentage.Text;
    //            dt.Rows[i]["Amount"] = txtAmount.Text;
    //        }
    //    }

    //    // Remove last row if any
    //    if (dt.Rows.Count > 0)
    //    {
    //        dt.Rows.RemoveAt(dt.Rows.Count - 1);
    //    }

    //    ViewState[key] = dt;

    //    rptAdditionalExcessPop.DataSource = dt;
    //    rptAdditionalExcessPop.DataBind();
    //}

    protected void btnRemoveRow_Click2(object sender, EventArgs e)
    {
        pnlPopup.Visible = true;
        Panel2.Visible = true;

        string policyNo = txtPolNoPop.Text;
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

        // Delete from DB if last row has an ExcessId
        if (dt.Rows.Count > 0)
        {
            DataRow lastRow = dt.Rows[dt.Rows.Count - 1];
            string excessId = lastRow["ExcessId"].ToString();

            if (!string.IsNullOrWhiteSpace(excessId))
            {
                bool result = _Sql.DelteFromExtraExcess(txtPolNoPop.Text, Convert.ToInt32(excessId));
            }

            // Then remove the row from the table
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
        }

        ViewState[key] = dt;

        rptAdditionalExcessPop.DataSource = dt;
        rptAdditionalExcessPop.DataBind();
    }

    protected void rptAdditionalExcessPop_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string policyNo = hiddenPolicyNo.Value;
        string key = "Excess_" + policyNo;

        if (e.CommandName == "Remove")
        {
            string excessIdToRemove = e.CommandArgument.ToString();

            // Assuming you store the additional excesses in ViewState
            DataTable dt = ViewState["AdditionalExcess"] as DataTable;
            if (dt != null)
            {
                DataRow[] rows = dt.Select("ExcessId = '" + excessIdToRemove + "'");
                if (rows.Length > 0)
                {
                    dt.Rows.Remove(rows[0]);
                }

                ViewState[key] = dt;
                rptAdditionalExcessPop.DataSource = dt;
                rptAdditionalExcessPop.DataBind();
            }
        }
    }



}