<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
   <script language="javascript">
function printdiv(printpage)
{
var headstr = "<html><head><title></title></head><body>";
var footstr = "</body>";
var newstr = document.all.item(printpage).innerHTML;
var oldstr = document.body.innerHTML;
document.body.innerHTML = headstr+newstr+footstr;
window.print();
document.body.innerHTML = oldstr;
return false;
}
</script>

    

    <script type="text/javascript">

     function custom_alert(message, title)
        {
                if ( !title )
                    title = 'Alert';

                if ( !message )
                     message = 'No Message to Display.';


            if (title == 'Alert') {

                swal
                    ({
                        title: title,
                        text: message,
                        icon: "warning",
                        button: true,
                        closeOnClickOutside: false,
                    });
            }
            else if (title == 'Success')
            {
                 swal
                    ({
                        title: title,
                        text: message,
                        icon: "success",
                         button: false,
                        closeOnClickOutside: false,
                    });

            }

        }

        function clientFunctionValidation()
        {
   
             custom_alert('Processing.. wait..', 'Success');
             return true;
        }
        </script>
     <style type="text/css">
   .swal-overlay
   {
            background-color: rgba(43, 165, 137, 0.45);
   }
   .testClass {
            /*font-family: Courier New, Courier, monospace;*/
            font-size: 12px;
            font-weight: 600;
        }

        .testClassHeader {
            /*font-family: Courier New, Courier, monospace;*/
            font-size: 13px;
            font-weight: 700;
        }

        .long-textbox {
            max-height: 40px;
            margin-top: 5px;
            margin-bottom: 50px;
        }
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />
     <asp:HiddenField ID="tempRefNo" runat="server" />
     <asp:HiddenField ID="tempDID" runat="server" />
    <asp:HiddenField ID="tempPID" runat="server" />
    <asp:HiddenField ID="tempFlag" runat="server" />
    <asp:HiddenField ID="tempBranchCode" runat="server" />
    <asp:HiddenField ID="tempUserBranch" runat="server" />
     <asp:HiddenField ID="tempUserType" runat="server" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>

      <div class="container" id="QuoViewDetailsHome" runat="server" >

        <div class="form-group row bg-transparent">
  
        <div class="col-sm-1 w-25 justify-content-end d-flex">
                <img src="../../Images/iconViewInfo.png" runat="server"  class="img-responsive" height="30" width="30"/>
        </div>
        <div class="col-sm-8">
        <label for="" class="h6 font-weight-bold text-danger" runat="server" id="txtTime"></label>
        <label for="" class="h6 font-weight-bold text-success" runat="server" id="txtTimeSlic"></label>
        </div>
        <div class="col-sm-2">
            <%--<asp:Button ID="Button1" runat="server"  Text="Print" class="btn btn-primary" OnClientClick="javascript:window.print();" />--%>
            <input name="b_print" type="button" class="btn btn-primary" onClick="printdiv('div_print');" value=" Print ">
        </div>
        <%-- <div id="div_print"><p>Test Page</p></div>--%>
    </div>
          <br/> <div id="div_print" class="container">
          <div class="form-group row">
        <label for="CusDetails" class="col-sm-4 h6 font-weight-bold text-info">Customer Details</label>
              </div>
          <hr />
    <div class="form-group row">
        <label for="ReferenceID" class="col-sm-4 h6 font-weight-bold">1. Reference ID</label>
        <label Id="ReferenceID" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>
           
    <div class="form-group row">
        <label for="cusName" class="col-sm-4 h6 font-weight-bold">2. Customer Name</label>
        <label Id="cusName" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>

           <div class="form-group row">
        <label for="cusAdd" class="col-sm-4 h6 font-weight-bold">3. Customer Address</label>
        <label Id="cusAdd" class="col-sm-4 h6 font-weight-normal" runat="server"></label>

    </div>

   <div class="form-group row">
        <label for="cusPhone" class="col-sm-4 h6 font-weight-bold">4. Customer Contact No.</label>
        <label Id="cusPhone" class="col-sm-4 h6 font-weight-normal" runat="server"></label>      
    </div>
            
   <div class="form-group row">
        <label for="emailId" class="col-sm-4 h6 font-weight-bold">5. Email</label>
        <label Id="emailId" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>

            <div class="form-group row">
        <label for="cusNIC" class="col-sm-4 h6 font-weight-bold">6. Customer NIC</label>
        <label Id="cusNIC" class="col-sm-4 h6 font-weight-normal" runat="server"></label>    
          </div>
          <div class="form-group row">
         <label for="cusBR" class="col-sm-4 h6 font-weight-bold">7. BR Number</label>
        <label Id="cusBR" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>

          <div class="form-group row">
        <label for="otherId" class="col-sm-4 h6 font-weight-bold">8. Other Insurance SLIC</label>
        <label Id="otherId" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>
          <div class="form-group row">
        <label for="InterId" class="col-sm-4 h6 font-weight-bold">9. Intermediary Code</label>
        <label Id="InterId" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>
            
           
            <hr/>


            <div class="form-group row">
        <label for="depId" class="col-sm-4 h6 font-weight-bold">10. Department Name</label>
        <label Id="depId" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>
             <div class="form-group row">
        <label for="pID" class="col-sm-4 h6 font-weight-bold">11. Product Name</label>
        <label Id="pID" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>

            <div class="form-group row">
        <label for="enteredDate" class="col-sm-4 h6 font-weight-bold">12. Entered Date</label>
        <label Id="enteredDate" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>

          <hr />

             <div class="form-group row">
        <label for="locaId" class="col-sm-4 h6 font-weight-bold">13. Location</label>
        <label Id="locaId" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>


    <div class="form-group row">
        <label for="lblsumInsu" class="col-sm-4 h6 font-weight-bold">14. Sum Insured</label>
        <label Id="lblfullVal" class="col-sm-2 h6 font-weight-bold text-right ml-4" runat="server">Full value(LKR)</label>
     
    </div>
             
     <div class="form-group row">
        <label for="txtglassVal" class="col-sm-4 h6  ml-4">&#8544;. Glass Value</label>
        <label Id="txtglassVal" class="col-sm-2 h6 font-weight-normal text-right" runat="server"></label>

    </div>

    <div class="form-group row"> 
        <label for="txtFitting" class="col-sm-4 h6  ml-4">&#8545;. Fitting Charges</label>
        <label Id="txtFitting" class="col-sm-2 h6 font-weight-normal text-right" runat="server"></label>
      
    </div>

  <div class="form-group row">
        <label for="txtTransport" class="col-sm-4 h6  ml-4">&#8546;. Transport</label>
        <label Id="txtTransport" class="col-sm-2 h6 font-weight-normal text-right" runat="server"></label>
    </div>



        <%--  -------additional covers--------------->>>>>>>>>--%>
           <div class="form-group row">
        <label for="lblAddditionalTemp" class="col-sm-6 h6 font-weight-bold">15. Additional Covers(If not covered under fire policy)</label>
        <label Id="lblAddditional" class="col-sm-3 h6 font-weight-bold text-right" runat="server"></label>
     
    </div>
             
     <div class="form-group row">
        <label for="txtSrcc" class="col-sm-4 h6 ml-4">&#8544;. SRCC</label>
        <label Id="txtSrcc" class="col-sm-2 h6 font-weight-normal text-right" runat="server"></label>
    </div>

    <div class="form-group row"> 
        <label for="txtTC" class="col-sm-4 h6 ml-4">&#8545;. TC</label>
        <label Id="txtTC" class="col-sm-2 h6 font-weight-normal text-right" runat="server"></label>     
    </div>
          

   <div class="form-group row">
        <label for="claimExpTemp" class="col-sm-4 h6 font-weight-bold">16. Last 3 years claims experience</label>
        <label Id="claimExp" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>

   <div class="form-group row" runat="server" id="clamNoRaw">
        <label for="noOfClaim" class="col-sm-4 h6 font-weight-bold"></label>
        <label for="noOfClaim" class="col-sm-2 h6 font-weight-normal" runat="server">No. of claims</label>
        <label Id="noOfClaim" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>

     <div class="form-group row" runat="server" id="clamValRaw">
        <label for="valueOfCliam" class="col-sm-4 h6 font-weight-bold"></label>
        <label for="valueOfCliam" class="col-sm-2 h6 font-weight-normal" runat="server">Value of claims</label>
        <label Id="valueOfCliam" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>


     <div class="form-group row">
        <label for="txtFirePol" class="col-sm-4 h6 font-weight-bold">17. Is the building insured under Fire Policy</label>
        <label Id="txtFirePol" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>
   <div class="form-group row">
        <label for="txtOccupation" class="col-sm-4 h6 font-weight-bold">18. Occupation</label>
        <label Id="txtOccupation" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>
    <div class="form-group row">
        <label for="spInfo" class="col-sm-4 h6 font-weight-bold">19. Special Infomations</label>
        <label Id="spInfo" class="col-sm-4 h6 font-weight-normal" runat="server"></label>
    </div>
          <hr />

           <div class="form-group row">
        <label for="" class="col-sm-4 h6 font-weight-bold text-info">Attached Documents</label>
              </div>
              <div class="form-group row col-sm-12">
                  <div class="col-sm-12 font-weight-bold d-flex justify-content-center align-items-center" runat="server">
                  <div class="table-responsive-sm" >  
       <%--   <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="4" CssClass="table table-striped table-light table-hover font-weight-normal table-bordered w-100" OnPageIndexChanging="GridView1_PageIndexChanging">  
            <Columns>  
                <asp:BoundField DataField="F_NAME" HeaderText="File Name" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass"/>  
                <asp:TemplateField ItemStyle-HorizontalAlign="Center" HeaderText="Attached Documents" HeaderStyle-CssClass="text-black-100 testClassHeader" ItemStyle-CssClass="text-black-100 testClass">  
                    <ItemTemplate>  
                        <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"  
                            CommandArgument='<%# Eval("F_NO") %>'></asp:LinkButton>  
                    </ItemTemplate>  
                </asp:TemplateField>  
            </Columns>  
        </asp:GridView> --%> 
                      </div>
                      </div>
       
    </div>
          <hr />
              </div>

           <div class="form-group row" runat="server">
       
        <div class="col-sm-8" runat="server">
             <asp:Button type="Submit" class="btn btn-success" runat="server" Text="Next" ID="btnProceed" 
                 OnClientClick="return clientFunctionValidation()" ClientIDMode="Static"/>
           
        </div>   
         
    </div>
           <hr />
     </div>
</asp:Content>

