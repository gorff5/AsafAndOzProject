using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void Button_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string UserName = userName.Text;
        string Password = password.Text;
        string url = "http://localhost:52063/LoginHandler.ashx";
        UsreLogin userLogin = new UsreLogin(UserName, Password);
        JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
        string sendStr = myJavaScriptSerializer.Serialize(userLogin);

        url += "?JSON=" + sendStr;


        HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
        Stream receiveStream = myHttpWebResponse.GetResponseStream();
        Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
        StreamReader readStream = new StreamReader(receiveStream, encode);


        Char[] read = new Char[1000];
        int count = readStream.Read(read, 0, 256);
        string resultStr = new String(read, 0, count);
        myHttpWebResponse.Close();
        readStream.Close();

        if (resultStr.CompareTo("true") == 1)
        {
            Response.Redirect("Default.aspx");
        }

       
    }
}