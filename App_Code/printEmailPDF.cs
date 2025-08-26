using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Image = iTextSharp.text.Image;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Configuration;

/// <summary>
/// Summary description for printEmailPDF
/// </summary>
public class printEmailPDF
{
    private string FromEmailid = ConfigurationManager.AppSettings["senderEmailId"];
    private string FromEmailPasscode = ConfigurationManager.AppSettings["senderEmailPasscode"];
    string toEmail, EmailSubj, EmailMsg, emailUrl;
    string dateOfIssue, vehicleType, make, sumInsured, periodOfCover, vehicleChassNo, yOm, fuelType = "", model="";
    string netPremium, rcc, tc, roadSafetyFund, stampDuty, policyFee, nbt, taxes, totalPre, adCovers, issuedBy, pdfDate, epfAgentCode, adminFee = "";
    string passangers = "", insuredName = "", note = "";
    string authPersonSentence = "";
    int authPerValue = 1, qAuth = 0;
    string qrefNo, UsrId = ""; bool IsVechleNo;

    string receiverEmailId = "";

   
    public printEmailPDF()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool getPrintEmailPDF(string isEmail,string qrefNo,string userID)
    {
        var quot = new printQuotation();
        //qAuth = Convert.ToInt32(quot.quotationApprovalIs(qrefNo));
        qAuth = 1;
        UsrId = userID;
        
        quot.pushprintQuotation(qrefNo, out dateOfIssue, out vehicleType, out make, out sumInsured, out periodOfCover, out vehicleChassNo, out yOm, out fuelType,
              out netPremium, out rcc, out tc, out adminFee, out roadSafetyFund, out stampDuty, out policyFee, out nbt, out taxes, out totalPre, out adCovers,
              out issuedBy, out pdfDate, out epfAgentCode, out passangers, out insuredName, out note, out receiverEmailId,out IsVechleNo,out model);

        bool rtn = false;

        #region master settings

        Double a4Width = PageSize.A4.Width;
        int orga4Width = Convert.ToInt32(a4Width);
        Document document = new Document(PageSize.A4, -50, -50, 2, 2);

        Font RsCtnFont = FontFactory.GetFont("Oxygen", 6, Font.ITALIC, new Color(107, 109, 113));
        Font tcFont = FontFactory.GetFont("Oxygen", 8, Font.NORMAL, new Color(242, 105, 50));

        Font motorCom = FontFactory.GetFont("Oxygen", 6, Font.NORMAL, new Color(107, 109, 113));
        Font NormalFont2 = FontFactory.GetFont("Oxygen", 7, Font.NORMAL, new Color(107, 109, 113));
        Font ConvenienceImgFont = FontFactory.GetFont("Oxygen", 7, Font.BOLD, new Color(0, 135, 185));
        Font AuthorFont = FontFactory.GetFont("Oxygen", 7, Font.NORMAL, new Color(43, 137, 174));
        Font NormalFont8 = FontFactory.GetFont("Oxygen", 8, Font.NORMAL, new Color(107, 109, 113));
        Font NormalFont8i = FontFactory.GetFont("Oxygen", 8, Font.ITALIC, new Color(107, 109, 113));
        Font NormalFont9 = FontFactory.GetFont("Oxygen", 9, Font.NORMAL, new Color(107, 109, 113));
        Font SubTileFont = FontFactory.GetFont("Oxygen", 10, Font.BOLD, new Color(242, 105, 50));
        Font HeaderContentFont = FontFactory.GetFont("Oxygen", 10, Font.NORMAL, new Color(107, 109, 113));
        Font TileFont = FontFactory.GetFont("Oxygen", 12, Font.NORMAL, new Color(107, 109, 113));
        Font BenTileFont = FontFactory.GetFont("Oxygen", 12, Font.BOLD, new Color(43, 137, 174));
        Font MainTileFont = FontFactory.GetFont("Oxygen", 13, Font.BOLD, new Color(43, 137, 174));
        Font preBreakTitleFont = FontFactory.GetFont("Oxygen", 10, Font.NORMAL, new Color(107, 109, 113));
        Font FooterImportantNoticeFont = FontFactory.GetFont("Oxygen", 10, Font.NORMAL, new Color(255, 255, 255));
        Font footerAddfont = FontFactory.GetFont("Oxygen", 8, Font.NORMAL, new Color(255, 255, 255));


        var headerLeftLogo = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/motorPlus.png"));
        var headerRightLogo = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/slicLogo.png"));
        var patnersLogo = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/patnersLogo.png"));
        var patnersQR = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/quotQR.png"));
        var bullet = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/carIcon.png"));
        var aaa = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/aaa.png"));
        var bankCard = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/banksLogoBG.png"));
        var ImprtNotice = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/ImpNote.png"));

        #endregion

        #region benefits list

        Chunk bullet1 = new Chunk(bullet, 0, 0, true);
        bullet.ScaleToFit(10, 10);


        List list1 = new List(List.UNORDERED, 10f);
        //list1.SetListSymbol(new Chunk(Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"../images/carIcon.png"))));
        list1.SetListSymbol("\u2022");

        list1.IndentationLeft = 5f;
        list1.Add(new iTextSharp.text.ListItem("Settlement of Insurance claims below Rs. 75,000 within 3 working hours.\n\n", NormalFont2));
        list1.Add(new iTextSharp.text.ListItem("Fast track settlement method available for Claim below Rs.500,000/- with less documentation when repaired at SLIC partner garages.\n\n", NormalFont2));
        list1.Add(new iTextSharp.text.ListItem("Get a per day rental allowance* for Private cars, Jeeps and Double cabs when your vehicle is repaired at a SLIC partner garage, based on labour hours in excess of 4 days up to maximum of 10 days.\n\n", NormalFont2));
        list1.Add(new iTextSharp.text.ListItem("Protection of 'No Claim Bonus' for loyal customers who earned 60% or more No Claim Bonus without additional premium for one claim for the respective policy year.", NormalFont2));

        List list2 = new List(List.UNORDERED, 10f);
        list2.SetListSymbol("\u2022");

        list2.IndentationLeft = 5f;
        list2.Add(new iTextSharp.text.ListItem("100% free air bag cover for private cars and private dual purpose vehicles  (Based on the year of manufacture).*\n\n", NormalFont2));
        list2.Add(new iTextSharp.text.ListItem("Free personal accidents cover worth of Rs. 1,000,000 for the insured and\nRs. 50, 000 for 3 passengers in the event of the death due to an accident.", NormalFont2));

        List list3 = new List(List.UNORDERED, 10f);
        list3.SetListSymbol("\u2022");

        list3.IndentationLeft = 5f;
        list3.Add(new iTextSharp.text.ListItem("Accurate on-site assessment of accident damages by our team of over 175 technical professionals.\n\n", NormalFont2));
        list3.Add(new iTextSharp.text.ListItem("Precise settlement of insurance claim.", NormalFont2));

        List list4 = new List(List.UNORDERED, 10f);
        list4.SetListSymbol("\u2022");

        list4.IndentationLeft = 5f;
        list4.Add(new iTextSharp.text.ListItem("Renew your motor insurance Online via Sri Lanka Insurance customer portal and Sri Lanka Insurance mobile app.\n\n", NormalFont2));
        list4.Add(new iTextSharp.text.ListItem("Send repairer estimate through Whatsapp and Viber on 0772 357 357.", NormalFont2));

        List list5 = new List(List.UNORDERED, 10f);
        list5.SetListSymbol("\u2022");

        list5.IndentationLeft = 5f;
        list5.Add(new iTextSharp.text.ListItem("Cashless settlement facility at authorized vehicle agencies* (manufacturing year within 5 years) and SLIC partner garages without owner's account contribution.", NormalFont2));

        List list6 = new List(List.UNORDERED, 10f);
        list6.SetListSymbol("\u2022");

        list6.IndentationLeft = 5f;
        list6.Add(new iTextSharp.text.ListItem("Flexible payment plans and vast variety of payment options. (HSBC, AMEX, and COM BANK credit card)", NormalFont2));
        #endregion

        #region important notice list
        List impNoteList = new List(List.UNORDERED, 10f);
        impNoteList.SetListSymbol("\u2022");

        impNoteList.IndentationLeft = 5f;
        impNoteList.Add(new iTextSharp.text.ListItem("In additional to above premium, Luxury / Semi Luxury taxes should be paid if applicable.", NormalFont2));
        impNoteList.Add(new iTextSharp.text.ListItem("We strongly recommend that Sum insured of the vehicle reflects its market value.", NormalFont2));
        impNoteList.Add(new iTextSharp.text.ListItem("This quotation is invalid if the vehicle is currently a SLIC renewal or have previous claims with SLIC.", NormalFont2));
        impNoteList.Add(new iTextSharp.text.ListItem("This quotation is valid for 15 days from the date of this quotation.", NormalFont2));
        #endregion

        if (vehicleType != "PRIVATE CAR" || vehicleType != "DOUBLE CAB" || vehicleType != "CREW CAB"
                 || vehicleType != "VAN")
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                Paragraph paragraph = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = null;
                
                int[] clmnTablWidth = { orga4Width };
                int[] mainTablWidth = { orga4Width / 2, orga4Width / 2 };
                int[] preBreakTablWidth = { orga4Width / 2, orga4Width / 2, orga4Width / 4 };
                int[] preBreakClmnTablWidth = { (orga4Width / 2), (orga4Width / 2) };
                int[] imporyNoticeMainTablWidth = { (orga4Width / 5), (orga4Width / 3), (orga4Width / 1) };


                document.Open();

                #region header table

                PdfPTable headerMainTbl = new PdfPTable(2);
                headerMainTbl.SetWidths(mainTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5;
                cell.BackgroundColor = new Color(43, 137, 174);
                cell.Colspan = 2;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                headerMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 20;
                cell.PaddingRight = 20;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(headerLeftLogo);
                headerMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 200;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(headerRightLogo);
                headerMainTbl.AddCell(cell);

                #endregion

                #region header table title
                int[] headerTitleTblWidth = { orga4Width / 1, orga4Width / 5 };
                PdfPTable headerTitleTbl = new PdfPTable(2);
                headerTitleTbl.SetWidths(headerTitleTblWidth);


                cell = new PdfPCell(new Phrase("Motor Insurance Quotation - Comprehensive  (Private Use)", MainTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerTitleTbl.AddCell(cell);

                if (qrefNo.Length > 12)
                {
                    cell = new PdfPCell(new Phrase("REF: " + qrefNo, NormalFont8));

                }
                else
                {
                    cell = new PdfPCell(new Phrase("REF: " + qrefNo, NormalFont9));

                }
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                headerTitleTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Thank you for the trust you have placed on Sri Lanka Insurance to invite our participation to protect your vehicle based on the information provided, we are pleased to forward our quotation as follows.", HeaderContentFont));
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 8;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                headerTitleTbl.AddCell(cell);

                #endregion

                #region top content table

                int[] topContentTblWidth = { 6, 1, 10, 5, 1, 8, 6, 1, 10 };

                PdfPTable topContentTbl = new PdfPTable(9);
                topContentTbl.SetWidths(topContentTblWidth);
                topContentTbl.HorizontalAlignment = Element.ALIGN_CENTER;

                //first regionLine strarts

                cell = new PdfPCell(new Phrase("Date of issue", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(dateOfIssue, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Vehicle Type", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(vehicleType, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Make", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(make, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //second regionLine strarts
                cell = new PdfPCell(new Phrase("Period of Cover", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont2));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(periodOfCover, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Sum Insured", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(sumInsured, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Model", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(model, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //third regionLine strarts
                if (IsVechleNo)
                {
                    cell = new PdfPCell(new Phrase("Vehicle No.", NormalFont9));
                }
                else
                {
                    cell = new PdfPCell(new Phrase("Chassis No.", NormalFont9));

                }
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(vehicleChassNo, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("YOM", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(yOm, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Fuel Type", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(fuelType, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //fourth regionLine  strarts



                cell = new PdfPCell(new Phrase("Insured Name", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(insuredName, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Passengers", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.PaddingTop = -5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(passangers, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                #endregion

                #region top hr line table

                PdfPTable headerHrLineTbl = new PdfPTable(2);
                headerHrLineTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                headerHrLineTbl.AddCell(cell);

                #endregion

                #region bottom hr line table

                PdfPTable footerHrLineTbl = new PdfPTable(2);
                footerHrLineTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 20;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                footerHrLineTbl.AddCell(cell);

                #endregion

                #region premium breakup table

                //premium break up 1st clumn  table

                PdfPTable preBreakcolumn1Tbl1 = new PdfPTable(2);
                preBreakcolumn1Tbl1.SetWidths(preBreakClmnTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rs.Cts.", RsCtnFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Net Premium", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(netPremium, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("RCC On Vehicle", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(rcc, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("TC On Vehicle", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(tc, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Admin Fee", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(adminFee, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Road Safety Fund", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(roadSafetyFund, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Stamp Duty", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(stampDuty, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);


                //premium break up 2ND clumn  table

                PdfPTable preBreakcolumn1Tbl2 = new PdfPTable(2);
                preBreakcolumn1Tbl2.SetWidths(preBreakClmnTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rs.Cts.", RsCtnFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Policy Fee", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(policyFee, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                if (nbt == "0.00")
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    preBreakcolumn1Tbl2.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    preBreakcolumn1Tbl2.AddCell(cell);

                }
                else
                {
                    cell = new PdfPCell(new Phrase("NBT", NormalFont8));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = 3;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    preBreakcolumn1Tbl2.AddCell(cell);

                    cell = new PdfPCell(new Phrase(nbt, NormalFont8));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = 3;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    preBreakcolumn1Tbl2.AddCell(cell);


                }


                cell = new PdfPCell(new Phrase("Taxes", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(taxes, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 20;

                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);


                cell = new PdfPCell(new Phrase("Total Payable Amount", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(totalPre, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.PaddingBottom = 5;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 2;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                //main table
                PdfPTable preBreakMainTbl = new PdfPTable(3);
                preBreakMainTbl.SetWidths(preBreakTablWidth);

                cell = new PdfPCell(new Phrase("Premium Breakup: ", preBreakTitleFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                preBreakMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(preBreakcolumn1Tbl1);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakMainTbl.AddCell(cell);



                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(preBreakcolumn1Tbl2);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakMainTbl.AddCell(cell);

                int[] authtblWidth = { orga4Width / 4 };
                PdfPTable authTbl = new PdfPTable(1);
                authTbl.SetWidths(authtblWidth);

                if (qAuth == 0)
                {
                    cell = new PdfPCell(new Phrase("Premium is subject to confirmation by an Authorized officer", AuthorFont));
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = new Color(43, 137, 174);
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.FixedHeight = 0;
                    cell.PaddingTop = 5;
                    cell.PaddingBottom = 5;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    authTbl.AddCell(cell);

                }
                else
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    authTbl.AddCell(cell);
                }

                cell = new PdfPCell();
                cell.PaddingTop = 20;
                cell.AddElement(authTbl);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                preBreakMainTbl.AddCell(cell);



                #endregion

                #region Cover Extension table

                PdfPTable coversTable = new PdfPTable(1);
                coversTable.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase(adCovers, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                //cell.FixedHeight = 40f;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                coversTable.AddCell(cell);

                if (string.IsNullOrEmpty(note))
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    coversTable.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    coversTable.AddCell(cell);

                }
                else
                {
                    cell = new PdfPCell(new Phrase("Note: " + note, NormalFont8i));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = -5;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    coversTable.AddCell(cell);



                }


                PdfPTable CoverExtensionTbl = new PdfPTable(1);
                CoverExtensionTbl.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5f;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                CoverExtensionTbl.AddCell(cell);


                cell = new PdfPCell(new Phrase("Cover Extension :", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 2;
                //cell.FixedHeight = 20f;
                cell.BackgroundColor = new Color(232, 231, 231);
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;


                CoverExtensionTbl.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(coversTable);
                cell.PaddingLeft = -25;

                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                CoverExtensionTbl.AddCell(cell);



                #endregion

                #region empty rows

                PdfPTable emptytopRow = new PdfPTable(1);
                emptytopRow.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5f;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.FixedHeight = 10;
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                emptytopRow.AddCell(cell);

                PdfPTable emptybottomRow = new PdfPTable(1);
                emptybottomRow.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5f;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.FixedHeight = 100;
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                emptybottomRow.AddCell(cell);

                #endregion
                

                #region benefits table
                //table 1


                PdfPTable benSubTbl1 = new PdfPTable(1);
                benSubTbl1.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("Speedy Settlement", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl1.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list1);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                //cell.AddElement(image);
                benSubTbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Comprehensive Coverage", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl1.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list2);
                //cell.AddElement(image);
                benSubTbl1.AddCell(cell);


                //table 2
                PdfPTable benSubTbl2 = new PdfPTable(1);
                benSubTbl2.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("Accuracy Guaranteed", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list3);
                //cell.AddElement(image);
                benSubTbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Technology", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list4);
                //cell.AddElement(image);
                benSubTbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Convenience", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list5);
                benSubTbl2.AddCell(cell);


                //---images table strats
                int[] imgTblWidth = { orga4Width / 2, orga4Width / 2 };
                var imgTable = new PdfPTable(2);
                imgTable.SetWidths(imgTblWidth);

                cell = new PdfPCell(new Phrase("Authorized Vehicle Agents", ConvenienceImgFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingLeft = 20;
                cell.PaddingRight = -25;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                imgTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Partner Garages", ConvenienceImgFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingLeft = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                imgTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 20;
                cell.PaddingRight = -25;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(patnersLogo);
                imgTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 50;
                cell.PaddingRight = 40;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(patnersQR);
                imgTable.AddCell(cell);

                //---images table ends
                cell = new PdfPCell(imgTable);
                cell.Border = Rectangle.NO_BORDER;
                benSubTbl2.AddCell(cell);


                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 0;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list6);
                benSubTbl2.AddCell(cell);

                //cell = new PdfPCell();
                //cell.AddElement(bankCard);
                //cell.Border = Rectangle.NO_BORDER;
                //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //cell.PaddingTop = -10;
                //cell.PaddingLeft = 102;
                //cell.PaddingRight = 100;
                //benSubTbl2.AddCell(cell);

                //main table
                PdfPTable benMainTbl = new PdfPTable(2);
                benMainTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase("Value Added Services", BenTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                benMainTbl.AddCell(cell);

                cell = new PdfPCell(benSubTbl1);
                cell.Border = Rectangle.NO_BORDER;
                benMainTbl.AddCell(cell);

                cell = new PdfPCell(benSubTbl2);
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                benMainTbl.AddCell(cell);

                #endregion

                #region footerImpNotice table

                int[] xSize = { orga4Width / 3 };
                PdfPTable ImpNotTbl = new PdfPTable(1);
                ImpNotTbl.SetWidths(xSize);

                cell = new PdfPCell(new Phrase("", FooterImportantNoticeFont));
                cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = new Color(242, 102, 49);
                //cell.PaddingTop = 5;
                cell.PaddingLeft = 35;
                cell.AddElement(ImprtNotice);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                ImpNotTbl.AddCell(cell);

                PdfPTable ImportNoticeTbl1 = new PdfPTable(3);
                ImportNoticeTbl1.SetWidths(imporyNoticeMainTablWidth);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.PaddingTop = 5;
                ImportNoticeTbl1.AddCell(cell);


                cell = new PdfPCell(new Phrase("*Conditions apply", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 100;
                cell.Rotation = 0;
                cell.PaddingTop = -20;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                ImportNoticeTbl1.AddCell(cell);


                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(ImpNotTbl);
                cell.PaddingLeft = -70;
                cell.PaddingBottom = 20;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                ImportNoticeTbl1.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 0;
                cell.PaddingBottom = 3;
                cell.AddElement(impNoteList);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                ImportNoticeTbl1.AddCell(cell);

                #endregion


                #region footerUserData table

                int[] userDataSize = { 200, 200, 200, 250 };
                PdfPTable UserDataTbl = new PdfPTable(4);
                UserDataTbl.SetWidths(userDataSize);

                cell = new PdfPCell(new Phrase("Issued By : " + UsrId, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);

                string printDate = "";

                if (qAuth == 0)
                {
                    printDate = "Print Date : ";
                }
                else
                {
                    printDate = "Reprint Date : ";
                }

                cell = new PdfPCell(new Phrase(printDate + pdfDate, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);

                var findDev = new DeviceFinder();

                cell = new PdfPCell(new Phrase("IP address : " + findDev.GetDeviceIPAddress(), motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);


                cell = new PdfPCell(new Phrase(epfAgentCode, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);


                #endregion

                #region footer table

                int[] footerTblSize = { orga4Width / 1, orga4Width / 4 };
                int[] footerAddSize = { orga4Width / 1 };
                int[] footerAddverSize = { orga4Width / 4 };

                //footer address table

                var footerAddtbl = new PdfPTable(1);
                footerAddtbl.SetWidths(footerAddSize);

                cell = new PdfPCell(new Phrase("No.21, Vauxhall Street, Colombo 02, Sri Lanka. Telephone : +94 11 2357 000, +94 11 2357 357, Fax : +94 112 447742", footerAddfont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("www.srilankainsurance.com", footerAddfont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Company Registration Number - PB289", footerAddfont));
                cell.PaddingBottom = 10;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                //footer advertisement table

                var footerAddver = new PdfPTable(1);
                footerAddver.SetWidths(footerAddverSize);

                cell = new PdfPCell();
                cell.Padding = 0;
                cell.AddElement(aaa);
                cell.PaddingBottom = 0;
                cell.Border = Rectangle.NO_BORDER;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                footerAddver.AddCell(cell);



                //footer main table
                var footerTbl = new PdfPTable(2);
                footerTbl.SetWidths(footerTblSize);

                cell = new PdfPCell(footerAddtbl);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = new Color(43, 137, 174);
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                footerTbl.AddCell(cell);

                cell = new PdfPCell(footerAddver);
                cell.Border = Rectangle.NO_BORDER;
                cell.BackgroundColor = new Color(43, 137, 174);
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                footerTbl.AddCell(cell);

                #endregion

                document.Add(headerMainTbl);
                document.Add(headerTitleTbl);
                document.Add(topContentTbl);
                document.Add(headerHrLineTbl);
                document.Add(preBreakMainTbl);
                document.Add(emptytopRow);
                document.Add(CoverExtensionTbl);
                document.Add(emptybottomRow);
                //if (vehicleType != "PRIVATE LORRY")
                //{
                //document.Add(benMainTbl);
                document.Add(footerHrLineTbl);
                //document.Add(ImportNoticeTbl1);
                //}

                document.Add(UserDataTbl);

                document.Add(footerTbl);


                document.Close();



                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                //-----email start

                if (isEmail == "Y")
                {
                    try
                    {

                        var _sendEmail = new SendEmail();

                        string subject = "Sri Lanka Insurance Motor Quotation for " + vehicleType.ToString() + " (" + qrefNo + ") ";

                        string emailmgs = "<center><div style='width: 95%;text-align:left;box-shadow: 0 0 1.5% #dfdad6;font-family:arial'>" +
                                          "<div><img style='width: 80vw' src='https://www.srilankainsurance.lk/motorQuotation/emailTop.jpg'></div><br>" +
                                          "<div style='padding-top:0%;font-size: 17px;text-transform: none;text-align:left;color:#000;font-family:arial;font-weight:normal'>" +
                                          "Dear Sir/Madam,<p style='padding-left: 1%;font-size: 16px;text-transform: none;line-height: 25px'>We are enclosing the best quotation (" + qrefNo + ") for your kind consideration. </p>" +
                                          "<div style='color: #000;font-size: 17px;text-align: left;line-height: 20px;padding-left: 1%;padding-right: 1%'>" +
                                          "Thank you for your trust on us.<hr/><br /><i style='Color: #00bab8;font-size:12px'>" +
                                          "<a href='https://www.srilankainsurance.com'>https://www.srilankainsurance.com</a></i>" +
                                          "</div></center>";


                        //receiverEmailId = "mrjanuplus@gmail.com";
                        toEmail = Convert.ToString(receiverEmailId);
                        EmailMsg = Convert.ToString(emailmgs);
                        EmailSubj = Convert.ToString(subject);



                        _sendEmail.Email_With_Attachment(FromEmailid, FromEmailPasscode, "Sri Lanka Insurance", toEmail, insuredName,"","", EmailSubj, EmailMsg, new MemoryStream(bytes), qrefNo + ".pdf","Y");


                        rtn = true;


                    }
                    catch (Exception ex)
                    {
                        string x = ex.ToString();
                    }
                }
                else
                {
                    System.Web.HttpContext.Current.Response.Buffer = false;

                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.ClearContent();
                    System.Web.HttpContext.Current.Response.ClearHeaders();

                    System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + qrefNo + ".pdf");
                    System.Web.HttpContext.Current.Response.BinaryWrite(bytes);

                    rtn = true;
                }



                //-------email ends



                //pdf.Text = "PDF";
                //pdf.Enabled = true;
            }
        }
        else 
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                Paragraph paragraph = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = null;


                int[] clmnTablWidth = { orga4Width };
                int[] mainTablWidth = { orga4Width / 2, orga4Width / 2 };
                int[] preBreakTablWidth = { orga4Width / 2, orga4Width / 2, orga4Width / 4 };
                int[] preBreakClmnTablWidth = { (orga4Width / 2), (orga4Width / 2) };
                int[] imporyNoticeMainTablWidth = { (orga4Width / 5), (orga4Width / 3), (orga4Width / 1) };


                document.Open();

                #region header table

                PdfPTable headerMainTbl = new PdfPTable(2);
                headerMainTbl.SetWidths(mainTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5;
                cell.BackgroundColor = new Color(43, 137, 174);
                cell.Colspan = 2;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                headerMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 20;
                cell.PaddingRight = 20;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(headerLeftLogo);
                headerMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 200;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(headerRightLogo);
                headerMainTbl.AddCell(cell);

                #endregion

                #region header table title
                int[] headerTitleTblWidth = { orga4Width / 1, orga4Width / 5 };
                PdfPTable headerTitleTbl = new PdfPTable(2);
                headerTitleTbl.SetWidths(headerTitleTblWidth);


                cell = new PdfPCell(new Phrase("Motor Insurance Quotation - Comprehensive  (Private Use)", MainTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerTitleTbl.AddCell(cell);

                if(qrefNo.Length >12)
                {
                    cell = new PdfPCell(new Phrase("REF: " + qrefNo, NormalFont8));
                    
                }
                else
                {
                    cell = new PdfPCell(new Phrase("REF: " + qrefNo, NormalFont9));

                }
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                headerTitleTbl.AddCell(cell);


                cell = new PdfPCell(new Phrase("Thank you for the trust you have placed on Sri Lanka Insurance to invite our participation to protect your vehicle based on the information provided, we are pleased to forward our quotation as follows.", HeaderContentFont));
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 8;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                headerTitleTbl.AddCell(cell);

                #endregion

                #region top content table

                int[] topContentTblWidth = { 6, 1, 10, 5, 1, 8, 6, 1, 10 };

                PdfPTable topContentTbl = new PdfPTable(9);
                topContentTbl.SetWidths(topContentTblWidth);
                topContentTbl.HorizontalAlignment = Element.ALIGN_CENTER;

                //first regionLine strarts

                cell = new PdfPCell(new Phrase("Date of issue", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(dateOfIssue, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Vehicle Type", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(vehicleType, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Make", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(make, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //second regionLine strarts
                cell = new PdfPCell(new Phrase("Period of Cover", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont2));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(periodOfCover, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Sum Insured", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(sumInsured, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Model", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(model, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //third regionLine strarts
                if (IsVechleNo)
                {
                    cell = new PdfPCell(new Phrase("Vehicle No.", NormalFont9));
                }
                else
                {
                    cell = new PdfPCell(new Phrase("Chassis No.", NormalFont9));

                }
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(vehicleChassNo, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("YOM", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(yOm, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Fuel Type", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(fuelType, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //fourth regionLine  strarts

             

                cell = new PdfPCell(new Phrase("Insured Name", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(insuredName, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Passengers", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.PaddingTop = -5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(passangers, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                #endregion

                #region top hr line table

                PdfPTable headerHrLineTbl = new PdfPTable(2);
                headerHrLineTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                headerHrLineTbl.AddCell(cell);

                #endregion

                #region bottom hr line table

                PdfPTable footerHrLineTbl = new PdfPTable(2);
                footerHrLineTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 20;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                footerHrLineTbl.AddCell(cell);

                #endregion

                #region premium breakup table

                //premium break up 1st clumn  table

                PdfPTable preBreakcolumn1Tbl1 = new PdfPTable(2);
                preBreakcolumn1Tbl1.SetWidths(preBreakClmnTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rs.Cts.", RsCtnFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Net Premium", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(netPremium, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("RCC On Vehicle", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(rcc, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("TC On Vehicle", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(tc, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Admin Fee", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(adminFee, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Road Safety Fund", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(roadSafetyFund, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Stamp Duty", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(stampDuty, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);


                //premium break up 2ND clumn  table

                PdfPTable preBreakcolumn1Tbl2 = new PdfPTable(2);
                preBreakcolumn1Tbl2.SetWidths(preBreakClmnTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rs.Cts.", RsCtnFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Policy Fee", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(policyFee, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                if (nbt == "0.00")
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    preBreakcolumn1Tbl2.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    preBreakcolumn1Tbl2.AddCell(cell);

                }
                else
                {
                    cell = new PdfPCell(new Phrase("NBT", NormalFont8));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = 3;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    preBreakcolumn1Tbl2.AddCell(cell);

                    cell = new PdfPCell(new Phrase(nbt, NormalFont8));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = 3;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    preBreakcolumn1Tbl2.AddCell(cell);


                }


                cell = new PdfPCell(new Phrase("Taxes", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(taxes, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 20;

                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);


                cell = new PdfPCell(new Phrase("Total Payable Amount", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(totalPre, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.PaddingBottom = 5;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 2;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                //main table
                PdfPTable preBreakMainTbl = new PdfPTable(3);
                preBreakMainTbl.SetWidths(preBreakTablWidth);

                cell = new PdfPCell(new Phrase("Premium Breakup: ", preBreakTitleFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                preBreakMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(preBreakcolumn1Tbl1);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakMainTbl.AddCell(cell);



                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(preBreakcolumn1Tbl2);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakMainTbl.AddCell(cell);

                int[] authtblWidth = { orga4Width / 4 };
                PdfPTable authTbl = new PdfPTable(1);
                authTbl.SetWidths(authtblWidth);

                if (qAuth == 0)
                {
                    cell = new PdfPCell(new Phrase("Premium is subject to confirmation by an Authorized officer", AuthorFont));
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = new Color(43, 137, 174);
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.FixedHeight = 0;
                    cell.PaddingTop = 5;
                    cell.PaddingBottom = 5;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    authTbl.AddCell(cell);

                }
                else
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    authTbl.AddCell(cell);
                }

                cell = new PdfPCell();
                cell.PaddingTop = 20;
                cell.AddElement(authTbl);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                preBreakMainTbl.AddCell(cell);



                #endregion

                #region Cover Extension table

                PdfPTable coversTable = new PdfPTable(1);
                coversTable.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase(adCovers, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                //cell.FixedHeight = 40f;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                coversTable.AddCell(cell);

                if (string.IsNullOrEmpty(note))
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    preBreakcolumn1Tbl2.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    coversTable.AddCell(cell);

                }
                else
                {
                    cell = new PdfPCell(new Phrase("Note: " + note, NormalFont8i));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = -5;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    coversTable.AddCell(cell);



                }


                PdfPTable CoverExtensionTbl = new PdfPTable(1);
                CoverExtensionTbl.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5f;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                CoverExtensionTbl.AddCell(cell);


                cell = new PdfPCell(new Phrase("Cover Extension :", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 2;
                //cell.FixedHeight = 20f;
                cell.BackgroundColor = new Color(232, 231, 231);
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;


                CoverExtensionTbl.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(coversTable);
                cell.PaddingLeft = -25;
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                CoverExtensionTbl.AddCell(cell);

                #endregion

                #region benefits table
                //table 1


                PdfPTable benSubTbl1 = new PdfPTable(1);
                benSubTbl1.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("Speedy Settlement", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl1.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list1);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                //cell.AddElement(image);
                benSubTbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Comprehensive Coverage", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl1.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list2);
                //cell.AddElement(image);
                benSubTbl1.AddCell(cell);


                //table 2
                PdfPTable benSubTbl2 = new PdfPTable(1);
                benSubTbl2.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("Accuracy Guaranteed", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list3);
                //cell.AddElement(image);
                benSubTbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Technology", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list4);
                //cell.AddElement(image);
                benSubTbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Convenience", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list5);
                benSubTbl2.AddCell(cell);


                //---images table strats
                int[] imgTblWidth = { orga4Width / 2, orga4Width / 2 };
                var imgTable = new PdfPTable(2);
                imgTable.SetWidths(imgTblWidth);

                cell = new PdfPCell(new Phrase("Authorized Vehicle Agents", ConvenienceImgFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingLeft = 20;
                cell.PaddingRight = -25;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                imgTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Partner Garages", ConvenienceImgFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingLeft = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                imgTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 20;
                cell.PaddingRight = -25;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(patnersLogo);
                imgTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 50;
                cell.PaddingRight = 40;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(patnersQR);
                imgTable.AddCell(cell);

                //---images table ends
                cell = new PdfPCell(imgTable);
                cell.Border = Rectangle.NO_BORDER;
                benSubTbl2.AddCell(cell);


                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 0;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list6);
                benSubTbl2.AddCell(cell);

                //cell = new PdfPCell();
                //cell.AddElement(bankCard);
                //cell.Border = Rectangle.NO_BORDER;
                //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //cell.PaddingTop = -10;
                //cell.PaddingLeft = 102;
                //cell.PaddingRight = 100;
                //benSubTbl2.AddCell(cell);

                //main table
                PdfPTable benMainTbl = new PdfPTable(2);
                benMainTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase("Value Added Services", BenTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                benMainTbl.AddCell(cell);

                cell = new PdfPCell(benSubTbl1);
                cell.Border = Rectangle.NO_BORDER;
                benMainTbl.AddCell(cell);

                cell = new PdfPCell(benSubTbl2);
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                benMainTbl.AddCell(cell);

                #endregion

                #region footerImpNotice table

                int[] xSize = { orga4Width / 3 };
                PdfPTable ImpNotTbl = new PdfPTable(1);
                ImpNotTbl.SetWidths(xSize);

                cell = new PdfPCell(new Phrase("", FooterImportantNoticeFont));
                cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = new Color(242, 102, 49);
                //cell.PaddingTop = 5;
                cell.PaddingLeft = 35;
                cell.AddElement(ImprtNotice);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                ImpNotTbl.AddCell(cell);

                PdfPTable ImportNoticeTbl1 = new PdfPTable(3);
                ImportNoticeTbl1.SetWidths(imporyNoticeMainTablWidth);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.PaddingTop = 5;
                ImportNoticeTbl1.AddCell(cell);


                cell = new PdfPCell(new Phrase("*Conditions apply", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 100;
                cell.Rotation = 0;
                cell.PaddingTop = -20;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                ImportNoticeTbl1.AddCell(cell);


                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(ImpNotTbl);
                cell.PaddingLeft = -70;
                cell.PaddingBottom = 20;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                ImportNoticeTbl1.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 0;
                cell.PaddingBottom = 3;
                cell.AddElement(impNoteList);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                ImportNoticeTbl1.AddCell(cell);

                #endregion


                #region footerUserData table

                int[] userDataSize = { 200, 200, 200, 250 };
                PdfPTable UserDataTbl = new PdfPTable(4);
                UserDataTbl.SetWidths(userDataSize);

                cell = new PdfPCell(new Phrase("Issued By : " + UsrId, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);

                string printDate = "";

                if (qAuth == 0)
                {
                    printDate = "Print Date : ";
                }
                else
                {
                    printDate = "Reprint Date : ";
                }

                cell = new PdfPCell(new Phrase(printDate + pdfDate, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);

                var ipAddr = new DeviceFinder();

                cell = new PdfPCell(new Phrase("IP address : " + ipAddr.GetDeviceIPAddress(), motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);


                cell = new PdfPCell(new Phrase(epfAgentCode, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);


                #endregion

                #region footer table

                int[] footerTblSize = { orga4Width / 1, orga4Width / 4 };
                int[] footerAddSize = { orga4Width / 1 };
                int[] footerAddverSize = { orga4Width / 4 };

                //footer address table

                var footerAddtbl = new PdfPTable(1);
                footerAddtbl.SetWidths(footerAddSize);

                cell = new PdfPCell(new Phrase("No.21, Vauxhall Street, Colombo 02, Sri Lanka. Telephone : +94 11 2357 000, +94 11 2357 357, Fax : +94 112 447742", footerAddfont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("www.srilankainsurance.com", footerAddfont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Company Registration Number - PB289", footerAddfont));
                cell.PaddingBottom = 10;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                //footer advertisement table

                var footerAddver = new PdfPTable(1);
                footerAddver.SetWidths(footerAddverSize);

                cell = new PdfPCell();
                cell.Padding = 0;
                cell.AddElement(aaa);
                cell.PaddingBottom = 0;
                cell.Border = Rectangle.NO_BORDER;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                footerAddver.AddCell(cell);



                //footer main table
                var footerTbl = new PdfPTable(2);
                footerTbl.SetWidths(footerTblSize);

                cell = new PdfPCell(footerAddtbl);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = new Color(43, 137, 174);
                footerTbl.AddCell(cell);

                cell = new PdfPCell(footerAddver);
                cell.Border = Rectangle.NO_BORDER;
                cell.BackgroundColor = new Color(43, 137, 174);
                footerTbl.AddCell(cell);

                #endregion

                document.Add(headerMainTbl);
                document.Add(headerTitleTbl);
                document.Add(topContentTbl);
                document.Add(headerHrLineTbl);
                document.Add(preBreakMainTbl);
                document.Add(CoverExtensionTbl);
             
                document.Add(benMainTbl);
                document.Add(footerHrLineTbl);
                document.Add(ImportNoticeTbl1);
                
                document.Add(UserDataTbl);

                document.Add(footerTbl);
                document.Close();



                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                //-----email start

                if (isEmail == "Y")
                {
                    try
                    {

                        var _sendEmail = new SendEmail();

                        string subject = "Sri Lanka Insurance Motor Quotation for " + vehicleType.ToString() + " (" + qrefNo + ")";


                        string emailmgs = "<center><div style='width: 95%;text-align:left;box-shadow: 0 0 1.5% #dfdad6;font-family:arial'>" +
                                          "<div><img style='width: 80vw' src='https://www.srilankainsurance.lk/motorQuotation/emailTop.jpg'></div><br>" +
                                          "<div style='padding-top:0%;font-size: 17px;text-transform: none;text-align:left;color:#000;font-family:arial;font-weight:normal'>" +
                                          "Dear Sir/Madam,<p style='padding-left: 1%;font-size: 16px;text-transform: none;line-height: 25px'>We are enclosing the best quotation ("+ qrefNo + ") for your kind consideration. </p>" +
                                          "<div style='color: #000;font-size: 17px;text-align: left;line-height: 20px;padding-left: 1%;padding-right: 1%'>" +
                                          "Thank you for your trust on us.<hr/><br /><i style='Color: #00bab8;font-size:12px'>" +
                                          "<a href='https://www.srilankainsurance.com'>https://www.srilankainsurance.com</a></i>" +
                                          "</div></center>";


                        //receiverEmailId = "mrjanuplus@gmail.com";
                        toEmail = Convert.ToString(receiverEmailId);
                        EmailMsg = Convert.ToString(emailmgs);
                        EmailSubj = Convert.ToString(subject);



                        _sendEmail.Email_With_Attachment(FromEmailid, FromEmailPasscode, "Sri Lanka Insurance", toEmail, insuredName,"","", EmailSubj, EmailMsg, new MemoryStream(bytes), qrefNo + ".pdf","Y");


                        rtn = true;
                      

                    }
                    catch (Exception ex)
                    {
                        string x = ex.ToString();
                    }
                }
                else
                {
                    HttpContext.Current.Response.Buffer = false;

                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.ClearHeaders();

                    HttpContext.Current.Response.ContentType = "application/pdf";
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + qrefNo + ".pdf");
                    HttpContext.Current.Response.BinaryWrite(bytes);

                    rtn = true;
                }

                
            }
        }
       

        return rtn;
    }


    public bool getPDFQuotationAttachment(string qrefNo, string userID,out MemoryStream ms)
    {
        ms = new MemoryStream();
        UsrId = userID;
        var quot = new printQuotation();
        


        quot.pushprintQuotation(qrefNo, out dateOfIssue, out vehicleType, out make, out sumInsured, out periodOfCover, out vehicleChassNo, out yOm, out fuelType,
              out netPremium, out rcc, out tc, out adminFee, out roadSafetyFund, out stampDuty, out policyFee, out nbt, out taxes, out totalPre, out adCovers,
              out issuedBy, out pdfDate, out epfAgentCode, out passangers, out insuredName, out note, out receiverEmailId, out IsVechleNo,out model);

        bool rtn = false;

        //qAuth = Convert.ToInt32(quot.quotationApprovalIs(qrefNo));
        qAuth = 1;
        #region master settings

        Double a4Width = PageSize.A4.Width;
        int orga4Width = Convert.ToInt32(a4Width);
        Document document = new Document(PageSize.A4, -50, -50, 2, 2);

        Font RsCtnFont = FontFactory.GetFont("Oxygen", 6, Font.ITALIC, new Color(107, 109, 113));
        Font tcFont = FontFactory.GetFont("Oxygen", 8, Font.NORMAL, new Color(242, 105, 50));

        Font motorCom = FontFactory.GetFont("Oxygen", 6, Font.NORMAL, new Color(107, 109, 113));
        Font NormalFont2 = FontFactory.GetFont("Oxygen", 7, Font.NORMAL, new Color(107, 109, 113));
        Font ConvenienceImgFont = FontFactory.GetFont("Oxygen", 7, Font.BOLD, new Color(0, 135, 185));
        Font AuthorFont = FontFactory.GetFont("Oxygen", 7, Font.NORMAL, new Color(43, 137, 174));
        Font NormalFont8 = FontFactory.GetFont("Oxygen", 8, Font.NORMAL, new Color(107, 109, 113));
        Font NormalFont8i = FontFactory.GetFont("Oxygen", 8, Font.ITALIC, new Color(107, 109, 113));
        Font NormalFont9 = FontFactory.GetFont("Oxygen", 9, Font.NORMAL, new Color(107, 109, 113));
        Font SubTileFont = FontFactory.GetFont("Oxygen", 10, Font.BOLD, new Color(242, 105, 50));
        Font HeaderContentFont = FontFactory.GetFont("Oxygen", 10, Font.NORMAL, new Color(107, 109, 113));
        Font TileFont = FontFactory.GetFont("Oxygen", 12, Font.NORMAL, new Color(107, 109, 113));
        Font BenTileFont = FontFactory.GetFont("Oxygen", 12, Font.BOLD, new Color(43, 137, 174));
        Font MainTileFont = FontFactory.GetFont("Oxygen", 13, Font.BOLD, new Color(43, 137, 174));
        Font preBreakTitleFont = FontFactory.GetFont("Oxygen", 10, Font.NORMAL, new Color(107, 109, 113));
        Font FooterImportantNoticeFont = FontFactory.GetFont("Oxygen", 10, Font.NORMAL, new Color(255, 255, 255));
        Font footerAddfont = FontFactory.GetFont("Oxygen", 8, Font.NORMAL, new Color(255, 255, 255));


        var headerLeftLogo = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/motorPlus.png"));
        var headerRightLogo = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/slicLogo.png"));
        var patnersLogo = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/patnersLogo.png"));
        var patnersQR = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/quotQR.png"));
        var bullet = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/carIcon.png"));
        var aaa = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/aaa.png"));
        var bankCard = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/banksLogoBG.png"));
        var ImprtNotice = Image.GetInstance(HttpContext.Current.Server.MapPath(@"../images/ImpNote.png"));


        #endregion

        #region benefits list

        Chunk bullet1 = new Chunk(bullet, 0, 0, true);
        bullet.ScaleToFit(10, 10);


        List list1 = new List(List.UNORDERED, 10f);
        //list1.SetListSymbol(new Chunk(Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath(@"../images/carIcon.png"))));
        list1.SetListSymbol("\u2022");

        list1.IndentationLeft = 5f;
        list1.Add(new iTextSharp.text.ListItem("Settlement of Insurance claims below Rs. 75,000 within 3 working hours.\n\n", NormalFont2));
        list1.Add(new iTextSharp.text.ListItem("Fast track settlement method available for Claim below Rs.500,000/- with less documentation when repaired at SLIC partner garages.\n\n", NormalFont2));
        list1.Add(new iTextSharp.text.ListItem("Get a per day rental allowance* for Private cars, Jeeps and Double cabs when your vehicle is repaired at a SLIC partner garage, based on labour hours in excess of 4 days up to maximum of 10 days.\n\n", NormalFont2));
        list1.Add(new iTextSharp.text.ListItem("Protection of 'No Claim Bonus' for loyal customers who earned 60% or more No Claim Bonus without additional premium for one claim for the respective policy year.", NormalFont2));

        List list2 = new List(List.UNORDERED, 10f);
        list2.SetListSymbol("\u2022");

        list2.IndentationLeft = 5f;
        list2.Add(new iTextSharp.text.ListItem("100% free air bag cover for private cars and private dual purpose vehicles  (Based on the year of manufacture).*\n\n", NormalFont2));
        list2.Add(new iTextSharp.text.ListItem("Free personal accidents cover worth of Rs. 1,000,000 for the insured and\nRs. 50, 000 for 3 passengers in the event of the death due to an accident.", NormalFont2));

        List list3 = new List(List.UNORDERED, 10f);
        list3.SetListSymbol("\u2022");

        list3.IndentationLeft = 5f;
        list3.Add(new iTextSharp.text.ListItem("Accurate on-site assessment of accident damages by our team of over 175 technical professionals.\n\n", NormalFont2));
        list3.Add(new iTextSharp.text.ListItem("Precise settlement of insurance claim.", NormalFont2));

        List list4 = new List(List.UNORDERED, 10f);
        list4.SetListSymbol("\u2022");

        list4.IndentationLeft = 5f;
        list4.Add(new iTextSharp.text.ListItem("Renew your motor insurance Online via Sri Lanka Insurance customer portal and Sri Lanka Insurance mobile app.\n\n", NormalFont2));
        list4.Add(new iTextSharp.text.ListItem("Send repairer estimate through Whatsapp and Viber on 0772 357 357.", NormalFont2));

        List list5 = new List(List.UNORDERED, 10f);
        list5.SetListSymbol("\u2022");

        list5.IndentationLeft = 5f;
        list5.Add(new iTextSharp.text.ListItem("Cashless settlement facility at authorized vehicle agencies* (manufacturing year within 5 years) and SLIC partner garages without owner's account contribution.", NormalFont2));

        List list6 = new List(List.UNORDERED, 10f);
        list6.SetListSymbol("\u2022");

        list6.IndentationLeft = 5f;
        list6.Add(new iTextSharp.text.ListItem("Flexible payment plans and vast variety of payment options. (HSBC, AMEX, and COM BANK credit card)", NormalFont2));
        #endregion

        #region important notice list
        List impNoteList = new List(List.UNORDERED, 10f);
        impNoteList.SetListSymbol("\u2022");

        impNoteList.IndentationLeft = 5f;
        impNoteList.Add(new iTextSharp.text.ListItem("In additional to above premium, Luxury / Semi Luxury taxes should be paid if applicable.", NormalFont2));
        impNoteList.Add(new iTextSharp.text.ListItem("We strongly recommend that Sum insured of the vehicle reflects its market value.", NormalFont2));
        impNoteList.Add(new iTextSharp.text.ListItem("This quotation is invalid if the vehicle is currently a SLIC renewal or have previous claims with SLIC.", NormalFont2));
        impNoteList.Add(new iTextSharp.text.ListItem("This quotation is valid for 15 days from the date of this quotation.", NormalFont2));
        #endregion

        if (vehicleType != "PRIVATE CAR" || vehicleType != "DOUBLE CAB" || vehicleType != "CREW CAB"
                || vehicleType != "VAN")
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                Paragraph paragraph = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = null;

                int[] clmnTablWidth = { orga4Width };
                int[] mainTablWidth = { orga4Width / 2, orga4Width / 2 };
                int[] preBreakTablWidth = { orga4Width / 2, orga4Width / 2, orga4Width / 4 };
                int[] preBreakClmnTablWidth = { (orga4Width / 2), (orga4Width / 2) };
                int[] imporyNoticeMainTablWidth = { (orga4Width / 5), (orga4Width / 3), (orga4Width / 1) };


                document.Open();

                #region header table

                PdfPTable headerMainTbl = new PdfPTable(2);
                headerMainTbl.SetWidths(mainTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5;
                cell.BackgroundColor = new Color(43, 137, 174);
                cell.Colspan = 2;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                headerMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 20;
                cell.PaddingRight = 20;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(headerLeftLogo);
                headerMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 200;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(headerRightLogo);
                headerMainTbl.AddCell(cell);

                #endregion

                #region header table title
                int[] headerTitleTblWidth = { orga4Width / 1, orga4Width / 5 };
                PdfPTable headerTitleTbl = new PdfPTable(2);
                headerTitleTbl.SetWidths(headerTitleTblWidth);


                cell = new PdfPCell(new Phrase("Motor Insurance Quotation - Comprehensive  (Private Use)", MainTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerTitleTbl.AddCell(cell);

                if (qrefNo.Length > 12)
                {
                    cell = new PdfPCell(new Phrase("REF: " + qrefNo, NormalFont8));

                }
                else
                {
                    cell = new PdfPCell(new Phrase("REF: " + qrefNo, NormalFont9));

                }
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                headerTitleTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Thank you for the trust you have placed on Sri Lanka Insurance to invite our participation to protect your vehicle based on the information provided, we are pleased to forward our quotation as follows.", HeaderContentFont));
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 8;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                headerTitleTbl.AddCell(cell);

                #endregion

                #region top content table

                int[] topContentTblWidth = { 6, 1, 10, 5, 1, 8, 6, 1, 10 };

                PdfPTable topContentTbl = new PdfPTable(9);
                topContentTbl.SetWidths(topContentTblWidth);
                topContentTbl.HorizontalAlignment = Element.ALIGN_CENTER;

                //first regionLine strarts

                cell = new PdfPCell(new Phrase("Date of issue", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(dateOfIssue, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Vehicle Type", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(vehicleType, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Make", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(make, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //second regionLine strarts
                cell = new PdfPCell(new Phrase("Period of Cover", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont2));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(periodOfCover, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Sum Insured", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(sumInsured, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Model", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(model, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //third regionLine strarts
                if (IsVechleNo)
                {
                    cell = new PdfPCell(new Phrase("Vehicle No.", NormalFont9));
                }
                else
                {
                    cell = new PdfPCell(new Phrase("Chassis No.", NormalFont9));

                }
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(vehicleChassNo, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("YOM", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(yOm, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Fuel Type", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(fuelType, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //fourth regionLine  strarts



                cell = new PdfPCell(new Phrase("Insured Name", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(insuredName, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Passengers", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.PaddingTop = -5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(passangers, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                #endregion

                #region top hr line table

                PdfPTable headerHrLineTbl = new PdfPTable(2);
                headerHrLineTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                headerHrLineTbl.AddCell(cell);

                #endregion

                #region bottom hr line table

                PdfPTable footerHrLineTbl = new PdfPTable(2);
                footerHrLineTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 20;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                footerHrLineTbl.AddCell(cell);

                #endregion

                #region premium breakup table

                //premium break up 1st clumn  table

                PdfPTable preBreakcolumn1Tbl1 = new PdfPTable(2);
                preBreakcolumn1Tbl1.SetWidths(preBreakClmnTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rs.Cts.", RsCtnFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Net Premium", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(netPremium, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("RCC On Vehicle", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(rcc, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("TC On Vehicle", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(tc, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Admin Fee", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(adminFee, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Road Safety Fund", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(roadSafetyFund, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Stamp Duty", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(stampDuty, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);


                //premium break up 2ND clumn  table

                PdfPTable preBreakcolumn1Tbl2 = new PdfPTable(2);
                preBreakcolumn1Tbl2.SetWidths(preBreakClmnTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rs.Cts.", RsCtnFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Policy Fee", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(policyFee, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                if (nbt == "0.00")
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    preBreakcolumn1Tbl2.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    preBreakcolumn1Tbl2.AddCell(cell);

                }
                else
                {
                    cell = new PdfPCell(new Phrase("NBT", NormalFont8));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = 3;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    preBreakcolumn1Tbl2.AddCell(cell);

                    cell = new PdfPCell(new Phrase(nbt, NormalFont8));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = 3;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    preBreakcolumn1Tbl2.AddCell(cell);


                }


                cell = new PdfPCell(new Phrase("Taxes", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(taxes, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 20;

                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);


                cell = new PdfPCell(new Phrase("Total Payable Amount", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(totalPre, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.PaddingBottom = 5;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 2;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                //main table
                PdfPTable preBreakMainTbl = new PdfPTable(3);
                preBreakMainTbl.SetWidths(preBreakTablWidth);

                cell = new PdfPCell(new Phrase("Premium Breakup: ", preBreakTitleFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                preBreakMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(preBreakcolumn1Tbl1);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakMainTbl.AddCell(cell);



                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(preBreakcolumn1Tbl2);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakMainTbl.AddCell(cell);

                int[] authtblWidth = { orga4Width / 4 };
                PdfPTable authTbl = new PdfPTable(1);
                authTbl.SetWidths(authtblWidth);

                if (qAuth == 0)
                {
                    cell = new PdfPCell(new Phrase("Premium is subject to confirmation by an Authorized officer", AuthorFont));
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = new Color(43, 137, 174);
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.FixedHeight = 0;
                    cell.PaddingTop = 5;
                    cell.PaddingBottom = 5;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    authTbl.AddCell(cell);

                }
                else
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    authTbl.AddCell(cell);
                }

                cell = new PdfPCell();
                cell.PaddingTop = 20;
                cell.AddElement(authTbl);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                preBreakMainTbl.AddCell(cell);



                #endregion

                #region Cover Extension table

                PdfPTable coversTable = new PdfPTable(1);
                coversTable.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase(adCovers, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                //cell.FixedHeight = 40f;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                coversTable.AddCell(cell);

                if (string.IsNullOrEmpty(note))
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    coversTable.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    coversTable.AddCell(cell);

                }
                else
                {
                    cell = new PdfPCell(new Phrase("Note: " + note, NormalFont8i));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = -5;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    coversTable.AddCell(cell);



                }


                PdfPTable CoverExtensionTbl = new PdfPTable(1);
                CoverExtensionTbl.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5f;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                CoverExtensionTbl.AddCell(cell);


                cell = new PdfPCell(new Phrase("Cover Extension :", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 2;
                //cell.FixedHeight = 20f;
                cell.BackgroundColor = new Color(232, 231, 231);
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;


                CoverExtensionTbl.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(coversTable);
                cell.PaddingLeft = -25;

                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                CoverExtensionTbl.AddCell(cell);



                #endregion

                #region empty rows

                PdfPTable emptytopRow = new PdfPTable(1);
                emptytopRow.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5f;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.FixedHeight = 10;
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                emptytopRow.AddCell(cell);

                PdfPTable emptybottomRow = new PdfPTable(1);
                emptybottomRow.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5f;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.FixedHeight = 100;
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                emptybottomRow.AddCell(cell);

                #endregion


                #region benefits table
                //table 1


                PdfPTable benSubTbl1 = new PdfPTable(1);
                benSubTbl1.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("Speedy Settlement", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl1.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list1);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                //cell.AddElement(image);
                benSubTbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Comprehensive Coverage", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl1.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list2);
                //cell.AddElement(image);
                benSubTbl1.AddCell(cell);


                //table 2
                PdfPTable benSubTbl2 = new PdfPTable(1);
                benSubTbl2.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("Accuracy Guaranteed", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list3);
                //cell.AddElement(image);
                benSubTbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Technology", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list4);
                //cell.AddElement(image);
                benSubTbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Convenience", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list5);
                benSubTbl2.AddCell(cell);


                //---images table strats
                int[] imgTblWidth = { orga4Width / 2, orga4Width / 2 };
                var imgTable = new PdfPTable(2);
                imgTable.SetWidths(imgTblWidth);

                cell = new PdfPCell(new Phrase("Authorized Vehicle Agents", ConvenienceImgFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingLeft = 20;
                cell.PaddingRight = -25;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                imgTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Partner Garages", ConvenienceImgFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingLeft = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                imgTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 20;
                cell.PaddingRight = -25;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(patnersLogo);
                imgTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 50;
                cell.PaddingRight = 40;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(patnersQR);
                imgTable.AddCell(cell);

                //---images table ends
                cell = new PdfPCell(imgTable);
                cell.Border = Rectangle.NO_BORDER;
                benSubTbl2.AddCell(cell);


                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 0;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list6);
                benSubTbl2.AddCell(cell);

                //cell = new PdfPCell();
                //cell.AddElement(bankCard);
                //cell.Border = Rectangle.NO_BORDER;
                //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //cell.PaddingTop = -10;
                //cell.PaddingLeft = 102;
                //cell.PaddingRight = 100;
                //benSubTbl2.AddCell(cell);

                //main table
                PdfPTable benMainTbl = new PdfPTable(2);
                benMainTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase("Value Added Services", BenTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                benMainTbl.AddCell(cell);

                cell = new PdfPCell(benSubTbl1);
                cell.Border = Rectangle.NO_BORDER;
                benMainTbl.AddCell(cell);

                cell = new PdfPCell(benSubTbl2);
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                benMainTbl.AddCell(cell);

                #endregion

                #region footerImpNotice table

                int[] xSize = { orga4Width / 3 };
                PdfPTable ImpNotTbl = new PdfPTable(1);
                ImpNotTbl.SetWidths(xSize);

                cell = new PdfPCell(new Phrase("", FooterImportantNoticeFont));
                cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = new Color(242, 102, 49);
                //cell.PaddingTop = 5;
                cell.PaddingLeft = 35;
                cell.AddElement(ImprtNotice);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                ImpNotTbl.AddCell(cell);

                PdfPTable ImportNoticeTbl1 = new PdfPTable(3);
                ImportNoticeTbl1.SetWidths(imporyNoticeMainTablWidth);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.PaddingTop = 5;
                ImportNoticeTbl1.AddCell(cell);


                cell = new PdfPCell(new Phrase("*Conditions apply", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 100;
                cell.Rotation = 0;
                cell.PaddingTop = -20;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                ImportNoticeTbl1.AddCell(cell);


                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(ImpNotTbl);
                cell.PaddingLeft = -70;
                cell.PaddingBottom = 20;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                ImportNoticeTbl1.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 0;
                cell.PaddingBottom = 3;
                cell.AddElement(impNoteList);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                ImportNoticeTbl1.AddCell(cell);

                #endregion


                #region footerUserData table

                int[] userDataSize = { 200, 200, 200, 250 };
                PdfPTable UserDataTbl = new PdfPTable(4);
                UserDataTbl.SetWidths(userDataSize);

                cell = new PdfPCell(new Phrase("Issued By : " + UsrId, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);

                string printDate = "";

                if (qAuth == 0)
                {
                    printDate = "Print Date : ";
                }
                else
                {
                    printDate = "Reprint Date : ";
                }

                cell = new PdfPCell(new Phrase(printDate + pdfDate, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);

                var findDev = new DeviceFinder();

                cell = new PdfPCell(new Phrase("IP address : " + findDev.GetDeviceIPAddress(), motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);


                cell = new PdfPCell(new Phrase(epfAgentCode, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);


                #endregion

                #region footer table

                int[] footerTblSize = { orga4Width / 1, orga4Width / 4 };
                int[] footerAddSize = { orga4Width / 1 };
                int[] footerAddverSize = { orga4Width / 4 };

                //footer address table

                var footerAddtbl = new PdfPTable(1);
                footerAddtbl.SetWidths(footerAddSize);

                cell = new PdfPCell(new Phrase("No.21, Vauxhall Street, Colombo 02, Sri Lanka. Telephone : +94 11 2357 000, +94 11 2357 357, Fax : +94 112 447742", footerAddfont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("www.srilankainsurance.com", footerAddfont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Company Registration Number - PB289", footerAddfont));
                cell.PaddingBottom = 10;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                //footer advertisement table

                var footerAddver = new PdfPTable(1);
                footerAddver.SetWidths(footerAddverSize);

                cell = new PdfPCell();
                cell.Padding = 0;
                cell.AddElement(aaa);
                cell.PaddingBottom = 0;
                cell.Border = Rectangle.NO_BORDER;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                footerAddver.AddCell(cell);



                //footer main table
                var footerTbl = new PdfPTable(2);
                footerTbl.SetWidths(footerTblSize);

                cell = new PdfPCell(footerAddtbl);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = new Color(43, 137, 174);
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                footerTbl.AddCell(cell);

                cell = new PdfPCell(footerAddver);
                cell.Border = Rectangle.NO_BORDER;
                cell.BackgroundColor = new Color(43, 137, 174);
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                footerTbl.AddCell(cell);

                #endregion

                document.Add(headerMainTbl);
                document.Add(headerTitleTbl);
                document.Add(topContentTbl);
                document.Add(headerHrLineTbl);
                document.Add(preBreakMainTbl);
                document.Add(emptytopRow);
                document.Add(CoverExtensionTbl);
                document.Add(emptybottomRow);
                //if (vehicleType != "PRIVATE LORRY")
                //{
                //document.Add(benMainTbl);
                document.Add(footerHrLineTbl);
                //document.Add(ImportNoticeTbl1);
                //}

                document.Add(UserDataTbl);

                document.Add(footerTbl);


                document.Close();



                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

               

                ms = new MemoryStream(bytes);
                rtn = true;
                
            }
        }
        else
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);
                Phrase phrase = null;
                Paragraph paragraph = null;
                PdfPCell cell = null;
                PdfPTable table = null;
                Color color = null;


                int[] clmnTablWidth = { orga4Width };
                int[] mainTablWidth = { orga4Width / 2, orga4Width / 2 };
                int[] preBreakTablWidth = { orga4Width / 2, orga4Width / 2, orga4Width / 4 };
                int[] preBreakClmnTablWidth = { (orga4Width / 2), (orga4Width / 2) };
                int[] imporyNoticeMainTablWidth = { (orga4Width / 5), (orga4Width / 3), (orga4Width / 1) };


                document.Open();

                #region header table

                PdfPTable headerMainTbl = new PdfPTable(2);
                headerMainTbl.SetWidths(mainTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5;
                cell.BackgroundColor = new Color(43, 137, 174);
                cell.Colspan = 2;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                headerMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 20;
                cell.PaddingRight = 20;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(headerLeftLogo);
                headerMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 200;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(headerRightLogo);
                headerMainTbl.AddCell(cell);

                #endregion

                #region header table title
                int[] headerTitleTblWidth = { orga4Width / 1, orga4Width / 5 };
                PdfPTable headerTitleTbl = new PdfPTable(2);
                headerTitleTbl.SetWidths(headerTitleTblWidth);


                cell = new PdfPCell(new Phrase("Motor Insurance Quotation - Comprehensive  (Private Use)", MainTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerTitleTbl.AddCell(cell);

                if (qrefNo.Length > 12)
                {
                    cell = new PdfPCell(new Phrase("REF: " + qrefNo, NormalFont8));

                }
                else
                {
                    cell = new PdfPCell(new Phrase("REF: " + qrefNo, NormalFont9));

                }
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                headerTitleTbl.AddCell(cell);


                cell = new PdfPCell(new Phrase("Thank you for the trust you have placed on Sri Lanka Insurance to invite our participation to protect your vehicle based on the information provided, we are pleased to forward our quotation as follows.", HeaderContentFont));
                cell.Colspan = 2;
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 8;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                headerTitleTbl.AddCell(cell);

                #endregion

                #region top content table

                int[] topContentTblWidth = { 6, 1, 10, 5, 1, 8, 6, 1, 10 };

                PdfPTable topContentTbl = new PdfPTable(9);
                topContentTbl.SetWidths(topContentTblWidth);
                topContentTbl.HorizontalAlignment = Element.ALIGN_CENTER;

                //first regionLine strarts

                cell = new PdfPCell(new Phrase("Date of issue", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(dateOfIssue, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Vehicle Type", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(vehicleType, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Make", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(make, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //second regionLine strarts
                cell = new PdfPCell(new Phrase("Period of Cover", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont2));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(periodOfCover, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Sum Insured", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(sumInsured, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Model", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(model, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //third regionLine strarts
                if (IsVechleNo)
                {
                    cell = new PdfPCell(new Phrase("Vehicle No.", NormalFont9));
                }
                else
                {
                    cell = new PdfPCell(new Phrase("Chassis No.", NormalFont9));

                }
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(vehicleChassNo, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("YOM", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(yOm, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Fuel Type", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(fuelType, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                //fourth regionLine  strarts



                cell = new PdfPCell(new Phrase("Insured Name", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(insuredName, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Passengers", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.PaddingTop = -5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase(passangers, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont9));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                topContentTbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                topContentTbl.AddCell(cell);
                //endregion

                #endregion

                #region top hr line table

                PdfPTable headerHrLineTbl = new PdfPTable(2);
                headerHrLineTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                headerHrLineTbl.AddCell(cell);

                #endregion

                #region bottom hr line table

                PdfPTable footerHrLineTbl = new PdfPTable(2);
                footerHrLineTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 20;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                footerHrLineTbl.AddCell(cell);

                #endregion

                #region premium breakup table

                //premium break up 1st clumn  table

                PdfPTable preBreakcolumn1Tbl1 = new PdfPTable(2);
                preBreakcolumn1Tbl1.SetWidths(preBreakClmnTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rs.Cts.", RsCtnFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Net Premium", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(netPremium, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("RCC On Vehicle", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(rcc, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("TC On Vehicle", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(tc, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Admin Fee", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(adminFee, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Road Safety Fund", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(roadSafetyFund, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Stamp Duty", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase(stampDuty, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl1.AddCell(cell);


                //premium break up 2ND clumn  table

                PdfPTable preBreakcolumn1Tbl2 = new PdfPTable(2);
                preBreakcolumn1Tbl2.SetWidths(preBreakClmnTablWidth);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Rs.Cts.", RsCtnFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Policy Fee", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(policyFee, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                if (nbt == "0.00")
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    preBreakcolumn1Tbl2.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    preBreakcolumn1Tbl2.AddCell(cell);

                }
                else
                {
                    cell = new PdfPCell(new Phrase("NBT", NormalFont8));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = 3;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    preBreakcolumn1Tbl2.AddCell(cell);

                    cell = new PdfPCell(new Phrase(nbt, NormalFont8));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = 3;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    preBreakcolumn1Tbl2.AddCell(cell);


                }


                cell = new PdfPCell(new Phrase("Taxes", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(taxes, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 20;

                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 1;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);


                cell = new PdfPCell(new Phrase("Total Payable Amount", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(totalPre, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 3;
                cell.PaddingBottom = 5;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                preBreakcolumn1Tbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase(""));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 2;
                cell.BackgroundColor = new Color(107, 109, 113);
                cell.Colspan = 2;
                cell.PaddingTop = 10;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakcolumn1Tbl2.AddCell(cell);

                //main table
                PdfPTable preBreakMainTbl = new PdfPTable(3);
                preBreakMainTbl.SetWidths(preBreakTablWidth);

                cell = new PdfPCell(new Phrase("Premium Breakup: ", preBreakTitleFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                preBreakMainTbl.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(preBreakcolumn1Tbl1);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakMainTbl.AddCell(cell);



                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(preBreakcolumn1Tbl2);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                preBreakMainTbl.AddCell(cell);

                int[] authtblWidth = { orga4Width / 4 };
                PdfPTable authTbl = new PdfPTable(1);
                authTbl.SetWidths(authtblWidth);

                if (qAuth == 0)
                {
                    cell = new PdfPCell(new Phrase("Premium is subject to confirmation by an Authorized officer", AuthorFont));
                    cell.Border = Rectangle.BOX;
                    cell.BorderColor = new Color(43, 137, 174);
                    cell.VerticalAlignment = Element.ALIGN_MIDDLE;
                    cell.FixedHeight = 0;
                    cell.PaddingTop = 5;
                    cell.PaddingBottom = 5;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    authTbl.AddCell(cell);

                }
                else
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    authTbl.AddCell(cell);
                }

                cell = new PdfPCell();
                cell.PaddingTop = 20;
                cell.AddElement(authTbl);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                preBreakMainTbl.AddCell(cell);



                #endregion

                #region Cover Extension table

                PdfPTable coversTable = new PdfPTable(1);
                coversTable.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase(adCovers, NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 3;
                //cell.FixedHeight = 40f;
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                coversTable.AddCell(cell);

                if (string.IsNullOrEmpty(note))
                {
                    // Add an empty cell
                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    preBreakcolumn1Tbl2.AddCell(cell);

                    cell = new PdfPCell();
                    cell.Border = PdfPCell.NO_BORDER;
                    coversTable.AddCell(cell);

                }
                else
                {
                    cell = new PdfPCell(new Phrase("Note: " + note, NormalFont8i));
                    cell.Border = Rectangle.NO_BORDER;
                    cell.PaddingTop = 3;
                    cell.PaddingLeft = -5;
                    cell.Border = Rectangle.NO_BORDER;
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    coversTable.AddCell(cell);



                }


                PdfPTable CoverExtensionTbl = new PdfPTable(1);
                CoverExtensionTbl.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 5f;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                CoverExtensionTbl.AddCell(cell);


                cell = new PdfPCell(new Phrase("Cover Extension :", NormalFont8));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 3;
                cell.PaddingLeft = 2;
                //cell.FixedHeight = 20f;
                cell.BackgroundColor = new Color(232, 231, 231);
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;


                CoverExtensionTbl.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(coversTable);
                cell.PaddingLeft = -25;
                cell.HorizontalAlignment = Element.ALIGN_TOP;
                cell.VerticalAlignment = Element.ALIGN_LEFT;
                CoverExtensionTbl.AddCell(cell);

                #endregion

                #region benefits table
                //table 1


                PdfPTable benSubTbl1 = new PdfPTable(1);
                benSubTbl1.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("Speedy Settlement", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl1.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list1);
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                //cell.AddElement(image);
                benSubTbl1.AddCell(cell);

                cell = new PdfPCell(new Phrase("Comprehensive Coverage", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl1.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list2);
                //cell.AddElement(image);
                benSubTbl1.AddCell(cell);


                //table 2
                PdfPTable benSubTbl2 = new PdfPTable(1);
                benSubTbl2.SetWidths(clmnTablWidth);

                cell = new PdfPCell(new Phrase("Accuracy Guaranteed", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list3);
                //cell.AddElement(image);
                benSubTbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Technology", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list4);
                //cell.AddElement(image);
                benSubTbl2.AddCell(cell);

                cell = new PdfPCell(new Phrase("Convenience", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list5);
                benSubTbl2.AddCell(cell);


                //---images table strats
                int[] imgTblWidth = { orga4Width / 2, orga4Width / 2 };
                var imgTable = new PdfPTable(2);
                imgTable.SetWidths(imgTblWidth);

                cell = new PdfPCell(new Phrase("Authorized Vehicle Agents", ConvenienceImgFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingLeft = 20;
                cell.PaddingRight = -25;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                imgTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Partner Garages", ConvenienceImgFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 10;
                cell.PaddingLeft = 10;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                imgTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 20;
                cell.PaddingRight = -25;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(patnersLogo);
                imgTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("", SubTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 5;
                cell.PaddingLeft = 50;
                cell.PaddingRight = 40;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(patnersQR);
                imgTable.AddCell(cell);

                //---images table ends
                cell = new PdfPCell(imgTable);
                cell.Border = Rectangle.NO_BORDER;
                benSubTbl2.AddCell(cell);


                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 0;
                cell.PaddingLeft = 10;
                benSubTbl2.AddCell(cell);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(list6);
                benSubTbl2.AddCell(cell);

                //cell = new PdfPCell();
                //cell.AddElement(bankCard);
                //cell.Border = Rectangle.NO_BORDER;
                //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //cell.PaddingTop = -10;
                //cell.PaddingLeft = 102;
                //cell.PaddingRight = 100;
                //benSubTbl2.AddCell(cell);

                //main table
                PdfPTable benMainTbl = new PdfPTable(2);
                benMainTbl.SetWidths(mainTablWidth);


                cell = new PdfPCell(new Phrase("Value Added Services", BenTileFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 2;
                cell.PaddingTop = 3;
                cell.PaddingBottom = 3;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                benMainTbl.AddCell(cell);

                cell = new PdfPCell(benSubTbl1);
                cell.Border = Rectangle.NO_BORDER;
                benMainTbl.AddCell(cell);

                cell = new PdfPCell(benSubTbl2);
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingBottom = 10;
                benMainTbl.AddCell(cell);

                #endregion

                #region footerImpNotice table

                int[] xSize = { orga4Width / 3 };
                PdfPTable ImpNotTbl = new PdfPTable(1);
                ImpNotTbl.SetWidths(xSize);

                cell = new PdfPCell(new Phrase("", FooterImportantNoticeFont));
                cell.Border = Rectangle.NO_BORDER;
                //cell.BackgroundColor = new Color(242, 102, 49);
                //cell.PaddingTop = 5;
                cell.PaddingLeft = 35;
                cell.AddElement(ImprtNotice);
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                ImpNotTbl.AddCell(cell);

                PdfPTable ImportNoticeTbl1 = new PdfPTable(3);
                ImportNoticeTbl1.SetWidths(imporyNoticeMainTablWidth);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.Colspan = 3;
                cell.PaddingTop = 5;
                ImportNoticeTbl1.AddCell(cell);


                cell = new PdfPCell(new Phrase("*Conditions apply", tcFont));
                cell.Border = Rectangle.NO_BORDER;
                cell.FixedHeight = 100;
                cell.Rotation = 0;
                cell.PaddingTop = -20;
                cell.PaddingBottom = 0;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                ImportNoticeTbl1.AddCell(cell);


                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.AddElement(ImpNotTbl);
                cell.PaddingLeft = -70;
                cell.PaddingBottom = 20;
                cell.VerticalAlignment = Element.ALIGN_TOP;
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                ImportNoticeTbl1.AddCell(cell);

                cell = new PdfPCell();
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = 0;
                cell.PaddingBottom = 3;
                cell.AddElement(impNoteList);
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                ImportNoticeTbl1.AddCell(cell);

                #endregion


                #region footerUserData table

                int[] userDataSize = { 200, 200, 200, 250 };
                PdfPTable UserDataTbl = new PdfPTable(4);
                UserDataTbl.SetWidths(userDataSize);

                cell = new PdfPCell(new Phrase("Issued By : " + UsrId, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);

                string printDate = "";

                if (qAuth == 0)
                {
                    printDate = "Print Date : ";
                }
                else
                {
                    printDate = "Reprint Date : ";
                }

                cell = new PdfPCell(new Phrase(printDate + pdfDate, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);

                var ipAddr = new DeviceFinder();

                cell = new PdfPCell(new Phrase("IP address : " + ipAddr.GetDeviceIPAddress(), motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);


                cell = new PdfPCell(new Phrase(epfAgentCode, motorCom));
                cell.Border = Rectangle.NO_BORDER;
                cell.PaddingTop = -5;
                cell.PaddingBottom = 5;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                UserDataTbl.AddCell(cell);


                #endregion

                #region footer table

                int[] footerTblSize = { orga4Width / 1, orga4Width / 4 };
                int[] footerAddSize = { orga4Width / 1 };
                int[] footerAddverSize = { orga4Width / 4 };

                //footer address table

                var footerAddtbl = new PdfPTable(1);
                footerAddtbl.SetWidths(footerAddSize);

                cell = new PdfPCell(new Phrase("No.21, Vauxhall Street, Colombo 02, Sri Lanka. Telephone : +94 11 2357 000, +94 11 2357 357, Fax : +94 112 447742", footerAddfont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("www.srilankainsurance.com", footerAddfont));
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                cell = new PdfPCell(new Phrase("Company Registration Number - PB289", footerAddfont));
                cell.PaddingBottom = 10;
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_CENTER;
                footerAddtbl.AddCell(cell);

                //footer advertisement table

                var footerAddver = new PdfPTable(1);
                footerAddver.SetWidths(footerAddverSize);

                cell = new PdfPCell();
                cell.Padding = 0;
                cell.AddElement(aaa);
                cell.PaddingBottom = 0;
                cell.Border = Rectangle.NO_BORDER;
                cell.VerticalAlignment = Element.ALIGN_BOTTOM;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                footerAddver.AddCell(cell);



                //footer main table
                var footerTbl = new PdfPTable(2);
                footerTbl.SetWidths(footerTblSize);

                cell = new PdfPCell(footerAddtbl);
                cell.Border = Rectangle.NO_BORDER;
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.BackgroundColor = new Color(43, 137, 174);
                footerTbl.AddCell(cell);

                cell = new PdfPCell(footerAddver);
                cell.Border = Rectangle.NO_BORDER;
                cell.BackgroundColor = new Color(43, 137, 174);
                footerTbl.AddCell(cell);

                #endregion

                document.Add(headerMainTbl);
                document.Add(headerTitleTbl);
                document.Add(topContentTbl);
                document.Add(headerHrLineTbl);
                document.Add(preBreakMainTbl);
                document.Add(CoverExtensionTbl);

                document.Add(benMainTbl);
                document.Add(footerHrLineTbl);
                document.Add(ImportNoticeTbl1);

                document.Add(UserDataTbl);

                document.Add(footerTbl);
                document.Close();



                byte[] bytes = memoryStream.ToArray();
                memoryStream.Close();

                ms = new MemoryStream(bytes);
                rtn = true;
               


            }
        }


        return rtn;
    }
}