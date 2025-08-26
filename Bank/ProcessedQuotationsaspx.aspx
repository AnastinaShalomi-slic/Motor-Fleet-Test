<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProcessedQuotationsaspx.aspx.cs" Inherits="Bank_ProcessedQuotationsaspx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function custom_alert(message, title) {
            if (!title)
                title = 'Alert';

            if (!message)
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
            else if (title == 'Success') {
                swal
                    ({
                        title: title,
                        text: message,
                        icon: "success",
                        closeOnClickOutside: false,
                        button: false,
                    });
            }
            else if (title == 'Info') {
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



            custom_alert('Response send to SLIC..', 'Success');

        }
    </script>

    <style type="text/css">
        .swal-overlay {
            background-color: rgba(43, 165, 137, 0.45);
        }

        .testClass {
            font-size: 12px;
            font-weight: 600;
            color: rgb(0, 0, 0);
            text-align: center;
        }

        .testClassItem {
            font-size: 12px;
            font-weight: 600;
            color: rgb(0, 0, 0);
            text-align: left;
        }

        .testClassHeader {
            font-size: 13px;
            font-weight: 700;
            text-align: center;
        }

        .long-textbox {
            max-height: 40px;
            margin-top: 5px;
            margin-bottom: 50px;
        }

        .form-group {
            margin-bottom: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />
    <asp:HiddenField ID="tempDID" runat="server" />
    <asp:HiddenField ID="tempPID" runat="server" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"></asp:ToolkitScriptManager>
    <div class="container" id="QuoViewDetailsBurglary" runat="server">

        <div class="col-sm-12">
            <label id="statusView1" class="col-sm-12 h4 font-weight-bold text-black bg-light h-auto d-flex justify-content-center align-items-center" runat="server"></label>
            <label id="statusView2" class="col-sm-12 h4 font-weight-bold text-black bg-light h-auto d-flex justify-content-center align-items-center" runat="server"></label>
            <label id="statusView3" class="col-sm-12 h4 font-weight-bold text-black bg-light h-auto d-flex justify-content-center align-items-center" runat="server"></label>
            <label id="SlicRemarks" class="col-sm-12 h6 font-weight-bold table-info bg-light h-auto text-info d-flex justify-content-center align-items-center" runat="server"></label>

        </div>

        <div class="form-group row" runat="server" visible="false" id="rejectDiv">
            <label for="" class="col-sm-2 font-weight-bolder">Reject reason :</label>
            <label for="" class="col-sm-10 font-weight-normal text-black text-justify" id="statusId" runat="server"></label>
        </div>
        <div class="form-group row border border-info p-1 bg-info">
            <label for="" class="col-sm-3 font-weight-bolder">SLIC previous remarks</label><%--<br /> (First one is the latest.)--%>
            <label for="" class="col-sm-9 font-weight-normal text-white text-justify h5" id="txtSlicRemarks" runat="server"></label>
        </div>
        <div class="form-group row border border-info p-1 bg-info">
            <label for="" class="col-sm-3 font-weight-bolder">Bank previous remarks</label><%--<br /> (First one is the latest.)--%>
            <label for="" class="col-sm-9 font-weight-normal text-white text-justify h5" id="txtBankRemarks" runat="server"></label>
        </div>
        <hr />
        <%--edit button sectiion--%>
        <div class="form-group row" id="bankResp" runat="server">
            <label for="" class="col-sm-2 font-weight-bolder">Bank remarks</label>
            <div class="col-sm-7" runat="server">
                <textarea type="text" class="form-control" id="txtBankRe" placeholder="Remarks" runat="server" rows="5"></textarea>
                <label class="col-sm-2 text-danger" runat="server" id="validation2" style="display: none">*Required</label>
            </div>
            <div class="col-sm-2">
                <asp:Button type="button" class="btn btn-success" runat="server" Text="Reply"
                    OnClientClick="clientFunctionValidationGrid()" ClientIDMode="Static" ID="btEdit" OnClick="btEdit_Click" />
            </div>
        </div>
        <%-- end edit--%>

        <div class="form-group row" runat="server">
            <div class="col-sm-12 text-center font-weight-bold">Request info.</div>
        </div>
        <hr />
        <div class="form-group row" runat="server">

            <div class="col-sm-2">Request ID</div>
            <div class="col-sm-4">
                <asp:TextBox ID="Req_id" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-2">Purpose</div>
            <div class="col-sm-4">
                <asp:TextBox ID="txtpurpose" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>

        </div>
        <div class="form-group row" runat="server">

            <div class="col-sm-2">Vehicle type</div>
            <div class="col-sm-4">
                <asp:TextBox ID="v_type" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-2">Registratiion number</div>
            <div class="col-sm-4">
                <asp:TextBox ID="txtV_reg" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>

        </div>
        <div class="form-group row" runat="server">

            <div class="col-sm-2">Year of manufature</div>
            <div class="col-sm-4">
                <asp:TextBox ID="yom" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-2">Customer name</div>
            <div class="col-sm-4">
                <asp:TextBox ID="txtcusname" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>


        </div>
        <div class="form-group row" runat="server">

            <div class="col-sm-2">Sum insured</div>
            <div class="col-sm-4">
                <asp:TextBox ID="txtsumInsu" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-2">Customer contact name</div>
            <div class="col-sm-4">
                <asp:TextBox ID="txtCusCon" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>

        </div>
        <div class="form-group row" runat="server">

            <div class="col-sm-2">Make of vehicle</div>
            <div class="col-sm-4">
                <asp:TextBox ID="txtMake" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-2">Fuel type</div>
            <div class="col-sm-4">
                <asp:TextBox ID="txtfuel" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>

        </div>
        <div class="form-group row" runat="server">

            <div class="col-sm-2">Model of vehicle</div>
            <div class="col-sm-4">
                <asp:TextBox ID="txtmodel" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>
            <div class="col-sm-2">Customer email</div>
            <div class="col-sm-4">
                <asp:TextBox ID="txtcusEmail" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox></div>

        </div>

        <hr />
        <div class="form-group row" runat="server">
            <div class="col-sm-12 text-center font-weight-bold">Attached quotation(s) from SLIC</div>
        </div>
        <hr />
        <div class="form-group row">
            <div class="col-sm-2"></div>
            <div class="col-sm-8">
                <div class="font-weight-bold" runat="server">
                    <div class="table-responsive-sm">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="4" CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100" OnPageIndexChanging="GridView1_PageIndexChanging">
                            <Columns>
                                <asp:BoundField DataField="Q_NAME" HeaderText="File name" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClassItem" />
                                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Attached quotations" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <label class="text-primary stretched-link" runat="server">
                                            Download&nbsp;
                                            <asp:ImageButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                                CommandArgument='<%# Eval("Q_NO") %>' ImageUrl="~/Images/icons8-downloading-updates-20.png"></asp:ImageButton>
                                        </label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-sm-2"></div>
        </div>
        <hr />
    </div>
</asp:Content>
