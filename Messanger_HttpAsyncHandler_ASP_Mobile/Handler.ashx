<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.Threading;

public class Handler : IHttpAsyncHandler, System.Web.SessionState.IReadOnlySessionState
{
    public IAsyncResult BeginProcessRequest(HttpContext ctx, AsyncCallback cb, Object obj)
    {
        AsyncResult currentAsyncState = new AsyncResult(ctx, cb, obj);
        
        ThreadPool.QueueUserWorkItem(new WaitCallback(RequestWorker), currentAsyncState);

        return currentAsyncState;
    }
    
    public void EndProcessRequest(IAsyncResult ar)
    {
    }


    
      
    public bool IsReusable
    {
        get { return true; }
    }
    
    public void ProcessRequest(HttpContext context)
    {
    }
    
   
    
    
    private void RequestWorker(Object obj)
    {
        // obj - second parametr in ThreadPool.QueueUserWorkItem()
        AsyncResult myAsyncResult = obj as AsyncResult;

        string command = "";
        if (myAsyncResult._context.Request.QueryString["cmd"] != null)
            command = myAsyncResult._context.Request.QueryString["cmd"];

        string guid = "";
        if (myAsyncResult._context.Request.QueryString["guid"] != null)
            guid = myAsyncResult._context.Request.QueryString["guid"];

 //       string callbackStr = "";
 //       if (myAsyncResult._context.Request.QueryString["callback"] != null)
 //           callbackStr = myAsyncResult._context.Request.QueryString["callback"];

        bool isMobile = false;
        if (myAsyncResult._context.Request.QueryString["Mobile"] != null)
            isMobile = true;

        string message = "";
        if (myAsyncResult._context.Request.QueryString["message"] != null)
            message = myAsyncResult._context.Request.QueryString["message"];

        string userName = "";
        if (myAsyncResult._context.Request.QueryString["userName"] != null)
            userName = myAsyncResult._context.Request.QueryString["userName"]; 
                
        switch (command)
        {
            case "Register":
//                AsyncServer.RegicterClient(myAsyncResult, callbackStr, isMobile);
                AsyncServer.RegisterClient(myAsyncResult, isMobile);
                myAsyncResult.CompleteRequest();
                break;
            case "Unregister":
                AsyncServer.UnregisterClient(myAsyncResult);
                myAsyncResult.CompleteRequest();
                break;
            case "Set":
                AsyncServer.Set("ans");
                myAsyncResult.CompleteRequest();
                break;
            case "Get":
                if (guid != null)
                {
 //                   AsyncServer.Get(myAsyncResult, guid, callbackStr, isMobile);
                    AsyncServer.Get(myAsyncResult, guid, isMobile);
                }
                break;

        }
    }
}