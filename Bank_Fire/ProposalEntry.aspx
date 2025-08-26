<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ProposalEntry.aspx.cs" Inherits="Bank_Fire_ProposalEntry" Culture = "en-GB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="Stylesheet" href="../JavaScripts/select2.css" />
    <script src="../JavaScripts/select2.js"></script>

<style type="text/css">        
   .fieldset_1{ margin-top:10px; height:520px; border-color:#b3b3b3;} 
   .importGrid_Header{height:25px; font-family: "Times New Roman"; font-size:10pt; background-color:#e6e6e6; color:#000000; white-space:nowrap;}
   .importGrid_Row{height:20px; font-family: "Times New Roman"; font-size:10pt; color:#000000; white-space:normal;}
   .importGrid_Footer{height:25px; font-family: "Times New Roman"; font-size:11pt; background-color:#f2f2f2; color:#000000; font-weight: bold;}
   .wordrap {white-space: nowrap;}
   
   .ui-position{padding-left: 10px;}  
   .ui-datepicker { font-size:10pt !important;}
   .ui-datepicker td { font-size:10pt !important; color: #549fa8; padding: 0.35em 0.2em !important; border:0px;}
   .ui-datepicker tr {border:0px;}
   .ui-datepicker-trigger{margin-left:3px; vertical-align:middle; margin-bottom:3px;}
   
   .gridfont{font-size:12px;}   
   .hidden{ display:none; }
     
   .cleare {
        padding: 0px;
        margin: 0px;
    }
    .ui-dialog 
    {
    /*background: #549fa8;*/
   
    }
    .ui-dialog-buttonpane
    {
        /*background: #ff006e;*/
    }
    .ui-dialog-buttonset
    {
        /*background: #ff0000;*/
    }
      .ui-dialog .ui-dialog-content
      {
    border: 0px;
    padding: .5em 1em;
    padding-left:50px;
    background: #fff;
    overflow: auto;
    zoom: 1;
   
      }

      .ui-dialog .ui-dialog-buttonpane .ui-dialog-buttonset .yescls
{
    font-size:15px;
    color: #fff;
    width: 50px;
    background:#355681;
    /*background:#B00020;*/
    
}
    
.GridviewScrollHeader TH, .GridviewScrollHeader TD
    {
        padding: 4px;        
        font-weight: 500;
        white-space:nowrap;
        border-right: 1px solid #AAAAAA;
        border-bottom: 1px solid #AAAAAA;
        background-color: #283850; /*#b3cccc;*/
        text-align: center;
        vertical-align: bottom;
        font-family:Arial;
        color:#fff;
        
    }
    .GridviewScrollItem TD
    {
        padding: 4px;
        white-space: nowrap;
        border-right: 1px solid #AAAAAA;
        border-bottom: 1px solid #AAAAAA;
        background-color: #E2E9EE;
        font-family:Arial;
        font-weight:100;
     
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
 
   .search
        {
            background: url('../Images/calender.png') no-repeat 99% 10%;
            padding-left: 18px;

        }
        
    .setdate{
            background: url('../Images/404-200.png') no-repeat 99% 10%;
            background-color:White;
            border : 1px solid #b3b3b3;
            height:25px;
            width:180px;
            font-size:medium;
            text-align:center;
            border-radius:0px;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }
    
     .settxt{
            border : 1px solid #b3b3b3;
            padding-left: 6px;
            height:25px;
            text-align:left;
            font-size:medium;
            width:350px;
            border-radius:0px;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            }
    
    .setdrop{
            
            background-position:99% 10%;  
            padding-left: 6px;
            border : 1px solid #b3b3b3;
            height:26px;
            width:358px;
            border-radius:0px;
            font-size:medium;
            /*margin-left:-80%;*/
           
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
          
        }

    
    .ui_comp_hide{ display:none}
    
    .btn_clz{
            background-color:#e3c353; /*#355681*/ /*#476b6b; #006666;*/
            color: #000000;
            font-size: 12px;
            width: 150px;
            font-size:16px;
            height:30px;
            border:0px;
            /*background-color: cadetblue;*/
            border-radius:0px;
            font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
        }
    .btn_clz:hover
        {
            background-color: #b3e6ff;
            color: #000;
        }
    

    .label_css
    {
      font-size:medium;  
      font-family: Arial; 
      color: #fff;/*#355681*/
      font-style:normal;
      font-size:14px;
      font-weight:300;    
       font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
    }
  

         .main {
    padding-top: 20px;
    margin-left: 10%;
    border: solid 0px;
    width: 84%;
    /*background-color: lightgreen;*/
        }

.first_div{
    margin-left:-10%;
    margin-top:-40px;
}
 .txt_box
     {
       text-align:center;   
       
       
 font-family    :  verdana, arial, snas-serif;
 font-size      :  12px;
 color          :  #000000;
 padding        :  3px;
 background     :  #f0f0f0;
 border-left    :  solid 1px #c1c1c1;
 border-top     :  solid 1px #cfcfcf;
 border-right   :  solid 1px #cfcfcf;
 border-bottom  :  solid 1px #6f6f6f;
  font-family:'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
     }

    .second_div
    {width:50%;
     margin-left:-8%;
    }
   
   
   
   
    .chk001 {
        margin-right:100px;
    }
   
   #tablemiddle td, tr {
       border: solid .5px #5e4f4e;
       
   }
    #tablemiddle {
        border-collapse: collapse;
    }
    .auto-style2 {
        width: 350px;
    }
    .auto-style3 {
        height: 21px;
    }
    .auto-style4 {
        height: 33px;
    }
    .auto-style5 {
        height: 1px;
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
    .auto-style6 {
        height: 65px;
    }

    input[type=radio] 
    {
    width: 16px;
    height: 16px;
  
    }
    .radioClz
    {
        font-size:17px;
    }
    .ChkBoxClass input {width:18px; height:18px;}
    .ddl{width:auto;}
    .form-group {
  margin-bottom: 5px;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       
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
         function searchFunction() {
             var nic = $('#<%=txt_nic.ClientID %>').val().trim(); // Get the NIC value and trim spaces

             if (nic === "") {
                 custom_alert('Please enter NIC number!', 'Alert');
                 $('#<%=lblNic.ClientID%>').html("Please enter the NIC number!");
                 return false; // Prevent form submission
             }

             // Validate NIC format
             const regex = /^([0-9]{9}[xXvV]|[0-9]{12})$/; // Adjust the regex if needed
             if (!regex.test(nic)) {
                 custom_alert('Please enter a valid NIC!', 'Alert');
                 $('#<%=lblNic.ClientID%>').html("NIC no. validation failed!");
                 return false; // Prevent form submission
             }

             return true; // Allow form submission if validation passes
         }
     </script>

      <script type="text/javascript">

          function custom_alert1(message, title) {
              if (!title)
                  title = 'Alert';

              if (!message)
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
              else if (title == 'Success') {
                  swal
                      ({
                          title: title,
                          text: message,
                          icon: "success",
                          button: true,
                          closeOnClickOutside: true,
                      });
              }

          }
      </script>
    <script type="text/javascript">
        // Function to show the modal with a success message
        function showSuccessMessage() {
            document.getElementById('customAlertMessage').innerText = 'Customer successfully created. Please proceed to fill in the policy details.';
            $('#customAlertModal').modal('show'); // Show the modal
        }

        // Function to show the modal with an error message
        function showErrorMessage(message) {
            document.getElementById('customAlertMessage').innerText = 'Error: ' + message;
            $('#customAlertModal').modal('show'); // Show the modal
        }
    </script>


    <script type="text/javascript">
        function createclientFunction() {
            var nic = $('#<%=txt_nic.ClientID %>').val();
            var initial = $('#<%=ddlInitials.ClientID %>').val();
            var propName = $('#<%=txt_nameOfProp.ClientID %>').val();
            var addline1 = $('#<%=txt_addline1.ClientID %>').val();
            var addline2 = $('#<%=txt_addline2.ClientID %>').val();
            var phone = $('#<%=txt_tele.ClientID %>').val();
            var landPhone = $('#<%=txt_landLine.ClientID %>').val();
            var dateofbirth = $('#<%=txt_dob.ClientID %>').val();

            if (initial == "0") {
                $('#<%=lblnameOfProp.ClientID%>').html("Please Select Status.");
               custom_alert('Please Select the Status.', 'Alert');
               return false;
           }


           if (propName == "") {
               $('#<%=lblnameOfProp.ClientID%>').html("Please Enter Proposer Name.");
               custom_alert('Please Enter Proposer Name.', 'Alert');
               return false;
           }

           if (nic == "") {
               custom_alert('Please enter NIC number!', 'Alert');
               $('#<%=lblNic.ClientID%>').html("Please enter valid NIC number!");
               return false;
           }

           var nicPattern = /^([0-9]{9}[x|X|v|V]|[0-9]{12})$/;
           if (!nicPattern.test(nic)) {
               custom_alert('Please enter a valid NIC!', 'Alert');
               $('#<%=lblNic.ClientID%>').html("NIC no. validation failed!");
               return false;
           }

           if (addline1 == "" || addline2 == "") {
               custom_alert('Please enter address line 1 and 2!', 'Alert');
               $('#<%=lblpostaladdress.ClientID%>').html("Please enter address line 1 and 2!");
               return false;
           }

           var phonePattern = /^(?:0|94|\+94)?(?:(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|912)(0|2|3|4|5|7|9)|7(0|1|2|5|6|7|8)\d)\d{6}$/;
           if (phone == "") {
               custom_alert('Please enter a phone number!', 'Alert');
               $('#<%=lbltelePhone.ClientID%>').html("Please enter a phone number!");
            return false;
        } else if (!phonePattern.test(phone)) {
            custom_alert('Please enter a valid phone number!', 'Alert');
            $('#<%=lbltelePhone.ClientID%>').html("Please enter a valid phone number!");
               return false;
           }

           if (landPhone != "" && !phonePattern.test(landPhone)) {
               custom_alert('Please enter a valid land line number!', 'Alert');
               $('#<%=lbllandPhone.ClientID%>').html("Please enter a valid phone number!");
               return false;
           }

           if (dateofbirth == "") {
               $('#<%=Label15.ClientID%>').html("Please Select birthday.");
                custom_alert('Please Select the birthday.', 'Alert');
                return false;
            }



            return true;
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

        $(function () {
            $("#ddlSlicCode").select2();           
        });

    function clientFunction() {
        
        var isValid = true;
        var next = "Y";
        var initial = $('#<%=ddlInitials.ClientID %>').val();
        var propName = $('#<%=txt_nameOfProp.ClientID %>').val();
        var nic = $('#<%=txt_nic.ClientID %>').val();
        var brnum= $('#<%=txt_br.ClientID %>').val();
        var addline1= $('#<%=txt_addline1.ClientID %>').val();
        var addline2= $('#<%=txt_addline2.ClientID %>').val();
        var phone = $('#<%=txt_tele.ClientID %>').val();
        var landPhone = $('#<%=txt_landLine.ClientID %>').val();
        var addline11 = $('#<%=txt_dweAdd1.ClientID %>').val();
        var addline22 = $('#<%=txt_dweAdd2.ClientID %>').val();
       
        var fromDate = $('#<%=txt_fromDate.ClientID %>').val();
        var valueSum = $('#<%=txt_sumInsu1.ClientID %>').val();
        var noFloors = $('#<%=txtNoofFloors.ClientID %>').val();
        var polTypes = $('#<%=hfpolType.ClientID %>').val();
        var solar = $('#<%=txt_solar.ClientID %>').val();
        var client = $('#<%=hiddenClientId.ClientID %>').val();

        var NsbCode = $('#<%=bank_code.ClientID %>').val();
        //alert(NsbCode);
        var slicCode = $('#<%=ddlSlicCode.ClientID %>').val();
        if (initial == "0")
        {     
            $('#<%=lblnameOfProp.ClientID%>').html("Please Select Status."); 
            custom_alert( 'Please Select the Status.', 'Alert' );
            return false;
        }

         if (propName == "")
        {     
            $('#<%=lblnameOfProp.ClientID%>').html("Please Enter Proposer Name."); 
            custom_alert( 'Please Enter Proposer Name.', 'Alert' );
            return false;
        }
  
        if (client == "") {
            $('#<%=lblNic.ClientID%>').html("Enter NIC & click Search button. Ensure client is created before proceeding.");
            custom_alert('Enter NIC & click Search button. Ensure client is created before proceeding.', 'Alert');
            return false;
        }

            if ((nic == "")) {
            custom_alert( 'Please enter NIC number!', 'Alert' );
            $('#<%=lblNic.ClientID%>').html("Please enter valid NIC number!");
            return false;

        }
       
     //if (nic  == "") {}

     if (nic  =! "") {
           var nic2 = $('#<%=txt_nic.ClientID %>').val();
           
           
                const regex = /^([0-9]{9}[x|X|v|V]|[0-9]{12})$/;
                //const regex = /^(?:19|20)?\d{2}(?:[0-35-8]\d\d(?<!(?:000|500|36[7-9]|3[7-9]\d|86[7-9]|8[7-9]\d)))\d{4}(?:[vVxX])$;
                var input = $('#<%=txt_nic.ClientID %>').val();

                if (regex.test(input)) {
                    //return true;
                }
                else {
                    custom_alert( 'Please enter a valid NIC!', 'Alert' );
                   $('#<%=lblNic.ClientID%>').html("NIC no. validation failed!");
                    return false;

                }

            }          


         if ((addline1 == "") || (addline2 == "")) {
            custom_alert( 'Please enter address line 1 and 2!', 'Alert' );
            
            $('#<%=lblpostaladdress.ClientID%>').html("Please enter address line 1 and 2!");
            return false;
        }   

        

  if (phone  == "") 
        {   custom_alert( 'Please enter a phone number!', 'Alert' );
            $('#<%=lbltelePhone.ClientID%>').html("Please enter a phone number!");
            return false;
        }

     else if(phone  =! "") {
        
                const regex = /^(?:0|94|\+94)?(?:(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|912)(0|2|3|4|5|7|9)|7(0|1|2|5|6|7|8)\d)\d{6}$/;
            
                var input = $('#<%=txt_tele.ClientID %>').val();

                if (regex.test(input)) {
                    //return true;
                }
                else {
                    custom_alert( 'Please enter a valid phone number!', 'Alert' );
                    $('#<%=lbltelePhone.ClientID%>').html("Please enter a valid phone number!");
                    return false;

                }

            }       

       
        
  if (landPhone  == "") 
        {  <%-- custom_alert( 'Please enter a phone number!', 'Alert!' );
            //alert("Please enter a phone number!");
            $('#<%= trlandPhone.ClientID%>').css("display", "");
            $('#<%=lbllandPhone.ClientID%>').html("Please enter a phone number!");
            return false;--%>
        }

     else if(landPhone  =! "") {
        
                const regex = /^(?:0|94|\+94)?(?:(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|912)(0|2|3|4|5|7|9)|7(0|1|2|5|6|7|8)\d)\d{6}$/;
            
                var input = $('#<%=txt_landLine.ClientID %>').val();

                if (regex.test(input)) {
                    //return true;
                }
                else {
                    custom_alert( 'Please enter a valid land line number!', 'Alert' );
                    $('#<%=lbllandPhone.ClientID%>').html("Please enter a valid phone number!");
                    return false;

                }

            }  

    

  if ((addline11 == "") || (addline22 == "")) {
            custom_alert( 'Please enter dwelling house address line 1 and 2!', 'Alert' );
            $('#<%=lbldwelAdd.ClientID%>').html("Please enter dwelling house address line 1 and 2!");
            return false;
        }  
      


  if ((noFloors == "")) {
            custom_alert( 'Please Enter No of Floors!', 'Alert' );
            $('#<%=lblnoFloors.ClientID%>').html("Please Enter No of Floors!");
            return false;
        }  

  if ($('#<%= rbflood.ClientID%>').prop('checked'))
  {
      //$('#<%= rbflood2.ClientID%>').removeAttr("checked");
      $('#<%= rbflood2.ClientID%>').prop('checked',false);
                 if (typeof $('#<%= chkl5.ClientID %> input:checked').val() === "undefined") {
                    //alert("Please Select a Option!");
                    custom_alert( 'Please Select a Option!', 'Alert' );
                   
                    $('#<%=lbl16Reason.ClientID%>').html("Please Select a Option!");
                    return false;
                    }
                    else{
                var selectedRB5 = $('#<%= chkl5.ClientID %> input:checked');
                var selectedValue5 = selectedRB5.val();
                var reason5 = $('#<%=txt_wordReason1.ClientID %>').val();

                if ((selectedValue5 == "1") && (reason5=="") ) {

                    //alert("Please Enter Details!");
                    custom_alert( 'Please Enter Details!', 'Alert' );
                    
                    $('#<%=lbl16_1.ClientID%>').html("Please Enter Details!");
                    return false;
    
                }
                        }
                    
           }

if (typeof $('#<%= chkl1.ClientID %> input:checked').val() === "undefined") {
                    custom_alert( 'Please Select a Construction option!', 'Alert' );
                    $('#<%=lblContact.ClientID%>').html("Please Select a Construction option!");
                    return false;
}

else if (typeof $('#<%= RbTermType.ClientID %> input:checked').val() === "undefined") {
                    //alert("Please Select an Ocupation option!");
                    custom_alert( 'Please Select a Term Type!', 'Alert' );
                    $('#<%=lblTermType.ClientID%>').html("Please Select a Term Type!");

                    return false;
}

<%--else if (typeof $('#<%= chkl3.ClientID %> input:checked').val() === "undefined") {
                    custom_alert( 'Please Select a Hazardous Ocupation Option!', 'Alert!' );
                    //alert("Please Select a Hazardous Ocupation Option!");
                    $('#<%=trHazodus.ClientID%>').css("display", "");
                    $('#<%=lblHazodus.ClientID%>').html("Please Select a Hazardous Ocupation Option!");

                    return false;
}--%>
else if (typeof $('#<%= chkl4.ClientID %> input:checked').val() === "undefined") {
                    custom_alert( 'Please Select Affected by Flood Option!', 'Alert' );
                    $('#<%=lblflood1.ClientID%>').html("Please Select Affected by Flood Option!");

                    return false;
}

else{

var selectedRB3 = $('#<%= chkl4.ClientID %> input:checked');
                var selectedValue3 = selectedRB3.val();
                var reason3 = $('#<%=txt_ninethReason.ClientID %>').val();

               
          
               if ((selectedValue3 == "1") && (reason3=="") ) {

                    //alert("Please Enter Details!");
                    custom_alert( 'Please Enter Reasons Details!', 'Alert' );
                   
                    $('#<%=lblreason3.ClientID%>').html("Please Enter Details!");
                    return false;
    
                }
                else {
                    // return true;
                } 
        }

        
         //});
         //});

         if ((fromDate == "")) {
             custom_alert( 'From date required!', 'Alert' );
            $('#<%=lbfromDat.ClientID%>').html("From date required!");
            return false;
        } 
         //$(function () {
              <%-- $('#<%= RbTermType.ClientID%>').on('click', function () {--%>
            
                 <%--  $('#<%= lblTermType.ClientID%>').text(''); --%>
        if ($('#<%= RbTermType.ClientID %> input:checked')) {
            var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');

            var selectedValue = selectedRB.val();
            if (selectedValue == "0") {
                   <%-- $('#<%=trcontact.ClientID%>').hide(); --%>     
                var term = $('#<%=ddlNumberOfYears.ClientID %>').val();

                if ((term == "0"))
                {
                    custom_alert( 'Please select No.of Years for long term', 'Alert' );
                     $('#<%=lbfromDat.ClientID%>').html("No. of Years required!");
            return false;
                }

            }
          
        }
        //solar sections ---->>>>>>>>>>>>>>>>>>>>
        if (polTypes == "2") {

            if ((valueSum == "")) {
                custom_alert('Enter Sum Value!', 'Alert');
                $('#<%=lblsumInsu1.ClientID%>').html("Required!");
                return false;
            }
            if ((solar == "")) {
                custom_alert('Enter Solar Value!', 'Alert');
                $('#<%=lblSolar.ClientID%>').html("Required!");
                return false;
            }

            if (typeof $('#<%= rbSolOne.ClientID %> input:checked').val() === "undefined")
            {
                    custom_alert( 'Please Select Solar Option!', 'Alert' );
                    $('#<%=lblrbSolOne.ClientID%>').html("Please Select the Option!");
                    return false;
            }

            if (typeof $('#<%= rbSolTwo.ClientID %> input:checked').val() === "undefined")
            {
                    custom_alert( 'Please Select Solar Option!', 'Alert' );
                    $('#<%=lblrbSolTwo.ClientID%>').html("Please Select the Option!");
                    return false;
            }

            var solarQueThree = $('#<%=txtSolarCountry.ClientID %>').val();
            if (solarQueThree == "")
            {

                    custom_alert( 'Country of Origin of Solar Panel System!', 'Alert' );
                    $('#<%=lblrbSolThree.ClientID%>').html("Please Enter Details!");
                    return false;
    
            }
            else {}

        }
        else if (polTypes == "1") {
            
            if ((valueSum == ""))
            {
                custom_alert('Enter Sum Value!', 'Alert');
                $('#<%=lblsumInsu1.ClientID%>').html("Required!");
                return false;
            }

        }
        else if (polTypes == "3") {

            if ((solar == ""))
            {
                custom_alert('Enter Solar Value!', 'Alert');
                $('#<%=lblSolar.ClientID%>').html("Required!");
                return false;
            }

             if (typeof $('#<%= rbSolOne.ClientID %> input:checked').val() === "undefined")
            {
                    custom_alert( 'Please Select Solar Option!', 'Alert' );
                    $('#<%=lblrbSolOne.ClientID%>').html("Please Select the Option!");
                    return false;
            }

            if (typeof $('#<%= rbSolTwo.ClientID %> input:checked').val() === "undefined")
            {
                    custom_alert( 'Please Select Solar Option!', 'Alert' );
                    $('#<%=lblrbSolTwo.ClientID%>').html("Please Select the Option!");
                    return false;
            }

            var solarQueThree = $('#<%=txtSolarCountry.ClientID %>').val();
            if (solarQueThree == "")
            {

                    custom_alert( 'Country of Origin of Solar Panel System!', 'Alert' );
                    $('#<%=lblrbSolThree.ClientID%>').html("Please Enter Details!");
                    return false;
    
            }
            else {}

        }
        else {}

        //nsb changes-----
        if (NsbCode == "7719") {

              var txtLoan = $('#<%=txtLoanNumber.ClientID %>').val();
            if (txtLoan == "")
            {

                    custom_alert( 'Please enter loan number!', 'Alert' );
                    $('#<%=lblLoan.ClientID%>').html("Please Enter Loan Number");
                    return false;
    
            }
            else {}         
                    
        }
        else
        {

        }

   
        <%--if (slicCode == "0")
        {     
            $('#<%=Label14.ClientID%>').html("Please Select SLIC Code."); 
            custom_alert( 'Please Select SLIC Code.', 'Alert' );
            return false;
        }--%>

        custom_alert('Processing for Premium!', 'Success');return true;
    }

    </script>
    
    
    <script>
    // VALIDATION SCRIPT FOR NUMBER.
    function isNumber(evt) {
        var iKeyCode = (evt.which) ? evt.which : evt.keyCode
        if (iKeyCode != 46 && iKeyCode > 31 && (iKeyCode < 48 || iKeyCode > 57))
            return false;

        return true;
        }    

      </script>



    <script type="text/javascript">
    function Comma(Num) { //function to add commas to textboxes
        Num += '';
        Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
        Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
        x = Num.split('.');
        x1 = x[0];
        x2 = x.length > 1 ? '.' + x[1] : '';
        var rgx = /(\d+)(\d{3})/;
        while (rgx.test(x1))
            x1 = x1.replace(rgx, '$1' + ',' + '$2');
        return x1 + x2;
    }
         </script>


     <script type="text/javascript">
         
          
        /*---------------------------auto complete ----------*/

    var toDate = new Date().format('dd/MM/yyyy');

    $(function () {
        $("input[id$='txt_fromDate']").datepicker({
            changeMonth: true,
            changeYear: false,
            showOtherMonths: true,
            yearRange: '0:+0',
            dateFormat: 'dd/mm/yy',
            defaultDate: +0,
            numberOfMonths: 1,
            maxDate: '1M',
            minDate: toDate,
            showAnim: 'slideDown',
            showButtonPanel: false,
            showWeek: true,
            firstDay: 1,
            stepMonths: 0,
            //showOn: "button",
            buttonImage: "../Images/delete.gif",
            buttonImageOnly: true,
            buttonText: "Select date",
            onSelect: function(dateText, instance) {
            date = $.datepicker.parseDate(instance.settings.dateFormat, dateText, instance.settings);
                if ($('#<%= RbTermType.ClientID %> input:checked')) {
                    var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');

                    var selectedValue = selectedRB.val();
                    if (selectedValue == "1") {
                   <%-- $('#<%=trcontact.ClientID%>').hide(); --%>     
                        date.setMonth(date.getMonth() + 12);
                        $("#txt_toDate").datepicker("setDate", date);

                    }

                }
            }
        });
    });

         /*add this mekhala*/
         $(function () {
             // Datepicker for Date of Birth
             $("input[id$='txt_dob']").datepicker({
                 changeMonth: true,
                 changeYear: true,
                 showOtherMonths: true,
                 yearRange: '-100:+0', // Allows selection of dates from 100 years ago up to the current year
                 dateFormat: 'yy-mm-dd', // Use lowercase 'mm' for months and 'yy' for a 4-digit year
                 defaultDate: -1, // Sets the default date to yesterday (optional, adjust if needed)
                 maxDate: 0, // Disallow future dates
                 showAnim: 'slideDown',
                 showButtonPanel: false,
                 showWeek: true,
                 firstDay: 1,
                 stepMonths: 1, // Navigate months one at a time
                 buttonImage: "../Images/calendar.gif", // Path to the calendar image
                 buttonImageOnly: true,
                 buttonText: "Select date",
                 onSelect: function (dateText, instance) {
                     var date = $.datepicker.parseDate(instance.settings.dateFormat, dateText, instance.settings);
                     // Additional logic can be added here if needed
                 }
             });
         });

        //end this code

    $(function () {
        $("input[id$='txt_toDate']").datepicker({
            changeMonth: true,
            changeYear: true,
            showOtherMonths: true,
            yearRange: '2016:+10',
            dateFormat: 'dd/mm/yy',
            defaultDate: +0,
            numberOfMonths: 1,
            //maxDate: toDate,
            showAnim: 'slideDown',
            showButtonPanel: false,
            showWeek: true,
            firstDay: 1,
            stepMonths: 0,
            showOn: "off",
            buttonImage: "../Images/delete.png",
            buttonImageOnly: true,
            buttonText: "Select date",
           onSelect: function (selectedDate) {
               $("#txt_fromDate").datepicker("option", "maxDate", selectedDate);

            }
        });
         });

         $(function () {
      $('#<%= ddlInitials.ClientID%>').on('click', function () {
        
        <%--$('#<%= lblnameOfProp.ClientID%>').css("display", "none");    --%> 
          $('#<%= lblnameOfProp.ClientID%>').text('');
       
     });
         });

          $(function () {
      $('#<%= txt_nameOfProp.ClientID%>').on('change keyup paste', function () {
        
        <%--$('#<%= lblnameOfProp.ClientID%>').css("display", "none");    --%> 
          $('#<%= lblnameOfProp.ClientID%>').text('');
       
     });
         });


          $(function () {
      $('#<%= txt_nic.ClientID%>').on('change keyup paste', function () {
        
         <%-- $('#<%= lblNic.ClientID%>').css("display", "");   --%>
        <%--  document.getElementById("<%=lblNic.ClientID %>").value = '';--%>
          $('#<%= lblNic.ClientID%>').text('');
       
     });
         });

          $(function () {
      $('#<%= txt_br.ClientID%>').on('change keyup paste', function () {
        
       <%-- $('#<%= lblNic.ClientID%>').css("display", "");   --%>  
          <%-- document.getElementById("<%=lblNic.ClientID %>").value = '';--%>
          $('#<%= lblNic.ClientID%>').text('');
       
     });
         });

         $(function () {
      $('#<%= txt_addline1.ClientID%>').on('change keyup paste', function () {
        
        
          $('#<%= lblpostaladdress.ClientID%>').text('');
       
     });
         });
         $(function () {
      $('#<%= txt_addline2.ClientID%>').on('change keyup paste', function () {
        
         $('#<%= lblpostaladdress.ClientID%>').text('');   
       
     });
         });


          $(function () {
      $('#<%= txt_tele.ClientID%>').on('change keyup paste', function () {
        
          $('#<%= lbltelePhone.ClientID%>').text('');  
       
     });
         });


         $(function () {
      $('#<%= txt_landLine.ClientID%>').on('change keyup paste', function () {
        
         $('#<%= lbllandPhone.ClientID%>').text('');  
       
     });
         });



           $(function () {
      $('#<%= txt_dweAdd1.ClientID%>').on('change keyup paste', function () {
        
        $('#<%= lbldwelAdd.ClientID%>').text('');      
       
     });
         });
         $(function () {
      $('#<%= txt_dweAdd2.ClientID%>').on('change keyup paste', function () {
        
        $('#<%= lbldwelAdd.ClientID%>').text('');    
       
     });
         });

       
           $(function () {
        $('#<%= txt_fromDate.ClientID%>').on('click', function () {
         
            $('#<%= lbfromDat.ClientID%>').text('');  
         });
     });

           $(function () {
        $('#<%= txt_toDate.ClientID%>').on('click', function () {
           $('#<%= lbfromDat.ClientID%>').text('');  
         });
          });


         
           $(function () {
               $('#<%= chkl1.ClientID%>').on('click', function () {
            
            $('#<%= lblContact.ClientID%>').text(''); 
         });
         });


         $(function () {
               $('#<%= RbTermType.ClientID%>').on('click', function () {
            
                   $('#<%= lblTermType.ClientID%>').text(''); 
                   document.getElementById("<%=ddlNumberOfYears.ClientID %>").value = '0';
                   var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');
                   document.getElementById("<%=txt_fromDate.ClientID %>").value = '';
                   document.getElementById("<%=txt_toDate.ClientID %>").value = '';
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                   <%-- $('#<%=trcontact.ClientID%>').hide(); --%>     
                    $('#<%= lblTermType.ClientID%>').text(''); 
                    $('#<%=DivTerm.ClientID%>').hide(); 
                }
                else {
                    $('#<%= lblTermType.ClientID%>').text(''); 
                   $('#<%=DivTerm.ClientID%>').show(); 
                    <%--document.getElementById("<%=txt_reason1.ClientID %>").value = '';--%>
                }    

         });
         });


    //term Year select
         

           $(function ()
            {$("#ddlNumberOfYears, #txt_fromDate").click( function () {
                <%--$('#<%= ddlNumberOfYears.ClientID%>','#<%= txt_fromDate.ClientID%>').change(function ()  {--%>
                    
        var str = $('#<%=txt_fromDate.ClientID%>').val();

        if( /^\d{2}\/\d{2}\/\d{4}$/i.test( str ) ) {

            var parts = str.split("/");

            var day = parts[0] && parseInt( parts[0], 10 );
            var month = parts[1] && parseInt( parts[1], 10 );
            var year = parts[2] && parseInt( parts[2], 10 );
            var duration = parseInt( $('#<%= ddlNumberOfYears.ClientID%>').val(), 10);

            if( day <= 31 && day >= 1 && month <= 12 && month >= 1 ) {

                var expiryDate = new Date( year, month - 1, day );
                expiryDate.setFullYear( expiryDate.getFullYear() + duration );

                var day = ( '0' + expiryDate.getDate() ).slice( -2 );
                var month = ( '0' + ( expiryDate.getMonth() + 1 ) ).slice( -2 );
                var year = expiryDate.getFullYear();

                $('#<%=txt_toDate.ClientID%>').val( day + "/" + month + "/" + year );

            } else {
                // display error message
            }
        }
   

  
                });

            });

            //end---




          $(function () {
              $('#<%= chkl4.ClientID%>').on('click', function () {
                  
                  $('#<%= lblflood1.ClientID%>').text('');  
         });
         });

         $(function () {
      $('#<%= txt_wordReason1.ClientID%>').on('change keyup paste', function () {
        
       $('#<%=lbl16_1.ClientID%>').text('');
       
     });
         });
     
            $(function () {
      $('#<%= txt_sumInsu1.ClientID%>').on('change keyup paste', function () {
          
           $('#<%= lblsumInsu1.ClientID%>').text('');  
     });
         });


              $(function () {
      $('#<%= txt_sumInsu2.ClientID%>').on('change keyup paste', function () {
       $('#<%= lblsumInsu2.ClientID%>').text(''); 
       
     });
         });


           $(function () {
      $('#<%= txt_solar.ClientID%>').on('change keyup paste', function () {
       $('#<%= lblSolar.ClientID%>').text(''); 
       
     });
         });


         $(function () {
             
             $('#<%=txt_solar.ClientID%>').on("change keyup paste", function () {
                 var a = 0;
                 var b = 0;
                 var c = 0;
                 a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, ""));
                 b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, ""));
                 c = parseFloat($('#txt_solar').val().replace(/,/g, ""));

                 if (a) { a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, "")); } else { a = 0; }
                 if (b) { b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, "")); } else { b = 0; }
                 if (c) { c = parseFloat($('#txt_solar').val().replace(/,/g, "")); } else { c = 0; }
                 var sum = a + b + c;
                 document.getElementById('<%=txt_sumInsuTotal.ClientID%>').value = Comma(sum);
                 //alert(sum);
             });
          }); 



         $(function () {
             $('#<%=txt_sumInsu2.ClientID%>').on("change keyup paste", function () {
                 var a = 0;
                 var b = 0;
                 var c = 0;
                 a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, ""));
                 b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, ""));
                 c = parseFloat($('#txt_solar').val().replace(/,/g, ""));

                 if (a) { a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, "")); } else { a = 0; }
                 if (b) { b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, "")); } else { b = 0; }
                 if (c) { c = parseFloat($('#txt_solar').val().replace(/,/g, "")); } else { c = 0; }
                 var sum = a + b + c;
                 document.getElementById('<%=txt_sumInsuTotal.ClientID%>').value = Comma(sum);
                 //alert(sum);
             });
          });        

      $(function () {
             $('#<%=txt_sumInsu1.ClientID%>').on("change keyup paste", function () {
                 var a = 0;
                 var b = 0;
                 var c = 0;
                 a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, ""));
                 b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, ""));
                 c = parseFloat($('#txt_solar').val().replace(/,/g, ""));

                 if (a) { a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, "")); } else { a = 0; }
                 if (b) { b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, "")); } else { b = 0; }
                 if (c) { c = parseFloat($('#txt_solar').val().replace(/,/g, "")); } else { c = 0; }
                 var sum = a + b + c;
                 document.getElementById('<%=txt_sumInsuTotal.ClientID%>').value = Comma(sum);
                 //alert(sum);
             });
          });    

      
      $(function () {
            $('#<%= chkl4.ClientID %> input').change(function () {
               

                var selectedRB = $('#<%= chkl4.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                    $('#<%=Div23.ClientID%>').show();                   
                }
                else {
                    $('#<%=Div23.ClientID%>').hide(); 
                    document.getElementById("<%=txt_ninethReason.ClientID %>").value = '';
                }                 
                
                 });
              });   

      $(function () {
      $('#<%= txt_ninethReason.ClientID%>').on('change keyup paste', function () {
          $('#<%= lblreason3.ClientID%>').text(''); 
     });
            });


       $(function () {
              $('#<%= rbflood.ClientID%>').on('click', function () {
                 
                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                      if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                          $('#<%= rbflood2.ClientID%>').prop('checked',false);
                          //$('#<%= rbflood2.ClientID%>').removeAttr("checked");
                          $('#<%=Div27.ClientID%>').show();

                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          //radiolist.removeAttr('checked');
                          radiolist.prop('checked',false);
                          //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");

                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                        <%--$('#<%=tr17.ClientID%>').hide(); --%>
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');

                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          //radiolist3.removeAttr('checked');

                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked',false);


                          //$("table[id$=txt_wordReason2] input:radio:checked").removeAttr("checked");
                          //$("table[id$=txt_wordReason3] input:radio:checked").removeAttr("checked");
                          //$("table[id$=txt_wordReason4] input:radio:checked").removeAttr("checked");

                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                          $("table[id$=chkl5] input:radio:checked").prop('checked',false);
                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                      <%--$('#<%=tr17.ClientID%>').hide(); --%>
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');

                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked',false);
                          //$("table[id$=txt_wordReason2] input:radio:checked").removeAttr("checked");
                          //$("table[id$=txt_wordReason3] input:radio:checked").removeAttr("checked");
                          //$("table[id$=txt_wordReason4] input:radio:checked").removeAttr("checked");
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                      }
                  }
           });
        });

         <%--$(function () {
              $('#<%= rbflood.ClientID%>').on('click', function () {
                  alert("****");
                   $('#<%=chkl5.ClientID %>').find("input[value='1']").attr("checked", "checked");
              });
                 });--%>


             $(function () {
            $('#<%= chkl5.ClientID %> input').change(function () {
               
                $('#<%=lbl16Reason.ClientID%>').text('');   
                $('#<%=lbl16_1.ClientID%>').text('');

                var selectedRB = $('#<%= chkl5.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                    $('#<%=Div28.ClientID%>').show();   
                    $('#<%=Div29.ClientID%>').show();  
                    $('#<%=Div30.ClientID%>').show();  
                    $('#<%=Div31.ClientID%>').show();  
                }
                else {
                    
                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                    document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked',false);

                }                 
                
                 });
              });   

  $(function () {
        $('#<%= txtNoofFloors.ClientID%>').on('change keyup paste', function () {
            
            $('#<%= lblnoFloors.ClientID%>').text(''); 

            });
         });


$(function () {
     
               var selectedRB = $('#<%= chkl1.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                   <%-- $('#<%=trcontact.ClientID%>').hide(); --%>     
                    $('#<%= lblContact.ClientID%>').text(''); 
                }
                else {
                   $('#<%= lblContact.ClientID%>').text(''); 
                    <%--document.getElementById("<%=txt_reason1.ClientID %>").value = '';--%>
                }                 
                
                
         }); 


         $(function () {
     
             var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');
             
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                   <%-- $('#<%=trcontact.ClientID%>').hide(); --%>     
                    $('#<%= lblTermType.ClientID%>').text(''); 
                    
                }
                else {
                    $('#<%= lblTermType.ClientID%>').text(''); 
                   
                    <%--document.getElementById("<%=txt_reason1.ClientID %>").value = '';--%>
                }                 
                
                
         }); 



          $(function () {
           
               
                var selectedRB = $('#<%= chkl4.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                    $('#<%=Div23.ClientID%>').show();                   
                }
                else {
                    $('#<%=Div23.ClientID%>').hide(); 
                   
                    document.getElementById("<%=txt_ninethReason.ClientID %>").value = '';
                }                 
                
              });  



          $(function () {
           
               
                var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "0") {
                    $('#<%=DivTerm.ClientID%>').show();                   
                }
                else {
                    $('#<%=DivTerm.ClientID%>').hide(); 
                   
                   
                }                 
                
              });  



         $(function () {
             
                 

                  if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                      $('#<%=Div27.ClientID%>').show();
                     
                      
                      <%--$("table[id$=chkl5] input:radio:checked").removeAttr("checked");

                        $('#<%=tr6.ClientID%>').hide();    
                    $('#<%=tr7.ClientID%>').hide();  
                    $('#<%=tr8.ClientID%>').hide();  
                    $('#<%=tr9.ClientID%>').hide(); 
                    document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                  }
                  else {
                      $('#<%=Div27.ClientID%>').hide(); 
                      
                      <%--$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                        $('#<%=tr6.ClientID%>').hide();    
                    $('#<%=tr7.ClientID%>').hide();  
                    $('#<%=tr8.ClientID%>').hide();  
                    $('#<%=tr9.ClientID%>').hide(); 
                     
                    
                    document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                  }

           
         });

          //flood no option



          $(function () {
              $('#<%= rbflood2.ClientID%>').on('click', function () {
                 

                  if ($('#<%= rbflood2.ClientID%>').prop('checked')) {
                     
                      $('#<%=Div27.ClientID%>').hide();

                       //$('#<%= rbflood.ClientID%>').removeAttr("checked");
                      $('#<%= rbflood.ClientID%>').prop('checked',false);

                      var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                     radiolist.removeAttr('checked');
                     //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");

                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                      document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     //radiolist1.removeAttr('checked');
                     //radiolist2.removeAttr('checked');
                     //radiolist3.removeAttr('checked');


                      radiolist1.prop('checked', false);
                      radiolist2.prop('checked', false);
                      radiolist3.prop('checked',false);

                   <%-- document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                  }
                  else {                 
                       $('#<%=Div27.ClientID%>').hide();

                      var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                     radiolist.removeAttr('checked');
                     //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                    $('#<%= rbflood.ClientID%>').removeAttr("checked");
                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                      document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     //radiolist1.removeAttr('checked');
                     //radiolist2.removeAttr('checked');
                      //radiolist3.removeAttr('checked');

                       radiolist1.prop('checked', false);
                      radiolist2.prop('checked', false);
                      radiolist3.prop('checked',false);
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>

                  }
           });
        });



       


             $(function () {        

                var selectedRB = $('#<%= chkl5.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                    $('#<%=Div28.ClientID%>').show();   
                    $('#<%=Div29.ClientID%>').show();  
                    $('#<%=Div30.ClientID%>').show();  
                    $('#<%=Div31.ClientID%>').show();  
                }
                else {
                    
                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                    document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     //radiolist1.removeAttr('checked');
                     //radiolist2.removeAttr('checked');
                     //radiolist3.removeAttr('checked');

                      radiolist1.prop('checked', false);
                      radiolist2.prop('checked', false);
                      radiolist3.prop('checked',false);

                }                                             
              }); 

         //address same ticked

         $(function () {
              $('#<%= chkSameAdd.ClientID%>').on('click', function () {
                 $('#<%= lbldwelAdd.ClientID%>').text('');   

                  if ($('#<%= chkSameAdd.ClientID%>').prop('checked')) {
                     
                      document.getElementById("<%=txt_dweAdd1.ClientID %>").value = document.getElementById("<%=txt_addline1.ClientID %>").value;
                      document.getElementById("<%=txt_dweAdd2.ClientID %>").value = document.getElementById("<%=txt_addline2.ClientID %>").value;
                    document.getElementById("<%=txt_dweAdd3.ClientID %>").value = document.getElementById("<%=txt_addline3.ClientID %>").value;
                      document.getElementById("<%=txt_dweAdd4.ClientID %>").value = document.getElementById("<%=txt_addline4.ClientID %>").value;
                  }
                  else {                 
                      document.getElementById("<%=txt_dweAdd1.ClientID %>").value = '';
                      document.getElementById("<%=txt_dweAdd2.ClientID %>").value = '';
                      document.getElementById("<%=txt_dweAdd3.ClientID %>").value = '';
                      document.getElementById("<%=txt_dweAdd4.ClientID %>").value = 0;
                       }
           });
        });

         //check box Selection
          $(function () {
              $('#<%= chkOtherPerils.ClientID%>').on('click', function () {

                  <%--$('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                  $('#<%= chkTR.ClientID%>').removeAttr("checked");
                  $('#<%= rbflood.ClientID%>').removeAttr("checked");--%>


                      $('#<%= chkSRCC.ClientID%>').prop('checked', false);
                      $('#<%= chkTR.ClientID%>').prop('checked', false);
                      $('#<%= rbflood.ClientID%>').prop('checked',false);


                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                      if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                          $('#<%=Div27.ClientID%>').show();
                         <%-- $('#<%= rbflood2.ClientID%>').removeAttr("checked");--%>
                           $('#<%= rbflood2.ClientID%>').prop('checked',false);

                          //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          //radiolist.removeAttr('checked');
                          radiolist.prop('checked', false);

                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                        <%-- $('#<%=tr17.ClientID%>').hide(); --%>
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');
                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          //radiolist3.removeAttr('checked');
                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                          $("table[id$=chkl5] input:radio:checked").prop('checked', false);
                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');
                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          //radiolist3.removeAttr('checked');
                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);
                      }
                
                  }
                  else {

                      <%--$('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      $('#<%= chkTR.ClientID%>').removeAttr("checked");
                      $('#<%= rbflood.ClientID%>').removeAttr("checked");--%>

                      $('#<%= chkSRCC.ClientID%>').prop('checked', false);
                      $('#<%= chkTR.ClientID%>').prop('checked', false);
                      $('#<%= rbflood.ClientID%>').prop('checked', false);



                       if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                          $('#<%=Div27.ClientID%>').show();
                          <%--$('#<%= rbflood2.ClientID%>').removeAttr("checked");--%>
                           $('#<%= rbflood2.ClientID%>').prop('checked', false);

                          //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                           var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                           
                          //radiolist.removeAttr('checked');
                           radiolist.prop('checked', false);

                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                        <%-- $('#<%=tr17.ClientID%>').hide(); --%>
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');
                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          // radiolist3.removeAttr('checked');

                           radiolist1.prop('checked', false);
                           radiolist2.prop('checked', false);
                           radiolist3.prop('checked', false);
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                           //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                           $("table[id$=chkl5] input:radio:checked").prop('checked', false);
                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');
                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          //radiolist3.removeAttr('checked');
                           radiolist1.prop('checked', false);
                           radiolist2.prop('checked', false);
                           radiolist3.prop('checked', false);
                      }
                  }

           });
         });

         $(function () {
              $('#<%= chkSRCC.ClientID%>').on('click', function () {
                 
                  //$('#<%= chkTR.ClientID%>').removeAttr("checked");
                  $('#<%= chkTR.ClientID%>').prop('checked',false);
                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                      //$('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      //$('#<%= chkTR.ClientID%>').removeAttr("checked");
                      $('#<%= chkSRCC.ClientID%>').prop('checked', false);
                      $('#<%= chkTR.ClientID%>').prop('checked',false);
                     custom_alert( 'Please Select Other perils to get SRCC', 'Alert' );
                      
                  }

           });
        });

          $(function () {
              $('#<%= chkTR.ClientID%>').on('click', function () {
                 
                  

                  if ($('#<%= chkSRCC.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                      //$('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      //$('#<%= chkTR.ClientID%>').removeAttr("checked");
                      $('#<%= chkSRCC.ClientID%>').prop('checked', false);
                      $('#<%= chkTR.ClientID%>').prop('checked',false);


                      <%--$('#<%= chkOtherPerils.ClientID%>').removeAttr("checked");--%>
                     custom_alert( 'Please Select SRCC to get TR', 'Alert' );
                      
                  }

           });
        });


          $(function () {
              $('#<%= rbflood.ClientID%>').on('click', function () {
                 
                 

                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                     // $('#<%= rbflood.ClientID%>').removeAttr("checked");
                       $('#<%= rbflood.ClientID%>').prop('checked',false);
                   
                     custom_alert( 'Please Select Other perils to get flood cover', 'Alert' );
                      
                  }

           });
        });

//solar section
          $(function () {
              $('#<%= rbSolOne.ClientID%>').on('click', function () {
                  
                  $('#<%= lblrbSolOne.ClientID%>').text('');  
              });
          });

          $(function () {
              $('#<%= rbSolTwo.ClientID%>').on('click', function () {
                  
                  $('#<%= lblrbSolTwo.ClientID%>').text('');  
              });
          });

          $(function () {
      $('#<%= txtSolarCountry.ClientID%>').on('change keyup paste', function () {
          $('#<%= lblrbSolThree.ClientID%>').text(''); 
     });
            });

         //---NSB--changes
           $(function () {
      $('#<%= txtLoanNumber.ClientID%>').on('change keyup paste', function () {
        
       <%-- $('#<%= lblNic.ClientID%>').css("display", "");   --%>  
          <%-- document.getElementById("<%=lblNic.ClientID %>").value = '';--%>
          $('#<%= lblLoan.ClientID%>').text('');
       
     });
         });

         ///------

         ///----26042023-changes
         $(function () {
      $('#<%= ddlSlicCode.ClientID%>').on('click', function () {
        
        <%--$('#<%= lblnameOfProp.ClientID%>').css("display", "none");    --%> 
          $('#<%= Label14.ClientID%>').text('');
       
     });
         });
         ///--

         function UserOrEmailAvailability() { //This function call on text change.   

             var obj = {};
             obj.useroremail = $.trim($('#<%=txt_nic.ClientID%>').val());
             obj.userMobile = $.trim($('#<%=txt_tele.ClientID%>').val());

            $.ajax({  
                type: "POST",  
                url: "ProposalEntry.aspx/CheckEmail", // this for calling the web method function in cs code.
                data: JSON.stringify(obj),
                
                contentType: "application/json; charset=utf-8",  
                dataType: "json",  
                success: OnSuccess,  
                failure: function (response) {  
                    alert(response);  
                }  
            });  
        }  
  
        // function OnSuccess  
        function OnSuccess(response) {  
            var msg = $("#<%=lblStatus.ClientID%>")[0];
             
             if (response.d == "true")
            {
                msg.style.display = "block";  
                msg.style.color = "red";  
                msg.innerHTML = "Kindly note that we are required to further review customer's information and we will revert soon. Page will reload.";

                swal({
                    title: "AML Warning",
                    text: "Kindly note that we are required to further review customer's information and we will revert soon. Page will reload.",
                    type:
                        "warning"
                }).then(function () {
                    location.reload();
                }
                );

            }
            else
            {

            }

           
        } 
        

         //end


    $(document).ready(function () {

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {

            $(function () {
                $("#ddlSlicCode").select2();
            });

            $(function () {
                $("input[id$='txt_fromDate']").datepicker({
                    changeMonth: true,
                    changeYear: false,
                    showOtherMonths: true,
                    yearRange: '0:+0',
                    dateFormat: 'dd/mm/yy',
                    defaultDate: +0,
                    numberOfMonths: 1,
                    maxDate: '1M',
                    minDate: toDate,
                    showAnim: 'slideDown',
                    showButtonPanel: false,
                    showWeek: true,
                    firstDay: 1,
                    stepMonths: 0,
                    //showOn: "button",
                    buttonImage: "../Images/delete.gif",
                    buttonImageOnly: true,
                    buttonText: "Select date",
                    onSelect: function(dateText, instance) {
                        date = $.datepicker.parseDate(instance.settings.dateFormat, dateText, instance.settings);


        if ($('#<%= RbTermType.ClientID %> input:checked')) {
            var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');

            var selectedValue = selectedRB.val();
            if (selectedValue == "1") {
                   <%-- $('#<%=trcontact.ClientID%>').hide(); --%>     
                date.setMonth(date.getMonth() + 12);
                $("#txt_toDate").datepicker("setDate", date);

            }
          
             }
            }
                });
            });


            $(function () {
                $("input[id$='txt_toDate']").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    showOtherMonths: true,
                    yearRange: '2016:+10',
                    dateFormat: 'dd/mm/yy',
                    defaultDate: +0,
                    numberOfMonths: 1,
                    //maxDate: toDate,
                    showAnim: 'slideDown',
                    showButtonPanel: false,
                    showWeek: true,
                    firstDay: 1,
                    stepMonths: 0,
                    //showOn: "button",
                    showOn: "off",
                    buttonImage: "../Images/delete.png",
                    buttonImageOnly: true,
                    buttonText: "Select date",
                    onSelect: function (selectedDate) {
                $("#txt_fromDate").datepicker("option", "maxDate", selectedDate);
            } 
                });

            });
   

 $(function () {
      $('#<%= ddlInitials.ClientID%>').on('click', function () {
        
        <%--$('#<%= lblnameOfProp.ClientID%>').css("display", "none");    --%> 
          $('#<%= lblnameOfProp.ClientID%>').text('');
       
     });
         });
           $(function () {
      $('#<%= txt_nameOfProp.ClientID%>').on('change keyup paste', function () {
        
         $('#<%= lblnameOfProp.ClientID%>').text('');   
       
     });
            });

             $(function () {
      $('#<%= txt_nic.ClientID%>').on('change keyup paste', function () {
        
        <%--$('#<%= lblNic.ClientID%>').css("display", "");    --%> 
           <%--document.getElementById("<%=lblNic.ClientID %>").value = '';--%>
          $('#<%= lblNic.ClientID%>').text('');
       
     });
         });

          $(function () {
      $('#<%= txt_br.ClientID%>').on('change keyup paste', function () {
        
        <%--$('#<%= lblNic.ClientID%>').css("display", "");   --%>  
           <%--document.getElementById("<%=lblNic.ClientID %>").value = '';--%>
          $('#<%= lblNic.ClientID%>').text('');
       
     });
          });


         $(function () {
      $('#<%= txt_addline1.ClientID%>').on('change keyup paste', function () {
        
        $('#<%= lblpostaladdress.ClientID%>').text('');   
       
             });

         });
         $(function () {
      $('#<%= txt_addline2.ClientID%>').on('change keyup paste', function () {
        
        $('#<%= lblpostaladdress.ClientID%>').text('');  
       
     });
         });

             $(function () {
      $('#<%= txt_tele.ClientID%>').on('change keyup paste', function () {
        
         $('#<%= lbltelePhone.ClientID%>').text('');   
       
     });
            });

             $(function () {
      $('#<%= txt_landLine.ClientID%>').on('change keyup paste', function () {
        
         $('#<%= lbllandPhone.ClientID%>').text('');  
       
     });
         });


 $(function () {
      $('#<%= txt_dweAdd1.ClientID%>').on('change keyup paste', function () {
        
        $('#<%= lbldwelAdd.ClientID%>').text('');       
       
     });
         });
         $(function () {
      $('#<%= txt_dweAdd2.ClientID%>').on('change keyup paste', function () {
        
        $('#<%= lbldwelAdd.ClientID%>').text('');      
       
     });
         });




           $(function () {
        $('#<%= txt_fromDate.ClientID%>').on('click', function () {
           $('#<%= lbfromDat.ClientID%>').text('');  
         });
     });

           $(function () {
        $('#<%= txt_toDate.ClientID%>').on('click', function () {
          $('#<%= lbfromDat.ClientID%>').text('');  
         });
            });

             $(function () {
        $('#<%= chkl1.ClientID%>').on('click', function () {
         $('#<%= lblContact.ClientID%>').text(''); 
         });
            });

          $(function () {
              $('#<%= RbTermType.ClientID%>').on('click', function () {
                  $('#<%= lblTermType.ClientID%>').text(''); 
                  document.getElementById("<%=ddlNumberOfYears.ClientID %>").value = '0';
                   document.getElementById("<%=txt_fromDate.ClientID %>").value = '';
                   document.getElementById("<%=txt_toDate.ClientID %>").value = '';

            var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');

                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                   <%-- $('#<%=trcontact.ClientID%>').hide(); --%>     
                    $('#<%= lblTermType.ClientID%>').text('');
                    $('#<%=DivTerm.ClientID%>').hide(); 
                }
                else {
                    $('#<%= lblTermType.ClientID%>').text(''); 
                    $('#<%=DivTerm.ClientID%>').show(); 
                    <%--document.getElementById("<%=txt_reason1.ClientID %>").value = '';--%>
                }    
         $('#<%= lblTermType.ClientID%>').text(''); 
         });
            });

            //term Year select
         

           $(function ()
            {   $("#ddlNumberOfYears, #txt_fromDate").click( function () {
                <%--$('#<%= ddlNumberOfYears.ClientID%>','#<%= txt_fromDate.ClientID%>').change(function ()  {--%>
                    
        var str = $('#<%=txt_fromDate.ClientID%>').val();

        if( /^\d{2}\/\d{2}\/\d{4}$/i.test( str ) ) {

            var parts = str.split("/");

            var day = parts[0] && parseInt( parts[0], 10 );
            var month = parts[1] && parseInt( parts[1], 10 );
            var year = parts[2] && parseInt( parts[2], 10 );
            var duration = parseInt( $('#<%= ddlNumberOfYears.ClientID%>').val(), 10);

            if( day <= 31 && day >= 1 && month <= 12 && month >= 1 ) {

                var expiryDate = new Date( year, month - 1, day );
                expiryDate.setFullYear( expiryDate.getFullYear() + duration );

                var day = ( '0' + expiryDate.getDate() ).slice( -2 );
                var month = ( '0' + ( expiryDate.getMonth() + 1 ) ).slice( -2 );
                var year = expiryDate.getFullYear();

                $('#<%=txt_toDate.ClientID%>').val( day + "/" + month + "/" + year );

            } else {
                // display error message
            }
        }
   

  
                });

            });

            //end---

              $(function () {
        $('#<%= chkl4.ClientID%>').on('click', function () {
           $('#<%= lblflood1.ClientID%>').text('');  

         });
       });

       



            $(function () {
      $('#<%= txt_wordReason1.ClientID%>').on('change keyup paste', function () {
        
       $('#<%=lbl16_1.ClientID%>').text('');
       
     });
         });

            $(function () {
      $('#<%= txt_sumInsu1.ClientID%>').on('change keyup paste', function () {
        
         $('#<%= lblsumInsu1.ClientID%>').text('');    
       
     });
            });


            $(function () {
      $('#<%= txt_sumInsu2.ClientID%>').on('change keyup paste', function () {
        
        $('#<%= lblsumInsu2.ClientID%>').text('');  
       
     });
            });

             $(function () {
      $('#<%= txt_solar.ClientID%>').on('change keyup paste', function () {
       $('#<%= lblSolar.ClientID%>').text(''); 
       
     });
         });

         $(function () {
             
             $('#<%=txt_solar.ClientID%>').on("change keyup paste", function () {
                 var a = 0;
                 var b = 0;
                 var c = 0;
                 a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, ""));
                 b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, ""));
                 c = parseFloat($('#txt_solar').val().replace(/,/g, ""));

                 if (a) { a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, "")); } else { a = 0; }
                 if (b) { b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, "")); } else { b = 0; }
                 if (c) { c = parseFloat($('#txt_solar').val().replace(/,/g, "")); } else { c = 0; }
                 var sum = a + b + c;
                 document.getElementById('<%=txt_sumInsuTotal.ClientID%>').value = Comma(sum);
                 //alert(sum);
             });
          }); 



         $(function () {
             $('#<%=txt_sumInsu2.ClientID%>').on("change keyup paste", function () {
                 var a = 0;
                 var b = 0;
                 var c = 0;
                 a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, ""));
                 b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, ""));
                 c = parseFloat($('#txt_solar').val().replace(/,/g, ""));

                 if (a) { a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, "")); } else { a = 0; }
                 if (b) { b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, "")); } else { b = 0; }
                 if (c) { c = parseFloat($('#txt_solar').val().replace(/,/g, "")); } else { c = 0; }
                 var sum = a + b + c;
                 document.getElementById('<%=txt_sumInsuTotal.ClientID%>').value = Comma(sum);
                 //alert(sum);
             });
          });        

      $(function () {
             $('#<%=txt_sumInsu1.ClientID%>').on("change keyup paste", function () {
                 var a = 0;
                 var b = 0;
                 var c = 0;
                 a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, ""));
                 b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, ""));
                 c = parseFloat($('#txt_solar').val().replace(/,/g, ""));

                 if (a) { a = parseFloat($('#txt_sumInsu1').val().replace(/,/g, "")); } else { a = 0; }
                 if (b) { b = parseFloat($('#txt_sumInsu2').val().replace(/,/g, "")); } else { b = 0; }
                 if (c) { c = parseFloat($('#txt_solar').val().replace(/,/g, "")); } else { c = 0; }
                 var sum = a + b + c;
                 document.getElementById('<%=txt_sumInsuTotal.ClientID%>').value = Comma(sum);
                 //alert(sum);
             });
          });    
         
             $(function () {
            $('#<%= chkl4.ClientID %> input').change(function () {
               
                var selectedRB = $('#<%= chkl4.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                    $('#<%=Div23.ClientID%>').show();                   
                }
                else {
                    $('#<%=Div23.ClientID%>').hide(); 
                   
                    document.getElementById("<%=txt_ninethReason.ClientID %>").value = '';
                }                 
                
                 });
              });   


        
            
            $(function () {
      $('#<%= txt_ninethReason.ClientID%>').on('change keyup paste', function () {
        
         $('#<%= lblreason3.ClientID%>').text(''); 
       
     });
            });

            
           
         
         $(function () {
              $('#<%= rbflood.ClientID%>').on('click', function () {
                 
                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                      if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                          $('#<%=Div27.ClientID%>').show();
                          <%--$('#<%= rbflood2.ClientID%>').removeAttr("checked");--%>
                          $('#<%= rbflood2.ClientID%>').prop('checked', false);

                          //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          //radiolist.removeAttr('checked');
                          radiolist.prop('checked', false);
                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                        <%-- $('#<%=tr17.ClientID%>').hide(); --%>
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');
                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          //radiolist3.removeAttr('checked');

                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                          $("table[id$=chkl5] input:radio:checked").prop('checked', false);
                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');
                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          //radiolist3.removeAttr('checked');
                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                           radiolist3.prop('checked', false);
                      }
                  }
           });
        });


         <%--     $(function () {
              $('#<%= rbflood.ClientID%>').on('click', function () {
                  alert("****");
            $('#<%=chkl5.ClientID %>').find("input[value='1']").attr("checked", "checked");
              });
                 });--%>



             $(function () {
            $('#<%= chkl5.ClientID %> input').change(function () {
               
                $('#<%=lbl16Reason.ClientID%>').text(''); 
                $('#<%=lbl16_1.ClientID%>').text('');
                var selectedRB = $('#<%= chkl5.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                    $('#<%=Div28.ClientID%>').show();   
                    $('#<%=Div29.ClientID%>').show();  
                    $('#<%=Div30.ClientID%>').show();  
                    $('#<%=Div31.ClientID%>').show();  
                }
                else {
                    
                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                    document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     //radiolist1.removeAttr('checked');
                     //radiolist2.removeAttr('checked');
                     //radiolist3.removeAttr('checked');

                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);

                }                 
                
                 });
              });   

        $(function () {
        $('#<%= txtNoofFloors.ClientID%>').on('change keyup paste', function () {
        
        $('#<%= lblnoFloors.ClientID%>').text('');     
       
            });
         });



$(function () {
     
               var selectedRB = $('#<%= chkl1.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                 $('#<%= lblContact.ClientID%>').text('');                   
                }
                else {
                    $('#<%= lblContact.ClientID%>').text(''); 
                    <%--document.getElementById("<%=txt_reason1.ClientID %>").value = '';--%>
                }                 
                
                
         }); 


$(function () {
     
    var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');

                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                   <%-- $('#<%=trcontact.ClientID%>').hide(); --%>     
                    $('#<%= lblTermType.ClientID%>').text(''); 
                  
                }
                else {
                    $('#<%= lblTermType.ClientID%>').text(''); 
                
                    <%--document.getElementById("<%=txt_reason1.ClientID %>").value = '';--%>
                }                 
                
                
         }); 




          

             $(function () {
           
               
                var selectedRB = $('#<%= chkl4.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                    $('#<%=Div23.ClientID%>').show();                   
                }
                else {
                    $('#<%=Div23.ClientID%>').hide(); 
                   
                    document.getElementById("<%=txt_ninethReason.ClientID %>").value = '';
                }                 
                
            });  

            $(function () {
           
               
                var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "0") {
                    $('#<%=DivTerm.ClientID%>').show();                   
                }
                else {
                    $('#<%=DivTerm.ClientID%>').hide(); 
                   
                   
                }                 
                
              }); 


             $(function () {
             
                 

                  if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                      $('#<%=Div27.ClientID%>').show();
                      
                      
                     <%-- $("table[id$=chkl5] input:radio:checked").removeAttr("checked");

                        $('#<%=tr6.ClientID%>').hide();    
                    $('#<%=tr7.ClientID%>').hide();  
                    $('#<%=tr8.ClientID%>').hide();  
                    $('#<%=tr9.ClientID%>').hide(); 
                    document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                  }
                  else {
                      $('#<%=Div27.ClientID%>').hide(); 
             
                      <%--$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                        $('#<%=tr6.ClientID%>').hide();    
                    $('#<%=tr7.ClientID%>').hide();  
                    $('#<%=tr8.ClientID%>').hide();  
                    $('#<%=tr9.ClientID%>').hide(); 
                     
                    
                    document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                  }

           
        });
              //flood no option



          $(function () {
              $('#<%= rbflood2.ClientID%>').on('click', function () {
                 

                  if ($('#<%= rbflood2.ClientID%>').prop('checked')) {
                     
                      $('#<%=Div27.ClientID%>').hide();
                      <%--$('#<%= rbflood.ClientID%>').removeAttr("checked");--%>
                      $('#<%= rbflood.ClientID%>').prop('checked', false);
                      var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                     radiolist.removeAttr('checked');
                     //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");

                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                      document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';

                   <%-- document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     //radiolist1.removeAttr('checked');
                     //radiolist2.removeAttr('checked');
                     //radiolist3.removeAttr('checked');

                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);
                  }
                  else {                 
                       $('#<%=Div27.ClientID%>').hide();

                      var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                      //radiolist.removeAttr('checked');
                      radiolist.prop('checked', false);
                      //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                     <%-- $('#<%= rbflood.ClientID%>').removeAttr("checked");--%>
                      $('#<%= rbflood.ClientID%>').prop('checked', false);

                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                      document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     //radiolist1.removeAttr('checked');
                     //radiolist2.removeAttr('checked');
                     //radiolist3.removeAttr('checked');
                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);
                   

                  }
           });
        });


       


             $(function () {
         
               

                var selectedRB = $('#<%= chkl5.ClientID %> input:checked');
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {
                    $('#<%=Div28.ClientID%>').show();   
                    $('#<%=Div29.ClientID%>').show();  
                    $('#<%=Div30.ClientID%>').show();  
                    $('#<%=Div31.ClientID%>').show();  
                }
                else {
                    
                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                    document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     //radiolist1.removeAttr('checked');
                     //radiolist2.removeAttr('checked');
                     //radiolist3.removeAttr('checked');
                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);

                }                 
                
                 
            }); 



             //address same ticked
         $(function () {
              $('#<%= chkSameAdd.ClientID%>').on('click', function () {
                 
                  $('#<%= lbldwelAdd.ClientID%>').text('');   
                  if ($('#<%= chkSameAdd.ClientID%>').prop('checked')) {
                     
                      document.getElementById("<%=txt_dweAdd1.ClientID %>").value = document.getElementById("<%=txt_addline1.ClientID %>").value;
                      document.getElementById("<%=txt_dweAdd2.ClientID %>").value = document.getElementById("<%=txt_addline2.ClientID %>").value;
                      document.getElementById("<%=txt_dweAdd3.ClientID %>").value = document.getElementById("<%=txt_addline3.ClientID %>").value;
                      document.getElementById("<%=txt_dweAdd4.ClientID %>").value = document.getElementById("<%=txt_addline4.ClientID %>").value;
                  }
                  else {                 
                      document.getElementById("<%=txt_dweAdd1.ClientID %>").value = '';
                      document.getElementById("<%=txt_dweAdd2.ClientID %>").value = '';
                      document.getElementById("<%=txt_dweAdd3.ClientID %>").value = '';
                      document.getElementById("<%=txt_dweAdd4.ClientID %>").value = 0;
                       }
           });
        });

            //check box Selection
          $(function () {
              $('#<%= chkOtherPerils.ClientID%>').on('click', function () {
                 <%-- $('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                  $('#<%= chkTR.ClientID%>').removeAttr("checked");
                  $('#<%= rbflood.ClientID%>').removeAttr("checked");--%>

                         $('#<%= chkSRCC.ClientID%>').prop('checked', false);
                          $('#<%= chkTR.ClientID%>').prop('checked', false);
                          $('#<%= rbflood.ClientID%>').prop('checked', false);


                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                      if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                          $('#<%=Div27.ClientID%>').show();
                          <%--$('#<%= rbflood2.ClientID%>').removeAttr("checked");--%>
                           $('#<%= rbflood2.ClientID%>').prop('checked', false);
                          //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          //radiolist.removeAttr('checked');
                          radiolist.prop('checked', false);
                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                        <%-- $('#<%=tr17.ClientID%>').hide(); --%>
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');
                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          //radiolist3.removeAttr('checked');

                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                          $("table[id$=chkl5] input:radio:checked").prop('checked', false);
                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');
                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          //radiolist3.removeAttr('checked');
                          radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);

                      }
                
                  }
                  else {

                      <%--$('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      $('#<%= chkTR.ClientID%>').removeAttr("checked");
                      $('#<%= rbflood.ClientID%>').removeAttr("checked");--%>

                          $('#<%= chkSRCC.ClientID%>').prop('checked', false);
                          $('#<%= chkTR.ClientID%>').prop('checked', false);
                          $('#<%= rbflood.ClientID%>').prop('checked', false);


                       if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                          $('#<%=Div27.ClientID%>').show();
                          <%--$('#<%= rbflood2.ClientID%>').removeAttr("checked");--%>
                           $('#<%= rbflood2.ClientID%>').prop('checked', false);
                          //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          radiolist.removeAttr('checked');

                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                        <%-- $('#<%=tr17.ClientID%>').hide(); --%>
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');
                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          //radiolist3.removeAttr('checked');
                           radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                           //$("table[id$=chkl5] input:radio:checked").removeAttr("checked");
                           $("table[id$=chkl5] input:radio:checked").prop('checked', false);
                          $('#<%=Div28.ClientID%>').hide();
                          $('#<%=Div29.ClientID%>').hide();
                          $('#<%=Div30.ClientID%>').hide();
                          $('#<%=Div31.ClientID%>').hide();
                          $('#<%=lbl16Reason.ClientID%>').text('');
                          $('#<%=lbl16_1.ClientID%>').text('');
                          document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                          var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                          var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                          var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                          //radiolist1.removeAttr('checked');
                          //radiolist2.removeAttr('checked');
                          //radiolist3.removeAttr('checked');
                           radiolist1.prop('checked', false);
                          radiolist2.prop('checked', false);
                          radiolist3.prop('checked', false);

                      }

                  }

           });
            });


             $(function () {
              $('#<%= chkSRCC.ClientID%>').on('click', function () {
                 
                  <%--$('#<%= chkTR.ClientID%>').removeAttr("checked");--%>
                  $('#<%= chkTR.ClientID%>').prop('checked', false);

                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                      <%--$('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      $('#<%= chkTR.ClientID%>').removeAttr("checked");--%>
                      $('#<%= chkSRCC.ClientID%>').prop('checked', false);
                      $('#<%= chkTR.ClientID%>').prop('checked', false);
                     custom_alert( 'Please Select Other perils to get SRCC', 'Alert' );
                     
                  }

           });
        });

            $(function () {
              $('#<%= chkTR.ClientID%>').on('click', function () {
                 
                  

                  if ($('#<%= chkSRCC.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                      <%--$('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      $('#<%= chkTR.ClientID%>').removeAttr("checked");--%>
                      $('#<%= chkSRCC.ClientID%>').prop('checked', false);
                      $('#<%= chkTR.ClientID%>').prop('checked', false);

                      <%--$('#<%= chkOtherPerils.ClientID%>').removeAttr("checked");--%>
                     custom_alert( 'Please Select SRCC to get TR', 'Alert' );
                   
                  }

           });
        });
             $(function () {
              $('#<%= rbflood.ClientID%>').on('click', function () {
                 
                 

                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                     <%-- $('#<%= rbflood.ClientID%>').removeAttr("checked");--%>
                   $('#<%= rbflood.ClientID%>').prop('checked', false);
                     custom_alert( 'Please Select Other perils to get flood cover', 'Alert' );
                      
                  }

           });
             });



            //solar section
          $(function () {
              $('#<%= rbSolOne.ClientID%>').on('click', function () {
                  
                  $('#<%= lblrbSolOne.ClientID%>').text('');  
              });
          });

          $(function () {
              $('#<%= rbSolTwo.ClientID%>').on('click', function () {
                  
                  $('#<%= lblrbSolTwo.ClientID%>').text('');  
              });
          });

          $(function () {
      $('#<%= txtSolarCountry.ClientID%>').on('change keyup paste', function () {
          $('#<%= lblrbSolThree.ClientID%>').text(''); 
     });
          });


//---NSB--changes
           $(function () {
      $('#<%= txtLoanNumber.ClientID%>').on('change keyup paste', function () {
        
       <%-- $('#<%= lblNic.ClientID%>').css("display", "");   --%>  
          <%-- document.getElementById("<%=lblNic.ClientID %>").value = '';--%>
          $('#<%= lblLoan.ClientID%>').text('');
       
     });
         });

         ///------
 ///----26042023-changes
         $(function () {
      $('#<%= ddlSlicCode.ClientID%>').on('click', function () {
        
        <%--$('#<%= lblnameOfProp.ClientID%>').css("display", "none");    --%> 
          $('#<%= Label14.ClientID%>').text('');
       
     });
         });
         ///--


         //end


<%--              $(function () {
             $('#<%=btnProceed.ClientID%>').click(function () {
                 alert("45233");
             });
         }); --%>

            function UserOrEmailAvailability() { //This function call on text change.  
             
             var obj = {};
             obj.useroremail = $.trim($('#<%=txt_nic.ClientID%>').val());
             obj.userMobile = $.trim($('#<%=txt_tele.ClientID%>').val());

            $.ajax({  
                type: "POST",  
                url: "ProposalEntry.aspx/CheckEmail", // this for calling the web method function in cs code.
                data: JSON.stringify(obj),
                
                contentType: "application/json; charset=utf-8",  
                dataType: "json",  
                success: OnSuccess,  
                failure: function (response) {  
                    alert(response);  
                }  
            });  
        }  
  
        // function OnSuccess  
        // function OnSuccess  
        function OnSuccess(response) {  
            var msg = $("#<%=lblStatus.ClientID%>")[0];
             
            if (response.d == "true")
            {
                msg.style.display = "block";  
                msg.style.color = "red";  
                msg.innerHTML = "Kindly note that we are required to further review customer's information and we will revert soon. Page will reload.";

                swal({
                    title: "AML Warning",
                    text: "Kindly note that we are required to further review customer's information and we will revert soon. Page will reload.",
                    type:
                        "warning"
                }).then(function () {
                    location.reload();
                }
                );

            }
            else
            {

            }

        } 
            

            /********************************************************************************/
        }

    });

   

     </script>


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />
    <asp:TextBox ID="resultcount" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtresultcount" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtProposalType" runat="server" Visible="false"></asp:TextBox>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="Less_text" runat="server" />
    <asp:HiddenField ID="Reson_temp" runat="server" />
    <asp:HiddenField ID="app_date" runat="server" />
    <asp:HiddenField ID="reqDate" runat="server" />
    <asp:HiddenField ID="HiddenFieldRemark" runat="server" />
     <asp:HiddenField ID="bank_code" runat="server" />
    <asp:HiddenField ID="statusHidden" runat="server" />
    <asp:HiddenField ID="sumInsuVal" runat="server" />
    <asp:HiddenField ID="user_epf" runat="server" />
    <asp:HiddenField ID="req_status" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
     <asp:HiddenField ID="hfpolType" runat="server" />
    <asp:HiddenField ID="hfbankname" runat="server" />
    <asp:HiddenField ID="hfbranchname" runat="server" />

       <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server"  style="margin-left: 12%; width: 100%; margin-top:0%;">
         <ContentTemplate>--%>
           <%--  
             <br />--%>

      <div class="container border border-info" id="mainDiv" runat="server">

        <div class="form-group row" runat="server" id="rowFirst">
        <div class="col-sm-12 text-center h5 font-weight-bold"><label runat="server" id="txtHeading"></label> Proposal Details</div>
        </div>

          
          <div class="form-group row" runat="server" id="Div2">
              <div class="col-sm-4">1. NIC number:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
              <div class="col-sm-4">
                  <asp:TextBox ID="txt_nic" runat="server" CssClass="form-control text-capitalize" ClientIDMode="Static" autocomplete="off"></asp:TextBox>
              </div>

              <asp:HiddenField ID="hiddenClientId" runat="server" />

              <asp:Button ID="SearchButton" runat="server" Text="Search" CssClass="btn btn-info text-white" OnClientClick="return searchFunction()" OnClick="Search_Click1" ClientIDMode="Static" />


              <%--<asp:Button ID="Button2" runat="server"  Text="debit"   CssClass="btn btn-info text-white"  OnClick="get_Click1" ClientIDMode="Static" />--%>

              <%--<asp:Button ID="btdebit" runat="server" Text="Get Debit Note" CssClass="btn btn-info m-1" OnClick="btdebit_Click" OnClientClick="Alert()" ></asp:Button>--%>

              <div class="col-sm-3">
                  <asp:Label ID="lblNic" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium"></asp:Label>
                  <asp:Label ID="lblStatus" runat="server" Font-Size="Small"></asp:Label>

              </div>
          </div>

          <div class="form-group row" runat="server" id="Div1">
            <div class="col-sm-4">2. Name of proposer:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
            <div class="col-sm-1">
                <asp:DropDownList ID="ddlInitials" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false" OnSelectedIndexChanged="txt_addline4_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="Dr.">Dr</asp:ListItem>
                    <asp:ListItem Value="Mr.">Mr</asp:ListItem>
                    <asp:ListItem Value="Mrs.">Mrs</asp:ListItem>
                    <asp:ListItem Value="Miss.">Miss</asp:ListItem>
                    <asp:ListItem Value="Ms.">Ms</asp:ListItem>
                    <asp:ListItem Value="Rev.">Rev</asp:ListItem>
                </asp:DropDownList>
            </div>
            <div class="col-sm-3">
                <asp:TextBox ID="txt_nameOfProp" runat="server" CssClass="form-control" ClientIDMode="Static" autocomplete="off"></asp:TextBox></div>
            <div class="col-sm-2">
                <asp:Label ID="lblnameOfProp" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small"></asp:Label></div>
        </div>

        <%--<div class="form-group row" runat="server" id="Div1">
        <div class="col-sm-4">1. Name of proposer:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
            <div class="col-sm-1 p-0 ml-3 mr-3"><asp:DropDownList ID="ddlInitials" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false" OnSelectedIndexChanged="txt_addline4_SelectedIndexChanged" >
                <asp:ListItem Value ="0">Select</asp:ListItem>
                <asp:ListItem Value ="Dr. ">Dr</asp:ListItem>
                <asp:ListItem Value ="Mr. ">Mr</asp:ListItem>
                <asp:ListItem Value ="Mrs. ">Mrs</asp:ListItem>
                <asp:ListItem Value ="Miss. ">Miss</asp:ListItem>
                <asp:ListItem Value ="Ms. ">Ms</asp:ListItem>
                <asp:ListItem Value ="Rev. ">Rev</asp:ListItem>
                </asp:DropDownList></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_nameOfProp" runat="server" CssClass="form-control"  ClientIDMode="Static" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-2"><asp:Label ID="lblnameOfProp" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div2">
        <div class="col-sm-4">2. NIC number:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_nic" runat="server" CssClass="form-control text-capitalize" ClientIDMode="Static" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lblNic" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium"></asp:Label>
            <asp:Label ID="lblStatus" runat="server" Font-Size="Small"></asp:Label>  
        </div>
        
                                  
                            
                       
        </div>--%>

         <div class="form-group row" runat="server" id="Div3" visible="false">
        <div class="col-sm-4">3. Business registration No:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_br" runat="server" CssClass="form-control"  ClientIDMode="Static" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label2" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div4">
        <div class="col-sm-4">3. Postal address line 1:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_addline1" runat="server" CssClass="form-control"  ClientIDMode="Static" autocomplete="off" MaxLength="25"></asp:TextBox></div>
        <div class="col-sm-2"><asp:Label ID="Label1" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div5">
        <div class="col-sm-4 pl-5">Address line 2:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_addline2" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false" autocomplete="off" MaxLength="25"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lblpostaladdress" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium"></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div6">
        <div class="col-sm-4 pl-5">Address line 3:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_addline3" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false" autocomplete="off" MaxLength="25"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label3" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

          <div class="form-group row" runat="server" id="Div7">
        <div class="col-sm-4 pl-5">Address line 4:</div>
        <div class="col-sm-4"><asp:DropDownList ID="txt_addline4" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false" OnSelectedIndexChanged="txt_addline4_SelectedIndexChanged" >
                      <asp:ListItem Value ="0">-- Select --</asp:ListItem>
                </asp:DropDownList></div>
        <div class="col-sm-3"><asp:Label ID="Label4" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>


        <div class="form-group row" runat="server" id="Div8">
        <div class="col-sm-4">4. Mobile No:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_tele" runat="server" CssClass="form-control"  ClientIDMode="Static" autocomplete="off" onchange="UserOrEmailAvailability()"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lbltelePhone" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

           <div class="form-group row" runat="server" id="Div9">
        <div class="col-sm-4">5. Land line No:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_landLine" runat="server" CssClass="form-control"  ClientIDMode="Static" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lbllandPhone" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

           <div class="form-group row" runat="server" id="Div10">
        <div class="col-sm-4">6. Email address:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_email" runat="server" CssClass="form-control"  ClientIDMode="Static" TextMode="Email" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label5" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

          <div class="form-group row" runat="server" id="DivDOB">
            <div class="col-sm-4">
                Date of Birth: 
   
                  <asp:Label runat="server" ForeColor="Red">*</asp:Label>
            </div>
            <div class="col-sm-3">
                Date:
                   <asp:TextBox ID="txt_dob" runat="server" CssClass="form-control text-center search" ClientIDMode="Static" autocomplete="off" onkeydown="return false"></asp:TextBox>
            </div>
            <div class="col-sm-3">
                <asp:Label ID="Label15" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium"></asp:Label>
            </div>
        </div>

            <div class="form-group row" runat="server" id="Div11">
        <div class="col-sm-4"> 7. Address of dwelling house to be insured: <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Address line 1:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_dweAdd1" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false" autocomplete="off"></asp:TextBox></div>
        
<%--<div class="col-sm-4"><asp:CheckBox ID="" runat="server" Value="1" Text="" style="padding-left:10px;"/></div>--%>
               
        <div class="custom-control custom-checkbox col-sm-3 pl-5" runat="server">
        <input type="checkbox" class="custom-control-input" name="chkSameAdd" id="chkSameAdd" runat="server" ClientIDMode="Static" value="1">
        <label class="custom-control-label" for="chkSameAdd">Same as above</label>
        </div> 
        </div>


          <div class="form-group row" runat="server" id="Div12">
        <div class="col-sm-4 pl-5">Address line 2:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_dweAdd2" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lbldwelAdd" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>


          <div class="form-group row" runat="server" id="Div13">
        <div class="col-sm-4 pl-5">Address line 3:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_dweAdd3" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label7" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>

          <div class="form-group row" runat="server" id="Div14">
        <div class="col-sm-4 pl-5">Address line 4:</div>
        <div class="col-sm-4"><asp:DropDownList ID="txt_dweAdd4" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false" OnSelectedIndexChanged="txt_dweAdd4_SelectedIndexChanged"  >
                                          <asp:ListItem Value ="0">-- Select --</asp:ListItem>
                                          </asp:DropDownList></div>
        <div class="col-sm-3"><asp:Label ID="Label6" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>



        <div class="d-flex justify-content-center">
            <asp:Button ID="Clienttxt" runat="server" Text="Create Client"
                ClientIDMode="Static"
                CssClass="btn btn-outline-info border border-1 border-info btnClz btn-sm lbls ms-2"
                OnClientClick="return createclientFunction()"
                OnClick="create_clientClick"
                Visible="false" />
        </div>


        <div class="col-sm-3 d-flex flex-column align-items-center mt-2">
            <asp:Label ID="ibiCClient" runat="server" Text="" ForeColor="Green" Font-Bold="false" Font-Size="Medium"></asp:Label>
            <asp:Label ID="Label16" runat="server" Font-Size="Small"></asp:Label>
        </div>

          <div class="form-group row" runat="server" id="DivTermType">
        <div class="col-sm-4">8. Policy type<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-7">

                <asp:RadioButtonList ID="RbTermType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group"> 
                                     <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz" id="termAnual">&nbsp;&nbsp;Annual private dwelling policy</asp:ListItem>
                                     <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz" id="termLong">&nbsp;&nbsp;Long term private dwelling policy</asp:ListItem>
                                     </asp:RadioButtonList>               
        </div>
        <div class="col-sm-1"><asp:Label ID="lblTermType" runat="server" Text="" ForeColor="Red" Font-Bold="True" Font-Size="Small" ></asp:Label></div>
        </div>

            <div class="form-group row" runat="server" id="Div15">
        <div class="col-sm-4"> 9. Period of insurance<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-3"> From: <asp:TextBox ID="txt_fromDate" runat="server" CssClass="form-control text-center search"  ClientIDMode="Static" autocomplete="off" onkeydown="return false"></asp:TextBox></div>
       
           <div class="col-sm-1" runat="server" id="DivTerm" style="display:none" ClientIDMode="Static">Terms<asp:DropDownList ID="ddlNumberOfYears" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false">
                                          <asp:ListItem Value ="0">Select</asp:ListItem>
                                          </asp:DropDownList></div>      
                
                <div class="col-sm-3">To: <asp:TextBox ID="txt_toDate" runat="server" CssClass="form-control text-center"  ClientIDMode="Static" autocomplete="off" onkeydown="return false">
                                          </asp:TextBox></div>
       <div class="col-sm-1">
           <asp:Label ID="lbfromDat" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
           <%--<asp:Label ID="lbltoDate" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label>--%>
           </div>
                </div>

           <div class="form-group row" runat="server" id="Div16">
        <div class="col-sm-4">10. Is the house under construction:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:RadioButtonList ID="chkl1" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group">
                                              <asp:ListItem id="yes" runat="server" Value="1" class="radio-inline m-2 radioClz" Text="Yes">&nbsp;&nbsp;Yes</asp:ListItem>
                                              <asp:ListItem  id="no" runat="server" Value="0" class="radio-inline m-2 radioClz" Text="No">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>

            
        </div>
        <div class="col-sm-3"><asp:Label ID="lblContact" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>



        <div class="form-group row" runat="server" id="Div17">
        <%--<div class="col-sm-1">11. </div>--%>
        <div class="col-sm-4">11.1. Value of the bank facility (LKR) &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;: </div>
        <div class="col-sm-4"><asp:TextBox ID="txt_bankVal" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="form-control" onkeypress="javascript:return isNumber(event);" onkeyup = "javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label8" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>
         <%-- changes NSB 11032022--%>
          <div class="form-group row" runat="server" id="Div44">
        <div class="col-sm-1"></div>
        <div class="col-sm-3 text-left">11.2. Bank Loan Number :</div>
        <div class="col-sm-4"><asp:TextBox ID="txtLoanNumber" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="form-control" autocomplete="off" MaxLength="20"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lblLoan" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>

 <div class="form-group row" runat="server" id="Div18">
        <div class="col-sm-10">12. Property to be insured: (Please mention the value as per the valuation report of the building)</div>
        <div class="col-sm-0"></div>
        <div class="col-sm-2"></div>
        </div>

         <div class="form-group row" runat="server" id="Div33">
             <div class="col-sm-1"></div>
        <div class="col-sm-7 text-center border border-dark">Property to be insured</div>
        <div class="col-sm-2 text-center border border-dark">Sum insured (LKR)</div>
        <div class="col-sm-2"></div>
        </div>

<div class="form-group row" runat="server" id="Div19" style="display:normal">
        <div class="col-sm-1"></div>
        <div class="col-sm-7 border border-dark">Value of the building and fixture-fittings including electrical and water installation.<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-2 border border-dark p-1">
            <asp:TextBox class="txt" ID="txt_sumInsu1" runat="server" ClientIDMode="Static" CssClass="form-control" onkeypress="javascript:return isNumber(event);" onkeyup = "javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox>             
        </div>
        <div class="col-sm-2"><asp:Label ID="lblsumInsu1" runat="server" Text="" ForeColor="Red" Font-Bold="True" Font-Size="Small" ></asp:Label></div>
        </div>


<div class="form-group row" runat="server" id="Div20" style="display:normal">
        <div class="col-sm-1"></div>
        <div class="col-sm-7 border border-dark">Value of the boundary and parapet wall</div>
        <div class="col-sm-2 border border-dark p-1">
             <asp:TextBox class="txt" ID="txt_sumInsu2" runat="server" ClientIDMode="Static" CssClass="form-control"  onkeypress="javascript:return isNumber(event)" onkeyup = "javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox>                
        </div>
        <div class="col-sm-2"><asp:Label ID="lblsumInsu2" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label> </div>
        </div>
          <div class="form-group row" runat="server" id="Div20I" style="display:normal">
        <div class="col-sm-1"></div>
        <div class="col-sm-7 border border-dark">On solar panel system & standard accessories<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-2 border border-dark p-1">
             <asp:TextBox class="txt" ID="txt_solar" runat="server" ClientIDMode="Static" CssClass="form-control"  onkeypress="javascript:return isNumber(event)" onkeyup = "javascript:this.value=Comma(this.value);" autocomplete="off"></asp:TextBox>                
        </div>
        <div class="col-sm-2"><asp:Label ID="lblSolar" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label> </div>
        </div>
<div class="form-group row" runat="server" id="Div21">
        <div class="col-sm-1"></div>
        <div class="col-sm-7 text-center border border-dark">Total</div>
        <div class="col-sm-2 border border-dark p-1">
             <asp:TextBox ID="txt_sumInsuTotal" runat="server" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>               
        </div>
        <div class="col-sm-2"></div>
        </div>

          <%--solar questions--%>

            <div class="form-group row" runat="server" id="Div37" style="display:normal">
        <div class="col-sm-1"></div>
        <div class="col-sm-6 font-weight-bold">If solar panel to be insured (Solar panel system details)</div>
        <div class="col-sm-2"></div>
        <div class="col-sm-2">
            <asp:Label ID="Label12" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>

          <div class="form-group row" runat="server" id="Div34" style="display:normal">
        <div class="col-sm-1"></div>
        <div class="col-sm-7">I)&nbsp; Is the local repairer / sole agent available in Sri Lanka in the event of repairable damage in respect of solar panel system?
            <asp:Label ID="lblrbSolOne" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" >*</asp:Label>
        </div>
        <div class="col-sm-2">
                                          <asp:RadioButtonList ID="rbSolOne" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group"> 
                                              <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes</asp:ListItem>
                                              <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>   
            </div>
        <div class="col-sm-2">
            
        </div>
        </div>

<div class="form-group row" runat="server" id="Div35" style="display:normal">
        <div class="col-sm-1"></div> 
        <div class="col-sm-7">II)&nbsp; Is the spare parts available in Sri Lanka in the event of repairable damage in respect of solar panel system? 
            <asp:Label ID="lblrbSolTwo" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" >*</asp:Label>
        </div>
        <div class="col-sm-2">
        
                                          <asp:RadioButtonList ID="rbSolTwo" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group" > 
                                              <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes</asp:ListItem>
                                              <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>  
            </div>
        <div class="col-sm-2">
             
        </div>
        </div>


<div class="form-group row" runat="server" id="Div36" style="display:normal">
        <div class="col-sm-1"></div>
        <div class="col-sm-6">III)&nbsp; Country of origin of solar panel system?
            <asp:Label ID="lblrbSolThree" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" >*</asp:Label>
        </div>
        <div class="col-sm-3">
                                    <asp:TextBox class="txt" ID="txtSolarCountry" runat="server" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>  
            </div>
        <div class="col-sm-2">
             
        </div>
        </div>

          <div class="form-group row" runat="server" id="Div43" style="display:normal">
        <div class="col-sm-1"></div>
        <div class="col-sm-6">IV)&nbsp; Model number or serial number (If available)</div>
        <div class="col-sm-3">
                                   <asp:TextBox class="txt" ID="txtSoloarModel" runat="server" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>  
            </div>
        <div class="col-sm-2">
             <asp:Label ID="Label13" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>
         <%-- end--%>




<div class="form-group row" runat="server" id="Div22">
        <div class="col-sm-7">13. Has the risk location of the proposed house ever been affected by flood during last 05 years?<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-3">

                <asp:RadioButtonList ID="chkl4" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group"> 
                                     <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes</asp:ListItem>
                                     <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                                     </asp:RadioButtonList>               
        </div>
        <div class="col-sm-2"><asp:Label ID="lblflood1" runat="server" Text="" ForeColor="Red" Font-Bold="True" Font-Size="Small" ></asp:Label></div>
        </div>



<div class="form-group row" runat="server" id="Div23" style="display:none">
    <div class="col-sm-1"></div>
        <div class="col-sm-3">If yes, Please give details<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-5">
                <asp:TextBox ID="txt_ninethReason" runat="server" CssClass="form-control"  ClientIDMode="Static" autocomplete="off"></asp:TextBox>            
        </div>
        <div class="col-sm-2"><asp:Label ID="lblreason3" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>


<div class="form-group row" runat="server" id="Div24">
        <div class="col-sm-4">14. Number of floors (Including ground floor):<asp:Label runat="server" ForeColor="Red">*</asp:Label> </div>
        <div class="col-sm-2">
                <asp:TextBox ID="txtNoofFloors" runat="server" CssClass="form-control text-center"  ClientIDMode="Static" Width="60%" TextMode="Number" min="0" onkeypress="javascript:return isNumber(event);" MaxLength="2" autocomplete="off"></asp:TextBox>         
        </div>
        <div class="col-sm-5"><asp:Label ID="lblnoFloors" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>


<div class="form-group row" runat="server" id="Div25">
        <div class="col-sm-4">15. Construction Details of the Building :</div><%--Tick as applicable on the construction of the building--%>

   <div class="col-sm-8 float-left text-left">
       Buildings are deemed to be constructed with walls of Bricks / Cement / Cement Blocks / Concrete and Doors & Windows with Wooden / Metal / Aluminum and Roof with Asbestos sheets / Tile / GI Sheets / Concrete / Metal, unless the insurer has been advised otherwise.
   </div>
        <div class="col-sm-7 d-none" runat="server" id="constructDetails">
            <b>Wall:</b>
                                Brick<asp:CheckBox ID="Chkbrick" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
                                Cement<asp:CheckBox ID="Chkcement" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
            <br />
            <b>Doors and Windows:</b>
                                
                                Wooden<asp:CheckBox ID="ChkWooden" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
                                Metal<asp:CheckBox ID="ChkMetal" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
            <br />
            <b>Floor:</b>
                                Tile<asp:CheckBox ID="Chktile" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
                                Cement<asp:CheckBox ID="ChkFloorcement" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
            <br />
            <b>Roof:</b>
                                Tile<asp:CheckBox ID="Chekrooftile" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
                                Asbestos<asp:CheckBox ID="Chasbastos" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
                                GI Sheets<asp:CheckBox ID="ChkGI" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
                                Concrete<asp:CheckBox ID="Chkconcreat" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>


        </div>
      
        </div>
<%--covers area--%>
          <div class="form-group row" runat="server" id="Div38">
        <div class="col-sm-4">16. Coverage required </div>
        <div class="col-sm-4">
              <label class="col-sm-5">i. Fire & lighting</label><asp:CheckBox ID="chkFirLight" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2" Checked="true"/>
            </div>
        <div class="col-sm-3"></div>
        </div>
          <div class="form-group row" runat="server" id="Div39">
        <div class="col-sm-4"></div>
        <div class="col-sm-4">
                <label class="col-sm-5">ii.	Other perils
                      <a href="#" Class="stretched-link" data-toggle="modal" data-target="#exampleModalCenter"><img src="../Images/select.png" class="img-fluid"/></a>
                </label><asp:CheckBox ID="chkOtherPerils" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
            </div>
        <div class="col-sm-3">
          
        </div>
        </div>
          <div class="form-group row" runat="server" id="Div40">
        <div class="col-sm-4"></div>
        <div class="col-sm-4">
               <label class="col-sm-5">iii. SRCC</label><asp:CheckBox ID="chkSRCC" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
            </div>
        <div class="col-sm-3"></div>
        </div>
          <div class="form-group row" runat="server" id="Div41">
        <div class="col-sm-4"></div>
        <div class="col-sm-4">
                <label class="col-sm-5">iv. TC</label><asp:CheckBox ID="chkTR" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
            </div>
        <div class="col-sm-3"></div>
        </div>
        


<div class="form-group row" runat="server" id="Div26">
        <div class="col-sm-4"></div>
        <div class="col-sm-4">
                <asp:CheckBox ID="rbfire" runat="server" Value="1" Text="&nbsp;Fire" style="padding-right:10px;" Visible="False"/>
                                <asp:CheckBox ID="rblighting" runat="server" Value="1" Text="&nbsp;Lighting" style="padding-right:10px;" Visible="False"/>
                                <label class="col-sm-5">v. Flood</label><asp:CheckBox ID="rbflood" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
                                <asp:CheckBox ID="rbflood2" runat="server" Value="0" Text="" CssClass="ChkBoxClass m-2" Visible="False"/>
            </div>
        <div class="col-sm-3"></div>
        </div>



<div class="form-group row" runat="server" id="Div27" style="display:none">
        <div class="col-sm-8">17. Are there any rivers, canals, reservoirs or other water course available in close proximity (within 300m)?<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-2">
                <asp:RadioButtonList ID="chkl5" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group"> 
                                 <asp:ListItem id="chk15_1" runat="server" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes</asp:ListItem>
                                  <asp:ListItem runat="server" Value="0" id="chk15_2" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                </asp:RadioButtonList> 
            </div>
        <div class="col-sm-2">
             <asp:Label ID="lbl16Reason" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>



<div class="form-group row" runat="server" id="Div28" style="display:none">
    <div class="col-sm-1"></div>
        <div class="col-sm-6">1) If yes, Please give distance to proposed residence<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-2">
                 <asp:TextBox ID="txt_wordReason1" runat="server" CssClass="form-control"  ClientIDMode="Static" autocomplete="off"></asp:TextBox>
            </div>
        <div class="col-sm-2">
            <asp:Label ID="lbl16_1" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>


<div class="form-group row" runat="server" id="Div29" style="display:none">
        <div class="col-sm-1"></div>
        <div class="col-sm-6">2) Are there adequate drainage facilities in the premises and neighborhood? </div>
        <div class="col-sm-2">
                                          <asp:RadioButtonList ID="txt_wordReason2" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group"> 
                                              <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes</asp:ListItem>
                                              <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>   
            </div>
        <div class="col-sm-2">
            <asp:Label ID="Label9" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>

<div class="form-group row" runat="server" id="Div30" style="display:none">
        <div class="col-sm-1"></div> 
        <div class="col-sm-6">3) Has there been flood / drainage over flows during the past 05 years? </div>
        <div class="col-sm-2">
        
                                          <asp:RadioButtonList ID="txt_wordReason3" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group" > 
                                              <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes</asp:ListItem>
                                              <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>  
            </div>
        <div class="col-sm-2">
             <asp:Label ID="Label10" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>


<div class="form-group row" runat="server" id="Div31" style="display:none">
        <div class="col-sm-1"></div>
        <div class="col-sm-6">4) Has the surrounding area of your premises or the entry path / road ways under gone water during last 05 years?</div>
        <div class="col-sm-2">
                                    <asp:RadioButtonList ID="txt_wordReason4" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group" > 
                                              <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes</asp:ListItem>
                                              <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList> 
            </div>
        <div class="col-sm-2">
             <asp:Label ID="Label11" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>

  <%--end--%>
          <%--27042023 changes--%>
          <div class="form-group row" runat="server" id="Div45">
        <div class="col-sm-4">17. SLIC Code<asp:Label runat="server" ForeColor="Red">*</asp:Label> :</div>
        <div class="col-sm-5"><asp:DropDownList ID="ddlSlicCode" runat="server"  ClientIDMode="Static" CssClass="form-control cleare"  AppendDataBoundItems="true" AutoPostBack="false" >
                      <asp:ListItem Value="0">-- Select --</asp:ListItem>
                  </asp:DropDownList></div>
        <div class="col-sm-3 hidden">
            <label class="m-2 h5 text-info font-weight-bold" runat="server" id="bpfR">Special BPF <asp:CheckBox ID="chkBf" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2" Checked="true"/></label>
        </div>
        <div class="col-sm-3">
            <asp:Label ID="Label14" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label>    
        </div>
  

        </div>
          <%--end--%>

<div class="form-group row" runat="server" id="Div32">
        <div class="col-sm-4"></div>
        <div class="col-sm-6">
          <asp:Button ID="btnProceed" runat="server"  Text="Procees"   CssClass="btn btn-info text-white" OnClientClick="return clientFunction()" OnClick="btnProceed_Click" ClientIDMode="Static" />
          <asp:Button ID="btnDone" runat="server"  Text="Back"  OnClientClick="this.value='Please wait...'"  CssClass="btn btn-info text-white" OnClick="btnDone_Click"/>                                        
        </div>
        <div class="col-sm-1">
            
        </div>
        </div>

 </div>
    <%--shedule calculations--%>
     <div class="container" id="ShedulCal" runat="server" visible="false">
    <div class="container border border-info pt-2" id="CalDiv" runat="server">
         <div class="form-group row" runat="server" id="CalDiv1">
             <div class="col-sm-3"></div>
             <div class="col-sm-2 bg-light font-weight-bold">Reference No.</div>
             <div class="col-sm-1"></div>
             <div class="col-sm-2"><asp:TextBox ID="txt_Ref_no" runat="server" CssClass="form-control bg-light font-weight-bold"></asp:TextBox>  </div>
        </div>
        <div class="form-group row" runat="server" id="CalDiv2">
        <div class="col-sm-5 text-center font-weight-bold h5">Premium Details</div>
        <div class="col-sm-4"></div>
        <div class="col-sm-2"></div>
        </div>

         <div class="form-group row" runat="server" id="CalDiv3">
        <div class="col-sm-3"></div>     
        <div class="col-sm-2 bg-light font-weight-bold">Net Premium</div>
        <div class="col-sm-1 bg-light font-weight-bold">Rs.</div>
        <div class="col-sm-2">
            <asp:TextBox ID="txt_NetPre" runat="server" CssClass="form-control bg-light font-weight-bold"></asp:TextBox>
        </div>
        </div>


        <div class="form-group row" runat="server" id="CalDiv4">
        <div class="col-sm-3"></div>
        <div class="col-sm-2 bg-light font-weight-bold">SRCC</div>
        <div class="col-sm-1 bg-light font-weight-bold">Rs.</div>
        <div class="col-sm-2">
            <asp:TextBox ID="txt_srcc" runat="server" CssClass="form-control bg-light font-weight-bold"></asp:TextBox>
        </div>
        </div>

        <div class="form-group row" runat="server" id="CalDiv5">
        <div class="col-sm-3"></div>
        <div class="col-sm-2 bg-light font-weight-bold">TC</div>
        <div class="col-sm-1 bg-light font-weight-bold">Rs.</div>
        <div class="col-sm-2">
           <asp:TextBox ID="txt_tr" runat="server" CssClass="form-control bg-light font-weight-bold" ></asp:TextBox>
        </div>
        </div>

         <div class="form-group row" runat="server" id="CalDiv6">
        <div class="col-sm-3"></div>
        <div class="col-sm-2 bg-light font-weight-bold">Admin Fee</div>
        <div class="col-sm-1 bg-light font-weight-bold">Rs.</div>
        <div class="col-sm-2">
           <asp:TextBox ID="txt_adminFee" runat="server" CssClass="form-control bg-light font-weight-bold"></asp:TextBox>
        </div>
        </div>

        <div class="form-group row" runat="server" id="CalDiv7">
        <div class="col-sm-3"></div>
        <div class="col-sm-2 bg-light font-weight-bold"><label id="txtPolFee" runat="server"></label></div>
        <div class="col-sm-1 bg-light font-weight-bold">Rs.</div>
        <div class="col-sm-2">
          <asp:TextBox ID="txtPoliFee" runat="server" CssClass="form-control bg-light font-weight-bold"></asp:TextBox>
        </div>
        </div>

        <div class="form-group row" runat="server" id="DivRenewalFee">
        <div class="col-sm-3"></div>
        <div class="col-sm-2 bg-light font-weight-bold">Renewal Fee</div>
        <div class="col-sm-1 bg-light font-weight-bold">Rs.</div>
        <div class="col-sm-2">
          <asp:TextBox ID="txt_renewal" runat="server" CssClass="form-control bg-light font-weight-bold"></asp:TextBox>
        </div>
        </div>
        <%--27062022 enabel this because Social Security Contribution Levy ( SSC )--%>
         <div class="form-group row" runat="server" id="CalDiv8" visible="true">
        <div class="col-sm-3"></div>
        <div class="col-sm-2 bg-light font-weight-bold">SSC Levy</div><%--Social Sec. Con.--%>
        <div class="col-sm-1 bg-light font-weight-bold">Rs.</div>
        <div class="col-sm-2">
          <asp:TextBox ID="txt_nbt" runat="server" CssClass="form-control bg-light font-weight-bold"></asp:TextBox>
        </div>
        </div>

        <div class="form-group row" runat="server" id="CalDiv9">
        <div class="col-sm-3"></div>
        <div class="col-sm-2 bg-light font-weight-bold">VAT</div>
        <div class="col-sm-1 bg-light font-weight-bold">Rs.</div>
        <div class="col-sm-2">
          <asp:TextBox ID="txt_vat" runat="server" CssClass="form-control bg-light font-weight-bold"></asp:TextBox>
        </div>
        </div>

        <div class="form-group row" runat="server" id="CalDiv10">
            <div class="col-sm-3"></div>
        <div class="col-sm-2 bg-light font-weight-bold">Total Payable</div>
        <div class="col-sm-1 bg-light font-weight-bold">Rs.</div>
        <div class="col-sm-2">
          <asp:TextBox ID="txtTotalPay" runat="server" CssClass="form-control bg-warning font-weight-bold"></asp:TextBox>
        </div>
        </div>

        </div>






     <div class="container border border-info border-top-0" id="quo" runat="server">
         <div class="form-group row" runat="server" id="re001">
             <div class="col-sm-1"></div>
        <div class="col-sm-9 text-center"><asp:Label ID="complete_tag" runat="server" Text="Need an approval from SLIC." CssClass="text-danger font-weight-bold h4"></asp:Label>
        </div>
        <div class="col-sm-1"></div>
        
        </div>


         <div class="form-group row" runat="server" id="re002">
             <div class="col-sm-1"></div>
        <div class="col-sm-9 text-center">
            <span id="spanRefNo" runat="server" class="text-danger text-center font-weight-bold h4"></span>
        </div>
        <div class="col-sm-1"></div>

        </div>

           <div class="form-group row" runat="server" id="re003">
               <div class="col-sm-1"></div>
        <div class="col-sm-9 text-center">
            <asp:Button ID="btsendrequest" runat="server"  Text="Send Request!" UseSubmitBehavior="true" OnClientClick="custom_alert('Request Successfully Sent to SLIC', 'Success')" CssClass="btn btn-info text-white" OnClick="btsendrequest_Click" />
            <asp:Button runat="server" CssClass="btn btn-info text-white" ID="Button1" Text="Back" OnClick="btCalBack_Click" />
        </div>
        <div class="col-sm-1"></div>
        </div>

<div class="form-group row" runat="server" id="re004">
    <div class="col-sm-1"></div>
        <div class="col-sm-9 text-center text-success font-weight-bold h5">
            <asp:Label ID="lblreqsend" runat="server" Text="Request successfully sent to SLIC"></asp:Label>
        </div>
        <div class="col-sm-1"></div>
        </div>
         

     </div>
    
    <div class="container border border-info border-top-0" id="printProp" runat="server">
        <div class="form-group row" runat="server" id="DivPrint1">
            <div class="col-sm-1"></div>
        <div class="col-sm-9 text-center"><asp:Label ID="lblprop" runat="server" Text="Click print for policy schedule" Visible="false" CssClass="text-success text-center font-weight-bold h5"></asp:Label>
       <span id="span1" runat="server" class="text-center text-success font-weight-bold h4">Click here to confirm and print the policy schedule / payment advice.</span>
        </div>
        <div class="col-sm-1"></div>
        </div>


         <div class="form-group row" runat="server" id="Div42">
             <div class="col-sm-1"></div>
        <div class="col-sm-9 text-center">
            <asp:Button runat="server" OnClick="Button1_Click" CssClass="btn btn-info text-white" ID="printWording" Text="Print"  OnClientClick="custom_alert('Processing for Schedule!', 'Success')"/>
            <asp:Button runat="server" CssClass="btn btn-info text-white" ID="btCalBack" Text="Back" OnClick="btCalBack_Click" />
        </div>
        <div class="col-sm-1"></div>
        </div>
    </div>
         </div>

     <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
    <!-- Modal -->
<div class="modal fade" id="exampleModalCenter" tabindex="-1" role="dialog" aria-labelledby="exampleModalCenterTitle" aria-hidden="true">
  <div class="modal-dialog modal-dialog-centered" role="document">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLongTitle">Other perils info.</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
          <span aria-hidden="true">&times;</span>
        </button>
      </div>
      <div class="modal-body">
       <p>
                1. Malicious Damage<br/>
                2. Explosion<br/>
                3. Cyclone, Storm, Tempest<br/>
                4. Earthquake with Fire & Shock<br/>
                5. Natural Disaster<br/>
                6. Aircraft Damage<br/>
                7. Impact Damage<br/>
                8. Bursting or Overflowing of Water Tanks, Apparatus or pipes<br/>
                9. Electrical Inclusion Clause</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <%--<button type="button" class="btn btn-primary">Save changes</button>--%>
      </div>
    </div>
  </div>
</div>
    <!-- Modal Popup -->
<div id="MyPopup" class="modal fade" role="dialog">
    <div class="modal-dialog">
        <!-- Modal content-->
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">
                    &times;</button>
                <h4 class="modal-title">
                </h4>
            </div>
            <div class="modal-body">
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-danger" data-dismiss="modal">
                    Close</button>
            </div>
        </div>
    </div>
</div>
</asp:Content>

