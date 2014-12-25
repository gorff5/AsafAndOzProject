<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Threading;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;

public class Handler : IHttpHandler
{
    private static Object _lock = new Object();
    private static JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
    
    //mySQL values
    private static string myConnectString = "server=eu-cdbr-azure-west-b.cloudapp.net;User Id=bd5ee543622578;password=fca73da9;database=realmoney";
    private MySqlConnection myConnect = new MySqlConnection(myConnectString);
    private MySqlCommand myCommand;
    private MySqlDataReader reader;
    
    public void ProcessRequest(HttpContext context)
    {
        lock (_lock)
        {
            if (context.Request.QueryString["JSON"] != null)
            {
                UsreLogin UL = (UsreLogin)myJavaScriptSerializer.Deserialize<UsreLogin>(context.Request.QueryString["JSON"]);
                if (UL.Register)
                    context.Response.Write(register(UL)); 
                else
                    context.Response.Write(login(UL));
            }
        }   
    }

    
    
    private String login(UsreLogin UL)
    {
        String ret = "";
        myCommand = new MySqlCommand("SELECT name,score FROM users where email='" + UL.Mail + "' AND password='" + UL.Password + "'", myConnect);
        try
        {
            myConnect.Open();
            reader = myCommand.ExecuteReader();
            reader.Read();
            player newPlayer = new player(reader[0].ToString(), UL.Mail, reader[1].ToString(), Guid.NewGuid().ToString());
            AsyncServer.readyPlayers.Add(newPlayer);
            ret = myJavaScriptSerializer.Serialize(newPlayer);
        }
        catch (Exception e)
        {
            //when no user found in db or db error
            myConnect.Close();
            ret = "False";
            return ret;
        }
        //user found! return user score as conformation
        myConnect.Close();
        return ret;
    }

    
    
    private String register(UsreLogin UL)
    {
        String ret;
        myCommand = new MySqlCommand("INSERT INTO users (name, email, password) SELECT * FROM (SELECT '" + UL.Name + "' as name,'" + UL.Mail + "'as email,'" + UL.Password + "'as password) AS tmp WHERE NOT EXISTS (SELECT email FROM users WHERE email = '" + UL.Mail + "') LIMIT 1;", myConnect);
        try
        {
            myConnect.Open();
            int rows_effacted = myCommand.ExecuteNonQuery();
            if (rows_effacted == 0)
                throw new Exception("user all ready exists");
        }
        catch (Exception e)
        {
            //when no user found in db or db error
            myConnect.Close();
            ret = "False";
            return ret;
        }
        //user found! return user score as conformation
        myConnect.Close();
        player newPlayer = new player(UL.Name, UL.Mail,"0", Guid.NewGuid().ToString());
        AsyncServer.readyPlayers.Add(newPlayer);
        ret = myJavaScriptSerializer.Serialize(newPlayer);
        return ret;
    }
    
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
