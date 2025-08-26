<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CancelConfirmation.aspx.cs" Inherits="SLIC_Fire_CancelConfirmation" %>
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
                        button: true,
                        closeOnClickOutside: false,
                    });
            }
            else if (title == 'Success')
            {
                 swal
                    ({
                        title: title,
                        text: message,
                         icon: "success",
                         button: false,
                        closeOnClickOutside: false,
                    });
            }

        }


    function clientFunction() 

         {

        var isValid = true;
        var next = "Y";
 
        var remarks = $('#<%=txtRemarks.ClientID %>').val();


        if (remarks == "")
        {
        
            $('#<%=lblremarkVal.ClientID%>').html("Please Enter Remarks."); 
            custom_alert( 'Please Enter Remarks.', 'Alert' );
            return false;
        }

             custom_alert('Success! Proposal Cancelled.', 'Success'); 
             return true;
       }
        
      
</script>
    
  

     <script type="text/javascript">

        /*---------------------------auto complete ----------*/

   
           $(function () {
      $('#<%= txtRemarks.ClientID%>').on('change keyup paste', function () {

           $('#<%= lblremarkVal.ClientID%>').text('');
       
       
     });
            });


      

    $(document).ready(function () {

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {

           $(function () {
      $('#<%= txtRemarks.ClientID%>').on('change keyup paste', function () {
        
        $('#<%= lblremarkVal.ClientID%>').text('');
       
     });
            });

            /********************************************************************************/
        }

    });

   

</script>

    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="hfRefId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />
   

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>
    <asp:TextBox ID="txtresultcount" runat="server" Visible="false"></asp:TextBox>
     <div class="container border border-info p-2" id="viewPropId" runat="server">
        <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-center">Proposal Cancellation</label>
        </div>
         <div class="form-group row" runat="server">
              <div class="col-sm-3 mr-0 h5" runat="server">
               <label class="font-weight-bold h5" runat="server"> Reference Number:</label>  
               </div>
             <div class="col-sm-4 mr-0 h5 text-left" runat="server">
                 <label class="font-weight-bold" runat="server" ID="txtRef"></label>  
             </div>
         </div>
          <div class="form-group row" runat="server">
              <div class="col-sm-3 mr-0" runat="server">
               <label class="font-weight-bold" runat="server">Reasons for Cancellation:<label class="text-danger">*</label></label>  
               </div>
             <div class="col-sm-4 mr-0" runat="server">
                 <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" placeholder="Remark" Rows="5" TextMode="MultiLine" autocomplete="off"> </asp:TextBox>
             </div>

              <div class="col-sm-1 mr-0" runat="server">
                 <asp:Label ID="lblremarkVal" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
             </div>

              <div class="col-sm-3 mr-0" runat="server">
                <asp:Button ID="btnProceed" runat="server"  Text="Apply Changes" class="btn btn-success testClass mr-1 mt-4 ml-0" OnClientClick="return clientFunction()"  ClientIDMode="Static" OnClick="btnProceed_Click" />
                <asp:Button ID="btnClear" runat="server"  Text="Clear" OnClientClick="this.value='Please wait...'" class="btn btn-success testClass mr-1 mt-4 ml-0" OnClick="btnClear_Click" />                                        
                                    
             </div>
         </div>
         </div>

</asp:Content>

