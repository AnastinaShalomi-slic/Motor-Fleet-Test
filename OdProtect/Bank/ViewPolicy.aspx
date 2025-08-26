<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_OD.master" AutoEventWireup="true" CodeFile="ViewPolicy.aspx.cs" Inherits="OdProtect_Bank_ViewPolicy" Culture = "en-GB"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">        
   
   .ui-position{padding-left: 10px;}  
   .ui-datepicker { font-size:10pt !important}
   .ui-datepicker-trigger{margin-left:3px; vertical-align:middle; margin-bottom:3px;}
   .hidden{ display:none; }
   .ui_comp_hide{ display:none}

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
            /*font-family: Courier New, Courier, monospace;*/
            font-size: 13px;
            font-weight: 500;
            color: #000000;
        }

        .testClassHeader {
            /*font-family: Courier New, Courier, monospace;*/
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

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=80);
            opacity: 0.4;
        }

        .ToUpper {
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

        .disabl_link {
            pointer-events: none;
            cursor: default;
            opacity: 0.6;
        }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <script type="text/javascript">

    var toDate = new Date().format('dd/MM/yyyy');

         $(function () {
             $("input[id$='txt_start_date']").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 showOtherMonths: true,
                 yearRange: '2016:+10',
                 dateFormat: 'dd/mm/yy',
                 defaultDate: +0,
                 numberOfMonths: 1,
                 maxDate: toDate,
                 showAnim: 'slideDown',
                 showButtonPanel: false,
                 showWeek: true,
                 firstDay: 1,
                 stepMonths: 0,
                 //showOn: "button",
                 buttonImage: "../Images/delete.gif",
                 buttonImageOnly: true,
                 buttonText: "Select date",
                 onSelect: function (date) {
                     var date2 = $("input[id$='txt_start_date']").datepicker('getDate');

                     //set date2 = date1 selected date         
                     // $("input[id$='txt_podate']").datepicker('setDate', date2);
                 }
             });
         });


         $(function () {
             $("input[id$='txt_end_date']").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 showOtherMonths: true,
                 yearRange: '2016:+10',
                 dateFormat: 'dd/mm/yy',
                 defaultDate: +0,
                 numberOfMonths: 1,
                 maxDate: toDate,
                 showAnim: 'slideDown',
                 showButtonPanel: false,
                 showWeek: true,
                 firstDay: 1,
                 stepMonths: 0,
                 //showOn: "button",
                 buttonImage: "../Images/delete.gif",
                 buttonImageOnly: true,
                 buttonText: "Select date",
                 onSelect: function (date) {
                     var date2 = $("input[id$='txt_end_date']").datepicker('getDate');
                 }
             });
         });


         function closeEditPopup() {
             <%--document.getElementById('<%= txt_resion.ClientID %>').value = '';--%>
             $find("editProposal").hide();
             return false;
         }
     
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

            if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || (charCode == " " || charCode == "Space" || charCode == 32 || charCode == 46   ))

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


         //$(function () {
         //   $('[id*=txtBdate]').datepicker({
         //       language: "tr",
         //       changeMonth: true,
         //       changeYear: true,
         //       showOtherMonths: true,
         //       yearRange: '1959:+65',
         //       dateFormat: 'dd/mm/yy',
         //       defaultDate: +0,
         //       numberOfMonths: 1,
         //       maxDate: toDate,
         //       showAnim: 'slideDown',
         //       showButtonPanel: false,
         //       showWeek: false,
         //       firstDay: 1,
         //       stepMonths: 0,
         //       //showOn: "off"
         //       buttonImage: "../Images/delete.png",
         //       todayBtn: true,
         //       todayHighlight: true,
         //       //buttonImageOnly: true,
         //       buttonText: "Select date"              
         //   });
         //});


         function CleareUpdate() {
             $("#<%= ddlInitials.ClientID%>")[0].selectedIndex = 0;
             document.getElementById('<%= hid_ProposalNo.ClientID%>').value = '';
             document.getElementById('<%= hid_Srid.ClientID%>').value = '';
             document.getElementById('<%= txt_CusName.ClientID %>').value = '';
             document.getElementById('<%= txt_addline1.ClientID %>').value = '';
             document.getElementById('<%= txt_addline2.ClientID %>').value = '';
             document.getElementById('<%= txt_addline3.ClientID %>').value = '';
             document.getElementById('<%= txt_addline4.ClientID %>').value = '';
             document.getElementById('<%= txtBdate.ClientID %>').value = '';
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
         }
      
        function Message(title, message, icon) {
                swal
                    ({
                        title: title,
                        text: message,
                        icon: icon,
                        button: true,
                        closeOnClickOutside: false,
                    });
            }
        
         function ClosePrintPolicy() {
             document.getElementById('<%= hid_sridPrt.ClientID%>').value = '';
             $find("printpolicy").hide();
             return false;
         }

    
  
         $(document).ready(function () {

             Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

             function EndRequestHandler(sender, args) {

                 $(function () {
                     $("input[id$='txt_start_date']").datepicker({
                         changeMonth: true,
                         changeYear: true,
                         showOtherMonths: true,
                         yearRange: '2016:+10',
                         dateFormat: 'dd/mm/yy',
                         defaultDate: +0,
                         numberOfMonths: 1,
                         maxDate: toDate,
                         showAnim: 'slideDown',
                         showButtonPanel: false,
                         showWeek: true,
                         firstDay: 1,
                         stepMonths: 0,
                         //showOn: "button",
                         buttonImage: "../Images/delete.gif",
                         buttonImageOnly: true,
                         buttonText: "Select date",
                         onSelect: function (date) {
                             var date2 = $("input[id$='txt_start_date']").datepicker('getDate');

                             //set date2 = date1 selected date         
                             //$("input[id$='txt_podate']").datepicker('setDate', date2);

                         }
                     });
                 });


                 $(function () {
                     $("input[id$='txt_end_date']").datepicker({
                         changeMonth: true,
                         changeYear: true,
                         showOtherMonths: true,
                         yearRange: '2016:+10',
                         dateFormat: 'dd/mm/yy',
                         defaultDate: +0,
                         numberOfMonths: 1,
                         maxDate: toDate,
                         showAnim: 'slideDown',
                         showButtonPanel: false,
                         showWeek: true,
                         firstDay: 1,
                         stepMonths: 0,
                         //showOn: "button",
                         buttonImage: "../Images/delete.gif",
                         buttonImageOnly: true,
                         buttonText: "Select date",
                         onSelect: function (date) {
                             var date2 = $("input[id$='txt_end_date']").datepicker('getDate');
                         }
                     });

                 });


                 //$(function () {
                 //    $('[id*=txtBdate]').datepicker({
                 //        language: "tr",
                 //        changeMonth: true,
                 //        changeYear: true,
                 //        showOtherMonths: true,
                 //        yearRange: '1959:+65',
                 //        dateFormat: 'dd/mm/yy',
                 //        defaultDate: +0,
                 //        numberOfMonths: 1,
                 //        maxDate: toDate,
                 //        showAnim: 'slideDown',
                 //        showButtonPanel: false,
                 //        showWeek: false,
                 //        firstDay: 1,
                 //        stepMonths: 0,
                 //        //showOn: "off"
                 //        buttonImage: "../Images/delete.png",
                 //        todayBtn: true,
                 //        todayHighlight: true,
                 //        //buttonImageOnly: true,
                 //        buttonText: "Select date"                      
                 //    });
                 //});

                 /********************************************************************************/
             }

         });

</script>
   
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>

    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />

    <asp:HiddenField ID="bank_code" runat="server" />
    <asp:HiddenField ID="branch_code" runat="server" />
    <asp:HiddenField ID="userName_code" runat="server" />
    <asp:HiddenField ID="bancass_email" runat="server" />

    <div class="" id="viewPropId" runat="server">

        <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-left">Search Criteria</label>
        </div>


        <div class="form-group row" runat="server">

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">From</label>
                <asp:TextBox ID="txt_start_date" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" ValidationGroup="VG0001" placeholder="From Date" autocomplete="off"></asp:TextBox>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">To</label>
                <asp:TextBox ID="txt_end_date" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" ValidationGroup="VG0001" placeholder="To Date" autocomplete="off"></asp:TextBox>
            </div>


            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">Policy Number</label>
                <asp:TextBox ID="txt_req_id" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" placeholder="Pol. No."></asp:TextBox>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">NIC Number</label>
                <asp:TextBox ID="txtNicNo" runat="server" CssClass="form-control text-center testClass text-uppercase" ClientIDMode="Static" placeholder="NIC" autocomplete="off"></asp:TextBox>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">Status</label>

                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_status" runat="server" class="custom-select testClass text-center"
                            ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="true">
                            <asp:ListItem Text="Select" Value="0" ></asp:ListItem>
                            <asp:ListItem Text="Completed" Value="C"></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="P" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="Rejected" Value="R"></asp:ListItem>

                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_status" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server" style="padding-top: 4vh">

                <asp:Button ID="btn_find" runat="server" Text="Search" ValidationGroup="VG0001" OnClick="btn_find_Click1"
                    ClientIDMode="Static" class="btn btn-success testClass mr-1 mt-6 ml-1" />
                <asp:Button ID="btn_clear" runat="server" Text="Clear" OnClick="btn_clear_Click" ClientIDMode="Static"
                    class="btn btn-success testClass mr-1 mt-6 ml-1" />
            </div>
        </div>

        <div class="form-group row" runat="server">
            <div class="col-sm-3 testClass" runat="server">
                <asp:CompareValidator ID="comp_val" ValidationGroup="VG0001" ForeColor="Red" runat="server"
                    ControlToValidate="txt_end_date" ControlToCompare="txt_start_date" Operator="GreaterThanEqual" Type="Date"
                    ErrorMessage="Start date must be less than End date." class="font-weight-bold testClass"></asp:CompareValidator>
            </div>
        </div>

        <hr class="bg-info" />


        <div class="form-group row" runat="server">
            <div class="col-md-12">
                <div class="table-responsive">                
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:GridView ID="Grid_Details" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="8"
                                CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100"
                                OnPageIndexChanging="Grid_Details_PageIndexChanging" OnRowDataBound="OnRowDataBound" >
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-dark testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <%--<asp:TemplateField HeaderText="Proposal Number" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="imgbtn_ptop" Text='<%# Eval("PRNO") %>'  runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="javascript:ShowPtoP(this); return false;" CausesValidation="false"  />
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>

                                    <asp:BoundField HeaderText="Proposal Number" DataField="PRNO" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Policy Number" DataField="POLNO" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Name of Insured" DataField="CUSTOMER" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Start Date" DataField="POLICY_SDATE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="End Date" DataField="POLICY_EDATE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Sum Insured" DataField="SUMINSURD" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Total Premium" DataField="TOT_PREMIUM" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Contact Number" DataField="CONNOMOB" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Entered Date" DataField="ENTDATE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Status" DataField="POL_STATUS" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />


                                    <asp:TemplateField HeaderText="Proposal/Schedule" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgbtn_select" runat="server" CommandArgument='<%#Eval("SRID")%>' CausesValidation="false"
                                                CommandName="cmd" ImageUrl="~/Images/icons8-view-64.png" OnClick="SelectRecord_Click" CssClass="img-fluid img_sizeIcon" />
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Edit Details" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgBtn_Edit" runat="server" CommandArgument='<%#Eval("PRNO")%>' CausesValidation="false"
                                                CommandName="cmd" ImageUrl="~/Images/icons8-edit-64.png" OnClick="imgBtn_Edit_Click"
                                                CssClass="img-fluid img_sizeIcon" />
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>

                                     <asp:BoundField HeaderText="srid" DataField="SRID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                </Columns>
                            </asp:GridView>



          <%--Popup Edit-Proposal Document--%>      
            <asp:LinkButton runat="server" ID="lnkDelDoc" Style="display: none;" />
            <asp:ModalPopupExtender ID="mpeEditProposal" runat="server" BehaviorID="editProposal" PopupControlID="pnlDelete" y="55" TargetControlID="lnkDelDoc" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
        
             <asp:Panel runat="server" ID="pnlDelete" BackColor="White" Width="75%"  Style="position:static;top:20%!important; padding:3px; border:solid; box-shadow: 10px 5px 5px #4d4d4d;padding:9px; display: none; border: 0px solid #9e9e9e; border-radius:4px; height:90vh;display:none">  
               
                 <div class="row">
                    <div class="col-sm-10" style="padding: 5px 15px 0px 30px">
                          <h4><b>Edit Proposal</b></h4>
                    </div>

                     <div class="col-sm-2"  style="padding: 5px 32px 0px 0px">
                    <button type="button" class="close"  aria-label="Close"">
                        <span aria-hidden="true" onclick="javascript:closeEditPopup(); CleareUpdate();">&times;</span></button>
                  </div>
                </div>

                <div class="modal-body">
                <div class="container" id="mainDiv" runat="server" style="height:78vh; overflow-y:scroll; overflow-x:hidden; padding:10px; margin-top:-7px">

                 <asp:HiddenField ID="hid_ProposalNo" runat="server" />
                 <asp:HiddenField ID="hid_Srid" runat="server" />
         
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
                      <div class="col-sm-3">3. Date of Birth<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
                      <div class="col-sm-3 form-inline">
                          <asp:TextBox ID="txtBdate" runat="server" CssClass="form-control" autocomplete="off" onkeydown="return false">                       
                          </asp:TextBox>
                  	        <div class="input-group-prepend"">
		                      <span class="input-group-text"> <i class="fa fa-calendar" style="padding:calc(0.2em + 1px);"></i> </span>
                            </div>    
                    </div>
                       <asp:RequiredFieldValidator ID="rfvDob" ErrorMessage="Date Of birth can't be blank" ControlToValidate="txtBdate"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
                  </div>

                    <div class="form-group row compGap" runat="server" id="Div2">
                        <div class="col-sm-3">4. NIC Number<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
                        <div class="col-sm-3">
                            <asp:TextBox ID="txt_nic" runat="server" CssClass="form-control text-capitalize" ClientIDMode="Static" autocomplete="off" MaxLength="12" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNic" ErrorMessage="NIC can't be blank" ControlToValidate="txt_nic" runat="server" ValidationGroup="rfvODP" CssClass="rfvControl" Display="Dynamic" />

                            <asp:RegularExpressionValidator ID="rfvNicFmt" ValidationGroup="rfvODP" Display="Dynamic" ValidationExpression="^([0-9]{9}[x|X|v|V]|[0-9]{12})$" ControlToValidate="txt_nic" runat="server">
                                    <small id="rmsgrfvnic" class="form-text rfvControl">Please enter a valid NIC number.</small>
                            </asp:RegularExpressionValidator>
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
                      <div class="col-sm-3">8. Value of the Bank Facility (LKR)<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
                      <div class="col-sm-3">
                          <asp:TextBox ID="txt_odLimit" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="form-control" onkeypress="javascript:return isNumber(event);" onkeyup="javascript:this.value=Comma(this.value); Calculate_Tax(this.value);"    autocomplete="off" MaxLength ="7"></asp:TextBox>
                           <asp:RequiredFieldValidator ID="rfvodLimit" ErrorMessage="Overdraft value can't be blank" ControlToValidate="txt_odLimit"  runat="server"  ValidationGroup ="rfvODP" CssClass="rfvControl" Display="Dynamic"/>
                      </div>
                  </div>

                    <div class="form-group row" runat="server" id="Div18" style="margin-top: 15px;">
                        <div class="col-sm-10" style="font-size: 1.2em; font-weight: 600">Premium Calculation</div>
                        <div class="col-sm-0"></div>
                        <div class="col-sm-2"></div>
                    </div>

                  <div class="form-group row" runat="server" id="Div33" style="margin-top:15px;">
                      <div class="col-sm-2"></div>
                      <div class="col-sm-6 text-center border border-dark">Property to be insured</div>
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
                      <div class="col-sm-3">9. SLIC Service Branch <asp:Label runat="server" ForeColor="Red">*</asp:Label>
                          :</div>
                      <div class="col-sm-7">
                          <asp:DropDownList ID="ddlSlicCode" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false" >
                              <asp:ListItem Value="0">-- Select --</asp:ListItem>
                          </asp:DropDownList>
                      </div>            
                  </div>
  

                  <div class="form-group row" runat="server" id="Div32" style="margin-top:25px;">
                      <div class="col-sm-8"></div>
                      <div class="col-sm-4" style="padding-left:3vw">
                          <asp:Button ID="btnProceed" runat="server" Text="Update Proposal" CssClass="btn btn-info text-white"  ValidationGroup ="rfvODP" OnClick="btnUpload_Proposal_Click" />                 
                      </div>
                      <div class="col-sm-1">
                      </div>
                  </div>
                  <br />
                </div>
            </div>
         </asp:Panel>
         <%--End - [Popup Edit-Proposal Document]--%>     
                            

       
            <%--END [Popup Policy Proposal]--%> 
            <asp:LinkButton runat="server" ID="lnk_printPolicy" Style="display: none;" />
            <asp:ModalPopupExtender ID="pop_PrintPolicy" runat="server" BehaviorID="printpolicy" PopupControlID="PnlPrint" y="25" TargetControlID="lnk_printPolicy" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
        
             <asp:Panel runat="server" ID="PnlPrint" BackColor="White" Width="90%" Style="position:static;top:20%!important; padding:3px; border:solid; box-shadow: 10px 5px 5px #4d4d4d;padding:9px; display: none; border: 0px solid #9e9e9e; border-radius:4px; height:95vh;">  
               
                 <div class="row">
                    <div class="col-sm-10" style="padding: 7px 15px 0px 30px">
                          <h4><b>Print Policy</b></h4>
                    </div>
                     <div class="col-sm-2"  style="padding: 7px 32px 0px 0px">
                    <button type="button" class="close"  aria-label="Close"">
                        <span aria-hidden="true" onclick="javascript:ClosePrintPolicy();">&times;</span></button>
                  </div>
                </div>
                 
                 <div class="form-group row" runat="server" id="Div5" style="margin-top: 5px;">
                     <div class="col-sm-12" style="padding-left: 3vw; padding-right: 3vw;">
                         <asp:Button ID="btn_PrintPayAdvice" runat="server" Text="Payment Advice" CssClass="btn btn-info text-white" OnClick="btn_PrintPayAdvice_Click" />
                         <asp:Button ID="btn_PrintPolicy" runat="server" Text="Policy Shedule" CssClass="btn btn-info text-white"  OnClick="btn_PrintPolicy_Click"/>
                         <asp:HiddenField ID="hid_sridPrt" runat="server" />
                         <hr />
                     </div>
                 </div>
                 <div class="modal-body" style="margin-top: -10px; height: 85vh;">

                     <div style="margin-top: -10px; height: 75vh; overflow: hidden">
                         <asp:Literal ID="ltEmbed" runat="server" />
                     </div>
                 </div>
                </asp:Panel>

                <%--END [Popup Policy Proposal]--%> 

                </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="Grid_Details" />
                        </Triggers>
              </asp:UpdatePanel>
           </div>
        </div>
      </div>
</div>
 
</asp:Content>





