<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" async="true" CodeFile="DebitNote.aspx.cs" Inherits="Bank_Fire_DebitNote" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .bordered {
            border: 1px solid black; /* Adjust the border style, width, and color as needed */
            padding: 2px; /* Adjust padding if necessary */
        }

        .courier-font {
            font-family: 'Courier New', Courier, monospace;
        }
    </style>

       <style>
        .bordered {
            border: 1px solid black; /* Adjust the border style, width, and color as needed */
            padding: 2px; /* Adjust padding if necessary */
        }

        .courier-font {
            font-family: 'Courier New', Courier, monospace;
        }

        /* New CSS for the border line */
        .border-line {
            border-top: 2px solid black; /* Adjust thickness and color if needed */
            margin-top: 20px; /* Space above the line */
            margin-bottom: 20px; /* Space below the line */
        }

        /* Center image within table cell */
        .center-image {
            text-align: center;
        }

        .dashed-line {
            border-top: 1px dashed black;
            width: 100%;
            margin: 10px 0;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"></asp:ToolkitScriptManager>

    <!-- Hidden Fields -->
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="Less_text" runat="server" />
    <asp:HiddenField ID="Reson_temp" runat="server" />
    <asp:HiddenField ID="app_date" runat="server" />
    <asp:HiddenField ID="reqDate" runat="server" />
    <asp:HiddenField ID="HiddenFieldRemark" runat="server" />
    <asp:HiddenField ID="bank_code" runat="server" />
    <asp:HiddenField ID="statusHidden" runat="server" />
    <asp:HiddenField ID="user_epf" runat="server" />
    <asp:HiddenField ID="req_status" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />
    <asp:HiddenField ID="hfRefId" runat="server" />
    <asp:HiddenField ID="hfSumInsu" runat="server" />
    <asp:HiddenField ID="hfFlag" runat="server" />

    <div class="container" id="mainDiv" runat="server" style="border: 1.5px solid #00adba; padding: 5px; margin: 20px auto; width: 100%; max-width: 1000px;">
        <!-- Button -->
        <div class="form-group row mt-1">
            <div class="col-sm-12 text-right">
                <asp:Button ID="btPDF" runat="server" Text="Print Debit Note" CssClass="btn btn-success" OnClick="btPDF_Click" OnClientClick="Alert()"></asp:Button>
            </div>
        </div>

        <!-- Title and Info -->
        <div class="form-group row text-center courier-font">
            <div class="col-sm-12 h5">
                <asp:Literal ID="litBranch" runat="server"></asp:Literal>
            </div>
        </div>


        <table style="width: 100%; border-collapse: collapse;">
            <!-- Row 1 -->
            <tr>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px; font-size: 12px;">VAT Reg.No. : 139052080 7000<br />
                    <asp:Label ID="txtreg" runat="server" Style="font-size: 12px;"></asp:Label>
                </td>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px; font-size: 12px;">SVAT Reg.No. : 13068<br />
                    <asp:Label ID="txtsvat" runat="server" Style="font-size: 12px;"></asp:Label>
                </td>
            </tr>

            <!-- Row 2 -->
            <tr>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px; font-weight: bold;">Fire</td>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">
                    <strong>DEBIT NOTE (N.B)</strong>
                </td>
                <td style="width: 40%; padding-left: 15px; padding-right: 15px; vertical-align: middle; text-align: left;">
                    <span class="px-2 py-1 d-inline-block bordered" style="font-weight: bold;">THIS IS NOT A TAX INVOICE</span>                   
                </td>
            </tr>

            <!-- Row 3 -->
            <tr>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px; font-weight: bold;">Debit Note No.</td>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtdebitNo" runat="server" CssClass="font-weight-bold"></asp:Label>
                </td>
                <td style="width: 40%; padding-left: 15px; padding-right: 15px; vertical-align: middle; text-align: left;">
                    Date:  <asp:Label ID="txtSystemDate" runat="server"></asp:Label>
                </td>
            </tr>

            <!-- Row 4 -->
            <tr>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">Proposal/Policy No.</td>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtpolicy" runat="server"></asp:Label>
                </td>
            </tr>

            <!-- Row 5 -->
            <tr>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">Sum Insured</td>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">
                    :  Rs.  <asp:Label ID="txtsum" runat="server"></asp:Label>
                </td>
            </tr>

            <!-- Row 6 -->
            <tr>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">Period of Insurance</td>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtPeriodInsu" runat="server"></asp:Label>
                </td>
            </tr>

            <!-- Row 7 -->
            <tr>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">Name of Insured</td>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtnameinsu" runat="server"></asp:Label>
                </td>
            </tr>

            <!-- Row 8 -->
            <tr>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">Address</td>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtadd" runat="server"></asp:Label>
                </td>
            </tr>
        </table>


        <!-- Dashed Line -->
        <div class="dashed-line"></div>

        <table style="width: 100%; border-collapse: collapse;">
            <tr>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">
                    <asp:Label ID="LabelRsCts" runat="server"></asp:Label>
                </td>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px;">Rs.Cts.
        </td>
                <td rowspan="5" style="width: 40%; text-align: center; vertical-align: middle;">
                    <img src='<%= ResolveUrl("~/Images/Debit.png") %>' alt="Debit Image" width="350" height="130" class="ml-2" />
                </td>
            </tr>
            <tr>
                <td style="padding-left: 15px; padding-right: 15px;">
                    <div class="font-weight-bold">Net Premium</div>
                </td>
                <td style="padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtNetPremium" runat="server"></asp:Label>
                </td>
            </tr>
            

            <tr>
                <td style="padding-left: 15px; padding-right: 15px;">SRCC
                </td>
                <td style="padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtSrcc" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="padding-left: 15px; padding-right: 15px;">TC
                </td>
                <td style="padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txttc" runat="server"></asp:Label>
                </td>
            </tr>

            <tr>
                <td style="padding-left: 15px; padding-right: 15px;">Policy Fee
                </td>
                <td style="padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtPolicyFee" runat="server"></asp:Label>
                </td>
            </tr>

            <%--<tr>
                <td style="padding-left: 15px; padding-right: 15px;">VAT:
                </td>
                <td style="padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtvat" runat="server"></asp:Label>
                </td>
            </tr>--%>

            <tr>
                <td style="padding-left: 15px; padding-right: 15px;">Administrative Fee
        </td>
                <td style="padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtAdminFee" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 15px; padding-right: 15px;">Taxes
        </td>
                <td style="padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="txtTaxes" runat="server"></asp:Label>
                </td>
            </tr>
        </table>

        <!-- Dashed Line -->
        <div class="dashed-line"></div>

              <table style="width: 100%; border-collapse: collapse;">
            <tr>
                <td style="width: 30%; padding-left: 15px; padding-right: 15px; font-weight: bold;">Total Debit Amount
                </td>
                <td style="padding-left: 15px; padding-right: 15px;">
                        :  <asp:Label ID="txtTotalDebitAmount" runat="server" CssClass="font-weight-bold"></asp:Label>                    
        </td>
                <td rowspan="5" style="width: 40%; text-align: center; vertical-align: middle;">
                   <%-- <img src='<%= ResolveUrl("~/Images/Debit.png") %>' alt="Debit Image" width="350" height="130" class="ml-2" />--%>
                </td>
            </tr>
            <%--<tr>
                <td style="padding-left: 15px; padding-right: 15px;">
                    <div class="font-weight-bold">Net Premium:</div>
                </td>
                <td style="padding-left: 15px; padding-right: 15px;">
                    :  <asp:Label ID="Label2" runat="server"></asp:Label>
                </td>
            </tr>--%>

        </table>

        <div class="form-group row">
            <div class="col-sm-6">
                <asp:Label ID="txtDebitAmount" runat="server"></asp:Label>
            </div>
        </div>

        <div class="form-group row mb-0">
            <div class="col-sm-6" style="display: flex; align-items: center;">
            <span style="margin-right: 10px;">Paid On:  </span>
                <asp:Label ID="txtPaidOn" runat="server"></asp:Label>
            </div>
        </div>

         <div class="form-group row mb-0">
            <div class="col-sm-6" style="display: flex; align-items: center;">
            <span style="margin-right: 10px;">SLI Code:  </span>
                <asp:Label ID="Txtslicode" runat="server"></asp:Label>
            </div>
        </div>

        <div class="form-group row mb-0">
            <div class="col-sm-6" style="display: flex; align-items: center;">
            <span style="margin-right: 10px;">File Service Branch:  </span>
                <asp:Label ID="txtfilebranch" runat="server"></asp:Label>
            </div>
        </div>


        
        <div class="form-group row mb-0">
            <div class="col-sm-6" style="display: flex; align-items: center;">
            <span style="margin-right: 10px;">Client ID:  </span>
                <asp:Label ID="txtClientID" runat="server" CssClass="font-weight-bold"></asp:Label>
            </div>
        </div>

            <%--<!-- Client ID Section -->
            <div class="col-sm-6">
                <div class="row">
                    <div class="col-sm-6 font-weight-bold">Client ID:</div>
                    <div class="col-sm-6">
                        <asp:Label ID="txtClientID" runat="server" CssClass="font-weight-bold"></asp:Label>
                    </div>
                </div>
            </div>
        </div>--%>

        
        <div class="form-group row">
            <div class="col-sm-6">
                <p style="text-indent: 0; text-align: justify; font-size: 14px;">
                    Subject to there being no claim from the date of Renewal to the date of payment
                </p>               
            </div>
        </div>

        <div class="form-group row">
            <div class="col-sm-12" style="display: flex; align-items: center;">
                <span style="margin-right: 10px;">Name of Debtor:</span>
                <asp:Label ID="txtNameDebtor" runat="server" />
            </div>
        </div>


        <div class="form-group row">
            <div class="col-sm-12" style="display: flex; align-items: center;">
                <span style="margin-right: 10px;">Agency Code:</span>
                <asp:Label ID="TxtAgencyCode" runat="server" />
            </div>
        </div>

        <div class="form-group row">
            <div class="col-sm-12">
                <p style="text-indent: 0; text-align: justify; font-size: 14px;">
                    If you disagree with the contents of this debit note please inform us within 14 days of the debit note. Otherwise, we will consider this as correct. Please settle the Premium within thirty days and quote Debit Note Number when making payments.
                </p>
                <p style="text-indent: 0; text-align: justify; font-weight: bold; font-size: 14px;">
                    The cover provided is subject to the conditions stipulated in the premium payment Warranty attached.
                </p>
            </div>
        </div>
     

        <div class="form-group row">
            <div class="col-sm-12">
                <h6 style="text-indent: 0; text-align: center; ">Manager
                </h6>
            </div>
        </div>

        <div style="display: flex; align-items: center;">
            <!-- Date Label -->
            <asp:Label ID="lblDate" runat="server" Style="margin-left: 6px; font-size: 14px; "></asp:Label>
            <span>,</span>

            <!-- Time Label -->
            <asp:Label ID="lblTime" runat="server" Style="margin-left: 6px;  font-size: 14px;"></asp:Label>
            <span>,</span>

            <!-- ID Label -->
            <asp:Label ID="lblID" runat="server" Style="margin-left: 6px; font-size: 14px;"></asp:Label>
            <span>,</span>

            <!-- IP Address Label -->
            <asp:Label ID="lblIP" runat="server" Style="margin-left: 6px; font-size: 14px;"></asp:Label>
        </div>



        <br />

        <!-- Premium Payment Warranty -->
        <div class="form-group row mt-4">
            <div class="col-sm-12">
                <h5 style="text-align: center; font-weight: bold; text-decoration: underline;">Premium Payment Warranty for Policies of General Insurance</h5>

                <div class="form-group row mb-0">
                    <div class="col-sm-6" style="display: flex; align-items: center; margin-top: 20px; margin-bottom: 20px;">
                        <span style="margin-right: 10px;">Debit Note No:</span>
                        <asp:Label ID="txtDebitNoteNo" runat="server" />
                    </div>
                </div>


                <p style="text-indent: 1em; text-align: justify;">
                    1. Notwithstanding anything herein contained but subject to clause 2 and 3 hereof, it is hereby agreed and declared that the full premium due and payable in respect of this insurance is required to be settled to the Insurer (Sri Lanka Insurance Corporation General Ltd.) on or before the premium due date specified in the Schedule of this Policy, Renewal Certificate, Endorsement, or Cover Note (which shall be a date not exceeding 60 days from the date of inception of the policy) and in the absence of any such premium due date, the full settlement of the premium is required to be made or effected on or before the expiry of the 60th day from the date of inception of this policy, Renewal Certificate, Endorsement, or Cover Note (hereinafter referred to as the “due date”).
       
                </p>

                <p style="text-align: justify;">
                    For the purpose of the warranty the “due date” shall be recognized from the date of inception or commencement of the coverage.
       
                </p>

                <p style="text-indent: 1em; text-align: justify;">
                    2. It is also declared and agreed that the settlement of the full premium on or before the due date shall operate as a condition precedent to the insurer’s (Sri Lanka Insurance Corporation General Ltd.) liability or an obligation to settle a claim under this Policy, Renewal Certificate, Endorsement, or Cover Note.
       
                </p>

                <p style="text-align: justify;">
                    In the event any claim arises between the date of commencement of this insurance and the “due date” for the settlement of premium, the insurer (Sri Lanka Insurance Corporation General Ltd.) may defer any decision on liability or postpone the settlement of any such claim until full settlement of the premium is effected on or before the “due date”.
       
                </p>

                <p style="text-indent: 1em; text-align: justify;">
                    3. It is also declared and agreed that where the full premium payable hereunder remains outstanding as at the closure of business of the insurer on the “due date” then the cover under this insurance and any obligations assumed or imputed under this insurance shall stand to be cancelled, ceased, and revoked immediately.
       
                </p>

                <p style="text-align: justify;">
                    However, such cancellation will not prejudice the right of the insurer (Sri Lanka Insurance Corporation General Ltd.) to invoke any legal defenses or to recover the full or any part of the defaulted premium attributable to the expired period of the insurance.
       
                </p>
            </div>
        </div>
    </div>
    </div>
</asp:Content>
