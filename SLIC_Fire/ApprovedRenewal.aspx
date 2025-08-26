<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.master" CodeFile="ApprovedRenewal.aspx.cs" Inherits="SLIC_Fire_ApprovedRenewal" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .form-group {
            margin-bottom: 15px;
        }

        .table th {
            background-color: #026888;
            color: white;
            text-align: center;
        }

        .table td {
            text-align: center;
        }

        .btn {
            min-width: 100px;
        }

        .section-title {
            font-size: 20px;
            font-weight: bold;
            text-align: center;
            margin-bottom: 15px;
            color: #026888;
        }

        .datepicker-trigger {
            margin-left: 5px;
            vertical-align: middle;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script>
        $(function () {
            $('#<%= txt_start_date.ClientID %>, #<%= txt_to_date.ClientID %>').datepicker({
                changeMonth: true,
                changeYear: true,
                showOtherMonths: true,
                yearRange: '2016:+10',
                dateFormat: 'dd/mm/yy'
            });
        });

        function toggleSearchFields() {
            var policyType = $("#ddl_polType").val();
            $("#ddlstatus_class, #ddlPropType_class").hide();
            if (policyType === "B") {
                $("#dd_bank, #dd_branch").show();
            } else {
                $("#dd_bank, #dd_branch").hide();
            }
        }

        $(document).ready(function () {
            toggleSearchFields();
        });
    </script>

    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />

    <!-- Header Section -->
    <div class="section-title">Renewed Policies</div>

    <!-- Search Fields -->
    <div class="container">
        <div class="row">
            <div class="col-md-3">
                <label class="font-weight-bold">From Date</label>
                <asp:TextBox ID="txt_start_date" runat="server" CssClass="form-control text-center" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label class="font-weight-bold">To Date</label>
                <asp:TextBox ID="txt_to_date" runat="server" CssClass="form-control text-center" placeholder="DD/MM/YYYY" autocomplete="off"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label class="font-weight-bold">Policy Number</label>
                <asp:TextBox ID="txt_pol_no" runat="server" CssClass="form-control text-center" placeholder="Policy No."></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label class="font-weight-bold">NIC Number</label>
                <asp:TextBox ID="txtNicNo" runat="server" CssClass="form-control text-center text-uppercase" placeholder="NIC" autocomplete="off"></asp:TextBox>
            </div>
        </div>

        <div class="row mt-3">
            <div class="col-md-4">
                <asp:Button ID="btn_find" runat="server" Text="Search" CssClass="btn btn-success mr-2" OnClick="btn_find_Click" />
                <asp:Button ID="btn_clear" runat="server" Text="Clear" CssClass="btn btn-secondary" OnClick="btn_clear_Click" />
            </div>
        </div>
    </div>

    <!-- Horizontal Line -->
    <hr class="bg-info my-4" />

    <!-- GridView Section -->
<div class="container">
    <div id="renewGridViewSection" runat="server">
        <div class="table-responsive" style="max-height: 400px; overflow-y: auto;">
            <asp:GridView ID="Grid_Details" runat="server" AutoGenerateColumns="false" PageSize="10" 
                CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="Dept" DataField="RNDEPT" />
                    <asp:BoundField HeaderText="Type" DataField="RNPTP" />
                    <asp:BoundField HeaderText="Policy No." DataField="RNPOL" />
                    <asp:BoundField HeaderText="Year" DataField="RNYR" />
                    <asp:BoundField HeaderText="Month" DataField="RNMNTH" />
                    <asp:BoundField HeaderText="Start Date" DataField="RNSTDT" />
                    <asp:BoundField HeaderText="End Date" DataField="RNENDT" />
                    <asp:BoundField HeaderText="Agent Code" DataField="RNAGCD" />
                    <asp:BoundField HeaderText="Net Premium" DataField="RNNET" />
                    <asp:BoundField HeaderText="RCC" DataField="RNRCC" />
                    <asp:BoundField HeaderText="TC" DataField="RNTC" />
                    <asp:BoundField HeaderText="Policy Fee" DataField="RNPOLFEE" />
                    <asp:BoundField HeaderText="VAT" DataField="RNVAT" />
                    <asp:BoundField HeaderText="NBT" DataField="RNNBT" />
                    <asp:BoundField HeaderText="Total" DataField="RNTOT" />
                    <asp:BoundField HeaderText="Customer Name" DataField="RNNAM" />
                    <asp:BoundField HeaderText="Address 1" DataField="RNADD1" />
                    <asp:BoundField HeaderText="Address 2" DataField="RNADD2" />
                    <asp:BoundField HeaderText="Address 3" DataField="RNADD3" />
                    <asp:BoundField HeaderText="Address 4" DataField="RNADD4" />
                    <asp:BoundField HeaderText="NIC" DataField="RNNIC" />
                    <asp:BoundField HeaderText="Contact" DataField="RNCNT" />
                    <asp:BoundField HeaderText="Reference No." DataField="RNREF" />
                    <asp:BoundField HeaderText="FBR" DataField="RNFBR" />
                    <asp:BoundField HeaderText="Admin Fee" DataField="RN_ADMINFEE" />
                    <asp:BoundField HeaderText="Entry Date" DataField="RNDATE" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</div>

    </div>
</asp:Content>

