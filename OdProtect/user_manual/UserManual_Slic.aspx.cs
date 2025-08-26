using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OdProtect_user_manual_UserManual_Slic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"100%\" height=\"100%\">";
        embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        embed += "</object>";
        ltEmbed.Text = string.Format(embed, ResolveUrl("~/OdProtect/user_manual/manual_pdf/ODP_UserManual.pdf"));
    }
}