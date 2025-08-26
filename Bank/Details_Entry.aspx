<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Details_Entry.aspx.cs" Inherits="Bank_Details_Entry" Culture="en-GB" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


    <style type="text/css">
        .main22 {
            padding-top: 20px;
            margin-left: 10%;
            border: solid 0px;
            width: 84%;
        }

        .settxt {
            background-position: 99% 10%;
            padding-left: 6px;
            border: 1px solid #b3b3b3;
            height: 25px;
            width: 282px;
            border-radius: 0px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .setdrop {
            background-position: 99% 10%;
            padding-left: 6px;
            border: 1px solid #b3b3b3;
            height: 25px;
            width: 290px;
            border-radius: 0px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

        .btn_clz {
            background-color: #45B39D;
            color: #fff;
            font-size: 12px;
            width: 150px;
            font-weight: bold;
            height: 30px;
            border: 0px;
            border-radius: 0px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

            .btn_clz:hover {
                background-color: #5bc0de;
                color: #000;
            }

        .auto-style2 {
            width: 430px;
        }


        .bt_other {
            background-color: #45B39D;
            color: #fff;
            font-size: 11px;
            width: 45px;
            font-weight: bold;
            height: 24px;
            border: 0px;
            margin-top: 0px;
            border-radius: 0px;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }

            .bt_other:hover {
                background-color: #5bc0de;
                color: #000;
            }

        td {
            color: #1A5276;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
    </style>



    <script type="text/javascript">
    function Comma(Num) { 
        Num += '';
        Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
        Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
        x = Num.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1))
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
      return x1 + x2;
    }
    </script>

    <script>
    
    function isNumber(evt) {
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;

        return true;
        }    

    </script>

   </asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"></asp:ToolkitScriptManager>
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />


    <div class="container" id="mainDiv" runat="server">

        <div class="form-group row" runat="server" id="rowFirst">
            <div class="col-sm-12 text-center h5 font-weight-bold">Proposal Details</div>
        </div>

        <div class="form-group row" runat="server" id="Div1">

            <div class="container border border-info">
                <%--first container--%>
                <div class="form-group row" runat="server" id="Div3">
                    <div class="col-sm-12 text-center h6 font-weight-bold mt-2">Mandatory Details</div>
                </div>
                <div class="form-group row" runat="server" id="Div2">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Vehicle Type</div>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddl_vType" runat="server" CssClass="custom-select text-capitalize"
                            ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false">
                            <asp:ListItem Selected="True" Text="--Select--" Value="ALL"></asp:ListItem>
                            <asp:ListItem Text="BUS" Value="Bus"></asp:ListItem>
                            <asp:ListItem Text="BICYCLE" Value="Bicycle"></asp:ListItem>
                            <asp:ListItem Text="CAR" Value="Car"></asp:ListItem>
                            <asp:ListItem Text="VAN" Value="Van"></asp:ListItem>
                            <asp:ListItem Text="JEEP" Value="Jeep"></asp:ListItem>
                            <asp:ListItem Text="SUV" Value="SUV"></asp:ListItem>
                            <asp:ListItem Text="DOUBLE CAB" Value="Double Cab"></asp:ListItem>
                            <asp:ListItem Text="LORRY" Value="Lorry"></asp:ListItem>
                            <asp:ListItem Text="PRIME MOVER" Value="Prime Mover"></asp:ListItem>
                            <asp:ListItem Text="TRACTOR" Value="Tractor"></asp:ListItem>
                            <asp:ListItem Text="THREE WHEELER" Value="Three Wheeler"></asp:ListItem>

                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2">
                        <asp:RequiredFieldValidator ID="req_Validator0" runat="server" ErrorMessage="Please Select a Type" ControlToValidate="ddl_vType"
                            ValidationGroup="VG01" InitialValue="ALL" CssClass="text-danger font-weight-normal"></asp:RequiredFieldValidator>

                    </div>
                </div>
                <div class="form-group row" runat="server" id="Div4">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Year of Manufacture</div>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="yom" runat="server" AutoPostBack="false" CssClass="custom-select text-capitalize"
                            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                            <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2">
                        <asp:RequiredFieldValidator ID="req_Validator1" runat="server" ErrorMessage="Please Select Year" ControlToValidate="yom"
                            ValidationGroup="VG01" InitialValue="0" CssClass="text-danger font-weight-normal"></asp:RequiredFieldValidator>

                    </div>
                </div>

                <div class="form-group row" runat="server" id="Div5">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Sum Insured (LKR)</div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="sumInsu" runat="server" CssClass="form-control" ClientIDMode="Static" onkeypress="javascript:return isNumber(event);" onkeyup="javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox>
                    </div>

                    <div class="col-sm-2">
                        <asp:RequiredFieldValidator ID="req_Validator2" runat="server" ControlToValidate="sumInsu" ValidationGroup="VG01" CssClass="text-danger font-weight-normal">
                                             <img src="../Images/required.png" align="middle" alt=""/>Field Required!</asp:RequiredFieldValidator>

                    </div>
                </div>


                <div class="form-group row" runat="server" id="Div6">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Make of Vehicle</div>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddl_make" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_make_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Value="0">-- Select --</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2">
                        <asp:RequiredFieldValidator ID="req_Validator3" runat="server" ErrorMessage="Please Select a Make" ControlToValidate="ddl_make"
                            ValidationGroup="VG01" InitialValue="0" CssClass="text-danger font-weight-normal"></asp:RequiredFieldValidator>
                    </div>

                </div>

                <div class="form-group row" runat="server" id="Div7">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Model of Vehicle</div>
                    <div class="col-sm-4">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_model" runat="server" ClientIDMode="Static" AppendDataBoundItems="true" CssClass="custom-select text-capitalize">
                                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txt_other" runat="server" CssClass="form-control" ClientIDMode="Static" Visible="False" ToolTip="Type Model Here..!"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_make" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-2">

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="bt_other" runat="server" Text="Other" CssClass="btn btn-info" OnClick="bt_other_Click" ToolTip="For new Model..!" />
                                <asp:Button ID="bt_cnOther" runat="server" Text="Cancel" CssClass="btn btn-info" ToolTip="To hide field..!" OnClick="bt_cnOther_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>

                <div class="form-group row" runat="server" id="Div8">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Purpose</div>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddl_purpose" runat="server" CssClass="custom-select text-capitalize"
                            ClientIDMode="Static" AppendDataBoundItems="true">
                            <asp:ListItem Selected="True" Text="--Select--" Value="All"></asp:ListItem>
                            <asp:ListItem Text="Private Use" Value="Private"></asp:ListItem>
                            <asp:ListItem Text="Rent" Value="Rent"></asp:ListItem>
                            <asp:ListItem Text="Hiring" Value="Hiring"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-2">

                        <asp:RequiredFieldValidator ID="req_Validator5" runat="server" ErrorMessage="Please Select a Purpose" ControlToValidate="ddl_purpose"
                            ValidationGroup="VG01" InitialValue="All" CssClass="text-danger font-weight-normal"></asp:RequiredFieldValidator>

                    </div>

                </div>

                <div class="form-group row" runat="server" id="Div9">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Customer Contact Number</div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="cusNo" runat="server" CssClass="form-control" ClientIDMode="Static" onkeypress="javascript:return isNumber(event);" autocomplete="off"></asp:TextBox>

                    </div>
                    <div class="col-sm-2">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cusNo" ValidationGroup="VG01" CssClass="text-danger font-weight-normal">
                                             <img src="../Images/required.png" align="middle" alt=""/>Field Required!</asp:RequiredFieldValidator>

                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                            ControlToValidate="cusNo" ErrorMessage="Phone Number Not Valid.!"
                            ValidationExpression="[0-9]{10}" CssClass="text-danger font-weight-normal" ValidationGroup="VG01"></asp:RegularExpressionValidator>

                    </div>

                </div>



            </div>
        </div>
        <%--second--%>
        <div class="form-group row" runat="server" id="Div10">

            <div class="container border border-info">
                <%--first container--%>
                <div class="form-group row" runat="server" id="Div11">
                    <div class="col-sm-12 text-center h6 font-weight-bold mt-2">Optional Details</div>
                </div>

                <div class="form-group row" runat="server" id="Div12">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Vehicle Registration Number</div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="regNo" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox></div>
                </div>

                <div class="form-group row" runat="server" id="Div13">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Customer Name</div>
                    <div class="col-sm-1 p-0 ml-3 mr-3">
                        <asp:DropDownList ID="ddlInitials" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false">
                            <asp:ListItem Value="">Status</asp:ListItem>
                            <asp:ListItem Value="Dr. ">Dr</asp:ListItem>
                            <asp:ListItem Value="Mr. ">Mr</asp:ListItem>
                            <asp:ListItem Value="Mrs. ">Mrs</asp:ListItem>
                            <asp:ListItem Value="Miss. ">Miss</asp:ListItem>
                            <asp:ListItem Value="Ms. ">Ms</asp:ListItem>
                            <asp:ListItem Value="Rev. ">Rev</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="cusName" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group row" runat="server" id="Div14">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Fuel Type</div>
                    <div class="col-sm-4">
                        <asp:DropDownList ID="ddl_fuel" runat="server" CssClass="custom-select text-capitalize"
                            ClientIDMode="Static" AppendDataBoundItems="true">
                            <asp:ListItem Selected="True" Text="--Select--" Value="A"></asp:ListItem>
                            <asp:ListItem Text="Petrol" Value="Petrol"></asp:ListItem>
                            <asp:ListItem Text="Diesel" Value="Diesel"></asp:ListItem>
                            <asp:ListItem Text="Hybrid" Value="Hybrid"></asp:ListItem>
                            <asp:ListItem Text="Electrical" Value="Electrical"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-group row" runat="server" id="Div15">
                    <div class="col-sm-2"></div>
                    <div class="col-sm-3">Customer Email Address</div>
                    <div class="col-sm-4">
                        <asp:TextBox ID="emailId" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
                    </div>
                </div>
                <%--TextMode="Email"--%>
            </div>
        </div>
        <div class="form-group row" runat="server" id="Div16">
            <div class="col-sm-2"></div>
            <div class="col-sm-3"></div>
            <div class="col-sm-4">
                <asp:Button ID="btn_send" runat="server" Text="Send"
                    ValidationGroup="VG01"
                    ClientIDMode="Static" CssClass="btn btn-success m-1" OnClick="btn_send_Click" />
                <asp:Button ID="btn_clear" runat="server" Text="Clear"
                    ValidationGroup="VG0001"
                    ClientIDMode="Static" CssClass="btn btn-success m-1" OnClick="btn_clear_Click" />
            </div>
        </div>
    </div>

</asp:Content>

