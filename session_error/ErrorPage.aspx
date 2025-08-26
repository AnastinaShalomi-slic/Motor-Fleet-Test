<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ErrorPage.aspx.cs" Inherits="session_error_ErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="Content/bootstrap-grid.css" rel="stylesheet" />


     <script src="<%= Page.ResolveUrl("~/Scripts/jquery-3.5.1.min.js") %>"></script>
     <script src="<%= Page.ResolveUrl("~/Scripts/popper.min.js") %>"></script>
     <script src="<%= Page.ResolveUrl("~/Scripts/bootstrap.min.js") %>"></script>
     <script src="<%= Page.ResolveUrl("~/sweetCSS/sweetalert.min.js")%>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/bootstrap-datepicker.min.js") %>"></script>
 
    <link href="Auto_compl_Js/jquery-ui.css" rel="stylesheet" />




     <script>
        (function (global) {

            if (typeof (global) === "undefined") {
                throw new Error("window is undefined");
            }

            var _hash = "!";
            var noBackPlease = function () {
                global.location.href += "#";

                // making sure we have the fruit available for juice....
                // 50 milliseconds for just once do not cost much (^__^)
                global.setTimeout(function () {
                    global.location.href += "!";
                }, 50);
            };

            // Earlier we had setInerval here....
            global.onhashchange = function () {
                if (global.location.hash !== _hash) {
                    global.location.hash = _hash;
                }
            };

            global.onload = function () {

                noBackPlease();

                // disables backspace on page except on input fields and textarea..
                document.body.onkeydown = function (e) {
                    var elm = e.target.nodeName.toLowerCase();
                    if (e.which === 8 && (elm !== 'input' && elm !== 'textarea')) {
                        e.preventDefault();
                    }
                    // stopping event bubbling up the DOM tree..
                    e.stopPropagation();
                };

            };

        })(window);
    </script>
     <style type="text/css">

      .page-holder 
      {
       
        min-height:40vh;
      
      }
       .imageclz
        {
            max-height:240px;
            /*max-width:200px;*/

            /*min-height:100px;*/
            min-width:260px;

        }
     .bg-cover
     {
        background-size: cover !important;
     }

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <asp:HiddenField ID="UserId" runat="server" />
       <asp:HiddenField ID="brCode" runat="server" />
    <div class="container" id="QuoViewDetailsBuglary" runat="server">
          <div class="form-group row" runat="server">
        <label for="CusDetails" class="col-sm-4 h6 font-weight-bold text-info">Info. Page</label>
              </div>
          <hr />
       <div class="form-group row h-auto" runat="server">
           <div class="col-sm-4 align-content-center" id ="one"></div>
           <div class="col-sm-4 align-content-center ml-4" id ="mainImg" runat="server"> 
                <div class="page-holder bg-cover" runat="server" id="errorImage"><img src="../Images/1234.jpg" class="img-fluid imageclz text-center"/></div>
                <div class="page-holder bg-cover" runat="server" id="oracleError"><img src="../Images/orcl_error.jpg" class="img-fluid imageclz text-center"/></div>
                <div class="page-holder bg-cover" runat="server" id="sucsessImage"><img src="../Images/iconDone100.png" class="img-fluid imageclz text-center"/></div>
               </div>

           <div class="col-sm-2 align-content-end" id ="two">
               <asp:Button type="Submit" class="btn btn-info" runat="server" Text="  Back  " ID="btBack" 
                ClientIDMode="Static" OnClick="btBack_Click"/>
           </div>
        </div>
        <div class="form-group row"> 
            <div class="col-sm-12 text-center" id="mainMsg" runat="server"></div>
        </div>
        </div>
</asp:Content>

