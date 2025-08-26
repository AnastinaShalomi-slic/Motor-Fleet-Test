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
using System.Threading;

public partial class BPF_Admin_Admin : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Update_class exe_up = new Update_class();
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
                        this.GetDetails(txt_un.Text.Trim());
                        this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
                        this.ClearAppText();
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




    protected void GetDetails(string userName)
    {
        DataTable details = new DataTable();
        try
        {
            string bank_val, branch_val, propTerm, propType = string.Empty;
           
            bank_val = ddl_bank.SelectedValue.ToString().Trim();
            branch_val = ddl_branch.SelectedValue.ToString().Trim();
            
            details = orcle_trans.GetRows(this._sql.GetBranchBPFAllowOrNot(Convert.ToInt32(bank_val), Convert.ToInt32(branch_val), userName.ToLower()), details);

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
        this.GetDetails(txt_un.Text.Trim().ToLower());
    }
    protected void SelectRecord_Click(object sender, EventArgs e)
    {

        ClearAppText();
        ImageButton btndetails = sender as ImageButton;

        //Get the Command Name.
        string commandName = btndetails.CommandName;

        //Get the Command Argument.
        string commandArgument = btndetails.CommandArgument;

        //Get the Row reference in which Button was clicked.
        GridViewRow row1 = (btndetails.NamingContainer as GridViewRow);

        //Get the Row Index.
        int rowIndex = row1.RowIndex;


        string[] arg = new string[4];
        arg = btndetails.CommandArgument.ToString().Split(';');

        string userName = arg[0].ToString().Trim();
        string bankCode = arg[1].ToString().Trim();
        string branchCode = arg[2].ToString().Trim();
        string flag = arg[3].ToString().Trim();
        string bankNmae = arg[4].ToString().Trim();
        string branchName = arg[5].ToString().Trim();

        lblBank.InnerHtml = bankCode;
        lblBranch.InnerHtml = branchCode;
        lblUn.InnerHtml = userName;

        lblBankName.InnerHtml = bankNmae;
        lblBranchName.InnerHtml = branchName;

        if (flag == "N")
        {
            chkstatus2.Checked = true;
            //chkstatus2.Attributes.Add("style", "checked");
        }

        else if (flag == "Y") { chkstatus1.Checked = true; }
        int intAuthCode = 1;
        string authCode = intAuthCode.ToString();

        foreach (GridViewRow row in Grid_Details.Rows)
        {
            //if (Convert.ToInt32(lblindex) > 11) { indexVal = Convert.ToInt32(lblindex) - 11; }
            //else { indexVal = Convert.ToInt32(lblindex); }

            if (row.RowIndex == Grid_Details.SelectedIndex + (rowIndex + 1))
            {
                //row.BackColor = Color.Yellow;
                row.BackColor = ColorTranslator.FromHtml("#e28743");

            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                //row.BackColor = Color.White;
            }
        }

    }


    protected void ClearText()
    {
        this.IniGridview();
        txt_un.Text = string.Empty;
        this.CleareDropDownList(ddl_bank);
        this.CleareDropDownList(ddl_branch);
        this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
    }


    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
        this.ClearAppText();
    }
    protected void btn_find_Click1(object sender, EventArgs e)
    {

        this.GetDetails(txt_un.Text.Trim().ToLower());
    }


    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        if (Grid_Details.Rows.Count > 0)
        {
            //ExportGridToExcel();
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
            Response.AddHeader("content-disposition", "attachment;filename=FirePolicyPending.xls");

            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                Grid_Details.AllowPaging = false;
                this.GetDetails(txt_un.Text.Trim().ToLower());
                //--end-->


                string wz = "Bancassurance BPF Authority List<br/>" +
                "Report Description : Bancassurance BPF Authority List Report. (" + ddl_bank.SelectedItem.ToString() + "/" + ddl_branch.SelectedItem.ToString() + ")<br/>" +
                //"Date From: " + txt_start_date.Text.ToString().ToUpper() + " To: " + txt_to_date.Text.ToString().ToUpper() + "<br/>" +
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
                        cell.BackColor = Grid_Details.HeaderStyle.BackColor = Color.AliceBlue;
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
            string op_ststus = e.Row.Cells[4].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (op_ststus.Equals("Deactive"))
                {
                    //cell.BackColor = Color.FromName("#F4F996"); //
                    e.Row.Cells[4].BackColor = Color.FromName("#ffcccc");
                }

                else if (op_ststus.Equals("Active"))
                {
                    //cell.BackColor = Color.FromName("#cffae2"); //#cffae2
                    e.Row.Cells[4].BackColor = Color.FromName("#cffae2");
                }

                else if (op_ststus.Equals("Wrong"))
                {
                    //cell.BackColor = Color.FromName("#ffcccc"); //#cffae2
                    e.Row.Cells[4].BackColor = Color.FromName("#F4F996");

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

    protected void ClearAppText()
    {
        lblBank.InnerHtml = "";
        lblBranch.InnerHtml = "";
        lblBankName.InnerHtml = "";
        lblBranchName.InnerHtml = "";
        lblUn.InnerHtml = "";
        rtnLbl.InnerHtml = "";
        chkstatus1.Checked = false;
        chkstatus2.Checked = false;
        this.GetDetails(txt_un.Text.Trim());
    }
    protected void btnApp_Click(object sender, EventArgs e)
    {
        Thread.Sleep(3000);
        string flag = "";
        if (chkstatus1.Checked) { flag = "Y"; }
        else if (chkstatus2.Checked) { flag = "N"; }
        else { flag = "W"; }



        bool iSReq = this.exe_up.update_BPF_Allow(flag, Session["UserId"].ToString(), lblBank.InnerHtml.ToString(), lblBranch.InnerHtml.ToString(), lblUn.InnerHtml.ToString());
        if (iSReq)
        {
            rtnLbl.Visible = true;
            rtnLbl.InnerHtml = "Record updated.";
            //ClearAppText();
        }
        //this.ClearAppText();
        this.IniGridview();
        //ddl_bank.SelectedItem.Value = lblBank.InnerText.ToString();
        //ddl_branch.SelectedItem.Value = lblBranch.InnerText.ToString();
        txt_un.Text = lblUn.InnerHtml.ToString();
        this.GetDetails(txt_un.Text.Trim().ToLower());
    }

    protected void btnClr_Click(object sender, EventArgs e)
    {
        ClearAppText();
    }
}