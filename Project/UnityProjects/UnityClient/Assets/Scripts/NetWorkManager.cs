using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NetWorkManager : MonoBehaviour
{
    public Text msgText;
    GameStart gameStart;
    void Start()
    {
        gameStart = GetComponent<GameStart>();
        if (msgText == null)
        {
            msgText = GameObject.Find("Msg").GetComponent<Text>();
        }
        msgText.text = "";
    }

    void LateUpdate()
    {
        if (MsgPool.Instance.CheckHasMsg())
        {
            string msg = MsgPool.Instance.GetAndRemoveMsg() + "\n";
            msgText.text += msg;
        }
    }

    public void SendMsg(string msg)
    {
        gameStart.SendMsg(msg);
    }

    public void SendMsgWithClientName(string msg, string name)
    {
        string newMsg = name + ":" + msg;
        gameStart.SendMsg(newMsg);
    }
}
