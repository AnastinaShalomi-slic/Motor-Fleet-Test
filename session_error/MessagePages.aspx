<%@ Page Title="" Language="C#"  AutoEventWireup="true" CodeFile="MessagePages.aspx.cs" Inherits="session_error_MessagePages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title runat="server" id="pageTitle">Message Page</title>

    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
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
            min-width:260px;

        }
     .bg-cover
     {
        background-size: cover !important;
     }

    </style>
</head>
<body runat="server" id="pageBody">
     <form id="form1" runat="server" class="m-3">
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
        
               <asp:Button id="bt_back" runat="server" Text="Back" OnClick="bt_err_Click" class="btn btn-info" ClientIDMode="Static"/>               
                <asp:Button id="bt_ok" runat="server" Text="Done" OnClick="bt_ok_Click" class="btn btn-info" ClientIDMode="Static"/>
           </div>
        </div>
        <div class="form-group row"> 
            <div class="col-sm-12 text-center" id="mainMsg" runat="server"></div>
        </div>
        </div>


</form>
</body>
</html>