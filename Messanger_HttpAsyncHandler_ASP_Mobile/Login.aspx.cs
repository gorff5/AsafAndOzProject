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
    protected static JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();


    protected void Page_Load(object sender, EventArgs e)
    {
    }


    //login
    protected void login_Click(object sender, EventArgs e)
    {
        string mail = mailT.Text;
        string Password = passwordT.Text;
        if (mail == "" || Password == "")
        {
            //username or password wrong, we need to add gui to notify the user
            return;
        }
        //PORT OF LOCAL COMPUTER CHANGES ALL THE TIME PLEASE MAKE SURE THAT U HAVE THE RIGHT PORT NUMBER.
        UsreLogin UL = new UsreLogin(mail, Password);
        string resultStr = postJSON(UL);

        if (resultStr.CompareTo("False") == 0)
        {
            mailT.Text = "";
            passwordT.Text = "";
        }
        else
        {
            player me = (player)myJavaScriptSerializer.Deserialize<player>(resultStr);
            Response.Cookies["playerCookie"].Value = resultStr;
            Response.Redirect("Default.aspx");
        }
    }

    //register
    protected void register_Click(object sender, EventArgs e)
    {
        //first click JUST OPEN REGISTER OPTION
        if (register_button.Text == "Register")
        {
            nameL.Visible = true;
            nameT.Visible = true;
            register_button.Text="Finish";
        }
        else
        {
            string name = nameT.Text;
            string mail = mailT.Text;
            string Password = passwordT.Text;
            if (name == "" || mail == "" || Password == "")
            {
                //username or password wrong, we need to add gui to notify the user
                return;
            }
            UsreLogin UL = new UsreLogin(name,mail,Password);
            string resultStr = postJSON(UL);

            if (resultStr.CompareTo("False") == 0)
            {
                mailT.Text = "";
                passwordT.Text = "";
            }
            else
            {
                player me = (player)myJavaScriptSerializer.Deserialize<player>(resultStr);
                Response.Cookies["playerCookie"].Value = resultStr;
                Response.Redirect("Default.aspx");
            }
        }
    }

    //send userLogin class as json to server
    private String postJSON(UsreLogin ul)
    {
        try
        {
            //PORT OF LOCAL COMPUTER CHANGES ALL THE TIME PLEASE MAKE SURE THAT U HAVE THE RIGHT PORT NUMBER.
            string url = "http://localhost:53018/LoginHandler.ashx";
            string sendStr = myJavaScriptSerializer.Serialize(ul);
            url += "?JSON=" + sendStr;

            HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();
            Stream receiveStream = myHttpWebResponse.GetResponseStream();
            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(receiveStream, encode);

            Char[] read = new Char[1000];
            int count = readStream.Read(read, 0, 256);
            myHttpWebResponse.Close();
            readStream.Close();
            return new String(read, 0, count);
        }
        catch (Exception e)
        {  
            //server or intenet error
            return "ERROR";
        }
    }
}