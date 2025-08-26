using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text.pdf;
using iTextSharp.text;

/// <summary>
/// Summary description for MyPageEventHandler
/// </summary>
public class MyPageEventHandler : PdfPageEventHelper
{
    public Image ImageHeader { get; set; }
    int pageNo = 0;
    Font GreyFont = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL, new Color(120, 120, 120));

    public override void OnEndPage(PdfWriter writer, Document document)
    {
        pageNo++;

        document.Add(ImageHeader);
        PdfContentByte cb = writer.DirectContent;
        ColumnText ct = new ColumnText(cb);
        ct.SetSimpleColumn(new Phrase(new Chunk("Page No. " + pageNo.ToString(), GreyFont)), 510, 14, 600, 45, 25, Element.ALIGN_RIGHT | Element.ALIGN_BOTTOM);
        ct.Go();

    }


}