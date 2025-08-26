using System;
public partial class OdProtect_Common_PaymentAdvice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ODP_Print_PayAdvices oDP_Print_PayAdvices = new ODP_Print_PayAdvices();
        oDP_Print_PayAdvices.print_PaySlip(Request.QueryString["srid"]);
    }
}