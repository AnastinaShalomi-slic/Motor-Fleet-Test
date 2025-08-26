using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class session_error_MessagePages : System.Web.UI.Page
{
    string errorType = "";
    string app_msg = "";
    string code = "";
    string ref_code = "";
    public string headTitle, footTitle, displayMsg, errorCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
     {
        var dc = new EncryptDecrypt();

        errorType = dc.Decrypt(Request.QueryString.Get("error")).ToString();
        app_msg = dc.Decrypt(Request.QueryString.Get("APP_MSG")).ToString();
        code = dc.Decrypt(Request.QueryString.Get("code")).ToString();
        ref_code = dc.Decrypt(Request.QueryString.Get("ref_send")).ToString();

        if (ref_code == "#") { ref_code = ""; }

        if (errorType == "")
        {
            errorImage.Visible = true;
            oracleError.Visible = false;
            sucsessImage.Visible = false;
            headTitle = "401 Unauthorized";
            footTitle = "SLIC IT";

            bt_ok.Visible = false;
            bt_back.Visible = true;
        }
        else if (errorType == "ERROR_CLOSE") 
        {
            errorImage.Visible = false;
            oracleError.Visible = true;
            sucsessImage.Visible = false;
            headTitle = "Error in Oracle.";
            footTitle = "SLIC IT";

            bt_ok.Visible = false;
            bt_back.Visible = true;
        }
        else if (errorType == "REJECT")
        {
            errorImage.Visible = false;
            oracleError.Visible = false;
            sucsessImage.Visible = true;
            headTitle = "Successfully Rejected";
            footTitle = "";

            bt_back.Visible = false;
            bt_ok.Visible = true;
        }
        else if (errorType == "DONE")
        {
            errorImage.Visible = false;
            oracleError.Visible = false;
            sucsessImage.Visible = true;
            headTitle = "Successfully Completed";
            //footTitle = "SLIC IT";
            footTitle = "";

            bt_back.Visible = false;
            bt_ok.Visible = true;
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
        else if (code == "3") { Response.Redirect("~/Bank/Details_Entry.aspx"); }
        else if (code == "6") { Response.Redirect("~/Bank_Fire/ViewProposal.aspx"); }
        //admin--->
        else if (code == "7") { Response.Redirect("~/AdminPanel/AdminViewMotor.aspx"); }
        else if (code == "8") { Response.Redirect("~/AdminPanel/AdminView.aspx"); }
        else if (code == "14") { Response.Redirect("~/BankResponseTeam/BankTicket.aspx"); }
        else if (code == "15") { Response.Redirect("~/SLICResponseTeam/SLICTicket.aspx"); }
        //else if (code == "10") { Response.Redirect("../../MainIconPage.aspx"); }
        else
        {
            Session.Clear();
            //Response.Redirect("../../../Secworks/Signin.asp");
            Response.Redirect("~/Login.aspx");
        }
    }

    protected void bt_ok_Click(object sender, EventArgs e)
    {
        if (code == "1") { Response.Redirect("~/Bank/Details_Entry.aspx"); }
        else if (code == "2") { Response.Redirect("~/SLIC_Side/RequestView.aspx"); }
        else if (code == "3") { Response.Redirect("~/Bank/Details_Entry.aspx"); }
        else if (code == "4") { Response.Redirect("~/SLIC_Fire/Fire_Entered_Policy.aspx"); }
        else if (code == "5") { Response.Redirect("~/Bank_Fire/ViewProposal.aspx"); }
        else if (code == "6") { Response.Redirect("~/Bank_Fire/proposalTypes.aspx"); }
        else if (code == "10") { Response.Redirect("~/MainIconPage.aspx"); }
        else if (code == "11") { Response.Redirect("~/SLIC_Fire/ApprovedPolicy.aspx"); }
        else if (code == "13") { Response.Redirect("~/Bank/ViewQuotations.aspx"); }
        else if (code == "14") { Response.Redirect("~/BankResponseTeam/BankTicket.aspx"); }
        else if (code == "15") { Response.Redirect("~/SLICResponseTeam/SLICTicket.aspx"); }

    }

   
}