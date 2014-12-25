using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// each game have players, each one earn score
/// </summary>
[Serializable]
public class player
{
    public String name;
    public String mail;
    public String score;
    public String GUID;
    public String gameID;
    public Ques Ques;

    public player() { }

    public player(String name,String mail,String score,String GUID)
    {
        this.name = name;
        this.mail = mail;
        this.score = score;
        this.GUID = GUID;
        this.Ques = null;
    }
}