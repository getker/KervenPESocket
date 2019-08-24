using ConsoleServer;
using PENet;
using Protocol;
using System.Collections.Generic;

public class ServerSession : PESession<NetMsg> {
    protected override void OnConnected() {
        PETool.LogMsg("Client OnLine.");
        SendMsg(new NetMsg {
            text = "Welcome to connect!"
        });
    }

    protected override void OnReciveMsg(NetMsg msg) {
        PETool.LogMsg("Client Request:" + msg.text);
        BroadcastAllClient(msg.text);
        //SendMsg(new NetMsg {
        //    text = msg.text
        //});
    }

    protected override void OnDisConnected() {
        PETool.LogMsg("Client OffLine.");
    }

    private void BroadcastAllClient(string msgStr)
    {
        List<ServerSession> sessionLst = ServerStart.server.GetSesstionLst();
        for (int i = 0; i < sessionLst.Count; i++)
        {
            sessionLst[i].SendMsg(new NetMsg
            {
                text = msgStr
            });
        }
    }
}