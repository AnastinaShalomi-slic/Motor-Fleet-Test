<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="BPF_Admin_Admin" Culture = "en-GB"%>


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
            /*font-size: 10pt !important;
            max-width: 50%;*/
            /*min-width: 20%;*/

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
           .chkBox{
    font-size:200px;
    color:#000000;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
     <script type="text/javascript">

         function custom_alert(message, title) {
             if (!title)
                 title = 'Alert';

             if (!message)
                 message = 'No Message to Display.';


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
                 swal
                     ({
                         title: title,
                         text: message,
                         icon: "success",
                         button: false,
                         closeOnClickOutside: true,
                     });
             }

         }


         function Alert() {

             swal("Done!", "Request in process.!", "success", {
                 button: false,
             });
         }

         function AlertApp()
         {
             var uName = $('#<%=lblBank.ClientID %>').val();
           

             if (($('#<%= chkstatus1.ClientID%>').is(':checked')) || ($('#<%= chkstatus2.ClientID%>').is(':checked'))) {


             }
             else {

                 custom_alert('Please select a record first', 'Alert');
                 return false;
             }


             if (($('#<%= chkstatus1.ClientID%>').is(':checked')) )
             {
                  var radioValue = $("#<%= chkstatus1.ClientID%>:checked").val();
                
                 if (radioValue == '1') {
                   
                     swal("Done!", "BPF Payment Activated", "success", {
                         button: true,
                         timer: 3000
                     });
                  }
                  else {}

             }

             else if (($('#<%= chkstatus2.ClientID%>').is(':checked')) )
             {
                 var radioValue = $("#<%= chkstatus2.ClientID%>:checked").val();

                 if (radioValue == '0') {

                     swal("WOW", "BPF Payment Deactivated", "warning", {
                         button: true,
                         timer: 3000
                     });
                 }
                 else { }

             }



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
      <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional"><%--ChildrenAsTriggers="True"--%>
                        <ContentTemplate>
    <%--head section--%>
    <div class="" id="Div1" runat="server">
        <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-center">Special BPF Accsess</label>
        </div>
    </div>
    <div class="form-group row col-sm-12" runat="server">
    <%--First Section--%>
     <div class="col-sm-8 mr-2" id="viewPropId" runat="server">
       <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-left">Search Categories</label>
        </div>

          <div class="form-group row" runat="server">
        
               <div class="col-sm-2 mr-0 testClass" runat="server">
               <label class="font-weight-bold testClass" runat="server">User Name</label>
               <asp:TextBox ID="txt_un" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" placeholder="UN"></asp:TextBox>
              </div>

               <div class="col-sm-3 mr-0 testClass" runat="server">
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

                <div class="col-sm-3 mr-0 testClass" runat="server">
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

              
             <div class="col-sm-4 mr-0 testClass" runat="server">

                 <asp:Button ID="btn_find" runat="server" Text="Search" 
                                 ValidationGroup="VG0001" onclick="btn_find_Click1"                                   
                                  ClientIDMode="Static" class="btn btn-info testClass mr-1 mt-4 ml-0" OnClientClick="Alert()"/>

                 <asp:Button ID="btn_clear" runat="server"
                                 Text="Clear" onclick="btn_clear_Click"  ClientIDMode="Static" 
                                  class="btn btn-info testClass mr-1 mt-4 ml-1"/>

                 <asp:ImageButton ID="ImageButton8" runat="server" ImageAlign="Middle" 
                                ImageUrl="~/Images/icons8-downloading-updates-20.png"
                                ToolTip="Click here to download as excel file" class="btn btn-info testClass mr-1 mt-4 ml-1 img-fluid" OnClick="btexcel_Click" 
                                 OnClientClick="Alert()"/>


                 </div>
           
              </div>

         <hr class="bg-info"/>
         <div class="form-group row" runat="server">
            <div class="col-md-12">
                <div class="table-responsive"> 
 <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

 <asp:GridView ID="Grid_Details" Visible="true" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100 table-sm"
     OnPageIndexChanging="Grid_Details_PageIndexChanging" OnRowDataBound="OnRowDataBound"> 
                          <Columns>                            
                               <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                                  <ItemTemplate>
                                      <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" />
                               </asp:TemplateField>
                               <asp:BoundField HeaderText="Bank Name"  DataField="bbnam" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                    
                               <asp:BoundField HeaderText="Branch Name"  DataField="bbrnch" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="User Name"  DataField="username" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="Status"  DataField="status" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                
                               <asp:BoundField HeaderText="bank_code"  DataField="bank_code"  ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                               <asp:BoundField HeaderText="branch_code."  DataField="branch_code"  ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                               <asp:BoundField HeaderText="active_flag"  DataField="active_flag" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide"/> 
                              <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                               <ItemTemplate>
                                      <asp:UpdatePanel ID="UpdatePanel123" runat="server" class="col-sm-12" UpdateMode="Conditional">
                        <ContentTemplate>
                              <asp:ImageButton ID="imgbtn_select" runat="server"  CommandArgument = '<%#Eval("username")+";"+ Eval("bank_code")+";"+ Eval("branch_code")+";"+ Eval("active_flag")+";"+ Eval("bbnam")+";"+ Eval("bbrnch")%>'  CausesValidation= "false" 
                                   CommandName="cmd"  ImageUrl="~/Images/icons8-next.png" OnClick="SelectRecord_Click" CssClass="img-fluid img_sizeIcon"/>
                             </ContentTemplate>
                                            <Triggers>     
                             <asp:PostBackTrigger ControlID="imgbtn_select" />
                   </Triggers>
                                          </asp:UpdatePanel>
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

    <%--Functional section--%>
     <div class="container-fluid col-sm-3" id="Div2" runat="server">
         <div class="form-group row mt-5" runat="server"></div>
         <div class="form-group row mt-5" runat="server"></div>
         <div class="form-group row mt-5" runat="server"></div>

         <div class="form-group row" runat="server">
         <label for="" class="col-sm-12 font-weight-bolder h5 text-left">Info.</label>
         </div>

         <div class="form-group row" runat="server">
         <label class="font-weight-bold testClass col-sm-4" runat="server">Bank Name</label>
         <label class="font-weight-bold testClass col-sm-6" runat="server" id="lblBankName"></label>
         </div>

         <div class="form-group row" runat="server">
         <label class="font-weight-bold testClass col-sm-4" runat="server">Bank Code</label>
         <label class="font-weight-bold testClass col-sm-6" runat="server" id="lblBank"></label>
         </div>

         <div class="form-group row" runat="server">
         <label class="font-weight-bold testClass col-sm-4" runat="server">Branch Name</label>
         <label class="font-weight-bold testClass col-sm-6" runat="server" id="lblBranchName"></label>
         </div>

         <div class="form-group row" runat="server">
         <label class="font-weight-bold testClass col-sm-4" runat="server">Branch Code</label>
         <label class="font-weight-bold testClass col-sm-6" runat="server" id="lblBranch"></label>
         </div>

          <div class="form-group row" runat="server">
         <label class="font-weight-bold testClass col-sm-4" runat="server">User Name</label>
         <label class="font-weight-bold testClass col-sm-6" runat="server" id="lblUn"></label>
         </div>

         <div class="form-group row" runat="server">
         <label class="font-weight-bold testClass col-sm-4" runat="server">Status</label>
        <div class="form-check col-sm-2 ml-3">
        <input type="radio" class="form-check-input" name="exampleRadios" id="chkstatus1" runat="server" ClientIDMode="Static" value="1" AutoPostBack=True>
        <label class="form-check-label testClass" for="chkstatus1">Active</label>
        </div>
        <div class="form-check col-sm-2 ml-3" runat="server"> 
        <input type="radio" class="form-check-input" name="exampleRadios" id="chkstatus2" runat="server" ClientIDMode="Static" value="0" AutoPostBack=True>
        <label class="form-check-label testClass" for="chkstatus2">Deactive</label>
        </div> 
        <div class="form-check col-sm-2" runat="server">
        <label class="text-danger lbls" runat="server" id="validationstatus" ></label>
        </div>
         <div class="col-sm-2"></div>
         </div>
         <div class="form-group row" runat="server">
                <label class="col-sm-12 text-black-50 testClass">(to activate or deactivate check the radio button)</label>
                <label class="col-sm-12 text-warning h5 font-weight-bold" runat="server" id="rtnLbl"></label>
            </div>
         <div class="form-group row" runat="server">
                 <asp:Button ID="btnApp" runat="server" Text="Submit" 
                                 ValidationGroup="VG0001"                                  
                                  ClientIDMode="Static" class="btn btn-info testClass mr-1 mt-4 ml-3 btn-sm" OnClientClick="return AlertApp()" OnClick="btnApp_Click"/>

                 <asp:Button ID="btnClr" runat="server"
                                 Text="Clear"  ClientIDMode="Static" 
                                  class="btn btn-info testClass mr-1 mt-4 ml-1 btn-sm" OnClick="btnClr_Click"/>
            </div>
     </div>
    </div>
                            </ContentTemplate>
                            </asp:UpdatePanel>
    <%--Git--%>
</asp:Content>
