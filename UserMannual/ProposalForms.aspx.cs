using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserMannual_ProposalForms : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string slic = Session["EPFNum"].ToString();
            string bank = Session["bank_code"].ToString();
            string mainCategory = Session["FireMotorCat"].ToString();
            if (!string.IsNullOrEmpty(mainCategory) && mainCategory == "fire")
            {

                motor.Visible = false;
               

            }
            else
            {
                if (bank == "7010")
                {
                    motor.Visible = true;
                    motorPB.Visible = false;
                }
                else if (bank == "7135") { motorPB.Visible = true; motor.Visible = false; }
                else
                { motor.Visible = false;
                  motorPB.Visible = false;
                }
               
            }


        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }
}