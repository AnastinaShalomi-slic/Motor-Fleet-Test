<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FirstPage.aspx.cs" Inherits="FirstPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="Style_Sheets/firstPage.css" rel="stylesheet" />

    <div class="main" runat="server" style="margin-left:1%; width:100%;"> 
        <p>The page-container goes around everything on the page, and sets its minimum height to 100% of the viewport height (vh). As it is relative, its child elements can be set with absolute position based on it later.</p>
      

                    <table style="width: 100%" >
                                <tr style="background: #e5e5e5">
                                    <td colspan="6" style="text-align: center;font-weight:bold">
                                        Search Categories
                                    </td>                                   
                                </tr>                                
                    </table>
        <br/>
        <br/>
      
           <i class='fas fa-bullhorn' style='font-size: 25px; color: black'></i>
           <i class="fas fa-bell" style='font-size: 25px; color: black'></i>

        </div>

<div class="main2" runat="server" style="margin-left:1%; width:100%;"> 
  <ul class="list">
  <li class="li">
    <span>Sea Walk</span>
      <img class="image" src="Images/Login_cover.jpg" />
  </li>
  
  <li class="li">
    <span>Space</span>
   <img class="image" src="Images/scientific_space.jpg" />
  </li>
  
  <li class="li">
    <span>Galaxy</span>
      <img class="image" src="Images/galaxy_stars.jpg" />
  </li>
  <li class="li">
    <span>Golden Gate</span>
      <img class="image" src="Images/bridge_golden_gate.jpg" />
  </li>
       <li class="li">
    <span>City</span>
    <img class="image" src="Images/skyscrapers.jpg" />
  </li>
  </ul>
    </div>

</asp:Content>

