<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewPageSecond.aspx.cs" Inherits="SLIC_Side_ViewPageSecond" %>
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

            var temprefNo = $('#<%=refNo.ClientID %>').val();
        
            if (temprefNo == "")
            {   $('#<%= validation3.ClientID%>').css('display', '');
                custom_alert('Please enter reference number', 'Alert');
                return false;
            }


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

            var temprefNo = $('#<%=refNo.ClientID %>').val();
        
            if (temprefNo == "")
            {   $('#<%= validation3.ClientID%>').css('display', '');
                custom_alert('Please enter reference number', 'Alert');
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

             var ddl2 = $('#<%=ddlStatus.ClientID %>').val();
             var isUpload = $('#<%=isUploaded.ClientID %>').val();

               if ((ddl2=="D"))
                  {
                    var fileupload = $('#<%=FileUpload1.ClientID %>').val();
        
                    if(isUpload=="Y"){ }
                    else
                        {    //alert(isUpload);

                            if (fileupload == "" && isUpload=="")
                             {   <%--$('#<%= validation3.ClientID%>').css('display', '');--%>
                             custom_alert('Please select a file for upload', 'Alert');
                            return false;
                              }
                             else if (fileupload != "" && isUpload=="")
                             {   <%--$('#<%= validation3.ClientID%>').css('display', '');--%>
                             custom_alert('Please upload quotation first.', 'Alert');
                            return false;
                              }
                        }
                  }
            
            var ddl3 = $('#<%=ddlStatus.ClientID %>').val();
             if ((ddl3=="R")||(ddl3=="C")||(ddl3=="M"))
                  {
                    var tempremark = $('#<%=txtremark.ClientID %>').val();
        
                    if (tempremark == "")
                     {   $('#<%= validation2.ClientID%>').css('display', '');
                         custom_alert('Please enter remarks', 'Alert');
                         return false;
                     }
                   
                  }
               
            custom_alert('Successfully Processing wait...', 'Success');
            return true;

         }


      $(document).ready(function () {

          //Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        //  function EndRequestHandler(sender, args) {

              window.setTimeout(function () {
                  $(".alert").fadeTo(500, 0).slideUp(500, function () {
                      $(this).remove();
                  });
              }, 3000);
          //}
      });



          $(document).ready(function () {

         Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

         function EndRequestHandler(sender, args) {

             //window.setTimeout(function () {
             //    $(".alert").fadeTo(500, 0).slideUp(500, function () {
             //        $(this).remove();
             //    });
             //}, 3000);


             
             //function dropdown
            $(function () {


                $('#<%= ddlStatus.ClientID%>').change(function() {
                    $('#<%= validation1.ClientID%>').css('display', 'none');   
                   
                });

            });


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
                    $('#<%= refNo.ClientID%>').autocomplete({

                        source: function (request, response) {
                            
                            $.ajax({

                                url: '<%=ResolveUrl("QuotationFunctions.aspx/Get_Ref_No") %>',
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


              }

     });

   </script>

  <style type="text/css">
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
  </style>
<style type="text/css"> 
      .form-group 
      {
  margin-bottom: 5px;
      }
     </style>
   


<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>
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
    
     <div class="m-2" id="viewPropId" runat="server">
        <div class="form-group row" runat="server">
            <label for="" class="col-sm-10 font-weight-bolder h5 text-center">Motor quotation request view</label>
        </div>
         <div class="form-group row" runat="server">
             <%--panel 1st--%>
            <div class="col-sm-5 border border-info p-1 mt-1 mr-2 ml-2" runat="server">
                <%--<div class="border border-info p-1">--%>
                 <div class="form-group row" runat="server">
                    <div class="col-sm-12 text-center font-weight-bold">Request info.</div>
                </div>

                <div class="form-group row" runat="server">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-4">Request ID</div>
                    <div class="col-sm-4"><asp:TextBox ID="Req_id" runat="server"  CssClass="form-control"  Enabled="False"></asp:TextBox></div>
                </div>
                <div class="form-group row" runat="server">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-4">Vehicle type</div>
                    <div class="col-sm-4"><asp:TextBox ID="v_type" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
                <div class="form-group row" runat="server">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-4">Year of manufature</div>
                    <div class="col-sm-4"><asp:TextBox ID="yom" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
                 <div class="form-group row" runat="server">
                     <div class="col-sm-2"></div>
                    <div class="col-sm-4">Sum insured</div>
                    <div class="col-sm-4"><asp:TextBox ID="txtsumInsu" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
                 <div class="form-group row" runat="server">
                     <div class="col-sm-2"></div>
                    <div class="col-sm-4">Make of vehicle</div>
                    <div class="col-sm-4"><asp:TextBox ID="txtMake" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
                 <div class="form-group row" runat="server">
                     <div class="col-sm-2"></div>
                    <div class="col-sm-4">Model of vehicle</div>
                    <div class="col-sm-4"><asp:TextBox ID="txtmodel" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
                  <div class="form-group row" runat="server">
                      <div class="col-sm-2"></div>
                    <div class="col-sm-4">Purpose</div>
                    <div class="col-sm-4"><asp:TextBox ID="txtpurpose" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
                <div class="form-group row" runat="server">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-4">Registratiion number</div>
                    <div class="col-sm-4"><asp:TextBox ID="txtV_reg" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
                <div class="form-group row" runat="server">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-4">Customer name</div>
                    <div class="col-sm-4"><asp:TextBox ID="txtcusname" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
                <div class="form-group row" runat="server">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-4">Customer contact name</div>
                    <div class="col-sm-4"><asp:TextBox ID="txtCusCon" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
                  <div class="form-group row" runat="server">
                      <div class="col-sm-2"></div>
                    <div class="col-sm-4">Fuel type</div>
                    <div class="col-sm-4"><asp:TextBox ID="txtfuel" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
                <div class="form-group row" runat="server">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-4">Customer email</div>
                    <div class="col-sm-4"><asp:TextBox ID="txtcusEmail" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
                </div>
               <%-- </div>--%>
            </div>
             <%--panel 2nd--%>
            <div class="col-sm-6 border border-info p-1 mt-1 mr-2 ml-2" runat="server">
                <div class="form-group row" runat="server">
                    <div class="col-sm-12 text-center font-weight-bold">Quotation process panel</div>
                </div>
                
                
                 <asp:UpdatePanel ID="UpdatePanel4" runat="server" updatemode="Conditional">
    <ContentTemplate>
    <div class="container" id="progressId" runat="server">
          <div class="form-group row">
         <label for="" class="col-sm-3 font-weight-bolder" >Current Status</label>
         <label for="" class="col-sm-9 font-weight-bold text-black text-justify h5" id="txtStatOne" runat="server"></label>
         <label for="" class="col-sm-9 font-weight-bold text-black text-justify h5" id="txtStatTwo" runat="server"></label>
         <label for="" class="col-sm-9 font-weight-bold text-black text-justify h5" id="txtStatThree" runat="server"></label>
         <label for="" class="col-sm-9 font-weight-bold text-black text-justify h5" id="txtStatFour" runat="server"></label>
        </div>
        <div class="form-group row" runat="server" visible="false">
         <label for="" class="col-sm-3 font-weight-bolder" >Latest remark of SLIC</label>
         <label for="" class="col-sm-9 font-weight-normal text-info text-justify" id="statusId" runat="server"></label>
        </div>
         <div class="form-group row border border-info p-1">
         <label for="" class="col-sm-3 font-weight-bolder" >SLIC previous remarks</label>
         <label for="" class="col-sm-9 font-weight-normal text-danger text-justify" id="txtSlicRemarks" runat="server"></label>
        </div>
         <div class="form-group row border border-info p-1">
         <label for="" class="col-sm-3 font-weight-bolder" >Bank previous remarks</label>
         <label for="" class="col-sm-9 font-weight-normal text-danger text-justify" id="txtBankRemarks" runat="server"></label>
        </div>
        <hr />
         <div class="form-group row">
              <label class="col-sm-3 font-weight-bold" runat="server">Conditions selection</label>
           <div class="col-sm-7" runat="server">   
                <%-- <asp:UpdatePanel ID="UpdatePanel4" runat="server" updatemode="Conditional">
                        <ContentTemplate>--%> 
                <asp:DropDownList ID="ddlStatus" runat="server" class="custom-select"
                    ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="true" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                    <asp:ListItem Value="0">-- Select Options --</asp:ListItem>
                    <asp:ListItem Value="D">Complete and send quotation</asp:ListItem> 
                    <asp:ListItem Value="M">Request more details / Messages from SLIC</asp:ListItem> 
                    <asp:ListItem Value="C">Cancel previous quotation</asp:ListItem> 
                    <asp:ListItem Value="R">Reject</asp:ListItem> 
                    <%--<asp:ListItem Value="RM">Forward to Risk Management</asp:ListItem> 
                    <asp:ListItem Value="RI">Forward to Reinsurance</asp:ListItem>--%> 
                       </asp:DropDownList>
                           <%-- </ContentTemplate>--%>
                  <%--    <Triggers>
         <asp:Asyncpostbacktrigger controlid="ddlStatus" eventname="SelectedIndexChanged" />
       </Triggers>--%>
                     <%--</asp:UpdatePanel>--%>
            </div>
             <label class="col-sm-2 text-danger" runat="server" id="validation1" style="display:none" >*Required</label>
             </div>

   <div class="form-group row" runat="server">
            <label class="col-sm-3 font-weight-bold" runat="server">Remarks<label class="text-danger">*</label></label>
        <div class="col-sm-7" runat="server">  
            <textarea type="text" class="form-control" id="txtremark" placeholder="Remarks" runat="server" rows="5"></textarea>         
            <label class="col-sm-2 text-danger" runat="server" id="validation2" style="display:none" >*Required</label>
       </div>
    </div>

        <hr />


         <div class="form-group row" runat="server">
            <div class="col-sm-3 font-weight-bold" runat="server">Reference No.</div>
            <div class="col-sm-6" runat="server">
              <input type="text" class="form-control" id="refNo" placeholder="Reference Number" runat="server">
             <label class="col-sm-3 text-danger" runat="server" id="validation3" style="display:none" >*Required</label>  
           </div>
            <div class="col-sm-2" runat="server">
                <asp:Button ID="Button1" runat="server" Text="Validate" class="btn btn-success mt-1"  OnClientClick="return clientFunctionValidationGrid()" ClientIDMode="Static" OnClick="Button1_Click" Visible="false"/> 
            </div>
          
        </div>



        <div class="form-group row" runat="server">
            <div class="col-sm-3 font-weight-bold" runat="server">Select files..</div>
            <div class="col-sm-6" runat="server">
       <asp:FileUpload runat="server" ID="FileUpload1" AllowMultiple="true" Width="100%" Class="h-100 bg-transparent form-control"/>
      
           </div>
            <div class="col-sm-2" runat="server">
                <asp:Button ID="btnUpload" runat="server" Text="Upload " class="btn btn-success mt-1"  OnClick="Upload" OnClientClick="return clientFunctionValidation()" ClientIDMode="Static"/> 
            </div>
          
        </div>
        <hr />


         <div class="form-group row">
        <label for="" class="col-sm-4 h6 font-weight-bold text-info">Previous quotations</label>
              </div>

      
               <div class="form-group row">
                  <div class="col-sm-12 font-weight-bold d-flex justify-content-center align-items-center" runat="server">
                     <%-- <div class="col-sm-2" ></div>--%>
            
       <%--      <div class="col-sm-12 font-weight-bold d-flex justify-content-center align-items-center" runat="server">--%>
                  <div class="table-responsive-sm col-sm-8" >  
          <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="4" CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100" >  <%--OnPageIndexChanging="GridView1_PageIndexChanging"--%>
            <Columns>  
                <asp:BoundField DataField="Q_NAME" HeaderText="File name" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" /> 
                <asp:BoundField DataField="CREATED_ON" HeaderText="Created date" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" />  
                <asp:BoundField DataField="STATUS" HeaderText="Status" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" />  
                
                  

                 <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
   

                     <ItemTemplate><label class="text-primary" runat="server">Download
                                            <asp:ImageButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                                CommandArgument='<%# Eval("Q_NO") %>' ImageUrl="~/Images/viewIconOne.png"></asp:ImageButton>
                                        </label>
                                         </ItemTemplate>
                            </asp:TemplateField>


                              <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
 
                                     <ItemTemplate><label class="text-primary" runat="server">Remove
                                            <asp:ImageButton ID="lnkRemove" runat="server" Text="Remove" OnClick="RemoveFile"
                                                CommandArgument='<%# Eval("Q_NO") %>' ImageUrl="~/Images/delete.png"></asp:ImageButton>
                                        </label>
                                         </ItemTemplate>
                            </asp:TemplateField>


            </Columns>  
        </asp:GridView>  
                      </div>               

                 </div>         
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
          
        <div class="form-group row" runat="server">
            <div class=" col-sm-7" runat="server" id="Div1"> </div>
            <div class="col-sm-2" runat="server"></div>
            <div class="col-sm-2" runat="server">
                <asp:Button ID="btfinished" runat="server" Text="  Finish  " class="btn btn-success"   ClientIDMode="Static" OnClick="btfinished_Click" OnClientClick="return clientFunctionValidationFinished()"/> 
            </div>
          </div>


        </div>
        <hr/>

    </ContentTemplate>
        <Triggers>
         <asp:Asyncpostbacktrigger controlid="ddlStatus" eventname="SelectedIndexChanged" />
            <asp:PostBackTrigger ControlID="btnUpload" />  
            <asp:PostBackTrigger ControlID="GridView1" />
            <asp:PostBackTrigger ControlID="btfinished" />
            <%--<asp:AsyncPostBackTrigger ControlID="lnkDownload" EventName="Click" />--%>
         
       </Triggers>
         </asp:UpdatePanel>

            </div>
        </div>
     </div>

    <div id="coverScreen"  class="LockOn"></div>

    <%--<div class="MessagePanelDiv" >
   <asp:Panel ID="Message" runat="server" CssClass="hidepanel">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
    <asp:Label ID="labelMessage" runat="server" />
   </asp:Panel>
 </div>--%>

    
</asp:Content>

