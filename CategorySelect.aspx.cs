using System;


public partial class CategorySelect : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void rblSelectMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblSelectMenu.SelectedIndex == 0)
        {
            try {
                //Response.Redirect("~/Default.aspx",false);
                Response.Redirect("~/MainIconMotor.aspx", false);
                Session["FireMotorCat"] = "motor";
            }
            catch (Exception ex) {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
            }
        }
        else if (rblSelectMenu.SelectedIndex == 1)
        {
            //Response.Redirect("~/DefaultFire.aspx", false);
            Response.Redirect("~/MainIconPage.aspx", false);
            Session["FireMotorCat"] = "fire";
        }

      
    }
}