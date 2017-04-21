using System;
using System.Collections.Generic;
using System.Text;

namespace Maticsoft.Model.SysManage
{
   
    /// <summary>
    /// 用于记录用户日志的对象
    /// </summary>
    public class UserLog
    {
        public UserLog()
        { 
        }
        public UserLog(string url, string opInfo, string userName, string userType, string userIP)
        {
            Url = url;
            OPInfo = opInfo;
            UserName = userName;
            userName = UserType;
            UserIP = userIP;
        }

        #region Model
        private DateTime _OPTime=DateTime.Now;
        private string _Url;
        private string _OPInfo;
        private string _UserName;
        private string _UserIP;
        private string _UserType;
        #endregion

        #region properties
        /// <summary>
        /// 时间
        /// </summary>
        public DateTime OPTime
        {
            set { _OPTime = value; }
            get { return _OPTime; }
        }
        /// <summary>
        /// 页面
        /// </summary>
        public string Url
        {
            set { _Url = value; }
            get { return _Url; }
        }
        /// <summary>
        /// 操作
        /// </summary>
        public string OPInfo
        {
            set { _OPInfo = value; }
            get { return _OPInfo; }
        }
        /// <summary>
        /// 用户
        /// </summary>
        public string UserName
        {
            set { _UserName = value; }
            get { return _UserName; }
        }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string UserType
        {
            set { _UserType = value; }
            get { return _UserType; }
        }
        /// <summary>
        /// 用户IP
        /// </summary>
        public string UserIP
        {
            set { _UserIP = value; }
            get { return _UserIP; }
        }
        #endregion
    }
    
}
