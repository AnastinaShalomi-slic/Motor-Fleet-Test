<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FleetQuot.aspx.cs" Inherits="FleetQuot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-4">
            <div class="card shadow-lg rounded-3">
                <div class="card-header bg-primary text-white">
                    <h5 class="mb-0">Vehicle & Policy Details</h5>
                </div>
                <div class="card-body">
                    <div class="row mb-3">
                        <div class="col-md-4">
                            <label for="ddlVehicleType" class="form-label font-weight-bold">Vehicle Type</label>
                            <asp:DropDownList ID="ddlVehicleType" runat="server" CssClass="form-control text-center" AppendDataBoundItems="true" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlPolType_SelectedIndexChanged">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <br />
                        <br />

                        <div class="col-md-4">
                            <label for="ddlPolType" class="form-label font-weight-bold">Policy Type</label>
                            <asp:DropDownList ID="ddlPolType" runat="server" CssClass="form-control text-center" AutoPostBack="true" AppendDataBoundItems="true">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <br />
                        <br />

                        <div class="col-md-4">
                            <label for="ddlSubCat" class="form-label font-weight-bold">Sub Category</label>
                            <asp:DropDownList ID="ddlSubCat" runat="server" CssClass="form-control text-center" AppendDataBoundItems="true" AutoPostBack="true">
                                <asp:ListItem Value="0">-- Select --</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                <br />
                <br />
                <asp:GridView ID="gvCovers" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped"
                    HeaderStyle-CssClass="thead-dark" ShowHeaderWhenEmpty="True">
                    <Columns>

                        <asp:BoundField DataField="cover" HeaderText="Cover Name" ReadOnly="True" />


                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="No. of Passengers">
                            <ItemTemplate>
                                <asp:TextBox ID="txtPassengers" runat="server" CssClass="form-control text-center" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Sum Insured">
                            <ItemTemplate>
                                <asp:TextBox ID="txtSumInsured" runat="server" CssClass="form-control text-center" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="TR">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkTR" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="RCC">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkRCC" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


                <div class="form-group row" runat="server">
                    <div class="col-sm-4 offset-sm-1">
                        <asp:Button ID="btn_find" runat="server" Text="Cal PAB Drver cov" Style="background-color: #008B80;" CssClass="btn btn-success" OnClick="btn_PAB_DRV_Cal"  />
                        
                    </div>
                </div>

                <br />
                <asp:Label ID="pabRate" runat="server"></asp:Label>
            </div>
        </div>
    </form>

</body>
</html>
