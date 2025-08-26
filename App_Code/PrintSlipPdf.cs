using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Data;

/// <summary>PrintSlipPdf.cs
/// Summary description for PrintPdf
/// </summary>
public class PrintSlipPdf
{
    GetProposalDetails getPropClass = new GetProposalDetails();
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();

    string agentCode, agentName, BGI, BANK_ACC, BANK_EMAIL = string.Empty;

    string SC_POLICY_NO, SC_SUM_INSU, SC_NET_PRE,
            SC_RCC, SC_TR, SC_ADMIN_FEE,
            SC_POLICY_FEE, SC_NBT, SC_VAT, SC_TOTAL_PAY, CREATED_ON, CREATED_BY,
            FLAG, SC_Renewal_FEE, BPF_FEE, DEBIT_NO = string.Empty;



    string dh_bcode, dh_bbrcode, dh_name, dh_agecode, dh_agename, dh_nic, dh_br,
        dh_padd1, dh_padd2, dh_padd3, dh_padd4, dh_phone, dh_email, dh_iadd1, dh_iadd2, dh_iadd3,
        dh_iadd4, dh_pfrom, dh_pto, dh_uconstr, dh_occu_car, dh_occ_yes_reas, dh_haz_occu, dh_haz_yes_rea,
        dh_valu_build, dh_valu_wall,
        dh_valu_total, dh_aff_flood, dh_aff_yes_reas, dh_wbrick,
        dh_wcement, dh_dwooden, dh_dmetal, dh_ftile, dh_fcement, dh_rtile,
        dh_rasbes, dh_rgi, dh_rconcreat, dh_cov_fire,
        dh_cov_light, dh_cov_flood, dh_cfwateravl, dh_cfyesr1, dh_cfyesr2, dh_cfyesr3, dh_cfyesr4,
        dh_entered_by, dh_entered_on, dh_hold, DH_NO_OF_FLOORS, DH_OVER_VAL, DH_FINAL_FLAG,
       dh_isreq, dh_conditions, dh_isreject, dh_iscodi, dh_bcode_id, dh_bbrcode_id, DH_LOADING, DH_LOADING_VAL, LAND_PHONE, DH_VAL_BANKFAC,dh_deductible, dh_deductible_pre,
         TERM, Period, Fire_cover, Other_cover, SRCC_cover, TC_cover, Flood_cover,
        BANK_UPDATED_BY, BANK_UPDATED_ON, PROP_TYPE, DH_SOLAR_SUM, SOLAR_REPAIRE,
        SOLAR_PARTS, SOLAR_ORIGIN, SOLAR_SERIAL, Solar_Period, LOAN_NUMBER = string.Empty;

    public PrintSlipPdf()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void print_PaySlip(string refId)
    {


        this.getPropClass.GetSchedualCalValues(refId, out SC_POLICY_NO, out SC_SUM_INSU, out SC_NET_PRE,
               out SC_RCC, out SC_TR, out SC_ADMIN_FEE,
               out SC_POLICY_FEE, out SC_NBT, out SC_VAT, out SC_TOTAL_PAY, out CREATED_ON, out CREATED_BY,
               out FLAG, out SC_Renewal_FEE, out BPF_FEE, out DEBIT_NO);

        //------------------------->>-------------policy details-------------------->>>

        this.getPropClass.GetEnteredDetails(refId, out dh_bcode, out dh_bbrcode, out dh_name, out dh_agecode, out dh_agename, out dh_nic, out dh_br,
out dh_padd1, out dh_padd2, out dh_padd3, out dh_padd4, out dh_phone, out dh_email, out dh_iadd1, out dh_iadd2, out dh_iadd3,
out dh_iadd4, out dh_pfrom, out dh_pto, out dh_uconstr, out dh_occu_car, out dh_occ_yes_reas, out dh_haz_occu, out dh_haz_yes_rea,
out dh_valu_build, out dh_valu_wall,
out dh_valu_total, out dh_aff_flood, out dh_aff_yes_reas, out dh_wbrick,
out dh_wcement, out dh_dwooden, out dh_dmetal, out dh_ftile, out dh_fcement, out dh_rtile,
out dh_rasbes, out dh_rgi, out dh_rconcreat, out dh_cov_fire,
out dh_cov_light, out dh_cov_flood, out dh_cfwateravl, out dh_cfyesr1, out dh_cfyesr2, out dh_cfyesr3, out dh_cfyesr4,
out dh_entered_by, out dh_entered_on, out dh_hold, out DH_NO_OF_FLOORS, out DH_OVER_VAL, out DH_FINAL_FLAG,
out dh_isreq, out dh_conditions, out dh_isreject, out dh_iscodi, out dh_bcode_id, out dh_bbrcode_id, out DH_LOADING, out DH_LOADING_VAL, out LAND_PHONE, out DH_VAL_BANKFAC, out dh_deductible, out dh_deductible_pre,
out TERM, out Period, out Fire_cover, out Other_cover, out SRCC_cover, out TC_cover, out Flood_cover,
out BANK_UPDATED_BY, out BANK_UPDATED_ON, out PROP_TYPE, out DH_SOLAR_SUM, out SOLAR_REPAIRE,
    out SOLAR_PARTS, out SOLAR_ORIGIN, out SOLAR_SERIAL, out Solar_Period, out LOAN_NUMBER);





        Document document = new Document(PageSize.A4, 0, 0, 25, 10);

        MemoryStream output = new MemoryStream();
        PdfWriter writer = PdfWriter.GetInstance(document, output);
        string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];



        Phrase phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + dh_entered_by, new Font(Font.COURIER, 8));


        HeaderFooter header = new HeaderFooter(phrase, false);
        // top & bottom borders on by default 
        header.Border = Rectangle.NO_BORDER;
        // center header
        header.Alignment = 1;
        /*
         * HeaderFooter => add header __before__ opening document
         */
        if (dh_bcode_id != "7010##")
        {
            document.Footer = header;
        }


        document.Open();


        Font titleFont1 = FontFactory.GetFont("Times New Roman", 12, Font.BOLD, new Color(0, 0, 0));
        Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(255, 255, 255));
        Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font boldTableFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
        Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
        Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
        Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);

        Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
        Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
        Font bodyFont4_bold = FontFactory.GetFont("Times New Roman", 8, Font.BOLD, new Color(255, 0, 0));

        Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

        Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
        Font bodyFont2_bold_und = FontFactory.GetFont("Times New Roman", 9, Font.BOLD | Font.UNDERLINE, new Color(0, 0, 0));

        //bool policies_exists = chck.Existing_Policies_sub_NIC(pro.Customer_NIC, proposalNo, pro.bankcode);


        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/GeneralLogo2.png"));
        logo.ScalePercent(47f, 53f);
        logo.SetAbsolutePosition(40, 720);
        document.Add(logo);

        iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/New_Company_watermark.png"));
        watermark.ScalePercent(50f);
        watermark.SetAbsolutePosition(100, 350);
        document.Add(watermark);



        document.Add(new Paragraph(""));
        document.Add(new Paragraph("\n\n\n\n\n\n"));
        Paragraph titleLine = new Paragraph("Payment Advice", titleFont1);
        titleLine.SetAlignment("Center");
        document.Add(titleLine);


        int[] clmwidths10 = { 8, 100, 8 };

        PdfPTable tbl10 = new PdfPTable(3);

        tbl10.SetWidths(clmwidths10);

        tbl10.WidthPercentage = 100;
        tbl10.HorizontalAlignment = Element.ALIGN_CENTER;
        tbl10.SpacingBefore = 20;
        tbl10.SpacingAfter = 10;
        tbl10.DefaultCell.Border = 0;

        //document.Add(new Paragraph("\n\n\n\n"));
        int[] clmwidths111 = { 25, 47, 3 };

        PdfPTable tbl14 = new PdfPTable(3);

        tbl14.SetWidths(clmwidths111);

        tbl14.WidthPercentage = 60;
        tbl14.HorizontalAlignment = Element.ALIGN_LEFT;
        tbl14.SpacingBefore = 35;
        tbl14.SpacingAfter = 10;
        tbl14.DefaultCell.Border = 0;



        tbl14.AddCell(new Phrase("Policy Number", bodyFont2));
        PdfPCell cell = new PdfPCell(new Phrase(SC_POLICY_NO, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.Border = 0;
        tbl14.AddCell(cell);

        //cell = new PdfPCell(new Phrase("   ", bodyFont2));
        //cell.HorizontalAlignment = 0;
        //cell.Colspan = 3;
        //cell.Border = 0;
        //tbl14.AddCell(cell);

        tbl14.AddCell(new Phrase("Name of the Insured", bodyFont2));
        cell = new PdfPCell(new Phrase(dh_name, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.Border = 0;
        tbl14.AddCell(cell);

        //cell = new PdfPCell(new Phrase("   ", bodyFont2));
        //cell.HorizontalAlignment = 0;
        //cell.Colspan = 3;
        //cell.Border = 0;
        //tbl14.AddCell(cell);
        
            if (!string.IsNullOrEmpty(dh_nic)) {
            tbl14.AddCell(new Phrase("NIC No.", bodyFont2));
            cell = new PdfPCell(new Phrase(dh_nic, bodyFont2));
            cell.HorizontalAlignment = 0;
            cell.Colspan = 2;
            cell.Border = 0;
            tbl14.AddCell(cell);
        }
        else
        {
            tbl14.AddCell(new Phrase("BR No.", bodyFont2));
            cell = new PdfPCell(new Phrase(dh_br, bodyFont2));
            cell.HorizontalAlignment = 0;
            cell.Colspan = 2;
            cell.Border = 0;
            tbl14.AddCell(cell);
        }
        //cell = new PdfPCell(new Phrase("   ", bodyFont2));
        //cell.HorizontalAlignment = 0;
        //cell.Colspan = 3;
        //cell.Border = 0;
        //tbl14.AddCell(cell);
       
        tbl14.AddCell(new Phrase("Bank Name", bodyFont2));
        cell = new PdfPCell(new Phrase(dh_bcode, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.Border = 0;
        tbl14.AddCell(cell);

        //cell = new PdfPCell(new Phrase("   ", bodyFont2));
        //cell.HorizontalAlignment = 0;
        //cell.Colspan = 3;
        //cell.Border = 0;
        //tbl14.AddCell(cell);

        tbl14.AddCell(new Phrase("Branch Name", bodyFont2));
        cell = new PdfPCell(new Phrase(dh_bbrcode, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.Border = 0;
        tbl14.AddCell(cell);

        //cell = new PdfPCell(new Phrase("   ", bodyFont2));
        //cell.HorizontalAlignment = 0;
        //cell.Colspan = 3;
        //cell.Border = 0;
        //tbl14.AddCell(cell);

        tbl14.AddCell(new Phrase("Period of Insurance", bodyFont2));
        cell = new PdfPCell(new Phrase("From: "+dh_pfrom+" To: "+dh_pto, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.Border = 0;
        tbl14.AddCell(cell);

        //cell = new PdfPCell(new Phrase("   ", bodyFont2));
        //cell.HorizontalAlignment = 0;
        //cell.Colspan = 3;
        //cell.Border = 0;
        //tbl14.AddCell(cell);

        tbl14.AddCell(new Phrase("Sum Insured (Rs.)", bodyFont2));
        cell = new PdfPCell(new Phrase(dh_valu_total, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.Border = 0;
        tbl14.AddCell(cell);

        //cell = new PdfPCell(new Phrase("   ", bodyFont2));
        //cell.HorizontalAlignment = 0;
        //cell.Colspan = 3;
        //cell.Border = 0;
        //tbl14.AddCell(cell);

        tbl14.AddCell(new Phrase("Premium (RS.)", bodyFont2));
        cell = new PdfPCell(new Phrase(SC_TOTAL_PAY, bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 2;
        cell.Border = 0;
        tbl14.AddCell(cell);

        //this.GetAgentDetails(dh_bcode_id);
        this.GetAgentDetails(dh_agecode);
        //String wording001 = "";
        string wordings102 = "";
        bool show_boc_word = false;
        //decimal to_pay = 0;
        //decimal cash_in_hand = 0;

        PdfPCell cell123 = new PdfPCell();
          
                // wordings102 = "\nPlease credit Rs. " + (pro.tot_premiun - cash_in_hand).ToString("N0") + "/= being the premium payable.";
                wordings102 = "\nPlease credit Rs. " + SC_TOTAL_PAY + "/= being the premium payable.";
                wordings102 = "";
                show_boc_word = true;
                Phrase ph = new Phrase();
                Chunk chh1 = new Chunk("\nPlease credit Rs. ", bodyFont2);
                Chunk chh2 = new Chunk(SC_TOTAL_PAY + "", bodyFont2_bold);
                chh2.SetUnderline(0.5f, -1.5f);
                Chunk chh3 = new Chunk(" being the premium payable, to account number ", bodyFont2);
                Chunk chh4 = new Chunk(BANK_ACC, bodyFont2_bold);
                chh4.SetUnderline(0.5f, -1.5f);
                Chunk chh5 = new Chunk(" along with the remark of ", bodyFont2);
                Chunk chh6 = new Chunk(SC_POLICY_NO + ".", bodyFont2_bold);
                chh6.SetUnderline(0.5f, -1.5f);


                ph.Add(chh1);
                ph.Add(chh2);
                ph.Add(chh3);
                ph.Add(chh4);
                ph.Add(chh5);
                ph.Add(chh6);

                cell123 = new PdfPCell(ph);
                cell123.HorizontalAlignment = 0;
                cell123.Border = 0;
                cell123.Colspan = 3;
                tbl14.AddCell(cell123);





    


        //writer.CloseStream = false;
        cell = new PdfPCell(new Phrase("   ", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 3;
        cell.Border = 0;
        tbl14.AddCell(cell);

        cell = new PdfPCell(new Phrase("   ", bodyFont2));
        cell.HorizontalAlignment = 0;
        cell.Colspan = 3;
        cell.Border = 0;
        tbl14.AddCell(cell);

       
            cell = new PdfPCell(phrase);
            cell.HorizontalAlignment = 1;
            cell.Colspan = 3;
            cell.Border = 0;
            tbl14.AddCell(cell);
     

        tbl10.AddCell(new Phrase(" ", bodyFont2));

        PdfPCell bigCell = new PdfPCell(tbl14);
        bigCell.HorizontalAlignment = 2;
        bigCell.VerticalAlignment = Element.ALIGN_MIDDLE;
        bigCell.BorderWidth = 0f;
        bigCell.BorderWidthRight = 0f;
        bigCell.BorderWidthLeft = 0f;
        bigCell.Padding = 0;

        tbl10.AddCell(bigCell);
        tbl10.AddCell(new Phrase(" ", bodyFont2));

        document.Add(tbl10);


        document.Add(new Phrase("\n"));

        iTextSharp.text.Image BAsissc = null;

        if (dh_bcode_id == "7010")
        {

            BAsissc = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/BOC_voucher.png"));

        }
        else if (dh_bcode_id == "7135")
        {
            BAsissc = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/people_voucher.png"));
        }
        else if (dh_bcode_id == "7755")
        {
            BAsissc = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/RDB_voucher.png"));
        }
        else if (dh_bcode_id == "7719")
        {
            BAsissc = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/NSB_voucher.png"));
        }
        else { }

            BAsissc.SetAbsolutePosition(0, 0);
            BAsissc.ScalePercent(34.770f);
            document.Add(BAsissc);
            document.Add(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n", linebreak));
            //document.Add(new Phrase("                                                     " + pro.Customer_NIC.Trim().ToUpper() + "S" + proposalNo.Trim().Substring(10, 7), bodyFont2));
            //document.Add(new Phrase("                                                     " + pro.Customer_NIC.Trim().ToUpper() + "    " + proposalNo.Trim(), bodyFont2));
            document.Add(new Phrase("                                                     " + "    " + SC_POLICY_NO.Trim(), bodyFont2));

            document.Add(new Phrase("\n\n\n\n\n\n\n\n\n", linebreak));
            document.Add(new Phrase("                                                                                                                                                                                     Rs.  " + SC_TOTAL_PAY + "", bodyFont2));

        //}

        document.Close();

        //output.Position = 0;

        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("inline;filename=Fire_Policy_{0}.pdf", "payment"));
        //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=Fire_Policy_{0}.pdf", "payment"));
        System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());
    }


    //public void print_receipt(string username, bool reprint, string proposalno, string payRefno, string nic, string str_pemium, string str_paid, string cross_refNo, string transactionID)
    //{
    //    Document document = new Document(PageSize.A4, 50, 50, 50, 50);
    //    //Document document = new Document(PageSize.A4, 85, 25, 15, 45);

    //    MemoryStream output = new MemoryStream();
    //    PdfWriter writer = PdfWriter.GetInstance(document, output);
    //    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

    //    string re = "";
    //    re = (reprint ? " REPRINT" : "");

    //    Phrase phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + username + re, new Font(Font.COURIER, 6));


    //    HeaderFooter header = new HeaderFooter(phrase, false);
    //    // top & bottom borders on by default 
    //    header.Border = Rectangle.NO_BORDER;
    //    // center header
    //    header.Alignment = 1;
    //    /*
    //     * HeaderFooter => add header __before__ opening document
    //     */
    //    document.Footer = header;

    //    document.Open();


    //    Font titleFont1 = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(0, 0, 0));
    //    Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(255, 255, 255));
    //    Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
    //    Font boldTableFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
    //    Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
    //    Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
    //    Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
    //    Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
    //    Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
    //    Font bodyFont4_bold = FontFactory.GetFont("Times New Roman", 7, Font.BOLD);

    //    Font bodyFont5 = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL);
    //    Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);


    //    Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
    //    Font bodyFont2_bold_und = FontFactory.GetFont("Times New Roman", 9, Font.BOLD | Font.UNDERLINE, new Color(0, 0, 0));

    //    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo.gif"));
    //    logo.ScalePercent(25f);
    //    logo.SetAbsolutePosition(240, 750);
    //    document.Add(logo);

    //    iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/watermark.gif"));
    //    watermark.SetAbsolutePosition(65, 280);
    //    document.Add(watermark);

    //    iTextSharp.text.Image sign = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/SNSIGN.png"));
    //    sign.ScalePercent(25f);
    //    sign.SetAbsolutePosition(50, 398);
    //    PdfPCell pdfcel = new PdfPCell(sign);
    //    pdfcel.Border = 0;
    //    //document.Add(sign);


    //    document.Add(new Paragraph("\n\n"));

    //    int[] clmwidths10 = { 6, 100, 6 };

    //    PdfPTable tbl10 = new PdfPTable(3);

    //    tbl10.SetWidths(clmwidths10);

    //    tbl10.WidthPercentage = 100;
    //    tbl10.HorizontalAlignment = Element.ALIGN_CENTER;
    //    tbl10.SpacingBefore = 20;
    //    tbl10.SpacingAfter = 10;
    //    tbl10.DefaultCell.Border = 0;



    //    tbl10.AddCell(new Phrase(" ", bodyFont2));
    //    tbl10.AddCell(new Phrase(" ", bodyFont2));
    //    tbl10.AddCell(new Phrase(" ", bodyFont2));



    //    int[] clmwidths = { 20, 1, 20 };

    //    PdfPTable tbl = new PdfPTable(3);

    //    tbl.SetWidths(clmwidths);

    //    tbl.WidthPercentage = 100;
    //    tbl.HorizontalAlignment = Element.ALIGN_LEFT;
    //    tbl.SpacingBefore = 8;
    //    tbl.SpacingAfter = 8;
    //    tbl.DefaultCell.Border = 0;


    //    tbl.AddCell(new Phrase("Payment Slip.", bodyFont2_bold));
    //    tbl.AddCell(new Phrase("", bodyFont2_bold));
    //    tbl.AddCell(new Phrase("", bodyFont2_bold));

    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));

    //    tbl.AddCell(new Phrase("Proposal No.", bodyFont2));
    //    tbl.AddCell(new Phrase(":", bodyFont2));
    //    tbl.AddCell(new Phrase(proposalno, bodyFont2));

    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));

    //    tbl.AddCell(new Phrase("DTA Payment Reference No.", bodyFont2));
    //    tbl.AddCell(new Phrase(":", bodyFont2));
    //    tbl.AddCell(new Phrase(payRefno, bodyFont2));

    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));

    //    tbl.AddCell(new Phrase("NIC", bodyFont2));
    //    tbl.AddCell(new Phrase(":", bodyFont2));
    //    tbl.AddCell(new Phrase(nic, bodyFont2));

    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));

    //    tbl.AddCell(new Phrase("Premium", bodyFont2));
    //    tbl.AddCell(new Phrase(":", bodyFont2));
    //    tbl.AddCell(new Phrase(str_pemium, bodyFont2));

    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));

    //    tbl.AddCell(new Phrase("Amount Received", bodyFont2));
    //    tbl.AddCell(new Phrase(":", bodyFont2));
    //    tbl.AddCell(new Phrase(str_paid, bodyFont2));

    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));

    //    tbl.AddCell(new Phrase("Cross Reference No.", bodyFont2));
    //    tbl.AddCell(new Phrase(":", bodyFont2));
    //    tbl.AddCell(new Phrase(cross_refNo, bodyFont2));

    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));
    //    tbl.AddCell(new Phrase(" ", bodyFont2));

    //    tbl.AddCell(new Phrase("Transaction ID", bodyFont2));
    //    tbl.AddCell(new Phrase(":", bodyFont2));
    //    tbl.AddCell(new Phrase(transactionID, bodyFont2));


    //    tbl10.AddCell(new Phrase(" ", bodyFont2));
    //    PdfPCell bigCell = new PdfPCell(tbl);
    //    bigCell.HorizontalAlignment = 2;
    //    bigCell.VerticalAlignment = Element.ALIGN_MIDDLE;
    //    bigCell.BorderWidth = 0f;
    //    bigCell.BorderWidthRight = 0f;
    //    bigCell.BorderWidthLeft = 0f;
    //    bigCell.Padding = 0;
    //    tbl10.AddCell(bigCell);
    //    tbl10.AddCell(new Phrase(" ", bodyFont2));

    //    document.Add(tbl10);
    //    document.Close();

    //    //output.Position = 0;

    //    System.Web.HttpContext.Current.Response.Buffer = false;

    //    System.Web.HttpContext.Current.Response.Clear();
    //    System.Web.HttpContext.Current.Response.ClearContent();
    //    System.Web.HttpContext.Current.Response.ClearHeaders();


    //    System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
    //    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=DTA_Scheme_{0}.pdf", "Payment"));
    //    System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());

    //    System.Web.HttpContext.Current.Response.End();
    //}

    //public void print_policy_cancel_request(string proposalNo, string username, double loan, double int_rate, int term, string add1, string add2, string add3, string add4, string loanType)
    //{

    //    Proposal pro = new Proposal(proposalNo, true);
    //    Banks bnk = new Banks(pro.branchcode);
    //    Checks chck = new Checks();
    //    Document document = new Document(PageSize.A4, 50, 50, 50, 50);

    //    MemoryStream output = new MemoryStream();
    //    PdfWriter writer = PdfWriter.GetInstance(document, output);
    //    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];



    //    Phrase phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + username, new Font(Font.COURIER, 8));


    //    HeaderFooter header = new HeaderFooter(phrase, false);
    //    // top & bottom borders on by default 
    //    header.Border = Rectangle.NO_BORDER;
    //    // center header
    //    header.Alignment = 1;
    //    /*
    //     * HeaderFooter => add header __before__ opening document
    //     */
    //    document.Footer = header;

    //    document.Open();

    //    Font titleFont1 = FontFactory.GetFont("Times New Roman", 12, Font.BOLD, new Color(0, 0, 0));
    //    Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD, new Color(255, 255, 255));
    //    Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
    //    Font boldTableFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
    //    Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
    //    Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
    //    Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);

    //    Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
    //    Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);

    //    Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);

    //    Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);

    //    Chunk ch1 = new Chunk("SLIC Other Loan Scheme\nCancellation Request Letter", titleFont1);
    //    ch1.SetUnderline(0.5f, -1.5f);

    //    document.Add(new Paragraph(""));
    //    Paragraph titleLine = new Paragraph();
    //    titleLine.Add(ch1);
    //    titleLine.SetAlignment("Center");
    //    document.Add(titleLine);

    //    document.Add(new Paragraph("\n\n"));
    //    document.Add(new Paragraph("Please cancel policy no " + proposalNo + " with effect from " + DateTime.Today.ToString("yyy/MM/dd") + " which is taken to cover loan amount of Rs. " + pro.loan_amount.ToString("N2") + ". Please issue a fresh policy by utilizing the surrender value if any lying under the above policy.", bodyFont));

    //    int[] clmwidths111 = { 1, 3 };

    //    PdfPTable tbl14 = new PdfPTable(2);

    //    tbl14.SetWidths(clmwidths111);

    //    tbl14.WidthPercentage = 100;
    //    tbl14.HorizontalAlignment = Element.ALIGN_LEFT;
    //    tbl14.SpacingBefore = 20;
    //    tbl14.SpacingAfter = 50;
    //    tbl14.DefaultCell.Border = 0;



    //    tbl14.AddCell(new Phrase("Name", bodyFont));
    //    tbl14.AddCell(new Phrase(pro.Customer_cvl_status + " " + pro.Customer_Initials + " " + pro.Customer_LastName, bodyFont));

    //    tbl14.AddCell(new Phrase("NIC", bodyFont));
    //    tbl14.AddCell(new Phrase(pro.Customer_NIC, bodyFont));

    //    tbl14.AddCell(new Phrase("Date of Birth", bodyFont));
    //    tbl14.AddCell(new Phrase(pro.Customer_DOB, bodyFont));

    //    tbl14.AddCell(new Phrase("Address", bodyFont));
    //    tbl14.AddCell(new Phrase(add1, bodyFont));

    //    tbl14.AddCell(new Phrase("", bodyFont));
    //    tbl14.AddCell(new Phrase(add2, bodyFont));

    //    tbl14.AddCell(new Phrase("", bodyFont));
    //    tbl14.AddCell(new Phrase(add3, bodyFont));

    //    tbl14.AddCell(new Phrase("", bodyFont));
    //    tbl14.AddCell(new Phrase(add4, bodyFont));

    //    tbl14.AddCell(new Phrase("New Loan Type", bodyFont));
    //    tbl14.AddCell(new Phrase(loanType, bodyFont));

    //    tbl14.AddCell(new Phrase("New Loan Amount (Rs.)", bodyFont));
    //    tbl14.AddCell(new Phrase(loan.ToString("N"), bodyFont));

    //    tbl14.AddCell(new Phrase("Interest Rate", bodyFont));
    //    tbl14.AddCell(new Phrase(int_rate.ToString("N") + "%", bodyFont));

    //    tbl14.AddCell(new Phrase("Term (Years)", bodyFont));
    //    tbl14.AddCell(new Phrase(term.ToString(), bodyFont));

    //    tbl14.AddCell(new Phrase("New Branch", bodyFont));
    //    tbl14.AddCell(new Phrase(bnk.branchName, bodyFont));


    //    document.Add(tbl14);
    //    //document.Add(new Paragraph(".............................................\n", bodyFont));
    //    //document.Add(new Paragraph("Signature & rubber stamp of\n", bodyFont));
    //    //document.Add(new Paragraph("Manager/Authorized Officer.\n\n\n", bodyFont));


    //    //document.Add(new Paragraph("..................\n", bodyFont));
    //    //document.Add(new Paragraph("Date\n", bodyFont));


    //    document.Add(new Paragraph("\n\n", bodyFont));
    //    if (pro.bankcode == 7010)
    //    {
    //        document.Add(new Paragraph("The new quotation will be sent to the bank's email account by SLIC, after cencelling the requested policy/policies. ", bodyFont));
    //        document.Add(new Paragraph("\nPlease send this document via email", bodyFont));
    //        document.Add(new Paragraph("   * Email : lifegb@srilankainsurance.com", bodyFont));
    //    }
    //    else if (pro.bankcode == 7135)
    //    {
    //        document.Add(new Paragraph("The new quotation will be sent to the bank's email account by SLIC, after cencelling the requested policy/policies. ", bodyFont));
    //        document.Add(new Paragraph("\nPlease send this document via fax or email ", bodyFont));
    //        document.Add(new Paragraph("   * Fax     : 011-2357640", bodyFont));
    //        document.Add(new Paragraph("   * Email  : adgrouplife@srilankainsurance.com", bodyFont));
    //    }
    //    else if (pro.bankcode == 7311)
    //    {
    //        document.Add(new Paragraph("The new quotation will be sent to the bank's email account by SLIC, after cencelling the requested policy/policies. ", bodyFont));
    //        document.Add(new Paragraph("\nPlease send this document via fax or email ", bodyFont));
    //        document.Add(new Paragraph("   * Fax     : 011-2357640", bodyFont));
    //        document.Add(new Paragraph("   * Email  : adpab@srilankainsurance.com", bodyFont));
    //    }
    //    else if (pro.bankcode == 7755)
    //    {
    //        document.Add(new Paragraph("The new quotation will be sent to the bank's email account by SLIC, after cencelling the requested policy/policies. ", bodyFont));
    //        document.Add(new Paragraph("\nPlease send this document via fax or email ", bodyFont));
    //        document.Add(new Paragraph("   * Fax     : 011-2357640", bodyFont));
    //        document.Add(new Paragraph("   * Email  : adgrouplife@srilankainsurance.com", bodyFont));
    //    }
    //    document.Close();

    //    //output.Position = 0;

    //    System.Web.HttpContext.Current.Response.Buffer = false;

    //    System.Web.HttpContext.Current.Response.Clear();
    //    System.Web.HttpContext.Current.Response.ClearContent();
    //    System.Web.HttpContext.Current.Response.ClearHeaders();


    //    System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
    //    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename=DTA_Scheme_{0}.pdf", "calculation"));
    //    System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());

    //    System.Web.HttpContext.Current.Response.End();

    //}

    protected void GetAgentDetails(string agent_code)
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetBankCodebyAgent(agent_code), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {

                    agentCode = details.Rows[0]["BANK_CODE"].ToString();
                    agentName = details.Rows[0]["AGE_NAME"].ToString();
                    BGI = details.Rows[0]["BANCASS_GI"].ToString();
                    BANK_ACC = details.Rows[0]["BANK_ACC"].ToString();
                    BANK_EMAIL = details.Rows[0]["BANK_EMAIL"].ToString();

                }
                else
                {

                    string msg = "Message : No Records found. Dated on " + System.DateTime.Now.ToString();
                    var endc = new EncryptDecrypt();
                }
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
        }
    }


}