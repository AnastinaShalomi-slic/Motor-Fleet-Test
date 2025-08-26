using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TicketDefault : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);


        if (!Page.IsPostBack)
        {
            try
            {
                if (Session["bank_code"].ToString() != "")
                {


                    //UserId.Value = Session["bank_code"].ToString();
                    //brCode.Value = Session["branch_code"].ToString();
                    //user_epf.Value = Session["userName_code"].ToString();

                    //dropdown help maintance------>
                    //BOC

                    if (Session["bank_code"].ToString().Trim() == "7010")
                    {
                        BOC.Visible = true;
                        PB.Visible = false;
                        NSB.Visible = false;
                        SLIC.Visible = false;
                    }

                    //Pb
                    else if (Session["bank_code"].ToString().Trim() == "7135")
                    {
                        PB.Visible = true;
                        BOC.Visible = false;
                        NSB.Visible = false;
                        SLIC.Visible = false;
                    }
                    else if (Session["bank_code"].ToString().Trim() == "7719")
                    {
                        PB.Visible = false;
                        BOC.Visible = false;
                        NSB.Visible = true;
                        SLIC.Visible = false;
                    }
                    else
                    {
                        PB.Visible = false;
                        BOC.Visible = false;
                        SLIC.Visible = false;
                        NSB.Visible = false;
                    }

                    //------------------end-------->


                }
                else if (Session["UserId"].ToString() != "")
                {
                    PB.Visible = false;
                    BOC.Visible = false;
                    SLIC.Visible = true;
                    NSB.Visible = false;
                }
                else
                {
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("SE".ToString()));
                    //Response.Redirect("~/ErrorPage/session_expired.aspx");
                }
            }
            catch (Exception ex)
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
            }

        }
        else
        {
            /* IF PAGE.POSTBACK ASSIGHEN SESSION VARIABLE */

            //Session["bank_code"] = UserId.Value;
            //Session["branch_code"] = brCode.Value;
            //hfTicketId.Value = "";
        }


    }
}