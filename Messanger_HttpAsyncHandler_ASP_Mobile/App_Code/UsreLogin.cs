using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsreLogin
/// </summary>

[Serializable]
public class UsreLogin
{
    private string mail;
    private string password;

    public UsreLogin(UsreLogin UL)
    {
        this.mail = UL.mail;
        this.password = UL.password;
    }
    public UsreLogin() { }
	public UsreLogin(string userName, string password)
	{
        this.mail = userName;
        this.password = password;
	}
    public string Mail
    {
        get {
            return this.mail;
        }
        set
        {
            this.mail = value ;
        }
    }
    public string Password
    {
        get
        {
            return this.password;
        }
        set
        {
            this.password = value;
        }
    }
}