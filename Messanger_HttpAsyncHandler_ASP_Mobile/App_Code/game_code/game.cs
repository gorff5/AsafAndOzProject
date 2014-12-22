using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// this is the game class, for every instance we create game with players,ques,scores etc.
/// </summary>
public class game
{
    private Boolean waiting_for_players;
    private int num_of_players=0;
    private Ques[] game_questions;
    private player[] players=new player[8];

	public game(player p)
    {
        waiting_for_players = true;
        players[num_of_players] = p;
        num_of_players++;
        game_questions = getQues();
    }

    private Ques[] getQues()
    {
        throw new NotImplementedException();
    }

}