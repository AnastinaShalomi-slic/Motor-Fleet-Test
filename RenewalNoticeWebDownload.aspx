<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RenewalNoticeWebDownload.aspx.cs" Inherits="RenewalNoticeWebDownload" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Enter OTP</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@4.6.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .otp-box {
            max-width: 400px;
            margin: 100px auto;
            padding: 30px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            border-radius: 10px;
            background-color: #fff;
        }

        .form-control {
            font-size: 1.5rem;
            text-align: center;
            letter-spacing: 0.3rem;
        }
    </style>
</head>
<body class="bg-light">
    <form id="form1" runat="server">
        <div class="otp-box text-center" ID="pnlOTP" runat="server">
            <h6 class="mb-4">Enter the 6-digit OTP For Download Renewal Notice</h6>
            <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control" MaxLength="6" required="true" placeholder="------"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvOtp" runat="server" ControlToValidate="txtOTP"
                ErrorMessage="OTP is required" CssClass="text-danger d-block mt-2" Display="Dynamic"></asp:RequiredFieldValidator>

            <div class="mt-4">
                <asp:Button ID="btnSubmit" runat="server" Text="Verify OTP" CssClass="btn btn-primary btn-block" OnClick="btnVerify_Click" />
            </div>

            <div class="mt-3">
                <asp:LinkButton ID="btnResend" runat="server" Text="Resend OTP" CssClass="btn btn-link" OnClick="btnResend_Click" />
            </div>

            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger d-block mt-2" />
            <asp:Label ID="otpSentLbl" runat="server" ForeColor="Green" CssClass=" d-block mt-2" />
        </div>

        <asp:Panel ID="pnlSuccess" runat="server" CssClass="otp-box text-center" Visible="false">
            <h4 class="text-success">✅ OTP Verified Successfully</h4>
            <p>Your document is now downloading...</p>
        </asp:Panel>
    </form>
</body>
</html>

<%--<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="txtOtp" runat="server" Placeholder="Enter OTP"></asp:TextBox>
        <asp:Button ID="btnVerify" runat="server" Text="Verify OTP" OnClick="btnVerify_Click" />
        <asp:Label ID="otpError" runat="server" ForeColor="Red" />
            <asp:Label ID="otpSentLbl" runat="server" ForeColor="Green" />
        <div>
            <asp:Label ID="lblMessage" runat="server" CssClass="message-label" ForeColor="Green"></asp:Label>
        </div>
        </div>
    </form>
</body>
</html>--%>
