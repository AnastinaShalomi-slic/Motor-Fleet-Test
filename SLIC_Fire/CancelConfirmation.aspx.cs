using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SLIC_Fire_CancelConfirmation : System.Web.UI.Page
{
    
    Execute_sql _sql = new Execute_sql();
  
    Update_class exe_up = new Update_class();
 
    string Auth = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.ImageButton8);

        // ((MainMaster)Master).slected_manu.Value = "appReq";

        if (!Page.IsPostBack)
        {
            var endc = new EncryptDecrypt();
            try
            {
                Auth = "admin";//Session["Auth_Code"].ToString();
              
                if (Auth == "admin")
                {

                    if (Session["UserId"] != null)
                    {
                        
                        UserId.Value = Session["UserId"].ToString();
                        brCode.Value = Session["brcode"].ToString();
                        hfRefId.Value = endc.Decrypt(Request.QueryString["REQ_ID"]).Trim().ToString();
                        txtRef.InnerHtml = hfRefId.Value;
                    }


                    else
                    {
                      
                        Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("SE".ToString()));
                    }

                }
                else
                {
                    string msg = "You are not authorized to access this system.";


                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()));



                }


            }
            catch (Exception ex)
            {
             
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
            }
        }
        else
        {
            /* IF PAGE.POSTBACK ASSIGHEN SESSION VARIABLE */
            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
        }

    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        var endc = new EncryptDecrypt();
        try
        {

            //Response.Redirect("./CancelConfirmation.aspx?REQ_ID=" + endc.Encrypt(hfRefId.Value.ToString().Trim()), false);

            System.Threading.Thread.Sleep(3000);

            bool retunVal = exe_up.update_CancelFlag(hfRefId.Value.ToString().Trim(),txtRemarks.Text.ToString().Trim(), UserId.Value);
            if (retunVal)
            {
             
                string msg = "Proposal Successfully Cancelled.!";
                
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("10".ToString()), false);


            }
        }
        catch (Exception ex)
        {
            
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()), false);
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtRemarks.Text = "";
        
    }
}