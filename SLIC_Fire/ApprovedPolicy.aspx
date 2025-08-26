<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ApprovedPolicy.aspx.cs" Inherits="SLIC_Fire_ApprovedPolicy" Culture = "en-GB" EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<style type="text/css">        
   
   
       .ui-position{padding-left: 10px;}  
       .ui-datepicker { font-size:10pt !important}
       .ui-datepicker-trigger{margin-left:3px; vertical-align:middle; margin-bottom:3px;}
       .hidden{ display:none; }
        .ui_comp_hide{ display:none}
    
        .ui-position
        {
            padding-left: 10px;
        }

        .ui-datepicker
        {
           
             width: auto;
             height: auto;
             margin: 5px auto 0;
             font: 10pt Arial, sans-serif;
             -webkit-box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, .5);
             -moz-box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, .5);
             box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, .5);
        }

        .ui-datepicker-trigger
        {
            margin-left: 3px;
            vertical-align: middle;
            margin-bottom: 3px;
        }

        .swal-overlay 
        {
            background-color: rgba(43, 165, 137, 0.45);
        }

        .testClass
        {
            /*font-family: Courier New, Courier, monospace;*/
            font-size: 13px;
            font-weight: 500;
            color:#000000;
        }

        .testClassHeader 
        {
            /*font-family: Courier New, Courier, monospace;*/
            color:#026888;
            font-size: 14px;
            font-weight: 600;
            /*text-align:center;*/
        }

        .long-textbox 
        {
            max-height: 40px;
            margin-top: 5px;
            margin-bottom: 50px;
        } 
           .form-group 
           {
            margin-bottom: 5px;
           }
           /*.btn
           {
               min-width:100px;
               max-height:40px;
           }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
     <script type="text/javascript">

         function Alert()
         {

             swal("Done!", "Request in process.!", "success", {
             button: false,
         });
        }


         function AlertDownload() 
         {
              swal("Done!", "Excel File in process.!", "success", {
               button: false,
                 //timer: 3000,
            });
                     
         }
 </script>



     <script type="text/javascript">

        /*---------------------------auto complete ----------*/


        $(function () {
            $("[id$=txt_req_id]").autocomplete({

                source: function (request, response) {

                    $.ajax({
                        url: '<%=ResolveUrl("~/SLIC_Fire/ApprovedPolicy.aspx/GET_REQ_ID") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",

                        success: function (data) {

                            response($.map(data.d, function (item) {
                                return {

                                    label: item.split('-')[0],
                                    val: item.split('-')[1]


                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    //$("[id$=hfCustomerId]").val(i.item.val);
                },
                minLength: 1

            });
        });


        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {

                $(function () {
                    $("[id$=txt_req_id]").autocomplete({

                        source: function (request, response) {

                            $.ajax({

                                url: '<%=ResolveUrl("~/SLIC_Fire/ApprovedPolicy.aspx/GET_REQ_ID") %>',
                                data: "{ 'prefix': '" + request.term + "'}",
                                dataType: "json",

                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {

                                    response($.map(data.d, function (item) {
                                        return {
                                            label: item.split('-')[0],
                                            val: item.split('-')[1]

                                        }
                                    }))
                                },
                                error: function (response) {
                                    alert(response.responseText);
                                },
                                failure: function (response) {
                                    alert(response.responseText);
                                }
                            });
                        },
                        select: function (e, i) {
                            //$("[id$=hfCustomerId]").val(i.item.val);
                        },
                        minLength: 1

                    });

                });

                /********************************************************************************/
            }

        });
         
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
        $("input[id$='txt_to_date']").datepicker({
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
                $("input[id$='txt_to_date']").datepicker({
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
            <label for="" class="col-sm-12 font-weight-bolder h5 text-center" style="height: 24px">Approved Fire Policy View</label>
        </div>
       <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-left">Search Categories</label>
        </div>

          <div class="form-group row" runat="server">
             <div class="col-sm-1 testClass" runat="server"></div>
               <div class="col-sm-2 mr-0 testClass" runat="server">
               <label class="font-weight-bold testClass" runat="server">From</label>  
               <asp:TextBox ID="txt_start_date" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" ValidationGroup="VG0001" placeholder="From Date" autocomplete="off"></asp:TextBox>
               </div>

              <div class="col-sm-2 mr-0 testClass" runat="server">
              <label class="font-weight-bold testClass" runat="server">To</label>
              <asp:TextBox ID="txt_to_date" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" ValidationGroup="VG0001" placeholder="To Date" autocomplete="off"></asp:TextBox>
              </div>


               <div class="col-sm-2 mr-0 testClass" runat="server">
               <label class="font-weight-bold testClass" runat="server">Policy Number</label>
               <asp:TextBox ID="txt_req_id" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" placeholder="Pol. No."></asp:TextBox>
              </div>

              <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">NIC Number</label>
              <asp:TextBox ID="txtNicNo" runat="server" CssClass="form-control text-center testClass text-uppercase"  ClientIDMode="Static" placeholder="NIC" autocomplete="off"></asp:TextBox>  
              </div>



                <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">Status</label>

                     <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_status" runat="server" class="custom-select testClass text-center"
                               ClientIDMode="Static" AppendDataBoundItems="true">
                               <%--<asp:ListItem Text="--All--" Value="A"></asp:ListItem>--%>
                               <asp:ListItem Text="Completed" Value="N" Selected="True"></asp:ListItem>
                               <%--<asp:ListItem Text="Pending" Value="Y" ></asp:ListItem> 
                               <asp:ListItem Text="Rejected" Value="R"></asp:ListItem>--%>
                           </asp:DropDownList>      
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_status" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
              </div>
              </div>

              <div class="form-group row mb-4" runat="server">
                <div class="col-sm-1 testClass" runat="server">
                          <asp:CompareValidator ID="CompareValidator1" ValidationGroup = "VG0001" ForeColor = "Red" runat="server" 
                            ControlToValidate = "txt_start_date" ControlToCompare = "txt_to_date" Operator = "LessThanEqual" Type = "Date"
                            ErrorMessage="Start date must be less than End date." class="font-weight-bold testClass mt-4"></asp:CompareValidator>
                </div>
                <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">Bank</label>

<%--                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>--%>

                  <asp:DropDownList ID="ddl_bank" runat="server" class="custom-select testClass text-center" ClientIDMode="Static" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_bank_SelectedIndexChanged" AutoPostBack="true" > 
                        <asp:ListItem Value ="0">-- Select --</asp:ListItem>
                  </asp:DropDownList>
                            
                       <%-- </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_bank" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
              </div>

                <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">Branch</label>

                     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                  <asp:DropDownList ID="ddl_branch" runat="server" class="custom-select testClass text-center" ClientIDMode="Static" AppendDataBoundItems="true">
                        <asp:ListItem Value ="0">-- Select --</asp:ListItem>
                  </asp:DropDownList>                                  
                         </ContentTemplate>
                         <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="ddl_bank" EventName="SelectedIndexChanged" />
                         </Triggers>
                         </asp:UpdatePanel>
              </div>
                   <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">Term</label>

                     <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTerm" runat="server" class="custom-select testClass text-center"
                               ClientIDMode="Static" AppendDataBoundItems="true">
                               <asp:ListItem Text="All" Value="A"></asp:ListItem>
                               <asp:ListItem Text="Annual" Value="1"></asp:ListItem>
                               <asp:ListItem Text="Long Term" Value="0" ></asp:ListItem>      
                              
                           </asp:DropDownList>      
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTerm" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
              </div>
                    <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">Proposal Types</label>

                     <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlPropType" runat="server" class="custom-select testClass text-center"
                               ClientIDMode="Static" AppendDataBoundItems="true">
                               <asp:ListItem Text="All" Value="A"></asp:ListItem>
                               <asp:ListItem Text="Private Dwelling House Only" Value="1"></asp:ListItem>
                               <asp:ListItem Text="Solar Panel Only" Value="3" ></asp:ListItem>      
                              
                           </asp:DropDownList>      
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlPropType" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
              </div>

             <div class="col-sm-2 mr-0 testClass" runat="server">

                 <asp:Button ID="btn_find" runat="server" Text="Search" 
                                 ValidationGroup="VG0001" onclick="btn_find_Click1"                                   
                                  ClientIDMode="Static" class="btn btn-success testClass mr-1 mt-4 ml-0" OnClientClick="Alert()"/>

                 <asp:Button ID="btn_clear" runat="server"
                                 Text="Clear" onclick="btn_clear_Click"  ClientIDMode="Static" 
                                  class="btn btn-success testClass mr-1 mt-4 ml-1"/>

                 <asp:ImageButton ID="ImageButton8" runat="server" ImageAlign="Middle" 
                                ImageUrl="~/Images/icons8-downloading-updates-20.png"
                                ToolTip="Click here to download as excel file" class="btn btn-success testClass mr-1 mt-4 ml-1 img-fluid" OnClick="btexcel_Click" 
                                 OnClientClick="Alert()"/>

                 </div>

                  <div class="col-sm-1 mr-0 testClass" runat="server">
                     
                  </div>

           </div>
         <hr class="bg-info"/>

         <div class="form-group row" runat="server">
            <div class="col-md-12">
                <div class="table-responsive"> 
 <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

 <asp:GridView ID="Grid_Details" Visible="true" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="8"
                                CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100"
     OnPageIndexChanging="Grid_Details_PageIndexChanging"> 
                          <Columns>                            
                               <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                                  <ItemTemplate>
                                      <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" />
                               </asp:TemplateField>
                               <asp:BoundField HeaderText="Ref. No"  DataField="DH_REFNO" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                    
                               <asp:BoundField HeaderText="Bank"  DataField="DH_BCODE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="Branch"  DataField="DH_BBRCODE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="Cus. Name"  DataField="DH_NAME" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/> 
                              <asp:BoundField HeaderText="Cus. NIC"  DataField="DH_NIC"  ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                               <asp:BoundField HeaderText="Cus. Phone"  DataField="DH_PHONE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                      
                               <asp:BoundField HeaderText="Sum Insured"  DataField="DH_VALU_TOTAL" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>   
                                <asp:BoundField HeaderText="Debit No"  DataField="DEBIT_NO" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                             
                              <asp:BoundField HeaderText="Total Premium"  DataField="SC_TOTAL_PAY" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="Status"  DataField="DH_FINAL_FLAG"  ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                               <asp:BoundField HeaderText="policy No."  DataField="SC_POLICY_NO"  ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                               <asp:BoundField HeaderText="Loan Number"  DataField="LOAN_NUMBER" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide"/> 
                              <asp:BoundField HeaderText="Property Type"  DataField="PROP_TYPE"  ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                              <asp:BoundField HeaderText="Years"  DataField="Period"  ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                              <asp:BoundField HeaderText="Term"  DataField="TERM"  ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                               <asp:TemplateField HeaderText="Schedule" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                               <ItemTemplate>
                              <asp:ImageButton ID="imgbtn_select" runat="server"  CommandArgument = '<%#Eval("DH_REFNO")+";"+ Eval("DH_FINAL_FLAG")+";"+ Eval("DH_VALU_TOTAL")%>'  CausesValidation= "false" 
                                   CommandName="cmd"  ImageUrl="~/Images/icons8-view-64.png" OnClick="SelectRecord_Click" CssClass="img-fluid img_sizeIcon"/>
                             
                               </ItemTemplate>     
                               <ItemStyle />              
                               </asp:TemplateField>  
                                            
                     </Columns>
                     </asp:GridView>

                              </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                </div>
             </div>
         </div>
</asp:Content>
