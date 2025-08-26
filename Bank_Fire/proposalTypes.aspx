<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="proposalTypes.aspx.cs" Inherits="Bank_Fire_proposalTypes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style type="text/css">        
   
    input[type=radio] 
    {
    width: 25px;
    height: 25px;
  
    }
    .radioClz
    {
        font-size:23px;
    }
    .ChkBoxClass input {width:18px; height:18px;}


      * { font-family:Calibri; } 

        
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
          background-color: rgba(2, 104, 136, 0.27);
        }
     .cardProp
     {
        min-height:110px;
        min-width:240px;
        border:solid 1px #83939b;
        text-align:center;
        /*border-right: 5px solid rgba(24, 149, 165, 0.83) /*#b2081c*//*#FF2470 rgba(24, 149, 165, 0.83);
        border-radius: 12px;*/
     }
        
         .img_sizeMainPanel
    {
        max-height:240px;
        max-width:240px;
        align-items:center;
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
         background-color: rgba(2, 104, 136, 0.0);
         border:1px #ffffff solid;
     }
        

         .backGroundCat
      {
          background-color:#49A2AB;/*#037680;*/  /*#45B39D*/
          background-size:cover;
          background-repeat:no-repeat;
          background-position: 90% 10%;
          background-attachment: fixed;
          background-image: url(<%:ResolveUrl("~/Images/moon.jpg")%>);
          
      }
         .item_cssSecond
      {
          color:#000;
          font-family: Calibri;
          font-size: 18px;
          font-weight:bold;
      }

      
    .containerx {
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center;
  min-height: 500px;
}
    /*.form-group {
  margin-bottom: 5px;
}*/
    </style>
      <script type="text/javascript">
        function DisableBackButton() {
            window.history.forward()
        }
        DisableBackButton();
        window.onload = DisableBackButton;
        window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
        window.onunload = function () { void (0) }
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
                        closeOnClickOutside: true,
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
                         closeOnClickOutside: true,
                    });
            }

    }
  
    </script>

  

     <div class="container border border-info mt-5" id="mainDiv" runat="server">

          <div class="">

        <div class="form-group row mt-5" runat="server" id="Div1">
               <div class="col-sm-12 pt-0" >

         <div class="card-deck m-1" runat="server">
    <div class="card CardBackgroundMain h-100 pb-4 cardProp" runat="server" id ="pageSix">
        <div class="p-0" runat="server">
       <asp:LinkButton ID="LinkButton3" runat="server" onclick="Option1" CssClass="stretched-link">
       
   <img src="~/UI_Icons/SubIcon1.png" class="card-img-top img-fluid img_sizeMainPanel d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>
       </asp:LinkButton>
            </div>
 
    </div>
     <div class="card CardBackgroundMain h-100 pb-4 cardProp" runat="server" id ="pageEight">
          <div class="p-0" runat="server"><%--onclick="Option2"--%>
                
        <asp:LinkButton ID="LinkButton4" runat="server" CssClass="stretched-link" data-toggle="modal" data-target="#exampleModalCenter"> 
            <img src="~/UI_Icons/SubIcon2.png" class="card-img-top img-fluid img_sizeMainPanel d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>
       </asp:LinkButton>
          </div>

    </div>
             <div class="card CardBackgroundMain h-100 pb-4 cardProp" runat="server" id ="Div2">
        <div class="p-0" runat="server">
       <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="stretched-link" onclick="Option3">
       
   <img src="~/UI_Icons/SubIcon3.png" class="card-img-top img-fluid img_sizeMainPanel d-block mx-auto centerIconPanelMain" alt="..." runat="server"/>
       </asp:LinkButton>
            </div>

    </div>
</div>


</div>
        </div>
            </div>




     </div>
    <!-- Button trigger modal -->
<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModalCenter" runat="server" visible="false">
  Launch demo modal
</button>

<!-- Modal -->
<div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Bancassurance Info.</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
        Welcome to SLIC Bancassurance. Stay tuned for upcoming products. Thank You.
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
      </div>
    </div>
  </div>
</div>


</asp:Content>

