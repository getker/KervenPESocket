using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Protocol
{
    /// <summary>
    /// 消息类型
    /// </summary>
    public enum MsgType
    {
        BroadcastAll,               // 广播所有
        BroadcastAllWithCurClient,  // 广播所有（除去当前的客户端）
        Store,                      // 存储
        Default                       // 其他
    }
}
