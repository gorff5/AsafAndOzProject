using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// each game have players, each one earn score
/// </summary>
public class player
{
    private String name;
    private String mail;
    private int score;

    public player() { }

    public player(String name,String mail)
    {
        this.name = name;
        this.mail = mail;
        this.score = 0;
    }
}