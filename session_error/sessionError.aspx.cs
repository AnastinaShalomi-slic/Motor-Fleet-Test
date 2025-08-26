using System;

public partial class session_error_sessionError : System.Web.UI.Page
{
    string errorType = "";
    string ORA_ERROR = "";
    string APP_ERROR_MSG = "";
    string code = "";
    public string headTitle, footTitle, displayMsg, errorCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        var dc = new EncryptDecrypt();

         errorType = dc.Decrypt(Request.QueryString.Get("error")).ToString();
         APP_ERROR_MSG = dc.Decrypt(Request.QueryString.Get("APP_ERROR_MSG")).ToString();
         code = dc.Decrypt(Request.QueryString.Get("code")).ToString();

        if (errorType == "")
        {
            errorImage.Visible = true;
            oracleError.Visible = false;
            sucsessImage.Visible = false;
            headTitle = "Undefined error..!";
            footTitle = "SLIC IT";

            bt_ok.Visible = false;
            bt_back.Visible = true;

        }
        else if (errorType == "SE")
        {
            errorImage.Visible = true;
            oracleError.Visible = false;
            sucsessImage.Visible = false;
            headTitle = "Whoops! Sorry session expired !";
            footTitle = "SLIC IT";

            bt_ok.Visible = false;
            bt_back.Visible = true;
        }
        
        else if (errorType == "URL")
        {
            errorImage.Visible = true;
            oracleError.Visible = false;
            sucsessImage.Visible = false;
            headTitle = "Whoops! Looks like this page doesn't exist.";
            footTitle = "SLIC IT";

            bt_ok.Visible = false;
            bt_back.Visible = true;
        }
        else if (errorType == "Auth")
        {
            errorImage.Visible = true;
            oracleError.Visible = false;
            sucsessImage.Visible = false;
            headTitle = "401 Unauthorized Error.";
            footTitle = "SLIC IT";

            bt_ok.Visible = false;
            bt_back.Visible = true;
        }

        else if (errorType == "AuthLock")
        {
            errorImage.Visible = true;
            oracleError.Visible = false;
            sucsessImage.Visible = false;
            headTitle = APP_ERROR_MSG;//"401 Unauthorized Error.";
            footTitle = "SLIC IT";

            bt_ok.Visible = false;
            bt_back.Visible = true;
        }
        else if (errorType == "ORCL")
        {
            errorImage.Visible = true;
            oracleError.Visible = false;
            sucsessImage.Visible = false;
            headTitle = "ORA Error.";
            footTitle = "SLIC IT";

            bt_ok.Visible = false;
            bt_back.Visible = true;
        }

        else if (errorType == "APP_ERROR")
        {
            errorImage.Visible = true;
            oracleError.Visible = false;
            sucsessImage.Visible = false;
            headTitle = "APP_ERROR.";
            footTitle = "SLIC IT";

            bt_ok.Visible = false;
            bt_back.Visible = true;
        }
        else { }

        mainMsg.InnerHtml = "<h2><center>" + headTitle + "</center></h2> </br>" +
                            "<h4><center>" + displayMsg + "</center></h4> </br>" +
                            "<h4><center>" + footTitle + "</center></h4> </br>";

    }
	
	protected void bt_err_Click(object sender, EventArgs e)
    {

        if (code == "1") { Response.Redirect("~/Bank/Details_Entry.aspx"); }
        else if (code == "2") { Response.Redirect("~/SLIC_Side/RequestView.aspx"); }
        else if (code == "3") { Response.Redirect("~/SLIC_Side/ViewPageSecond.aspx"); }
        else if (code == "4") { Response.Redirect("~/Bank/ViewQuotations.aspx"); }
        else if (code == "5") { Response.Redirect("~/Bank_Fire/ViewProposal.aspx"); }
        else if (code == "7") { Response.Redirect("~/Bank_Fire/ViewProposal.aspx"); }
        else if (code == "8") { Response.Redirect("~/SLIC_Fire/Fire_Entered_Policy.aspx"); }
        else
        {
            //Session.Clear();
            //Response.Redirect("../../../Secworks/Signin.asp");
            Response.Redirect("~/SLIC_Fire/FireRenewal.aspx");
            //Response.Redirect("~/Login.aspx");
        }
    }
}