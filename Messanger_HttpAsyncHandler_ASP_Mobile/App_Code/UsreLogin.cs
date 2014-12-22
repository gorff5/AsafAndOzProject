using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UsreLogin
/// this class made for passing user infromation between client and server (serealized json)
/// </summary>

[Serializable]
public class UsreLogin
{
    //login values
    private string mail;
    private string password;
    //additional registration values
    private Boolean register;
    private string name=null;
    
    //searilization constructor
    public UsreLogin() { }
    
    //login constructor
	public UsreLogin(string mail, string password)
	{
        register = false;
        this.mail = mail;
        this.password = password;
	}
    //register constructor
    public UsreLogin(string name,string mail, string password)
    {
        register = true; 
        this.name = name;
        this.mail = mail;
        this.password = password;
    }


    /// <summary>
    /// /////////////////////////////////////////////////////////////////GETERS AND SETERS/////////////////////////////////////////
    /// </summary>

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

    public string Name
    {
        get
        {
            return this.name;
        }
        set
        {
            this.name = value;
        }
    }


    public Boolean Register
    {
        get
        {
            return this.register;
        }
        set
        {
            this.register = value;
        }
    }
}