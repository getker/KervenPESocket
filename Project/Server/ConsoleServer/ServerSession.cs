using ConsoleServer;
using PENet;
using Protocol;
using System.Net.Sockets;
using System.Collections.Generic;

public class ServerSession : PESession<NetMsg> {
    Socket curSendClientSocket = null;
    protected override void OnConnected() {
        PETool.LogMsg("Client OnLine.");
        SendMsg(new NetMsg {
            text = "Welcome to connect!"
        });
    }

    protected override void OnReciveMsg(NetMsg msg) {
        curSendClientSocket = GetClientSocket();
        HandleReceiveMsg(msg);
    }

    private void HandleReceiveMsg(NetMsg msg)
    {
        PETool.LogMsg("Client Request:" + msg.msgType + msg.text);
        switch (msg.msgType)
        {
            case MsgType.BroadcastAll:
                BroadcastAllClient(msg.text);
                break;
            case MsgType.BroadcastAllWithCurClient:
                BroadcastAllWithCurClient(msg.text, curSendClientSocket);
                break;
            case MsgType.Store:

                break;
            case MsgType.Default:

                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 转发所有客户端（除了发消息的客户端自身）
    /// </summary>
    /// <param name="msgStr">消息内容</param>
    private void BroadcastAllWithCurClient(string msgStr, Socket curSendClientSocket)
    {
        List<ServerSession> sessionLst = ServerStart.server.GetSesstionLst();
        for (int i = 0; i < sessionLst.Count; i++)
        {
            if (sessionLst[i].GetClientSocket().RemoteEndPoint == curSendClientSocket.RemoteEndPoint) continue;
            sessionLst[i].SendMsg(new NetMsg
            {
                text = msgStr
            });
        }
    }

    /// <summary>
    /// 广播所有客户端
    /// </summary>
    /// <param name="msgStr">消息内容</param>
    private void BroadcastAllClient(string msgStr)
    {
        List<ServerSession> sessionLst = ServerStart.server.GetSesstionLst();
        for (int i = 0; i < sessionLst.Count; i++)
        {
            PETool.LogMsg("Client AddressFamily sessionLst:" + sessionLst[i].GetClientSocket().RemoteEndPoint);
            sessionLst[i].SendMsg(new NetMsg
            {
                text = msgStr
            });
        }
    }

    protected override void OnDisConnected()
    {
        PETool.LogMsg("Client OffLine.");
    }
}