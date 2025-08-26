using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using System.IO;
using iTextSharp.text.pdf;
using System.Data;

/// <summary>
/// Summary description for ODP_Print_PayAdvices
/// </summary>
public class ODP_Print_PayAdvices
{

    public void print_PaySlip(string refId)
    {
        ODP_Transaction getRecord = new ODP_Transaction();
        ODP_DQL oDP_DQL = new ODP_DQL();
        try
        {
            List<ODP_PrtAdvice> print_PAprofile = getRecord.GetPaymentAdviceInfo(oDP_DQL.GetPayAdvice(refId));

            if (getRecord.Trans_Sucess_State == true)
            {
                if (print_PAprofile.Count > 0)
                {
                    Document document = new Document(PageSize.A4, 0, 0, 25, 10);

                    MemoryStream output = new MemoryStream();
                    PdfWriter writer = PdfWriter.GetInstance(document, output);
                    string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

                    Phrase phrase = new Phrase(DateTime.Now.ToString() + "  " + ip + "  " + "7010##", new Font(Font.COURIER, 8));

                    HeaderFooter header = new HeaderFooter(phrase, false);
                    header.Border = Rectangle.NO_BORDER;
                    // center header
                    header.Alignment = 1;

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
                  
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/OdProtect/images/slic_gen_Logo.png"));
                    logo.ScalePercent(40f);
                    logo.SetAbsolutePosition(220, 720);
                    document.Add(logo);

                    iTextSharp.text.Image watermark = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/slic_logo2.png"));
                    watermark.ScalePercent(50f);
                    watermark.SetAbsolutePosition(100, 350);
                    document.Add(watermark);

                    document.Add(new Paragraph(""));
                    document.Add(new Paragraph("\n\n\n\n\n\n"));
                    Paragraph titleLine = new Paragraph(new string(' ', 10) +"Payment Advice", titleFont1);                  
                    titleLine.SetAlignment("Left");                 
                    document.Add(titleLine);


                    int[] clmwidths10 = { 8, 100, 8 };
                    PdfPTable tbl10 = new PdfPTable(3);
                    tbl10.SetWidths(clmwidths10);

                    tbl10.WidthPercentage = 100;
                    tbl10.HorizontalAlignment = Element.ALIGN_CENTER;
                    tbl10.SpacingBefore = 20;
                    tbl10.SpacingAfter = 10;
                    tbl10.DefaultCell.Border = 0;


                    int[] clmwidths111 = { 25, 47, 3 };
                    PdfPTable tbl14 = new PdfPTable(3);
                    tbl14.SetWidths(clmwidths111);

                    tbl14.WidthPercentage = 60;
                    tbl14.HorizontalAlignment = Element.ALIGN_LEFT;
                    tbl14.SpacingBefore = 35;
                    tbl14.SpacingAfter = 10;
                    tbl14.DefaultCell.Border = 0;



                    tbl14.AddCell(new Phrase("Policy Number", bodyFont2));
                    PdfPCell cell = new PdfPCell(new Phrase(print_PAprofile[0].out_polno, bodyFont2));
                    cell.HorizontalAlignment = 0;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    tbl14.AddCell(cell);

                    tbl14.AddCell(new Phrase("Name of the Insured", bodyFont2));
                    cell = new PdfPCell(new Phrase(print_PAprofile[0].out_cus_name, bodyFont2));
                    cell.HorizontalAlignment = 0;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    tbl14.AddCell(cell);
                   
                    tbl14.AddCell(new Phrase("NIC No.", bodyFont2));
                    cell = new PdfPCell(new Phrase(print_PAprofile[0].out_nic, bodyFont2));
                    cell.HorizontalAlignment = 0;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    tbl14.AddCell(cell);
                   
                    tbl14.AddCell(new Phrase("Bank Name", bodyFont2));
                    cell = new PdfPCell(new Phrase(print_PAprofile[0].out_bbnam, bodyFont2));
                    cell.HorizontalAlignment = 0;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    tbl14.AddCell(cell);

                    tbl14.AddCell(new Phrase("Branch Name", bodyFont2));
                    cell = new PdfPCell(new Phrase(print_PAprofile[0].out_bbrnch, bodyFont2));
                    cell.HorizontalAlignment = 0;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    tbl14.AddCell(cell);

                    tbl14.AddCell(new Phrase("Period of Insurance", bodyFont2));
                    cell = new PdfPCell(new Phrase("From: " + print_PAprofile[0].out_policy_sdate+ " To: " + print_PAprofile[0].out_policy_edate, bodyFont2));
                    cell.HorizontalAlignment = 0;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    tbl14.AddCell(cell);

                    tbl14.AddCell(new Phrase("Sum Insured (Rs.)", bodyFont2));
                    cell = new PdfPCell(new Phrase(print_PAprofile[0].out_suminsurd.ToString("N2"), bodyFont2));
                    cell.HorizontalAlignment = 0;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    tbl14.AddCell(cell);

                    tbl14.AddCell(new Phrase("Premium (RS.)", bodyFont2));
                    cell = new PdfPCell(new Phrase(print_PAprofile[0].out_tot_premium.ToString("N2"), bodyFont2));
                    cell.HorizontalAlignment = 0;
                    cell.Colspan = 2;
                    cell.Border = 0;
                    tbl14.AddCell(cell);
                  
                    string wordings102 = "";                  
                    PdfPCell cell123 = new PdfPCell();
                
                    Phrase ph = new Phrase();
                    Chunk chh1 = new Chunk("\nPlease credit Rs. ", bodyFont2);
                    Chunk chh2 = new Chunk(print_PAprofile[0].out_tot_premium.ToString("N2") + "", bodyFont2_bold);
                    chh2.SetUnderline(0.5f, -1.5f);
                    Chunk chh3 = new Chunk(" being the premium payable, to account number ", bodyFont2);
                    Chunk chh4 = new Chunk("82022680", bodyFont2_bold);
                    chh4.SetUnderline(0.5f, -1.5f);
                    Chunk chh5 = new Chunk(" along with the remark of ", bodyFont2);
                    Chunk chh6 = new Chunk(print_PAprofile[0].out_polno + ".", bodyFont2_bold);
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

                    //if (dh_bcode_id == "7010")
                    //{
                        BAsissc = iTextSharp.text.Image.GetInstance(System.Web.HttpContext.Current.Server.MapPath("~/Images/BOC_voucher.png"));
                    //}
                   
                    BAsissc.SetAbsolutePosition(0, 0);
                    BAsissc.ScalePercent(34.770f);
                    document.Add(BAsissc);
                    document.Add(new Phrase("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n", linebreak));
                    //document.Add(new Phrase("                                                     " + pro.Customer_NIC.Trim().ToUpper() + "S" + proposalNo.Trim().Substring(10, 7), bodyFont2));
                    //document.Add(new Phrase("                                                     " + pro.Customer_NIC.Trim().ToUpper() + "    " + proposalNo.Trim(), bodyFont2));
                    document.Add(new Phrase("                                                     " + "    " + print_PAprofile[0].out_polno, bodyFont2));

                    document.Add(new Phrase("\n\n\n\n\n\n\n\n\n", linebreak));
                    document.Add(new Phrase("                                                                                                                                                                                     Rs.  " + print_PAprofile[0].out_tot_premium.ToString("N2") + "", bodyFont2));

                document.Close();

                    System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("inline;filename=ODP_{0}.pdf", "Payment_Advise"));
                    System.Web.HttpContext.Current.Response.BinaryWrite(output.ToArray());

                }
                else
                {
                    /*User profile not registed on e_document System*/
                    string msg_heading = "Error: Message";
                    string message = "Record not found.";
                    //HttpContext.Current.Response.Redirect("UserPage.aspx");
                    // Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
                }
            }

            else
            {
                /*Any Oracle Transaction Error*/
                string msg_heading = "Error: In User Profile.";
                string message = "User Does Not Register On eDocument System.Please Contact System Administrator To Register.";
                //HttpContext.Current.Response.Redirect("UserPage.aspx");
                //Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
            }
        }

        catch (Exception ex)
        {
            string msg_heading = "Error: In User Profile.";
            string message = ex.Message;
            //HttpContext.Current.Response.Redirect("UserPage.aspx");
            // Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
        }   
    }

}