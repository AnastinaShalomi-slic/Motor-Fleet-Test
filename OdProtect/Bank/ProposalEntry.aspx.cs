using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web;
using System.Linq;
using NicDobValidation;

public partial class OdProtect_Bank_ProposalEntry : System.Web.UI.Page
{
    string Auth = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        Execute_sql _sql = new Execute_sql();
        Page.Header.DataBind();

        if (!Page.IsPostBack)
        {
            try
            {                
                string propType = string.Empty;
                var en = new EncryptDecrypt();
           
                    if (Session["bank_code"].ToString() != "")
                    {                       
                        bank_code.Value = Session["bank_code"].ToString();
                        branch_code.Value = Session["branch_code"].ToString();
                        userName_code.Value = Session["userName_code"].ToString();
                        bancass_email.Value = Session["bancass_email"].ToString();
                        auth_Code.Value = Session["Auth_Code"].ToString();

                        this.InitializedListSLICCode(ddlSlicCode, "desig", "slic_code", _sql.GetSLIC_Code(Session["bank_code"].ToString(), Session["branch_code"].ToString()), "'slic_code'");
                        this.Default_Dissable();
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
            Session["bank_code"] = bank_code.Value;
            Session["branch_code"] = branch_code.Value;
            Session["userName_code"] = userName_code.Value;
            Session["bancass_email"] = bancass_email.Value;
            Session["Auth_Code"] = auth_Code.Value;
        }
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
    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        litCusName.Text = (ddlInitials.SelectedValue + txt_CusName.Text.Trim()).ToUpper();

        List<string> address = new List<string>();
        address.Add(txt_addline1.Text.Trim());
        address.Add(txt_addline2.Text.Trim());
        address.Add(txt_addline3.Text.Trim());
        address.Add(txt_addline4.Text.Trim());
        string list = string.Join(",<br/> ", address.Where(x => !string.IsNullOrEmpty(x)));
        litProposalNo.Text = list + ".";

        litDob.Text = txt_DOB.Text;
        litNic.Text = txt_nic.Text.Trim();
        litMobileNo.Text = txt_tele.Text.Trim();
        litEmail.Text = txt_email.Text.Trim();
        litBtype.Text = txtBusType.Text.Trim();
        litvbf.Text = txt_odLimit.Text.Trim();
        litTotPremium.Text = txt_sumInsuTotal.Text.Trim();

        if (cvalNicMatchDob.IsValid)
        {
            popupConProposal.Show();
            cvalNicMatchDob.Text = string.Empty;
        }

        else
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error','NIC does not matched with date of birth. Please check', 'error');", true);
    }

    protected void Validate_Numeric(object sender, ServerValidateEventArgs e)
    {
        NicDobValidation.NicDobValidation nicDobValidation = new NicDobValidation.NicDobValidation();
        bool validated = nicDobValidation.checkNICWithDob(txt_DOB.Text.Trim(), txt_nic.Text.Trim());

        if (validated == true)
            e.IsValid = true;
        else
            e.IsValid = false;
    }


    /* [System.Web.Services.WebMethod]
     public static string CheckEmail(string useroremail, string userMobile)
     {
         string bank = HttpContext.Current.Session["temp_bank"].ToString();
         string branch = HttpContext.Current.Session["temp_branch"].ToString();

         string retval = "";
         string result = "";

         using (OracleConnection conn = new OracleConnection())
         {
             conn.ConnectionString = ConfigurationManager.ConnectionStrings["CONN_STRING_ORCL"].ConnectionString;
             conn.Open();
             using (OracleCommand cmd = new OracleCommand("SLICCOMMON.PROC_AML_ONBOARD_LOG", conn))
             {
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("sysName", "BANCASSUARANCE");
                 cmd.Parameters.AddWithValue("brCode", bank + " - " + branch);
                 cmd.Parameters.AddWithValue("nicNumber", useroremail);
                 cmd.Parameters.AddWithValue("passportNumber", "");
                 cmd.Parameters.AddWithValue("polNumber", "");
                 cmd.Parameters.AddWithValue("phoneNumber", userMobile);

                 cmd.Parameters.Add("responseVal", OracleType.VarChar, 200).Direction = ParameterDirection.Output;
                 OracleDataReader dr = cmd.ExecuteReader();
                 result = cmd.Parameters["responseVal"].Value.ToString();
                 dr.Close();
             }
             conn.Close();
         }

         if (result == "AML Listed Person")
             retval = "true";

         else
             retval = "false";

         return retval;
     }*/

    protected void InitializedListSLICCode(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {
        ODP_Transaction oDP_Transaction = new ODP_Transaction();
        DataTable resultset= new DataTable();
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
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Initialization Service Personal.[No Record Found.]', 'error');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Initialization of Service Personal.[ORCL]', 'error');", true);               
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

    protected void ConfirmProposal_Click(object sender, EventArgs e)
    {
        ODPEntry oDPEntry = new ODPEntry();
        ODP_Transaction oDP_Transaction = new ODP_Transaction();
        ltEmbed.Text = string.Empty;
        try
        {
            oDPEntry.PIN_TITLE = ddlInitials.SelectedItem.Value;
            oDPEntry.PIN_CUS_NAME = txt_CusName.Text.Trim().ToUpper();
            oDPEntry.PIN_ADD_L1 = txt_addline1.Text.Trim();
            oDPEntry.PIN_ADD_L2 = txt_addline2.Text.Trim();
            oDPEntry.PIN_ADD_L3 = txt_addline3.Text.Trim();
            oDPEntry.PIN_ADD_L4 = txt_addline4.Text.Trim();
            oDPEntry.PIN_DOB = DateTime.ParseExact(txt_DOB.Text.Trim(), "yyyy/MM/dd", CultureInfo.InvariantCulture);
            oDPEntry.PIN_NIC = txt_nic.Text.Trim();
            oDPEntry.PIN_CONNOMOB = int.Parse(txt_tele.Text.Trim());
            oDPEntry.PIN_E_MAIL = txt_email.Text.Trim();
            oDPEntry.PIN_BTUPE = txtBusType.Text.Trim();
            oDPEntry.PIN_SERBR = int.Parse("10");
            oDPEntry.PIN_BANK_CODE = int.Parse(bank_code.Value);
            oDPEntry.PIN_BRANCH_CODE = int.Parse(branch_code.Value);
            oDPEntry.PIN_ENTDBY = userName_code.Value.Trim();
            oDPEntry.PIN_MECODE = int.Parse(ddlSlicCode.SelectedValue.Trim());
            oDPEntry.PIN_SUMINSURD = double.Parse(txt_odLimit.Text.Trim());
            oDPEntry.PIN_NETPREMIUM = double.Parse(txt_Netpremium.Text.Trim());
            oDPEntry.PIN_SRCC = double.Parse(txt_SRCC.Text.Trim());
            oDPEntry.PIN_TC = double.Parse(txt_TC.Text.Trim());
            oDPEntry.PIN_ADMIN_FEE = double.Parse(txtAdminFee.Text.Trim());
            oDPEntry.PIN_POLICY_FEE = double.Parse(txtPolicyFee.Text.Trim());
            oDPEntry.PIN_VAT = double.Parse(tctVat.Text.Trim());
            oDPEntry.PIN_TOT_PREMIUM = double.Parse(txt_sumInsuTotal.Text.Trim());

            string srid = oDP_Transaction.AddProposal(oDPEntry);

            if (srid != "#")
            {
                string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"100%\">";
                embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
                embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
                embed += "</object>";
                ltEmbed.Text = string.Format(embed, ResolveUrl("~/OdProtect/Common/PaymentAdvice.aspx?srid=" + srid));

                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallFunction", "CleareProposal()", true);
                pop_PrintAdvice.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error','Error, In Proposal Submission.[ORCL]', 'error');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Exception, In Proposal Submission.', 'error');", true);

            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (EVT- ConfirmProposal_Click()) -> - [BANK : " + bank_code.Value + "] - [BRANC : " + branch_code.Value + "]- [BRANC : " + userName_code.Value.Trim() + "]. > ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }

    protected void txt_nic_TextChanged(object sender, EventArgs e)
    {
        cvalNicMatchDob.Validate();
    }
}