<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FireDefault.aspx.cs" Inherits="FireDefault" %>

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
/*.carousel .carousel-indicators li {background-color: red;}
.carousel .carousel-indicators li.active {background-color: blue;}*/
/*.carousel-control-prev-icon, .carousel-control-next-icon {
    height: 60px;
    width: 60px;
    outline: #ffffff;
    background-color: #00adba;
    background-size: 50%, 50%;
    border-radius: 40%;
    padding:5px;
    border: 2px solid #ffffff;
    
}*/
     /*.carousel-control-prev-icon {
    width: 40px;
    height: 40px;
    background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' fill='%23c593d8' width='8' height='8' viewBox='0 0 8 8'%3e%3cpath d='M5.25 0l-4 4 4 4 1.5-1.5L4.25 4l2.5-2.5L5.25 0z'/%3e%3c/svg%3e");
}

.carousel-control-next-icon {
    
    width: 40px;
    height: 40px;
    background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' fill='%23c593d8' width='8' height='8' viewBox='0 0 8 8'%3e%3cpath d='M2.75 0l-1.5 1.5L3.75 4l-2.5 2.5L2.75 8l4-4-4-4z'/%3e%3c/svg%3e");
}*/

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
    <%--<div class="form-group" id="mainDiv" runat="server">--%>

    <%--news feed--%>
       

        <%--end--%>
   <%-- <div class="container border border-info" id="Div1" runat="server">--%>
       <!-- Main jumbotron for a primary marketing message or call to action -->
  <div class="jumbotron">
    <div class="container">
    

     <div id="carouselExampleIndicators" class="carousel slide" data-ride="carousel">
  <ol class="carousel-indicators">
    <li data-target="#carouselExampleIndicators" data-slide-to="0" class="active"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="1"></li>
    <li data-target="#carouselExampleIndicators" data-slide-to="2"></li>
  </ol>
   <%--BOC side--%>
  <div class="carousel-inner" id ="BOC" runat="server">
    <div class="carousel-item active">
      <img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/BOCE/BOCALL.jpg")%> alt="First slide">
         <div class="carousel-caption d-none d-md-block">
   <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/BOCE/BOCFireE.jpg")%> alt="Second slide">
        <div class="carousel-caption d-none d-md-block">
   <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/BOCE/BOCMotorE.jpg")%> alt="Third slide">
        <div class="carousel-caption d-none d-md-block">
    <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
  </div>
          <%--PB side--%>
  <div class="carousel-inner" id ="PB" runat="server">
    <div class="carousel-item active">
      <%--<img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/PBE/PBMotorALL.jpg")%> alt="First slide">--%>
        <img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/PBE/PBFireE.jpg")%> alt="Second slide">
         <div class="carousel-caption d-none d-md-block">
   <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/PBE/PBFireE.jpg")%> alt="Second slide">
        <div class="carousel-caption d-none d-md-block">
   <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
    <div class="carousel-item">
      <%--<img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/PBE/PBMotorE.jpg")%> alt="Third slide">--%>
        <img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/PBE/PBFireE.jpg")%> alt="Second slide">
        <div class="carousel-caption d-none d-md-block">
    <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
  </div>

          <%--NSB side--%>
  <div class="carousel-inner" id ="NSB" runat="server">
    <div class="carousel-item active">
     <%-- <img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/PBE/PBMotorALL.jpg")%> alt="First slide">--%>
          <img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/NSBE/NSBFireE.jpg")%> alt="Second slide">
         <div class="carousel-caption d-none d-md-block">
   <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/NSBE/NSBFireE.jpg")%> alt="Second slide">
        <div class="carousel-caption d-none d-md-block">
   <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
    <div class="carousel-item">
      <%--<img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/PBE/PBMotorE.jpg")%> alt="Third slide">--%>
          <img class="d-block w-100" src=<%:ResolveUrl("~/UserHelp/E_Flyers/NSBE/NSBFireE.jpg")%> alt="Second slide">
        <div class="carousel-caption d-none d-md-block">
    <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
  </div>


          <%--SLIC side--%>
  <div class="carousel-inner" id ="SLIC" runat="server">
    <div class="carousel-item active">
      <img class="d-block w-100" src=<%:ResolveUrl("~/Images/14.jpg")%> alt="First slide">
         <div class="carousel-caption d-none d-md-block">
   <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
    <div class="carousel-item">
      <img class="d-block w-100" src=<%:ResolveUrl("~/Images/15_1.jpg")%> alt="Second slide">
        <div class="carousel-caption d-none d-md-block">
   <h5>Sri Lanka Insurance</h5>
    <p>Largest and strongest composite insurance provider in Sri Lanka</p>
  </div>
    </div>
    
  </div>
 <a class="carousel-control-prev" href="#carouselExampleIndicators" role="button" data-slide="prev">
                        <i class="fas fa-arrow-circle-left" aria-hidden="true"></i><%--<i class="far fa-arrow-alt-circle-right"></i>--%>
                        <span class="sr-only">Previous</span>
                    </a>
                    <a class="carousel-control-next" href="#carouselExampleIndicators" role="button" data-slide="next">
                        <i class="fas fa-arrow-circle-right" aria-hidden="true"></i>
                        <span class="sr-only">Next</span>
                    </a>


</div>


    </div>
  </div>
          
</asp:Content>

