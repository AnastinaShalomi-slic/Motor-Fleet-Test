<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="EditCustomerDetails.aspx.cs" Inherits="Bank_Fire_EditCustomerDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  

<style type="text/css">        
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
    .form-group
    {
  margin-bottom: 5px;
    }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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


        function clientFunction()
        {

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

            if ((nic == "")) {
            custom_alert( 'Please enter NIC number!', 'Alert' );
            $('#<%=lblNic.ClientID%>').html("Please enter valid NIC number!");
            return false;

        }
       

     if (nic  =! "") {
           var nic2 = $('#<%=txt_nic.ClientID %>').val();
           
           
                const regex = /^([0-9]{9}[x|X|v|V]|[0-9]{12})$/;
                var input = $('#<%=txt_nic.ClientID %>').val();

                if (regex.test(input)) {
                   
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
                }
                else {
                    custom_alert( 'Please enter a valid phone number!', 'Alert' );
                    $('#<%=lbltelePhone.ClientID%>').html("Please enter a valid phone number!");
                    return false;

                }

            }       

       
        
  if (landPhone  == "") 
        { }

     else if(landPhone  =! "") {
        
                const regex = /^(?:0|94|\+94)?(?:(11|21|23|24|25|26|27|31|32|33|34|35|36|37|38|41|45|47|51|52|54|55|57|63|65|66|67|81|912)(0|2|3|4|5|7|9)|7(0|1|2|5|6|7|8)\d)\d{6}$/;
            
                var input = $('#<%=txt_landLine.ClientID %>').val();

                if (regex.test(input)) {
                }
                else {
                    custom_alert( 'Please enter a valid land line number!', 'Alert' );
                    $('#<%=lbllandPhone.ClientID%>').html("Please enter a valid phone number!");
                    return false;

                }

            }  

    

        if ((addline11 == "") || (addline22 == ""))
        {
            custom_alert( 'Please enter dwelling house address line 1 and 2!', 'Alert' );
            $('#<%=lbldwelAdd.ClientID%>').html("Please enter dwelling house address line 1 and 2!");
            return false;
        }  
      


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
            changeYear: true,
            showOtherMonths: true,
            yearRange: '2016:+10',
            dateFormat: 'dd/mm/yy',
            defaultDate: +0,
            numberOfMonths: 1,
            //maxDate: toDate,
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
            buttonImage: "../Images/delete.gif",
            buttonImageOnly: true,
            buttonText: "Select date",
           onSelect: function (selectedDate) {
               $("#txt_fromDate").datepicker("option", "maxDate", selectedDate);

            }
        });
         });

         $(function () {
      $('#<%= ddlInitials.ClientID%>').on('click', function () {
        
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
        
          $('#<%= lblNic.ClientID%>').text('');
       
     });
         });

          $(function () {
      $('#<%= txt_br.ClientID%>').on('change keyup paste', function () {
        
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
                    $('#<%= lblTermType.ClientID%>').text(''); 
                    $('#<%=DivTerm.ClientID%>').hide(); 
                }
                else {
                    $('#<%= lblTermType.ClientID%>').text(''); 
                   $('#<%=DivTerm.ClientID%>').show(); 
                }    

         });
         });


           $(function ()
            {$("#ddlNumberOfYears, #txt_fromDate").click( function () {     
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
              
            }
        }
   

  
                });

            });



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
                          $('#<%= rbflood2.ClientID%>').removeAttr("checked");
                          $('#<%=Div27.ClientID%>').show();

                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          radiolist.removeAttr('checked');

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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
                         
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          $("table[id$=chkl5] input:radio:checked").removeAttr("checked");
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');

                      }
                  }
           });
        });


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
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');

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
                }                 
                
                
         }); 


         $(function () {
     
             var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');
             
                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {  
                    $('#<%= lblTermType.ClientID%>').text(''); 
                    
                }
                else {
                    $('#<%= lblTermType.ClientID%>').text(''); 
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

                  }
                  else {
                      $('#<%=Div27.ClientID%>').hide(); 

                  }

           
         });

          //flood no option



          $(function () {
              $('#<%= rbflood2.ClientID%>').on('click', function () {
                 

                  if ($('#<%= rbflood2.ClientID%>').prop('checked')) {
                     
                      $('#<%=Div27.ClientID%>').hide();
                       $('#<%= rbflood.ClientID%>').removeAttr("checked");


                      var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                     radiolist.removeAttr('checked');

                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                      document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');

                  }
                  else {                 
                       $('#<%=Div27.ClientID%>').hide();

                      var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                     radiolist.removeAttr('checked');
                    $('#<%= rbflood.ClientID%>').removeAttr("checked");
                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                      document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');

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
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');

                }                                             
              }); 

         //address same ticked

         $(function () {
              $('#<%= chkSameAdd.ClientID%>').on('click', function () {
                 

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

                  $('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                  $('#<%= chkTR.ClientID%>').removeAttr("checked");
                  $('#<%= rbflood.ClientID%>').removeAttr("checked");

                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                      if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                          $('#<%=Div27.ClientID%>').show();
                          $('#<%= rbflood2.ClientID%>').removeAttr("checked");

                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          radiolist.removeAttr('checked');

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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');

                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          $("table[id$=chkl5] input:radio:checked").removeAttr("checked");
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');

                      }
                
                  }
                  else {

                      $('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      $('#<%= chkTR.ClientID%>').removeAttr("checked");
                      $('#<%= rbflood.ClientID%>').removeAttr("checked");

                       if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                          $('#<%=Div27.ClientID%>').show();
                          $('#<%= rbflood2.ClientID%>').removeAttr("checked");

                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          radiolist.removeAttr('checked');

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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          $("table[id$=chkl5] input:radio:checked").removeAttr("checked");
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');

                      }
                  }

           });
         });

         $(function () {
              $('#<%= chkSRCC.ClientID%>').on('click', function () {
                 
                  $('#<%= chkTR.ClientID%>').removeAttr("checked");

                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                      $('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      $('#<%= chkTR.ClientID%>').removeAttr("checked");
                     custom_alert( 'Please Select Other perils to get SRCC', 'Alert' );
                      
                  }

           });
        });

          $(function () {
              $('#<%= chkTR.ClientID%>').on('click', function () {
                 
                  

                  if ($('#<%= chkSRCC.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                      $('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      $('#<%= chkTR.ClientID%>').removeAttr("checked");
                     custom_alert( 'Please Select SRCC to get TR', 'Alert' );
                      
                  }

           });
        });


          $(function () {
              $('#<%= rbflood.ClientID%>').on('click', function () {
                 
                 

                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                      $('#<%= rbflood.ClientID%>').removeAttr("checked");
                   
                     custom_alert( 'Please Select Other perils to get flood cover', 'Alert' );
                      
                  }

           });
        });




    $(document).ready(function () {

        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);

        function EndRequestHandler(sender, args) {

            $(function () {
                $("input[id$='txt_fromDate']").datepicker({
                    changeMonth: true,
                    changeYear: true,
                    showOtherMonths: true,
                    yearRange: '2016:+10',
                    dateFormat: 'dd/mm/yy',
                    defaultDate: +0,
                    numberOfMonths: 1,
                    //maxDate: toDate,
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
                    buttonImage: "../Images/delete.gif",
                    buttonImageOnly: true,
                    buttonText: "Select date",
                    onSelect: function (selectedDate) {
                $("#txt_fromDate").datepicker("option", "maxDate", selectedDate);
            } 
                });

            });
   

 $(function () {
      $('#<%= ddlInitials.ClientID%>').on('click', function () {

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

          $('#<%= lblNic.ClientID%>').text('');
       
     });
         });

          $(function () {
      $('#<%= txt_br.ClientID%>').on('change keyup paste', function () {

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
                    $('#<%= lblTermType.ClientID%>').text('');
                    $('#<%=DivTerm.ClientID%>').hide(); 
                }
                else {
                    $('#<%= lblTermType.ClientID%>').text(''); 
                    $('#<%=DivTerm.ClientID%>').show(); 
                }    
         $('#<%= lblTermType.ClientID%>').text(''); 
         });
            });

         

           $(function ()
            {   $("#ddlNumberOfYears, #txt_fromDate").click( function () {
                    
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
              
            }
        }
   

  
                });

            });



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
                          $('#<%= rbflood2.ClientID%>').removeAttr("checked");

                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          radiolist.removeAttr('checked');

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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
                  
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          $("table[id$=chkl5] input:radio:checked").removeAttr("checked");
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');

                      }
                  }
           });
        });


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
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');

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
                }                 
                
                
         }); 


$(function () {
     
    var selectedRB = $('#<%= RbTermType.ClientID %> input:checked');

                var selectedValue = selectedRB.val();
                if (selectedValue == "1") {   
                    $('#<%= lblTermType.ClientID%>').text(''); 
                  
                }
                else {
                    $('#<%= lblTermType.ClientID%>').text(''); 

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
              
                  }
                  else {
                      $('#<%=Div27.ClientID%>').hide(); 

                  }

           
        });




          $(function () {
              $('#<%= rbflood2.ClientID%>').on('click', function () {
                 

                  if ($('#<%= rbflood2.ClientID%>').prop('checked')) {
                     
                      $('#<%=Div27.ClientID%>').hide();
                      $('#<%= rbflood.ClientID%>').removeAttr("checked");
                      var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                     radiolist.removeAttr('checked');

                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                      document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';

                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');
                  }
                  else {                 
                       $('#<%=Div27.ClientID%>').hide();

                      var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                     radiolist.removeAttr('checked');
                     
                       $('#<%= rbflood.ClientID%>').removeAttr("checked");

                    $('#<%=Div28.ClientID%>').hide();    
                    $('#<%=Div29.ClientID%>').hide();  
                    $('#<%=Div30.ClientID%>').hide();  
                    $('#<%=Div31.ClientID%>').hide(); 
                      document.getElementById("<%=txt_wordReason1.ClientID %>").value = '';
                      var radiolist1 = $('#<%= txt_wordReason2.ClientID%>').find('input:radio');
                      var radiolist2 = $('#<%= txt_wordReason3.ClientID%>').find('input:radio');
                      var radiolist3 = $('#<%= txt_wordReason4.ClientID%>').find('input:radio');
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');
                   

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
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');

                }                 
                
                 
            }); 



         $(function () {
              $('#<%= chkSameAdd.ClientID%>').on('click', function () {
                 

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

         
          $(function () {
              $('#<%= chkOtherPerils.ClientID%>').on('click', function () {
                  $('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                  $('#<%= chkTR.ClientID%>').removeAttr("checked");
                  $('#<%= rbflood.ClientID%>').removeAttr("checked");

                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                      if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                          $('#<%=Div27.ClientID%>').show();
                          $('#<%= rbflood2.ClientID%>').removeAttr("checked");

                        
                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          radiolist.removeAttr('checked');

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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
                 
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          $("table[id$=chkl5] input:radio:checked").removeAttr("checked");
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');

                      }
                
                  }
                  else {

                      $('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      $('#<%= chkTR.ClientID%>').removeAttr("checked");
                      $('#<%= rbflood.ClientID%>').removeAttr("checked");

                       if ($('#<%= rbflood.ClientID%>').prop('checked')) {
                          $('#<%=Div27.ClientID%>').show();
                          $('#<%= rbflood2.ClientID%>').removeAttr("checked");

                       
                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          radiolist.removeAttr('checked');

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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
            
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          $("table[id$=chkl5] input:radio:checked").removeAttr("checked");
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');

                      }

                  }

           });
            });


             $(function () {
              $('#<%= chkSRCC.ClientID%>').on('click', function () {
                 
                  $('#<%= chkTR.ClientID%>').removeAttr("checked");

                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                      $('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      $('#<%= chkTR.ClientID%>').removeAttr("checked");
                     custom_alert( 'Please Select Other perils to get SRCC', 'Alert' );
                     
                  }

           });
        });

            $(function () {
              $('#<%= chkTR.ClientID%>').on('click', function () {
                 
                  

                  if ($('#<%= chkSRCC.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                      $('#<%= chkSRCC.ClientID%>').removeAttr("checked");
                      $('#<%= chkTR.ClientID%>').removeAttr("checked");
                   
                     custom_alert( 'Please Select SRCC to get TR', 'Alert' );
                   
                  }

           });
        });
             $(function () {
              $('#<%= rbflood.ClientID%>').on('click', function () {
                 
                 

                  if ($('#<%= chkOtherPerils.ClientID%>').prop('checked')) {
                     
                
                  }
                  else {

                      $('#<%= rbflood.ClientID%>').removeAttr("checked");
                   
                     custom_alert( 'Please Select Other perils to get flood cover', 'Alert' );
                      
                  }

           });
        });
         //end

        /********************************************************************************/
        }

    });

   

</script>


    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="hfRefId" runat="server" />
    <asp:HiddenField ID="hfSumInsu" runat="server" />
    <asp:HiddenField ID="hfFlag" runat="server" />
    <asp:HiddenField ID="hfbankcode" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />
       

      <div class="container border border-info" id="mainDiv" runat="server">

        <div class="form-group row" runat="server" id="rowFirst">
        <div class="col-sm-12 text-center h5 font-weight-bold"><label runat="server" id="txtHeading"></label> Proposal Details</div>
        </div>

          <div class="form-group row" runat="server" id="Div32">
        <div class="col-sm-2"></div>
        <div class="col-sm-6 text-warning font-weight-bold text-center">Only customer name and address available for changes.</div>
        <div class="col-sm-4">

            <asp:Button ID="Button1" runat="server"  Text="Apply Changes" CssClass="btn btn-info text-white" OnClientClick="return clientFunction()"  OnClick="Button1_Click"  />
            <asp:Button ID="Button2" runat="server"  Text="Back"  OnClientClick="this.value='Please wait...'" CssClass="btn btn-info text-white" OnClick="Button2_Click"  />
                            
        </div>
       
        </div>

        <div class="form-group row" runat="server" id="Div1">
        <div class="col-sm-4">1. Name of proposer:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
            <div class="col-sm-1 p-0 ml-3 mr-3" runat="server" visible="false"><asp:DropDownList ID="ddlInitials" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false" >
                <asp:ListItem Value ="0">Select</asp:ListItem>
                <asp:ListItem Value ="Dr. ">Dr</asp:ListItem>
                <asp:ListItem Value ="Mr. ">Mr</asp:ListItem>
                <asp:ListItem Value ="Mrs. ">Mrs.</asp:ListItem>
                <asp:ListItem Value ="Miss. ">Miss</asp:ListItem>
                <asp:ListItem Value ="Ms. ">Ms</asp:ListItem>
                <asp:ListItem Value ="Rev. ">Rev</asp:ListItem>
                </asp:DropDownList></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_nameOfProp" runat="server" CssClass="form-control border border-warning"  ClientIDMode="Static" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-2"><asp:Label ID="lblnameOfProp" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div2">
        <div class="col-sm-4">2. NIC number:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_nic" runat="server" CssClass="form-control text-capitalize" ClientIDMode="Static"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lblNic" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium"></asp:Label></div>
        </div>

         <div class="form-group row" runat="server" id="Div3" visible="false">
        <div class="col-sm-4">3. Business registration No:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_br" runat="server" CssClass="form-control"  ClientIDMode="Static"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label2" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div4">
        <div class="col-sm-4">3. Postal address line 1:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_addline1" runat="server" CssClass="form-control border border-warning"  ClientIDMode="Static" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-2"><asp:Label ID="Label1" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div5">
        <div class="col-sm-4 pl-5">Address line 2:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_addline2" runat="server" CssClass="form-control border border-warning"  ClientIDMode="Static" AutoPostBack="false" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lblpostaladdress" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium"></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div6">
        <div class="col-sm-4 pl-5">Address line 3:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_addline3" runat="server" CssClass="form-control border border-warning"  ClientIDMode="Static" AutoPostBack="false" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label3" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

          <div class="form-group row" runat="server" id="Div7">
        <div class="col-sm-4 pl-5">Address line 4:</div>
        <div class="col-sm-4"><asp:DropDownList ID="txt_addline4" runat="server" CssClass="custom-select text-capitalize border border-warning" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false" >
                      <asp:ListItem Value ="0">-- Select --</asp:ListItem>
                </asp:DropDownList></div>
        <div class="col-sm-3"><asp:Label ID="Label4" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div8">
        <div class="col-sm-4">4. Mobile No:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_tele" runat="server" CssClass="form-control"  ClientIDMode="Static" ></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lbltelePhone" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

           <div class="form-group row" runat="server" id="Div9">
        <div class="col-sm-4">5. Land line No:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_landLine" runat="server" CssClass="form-control"  ClientIDMode="Static" ></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lbllandPhone" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

           <div class="form-group row" runat="server" id="Div10">
        <div class="col-sm-4">6. Email address:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_email" runat="server" CssClass="form-control"  ClientIDMode="Static" TextMode="Email" ></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label5" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>


           <div class="form-group row" runat="server" id="Div11">
        <div class="col-sm-4"> 7. Address of dwelling house to be insured: <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Address line 1:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_dweAdd1" runat="server" CssClass="form-control border border-warning"  ClientIDMode="Static" AutoPostBack="false" autocomplete="off"></asp:TextBox></div>
        
               
        <div class="custom-control custom-checkbox col-sm-3 pl-5" runat="server">
        <input type="checkbox" class="custom-control-input" name="chkSameAdd" id="chkSameAdd" runat="server" ClientIDMode="Static" value="1">
        <label class="custom-control-label" for="chkSameAdd">Same as above</label>
        </div> 
        </div>


          <div class="form-group row" runat="server" id="Div12">
        <div class="col-sm-4 pl-5">Address line 2:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_dweAdd2" runat="server" CssClass="form-control border border-warning"  ClientIDMode="Static" AutoPostBack="false" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lbldwelAdd" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>


          <div class="form-group row" runat="server" id="Div13">
        <div class="col-sm-4 pl-5">Address line 3:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_dweAdd3" runat="server" CssClass="form-control border border-warning"  ClientIDMode="Static" AutoPostBack="false" autocomplete="off"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label7" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>

          <div class="form-group row" runat="server" id="Div14">
        <div class="col-sm-4 pl-5">Address line 4:</div>
        <div class="col-sm-4"><asp:DropDownList ID="txt_dweAdd4" runat="server" CssClass="custom-select text-capitalize border border-warning" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false">
                                          <asp:ListItem Value ="0">-- Select --</asp:ListItem>
                                          </asp:DropDownList></div>
        <div class="col-sm-3"><asp:Label ID="Label6" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>


          <div class="form-group row" runat="server" id="DivTermType">
        <div class="col-sm-4">8. Policy type<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-7">

                <asp:RadioButtonList ID="RbTermType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group"> 
                                     <asp:ListItem  runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz" id="termAnual">&nbsp;&nbsp;Annual private dwelling policy&nbsp;&nbsp;</asp:ListItem>
                                     <asp:ListItem  runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz" id="termLong">&nbsp;&nbsp;Long term private dwelling policy</asp:ListItem>
                                     </asp:RadioButtonList>               
        </div>
        <div class="col-sm-1"><asp:Label ID="lblTermType" runat="server" Text="" ForeColor="Red" Font-Bold="True" Font-Size="Small" ></asp:Label></div>
        </div>




            <div class="form-group row" runat="server" id="Div15">
        <div class="col-sm-4"> 9. Period of insurance<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-3"> From: <asp:TextBox ID="txt_fromDate" runat="server" CssClass="form-control text-center"  ClientIDMode="Static"></asp:TextBox></div>
       
           <div class="col-sm-1" runat="server" id="DivTerm" style="display:none" ClientIDMode="Static">Terms
               <asp:TextBox ID="ddlNumberOfYears" runat="server" CssClass="form-control text-center"  ClientIDMode="Static"></asp:TextBox>
            

           </div>      
                
                <div class="col-sm-3">To: <asp:TextBox ID="txt_toDate" runat="server" CssClass="form-control text-center"  ClientIDMode="Static"></asp:TextBox></div>
       <div class="col-sm-1">
           <asp:Label ID="lbfromDat" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
         
           </div>
                </div>

           <div class="form-group row" runat="server" id="Div16">
        <div class="col-sm-4">10. Is the house under construction:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:RadioButtonList ID="chkl1" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group">
                                              <asp:ListItem id="yes" runat="server" Value="1" class="radio-inline m-2 radioClz" Text="Yes">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                              <asp:ListItem  id="no" runat="server" Value="0" class="radio-inline m-2 radioClz" Text="No">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>

            
        </div>
        <div class="col-sm-3"><asp:Label ID="lblContact" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>



        <div class="form-group row" runat="server" id="Div17">
        <div class="col-sm-4">11.1. Value of the bank facility (LKR) : </div>
        <div class="col-sm-4"><asp:TextBox ID="txt_bankVal" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="form-control" onkeypress="javascript:return isNumber(event);" onkeyup = "javascript:this.value=Comma(this.value);"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label8" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>
             <%-- changes NSB 11032022--%>
          <div class="form-group row" runat="server" id="Div44">
        <div class="col-sm-1"></div>
        <div class="col-sm-3 float-right">11.2. Loan Number &nbsp;:</div>
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

<div class="form-group row" runat="server" id="Div19">
        <div class="col-sm-1"></div>
        <div class="col-sm-7 border border-dark">Value of the building and fixture-fittings including electrical and water installation.<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-2 border border-dark p-1">
            <asp:TextBox class="txt" ID="txt_sumInsu1" runat="server" ClientIDMode="Static" CssClass="form-control" onkeypress="javascript:return isNumber(event);" onkeyup = "javascript:this.value=Comma(this.value);"></asp:TextBox>             
        </div>
        <div class="col-sm-2"><asp:Label ID="lblsumInsu1" runat="server" Text="" ForeColor="Red" Font-Bold="True" Font-Size="Small" ></asp:Label></div>
        </div>


<div class="form-group row" runat="server" id="Div20">
        <div class="col-sm-1"></div>
        <div class="col-sm-7 border border-dark">Value of the boundary and parapet wall</div>
        <div class="col-sm-2 border border-dark p-1">
             <asp:TextBox class="txt" ID="txt_sumInsu2" runat="server" ClientIDMode="Static" CssClass="form-control"  onkeypress="javascript:return isNumber(event)" onkeyup = "javascript:this.value=Comma(this.value);"></asp:TextBox>                
        </div>
        <div class="col-sm-2"><asp:Label ID="lblsumInsu2" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label> </div>
        </div>
          <div class="form-group row" runat="server" id="Div20I" style="display:none">
        <div class="col-sm-1"></div>
        <div class="col-sm-7 border border-dark">On solar panel system & standard accessories<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-2 border border-dark p-1">
             <asp:TextBox class="txt" ID="txt_solar" runat="server" ClientIDMode="Static" CssClass="form-control"  onkeypress="javascript:return isNumber(event)" onkeyup = "javascript:this.value=Comma(this.value);"></asp:TextBox>                
        </div>
        <div class="col-sm-2"><asp:Label ID="lblSolar" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label> </div>
        </div>
<div class="form-group row" runat="server" id="Div21">
        <div class="col-sm-1"></div>
        <div class="col-sm-7 text-center border border-dark">Total</div>
        <div class="col-sm-2 border border-dark p-1">
             <asp:TextBox ID="txt_sumInsuTotal" runat="server" ClientIDMode="Static" CssClass="form-control"></asp:TextBox>               
        </div>
        <div class="col-sm-2"></div>
        </div>

          <%--solar questions--%>

            <div class="form-group row" runat="server" id="Div37" style="display:none">
        <div class="col-sm-1"></div>
        <div class="col-sm-6 font-weight-bold">If solar panel to be insured (Solar panel system details)</div>
        <div class="col-sm-2"></div>
        <div class="col-sm-2">
            <asp:Label ID="Label12" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>

          <div class="form-group row" runat="server" id="Div34" style="display:none">
        <div class="col-sm-1"></div>
        <div class="col-sm-6">I) Is the local repairer / sole agent available in Sri Lanka in the event of repairable damage in respect of solar panel system?</div>
        <div class="col-sm-2">
                                          <asp:RadioButtonList ID="rbSolOne" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group"> 
                                              <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                              <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>   
            </div>
        <div class="col-sm-2">
            <asp:Label ID="lblrbSolOne" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>

<div class="form-group row" runat="server" id="Div35" style="display:none">
        <div class="col-sm-1"></div> 
        <div class="col-sm-6">II) Is the spare parts available in Sri Lanka in the event of repairable damage in respect of solar panel system? </div>
        <div class="col-sm-2">
        
                                          <asp:RadioButtonList ID="rbSolTwo" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group" > 
                                              <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                              <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList>  
            </div>
        <div class="col-sm-2">
             <asp:Label ID="lblrbSolTwo" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>


<div class="form-group row" runat="server" id="Div36" style="display:normal">
        <div class="col-sm-1"></div>
        <div class="col-sm-6">III)&nbsp; Country of origin of solar panel system?</div>
        <div class="col-sm-3">
                                    <asp:TextBox class="txt" ID="txtSolarCountry" runat="server" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>  
            </div>
        <div class="col-sm-2">
             <asp:Label ID="lblrbSolThree" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
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
                                     <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                     <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                                     </asp:RadioButtonList>               
        </div>
        <div class="col-sm-2"><asp:Label ID="lblflood1" runat="server" Text="" ForeColor="Red" Font-Bold="True" Font-Size="Small" ></asp:Label></div>
        </div>



<div class="form-group row" runat="server" id="Div23" style="display:none">
    <div class="col-sm-1"></div>
        <div class="col-sm-3">If yes, Please give details<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-5">
                <asp:TextBox ID="txt_ninethReason" runat="server" CssClass="form-control"  ClientIDMode="Static"></asp:TextBox>            
        </div>
        <div class="col-sm-2"><asp:Label ID="lblreason3" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>


<div class="form-group row" runat="server" id="Div24">
        <div class="col-sm-4">14. Number of floors (Including ground floor):<asp:Label runat="server" ForeColor="Red">*</asp:Label> </div>
        <div class="col-sm-2">
                <asp:TextBox ID="txtNoofFloors" runat="server" CssClass="form-control text-center"  ClientIDMode="Static" Width="60%" TextMode="Number" min="0" onkeypress="javascript:return isNumber(event);" MaxLength="2"></asp:TextBox>         
        </div>
        <div class="col-sm-5"><asp:Label ID="lblnoFloors" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>


<div class="form-group row" runat="server" id="Div25">
        <div class="col-sm-4">15. Construction Details of the Building :</div>
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
              <label class="col-sm-5">i. Fire & Lighting</label><asp:CheckBox ID="chkFirLight" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2" Checked="true"/>
            </div>
        <div class="col-sm-3"></div>
        </div>
          <div class="form-group row" runat="server" id="Div39">
        <div class="col-sm-4"></div>
        <div class="col-sm-4">
                <label class="col-sm-5">ii.	Other Perils
                      <a href="#" data-toggle="tooltip" title="<p>1. Fire & Lightning<br/>
                2. Malicious Damage<br/>
                3. Explosion<br/>
                4. Cyclone, Storm, Tempest<br/>
                5. Earthquake with Fire & Shock<br/>
                6. Natural Disaster<br/>
                7. Aircraft Damage<br/>
                8. Impact Damage<br/>
                9. Bursting or Overflowing of Water Tanks, Apparatus or pipes<br/>
                10. Electrical Inclusion Clause</p>"><img src="../Images/select.png" class="img-fluid"/></a>
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
                                 <asp:ListItem id="chk15_1" runat="server" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
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
                 <asp:TextBox ID="txt_wordReason1" runat="server" CssClass="form-control"  ClientIDMode="Static"></asp:TextBox>
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
                                              <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
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
                                              <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
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
                                              <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz">&nbsp;&nbsp;Yes&nbsp;&nbsp;</asp:ListItem>
                                              <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz">&nbsp;&nbsp;No</asp:ListItem>
                                        </asp:RadioButtonList> 
            </div>
        <div class="col-sm-2">
             <asp:Label ID="Label11" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
        </div>
        </div>

 </div>

</asp:Content>