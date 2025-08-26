using System;
using System.Collections.Generic;
//using System.Linq;
//using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Data.OracleClient;
using System.Configuration;
using ClosedXML.Excel;
using System.IO;
using System.Drawing;

public partial class Bank_ViewQuotations : System.Web.UI.Page
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



                    if (Session["bank_code"].ToString() != "")
                    {

                        UserId.Value = Session["UserId"].ToString();
                        brCode.Value = Session["brcode"].ToString();
                        this.IniGridview();
                        this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), null);

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
            /* IF PAGE.POSTBACK ASSIGHEN SESSION VARIABLE */
            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
        }

        if (Grid_Details.Rows.Count == 0)
        {
            // ImageButton8.Visible = false;
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
                cmd.CommandText = "select REQ_ID from QUOTATION.BANK_REQ_ENTRY_DETAILS where REQ_ID like '%" + prefix.ToUpper() + "%' AND ROWNUM <= 10 order by ENTERED_ON desc";
                cmd.Connection = conn;
                conn.Open();
                using (OracleDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        req_no.Add(string.Format("{0}", sdr["REQ_ID"]));
                    }
                }
                conn.Close();
            }
            return req_no.ToArray();
        }
    }




    protected void GetDetails(string start_date, string end_date, string req_id, string app_status)
    {

        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetReqDetails(start_date, end_date, req_id, app_status, Session["bank_code"].ToString(), Session["branch_code"].ToString(), Session["userName_code"].ToString()), details);

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
        this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim());
    }
    protected void IniGridview()
    {
        Grid_Details.DataSource = null;
        Grid_Details.DataBind();
    }


    protected void SelectRecord_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btndetails = sender as ImageButton;

            string[] arg = new string[3];
            arg = btndetails.CommandArgument.ToString().Split(';');
            string REQ_ID = arg[0].ToString().Trim();
            string FLAG = arg[1].ToString().Trim();
            string BankCode = arg[2].ToString().Trim();
            int intAuthCode = 0;

            string authCode = intAuthCode.ToString();
            var en = new EncryptDecrypt(); //imgbtn_select
            Response.Redirect("~/Bank/ProcessedQuotationsaspx.aspx?REQ_ID=" + en.Encrypt(REQ_ID.Trim()) + "&V_FLAG=" + en.Encrypt(FLAG.Trim()) + "&Bank_code=" + en.Encrypt(BankCode.Trim()), false);

        }

        catch (Exception ex)
        {

            string msg = "Message : File not found.!";
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("3".ToString()));


        }
    }

    private void link(string ref_no_temp, string authCode)
    {
        Response.Redirect("~/MotorQuotation/MotorQuotationView.aspx?Ref_No=" + ref_no_temp + "&Auth=" + authCode);
    }


    protected void ClearText()
    {
        this.IniGridview();
        ddl_status.SelectedIndex = 0;
        txt_end_date.Text = string.Empty;
        txt_req_id.Text = string.Empty;
        txt_start_date.Text = string.Empty;


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
        this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim());
    }


    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        if (Grid_Details.Rows.Count > 0)
        {
            ExportGridToExcel();
            string msg = "Successfully Created.!";
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("1".ToString()));
        }
        else
        {
            string msg = "Message : Cannot be downloaded because no record found.";
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("1".ToString()));

        }
    }

    private void ExportGridToExcel()
    {
        try
        {
            DataTable dt = new DataTable();
            // Gridview Records to Datatable
            // 01. Grid Column to Table
            for (int i = 1; i < Grid_Details.Columns.Count; i++)
            {
                dt.Columns.Add(Grid_Details.Columns[i].ToString());
            }

            // 02. Grid Rows to Table
            foreach (GridViewRow row in Grid_Details.Rows)
            {
                DataRow dr = dt.NewRow();
                for (int j = 1; j < Grid_Details.Columns.Count; j++)
                {
                    dr[Grid_Details.Columns[j].ToString()] = row.Cells[j].Text;
                }

                dt.Rows.Add(dr);
            }

            //-------------------------------------------------------------

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Quotations-" + DateTime.Now.ToString("yyyy-MM"));

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


                ws.Cell(2, 2).Value = "Motor Insurance Quotation - Comprehensive (Private Use)";
                ws.Cell(2, 2).Style.Font.Bold = true;

                //ws.Cell(3, 2).Value = "Report Description : Quotation Reports.";
                ws.Cell(4, 2).Value = "Date Of Genarate : " + System.DateTime.Now;
                ws.Cell(5, 2).Value = "Genarate By  : " + UserId.Value.ToString();

                int RowCount = dt.Rows.Count;
                int ColumnCount = dt.Columns.Count;

                string[] ColumnHead = { "NO", "BRANCH", "REFRENCE NO", "VEHICLE NO", "ENTERED DATE", "PREMIUM" };

                int[] ColumnSize = { 5, 25, 20, 25, 25, 25 };

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

                    ws.Cell(9 + rows, 3).Value = dt.Rows[rows]["Branch"];
                    ws.Cell(9 + rows, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 4).Value = dt.Rows[rows]["Refrence No"];
                    ws.Cell(9 + rows, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 5).Value = dt.Rows[rows]["Vehicle No"];
                    ws.Cell(9 + rows, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    ws.Cell(9 + rows, 6).Style.DateFormat.Format = "dd/MM/yyyy";
                    ws.Cell(9 + rows, 6).Value = DateTime.ParseExact(dt.Rows[rows]["Entered Date"].ToString(), "dd/MM/yyyy", null);
                    ws.Cell(9 + rows, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 7).Value = dt.Rows[rows]["Premium"];
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
                Response.AddHeader("content-disposition", "attachment;filename=Motor_Insurance_Quotation_Comprehensive(Private_Use).xlsx");

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
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
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
            string op_ststus = e.Row.Cells[10].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (op_ststus.Equals("Pending"))
                {
                    //cell.BackColor = Color.FromName("#F4F996"); //
                    e.Row.Cells[10].BackColor = Color.FromName("#EEE579");
                }

                else if (op_ststus.Equals("Completed"))
                {
                    //cell.BackColor = Color.FromName("#cffae2"); //#cffae2
                    e.Row.Cells[10].BackColor = Color.FromName("#79EE9C");
                }

                else if (op_ststus.Equals("Reject"))
                {
                    //cell.BackColor = Color.FromName("#ffcccc"); //#cffae2
                    e.Row.Cells[10].BackColor = Color.FromName("#EE7979");

                }

                else if (op_ststus.Equals("Need more Info."))
                {
                    //cell.BackColor = Color.FromName("#ffcccc"); //#cffae2
                    e.Row.Cells[10].BackColor = Color.FromName("#add8e6");

                }
                else if (op_ststus.Equals("Cancel"))
                {
                    //cell.BackColor = Color.FromName("#ffcccc"); //#cffae2
                    e.Row.Cells[10].BackColor = Color.FromName("#D5D8DC ");

                }
                else if (op_ststus.Equals("Forward to Risk Managment"))
                {
                    //cell.BackColor = Color.FromName("#ffcccc"); //#cffae2
                    e.Row.Cells[10].BackColor = Color.FromName("#D5D8DC ");

                }
                else if (op_ststus.Equals("Forward to Reinsurance"))
                {
                    //cell.BackColor = Color.FromName("#ffcccc"); //#cffae2
                    e.Row.Cells[10].BackColor = Color.FromName("#D5D8DC ");

                }
            }
        }


    }


}