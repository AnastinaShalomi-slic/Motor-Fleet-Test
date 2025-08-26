<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DefaultFire.aspx.cs" Inherits="DefaultFire" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">

    .main22 {
    padding-top: 20px;
    margin-left: 10%;    
    border: solid 0px;
    width: 84%;
    /*background-color: lightgreen;*/
     height:450px;
     font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }


      .newsBox, .underWrite, .officer {
            color: #1A5276;
         } 
   </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

        <div class="form-group row" id="details_officer" runat="server" visible="true">
            <div class="col-sm-6"><label class="col-sm-6 font-weight-bold">Officer Details</label>
                <br />
            <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="PlaceHo1">
         <ItemTemplate>
              <div class="newsBox">
        <strong>Name: </strong>
        <asp:Label runat="server" ID="lblName" Text='<%# Eval("OFFICER_NAME") %>'></asp:Label>
        <br />
        <strong>Designation:</strong>
        <asp:Label runat="server" ID="lblDesig" Text='<%# Eval("DESIGNATION") %>'></asp:Label>
        <br />
        <strong>Contact No: </strong>
        <asp:Label runat="server" ID="lblcont" Text='<%# Eval("CONTACT_NO") %>'></asp:Label>
        <br />
        <strong>Email: </strong>
        <asp:Label runat="server" ID="lblemail" Text='<%# Eval("EMAIL") %>'></asp:Label>
        <br />
        <br />
                  </div>
    </ItemTemplate>
    <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="PlaceHo1"></asp:PlaceHolder>
    </LayoutTemplate>
    </asp:ListView>
                </div>

        <div class="col-sm-6"><label class="col-sm-6 font-weight-bold">Underwrite Officer's Details</label><br />
        <asp:ListView ID="ListView2" runat="server" ItemPlaceholderID="PlaceHo2">
        <ItemTemplate>
             <div class="newsBox">
        <strong>Name: </strong>
        <asp:Label runat="server" ID="lblName" Text='<%# Eval("OFFICER_NAME") %>'></asp:Label>
        <br />
        <strong>Designation:</strong>
        <asp:Label runat="server" ID="lblDesig" Text='<%# Eval("DESIGNATION") %>'></asp:Label>
        <br />
        <strong>Contact No: </strong>
        <asp:Label runat="server" ID="lblcont" Text='<%# Eval("CONTACT_NO") %>'></asp:Label>
        <br />
        <strong>Email: </strong>
        <asp:Label runat="server" ID="lblemail" Text='<%# Eval("EMAIL") %>'></asp:Label>
        <br />
        <br /></div>

     </ItemTemplate>
     <LayoutTemplate>
        <asp:PlaceHolder runat="server" ID="PlaceHo2"></asp:PlaceHolder>
     </LayoutTemplate>
     </asp:ListView>
                </div>
        </div>  


</asp:Content>
