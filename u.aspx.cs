using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SLIC_Fire_u : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string key = Request.QueryString["key"];
        if (!string.IsNullOrEmpty(key))
        {
            string longUrl = ShortenerHelper.GetLongUrl(key); // stored earlier
            if (!string.IsNullOrEmpty(longUrl))
            {
                longUrl = longUrl + "&shortKey=" + key;
                Response.Redirect(longUrl, true);
                return;
            }
        }

        Response.Write("Invalid or expired short URL.");
    }
}