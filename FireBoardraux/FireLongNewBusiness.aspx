<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FireLongNewBusiness.aspx.cs" Inherits="FireBoardraux_FireLongNewBusiness" %>

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
            font-size: 13px;
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
            <label for="" class="col-sm-12 font-weight-bolder h5 text-center">Fire Boardraux Long Term</label>
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
                    <label class="font-weight-bold testClass" runat="server">Types</label>

                     <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_status" runat="server" class="custom-select testClass text-center"
                               ClientIDMode="Static" AppendDataBoundItems="true">
                               <asp:ListItem Text="New Business" Value="SYS-NEBU" Selected="True"></asp:ListItem>
                           </asp:DropDownList>      
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddl_status" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
              </div>
               <div class="col-sm-2 testClass mt-4" runat="server"><label class="text-success font-weight-bold h6" id="lblTotR" runat="server" visible="false"></label>
                          <asp:CompareValidator ID="CompareValidator1" ValidationGroup = "VG0001" ForeColor = "Red" runat="server" 
                            ControlToValidate = "txt_start_date" ControlToCompare = "txt_to_date" Operator = "LessThanEqual" Type = "Date"
                            ErrorMessage="Start date must be less than End date." class="font-weight-bold testClass mt-4"></asp:CompareValidator>
                </div>

                <div class="col-sm-3 mr-0 testClass" runat="server">

                 <asp:Button ID="btn_find" runat="server" Text="Search" 
                                 ValidationGroup="VG0001" onclick="btn_find_Click1"                                   
                                  ClientIDMode="Static" class="btn btn-success testClass mr-1 mt-4 ml-0" OnClientClick="Alert()"/>

                 <asp:Button ID="btn_clear" runat="server"
                                 Text="Clear" onclick="btn_clear_Click"  ClientIDMode="Static" 
                                  class="btn btn-success testClass mr-1 mt-4 ml-1"/>

                 <asp:ImageButton ID="ImageButton8" runat="server" ImageAlign="Middle" 
                                ImageUrl="~/Images/icons8-downloading-updates-20.png"
                                ToolTip="Click here to download as excel file" class="btn btn-success testClass mr-1 mt-4 ml-1 img-fluid" OnClick="ImageButton8_Click" 
                                 OnClientClick="Alert()" Visible="false"/>


                 </div>
              </div>


         <div class="form-group row" runat="server">
            <div class="col-md-12">
                <div class="table-responsive"> 
 <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

 <asp:GridView ID="Grid_Details" Visible="true" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100"
     OnPageIndexChanging="Grid_Details_PageIndexChanging" ShowFooter="true"> 
                          <Columns>                            
                               <asp:TemplateField HeaderText="SEQ" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                                  <ItemTemplate>
                                      <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" />
                               </asp:TemplateField>
                               <asp:BoundField HeaderText="MONTH - CEEDED MONTH"  DataField="BMONTH" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                    
                               <asp:BoundField HeaderText="UNDERWRITING YEAR"  DataField="BYEAR" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="POLICY NUMBER"  DataField="SC_POLICY_NO" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="INITIAL POL. COMME. MONTH"  DataField="INMONTH" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="NO. OF POLICY YEARS"  DataField="NOOFPOLICYYEARS" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="RENEWAL DUE YEAR(2ND,3RD, ETC)"  DataField="RENEWAL_DUE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="POL. PERIOD - CURRENT PERIOD"  DataField="CURPERIOD" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="RECEIPT NUMBER"  DataField="RECEIPT_NUMBER" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="RECEPT DATE"  DataField="RECEPT_DATE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="FUNCTION NAME"  DataField="FUNCTIONAL_NAME" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="FUNCTION NUMBER"  DataField="FUNCTIONAL_NUMBER" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="METERIAL NUMBER"  DataField="METERIAL_NUMBER" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="DESCRIPTION"  DataField="DESCRIPTION" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="METERIAL GROUP"  DataField="METERIAL_GROUP" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                                                           
                               <asp:BoundField HeaderText="LOB NUMBER"  DataField="LOB_NUMBER" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                                                            
                               <asp:BoundField HeaderText="LOB NAME"  DataField="LOB_NAME" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="NAME OF INSURED"  DataField="NAME_OF_INSURED" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="LOCATION"  DataField="LOCATION" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="FROM"  DataField="POL_FROM" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="TO"  DataField="POL_TO" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="FROM"  DataField="END_FROM" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="TO"  DataField="END_TO" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/> 
                               <asp:BoundField HeaderText="MD (LKR)"  DataField="MD_SUM_INSURED" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" DataFormatString="{0:n2}"/>                                                             
                               <asp:BoundField HeaderText="TOTAL SUM INSURED"  DataField="TOTAL_SUM_INSURED" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" DataFormatString="{0:n2}"/>
                               <asp:BoundField HeaderText="ORIGINAL/COMBINED RATE"  DataField="ORIGINAL_COMINED_RATE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/> 
                               <asp:BoundField HeaderText="TOTAL RATE"  DataField="TOTAL_RATE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                                             
                               <asp:BoundField HeaderText="TOTAL PREMIUM"  DataField="TOTAL_PREMIUM" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" DataFormatString="{0:n2}"/>                                                             
                               <asp:BoundField HeaderText="ADMIN FEE(FOR RESP.YEAR)" DataField="ADMIN_FEE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" DataFormatString="{0:n2}"/>                                                             
                               <asp:BoundField HeaderText="POLICY / RENEWAL FEE(FOR RESP.YEAR)" DataField="POL_OR_RENEWAL" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" DataFormatString="{0:n2}"/>                                                             
                               <asp:BoundField HeaderText="PREMIUM TO_SRCC" DataField="RCC_FUND" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" DataFormatString="{0:n2}"/>                                                             
                               <asp:BoundField HeaderText="PREMIUM TO TR" DataField="TR_FUND" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" DataFormatString="{0:n2}"/> 
                               <asp:BoundField HeaderText="PREMIUM (EX. SRCC & TR)" DataField="PREMIUM_EXTRSRCC" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" DataFormatString="{0:n2}"/>  
                              

                               <asp:BoundField HeaderText="NET RATE"  DataField="NET_RATE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" DataFormatString="{0:0.00}%"/>                                
                               <asp:BoundField HeaderText="NET RETENTED SI"  DataField="NET_RETAINTED_SI" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" DataFormatString="{0:n2}"/>                                                      
                               <asp:BoundField HeaderText="FIRE TREATY A"  DataField="FIRE_TREATY_A" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>   
                               <asp:BoundField HeaderText="FIRE TREATY B"  DataField="FIRE_TREATY_B" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               
                               <asp:BoundField HeaderText="FACULTATIVE SI"  DataField="FACULTATIVE_SI" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="NET RETENTED SI"  DataField="EXT_NET_RETAINTED_SI" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="FIRE TREATY A" DataField="EXT_FIRE_TREATY_A" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="FIRE TREATY B" DataField="EXT_FIRE_TREATY_B" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               
                              <asp:BoundField HeaderText="FACULTATIVE SI" DataField="EXT_FACULTATIVE_SI" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="NET RETENTION" DataField="PRE_NET_RETAINTION" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="FIRE TREATY A" DataField="PRE_FIRE_TREATY_A" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="FIRE TREATY B" DataField="PRE_FIRE_TREATY_B" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="FACULTATIVE" DataField="PRE_FACULTATIVE" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="PROFIT CENTER" DataField="PROFIT_CENTER" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="COST CENTER" DataField="COST_CENTER" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>  
                               <asp:BoundField HeaderText="REMARK" DataField="REMARK" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>                                      
                                            
                     </Columns>
                     </asp:GridView>

                              </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                </div>
             </div>


         </div>
</asp:Content>
