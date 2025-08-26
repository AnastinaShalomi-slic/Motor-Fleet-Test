using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class session_error_ErrorPage : System.Web.UI.Page
{
    public string headTitle, footTitle, displayMsg, errorCode = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        var dc = new EncryptDecrypt();
        displayMsg = dc.Decrypt(Request.QueryString.Get("APP_MSG")).ToString();
        errorCode = dc.Decrypt(Request.QueryString.Get("code")).ToString();

        if (!Page.IsPostBack)
        {

            try
            {
                string Auth = "admin";//Session["Auth_Code"].ToString();


                if (Auth == "admin")
                {

                    //if (Session["UserId"].ToString() != "")
                    //{




                        brCode.Value = Session["brcode"].ToString();
                        UserId.Value = Session["UserId"].ToString();
                        //var dc = new EncryptDecrypt();

                        



                        if (errorCode == "1")
                        {
                            errorImage.Visible = true;
                            oracleError.Visible = false;
                            sucsessImage.Visible = false;
                            headTitle = "401 Unauthorized";
                            footTitle = "SLIC IT";
                        }
                        else if (errorCode == "2")
                        {
                            errorImage.Visible = false;
                            oracleError.Visible = true;
                            sucsessImage.Visible = false;
                            headTitle = "Error in Oracle.";
                            footTitle = "SLIC IT";
                        }
                        else if (errorCode == "3")
                        {
                            errorImage.Visible = false;
                            oracleError.Visible = false;
                            sucsessImage.Visible = true;
                            headTitle = "Successfully Completed";
                            footTitle = "SLIC IT";
                        }
                        else if (errorCode == "4")
                        {
                            errorImage.Visible = false;
                            oracleError.Visible = false;
                            sucsessImage.Visible = true;
                            headTitle = "Successfully Completed";
                            footTitle = "SLIC IT";
                        }
                        else if (errorCode == "5")
                        {
                        errorImage.Visible = false;
                        oracleError.Visible = false;
                        sucsessImage.Visible = true;
                        headTitle = "Successfully Updated";
                        footTitle = "SLIC IT";
                        }

                    else if (errorCode == "6")
                    {
                        errorImage.Visible = false;
                        oracleError.Visible = false;
                        sucsessImage.Visible = true;
                        headTitle = "Successfully Updated Policy Number.";
                        footTitle = "SLIC IT";
                    }

                    else if (errorCode == "7")
                    {
                        errorImage.Visible = true;
                        oracleError.Visible = false;
                        sucsessImage.Visible = false;
                        headTitle = "Warning!.";
                        footTitle = "SLIC IT";
                    }
                    else { }

                        mainMsg.InnerHtml = "<h2><center>" + headTitle + "</center></h2> </br>" +
                                              "<h4><center>" + displayMsg + "</center></h4> </br>" +
                                              "<h4><center>" + footTitle + "</center></h4> </br>";

                    //}


                    //else
                    //{
                    //    var endc = new EncryptDecrypt();
                    //    string msg = "Session expired. Please login again.";
                    //    Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("1".ToString()), false);
                    //}

                }
                else
                {
                    //string msg = "You are not authorized to access this system.";
                    //var endc = new EncryptDecrypt();
                    //Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("1".ToString()), false);
                    Response.Redirect("~/SessionTrans.aspx");

                }


            }
            catch (Exception ex)
            {
                //var endc = new EncryptDecrypt();
                //string msg = ex.ToString();
                //Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("1".ToString()), false);
                Response.Redirect("~/SessionTrans.aspx");
            }
        }
        else
        {
            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
        }
    }

    protected void btBack_Click(object sender, EventArgs e)
    {

        if (errorCode == "1")
        {   //Response.Redirect("~/Default.aspx");
            Response.Redirect("~/Login.aspx");
        }
        else if (errorCode == "2") { Response.Redirect("~/Bank_Fire/ProposalEntry.aspx"); }
        else if (errorCode == "3") { Response.Redirect("~/FireDefault.aspx"); }
        //else if (errorCode == "4") { Response.Redirect("~/BackOffic/ProcessQuotationAll.aspx?brcode="+ brCode.Value); }//{ Response.Redirect("~/Default.aspx"); } 
        //else if (errorCode == "5") { Response.Redirect("~/ProposalForms/ViewProgress.aspx"); }
        //else if (errorCode == "6") { Response.Redirect("~/BackOffic/PolicyEnter/CompletedPolicy.aspx"); }
        //else if (errorCode == "7") { Response.Redirect("~/BackOffic/PolicyEnter/CompletedPolicy.aspx"); }
        else
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}