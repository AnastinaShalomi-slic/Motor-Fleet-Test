<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LoadProposalView.aspx.cs" Inherits="SLIC_Fire_LoadProposalView" %>
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
<style type="text/css" media="print">
    body {visibility:hidden; }
   .notPrint {visibility:hidden; display:none}
   .noprintcontent { visibility:hidden; }
   .print { visibility:visible; display:block; }
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
     function clientFunctionChanges()
     {
         var option = $('#<%=ddl_options.ClientID %>').val();
         var loadingVal = $('#<%=txtprecent.ClientID %>').val();
         var totalpay = $('#<%=txttotalpay.ClientID %>').val();

          if (option == "8" || option == "3" || option == "4")
          {
              if (loadingVal == "") {
            <%--$('#<%=lblnameOfProp.ClientID%>').html("Please Select Status."); --%>
                  custom_alert('Please enter loading values first.', 'Alert');
                  return false;
              }
              else if (loadingVal != "" && totalpay == "") {
                  custom_alert('Please press the loading button.', 'Alert');
                  return false;
              }
             // else { custom_alert('Processing.', 'Success');return true;}
             //return false;
         }
         custom_alert('Please wait, Processing', 'Success');
         //custom_alert('Processing for Premium!', 'Success');return true;
     }

      function clientFunctionLoading()
     {
         
          var loadingVal = $('#<%=txtprecent.ClientID %>').val();
          var totalpay = $('#<%=txttotalpay.ClientID %>').val();
         
          if (loadingVal == "") {
            <%--$('#<%=lblnameOfProp.ClientID%>').html("Please Select Status."); --%>
              custom_alert('Please enter loading values first.', 'Alert');
              return false;
          }
          else if (loadingVal != "" && totalpay == "")
          { custom_alert('Please press the loading button.', 'Alert');
              return false;
          }

          custom_alert('Please wait, Processing', 'Success');
       
         //custom_alert('Processing for Premium!', 'Success');return true;
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
      


            custom_alert('Please wait, Processing', 'Success');return true;
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
                          $('#<%= rbflood2.ClientID%>').removeAttr("checked");
                          $('#<%=Div27.ClientID%>').show();

                          var radiolist = $('#<%= chkl5.ClientID%>').find('input:radio');
                          radiolist.removeAttr('checked');
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
                          //$("table[id$=txt_wordReason2] input:radio:checked").removeAttr("checked");
                          //$("table[id$=txt_wordReason3] input:radio:checked").removeAttr("checked");
                          //$("table[id$=txt_wordReason4] input:radio:checked").removeAttr("checked");

                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
                      }
                      else {
                          $('#<%=Div27.ClientID%>').hide();
                          $("table[id$=chkl5] input:radio:checked").removeAttr("checked");
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
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
                       $('#<%= rbflood.ClientID%>').removeAttr("checked");


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
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');
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
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
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

                      $('#<%= rbflood.ClientID%>').removeAttr("checked");
                   
                     custom_alert( 'Please Select Other perils to get flood cover', 'Alert' );
                      
                  }

           });
        });

       $(function () {
        $('#<%= txtprecent.ClientID%>').on('change keyup paste', function () {
            
             document.getElementById("<%=txttotalpay.ClientID %>").value = '';

            });
         });

         //end


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
                          $('#<%= rbflood2.ClientID%>').removeAttr("checked");

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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
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
                      $('#<%= rbflood.ClientID%>').removeAttr("checked");
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
                     radiolist1.removeAttr('checked');
                     radiolist2.removeAttr('checked');
                     radiolist3.removeAttr('checked');
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
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
                          radiolist1.removeAttr('checked');
                          radiolist2.removeAttr('checked');
                          radiolist3.removeAttr('checked');
                    <%--document.getElementById("<%=txt_wordReason2.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason3.ClientID %>").value = '';
                    document.getElementById("<%=txt_wordReason4.ClientID %>").value = '';--%>
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

                      $('#<%= rbflood.ClientID%>').removeAttr("checked");
                   
                     custom_alert( 'Please Select Other perils to get flood cover', 'Alert' );
                      
                  }

           });
             });

            
       $(function () {
        $('#<%= txtprecent.ClientID%>').on('change keyup paste', function () {
            
           
            document.getElementById("<%=txttotalpay.ClientID %>").value = '';
            });
         });
         //end

        /********************************************************************************/
        }

    });

   

</script>

    
       <script language="javascript">
function printdiv(printpage)
{
var headstr = "<html><head><title></title></head><body>";
var footstr = "</body>";
var newstr = document.all.item(printpage).innerHTML;
var oldstr = document.body.innerHTML;
document.body.innerHTML = headstr+newstr+footstr;
window.print();
document.body.innerHTML = oldstr;
return false;
}
</script>



<script type="text/javascript">
        function PrintDivContent(divId) {
            var printContent = document.getElementById(divId);
            var WinPrint = window.open('', '', 'left=0,top=0,toolbar=0,sta­tus=0 , scrollbars=yes');
            WinPrint.document.write(printContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
    </script>

<script type="text/javascript">  
    function PrintDiv() 
   {  
       var divContents = document.getElementById("printdivcontent").innerHTML;  
       var printWindow = window.open('', '', 'height=200,width=400');  
       printWindow.document.write('<html><head><title>Print DIV Content</title>');  
       printWindow.document.write('</head><body >');  
       printWindow.document.write(divContents);  
       printWindow.document.write('</body></html>');  
       printWindow.document.close();  
       printWindow.print();  
    }  
</script> 
<script language="javascript" type="text/javascript">

    function Button1_onclick()
    {
        //var div = document.getElementById('innerData').outerHTML;
    //      var mywindow = window.open('', 'Print Contents');
    //      mywindow.document.write('<html><head><title>Print Contents</title>');
    //      mywindow.document.write('<style>.myDiv {border: 1px dotted black; text-align: center; width: 100%;}</style>');
    //      mywindow.document.write('</head><body>');
    //      mywindow.document.write(div);
    //      mywindow.document.write('</body></html>');

    //      mywindow.document.close();  // necessary for IE >= 10
    //      mywindow.focus();           // necessary for IE >= 10

    //      mywindow.print();
    //      mywindow.close();
                    //open new window set the height and width =0,set windows position at bottom
                    var a = window.open ('','','left =' + screen.width + ',top=' + screen.height + ',width=0,height=0,toolbar=0,scrollbars=0,status=0');
                    //write gridview data into newly open window
                    a.document.write(document.getElementById('innerData').innerHTML);
                    a.document.close();
                    a.focus();
                    //call print
                    a.print();
                    a.close();
                    return false;

    }
</script>
  
   
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" ScriptMode="Release"> </asp:ToolkitScriptManager>
    <asp:HiddenField ID="UserId" runat="server" />
    <asp:HiddenField ID="hfRefId" runat="server" />
    <asp:HiddenField ID="hfSumInsu" runat="server" />
    <asp:HiddenField ID="hfnetcal" runat="server" />
    <asp:HiddenField ID="hfadmincal" runat="server" />
    <asp:HiddenField ID="hfnbtcal" runat="server" />
    <asp:HiddenField ID="hfvatcal" runat="server" />
    <asp:HiddenField ID="hftotalcal" runat="server" />
    <asp:HiddenField ID="hfFlag" runat="server" />
    <asp:HiddenField ID="hfbankcode" runat="server" />
    <asp:HiddenField ID="hfbankUN" runat="server" />
     <asp:HiddenField ID="hfPolNo" runat="server" />
    <asp:HiddenField ID="brCode" runat="server" />
    <asp:HiddenField ID="hfAgentCode" runat="server" />

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
            

          <div class="container border border-info" id="Div32" runat="server">
                <asp:TextBox ID="txt_NetPre" runat="server" Visible="false"></asp:TextBox>             
                <asp:TextBox ID="txt_adminCal" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txt_nbt" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txt_vat" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txt_total" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtagent" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtTerm" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtPeriod" runat="server" Visible="false"></asp:TextBox>

                <asp:TextBox ID="txtRCC" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtTR" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtAdmin" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtRenewal" runat="server" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtPolicyFee" runat="server" Visible="false"></asp:TextBox>


        <div class="form-group row" runat="server" id="Div43">
        <div class="col-sm-12 text-center h5 font-weight-bold m-2">
            <asp:Label ID="lblreasonforhold" runat="server" Text="" CssClass="p-1 text-info"></asp:Label><br />
            <asp:Label ID="lblapproved" runat="server" Text="" CssClass="p-1 text-success"></asp:Label><br />
            <asp:Label ID="lblInterinChanges" runat="server" Text="" CssClass="p-1 text-warning"></asp:Label>                           
        </div>
        </div>
  <div class="form-group row" runat="server" id="Div44">
        <div class="col-sm-2 text-center h5 font-weight-bold"><label runat="server" id="wording1">Action :</label></div>
        <div class="col-sm-4 text-center h5 font-weight-bold">
               <asp:DropDownList ID="ddl_options" runat="server" CssClass="custom-select text-capitalize mt-1"
                                ClientIDMode="Static" AppendDataBoundItems="true" OnSelectedIndexChanged="ddl_options_SelectedIndexChanged" AutoPostBack="true" >
                               <asp:ListItem Selected="True" Text="--Select--" Value="0"></asp:ListItem>
                               <asp:ListItem Text="Approve" Value="1"></asp:ListItem>
                               <asp:ListItem Text="Conditions" Value="2"></asp:ListItem>
                               <asp:ListItem Text="Loadings" Value="3"></asp:ListItem>
                               <asp:ListItem Text="Conditions & Loadings" Value="4"></asp:ListItem>
                               <asp:ListItem Text="Reject" Value="5"></asp:ListItem>
                               <asp:ListItem Text="Deductible" Value="6"></asp:ListItem>
                               <asp:ListItem Text="Conditions & Deductible" Value="7"></asp:ListItem>
                               <asp:ListItem Text="Conditions, Deductible & Loadings" Value="8"></asp:ListItem>
                           </asp:DropDownList>
        </div>
      
<div class="col-sm-6 text-center h5 font-weight-bold">
                      <asp:Button ID="btnDone" runat="server"  Text="Back"  OnClientClick="this.value='Wait...'" CssClass="btn btn-info text-white mt-1" OnClick="btnDone_Click" /> 
                      <asp:Button ID="btViewSchhedule" runat="server"  Text="View Schedule"  OnClientClick="this.value='Wait...'" CssClass="btn btn-info text-white mt-1" OnClick="btViewSchhedule_Click"  />             
                      <asp:Button ID="btback" runat="server"  Text="Back"  OnClientClick="this.value='Wait...'" CssClass="btn btn-info text-white mt-1" OnClick="btback_Click" />  
                      <asp:Button ID="btbackReject" runat="server"  Text="Back"  OnClientClick="this.value='Wait...'" CssClass="btn btn-info text-white mt-1" OnClick="btback_Click2" /> 
                      <%--<asp:Button ID="Button1" runat="server"  Text="Print" CssClass="btn btn-info text-white mt-1" OnClientClick="PrintDiv();" />--%>
                      <%--  <input name="b_print" type="button" class="btn btn-primary" onClick="printdiv();" value=" Print ">--%>
                      <input id="Button1" type="button" value="Print" language="javascript" onclick="return Button1_onclick()" class="btn btn-info text-white mt-1"/>
   <%-- <input name="b_print" type="button" class="btn btn-primary" onClick="printdiv('innerData');" value=" Print ">--%>
                      <asp:Button ID="btChangeDeduct" runat="server"  Text="Change Deductible" CssClass="btn btn-info text-white mt-1" OnClick="btChangeDeduct_Click"   />
                      <asp:Button ID="btnCancelProp" runat="server"  Text="Cancel Shedule" CssClass="btn btn-info text-white mt-1" OnClick="btnCancelProp_Click"  />
</div>     
          </div>  
              

 <%--<div class="form-group row" runat="server" id="condtiontable">
        <div class="col-sm-12 text-center h5 font-weight-bold">Pending Proposal Details</div>
        </div>--%>
 <div class="m-0" runat="server" id="trcondi001">

       <div class="form-group row" runat="server" id="idApp">
           
           <div class="col-sm-4 text-center h4 font-weight-bold text-success">
               <asp:Label ID="Label14" runat="server" Text="Give approval"></asp:Label>
           </div>
       </div>
   <div class="form-group row" runat="server" id="idCondi">
           <div class="col-sm-2 h6 font-weight-bold text-center">
            <asp:Label ID="lblconditext" runat="server" Text="Conditions:"></asp:Label>
           </div>
       <div class="col-sm-4 h6 font-weight-bold">
           <asp:TextBox ID="txtcoditionSlic" runat="server" TextMode="MultiLine" CssClass="form-control" autocomplete="off"></asp:TextBox>
           </div>
        <div class="col-sm-2 h6 font-weight-bold">
          <asp:RequiredFieldValidator ID="req_Validator2" runat="server" ControlToValidate="txtcoditionSlic" ValidationGroup="VG01" CssClass="text-danger font-weight-normal">
              <img src="../Images/required.png" class="img-fluid"/></asp:RequiredFieldValidator>
           </div>
       </div>
 
 
     <div class="form-group row" runat="server" id="idLoading">
           <div class="col-sm-2 text-center h6 font-weight-bold">
             <asp:Label ID="lblnet" runat="server" Text="Net Premium:"></asp:Label>
           </div>
         <div class="col-sm-2 text-center h6 font-weight-bold">
             <asp:TextBox ID="txtnetpremium" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
           </div>


         <div class="col-sm-1 text-center h6 font-weight-bold">
             <asp:Label ID="lblloading" runat="server" Text="Loading(%)"></asp:Label>
           </div>
         <div class="col-sm-2 text-center h6 font-weight-bold">
             <asp:TextBox ID="txtprecent" runat="server" CssClass="form-control" onkeypress="javascript:return isNumber(event);" autocomplete="off"></asp:TextBox>
           </div>
         <div class="col-sm-0 text-center h6 font-weight-bold">
              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtprecent" ValidationGroup="VG01" CssClass="text-danger font-weight-normal">
                                             <img src="../Images/required.png" class="img-fluid"/></asp:RequiredFieldValidator>
           </div>


       <div class="col-sm-1 text-center h6 font-weight-bold">
         <asp:Label ID="lbltotalpay" runat="server" Text="TotalPayble:"></asp:Label>
       </div>
         <div class="col-sm-2 text-center h6 font-weight-bold">
             <asp:TextBox ID="txttotalpay" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
        </div>
         <div class="col-sm-0 text-center h6 font-weight-bold">
              <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttotalpay" ValidationGroup="VG01" CssClass="text-danger font-weight-normal">
                                             <img src="../Images/required.png" class="img-fluid"/></asp:RequiredFieldValidator>
           </div>
     </div>



 <div class="form-group row" runat="server" id="idReject">
           <div class="col-sm-4 text-center h4 font-weight-bold text-danger">
            <asp:Label ID="Label13" runat="server" Text="Reject Proposal"></asp:Label>
           </div>
       </div>

 <div class="form-group row" runat="server" id="idDedc">

           <div class="col-sm-2 text-center h6 font-weight-bold">
           <asp:Label ID="lblDeduc" runat="server" Text="Deducible Precentage(%):"></asp:Label>
           </div>
     <div class="col-sm-2 text-center h6 font-weight-bold">
           <asp:TextBox ID="txtDeducPre" runat="server"  onkeypress="javascript:return isNumber(event);" CssClass="form-control" autocomplete="off"></asp:TextBox>
           </div>

      <div class="col-sm-0 text-center h6 font-weight-bold">
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDeducPre" ValidationGroup="VG01" CssClass="text-danger font-weight-normal">
                                             <img src="../Images/required.png" class="img-fluid"/></asp:RequiredFieldValidator>
           </div>

      <div class="col-sm-1 text-center h6 font-weight-bold">
           <asp:Label ID="lbldeVal" runat="server" Text="Value:"></asp:Label>
           </div>
     <div class="col-sm-2 text-center h6 font-weight-bold">
           <asp:TextBox ID="txtDeducVal" runat="server" onkeypress="javascript:return isNumber(event);" CssClass="form-control" autocomplete="off"></asp:TextBox>
           </div>

      <div class="col-sm-2 text-center h6 font-weight-bold">
       <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDeducVal" ValidationGroup="VG01" CssClass="text-danger font-weight-normal">
                                             <img src="../Images/required.png" class="img-fluid"/></asp:RequiredFieldValidator>   
      </div>

       </div>

 <div class="form-group row" runat="server" id="Div45">
       <div class="col-sm-8 text-center h6 font-weight-bold">
       <asp:Button ID="btLoading" runat="server"  Text="Loading"  CssClass="btn btn-info text-white" OnClick="btLoading_Click" ValidationGroup="VG02"  />
       <asp:Button ID="btnProceedView" runat="server"  Text="Apply Changes"  OnClientClick="clientFunctionChanges()" CssClass="btn btn-info text-white" OnClick="btnProceedView_Click" ValidationGroup="VG01"  />
       <asp:Button ID="btDeductApply" runat="server"  Text="Apply Deductible"  OnClientClick="this.value='Please wait...'" CssClass="btn btn-info text-white" OnClick="btDeductApply_Click" />            
       </div>
       </div>



 </div>
        </div>

<%--proposal details sections--%>
           
     <div id="innerData">          
 <div class="container border border-info" id="mainDiv" runat="server">

        <div class="form-group row" runat="server" id="rowFirst">
        <div class="col-sm-12 text-center h5 font-weight-bold"><label runat="server" id="txtHeading"></label> Proposal Details</div>
        </div>

        <div class="form-group row" runat="server" id="Div1">
        <div class="col-sm-4">1. Name of proposer:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
            <div class="col-sm-1 p-0 ml-3 mr-3" runat="server" visible="false"><asp:DropDownList ID="ddlInitials" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false" >
                <asp:ListItem Value ="">Select</asp:ListItem>
                <asp:ListItem Value ="Dr. ">Dr</asp:ListItem>
                <asp:ListItem Value ="Mr. ">Mr</asp:ListItem>
                <asp:ListItem Value ="Mrs. ">Mrs.</asp:ListItem>
                <asp:ListItem Value ="Miss. ">Miss</asp:ListItem>
                <asp:ListItem Value ="Ms. ">Ms</asp:ListItem>
                <asp:ListItem Value ="Rev. ">Rev</asp:ListItem>
                </asp:DropDownList></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_nameOfProp" runat="server" CssClass="form-control"  ClientIDMode="Static" ></asp:TextBox></div>
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
        <div class="col-sm-4"><asp:TextBox ID="txt_addline1" runat="server" CssClass="form-control"  ClientIDMode="Static"></asp:TextBox></div>
        <div class="col-sm-2"><asp:Label ID="Label1" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div5">
        <div class="col-sm-4 pl-5">Address line 2:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_addline2" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lblpostaladdress" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium"></asp:Label></div>
        </div>

        <div class="form-group row" runat="server" id="Div6">
        <div class="col-sm-4 pl-5">Address line 3:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_addline3" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label3" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>

          <div class="form-group row" runat="server" id="Div7">
        <div class="col-sm-4 pl-5">Address line 4:</div>
        <div class="col-sm-4">
            <asp:TextBox ID="txt_addline4" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false"></asp:TextBox>
        </div>
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
        <div class="col-sm-4"><asp:TextBox ID="txt_dweAdd1" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false"></asp:TextBox></div>
        
<%--<div class="col-sm-4"><asp:CheckBox ID="" runat="server" Value="1" Text="" style="padding-left:10px;"/></div>--%>
               
        <div class="custom-control custom-checkbox col-sm-3 pl-5" runat="server" visible="false">
        <input type="checkbox" class="custom-control-input" name="chkSameAdd" id="chkSameAdd" runat="server" ClientIDMode="Static" value="1">
        <label class="custom-control-label" for="chkSameAdd">Same as above</label>
        </div> 
        </div>


          <div class="form-group row" runat="server" id="Div12">
        <div class="col-sm-4 pl-5">Address line 2:<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-4"><asp:TextBox ID="txt_dweAdd2" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="lbldwelAdd" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label></div>
        </div>


          <div class="form-group row" runat="server" id="Div13">
        <div class="col-sm-4 pl-5">Address line 3:</div>
        <div class="col-sm-4"><asp:TextBox ID="txt_dweAdd3" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false"></asp:TextBox></div>
        <div class="col-sm-3"><asp:Label ID="Label7" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>

          <div class="form-group row" runat="server" id="Div14">
        <div class="col-sm-4 pl-5">Address line 4:</div>
        <div class="col-sm-4">
            <asp:TextBox ID="txt_dweAdd4" runat="server" CssClass="form-control"  ClientIDMode="Static" AutoPostBack="false"></asp:TextBox>
        </div>
        <div class="col-sm-3"><asp:Label ID="Label6" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label></div>
        </div>


          <div class="form-group row" runat="server" id="DivTermType">
        <div class="col-sm-4">8. Policy type<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-7">

                <asp:RadioButtonList ID="RbTermType" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal" AppendDataBoundItems="True" AutoPostBack="false"  CssClass="btn-group"> 
                                     <asp:ListItem runat="server" Text="Yes" Value="1" class="radio-inline m-2 radioClz" id="termAnual">&nbsp;&nbsp;Annual private dwelling policy&nbsp;&nbsp;</asp:ListItem>
                                     <asp:ListItem runat="server" Text="No" Value="0" class="radio-inline m-2 radioClz" id="termLong">&nbsp;&nbsp;Long term private dwelling policy</asp:ListItem>
                                     </asp:RadioButtonList>               
        </div>
        <div class="col-sm-1"><asp:Label ID="lblTermType" runat="server" Text="" ForeColor="Red" Font-Bold="True" Font-Size="Small" ></asp:Label></div>
        </div>




            <div class="form-group row" runat="server" id="Div15">
        <div class="col-sm-4"> 9. Period of insurance<asp:Label runat="server" ForeColor="Red">*</asp:Label></div>
        <div class="col-sm-3"> From: <asp:TextBox ID="txt_fromDate" runat="server" CssClass="form-control text-center"  ClientIDMode="Static"></asp:TextBox></div>
       
           <div class="col-sm-1" runat="server" id="DivTerm" style="display:none" ClientIDMode="Static">Terms
               <%--<asp:DropDownList ID="ddlNumberOfYears" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false">
                                          <asp:ListItem Value ="0">Select</asp:ListItem>
                                          </asp:DropDownList>--%>
                   <asp:TextBox ID="ddlNumberOfYears" runat="server" CssClass="form-control text-center"  ClientIDMode="Static"></asp:TextBox>
             <%--  <asp:DropDownList ID="ddlNumberOfYears" runat="server" CssClass="custom-select text-capitalize" ClientIDMode="Static" AppendDataBoundItems="true" AutoPostBack="false">
                                          <asp:ListItem Value ="0">Select</asp:ListItem>
                                          </asp:DropDownList>--%>
           </div>      
                
                <div class="col-sm-3">To: <asp:TextBox ID="txt_toDate" runat="server" CssClass="form-control text-center"  ClientIDMode="Static"></asp:TextBox></div>
       <div class="col-sm-1">
           <asp:Label ID="lbfromDat" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
           <%--<asp:Label ID="lbltoDate" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Medium" ></asp:Label>--%>
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
          <div class="form-group row" runat="server" id="Div46">
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
        <div class="col-sm-7 border border-dark">On solar panel system & standard accessories</div>
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
        <div class="col-sm-6">I) Is the local repairer / sole agent available in Sri Lanka in the event of repairable damage in respect of Solar panel system?</div>
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

          <div class="form-group row" runat="server" id="Div42" style="display:normal">
        <div class="col-sm-1"></div>
        <div class="col-sm-6">IV)&nbsp; Model number or serial number (If available)</div>
        <div class="col-sm-3">
                                   <asp:TextBox class="txt" ID="txtSoloarModel" runat="server" ClientIDMode="Static" CssClass="form-control" autocomplete="off"></asp:TextBox>  
            </div>
        <div class="col-sm-2">
             <asp:Label ID="Label15" runat="server" Text="" ForeColor="Red" Font-Bold="false" Font-Size="Small" ></asp:Label>
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
                                <%--<label class="h6" runat="server">Brick</label>--%>Brick<asp:CheckBox ID="Chkbrick" runat="server" Value="1" Text="" CssClass="ChkBoxClass m-2"/>
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
       <%-- <div class="col-sm-1"></div>--%>
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

  <%--end--%>



 </div>
     </div>

              
      

</div>

     </ContentTemplate>
             
                    <Triggers>
         <asp:Asyncpostbacktrigger controlid="ddl_options" eventname="SelectedIndexChanged" />
                       </Triggers>
    </asp:UpdatePanel>

</asp:Content>