using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    ExcuteOraSide ora_side = new ExcuteOraSide();
    Execute_sql _sql = new Execute_sql();
    LogFile Err = new LogFile();
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.ImageButton8);
        //((MainMaster)Master).slected_manu.Value = "appReq";

        if (!Page.IsPostBack)
        {
            try
            {
                //this.CleareDropDownList(ddl_bank);
                //this.ClearText();
                //this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");
                //user_name.InnerHtml = "Super User Acc. - " + Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                gridPanel.Visible = false;
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

            //Server.TransferRequest(Request.Url.AbsolutePath, false);

        }
    }

    protected void ClearText()
    {
        IniGridview();
        this.CleareDropDownList(ddl_bank);

        this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodes(), "'bbnam'");

    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
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

    protected void CleareDropDownList(DropDownList ddl)
    {
        var firstitem_1 = ddl.Items[0];
        ddl.Items.Clear();
        ddl.Items.Add(firstitem_1);
    }
    protected void IniGridview()
    {
        Grid_Details.DataSource = null;
        Grid_Details.DataBind();
    }
    protected void ddl_bank_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetDetails(ddl_bank.SelectedValue.ToString().Trim());
    }

    protected void btn_find_Click1(object sender, EventArgs e)
    {
        this.GetDetails(ddl_bank.SelectedValue.ToString().Trim());
    }

    protected void GetDetails(string bankCode)
    {
        DataTable details = new DataTable();
        try
        {

            details = orcle_trans.GetRows(this._sql.GetImmediateOfficer(bankCode), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    Grid_Details.DataSource = details;
                    Grid_Details.DataBind();
                    gridPanel.Visible = true;
                    
                    string email = details.Rows[0]["email"].ToString();
                    emailVal.InnerHtml = "Email : " + email;


                    //Grid_Details.FooterRow.Cells.RemoveAt(1);
                    //Grid_Details.FooterRow.Cells[2].ForeColor = System.Drawing.Color.White;
                    //Grid_Details.FooterRow.Cells[2].Font.Size = 16;
                    //Grid_Details.FooterRow.Cells[2].Text = "Email : " + email;
                }
                else
                {
                    Grid_Details.DataSource = null;
                    Grid_Details.DataBind();
                    gridPanel.Visible = false;

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
        this.GetDetails(ddl_bank.SelectedValue.ToString().Trim());
    }


    protected void OnDataBound(object sender, EventArgs e)
    {
        for (int i = Grid_Details.Rows.Count - 1; i > 0; i--)
        {
            GridViewRow row = Grid_Details.Rows[i];
            GridViewRow previousRow = Grid_Details.Rows[i - 1];

            if (row.Cells[3].Text == previousRow.Cells[3].Text)
            {
                if (previousRow.Cells[3].RowSpan == 0)
                {
                    if (row.Cells[3].RowSpan == 0)
                    {
                        previousRow.Cells[3].RowSpan += 3;
                    }
                    else
                    {
                        previousRow.Cells[3].RowSpan = row.Cells[3].RowSpan + 1;
                    }
                    row.Cells[3].Visible = false;
                }
            }
        }
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string op_ststus = e.Row.Cells[1].Text;

            foreach (TableCell cell in e.Row.Cells)
            {
                if (op_ststus.Equals("Pending"))
                {
                    //cell.BackColor = Color.FromName("#F4F996"); //
                    e.Row.Cells[10].BackColor = Color.FromName("#F4F996");
                }

                else if (op_ststus.Equals("Completed"))
                {
                    //cell.BackColor = Color.FromName("#cffae2"); //#cffae2
                    e.Row.Cells[10].BackColor = Color.FromName("#cffae2");
                }

                else if (op_ststus.Equals("Rejected"))
                {
                    //cell.BackColor = Color.FromName("#ffcccc"); //#cffae2
                    e.Row.Cells[10].BackColor = Color.FromName("#ffcccc");

                }
            }


        }


    }


    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        //if ((urName.Value == "admin" && passwrd.Value == "123")) {
            if (!string.IsNullOrEmpty(urName.Value ) && !string.IsNullOrEmpty(passwrd.Value))
            {
                var endc = new EncryptDecrypt();               

            //string message = "Logged Successfully.";
            //System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //sb.Append("<script type = 'text/javascript'>");
            //sb.Append("window.onload=function(){");
            //sb.Append("alert('");
            //sb.Append(message);
            //sb.Append("')};");
            //sb.Append("</script>");
            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", sb.ToString());

            // Response.Redirect("~/Default.aspx");
            Response.Redirect("~/SessionTrans.aspx?user_name=" + endc.Encrypt(urName.Value.ToString().Trim())
                + "&Pw=" + endc.Encrypt(passwrd.Value.ToString().Trim()));
        }
        else {  }

        
    }
}