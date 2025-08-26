using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class OdProtect_BackOffice_ViewPolicy : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        Page.Header.DataBind();
        if (!Page.IsPostBack)
        {
            try
            {
                if (!String.IsNullOrEmpty(Session["EPFNum"].ToString()) && !String.IsNullOrEmpty(Session["brcode"].ToString()))
                {
                    UserId.Value = Session["EPFNum"].ToString();
                    brCode.Value = Session["brcode"].ToString();
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
            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;          
        }
    }

    protected void Initilazation()
    {
        this.Default_Dissable();
        this.SetInitialDates();
        this.GetDetails();
    }

    protected void Default_Dissable()
    {
        ddl_status.Attributes.Add("disabled", "disabled");
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

        DataTable details = new DataTable();
        try
        {
            details = oDP_Transaction.GetRows(_sql.GetODPolicy_BackProcessView(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim(), txtNicNo.Text.Trim(), ddl_status.SelectedValue), details, "Loading Overdraft Policy");

            if (oDP_Transaction.Trans_Sucess_State == true)
            {
                if (details.Rows.Count > 0)
                {
                    Grid_Details.DataSource = details;
                    Grid_Details.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Message', 'No Record Found.', 'info');", true);
                    Grid_Details.DataSource = null;
                    Grid_Details.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Loading Overdraft Policy.[ORCL]', 'error');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Ecxeption', 'In Loading Overdraft Policy.', 'error');", true);
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (METH- GetDetails(..) -> - [USER : " + UserId.Value + "] - [BRANCH : " + brCode.Value + "]> ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }

    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_Details.PageIndex = e.NewPageIndex;       
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
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Exception', 'In Record Selection.', 'error');", true);
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (EVT- SelectRecord_Click() -> - [USER : " + UserId.Value + "] - [BRANCH : " + brCode.Value + "]> ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }

    protected void ClearText()
    {
        txt_end_date.Text = string.Empty;
        txt_start_date.Text = string.Empty;
        txt_req_id.Text = string.Empty;
        ddl_status.SelectedIndex = 0;
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }

    protected void btn_find_Click1(object sender, EventArgs e)
    {
        this.GetDetails();
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