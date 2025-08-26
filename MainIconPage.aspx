<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MainIconPage.aspx.cs" Inherits="MainIconPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <meta http-equiv="X-UA-Compatible" content="IE=edge" runat="server"/>
    <meta name="viewport" content="initial-scale=1.0, maximum-scale=1.0, user-scalable=no, width=device-width" runat="server"/> 
    <%--<meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" runat="server">--%>
    <title>Motor Options</title>

    
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
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });

        $('.carousel').carousel({
            interval: 1500
        });
    </script>
    <style type="text/css">
        * { font-family:Calibri; } 

        .bs-example {
            margin: auto;
            /*background-color:#80EBED;*/
        }
            .centerIconPanelMain
       {
                text-align: center;
                /*width: 100%;*/
                font-size: 100px; 
                color: #ffffff;
                /*border:solid #ff0000;*/
       }
 .CardBackgroundMain
     {
         /*background-color:  rgba(15, 95, 73, 0.90);*/ /*rgba(0,172,186, 0.50);*/
         background-color: rgba(255, 255, 255, 0.10);
     }
     .cardProp
     {
        min-height:110px;
        min-width:110px;
        border:1px solid #83939b;
        text-align:center;
        /*border-right: 5px solid rgba(24, 149, 165, 0.83) /*#b2081c*//*#FF2470 rgba(24, 149, 165, 0.83);
        border-radius: 12px;*/
       
     }
        
         .img_sizeMain
    {
        max-height:50px;
        max-width:50px;
        align-items:center;
    }

              .img_sizeHome
    {
        max-height:40px;
        max-width:40px;
        align-items:center;
    }

   .img_sizeIcon
    {
        max-height:30px;
        max-width:30px;
        /*align-items:center;*/
    }

         .item_cssSecondMain
      {
          color:#ffd800;
          font-family: Calibri;
          font-size: 18px;
          font-weight:bold;
      }

     .CardBackgroundMain
     {
         /*background-color:  rgba(15, 95, 73, 0.90);*/ /*rgba(0,172,186, 0.50);*/
         background-color: rgba(2, 104, 136, 0.27);
     }
         .testClass
        {
            /*font-family: Courier New, Courier, monospace;*/
            font-size: 14px;
            font-weight: 600;
            color:#ffffff;
        }

         .backGround
      {
          background-color:#026888;
          /*background-color:rgb(22, 163, 135);*//*#45B39D #037680*/
          /*background-image:url(../IconPack/BackgroundImage.png);*/
          /*background-image:url(../Images/Health1.jpg);*/
          /*background-color:#edebeb;*/
          background-size:cover;
          background-repeat:no-repeat;
          background-position: 90% 10%;
          background-attachment: fixed;
          /*background-image: url("../Images/001_SLIC.jpg");*/
          
      }
         .item_cssSecond
      {
          color:#000;
          font-family: Calibri;
          font-size: 18px;
          font-weight:bold;
      }
    .carousel
    {
        /*background: #193a53; #0aae9a; #8ACCCB*/
        background: #026888; /*#217C7A;*/
        /*margin-top: 30px;*/
    }
    .carousel-item
    {
        text-align: center;
        max-height: 220px; 
        min-height: 160px; 
        
        /* Prevent carousel from being distorted if for some reason image doesn't load */
    }

</style>
</head>
<body class="backGround" runat="server">

    <form id="form1" runat="server" class="m-4"> 
     
        
        <div class="form-group row mt-0" runat="server" id="Div3">

            <div class="col-sm-3" runat="server"></div>
             <div class="col-sm-6 text-center" runat="server">
                 <a runat="server" title="Home" id="a3"  href="MainIconPage.aspx" class="">
                        
                     <label runat="server" id="Label1" class="text-center text-capitalize text-white font-weight-normal">
                         <img runat="server" id="Img2" src="~/Images/114.png" class="img-fluid d-block mx-auto img_sizeHome"/>
                         Fire Online System</label>
                 </a>
                 
             </div>
              <div class="col-sm-1 text-center" runat="server"> </div>
             <div class="col-sm-2 text-center" runat="server">  
                   
                        
             </div>        
       </div>

          <div class="form-group row mt-0" runat="server" id="Div2">

             <div class="col-sm-3" runat="server"></div>
             <div class="col-sm-6 text-center" runat="server">
                 <label runat="server" id="user_name" class="text-center text-capitalize text-white font-weight-normal">Test Bank - Branch</label>
             </div>
              <div class="col-sm-0" runat="server">
                  
              </div>
             <div class="col-sm-3 text-center" runat="server">  
                   <a runat="server" title="Home" id="a4"  href="MainIconPage.aspx" class="">
                        <img runat="server" id="Img3" src="~/Images/HomeIconSecond.png" class="img-fluid img_sizeIcon" />
                 </a>
                 <asp:Button runat="server" id="signOutIdImg" src="~/Images/signOut.png" Text="Log Out" href="Login.aspx" ClientIDMode="Static"
                                     OnClick="signOutIdImg_Click" CssClass="btn btn-info text-white text-capitalize"/>         
             </div>
         </div>



         <%--news feed--%>
        <div class="form-group row" runat="server" id="Div4">
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
        </div>

        <%--end--%>



        <div class="form-group row" runat="server" id="Div1">
             <div class="col-sm-1 pt-0" ></div>
               <div class="col-sm-10 pt-0" >

         <div class="card-deck m-1" runat="server">

    <div class="card CardBackgroundMain h-50 pb-2 cardProp" runat="server" id ="pageSix">
        
        <div class="p-2" runat="server">
        
  <a runat="server" href="~/Bank_Fire/ProposalEntry.aspx" class="stretched-link">
        <img src="~/images/requestDoc.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>           
     </a>    
            </div>
          
          <div class="card-footer text-center" runat="server">
        <h5 class="card-title text-center item_cssSecond text-white" runat="server">Proposal Entry</h5>
        </div>
    </div>

     <div class="card CardBackgroundMain h-50 pb-2 cardProp" runat="server" id ="pageEight">
        
        <div class="p-2" runat="server">
        
  <a runat="server" href="~/Bank_Fire/ViewProposal.aspx" class="stretched-link">
        <img src="~/images/viewReq.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>           
     </a>    
            </div>
          
          <div class="card-footer text-center" runat="server">
        <h5 class="card-title text-center item_cssSecond text-white" runat="server">View Proposal</h5>
        </div>
    </div>


             <div class="card CardBackgroundMain h-50 pb-2 cardProp" runat="server" id ="pageSeven">
        
        <div class="p-2" runat="server">
        
  <a runat="server" href="~/Contacts/DefaultFire.aspx" class="stretched-link">
        <img src="~/images/CustomCare.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>           
     </a>    
            </div>
          
          <div class="card-footer text-center" runat="server">
        <h5 class="card-title text-center item_cssSecond text-white" runat="server">Contact SLIC</h5>
        </div>
    </div>
             <div class="card CardBackgroundMain h-50 pb-2 cardProp" runat="server" id ="pageNine">
        
        <div class="p-2" runat="server">
        
  <a runat="server" href="~/SLIC_Fire/Fire_Entered_Policy.aspx" class="stretched-link">
        <img src="~/images/pendingDoc.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>           
     </a>    
            </div>
          
          <div class="card-footer text-center" runat="server">
        <h5 class="card-title text-center item_cssSecond text-white" runat="server">Pending Proposals</h5>
        </div>
    </div>
             <div class="card CardBackgroundMain h-50 pb-2 cardProp" runat="server" id ="pageeleven">
        
        <div class="p-2" runat="server">
        
  <a runat="server" href="~/SLIC_Fire/ApprovedPolicy.aspx" class="stretched-link">
        <img src="~/images/approvedDoc.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>           
     </a>    
            </div>
          
          <div class="card-footer text-center" runat="server">
        <h5 class="card-title text-center item_cssSecond text-white" runat="server">Approved Proposals</h5>
        </div>
    </div>

</div>




<%--second deck row  d-block mx-auto--%>
                   <div class="card-deck m-2" runat="server">
<div class="card CardBackgroundMain h-50 pb-2 cardProp" runat="server" id ="pagethirteen">
        
        <div class="p-2" runat="server">
        
  <a runat="server" href="~/SLIC_Fire/RejectedPolicy.aspx" class="stretched-link">
        <img src="~/images/rejectDoc.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>           
     </a>    
            </div>
          
          <div class="card-footer text-center" runat="server">
        <h5 class="card-title text-center item_cssSecond text-white" runat="server">Rejected Proposals</h5>
        </div>
    </div>

     <div class="card CardBackgroundMain h-50 pb-2 cardProp" runat="server" id ="pageTen">
        
        <div class="p-2" runat="server">
        
  <a runat="server" href="~/UserMannual/User_manual.aspx" class="stretched-link">
        <img src="~/images/userManual001.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>           
     </a>    
            </div>
          
          <div class="card-footer text-center" runat="server">
        <h5 class="card-title text-center item_cssSecond text-white" runat="server">User Manual</h5>
        </div>
    </div>


             <div class="card CardBackgroundMain h-50 pb-2 cardProp" runat="server" id ="pagetwelve">
        
        <div class="p-2" runat="server">
        
  <a runat="server" href="~/UserMannual/PolicyFireBooklet.aspx" class="stretched-link">
        <img src="~/images/booklet001.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>           
     </a>    
            </div>
          
          <div class="card-footer text-center" runat="server">
        <h5 class="card-title text-center item_cssSecond text-white" runat="server">Policy Booklet</h5>
        </div>
    </div>
             <div class="card CardBackgroundMain h-50 pb-2 cardProp" runat="server" id ="AdminPanel">
        
        <div class="p-2" runat="server">
        
  <a runat="server" href="~/AdminPanel/AdminView.aspx" class="stretched-link">
        <img src="~/images/progress.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>           
     </a>    
            </div>
          
          <div class="card-footer text-center" runat="server">
        <h5 class="card-title text-center item_cssSecond text-white" runat="server">Progress View</h5>
        </div>
    </div>
             <div class="card CardBackgroundMain h-50 pb-2 cardProp" runat="server" id ="fireReports">
        
        <div class="p-2" runat="server">
        
  <a runat="server" href="~/Reports/ProcessReports.aspx" class="stretched-link">
        <img src="~/images/proposalForm.png" class="card-img-top img-fluid img_sizeMain d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>           
     </a>    
            </div>
          
          <div class="card-footer text-center" runat="server">
        <h5 class="card-title text-center item_cssSecond text-white" runat="server">Fire Reports</h5>
        </div>
    </div>
</div>

</div>
 <div class="col-sm-1 pt-0" ></div>

        </div>


      
<footer class="bg-transparent text-center text-lg-center footer text-white font-weight-normal fixed-bottom" runat="server"> 
    &copy; Copy Right Reserved. SLIC <%=DateTime.Now.Year%> Version 1.0.0
 
  
  
</footer>
      
        </form>
</body>
</html>


