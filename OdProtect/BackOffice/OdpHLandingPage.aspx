<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage_Odb.master" AutoEventWireup="true" CodeFile="OdpHLandingPage.aspx.cs" Inherits="OdProtect_BackOffice_OdpHLandingPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $('.carousel').carousel({
            interval: 1000
        });

    </script>
 <style type="text/css">


.container .carousel-control-prev {
    /*left: -110px;*/
    border-bottom: 0;
    font-size: 40px;
    color: #00adba;
}
 
.container .carousel-control-next {
    /*right: -110px;*/
    border-bottom: 0;
    font-size: 40px;
    color: #00adba;
}
</style>
  
  <div class="jumbotron">
    <div class="container">
    

        <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
            <ol class="carousel-indicators">
                <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
                <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
            </ol>


            <%--BOC side--%>
            <div class="carousel-inner" id="BOC" runat="server">

                  <div class="carousel-item active">
                    <img class="d-block w-100" src='<%:ResolveUrl("~/OdProtect/eflyer/ODP_E_Flyer_BOC.png")%>' alt="Second slide">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>Sri Lanka Insurance</h5>
                        <p>Largest and strongest composite insurance provider in Sri Lanka</p>
                    </div>
                </div>
                <div class="carousel-item">
                    <img class="d-block w-100" src='<%:ResolveUrl("~/UserHelp/E_Flyers/BOCE/BOCALL.jpg")%>' alt="First slide">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>Sri Lanka Insurance</h5>
                        <p>Largest and strongest composite insurance provider in Sri Lanka</p>
                    </div>
                </div>
                <div class="carousel-item">
                    <img class="d-block w-100" src='<%:ResolveUrl("~/UserHelp/E_Flyers/BOCE/BOCFireE.jpg")%>' alt="Second slide">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>Sri Lanka Insurance</h5>
                        <p>Largest and strongest composite insurance provider in Sri Lanka</p>
                    </div>
                </div>
                <div class="carousel-item">
                    <img class="d-block w-100" src='<%:ResolveUrl("~/UserHelp/E_Flyers/BOCE/BOCFireE.jpg")%>' alt="Second slide">
                    <div class="carousel-caption d-none d-md-block">
                        <h5>Sri Lanka Insurance</h5>
                        <p>Largest and strongest composite insurance provider in Sri Lanka</p>
                    </div>
                </div>
            </div>

        </div>
    </div>
  </div>          
</asp:Content>

