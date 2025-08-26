<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;

public class Handler : IHttpHandler, System.Web.SessionState.IReadOnlySessionState {

    public void ProcessRequest (HttpContext context) {

        //string int_userIdentity = HttpContext.Current.Session["EPFNum"].ToString();
        //string int_userIdentity_brc = HttpContext.Current.Session["brcode"].ToString();

        //string out_userIdentity = HttpContext.Current.Session["bank_code"].ToString();
        //string out_userIdentity_brc = HttpContext.Current.Session["temp_branch"].ToString();

        if(!String.IsNullOrEmpty(HttpContext.Current.Session["EPFNum"].ToString()) && !String.IsNullOrEmpty(HttpContext.Current.Session["brcode"].ToString())) {
        context.Response.Redirect("~/OdProtect/BackOffice/OdpHLandingPage.aspx"); 
        }

        if(!String.IsNullOrEmpty(HttpContext.Current.Session["bank_code"].ToString()) && !String.IsNullOrEmpty(HttpContext.Current.Session["temp_branch"].ToString())) {
        context.Response.Redirect("~/OdProtect/Bank/OdpLandingPage.aspx"); 
        }          
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

}