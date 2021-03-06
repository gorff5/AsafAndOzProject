﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

/// <summary>
/// this is the game class, for every instance we create game with players,ques,scores etc.
/// </summary>
public class game
{
    public String id;   //every game need a id, because thare can be many games, and player will not know wich game he belong to.
    public Boolean waiting_for_players;
    public int num_of_players=0;
    public Ques[] game_questions;
    public int current_ques_num;//wich question to be next to send to the players
    public player[] players;

	public game(player p)
    {
        game_questions = new Ques[5];
        players=new player[8];
        id = p.GUID;
        p.gameID = id;
        waiting_for_players = true;
        players[num_of_players] = p;
        num_of_players++;
        getAllQues();
    }

    //initialize question array at once from sql.
    private void getAllQues()
    {
        MySqlConnection myConnect;
        MySqlCommand myCommand;
        MySqlDataReader reader;
        string myConnectString = "server=eu-cdbr-azure-west-b.cloudapp.net;User Id=bd5ee543622578;password=fca73da9;database=realmoney";
        myConnect = new MySqlConnection(myConnectString);
        myConnect.Open();
        myCommand = new MySqlCommand("SELECT * FROM ques", myConnect);
        reader = myCommand.ExecuteReader();
        reader.Read();
        for (int i = 0; i < game_questions.Length ; i++)
        {
            if (reader.Read())
            {
                Ques q = new Ques(reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
                game_questions[i] = q;
            }
        }
        myConnect.Close();
    }

    //return one quesion from question array
    public Ques getQues()
    {
        return game_questions[current_ques_num];
    }
    //set next question
    public void nextQues()
    {
        current_ques_num++;
    }

    //add new player to game
    internal void add(player newPlayer)
    {
        players[num_of_players] = newPlayer;
        num_of_players++;
        newPlayer.gameID = id;//the player plays in this game
        Thread workerThread = new Thread(startGame);
        workerThread.Start();//open thread beacuse the second player need to know that he is registered succesfuly to server befor getting notification from server.
        //and after he get message that the reg succed he will get the first queston.
    }

    //start game
    public void startGame()
    {
        System.Threading.Thread.Sleep(10000);
        waiting_for_players = false;
        for (int i = 0; i < num_of_players; i++)
        {
            players[i].Ques = game_questions[current_ques_num];
        }
        current_ques_num++;
        AsyncServer.Set("id");
    }
}