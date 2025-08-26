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

public partial class SLIC_Fire_ApprovedPolicy : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    string Auth = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.ImageButton8);
        // ((MainMaster)Master).slected_manu.Value = "appReq";

        if (!Page.IsPostBack)
        {
            try
            {
                Auth = "admin";

                if (Auth == "admin")
                {


                    if (Session["UserId"].ToString() != "")
                    {

                        UserId.Value = Session["UserId"].ToString();
                        brCode.Value = Session["brcode"].ToString();
                        //this.IniGridview();
                       // this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), "N", txtNicNo.Text.Trim());
                        // this.InitializedListBox(ddl_branch, "BRNAM", "BRCOD", this._sql.GetBranch(Int32.Parse(brCode.Value)), "'Branch'");
                        //this.InitializedListEPF(ddl_epf, "approver_epf", "approver_epf", this._sql.GetEPF(), "'EPF'");
                        //this.GetDetails((Int32.Parse(brCode.Value)), null, null, Int32.Parse(ddl_branch.SelectedValue), null, ddl_status.SelectedValue.ToString().ToUpper().Trim(), ddl_epf.SelectedValue.ToString().Trim());
                        //txt_ref_no.Focus();
                        this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");

                        //ddl_status.Attributes.Add("readonly", "readonly");
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
                //Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(ex.Message.ToString()));

            }

            // Grid_Details.RowDataBound += new GridViewRowEventHandler(OnRowDataBound);
        }
        else
        {
            /* IF PAGE.POSTBACK ASSIGHEN SESSION VARIABLE */
            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
        }

        //if (Grid_Details.Rows.Count == 0)
        //{
        //    ImageButton8.Visible = false;
        //}


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




    protected void GetDetails(string start_date, string end_date, string req_id, string app_status, string nIC)
    {
        DataTable details = new DataTable();
        try
        {
            string bank_val, branch_val, propTerm, propType = string.Empty;
            //if (ddl_bank.SelectedItem.ToString().Trim() == "-- Select --") { bank_val = "0"; } else { bank_val = ddl_bank.SelectedItem.ToString().Trim(); }

            //if (ddl_branch.SelectedItem.ToString().Trim() == "-- Select --") { branch_val = "0"; } else { branch_val = ddl_branch.SelectedItem.ToString().Trim(); }

            bank_val = ddl_bank.SelectedValue.ToString().Trim();
            branch_val = ddl_branch.SelectedValue.ToString().Trim();
            propTerm = ddlTerm.SelectedValue.ToString().Trim();
            propType = ddlPropType.SelectedValue.ToString().Trim();

            details = orcle_trans.GetRows(this._sql.GetFireDetails(start_date, end_date, null, req_id, null, null, app_status, bank_val, branch_val, null, nIC, propTerm, propType), details);

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


    protected void IniGridview()
    {
        Grid_Details.DataSource = null;
        Grid_Details.DataBind();
    }

    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        Grid_Details.PageIndex = e.NewPageIndex;
        //bindgridview will get the data source and bind it again
        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(), txtNicNo.Text.Trim());
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
        ddl_status.SelectedValue = "N";
        txt_to_date.Text = string.Empty;
        txtNicNo.Text = string.Empty;
        txt_req_id.Text = string.Empty;
        txt_start_date.Text = string.Empty;
        this.CleareDropDownList(ddl_bank);
        this.CleareDropDownList(ddl_branch);
        ddlTerm.SelectedIndex = 0;
        ddlPropType.SelectedIndex = 0;
        this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
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


        this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(), txtNicNo.Text.Trim());
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
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("1".ToString()));

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
            //Response.AddHeader("content-disposition", "attachment;filename=FirePolicyCompleted Date From  : " + txt_start_date.Text + "  To : " + txt_to_date.Text.Trim() + ".xls");
            Response.AddHeader("content-disposition", "attachment;filename=FirePolicyCompleted.xls");

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                Grid_Details.AllowPaging = false;
                this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), ddl_status.SelectedValue.ToString().ToUpper().Trim(), txtNicNo.Text.Trim());
                //--end-->


                string wz = "Fire Policy Completed Report<br/>" +
                "Report Description: PDH Fire Policy Completed Report. (" + ddlTerm.SelectedItem.ToString() + "/" + ddlPropType.SelectedItem.ToString() + ")<br/>" +
                "Date From: " + txt_start_date.Text.ToString().ToUpper() + " To: " + txt_to_date.Text.ToString().ToUpper() + "<br/>" +
                "Genarate By  : " + Session["UsrName"].ToString().Trim() + " - " + Session["EPFNum"].ToString() + "<br/>" +
                "Date of Genarate : " + System.DateTime.Now;

                hw.Write("<div> <h3 align=leftss><span style=");
                hw.Write(HtmlTextWriter.DoubleQuoteChar);
                hw.Write("font-weight:bold; font-family:'Segoe UI'; color: #81040a;");
                hw.Write(HtmlTextWriter.DoubleQuoteChar);
                hw.Write(">" + wz + "</span> </h3></div>");
                hw.WriteLine();

                Grid_Details.HeaderRow.BackColor = Color.White;
                Grid_Details.Columns[15].Visible = false;
                Grid_Details.Columns[9].Visible = false;

                foreach (TableCell cell in Grid_Details.HeaderRow.Cells)
                {
                    if (cell.Text.Equals("Schedule"))
                    {
                        cell.BackColor = Grid_Details.HeaderStyle.BackColor=Color.AliceBlue;
                        cell.CssClass = "hideColumn";
                        cell.Visible = false;

                    }
                    else
                    { 
                    cell.BackColor = Grid_Details.HeaderStyle.BackColor;
                    cell.CssClass = "bhead";
                    }
                    
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
                    border:1px solid #fff;}
                    .hideColumn{background:#5afc03;visbility: hidden; display: none; width :0px;}
                    </style>";

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
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("1".ToString()));

        }


    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
           server control at run time. */
    }
    private void ExportGridToExcel()
    {
        //try
        //{
            DataTable dt = new DataTable();
           
            string bank_val, branch_val, propTerm, propType = string.Empty;
            
            bank_val = ddl_bank.SelectedValue.ToString().Trim();
            branch_val = ddl_branch.SelectedValue.ToString().Trim();
            propTerm = ddlTerm.SelectedValue.ToString().Trim();
            propType = ddlPropType.SelectedValue.ToString().Trim();

            dt = orcle_trans.GetRows(this._sql.GetFireDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), null, txt_req_id.Text.Trim().ToUpper(), null, null, ddl_status.SelectedValue.ToString().ToUpper().Trim(), bank_val, branch_val, null, txtNicNo.Text.Trim(), propTerm, propType), dt);

            //// Gridview Records to Datatable
            //// 01. Grid Column to Table
            //for (int i = 1; i < Grid_Details.Columns.Count; i++)
            //{
            //    dt.Columns.Add(Grid_Details.Columns[i].ToString());
            //}

            //// 02. Grid Rows to Table
            //foreach (GridViewRow row in Grid_Details.Rows)
            //{
            //    DataRow dr = dt.NewRow();
            //    for (int j = 1; j < Grid_Details.Columns.Count; j++)
            //    {
            //        dr[Grid_Details.Columns[j].ToString()] = row.Cells[j].Text;
            //    }

            //    dt.Rows.Add(dr);
            //}

            //-------------------------------------------------------------

            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Fire_Policy-" + DateTime.Now.ToString("yyyy-MM"));

                ws.Column(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                ws.Column(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Column(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Column(5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Column(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Column(7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Column(8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

                ws.Range("B2:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range("B2:F6").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#6790BA"));
                ws.Range("B3:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range("B4:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range("B5:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
                ws.Range("B6:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;


                ws.Cell(2, 2).Value = "Fire Policy Completed Report";
                ws.Cell(2, 2).Style.Font.Bold = true;

                ws.Cell(3, 2).Value = "Report Description : PDH Fire Policy Completed Report.";
                ws.Cell(4, 2).Value = "Date Of Genarate : " + System.DateTime.Now;
                ws.Cell(5, 2).Value = "Genarate By  : " + Session["UsrName"].ToString().Trim()+" - "+Session["EPFNum"].ToString();
                string fromdate, toDate = string.Empty;

            if (txt_start_date.Text.ToString().Trim() == null) { fromdate = ""; }
            else { fromdate = txt_start_date.Text.ToString().Trim(); }

            if (txt_to_date.Text.ToString().Trim() == "") { toDate = ""; }
            else { toDate = txt_to_date.Text.ToString().Trim(); }

            ws.Cell(6, 2).Value = "Period : " + fromdate + " - " + toDate;

            int RowCount = dt.Rows.Count;
                int ColumnCount = dt.Columns.Count;

                string[] ColumnHead = { "NO", "Ref. No", "Policy No","Bank", "Branch", "Cus. Name", "Cus. Phone", "Sum Insured", "Total Premium","Cus. NIC", "Property Type","Years","Term" };

                int[] ColumnSize = { 5, 25, 20, 25, 25, 25,25,25,25,25,25,25,25 };

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

                    ws.Cell(9 + rows, 3).Value = dt.Rows[rows]["DH_REFNO"];
                    ws.Cell(9 + rows, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 4).Value = dt.Rows[rows]["SC_POLICY_NO"];
                    ws.Cell(9 + rows, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    ws.Cell(9 + rows, 5).Value = dt.Rows[rows]["DH_BCODE"];
                    ws.Cell(9 + rows, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    //ws.Cell(9 + rows, 6).Style.DateFormat.Format = "dd/MM/yyyy";
                    //ws.Cell(9 + rows, 6).Value = DateTime.ParseExact(dt.Rows[rows]["Entered Date"].ToString(), "dd/MM/yyyy", null);
                    //ws.Cell(9 + rows, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    //ws.Cell(9 + rows, 6).Value = dt.Rows[rows]["Entered Date"];


                    ws.Cell(9 + rows, 6).Value = dt.Rows[rows]["DH_BBRCODE"];
                    ws.Cell(9 + rows, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    ws.Cell(9 + rows, 7).Value = dt.Rows[rows]["DH_NAME"];
                    ws.Cell(9 + rows, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    ws.Cell(9 + rows, 8).Value = dt.Rows[rows]["DH_PHONE"];
                    ws.Cell(9 + rows, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    ws.Cell(9 + rows, 9).Value = dt.Rows[rows]["DH_VALU_TOTAL"];
                    ws.Cell(9 + rows, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                    ws.Cell(9 + rows, 10).Value = dt.Rows[rows]["SC_TOTAL_PAY"];
                    ws.Cell(9 + rows, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //10012022_

                ws.Cell(9 + rows, 11).Value = dt.Rows[rows]["DH_NIC"];
                ws.Cell(9 + rows, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 12).Value = dt.Rows[rows]["PROP_TYPE"];
                ws.Cell(9 + rows, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 13).Value = dt.Rows[rows]["Period"];
                ws.Cell(9 + rows, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 14).Value = dt.Rows[rows]["TERM"];
                ws.Cell(9 + rows, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

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
                Response.AddHeader("content-disposition", "attachment;filename=FirePolicyCompleted.xlsx");

                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(Response.OutputStream);

                }


                Response.Flush();
                Response.End();
            }
        //}

        //catch (Exception ex)
        //{
        //    var endc = new EncryptDecrypt();
        //    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()),false);
        //}
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
                    // ddl_make.Items.Insert(0, new ListItem("--All--", "0"));
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
                    //ddl_model.Items.Insert(0, new ListItem("--All--", "0"));
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
        // ddl_model.SelectedIndex = 0;
        //this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), "");
        this.InitializedListBranch(ddl_branch, "bbrnch", "bbcode", this._sql.GetBranch(ddl_bank.SelectedValue.ToString()), "'bbrnch'");
    }
    
}
