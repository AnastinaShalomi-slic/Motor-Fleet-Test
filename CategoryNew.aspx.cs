using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CategoryNew : System.Web.UI.Page
{
    ORCL_Connection orcl_con = new ORCL_Connection();
    string motorValidate = "";
    string fireValidate = "";

    int fireValidate2 = 0;
    string slic = string.Empty;
    string bank = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
             slic = Session["EPFNum"].ToString();
             bank = Session["bank_code"].ToString();

            motorValidate = Session["bank_name_code"].ToString();
            fireValidate = Session["fireValidate"].ToString();

            if (!string.IsNullOrEmpty(fireValidate))          
                fireValidate2 = Convert.ToInt32(fireValidate);
            
            else          
                fireValidate2 = 0;
            

            if (!string.IsNullOrEmpty(bank))
            {
                if (Session["userName_code"].ToString().Substring(4) == "admin")
                {
                    user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                    //pageSix.Visible = true;
                    pageEight.Visible = true;
                }

                else
                {
                    if (!string.IsNullOrEmpty(motorValidate) && (fireValidate2 == 1))
                    {
                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";

                        if (Session["bank_code"].ToString().Trim() == "7135")
                        {
                           // pageSix.Visible = false;
                            pageEight.Visible = true;
                        }
                        else
                        {
                           // pageSix.Visible = true;
                            pageEight.Visible = true;
                        }
                    }

                    else if (!string.IsNullOrEmpty(motorValidate) && (fireValidate2 != 1))
                    {
                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                        if (Session["bank_code"].ToString().Trim() == "7135")
                        {
                            //pageSix.Visible = false;
                            pageEight.Visible = false;
                        }
                        else
                        {
                           // pageSix.Visible = true;
                            pageEight.Visible = false;
                        }
                    }

                    else if (string.IsNullOrEmpty(motorValidate) && (fireValidate2 == 1))
                    {
                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                        if (Session["bank_code"].ToString().Trim() == "7135")
                        {
                           // pageSix.Visible = false;
                            pageEight.Visible = true;
                        }
                        else
                        {
                           // pageSix.Visible = false;
                            pageEight.Visible = true;
                        }
                    }
                    else
                    {
                        var endc = new EncryptDecrypt();
                        Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()));
                    }
                }
                //pageTicket.Visible = true;
                user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
            }

            else if (!string.IsNullOrEmpty(slic))
            {

                user_name.InnerHtml = Session["UsrName"].ToString() + " ( " + Session["EPFNum"].ToString().Substring(1) + " )";
                //user_name.Text = "SEC****";
               // pageSix.Visible = true;
                pageEight.Visible = true;
                //pageTicket.Visible = true;
            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()));
            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()));
            //Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(ex.Message.ToString()));

        }
    }

    protected void MotorClick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/MotorDefault.aspx", false);
            Session["FireMotorCat"] = "motor";
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }
   
    protected void FireClick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/FireDefault.aspx", false);
            Session["FireMotorCat"] = "fire";
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }

    protected void OdClick(object sender, EventArgs e)
    {
        try
        {
            if (!String.IsNullOrEmpty(bank) && int.Parse(bank) == 7010)
            {
                    Response.Redirect("~/OdProtect/Handler.ashx", false);
            }

            else if (!String.IsNullOrEmpty(slic))
            {
                Response.Redirect("~/OdProtect/Handler.ashx", false);
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "custom_alert('You are not authorized to perform this transaction.', 'Alert');", true);
            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }

    protected void TicketClick(object sender, EventArgs e)
    {
        try
        {
            Response.Redirect("~/TicketDefault.aspx", false);
            Session["FireMotorCat"] = "ticket";
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }

    protected void signOutIdImg_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Secworks/Signin.asp");
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        using (OracleConnection conn = orcl_con.GetConnection())
        {
            conn.Open();
            using (OracleCommand cmd = new OracleCommand("slic_net.send_html_email_cc", conn))
            {           
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("p_to", "kavindad@srilankainsurance.com");
                cmd.Parameters.AddWithValue("p_cc", "");
                cmd.Parameters.AddWithValue("p_bcc", "");
                cmd.Parameters.AddWithValue("p_from", "motorquotation@srilankainsurance.com");
                cmd.Parameters.AddWithValue("p_subject", "Test");
                cmd.Parameters.AddWithValue("p_text_msg", "Test text");
                cmd.Parameters.AddWithValue("p_html_msg", "Test HTML");
              
                OracleDataReader dr = cmd.ExecuteReader();          
                dr.Close();
            }
            conn.Close();
        }
    }
}