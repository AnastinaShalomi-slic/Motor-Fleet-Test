<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage2.master" AutoEventWireup="true" CodeFile="FireRenewalSumNotChanged.aspx.cs" Inherits="SLIC_Fire_FireRenewalSumNotChanged" %>

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
                const regex = /^(?:0|94|\+94)?(?:(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|912)(0|2|3|4|5|7|9)|7(0|1|2|5|6|7|8|4)\d)\d{6}$/;
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

            window.setTimeout(function () {
                $(".alert").fadeTo(500, 0).slideUp(500, function () {
                    $(this).remove();
                });
            }, 5000);



        });
    </script>


    <script>    
        function showRenewForm() {


            <%--var gridView = document.getElementById('<%= renewGridViewSection.ClientID %>');

            if (gridView) {
                gridView.style.display = "none"; // Hide GridView section
            } else {
                alert("GridView section not found!");
            }

            if (renewForm) {
                renewForm.style.display = "block"; // Show Renewal form
            } else {
                alert("Renewal form section not found!");
            }--%>

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
                Fire Policy - Renewal
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
                            <%--<asp:ListItem Text="Bancassurance" Value="B" Selected="True"></asp:ListItem>--%>
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
        </div>

        

        <!-- Additional Search Fields -->
        <div class="form-group row" runat="server">
            <div class="form-group col-md-3 offset-sm-1">
                <label class="font-weight-bold">Status</label>
                <asp:DropDownList ID="ddl_status" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlTerm_SelectedIndexChanged">
                    <asp:ListItem Text="Sum Insured Not Changed" Value="SUM_N_CHANGED" Selected="False"></asp:ListItem>
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

            <div class="col-sm-2" runat="server" id="divBudiUnit" visible="false">
                <label class="font-weight-bold">Business Unit</label>
                <asp:DropDownList ID="ddlBusUnit" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true" >
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                    <asp:ListItem Value="334">CBD-BK Branch</asp:ListItem>
                    <asp:ListItem Value="333">Key Account</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>

        <!-- Search Fields -->
        <div class="form-group row" runat="server">
           <div class="form-group col-md-3 offset-sm-1">
                <label class="font-weight-bold">Sub Department</label>
                <asp:DropDownList ID="ddlSubDept" runat="server" CssClass="custom-select text-center" AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlsubDept_SelectedIndexChanged">
                    <asp:ListItem Text="Select" Value="N" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="Engineering" Value="EN" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="Fire" Value="FI" Selected="False"></asp:ListItem>                   
                </asp:DropDownList>
            </div>
            <div class="form-group col-md-3 offset-sm-1" runat="server" id="divEngineering" visible="false">
                <label class="font-weight-bold">Engineering Products</label>
                <asp:DropDownList ID="ddlEngineering" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true" >
                    <asp:ListItem Text="Select" Value="N" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="FEE" Value="FEE" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="FMB" Value="FMB" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="FPV" Value="FPV" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="CMI" Value="CMI" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="FPM" Value="FPM" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="FCM" Value="FCM" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="FCS" Value="FCS" Selected="False"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="form-group col-md-3 offset-sm-1" runat="server" id="divFire" visible="false">
                <label class="font-weight-bold">Fire Products</label>
                <asp:DropDownList ID="ddlFirePro" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true" >
                    <asp:ListItem Text="Select" Value="N" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="FPD" Value="FPD" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="FBP" Value="FBP" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="FTC" Value="FTC" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="FHC" Value="FHC" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="FRT" Value="FRT" Selected="False"></asp:ListItem>
                    <asp:ListItem Text="FCL" Value="FCL" Selected="False"></asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>


        <!-- Term and Proposal Type Selection -->
        <div class="form-group row" runat="server" style="display: none">
            <div class="col-sm-2 offset-sm-1">
                <label class="font-weight-bold">Term</label>
                <asp:DropDownList ID="ddlTerm" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true">
                    <asp:ListItem Text="All" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Annual" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Long Term" Value="0"></asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-sm-2">
                <label class="font-weight-bold">Proposal Types</label>
                <asp:DropDownList ID="ddlPropType" runat="server" CssClass="custom-select text-center" AppendDataBoundItems="true">
                    <asp:ListItem Text="All" Value="A"></asp:ListItem>
                    <asp:ListItem Text="Private Dwelling House Only" Value="1"></asp:ListItem>
                    <asp:ListItem Text="Solar Panel Only" Value="3"></asp:ListItem>
                </asp:DropDownList>
            </div>
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




    <%-- table for sum insured not changed --%>
    <!-- Table Section -->

    <div id="DivSumInsuNotCha" runat="server" class="table-responsive custom-table" style="width: auto;" visible="false">
        <asp:GridView ID="SunInsuNotChangedGrid" runat="server" AutoGenerateColumns="false" AllowPaging="false" PageSize="8"
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


    <div class="form-group row" runat="server">
        <div class="col-sm-4 offset-sm-1">
            <%--<asp:Button ID="btn_calPremium" runat="server" Text="Premium Calculation" Visible="false" CssClass="btn btn-success" Style="background-color: #008B80;" OnClick="btn_PremCal_Click1" OnClientClick="return clientFunctionValidation()" />
            <asp:Button ID="btnSentAppro" runat="server" Text="Send to Approve" Visible="false" CssClass="btn btn-info" Style="background-color: #008B80;" OnClick="btnSentAprr_Click" OnClientClick="return clientFunctionValidation()" />--%>
            <asp:Button ID="btnSmsSent" runat="server" Text="Send SMS" Visible="true" CssClass="btn btn-info" Style="background-color: #008B80;" OnClick="btnSentSMS_Click" OnClientClick="return clientFunctionValidation()" />
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
                        <asp:TextBox ID="txtPolicyFee1" runat="server" CssClass="form-control text-center " step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
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
                    <div class="form-group col-md-3">
                        <label>Excess Precentage:</label>
                        <asp:TextBox ID="txtExcessPre" runat="server" CssClass="form-control text-center " step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-3">
                        <label>Excess Amount:</label>
                        <asp:TextBox ID="txtExcessAmo" runat="server" CssClass="form-control text-center " step="0.01" onkeyup="javascript:this.value=Comma(this.value);"
                            onkeypress="return isNumber(event);"></asp:TextBox>
                    </div>
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
                                    <th style="width: 70%;"><b>Excess Name</b></th>
                                    <th style="width: 15%;"><b>Percentage</b></th>
                                    <th style="width: 15%;"><b>Amount</b></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
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
                            </tbody>
                        </table>
                    </asp:Panel>
                </div>



                <br />
                <div class="form-row">
                    <div class="form-group col-md-3">
                        <%-- OnClick="btn_PremCal_Click1" OnClick="btnSentAprr_Click" OnClick="btnSentSMS_Click"--%>
                       <%-- <asp:Button ID="btnPreCal" runat="server" Text="Premium Calculation" CssClass="btn btn-success" OnClick="btn_PremCal_Click2" Style="background-color: #008B80;" OnClientClick="return clientFunctionValidation2()" />--%>
                    </div>
                    <div class="form-group col-md-3">
                        <%--<asp:Button ID="btnSendToApp" runat="server" Text="Send to Approve" CssClass="btn btn-info" Visible="false" Style="background-color: #008B80;" OnClick="btn_sentToApp_Click" OnClientClick="return clientFunctionValidation()" />
                        <asp:Button ID="Button3" runat="server" Text="Send SMS" Visible="false" CssClass="btn btn-info" Style="background-color: #008B80;" OnClientClick="return clientFunctionValidation()" />--%>
                    </div>
                </div>

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


