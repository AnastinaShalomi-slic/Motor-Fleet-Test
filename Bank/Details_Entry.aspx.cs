using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bank_Details_Entry : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Insert_class exe_in = new Insert_class();
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

                    string bcode = Session["bank_code"].ToString();

                    if (!string.IsNullOrEmpty(bcode))
                    {

                        UserId.Value = Session["UserId"].ToString();
                        brCode.Value = Session["brcode"].ToString();

                        this.InitializedListMake(ddl_make, "VH_MAKE", "MAKE_ID", this._sql.GetVMake(), "'MAKE_ID'");

                        bt_cnOther.Visible = false;

                        //-----for years select------->>>
                        int year = DateTime.Now.Year;
                        for (int i = year; i >= year - 30; i--)
                        {
                            ListItem li = new ListItem(i.ToString());
                            yom.Items.Add(li);
                        }

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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void InitializedListMake(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
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


    protected void InitializedListModel(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {


        DataTable getrecord = new DataTable();
        try
        {
            ///btn_clear.Enabled = true;

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

        }
    }
    protected void ClearText()
    {


    }


    protected void CleareDropDownList(DropDownList ddl)
    {
        var firstitem_1 = ddl.Items[0];
        ddl.Items.Clear();
        ddl.Items.Add(firstitem_1);
    }

    protected void ddl_make_SelectedIndexChanged(object sender, EventArgs e)
    {
        bt_other.Enabled = true;
        bt_other.Visible = true;
        bt_cnOther.Visible = false;
        ddl_model.Visible = true;
        ddl_model.Enabled = true;
        txt_other.Visible = false;
        this.CleareDropDownList(ddl_model);

        this.InitializedListModel(ddl_model, "MODEL_NAME", "MODEL_ID", this._sql.GetVModel(ddl_make.SelectedValue.ToString()), "'MODEL_NAME'");
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        ddl_model.Visible = true;
        txt_other.Visible = false;
        ddl_vType.SelectedIndex = 0;
        this.CleareDropDownList(ddl_model);
        this.CleareDropDownList(ddl_make);
        ddl_purpose.SelectedIndex = 0;
        ddl_fuel.SelectedIndex = 0;
        emailId.Text = string.Empty;
        cusName.Text = string.Empty;
        cusNo.Text = string.Empty;
        regNo.Text = string.Empty;
        sumInsu.Text = string.Empty;
        yom.SelectedIndex = 0;

        this.InitializedListMake(ddl_make, "VH_MAKE", "MAKE_ID", this._sql.GetVMake(), "'MAKE_ID'");

    }


    protected void GetPhoneNumberofOfficers(string req_id)
    {
        string officerContact = string.Empty;
        int rtnCount = 0;
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetOfficer(Session["bank_code"].ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    for (int i = 0; i < details.Rows.Count; i++)
                    {
                        officerContact = details.Rows[i]["CONTACT_NO"].ToString().Substring(1);
                        officerContact = "94" + officerContact;

                        string txt_body = "Motor Quotation Request from Bank. Reference ID : ";

                        this.exe_in.Send_sms_to_customer(officerContact, req_id, txt_body, out rtnCount);

                    }

                }
                else
                {
                    officerContact = "";

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

    protected void btn_send_Click(object sender, EventArgs e)
    {
        int model_code = 0;
        string other_model = string.Empty;
        string gen_req_id = string.Empty;
        string txtfuel = "";
        if (string.IsNullOrEmpty(ddl_model.SelectedValue.ToString()) || ddl_model.SelectedValue.ToString() == "0")
        {
            model_code = 0;
        }
        else
        {
            model_code = Convert.ToInt32(ddl_model.SelectedValue.ToString());
        }

        if (string.IsNullOrEmpty(txt_other.Text))
        {
            other_model = "N";
        }
        else
        {
            other_model = txt_other.Text.ToString();
        }

        if (ddl_fuel.SelectedValue.ToString() == "A")
        { txtfuel = ""; }
        else
        { txtfuel = ddl_fuel.SelectedValue.ToString(); }


        bool sucess = this.exe_in.insert_quotation_details(ddl_vType.SelectedValue.ToString(), Convert.ToInt32(yom.SelectedItem.Text.ToString().Trim()), Convert.ToDouble(sumInsu.Text.ToString().Trim()),
           Convert.ToInt32(ddl_make.SelectedValue.ToString()), model_code, ddl_purpose.SelectedValue.ToString(),
           regNo.Text.ToString(), ddlInitials.SelectedValue.ToString() + cusName.Text.ToString(), cusNo.Text.ToString(), txtfuel, Session["userName_code"].ToString(), emailId.Text.ToString(), "P", Session["bank_code"].ToString(), Session["branch_code"].ToString(), txt_other.Text.ToString(),
           Session["bancass_email"].ToString(), Session["bank_name_code"].ToString(), out gen_req_id);


        //send sms alert to sales officers-------------------

        if (!string.IsNullOrEmpty(gen_req_id))
        {
            this.GetPhoneNumberofOfficers(gen_req_id);
        }

        //----------------------------------------------------
        if (sucess == true)
        {

            string msg = "Quotation Request Successfully Sent to SLIC.!";
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("1".ToString()));
            this.ClearText();

        }
        else
        {

            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));
        }
    }

    protected void bt_other_Click(object sender, EventArgs e)
    {
        txt_other.Visible = true;
        ddl_model.Visible = false;
        ddl_model.Enabled = false;
        bt_cnOther.Visible = true;
        bt_other.Visible = false;
    }

    protected void bt_cnOther_Click(object sender, EventArgs e)
    {
        txt_other.Visible = false;
        ddl_model.Visible = true;
        ddl_model.Enabled = true;
        bt_other.Visible = true;
        bt_cnOther.Visible = false;
    }



}