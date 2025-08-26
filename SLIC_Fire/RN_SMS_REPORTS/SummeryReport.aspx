<%@ Page  Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true"  CodeFile="SummeryReport.aspx.cs" Inherits="SLIC_Fire_RN_SMS_REPORTS_SummeryReport" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

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
            <label class="col-sm-12 font-weight-bold h5 text-center" style="height: 24px">
                Fire Policy - Renewal Summery SMS Reports
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
                <label class="font-weight-bold">Policy No</label>
                <asp:TextBox ID="txtPolNo" runat="server" CssClass="form-control text-center" placeholder="Policy No" autocomplete="off"></asp:TextBox>
            </div>
        </div>

        <!-- Term and Proposal Type Selection -->

        <!-- Action Buttons -->
        <div class="form-group row" runat="server">
            <div class="col-sm-4 offset-sm-1">
                <asp:Button ID="btn_find" runat="server" Text="Search" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btn_find_Click1" OnClientClick="return clientFunctionValidation()" />
                <asp:Button ID="btn_clear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btn_clear_Click" />
                <%--<asp:Button ID="btn_download" runat="server" Text="Download" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btn_download_Click1" Visible="false" OnClientClick="return clientFunctionValidation()" />--%>
            </div>

        </div>


        <!-- Empty Column -->
        <div class="col-sm-1 mr-0 testClass" runat="server" style="left: 1px; top: 0px"></div>
    </div>

    <hr class="bg-info" />

    <!-- Table Section -->

    <div id="renewGridViewSection" runat="server" class="table-responsive custom-table" style="width: auto;">
        <asp:GridView ID="Grid_Details" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="30"
            CssClass="table table-striped table-hover table-bordered text-nowrap " Width="100%" DataKeyNames="POLICY_NO"
            OnPageIndexChanging="Grid_Details_PageIndexChanging" OnRowDataBound="Grid_Details_RowDataBound" OnRowCommand="Grid_Details_RowCommand"
>
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
                <%--<asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnDownload" runat="server" Text="Download"
                            CommandName="DownloadNotice"
                            CommandArgument='<%# Eval("POLICY_NO") %>'
                            CssClass="btn btn-sm btn-primary" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>--%>


            </Columns>
        </asp:GridView>
    </div>

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

