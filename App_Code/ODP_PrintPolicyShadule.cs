using System;
using System.Linq;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

/// <summary>
/// Summary description for ODP_PrintPolicyShadule
/// </summary>
public class ODP_PrintPolicyShadule
{

    public void PrintPolicy(string refId)
    {
        ODP_Transaction getRecord = new ODP_Transaction();
        ODP_DQL oDP_DQL = new ODP_DQL();

        Font fnt_figur = FontFactory.GetFont("Arial", 10, Font.NORMAL, new Color(32, 32, 32));
        Font fnt_summery_1 = FontFactory.GetFont("Arial", 8, Font.NORMAL, new Color(32, 32, 32));
        try
        {
            byte[] bytes = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/OdProtect/Common/Template/od_policy.pdf"));

            System.Collections.Generic.List<ODP_PolicyPrint> printpolicy = getRecord.GetPolicyPrintInfo(oDP_DQL.GetPolicyPrintInfo(refId));

            if (getRecord.Trans_Sucess_State == true)
            {
                if (printpolicy.Count > 0)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        PdfReader reader = new PdfReader(bytes);
                        PdfStamper stamper = new PdfStamper(reader, stream);

                        int pages = reader.NumberOfPages;
                        for (int i = 1; i <= pages; i++)
                        {
                            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(i.ToString(), fnt_figur), 568f, 15f, 0);
                            
                            if(i == 3)
                                ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_CENTER, new Phrase("This is system generated print and therefore requires no authorized signature.", fnt_figur), 300f, 35f, 0);
                        }

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_polno, fnt_figur), 405f, 685f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_cus_name.ToUpper(), fnt_figur), 208f, 553f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_nic, fnt_figur), 208f, 531f, 0);

                        System.Collections.Generic.List<string> address = new System.Collections.Generic.List<string>();
                        address.Add(printpolicy[0].out_add_l1.Trim());
                        address.Add(printpolicy[0].out_add_l2.Trim());
                        address.Add(printpolicy[0].out_add_l3.Trim());
                        address.Add(printpolicy[0].out_add_l4.Trim());
                        string list = string.Join(", ", address.Where(x => !string.IsNullOrEmpty(x)));

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(list + ".", fnt_figur), 208f, 513f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_bbrnch, fnt_summery_1), 208f, 478f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase("Rs."+printpolicy[0].out_suminsurd.ToString("N2"), fnt_summery_1), 208f, 465f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_policy_sdate, fnt_summery_1), 235f, 447f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_policy_edate, fnt_summery_1), 302f, 447f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_policy_rendate, fnt_figur), 138f, 428f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 185f, 409f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase("Rs."+ printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 272f, 374f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase("Rs." + printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 272f, 356f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase("Rs." + printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 272f, 339f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_netpremium.ToString("N2"), fnt_figur), 515f, 397f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_admin_fee.ToString("N2"), fnt_figur), 515f, 384f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_policy_fee.ToString("N2"), fnt_figur), 515f, 371f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_vat.ToString("N2"), fnt_figur), 515f, 358f, 0);

                        double caltot_Premium = (printpolicy[0].out_netpremium + printpolicy[0].out_admin_fee + printpolicy[0].out_policy_fee + printpolicy[0].out_vat);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(caltot_Premium.ToString("N2"), fnt_figur), 515f, 341f, 0);

                        stamper.Close();
                        bytes = stream.ToArray();
                    }
                    System.Web.HttpContext.Current.Response.Clear();
                    System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("inline;filename=OD_{0}.pdf", "Policy_"+ printpolicy[0].out_polno));
                    System.Web.HttpContext.Current.Response.BinaryWrite(bytes);
                    System.Web.HttpContext.Current.Response.End();

                }
                else
                {
                    //Policy Infomation Not Found
                }
            }
            else
            {
                //ORCL Error Ececuite DQL Policy Info
            }
        }
        catch (Exception ex)
        {
            //Application Error
            //HttpContext.Current.Response.Redirect("UserPage.aspx");
            //Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
        }
    }


    public byte[] BOC_Mail(string refId)
    {
        ODP_Transaction getRecord = new ODP_Transaction();
        ODP_DQL oDP_DQL = new ODP_DQL();
        byte[] bytes = null;

        Font fnt_figur = FontFactory.GetFont("Arial", 10, Font.NORMAL, new Color(32, 32, 32));
        Font fnt_summery_1 = FontFactory.GetFont("Arial", 8, Font.NORMAL, new Color(32, 32, 32));
        try
        {
            bytes = File.ReadAllBytes(System.Web.HttpContext.Current.Server.MapPath("~/OdProtect/Common/Template/od_policy.pdf"));

            System.Collections.Generic.List<ODP_PolicyPrint> printpolicy = getRecord.GetPolicyPrintInfo(oDP_DQL.GetPolicyPrintInfo(refId));

            if (getRecord.Trans_Sucess_State == true)
            {
                if (printpolicy.Count > 0)
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        PdfReader reader = new PdfReader(bytes);
                        PdfStamper stamper = new PdfStamper(reader, stream);

                        int pages = reader.NumberOfPages;
                        for (int i = 1; i <= pages; i++)
                        {
                            ColumnText.ShowTextAligned(stamper.GetUnderContent(i), Element.ALIGN_RIGHT, new Phrase(i.ToString(), fnt_figur), 568f, 15f, 0);
                        }

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_polno, fnt_figur), 405f, 685f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_cus_name.ToUpper(), fnt_figur), 208f, 553f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_nic, fnt_figur), 208f, 531f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase("No-23, Kudugala, Ambatenna", fnt_figur), 208f, 513f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_bbrnch, fnt_summery_1), 208f, 478f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase("Rs." + printpolicy[0].out_suminsurd.ToString("N2"), fnt_summery_1), 208f, 465f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_policy_sdate, fnt_summery_1), 235f, 447f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_policy_edate, fnt_summery_1), 302f, 447f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_policy_sdate, fnt_figur), 138f, 428f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_LEFT, new Phrase(printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 185f, 409f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase("Rs." + printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 220f, 374f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase("Rs." + printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 220f, 355f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase("Rs." + printpolicy[0].out_suminsurd.ToString("N2"), fnt_figur), 220f, 339f, 0);

                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_netpremium.ToString("N2"), fnt_figur), 515f, 397f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_admin_fee.ToString("N2"), fnt_figur), 515f, 384f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_policy_fee.ToString("N2"), fnt_figur), 515f, 371f, 0);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(printpolicy[0].out_vat.ToString("N2"), fnt_figur), 515f, 358f, 0);

                        double caltot_Premium = (printpolicy[0].out_netpremium + printpolicy[0].out_admin_fee + printpolicy[0].out_policy_fee + printpolicy[0].out_vat);
                        ColumnText.ShowTextAligned(stamper.GetUnderContent(1), Element.ALIGN_RIGHT, new Phrase(caltot_Premium.ToString("N2"), fnt_figur), 515f, 341f, 0);

                        stamper.Close();
                        bytes = stream.ToArray();
                    }
                    System.Web.HttpContext.Current.Response.Clear();
                   
                    //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                    //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("inline;filename=OD_{0}.pdf", "Policy_" + printpolicy[0].out_polno));
                    //System.Web.HttpContext.Current.Response.BinaryWrite(bytes);
                    //System.Web.HttpContext.Current.Response.End();

                }
                else
                {
                    //Policy Infomation Not Found
                }
            }
            else
            {
                //ORCL Error Ececuite DQL Policy Info
            }
        }
        catch (Exception ex)
        {
            //Application Error
            //HttpContext.Current.Response.Redirect("UserPage.aspx");
            //Response.Redirect("~/ErrorPages/Error_.aspx?mh_=" + msg_heading + "&msg_=" + message, false);
        }

        return bytes;
       
    }

}