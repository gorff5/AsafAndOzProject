<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Threading;
using System.Web.Script.Serialization;

public class Handler : IHttpHandler
{
    private static JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["JSON"] != null)
        {
            UsreLogin UL = (UsreLogin)myJavaScriptSerializer.Deserialize<UsreLogin>(context.Request.QueryString["JSON"]);

            if (UL.UserName == UL.Password)
            {
                context.Response.Write("True");
            }
            else
            {
                context.Response.Write("False");
            }
        }    
        
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}