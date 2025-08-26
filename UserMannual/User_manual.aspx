<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="User_manual.aspx.cs" Inherits="UserMannual_User_manual" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            height:500px;
            width:95%;
            /*padding-left:5%;*/
            margin-left:5%;
        }


    .main22 {
    padding-top: 20px;
    margin-left: 10%;    
    border: solid 0px;
    width: 84%;
    /*background-color: lightgreen;*/
   
        }
    .main223 {
    padding-top: 20px;
    margin-left: 45%;    
    border: 1px solid 0px;
    width: 90%;
    height:480px;
    align-content:center;
    /*background-color: lightgreen;*/
   
        }
  
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server"  style="margin-left: -5%; width: 100%; margin-top:0%;">
         <ContentTemplate>
    <br />
    <div runat="server" id="motorBOC" class="main223">
 <a runat="server" href="./final/MotorUsermanualFinal.pdf" id ="pageTen">        
       <div style="margin-top:10px; color:black; font-weight:600; padding-left:0px; width:120px; margin-left:5px; color:#1A5276">User Manual</div>
       <div class="icon1" runat="server" style="background-image:url(../Images/userManual.png); background-repeat:no-repeat; border:solid 1px #45B39D; height: 100px; width: 100px; margin-top:20px;">           
       </div>
            <div style="margin-top:10px; color:black; font-weight:600; padding-left:0px; width:200px; margin-left:0px; text-align:start; color:#1A5276">Click here to view.</div>
        </a>
    </div>

    <div runat="server" id="fireBOC" class="main223">
       <a runat="server" href="./final/FireOnlineSystemUserManualFinal.pdf" id ="A1">        
       <div style="margin-top:10px; color:black; font-weight:600; padding-left:0px; width:120px; margin-left:5px; color:#1A5276">User Manual</div>
       <div class="icon1" runat="server" style="background-image:url(../Images/userManual.png); background-repeat:no-repeat; border:solid 1px #45B39D; height: 100px; width: 100px; margin-top:20px;">           
       </div>
            <div style="margin-top:10px; color:black; font-weight:600; padding-left:0px; width:200px; margin-left:0px; text-align:start; color:#1A5276">Click here to view.</div>
        </a>
    </div>


    <div runat="server" id="fire_booklet" class="main223">
       
      <a runat="server" href="./final/MergedFirePolicy.pdf" id ="A2">        
       <div style="margin-top:10px; color:black; font-weight:600; padding-left:0px; width:120px; margin-left:5px; color:#1A5276">User Manual</div>
       <div class="icon1" runat="server" style="background-image:url(../Images/userManual.png); background-repeat:no-repeat; border:solid 1px #45B39D; height: 100px; width: 100px; margin-top:20px;">           
       </div>
            <div style="margin-top:10px; color:black; font-weight:600; padding-left:0px; width:200px; margin-left:0px; text-align:start; color:#1A5276">Click here to view.</div>
        </a>
    </div>


        <%--     ///pb documents------%>
    <div runat="server" id="motorPB" class="main223">
       <a runat="server" href="./final/MotorUserManual(PB).pdf" id ="A3">        
       <div style="margin-top:10px; color:black; font-weight:600; padding-left:0px; width:120px; margin-left:5px; color:#1A5276">User Manual</div>
       <div class="icon1" runat="server" style="background-image:url(../Images/userManual.png); background-repeat:no-repeat; border:solid 1px #45B39D; height: 100px; width: 100px; margin-top:20px;">           
       </div>
            <div style="margin-top:10px; color:black; font-weight:600; padding-left:0px; width:200px; margin-left:0px; text-align:start; color:#1A5276">Click here to view.</div>
        </a>
    </div>

    <div runat="server" id="firePB" class="main223">
       <a runat="server" href="./final/FireOnlineSystemUserManual(PB).pdf" id ="A4">        
       <div style="margin-top:10px; color:black; font-weight:600; padding-left:0px; width:120px; margin-left:5px; color:#1A5276">User Manual</div>
       <div class="icon1" runat="server" style="background-image:url(../Images/userManual.png); background-repeat:no-repeat; border:solid 1px #45B39D; height: 100px; width: 100px; margin-top:20px;">           
       </div>
            <div style="margin-top:10px; color:black; font-weight:600; padding-left:0px; width:200px; margin-left:0px; text-align:start; color:#1A5276">Click here to view.</div>
        </a>
    </div>



             </ContentTemplate>
          </asp:UpdatePanel>
</asp:Content>

