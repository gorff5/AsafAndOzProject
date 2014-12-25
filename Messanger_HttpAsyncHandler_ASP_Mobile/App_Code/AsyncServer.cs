using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
public class AsyncServer
{
    private static Object _lock = new Object();
    private static List<AsyncResult> _clientStateList = new List<AsyncResult>();
    private static List<game> online_games = new List<game>();
    private static JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
    private static Random rnd = new Random();

    //this list contain players who are logged in and ready to play, the registerClient function will check if player is ready in this list.
    public static List<player> readyPlayers = new List<player>();

    public static void Set(String gameID)
    {

        lock (_lock)
        {
            string resultStr;
            game g = findGame(gameID);
            g.nextQues();
            foreach (AsyncResult clientState in _clientStateList)
            {
                if (clientState._context.Session != null)
                {
                    if (clientState.Player.gameID == g.id)
                    {
                        clientState.Player.Ques = g.getQues();
                        resultStr = myJavaScriptSerializer.Serialize(clientState.Player);
                        if (clientState._isMobile == false)
                            clientState._context.Response.Write(resultStr);
                        else
                        {
                            //                        string str = clientState._callbackStr + "({ res: '" + message.UserNameMessage_Out() + "'})";
                            string str = "{ res: '" + gameID + "'}";
                            clientState._context.Response.Write(str);
                        }
                        clientState.CompleteRequest();
                    }
                }
            }
        }
    }

    private static game findGame(string gameID)
    {
        foreach (game g in online_games)//search for online games with the same id
        {
            if (g.id.CompareTo(gameID)==0) // Will match once
            {
                return g;
            }
        }
        return null;
    }

//    public static void Get(AsyncResult state, string guid, string callbackStr, bool isMobile)
    public static void Get(AsyncResult state, string guid, bool isMobile)
    {
        lock (_lock)
        {
            AsyncResult clientState = _clientStateList.Find(
                delegate(AsyncResult cs)
                {
                    return cs.ClientGuid == guid;
                }
            );
            if (clientState != null)
            {
                clientState._context = state._context;
                clientState._state = state._state;
                clientState._callback = state._callback;

 ///               clientState._callbackStr = callbackStr;
                clientState._isMobile = isMobile;
            }
        }
    }

 //   public static void RegicterClient(AsyncResult state, string callbackStr, bool isMobile)
     public static void RegisterClient(AsyncResult state, String guid,bool isMobile)
    {
        lock (_lock)
        {
            foreach (player p in readyPlayers)//search if there is really logged in player with the same guid in list
            {
                if (p.GUID.CompareTo(guid)==0) // Will match once
                {
                    state.ClientGuid = guid;
                    state.Player = p;
                    _clientStateList.Add(state);
                    arrangeGames(p);
                    string resultStr = myJavaScriptSerializer.Serialize(p);
                    if (isMobile == false)
                        state._context.Response.Write(state.ClientGuid);
                    else
                    {
                        //               string str = callbackStr + "({ res: '" + state.ClientGuid + "'})";
                        string str = "{ res: '" + state.ClientGuid + "'}";
                        state._context.Response.Write(resultStr);
                    };
                    break;
                }
            }
        }
    }

    public static void UnregisterClient(AsyncResult state)
    {
        lock (_lock)
        {
             _clientStateList.Remove(state);
        }
    }

    //find game for new player.
    private static void arrangeGames(player newPlayer)
    {
        foreach (game g in online_games)//search for online games that waiting for more players;
        {
            if (g.waiting_for_players) // Will match once
            {
                g.add(newPlayer);
                return;
            }
        }
        //if there isnt online games available for this player make new game;
        game newGame = new game(newPlayer);
        online_games.Add(newGame);
        return;
    }
}
