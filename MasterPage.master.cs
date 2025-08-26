using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    //public string selectYear="2017";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           

            string slic = Session["EPFNum"].ToString();
            string bank = Session["bank_code"].ToString();
            string mainCategory = Session["FireMotorCat"].ToString();
            string FireAdmin = Session["AdminForFire"].ToString();   //"FireAdmin";
            string BPFAdmin = Session["BPFAdmin"].ToString();   //"FireAdmin";

            if (!string.IsNullOrEmpty(mainCategory) && mainCategory == "fire")
            {
                //slicInq.Visible = false;
                //bankInq.Visible = false;
                //userManualMotor.Visible = false;
                //userManualFire.Visible = true;

                //bookFire.Visible = true;
                //bookMotor.Visible = false;

                if (!string.IsNullOrEmpty(bank))
                {
                    //dropdown help maintance------>
                    //BOC

                    if (Session["bank_code"].ToString().Trim() == "7010")
                    {
                        //BOC1.Visible = true;
                        //BOC3.Visible = false;
                    }

                    //Pb
                    else if (Session["bank_code"].ToString().Trim() == "7135")
                    {
                        //PB1.Visible = true;
                        //PB3.Visible = false;
                    }
                    else { }
                    
                    //------------------end-------->


                    if (Session["userName_code"].ToString().Substring(Session["userName_code"].ToString().Length - 5) == "admin")
                    {

                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                        //pageSix.Visible = false;
                        //pageEight.Visible = false;
                        //pageNine.Visible = false;
                        //pageeleven.Visible = false;

                        pageFireRenewal.Visible = false;

                        //pagethirteen.Visible = false;
                        //pageOne.Visible = false;
                        //pageTwo.Visible = false;
                        //pageThree.Visible = false;
                        //pagesixteen.Visible = true;
                        //pageseventeen.Visible = false;
                        //pageFireReports.Visible = false;
                        //pageBoardraux.Visible = false;
                        //pageBPF.Visible = false;

                    }
                    else
                    {


                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                        //pageSix.Visible = true;
                        //pageEight.Visible = true;
                        //pageNine.Visible = false;
                        //pageeleven.Visible = false;

                        pageFireRenewal.Visible = false;

                        //pagethirteen.Visible = false;
                        //pageOne.Visible = false;
                        //pageTwo.Visible = false;
                        //pageThree.Visible = false;
                        //pagesixteen.Visible = false;
                        //pageseventeen.Visible = false;
                        //pageFireReports.Visible = false;
                        //pageBoardraux.Visible = false;
                        //pageBPF.Visible = false;
                    }



                    //bankIcon.Attributes.Add("style", " background-image: url('~/Images/BOC_logo.jpg');");

                }

                else
                {

                    if(FireAdmin == "FireAdmin")
                    {
                        //pagesixteen.Visible = true;
                        //pageseventeen.Visible = false;
                        //pageBoardraux.Visible = true;
                        //pageBPF.Visible = false;
                    }

                    else {
                        //pagesixteen.Visible = false;
                        //pageseventeen.Visible = false;
                        //pageBoardraux.Visible = false;
                        //pageBPF.Visible = false;
                    }

                    //BPF CHANGES
                    

                        if (BPFAdmin == "BPFAdmin")
                    {
                        //pageBPF.Visible = true;
                    }

                    else
                    {
                        //pageBPF.Visible = false;
                    }
                    //

                    user_name.InnerHtml = Session["UsrName"].ToString() + " ( " + Session["EPFNum"].ToString().Substring(1) + " )";
                    //user_name.Text = "SEC****";
                    //pageSix.Visible = false;
                    //pageOne.Visible = false;
                    //pageTwo.Visible = false;
                    //pageThree.Visible = false;
                    //pageEight.Visible = false;
                    //pageNine.Visible = true;
                    //pageeleven.Visible = true;
                    //pagethirteen.Visible = true;
                    //pageseventeen.Visible = false;
                    //pageFireReports.Visible = true;
                }

                //redirect page-------->>>
                
               
            }
            else if (!string.IsNullOrEmpty(mainCategory) && mainCategory == "motor")
            {
                //slicInq.Visible = false;
                //bankInq.Visible = false;
                //userManualMotor.Visible = true;
                //userManualFire.Visible = false;

                //bookFire.Visible = false;
                //bookMotor.Visible = true;

                pageFireRenewal.Visible = false;

                if (!string.IsNullOrEmpty(bank))
                {

                    //dropdown help maintance------>
                    //BOC
                    if (Session["bank_code"].ToString().Trim() == "7010")
                    {
                        //BOC4.Visible = true;
                        //BOC5.Visible = true;
                        //BOC7.Visible = true;
                        //BOC8.Visible = true;
                    }
                    //Pb
                    else if (Session["bank_code"].ToString().Trim() == "7135")
                    {
                        //PB4.Visible = true;
                        //PB5.Visible = true;
                        //PB7.Visible = true;
                        //PB8.Visible = true;
                    }
                    else { }

                    //------------------end-------->




                    if (Session["userName_code"].ToString().Substring(Session["userName_code"].ToString().Length - 5) == "admin")
                    {
                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                        //pageOne.Visible = false;
                        //pageTwo.Visible = false;
                        //pageThree.Visible = false;
                        //pageSix.Visible = false;
                        //pageEight.Visible = false;
                        //pageNine.Visible = false;
                        //pageeleven.Visible = false;

                        pageFireRenewal.Visible = false;

                        //pagethirteen.Visible = false;
                        //pagesixteen.Visible = false;
                        //pageseventeen.Visible = true;
                        //pageFireReports.Visible = false;
                        //pageBoardraux.Visible = false;
                        //pageBPF.Visible = false;
                    }
                    else
                    {



                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                        //pageOne.Visible = true;
                        //pageTwo.Visible = true;
                        //pageThree.Visible = false;
                        //pageSix.Visible = false;
                        //pageEight.Visible = false;
                        //pageNine.Visible = false;
                        //pageeleven.Visible = false;

                        pageFireRenewal.Visible = false;

                        //pagethirteen.Visible = false;
                        //pagesixteen.Visible = false;
                        //pageseventeen.Visible = false;
                        //pageFireReports.Visible = false;
                        //pageBoardraux.Visible = false;
                        //pageBPF.Visible = false;
                        //this.GetDetails();
                        //this.GetDetailsUnderWriter();
                    }
                }

                else
                {
                    user_name.InnerHtml = Session["UsrName"].ToString() + " ( " + Session["EPFNum"].ToString().Substring(1) + " )";
                    //user_name.Text = "SEC****";
                    //pageOne.Visible = false;
                    //pageTwo.Visible = false;
                    //pageThree.Visible = true;

                    //pageSix.Visible = false;
                    //pageEight.Visible = false;
                    //pageNine.Visible = false;
                    //pageeleven.Visible = false;

                    pageFireRenewal.Visible = false;

                    //pagethirteen.Visible = false;
                    //pagesixteen.Visible = false;
                    //pageseventeen.Visible = true;
                    //pageFireReports.Visible = false;
                    //pageBoardraux.Visible = false;
                    //pageBPF.Visible = false;

                }
            }
            else
            {
                //userManualMotor.Visible = false;
                //userManualFire.Visible = false;
                //bookFire.Visible = false;
                //bookMotor.Visible = false;
                //dropdownMenuButton.Visible = false;
                if (!string.IsNullOrEmpty(bank))
                {

                    //dropdown help maintance------>
                    //BOC
                    if (Session["bank_code"].ToString().Trim() == "7010")
                    {
                        //BOC4.Visible = false;
                        //BOC5.Visible = false;
                        //BOC7.Visible = false;
                        //BOC8.Visible = false;
                    }
                    //Pb
                    else if (Session["bank_code"].ToString().Trim() == "7135")
                    {
                        //PB4.Visible = false;
                        //PB5.Visible = false;
                        //PB7.Visible = false;
                        //PB8.Visible = false;
                    }
                    else { }

                    //------------------end-------->




                    if (Session["userName_code"].ToString().Substring(Session["userName_code"].ToString().Length - 5) == "admin")
                    {
                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                        //pageOne.Visible = false;
                        //pageTwo.Visible = false;
                        //pageThree.Visible = false;
                        //pageSix.Visible = false;
                        //pageEight.Visible = false;
                        //pageNine.Visible = false;
                        //pageeleven.Visible = false;

                        pageFireRenewal.Visible = false;

                        //pagethirteen.Visible = false;
                        //pagesixteen.Visible = false;
                        //pageseventeen.Visible = false;
                        //pageFireReports.Visible = false;
                        //pageBoardraux.Visible = false;
                        //pageBPF.Visible = false;
                        //slicInq.Visible = false;
                        //bankInq.Visible = true;
                    }
                    else
                    {



                        user_name.InnerHtml = Session["temp_bank"].ToString() + " ( " + Session["temp_branch"].ToString() + " )";
                        //pageOne.Visible = false;
                        //pageTwo.Visible = false;
                        //pageThree.Visible = false;
                        //pageSix.Visible = false;
                        //pageEight.Visible = false;
                        //pageNine.Visible = false;
                        //pageeleven.Visible = false;

                        pageFireRenewal.Visible = false;

                        //pagethirteen.Visible = false;
                        //pagesixteen.Visible = false;
                        //pageseventeen.Visible = false;
                        //pageFireReports.Visible = false;
                        //pageBoardraux.Visible = false;
                        //pageBPF.Visible = false;
                        //slicInq.Visible = false;
                        //bankInq.Visible = true;
                        //this.GetDetails();
                        //this.GetDetailsUnderWriter();
                    }
                }

                else
                {
                    user_name.InnerHtml = Session["UsrName"].ToString() + " ( " + Session["EPFNum"].ToString().Substring(1) + " )";
                    //user_name.Text = "SEC****";
                    //pageOne.Visible = false;
                    //pageTwo.Visible = false;
                    //pageThree.Visible = false;

                    //pageSix.Visible = false;
                    //pageEight.Visible = false;
                    //pageNine.Visible = false;
                    //pageeleven.Visible = false;

                    pageFireRenewal.Visible = false;

                    //pagethirteen.Visible = false;
                    //pagesixteen.Visible = false;
                    //pageseventeen.Visible = false;
                    //pageFireReports.Visible = false;
                    //pageBoardraux.Visible = false;
                    //pageBPF.Visible = false;
                    //slicInq.Visible = true;
                    //bankInq.Visible = false;

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
