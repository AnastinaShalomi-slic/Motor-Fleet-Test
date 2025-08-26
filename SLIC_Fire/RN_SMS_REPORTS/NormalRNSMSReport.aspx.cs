using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SLIC_Fire_RN_SMS_REPORTS_NormalRNSMSReport : System.Web.UI.Page
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
        btn_download.Visible = false;

        if (!Page.IsPostBack)
        {
            try
            {
                //Auth = Session["AccessAdmin"].ToString();
                Auth = "admin";

                if (Auth == "admin")
                {
                    string ppwReport = string.Empty;
                    ppwReport = Request.QueryString["PPW"].Trim().ToString();

                    if (ppwReport == "flse")
                    {

                        Grid_Details.Columns[23].Visible = false; 
                        lblSms.Visible = true;
                        lblPPWRpt.Visible = false;
                        reportCat.Visible = true;
                        reportType.Visible = true;
                        reportType.Visible = true;
                    }
                    else if (ppwReport == "true")
                    {
                        Grid_Details.Columns[23].Visible = true;
                        lblSms.Visible = false;
                        lblPPWRpt.Visible = true;
                        reportCat.Visible = false;
                        reportType.Visible = false;
                        reportType.Visible = false;
                    }

                    if (Session["UserId"].ToString() != "")
                    {

                        UserId.Value = Session["UserId"].ToString();
                        brCode.Value = Session["brcode"].ToString();

                        //this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
                        this.InitializedListBranch(ddl_branch, "BRNAM", "BRCOD", this._sql.GetBranch(Convert.ToInt32(brCode.Value)), "'BRCOD'");

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
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.IniGridview();
        ddl_RepType.SelectedValue = "N";
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
        string ppwReport = string.Empty;
        ppwReport = Request.QueryString["PPW"].Trim().ToString();

        if (ppwReport == "true")
        {
            this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), "", "PPW_Canceled", ddl_branch.SelectedValue.ToString());
        }
        else
        {
            this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_RepType.SelectedValue.ToString().Trim(), ddl_category.SelectedValue.ToString().Trim(), ddl_branch.SelectedValue.ToString());
        }

    }

    protected void GetDetails(string start_date, string end_date, string reportType, string category, string branch)
    {

        DataTable sentSmsList = new DataTable();
        try
        {
            string bank_val, branch_val, propTerm, propType = string.Empty;

            if(Session["AccessAdmin"].ToString() == "Y")
            {
                sentSmsList = orcle_trans.GetRows(this._sql.GetFireRenewalSMSReports(start_date, end_date, reportType, category, branch, ""), sentSmsList);
            }
            else
            {
                sentSmsList = orcle_trans.GetRows(this._sql.GetFireRenewalSMSReports(start_date, end_date, reportType, category, Session["brcode"].ToString(), ""), sentSmsList);
            }

            

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (sentSmsList.Rows.Count > 0)
                {
                    Grid_Details.Visible = true;

                    // Store the full data in Session
                    Session["FullData"] = sentSmsList;

                    Grid_Details.DataSource = sentSmsList;
                    Grid_Details.DataBind();
                    btn_download.Visible = true;

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

        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_category.SelectedValue.ToString().ToUpper().Trim(), ddl_category.SelectedValue.ToString().Trim(), ddl_branch.SelectedValue.ToString());
        //btn_PremCal_Click1(null, null);
    }

    protected void Grid_Details_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string smsStatusVal = "";
            // Assuming you have a column `is_locked` in the data source.
            string smsStatus = e.Row.Cells[12].Text.ToString();


            //download button
            // Get the SMS status

            // Find the Download button
            Button btnDownload = (Button)e.Row.FindControl("btnDownload");
            Button btnEdit = (Button)e.Row.FindControl("btnEdit");

            // Enable button only if status is 'A'
            if (btnDownload != null)
            {
                btnDownload.Enabled = (smsStatus == "A");
            }

            // Enable button only if status is 'A'
            if (btnEdit != null)
            {
                btnEdit.Enabled = (smsStatus == "N");
            }


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
        if (e.CommandName == "DownloadNotice")
        {
            string policyNo = e.CommandArgument.ToString();

            // Call your method to generate/download the renewal notice
            DownloadRenewalNotice(policyNo);
        }
    }

    private void DownloadRenewalNotice(string policyNo)
    {
        // Your logic to generate or fetch the PDF and send to client
        // Example:
        // Response.ContentType = "application/pdf";
        // Response.AppendHeader("Content-Disposition", "attachment; filename=RenewalNotice.pdf");
        // Response.BinaryWrite(pdfBytes);
        // Response.End();

        FireRnNote_pdf pdfPrint = new FireRnNote_pdf();;

        pdfPrint.print_policy(policyNo);
    }


    protected void ddlTerm_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_download.Visible = false;
        Grid_Details.Visible = false;

        Grid_Details.DataSource = null;
        Grid_Details.DataBind();


    }

    protected void ddlCate_SelectedIndexChanged(object sender, EventArgs e)
    {
        btn_download.Visible = false;
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

    protected void btn_download_Click1(object sender, EventArgs e)
    {
        if (Grid_Details.Rows.Count > 0)
        {
            bool dowVal = ExportGridToExcel();

            if (dowVal)
            {
                lblAlertMessage.Text = "Report doenloaded Successfull";
                lblAlertMessage.Attributes.Add("data-alert-title", "Success");
                lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
                lblAlertMessage.Visible = true;
                // Trigger the JavaScript function manually after setting the message
                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('Report doenloaded Successfull', 'Success');", true);

                // Clear alert message immediately after showing it
                lblAlertMessage.Text = "";
                lblAlertMessage.Visible = false;

            }
            else
            {
                lblAlertMessage.Text = "Report not downloaded.";
                lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
                lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
                lblAlertMessage.Attributes.Add("data-alert-type", "Alert");
                lblAlertMessage.Visible = true;

                ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('" + lblAlertMessage.Text + "', 'Alert');", true);

                // Clear alert message immediately after showing it
                lblAlertMessage.Text = "";
                lblAlertMessage.Visible = false;
            }

        }
        else
        {
            lblAlertMessage.Text = "Cannot be downloaded because no record found.";
            lblAlertMessage.Attributes.Add("data-alert-title", "Alert");
            lblAlertMessage.Attributes.Add("data-alert-message", lblAlertMessage.Text);
            lblAlertMessage.Attributes.Add("data-alert-type", "Alert");
            lblAlertMessage.Visible = true;

            ScriptManager.RegisterStartupScript(this, GetType(), "showAlert", "custom_alert1('" + lblAlertMessage.Text + "', 'Alert');", true);

            // Clear alert message immediately after showing it
            lblAlertMessage.Text = "";
            lblAlertMessage.Visible = false;

        }
    }

    private bool ExportGridToExcel()
    {
        bool downSta = true;
        try
        {
            DataTable details = new DataTable();
            DataTable dt = new DataTable();

            string ppwReport = string.Empty;
            ppwReport = Request.QueryString["PPW"].Trim().ToString();

            if (ppwReport == "true")
            {
                if (Session["AccessAdmin"].ToString() == "Y")
                {
                    details = orcle_trans.GetRows(this._sql.GetFireRenewalSMSReports(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), "", "PPW_Canceled", ddl_branch.SelectedValue.ToString(), ""), details);

                }
                else
                {
                    details = orcle_trans.GetRows(this._sql.GetFireRenewalSMSReports(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), "", "PPW_Canceled", Session["brcode"].ToString(), ""), details);
                    //details = orcle_trans.GetRows(this._sql.GetFireRenewalSMSReports(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), "", "PPW_Canceled", ddl_branch.SelectedValue.ToString()), details);
                    //details = orcle_trans.GetRows(this._sql.GetAllMotDisReq(fromDate.Value.ToString(), toDate.Value.ToString(), ddlStatus.SelectedValue.ToString(), ddlBranch.SelectedValue.ToString(), refNo.Value.ToString(), Session["UserId"].ToString(), txtVehNo.Value.ToString()), details);

                }
            }
            else
            {
                if (Session["AccessAdmin"].ToString() == "Y")
                {
                    details = orcle_trans.GetRows(this._sql.GetFireRenewalSMSReports(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_RepType.SelectedValue.ToString().Trim(), ddl_category.SelectedValue.ToString().Trim(), ddl_branch.SelectedValue.ToString(), ""), details);

                }
                else
                {
                    details = orcle_trans.GetRows(this._sql.GetFireRenewalSMSReports(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_RepType.SelectedValue.ToString().Trim(), ddl_category.SelectedValue.ToString().Trim(), Session["brcode"].ToString(), ""), details);
                    //details = orcle_trans.GetRows(this._sql.GetFireRenewalSMSReports(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_RepType.SelectedValue.ToString().Trim(), ddl_category.SelectedValue.ToString().Trim(), ddl_branch.SelectedValue.ToString()), details);
                    //details = orcle_trans.GetRows(this._sql.GetAllMotDisReq(fromDate.Value.ToString(), toDate.Value.ToString(), ddlStatus.SelectedValue.ToString(), ddlBranch.SelectedValue.ToString(), refNo.Value.ToString(), Session["UserId"].ToString(), txtVehNo.Value.ToString()), details);

                }
            }




            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Fire Renewal SMS List -" + DateTime.Now.ToString("yyyy-MM"));

                ws.Column(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Column(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Column(5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Column(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Column(7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                //ws.Column(8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.Range("B2:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range("B2:F6").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#6790BA"));
                ws.Range("B3:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range("B4:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range("B5:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                //ws.Range("B6:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;


                ws.Cell(2, 2).Value = "Fire Renewal SMS List Report";
                ws.Cell(2, 2).Style.Font.Bold = true;

                //ws.Cell(3, 2).Value = "Report Description : Quotation Reports.";
                ws.Cell(4, 2).Value = "Date Of Genarate : " + System.DateTime.Now;
                ws.Cell(5, 2).Value = "Genarate By  : " + Session["UsrName"].ToString().Trim() + " - " + Session["EPFNum"].ToString();

                int RowCount = details.Rows.Count;
                int ColumnCount = details.Columns.Count;

                string[] ColumnHead = { "NO", "Policy No.", "Pol. End Date", "Pol. Branch", "Cus. Name", "Cus. Mobile No", "Sum Insured", "Net Pre.", "RCC", "TC", "Admin Fee","Tot Premium", "SMS Status" };

                int[] ColumnSize = { 5, 25, 20, 25, 25, 25, 25, 25, 25, 25, 25, 25 , 25};

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

                    ws.Cell(9 + rows, 3).Value = details.Rows[rows]["POLICY_NO"];
                    ws.Cell(9 + rows, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 4).Value = details.Rows[rows]["EXPIRE_DATE"];
                    ws.Cell(9 + rows, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 5).Value = details.Rows[rows]["BRANCH_NAME"];
                    ws.Cell(9 + rows, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;


                    //ws.Cell(9 + rows, 6).Style.DateFormat.Format = "dd/MM/yyyy";
                    //string date;
                    //DateTime requestDate;

                    //// Try to parse the date from the cell text
                    //if (DateTime.TryParse(details.Rows[rows]["request_date"].ToString(), out requestDate))
                    //{
                    //    // Format the date to MM/dd/yyyy and update the cell text
                    //    details.Rows[rows]["request_date"] = requestDate.ToString("yyyy/MM/dd hh:mm tt");
                    //}
                    ws.Cell(9 + rows, 6).Value = details.Rows[rows]["CUSTOMER_NAME"].ToString();
                    ws.Cell(9 + rows, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    //string date2;
                    //DateTime responseDate;
                    //// Try to parse the date from the cell text
                    //if (DateTime.TryParse(details.Rows[rows]["APPROVED_DATE"].ToString(), out responseDate))
                    //{
                    //    // Format the date to MM/dd/yyyy and update the cell text
                    //    details.Rows[rows]["APPROVED_DATE"] = responseDate.ToString("yyyy/MM/dd hh:mm tt");
                    //}
                    ws.Cell(9 + rows, 7).Value = details.Rows[rows]["customerPhoNo"].ToString();
                    ws.Cell(9 + rows, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    ws.Cell(9 + rows, 8).Value = details.Rows[rows]["SUM_INSURED_L"];
                    ws.Cell(9 + rows, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    ws.Cell(9 + rows, 9).Value = details.Rows[rows]["BASIC_PREMIUM"];
                    ws.Cell(9 + rows, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 10).Value = details.Rows[rows]["RCC"];
                    ws.Cell(9 + rows, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 11).Value = details.Rows[rows]["TC"];
                    ws.Cell(9 + rows, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 12).Value = details.Rows[rows]["adminfee"];
                    ws.Cell(9 + rows, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 13).Value = details.Rows[rows]["TOT_PREMIUM"];
                    ws.Cell(9 + rows, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    string status = details.Rows[rows]["RN_SMS_STATUS"].ToString();

                    string rowValu = string.Empty;
                    if (status == "A")
                    {
                        rowValu = "SMS Sent";
                    }
                    else if (status == "I")
                    {
                        rowValu = "Intermediate Status";
                    }
                    else if (status == "N")
                    {
                        rowValu = "SMS Not Sent";
                    }


                    ws.Cell(9 + rows, 14).Value = rowValu;
                    ws.Cell(9 + rows, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                }

                //Export the Excel file.
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;filename=FireRenewalSMSReport.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);

                }



                Response.Flush();
                Response.End();


            }
        }

        catch (Exception ex)
        {
            downSta = false;
            //var endc = new EncryptDecrypt();
            //Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }

        return downSta;
    }

    protected void btn_Edit_Click(object sender, EventArgs e)
    {
        Grid_Details.Visible = false;
        renewGridViewSection.Visible = false;
        Panel1.Visible = true;

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

        Label lblAdminFeePre = (Label)row.FindControl("lblAminPRe");
        string adminFeePre = string.Empty;
        if (lblAdminFeePre != null)
        {
            adminFeePre = lblAdminFeePre.Text;
        }

        //for get policy start date and end date, sum isured 
        ora_side.GetPayFileDetails(policyNo, out sDate, out enDate, out add1, out add2, out add3, out add4);

        string sumInsured = GetSafeCellText(row.Cells[6]);
        //string sumInsured2 = GetSafeCellText(row.Cells[7]);
        //string sumInsured3 = GetSafeCellText(row.Cells[8]);
        string totalPremium = string.Empty;
        string netPremium = GetSafeCellText(row.Cells[7]);
        string rcc = GetSafeCellText(row.Cells[8]);
        string tr = GetSafeCellText(row.Cells[9]);
        string adminfee = GetSafeCellText(row.Cells[10]);
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
        //txtAdminFee.Text = adminfee;
        txtAdminFee.Text = adminFeePre;
        txtPolicyFee1.Text = "100.00";
        txtNbt.Text = nbt;
        txtVat.Text = vat;
        txtAgency.Text = agency;
        rnYear.Value = rnYearVal;
        rnMonth.Value = rnMonthVal;

        //get claim details
        //getPreClaimDetail(policyNo);

        if (!string.IsNullOrWhiteSpace(sumInsured))
            txtSumInsured1.Text = Convert.ToDouble(sumInsured).ToString("N2");

        //if (!string.IsNullOrWhiteSpace(sumInsured2))
        //    txtSumInsured2.Text = Convert.ToDouble(sumInsured2).ToString("N2");

        //if (!string.IsNullOrWhiteSpace(sumInsured3))
        //    txtSumInsured3.Text = Convert.ToDouble(sumInsured3).ToString("N2");

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

    private string GetSafeCellText(TableCell cell)
    {
        if (cell == null || string.IsNullOrEmpty(cell.Text) || cell.Text.Trim() == "" || cell.Text.Trim() == "&nbsp;")
        {
            return "";
        }
        return cell.Text.Trim();
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
        if (dt.Rows.Count > 0)
        {
            dt.Rows.RemoveAt(dt.Rows.Count - 1);
        }

        ViewState[key] = dt;

        rptAdditionalExcess.DataSource = dt;
        rptAdditionalExcess.DataBind();
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

    private DataTable CreateExcessDataTable()
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("ExcessId");
        dt.Columns.Add("ExcessName");
        dt.Columns.Add("Percentage");
        dt.Columns.Add("Amount");
        return dt;
    }

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
        txtadminFeeVal2.Value = Cal_ADMIN_FEETemp.ToString("N2");
        txtAdminFee2.Text = (Cal_ADMIN_FEETemp + Cal_NBTTemp).ToString("N2");


        Panel1.Visible = true;
        btnSendToApp.Visible = true;
    }

    protected void btnClose_Click2(object sender, EventArgs e)
    {
        string policyNo = HiddenFieldSelectedPolicy.Value;
        ViewState["Excess_" + policyNo] = null;

        Panel1.Visible = false;
        renewGridViewSection.Visible = true;
    }

    protected void btn_sentToApp_Click(object sender, EventArgs e)
    {

        List<FireRenewalMast.FireRenewalMastClass> details = new List<FireRenewalMast.FireRenewalMastClass>();


        string policyNo = txtPolicyNo.Text.Trim();
        string rndept = "F";
        string rnType = "FD_RENEWAL";
        string eDate = txtPolicyEndDate.Text.Trim();

        string oneYearBackString = txtPolicyStartDate.Text.Trim();
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



        double netPremium = Convert.ToDouble(txtNetPremium.Text);  // BASIC_PREMIUM
        double srcc = Convert.ToDouble(txtRcc.Text);        // RCC
        double tc = Convert.ToDouble(txtTr.Text);          // TC

        //double adminfee = Convert.ToDouble(txtAdminFee2.Text);
        double adminfee = Convert.ToDouble(txtadminFeeVal2.Value);

        double nbtVal = Convert.ToDouble(txtNbt.Text);
        double vatVal = Convert.ToDouble(txtVat.Text);


        string cusName = txtCustomerName.Text;

        if (cusName.Contains("&#160;"))
        {
            cusName = cusName.Replace("&#160;", " ");
        }

        if (cusName.Contains("&amp;"))
        {
            cusName = cusName.Replace("&amp;", "");
        }


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

}