using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Bank_Fire_proposalTypes : System.Web.UI.Page
{
    string Auth = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        Session["r01"] = "";
        if (!Page.IsPostBack)
        {
            try
            {
                Auth = "admin";//Session["Auth_Code"].ToString();

                if (Auth == "admin")
                {



                    if (Session["bank_code"].ToString() != "")
                    {


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
        }


    }
    
    protected void Option1(object sender, EventArgs e)
    {

        try
        {
            var endc = new EncryptDecrypt();
            string PropType = "1";

            Response.Redirect("~/Bank_Fire/ProposalEntry.aspx?Type=" + endc.Encrypt(PropType.ToString()),false);
            
           
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }

    }

    protected void Option2(object sender, EventArgs e)
    {

        try
        {

            var endc = new EncryptDecrypt();
            string PropType = "2";

            Response.Redirect("~/Bank_Fire/ProposalEntry.aspx?Type=" + endc.Encrypt(PropType.ToString()), false);

        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }

    }
    protected void Option3(object sender, EventArgs e)
    {

        try
        {

            var endc = new EncryptDecrypt();
            string PropType = "3";

            Response.Redirect("~/Bank_Fire/ProposalEntry.aspx?Type=" + endc.Encrypt(PropType.ToString()), false);

        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }

    }

}