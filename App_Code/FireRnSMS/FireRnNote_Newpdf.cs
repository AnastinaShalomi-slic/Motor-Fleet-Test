using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Data;
using System.Globalization;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for FireRnNote_pdf
/// </summary>
public class FireRnNote_Newpdf
{
    Oracle_Transaction Oracle_Trans = new Oracle_Transaction();
    Execute_sql _Sql = new Execute_sql();
    public double RNNET, RNRCC, RNTC, RNPOLFEE, RNVAT, RNNBT, RNTOT, RN_ADMINFEE, RN_ADMINFEEVal, sumisured, excessPre, excessAmo, excessPre2, excessAmo2 = 0;
    public string RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNNAM, RNADD1, RNADD2, RNADD3, RNADD4, RNREF, Remark = "";
    public FireRnNote_Newpdf()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private double ConvertToDouble(object value)
    {
        if (value == null || value == DBNull.Value || string.IsNullOrWhiteSpace(value.ToString()))
            return 0.0;

        double result;
        if (double.TryParse(value.ToString(), out result))
            return result;

        return 0.0;
    }

    public void print_policy(string policyNo)
    {
        DataTable details = new DataTable();
        details = Oracle_Trans.GetRows(_Sql.GetMastData(policyNo), details);

        DataTable detailsExtraExcess = new DataTable();
        detailsExtraExcess = Oracle_Trans.GetRows(_Sql.GetFireRenewalExtraAccess(policyNo), detailsExtraExcess);

        if (Oracle_Trans.Trans_Sucess_State == true)
        {

            if (details.Rows.Count > 0)
            {
                RNDEPT = details.Rows[0]["RNDEPT"].ToString();
                RNPTP = details.Rows[0]["RNPTP"].ToString();
                RNYR = details.Rows[0]["RNYR"].ToString();
                //RNSTDT = details.Rows[0]["RNSTDT"].ToString();
                string rawDate = details.Rows[0]["RNSTDT"].ToString();

                string[] formats = { "d/M/yyyy hh:mm:ss tt", "d/M/yyyy HH:mm:ss", "dd/MM/yyyy hh:mm:ss tt", "dd/MM/yyyy HH:mm:ss" };
                DateTime parsedDate;

                if (DateTime.TryParseExact(rawDate, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
                {
                    string onlyDate = parsedDate.ToString("dd-MMM-yyyy"); // e.g., "06-May-2024"
                    RNSTDT = onlyDate;
                }
                else
                {
                    // Handle invalid date format
                    RNSTDT = "Invalid date format";
                }

                //DateTime rnStarttDate = DateTime.ParseExact(details.Rows[0]["RNSTDT"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                //RNSTDT = rnStarttDate.ToString("dd-MMM-yyyy");

                RNENDT = details.Rows[0]["RNENDT"].ToString();
                DateTime parsedDate2;

                if (DateTime.TryParseExact(RNENDT, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate2))
                {
                    string onlyDate = parsedDate2.ToString("dd-MMM-yyyy"); // e.g., "06-May-2024"
                    RNENDT = onlyDate;
                }
                else
                {
                    // Handle invalid date format
                    RNENDT = "Invalid date format";
                }
                

                // Inside your method
                RNNAM = details.Rows[0]["RNNAM"] != DBNull.Value ? details.Rows[0]["RNNAM"].ToString() : string.Empty;
                RNADD1 = details.Rows[0]["RNADD1"] != DBNull.Value ? details.Rows[0]["RNADD1"].ToString() : string.Empty;
                RNADD2 = details.Rows[0]["RNADD2"] != DBNull.Value ? details.Rows[0]["RNADD2"].ToString() : string.Empty;

                RNNET = ConvertToDouble(details.Rows[0]["RNNET"]);
                RNRCC = ConvertToDouble(details.Rows[0]["RNRCC"]);
                RNTC = ConvertToDouble(details.Rows[0]["RNTC"]);
                RNPOLFEE = ConvertToDouble(details.Rows[0]["RNPOLFEE"]);
                RNVAT = ConvertToDouble(details.Rows[0]["RNVAT"]);
                RNNBT = ConvertToDouble(details.Rows[0]["RNNBT"]);
                RNTOT = ConvertToDouble(details.Rows[0]["RNTOT"]);
                RN_ADMINFEE = ConvertToDouble(details.Rows[0]["RN_ADMINFEE"]);
                RN_ADMINFEEVal = RN_ADMINFEE + RNNBT;
                sumisured = ConvertToDouble(details.Rows[0]["RNSUMINSUR"]);

                RNREF = details.Rows[0]["RNREF"] != DBNull.Value ? details.Rows[0]["RNREF"].ToString() : string.Empty;



                Remark = details.Rows[0]["REMARK"] != DBNull.Value ? details.Rows[0]["REMARK"].ToString() : string.Empty;

                object preObj = details.Rows[0]["EXCESS_PRE"];
                object amoObj = details.Rows[0]["EXCESS_AMO"];
                object preObj2 = details.Rows[0]["EXCESS_PRE2"];
                object amoObj2 = details.Rows[0]["EXCESS_AMO2"];

                excessPre = (preObj != DBNull.Value && !string.IsNullOrWhiteSpace(preObj.ToString())) ? Convert.ToDouble(preObj) : 0;

                excessAmo = (amoObj != DBNull.Value && !string.IsNullOrWhiteSpace(amoObj.ToString())) ? Convert.ToDouble(amoObj) : 0;

                excessPre2 = (preObj2 != DBNull.Value && !string.IsNullOrWhiteSpace(preObj2.ToString())) ? Convert.ToDouble(preObj2) : 0;

                excessAmo2 = (amoObj2 != DBNull.Value && !string.IsNullOrWhiteSpace(amoObj2.ToString())) ? Convert.ToDouble(amoObj2) : 0;
            }
        }

        Document document = new Document(PageSize.A4, 50, 50, 30, 5);
        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        Phrase phrase;

        phrase = new Phrase(DateTime.Now.ToString() + " " + "\nSri Lanka Insurance Corporation General Ltd., No.21, Vauxhall Street, Colombo 02. Sri Lanka,   Tel. 2357110/7120", new Font(Font.COURIER, 8));

        HeaderFooter header = new HeaderFooter(phrase, false);
        // top & bottom borders on by default 
        header.Border = Rectangle.NO_BORDER;
        // center header 
        header.Alignment = 1;
        /* 
         * HeaderFooter => add header __before__ opening document 
         */
        document.Footer = header;

        Font titleFont1 = FontFactory.GetFont("Times New Roman", 9, Font.BOLD, new Color(0, 0, 0));
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(255, 255, 255));
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont_bold = FontFactory.GetFont("Times New Roman", 8, Font.BOLD);

        Font bodyFont_sml = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont_bold_sml = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

        Font bodyFont_bold_sm = FontFactory.GetFont("Times New Roman", 8, Font.BOLD);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_white_bold = FontFactory.GetFont("Times New Roman", 8, Font.BOLD, new Color(255, 255, 255));
        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font underlineAndBoldFont = FontFactory.GetFont("Times New Roman", 9, Font.UNDERLINE | Font.BOLD);

        int rowCount = 0;

        string root = System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_gen_Logo.png");

        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(root);

        logo.ScalePercent(25f);
        logo.SetAbsolutePosition((PageSize.A4.Width - logo.ScaledWidth) / 2, 740);


        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/Gi_Watermark.gif"));
        watermark.SetAbsolutePosition((PageSize.A4.Width - watermark.ScaledWidth) / 2, (PageSize.A4.Height - watermark.ScaledHeight) / 2);
        //document.Add(watermark); 

        MyPageEventHandler e = new MyPageEventHandler()
        {
            ImageHeader = watermark
        };
        writer.PageEvent = e;
        document.Open();
        document.Add(logo);


        document.Add(new Paragraph("\n\n\n\n\n\n\n", bodyFont));
        Chunk titch1 = new Chunk("Fire Insurance Renewal Notice\n\n", boldTableFont);
        //titch1.SetUnderline(0.5f, -1.5f); 
        Paragraph titleLine = new Paragraph(titch1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);


        // Fonts
        Font regularFont = FontFactory.GetFont("Times New Roman", 8, Color.BLACK);
        Font boldFont = FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK);

        // ========== Outer Table: 1 Row, 2 Columns ==========
        PdfPTable outerTable = new PdfPTable(2);
        outerTable.WidthPercentage = 100;
        outerTable.SetWidths(new float[] { 1.2f, 2f }); // Left column narrow, right column wider

        // ===== Left Cell: Name + Address + Greeting =====
        PdfPTable leftInner = new PdfPTable(1);
        leftInner.DefaultCell.Border = Rectangle.NO_BORDER;

        leftInner.AddCell(new Phrase(RNNAM, regularFont));
        leftInner.AddCell(new Phrase(RNADD1, regularFont));
        leftInner.AddCell(new Phrase(RNADD2, regularFont));
        //leftInner.AddCell(new Phrase("…………………………", regularFont));
        leftInner.AddCell(new Phrase("\nDear Sir / Madam,", regularFont));

        PdfPCell leftCell = new PdfPCell(leftInner);
        leftCell.Border = Rectangle.NO_BORDER;
        outerTable.AddCell(leftCell);

        // ===== Right Cell: Premium Details Table =====
        PdfPTable rightInner = new PdfPTable(3);
        rightInner.TotalWidth = 250f; // fixed width
        rightInner.LockedWidth = true;
        //rightInner.WidthPercentage = 20;
        rightInner.SetWidths(new float[] { 1.5f, 0.5f, 1.5f });

        // Premium table rows (left: label, right: amount or "Rs.")
        string[,] premiumRows = new string[,]
        {
            { "Policy No","", policyNo },
            { "Renewal Date","", RNENDT },
            { "Renewal Reference No","", RNREF },
            { "Sum Insured", "Rs." , sumisured.ToString("N2") },
            { "Net Premium", "Rs." , RNNET.ToString("N2")},
            { "SRCC Premium", "Rs." , RNRCC.ToString("N2") },
            { "TR Premium", "Rs." , RNTC.ToString("N2")},
            { "Admin Fee", "Rs." , RN_ADMINFEEVal.ToString("N2")},
            { "Renewal Fee", "Rs." , RNPOLFEE.ToString("N2") },
            { "VAT", "Rs." , RNVAT.ToString("N2")},
            { "Total Renewal Premium", "Rs." , RNTOT.ToString("N2")}
        };

        for (int i = 0; i < premiumRows.GetLength(0); i++)
        {
            PdfPCell labelCell = new PdfPCell(new Phrase(premiumRows[i, 0], regularFont));
            labelCell.HorizontalAlignment = Element.ALIGN_LEFT;
            labelCell.Padding = 2f;
            rightInner.AddCell(labelCell);

            PdfPCell mddelCell = new PdfPCell(new Phrase(premiumRows[i, 1], regularFont));
            labelCell.HorizontalAlignment = Element.ALIGN_LEFT;
            labelCell.Padding = 2f;
            rightInner.AddCell(mddelCell);

            PdfPCell valueCell = new PdfPCell(new Phrase(premiumRows[i, 2], regularFont));
            valueCell.HorizontalAlignment = Element.ALIGN_LEFT;
            valueCell.Padding = 2f;
            valueCell.HorizontalAlignment = Element.ALIGN_RIGHT;
            rightInner.AddCell(valueCell);
        }

        PdfPCell rightCell = new PdfPCell(rightInner);
        rightCell.Border = Rectangle.NO_BORDER;
        outerTable.AddCell(rightCell);

        // Add the final two-column layout to the document
        document.Add(outerTable);

        

        document.Add(new Paragraph("\n", bodyFont));
        Chunk specEndoP12 = new Chunk("1. The policy expires on the below specified date.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph endoP12 = new Paragraph(specEndoP12);
        document.Add(endoP12);

        Chunk specEndoP13 = new Chunk("2. Premium payment must be made on or before the expiry date.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph endoP13 = new Paragraph(specEndoP13);
        document.Add(endoP13);

        Chunk specEndoP14 = new Chunk("3. Renewal Premium and Notice should be forwarded to SLICGL.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph endoP14 = new Paragraph(specEndoP14);
        document.Add(endoP14);

        Chunk specEndoP15 = new Chunk("4. If property changes occur, an amended renewal notice should be forwarded.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph endoP15 = new Paragraph(specEndoP15);
        document.Add(endoP15);

        Chunk specEndoP16 = new Chunk("5. SLICGL reserves the right to revise terms & conditions if property suffers loss or damage.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph endoP16 = new Paragraph(specEndoP16);
        document.Add(endoP16);

        Chunk specEndoP17 = new Chunk("6. Renewal is issued subject to the prevailing proposal form and other submitted documents.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph endoP17 = new Paragraph(specEndoP17);
        document.Add(endoP17);


       

        document.Add(new Paragraph("\n", bodyFont));
        Chunk specEndoP6 = new Chunk("Special Note - Emphasizes importance of insured property value.", FontFactory.GetFont("Times New Roman", 9, Font.BOLD, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph endoP6 = new Paragraph(specEndoP6);
        document.Add(endoP6);

        //document.Add(new Paragraph("\n", bodyFont));
        Chunk specEndoP7 = new Chunk("i. In order to prevent claim reductions due to under-insurance, the policyholder asked to review the sum covered, which should be the actual property of value reviewed every three years.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph endoP7 = new Paragraph(specEndoP7);
        document.Add(endoP7);

        //document.Add(new Paragraph("\n", bodyFont));
        Chunk specEndoP8 = new Chunk("ii. Revision of annual premium payable indicated herein on amendment of rates applicable in respect of SRCC, TC, & Taxes accordance with the amendments of Government rules and regulations.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph endoP8 = new Paragraph(specEndoP8);
        document.Add(endoP8);

        //ectra excess

        // Create a paragraph to hold all the excess info

        if (!string.IsNullOrEmpty(Remark))
        {
            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP20 = new Chunk("*. '" + Remark + "' ", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP20 = new Paragraph(specEndoP20);
            document.Add(endoP20);
        }



        if (excessAmo != 0 || excessPre != 0)
        {
            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP9 = new Chunk("Revise excess as follows.", FontFactory.GetFont("Times New Roman", 9, Font.BOLD, Color.RED));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP9 = new Paragraph(specEndoP9);
            document.Add(endoP9);

            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP22 = new Chunk("*. SRCC- 10% on each & every loss", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.RED));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP22 = new Paragraph(specEndoP22);
            document.Add(endoP22);

            document.Add(new Paragraph("\n", bodyFont));
            Chunk specEndoP19 = new Chunk("*. All  Natural Perils including Cyclone/Storm/Tempest/Flood/Earthquake (with fire & shock)– " + excessPre + " % with a minimum of Rs. " + excessAmo + " / -on each & every loss", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.RED));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP19 = new Paragraph(specEndoP19);
            document.Add(endoP19);
        }

        if (excessAmo2 != 0 || excessPre2 != 0)
        {
            Chunk specEndoP20 = new Chunk("*. All Other Perils – " + excessPre2 + " % with a minimum of Rs. " + excessAmo2 + " / -on each & every loss", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.RED));
            //titchTitle.SetUnderline(0.5f, -1.5f);
            Paragraph endoP20 = new Paragraph(specEndoP20);
            document.Add(endoP20);
        }

        // Loop through the DataTable and append each row
        if (detailsExtraExcess.Rows.Count > 0)
        {
            document.Add(new Paragraph("\n", bodyFont));

            // Now create a new paragraph for the details with regular font
            Paragraph excessInfoParagraph = new Paragraph();

            foreach (DataRow row in detailsExtraExcess.Rows)
            {
                string excessName = row["EXCESS_DESCRIPTION"].ToString();
                string percentage = row["excess_precentage"].ToString();
                string amount = Convert.ToDecimal(row["EXCESS_AMOUNT"]).ToString("N2");

                string line = string.Format("*. {0} - {1}% with a minimum of Rs. {2} on each & every loss\n", excessName, percentage, amount);
                excessInfoParagraph.Add(new Chunk(line, FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.RED)));
            }

            // Add the details paragraph to the document
            document.Add(excessInfoParagraph);
        }

        document.Add(new Paragraph("\n", bodyFont));
        Chunk specEndoP23 = new Chunk("Payment methods", FontFactory.GetFont("Times New Roman", 9, Font.BOLD, Color.BLACK));
        Paragraph endoP23 = new Paragraph(specEndoP23);
        document.Add(endoP23);


        Chunk specEndoP24 = new Chunk("1. Visit any branch of Sri Lanka Insurance Corporation General Ltd.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        Paragraph endoP24 = new Paragraph(specEndoP24);
        document.Add(endoP24);

        Chunk specEndoP25 = new Chunk("     *. Payment can be made by Cash", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        Paragraph endoP25 = new Paragraph(specEndoP25);
        document.Add(endoP25);

        Chunk chunk1 = new Chunk("     *. Payment by cheque should be made payable to ", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        Chunk chunk2 = new Chunk("      Sri Lanka Insurance Corporation General Limited. ", FontFactory.GetFont("Times New Roman", 8, Font.BOLD, Color.BLACK));
        Chunk chunk3 = new Chunk("      [Please include your policy number and contact details on the reverse of the cheque]", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));

        // Create Paragraph and add chunks
        Paragraph endoP26 = new Paragraph();
        endoP26.Add(chunk1);
        endoP26.Add(chunk2);
        endoP26.Add(chunk3);

        Chunk specEndoP27 = new Chunk("2. Website- www.srilankainsurance.com  /  SLIC Mobile app", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        Paragraph endoP27 = new Paragraph(specEndoP27);
        document.Add(endoP27);

        Chunk specEndoP28 = new Chunk("3. Direct Bank Transfer (Online or ATM) - Transfer funds to the following account:", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        Paragraph endoP28 = new Paragraph(specEndoP28);
        document.Add(endoP28);

        document.NewPage();
        //payment detail table
        float[] columnWidths = { 3, 3 };

        // Create a table with 3 columns
        PdfPTable table = new PdfPTable(2);
        table.SetWidths(columnWidths);
        table.WidthPercentage = 50; // 
        table.HorizontalAlignment = Element.ALIGN_CENTER; // Center the table horizontally
        table.SpacingBefore = 20;
        table.SpacingAfter = 20;
        table.DefaultCell.Border = Rectangle.BOX;

        // Add table rows
        table.AddCell(new Phrase("Bank Of Ceylon", bodyFont));
        table.AddCell(new Phrase("464 ", bodyFont));


        table.AddCell(new Phrase("People’s Bank", bodyFont));
        table.AddCell(new Phrase("014100160112340", bodyFont));
        
        table.AddCell(new Phrase("Sampath Bank", bodyFont));
        table.AddCell(new Phrase("002960001543", bodyFont));
        

        table.AddCell(new Phrase("Commercial Bank", bodyFont));
        table.AddCell(new Phrase("1480020022", bodyFont));
       

        table.AddCell(new Phrase("Hatton National Bank", bodyFont));
        table.AddCell(new Phrase("003010011166", bodyFont));
  
        document.Add(table);

        //end of payment table

        Chunk specEndoP21 = new Chunk("\n This is a computer generated document. No signature is required.", FontFactory.GetFont("Times New Roman", 8, Font.NORMAL, Color.BLACK));
        //titchTitle.SetUnderline(0.5f, -1.5f);
        Paragraph endoP21 = new Paragraph(specEndoP21);
        document.Add(endoP21);


        document.Close();
        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Fire_{0}.pdf", "Renewal_Notice"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
        output.Close();
    }
}