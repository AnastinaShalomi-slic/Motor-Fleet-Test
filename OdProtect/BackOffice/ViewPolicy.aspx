<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Odb.master" AutoEventWireup="true" CodeFile="ViewPolicy.aspx.cs" Inherits="OdProtect_BackOffice_ViewPolicy" Culture = "en-GB"%>
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
        background-color: #454545;
        filter: alpha(opacity=90);
        opacity: 0.8;
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


                
                 /********************************************************************************/
             }

         });

</script>
   
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>

    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />

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
                            <asp:ListItem Text="Completed" Value="C" Selected="True"></asp:ListItem>

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
                                OnPageIndexChanging="Grid_Details_PageIndexChanging" >
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-dark testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
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
                               
                                     <asp:BoundField HeaderText="srid" DataField="SRID" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                </Columns>
                            </asp:GridView>
       

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






