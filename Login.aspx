<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ Register Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" TagPrefix="ajax" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta http-equiv="X-UA-Compatible" content="IE=edge" runat="server"/>
    <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no, width=device-width" runat="server"/> 
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" runat="server">--%>
    <title>Login to System</title>

    
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
        * { font-family:Calibri; } 

        .bs-example {
            margin: auto;
            /*background-color:#80EBED;*/
        }
            .centerIconPanelMain
       {
                text-align: center;
                /*width: 100%;*/
                font-size: 100px; 
                color: #ffffff;
                /*border:solid #ff0000;*/
       }
 .CardBackgroundMain
     {
         /*background-color:  rgba(15, 95, 73, 0.90);*/ /*rgba(0,172,186, 0.50);*/
         background-color: rgba(255, 255, 255, 0.10);
         /*background-color: #45B39D;*/
     }
     .cardPropMain
     {
        min-height:110px;
        min-width:240px;
        border:0px;
        text-align:center;
        /*border-right: 5px solid rgba(24, 149, 165, 0.83) /*#b2081c*//*#FF2470 rgba(24, 149, 165, 0.83);
        border-radius: 12px;*/
     }
        
         .img_sizeMain
    {
        max-height:360px;
        max-width:360px;
        align-items:center;
    }
         .item_cssSecondMain
      {
          color:#ffd800;
          font-family: Calibri;
          font-size: 18px;
          font-weight:bold;
      }

     .CardBackgroundMain
     {
         /*background-color:  rgba(15, 95, 73, 0.90);*/ /*rgba(0,172,186, 0.50);*/
         background-color:#00adba;
     }
         .testClass
        {
            /*font-family: Courier New, Courier, monospace;*/
            font-size: 18px;
            font-weight: 600;
            color:#ffffff;
        }

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
          /*background-image: url(<%:ResolveUrl("~/Images/cover_slic_logo.jpg")%>);*/
          
      }
            .containerx
            {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  min-height: 400px;
  min-width: 240px;
}
            .DTA_col{
               background-color: #F1C10B;/*ffc000*/
            }
</style>

</head>
<body class="CardBackgroundMain" runat="server">
      <form runat="server" autocomplete="off">

          <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<%-- <div class="border border-info" id="Div2" runat="server">--%>
        <div class="container">

            <div class="mt-5">
                 <div class="row col-sm-12 mt-5"></div>
                <div class="row col-sm-12 mt-5">
                    <div class="col-sm-6 mt-5 border border-bottom-0 border-top-0 border-right border-left-0 border-white">
                        <div class="col-sm-12">
     <img src=<%:ResolveUrl("~/images/slic2.png")%> class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..."/>
               </div>
                        <div class="col-sm-12">
                            <h2 class="text-white font-weight-bold text-center">Bancassurance General Insurance</h2>
                            <%--Login Page--%>
                            <h4 class="text-white font-weight-bold text-center">Online Platform</h4>
                        </div>
                    </div>

                    <div class="col-sm-6 mt-5">
                         <div class="bg-transparent form-group row m-2 mt-3">
                 
               <div class="col-sm-10 testClass" runat="server">
                    <label runat="server" class="font-weight-bold testClass">User Name</label>
                <input type="text" name="" class="form-control" placeholder="Enter user name" required="required" id="urName" runat="server" autocomplete="off"/>
                </div>
                   
              </div>


               <div class="bg-transparent form-group row m-2 mt-3">
                  
                   <div class="col-sm-10 testClass" runat="server">
                       <label runat="server" class="font-weight-bold testClass">Password</label>
                <input type="password" name="" class="form-control" placeholder="Enter Password" required="required" id="passwrd" runat="server" autocomplete="off"/>
                </div>
               
                </div>

                    <div class="bg-transparent form-group row m-2 mt-3">
                        
                   <div class="col-sm-10 testClass" runat="server">
                       <asp:Button runat="server" class="btn btn-outline-info float-left border border-white font-weight-bold text-white" Text="Sign In" OnClick="Unnamed1_Click" ForeColor="White" />
               </div>
                      
                        </div>

                        <%--31012022 developments--%>
        
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="m-2 col-sm-12">
                        <ContentTemplate>
                            <hr/>
<%--<div class="row col-sm-12 mt-4 mb-4">--%>
                            <div class="row mt-4">
         <div class="col-sm-6 testClass" runat="server">
                  <label class="font-weight-bold" runat="server">Immediate Contacts Details</label> 
             </div>
             <div class="col-sm-6 testClass" runat="server">
             <asp:DropDownList ID="ddl_bank" runat="server" class="custom-select text-white text-center border border-white bg-info" ClientIDMode="Static" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_bank_SelectedIndexChanged" AutoPostBack="true" > 
                        <asp:ListItem Value ="0" Selected="True">-- Select --</asp:ListItem>
                        <asp:ListItem Text="BANK OF CEYLON" Value="7010"></asp:ListItem>
                        <asp:ListItem Text="PEOPLE'S BANK" Value="7135"></asp:ListItem>
                        <asp:ListItem Text="REGIONAL DEVELOPMENT BANK" Value="7755"></asp:ListItem>
		        <asp:ListItem Text="NATIONAL SAVINGS BANK" Value="7719"></asp:ListItem>
                        <asp:ListItem Text="COMMERCIAL BANK" Value="7056"></asp:ListItem>
                        <asp:ListItem Text="SAMPATH BANK" Value="7278"></asp:ListItem>
			<asp:ListItem Text="CEYBANK" Value="9999"></asp:ListItem>
			<asp:ListItem Text="PAN ASIA BANKING CORPORATION PLC" Value="7311"></asp:ListItem>
			<asp:ListItem Text="CITIZEN DEVELOPMENT BUSINESS FINANCE PLC" Value="7746"></asp:ListItem>  
                  </asp:DropDownList>
             
              </div>
     <div class="col-sm-1 testClass" runat="server" visible="false">
                    <%-- <asp:Button ID="btn_find" runat="server" Text="Search" ValidationGroup="VG0001" onclick="btn_find_Click1"                                   
                         ClientIDMode="Static" class="btn btn-success testClass mr-1 mt-4 ml-0"/>--%>
                    <asp:Button ID="btn_clear" runat="server" Text="Clear" onclick="btn_clear_Click" ClientIDMode="Static" 
                                  class="btn btn-outline-info border border-white font-weight-bold text-white mt-1 text-center" Visible="false"/>
                 </div>
                                </div>
                             <div class="row mt-2">
     <div class="col-sm-12 testClass" runat="server" id="gridPanel">
          <div class="table-responsive"> 

                            <asp:GridView ID="Grid_Details" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="8"
                                CssClass="table table-striped table-hover font-weight-normal table-borderless w-100 border border-0"
                                OnPageIndexChanging="Grid_Details_PageIndexChanging" ShowHeader="false" ShowFooter="false">

                                <Columns>                            
                              <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-dark testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
                                  <ItemTemplate>
                                      <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>' ></asp:Label>
                                  </ItemTemplate>
                                  <ItemStyle HorizontalAlign="Center" />
                              </asp:TemplateField>

                                <asp:BoundField HeaderText="Officer Name"  DataField="officer_name" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>   
                                <asp:BoundField HeaderText="Contact Number"  DataField="contact_no" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass"/>    
                                <%--<asp:BoundField HeaderText="Email"  DataField="email" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" Visible="false"/>  --%>  

                     </Columns>
                            </asp:GridView>
              <label id="emailVal" runat="server" class="col-sm-12 text-center"></label>
                </div>
     </div>
                                 </div>
                            <hr />
                            <div class="row mt-5">
                                
                                <div class="col-sm-12">
                                <div class="card text-center border border-warning p-0 bg-info">
  <%--<div class="card-header"></div>--%>
  <div class="card-body bg-info p-1 text-white mt-2 mb-2">
    
    <%--<p class="">--%>
        <h5 class="">Click here to obtain <b><u>Online DTA Policies</u></b></h5>
        (Use the same <b>User name</b> & the <b class="font-weight-bold">Password</b> provided for SLIC Online Bancassurance GI system).<%--</p>--%><br /><a href="https://www.srilankainsurance.lk/DTA/" class="btn btn-info stretched-link text-white font-weight-bold border-white mt-1 mb-1">Click Here</a>
  </div>
  <%--<div class="card-footer text-muted"></div>--%>
</div>
                                </div>
                            </div>
<%--</div>--%>
 </ContentTemplate>
                         <Triggers>
                         <asp:AsyncPostBackTrigger ControlID="ddl_bank" EventName="SelectedIndexChanged" />
                         </Triggers>
                         </asp:UpdatePanel>


                    </div>
                </div>

                

            </div>


            </div>
        

      </form>

</body>
</html>