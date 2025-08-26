using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserMannual_User_manual : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Insert_class exe_in = new Insert_class();
    string Auth = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        

        try
        {
            string slic = Session["EPFNum"].ToString();
            string bank = Session["bank_code"].ToString();
            string mainCategory = Session["FireMotorCat"].ToString();
            if (!string.IsNullOrEmpty(mainCategory) && mainCategory == "fire")
            {


                if (bank == "7010")
                {
                    motorBOC.Visible = false;
                    fireBOC.Visible = true;
                    fire_booklet.Visible = false;
                    firePB.Visible = false;
                    motorPB.Visible = false;

                }
                else if (bank == "7135")
                {
                    motorBOC.Visible = false;
                    fireBOC.Visible = false;
                    fire_booklet.Visible = false;
                    firePB.Visible = true;
                    motorPB.Visible = false;
                }
                else
                {
                    motorBOC.Visible = false;
                    fireBOC.Visible = true;
                    fire_booklet.Visible = false;
                    firePB.Visible = false;
                    motorPB.Visible = false;
                }



            }
            else //motor category
            {

                if (bank == "7010")
                {
                    motorBOC.Visible = true;
                    fireBOC.Visible = false;
                    fire_booklet.Visible = false;
                    firePB.Visible = false;
                    motorPB.Visible = false;
                }
                else if (bank == "7135")
                {
                    motorBOC.Visible = false;
                    fireBOC.Visible = false;
                    fire_booklet.Visible = false;
                    firePB.Visible = false;
                    motorPB.Visible = true;
                }
                else
                {
                    motorBOC.Visible = true;
                    fireBOC.Visible = false;
                    fire_booklet.Visible = false;
                    firePB.Visible = false;
                    motorPB.Visible = false;
                }

               
            }


            //    if (!string.IsNullOrEmpty(bank))
            //{
            //    this.GetDetails();
            //    this.GetDetailsUnderWriter();
            //}
            //else { details_officer.Visible = false; }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }


    }

    //protected void GetDetails()
    //{
    //    DataTable details = new DataTable();
    //    try
    //    {
    //        details = orcle_trans.GetRows(this._sql.GetOfficer(Session["bank_code"].ToString()), details);

    //        if (orcle_trans.Trans_Sucess_State == true)
    //        {

    //            if (details.Rows.Count > 0)
    //            {
    //                ListView1.DataSource = details;
    //                ListView1.DataBind();
    //            }
    //            else
    //            {
    //                ListView1.DataSource = null;
    //                ListView1.DataBind();

    //            }
    //        }
    //        else
    //        {
    //            var endc = new EncryptDecrypt();
    //            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        var endc = new EncryptDecrypt();
    //        Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

    //    }
    //}
    //protected void GetDetailsUnderWriter()
    //{
    //    DataTable details = new DataTable();
    //    try
    //    {
    //        details = orcle_trans.GetRows(this._sql.GetunderWriter(Session["bank_code"].ToString()), details);

    //        if (orcle_trans.Trans_Sucess_State == true)
    //        {

    //            if (details.Rows.Count > 0)
    //            {
    //                ListView2.DataSource = details;
    //                ListView2.DataBind();
    //            }
    //            else
    //            {
    //                ListView2.DataSource = null;
    //                ListView2.DataBind();

    //            }
    //        }
    //        else
    //        {
    //            var endc = new EncryptDecrypt();
    //            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        var endc = new EncryptDecrypt();
    //        Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

    //    }
    //}
}