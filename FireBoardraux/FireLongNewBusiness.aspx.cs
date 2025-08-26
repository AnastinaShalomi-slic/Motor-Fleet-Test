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
using System.Linq;

public partial class FireBoardraux_FireLongNewBusiness : System.Web.UI.Page
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
                        this.IniGridview();
                        //this.GetDetails(txt_start_date.Text.Trim(), txt_to_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), "N", txtNicNo.Text.Trim());
                 
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


    protected object[] Request_()
    {
        object[] ExecParameter = new object[8];

        ExecParameter[0] = "F"; /*Get Department*/
        ExecParameter[1] = "F";/*Get Sub Department*/
        ExecParameter[2] = txt_start_date.Text.Trim();/*Get Date From*/
        ExecParameter[3] = txt_to_date.Text.Trim(); /*Get Date To*/
        ExecParameter[4] = "";
        ExecParameter[5] = 0;
        ExecParameter[6] = "SYS-NEBU";
        ExecParameter[7] = "BORDEREAUX_FIRE_LONG_TERM"; /*PROC NAME*/

        return ExecParameter;
    }

    protected void GetDetails()
    {
        DataTable details = new DataTable();
        try
        {

            details = orcle_trans.GetSpRows(this.Request_(), details);

            
            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {

                    Grid_Details.DataSource = details;
                    Grid_Details.DataBind();
                    ImageButton8.Visible = true;
                    lblTotR.Visible = true;
                    lblTotR.InnerHtml = "No. of records found : " + details.Rows.Count.ToString();

                    decimal total = details.AsEnumerable().Sum(row => row.Field<decimal>("TOTAL_PREMIUM"));
                    Grid_Details.FooterRow.Cells[26].ForeColor = System.Drawing.Color.Green;
                    Grid_Details.FooterRow.Cells[26].Font.Size = 12;
                    Grid_Details.FooterRow.Cells[26].Text = "Total";


                    Grid_Details.FooterRow.Cells[27].ForeColor = System.Drawing.Color.Green;
                    Grid_Details.FooterRow.Cells[27].Font.Size = 11;
                    Grid_Details.FooterRow.Cells[27].Text = total.ToString("N2");

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
        this.GetDetails();
    }
   
    protected void ClearText()
    {
        this.IniGridview();
        ddl_status.SelectedValue = "SYS-NEBU";
        txt_to_date.Text = string.Empty;
        txt_start_date.Text = string.Empty;
        ImageButton8.Visible = false;
        lblTotR.Visible = false;
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


        this.GetDetails();
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

    private void ExportGridToExcel()
    {
        //try
        //{
        DataTable dt = new DataTable();
        dt = orcle_trans.GetSpRows(this.Request_(), dt);

        //-------------------------------------------------------------


        using (XLWorkbook wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add("FPL_Boardraux-" + DateTime.Now.ToString("yyyy-MM"));

            ws.Column(2).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(3).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(4).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(5).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(7).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(8).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(9).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(10).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(11).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(12).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(13).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(14).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Column(15).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(16).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(17).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(18).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(19).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(20).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(21).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(22).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(23).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(24).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(25).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(26).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(27).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;


            ws.Column(28).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(29).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(30).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(31).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(32).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(33).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(34).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(35).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(36).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(37).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(38).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(39).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            ws.Column(40).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(41).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(42).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(43).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(44).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(45).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(46).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(47).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(48).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Column(49).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;



            ws.Range("B2:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Range("B2:F6").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#6790BA"));
            ws.Range("B3:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Range("B4:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Range("B5:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            ws.Range("B6:F3").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;

            //--------main titles---------------------------------------------------------------->>>
         
            ws.Cell(7, 1).WorksheetRow().Height = 20;

            ws.Range("B7:T7").Row(1).Merge();
            ws.Range("B7:T7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#D6DBE2"));

            //ws.Range("O7:P7").Row(1).Merge();
            //ws.Range("O7:P7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#FFFF"));

            ws.Range("U7:V7").Row(1).Merge();
            ws.Range("U7:V7").Row(1).Merge().Value = "ORIGINAL POLICY PERIOD";
            ws.Range("U7:V7").Row(1).Merge().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Range("U7:V7").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range("U7:V7").Row(1).Merge().Style.Font.Bold = true;
            ws.Range("U7:V7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#79d279"));

            ws.Range("W7:X7").Row(1).Merge();
            ws.Range("W7:X7").Row(1).Merge().Value = "ENDORSEMENT";
            ws.Range("W7:X7").Row(1).Merge().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Range("W7:X7").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range("W7:X7").Row(1).Merge().Style.Font.Bold = true;
            ws.Range("W7:X7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#79a6d2"));

            ws.Range("Y7:AH7").Row(1).Merge();
            //ws.Range("Y7:AH7").Row(1).Merge().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            //ws.Range("Y7:AH7").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //ws.Range("Y7:AH7").Row(1).Merge().Style.Font.Bold = true;
            ws.Range("Y7:AH7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#D6DBE2"));



            ws.Range("AI7:AM7").Row(1).Merge();
            ws.Range("AI7:AM7").Row(1).Merge().Value = "ORIGINAL SPREAD";
            ws.Range("AI7:AM7").Row(1).Merge().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Range("AI7:AM7").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range("AI7:AM7").Row(1).Merge().Style.Font.Bold = true;
            ws.Range("AI7:AM7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#ffbf80"));

           

            ws.Range("AN7:AQ7").Row(1).Merge();
            ws.Range("AN7:AQ7").Row(1).Merge().Value = "AFTER THE EXTENTION";
            ws.Range("AN7:AQ7").Row(1).Merge().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Range("AN7:AQ7").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range("AN7:AQ7").Row(1).Merge().Style.Font.Bold = true;
            ws.Range("AN7:AQ7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#79d2a6"));


            ws.Range("AR7:AU7").Row(1).Merge();
            ws.Range("AR7:AU7").Row(1).Merge().Value = "PREMIUM ALOCATION";
            ws.Range("AR7:AU7").Row(1).Merge().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Range("AR7:AU7").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range("AR7:AU7").Row(1).Merge().Style.Font.Bold = true;
            ws.Range("AR7:AU7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#d2a679"));

            //ws.Range("AX7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#ffbf80"));


            ws.Range("AV7:AW7").Row(1).Merge();
            ws.Range("AV7:AW7").Row(1).Merge().Value = "PROFIT / COST CENTER";
            ws.Range("AV7:AW7").Row(1).Merge().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            ws.Range("AV7:AW7").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            ws.Range("AV7:AW7").Row(1).Merge().Style.Font.Bold = true;
            ws.Range("AV7:AW7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#85e0e0"));


            //ws.Range("AV7:AW7").Row(1).Merge();
            //ws.Range("AV7:AW7").Row(1).Merge().Value = "FOR ADDITIONAL REFERENCES";
            //ws.Range("AV7:AW7").Row(1).Merge().Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            //ws.Range("AV7:AW7").Row(1).Merge().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            //ws.Range("AV7:AW7").Row(1).Merge().Style.Font.Bold = true;
            //ws.Range("AV7:AW7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#ffd480"));

            ws.Range("AX7").Style.Fill.SetBackgroundColor(XLColor.FromHtml("#D6DBE2"));
            //----------------------------------------------------------------------------------->>>

            ws.Cell(2, 2).Value = "Fire Policy Long Term Boardraux";
            ws.Cell(2, 2).Style.Font.Bold = true;

            ws.Cell(3, 2).Value = "Report Description : Fire Policy Long Term Boardraux.";
            ws.Cell(4, 2).Value = "Date Of Genarate : " + System.DateTime.Now;
            ws.Cell(5, 2).Value = "Genarate By  : " + Session["UsrName"].ToString().Trim() + " - " + Session["EPFNum"].ToString();
            string fromdate, toDate = string.Empty;

            if (txt_start_date.Text.ToString().Trim() == null) { fromdate = ""; }
            else { fromdate = txt_start_date.Text.ToString().Trim(); }

            if (txt_to_date.Text.ToString().Trim() == "") { toDate = ""; }
            else { toDate = txt_to_date.Text.ToString().Trim(); }

            ws.Cell(6, 2).Value = "Period : " + fromdate + " - " + toDate;

            int RowCount = dt.Rows.Count;
            int ColumnCount = dt.Columns.Count;

            string[] ColumnHead = { "SEQ",
                        "MONTH - CEEDED MONTH",
                        "UNDERWRITING YEAR",
                        "POLICY NUMBER",
                        "INITIAL POL. COMME. MONTH",
                        "NO. OF POLICY YEARS",
                        "RENEWAL DUE YEAR(2ND,3RD, ETC)",
                        "POL. PERIOD - CURRENT PERIOD",
                        "RECEIPT NUMBER",
                        "RECEPT DATE",
                        "FUNCTION NAME",
                        "FUNCTION NUMBER",
                        "METERIAL NUMBER",
                        "DESCRIPTION",
                        "METERIAL GROUP",
                        "LOB NUMBER",
                        "LOB NAME",
                        "NAME OF INSURED",
                        "LOCATION",
                        "FROM",
                        "TO",
                        "FROM",
                        "TO",
                        "MD (LKR)",
                        "TOTAL SUM INSURED",
                        "ORIGINAL/COMBINED RATE",
                        "TOTAL RATE",
                        "TOTAL PREMIUM",
                        "ADMIN FEE(FOR RESP.YEAR)",
                        "POLICY / RENEWAL FEE(FOR RESP.YEAR)",
                        "PREMIUM TO_SRCC",
                        "PREMIUM TO TR",
                        "PREMIUM (EX. SRCC & TR)",
                        "NET RATE (%)",
                        "NET RETENTED SI",
                        "FIRE TREATY A",
                        "FIRE TREATY B",
                        "FACULTATIVE SI",
                        "NET RETENTED SI",
                        "FIRE TREATY A",
                        "FIRE TREATY B",
                        "FACULTATIVE SI",
                        "NET RETENTION",
                        "FIRE TREATY A",
                        "FIRE TREATY B",
                        "FACULTATIVE",
                        "PROFIT CENTER",
                        "COST CENTER",
                        "REMARK"};

            int[] ColumnSize = { 5, 20, 25, 15, 25, 15, 25, 20, 20, 25,
                                    20, 15, 15, 20, 20, 15, 15, 60, 75, 15,
                                    15, 20, 20, 25, 15, 10, 20, 20 ,20, 20,
                                    10, 15, 15, 15, 15, 15, 15, 15, 15, 15,
                                    15, 15, 15, 15, 15, 15, 25, 25,25};

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

                ws.Cell(9 + rows, 3).Value = dt.Rows[rows]["BMONTH"];
                ws.Cell(9 + rows, 3).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 4).Value = dt.Rows[rows]["BYEAR"];
                ws.Cell(9 + rows, 4).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 5).Value = dt.Rows[rows]["SC_POLICY_NO"];
                ws.Cell(9 + rows, 5).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 6).Value = dt.Rows[rows]["INMONTH"];
                ws.Cell(9 + rows, 6).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 7).Value = dt.Rows[rows]["NOOFPOLICYYEARS"];
                ws.Cell(9 + rows, 7).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 8).Value = dt.Rows[rows]["RENEWAL_DUE"];
                ws.Cell(9 + rows, 8).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 9).Value = dt.Rows[rows]["CURPERIOD"];
                ws.Cell(9 + rows, 9).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 10).Value = dt.Rows[rows]["RECEIPT_NUMBER"];
                ws.Cell(9 + rows, 10).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 11).Style.NumberFormat.Format = "dd/MM/yyyy";
                ws.Cell(9 + rows, 11).Value = dt.Rows[rows]["RECEPT_DATE"];
                ws.Cell(9 + rows, 11).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 12).Value = dt.Rows[rows]["FUNCTIONAL_NAME"];
                ws.Cell(9 + rows, 12).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 13).Value = dt.Rows[rows]["FUNCTIONAL_NUMBER"];
                ws.Cell(9 + rows, 13).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 14).Value = dt.Rows[rows]["METERIAL_NUMBER"];
                ws.Cell(9 + rows, 14).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 15).Value = dt.Rows[rows]["DESCRIPTION"];
                ws.Cell(9 + rows, 15).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 16).Value = dt.Rows[rows]["METERIAL_GROUP"];
                ws.Cell(9 + rows, 16).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 17).Value = dt.Rows[rows]["LOB_NUMBER"];
                ws.Cell(9 + rows, 17).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 18).Value = dt.Rows[rows]["LOB_NAME"];
                ws.Cell(9 + rows, 18).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 19).Value = dt.Rows[rows]["NAME_OF_INSURED"];
                ws.Cell(9 + rows, 19).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 20).Value = dt.Rows[rows]["LOCATION"];
                ws.Cell(9 + rows, 20).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 21).Style.NumberFormat.Format = "dd/MM/yyyy";
                ws.Cell(9 + rows, 21).Value = dt.Rows[rows]["POL_FROM"];
                ws.Cell(9 + rows, 21).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 22).Style.NumberFormat.Format = "dd/MM/yyyy";
                ws.Cell(9 + rows, 22).Value = dt.Rows[rows]["POL_TO"];
                ws.Cell(9 + rows, 22).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 23).Style.NumberFormat.Format = "dd/MM/yyyy";
                ws.Cell(9 + rows, 23).Value = dt.Rows[rows]["END_FROM"];
                ws.Cell(9 + rows, 23).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 24).Style.NumberFormat.Format = "dd/MM/yyyy";
                ws.Cell(9 + rows, 24).Value = dt.Rows[rows]["END_TO"];
                ws.Cell(9 + rows, 24).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 25).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 25).Value = dt.Rows[rows]["MD_SUM_INSURED"];
                ws.Cell(9 + rows, 25).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 26).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 26).Value = dt.Rows[rows]["TOTAL_SUM_INSURED"];
                ws.Cell(9 + rows, 26).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 27).Value = dt.Rows[rows]["ORIGINAL_COMINED_RATE"];
                ws.Cell(9 + rows, 27).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 28).Value = dt.Rows[rows]["TOTAL_RATE"];
                ws.Cell(9 + rows, 28).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 29).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 29).Value = dt.Rows[rows]["TOTAL_PREMIUM"];
                ws.Cell(9 + rows, 29).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 30).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 30).Value = dt.Rows[rows]["ADMIN_FEE"];
                ws.Cell(9 + rows, 30).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 31).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 31).Value = dt.Rows[rows]["POL_OR_RENEWAL"];
                ws.Cell(9 + rows, 31).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 32).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 32).Value = dt.Rows[rows]["RCC_FUND"];
                ws.Cell(9 + rows, 32).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 33).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 33).Value = dt.Rows[rows]["TR_FUND"];
                ws.Cell(9 + rows, 33).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 34).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 34).Value = dt.Rows[rows]["PREMIUM_EXTRSRCC"];
                ws.Cell(9 + rows, 34).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                //ws.Cell(9 + rows, 35).Style.NumberFormat.Format = "0.000%";
                ws.Cell(9 + rows, 35).Value = dt.Rows[rows]["NET_RATE"];
                ws.Cell(9 + rows, 35).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 36).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 36).Value = dt.Rows[rows]["NET_RETAINTED_SI"];
                ws.Cell(9 + rows, 36).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 37).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 37).Value = dt.Rows[rows]["FIRE_TREATY_A"];
                ws.Cell(9 + rows, 37).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 38).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 38).Value = dt.Rows[rows]["FIRE_TREATY_B"];
                ws.Cell(9 + rows, 38).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 39).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 39).Value = dt.Rows[rows]["FACULTATIVE_SI"];
                ws.Cell(9 + rows, 39).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 40).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 40).Value = dt.Rows[rows]["EXT_NET_RETAINTED_SI"];
                ws.Cell(9 + rows, 40).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 41).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 41).Value = dt.Rows[rows]["EXT_FIRE_TREATY_A"];
                ws.Cell(9 + rows, 41).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 42).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 42).Value = dt.Rows[rows]["EXT_FIRE_TREATY_B"];
                ws.Cell(9 + rows, 42).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 43).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 43).Value = dt.Rows[rows]["EXT_FACULTATIVE_SI"];
                ws.Cell(9 + rows, 43).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 44).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 44).Value = dt.Rows[rows]["PRE_NET_RETAINTION"];
                ws.Cell(9 + rows, 44).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 45).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 45).Value = dt.Rows[rows]["PRE_FIRE_TREATY_A"];
                ws.Cell(9 + rows, 45).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 46).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 46).Value = dt.Rows[rows]["PRE_FIRE_TREATY_B"];
                ws.Cell(9 + rows, 46).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 47).Style.NumberFormat.Format = "#,##0.00";
                ws.Cell(9 + rows, 47).Value = dt.Rows[rows]["PRE_FACULTATIVE"];
                ws.Cell(9 + rows, 47).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 48).Value = dt.Rows[rows]["PROFIT_CENTER"];
                ws.Cell(9 + rows, 48).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 49).Value = dt.Rows[rows]["COST_CENTER"];
                ws.Cell(9 + rows, 49).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                ws.Cell(9 + rows, 50).Value = dt.Rows[rows]["REMARK"];
                ws.Cell(9 + rows, 50).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;

                if (rows == (RowCount - 1))
                {
                    ws.Range("B" + (9 + rows).ToString() + ":AX" + ColumnCount.ToString()).Row(1).Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    ws.Range("B" + (9 + (rows + 1)).ToString() + ":AX" + ColumnCount.ToString()).Row(1).Style.Fill.SetBackgroundColor(XLColor.FromHtml("#b3d7ff"));
                }

            }


            //ws.Column(2).Width = 20;
            //for (int x = 3; x <= 36; x++)
            //{
            //    ws.Column(x).AdjustToContents();

            //    if (x == 3 || x == 5 || x == 7 || x == 8 || x == 11 || x == 13)
            //    {
            //        ws.Column(x).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            //    }

            //    else
            //    {
            //        ws.Column(x).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Left;
            //    }
            //}


            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=FirePolicyBoardrauxLongTerm.xlsx");

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

    
    
    protected void CleareDropDownList(DropDownList ddl)
    {
        var firstitem_1 = ddl.Items[0];
        ddl.Items.Clear();
        ddl.Items.Add(firstitem_1);
    }

   
   
}