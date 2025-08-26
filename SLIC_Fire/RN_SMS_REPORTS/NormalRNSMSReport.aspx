<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="NormalRNSMSReport.aspx.cs" Inherits="SLIC_Fire_RN_SMS_REPORTS_NormalRNSMSReport" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .ui-position {
            padding-left: 10px;
        }

        .ui-datepicker {
            font-size: 10pt !important;
        }

        .ui-datepicker-trigger {
            margin-left: 3px;
            vertical-align: middle;
            margin-bottom: 3px;
        }

        .hidden {
            display: none;
        }

        .ui_comp_hide {
            display: none;
        }

        .ui-position {
            padding-left: 10px;
        }

        .ui-datepicker {
            width: auto;
            height: auto;
            margin: 5px auto 0;
            font: 10pt Arial, sans-serif;
            -webkit-box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, .5);
            box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, .5);
        }

        .ui-datepicker-trigger {
            margin-left: 3px;
            vertical-align: middle;
            margin-bottom: 3px;
        }

        .swal-overlay {
            background-color: rgba(43, 165, 137, 0.45);
        }

        .testClass {
            font-size: 13px;
            font-weight: 500;
            color: #000000;
        }

        .testClassHeader {
            color: #026888;
            font-size: 14px;
            font-weight: 600;
        }

        .long-textbox {
            max-height: 40px;
            margin-top: 5px;
            margin-bottom: 50px;
        }

        .form-group {
            margin-bottom: 5px;
        }

        .custom-table .table {
            width: 100% !important;
            max-width: 50000px !important; /* Adjust width as needed */
        }

            .custom-table .table th, .custom-table .table td {
                padding: 10px !important; /* Adjust padding */
            }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(function () {
            $('#<%= txt_start_date.ClientID %>').datepicker({

                changeMonth: true,

                changeYear: true,
                showOtherMonths: true,
                yearRange: '2016:+10',
                dateFormat: 'dd/mm/yy',

            });
        });

        $(function () {

            $('#<%= txt_to_date.ClientID %>').datepicker({

                changeMonth: true,

                changeYear: true,
                showOtherMonths: true,
                yearRange: '2016:+10',
                dateFormat: 'dd/mm/yy',

            });
        });

        function toggleSearchFields() {

            var policyType = $("#ddl_polType").val();

            $("#ddlstatus_class").hide();
            $("#ddlPropType_class").hide();
            $("#ddlPropType_class").hide();


            if (policyType === "F") {

                $("#dd_bank").hide();
                $("#dd_branch").hide();

            }
            else if (policyType === "B") {
                $("#dd_bank").show();
                $("#dd_branch").show();
            }
        }

        // Call the function on page load to set initial visibility
        $(document).ready(function () {
            toggleSearchFields();
        });

    </script>

    <script type="text/javascript">
        function custom_alert(message, title) {
            if (!title)
                title = 'Alert';

            if (!message)
                message = 'No Message to Display.';

            var imageUrl = '<%= ResolveUrl("~/Images/5.gif") %>';
            if (title == 'Alert') {

                swal
                    ({
                        title: title,
                        text: message,
                        icon: "warning",
                        button: true,
                        closeOnClickOutside: true,
                    });
            }
            else if (title == 'Success') {
                swal({
                    title: "Successfully Processing",
                    text: message,
                    icon: imageUrl,
                    button: false,
                    closeOnClickOutside: true,
                });
            }





        }


        function clientFunctionValidationN() {

            // If everything is valid, show success message
            custom_alert('Processing... please wait...', 'Success');
            return true; // Allow submission
        }

        function clientFunctionValidation2() {

            //validate name1
            var name1 = $('#<%=txtCustomerName.ClientID %>').val();

            if (name1 == "") {
                $('#<%= name1Validator.ClientID%>').css('display', '');
                custom_alert('Please enter first name.', 'Alert');
                return false;
            }

            //mobile no vali
            //validate requester phon no
            var reqPhone = $('#<%=txtCustomerPhone.ClientID %>').val();
            if (reqPhone == "") {
                $('#<%= txtCusMobNoVali.ClientID%>').css('display', '');
                custom_alert('Please enter customer mobile number.', 'Alert');
                return false;
            }

            //alert(reqPhone)

            //validate correct phone number
            if (reqPhone !== "") {
                //alert(reqPhone)
                //const regex = /^(?:\+94|94|0)?(70|71|72|74|75|76|77|78)\d{7}$/;
                var regex = /^(?:\+94|94|0)?(7[0-8]\d{7}|(?:11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|91|92|94)\d{6,7})$/;

                //const regex = /^(?:0|94|\+94)?(?:(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|912)(0|2|3|4|5|7|9)|7(0|1|2|5|6|7|8|4)\d)\d{6}$/;
                if (!regex.test(reqPhone)) {
                    custom_alert('Please enter a valid requester phone number.', 'Alert');
                    $('#<%= txtCusMobNoVali.ClientID%>').css("display", "");
                    $('#<%= txtCusMobNoVali.ClientID%>').html("Please enter a valid NIC!");
                    return false;
                }
            }

            //sum insured vali
            var sumVal1 = $('#<%=txtSumInsured1.ClientID %>').val();

            if (sumVal1 == "") {
                $('#<%= txtSumInsu1Vali.ClientID%>').css('display', '');
                custom_alert('Please enter suminsured value.', 'Alert');
                return false;
            }

            //policy fee val 
            var polFee = $('#<%=txtPolicyFee1.ClientID %>').val();

            if (polFee == "") {
                $('#<%= txtPolicyFee1Val.ClientID%>').css('display', '');
                custom_alert('Please enter policy fee value.', 'Alert');
                return false;
            }

            // If everything is valid, show success message
            custom_alert('Processing... please wait...', 'Success');
            return true; // Allow submission
        }

        function clientFunctionValidation() {

            var category = $('#<%=ddl_category.ClientID %>').val();

            if (category == "No") {
                custom_alert('Please select category.', 'Alert');
                return false;

            }
            else {

            }

            var reqType = $('#<%=ddl_RepType.ClientID %>').val();

            if (reqType == "N") {
                custom_alert('Please select report type.', 'Alert');
                return false;

            }
            else {

            }
            // If everything is valid, show success message
            custom_alert('Processing... please wait...', 'Success');
            return true; // Allow submission
        }



        $(document).ready(function () {

            //name 1 
            $(function () {
                $('#<%= txtCustomerName.ClientID%>').on('change keyup paste', function () {

                    $('#<%= name1Validator.ClientID%>').css('display', 'none');

                });

            });

            //mobile no 
            $(function () {
                $('#<%= txtCustomerPhone.ClientID%>').on('change keyup paste', function () {

                    $('#<%= txtCusMobNoVali.ClientID%>').css('display', 'none');

                });

            });

            //sun imsred value1
            $(function () {
                $('#<%= txtSumInsured1.ClientID%>').on('change keyup paste', function () {

                    $('#<%= txtSumInsu1Vali.ClientID%>').css('display', 'none');

                });

            });

            //policy fee value1
            $(function () {
                $('#<%= txtPolicyFee1.ClientID%>').on('change keyup paste', function () {

                    $('#<%= txtPolicyFee1Val.ClientID%>').css('display', 'none');

                });

            });



            window.setTimeout(function () {
                $(".alert").fadeTo(500, 0).slideUp(500, function () {
                    $(this).remove();
                });
            }, 5000);



        });
    </script>


    <script>    
        function showRenewForm() {


            var gridView = document.getElementById('<%= renewGridViewSection.ClientID %>');

            if (gridView) {
                gridView.style.display = "none"; // Hide GridView section
            } else {
                alert("GridView section not found!");
            }

            if (renewForm) {
                renewForm.style.display = "block"; // Show Renewal form
            } else {
                alert("Renewal form section not found!");
            }

            return false; // Prevent postback
        }
    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"></asp:ToolkitScriptManager>
    <asp:Label ID="lblAlertMessage" runat="server" ClientIDMode="Static" Style="display: none;"></asp:Label>
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />

    <div id="viewPropId" runat="server">
        <!-- Header Section -->
        <div class="form-group row" runat="server">
            <label class="col-sm-12 font-weight-bold h5 text-center" style="height: 24px" runat="server" id="lblSms">
                Fire Policy - Renewal SMS Report
            </label>
            <label class="col-sm-12 font-weight-bold h5 text-center" style="height: 24px" runat="server" id="lblPPWRpt">
                Fire Policy - PPW Canceled Report
            </label>
        </div>

        <!-- Search Fields -->
        <div class="form-group row" runat="server">
            <%--<div class="col-sm-2">
            <label class="font-weight-bold">Policy Number</label>
            <asp:TextBox ID="txt_pol_no" runat="server" CssClass="form-control text-center" placeholder="Pol. No."></asp:TextBox>
        </div>
        
        <div class="col-sm-2">
            <label class="font-weight-bold">NIC Number</label>
            <asp:TextBox ID="txtNicNo" runat="server" CssClass="form-control text-center text-uppercase" placeholder="NIC" autocomplete="off"></asp:TextBox>
        </div>--%>
        </div>

        <!-- Additional Search Fields -->
        <div class="form-group row" runat="server" clientidmode="Static">
            <div class="col-sm-2 offset-sm-1" runat="server" id="reportCat">
                <label class="font-weight-bold">Status</label>
                <asp:DropDownList ID="ddl_category" runat="server" CssClass="custom-select text-center" AutoPostBack="True"
                    AppendDataBoundItems="true" OnSelectedIndexChanged="ddlCate_SelectedIndexChanged">
                    <asp:ListItem Text="Select Category" Value="No" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Normal" Value="N" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="With Claims" Value="WC" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="Sum Insured Not Changed" Value="SUM_N_CHANGED" Selected="False"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-sm-2 offset-sm-1" id="reportType" runat="server">
                <label class="font-weight-bold">Status</label>
                <asp:DropDownList ID="ddl_RepType" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true" AutoPostBack="True"
                    OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged">
                    <asp:ListItem Text="Select Report Type" Value="N" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="SMS Send List" Value="A" Selected="false"></asp:ListItem>
                    <asp:ListItem Text="Intermediate Status List" Value="I_Status" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="SMS Not Sent List" Value="Not_Sent" Selected="False"></asp:ListItem>
                </asp:DropDownList>
            </div>

        </div>

        <div class="form-group row" runat="server" clientidmode="Static">
            <div class="col-sm-2 offset-sm-1">
                <label class="font-weight-bold">From</label>
                <asp:TextBox ID="txt_start_date" runat="server" CssClass="form-control text-center" placeholder="From Date" autocomplete="off"></asp:TextBox>
            </div>

            <div class="col-sm-2">
                <label class="font-weight-bold">To</label>
                <asp:TextBox ID="txt_to_date" runat="server" CssClass="form-control text-center" placeholder="To Date" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col-sm-2">
                <label class="font-weight-bold">Branch</label>
                <asp:DropDownList ID="ddl_branch" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <!-- Term and Proposal Type Selection -->

        <!-- Action Buttons -->
        <div class="form-group row" runat="server">
            <div class="col-sm-4 offset-sm-1">
                <asp:Button ID="btn_find" runat="server" Text="Search" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btn_find_Click1" OnClientClick="return clientFunctionValidation()" />
                <asp:Button ID="btn_clear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btn_clear_Click" />
                <asp:Button ID="btn_download" runat="server" Text="Download" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btn_download_Click1" Visible="false" OnClientClick="return clientFunctionValidation()" />
            </div>

        </div>


        <!-- Empty Column -->
        <div class="col-sm-1 mr-0 testClass" runat="server" style="left: 1px; top: 0px"></div>
    </div>

    <hr class="bg-info" />

    <!-- Table Section -->

    <div id="renewGridViewSection" runat="server" class="table-responsive custom-table" style="width: auto;" visible="true">
        <asp:GridView ID="Grid_Details" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="30"
            CssClass="table table-striped table-hover table-bordered text-nowrap " Width="100%" DataKeyNames="POLICY_NO"
            OnPageIndexChanging="Grid_Details_PageIndexChanging" OnRowDataBound="Grid_Details_RowDataBound" OnRowCommand="Grid_Details_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="S.No">
                    <ItemTemplate>
                        <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:BoundField HeaderText="Policy No." DataField="POLICY_NO" />
                <asp:BoundField HeaderText="Pol. End Date" DataField="EXPIRE_DATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                <asp:BoundField HeaderText="Pol. Branch" DataField="BRANCH_NAME" />
                <asp:BoundField HeaderText="Cus. Name" DataField="CUSTOMER_NAME" />
                <asp:BoundField HeaderText="Cus. Mobile No" DataField="customerPhoNo" />
                <asp:BoundField HeaderText="Sum Insured" DataField="SUM_INSURED_L" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Net Pre." DataField="BASIC_PREMIUM" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="RCC" DataField="RCC" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="TC" DataField="TC" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Admin Fee" DataField="adminfee" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Total Premium" DataField="TOT_PREMIUM" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="SMS Status" DataField="RN_SMS_STATUS" DataFormatString="{0:n2}" />

                <asp:TemplateField HeaderText="Total Premium" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalPremium" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NBT" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblNbt" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblVat" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AdminFeeVal" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblAdminFeeVal" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RN Year" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RN Month" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblMonth" runat="server" Text='<%# Eval("MONTH") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Cus. NIC" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCusNic" runat="server" Text='<%# Eval("NIC_NO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Agent Code" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblAgntCode" runat="server" Text='<%# Eval("AGENCY") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sum Insured" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblSunInsu" runat="server" Text='<%# Eval("SUM_INSURED_L") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnDownload" runat="server" Text="Download"
                            CommandName="DownloadNotice"
                            CommandArgument='<%# Eval("POLICY_NO") %>'
                            CssClass="btn btn-sm btn-primary" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Send SMS" Visible="false">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btn_Edit_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Admin Fee Precentage" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblAminPRe" runat="server" Text='<%# Eval("adminfeePRe") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>

    <style>
        /* Light background for read-only fields */
        .highlight-field {
            background-color: #f7f7f7 !important;
            border: 1px solid #ddd;
        }

        .form-section {
            border: 1px solid #ddd;
            border-radius: 10px;
            padding: 15px;
            margin-bottom: 15px;
            background-color: #fcfcfc;
            box-shadow: 0px 2px 5px rgba(0, 0, 0, 0.1);
        }

        .section-header {
            font-size: 18px;
            font-weight: bold;
            color: #0056b3;
            margin-bottom: 10px;
            border-bottom: 2px solid #ddd;
            padding-bottom: 5px;
        }

        .btn-custom {
            width: 150px;
        }

        .text-danger {
            white-space: nowrap;
            overflow: hidden;
            text-overflow: ellipsis;
        }

        .auto-style1 {
            width: 70%;
            height: 41px;
        }

        .auto-style2 {
            width: 15%;
            height: 41px;
        }
    </style>


    <%-- premium calculation --%>
    <asp:HiddenField ID="HiddenFieldSelectedPolicy" runat="server" />
    <asp:Panel ID="Panel1" runat="server" CssClass="container" Visible="false">
        <asp:HiddenField ID="rnYear" runat="server" />
        <asp:HiddenField ID="rnMonth" runat="server" />
        <div id="renewFormSection">
            <h4 class="text-center col-sm-12 font-weight-bold h5 text-center">Premium Calculation & Send to Approve</h4>

            <!-- Policy Details Section -->
            <div class="form-section">
                <div class="section-header">Policy Details</div>
                <div class="form-row">
                    <div class="form-group col-md-3">
                        <label for="txtPolicyNo">Pol. No</label>
                        <asp:TextBox ID="txtPolicyNo" runat="server" CssClass="form-control text-center highlight-field" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtPolicyEndDate">Pol. Start Date</label>
                        <asp:TextBox ID="txtPolicyStartDate" runat="server" CssClass="form-control text-center highlight-field" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtPolicyEndDate">Pol. End Date</label>
                        <asp:TextBox ID="txtPolicyEndDate" runat="server" CssClass="form-control text-center highlight-field" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtBank">Agency</label>
                        <asp:TextBox ID="txtAgency" runat="server" CssClass="form-control text-center highlight-field"></asp:TextBox>
                    </div>
                </div>
            </div>

            <!-- Customer Details Section -->
            <div class="form-section">
                <div class="section-header">Customer Details</div>
                <div class="form-row">
                    <div class="form-group col-md-3">
                        <label for="txtBranch">Branch</label>
                        <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control text-center highlight-field" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtCustomerName">Cus. Name</label>
                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="form-control text-center highlight-field"></asp:TextBox>
                        <label runat="server" id="name1Validator" class="text-danger" style="display: none; width: 100%; text-align: center;" text="">*Required!</label>

                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtCustomerNIC">Cus. NIC</label>
                        <asp:TextBox ID="txtCustomerNIC" runat="server" CssClass="form-control text-center highlight-field" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtCustomerPhone">Cus. Phone</label>
                        <asp:TextBox ID="txtCustomerPhone" runat="server" CssClass="form-control text-center highlight-field"></asp:TextBox>
                        <label id="txtCusMobNoVali" runat="server" class="text-danger" style="display: none; width: 100%; text-align: center;">*Required! </label>
                    </div>


                    <div class="form-group col-md-3">
                        <label for="txtBranch">Address 1</label>
                        <asp:TextBox ID="txtAdd1" runat="server" CssClass="form-control text-center highlight-field" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtCustomerName">Address 2</label>
                        <asp:TextBox ID="txtAdd2" runat="server" CssClass="form-control text-center highlight-field" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtCustomerNIC">Address 3</label>
                        <asp:TextBox ID="txtAdd3" runat="server" CssClass="form-control text-center highlight-field" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label for="txtCustomerPhone">Address 4</label>
                        <asp:TextBox ID="txtAdd4" runat="server" CssClass="form-control text-center highlight-field" ReadOnly="true"></asp:TextBox>
                    </div>

                </div>
            </div>

            <%-- Previous Claim Details --%>
            <div class="form-section" style="padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #f9f9f9; max-width: 100%; overflow-x: auto;">
                <div class="section-header" style="font-size: 18px; font-weight: bold; margin-bottom: 10px;">
                    Previous Details
                </div>
                <a href="http://insurance-app/Beegeneral/clmworks/Claims%20inquiry%20Report/claimsInquiryIndex.asp"
                    target="_blank"
                    rel="noopener noreferrer"
                    class="btn btn-primary">Previous Claim Details
                </a>

                <%-- Change this in to b-receipt inauiry live URL --%> <%--http://generaltest-app:8088/Beegeneral/PremiumReceipt/OutstDetsearch/PolicySearch1.asp--%>
                <a href="http://receipt-app/secworks/signin.asp"
                    target="_blank"
                    rel="noopener noreferrer"
                    class="btn btn-primary">Premium Receipt Inquiry
                </a>

                <%--<div class="form-row">
                    <div style="overflow-x: auto;">
                        <asp:GridView ID="prevClaimDetailGrid" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="8"
                            CssClass="table table-striped table-hover table-bordered text-nowrap"
                            Width="100%" DataKeyNames="POLICY_NO">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Policy No." DataField="INPNO" />
                                <asp:BoundField HeaderText="Inv. No" DataField="INNO" />
                                <asp:BoundField HeaderText="ININA" DataField="ININA" />
                                <asp:BoundField HeaderText="INRID" DataField="INRID" />
                                <asp:BoundField HeaderText="INDLS1" DataField="INDLS1" />
                                <asp:BoundField HeaderText="INDTL" DataField="INDTL" />
                                <asp:BoundField HeaderText="INCLS" DataField="INCLS" />
                                <asp:BoundField HeaderText="INREQ" DataField="INREQ" DataFormatString="{0:n2}" />
                                <asp:BoundField HeaderText="INDTI" DataField="INDTI" />
                                <asp:BoundField HeaderText="INPYR" DataField="INPYR" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>--%>
            </div>


            <%-- end of previous claim details --%>

            <!-- Sum Insured Section -->
            <div class="form-section">
                <div class="section-header">Sum Insured Details</div>
                <div class="form-row">
                    <div class="form-group col-md-4">
                        <label>Sum Insured :</label>
                        <asp:Label ID="lblSumYear" runat="server"></asp:Label>
                        <asp:TextBox ID="txtSumInsured1" runat="server" CssClass="form-control text-center " step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                        <label runat="server" id="txtSumInsu1Vali" class="text-danger" style="display: none; width: 100%; text-align: center;" text="">*Required!</label>
                    </div>
                    <%--<div class="form-group col-md-4">
                        <label>Sum Insured :</label>
                        <asp:Label ID="lblSumYear2" runat="server"></asp:Label>
                        <asp:TextBox ID="txtSumInsured2" runat="server" CssClass="form-control text-center " ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label>Sum Insured :</label>
                        <asp:Label ID="lblSumYear3" runat="server"></asp:Label>
                        <asp:TextBox ID="txtSumInsured3" runat="server" CssClass="form-control text-center " ReadOnly="true"></asp:TextBox>
                    </div>--%>
                </div>
            </div>

            <!-- Premium Details Section -->
            <div class="form-section">
                <div class="section-header">Premium Details</div>
                <div class="form-row">
                    <div class="form-group col-md-3">
                        <label>Net Premium:</label>
                        <asp:TextBox ID="txtNetPremium" runat="server" CssClass="form-control text-center " step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                        <label runat="server" id="txtNetPreVali" class="text-danger" style="display: none; width: 100%; text-align: center;" text="">*Required!</label>
                    </div>
                    <div class="form-group col-md-3">
                        <label>RCC:</label>
                        <asp:TextBox ID="txtRcc" runat="server" CssClass="form-control text-center " step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>

                    </div>
                    <div class="form-group col-md-3">
                        <label>TR:</label>
                        <asp:TextBox ID="txtTr" runat="server" CssClass="form-control text-center " step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label>Admin Fee Precentage:</label>
                        <asp:TextBox ID="txtAdminFee" runat="server" CssClass="form-control text-center " step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group col-md-3">
                        <label>Policy Fee:</label>
                        <asp:TextBox ID="txtPolicyFee1" runat="server" CssClass="form-control text-center " Text="100.00" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);" ReadOnly="true"></asp:TextBox>
                        <label runat="server" id="txtPolicyFee1Val" class="text-danger" style="display: none; width: 100%; text-align: center;" text="">*Required!</label>
                    </div>

                    <div class="form-group col-md-3">
                        <label>VAT:</label>
                        <asp:TextBox ID="txtVat" runat="server" CssClass="form-control text-center " ReadOnly="true" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>
                    <asp:HiddenField runat="server" ID="txtadminFeeVal2" />
                    <div class="form-group col-md-3">
                        <label>Admin Fee:</label>
                        <asp:TextBox ID="txtAdminFee2" runat="server" CssClass="form-control text-center " ReadOnly="true" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label>Total Premium:</label>
                        <asp:TextBox ID="txtTotalPremium" runat="server" CssClass="form-control text-center " ReadOnly="true" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <%--<div class="form-group col-md-3">
                        <label>Excess Precentage:</label>
                        <asp:TextBox ID="txtExcessPre" runat="server" CssClass="form-control text-center " step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label>Excess Amount:</label>
                        <asp:TextBox ID="txtExcessAmo" runat="server" CssClass="form-control text-center " step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>--%>


                    <div class="form-group col-md-6">
                        <label>Remark:</label>
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="10"></asp:TextBox>
                    </div>
                    <script>
                        document.addEventListener("DOMContentLoaded", function () {
                            var textbox = document.getElementById("<%= txtRemark.ClientID %>");

                            if (textbox) {
                                textbox.addEventListener("keydown", function (e) {
                                    if (e.key === "Enter") {
                                        e.preventDefault();

                                        var start = this.selectionStart;
                                        var end = this.selectionEnd;
                                        var value = this.value;

                                        this.value = value.substring(0, start) + "\n" + value.substring(end);
                                        this.selectionStart = this.selectionEnd = start + 1;
                                    }
                                });
                            }
                        });
                    </script>


                    <div class="form-group col-md-3" runat="server" visible="false">
                        <%-- NBT change to SSL --%>
                        <label>SSL :</label>
                        <asp:TextBox ID="txtNbt" runat="server" CssClass="form-control text-center " ReadOnly="true" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>

                </div>

                <div class="form-row">
                    <asp:Panel ID="pnlExcessTable" runat="server" CssClass="table-container">
                        <table border="1" cellspacing="0" cellpadding="5" style="width: 100%; border-collapse: collapse;">
                            <thead style="background-color: #f1f1f1;">
                                <tr>
                                    <th class="auto-style1" style="width: 5%;"><b>Excess Id</b></th>
                                    <th class="auto-style1" style="width: 70%;"><b>Excess Name</b></th>
                                    <th class="auto-style2" style="width: 10%;"><b>Percentage</b></th>
                                    <th class="auto-style2" style="width: 20%;"><b>Amount</b></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="txtExcessId3" runat="server">1</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtExcess1" runat="server" TextMode="MultiLine" Rows="3"
                                            CssClass="form-control">All Natural Perils including Cyclone/Storm/Tempest/Flood/Earthquake (with fire & shock)</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPercentage1" runat="server" CssClass="form-control" placeholder="%" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                                            onkeypress="return isNumber(event);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount1" runat="server" CssClass="form-control" placeholder="Amount" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                                            onkeypress="return isNumber(event);"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="txtExcessId4" runat="server">2</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtExcess2" runat="server" TextMode="MultiLine" Rows="2"
                                            CssClass="form-control">All Other Perils</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPercentage2" runat="server" CssClass="form-control" placeholder="%" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                                            onkeypress="return isNumber(event);"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAmount2" runat="server" CssClass="form-control" placeholder="Amount" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                                            onkeypress="return isNumber(event);"></asp:TextBox>
                                    </td>
                                </tr>

                                <!-- Dynamic Rows -->
                                <asp:Repeater ID="rptAdditionalExcess" runat="server">
                                    <ItemTemplate>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtExcessId" ReadOnly="true" runat="server" CssClass="form-control" Text='<%# Eval("ExcessId") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtExcessName" runat="server" CssClass="form-control" Text='<%# Eval("ExcessName") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPercentage" runat="server" CssClass="form-control" Text='<%# Eval("Percentage") %>' />
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control" Text='<%# Eval("Amount") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </tbody>
                        </table>
                        <br />
                        <asp:Button ID="btnAddRow" runat="server" Text="Add Excess" CssClass="btn btn-primary" Style="background-color: #008B80;" OnClick="btnAddRow_Click" />
                        <asp:Button ID="Button4" runat="server" Text="Remove Excess" CssClass="btn btn-danger" OnClick="btnRemoveRow_Click1" Style="margin-left: 10px;" />
                    </asp:Panel>
                </div>



                <br />
                <div class="form-row">
                    <div class="form-group col-md-3">
                        <%-- OnClick="btn_PremCal_Click1" OnClick="btnSentAprr_Click" OnClick="btnSentSMS_Click"--%>
                        <asp:Button ID="btnPreCal" runat="server" Text="Premium Calculation" CssClass="btn btn-success" OnClick="btn_PremCal_Click2" Style="background-color: #008B80;" OnClientClick="return clientFunctionValidation2()" />
                        <asp:Button ID="btn_close" runat="server" Text="Close" CssClass="btn btn-secondary" OnClick="btnClose_Click2" />
                    </div>
                    <div class="form-group col-md-3">
                        <asp:Button ID="btnSendToApp" runat="server" Text="Send to Approve" CssClass="btn btn-info" Visible="false" Style="background-color: #008B80;" OnClick="btn_sentToApp_Click" OnClientClick="return clientFunctionValidationN()" />
                        <asp:Button ID="Button3" runat="server" Text="Send SMS" Visible="false" CssClass="btn btn-info" Style="background-color: #008B80;" OnClientClick="return clientFunctionValidationN()" />
                    </div>
                </div>

            </div>

            <!-- Action Buttons -->
            <div class="text-center">
                <%-- <asp:Button ID="Edit" runat="server" Text="Edit" CssClass="btn btn-warning btn-custom" OnClick="btn_edit_Click" />--%>
                <%--<asp:Button ID="Submit" runat="server" Text="Submit" CssClass="btn btn-success btn-custom" OnClick="btn_submit_Click" />--%>
            </div>
        </div>
    </asp:Panel>

    <%-- end of premium calculation --%>

    <script type="text/javascript">

        function custom_alert1(message, title) {
            var title = $("#lblAlertMessage").attr("data-alert-title");
            var message = $("#lblAlertMessage").attr("data-alert-message");

            if (!title) title = 'Alert';
            if (!message) message = 'No Message to Display.';

            var imageUrl = '<%= ResolveUrl("~/Images/5.gif") %>';

            if (title === 'Alert') {
                swal({
                    title: title,
                    text: message,
                    icon: "warning",
                    button: true,
                    closeOnClickOutside: false,
                }).then(function () {
                    // Redirect to a specific page
                    // window.location.href = "/Slicgeneral/MotorDiscountRequest/Default.aspx"; // Change to your target page
                });
            } else if (title === 'Success') {
                swal({
                    title: title,
                    text: message,
                    icon: "success",
                    button: "OK",
                    closeOnClickOutside: false,
                }).then(function () {
                    // Redirect to a specific page
                    window.location.href = "/Slicgeneral/FireRenewalSMS/FireDefault.aspx"; // Change to your target page

                });
            } else if (title === 'Processing') {
                swal({
                    title: title,
                    text: message,
                    icon: imageUrl,
                    button: false,
                    closeOnClickOutside: false,
                });
            }
        }


        function clearAlert() {
            document.getElementById('lblAlertMessage').innerText = ''; // Replace 'alertMessage' with the ID of your alert message element
            if (typeof swal !== 'undefined' && swal.isVisible()) {
                swal.close(); // Close any active sweet alert popups
            }
        }


    </script>
    <script>
        function removeRow(row) {
            const tableRow = row.closest('tr');
            tableRow.parentNode.removeChild(tableRow);
        }
    </script>


    <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>--%>

    <script type="text/javascript">

        $(document).ready(function () {
            // Show custom alerts
            var alertType = $("#lblAlertMessage").attr("data-alert-type");
            var message = $("#lblAlertMessage").text();

            if (alertType !== "" && message !== "") {
                custom_alert1(message, alertType);
            }
        });


    </script>

</asp:Content>

