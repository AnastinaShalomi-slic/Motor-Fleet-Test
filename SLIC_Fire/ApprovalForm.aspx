<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage2.master" CodeFile="ApprovalForm.aspx.cs" Inherits="SLIC_Fire_ApprovalForm" Culture="en-GB" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" %>

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

    <style>
        .popup-grid {
            display: flex;
            flex-wrap: wrap;
            gap: 20px; /* spacing between form groups */
        }


            .popup-grid label {
                margin: 0;
                padding: 0;
                font-weight: 600;
                align-self: center;
            }

        .form-control {
            width: 100%;
            padding: 6px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }


        .popup-grid {
            display: flex;
            flex-wrap: wrap;
            gap: 20px;
        }

        .form-group-pop {
            flex: 1 1 45%; /* Two columns */
            display: flex;
            flex-direction: column;
        }

        .form-label-pop {
            font-weight: bold;
            margin-bottom: 4px;
        }

        .form-control-pop {
            width: 100%;
            padding: 6px;
            border: 1px solid #ccc;
            border-radius: 4px;
        }




        /* Responsive behavior for smaller screens */
        @media screen and (max-width: 768px) {
            .popup-grid {
                grid-template-columns: 1fr 1fr; /* 1 field per row (label + textbox) */
            }
        }

        @media screen and (max-width: 480px) {
            .popup-grid {
                grid-template-columns: 1fr; /* Stack label and textbox vertically */
            }
        }

        /* Optional styling for popup */
        .modal-popup {
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            background-color: #fff;
            z-index: 9999;
            padding: 20px;
            border-radius: 10px;
            width: 50%;
            max-height: 90vh; /* ✅ Allow scrolling within modal */
            overflow-y: auto; /* ✅ Enable vertical scroll */
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.3);
        }


        .modal-content {
            width: 100%;
        }

        .modal-header {
            font-size: 1.2em;
            margin-bottom: 10px;
        }

        .modal-body {
            overflow-y: auto;
            max-height: 70vh;
        }

        .close {
            float: right;
            font-size: 1.5em;
            cursor: pointer;
            background: none;
            border: none;
        }

        /* Responsive Fix: Stack columns on small screens */
        body.modal-open {
            overflow: hidden;
        }

        .table-container .form-control {
            width: 100%;
            padding: 6px;
            box-sizing: border-box;
            border: 1px solid #ccc;
            border-radius: 4px;
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

            // If everything is valid, show success message
            custom_alert('Processing... please wait...', 'Success');
            return true; // Allow submission
        }

        function clientFunctionValidation2() {

            //policy fee val 
            var totPremium = $('#<%=txtTotPre.ClientID %>').val();

            if (totPremium == "") {
                custom_alert('Do premium calculation first', 'Alert');
                return false;
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

    <style type="text/css">
        .fixed-column {
            position: sticky;
            right: 0;
            background-color: white;
            z-index: 2;
            min-width: 120px;
            text-align: center;
        }

        .table-responsive {
            overflow-x: auto;
            max-width: 100%;
        }

        .fixed-column .btn {
            width: 90px;
            height: 30px;
            font-size: 14px;
            margin: 5px;
        }

        .swal-overlay {
            background-color: rgba(43, 165, 137, 0.45);
        }
    </style>

    <!-- Hidden Field to store Policy Number -->
    <asp:HiddenField ID="hfPolicyNumber" runat="server" />
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />

    <!-- ToolkitScriptManager for AJAX support -->
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"></asp:ToolkitScriptManager>
    <asp:Label ID="lblAlertMessage" runat="server" ClientIDMode="Static" Style="display: none;"></asp:Label>

    <div id="viewPropId" runat="server">
        <!-- Header Section -->
        <div class="form-group row" runat="server">
            <label class="col-sm-12 font-weight-bold h5 text-center" style="height: 24px">
                Fire Renewal Approval 
            </label>
        </div>

        <!-- Policy Type Selection -->
        <div class="form-group row" runat="server">
            <div class="col-sm-3 offset-sm-1" runat="server">
                <label class="font-weight-bold">Policy Type</label>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_polType" runat="server" CssClass="custom-select text-center"
                            ClientIDMode="Static" AppendDataBoundItems="true" onchange="toggleSearchFields();">
                            <asp:ListItem Text="Fire" Value="F"></asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_polType" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="form-group col-md-3 offset-sm-1">
                <label class="font-weight-bold">Status</label>
                <asp:DropDownList ID="ddl_status" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged">
                    <asp:ListItem Text="Normal" Value="N" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="With Claims" Value="WC" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="Sum Insured Changed" Value="SUM_CHANGED" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="PPW Reinstatedated" Value="WPPWC" Selected="False"></asp:ListItem>
                    <%--<asp:ListItem Text="Sum Insured Not Changed" Value="SUM_N_CHANGED" Selected="False"></asp:ListItem>--%>
                </asp:DropDownList>
            </div>
            <div class="form-group col-md-3 offset-sm-1">
                <label class="font-weight-bold">Policy Number</label>
                <asp:TextBox ID="txt_pol_no" runat="server" CssClass="form-control text-center" placeholder="Pol. No."></asp:TextBox>
            </div>
        </div>

        <div class="form-group row" runat="server">
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
            <div class="col-sm-2" runat="server" id="divBudiUnit" visible="false">
                <label class="font-weight-bold">Business Unit</label>
                <asp:DropDownList ID="ddlBusUnit" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true" >
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    <asp:ListItem Value="334">CBD-BK Branch</asp:ListItem>
                    <asp:ListItem Value="333">Key Account</asp:ListItem>
                    <asp:ListItem Value="1">Other</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <!-- Search Fields -->
        <div class="form-group row" runat="server">
           <div class="form-group col-md-3 offset-sm-1">
                <label class="font-weight-bold">Sub Department</label>
                <asp:DropDownList ID="ddlSubDept" runat="server" CssClass="custom-select text-center" AutoPostBack="true" AppendDataBoundItems="true" >
                    <asp:ListItem Text="Select" Value="N" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Engineering" Value="EN" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="Fire" Value="FI" Selected="False"></asp:ListItem>                   
                </asp:DropDownList>
            </div>          
        </div>

        <!-- Additional Search Fields -->
        <%--<div class="form-group row" runat="server" id="divAdditionalSearchFields" clientidmode="Static">
            <div class="col-sm-2 offset-sm-1">
                <label class="font-weight-bold">Status</label>
                <asp:DropDownList ID="ddl_status" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true">
                    <asp:ListItem Text="Normal" Value="N" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="With Claims" Value="WC" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="With PPW Cancellation" Value="WPPWC" Selected="False"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>--%>

        <!-- Action Buttons -->
        <div class="form-group row" runat="server">
            <div class="col-sm-4 offset-sm-1">
                <asp:Button ID="btn_find" runat="server" Text="Search" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btn_find_Click1" OnClientClick="return clientFunctionValidation()" />
                <asp:Button ID="btn_clear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btn_clear_Click" />
                <%--<asp:Button ID="Button1" runat="server" Text="Download Test" CssClass="btn btn-secondary" OnClick="btn_dwn_Click" />--%>
            </div>

        </div>


        <!-- Empty Column -->
        <div class="col-sm-1 mr-0 testClass" runat="server" style="left: 1px; top: 0px"></div>
    </div>

    <hr class="bg-info" />

    <div class="container-fluid mt-4">
        <div class="card shadow-lg">
            <div class="card-header bg-light text-dark text-center">
                <h3 class="mb-0">Fire Renewal SMS - Approval</h3>
            </div>
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="Grid_Details" runat="server" AutoGenerateColumns="false" PageSize="10"
                        CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100" Width="100%" OnRowCommand="Grid_Details_RowCommand">
                        <Columns>
                            <asp:BoundField HeaderText="Dept" DataField="RNDEPT" />
                            <asp:BoundField HeaderText="Type" DataField="RNPTP" />
                            <asp:BoundField HeaderText="Policy No." DataField="RNPOL" />
                            <asp:BoundField HeaderText="Year" DataField="RNYR" />
                            <asp:BoundField HeaderText="Month" DataField="RNMNTH" />
                            <asp:BoundField HeaderText="Start Date" DataField="RNSTDT" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                            <asp:BoundField HeaderText="End Date" DataField="RNENDT" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                            <asp:BoundField HeaderText="Agent Code" DataField="RNAGCD" />
                            <asp:BoundField HeaderText="Net Premium" DataField="RNNET" DataFormatString="{0:N2}"/>
                            <asp:BoundField HeaderText="RCC" DataField="RNRCC" DataFormatString="{0:N2}" />
                            <asp:BoundField HeaderText="TC" DataField="RNTC" DataFormatString="{0:N2}"/>
                            <asp:BoundField HeaderText="Policy Fee" DataField="RNPOLFEE" DataFormatString="{0:N2}"/>
                            <asp:BoundField HeaderText="VAT" DataField="RNVAT" DataFormatString="{0:N2}" />
                            <asp:BoundField HeaderText="NBT" DataField="RNNBT" DataFormatString="{0:N2}" />
                            <asp:BoundField HeaderText="Total" DataField="RNTOT" DataFormatString="{0:N2}" />
                            <asp:BoundField HeaderText="Customer Name" DataField="RNNAM" />
                            <asp:BoundField HeaderText="Address 1" DataField="RNADD1" />
                            <asp:BoundField HeaderText="Address 2" DataField="RNADD2" />
                            <asp:BoundField HeaderText="Address 3" DataField="RNADD3" />
                            <asp:BoundField HeaderText="Address 4" DataField="RNADD4" />
                            <asp:BoundField HeaderText="NIC" DataField="RNNIC" />
                            <asp:BoundField HeaderText="Contact" DataField="RNCNT"  />
                            <asp:BoundField HeaderText="Reference No." DataField="RNREF" />
                            <asp:BoundField HeaderText="FBR" DataField="RNFBR" />
                            <asp:BoundField HeaderText="Admin Fee" DataField="RN_ADMINFEE" DataFormatString="{0:N2}"  />
                            <asp:BoundField HeaderText="Entry Date" DataField="RNDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" />
                            <asp:BoundField HeaderText="Rn by" DataField="RN_BY" Visible="false" />
                            <asp:BoundField HeaderText="IP" DataField="RN_IP" Visible="false" />
                            <asp:BoundField HeaderText="brcd" DataField="RN_BRCD" Visible="false" />
                            <%--<asp:BoundField HeaderText="Admin Fee Precentage" DataField="adminfee" />--%>
                            <asp:TemplateField HeaderText="Sum Insured" Visible="True">
                                <ItemTemplate>
                                    <asp:Label ID="lblSunInsu" runat="server" Text='<%# Eval("RNSUMINSUR", "{0:N2}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actions">
                                <ItemTemplate>
                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CommandName="RejectRow" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-danger" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select">
                                <HeaderTemplate>
                                    <span style="margin-left: 5px;">Select All</span>
                                    <asp:CheckBox ID="chkAll" runat="server" onclick="toggleAllCheckboxes(this);" />

                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSelect" runat="server" onclick="updateHeaderCheckbox();" />
                                </ItemTemplate>
                                <HeaderStyle CssClass="text-center bg-light text-black" />
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
            </div>
        </div>
    </div>

    <br />
    <div class="form-group row" runat="server">
        <div class="col-sm-4 offset-sm-1">
            <asp:Button ID="btnInsertSelected" runat="server" Text="Approve Selected Records" Style="background-color: #008B80; display: none;" CssClass="btn btn-success" OnClick="btnInsertSelected_Click" OnClientClick="return clientFunctionValidation()" />
        </div>
    </div>



    <script type="text/javascript">
        function toggleAllCheckboxes(source) {
            var grid = document.getElementById('<%= Grid_Details.ClientID %>');
            var checkboxes = grid.querySelectorAll("input[id*='chkSelect']");
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = source.checked;
            }

            toggleInsertButton();
        }

        function updateHeaderCheckbox() {
            var grid = document.getElementById('<%= Grid_Details.ClientID %>');
            var headerCheck = grid.querySelector("input[id*='chkAll']");
            var rowCheckboxes = grid.querySelectorAll("input[id*='chkSelect']");
            var allChecked = true;

            for (var i = 0; i < rowCheckboxes.length; i++) {
                if (!rowCheckboxes[i].checked) {
                    allChecked = false;
                    break;
                }
            }
            headerCheck.checked = allChecked;
            toggleInsertButton();
        }

        function toggleInsertButton() {
            var checkboxes = document.querySelectorAll("input[id*='chkSelect']");
            var anyChecked = Array.from(checkboxes).some(cb => cb.checked);
            var btn = document.getElementById('<%= btnInsertSelected.ClientID %>');
            btn.style.display = anyChecked ? 'inline-block' : 'none';
        }
    </script>

    <%-- Edit popup panel start --%>

    <asp:Panel ID="pnlPopup" runat="server" CssClass="modal-popup">
        <div class="popup-container">

            <br />
            <a href="http://insurance-app/Beegeneral/clmworks/Claims%20inquiry%20Report/claimsInquiryIndex.asp" target="_blank" rel="noopener noreferrer">Previous Claim Details</a>
            <%-- Previous Claim Details --%>
            <%--<div id="divPreClaim" runat="server" visible="false" class="form-section" style="padding: 20px; border: 1px solid #ddd; border-radius: 8px; background-color: #f9f9f9; max-width: 100%; overflow-x: auto;">
                <div class="section-header" style="font-size: 18px; font-weight: bold; margin-bottom: 10px;">Previous Claim Details</div>
                <div class="form-row">
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
                </div>
            </div>--%>
            <%-- end of previous claim details --%>

            <br />
            <h4 class="popup-title">Premium Details</h4>
            <hr />
            <div class="popup-grid">
                <!-- Row 1 -->
                <div class="form-group-pop">
                    <asp:Label Text="Net Premium:" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtNetPremPop" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" />
                </div>

                <div class="form-group-pop">
                    <asp:Label Text="RCC:" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtRCCPop" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" />
                </div>
                <!-- Row 2 -->
                <div class="form-group-pop">
                    <asp:Label Text="TR:" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtTRPop" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" />
                </div>
                <div class="form-group-pop">
                    <asp:Label Text="Remark:" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtRemrPop" runat="server" CssClass="form-control-pop" TextMode="MultiLine" Rows="10" />
                </div>
                <script>
                    document.addEventListener("DOMContentLoaded", function () {
                        var textbox = document.getElementById("<%= txtRemrPop.ClientID %>");

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

                <!-- Row 3 -->
                <div class="form-group-pop">
                    <asp:Label Text="Suminsured" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtSuminsuredValPop" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="false" />
                </div>
                <div class="form-group-pop">
                    <asp:Label Text="Policy No" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtPolNoPop" runat="server" CssClass="form-control-pop" ReadOnly="true" />
                </div>
                
                
                
                <!-- Row 4 -->
                
                <div class="form-group-pop">
                    <asp:Label Text="Admin Fee Precentage" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtAdminFeePre" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>
                <div class="form-group-pop">
                    <asp:Label Text="Admin Fee Value" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtAdminFeeVal" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>

                <!-- Row 5 -->
                <div class="form-group-pop">
                    <asp:Label Text="Policy Fee" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtPolFee" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>
                <div class="form-group-pop">
                    <asp:Label Text="VAT Value" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtVatValue" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>
                
                <!-- Row 6 -->
                <div class="form-group-pop">
                    <asp:Label Text="Total Premium" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtTotPre" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>
                <div class="form-group-pop">
                    <asp:Label Text="SSL Value" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtNbt" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>

            </div>
            <asp:HiddenField ID="HiddenRNyearPop" runat="server" />
            <asp:HiddenField ID="HiddenRnMonthPop" runat="server" />


            <br />
            <asp:Panel ID="Panel2" runat="server" CssClass="table-container">
                <table border="1" cellspacing="0" cellpadding="5" style="width: 100%; border-collapse: collapse;">
                    <thead style="background-color: #f1f1f1;">
                        <tr>
                            <th style="width: 5%;"><b>Excess Id</b></th>
                            <th style="width: 70%;"><b>Excess Name</b></th>
                            <th style="width: 10%;"><b>Percentage</b></th>
                            <th style="width: 20%;"><b>Amount</b></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr style="height: 60px;">
                            <td>
                                <asp:Label ID="txtExcessId1" runat="server">1</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server">All Natural Perils including Cyclone/Storm/Tempest/Flood/Earthquake (with fire & shock)</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExcePrePop1" runat="server" CssClass="form-control" placeholder="%" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                                    onkeypress="return isNumber(event);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExceAmoPo1" runat="server" CssClass="form-control" placeholder="Amount" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                                    onkeypress="return isNumber(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="txtExcessId2" runat="server">2</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" TextMode="MultiLine" Rows="2">All Other Perils</asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExcePrePop2" runat="server" CssClass="form-control" placeholder="%" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                                    onkeypress="return isNumber(event);"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtExceAmoPo2" runat="server" CssClass="form-control" placeholder="Amount" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                                    onkeypress="return isNumber(event);"></asp:TextBox>
                            </td>
                        </tr>
                        <asp:Repeater ID="rptAdditionalExcessPop" runat="server" OnItemCommand="rptAdditionalExcessPop_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:TextBox ReadOnly="true" ID="txtExcessId" runat="server" CssClass="form-control" Text='<%# Eval("ExcessId") %>' />
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
                                    <%--<td style="display: none;">
                                        <asp:HiddenField ID="hdnExcessId" runat="server" Value='<%# Eval("ExcessId") %>' />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("ExcessId") %>'
                                            Text="Remove" CssClass="btn btn-danger btn-sm" />
                                    </td>--%>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <br />
                <asp:Button ID="Button2" runat="server" Text="Add Excess" CssClass="btn btn-primary" Style="background-color: #008B80;" OnClick="btnAddRow_Click2" />
                <asp:Button ID="btnRemoveRow" runat="server" Text="Remove Excess" CssClass="btn btn-danger" OnClick="btnRemoveRow_Click2" Style="margin-left: 10px;" />
            </asp:Panel>

            <br />

            <div class="popup-buttons">
                <asp:Button ID="btnPremiumCal" runat="server" Text="Premium Calculation" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btnPrmCal_Click" OnClientClick="return clientFunctionValidation()" />
                <asp:Button ID="btnSave" enabled="false" runat="server" Text="Save" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btnEditSave_Click" OnClientClick="return clientFunctionValidation2()" />
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-secondary" OnClick="btnClose_Click" />
            </div>
        </div>
    </asp:Panel>

    <asp:HiddenField runat="server" ID="hiddenPolicyNo" />
    <script type="text/javascript">
        function showPopup() {
            document.getElementById('<%= pnlPopup.ClientID %>').style.display = 'block';
        }

        function hidePopup() {
            document.getElementById('<%= pnlPopup.ClientID %>').style.display = 'none';
        }
    </script>


    <%-- Edit popup panel end --%>

    <%-- Reject popup panel start --%>

    <asp:Panel ID="RejectPanel" runat="server" CssClass="modal-popup">
        <div class="popup-container">
            <h4 class="popup-title">Reject Panel</h4>
            <hr />
            <div class="popup-grid">
                <!-- Row 2 -->
                <div class="form-group-pop">
                    <asp:Label Text="Policy No" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtPolNoFRej" runat="server" CssClass="form-control-pop" />
                </div>

                <div class="form-group-pop">
                    <asp:Label Text="Remark:" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtRejReason" runat="server" CssClass="form-control-pop" TextMode="MultiLine" Rows="3" />
                </div>
                <script>
                    document.addEventListener("DOMContentLoaded", function () {
                        var textbox = document.getElementById("<%= txtRejReason.ClientID %>");

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

                <div class="form-group-pop">
                    <%--<asp:Label Text="RCC:" runat="server" CssClass="form-label-pop" AssociatedControlID="txtRCCPop" />
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                     onkeypress="return isNumber(event);" />--%>
                </div>
            </div>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:HiddenField ID="HiddenField2" runat="server" />


            <br />
            <br />

            <div class="popup-buttons">
                <asp:Button ID="btnReject" runat="server" Text="Reject" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btnReject_Click" OnClientClick="return clientFunctionValidation()" />
                <asp:Button ID="btnClose2" runat="server" Text="Close" CssClass="btn btn-secondary" />
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        function showPopup2() {
            document.getElementById('<%= RejectPanel.ClientID %>').style.display = 'block';
        }

        function hidePopup() {
            document.getElementById('<%= RejectPanel.ClientID %>').style.display = 'none';
        }
    </script>


    <%-- Reject popup panel end --%>

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
                    window.location.href = "/Slicgeneral/FireRenewalSMSProject/FireDefault.aspx"; // Change to your target page

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

        function custom_alert2(message, title) {
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
                })
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
            var alertType = $("#lblAlertMessage").attr("data-alert-type");
            var message = $("#lblAlertMessage").text();
            var method = $("#lblAlertMessage").attr("data-alert-method");
            //alert(method);
            if (alertType !== "" && message !== "") {
                if (method === "1") {
                    custom_alert1(message, alertType);
                } else {
                    custom_alert2(message, alertType);
                }
            }
        });


    </script>


</asp:Content>
