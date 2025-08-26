<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Odb.master" AutoEventWireup="true" CodeFile="Proposal_to_Policy.aspx.cs" Inherits="OdProtect_BackOffice_Proposal_to_Policy" Culture = "en-GB" %>
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

          function closePtP() {
              ClearePtP();
             $find("B_PnlPolicy").hide();
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

         function ClearePtP() {
             document.getElementById('<%= txtp_CusName.ClientID%>').value = '';
             document.getElementById('<%= txtp_ProposalNo.ClientID%>').value = '';
             document.getElementById('<%= txtp_SumInsured.ClientID%>').value = '';
             document.getElementById('<%= txtp_TPremium.ClientID%>').value = '';
             document.getElementById('<%= txtP_ReciptNo.ClientID%>').value = ''; 
             document.getElementById('<%= txtp_NIC.ClientID %>').value = '';
             document.getElementById('<%= hidMailBranch.ClientID%>').value = '';
             document.getElementById('<%= hid_sridPrt.ClientID%>').value = '';
         }

         $(function () {
            $('[id*=txtBdate]').datepicker({
                format: "dd/mm/yyyy",
                language: "tr",
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
                showWeek: false,
                firstDay: 1,
                stepMonths: 0,
                //showOn: "off"
                buttonImage: "../Images/delete.png",
                todayBtn: true,
                todayHighlight: true,
                //buttonImageOnly: true,
                buttonText: "Select date"
            });
         });

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

         function ShowPtoP(rowIndex) {
             var row = rowIndex.parentNode.parentNode;
             var proposalNo = row.cells[1].innerText;
             document.getElementById('<%= txtp_CusName.ClientID %>').value = row.cells[3].innerHTML;
             document.getElementById('<%= txtp_ProposalNo.ClientID %>').value =  proposalNo;
             document.getElementById('<%= txtp_SumInsured.ClientID %>').value = row.cells[6].innerHTML;
             document.getElementById('<%= txtp_TPremium.ClientID %>').value = row.cells[7].innerHTML;
             document.getElementById('<%= txtp_NIC.ClientID %>').value = row.cells[13].innerHTML;
             document.getElementById('<%= hidMailBranch.ClientID%>').value = row.cells[14].innerHTML;
             document.getElementById('<%= hid_sridPrt.ClientID%>').value = row.cells[15].innerHTML;
             $find("B_PnlPolicy").show();            
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


                 $(function () {
                     $('[id*=txtBdate]').datepicker({
                         format: "dd/mm/yyyy",
                         language: "tr",
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
                         showWeek: false,
                         firstDay: 1,
                         stepMonths: 0,
                         //showOn: "off"
                         buttonImage: "../Images/delete.png",
                         todayBtn: true,
                         todayHighlight: true,
                         //buttonImageOnly: true,
                         buttonText: "Select date"
                     });
                 });

                 /********************************************************************************/
             }
         });

</script>
   
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>

    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />

    <asp:UpdatePanel ID="udp_statistics_br" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
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
                        <asp:DropDownList ID="ddl_status" runat="server" class="custom-select testClass text-center" 
                            ClientIDMode="Static">
                            <asp:ListItem Text="Select" Value="0" ></asp:ListItem>
                            <asp:ListItem Text="Pending" Value="P" Selected="True"></asp:ListItem>
                        </asp:DropDownList>                   
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

                    <asp:GridView ID="Grid_Details" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="8"
                        CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100"
                        OnPageIndexChanging="Grid_Details_PageIndexChanging" OnRowDataBound="OnRowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-dark testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Proposal Number" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                                <ItemTemplate>
                                    <asp:LinkButton ID="imgbtn_ptop" Text='<%# Eval("PRNO") %>' runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="javascript:ShowPtoP(this); return false;" CausesValidation="false" />
                                </ItemTemplate>
                            </asp:TemplateField>

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

                            <asp:BoundField HeaderText="srid" DataField="SRID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="NIC" DataField="NIC" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                            <asp:BoundField HeaderText="BRANCH_CODE" DataField="BRANCH_CODE"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                            <asp:BoundField HeaderText="SRID" DataField="SRID"  HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden"/>
                            
                        </Columns>
                    </asp:GridView>
                          
         <%--Popup Policy Proposal--%>      
            <asp:LinkButton runat="server" ID="lnk_policy" Style="display: none;" />
            <asp:ModalPopupExtender ID="popupPolicy" runat="server" BehaviorID="B_PnlPolicy" PopupControlID="PnlPolicy" y="55" TargetControlID="lnk_policy" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
        
             <asp:Panel runat="server" ID="PnlPolicy" BackColor="White" Width="50%"  Style="position:static;top:20%!important; padding:3px; border:solid; box-shadow: 10px 5px 5px #4d4d4d;padding:9px; display: none; border: 0px solid #9e9e9e; border-radius:4px; height:70vh;">              
               
                 <div class="row">
                    <div class="col-sm-10" style="padding: 10px 15px 0px 30px">
                          <h4>Proposal to Policy</h4>
                    </div>
                     <div class="col-sm-2"  style="padding: 10px 32px 0px 0px">
                    <button type="button" class="close"  aria-label="Close"">
                        <span aria-hidden="true" onclick="javascript:ClearePtP();closePtP(); ">&times;</span></button>
                  </div>
                </div>

                <div class="modal-body" style="margin-top:-10px;">
                    <div class="container" id="Div1" runat="server" style="height:50vh; padding:10px;">
                        <asp:HiddenField ID="hidMailBranch" runat="server" />
                        <div class="form-group row compGap">
                            <div class="col-sm-12">
                                1. Name Of the Customer <asp:Label runat="server" ForeColor="Red"></asp:Label>
                                <asp:TextBox ID="txtp_CusName" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" MaxLength="82"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPtP_cusn" ErrorMessage="Name of the Customer Can't be Blank." ControlToValidate="txtp_CusName" runat="server" ValidationGroup="rfvPtP" CssClass="rfvControl" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:20px">
                            <div class="col-sm-4">
                                2. Customer NIC <asp:Label runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtp_NIC" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" MaxLength="12"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPtP_Nic" ErrorMessage="NIC Can't be Blank." ControlToValidate="txtp_NIC" runat="server" ValidationGroup="rfvPtP" CssClass="rfvControl" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:20px">
                            <div class="col-sm-4">
                                3. Proposal Number<asp:Label runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtp_ProposalNo" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" MaxLength="21"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPtP_Prp" ErrorMessage="Policy Number Can't be Blank." ControlToValidate="txtp_ProposalNo" runat="server" ValidationGroup="rfvPtP" CssClass="rfvControl" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:12px">
                            <div class="col-sm-4">
                                4. Sum Insured (Rs.)<asp:Label runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtp_SumInsured" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" MaxLength="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPtP_SIns" ErrorMessage="Sum Insured Can't be Blank." ControlToValidate="txtp_SumInsured" runat="server" ValidationGroup="rfvPtP" CssClass="rfvControl" Display="Dynamic" />
                            </div>
                        </div>


                        <div class="form-group row compGap" style="margin-top:12px">
                            <div class="col-sm-4">
                                5. Total Premium (Rs.)<asp:Label runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtp_TPremium" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" MaxLength="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvPtP_TPRM" ErrorMessage="Total Premium Can't be Blank." ControlToValidate="txtp_TPremium" runat="server" ValidationGroup="rfvPtP" CssClass="rfvControl" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group row compGap" style="margin-top:12px">
                            <div class="col-sm-4">
                                6. Pay Receipt No<asp:Label runat="server" ForeColor="Red"></asp:Label>
                            </div>
                            <div class="col-sm-6">
                                <asp:TextBox ID="txtP_ReciptNo" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off" MaxLength="30"></asp:TextBox>                               
                            </div>
                        </div>

                        <div class="form-group row" runat="server" id="Div4" style="margin-top: 25px;">
                            <div class="col-sm-7"></div>
                            <div class="col-sm-5" style="padding-left: 3vw">
                                <asp:Button ID="btnPtP" runat="server" Text="Proposal to Policy" CssClass="btn btn-info text-white" ValidationGroup="rfvPtP" OnClick="btnPtP_Click" />
                                <asp:Button ID="btn_cancel" runat="server" Text="Cancel" CssClass="btn btn-info text-white" OnClientClick="ClearePtP();" />
                            </div>
                            <div class="col-sm-1">
                            </div>
                        </div>
                    
                    </div>
                </div>
                </asp:Panel>
            <%--END [Popup Policy Proposal]--%> 


            <%--END [Popup Policy Proposal]--%> 
            <asp:LinkButton runat="server" ID="lnk_printPolicy" Style="display: none;" />
            <asp:ModalPopupExtender ID="pop_PrintPolicy" runat="server" BehaviorID="printpolicy" PopupControlID="PnlPrint" y="25" TargetControlID="lnk_printPolicy" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
        
             <asp:Panel runat="server" ID="PnlPrint" BackColor="White" Width="90%"  Style="position:static;top:20%!important; padding:3px; border:solid; box-shadow: 10px 5px 5px #4d4d4d;padding:9px; display: none; border: 0px solid #9e9e9e; border-radius:4px; height:95vh;">              
               
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
           </div>
        </div>
      </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>

</asp:Content>




