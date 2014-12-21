<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Threading;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;

public class Handler : IHttpHandler
{
    private static Object _lock = new Object();
    private static JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
    private String score;
    public void ProcessRequest(HttpContext context)
    {
        lock (_lock)
        {
            if (context.Request.QueryString["JSON"] != null)
            {
                UsreLogin UL = (UsreLogin)myJavaScriptSerializer.Deserialize<UsreLogin>(context.Request.QueryString["JSON"]);
                score="";
                MySqlConnection myConnect;
                MySqlCommand myCommand;
                MySqlDataReader reader;
                string myConnectString = "server=eu-cdbr-azure-west-b.cloudapp.net;User Id=bd5ee543622578;password=fca73da9;database=realmoney";
                myConnect = new MySqlConnection(myConnectString);
                myCommand = new MySqlCommand("SELECT score FROM users where email='" + UL.Mail + "' AND password='"+UL.Password+"'", myConnect);
                myConnect.Open();
                reader = myCommand.ExecuteReader();
                reader.Read();
                try
                {
                    score = reader[0].ToString();
                }
                catch (Exception e)
                {
                    //when no user found in db
                    context.Response.Write("False");
                }
                //user found reaturn user score as conformation
                context.Response.Write(score);
                myConnect.Close();
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