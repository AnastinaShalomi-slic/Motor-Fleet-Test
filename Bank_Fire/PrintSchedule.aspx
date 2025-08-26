<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" async="true" CodeFile="PrintSchedule.aspx.cs" Inherits="Bank_Fire_PrintSchedule" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   

<style type="text/css">       
   .fieldset_1{ margin-top:10px; height:520px; border-color:#b3b3b3;} 
   .importGrid_Header{height:25px; font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size:10pt; background-color:#fff; color:#000000; white-space:nowrap;}
   .importGrid_Row{height:20px; font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size:10pt; color:#000000; white-space:normal;}
   .importGrid_Footer{height:25px; font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif; font-size:11pt; background-color:#fff; color:#000000; font-weight: bold;}
   .wordrap {white-space: nowrap;}
   
   .ui-position{padding-left: 10px;}  
   .ui-datepicker { font-size:10pt !important}
   .ui-datepicker-trigger{margin-left:3px; vertical-align:middle; margin-bottom:3px;}
      
   .gridfont{font-size:12px;}   
   .hidden{ display:none; }
    
        
    
.GridviewScrollHeader TH, .GridviewScrollHeader TD
    {
        padding: 4px;        
        font-weight: 600;
        white-space:nowrap;
        border-right: 1px solid #AAAAAA;
        border-bottom: 1px solid #AAAAAA;
        background-color: #fff;
        text-align: center;
        vertical-align: bottom;
        font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }
    .GridviewScrollItem TD
    {
        padding: 4px;
        white-space: nowrap;
        border-right: 1px solid #AAAAAA;
        border-bottom: 1px solid #AAAAAA;
        background-color: #FFFFFF;
        font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }
    .GridviewScrollPager 
    {
        border-top: 1px solid #AAAAAA;
        background-color: #FFFFFF;
    }
    .GridviewScrollPager TD
    {
        padding-top: 3px;
        font-size: 12px;
        padding-left: 5px;
        padding-right: 5px;
    }
    .GridviewScrollPager A{color: #666666;}
    .GridviewScrollPager SPAN{ font-size: 14px; font-weight: bold; }
   
   .rowheight{ height:28px;}
    
    .ui_txt_border{ border : 1px solid  #a6a6a6; height:20px;}
    
    .ui_comp_hide{ display:none}
    
    
    .LabelClass 
    {
      display: inline-block;
      float: left;
      width: 194px;
      padding-top:5px;
      color:black;/*#355681;*/
      font-weight:bold;
      font-size:small;
      font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
     }
        
     .div_dec
     {
      padding-top:20px;
      padding-left:20px;
      padding-bottom:10px;
      width:100%;
     } 
     
     .div_dec_3
     {
      padding-top:20px;
      padding-left:20px;
      padding-bottom:10px;
      height:159px; 
      }  
      
     .div_attach
     {
     float:left;
     padding-left:10px;
     height: 161px;
     border-style:hidden;
     border-color:Gray;
     width:380px; 
     /*display:inline-block;*/
    
        
    }
     .div_two
     {
      float:left;
      width:50%; 
      margin-left:0px;
     }
     
     .div_three
     {
      float:left;
      width:50%;
      margin-top:-110px;
    }
     .remark_tag{
      float:left;
      width:50%;
      margin-top:10px;
      margin-left:200px;
     }
     .div_btn
     {
      padding-top:50px;
      padding-left:20px;
      padding-bottom:10px;
     }
     .btImgClass
     {
       margin-left:5px;                  
     }
     .btImgClass2
     {
       margin-left:-80%;                  
     }
     
     .labelpdf
     {
       margin-left:150px; 
       color:#000000;    
       font-weight:bold;
       font-size:small;   
      font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
     }
     
     
     .txt_box
     {
       text-align:right;   
     width:50%;  
       
font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
 font-size      :  16px;
 color          :  #000000;
 padding        :  3px;
 /*background     :  #f0f0f0;*/
 border-left    :  solid 0px #c1c1c1;
 border-top     :  solid 0px #cfcfcf;
 border-right   :  solid 0px #cfcfcf;
 border-bottom  :  solid 0px #6f6f6f;
 border-radius:8px;

     }
     
     .txt_box3
     {
         text-align:right;
     width:50%;    
 font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
 font-size      :  16px;
 color          :  #000000;
 padding        :  3px;
 /*background     :  #f0f0f0;*/
 border-left    :  solid 0px #c1c1c1;
 border-top     :  solid 0px #cfcfcf;
 border-right   :  solid 0px #cfcfcf;
 border-bottom  :  solid 0px #6f6f6f;   
 border-radius:8px;
         
    }
    
    .remark
    {   
      display: inline-block;
      float: left;
      margin-top:5px;
      /*margin-left:60px;*/
      /*padding-top:10px;*/
      border : 1px solid #b3b3b3;
      border-radius:8px;
      /*color:#355681;*/
      /*text-align:center;*/   
   
      
font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
 font-size      :  12px;
 color          :  #000000;
 padding        :  3px;
 background     :  #f0f0f0;
 border-left    :  solid 1px #c1c1c1;
 border-top     :  solid 1px #cfcfcf;
 border-right   :  solid 1px #cfcfcf;
border-bottom  :  solid 1px #6f6f6f;
      
    }
        
        
        
     .remarkLabel 
    {
      display: inline-block;
      float: left;
      width: 98px;
      padding-top:10px;
      margin-top:30px;
      margin-left:17px;
      color:black;/*#355681;*/
      font-weight:bold;
      font-size:small;
     font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
 
    }
     
    
    
      .btn_clz{
            background-color:#e3c353; /*#355681*/ /*#476b6b; #006666;*/
            color: #000000;
            font-size: 12px;
            width: 200px;
            font-weight: bold;
            height:30px;
            border:0px;
            /*background-color: cadetblue;*/
            border-radius:100px;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
        
        .btn_clz_2
        {
            background-color:#e3c353; /*#355681*/ /*#476b6b; #006666;*/
            color: #000000;
            font-size: 10px;
            width: 150px;
            font-weight: bold;
            height:30px;
            border:0px;
            /*background-color: cadetblue;*/
            border-radius:100px;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }
        
        
       .btn_clz:hover
        {
            background-color: #b3e6ff;
            color: #000;
        }
    
    
     .btn_clz_msg{
           background-color:#e3c353; /*#355681*/ /*#476b6b; #006666;*/
            color: #000000;
            font-size: 12px;
            width: 100px;
            font-weight: bold;
            height:30px;
            border:0px;
            /*background-color: cadetblue;*/
            border-radius:100px;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            
        }
       .btn_clz_msg:hover
        {
            background-color: #b3e6ff;
            color: #000;
        }


    .body_div 
         {
        background-color:#E2E9EE;
        width:100%;
        
         }

    .lblDocURL {
        color:black;
    }
    
      .hidepanel {
            display: none;
        }

        .MessagePanelDiv {
            position: fixed;
            left: 35%;
            top: 45%;
            width: 35%;
        }

        .pendingTimeclz{
            font-size:12px; 
            color:#fa5f5f;/*#fa9393;*/ 
            padding-right:30px;
            font-family:Arial, Helvetica, sans-serif;
            opacity: 1.0;
            /*display:none;*/
            /*text-shadow: 0px 0px 30px white;*/
  
        }

   

    .divLegent{ width:100%;}
  
    #tablemiddle td, tr {
       border: solid .5px #5e4f4e;
       
   }
    #tablemiddle {
        border-collapse: collapse;
        /*width:100%;*/
    }

    .auto-style3 {
        height: 21px;
    }

    .swal-overlay {
  background-color: rgba(43, 165, 137, 0.45);
       }

    td
    {
        /*color:#2f414d;*/
        color:#1A5276;
        font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }
    .form-group {
  margin-bottom: 5px;
}
    .labelxx {
    display:inline-block;
    text-align:right;
    width:30%;
    padding-right:12px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


     <script type="text/javascript">

     function Alert() {

     //$("#btn_find").click(function () {
          //swal("Hello world!");
                swal("Done!", "Request in process.!", "success", {
             button: false,
         });
               
   

        }



         function AlertDownload() 
      {
             swal("Done!", "Excel File in process.!", "success", {
                 button: false,
                 //timer: 3000,
            });
                     
      }
 </script>



     <script type="text/javascript">



        /*---------------------------auto complete ----------*/


        $(function () {
            $("[id$=quotRefNo]").autocomplete({

                source: function (request, response) {

                    $.ajax({
                        url: '<%=ResolveUrl("ViewPageSecond.aspx/Get_Ref_No") %>',
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
                    $("[id$=quotRefNo]").autocomplete({

                        source: function (request, response) {

                            $.ajax({

                                url: '<%=ResolveUrl("~/ViewPageSecond.aspx/Get_Ref_No") %>',
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





    function confirmFun(id,mgs)
    {
		
	var buttonText = document.getElementById(id);

            if (confirm(mgs)==true)
            {
                //buttonText.value = 'Please wait...';
                return true;
            }
			else
			{
				return false;
			}
            
        };

          $(document).ready(function () {
            window.setTimeout(function () {
                $(".alert").fadeTo(1000, 0).slideUp(0, function () {
                    $(this).remove();
                    //$(".alert2").show();
                    //$("#pendingTime").delay(100).fadeIn("fast");
                    
                });
            }, 3000);
        });     

        $(document).ready(function() {
           $('.pendingTimeclz').delay(1600).fadeIn(2200);
        });   
  </script>   
    


  <script type="text/javascript">

            function disableselect(e){

            return false

            }

            function reEnable(){

            return true

            }

                //if IE4+

        document.onselectstart=new Function ("return false")

        //if NS6

        if (window.sidebar){

            document.onmousedown=disableselect

            document.onclick=reEnable

        }

</script>

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>
<asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="Less_text" runat="server" />
    <asp:HiddenField ID="Reson_temp" runat="server" />
    <asp:HiddenField ID="app_date" runat="server" />
    <asp:HiddenField ID="reqDate" runat="server" /> 
    <asp:HiddenField ID="HiddenFieldRemark" runat="server" />
     <asp:HiddenField ID="bank_code" runat="server" />
    <asp:HiddenField ID="statusHidden" runat="server" />
    <asp:HiddenField ID="user_epf" runat="server" />
    <asp:HiddenField ID="req_status" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />
    <asp:HiddenField ID="hfRefId" runat="server" />
    <asp:HiddenField ID="hfSumInsu" runat="server" />
    <asp:HiddenField ID="hfFlag" runat="server" />



    <div class="container border border-info" id="mainDiv" runat="server">



       <div class="form-group row mt-1" runat="server" id="Div2">
        <div class="col-sm-4"></div>
        <div class="col-sm-4"></div>
        <div class="col-sm-4">
            <asp:Button ID="btPayAdvance" runat="server" Text="Print Payment Advice" CssClass="btn btn-success m-1" OnClick="btPayAdvance_Click" OnClientClick="Alert()" ></asp:Button>
            <asp:Button ID="btPDF" runat="server" Text="Print Policy Schedule" CssClass="btn btn-success m-1" OnClick="btPDF_Click" OnClientClick="Alert()" ></asp:Button>
            <asp:Button ID="btPropType" runat="server" Text="Back to Proposal Forms" CssClass="btn btn-info m-1" OnClick="btCat_Click" OnClientClick="Alert()" ></asp:Button>
        <asp:Button ID="btdebit" runat="server" Text="View Debit Note" CssClass="btn btn-info m-1" OnClick="btdebit_Click" OnClientClick="Alert()" Visible="false"></asp:Button>
        <asp:HiddenField ID="hfDebitNo" runat="server" />
        <asp:HiddenField ID="hfPaymentDate" runat="server" />
             <asp:HiddenField ID="hfBANGICODE" runat="server" />
             <asp:HiddenField ID="user_name" runat="server" />
            </div>
        </div>

 <div class="form-group row" runat="server" id="Div3">
       <div class="col-sm-12 text-center h5 font-weight-bold">Policy Schedule</div>
 </div>
 <div class="form-group row" runat="server" id="Div1">
       <div class="col-sm-12 text-center h5 font-weight-bold">Standard Fire Policy Schedule for Private Dwelling House</div>
 </div>
 <div class="form-group row" runat="server" id="Div4">
      <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Policy Number</div>
     <div class="col-sm-6"><label runat="server" id="policy_number"></label></div>
 </div>

          <div class="form-group row" runat="server" id="Div77">
      <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Debit Note Number</div>
     <div class="col-sm-6"><label runat="server" id="txt_debit"></label></div>
 </div>

         <div class="form-group row" runat="server" id="DivLoan">
      <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Bank Loan Number</div>
     <div class="col-sm-6"><label runat="server" id="txtLoan"></label></div>
 </div>

<div class="form-group row" runat="server" id="Div5">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Name of the Insured</div>
     <div class="col-sm-6"><label runat="server" id="nameOfInsu"></label></div>
 </div>


<div class="form-group row" runat="server" id="Div6">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Address</div>
     <div class="col-sm-6"><label runat="server" id="address"></label></div>
 </div>

<div class="form-group row" runat="server" id="Div7">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Financial Interests</div>
     <div class="col-sm-6"><label runat="server" id="fInterst"></label></div>
 </div>
<div class="form-group row" runat="server" id="Div8">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Period of Insurance</div>
     <div class="col-sm-6"><label runat="server" id="period"></label></div>
 </div>
<div class="form-group row" runat="server" id="Div9">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Sum Insured</div>
     <div class="col-sm-6"><label runat="server" id="sumInsu"></label></div>
 </div>

<div class="form-group row" runat="server" id="Div10">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Agency Code & Name</div>
     <div class="col-sm-6"><label runat="server" id="agentNameCode"></label></div>
 </div>

<div class="form-group row" runat="server" id="Div11">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Branch Code & Name</div>
     <div class="col-sm-6"><label runat="server" id="branchNameCode"></label></div>
 </div>
<div class="form-group row" runat="server" id="Div12">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Construction Details</div>
     <div class="col-sm-6"><label runat="server" id="constructDetails"></label></div>
 </div>

<div class="form-group row d-none" runat="server" id="Div13">
       <div class="col-sm-4"></div>
     <div class="col-sm-2 font-weight-bold">Walls :</div>
     <div class="col-sm-6"><label runat="server" id="exWall"></label></div>
 </div>
<div class="form-group row" runat="server" id="Div14">
       <div class="col-sm-4"></div>
     <div class="col-sm-2 font-weight-bold">Number of Floors :</div>
     <div class="col-sm-2"><label runat="server" id="numOfFloors"></label></div>
 </div>
        <div class="form-group row d-none" runat="server" id="Div15">
       <div class="col-sm-4"></div>
     <div class="col-sm-2 font-weight-bold">Roof :</div>
     <div class="col-sm-6"><label runat="server" id="roofDetails"></label></div>
 </div>
        <%--changes NSB--%>
        <div class="form-group row " runat="server" id="Div75">
       <div class="col-sm-4"></div>
          <%--<div class="col-sm-2 font-weight-bold">Number of Floors :</div>--%>
     <div class="col-sm-8 font-weight-bold"><label runat="server" id="Label1">
         Buildings are deemed to be constructed with walls of Bricks / Cement / Cement Blocks / Concrete and Doors & Windows with Wooden / Metal / Aluminum and Roof with Asbestos / Tile / GI Sheets / Concrete / Metal, unless the insurer has been advised otherwise. (Please refer special notes below)
    </label></div>
 </div>
        <%--end--%>
<div class="form-group row" runat="server" id="Div16">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Situated at</div>
     <div class="col-sm-6"><label runat="server" id="situated"></label></div>
 </div>
<div class="form-group row" runat="server" id="Div17">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-4 font-weight-bold">Detail of items to be insured</div>
     <div class="col-sm-6"></div>
 </div>

<div class="form-group row" runat="server" id="Div18">
       <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-7 border border-info font-weight-bold">Particulars of Risk</div>
     <div class="col-sm-2 border border-info font-weight-bold text-center">Sum Insured (Rs.)</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

<div class="form-group row" runat="server" id="Div19">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-7 border border-info">Building together with permanent fixtures & fittings including electrical wiring for permanent lighting & switches.</div>
     <div class="col-sm-2 border border-info text-center"><label runat="server" id="sumInWord1"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>


<div class="form-group row" runat="server" id="Div20">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-7 border border-info">Value of the boundary and parapet wall.</div>
     <div class="col-sm-2 border border-info text-center"><label runat="server" id="sumInWord2"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div51">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-7 border border-info">On solar panel system & standard accessories</div>
     <div class="col-sm-2 border border-info text-center"><label runat="server" id="lblSolarSum"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
<div class="form-group row" runat="server" id="Div21">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-7 border border-info font-weight-bold">Total Sum Insured</div>
     <div class="col-sm-2 border border-info font-weight-bold text-center"><label runat="server" id="SumInsuDisplay"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

<div class="form-group row" runat="server" id="Div22">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-10 font-weight-bold">This Insurance is subject to the Covers/Clauses/Endorsements indicated herein & attached hereto:</div>
     <%--<div class="col-sm-2"></div>--%>
 </div>
<div class="form-group row" runat="server" id="Div23">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-10 font-weight-bold">Scope of Cover</div>
     <%--<div class="col-sm-2"></div>--%>
 </div>

 <div class="form-group row" runat="server" id="Div24">
     <div class="col-sm-2 font-weight-bold"></div>
       <div id="dv1" runat="server" class="col-sm-9 text-justify"></div> 
     <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <%--extention of covers for solar--%>

        <div class="form-group row" runat="server" id="Div65">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-10 font-weight-bold">Extension of covers</div>
     <%--<div class="col-sm-2"></div>--%>
 </div>
 <div class="form-group row" runat="server" id="Div63"> 
     <div class="col-sm-2 font-weight-bold"></div>
       <div id="Div64" runat="server" class="col-sm-9 text-justify"><label runat="server" id="solarExt1"></label></div> 
     <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div66">
     <div class="col-sm-2 font-weight-bold"></div>
       <div id="Div67" runat="server" class="col-sm-9 text-justify"><label runat="server" id="solarExt2"></label></div> 
     <div class="col-sm-1 font-weight-bold"></div>
 </div>

        <%--end extention--%>

<div class="form-group row" runat="server" id="Div25">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-2 font-weight-bold">Premium Details</div>
     <div class="col-sm-2"></div>
 </div>

<%--premium section--%>

    <div class="form-group row" runat="server" id="Div26">
       <div class="col-sm-4"></div>
     <div class="col-sm-2">Net Premium</div>
     <div class="col-sm-1">Rs.</div>
     <div class="col-sm-4"><label runat="server" id="netPremium" class="labelxx"></label></div>
 </div>

  <div class="form-group row" runat="server" id="Div27">
       <div class="col-sm-4"></div>
     <div class="col-sm-2">SRCC</div>
     <div class="col-sm-1">Rs.</div>
     <div class="col-sm-4"><label runat="server" id="srccVal" class="labelxx"></label></div>
 </div>


<div class="form-group row" runat="server" id="Div28">
       <div class="col-sm-4"></div>
     <div class="col-sm-2">TR</div>
     <div class="col-sm-1">Rs.</div>
     <div class="col-sm-4"><label runat="server" id="trVal" class="labelxx"></label></div>
 </div>

<div class="form-group row" runat="server" id="Div29">
       <div class="col-sm-4"></div>
     <div class="col-sm-2">Admin Fee</div>
     <div class="col-sm-1">Rs.</div>
     <div class="col-sm-4"><label runat="server" id="adminFee" class="labelxx"></label></div>
 </div>

<div class="form-group row" runat="server" id="Div30">
       <div class="col-sm-4"></div>
     <div class="col-sm-2"><label id="txtPolFee" runat="server"></label></div>
     <div class="col-sm-1">Rs.</div>
     <div class="col-sm-4"><label runat="server" id="policyFee" class="labelxx"></label></div>
 </div>

   <div class="form-group row" runat="server" id="DivRenewalFee">
       <div class="col-sm-4"></div>
     <div class="col-sm-2">Renewal Fee</div>
     <div class="col-sm-1">Rs.</div>
     <div class="col-sm-4"><label runat="server" id="txt_renewal" class="labelxx"></label></div>
 </div>

<div class="form-group row" runat="server" id="Div31" visible="false">
       <div class="col-sm-4"></div>
     <div class="col-sm-2">Social Sec. Con.</div><%--NBT replace with SSC 01/.07/2022 start--%>
     <div class="col-sm-1">Rs.</div>
     <div class="col-sm-4"><label runat="server" id="nbtVal" class="labelxx"></label></div>
 </div>
<div class="form-group row" runat="server" id="Div32">
       <div class="col-sm-4"></div>
     <div class="col-sm-2">VAT</div>
     <div class="col-sm-1">Rs.</div>
     <div class="col-sm-4"><label runat="server" id="vatVal" class="labelxx"></label></div>
 </div>

<div class="form-group row" runat="server" id="Div33">
       <div class="col-sm-4"></div>
     <div class="col-sm-2 font-weight-bold">Total Payable</div>
     <div class="col-sm-1 font-weight-bold">Rs.</div>
     <div class="col-sm-4 font-weight-bold border border-0 border-top border-bottom border-dark"><label runat="server" id="totalPayble" class="labelxx"></label></div>
 </div>


<%--End--%>
<%--Excess--%>
<div class="form-group row" runat="server" id="Div34">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 font-weight-bold">Excess/Deductibles Applicable</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

<div class="form-group row" runat="server" id="Div35">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="deduct1"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div36">
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="deduct2"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div37">
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="deduct3"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div38">
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="deduct4"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div59">
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="deduct5"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div60">
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="deduct6"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div61">
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="deduct7"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>
         <div class="form-group row" runat="server" id="Div68"> <%--solar --%>
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="deduct8"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>
<div class="form-group row" runat="server" id="dedcuRow">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="deducID"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
<%--End--%>

 <%-- Clauses table---------------------------------------%>   
<div class="form-group row" runat="server" id="Div39">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 font-weight-bold">Clauses</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

<div class="form-group row" runat="server" id="Div40">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="clausesBank">* Bank / Mortgage clause</label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

<div class="form-group row" runat="server" id="Div41">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="claus1"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

<%--End--%>

 <%-- Warrenty table---------------------------------------%>   
<div class="form-group row" runat="server" id="Div42">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 font-weight-bold">Warranties :</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

<div class="form-group row" runat="server" id="Div43">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 text-justify"><label runat="server" id="waranty1"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
         <%-- Warrenty for solar---------------------------------------%>   
        <%--long only--%>
<div class="form-group row" runat="server" id="Div52">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 text-justify"><label runat="server" id="waranty2"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div53">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 text-justify"><label runat="server" id="waranty3"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div69">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 text-justify"><label runat="server" id="waranty4"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div70">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 text-justify"><label runat="server" id="waranty5"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
<%--End--%>
        <div class="form-group row" runat="server" id="Div47">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 text-justify"><label runat="server" id="defaultCondition"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
 

 <%-- Conditions table for solar---------------------------------------%>   
<div class="form-group row" runat="server" id="Div54">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 font-weight-bold">Exclusions</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

<div class="form-group row" runat="server" id="Div55">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 text-justify"><label runat="server" id="lblconditionSolar1"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

        

<%--End--%>
       



        <%--long term wording--%>
<div class="form-group row" runat="server" id="Div58">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 font-weight-bold">It is hereby declared & agreed that the within written policy will be automatically renewed for a further period of <label runat="server" id="longYear"></label> years, with effect from <label runat="server" id="longFrom"></label> and premium of Rs. <label runat="server" id="lblremainPremium"></label> will be adjusted annually.
       </div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
<div class="form-group row" runat="server" id="Div62">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 font-weight-bold">Further declared and agreed that the premium on SRCC, TR & Taxes will be revised based on amendments being done on rates applicable by NITF & the inland revenue department respectively .
       </div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <%--end--%>

        <%-- Financel table---------------------------------------%>   
<div class="form-group row" runat="server" id="Div44">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 font-weight-bold">Financial Interests :</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

<div class="form-group row" runat="server" id="Div45">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="fInterest2"></label></div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

        <div class="form-group row" runat="server" id="Div71">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-7 font-weight-bold">Particulars of Risk</div>
      <div class="col-sm-2 font-weight-bold">Assigned Value (Rs.)</div>
 </div>

          <div class="form-group row" runat="server" id="Div72">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-7 font-weight-bold"><label runat="server" id="fLbl"></label></div>
      <div class="col-sm-2 font-weight-bold"><label runat="server" id="FValuelbl" class="align-items-sm-end"></label></div>
 </div>

<%--End--%>

          <%-- Conditions table---------------------------------------%>   
        <div class="form-group row" runat="server" id="Div57">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 font-weight-bold">Conditions Applicable to Solar Panel System</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <div class="form-group row" runat="server" id="Div56">
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 text-justify"><label runat="server" id="lblconditionSolar2"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>

<div class="form-group row" runat="server" id="trcon1">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 font-weight-bold">Conditions Applicable to the Policy</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>

           <div class="form-group row" runat="server" id="Div73">
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="CondiAll"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>

        <div class="form-group row" runat="server" id="trcon2">
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="condiReasons"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>
        <%--changes 10032022--%>
        <div class="form-group row" runat="server" id="Div74">
            <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9"><label runat="server" id="lblQ13"></label></div>
            <div class="col-sm-1 font-weight-bold"></div>
 </div>
<%--End--%>

 <div class="form-group row" runat="server" id="Div76">
    <div class="col-sm-2 font-weight-bold"></div><%--<label runat="server" id="lblNote"></label>--%>
       <div class="col-sm-9 font-weight-bold text-justify">Note - <br />1. If any deviations in respect of construction details mentioned above, such changes should be emailed to <label runat="server" id="bankEmail" class="m-0"></label> within 21 days from the date of policy commencement. if not, we will consider construction details of the proposed building fall into categories as mentioned under construction details listed above.</div>
 <div class="col-sm-1 font-weight-bold"></div>
</div>

        <div class="form-group row" runat="server" id="Div46" visible="false">
    <div class="col-sm-2 font-weight-bold"></div><%--<label runat="server" id="lblNote"></label>--%>
       <div class="col-sm-9 font-weight-bold">2. We advise you to insure your building / solar panel for the new replacement value (Reinstatement basis) in order to avoid reduction for under insurance at the time of a claim.</div>
 <div class="col-sm-1 font-weight-bold"></div>
</div>

        <div class="form-group row" runat="server" id="Div48">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9">In witness where of the Undersigned being duly authorized by the 
                                  Insurers and on behalf of the Insures has (have) hereunder set his(their) hands.</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
<div class="form-group row" runat="server" id="Div49">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9 font-weight-bold">FIRE UNDERWRITING DEPARTMENT.<br /> 
                              SRI LANKA INSURANCE CORPORATION LIMITED.</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
<div class="form-group row" runat="server" id="Div50">
    <div class="col-sm-2 font-weight-bold"></div>
       <div class="col-sm-9">This is a system generated print and therefore requires no authorized signature.</div>
    <div class="col-sm-1 font-weight-bold"></div>
 </div>
        
</div>

 
  <div class="MessagePanelDiv">
   <asp:Panel ID="Message" runat="server" CssClass="hidepanel">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
    <asp:Label ID="labelMessage" runat="server" />
   </asp:Panel>
 </div>

    
</asp:Content>

