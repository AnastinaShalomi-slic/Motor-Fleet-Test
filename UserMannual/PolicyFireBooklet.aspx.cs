using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserMannual_PolicyFireBooklet : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Insert_class exe_in = new Insert_class();
    string Auth = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {


        try
        {
            string slic = Session["EPFNum"].ToString();
            string bank = Session["bank_code"].ToString();
            string mainCategory = Session["FireMotorCat"].ToString();
            if (!string.IsNullOrEmpty(mainCategory) && mainCategory == "fire")
            {

                fire_booklet.Visible = true;
                Motor_booklet.Visible = false;

            }
            else
            {
                fire_booklet.Visible = false;
                Motor_booklet.Visible = true;
            }

        
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }


    }

}