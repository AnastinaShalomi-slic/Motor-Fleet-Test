<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ViewQuotations.aspx.cs" Inherits="Bank_ViewQuotations" Culture="en-GB" %>

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
            font-size: 13px;
            font-weight: 500;
            color: #000000;
        }

        .testClassHeader {
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

        /*---------------------------auto complete ----------*/


        $(function () {
            $("[id$=txt_req_id]").autocomplete({

                source: function (request, response) {

                    $.ajax({
                        url: '<%=ResolveUrl("~/Bank/ViewQuotations.aspx/GET_REQ_ID") %>',
                        data: "{ 'prefix': '" + request.term + "'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",

                        success: function (data) {

                            response($.map(data.d, function (item) {
                                return {

                                    label: item.split('-')[0],
                                    val: item.split('-')[1]


                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                select: function (e, i) {
                    //$("[id$=hfCustomerId]").val(i.item.val);
                },
                minLength: 1

            });
        });


        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

            function EndRequestHandler(sender, args) {

                $(function () {
                    $("[id$=txt_req_id]").autocomplete({

                        source: function (request, response) {

                            $.ajax({

                                url: '<%=ResolveUrl("~/Bank/ViewQuotations.aspx/GET_REQ_ID") %>',
                                data: "{ 'prefix': '" + request.term + "'}",
                                dataType: "json",
                                type: "POST",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {

                                    response($.map(data.d, function (item) {
                                        return {
                                            label: item.split('-')[0],
                                            val: item.split('-')[1]

                                        }
                                    }))
                                },
                                error: function (response) {
                                    alert(response.responseText);
                                },
                                failure: function (response) {
                                    alert(response.responseText);
                                }
                            });
                        },
                        select: function (e, i) {
                            //$("[id$=hfCustomerId]").val(i.item.val);
                        },
                        minLength: 1

                    });

                });

                /********************************************************************************/
            }

        });




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

                    //set date2 = date1 selected date         
                    // $("input[id$='txt_podate']").datepicker('setDate', date2);
                }
            });
        });


        $(function () {
            $("input[id$='txt_end_date']").datepicker({
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

                            //set date2 = date1 selected date         
                            //$("input[id$='txt_podate']").datepicker('setDate', date2);

                        }
                    });
                });


                $(function () {
                    $("input[id$='txt_end_date']").datepicker({
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
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />


    <div class="" id="viewPropId" runat="server">
        <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-center">Get Motor Quotations</label>
        </div>
        <div class="form-group row" runat="server">
            <label for="" class="col-sm-12 font-weight-bolder h5 text-left">Search Categories</label>
        </div>
        <div class="form-group row" runat="server">
            <div class="col-sm-1 testClass" runat="server"></div>
            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">From</label>
                <asp:TextBox ID="txt_start_date" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" ValidationGroup="VG0001" placeholder="From Date" autocomplete="off"></asp:TextBox>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">To</label>
                <asp:TextBox ID="txt_end_date" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" ValidationGroup="VG0001" placeholder="To Date" autocomplete="off"></asp:TextBox>
            </div>


            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">Req ID</label>
                <asp:TextBox ID="txt_req_id" runat="server" CssClass="form-control text-center testClass" ClientIDMode="Static" placeholder="Req ID" autocomplete="off"></asp:TextBox>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <label class="font-weight-bold testClass" runat="server">Status</label>

                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddl_status" runat="server" class="custom-select testClass text-center"
                            ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="true">
                            <asp:ListItem Selected="True" Text="--All--" Value="A"></asp:ListItem>
                            <asp:ListItem Value="D">Completed</asp:ListItem>
                            <asp:ListItem Value="P">Pending</asp:ListItem>
                            <asp:ListItem Value="M">More Informations</asp:ListItem>
                            <asp:ListItem Value="C">Cancelled</asp:ListItem>
                            <asp:ListItem Value="R">Rejected</asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_status" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>

            <div class="col-sm-2 mr-0 testClass" runat="server">
                <asp:Button ID="btn_find" runat="server" Text="Search" ValidationGroup="VG0001" OnClick="btn_find_Click1"
                    ClientIDMode="Static" class="btn btn-success testClass mr-1 mt-4 ml-0" />
                <asp:Button ID="btn_clear" runat="server" Text="Clear" OnClick="btn_clear_Click" ClientIDMode="Static"
                    class="btn btn-success testClass mr-1 mt-4 ml-1" />
            </div>

        </div>
        <div class="form-group row" runat="server">
            <asp:CompareValidator ID="comp_val" ValidationGroup="VG0001" ForeColor="Red" runat="server"
                ControlToValidate="txt_start_date" ControlToCompare="txt_end_date" Operator="LessThanEqual" Type="Date"
                ErrorMessage="Start date must be less than End date." class="font-weight-bold testClass"></asp:CompareValidator>
        </div>



        <div class="form-group row" runat="server">


            <div class="col-md-12">
                <div class="table-responsive">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>

                            <asp:GridView ID="Grid_Details" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="8"
                                CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100"
                                OnPageIndexChanging="Grid_Details_PageIndexChanging" OnRowDataBound="OnRowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-CssClass="text-dark testClassHeader" ItemStyle-CssClass="text-black-100 testClass">
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_index" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Refrence No" ItemStyle-HorizontalAlign="Center" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRefNo" runat="server" Text='<%# Bind("QUO_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:BoundField HeaderText="Req. ID" DataField="REQ_ID" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Vehicle Type" DataField="V_TYPE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Year of Manu." DataField="YOM" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Sum Insured" DataField="SUM_INSU" DataFormatString="{0:00,000.00}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Make" DataField="vh_make" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Model" DataField="model_name" DataFormatString="{0:00,000.00}" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Purpose" DataField="PURPOSE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Entered Date" DataField="ENTERED_ON" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Status" DataField="FLAG" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Quotation No." DataField="QUO_NO" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField HeaderText="V. Reg. No." DataField="V_REG_NO" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Customer Name" DataField="CUS_NAME" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />
                                    <asp:BoundField HeaderText="Cus. Contact No." DataField="CUS_PHONE" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass" />

                                    <asp:BoundField HeaderText="BANK_CODE" DataField="BANK_CODE" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                                    <asp:BoundField HeaderText="ENTERED_BY" DataField="ENTERED_BY" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />
                                    <asp:BoundField HeaderText="V_FUEL" DataField="V_FUEL" ItemStyle-CssClass="ui_comp_hide" HeaderStyle-CssClass="ui_comp_hide" />


                                    <asp:TemplateField HeaderText="View Quotation" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-CssClass="text-black-100 testClass">
                                        <ItemTemplate>


                                            <asp:ImageButton ID="imgbtn_select" runat="server" CommandArgument='<%#Eval("REQ_ID")+";"+ Eval("FLAG")+";"+ Eval("BANK_CODE")%>' CausesValidation="false"
                                                CommandName="cmd" ImageUrl="~/Images/icons8-next.png" OnClick="SelectRecord_Click" CssClass="img-fluid img_sizeIcon" />

                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>

                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

