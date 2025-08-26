using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web;

public partial class superUserPanel : System.Web.UI.Page
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
                if (Session["userName_code"].ToString() != "")
                {
                    userName.Value = Session["userName_code"].ToString();
                    info.Visible = false;
                    btnPanel.Visible = false;
                    //this.ClearText();
                    this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodesOneItem(Session["bank_code"].ToString().Trim()), "'bbnam'");
                    user_name.InnerHtml = "Super User Acc. - "+Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
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
           Session["userName_code"] = userName.Value;
            //Server.TransferRequest(Request.Url.AbsolutePath, false);

        }
    }

    protected void ClearText()
    {
     
        this.CleareDropDownList(ddl_bank);
        this.CleareDropDownList(ddl_branch);
        this.InitializedListBank(ddl_bank, "bbnam", "bcode", this._sql.GetBankCodesOneItem(Session["bank_code"].ToString().Trim()), "'bbnam'");
        info.Visible = false;
        btnPanel.Visible = false;
        d1.InnerHtml = "";
        d2.InnerHtml = "";
        d3.InnerHtml = "";
        d4.InnerHtml = "";
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }
    protected void btn_find_Click1(object sender, EventArgs e)
    {
        try
        {
            string bank_code = ddl_bank.SelectedValue.ToString().Trim();
            string branch_code = ddl_branch.SelectedValue.ToString().Trim();

            string temp_bank, temp_branch, temp_bank_name = string.Empty;
            int ReCount = 0;

            if (branch_code.Length > 7) { branch_code = branch_code.Trim().Substring(0, 7); }
            else { branch_code = branch_code.Trim(); }


            Session["bank_code"] = bank_code.Trim(); //7010
            Session["branch_code"] = branch_code.Trim(); //7010001
                                                         //Session["userName_code"] = temp_user_name.Trim();

            if (branch_code != "")
            {
                //as400.as400_get_userBankEmail(user_name, out bancass_email);
                ora_side.get_userBankBranch(branch_code, out temp_bank, out temp_branch);
                ora_side.get_BankName(bank_code, out temp_bank_name);
                ora_side.get_fireValidation(bank_code, out ReCount);

                //Session["bancass_email"] = bancass_email.Trim();
                //Session["bancass_email"] = "Test@gmail.com"; cannot get without user name 
                Session["temp_bank"] = temp_bank.Trim();
                Session["temp_branch"] = temp_branch.Trim();


                //ReCount = 2;
                Session["bank_name_code"] = temp_bank_name.Trim();
                Session["fireValidate"] = ReCount;

                // Auth_Code.Value = "admin";
                Session["Auth_Code"] = "admin";
                //Response.Redirect("~/Default.aspx");
                //Response.Redirect("~/CategorySelect.aspx");

                lblOut.InnerHtml = Session["bank_code"].ToString() + "<br/>" + Session["branch_code"].ToString() + "<br/>" + Session["userName_code"].ToString() + "<br/>" +
                    Session["bancass_email"].ToString() + "<br/>" + Session["temp_bank"].ToString() + "<br/>" + Session["temp_branch"].ToString() + "<br/>" + Session["bank_name_code"] + "<br/>" +
                    Session["fireValidate"].ToString();

                if (!string.IsNullOrEmpty(temp_bank_name) || (ReCount == 1))
                {
                   
                    Response.Redirect("~/CategoryNew.aspx",false);
                }
                else
                {
                    string msg = "You are not authorized to access this system.Super User";

                    Err.ErrorLog(Server.MapPath("Logs/ErrorLog"), msg);

                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()) + "&APP_MSG=" + endc.Encrypt(msg));


                }



            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()));

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
        info.Visible = false;
        btnPanel.Visible = false;
        // ddl_model.SelectedIndex = 0;
        //this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), "");
        this.InitializedListBranch(ddl_branch, "bbrnch", "bbcode", this._sql.GetBranch(ddl_bank.SelectedValue.ToString()), "'bbrnch'");
    }
    protected void ddl_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        //this.CleareDropDownList(ddl_branch);
        // ddl_model.SelectedIndex = 0;
        //this.GetDetails(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim().ToUpper(), "");
        info.Visible = true;
        btnPanel.Visible = true;
        d1.InnerHtml = ":  &nbsp;" + ddl_bank.SelectedItem.ToString();
        d2.InnerHtml = ":  &nbsp;" + ddl_bank.SelectedValue.ToString();
        d3.InnerHtml = ":  &nbsp;" + ddl_branch.SelectedItem.ToString();
        d4.InnerHtml = ":  &nbsp;" + ddl_branch.SelectedValue.ToString();

    }
    protected void signOutIdImg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx", false);
    }
}