<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_OD.master" AutoEventWireup="true" CodeFile="Branchstatistics .aspx.cs" Inherits="OdProtect_Bank_Branchstatistics_" Culture = "en-GB"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .ui-position {
            padding-left: 10px;
        }

        .ui-datepicker {
            font-size: 10pt !important
        }

        .ui-datepicker-trigger {
            margin-left: 3px;
            vertical-align: middle;
            margin-bottom: 3px;
        }

        .hidden {
            display: none;
        }

        .ui_comp_hide {
            display: none
        }

        .ui-position {
            padding-left: 10px;
        }

        .ui-datepicker {
            width: auto;
            height: auto;
            margin: 5px auto 0;
            font: 10pt Arial, sans-serif;
            -webkit-box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, .5);
            -moz-box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, .5);
            box-shadow: 0px 0px 10px 0px rgba(0, 0, 0, .5);
        }

        .ui-datepicker-trigger {
            margin-left: 3px;
            vertical-align: middle;
            margin-bottom: 3px;
        }

        .swal-overlay {
            background-color: rgba(43, 165, 137, 0.45);
        }

        .testClass {
            /*font-family: Courier New, Courier, monospace;*/
            font-size: 13px;
            font-weight: 500;
            color: #000000;
        }

        .testClassHeader {
            /*font-family: Courier New, Courier, monospace;*/
            color: #026888;
            font-size: 14px;
            font-weight: 600;
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

    <script type="text/javascript">
       
         function Message(title, message, icon) {
                swal
                    ({
                        title: title,
                        text: message,
                        icon: icon,
                        button: true,
                        closeOnClickOutside: false,
                    });
            }
    </script>

    <script type="text/javascript">      
        var toDate = new Date().format('dd/MM/yyyy');

        $(function () {
            $("input[id$='txt_start_date']").datepicker({
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                yearRange: '2016:+10',
                dateFormat: 'dd/mm/yy',
                defaultDate: +0,
                numberOfMonths: 1,
                maxDate: toDate,
                showAnim: 'slideDown',
                showButtonPanel: false,
                showWeek: true,
                firstDay: 1,
                stepMonths: 0,
                //showOn: "button",
                buttonImage: "../Images/delete.gif",
                buttonImageOnly: true,
                buttonText: "Select date",
                onSelect: function (date) {
                    var date2 = $("input[id$='txt_start_date']").datepicker('getDate');
                }
            });
        });


        $(function () {
            $("input[id$='txt_to_date']").datepicker({
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                yearRange: '2016:+10',
                dateFormat: 'dd/mm/yy',
                defaultDate: +0,
                numberOfMonths: 1,
                maxDate: toDate,
                showAnim: 'slideDown',
                showButtonPanel: false,
                showWeek: true,
                firstDay: 1,
                stepMonths: 0,
                //showOn: "button",
                buttonImage: "../Images/delete.gif",
                buttonImageOnly: true,
                buttonText: "Select date",
                onSelect: function (date) {
                    var date2 = $("input[id$='txt_end_date']").datepicker('getDate');
                }
            });
        });


        $(function () {
            $('[id*=txt_toDate]').datepicker({
                format: "dd/mm/yyyy",
                language: "tr",
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                yearRange: '2016:+10',
                dateFormat: 'dd/mm/yy',
                defaultDate: +0,
                numberOfMonths: 1,
                maxDate: toDate,
                showAnim: 'slideDown',
                showButtonPanel: false,
                showWeek: false,
                firstDay: 1,
                stepMonths: 0,
                //showOn: "off"
                buttonImage: "../Images/delete.png",
                todayBtn: true,
                todayHighlight: true,
                //buttonImageOnly: true,
                buttonText: "Select date"
            });
        });
    




        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {

                $(function () {
                    $("input[id$='txt_start_date']").datepicker({
                        changeMonth: true,
                        changeYear: true,
                        showOtherMonths: true,
                        yearRange: '2016:+10',
                        dateFormat: 'dd/mm/yy',
                        defaultDate: +0,
                        numberOfMonths: 1,
                        maxDate: toDate,
                        showAnim: 'slideDown',
                        showButtonPanel: false,
                        showWeek: true,
                        firstDay: 1,
                        stepMonths: 0,
                        //showOn: "button",
                        buttonImage: "../Images/delete.gif",
                        buttonImageOnly: true,
                        buttonText: "Select date",
                        onSelect: function (date) {
                            var date2 = $("input[id$='txt_start_date']").datepicker('getDate');
                        }
                    });
                });


                $(function () {
                    $("input[id$='txt_to_date']").datepicker({
                        changeMonth: true,
                        changeYear: true,
                        showOtherMonths: true,
                        yearRange: '2016:+10',
                        dateFormat: 'dd/mm/yy',
                        defaultDate: +0,
                        numberOfMonths: 1,
                        maxDate: toDate,
                        showAnim: 'slideDown',
                        showButtonPanel: false,
                        showWeek: true,
                        firstDay: 1,
                        stepMonths: 0,
                        //showOn: "button",
                        buttonImage: "../Images/delete.gif",
                        buttonImageOnly: true,
                        buttonText: "Select date",
                        onSelect: function (date) {
                            var date2 = $("input[id$='txt_end_date']").datepicker('getDate');
                        }
                    });

                });

                /********************************************************************************/
            }

        });

    </script>

    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"></asp:ToolkitScriptManager>

    <asp:HiddenField ID="bank_code" runat="server" />
    <asp:HiddenField ID="branch_code" runat="server" />
    <asp:HiddenField ID="userName_code" runat="server" />
    <asp:HiddenField ID="bancass_email" runat="server" />
    <asp:HiddenField ID="auth_Code" runat="server" />

    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
    <ContentTemplate>

    <div class="" id="viewPropId" runat="server">
        <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-center">Branch Progress Statistics</label>
        </div>

        <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-left">Search Criteria</label>
        </div>

        <div class="form-group row" runat="server">
            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">From</label>
                <asp:TextBox ID="txt_start_date" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" ValidationGroup="VG0001" placeholder="From Date" autocomplete="off"></asp:TextBox>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">To</label>
                <asp:TextBox ID="txt_to_date" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" ValidationGroup="VG0001" placeholder="To Date" autocomplete="off"></asp:TextBox>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">Status</label>
                <asp:DropDownList ID="ddl_status" runat="server" class="custom-select testClass text-center"
                    ClientIDMode="Static" AppendDataBoundItems="true">
                    <asp:ListItem Text="--All--" Value="A"></asp:ListItem>
                    <asp:ListItem Value="C" Selected="True">Completed</asp:ListItem>
                    <asp:ListItem Value="P">Pending</asp:ListItem>
                </asp:DropDownList>
            </div>


            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">Bank</label>
                <asp:DropDownList ID="ddl_bank" runat="server" class="custom-select testClass text-center" AppendDataBoundItems="true">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">Branch</label>
                <asp:DropDownList ID="ddl_branch" runat="server" class="custom-select testClass text-center" AppendDataBoundItems="true">
                    <asp:ListItem Value="0">-- Select --</asp:ListItem>
                </asp:DropDownList>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <asp:Button ID="btn_find" runat="server" Text="Search"
                    ValidationGroup="VG0001" OnClick="btn_find_Click1"
                    ClientIDMode="Static" class="btn btn-success testClass mr-1 mt-4 ml-0" />

                <asp:Button ID="btn_clear" runat="server"
                    Text="Clear" OnClick="btn_clear_Click" ClientIDMode="Static"
                    class="btn btn-success testClass mr-1 mt-4 ml-1" />

                <asp:ImageButton ID="ImageButton8" runat="server" ImageAlign="Middle"
                    ImageUrl="~/Images/icons8-downloading-updates-20.png"
                    ToolTip="Click here to download as excel file" class="btn btn-success testClass mr-1 mt-4 ml-1 img-fluid" OnClick="btexcel_Click"/>
            </div>
        </div>

        <div class="form-group row mb-2" runat="server">
            <div class="col-sm-2 testClass" runat="server">
                <asp:CompareValidator ID="CompareValidator1" ValidationGroup="VG0001" ForeColor="Red" runat="server"
                    ControlToValidate="txt_start_date" ControlToCompare="txt_to_date" Operator="LessThanEqual" Type="Date"
                    ErrorMessage="Start date must be less than End date." class="font-weight-bold testClass mt-4"></asp:CompareValidator>
            </div>
            <div class="col-sm-10 mr-0 testClass" runat="server">
            </div>
        </div>


        <div class="form-group row" runat="server">
            <div class="col-md-12">
                <div class="table-responsive">
                    <asp:GridView ID="Grid_Details" Visible="true" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="8"
                        CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100"
                        OnPageIndexChanging="Grid_Details_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Bank" DataField="BBNAM" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                            <asp:BoundField HeaderText="Branch" DataField="BBRNCH" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                            <asp:BoundField HeaderText="Count" DataField="NOFPOL" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                            <asp:BoundField HeaderText="Entered Date" DataField="ENTDATE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                            <asp:BoundField HeaderText="Total Sum Insu." DataField="SUMINS" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>

        <div class="form-group row" runat="server">
            <div class="col-md-6">
                <div class="table-responsive">
                            <asp:GridView ID="GridViewTotalCount" Visible="true" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="8"
                                CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Overall Count" DataField="NOFPOL" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Overall Sum Insu." DataField="SUMINS" DataFormatString="{0:n2}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                </Columns>
                            </asp:GridView>
                </div>
            </div>
        </div>
    </div>
    </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="ImageButton8" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>




