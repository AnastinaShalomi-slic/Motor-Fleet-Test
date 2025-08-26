<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_OD.master" AutoEventWireup="true" CodeFile="ProposalEntry.aspx.cs" Inherits="OdProtect_Bank_ProposalEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <!-- Bootstrap DatePicker -->
    <script src="~/Scripts/bootstrap-datepicker.min.js" type="text/javascript"></script>
    <link rel="Stylesheet" href="../../JavaScripts/select2.css" />
    <script src="../../JavaScripts/select2.js"></script>

<style type="text/css">
   
    .swal-overlay {
        background-color: rgba(43, 165, 137, 0.45);
    }

    .form-group {
        margin-bottom: 5px;
    }

    .compGap {
        margin-top: 10px;
    }

    .ToUpper{
        text-transform: uppercase;
    }

    .rfvControl {
        color: #D50000;
        font-size: 0.90em;
        font-weight: 500;
    }

    input[type=text]:read-only {
        background: #ffffff;
    }

    .setbrakdown {
        text-align: right;
    }

     .modalBackground {
        background-color: Black;
        filter: alpha(opacity=80);
        opacity: 0.4;
    }
    .cleare {
        padding: 0px;
        margin: 0px;
    }
    .auto-style1 {
        position: relative;
        width: 100%;
        -ms-flex: 0 0 58.333333%;
        flex: 0 0 58.333333%;
        max-width: 58.333333%;
        top: 0px;
        left: 0px;
        padding-left: 15px;
        padding-right: 15px;
    }
 
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript">

        var toDate = new Date().format('yyyy/dd/mm');

         function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }

         function Message(title, message, icon) {
            swal
                ({
                    title: title,
                    text: message,
                    icon: icon,
                    button: true,
                    closeOnClickOutside: false
                });
        }

        $(function () {
            $("#ddlSlicCode").select2();           
        });
     
        $(function () {
            $('[id*=txt_DOB]').datepicker({
                format: "yyyy/dd/mm",
                language: "tr",
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                yearRange: '1959:+65',
                dateFormat: 'yy/mm/dd',
                defaultDate: +0,
                numberOfMonths: 1,
                maxDate: toDate,
                showAnim: 'slideDown',
                showButtonPanel: false,
                showWeek: false,
                firstDay: 1,
                stepMonths: 0,
                //showOn: "off"
                buttonImage: "../Images/delete.png",
                todayBtn: true,
                todayHighlight: true,
                //buttonImageOnly: true,
                buttonText: "Select date",
                onSelect: function () {
                    document.getElementById('<%= txt_nic.ClientID %>').value = '';
                    document.getElementById('<%= cvalNicMatchDob.ClientID %>').innerText = '';
                }
            });
        });
    

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

        function onlyNumbers(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }

        function isLetter() {
            var charCode = event.keyCode;

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || (charCode == " " || charCode == "Space" || charCode == 32 || charCode == 46  ))

                return true;
            else
                return false;
        }

        function Calculate_Tax(val) {

            let overdraft_amount = parseFloat(val.replace(/\D/g, "")).toFixed(2);
            let srcc = parseFloat(<%=ConfigurationManager.AppSettings["SRCC"] %>).toFixed(2);
            let tc = parseFloat(<%=ConfigurationManager.AppSettings["TC"] %>).toFixed(2);
       
            let net_premium = parseFloat((overdraft_amount * (1.9 / 100))).toFixed(2);
            document.getElementById("<%=txt_Netpremium.ClientID%>").value = Comma(net_premium);

            document.getElementById("<%=txt_SRCC.ClientID%>").value = Comma(srcc);
            document.getElementById("<%=txt_TC.ClientID%>").value = Comma(tc);

            
            //Policy fee calculation
            let policy_fee = 0.00;
            if (overdraft_amount > 499999) {
                policy_fee = 1250;                
            }

            else if ((overdraft_amount > 99999.99) && (overdraft_amount < 499999)) {
                policy_fee = 1000;
            }

            else if (overdraft_amount < 99999.99) {
                policy_fee = 700;
            }

            else
                policy_fee = 0;

            document.getElementById("<%=txtPolicyFee.ClientID%>").value = Comma(policy_fee.toFixed(2));
            //End :: Policy fee calculation


            //Calculate adminfee
            let inist_cal_adfee = (parseFloat(net_premium) + parseFloat(srcc) + parseFloat(tc)).toFixed(2);
            let inist_cal_adfee_ptag = (parseFloat(inist_cal_adfee) * (0.35 / 100)).toFixed(2);
            let admin_fee = ((parseFloat(inist_cal_adfee_ptag) + (((parseFloat(inist_cal_adfee) + parseFloat(inist_cal_adfee_ptag)) +  parseFloat(policy_fee)) * (2.5641 / 100))));
            document.getElementById("<%=txtAdminFee.ClientID%>").value =  Comma(parseFloat(admin_fee).toFixed(2));
            //End :: of Calculate adminfee

            //Vat Calculation
            let vat_calculation = ((parseFloat(inist_cal_adfee) + parseFloat(policy_fee) + parseFloat(admin_fee)) * (parseFloat(<%=ConfigurationManager.AppSettings["vat"] %>) / 100));
            document.getElementById("<%=tctVat.ClientID%>").value = Comma(vat_calculation.toFixed(2));
            //End :: Of vat Calculation

            //Calculate :: Total payabal amount
            let total_val = (parseFloat(net_premium) + parseFloat(policy_fee) + parseFloat(admin_fee) + parseFloat(vat_calculation));
            document.getElementById("<%=txt_sumInsuTotal.ClientID%>").value = Comma(total_val.toFixed(2));
            //End :: Calculate :: Total payabal amount
        }


         function CleareProposal() {
             $("#<%= ddlInitials.ClientID%>")[0].selectedIndex = 0;
             document.getElementById('<%= txt_CusName.ClientID %>').value = '';
             document.getElementById('<%= txt_addline1.ClientID %>').value = '';
             document.getElementById('<%= txt_addline2.ClientID %>').value = '';
             document.getElementById('<%= txt_addline3.ClientID %>').value = '';
             document.getElementById('<%= txt_addline4.ClientID %>').value = '';
             document.getElementById('<%= txt_DOB.ClientID %>').value = '';
             document.getElementById('<%= txt_nic.ClientID %>').value = '';
             document.getElementById('<%= txt_tele.ClientID %>').value = '';
             document.getElementById('<%= txt_email.ClientID %>').value = '';
             document.getElementById('<%= txtBusType.ClientID %>').value = '';
             document.getElementById('<%= txt_odLimit.ClientID %>').value = '';
             document.getElementById('<%= txt_Netpremium.ClientID %>').value = '';
             document.getElementById('<%= txt_SRCC.ClientID %>').value = '';
             document.getElementById('<%= txt_TC.ClientID %>').value = '';
             document.getElementById('<%= txtAdminFee.ClientID %>').value = '';
             document.getElementById('<%= txtPolicyFee.ClientID %>').value = '';
             document.getElementById('<%= tctVat.ClientID %>').value = '';
             document.getElementById('<%= txt_sumInsuTotal.ClientID %>').value = '';
             $("#<%= ddlSlicCode.ClientID%>")[0].selectedIndex = 0;
        }


        function closeEditPopup() {             
             $find("ConfirmProposal").hide();
             return false;
        }
     
        function ClosePrintAdvice() {             
             $find("printadvice").hide();
             return false;
        }

        
        $(document).ready(function () {
                 
         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
            function EndRequestHandler(sender, args) {

                 $(function () {
                    $("#ddlSlicCode").select2();
                });
                                     
         }
     });
               
    </script>
 
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" >
    </asp:ToolkitScriptManager>

    <asp:HiddenField ID="bank_code" runat="server" />
    <asp:HiddenField ID="branch_code" runat="server" />
    <asp:HiddenField ID="userName_code" runat="server" />
    <asp:HiddenField ID="bancass_email" runat="server" />

    <asp:HiddenField ID="auth_Code" runat="server" />

      <div class="container border border-info" id="mainDiv" runat="server">
          <div class="form-group row" runat="server" id="rowFirst">
              <div class="col-sm-3 text-xl-left h5 font-weight-bold" style="padding:15px 10px 0px 15px">       
                  <asp:Image ID="Image1" runat="server" ImageUrl="~/OdProtect/images/b_assurance_logo.png" Width="165px" Height="120px" />
              </div>
              <div class="col-sm-9 text-xl-left h5 font-weight-bold" style="padding:40px 0px 0px 10px;">       
                  <h3><u>Overdraft Proposal Entry Form</u></h3>
              </div>
          </div>
          <div class="form-group row" >
              <div class="col-sm-3">1. Name of the Customer<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-1 p-0 ml-3 mr-2">
                  <asp:DropDownList ID="ddlInitials" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false" >
                      <asp:ListItem Value="0">Select</asp:ListItem>
                      <asp:ListItem Value="Dr. ">Dr</asp:ListItem>
                      <asp:ListItem Value="Mr. ">Mr</asp:ListItem>
                      <asp:ListItem Value="Mrs. ">Mrs</asp:ListItem>
                      <asp:ListItem Value="Miss. ">Miss</asp:ListItem>
                      <asp:ListItem Value="Ms. ">Ms</asp:ListItem>
                      <asp:ListItem Value="Rev. ">Rev</asp:ListItem>
                  </asp:DropDownList>
                  <asp:RequiredFieldValidator ID="rfvInitials" ErrorMessage="Required" ControlToValidate="ddlInitials" InitialValue="0" runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>
              <div class="col-sm-7">
                  <asp:TextBox ID="txt_CusName" runat="server" CssClass="form-control ToUpper" ClientIDMode="Static" autocomplete="off" onkeypress="return isLetter();" MaxLength ="75"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfvCusName" ErrorMessage="Customer name can't be blank." ControlToValidate="txt_CusName"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>        
          </div>

           <div class="form-group row compGap">
              <div class="col-sm-3">2. Postal Address Line 1<asp:Label runat="server" ForeColor="Red">*</asp:Label>                
              </div>
              <div class="col-sm-5">
                  <asp:TextBox ID="txt_addline1" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" MaxLength ="65"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfvAdLine1" ErrorMessage="Adderss Line-1 can't be blank." ControlToValidate="txt_addline1"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>           
          </div>

          <div class="form-group row compGap">
              <div class="col-sm-3 pl-5">Address Line 2<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-5">
                  <asp:TextBox ID="txt_addline2" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="false" autocomplete="off" MaxLength ="65"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvAdLine2" ErrorMessage="Adderss Line-2 can't be blank." ControlToValidate="txt_addline2"  runat="server" ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>             
          </div>

          <div class="form-group row compGap">
              <div class="col-sm-3 pl-5">Address Line 3</div>
              <div class="col-sm-5">
                  <asp:TextBox ID="txt_addline3" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="false" autocomplete="off" MaxLength ="60"></asp:TextBox></div>            
          </div>

          <div class="form-group row compGap" runat="server" id="Div7">
              <div class="col-sm-3 pl-5">Address Line 4</div>
              <div class="col-sm-5">
                   <asp:TextBox ID="txt_addline4" runat="server" CssClass="form-control" ClientIDMode="Static" AutoPostBack="false" autocomplete="off" MaxLength ="60"></asp:TextBox>        
              </div>            
          </div>

          <div class="form-group row compGap" runat="server" id="Div15">
              <div class="col-sm-3">3. Date of Birth (yyyy/mm/dd) <asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-3 form-inline">
                  <asp:TextBox ID="txt_DOB" runat="server" CssClass="form-control" autocomplete="off" onkeydown="return false">                       
                  </asp:TextBox>
                  	<div class="input-group-prepend"">
		              <span class="input-group-text"> <i class="fa fa-calendar" style="padding:calc(0.2em + 1px);"></i> </span>
                    </div>    
            </div>
               <asp:RequiredFieldValidator ID="rfvDob" ErrorMessage="Date Of birth can't be blank" ControlToValidate="txt_DOB"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
          </div>

          <div class="form-group row compGap" runat="server" id="Div2">
              <div class="col-sm-3">4. NIC Number<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-3">
                  <asp:UpdatePanel ID="updatepnl" runat="server" UpdateMode="Conditional">  
                  <ContentTemplate>  
                  <asp:TextBox ID="txt_nic" runat="server" CssClass="form-control text-capitalize" ClientIDMode="Static" autocomplete="off" MaxLength="12" OnTextChanged="txt_nic_TextChanged" AutoPostBack ="true"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvNic" ErrorMessage="NIC can't be blank" ControlToValidate="txt_nic"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
                                  
                  <asp:RegularExpressionValidator ID="rfvNicFmt" ValidationGroup="rfvODP" Display="Dynamic" ValidationExpression="^([0-9]{9}[x|X|v|V]|[0-9]{12})$" ControlToValidate="txt_nic" runat="server">
                     <small id="rmsgrfvnic" class="form-text rfvControl">Please enter a valid NIC number.</small>
                  </asp:RegularExpressionValidator>

                   <asp:CustomValidator ID="cvalNicMatchDob" runat="server"  ErrorMessage="NIC does not matched with date of birth" OnServerValidate="Validate_Numeric" ControlToValidate="txt_nic" Display="Dynamic" CssClass="rfvControl" ValidationGroup="rfvODP" ></asp:CustomValidator>
                   </ContentTemplate>
                  </asp:UpdatePanel>
              </div>            
          </div>
        
          <div class="form-group row compGap" runat="server" id="Div8">
              <div class="col-sm-3">5. Mobile No<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-3">
                  <asp:TextBox ID="txt_tele" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" onKeypress="return onlyNumbers(event);" MaxLength="10"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfvContact" ErrorMessage="Contact number can't be blank" ControlToValidate="txt_tele"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
                   
                  <asp:RegularExpressionValidator ID="rfv_Vcno" ValidationGroup="rfvODP" Display="Dynamic" ValidationExpression="^(?:07|7(?:\+94))[1|2|4|5|6|7][0-9]{9,10}|(?:07|7)[1|2|4|5|6|7|8][0-9]{7}$" ControlToValidate="txt_tele" runat="server">
                     <small id="rmsg12" class="form-text rfvControl">Please enter a valid contact number.</small>
                    </asp:RegularExpressionValidator>
              </div>             
          </div>
       
          <div class="form-group row compGap" runat="server" id="Div10">
              <div class="col-sm-3">6. Email Address</div>
              <div class="col-sm-7">
                  <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" ClientIDMode="Static" TextMode="Email" autocomplete="off" MaxLength ="100"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="rfvemail" ValidationGroup="rfvODP" Display="Dynamic" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" ControlToValidate="txt_email" runat="server">
                     <small id="rmsemail" class="form-text rfvControl">Please enter a valid e-mail address.</small>
                    </asp:RegularExpressionValidator>
              </div>             
          </div>


          <div class="form-group row compGap" runat="server" id="Div3">
              <div class="col-sm-3">7. Nature of the Business<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-5">
                  <asp:TextBox ID="txtBusType" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" MaxLength="50"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfvBusType" ErrorMessage="Please enter business type" ControlToValidate="txtBusType"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>
          </div>

          <div class="form-group row compGap" runat="server" id="Div17">
              <div class="col-sm-3">8. Limit of the OD Facility (LKR)<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-3">
                  <%--<asp:TextBox ID="txt_odLimit" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="form-control" onkeypress="javascript:return onlyNumbers(event);" onkeyup="javascript:this.value=Comma(this.value); Calculate_Tax(this.value);"  autocomplete="off" MaxLength ="8"></asp:TextBox>                 --%>
                  <asp:TextBox ID="txt_odLimit" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="form-control" onkeypress="javascript:return onlyNumbers(event);" onkeyup="javascript:this.value=Comma(this.value); Calculate_Tax(this.value);"  autocomplete="off" MaxLength ="8"></asp:TextBox>                 
                  <asp:RequiredFieldValidator ID="rfvodLimit" ErrorMessage="Overdraft value can't be blank" ControlToValidate="txt_odLimit"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
                  <asp:RangeValidator ID="rV_OD_amt" runat="server" Type="Currency"  Display="Dynamic" CssClass="rfvControl"
                    MinimumValue="0" MaximumValue="1000000" ControlToValidate="txt_odLimit" 
                    ErrorMessage="Cant Accept Overdraft Facility. Max Facility Allowed is Rs.1,000,000.00"  ValidationGroup ="rfvODP"></asp:RangeValidator>
              </div>
          </div>

          <div class="form-group row" runat="server" id="Div18" style="margin-top:15px;">
              <div class="col-sm-10" style="font-size:1.2em; font-weight:600">Premium Calculation</div>
              <div class="col-sm-0"></div>
              <div class="col-sm-2"></div>
          </div>

          <div class="form-group row" runat="server" id="Div33" style="margin-top:15px;">
              <div class="col-sm-2"></div>
              <div class="col-sm-6 text-center border border-dark">Property to be insured </div>
              <div class="col-sm-2 text-center border border-dark">Sum insured (LKR) <asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-2"></div>
          </div>

          <div class="form-group row" runat="server" id="Div19" style="display: normal">
              <div class="col-sm-2"></div>
              <div class="col-sm-6 border border-dark">Net Premium.<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-2 border border-dark p-1">
                  <asp:TextBox ID="txt_Netpremium" runat="server" ClientIDMode="Static" CssClass="form-control setbrakdown" onkeypress="javascript:return onlyNumbers(event);" onkeyup="javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvSi" ErrorMessage="Required" ControlToValidate="txt_Netpremium"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>             
          </div>


          <div class="form-group row" runat="server" id="Div20" style="display: none">
              <div class="col-sm-2"></div>
              <div class="col-sm-6 border border-dark">Strike, Riot and Civil Commotion (SRCC)</div>
              <div class="col-sm-2 border border-dark p-1">
                  <asp:TextBox ID="txt_SRCC" runat="server" ClientIDMode="Static" CssClass="form-control setbrakdown" onkeypress="javascript:return onlyNumbers(event)" onkeyup="javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvSrcc" ErrorMessage="Required" ControlToValidate="txt_SRCC"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>             
          </div>

          <div class="form-group row" runat="server" id="Div20I" style="display: none">
              <div class="col-sm-2"></div>
              <div class="col-sm-6 border border-dark">Terrorist Cover (TC)<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-2 border border-dark p-1">
                  <asp:TextBox  ID="txt_TC" runat="server" ClientIDMode="Static" CssClass="form-control setbrakdown" onkeypress="javascript:return onlyNumbers(event)" onkeyup="javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="rfv_Tc" ErrorMessage="Required" ControlToValidate="txt_TC"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>           
          </div>

          <div class="form-group row" runat="server" id="Div9" style="display: normal">
              <div class="col-sm-2"></div>
              <div class="col-sm-6 border border-dark">Admin Fee<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-2 border border-dark p-1">
                  <asp:TextBox  ID="txtAdminFee" runat="server" ClientIDMode="Static" CssClass="form-control setbrakdown" onkeypress="javascript:return onlyNumbers(event)" onkeyup="javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvAdfee" ErrorMessage="Required" ControlToValidate="txtAdminFee"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>           
          </div>

           <div class="form-group row" runat="server" id="Div11" style="display: normal">
              <div class="col-sm-2"></div>
              <div class="col-sm-6 border border-dark">Policy Fee<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-2 border border-dark p-1">
                  <asp:TextBox ID="txtPolicyFee" runat="server" ClientIDMode="Static" CssClass="form-control setbrakdown" onkeypress="javascript:return onlyNumbers(event)" onkeyup="javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvPFee" ErrorMessage="Required" ControlToValidate="txtPolicyFee"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>           
          </div>
          <div class="form-group row" runat="server" id="Div12" style="display: normal;">
              <div class="col-sm-2"></div>
              <div class="col-sm-6 border border-dark">VAT<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-2 border border-dark p-1">
                  <asp:TextBox ID="tctVat" runat="server" ClientIDMode="Static" CssClass="form-control setbrakdown" onkeypress="javascript:return onlyNumbers(event)" onkeyup="javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="rfvVat" ErrorMessage="Required" ControlToValidate="tctVat"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>           
          </div>


          <div class="form-group row" runat="server" id="Div21">
              <div class="col-sm-2"></div>
              <div class="col-sm-6 text-center border border-dark">Total Premium</div>
              <div class="col-sm-2 border border-dark p-1">
                  <asp:TextBox ID="txt_sumInsuTotal" runat="server" ClientIDMode="Static" CssClass="form-control setbrakdown" autocomplete="off" onkeypress="javascript:return onlyNumbers(event)" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RrfvTotSi" ErrorMessage="Required" ControlToValidate="txt_sumInsuTotal"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
              </div>
              <div class="col-sm-2"></div>
          </div>
         
          <div class="form-group row" runat="server" id="Div45" style="margin-top:15px;">
              <div class="col-sm-3">9. SLIC-G Service Personal <asp:Label runat="server" ForeColor="Red"></asp:Label>
                  :</div>
              <div class="auto-style1">                
                  <asp:DropDownList ID="ddlSlicCode" runat="server"  ClientIDMode="Static" CssClass="form-control cleare"  AppendDataBoundItems="true" AutoPostBack="false" >
                      <asp:ListItem Value="0">-- Select --</asp:ListItem>
                  </asp:DropDownList>
              </div>            
          </div>
          <%--end--%>

          <div class="form-group row" runat="server" id="Div32" style="margin-top:25px;">
              <div class="col-sm-7"></div>
              <div class="col-sm-5" style="padding-left:6vw" >                  
                  <asp:Button ID="btnProceed" runat="server" Text="Submit Proposal" CssClass="btn btn-info text-white"  OnClick="btnProceed_Click" ClientIDMode="Static" ValidationGroup ="rfvODP" />
                  <asp:Button ID="btnDone" runat="server" Text="Back" OnClientClick="this.value='Please wait...'" CssClass="btn btn-info text-white" />
              </div>
              <div class="col-sm-1" style="display:block">
                  <br />
              </div>
          </div>
          <br />
    </div>


    <%--Popup Policy Proposal--%>      
            <asp:LinkButton runat="server" ID="lnk_policy" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupConProposal" runat="server" BehaviorID="ConfirmProposal" PopupControlID="PnlPolicy" y="48" TargetControlID="lnk_policy" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
        
             <asp:Panel runat="server" ID="PnlPolicy" BackColor="White" Width="50%"  Style="position:static;top:20%!important; padding:3px; border:solid; box-shadow: 10px 5px 5px #4d4d4d;padding:9px; display: none; border: 0px solid #9e9e9e; border-radius:4px; height:95vh;">              
               
                 <div class="row">
                    <div class="col-sm-10" style="padding: 15px 15px 0px 30px">
                          <h4>Confirm Proposal</h4>
                    </div>
                     <div class="col-sm-2"  style="padding: 15px 32px 0px 0px">
                    <button type="button" class="close"  aria-label="Close"">
                        <span aria-hidden="true" onclick="javascript:closeEditPopup();">&times;</span></button>
                  </div>
                </div>

                <div class="modal-body" style="margin-top:-10px">
                    <div class="container" id="Div1" runat="server" style="height:70vh; padding:10px;">
                    
                        <div class="form-group row compGap">
                            <div class="col-sm-4">
                                1. Name Of the Customer 
                            </div>
                            <div class="col-sm-8">                              
                                <asp:Literal ID="litCusName" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:10px">
                            <div class="col-sm-4">
                                2. Postal Address
                            </div>
                            <div class="col-sm-8">                              
                                 <asp:Literal ID="litProposalNo" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:10px">
                            <div class="col-sm-4">
                                3. Date of Birth (dd/mm/yyyy)
                            </div>
                            <div class="col-sm-8">
                                <asp:Literal ID="litDob" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:10px">
                            <div class="col-sm-4">
                                4. NIC Number
                            </div>
                            <div class="col-sm-8">
                              <asp:Literal ID="litNic" runat="server"></asp:Literal>                              
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:10px">
                            <div class="col-sm-4">
                                5. Mobile Number
                            </div>
                            <div class="col-sm-8">
                                <asp:Literal ID="litMobileNo" runat="server"></asp:Literal>     
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:10px">
                            <div class="col-sm-4">
                                6. Email Address
                            </div>
                            <div class="col-sm-8">
                                <asp:Literal ID="litEmail" runat="server"></asp:Literal>     
                            </div>
                        </div>

                         <div class="form-group row compGap" style="margin-top:10px">
                            <div class="col-sm-4">
                                6. Business Type
                            </div>
                            <div class="col-sm-8">
                                <asp:Literal ID="litBtype" runat="server"></asp:Literal>     
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:10px">
                            <div class="col-sm-4">
                                7. Value of the Bank Facility
                            </div>
                            <div class="col-sm-8">
                                <asp:Literal ID="litvbf" runat="server"></asp:Literal>     
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:10px">
                            <div class="col-sm-4">
                                8. Total Premium 
                            </div>
                            <div class="col-sm-8">
                                <asp:Literal ID="litTotPremium" runat="server"></asp:Literal>     
                            </div>
                        </div>

                        <div style="text-align: center; font-size: small; margin-top: 15px;">
                            Note * Please click "Confirm Proposal" to accept terms and conditions of the policy
                        </div>
                                                       
                        <div class="form-group row" runat="server" id="Div4" style="margin-top: 25px;">
                            <div class="col-sm-7"></div>
                            <div class="col-sm-5" style="padding-left: 3vw">
                                <asp:Button ID="ConfirmProposal" runat="server" Text="Confirm Proposal" CssClass="btn btn-info text-white" ValidationGroup="rfvODP" OnClick="ConfirmProposal_Click"  />
                                <asp:Button ID="btn_cancel" runat="server" Text="Edit" CssClass="btn btn-info text-white"  />
                            </div>
                            <div class="col-sm-1">
                            </div>
                  </div>
         
                    </div>
                </div>
                </asp:Panel>
                <%--END [Popup Policy Proposal]--%> 



    <%--Popup Policy Proposal--%>      
            <asp:LinkButton runat="server" ID="lnk_printPAdvice" Style="display: none;" />
            <asp:ModalPopupExtender ID="pop_PrintAdvice" runat="server" BehaviorID="printadvice" PopupControlID="PnlPrint" y="20" TargetControlID="lnk_printPAdvice" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
        
             <asp:Panel runat="server" ID="PnlPrint" BackColor="White" Width="80%"  Style="position:static;top:20%!important; padding:3px; border:solid; box-shadow: 10px 5px 5px #4d4d4d;padding:9px; display: none; border: 0px solid #9e9e9e; border-radius:4px; height:95vh;">              
               
                 <div class="row">
                    <div class="col-sm-10" style="padding: 0px 15px 0px 30px">
                          <h4>Confirm Proposal</h4>
                    </div>
                     <div class="col-sm-2"  style="padding: 0px 32px 0px 0px">
                    <button type="button" class="close"  aria-label="Close"">
                        <span aria-hidden="true" onclick="javascript:ClosePrintAdvice();">&times;</span></button>
                  </div>
                </div>

                <div class="modal-body" style="margin-top:-10px; height:90vh;">              
                    <asp:Literal ID="ltEmbed" runat="server" />                   
                </div>
                </asp:Panel>
                <%--END [Popup Policy Proposal]--%> 

</asp:Content>




