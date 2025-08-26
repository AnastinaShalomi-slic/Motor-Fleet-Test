using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using NicDobValidation;

public partial class OdProtect_Bank_ViewPolicy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        Page.Header.DataBind();
        if (!Page.IsPostBack)
        {
            try
            {
                if (Session["bank_code"].ToString() != "")
                {
                    bank_code.Value = Session["bank_code"].ToString();
                    branch_code.Value = Session["branch_code"].ToString();
                    userName_code.Value = Session["userName_code"].ToString();
                    bancass_email.Value = Session["bancass_email"].ToString();
                    this.Initilazation();
                }
                else
                {
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("SE".ToString()));
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
            /* IF PAGE.POSTBACK ASSIGHEN SESSION VARIABLE */
            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
            Session["bank_code"] = bank_code.Value;
            Session["branch_code"] = branch_code.Value;
            Session["userName_code"] = userName_code.Value;
            Session["bancass_email"] = bancass_email.Value;
        }
    }

    protected void Initilazation()
    {
        Execute_sql _sql = new Execute_sql();
        this.Default_Dissable();
        this.SetInitialDates();
        this.GetDetails();
        this.InitializedListSLICCode(ddlSlicCode, "desig", "slic_code", _sql.GetSLIC_Code(Session["bank_code"].ToString(), Session["branch_code"].ToString()), "'slic_code'");
    }

    protected void Default_Dissable()
    {
        txt_Netpremium.Attributes.Add("readonly", "read-only");
        txt_SRCC.Attributes.Add("readonly", "read-only");
        txt_TC.Attributes.Add("readonly", "read-only");
        txtAdminFee.Attributes.Add("readonly", "read-only");
        txtPolicyFee.Attributes.Add("readonly", "read-only");
        tctVat.Attributes.Add("readonly", "read-only");
        txt_sumInsuTotal.Attributes.Add("readonly", "read-only");
        txtBdate.Attributes.Add("readonly", "read-only");
        txt_nic.Attributes.Add("readonly", "read-only");
    }

    protected void SetInitialDates()
    {
        txt_start_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
        txt_end_date.Text = DateTime.Now.ToString("dd/MM/yyyy");
    }


    protected void GetDetails()
    {
        ODP_Transaction oDP_Transaction = new ODP_Transaction();
        Execute_sql _sql = new Execute_sql();

        DataTable resultset = new DataTable();
        try
        {
            resultset = oDP_Transaction.GetRows(_sql.GetODPolicy(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim(), txtNicNo.Text.Trim(), int.Parse(bank_code.Value), int.Parse(branch_code.Value), ddl_status.SelectedValue), resultset, "Read Bank Performed Policy");

            if (oDP_Transaction.Trans_Sucess_State == true)
            {
                if (resultset.Rows.Count > 0)
                {                   
                    Grid_Details.DataSource = resultset;
                    Grid_Details.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Message','Record not Found.','info' );", true);
                    Grid_Details.DataSource = null;
                    Grid_Details.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error','Loading Branch Performed Policy.', 'error');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error','Exception, in Loading Branch Performed Policy.', 'error');", true);
        
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (METH- GetDetails(..) -> - [BANK : " + bank_code.Value + "] - [BRANC : " + branch_code.Value + "]- [BRANC : " + userName_code.Value.Trim() + "]. > ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }

    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_Details.PageIndex = e.NewPageIndex;
        LinkButton rb = (LinkButton)Grid_Details.Rows[e.NewPageIndex].Cells[1].FindControl("imgbtn_ptop");
        rb.Enabled = false;      
        this.GetDetails();
    }

    protected void SelectRecord_Click(object sender, EventArgs e)
    {
        try
        {
            ImageButton btndetails = sender as ImageButton;
            string[] refno = new string[1];
            refno = btndetails.CommandArgument.ToString().Split('/');
            hid_sridPrt.Value = refno[0].ToString().Trim();
            ltEmbed.Text = string.Empty;
            pop_PrintPolicy.Show();
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Exception, In Record Selection.', 'error');", true);
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (EVT- SelectRecord_Click() -> - [BANK : " + bank_code.Value + "] - [BRANC : " + branch_code.Value + "]- [BRANC : " + userName_code.Value.Trim() + "]. > ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }


    protected void ClearText()
    {      
        this.SetInitialDates();
        ddl_status.SelectedIndex = 0;
        Grid_Details.DataSource = null;
        Grid_Details.DataBind();
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }


    protected void btn_find_Click1(object sender, EventArgs e)
    {
        this.GetDetails();
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ImageButton imgBtnEdit = (ImageButton)e.Row.FindControl("imgBtn_Edit");

            if (e.Row.Cells[10].Text == "P")
            {
                e.Row.Cells[11].BackColor = ColorTranslator.FromHtml("#F4F996");
            }

            else if (e.Row.Cells[10].Text == "C")
            {
                e.Row.Cells[11].BackColor = ColorTranslator.FromHtml("#cffae2");
                imgBtnEdit.Enabled = false;
            }

            else if (e.Row.Cells[10].Text == "R")
            {
                e.Row.Cells[11].BackColor = ColorTranslator.FromHtml("#ffcccc");
                imgBtnEdit.Enabled = false;
            }
        }
    }

    protected void imgBtn_Edit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ImageButton btndetails = sender as ImageButton;
            this.GetUserInfo(btndetails.CommandArgument.ToString());           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Exception, In Edit Policy Information.', 'error');", true);
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (EVT- imgBtn_Edit_Click() -> - [BANK : " + bank_code.Value + "] - [BRANC : " + branch_code.Value + "]- [BRANC : " + userName_code.Value.Trim() + "]. > ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }

    protected void InitializedListSLICCode(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {
        ODP_Transaction oDP_Transaction = new ODP_Transaction();
        DataTable resultset = new DataTable();
        try
        {
            resultset = oDP_Transaction.GetRows(executor, resultset, "Initialization of Service Personal");

            if (oDP_Transaction.Trans_Sucess_State == true)
            {
                if (resultset.Rows.Count > 1)
                {
                    target_list.DataSource = resultset;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                }

                else
                {
                    target_list.DataSource = null;
                    target_list.DataBind();
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error','Initialization of Service Personal.', 'error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Initialization of Service Personal.', 'error');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Exception, In Initialization Service Personal.', 'error');", true);

            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (METH- InitializedListSLICCode(..) -> - [BANK : " + bank_code.Value + "] - [BRANC : " + branch_code.Value + "]- [BRANC : " + userName_code.Value.Trim() + "]. > ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }

    protected void GetUserInfo(string proposalNo)
    {
        ODP_Transaction getRecord = new ODP_Transaction();
        ODP_PropsalUpdate userProfile = new ODP_PropsalUpdate();
        ODP_DQL oDP_DQL = new ODP_DQL();
        try
        {
            List<ODP_PropsalUpdate> profile = getRecord.GetUserProfile(oDP_DQL.GetCustomerODP_Profile(proposalNo));

            if (getRecord.Trans_Sucess_State == true)
            {
                if (profile.Count > 0)
                {
                    hid_ProposalNo.Value = profile[0].out_prno;
                    hid_Srid.Value = profile[0].out_srid;
                    ddlInitials.SelectedValue = profile[0].out_title;
                    txt_CusName.Text = profile[0].out_cus_name;
                    txt_addline1.Text = profile[0].out_add_l1;
                    txt_addline2.Text = profile[0].out_add_l2;
                    txt_addline3.Text = profile[0].out_add_l3;
                    txt_addline4.Text = profile[0].out_add_l4;
                    txtBdate.Text = profile[0].out_dob;
                    txt_nic.Text = profile[0].out_nic;
                    txt_tele.Text = profile[0].out_connomob;
                    txt_email.Text = profile[0].out_e_mail;
                    txtBusType.Text = profile[0].out_btupe;
                    txt_odLimit.Text = profile[0].out_suminsurd.ToString("N2");
                    txt_Netpremium.Text = profile[0].out_netpremium.ToString("N2");
                    txt_SRCC.Text = profile[0].out_srcc.ToString("N2");
                    txt_TC.Text = profile[0].out_tc.ToString("N2");
                    txtAdminFee.Text = profile[0].out_admin_fee.ToString("N2");
                    txtPolicyFee.Text = profile[0].out_policy_fee.ToString("N2");
                    tctVat.Text = profile[0].out_vat.ToString("N2");
                    txt_sumInsuTotal.Text = profile[0].out_tot_premium.ToString("N2");
                    mpeEditProposal.Show();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Policy Information Not Found.', 'error');", true);
                }
            }

            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Error in reading policy information.', 'error');", true);
            }
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error',  'Exception in reading policy information.', 'error');", true);

            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (METH- GetUserInfo(..)) -> - [BANK : " + bank_code.Value + "] - [BRANC : " + branch_code.Value + "]- [BRANC : " + userName_code.Value.Trim() + "]. > ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }


    protected void btnUpload_Proposal_Click(object sender, EventArgs e)
    {
        ODPEntry oDPEntry = new ODPEntry();
        ODP_Transaction oDP_Transaction = new ODP_Transaction();
        try
        {
            oDPEntry.PIN_PRNO = hid_ProposalNo.Value.Trim();
            oDPEntry.PIN_SRID = hid_Srid.Value.Trim();
            oDPEntry.PIN_TITLE = ddlInitials.SelectedItem.Value;
            oDPEntry.PIN_CUS_NAME = txt_CusName.Text.Trim().ToUpper(); ;
            oDPEntry.PIN_ADD_L1 = txt_addline1.Text.Trim();
            oDPEntry.PIN_ADD_L2 = txt_addline2.Text.Trim();
            oDPEntry.PIN_ADD_L3 = txt_addline3.Text.Trim();
            oDPEntry.PIN_ADD_L4 = txt_addline4.Text.Trim();
            oDPEntry.PIN_DOB = DateTime.ParseExact(txtBdate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            oDPEntry.PIN_NIC = txt_nic.Text.Trim();
            oDPEntry.PIN_CONNOMOB = int.Parse(txt_tele.Text.Trim());
            oDPEntry.PIN_E_MAIL = txt_email.Text.Trim();
            oDPEntry.PIN_BTUPE = txtBusType.Text.Trim();
            oDPEntry.PIN_SERBR = int.Parse("10");
            oDPEntry.PIN_ENTDBY = userName_code.Value.Trim();
            oDPEntry.PIN_SUMINSURD = double.Parse(txt_odLimit.Text.Trim());
            oDPEntry.PIN_NETPREMIUM = double.Parse(txt_Netpremium.Text.Trim());
            oDPEntry.PIN_SRCC = double.Parse(txt_SRCC.Text.Trim());
            oDPEntry.PIN_TC = double.Parse(txt_TC.Text.Trim());
            oDPEntry.PIN_ADMIN_FEE = double.Parse(txtAdminFee.Text.Trim());
            oDPEntry.PIN_POLICY_FEE = double.Parse(txtPolicyFee.Text.Trim());
            oDPEntry.PIN_VAT = double.Parse(tctVat.Text.Trim());
            oDPEntry.PIN_TOT_PREMIUM = double.Parse(txt_sumInsuTotal.Text.Trim());

            bool sucess = oDP_Transaction.UpdateProposal(oDPEntry);

            if (sucess == true)
            {             
                this.GetDetails();
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Success', 'Policy Updation Sucessfully Completed.', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Error in update policy information.', 'error');", true);

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Exception, in update policy information.', 'error');", true);

            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (EVT- btnUpload_Proposal_Click(..)) -> - [BANK : " + bank_code.Value + "] - [BRANC : " + branch_code.Value + "]- [BRANC : " + userName_code.Value.Trim() + "]. > ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }
   
    protected void btn_PrintPayAdvice_Click(object sender, EventArgs e)
    {
        ltEmbed.Text = string.Empty;
        try
        {
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"100%\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            ltEmbed.Text = string.Format(embed, ResolveUrl("~/OdProtect/Common/PaymentAdvice.aspx?srid=" + hid_sridPrt.Value.Trim()));
            pop_PrintPolicy.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Exception', 'In Printing Payment Advice / Policy Shadule.', 'error');", true);
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (EVT- btn_PrintPayAdvice_Click()) -> - [USER : " + UserId.Value + "] - [BRANCH : " + brCode.Value + "]> ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }


    protected void btn_PrintPolicy_Click(object sender, EventArgs e)
    {
        ltEmbed.Text = string.Empty;
        try
        {
            string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"100%\">";
            embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
            embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
            embed += "</object>";
            ltEmbed.Text = string.Format(embed, ResolveUrl("~/OdProtect/Common/PrintPolicyShadule.aspx?srid=" + hid_sridPrt.Value.Trim()));
            pop_PrintPolicy.Show();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Exception, In Printing Payment Advice / Policy Shadule.', 'error');", true);

            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (EVT- btn_PrintPolicy_Click()) -> - [USER : " + UserId.Value + "] - [BRANCH : " + brCode.Value + "]> ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }
}