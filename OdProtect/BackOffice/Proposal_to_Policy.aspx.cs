using System;
using System.Web.UI;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net.Mail;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Drawing;
using Font = iTextSharp.text.Font;
using Color = iTextSharp.text.Color;
using System.Linq;

public partial class OdProtect_BackOffice_Proposal_to_Policy : System.Web.UI.Page
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
                    this.Initalization();
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

    protected void Initalization()
    {
        this.Default_Dissable();
        this.SetInitialDates();
        this.GetDetails();
    }

    protected void Default_Dissable()
    {
        txtp_CusName.Attributes.Add("readonly", "read-only");
        txtp_ProposalNo.Attributes.Add("readonly", "read-only");
        txtp_SumInsured.Attributes.Add("readonly", "read-only");
        txtp_TPremium.Attributes.Add("readonly", "read-only");
        ddl_status.Attributes.Add("disabled", "disabled");
        txtp_NIC.Attributes.Add("disabled", "disabled");
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
            resultset = oDP_Transaction.GetRows(_sql.GetODPolicy_BackProcess(txt_start_date.Text.Trim(), txt_end_date.Text.Trim(), txt_req_id.Text.Trim(), txtNicNo.Text.Trim(), ddl_status.SelectedValue), resultset, "Loading Proposal(Branch)");

            if (oDP_Transaction.Trans_Sucess_State == true)
            {
                if (resultset.Rows.Count > 0)
                {
                    Grid_Details.DataSource = resultset;
                    Grid_Details.DataBind();         
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Message','No Record Found.', 'info');", true);
                    Grid_Details.DataSource = null;
                    Grid_Details.DataBind();
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message( 'Error', 'Loading Pending Proposal.[ORCL]', 'error');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Exception','Loading Proposal.', 'error');", true);
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
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Exception', 'In Selection Record.', 'error');", true);
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (EVT- SelectRecord_Click()) -> - [USER : " + UserId.Value + "] - [BRANCH : " + brCode.Value + "]> ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
        }
    }

    protected void ClearText()
    {      
        txt_req_id.Text = string.Empty;
        txtNicNo.Text = string.Empty;
        ddl_status.SelectedIndex = 1;
        this.SetInitialDates();
        this.GetDetails();
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
            LinkButton lnkBtnPtoP = (LinkButton)e.Row.FindControl("imgbtn_ptop");

            if (e.Row.Cells[10].Text == "P")
            {
                e.Row.Cells[11].BackColor = ColorTranslator.FromHtml("#F4F996");
            }

            else if (e.Row.Cells[10].Text == "C")
            {
                e.Row.Cells[11].BackColor = ColorTranslator.FromHtml("#cffae2");
                lnkBtnPtoP.Enabled = false;
            }

            else if (e.Row.Cells[10].Text == "R")
            {
                e.Row.Cells[11].BackColor = ColorTranslator.FromHtml("#ffcccc");
            }
        }
    }

    protected void btnPtP_Click(object sender, EventArgs e)
    {
        ODP_ProtoPolicy oDPEntry = new ODP_ProtoPolicy();
        ODP_Transaction oDP_Transaction = new ODP_Transaction();
        try
        {
            oDPEntry.PIN_PRNO = txtp_ProposalNo.Text.Trim();
            oDPEntry.PIN_PAYRECIPT_NO = txtP_ReciptNo.Text.Trim();
            //oDPEntry.PIN_BANK_CODE = int.Parse(bank_code.Value);
            //oDPEntry.PIN_BRANCH_CODE = int.Parse(branch_code.Value);

            if (UserId.Value.Trim().Substring(0, 1) == "0")
                oDPEntry.PIN_ENTDBY = "SEC" + UserId.Value.Trim().Substring(1);
            else
                oDPEntry.PIN_ENTDBY = UserId.Value.Trim();

            bool sucess = oDP_Transaction.Proposalto_Policy(oDPEntry);
            if (sucess == true)
            {
                this.SendPolicyShadule(hid_sridPrt.Value);        
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Success', 'Proposal Converted to Policy.', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Error', 'Proposal Convert to Policy', 'error');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "Message('Exception', 'Proposal Convert to Policy', 'error');", true);
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (EVT- btnPtP_Click()) -> - [USER : " + UserId.Value + "] - [BRANCH : " + brCode.Value + "]> ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);
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

    public void SendPolicyShadule(string refId)
    {
        ODP_Transaction getRecord = new ODP_Transaction();
        ODP_DQL oDP_DQL = new ODP_DQL();

        Font fnt_figur = FontFactory.GetFont("Arial", 10, Font.NORMAL, new Color(32, 32, 32));
        Font fnt_summery_1 = FontFactory.GetFont("Arial", 8, Font.NORMAL, new Color(32, 32, 32));
        try
        {
            string body = string.Empty;

            using (System.IO.StreamReader reader = new System.IO.StreamReader(Server.MapPath("~/OdProtect/eMail/mail_body/od_cus_email.html")))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{CUSTOMER_NAME}", txtp_CusName.Text.ToUpper());
            body = body.Replace("{NIC}", txtp_NIC.Text);
            body = body.Replace("{SYSTEM_DATE}", DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss tt"));

            byte[] bytes = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/OdProtect/Common/Template/od_policy.pdf"));

            System.Collections.Generic.List<ODP_PolicyPrint> printpolicy = getRecord.GetPolicyPrintInfo(oDP_DQL.GetPolicyPrintInfo(refId));

            if (getRecord.Trans_Sucess_State == true)
            {
                if (printpolicy.Count > 0)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        PdfReader reader = new PdfReader(bytes);
                        PdfStamper stamper = new PdfStamper(reader, stream);

                        int pages = reader.NumberOfPages;
                        for (int i = 1; i <= pages; i++)
                        {
                            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(i.ToString(), fnt_figur), 568f, 15f, 0);

                            if (i == 3)
                                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_CENTER, new Phrase("This is system generated print and therefore requires no authorized signature.", fnt_figur), 300f, 35f, 0);
                        }

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_polno, fnt_figur), 405f, 685f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_cus_name.ToUpper(), fnt_figur), 208f, 553f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_nic, fnt_figur), 208f, 531f, 0);

                        System.Collections.Generic.List<string> address = new System.Collections.Generic.List<string>();
                        address.Add(printpolicy[0].out_add_l1.Trim());
                        address.Add(printpolicy[0].out_add_l2.Trim());
                        address.Add(printpolicy[0].out_add_l3.Trim());
                        address.Add(printpolicy[0].out_add_l4.Trim());
                        string list = string.Join(", ", address.Where(x => !string.IsNullOrEmpty(x)));

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(list + ".", fnt_figur), 208f, 513f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_bbrnch, fnt_summery_1), 208f, 478f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase("Rs." + printpolicy[0].out_suminsurd.ToString("N2"), fnt_summery_1), 208f, 465f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_policy_sdate, fnt_summery_1), 235f, 447f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_policy_edate, fnt_summery_1), 302f, 447f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_policy_rendate, fnt_figur), 138f, 428f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 185f, 409f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase("Rs." + printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 272f, 374f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase("Rs." + printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 272f, 356f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase("Rs." + printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 272f, 339f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_netpremium.ToString("N2"), fnt_figur), 515f, 397f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_admin_fee.ToString("N2"), fnt_figur), 515f, 384f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_policy_fee.ToString("N2"), fnt_figur), 515f, 371f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_vat.ToString("N2"), fnt_figur), 515f, 358f, 0);

                        double caltot_Premium = (printpolicy[0].out_netpremium + printpolicy[0].out_admin_fee + printpolicy[0].out_policy_fee + printpolicy[0].out_vat);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(caltot_Premium.ToString("N2"), fnt_figur), 515f, 341f, 0);

                        stamper.Close();
                        bytes = stream.ToArray();
                    }

                    this.SendHtmlFormattedEmail(body, bytes, hidMailBranch.Value, txtp_CusName.Text.ToUpper(), txtp_NIC.Text);
                
                }
                else
                {
                    //Policy Infomation Not Found
                    ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "message('Error Message', 'Policy information not found. Please contact SLIC-GI(IT) Department', 'swal-modal-error','swal-button-error', 1, 1);", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "message('Error Message', '[ORCL] Reading policy information. Please contact SLIC-GI(IT) Department', 'swal-modal-error','swal-button-error', 1, 1);", true);
            }
        }
        catch (Exception ex)
        {
            ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
            string Error = "Exception :: Request  - (METH- SendPolicyShadule(.. para)) -> - [USER : " + UserId.Value + "] - [BRANCH : " + brCode.Value + "]> ";
            oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);

            ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "message('Error Message', 'Exception occue in writing policy schedule.', 'swal-modal-error','swal-button-error', 1, 1);", true);
        }

    }

    protected void SendHtmlFormattedEmail(string body, byte[] bytes, string branch_code, string customer_name, string nic)
    {
        string target_email = "boc" + branch_code + "@boc.lk";
        //string target_email = "sanjeewar@srilankainsurance.com";
        Regex pattern = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");

        if (pattern.IsMatch(target_email))
        {
            try
            {
                MailMessage message = new MailMessage();

                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");      
                message.AlternateViews.Add(htmlView);

                MailAddress fromAddress = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["sent_mail"]);
                message.From = fromAddress;
                message.Subject = "OD Protector policy of "+ customer_name + " (" + nic + ")";
                message.To.Add(target_email);
                message.Body = body;

                message.IsBodyHtml = true;
                message.Priority = MailPriority.High;

                SmtpClient sendClient = new SmtpClient();
                sendClient.Host = System.Configuration.ConfigurationManager.AppSettings["Host"];
                sendClient.Port = int.Parse(System.Configuration.ConfigurationManager.AppSettings["Port"]);

                sendClient.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = ConfigurationManager.AppSettings["sent_mail"];
                NetworkCred.Password = ConfigurationManager.AppSettings["sent_pwd"];
                sendClient.UseDefaultCredentials = true;
                sendClient.Credentials = NetworkCred;
                message.BodyEncoding = System.Text.Encoding.GetEncoding("utf-8");

                string set_attachment_name = txtp_ProposalNo.Text.Trim();
                message.Attachments.Add(new Attachment(new System.IO.MemoryStream(bytes), set_attachment_name+".pdf"));
               
                ODP_MailLog log_tr = new ODP_MailLog();
                log_tr.write_log(txtp_ProposalNo.Text + " ::: " + target_email + " ::: " + " ->[ORG-E-Mail] ::: " + System.Configuration.ConfigurationManager.AppSettings["sent_mail"] + " ::: " + DateTime.Now.ToString("dd:MM:yyyy HH:mm:ss tt") + " ::: " + UserId.Value, "ODP");

                sendClient.Send(message);

                this.GetDetails();
            }
            catch (Exception ex)
            {
                ODP_LogExcp oDP_LogExcp = new ODP_LogExcp();
                string Error = "Exception :: Request  - (METH- SendHtmlFormattedEmail()) -> - [USER : " + UserId.Value + "] - [BRANCH : " + brCode.Value + "]> ";
                oDP_LogExcp.WriteLog(Error + ex.Message + Environment.NewLine + System.Web.HttpContext.Current.Request.Url.AbsolutePath + Environment.NewLine);

                ScriptManager.RegisterStartupScript(this, GetType(), "Popup", "message('Error Message', 'Exception occue in email sending.', 'swal-modal-error','swal-button-error', 1, 1);", true);
            }
        }
    }

}