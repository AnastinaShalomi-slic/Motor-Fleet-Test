using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Data.OracleClient;
using System.Configuration;
using ClosedXML.Excel;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

public partial class AdminPanel_AdminViewMotor : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    string Auth = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);


        if (!Page.IsPostBack)
        {
            try
            {
                Auth = "admin";//Session["Auth_Code"].ToString();

                if (Auth == "admin")
                {

                    string bank = Session["bank_code"].ToString();


                    UserId.Value = Session["UserId"].ToString();
                    brCode.Value = Session["brcode"].ToString();
                    this.IniGridview();

                    if (!string.IsNullOrEmpty(bank))
                    {

                        if ((Session["userName_code"].ToString().Substring(4) == "admin"))
                        {
                            this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
                            ddl_bank.SelectedValue = Session["bank_code"].ToString();
                            this.InitializedListBranch(ddl_branch, "bbrnch", "bbcode", this._sql.GetBranch(ddl_bank.SelectedValue.ToString()), "'bbrnch'");

                            ddl_bank.Attributes.Add("disabled", "disabled");
                            this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());
                            this.GetDetailsTotal(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());

                        }
                    }
                    else
                    {
                        this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
                        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());
                        this.GetDetailsTotal(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());
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
            /* IF PAGE.POSTBACK ASSIGHEN SESSION VARIABLE */
            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
        }

    }


    [WebMethod]
    public static string[] GET_REQ_ID(string prefix)
    {
        List<string> req_no = new List<string>();
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["CONN_STRING_ORCL"].ConnectionString;
            using (OracleCommand cmd = new OracleCommand())
            {
                cmd.CommandText = " select sc_policy_no from QUOTATION.FIRE_DH_SCHEDULE_CALC where sc_policy_no like '%" + prefix.ToUpper() + "%' AND sc_policy_no IS NOT NULL AND ROWNUM <= 10 order by created_on desc";
                cmd.Connection = conn;
                conn.Open();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        req_no.Add(string.Format("{0}", sdr["sc_policy_no"]));
                    }
                }
                conn.Close();
            }
            return req_no.ToArray();
        }
    }


    protected void GetDetails(string start_date, string end_date, string app_status)
    {
        DataTable details = new DataTable();
        try
        {
            string bank_val, branch_val = string.Empty;

            bank_val = ddl_bank.SelectedValue.ToString().Trim();
            branch_val = ddl_branch.SelectedValue.ToString().Trim();


            details = orcle_trans.GetRows(this._sql.GetMotorTotalSumVal(start_date, end_date, app_status, bank_val, branch_val, null), details);



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

    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Grid_Details.PageIndex = e.NewPageIndex;
        //bindgridview will get the data source and bind it again

        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.ToString().ToUpper().Trim());
    }
    protected void GetDetailsTotal(string start_date, string end_date, string app_status)
    {
        DataTable details = new DataTable();
        try
        {
            string bank_val, branch_val = string.Empty;

            bank_val = ddl_bank.SelectedValue.ToString().Trim();
            branch_val = ddl_branch.SelectedValue.ToString().Trim();


            details = orcle_trans.GetRows(this._sql.GetMotorTotalCategoryCounts(start_date, end_date, app_status, bank_val, branch_val, null), details);



            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    GridViewTotalCount.DataSource = details;
                    GridViewTotalCount.DataBind();
                }
                else
                {
                    GridViewTotalCount.DataSource = null;
                    GridViewTotalCount.DataBind();

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

    protected void IniGridview()
    {
        Grid_Details.DataSource = null;
        Grid_Details.DataBind();
        GridViewTotalCount.DataSource = null;
        GridViewTotalCount.DataBind();
    }


    protected void SelectRecord_Click(object sender, EventArgs e)
    {
        ImageButton btndetails = sender as ImageButton;

        string[] arg = new string[3];
        arg = btndetails.CommandArgument.ToString().Split(';');

        string REF_NO = arg[0].ToString().Trim();
        string FLAG = arg[1].ToString().Trim();
        string SUM_INSU = arg[2].ToString().Trim();


        int intAuthCode = 1;
        string authCode = intAuthCode.ToString();
        var en = new EncryptDecrypt();

        Response.Redirect("LoadProposalView.aspx?REQ_ID=" + en.Encrypt(REF_NO.Trim()) + "&V_FLAG=" + en.Encrypt(FLAG.Trim()) + "&SUM_INSU=" + en.Encrypt(SUM_INSU.Trim()), false);

    }




    protected void ClearText()
    {
        this.IniGridview();
        ddl_bank.SelectedIndex = 0;
        ddl_branch.SelectedIndex = 0;
        ddl_status.SelectedIndex = 0;
        txt_to_date.Text = string.Empty;
        txt_start_date.Text = string.Empty;

        if (Session["userName_code"].ToString().Substring(4).Trim() == "admin")
        {
            this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
            ddl_bank.SelectedValue = Session["bank_code"].ToString();
            this.InitializedListBranch(ddl_branch, "bbrnch", "bbcode", this._sql.GetBranch(ddl_bank.SelectedValue.ToString()), "'bbrnch'");
            ddl_bank.Attributes.Add("disabled", "disabled");
            this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());
            this.GetDetailsTotal(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());

        }
        else
        {
            this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
            this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());
            this.GetDetailsTotal(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());
        }
    }


    public enum PopupMessageType
    {
        Error,
        Message,
        Warning,
        Success
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }
    protected void btn_find_Click1(object sender, EventArgs e)
    {


        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());
        this.GetDetailsTotal(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());
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
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("7".ToString()));

        }
    }


    protected void btexcel_Click(object sender, EventArgs e)
    {

        if (Grid_Details.Rows.Count > 0)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=MotorPolicyReport.xls");

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                Grid_Details.AllowPaging = false;
                this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString());
                //--end-->


                string wz = "Motor Policy " + ddl_status.SelectedItem.ToString() + " Report<br/>" +
                "Date From: " + txt_start_date.Text.ToString().ToUpper() + " To: " + txt_to_date.Text.ToString().ToUpper() + "<br/>" +
                "Genarate By  : " + Session["userName_code"].ToString().ToUpper() + "<br/>" +
                "Date of Genarate : " + System.DateTime.Now;

                hw.Write("<div> <h3 align=center><span style=");
                hw.Write(HtmlTextWriter.DoubleQuoteChar);
                hw.Write("font-weight:bold; font-family:'Segoe UI'; color: #81040a;");
                hw.Write(HtmlTextWriter.DoubleQuoteChar);
                hw.Write(">" + wz + "</span> </h3></div>");
                hw.WriteLine();

                Grid_Details.HeaderRow.BackColor = Color.White;

                foreach (TableCell cell in Grid_Details.HeaderRow.Cells)
                {
                    cell.BackColor = Grid_Details.HeaderStyle.BackColor;
                    cell.CssClass = "bhead";
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
                    border:1px solid #fff;}</style>";

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
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("7".ToString()));

        }


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }

    private void ExportGridToExcel()
    {

        DataTable dt = new DataTable();

        string bank_val, branch_val = string.Empty;

        bank_val = ddl_bank.SelectedValue.ToString().Trim();
        branch_val = ddl_branch.SelectedValue.ToString().Trim();


        dt = orcle_trans.GetRows(this._sql.GetMotorTotalSumVal(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim().ToUpper().ToString(), bank_val, branch_val, null), dt);


        //-------------------------------------------------------------

        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add("Motor_Policy_Details-" + DateTime.Now.ToString("yyyy-MM"));

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


            ws.Cell(2, 2).Value = "Motor Policy " + ddl_status.SelectedItem.ToString() + " Report";
            ws.Cell(2, 2).Style.Font.Bold = true;

            ws.Cell(3, 2).Value = "Date From  : " + txt_start_date.Text.ToString().ToUpper() + " To : " + txt_to_date.Text.ToString().ToUpper();

            ws.Cell(4, 2).Value = "Genarate By  : " + Session["userName_code"].ToString().ToUpper();
            ws.Cell(5, 2).Value = "Date Of Genarate : " + System.DateTime.Now;

            int RowCount = dt.Rows.Count;
            int ColumnCount = dt.Columns.Count;


            string[] ColumnHead = { "NO", "Bank", "Branch", "Count", "Entered Date", "Total Sum Insu.", "Purpose", "Vehicle Category" };

            int[] ColumnSize = { 5, 25, 25, 25, 25, 25, 25, 25, 25 };

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


                ws.Cell(9 + rows, 3).Value = dt.Rows[rows]["bbnam"];
                ws.Cell(9 + rows, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 4).Value = dt.Rows[rows]["bbrnch"];
                ws.Cell(9 + rows, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 5).Value = dt.Rows[rows]["total_count"];
                ws.Cell(9 + rows, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 6).Style.DateFormat.Format = "dd/MM/yyyy";
                ws.Cell(9 + rows, 6).Value = DateTime.ParseExact(dt.Rows[rows]["created_date"].ToString(), "dd/MM/yyyy", null);
                ws.Cell(9 + rows, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                // ws.Cell(9 + rows, 6).Value = dt.Rows[rows]["Entered Date"];

                ws.Cell(9 + rows, 7).Value = dt.Rows[rows]["sum_insu"];
                ws.Cell(9 + rows, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;



                ws.Cell(9 + rows, 8).Value = ReplaceSpace(dt.Rows[rows]["PURPOSE"].ToString());
                ws.Cell(9 + rows, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                ws.Cell(9 + rows, 9).Value = ReplaceSpace(dt.Rows[rows]["V_TYPE"].ToString());
                ws.Cell(9 + rows, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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
            Response.AddHeader("content-disposition", "attachment;filename=MotorPolicyReport.xlsx");

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);

            }


            Response.Flush();
            Response.End();
        }

    }
    protected string ReplaceSpace(string txt)
    {

        string inputString = txt;
        Regex re = new Regex("&nbsp;");
        string outputString = re.Replace(inputString, "");
        return outputString;

    }
    protected void OnDataBound(object sender, EventArgs e)
    {
        for (int i = Grid_Details.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = Grid_Details.Rows[i];
            GridViewRow previousRow = Grid_Details.Rows[i - 1];

            if (row.Cells[1].Text == previousRow.Cells[1].Text)
            {
                if (previousRow.Cells[1].RowSpan == 0)
                {
                    if (row.Cells[1].RowSpan == 0)
                    {
                        previousRow.Cells[1].RowSpan += 2;
                    }
                    else
                    {
                        previousRow.Cells[1].RowSpan = row.Cells[2].RowSpan + 1;
                    }
                    row.Cells[1].Visible = false;
                }
            }
        }
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string op_ststus = e.Row.Cells[11].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (op_ststus.Equals("Pending"))
                {
                    //cell.BackColor = Color.FromName("#F4F996"); //
                    e.Row.Cells[11].BackColor = Color.FromName("#F4F996");
                }

                else if (op_ststus.Equals("Completed"))
                {
                    //cell.BackColor = Color.FromName("#cffae2"); //#cffae2
                    e.Row.Cells[11].BackColor = Color.FromName("#cffae2");
                }

                else if (op_ststus.Equals("Rejected"))
                {
                    //cell.BackColor = Color.FromName("#ffcccc"); //#cffae2
                    e.Row.Cells[11].BackColor = Color.FromName("#ffcccc");

                }
            }
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

        this.InitializedListBranch(ddl_branch, "bbrnch", "bbcode", this._sql.GetBranch(ddl_bank.SelectedValue.ToString()), "'bbrnch'");
    }


}