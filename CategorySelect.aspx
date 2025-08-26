<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategorySelect.aspx.cs" Inherits="CategorySelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
     <script src="../JavaScripts/Font_Awsome.js"></script>
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>
    <title>Category Select Page</title>
    <style type="text/css">
   .radioButtonList input[type="radio"] 
   {
     width: 22px; 
     height:22px;
     float: left; 
     margin-left: 200px;
     margin-top:0px;
     
    border-radius: 15px;
    display: inline-block;
    visibility: visible;
    border: 2px solid white;
  /*box-sizing: unset;*/
   

   }

.radioButtonList label {      
   width: 45%;      
   display: inline-block;   
   float: right;      
   font-size: large; 
   color: #283850 !important;      
   font-style: normal;  
   padding-left:10px;
   margin-left:50px;
   margin-top:-25px;
   padding-bottom:5px;
   padding-top:5px;
   margin-bottom:5px;
   font-weight:700;
   background-color: cadetblue;
   border-radius:50px;
 
}
 body {
    margin: 5%;
    padding: 0;
    /*background-image: url('../Images/001_SLIC.jpg');*/
    /*background: url('Images/cover_slic_logo_third.jpg') no-repeat;*/
    /*background-size: contain;*/
    /*background-repeat:no-repeat;*/
    /*background-position: top,right;*/ 
    background-color: #E3F0F0;
    background-size:auto;
    font-family: sans-serif;
    /*height: auto;*/
    /*overflow :hidden;*/
}

 .footer {
    position: fixed;
    bottom: 0%;
    width: 100%;
    vertical-align: bottom;
    text-align: center;
    color: #283850;
    font-size: 16px;
    font-family: Oxygen, Arial, Helvetica, sans-serif;
    width: 100%;

}

    .footer a {
        color: cadetblue;
    }


    </style>
</head>
<body>
    <form id="form1" runat="server">
       <div id="main" runat="server">

            <fieldset runat="server" id="field1" style="width:98%;  border: 1px solid #4d414a;" >
                <legend style="color:#4d414a; border-radius: 10px; border-color:#000;">Select an Option</legend>
        <table>
        
            <tr>
                <td>
                <asp:RadioButtonList ID="rblSelectMenu" runat="server" AutoPostBack="true" 
                        onselectedindexchanged="rblSelectMenu_SelectedIndexChanged" CssClass="radioButtonList"
                        RepeatDirection="Vertical">
                        <asp:ListItem runat="server">&nbsp;Motor Insurance</asp:ListItem>
                        <asp:ListItem runat="server">&nbsp;Fire Insurance</asp:ListItem>
                       
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
                </fieldset>
    </div>
         <div>
     <footer class="footer">
       <%--     <a href="#">--%>
            <i class='far fa-copyright' style='font-size: 20px; color: black; margin-left:1%;'></i>&nbsp;Copy Right Reserved. SLIC <%=DateTime.Now.Year%> Version 1.0.0<%--</a>--%>

        </footer>
        </div>
    </form>
</body>
</html>
