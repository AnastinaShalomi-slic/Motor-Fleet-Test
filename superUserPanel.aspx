<%@ Page Language="C#" AutoEventWireup="true" CodeFile="superUserPanel.aspx.cs" Inherits="superUserPanel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       <meta http-equiv="X-UA-Compatible" content="IE=edge" runat="server"/>
    <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no, width=device-width" runat="server"/> 
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" runat="server">--%>
    <title>Super User</title>

    
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="Content/bootstrap-grid.css" rel="stylesheet" />
 

    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-3.5.1.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/popper.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/bootstrap.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/sweetCSS/sweetalert.min.js")%>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/bootstrap-datepicker.min.js") %>"></script>
   

    <script src="<%= Page.ResolveUrl("~/JavaScripts/Font_Awsome.js")%>"></script>
    <link href="Auto_compl_Js/jquery-ui.css" rel="stylesheet" />
    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
    </script>
     <style type="text/css">
        .backGround
      {   background-color:#00adba;/*#45B39D;*//*#037680;#026888*/
          /*background-color:rgba(69, 179, 157,1);*/  /*#45B39D*/
          /*background-image:url(../IconPack/BackgroundImage.png);*/
          /*background-image:url(../Images/Health1.jpg);*/
          /*background-color:#edebeb;*/
          background-size:cover;
          background-repeat:no-repeat;
          background-position: 90% 10%;
          background-attachment: fixed;
          background-image: url(<%:ResolveUrl("~/Images/superUser.jpg")%>);
          
      }
         </style>
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
                        closeOnClickOutside: true,
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
                         timer: 3000,
                         closeOnClickOutside: true,
                    });
            }

         }
         function clientFunction()
         {
              custom_alert( 'Waiting.', 'Success' );
         }
</script>
</head>
<body class="CardBackgroundMain backGround" runat="server"> 
    <form runat="server" class="m-4">
        <asp:HiddenField ID="userName" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
       <%-- <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>--%>
          <div class="form-group row mt-5" runat="server" id="Div2">

             <div class="col-sm-3" runat="server"></div>
             <div class="col-sm-6 text-center" runat="server">
                 <label runat="server" id="user_name" class="text-center text-capitalize text-white font-weight-bold">Test Bank - Branch</label>
             </div>
             <div class="col-sm-3 text-center" runat="server">
                 <asp:Button runat="server" id="signOutIdImg" src="~/Images/signOut.png" Text="Log Out" href="Login.aspx" ClientIDMode="Static"
                                     OnClick="signOutIdImg_Click" CssClass="btn btn-outline-light text-capitalize"/>
                             
             </div>
         </div> <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="m-0">
                        <ContentTemplate>
       <div class="jumbotron pt-5 border border-light bg-transparent" runat="server"> 
           <div class="container" runat="server">
               <div class="form-group row mt-0" runat="server" >
                    <label class="col-sm-12 font-weight-normal text-center text-white" runat="server" visible="false">Select the bank and branch you want to process</label>
               </div>
            <div class="form-group row" runat="server">
                <div class="col-sm-2"></div>
                <div class="col-sm-1">
                    <label class="font-weight-bold text-white" runat="server">Bank</label>
                </div>
                <div class="col-sm-3">
                  
                     <asp:DropDownList ID="ddl_bank" runat="server" class="custom-select text-black text-center" ClientIDMode="Static" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_bank_SelectedIndexChanged" AutoPostBack="true" > 
                        <asp:ListItem Value ="0">-- Select --</asp:ListItem>
                  </asp:DropDownList>
                           
                </div>
                <div class="col-sm-1"> <label class="font-weight-bold text-white" runat="server">Branch</label></div>
                <div class="col-sm-3">
                   
                  <asp:DropDownList ID="ddl_branch" runat="server" class="custom-select text-black text-center" ClientIDMode="Static" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_branch_SelectedIndexChanged" AutoPostBack="true"  EnableViewState="true">
                        <asp:ListItem Value ="0">-- Select --</asp:ListItem>
                  </asp:DropDownList>                                  
                         
                </div>
                <div class="col-sm-2"></div>
                </div>


              
                

           <div id="info" runat="server" class="bg-transparent text-white col-sm-12">
                <div class="form-group row mb-0 pt-1"><div class="col-sm-4"></div>
                    <div class="col-sm-2 text-left">
                        <label class="font-weight-bold testClass h6 text-center" runat="server" id="Label1">Bank Name</label></div>
                    <%--<div class="col-sm-1">:</div>--%>
                    <div class="col-sm-4 text-left">
                    <label class="font-weight-bold testClass h6 text-center" runat="server" id="d1">Bank</label>
                </div><div class="col-sm-2"></div>
                    </div>
               <div class="form-group row mb-0"><div class="col-sm-4"></div>
                   <div class="col-sm-2 text-left">
                        <label class="font-weight-bold testClass h6 text-center" runat="server" id="Label2">Bank Code</label></div>
                   <%--<div class="col-sm-1">:</div>--%>
                    <div class="col-sm-4 text-left">
                    <label class="font-weight-bold testClass h6 text-center" runat="server" id="d2">Bank</label>
                </div><div class="col-sm-2"></div>
                   </div>
               <div class="form-group row mb-0"><div class="col-sm-4"></div>
                   <div class="col-sm-2 text-left">
                        <label class="font-weight-bold testClass h6 text-center" runat="server" id="Label3">Branch Name</label></div>
                   <%--<div class="col-sm-1">:</div>--%>
                    <div class="col-sm-4 text-left">
                    <label class="font-weight-bold testClass h6" runat="server" id="d3">Bank</label>
                </div>
                   <div class="col-sm-2"></div>
                   </div>
               <div class="form-group row mb-0"><div class="col-sm-4"></div>
                   <div class="col-sm-2 text-left">
                        <label class="font-weight-bold testClass h6 text-center" runat="server" id="Label4">Branch Code</label></div>
                   <%--<div class="col-sm-1">:</div>--%>
                   <div class="col-sm-4 text-left">
                    <label class="font-weight-bold testClass h6 text-center" runat="server" id="d4">Bank</label>
                </div>
                   <div class="col-sm-2"></div>
                </div>
            </div>

             <div id="btnPanel" runat="server">
                <div class="form-group row">
             <div class="col-sm-2 testClass" runat="server">

                 <asp:Button ID="btn_find" runat="server" Text="Next" 
                                 ValidationGroup="VG0001" onclick="btn_find_Click1"                                   
                                  ClientIDMode="Static" class="btn btn-outline-light mr-1 mt-4 ml-0" /><%--OnClientClick="clientFunction()"--%>

                 <asp:Button ID="btn_clear" runat="server"
                                 Text="Clear" onclick="btn_clear_Click"  ClientIDMode="Static" 
                                  class="btn btn-outline-light mr-1 mt-4 ml-1"/>
                 </div>
                </div>
                    </div>

               <div class="bg-danger text-white" runat="server" visible="false"><label id="lblOut" runat="server" class="text-white"></label></div>
        </div>
           </div>
                </ContentTemplate>
                         <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="ddl_bank" EventName="SelectedIndexChanged" />
                         </Triggers>
                         </asp:UpdatePanel>

        <footer class="bg-transparent text-center text-lg-center footer text-white font-weight-normal fixed-bottom" runat="server"> 
    &copy; Copy Right Reserved. SLIC <%=DateTime.Now.Year%> Version 1.0.0
 
  
  
</footer>
    </form>
</body>
</html>
