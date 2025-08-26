using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RenewalNoticeWebDownload : System.Web.UI.Page
{
    Execute_sql _sql = new Execute_sql();
    FireRnNote_pdf pdfPrint = new FireRnNote_pdf();
    protected void Page_Load(object sender, EventArgs e)
    {
        string policyNo = Request.QueryString["polno"];
        string mobileNo = Request.QueryString["mobileNo"];
        string key = Request.QueryString["shortKey"];

        var en = new EncryptDecrypt();

        otpSentLbl.Visible = false;
        otpSentLbl.Text = "";
        lblMessage.Visible = false;
        lblMessage.Text = "";

        if (!Page.IsPostBack)
        {
            if (!string.IsNullOrEmpty(mobileNo))
            {
                mobileNo = en.Decrypt(mobileNo);
            }

            if (!string.IsNullOrEmpty(policyNo) && !string.IsNullOrEmpty(mobileNo))
            {
                bool result = generateAndSendOTP(mobileNo, key, en.Decrypt(policyNo));

                if (result)
                {
                    otpSentLbl.Visible = true;
                    otpSentLbl.Text = "OTP sent your mobile number. Verify that ";
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Invalid OTP";
                }



                // Optional redirect after download (use JS on client side)
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "❌ An error occurred. Please contact the head office.";
                lblMessage.ForeColor = System.Drawing.Color.Red;


            }
        }
        else
        {

        }


    }

    public bool generateAndSendOTP(string mobileNo, string shortKey, string policyNo)
    {

        string otp = new Random().Next(100000, 999999).ToString();
        DateTime otpExpiry = DateTime.Now.AddMinutes(5); // 5-minute expiry

        bool updateOtpandSend = _sql.UpdateOtpandSMSSend(shortKey, mobileNo, otp, otpExpiry, policyNo);


        return updateOtpandSend;
    }

    protected void btnVerify_Click(object sender, EventArgs e)
    {
        string key = Request.QueryString["shortKey"];
        string enteredOtp = txtOTP.Text.Trim();

        var en = new EncryptDecrypt();

        using (var con = new OracleConnection(ConfigurationManager.ConnectionStrings["CONN_STRING_ORCL"].ConnectionString))
        {
            con.Open();
            string query = @"
            SELECT LongUrl 
            FROM SLIC_CNOTE.shorturls 
            WHERE ShortKey = :key AND OTP = :otp AND OTP_EXPIRY >= SYSDATE";

            using (var cmd = new OracleCommand(query, con))
            {
                cmd.Parameters.Add("key", key);
                cmd.Parameters.Add("otp", enteredOtp);

                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    // Invalidate OTP if it's one-time use:
                    var clearOtp = new OracleCommand("UPDATE SLIC_CNOTE.shorturls SET OTP = NULL WHERE ShortKey = :key", con);
                    clearOtp.Parameters.Add("key", key);
                    int res = clearOtp.ExecuteNonQuery();

                    if (res <= 0)
                    {

                    }
                    else
                    {
                        otpSentLbl.Visible = false;
                        otpSentLbl.Text = "";
                        lblMessage.Visible = false;
                        lblMessage.Text = "";

                        

                        string policyNo = Request.QueryString["polno"];

                        pdfPrint.print_policy(en.Decrypt(policyNo));

                        pnlOTP.Visible = false;
                        pnlSuccess.Visible = true;

                    }

                    
                }
                else
                {
                    lblMessage.Visible = true;
                    lblMessage.Text = "Invalid or expired OTP.";
                }
            }
        }
    }

    protected void btnResend_Click(object sender, EventArgs e)
    {
        otpSentLbl.Visible = false;
        otpSentLbl.Text = "";
        lblMessage.Visible = false;
        lblMessage.Text = "";

        string policyNo = Request.QueryString["polno"];
        string mobileNo = Request.QueryString["mobileNo"];
        string key = Request.QueryString["shortKey"];

        var en = new EncryptDecrypt();

        if (!string.IsNullOrEmpty(mobileNo))
        {
            mobileNo = en.Decrypt(mobileNo);
        }

        if (!string.IsNullOrEmpty(policyNo) && !string.IsNullOrEmpty(mobileNo))
        {
            bool result = generateAndSendOTP(mobileNo, key, en.Decrypt(policyNo));

            if (result)
            {
                otpSentLbl.Visible = true;
                otpSentLbl.Text = "OTP sent your mobile number. Verify that ";
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Invalid OTP";
            }
        }

            
    }

}