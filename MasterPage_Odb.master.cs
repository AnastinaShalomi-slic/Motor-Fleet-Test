using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage_Odb : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string slic = Session["EPFNum"].ToString();
            string bank = Session["bank_code"].ToString();
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()));
            //Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(ex.Message.ToString()));
        }
    }

    protected void signOutIdImg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx", false);
    }
}
