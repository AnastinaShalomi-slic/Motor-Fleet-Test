using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Drawing;
using System.Text.RegularExpressions;

public partial class OdProtect_BackOffice_BusinessStatistics_Info : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        if (!Page.IsPostBack)
        {
            try
            {
                UserId.Value = Session["UserId"].ToString();
                brCode.Value = Session["brcode"].ToString();
                this.Initalization();
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


    protected void Initalization()
    {
        Execute_sql _sql = new Execute_sql();
        this.IniGridview();
        this.SetInitialDates();
        this.InitializedListBank(ddl_bank, "bbnam", "bcode", _sql.GetOdpBankCodes(0), "'bbnam'");
        this.GetDetails();
    }

    protected void SetInitialDates()
    {
        txt_start_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txt_to_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }

    protected void GetDetails()
    {
        ODP_Transaction oDP_Transaction = new ODP_Transaction();
        Execute_sql _sql = new Execute_sql();
        DataTable resultset = new DataTable();

        try
        {
            resultset = oDP_Transaction.GetRows(_sql.GetOdpPerformanceMoreInfo(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim(), int.Parse(ddl_bank.SelectedValue.Trim()), int.Parse(ddl_branch.SelectedValue.Trim())), resultset, "Overdraft Policy Statistics");

            if (oDP_Transaction.Trans_Sucess_State == true)
            {
                if (resultset.Rows.Count > 0)
                {
                    Grid_Details.DataSource = resultset;
                    Grid_Details.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Message', 'Record Not Found.', 'info');", true);
                    Grid_Details.DataSource = null;
                    Grid_Details.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Loading Overdraft Policy.', 'error');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Exceptio', 'In Loading Overdraft Policy.', 'error');", true);
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (METH- GetDetails(..) -> - [USER : " + UserId.Value + "] - [BRANCH : " + brCode.Value + "]> ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }

    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Grid_Details.PageIndex = e.NewPageIndex;
        //bindgridview will get the data source and bind it again
        this.GetDetails();
    }

    

    protected void IniGridview()
    {
        Grid_Details.DataSource = null;
        Grid_Details.DataBind();
        
    }



    protected void ClearText()
    {
        this.SetInitialDates();
        this.IniGridview();
        ddl_bank.SelectedIndex = 0;
        ddl_branch.SelectedIndex = 0;
        ddl_status.SelectedIndex = 0;
        Grid_Details.DataSource = null;   
        Grid_Details.DataBind();
       
    }


    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }

    protected void btn_find_Click1(object sender, EventArgs e)
    {
        this.GetDetails();     
    }

    protected void btexcel_Click(object sender, EventArgs e)
    {
        if (Grid_Details.Rows.Count > 0)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=OverdraftPolicyReport.xls");

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                Grid_Details.AllowPaging = false;
                this.GetDetails();
                //--end-->

                string wz = "Overdraft Policy " + ddl_status.SelectedItem.ToString() + " Report<br/>" +
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
        ODP_Transaction oDP_Transaction = new ODP_Transaction();
        Execute_sql _sql = new Execute_sql();

        DataTable resultset = new DataTable();

        string bank_val, branch_val = string.Empty;

        bank_val = ddl_bank.SelectedValue.ToString().Trim();
        branch_val = ddl_branch.SelectedValue.ToString().Trim();

        resultset = oDP_Transaction.GetRows(_sql.GetOdpIncome(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), ddl_status.SelectedValue.Trim(), int.Parse(ddl_bank.SelectedValue.Trim()), int.Parse(ddl_branch.SelectedValue.Trim())), resultset, "Overdraft Policies Statistics");
        //-------------------------------------------------------------

        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add("Overdraft_Policy_Details-" + DateTime.Now.ToString("yyyy-MM"));

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


            ws.Cell(2, 2).Value = "Overdraft Policy " + ddl_status.SelectedItem.ToString() + " Report";
            ws.Cell(2, 2).Style.Font.Bold = true;

            ws.Cell(3, 2).Value = "Date From  : " + txt_start_date.Text.ToString().ToUpper() + " To : " + txt_to_date.Text.ToString().ToUpper();

            ws.Cell(4, 2).Value = "Genarate By  : " + Session["userName_code"].ToString().ToUpper();
            ws.Cell(5, 2).Value = "Date Of Genarate : " + System.DateTime.Now;

            int RowCount = resultset.Rows.Count;
            int ColumnCount = resultset.Columns.Count;


            string[] ColumnHead = { "NO", "Bank", "Branch", "Count", "Entered Date", "Total Sum Insu." };

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


                ws.Cell(9 + rows, 3).Value = resultset.Rows[rows]["bbnam"];
                ws.Cell(9 + rows, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 4).Value = resultset.Rows[rows]["bbrnch"];
                ws.Cell(9 + rows, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 5).Value = resultset.Rows[rows]["total_count"];
                ws.Cell(9 + rows, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 6).Style.DateFormat.Format = "dd/MM/yyyy";
                ws.Cell(9 + rows, 6).Value = DateTime.ParseExact(resultset.Rows[rows]["created_date"].ToString(), "dd/MM/yyyy", null);
                ws.Cell(9 + rows, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                // ws.Cell(9 + rows, 6).Value = dt.Rows[rows]["Entered Date"];

                ws.Cell(9 + rows, 7).Value = resultset.Rows[rows]["sum_insu"];
                ws.Cell(9 + rows, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
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
            Response.AddHeader("content-disposition", "attachment;filename=OverdraftPolicyReport.xlsx");

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
        ODP_Transaction oDP_Transaction = new ODP_Transaction();
        Execute_sql _sql = new Execute_sql();
        DataTable result_set = new DataTable();
        try
        {

            result_set = oDP_Transaction.GetRows(executor, result_set, "Bank Initalization");

            if (oDP_Transaction.Trans_Sucess_State == true)
            {
                if (result_set.Rows.Count > 1)
                {
                    target_list.DataSource = result_set;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                    target_list.SelectedValue = bank_code.Value.ToString().Trim();
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Bank Initialization Fail.', 'error');", true);
                    target_list.DataSource = null;
                    target_list.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Bank Initialization Fail.[ORCL]', 'error');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Exception', 'In Bank Initialization.', 'error');", true);
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (METH- InitializedListBank(..) -> - [BANK : " + bank_code.Value + "] - [BRANC : " + branch_code.Value + "]- [BRANC : " + userName_code.Value.Trim() + "]. > ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }


    protected void InitializedListBranch(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {
        Oracle_Transaction orcle_trans = new Oracle_Transaction();
        Execute_sql _sql = new Execute_sql();
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
                    target_list.SelectedValue = branch_code.Value.ToString();
                }

                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Bank Branch Initialization Fail.', 'error');", true);
                    target_list.DataSource = null;
                    target_list.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Bank Branch Initialization Fail.[ORCL]', 'error');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Exception', 'In Bank Branch Initialization.', 'error');", true);
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (METH- InitializedListBranch(..) -> - [BANK : " + bank_code.Value + "] - [BRANC : " + branch_code.Value + "]- [BRANC : " + userName_code.Value.Trim() + "]. > ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
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
        Execute_sql _sql = new Execute_sql();
        this.CleareDropDownList(ddl_branch);
        this.InitializedListBranch(ddl_branch, "bbrnch", "bbcode", _sql.GetBranch(ddl_bank.SelectedValue.ToString()), "'bbrnch'");
    }
}