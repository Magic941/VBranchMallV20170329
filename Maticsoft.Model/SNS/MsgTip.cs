using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.SNS
{  /// <summary>
    /// 消息提示
    /// </summary>
    [Serializable]
   
     public  class MsgTip
    {
        private int _count;
        private int _msgtype;
        /// <summary>
        /// 小组的id
        /// </summary>
        public int Count
        {
            set { _count = value; }
            get { return _count; }
        }
        /// <summary>
        /// 小组的id
        /// </summary>
        public int _MsgType
        {
            set { _msgtype = value; }
            get { return _msgtype; }
        }
    }
}
