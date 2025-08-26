using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultFire : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Insert_class exe_in = new Insert_class();
    Update_class _update = new Update_class();
    string Auth = string.Empty;

    string agentCode, agentName, BGI = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            try
            {
                string slic = Session["EPFNum"].ToString();
                string bank = Session["bank_code"].ToString();
                //Session["FireCat"] = "bankSide";

                if (!string.IsNullOrEmpty(bank))
                {
                    this.GetDetails();
                    this.GetDetailsUnderWriter();
                }
                else { details_officer.Visible = false; }

               

            }
            catch (Exception ex)
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
            }

        }
        else
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt("Please Login Again."));
        }
    }



    protected void GetDetails()
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetOfficer(Session["bank_code"].ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    ListView1.DataSource = details;
                    ListView1.DataBind();
                }
                else
                {
                    ListView1.DataSource = null;
                    ListView1.DataBind();

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
    protected void GetDetailsUnderWriter()
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetunderWriter(Session["bank_code"].ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    ListView2.DataSource = details;
                    ListView2.DataBind();
                }
                else
                {
                    ListView2.DataSource = null;
                    ListView2.DataBind();

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

   

}