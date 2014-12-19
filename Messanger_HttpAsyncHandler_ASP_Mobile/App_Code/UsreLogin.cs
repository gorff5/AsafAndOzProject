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
    private string userName;
    private string password;

    public UsreLogin(UsreLogin UL)
    {
        this.userName = UL.userName;
        this.password = UL.password;
    }
    public UsreLogin() { }
	public UsreLogin(string userName, string password)
	{
        this.userName = userName;
        this.password = password;
	}
    public string UserName
    {
        get {
            return this.userName;
        }
        set
        {
            this.userName = value ;
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