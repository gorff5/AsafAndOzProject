using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
public class AsyncServer
{
    private static Object _lock = new Object();
    private static List<AsyncResult> _clientStateList = new List<AsyncResult>();
    private static JavaScriptSerializer myJavaScriptSerializer = new JavaScriptSerializer();
    private static    Random rnd = new Random();

    public static void Set(String ans)
    {

        lock (_lock)
        {
            
            MySqlConnection myConnect;
            MySqlCommand myCommand;
            MySqlDataReader reader;
            string myConnectString = "server=eu-cdbr-azure-west-b.cloudapp.net;User Id=bd5ee543622578;password=fca73da9;database=realmoney";
            myConnect = new MySqlConnection(myConnectString);
            myCommand = new MySqlCommand("SELECT * FROM ques where id='"+rnd.Next(1,6)+"'", myConnect);
            myConnect.Open();
            reader = myCommand.ExecuteReader();
            reader.Read();
            Ques q = new Ques(reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
         
            string resultStr = myJavaScriptSerializer.Serialize(q);
            foreach (AsyncResult clientState in _clientStateList)
            {
                if (clientState._context.Session != null)
                {
                    if( clientState._isMobile == false)
                        clientState._context.Response.Write(resultStr);
                    else
                    {
//                        string str = clientState._callbackStr + "({ res: '" + message.UserNameMessage_Out() + "'})";
                        string str = "{ res: '" + ans + "'}";
                        clientState._context.Response.Write(str);
                    }
                    clientState.CompleteRequest();
                }
            }
            myConnect.Close();
        }
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
     public static void RegisterClient(AsyncResult state, bool isMobile)
    {
        lock (_lock)
        {
            state.ClientGuid = Guid.NewGuid().ToString();
            _clientStateList.Add(state);
            if (isMobile == false)
                state._context.Response.Write(state.ClientGuid);
            else
            {
 //               string str = callbackStr + "({ res: '" + state.ClientGuid + "'})";
                string str = "{ res: '" + state.ClientGuid + "'}";
                state._context.Response.Write(str);
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
}
