using System;
public partial class OdProtect_Common_PrintPolicyShadule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ODP_PrintPolicyShadule oDP_PrintPolicyShadule = new ODP_PrintPolicyShadule();
        oDP_PrintPolicyShadule.PrintPolicy(Request.QueryString["srid"]);
    }
}