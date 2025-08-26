<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="FireRnViewProgress.aspx.cs" Inherits="SLIC_Fire_FireRnViewProgress" %>

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



    <script>
        // VALIDATION SCRIPT FOR NUMBER.
        function isNumber(evt) {
            var iKeyCode = (evt.which) ? evt.which : evt.keyCode
            if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
                return false;

            return true;
        }

    </script>

    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <script type="text/javascript">
        function Comma(Num) { //function to add commas to textboxes
            Num += '';
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            return x1 + x2;
        }
    </script>
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

        function clientFunctionValidation1() {

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

            //validate correct phone number
            if (reqPhone !== "") {
                //const regex = /^(?:\+94|94|0)?(70|71|72|74|75|76|77|78)\d{7}$/;
                const regex = /^(?:0|94|\+94)?(?:(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|91|92|912)(0|2|3|4|5|7|9)|7(0|1|2|5|6|7|8|4)\d)\d{6}$/;
                //var regex = /^94(70|71|72|74|75|76|77|78)\d{7}$|^94(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|91|92|94)\d{6,7}$/;

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

    <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"></asp:ToolkitScriptManager>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

    <asp:Label ID="lblAlertMessage" runat="server" ClientIDMode="Static" Style="display: none;"></asp:Label>
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />
    <div id="viewPropId" runat="server">
        <!-- Header Section -->
        <div class="form-group row" runat="server">
            <label class="col-sm-12 font-weight-bold h5 text-center" style="height: 24px">
                Not Approved - Renewal Records
            </label>
        </div>

        <!-- Policy Type Selection -->
        <div class="form-group row" runat="server">
            <div class="col-sm-3 offset-sm-1" runat="server">
                <label class="font-weight-bold">Department</label>
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

            <div class="col-sm-2 offset-sm-1">
                <label class="font-weight-bold">From</label>
                <asp:TextBox ID="txt_start_date" runat="server" CssClass="form-control text-center" placeholder="From Date" autocomplete="off"></asp:TextBox>
            </div>

            <div class="col-sm-2">
                <label class="font-weight-bold">To</label>
                <asp:TextBox ID="txt_to_date" runat="server" CssClass="form-control text-center" placeholder="To Date" autocomplete="off"></asp:TextBox>
            </div>

            <!-- ✅ New column here -->
            <div class="col-sm-2">
                <label class="font-weight-bold">SMS Status</label>
                <asp:DropDownList ID="ddl_SMSStatus" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true">
                    <%--<asp:ListItem Text="Select" Value="All" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="SMS Send" Value="A" ></asp:ListItem>
                    <asp:ListItem Text="Intermediate Level" Value="I" Selected="False"></asp:ListItem>--%>
                    <%--<asp:ListItem Text="Sum Insured Not Changed" Value="SUM_N_CHANGED" Selected="False"></asp:ListItem>--%>
                    <asp:ListItem Text="Reject" Value="R" Selected="True"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>



        <!-- Additional Search Fields -->
        <div class="form-group row" runat="server">
            <div class="form-group col-md-3 offset-sm-1">
                <label class="font-weight-bold">Status</label>
                <asp:DropDownList ID="ddl_status" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged">
                    <asp:ListItem Text="Normal" Value="N" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="With Claims" Value="WC" Selected="False"></asp:ListItem>
                    <%--<asp:ListItem Text="Sum Insured Not Changed" Value="SUM_N_CHANGED" Selected="False"></asp:ListItem>--%>
                    <asp:ListItem Text="Sum Insured Changed" Value="SUM_CHANGED" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="PPW Reinstatedated" Value="WPPWC" Selected="False"></asp:ListItem>
                </asp:DropDownList>
            </div>
            
            <div class="form-group col-md-3 offset-sm-1">
                <label class="font-weight-bold">Policy Number</label>
                <asp:TextBox ID="txt_pol_no" runat="server" CssClass="form-control text-center" placeholder="Pol. No."></asp:TextBox>
            </div>
            <div class="col-sm-2">
                <label class="font-weight-bold">Branch</label>
                <asp:DropDownList ID="ddl_branch" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </div>

            <%--<div class="col-sm-2" >
            <label class="font-weight-bold">Bank</label>
            <asp:DropDownList ID="ddl_bank" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true"
                OnSelectedIndexChanged="ddl_bank_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Value="0">-- Select --</asp:ListItem>
            </asp:DropDownList>
        </div>

            --%>
        </div>



        <!-- Action Buttons -->
        <div class="form-group row" runat="server">
            <div class="col-sm-4 offset-sm-1">
                <asp:Button ID="btn_find" runat="server" Text="Search" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btn_find_Click1" OnClientClick="return clientFunctionValidation()" />
                <asp:Button ID="btn_clear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btn_clear_Click" />
            </div>
        </div>


        <!-- Empty Column -->
        <div class="col-sm-1 mr-0 testClass" runat="server" style="left: 1px; top: 0px"></div>
    </div>

    <!-- Horizontal Line -->
    <hr class="bg-info" />

    <!-- Table Section -->

    <div id="renewGridViewSection" runat="server" class="table-responsive custom-table" style="width: auto;">
        <asp:GridView ID="Grid_Details" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="8"
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
                <asp:TemplateField HeaderText="Cus. Phone">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCusPhone" runat="server"
                            CssClass="form-control input-sm" Style="width: 120px;"
                            Text='<%# "94" + Eval("customerPhoNo") %>' AutoPostBack="true" OnTextChanged="txtCusPhone_TextChanged" />
                        <asp:CustomValidator ID="cvPhone" runat="server"
                            ErrorMessage="Invalid mobile number"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="txtCusPhone" />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:BoundField HeaderText="Sum Insured" DataField="SUM_INSURED_L" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Net Pre." DataField="BASIC_PREMIUM" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="RCC" DataField="RCC" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="TC" DataField="TC" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Admin Fee" DataField="adminFeePre" DataFormatString="{0:n2}" />
                <%--<asp:BoundField HeaderText="Policy Fee" DataField="" DataFormatString="{0:n2}" />--%>
                <%--<asp:TemplateField HeaderText="Policy Fee">
                    <ItemTemplate>
                        <asp:TextBox ID="txtPolicyFee" runat="server" Text="0"
                            AutoPostBack="true"
                            CssClass="form-control input-sm"
                            OnTextChanged="txtPolicyFee_TextChanged" />
                        <asp:HiddenField ID="hfPolicyFee" runat="server" Value="0" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="Policy Fee">
                    <ItemTemplate>
                        <asp:Label ID="lblPolicyFee" runat="server" Text='<%# String.Format("{0:n2}", 100) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:TemplateField HeaderText="Total Premium">
                    <ItemTemplate>
                        <%--<asp:Label ID="lblTotalPremium" runat="server" Text=""></asp:Label>--%>
                         <asp:Label ID="lblTotalPremium" runat="server" Text='<%# Bind("TOT_PREMIUM", "{0:n2}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NBT" Visible="false">
                    <ItemTemplate>
                        <%--<asp:Label ID="lblNbt" runat="server" Text=""></asp:Label>--%>
                        <asp:Label ID="lblNbt" runat="server" Text='<%# Eval("NBT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="VAT" Visible="false">
                    <ItemTemplate>
                        <%--<asp:Label ID="lblVat" runat="server" Text=""></asp:Label>--%>
                        <asp:Label ID="lblVat" runat="server" Text='<%# Eval("VAT") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AdminFeeVal" Visible="false">
                    <ItemTemplate>
                        <%--<asp:Label ID="lblAdminFeeVal" runat="server" Text=""></asp:Label>--%>
                        <asp:Label ID="lblAdminFeeVal" runat="server" Text='<%# Eval("ADMINFEE") %>'></asp:Label>
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
                <asp:BoundField HeaderText="Reject Reason" DataField="REJECT_REASON" />
                <asp:TemplateField HeaderText="Actions">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CommandName="EditRow" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-primary" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Select">
                    <HeaderTemplate>
                        <span style="margin-left: 5px;">Select All</span>
                        <asp:CheckBox ID="chkAll" runat="server" onclick="toggleAllCheckboxes(this);" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="chkSelect" runat="server" onclick="return validateCheckbox(this);" />
                    </ItemTemplate>
                    <HeaderStyle CssClass="text-center bg-light text-black" />
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Admin Fee Precentage" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblAminPRe" runat="server" Text='<%# Eval("adminFeePre") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

     <asp:HiddenField ID="HiddenFieldSelectedPolicy" runat="server" />

    <%--<script type="text/javascript">
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
            var btn = document.getElementById('<%= btnSentAppro.ClientID %>');
            btn.disabled = !anyChecked;
        }
    </script>--%>

    <script type="text/javascript">
        // Handle individual checkbox selection

        function validateCheckbox(cb) {
            var row = cb.closest("tr");
            var phoneInput = row.querySelector("input[id*='txtCusPhone']");
            var premiumLabel = row.querySelector("span[id*='lblTotalPremium']");
            var premium = premiumLabel ? premiumLabel.innerText.trim() : "";

            if (phoneInput) {
                var phone = phoneInput.value.trim();
               // var phoneRegex = /^94(70|71|72|74|75|76|77|78)\d{7}$/;
                const phoneRegex = /^(?:0|94|\+94)?(?:(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|91|92|912)(0|2|3|4|5|7|9)|7(0|1|2|5|6|7|8|4)\d)\d{6}$/;

                //var phoneRegex = /^94(70|71|72|74|75|76|77|78)\d{7}$|^94(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|91|92|94)\d{6,7}$/;

                if (!phoneRegex.test(phone)) {
                    custom_alert('Please enter valid customer phone number.', 'Alert');
                    cb.checked = false;
                    return false;
                }
            }

            if (premium === "0" || premium === "" || premium === "0.00") {
                custom_alert("Total Premium cannot be 0 or empty. Do Premium calculation. ", 'Alert');
                cb.checked = false;
                return false;
            }

            updateHeaderCheckbox();
            toggleInsertButton();
            return true;
        }


        // Handle "Select All" checkbox
        function toggleAllCheckboxes(source) {
            var grid = document.getElementById('<%= Grid_Details.ClientID %>');
            var checkboxes = grid.querySelectorAll("input[id*='chkSelect']");
            var validCount = 0;
            //var phoneRegex = /^94(70|71|72|74|75|76|77|78)\d{7}$/;
            const phoneRegex = /^(?:0|94|\+94)?(?:(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|91|92|912)(0|2|3|4|5|7|9)|7(0|1|2|5|6|7|8|4)\d)\d{6}$/;

            //var phoneRegex = /^94(70|71|72|74|75|76|77|78)\d{7}$|^94(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|91|92|94)\d{6,7}$/;

            for (var i = 0; i < checkboxes.length; i++) {
                var row = checkboxes[i].closest("tr");
                var phoneInput = row.querySelector("input[id*='txtCusPhone']");
                var premiumLabel = row.querySelector("span[id*='lblTotalPremium']");

                var premium = premiumLabel ? premiumLabel.innerText.trim() : "";


                if (phoneInput) {
                    var phone = phoneInput.value.trim();

                    if (phoneRegex.test(phone) && premium !== "0" && premium !== "" && premium !== "0.00") {
                        checkboxes[i].checked = source.checked;
                        validCount++;
                    } else {
                        checkboxes[i].checked = false;
                    }

                    if (phone !== "94" && phone !== "" && premium !== "0" && premium !== "" && premium !== "0.00") {
                        checkboxes[i].checked = source.checked;
                        validCount++;
                    } else {
                        checkboxes[i].checked = false;
                    }
                }
            }

            if (validCount < checkboxes.length && source.checked) {
                //alert("Some rows were not selected because they are missing a valid mobile number.");
                custom_alert('Some rows were not selected because they are missing a valid mobile number or total prmiumn.', 'Alert');
            }

            updateHeaderCheckbox();
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
            var btn = document.getElementById('<%= btnSentAppro.ClientID %>');
            btn.disabled = !anyChecked;
        }
    </script>

    <%-- Edit popup panel start --%>

    <asp:Panel ID="pnlPopup" runat="server" CssClass="modal-popup">
        <div class="popup-container">
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
                <!-- Row 1 -->
                <!-- Row 3 -->
                <div class="form-group-pop">
                    <asp:Label Text="Policy Fee" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtPolFee" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>
                <div class="form-group-pop">
                   <%-- <asp:Label Text="Admin Fee Precentage" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />--%>
                </div>
                <!-- Row 4 -->
                <div class="form-group-pop">
                    <asp:Label Text="Policy No" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtPolNoPop" runat="server" CssClass="form-control-pop" ReadOnly="true" />
                </div>
                <div class="form-group-pop">
                    <asp:Label Text="Admin Fee Precentage" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtAdminFeePre" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>

                <!-- Row 5 -->
                <div class="form-group-pop">
                    <asp:Label Text="Total Premium" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtTotPre" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>
                <div class="form-group-pop">
                    <asp:Label Text="NBT Value" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtNbtPop" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>
                <!-- Row 6 -->
                <div class="form-group-pop">
                    <asp:Label Text="VAT Value" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtVatValue" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                        onkeypress="return isNumber(event);" ReadOnly="true" />
                </div>
                <div class="form-group-pop">
                    <asp:Label Text="Admin Fee Value" runat="server" CssClass="form-label-pop" />
                    <asp:TextBox ID="txtAdminFeeVal" runat="server" CssClass="form-control-pop" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
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
                        <asp:Repeater ID="rptAdditionalExcessPop" runat="server">
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
                <asp:Button ID="Button1" runat="server" Text="Add Excess" CssClass="btn btn-primary" Style="background-color: #008B80;" OnClick="btnAddRow_Click2" />
                <asp:Button ID="btnRemoveRow" runat="server" Text="Remove Excess" CssClass="btn btn-danger" OnClick="btnRemoveRow_Click2" Style="margin-left: 10px;" />
            </asp:Panel>

            <br />

            <div class="popup-buttons">
                <asp:Button ID="btnPremiumCal" runat="server" Text="Premium Calculation" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btnPrmCal_Click" OnClientClick="return clientFunctionValidation()" />
                <asp:Button ID="btnSave" runat="server" Text="Save" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btnEditSave_Click" OnClientClick="return clientFunctionValidation1()" />
                <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-secondary" OnClick="btnClose_Click"/>
            </div>
        </div>
    </asp:Panel>

    <script type="text/javascript">
        function showPopup() {
            document.getElementById('<%= pnlPopup.ClientID %>').style.display = 'block';
        }

        function hidePopup() {
            document.getElementById('<%= pnlPopup.ClientID %>').style.display = 'none';
        }
    </script>


    <%-- Edit popup panel end --%>


    <%--<script type="text/javascript">
        function validatePhone(sender, args) {
            var phone = args.Value;
            var regex = /^94\d{9}$/; 
            args.IsValid = regex.test(phone);
        }
    </script>--%>

    <%-- table for sum insured not changed --%>
    <!-- Table Section -->

    <div id="DivSumInsuNotCha" runat="server" class="table-responsive custom-table" style="width: auto;" visible="false">
        <asp:GridView ID="SunInsuNotChangedGrid" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="8"
            CssClass="table table-striped table-hover table-bordered text-nowrap " Width="100%" DataKeyNames="POLICY_NO" OnSelectedIndexChanged="SunInsuNotChangedGrid_SelectedIndexChanged">
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
                <asp:TemplateField HeaderText="Cus. Phone">
                    <ItemTemplate>
                        <asp:TextBox ID="txtCusPhone" runat="server"
                            CssClass="form-control input-sm"
                            Text='<%# "94" + Eval("customerPhoNo") %>' AutoPostBack="true" OnTextChanged="txtCusPhone_TextChanged2" />
                        <asp:CustomValidator ID="cvPhone" runat="server"
                            ErrorMessage="Invalid mobile number"
                            ForeColor="Red"
                            Display="Dynamic"
                            ControlToValidate="txtCusPhone" />
                    </ItemTemplate>
                </asp:TemplateField>


                <asp:BoundField HeaderText="Sum Insured L" DataField="SUM_INSURED_L" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Sum Insured LL" DataField="SUM_INSURED_LL" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Sum Insured LLL" DataField="SUM_INSURED_LLL" DataFormatString="{0:n2}" />

                <asp:TemplateField HeaderText="Net Pre." Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblNetPRe" runat="server" Text='<%# Eval("BASIC_PREMIUM") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RCC" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblRCC" runat="server" Text='<%# Eval("RCC") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TC" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblTc" runat="server" Text='<%# Eval("TC") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="adminfee" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblAdminFee" runat="server" Text='<%# Eval("adminfee") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>


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


            </Columns>
        </asp:GridView>
    </div>

    <%-- end of table sum insured not changed  --%>

    <%-- Grid for With claim policy list --%>

    <div id="Div1" runat="server" class="table-responsive custom-table" style="width: auto;" visible="true">
        <asp:GridView ID="WithClaimGrid" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="8"
            CssClass="table table-striped table-hover table-bordered text-nowrap " Width="100%" DataKeyNames="POLICY_NO">
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
                <asp:BoundField HeaderText="Cus. Phone" DataField="customerPhoNo" />

                <asp:BoundField HeaderText="Sum Insured L" DataField="SUM_INSURED_L" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Sum Insured LL" DataField="SUM_INSURED_LL" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Sum Insured LLL" DataField="SUM_INSURED_LLL" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Net Pre." DataField="BASIC_PREMIUM" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="RCC" DataField="RCC" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="TC" DataField="TC" DataFormatString="{0:n2}" />
                <asp:BoundField HeaderText="Admin Fee" DataField="adminFeePre" DataFormatString="{0:n2}" />


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
                <asp:BoundField HeaderText="Reject Reason" DataField="REJECT_REASON" />
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="btn btn-primary" OnClick="btn_Edit_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateField>



            </Columns>
        </asp:GridView>
    </div>

    <%-- end of  Grid for With claim policy list --%>


    <div class="form-group row" runat="server">
        <div class="col-sm-4 offset-sm-1">
            <%--<asp:Button ID="btn_calPremium" runat="server" Text="Premium Calculation" Visible="true" CssClass="btn btn-success" Style="background-color: #008B80;" OnClick="btn_PremCal_Click1" OnClientClick="return clientFunctionValidation()" />--%>
            <asp:Button ID="btnSentAppro" runat="server" Text="Send to Approve" Visible="true" CssClass="btn btn-info" Style="background-color: #008B80;" OnClick="btnSentAprr_Click" OnClientClick="return clientFunctionValidation()" />
            <%--<asp:Button ID="btnSmsSent" runat="server" Text="Send SMS" Visible="false" CssClass="btn btn-info" Style="background-color: #008B80;" OnClick="btnSentSMS_Click" OnClientClick="return clientFunctionValidation()" />--%>
        </div>
    </div>

    <!-- Data Insert Popup Modal -->

    <!-- Success Modal -->
    <div class="modal fade" id="successModal" tabindex="-1" role="dialog" aria-labelledby="successModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="successModalLabel">Success</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Data inserted successfully!
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
    </div>

    <!-- Error Modal -->
    <div class="modal fade" id="errorModal" tabindex="-1" role="dialog" aria-labelledby="errorModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="errorModalLabel">Error</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    Failed to insert data. Please try again!
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-danger" data-dismiss="modal">OK</button>
                </div>
            </div>
        </div>
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
    </style>

    <asp:Panel ID="Panel1" runat="server" CssClass="container">
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
                    <div class="form-group col-md-4">
                        <label>Sum Insured :</label>
                        <asp:Label ID="lblSumYear2" runat="server"></asp:Label>
                        <asp:TextBox ID="txtSumInsured2" runat="server" CssClass="form-control text-center " ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-4">
                        <label>Sum Insured :</label>
                        <asp:Label ID="lblSumYear3" runat="server"></asp:Label>
                        <asp:TextBox ID="txtSumInsured3" runat="server" CssClass="form-control text-center " ReadOnly="true"></asp:TextBox>
                    </div>
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
                        <asp:TextBox ID="txtPolicyFee1" runat="server" CssClass="form-control text-center" Text="100.00" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                        <label runat="server" id="txtPolicyFee1Val" class="text-danger" style="display: none; width: 100%; text-align: center;" text="">*Required!</label>
                    </div>
                    <div class="form-group col-md-3">
                        <label>NBT:</label>
                        <asp:TextBox ID="txtNbt" runat="server" CssClass="form-control text-center " ReadOnly="true" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label>VAT:</label>
                        <asp:TextBox ID="txtVat" runat="server" CssClass="form-control text-center " ReadOnly="true" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
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
                    <div class="form-group col-md-3">
                        <label>Admin Fee:</label>
                        <asp:TextBox ID="txtAdminFee2" runat="server" CssClass="form-control text-center " ReadOnly="true" step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label>Remark:</label>
                        <asp:TextBox ID="txtRemark" runat="server" CssClass="form-control text-center " TextMode="MultiLine" Rows="4"></asp:TextBox>
                    </div>

                </div>

                <div class="form-row">
                    <asp:Panel ID="pnlExcessTable" runat="server" CssClass="table-container">
                        <table border="1" cellspacing="0" cellpadding="5" style="width: 100%; border-collapse: collapse;">
                            <thead style="background-color: #f1f1f1;">
                                <tr>
                                    <th class="auto-style1" style="width: 5%;"><b>Excess Id</b></th>
                                    <th style="width: 70%;"><b>Excess Name</b></th>
                                    <th style="width: 10%;"><b>Percentage</b></th>
                                    <th style="width: 20%;"><b>Amount</b></th>
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
                        <asp:Button ID="btnSendToApp" runat="server" Text="Send to Approve" CssClass="btn btn-info" Visible="false" Style="background-color: #008B80;" OnClick="btn_sentToApp_Click" OnClientClick="return clientFunctionValidation()" />
                        <asp:Button ID="Button3" runat="server" Text="Send SMS" Visible="false" CssClass="btn btn-info" Style="background-color: #008B80;" OnClientClick="return clientFunctionValidation()" />
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
                    // window.location.href = "/Slicgeneral/FireRenewalSMSProject/Default.aspx"; // Change to your target page
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
