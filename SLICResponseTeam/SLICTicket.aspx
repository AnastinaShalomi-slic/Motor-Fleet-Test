<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SLICTicket.aspx.cs" Inherits="SLICResponseTeam_SLICTicket" EnableEventValidation="false"%>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">
        function custom_alert(message, title)
        {
                if ( !title )
                    title = 'Alert';

                if ( !message )
                     message = 'No Message to Display.';


            if (title == 'Alert') {

                swal
                    ({
                        title: title,
                        text: message,
                        icon: "warning",
                        closeOnClickOutside: false,
                        button: true
                    });
            }
            else if (title == 'Success')
            {
                 swal
                    ({
                        title: title,
                        text: message,
                         icon: "success",
                         closeOnClickOutside: false,
                         button: false,
                    });
            }
   else if (title == 'Info')
            {
                 swal
                    ({
                        title: title,
                        text: message,
                        icon: "info",
                        button: true,
                        closeOnClickOutside: true,
                    });

            }

        }




         function clientFunctionValidationGrid() {

             var temprefNo = $('#<%=refNo.ClientID %>').val();

             if (temprefNo == "") {
                 $('#<%= validation3.ClientID%>').css('display', '');
                 custom_alert('Please enter reference number', 'Alert');
                 return false;
             }

             window.setTimeout(function () {
                 $(".alert").fadeTo(500, 0).slideUp(500, function () {
                     $(this).remove();
                 });
             }, 3000);


             custom_alert('Processing.', 'Info');
             return true;


         }
         



        function clientFunctionValidation()
        {

           <%-- var temprefNo = $('#<%=refNo.ClientID %>').val();
        
            if (temprefNo == "")
            {   $('#<%= validation3.ClientID%>').css('display', '');
                custom_alert('Please enter reference number', 'Alert');
                return false;
            }--%>


            var fileupload = $('#<%=FileUpload1.ClientID %>').val();
        
            if (fileupload == "")
            {   <%--$('#<%= validation3.ClientID%>').css('display', '');--%>
                custom_alert('Please select a file for upload', 'Alert');
                return false;
            }

             var ddl1 = $('#<%=ddlStatus.ClientID %>').val();
           
            if (ddl1=="0")
            {    $('#<%= validation1.ClientID%>').css('display', '');
                 custom_alert('Please select a option.', 'Alert');
                 return false;
               
            }
            else
            {
                 $('#<%= validation1.ClientID%>').css('display', 'none');
            }
         
            custom_alert('File uploaded.', 'Success');
            return true;

         }


           function clientFunctionValidationFinished()
        {

            <%--var temprefNo = $('#<%=refNo.ClientID %>').val();
        
            if (temprefNo == "")
            {   $('#<%= validation3.ClientID%>').css('display', '');
                custom_alert('Please enter reference number', 'Alert');
                return false;
            }--%>


            

           var ddl1 = $('#<%=ddlStatus.ClientID %>').val();
           
            if (ddl1=="0")
            {    $('#<%= validation1.ClientID%>').css('display', '');
                 custom_alert('Please select a option.', 'Alert');
                 return false;
               
            }
            else
            {  
                 
                 $('#<%= validation1.ClientID%>').css('display', 'none');
            }

             var ddl2 = $('#<%=ddlStatus.ClientID %>').val();
             var isUpload = $('#<%=isUploaded.ClientID %>').val();

               if ((ddl2=="1")||(ddl2=="2")||(ddl2=="3")||(ddl2=="4"))
                  {
                    var fileupload = $('#<%=FileUpload1.ClientID %>').val();
        
                    if(isUpload=="Y"){ }
                    else
                        {    

                            //if (fileupload == "" && isUpload=="")
                            // {   
                            // custom_alert('Please select a file for upload', 'Alert');
                            //return false;
                            //  }
                           if (fileupload != "" && isUpload=="")
                            {   
                             custom_alert('Please upload quotation first.', 'Alert');
                            return false;
                            }
                        }
                  }
            
            var ddl3 = $('#<%=ddlStatus.ClientID %>').val();
              if ((ddl2=="1")||(ddl2=="2")||(ddl2=="3")||(ddl2=="4"))
                  {
                    var tempremark = $('#<%=txtremark.ClientID %>').val();
        
                    if (tempremark == "")
                     {   $('#<%= validation2.ClientID%>').css('display', '');
                         custom_alert('Please enter remarks', 'Alert');
                         return false;
                     }
                   
               }

                  var displyRef = $('#<%=tempRefNo.ClientID %>').val();
               
                 custom_alert('Successfully Processing wait.... \n\n Reference No. '+displyRef, 'Success'); 
         
                  return true;

         }


      $(document).ready(function () {

              window.setTimeout(function () {
                  $(".alert").fadeTo(500, 0).slideUp(500, function () {
                      $(this).remove();
                  });
              }, 3000);

            $(function () {


                $('#<%= ddlStatus.ClientID%>').change(function() {
                    $('#<%= validation1.ClientID%>').css('display', 'none');   
                   
                });

            });


             $(function () {
                     $('#<%= txtremark.ClientID%>').on('change keyup paste', function () {                 
                         $('#<%= validation2.ClientID%>').css('display', 'none');
                     });
       });
       
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

       //auto complete refernce number
           $(function () {
                    $('#<%= txt_req_id.ClientID%>').autocomplete({

                        source: function (request, response) {
                            
                            $.ajax({

                                 url: '<%=ResolveUrl("~/SLICResponseTeam/SLICTicket.aspx/Get_Ticket_No") %>',
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
        ////-------end---------->

          $(document).ready(function () {

         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

         function EndRequestHandler(sender, args) {

             //window.setTimeout(function () {
             //    $(".alert").fadeTo(500, 0).slideUp(500, function () {
             //        $(this).remove();
             //    });
             //}, 3000);


             
             //function dropdown
          


                 //---ref validatiion
                 $(function () {
                     $('#<%= refNo.ClientID%>').on('change keyup paste', function () {
                         $('#<%= validation3.ClientID%>').css('display', 'none');

                     });

                 });


                 //---remark validatiion

              $(function () {
                     $('#<%= txtremark.ClientID%>').on('change keyup paste', function () {                 
                         $('#<%= validation2.ClientID%>').css('display', 'none');
                     });
                 });


  
             //auto complete refernce number
           $(function () {
                    $('#<%=txt_req_id.ClientID%>').autocomplete({

                           source: function (request, response) {
                            
                            $.ajax({

                               url: '<%=ResolveUrl("~/SLICResponseTeam/SLICTicket.aspx/Get_Ticket_No") %>',
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
        ////-------end---------->

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


              }

     });

   </script>

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
   
   .testClass {
            /*font-family: Courier New, Courier, monospace;*/
            font-size: 12px;
            font-weight: 600;
            color:rgb(0, 0, 0);
        }

        .testClassHeader {
            /*font-family: Courier New, Courier, monospace;*/
            font-size: 13px;
            font-weight: 700;
        }

        .long-textbox {
            max-height: 40px;
            margin-top: 5px;
            margin-bottom: 50px;
        }
        .btnCss
        { 
            font-size:xx-large;      
            
        }
        a:link {
            color: rgb(15, 25, 88);
                }

        /* visited link */
        a:visited
        {
          color: rgb(15, 25, 88);
        }

        /* mouse over link */
        a:hover 
        {
        color: red;
        }

        /* selected link */
        a:active 
        {
        color: red;
        }

  </style>
<style type="text/css"> 
      .form-group 
      {
  margin-bottom: 5px;
      }
     </style>
   


<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hfTicketId" runat="server" />
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="Less_text" runat="server" />
    <asp:HiddenField ID="Reson_temp" runat="server" />
    <asp:HiddenField ID="app_date" runat="server" />
    <asp:HiddenField ID="reqDate" runat="server" />
    <asp:HiddenField ID="HiddenFieldRemark" runat="server" />
     <asp:HiddenField ID="bank_code" runat="server" />
    <asp:HiddenField ID="statusHidden" runat="server" />
    <asp:HiddenField ID="user_epf" runat="server" />
    <asp:HiddenField ID="req_status" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />
    <asp:HiddenField ID="isUploaded" runat="server" />
     <asp:HiddenField ID="tempStatus" runat="server" />
    <asp:HiddenField ID="bankEnmailId" runat="server" />
    <asp:HiddenField ID="tempRefNo" runat="server" />
    
     <div class="m-2" id="viewPropId" runat="server">
        <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-center">Quick Response Team - view and send reply to banks.</label>
        </div>
         <div class="form-group row" runat="server">
             <asp:TextBox ID="txtRefNo" runat="server" Visible="false"></asp:TextBox>
             <asp:TextBox ID="txtSelectTicket" runat="server" Visible="false"></asp:TextBox>
             <%--panel 2nd--%>
            <div class="col-sm-12 border border-info p-1 mt-1 mr-2 ml-2" runat="server">
  <asp:UpdatePanel ID="UpdatePanel4" runat="server" updatemode="Conditional">
    <ContentTemplate>
    <div class="container" id="progressId" runat="server">

          <div class="jumbotron text-center m-2" id="idNew" runat="server" visible="false">
        <asp:LinkButton class="btn btn-large btn-success col-sm-4" runat="server" OnClick="btNewClick">
           <i class="fa fa-paper-plane h2" aria-hidden="true"></i>
                <span class="h1 font-weight-bold text-center text-white m-3">New Inquiry</span>
        </asp:LinkButton>
         </div>
          <div class="jumbotron text-center m-2" id="idView" runat="server">
        <asp:LinkButton class="btn btn-large btn-success col-sm-4" runat="server" onclick="btViewClick">
           <i class="fa fa-envelope h2" aria-hidden="true"></i>
                <span class="h1 font-weight-bold text-center text-white m-3">View Inquiry</span>
        </asp:LinkButton>
         </div>


          <div runat="server" id="divView"> 

           <div class="" id="Div2" runat="server">
      
       <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-left">Search Categories</label>
        </div>
          <div class="form-group row" runat="server">
               <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">From</label>
                   <asp:TextBox ID="txt_start_date" runat="server" CssClass="form-control text-center testClass"  ClientIDMode="Static" ValidationGroup="VG0001" placeholder="From Date" autocomplete="off"></asp:TextBox>
                </div>

              <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">To</label>
              <asp:TextBox ID="txt_end_date" runat="server" CssClass="form-control text-center testClass"  ClientIDMode="Static" ValidationGroup="VG0001" placeholder="To Date" autocomplete="off"></asp:TextBox>  
              </div>


               <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">Inquiry Number</label>
              <asp:TextBox ID="txt_req_id" runat="server" CssClass="form-control text-center testClass"  ClientIDMode="Static" placeholder="Inquiry No."></asp:TextBox>  
              </div>

             

                <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">Types</label>

                     <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_status" runat="server" class="custom-select testClass text-center"
                                ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Value="0">-- Select Options --</asp:ListItem>
                    <asp:ListItem Value="1">Motor insurance</asp:ListItem> 
                    <asp:ListItem Value="2">Fire insurance (house and solar panel)</asp:ListItem> 
                    <asp:ListItem Value="3">Fire insurance (all other categories)</asp:ListItem> 
                    <asp:ListItem Value="4">Title insurance</asp:ListItem> 
                    <asp:ListItem Value="5">Marine cargo insurance</asp:ListItem> 
                    <asp:ListItem Value="6">Debit note request</asp:ListItem> 
                    <asp:ListItem Value="7">All other categories</asp:ListItem> 

                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_status" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
              </div>

               <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">Status</label>

                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddlTicketStatus" runat="server" class="custom-select testClass text-center"
                                ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Value="0">-- Select Options --</asp:ListItem>
                                <asp:ListItem Value="P" Selected="True">Pending</asp:ListItem> 
                                <asp:ListItem Value="D">Complete</asp:ListItem> 
                          

                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlTicketStatus" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
              </div>

              </div>

               <div class="form-group row" runat="server">
               <div class="col-sm-2 mr-0 testClass" runat="server">
                    <label class="font-weight-bold testClass" runat="server">Bank</label>

                     <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
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

                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
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
                     <asp:Button ID="btn_find" runat="server" Text="Search" ValidationGroup="VG0001" onclick="btn_find_Click1"                                   
                         ClientIDMode="Static" class="btn btn-success testClass mr-1 mt-4 ml-0"/>
                    <asp:Button ID="btn_clear" runat="server" Text="Clear" onclick="btn_clear_Click" ClientIDMode="Static" 
                                  class="btn btn-success testClass mr-1 mt-4 ml-1"/>
                   <asp:ImageButton ID="ImageButton8" runat="server" ImageAlign="Middle" 
                                ImageUrl="~/Images/icons8-downloading-updates-20.png"
                                ToolTip="Click here to download as excel file" class="btn btn-success testClass mr-1 mt-4 ml-1 img-fluid" OnClick="btexcel_Click" 
                                 OnClientClick="Alert()"/>
                 </div>
                  
           </div>
         <div class="form-group row" runat="server">
           <asp:CompareValidator ID="comp_val" ValidationGroup = "VG0001" ForeColor = "Red" runat="server" 
                            ControlToValidate = "txt_start_date" ControlToCompare = "txt_end_date" Operator = "LessThanEqual" Type = "Date"
                            ErrorMessage="Start date must be less than End date." class="font-weight-bold testClass"></asp:CompareValidator>
        </div>



           <div class="form-group row" runat="server">
            <div class="col-md-12">
                <div class="table-responsive"> 
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:GridView ID="Grid_Details" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="8"
                                CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100"
                                OnPageIndexChanging="Grid_Details_PageIndexChanging" OnRowDataBound="OnRowDataBound">

                                <Columns>                            
                              <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-dark testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
                                  <ItemTemplate>
                                      <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Left" />
                              </asp:TemplateField>

                                <asp:BoundField HeaderText="Inquiry Number"  DataField="T_REF" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>    
                                <asp:BoundField HeaderText="Bank Name"  DataField="T_BANK_NAME" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>    
                                <asp:BoundField HeaderText="Branch Name"  DataField="T_BRANCH_NAME" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>
                                <asp:BoundField HeaderText="Request Type"  DataField="T_FLAG" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>
                                <asp:BoundField HeaderText="Bank Remarks"  DataField="T_BANK_REMARK" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/> 
                                <asp:BoundField HeaderText="Created By"  DataField="T_CREATED_BY" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/> 
                                <asp:BoundField HeaderText="Created Date"  DataField="T_CREATED_ON" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                
                                <asp:BoundField HeaderText="Status"  DataField="T_STATUS" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                               
                                <asp:BoundField HeaderText="SLIC Remarks"  DataField="T_SLIC_REMARK" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                                <asp:BoundField HeaderText="Responded Date"  DataField="T_UP_ON_SLIC" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                                <asp:BoundField HeaderText="Responded By"  DataField="T_UP_BY_SLIC" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />

                               <asp:BoundField HeaderText="Bank Email"  DataField="T_BANK_EMAIL" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide"/>
                               <asp:BoundField HeaderText="Bank Code"  DataField="T_BANK_CODE" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide"  />

                               <asp:TemplateField HeaderText="View" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                               <ItemTemplate>

                                   <asp:ImageButton ID="imgbtn_select" runat="server"  CommandArgument = '<%#Eval("T_REF")+";"+ Eval("T_STATUS")+";"+ Eval("T_BANK_EMAIL")+";"+ Eval("T_BANK_CODE")%>'  CausesValidation= "false" 
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
          <%----- view more about ticket----%>
          <div runat="server" id="moreInfo">
              <div class="form-group row" runat="server">
                  <label for="" class="col-sm-4 font-weight-bolder h6 text-left"></label>
            <label for="" class="col-sm-2 font-weight-bolder h6 text-left">Inquiry Number</label>
            <label for="" class="col-sm-6 font-weight-bold h6 text-left text-danger" runat="server" id="lblTicket"></label>
        </div>

              <div class="form-group row" runat="server">
                  <label for="" class="col-sm-4 font-weight-bolder h6 text-left"></label>
            <label for="" class="col-sm-2 font-weight-bolder h6 text-left">Bank Name</label>
                  <label for="" class="col-sm-6 font-weight-normal h6 text-left" runat="server" id="lblBankName"></label>
        </div>
              <div class="form-group row" runat="server">
                  <label for="" class="col-sm-4 font-weight-bolder h6 text-left"></label>
            <label for="" class="col-sm-2 font-weight-bolder h6 text-left">Branch Name</label>
                  <label for="" class="col-sm-6 font-weight-normal h6 text-left" runat="server" id="lblBankBr"></label>
        </div>
              <div class="form-group row" runat="server">
                  <label for="" class="col-sm-4 font-weight-bolder h6 text-left"></label>
            <label for="" class="col-sm-2 font-weight-bolder h6 text-left">Bank remarks</label>
                  <label for="" class="col-sm-6 font-weight-normal h6 text-left" runat="server" id="lblBankRemaks"></label>
        </div>
              <div class="form-group row" runat="server">
                  <label for="" class="col-sm-4 font-weight-bolder h6 text-left"></label>
            <label for="" class="col-sm-2 font-weight-bolder h6 text-left">Entered date</label>
                  <label for="" class="col-sm-6 font-weight-normal h6 text-left" runat="server" id="lblEnteredDate"></label>
        </div>
              <div class="form-group row" runat="server">
                  <label for="" class="col-sm-4 font-weight-bolder h6 text-left"></label>
            <label for="" class="col-sm-2 font-weight-bolder h6 text-left">SLIC remarks</label>
                  <label for="" class="col-sm-6 font-weight-normal h6 text-left" runat="server" id="lblSLICRemaks"></label>
        </div>
              <div class="form-group row" runat="server">
                  <label for="" class="col-sm-4 font-weight-bolder h6 text-left"></label>
            <label for="" class="col-sm-2 font-weight-bolder h6 text-left">Replied date</label>
             <label for="" class="col-sm-6 font-weight-normal h6 text-left" runat="server" id="lblReplyDate"></label>
        </div>

        <div class="form-group row" runat="server" id ="WbankLbl" >
                  
            <label for="" class="col-sm-12 font-weight-bolder h6 text-center" runat="server" id="txtBankLbl"></label>
             
        </div>
              <div class="form-group row"> 
                  <%--bank document--%>
                  <div class="col-sm-12 font-weight-bold d-flex justify-content-center align-items-center" runat="server">
     
                  <div class="table-responsive-sm col-sm-10" >
          <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="4" CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100" OnPageIndexChanging="GridView2_PageIndexChanging">  <%--OnPageIndexChanging="GridView1_PageIndexChanging"--%>
            <Columns>  
                <asp:BoundField DataField="T_NAME" HeaderText="File name" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" /> 
                <asp:BoundField DataField="CREATED_ON" HeaderText="Created date" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" />  
                <asp:BoundField DataField="STATUS" HeaderText="Status" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" />  
                
                  

                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
   
                     <ItemTemplate><label class="text-primary" runat="server">Download
                                            <asp:ImageButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                                CommandArgument='<%# Eval("T_NO") %>' ImageUrl="~/Images/viewIconOne.png"></asp:ImageButton>
                                        </label>
                                         </ItemTemplate>
                            </asp:TemplateField>

            </Columns>  
        </asp:GridView>  
                      </div>               

                 </div>   
                  </div>
          <div class="form-group row" runat="server" id = "slicLbl">
            <label for="" class="col-sm-12 font-weight-bolder h6 text-center" runat="server" id="txtSlicLbl"></label>
        </div>
              <div class="form-group row"> 
                  <%--slic document--%>
                  <div class="col-sm-12 font-weight-bold d-flex justify-content-center align-items-center" runat="server">
     
                  <div class="table-responsive-sm col-sm-10" >  
          <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="4" CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100" OnPageIndexChanging="GridView3_PageIndexChanging">  <%--OnPageIndexChanging="GridView1_PageIndexChanging"--%>
            <Columns>  
                <asp:BoundField DataField="T_NAME" HeaderText="File name" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" /> 
                <asp:BoundField DataField="CREATED_ON" HeaderText="Created date" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" />  
                <asp:BoundField DataField="STATUS" HeaderText="Status" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" /> 
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
                     <ItemTemplate>
                         <label class="text-primary" runat="server">Download
                                      <asp:ImageButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFileSLIC"
                                       CommandArgument='<%# Eval("T_NO") %>' ImageUrl="~/Images/viewIconOne.png"></asp:ImageButton>
                                      </label>
                   </ItemTemplate>
                            </asp:TemplateField>

            </Columns>  
        </asp:GridView>  
                      </div>               

                 </div>         
             </div>

          </div>
          <%--end--%>
                  </div>



      <div runat="server" id="divNew">
        <hr />
         <div class="form-group row" id="rowOpt" runat="server">
              <label class="col-sm-3 font-weight-bold" runat="server">Inquiry Types</label>
           <div class="col-sm-7" runat="server">   
                <%-- <asp:UpdatePanel ID="UpdatePanel4" runat="server" updatemode="Conditional">
                        <ContentTemplate>--%> 
                <asp:DropDownList ID="ddlStatus" runat="server" class="custom-select"
                    ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="true"> <%--OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"--%>
                   <asp:ListItem Value="0">-- Select Options --</asp:ListItem>
                                <asp:ListItem Value="1">Motor insurance</asp:ListItem> 
                                <asp:ListItem Value="2">Fire insurance (house and solar panel)</asp:ListItem> 
                                <asp:ListItem Value="3">Fire insurance (all other categories)</asp:ListItem> 
                                <asp:ListItem Value="4">Title insurance</asp:ListItem> 
                                <asp:ListItem Value="5">Marine cargo insurance</asp:ListItem> 
                                <asp:ListItem Value="6">Debit note request</asp:ListItem> 
                                <asp:ListItem Value="7">All other categories</asp:ListItem>
               </asp:DropDownList>
                           <%-- </ContentTemplate>--%>
                  <%--    <Triggers>
         <asp:Asyncpostbacktrigger controlid="ddlStatus" eventname="SelectedIndexChanged" />
       </Triggers>--%>
                     <%--</asp:UpdatePanel>--%>
            </div>
             <label class="col-sm-2 text-danger" runat="server" id="validation1" style="display:none" >*Required</label>
             </div>

         <div class="form-group row" runat="server" id="rowRemark">
            <label class="col-sm-3 font-weight-bold" runat="server">Remarks<label class="text-danger">*</label></label>
        <div class="col-sm-7" runat="server">  
            <textarea type="text" class="form-control" id="txtremark" placeholder="Remarks" runat="server" rows="5"></textarea>         
            <label class="col-sm-2 text-danger" runat="server" id="validation2" style="display:none" >*Required</label>
       </div>
    </div>

        <hr />


         <div class="form-group row" runat="server" id="rowRef">
            <div class="col-sm-3 font-weight-bold" runat="server">Reference No.</div>
            <div class="col-sm-6" runat="server">
              <input type="text" class="form-control" id="refNo" placeholder="Reference Number" runat="server">
             <label class="col-sm-3 text-danger" runat="server" id="validation3" style="display:none" >*Required</label> 
           </div>
            <div class="col-sm-2" runat="server">
                <asp:Button ID="Button1" runat="server" Text="Validate" class="btn btn-success mt-1" OnClientClick="return clientFunctionValidationGrid()" ClientIDMode="Static" OnClick="Button1_Click" Visible="false"/> 
            </div>
        </div>



        <div class="form-group row" runat="server" id="rowUpload">
            <div class="col-sm-3 font-weight-bold" runat="server">Select files..</div>
            <div class="col-sm-6" runat="server">
       <asp:FileUpload runat="server" ID="FileUpload1" AllowMultiple="true" Width="100%" Class="h-100 bg-transparent form-control"/>
      
           </div>
            <div class="col-sm-2" runat="server">
                <asp:Button ID="btnUpload" runat="server" Text="        Upload        " class="btn btn-success mt-1"  OnClick="Upload" OnClientClick="return clientFunctionValidation()" ClientIDMode="Static"/> 
            </div>
          
        </div>
         <div class="form-group row" runat="server" id="rowFinish">
            <div class=" col-sm-7" runat="server" id="Div1"> </div>
            <div class="col-sm-2" runat="server"></div>
            <div class="col-sm-2" runat="server">
                <asp:Button ID="btfinished" runat="server" Text=" Send Response " class="btn btn-success"   ClientIDMode="Static" OnClick="btfinished_Click" OnClientClick="return clientFunctionValidationFinished()"/> 
            </div>
          </div>
        <hr />
          <div class="form-group row">
        <label for="" class="col-sm-4 h6 font-weight-bold text-info">Uploaded Documents</label>
              </div>

      
       <div class="form-group row">
                  <div class="col-sm-12 font-weight-bold d-flex justify-content-center align-items-center" runat="server">
     
                  <div class="table-responsive-sm col-sm-8" >  
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="4" CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100" OnPageIndexChanging="GridView1_PageIndexChanging">  <%----%>
            <Columns>  
                <asp:BoundField DataField="T_NAME" HeaderText="File name" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" /> 
                <asp:BoundField DataField="CREATED_ON" HeaderText="Created date" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" />  
                <asp:BoundField DataField="STATUS" HeaderText="Status" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" />  
                
                  

                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
   
                     <ItemTemplate><label class="text-primary" runat="server">Download
                                            <asp:ImageButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFileSLIC"
                                                CommandArgument='<%# Eval("T_NO") %>' ImageUrl="~/Images/viewIconOne.png"></asp:ImageButton>
                                        </label>
                                         </ItemTemplate>
                            </asp:TemplateField>


                              <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
 
                                     <ItemTemplate><label class="text-primary" runat="server">Remove
                                            <asp:ImageButton ID="lnkRemove" runat="server" Text="Remove" OnClick="RemoveFile"
                                                CommandArgument='<%# Eval("T_NO") %>' ImageUrl="~/Images/delete.png"></asp:ImageButton>
                                        </label>
                                         </ItemTemplate>
                            </asp:TemplateField>


            </Columns>  
        </asp:GridView>  
                      </div>               

                 </div>         
             </div>

          </div>



    
                

          </div>
          <%--end--%>
         
       </div>



         <div class="form-group row" runat="server">
             
             <div class=" col-sm-8 row align-self-sm-start justify-content-sm-center w-100 font-weight-normal" runat="server" id="mainAlert"> 
          <%-- <div class=" col-sm-8 row font-weight-normal" runat="server" id="mainAlert"> --%>
    <!-- Success Alert -->
    <div class="alert alert-success alert-dismissible fade show" runat="server" id="sucsessAlert">
        <h5><strong>Success!</strong></h5><br /> Your files have been sent successfully.
          <P runat="server" id="sucsessContent"></P>
          <P runat="server" id="sucsessContent2"></P>
        <button type="button" class="close" data-dismiss="alert">&times;</button>
    </div>
    <!-- Error Alert -->
    <div class="alert alert-danger alert-dismissible fade show" runat="server"  id="errorAlert">
        <h5><strong>Error!</strong></h5> A problem has been occurred while submitting your data.
        <hr />
         <P runat="server" id="errorContent"></P>
         <P runat="server" id="errorContent2"></P>
        <button type="button" class="close" data-dismiss="alert">&times;</button>
    </div>
    <!-- Warning Alert -->
    <div class="alert alert-warning alert-dismissible fade show" runat="server"  id="warningAlert">
        <h5><strong>Warning!</strong></h5><%--There was a problem with database.--%>
        <P runat="server" id="warningContent"></P>
        <P runat="server" id="warningContent2"></P>
        <button type="button" class="close" data-dismiss="alert">&times;</button>
    </div>
    <!-- Info Alert -->
    <div class="alert alert-info alert-dismissible fade show" runat="server"  id="infoAlert">
        <h5><strong>Info!</strong></h5> Please read the comments carefully.
        <button type="button" class="close" data-dismiss="alert">&times;</button>
    </div>
               </div>
      <div class="col-sm-2" runat="server"></div>
      <div class="col-sm-2 ml-4" runat="server"></div>
</div> 
        </div>
     

    </ContentTemplate>
        <Triggers>
         <asp:Asyncpostbacktrigger controlid="ddlStatus" eventname="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnUpload" />  
            <asp:PostBackTrigger ControlID="GridView1" />
            <asp:PostBackTrigger ControlID="GridView2" />
            <asp:PostBackTrigger ControlID="GridView3" />
            <asp:PostBackTrigger ControlID="Grid_Details" />
            <asp:PostBackTrigger ControlID="btfinished" />
            <asp:PostBackTrigger ControlID="ImageButton8" />
           
         
       </Triggers>
         </asp:UpdatePanel>

            </div>
        </div>
     </div>

    <div id="coverScreen"  class="LockOn"></div>
   
</asp:Content>
