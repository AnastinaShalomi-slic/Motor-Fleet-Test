using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MainIconMotor : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            string slic = Session["EPFNum"].ToString();
            string bank = Session["bank_code"].ToString();
            string mainCategory = Session["FireMotorCat"].ToString();
            string FireAdmin = Session["AdminForFire"].ToString();   //"FireAdmin";

            if (!string.IsNullOrEmpty(mainCategory) && mainCategory == "motor")
            {
                //home.Visible = true;

                if (!string.IsNullOrEmpty(bank))
                {


                    if (Session["userName_code"].ToString().Substring(4) == "admin")
                    {



                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                        pageSix.Visible = false;
                        pageSeven.Visible = true;
                        pageEight.Visible = false;
                        pageTen.Visible = true;

                        pageNine.Visible = false;
                        pageeleven.Visible = false;
                        
                        pagethirteen.Visible = false;

                        pagetwelve.Visible = true;
                        pagefourteen.Visible = true;
                        AdminPanel.Visible = true;

                    }
                    else
                    {
                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                        pageSix.Visible = true;
                        pageSeven.Visible = true;
                        pageEight.Visible = true;
                        pageTen.Visible = true;

                        pageNine.Visible = false;
                        pageeleven.Visible = false;
                        pagethirteen.Visible = false;

                       
                        pagetwelve.Visible = true;
                        pagefourteen.Visible = true;
                        AdminPanel.Visible = false;
                    }

                    //bankIcon.Attributes.Add("style", " background-image: url('~/Images/BOC_logo.jpg');");

                }

                else
                {

                    if (FireAdmin == "FireAdmin")
                    {
                        AdminPanel.Visible = true;
                    }

                    user_name.InnerHtml = Session["UsrName"].ToString() + " ( " + Session["EPFNum"].ToString().Substring(1) + " )";
                    //user_name.Text = "SEC****";
                    pageSix.Visible = false;
                    pageSeven.Visible = false;
                    //pageOne.Visible = false;
                    //pageTwo.Visible = false;
                    //pageThree.Visible = false;
                    //pageFour.Visible = false;
                    //pageFive.Visible = false;
                    pageEight.Visible = false;
                    pageNine.Visible = true;
                    pageTen.Visible = true;

                    pageeleven.Visible = false;
                    pagetwelve.Visible = true;
                    pagethirteen.Visible = false;
                    pagefourteen.Visible = true;
                    AdminPanel.Visible = true;
                }
            }
            else
            {
                //home.Visible = false;
                if (!string.IsNullOrEmpty(bank))
                {
                    user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                    //pageOne.Visible = true;
                    //pageTwo.Visible = true;
                    //pageThree.Visible = false;

                    //pageFour.Visible = true;
                    pageSix.Visible = false;
                    pageSeven.Visible = false;
                    pageEight.Visible = false;
                    pageNine.Visible = false;
                    pageTen.Visible = false;
                    pageeleven.Visible = false;
                    pagetwelve.Visible = false;
                    pagethirteen.Visible = false;
                    AdminPanel.Visible = false;
                    //this.GetDetails();
                    //this.GetDetailsUnderWriter();

                }

                else
                {
                    user_name.InnerHtml = Session["UsrName"].ToString() + " ( " + Session["EPFNum"].ToString().Substring(1) + " )";
                    //user_name.Text = "SEC****";
                    //pageOne.Visible = false;
                    //pageTwo.Visible = false;
                    //pageThree.Visible = true;
                    ////details_officer.Visible = false;

                    //pageFour.Visible = false;
                    pageSix.Visible = false;
                    pageSeven.Visible = false;
                    pageEight.Visible = false;
                    pageNine.Visible = false;
                    pageTen.Visible = false;
                    pageeleven.Visible = false;
                    pagetwelve.Visible = false;
                    pagethirteen.Visible = false;
                    AdminPanel.Visible = false;

                }
            }
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