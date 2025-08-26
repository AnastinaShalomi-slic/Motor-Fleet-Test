using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;
using System;
using System.Configuration;
using System.Data.OracleClient;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

public partial class Bank_Fire_DebitNote : System.Web.UI.Page
{
    GetDebitDetails_API.DebitNotePostDataModel DebitNotePostDataModel = new GetDebitDetails_API.DebitNotePostDataModel();
    GetDebitDetails_API GetDebitDetails_API = new GetDebitDetails_API();

    GetDebitDetails_API.DebitnoteDetailsApiResponse DebitnoteDetailsApiResponse = new GetDebitDetails_API.DebitnoteDetailsApiResponse();

    GetProposalDetails getPropClass = new GetProposalDetails();

    string SC_POLICY_NO, SC_SUM_INSU, SC_NET_PRE,
            SC_RCC, SC_TR, SC_ADMIN_FEE,
            SC_POLICY_FEE, SC_NBT, SC_VAT, SC_TOTAL_PAY, CREATED_ON, CREATED_BY,
            FLAG, SC_Renewal_FEE, BPF_FEE, DEBIT_NO = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                var en = new EncryptDecrypt();
                // Retrieve and decrypt parameters from the query string
                string debitNo = en.Decrypt(Request.QueryString["Data"]).Trim().ToString();
                string datepayment = en.Decrypt(Request.QueryString["PaymentDate"]).Trim().ToString();
                string branch = en.Decrypt(Request.QueryString["dh_bbrcode"]).Trim().ToString();
                string BANCGI = en.Decrypt(Request.QueryString["BANCGI"]).Trim().ToString();

                string refN = en.Decrypt(Request.QueryString["refN"]).Trim().ToString();

                this.getPropClass.GetSchedualCalValues(refN, out SC_POLICY_NO, out SC_SUM_INSU, out SC_NET_PRE,
              out SC_RCC, out SC_TR, out SC_ADMIN_FEE,
              out SC_POLICY_FEE, out SC_NBT, out SC_VAT, out SC_TOTAL_PAY, out CREATED_ON, out CREATED_BY,
              out FLAG, out SC_Renewal_FEE, out BPF_FEE, out DEBIT_NO);

                // Assuming branch is "7010001-City Office"
                string[] parts = branch.Split('-');
                if (parts.Length > 1)
                {
                    litBranch.Text = parts[1]; // This will set litBranch.Text to "City Office"
                }
                else
                {
                    litBranch.Text = branch; // Fallback if the hyphen is not found
                }

                int data = int.Parse(debitNo);
                int BANCAGI = int.Parse(BANCGI);

                var debitNotePostDataModel = new GetDebitDetails_API.DebitNotePostDataModel
                {
                    sliBranch = BANCAGI,
                    paymentDate = datepayment,
                    paymentLocation = 21, // Hardcoded value
                    paymentType = "211",  // Hardcoded value
                    sequenceNo = data
                };

                // Call the GetDebitDetailsdata method and wait for the result synchronously
                var api = new GetDebitDetails_API();
                Task<GetDebitDetails_API.DebitnoteDetailsApiResponse> apiTask = api.GetDebitDetailsdata(debitNotePostDataModel);
                apiTask.Wait();  // Wait for the task to complete

                GetDebitDetails_API.DebitnoteDetailsApiResponse response = apiTask.Result;

                if (response.ResponseCode == 200 && response.Data != null)
                {
                    // Extract data from the response
                    int suminsured = response.Data.sumInsured;
                    string policyNo = response.Data.policyNo;
                    string policytype = response.Data.policyType;
                    int debitnumber = response.Data.sequenceNo;
                    DateTime stared = response.Data.startDate;
                    DateTime end = response.Data.expireDate;
                    string fullname = response.Data.name1;
                    string fullname2 = response.Data.name2;
                    double netpremium = response.Data.premium;
                    double policyfee = response.Data.policyFee;
                    double adminfee = response.Data.cess;
                    double srcc = response.Data.rcc;
                    double tc = response.Data.tc;
                    double nbt = response.Data.nbt;
                    double totaldebitamount = response.Data.paymentAmount1;
                    DateTime paymentdate = response.Data.paymentDate;
                    string add1 = response.Data.address1;
                    string add2 = response.Data.address2;
                    string add3 = response.Data.address3;
                    string add4 = response.Data.address4;
                    double tax = response.Data.roadTax;
                    double vat = response.Data.vat;
                    string debtorcode = response.Data.debtorCode;
                    string year = DateTime.Now.Year.ToString();
                    string periodInsu = String.Format("From {0:yyyy-MM-dd} To {1:yyyy-MM-dd}", stared, end);
                    string totalInWords = ConvertCurrencyToWords(totaldebitamount);
                    string srbranch = response.Data.serviceBranch;
                    string client = response.Data.clientId;
                    string agentcode = response.Data.agentCode;
                    string ageName = GetAgeName(agentcode);
                    int sliccode1 = response.Data.sliNo1;
                    int sliccode2 = response.Data.sliNo2;
                    string MEname = GetMEName(sliccode1, sliccode2);
                    string combinedSlicode = sliccode1.ToString() + sliccode2.ToString() + " " + MEname;
                    string status = response.Data.status;

                    // Set values in textboxes or labels
                    txtsum.Text = suminsured.ToString("N2");
                    txtdebitNo.Text = DEBIT_NO;
                    txtpolicy.Text = policyNo;
                    txtNetPremium.Text = netpremium.ToString("N2");
                    txtPeriodInsu.Text = periodInsu.ToString();
                    txtnameinsu.Text = String.Concat(status, fullname, fullname2);
                    txtPolicyFee.Text = policyfee.ToString("N2");
                    txtAdminFee.Text = (adminfee + nbt).ToString("N2");
                    txtSrcc.Text = srcc.ToString("N2");
                    txttc.Text = tc.ToString("N2");
                    txtTotalDebitAmount.Text = totaldebitamount.ToString("N2");
                    //txtDebitAmount.Text = 
                    txtSystemDate.Text = paymentdate.ToString("yyyy-MM-dd");
                    txtadd.Text = add1 + " " + add2 + " " + add3 + " " + add4;
                    txtTaxes.Text = vat.ToString("N2");
                    //txtvat.Text = vat.ToString("N2");
                    txtDebitAmount.Text = totalInWords;
                    txtDebitNoteNo.Text = DEBIT_NO;
                    txtNameDebtor.Text = debtorcode.ToString() + " " + ageName.ToString();
                    txtPaidOn.Text = policytype.ToString();
                    // Check if srbranch is not null or empty, if not, set txtfilebranch to srbranch, else set it to BANCAGI
                    txtfilebranch.Text = !string.IsNullOrEmpty(srbranch) ? srbranch : BANCAGI.ToString();


                    //TxtAgencyCode.Text = agentcode.ToString();
                    //Txtslicode.Text = sliccode1.ToString() + sliccode2.ToString() +" " + MEname.ToString();


                    txtClientID.Text = client != null ? client.ToString() : string.Empty;
                    lblDate.Text = DateTime.Now.ToString("yyyy/MM/dd");   // Current Date
                    lblTime.Text = DateTime.Now.ToString("HH:mm:ss");     // Current Time
                    lblID.Text = srbranch;                                 // Dynamically set ID
                    lblIP.Text = GetIPAddress();

                    TxtAgencyCode.Text = !string.IsNullOrEmpty(agentcode.ToString()) ? agentcode.ToString() : "N/A"; // Check for empty agency code
                    Txtslicode.Text = string.IsNullOrEmpty(combinedSlicode.Trim()) ? "N/A" : combinedSlicode;

                }
                else if (response.ResponseCode == 400)
                {
                    var endc = new EncryptDecrypt();

                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("".ToString()));
                }
            }
            catch (AggregateException ex)
            {
                // Handle exceptions that are aggregated from the Task
                foreach (var innerEx in ex.InnerExceptions)
                {
                    if (innerEx is FormatException)
                    {
                        // Handle format exception
                        Response.Write("Error: Invalid value in query string. Please check the input.");
                    }
                    else
                    {
                        // Handle other exceptions
                        Response.Write("Error: An unexpected error occurred - " + innerEx.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                var endc = new EncryptDecrypt();

                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("URL".ToString()));
            }
        }
    }

    private string GetAgeName(string agentcode)
    {
        // Initialize ageName to null; you can also set it to an empty string if preferred
        string ageName = null;

        // Fetch details from QUOTATION.fire_agent_info table using debtorCode
        using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]))
        {
            // SQL query to get the age_name based on debtorCode
            string sql_getagentdetails = "SELECT age_name FROM quotation.fire_agent_info WHERE AGENCY_CODE = :agentcode";

            using (var cmd = new OracleCommand(sql_getagentdetails, conn))
            {
                cmd.Parameters.Add(new OracleParameter("agentcode", agentcode.Trim())); // Use debtorCode instead of text box input

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar(); // Execute the query to get a single value

                    // If the result is not null, assign it to ageName
                    if (result != null)
                    {
                        ageName = result.ToString(); // Get the age name
                    }
                    // If result is null, ageName will remain null, which you can check later
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log error)
                    Response.Write("Error retrieving age name: " + ex.Message);
                }
            }
        }

        // Return null if no value was found; you can also return an empty string if desired
        return ageName; // Can return null or an empty string if no match found
    }


    private string GetMEName(int sliccode1, int sliccode2)
    {
        // Initialize meName to null; you can also set it to an empty string if preferred
        string meName = null;

        // Create a concatenated string from sliccode1 and sliccode2
        string mecode = (sliccode1.ToString() + sliccode2.ToString()).Trim();

        // Fetch details from QUOTATION.BANCASSURANCE_ME table using the concatenated mecode
        using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]))
        {
            // SQL query to get the ME name based on the mecode
            string sql_getMEName = "SELECT MENAME FROM QUOTATION.BANCASSURANCE_ME WHERE slicode = :mecode";

            using (var cmd = new OracleCommand(sql_getMEName, conn))
            {
                cmd.Parameters.Add(new OracleParameter("mecode", mecode)); // Use mecode parameter

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar(); // Execute the query to get a single value

                    // If the result is not null, assign it to meName
                    if (result != null)
                    {
                        meName = result.ToString(); // Get the ME name
                    }
                    // If result is null, meName will remain null
                }
                catch (Exception ex)
                {
                    // Handle exceptions (e.g., log error)
                    // For production code, consider using a logging framework
                    Console.WriteLine("Error retrieving ME name: " + ex.Message); // Example of logging to console
                }
            }
        }

        // Return null if no value was found; you can also return an empty string if desired
        return meName; // Can return null or an empty string if no match found
    }



    private string GetIPAddress()
    {
        string ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
        if (string.IsNullOrEmpty(ipAddress))
        {
            ipAddress = Request.ServerVariables["REMOTE_ADDR"];
        }
        return ipAddress;
    }

    public static string NumberToWords(int number)
    {
        if (number == 0)
            return "zero";

        if (number < 0)
            return "minus " + NumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 1000000) > 0)
        {
            words += NumberToWords(number / 1000000) + " million ";
            number %= 1000000;
        }

        if ((number / 1000) > 0)
        {
            words += NumberToWords(number / 1000) + " thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += NumberToWords(number / 100) + " hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            var unitsMap = new[]
            {
            "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
            "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen"
        };
            var tensMap = new[]
            {
            "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety"
        };

            if (number < 20)
                words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += "-" + unitsMap[number % 10];
            }
        }

        return words;
    }


    public static string ConvertCurrencyToWords(double amount)
    {
        // Convert the integer part (Rupees)
        int rupees = (int)amount;

        // Convert the decimal part (cents)
        int cents = (int)Math.Round((amount - rupees) * 100);

        // Convert rupees to words
        string rupeesInWords = NumberToWords(rupees);

        // Construct the final result with Rupees and Cents
        string result = "Sri Lankan Rupees " + rupeesInWords;

        if (cents > 0)
        {
            result += " and Cts. " + NumberToWords(cents);
        }

        result += " Only.";

        return result;
    }

    protected async void btPDF_Click(object sender, EventArgs e)
    {
        // Generate the PDF asynchronously
        byte[] pdfBytes = await GenerateDebitNote();

        //await GetDebitDetails_API.GetDebitDetailsdata(debitNotePostDataModel);
      

        if (pdfBytes != null)
        {
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=debitnote.pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(pdfBytes);
            Response.Flush(); // Send the response to the client
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }

    private async Task<byte[]> GenerateDebitNote()
    {
      
        try
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4, 40, 40, 40, 40);
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                document.Open();

                // Define fonts
                Font fontBold = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10);
                Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                Font normalFontLarge = FontFactory.GetFont(FontFactory.HELVETICA, 11);
                Font fontBoldUnderline = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12f, Font.UNDERLINE);
                Font smallFont = FontFactory.GetFont(FontFactory.HELVETICA, 8);


                // Title and Branch Info
                PdfPTable titleTable = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 20f
                };
                titleTable.SetWidths(new float[] { 1f, 1f }); // Equal column widths

                // Title Cell
                PdfPCell titleCell = new PdfPCell(new Phrase(litBranch.Text))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER, // Center the title text
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 2 // Span across both columns
                };
                titleTable.AddCell(titleCell);

                // VAT and SVAT info
                PdfPCell vatCell = new PdfPCell(new Phrase("VAT Reg.No.: 139052080 7000         SVAT Reg.No.: 13068", smallFont))
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 2, // Span across both columns
                    MinimumHeight = 20f
                };
                vatCell.PaddingTop = 20f;

                titleTable.AddCell(vatCell);

                document.Add(titleTable);


                // Fire, Debit Note No., and Other Info
                PdfPTable infoTable = new PdfPTable(3)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 20f
                };
                infoTable.SetWidths(new float[] { 25f, 35f, 40f });

                // Add header row with merged columns
                PdfPCell cell1 = new PdfPCell(new Phrase("Fire:", fontBold))
                {
                    Border = Rectangle.NO_BORDER,
                    Colspan = 1
                };

                PdfPCell cell2 = new PdfPCell(new Phrase("DEBIT NOTE (N.B)", fontBold))
                {
                    Border = Rectangle.NO_BORDER,
                    Colspan = 1,
                    //HorizontalAlignment = Element.ALIGN_CENTER
                };

                PdfPCell cell3 = new PdfPCell(new Phrase("THIS IS NOT A TAX INVOICE", normalFont))
                {
                    Border = Rectangle.BOX, // Adds box outline
                    BorderWidth = 1f, // Sets border width
                    BorderColor = Color.BLACK, // Sets border color
                    Colspan = 1,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5f // Adds padding inside the cell
                };

                // Add cells to the table
                infoTable.AddCell(cell1);
                infoTable.AddCell(cell2);
                infoTable.AddCell(cell3);             

                // Create and configure cells with no borders
                PdfPCell cell;

                // Add the first cell
                cell = new PdfPCell(new Phrase("Debit Note No.", fontBold));
                cell.Border = Rectangle.NO_BORDER; // No border
                infoTable.AddCell(cell);

                // Add the second cell
                cell = new PdfPCell(new Phrase(":   " + txtdebitNo.Text, fontBold));
                cell.Border = Rectangle.NO_BORDER; // No border
                infoTable.AddCell(cell);

                // Add the third cell
                cell = new PdfPCell(new Phrase("Date: " + txtSystemDate.Text, normalFont));
                cell.Border = Rectangle.NO_BORDER; // No border
                infoTable.AddCell(cell);

                AddTableRow(infoTable, "Proposal/Policy No.", ":   "+txtpolicy.Text);
                AddTableRow(infoTable, "Sum Insured", ":   Rs. " + txtsum.Text);
                AddTableRow(infoTable, "Period of Insurance", ":   "+txtPeriodInsu.Text);
                AddTableRow(infoTable, "Name of Insured", ":   "+txtnameinsu.Text);
                AddTableRow(infoTable, "Address", ":   "+txtadd.Text);

                document.Add(infoTable);

                // Create a dashed line using PdfContentByte
                PdfContentByte canvas = writer.DirectContent;
                canvas.SetLineWidth(1f);
                canvas.SetColorStroke(Color.BLACK);
                canvas.SetLineDash(3f, 3f); // Dash pattern: 3pt on, 3pt off

                // Draw dashed line
                float lineY = document.PageSize.Height - 235f; // Adjust Y position as needed
                canvas.MoveTo(40f, lineY); // Starting point of the line
                canvas.LineTo(550f, lineY); // Ending point of the line
                canvas.Stroke();

                // Add vertical space before the image
                Paragraph spacerBefore = new Paragraph(" ");
                spacerBefore.SpacingBefore = 30f; // Space before
                document.Add(spacerBefore);

                // Create the table with 3 columns
                PdfPTable feesTable = new PdfPTable(3)
                {
                    WidthPercentage = 100
                };
                feesTable.SetWidths(new float[] { 25f, 35f, 40f });

                // Load the image
                string imagePath = HttpContext.Current.Server.MapPath("~/Images/Debit.png");
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(imagePath);
                image.ScaleAbsolute(190f, 75f); // Adjust size as needed

                // Create a PdfPCell for the image
                PdfPCell imageCell = new PdfPCell(image)
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Colspan = 3 // Span across all 3 columns
                };

                // Create a PdfPCell for the labels
                PdfPCell labelCell = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                    Phrase = new Phrase("\n\nNet Premium:\n\nPolicy Fee:\n\nSRCC:\n\nTC\n\nAdministrative Fee:\n\nTaxes:", normalFont)
                };

                // Add the label cell to the table
                feesTable.AddCell(labelCell);

                // Create a PdfPCell for the values
                PdfPCell valueCell = new PdfPCell
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_LEFT, // Ensure text is left-aligned
                    Phrase = new Phrase(
                    "              Rs.Cts." + "\n" + "\n" +
                    ":              " + txtNetPremium.Text + "\n"+"\n" +
                    ":              " + txtPolicyFee.Text +"\n"+"\n" +
                    ":              " + txtSrcc.Text +"\n"+"\n" +
                    ":              " + txttc.Text +"\n"+"\n" +
                    ":              " + txtAdminFee.Text +"\n"+"\n"+
                    ":              " + txtTaxes.Text,
                    normalFont
                    )
                };


                // Add the value cell to the table
                feesTable.AddCell(valueCell);

                // Add the image cell to the table
                feesTable.AddCell(imageCell);

                // Add the table to the document
                document.Add(feesTable);

                // Create another dashed line after the fees table
                canvas.SetLineDash(3f, 3f); // Dash pattern: 3pt on, 3pt off
                lineY = document.PageSize.Height - 430; // Adjust Y position as needed
                canvas.MoveTo(40f, lineY); // Starting point of the line
                canvas.LineTo(550f, lineY); // Ending point of the line
                canvas.Stroke();

                // Add vertical space before the final table
                spacerBefore.SpacingBefore = 20f; // Space before
                document.Add(spacerBefore);



                // Final Rows
                PdfPTable finalTable = new PdfPTable(2)
                {
                    WidthPercentage = 100,
                    SpacingBefore = 5f
                };
                // Set the column widths
                finalTable.SetWidths(new float[] { 33f, 67f }); // Adjust the column widths as needed

                // Row 1: Total Debit Amount
                finalTable.AddCell(new PdfPCell(new Phrase("Total Debit Amount:", fontBold)) { Border = Rectangle.NO_BORDER });
                finalTable.AddCell(new PdfPCell(new Phrase(txtTotalDebitAmount.Text, fontBold)) { Border = Rectangle.NO_BORDER });

                // Row 2: Debit Amount (Spanning across both columns)
                finalTable.AddCell(new PdfPCell(new Phrase(txtDebitAmount.Text, normalFont))
                {
                    Border = Rectangle.NO_BORDER,
                    Colspan = 2 // Span across both columns
                });

                // Row 3: Paid On
                finalTable.AddCell(new PdfPCell(new Phrase("Paid On:", normalFont)) { Border = Rectangle.NO_BORDER });
                finalTable.AddCell(new PdfPCell(new Phrase(txtPaidOn.Text, normalFont)) { Border = Rectangle.NO_BORDER });

                // Row 4: SLI Code
                finalTable.AddCell(new PdfPCell(new Phrase("SLI Code:", normalFont)) { Border = Rectangle.NO_BORDER });
                finalTable.AddCell(new PdfPCell(new Phrase(Txtslicode.Text, normalFont)) { Border = Rectangle.NO_BORDER });

                // Row 5: File Service Branch
                finalTable.AddCell(new PdfPCell(new Phrase("File Service Branch:", normalFont)) { Border = Rectangle.NO_BORDER });
                finalTable.AddCell(new PdfPCell(new Phrase(txtfilebranch.Text, normalFont)) { Border = Rectangle.NO_BORDER });

                // Row 6: Client ID
                finalTable.AddCell(new PdfPCell(new Phrase("Client ID:", normalFont)) { Border = Rectangle.NO_BORDER });
                finalTable.AddCell(new PdfPCell(new Phrase(txtClientID.Text, normalFont)) { Border = Rectangle.NO_BORDER });

                // Row 7: Note (Merged across two columns)
                finalTable.AddCell(new PdfPCell(new Phrase("Subject to there being no claim from the date of Renewal to the date of payment", smallFont))
                {
                    Border = Rectangle.NO_BORDER,
                    Colspan = 2, // Merge across both columns
                    HorizontalAlignment = Element.ALIGN_JUSTIFIED // Optional: Align the text
                });

                // Row 8: Name of Debtor
                finalTable.AddCell(new PdfPCell(new Phrase("Name of Debtor:", normalFont)) { Border = Rectangle.NO_BORDER });
                finalTable.AddCell(new PdfPCell(new Phrase(txtNameDebtor.Text, normalFont)) { Border = Rectangle.NO_BORDER });

                // Row 9: Agency Code
                finalTable.AddCell(new PdfPCell(new Phrase("Agency Code:", normalFont)) { Border = Rectangle.NO_BORDER });
                finalTable.AddCell(new PdfPCell(new Phrase(TxtAgencyCode.Text, normalFont)) { Border = Rectangle.NO_BORDER });

                // Add the final table to the document
                document.Add(finalTable);


                // Additional Text
                Paragraph noteParagraph = new Paragraph("If you disagree with the contents of this debit note please inform us within 14 days of the debit note. Otherwise, we will consider this as correct. Please settle the amount mentioned in the debit note to the bank account details provided.", smallFont)
                {
                    SpacingBefore = 20f,
                    Alignment = Element.ALIGN_JUSTIFIED
                };
                document.Add(noteParagraph);

                // Assuming smallFont contains the correct font size, but you want to make it bold:
                Font smallBoldFont = FontFactory.GetFont(FontFactory.HELVETICA, smallFont.Size, Font.BOLD);

                Paragraph warrantyParagraph = new Paragraph("The cover provided is subject to the conditions stipulated in the premium payment Warranty attached.", smallBoldFont)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 20f
                };

                document.Add(warrantyParagraph);

                Paragraph warrantyParagraph2 = new Paragraph("Manager", smallBoldFont)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 20f,
                    Alignment = Element.ALIGN_CENTER // Center the paragraph
                };

                document.Add(warrantyParagraph2);

                // Create a PdfPTable with 1 column
                PdfPTable finalTable7 = new PdfPTable(1); // One column table

                // Create a PdfPCell with the data displayed in one line without labels
                PdfPCell combinedCell = new PdfPCell(new Phrase(
                    lblDate.Text + "  " +
                    lblTime.Text + "  " +
                    lblID.Text + "  " +
                    lblIP.Text,
                    smallFont // You can switch to smallBoldFont if you want bold text
                ))
                {
                    Border = Rectangle.NO_BORDER, // No border for the cell
                    HorizontalAlignment = Element.ALIGN_LEFT // Ensure text is left-aligned
                };

                // Add the cell to the table
                finalTable7.AddCell(combinedCell);

                // Set table alignment to the left of the page
                finalTable7.HorizontalAlignment = Element.ALIGN_LEFT;

                // Optionally set the table width percentage to use full page width
                finalTable7.WidthPercentage = 100; // Adjust percentage as needed to fit your layout

                // Add the table to the document
                document.Add(finalTable7);


                // Add premium payment warranty section
                document.NewPage();

                // Load the logo image
                string imagePath2 = HttpContext.Current.Server.MapPath("~/Images/BancassuranceLogo.png");
                iTextSharp.text.Image image1 = iTextSharp.text.Image.GetInstance(imagePath2);
                image1.ScaleAbsolute(500f, 125f); // Adjust size as needed

                // Create a PdfPCell for the image
                PdfPCell imageCell2 = new PdfPCell(image1)
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE
                };

                // Add image cell to a new table if needed
                PdfPTable logoTable = new PdfPTable(1) { WidthPercentage = 100 };
                logoTable.AddCell(imageCell2);
                document.Add(logoTable);

                

                // Create a Phrase with underlined font
                Phrase warrantyPhrase = new Phrase("PREMIUM PAYMENT WARRANTY FOR POLICIES OF GENERAL INSURANCE", fontBoldUnderline);

                // Create the paragraph
                Paragraph warrantyTitle = new Paragraph(warrantyPhrase)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 20f,
                    SpacingAfter = 10f
                };

                document.Add(warrantyTitle);

                // Add table rows for the warranty section
                // Initialize the PdfPTable
                PdfPTable finalTable1 = new PdfPTable(2); // Adjust the number of columns as necessary
                finalTable1.WidthPercentage = 100; // Set the width of the table

                // Add table rows for the warranty section
                 AddTableRow(finalTable1, txtDebitNoteNo.Text);

                // Add the table to the document
                document.Add(finalTable1);

                document.Add(new Paragraph("1.Notwithstanding anything herein contained but subject to clause 2 and 3 hereof, it is hereby agreed and declared that the full premium due and payable in respect of this insurance is required to be settled to the Insurer (Sri Lanka Insurance Corporation General Ltd.) on or before the premium due date specified in the Schedule of this Policy, Renewal Certificate, Endorsement, or Cover Note (which shall be a date not exceeding 60 days from the date of inception of the policy) and in the absence of any such premium due date, the full settlement of the premium is required to be made or effected on or before the expiry of the 60th day from the date of inception of this policy, Renewal Certificate, Endorsement, or Cover Note (hereinafter referred to as the “due date”).", normalFont)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                    Alignment = Element.ALIGN_JUSTIFIED
                });

                document.Add(new Paragraph("For the purpose of the warranty the “due date” shall be recognized from the date of inception or commencement of the coverage.", normalFont)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                    Alignment = Element.ALIGN_JUSTIFIED
                });

                document.Add(new Paragraph("2. It is also declared and agreed that the settlement of the full premium on or before the due date shall operate as a condition precedent to the insurer’s (Sri Lanka Insurance Corporation General Ltd.) liability or an obligation to settle a claim under this Policy, Renewal Certificate, Endorsement, or Cover Note. ", normalFont)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                    Alignment = Element.ALIGN_JUSTIFIED
                });

                document.Add(new Paragraph("In the event any claim arises between the date of commencement of this insurance and the “due date” for the settlement of premium, the insurer (Sri Lanka Insurance Corporation General Ltd.) may defer any decision on liability or postpone the settlement of any such claim until full settlement of the premium is effected on or before the “due date”. ", normalFont)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                    Alignment = Element.ALIGN_JUSTIFIED
                });

                document.Add(new Paragraph("3. It is also declared and agreed that where the full premium payable hereunder remains outstanding as at the closure of business of the insurer on the “due date” then the cover under this insurance and any obligations assumed or imputed under this insurance shall stand to be cancelled, ceased, and revoked immediately. ", normalFont)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                    Alignment = Element.ALIGN_JUSTIFIED
                });

                document.Add(new Paragraph("However, such cancellation will not prejudice the right of the insurer (Sri Lanka Insurance Corporation General Ltd.) to invoke any legal defenses or to recover the full or any part of the defaulted premium attributable to the expired period of the insurance.  ", normalFont)
                {
                    SpacingBefore = 10f,
                    SpacingAfter = 10f,
                    Alignment = Element.ALIGN_JUSTIFIED
                });

                // Define a PdfPTable for the company name
                PdfPTable companyTable = new PdfPTable(1);
                companyTable.TotalWidth = document.PageSize.Width;
                companyTable.DefaultCell.Border = PdfPCell.NO_BORDER;

                PdfPCell cell59 = new PdfPCell(new Phrase("Company Registration No - PB 5208", smallFont));
                cell59.Border = PdfPCell.NO_BORDER;
                cell59.HorizontalAlignment = Element.ALIGN_CENTER;
                cell59.Phrase.Font.Size = 8;
                companyTable.AddCell(cell59);

                PdfPCell cell60 = new PdfPCell(new Phrase("Sri Lanka Insurance Corporation General Limited", smallBoldFont));
                cell60.Border = PdfPCell.NO_BORDER;
                cell60.HorizontalAlignment = Element.ALIGN_CENTER;
                cell60.Phrase.Font.Size = 8;
                companyTable.AddCell(cell60);

                PdfPCell cell61 = new PdfPCell(new Phrase("21, Vauxhall Street, Colombo 02", smallBoldFont));
                cell61.Border = PdfPCell.NO_BORDER;
                cell61.HorizontalAlignment = Element.ALIGN_CENTER;
                cell61.Phrase.Font.Size = 8;
                companyTable.AddCell(cell61);

                PdfPCell cell62 = new PdfPCell(new Phrase("Tel: 011-2357457  Fax: 011-2357236  Email: email@srilankainsurance.com  Web: www.srilankainsurance.com", normalFont));
                cell62.Border = PdfPCell.NO_BORDER;
                cell62.HorizontalAlignment = Element.ALIGN_CENTER;
                cell62.Phrase.Font.Size = 8;
                companyTable.AddCell(cell62);

                // Calculate the position to add the company name (at the bottom of the page)
                float bottomMargin = document.BottomMargin;
                float footerHeight = companyTable.TotalHeight;
                float yPosition = bottomMargin + footerHeight;

                // Add the company name table to the page
                companyTable.WriteSelectedRows(0, -1, 0, yPosition, writer.DirectContent);


                document.Close();
                writer.Close();

                return memoryStream.ToArray();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions
            Console.WriteLine("An error occurred: " + ex.Message);
            return null;
        }
    }

    private void AddTableRow(PdfPTable table, string column1Text, string column2Text = "", Font column2Font = null, string column3Text = "", Font column3Font = null, int colspan = 1)
    {
        PdfPCell cell1 = new PdfPCell(new Phrase(column1Text, FontFactory.GetFont(FontFactory.HELVETICA, 10)))
        {
            Border = Rectangle.NO_BORDER
        };

        PdfPCell cell2 = new PdfPCell(new Phrase(column2Text, column2Font ?? FontFactory.GetFont(FontFactory.HELVETICA, 10)))
        {
            Border = Rectangle.NO_BORDER
        };

        PdfPCell cell3 = new PdfPCell(new Phrase(column3Text, column3Font ?? FontFactory.GetFont(FontFactory.TIMES_ROMAN, 10)))
        {
            Border = Rectangle.NO_BORDER
        };

        if (colspan > 1)
        {
            cell1.Colspan = colspan;
            cell2.Colspan = colspan;
            cell3.Colspan = colspan;
        }

        table.AddCell(cell1);
        table.AddCell(cell2);
        table.AddCell(cell3);
    }

}
