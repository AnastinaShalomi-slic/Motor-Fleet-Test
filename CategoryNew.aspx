<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CategoryNew.aspx.cs" Inherits="CategoryNew" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" runat="server" />
    <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no, width=device-width" runat="server" />
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" runat="server">--%>
    <title>Select Options</title>


    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <link href="Content/bootstrap-datepicker.css" rel="stylesheet" />
    <link href="Content/bootstrap-grid.css" rel="stylesheet" />


    <script src="<%= Page.ResolveUrl("~/Scripts/jquery-3.5.1.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/popper.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/bootstrap.min.js") %>"></script>
    <script src="<%= Page.ResolveUrl("~/sweetCSS/sweetalert.min.js")%>"></script>
    <script src="<%= Page.ResolveUrl("~/Scripts/bootstrap-datepicker.min.js") %>"></script>

    <script src="<%= Page.ResolveUrl("~/JavaScripts/Font_Awsome.js")%>"></script>
    <link href="Auto_compl_Js/jquery-ui.css" rel="stylesheet" />
    <script>

        function custom_alert(message, title) {

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
            else if (title == 'Success') {
                swal
                    ({
                        title: title,
                        text: message,
                        icon: "success",
                        button: true,
                        closeOnClickOutside: false,
                    });
            }

            else if (title == 'Error') {
                swal
                    ({
                        title: title,
                        text: message,
                        icon: "error",
                        button: true,
                        closeOnClickOutside: false,
                    });
            }
        }


        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $('.carousel').carousel({
            interval: 1500
        });

    </script>
    <style type="text/css">
        * {
            font-family: Calibri;
        }

        .bs-example {
            margin: auto;
            /*background-color:#80EBED;*/
        }

        .centerIconPanelMain {
            text-align: center;
            /*width: 100%;*/
            font-size: 100px;
            color: #ffffff;
            /*border:solid #ff0000;*/
        }

        .CardBackgroundMain {
            /*background-color:  rgba(15, 95, 73, 0.90);*/ /*rgba(0,172,186, 0.50);*/
            background-color: rgba(2, 104, 136, 0.27);
        }

        .cardProp {
            min-height: 110px;
            min-width: 240px;
            border: solid 1px #83939b;
            text-align: center;
            /*border-right: 5px solid rgba(24, 149, 165, 0.83) /*#b2081c*/ /*#FF2470 rgba(24, 149, 165, 0.83);
        border-radius: 12px;*/
        }

        .img_sizeMain {
            max-height: 240px;
            max-width: 240px;
            align-items: center;
        }

        .item_cssSecondMain {
            color: #ffd800;
            font-family: Calibri;
            font-size: 18px;
            font-weight: bold;
        }

        .CardBackgroundMain {
            /*background-color:  rgba(15, 95, 73, 0.90);*/ /*rgba(0,172,186, 0.50);*/
            background-color: rgba(2, 104, 136, 0.0);
            border: 1px #ffffff solid;
        }


        .backGroundCat {
            /*background-color:#49A2AB;*/ /*#037680;*/ /*#45B39D*/
            /*background-image:url(../IconPack/BackgroundImage.png);*/
            /*background-image:url(../Images/Health1.jpg);*/
            /*background-color:#edebeb;*/
            background-size: cover;
            background-repeat: no-repeat;
            background-position: 90% 10%;
            background-attachment: fixed;
            /*background-image: url(<%:ResolveUrl("~/Images/moon.jpg")%>);*/
            /*background-image: url("../Images/001_SLIC.jpg");*/
        }

        .item_cssSecond {
            color: #000;
            font-family: Calibri;
            font-size: 18px;
            font-weight: bold;
        }

        .carousel {
            /*background: #193a53; #0aae9a; #8ACCCB*/
            background: #026888; /*#217C7A;*/
            /*margin-top: 30px;*/
        }

        .carousel-item {
            text-align: center;
            max-height: 220px;
            min-height: 160px;
            /* Prevent carousel from being distorted if for some reason image doesn't load */
        }

        .containerx {
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            min-height: 500px;
        }
    </style>

</head>
<body class="backGroundCat" runat="server">

    <form id="form1" runat="server" class="m-4">
        <asp:HiddenField ID="bank_code" runat="server" />
        <div class="form-group row mt-5" runat="server" id="Div2">

            <div class="col-sm-3" runat="server"></div>
            <div class="col-sm-6 text-center" runat="server">
                <label runat="server" id="user_name" class="text-center text-capitalize text-black-50 font-weight-normal">Test Bank - Branch</label>
            </div>
            <div class="col-sm-3 text-center" runat="server">
                <asp:Button runat="server" ID="signOutIdImg" src="../../../Secworks/Signin.asp" Text="Log Out"  ClientIDMode="Static"
                    OnClick="signOutIdImg_Click" CssClass="btn btn-info text-white text-capitalize" />
            </div>
        </div>

        <%--news feed--%>
        <%--<div class="form-group row" runat="server" id="Div3">
               <div class="col-sm-12 m-1" >
                   <div id="carouselExampleIndicators" class="carousel slide border border-light" data-ride="carousel">
  <ol class="carousel-indicators">
    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
  </ol>
  <div class="carousel-inner">
    <div class="carousel-item active">
      <img class="img-fluid" src="/Images/779.jpg"" alt="First slide"/>
        <div class="carousel-caption d-none d-md-block">
    <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
    <div class="carousel-item">
      <img class="img-fluid" src="/Images/moon.jpg" alt="Second slide"/>
        <div class="carousel-caption d-none d-md-block">
   <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
    <div class="carousel-item">
      <img class="img-fluid" src="/Images/scientific_space.jpg" alt="Third slide"/>
        <div class="carousel-caption d-none d-md-block">
    <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
  </div>
  <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
    <span class="sr-only">Previous</span>
  </a>
  <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
    <span class="carousel-control-next-icon" aria-hidden="true"></span>
    <span class="sr-only">Next</span>
  </a>
</div>
               </div>
        </div>--%>

        <%--end--%>


        <div class="container" id="mainDiv" runat="server">
            <%--<div class="containerx">--%>
            <div class="">
                <div class="form-group row mt-5" runat="server" id="Div1">
                    <div class="col-sm-12 pt-1">

                        <div class="card-deck m-1" runat="server">
                            <%--<div class="card CardBackgroundMain h-75 pb-4 cardProp" runat="server" id="pageSix">
                                <div class="p-2" runat="server">
                                    
                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="MotorClick" CssClass="stretched-link">
                                    <img src="~/UI_Icons/MotorInsuranceIcon.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>
                                     </asp:LinkButton>
                                </div>
                            </div>--%>
                            <div class="card CardBackgroundMain h-75 pb-4 cardProp" runat="server" id="pageEight">
                                <div class="p-2" runat="server">

                                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="FireClick" CssClass="stretched-link"> 
                                        <img src="~/UI_Icons/FireInsuranceIcon.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>
                                    </asp:LinkButton>
                                </div>
                                <%--<div class="card-footer text-center" runat="server">
                                <h5 class="card-title text-center item_cssSecond text-white" runat="server">Fire Insurance</h5>
                                </div>--%>
                            </div>



                            <%--Sanjeewa Kumara Ranaweera 08/04/2024--%>
                            <%--<div class="card CardBackgroundMain h-75 pb-4 cardProp" runat="server" id="Div3">
                                <div class="p-2" runat="server">

                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="OdClick" CssClass="stretched-link"> 
                                <img src="~/OdProtect/images/OdpLogo.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>
                                    </asp:LinkButton>
                                </div>
                            </div>--%>
                            <%--End  Sanjeewa Kumara Ranaweera 08/04/2024--%>




                            <%--<div class="card CardBackgroundMain h-50 pb-4 cardProp" runat="server" id="pageTicket">
                                <div class="p-2" runat="server">

                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="TicketClick" CssClass="stretched-link"> 
                                        <img src="~/UI_Icons/ticket.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>
                                    </asp:LinkButton>
                                </div>
                            </div>--%>
                            <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" Visible="false" />
                        </div>


                    </div>
                </div>
            </div>
        </div>

        <%-- <div class="navbar fixed-bottom" runat="server">--%>
        <footer class="bg-transparent text-center text-lg-center footer text-white font-weight-normal fixed-bottom" runat="server">
            &copy; Copy Right Reserved. SLIC <%=DateTime.Now.Year%> Version 1.0.0   
        </footer>
        <%--</div>--%>
    </form>
</body>

</html>
