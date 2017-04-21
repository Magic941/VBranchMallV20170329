using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class MailConfig
    {
        public MailConfig()
        { }
        #region Model
        private int _id;
        private int _userid;
        private string _mailaddress;
        private string _username;
        private string _password;
        private string _smtpserver;
        private int _smtpport=25;
        private bool _smtpssl;
        private string _popserver;
        private int _popport=110;
        private bool _popssl;
        /// <summary>
        /// 
        /// </summary>
        public int ID
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int UserID
        {
            set { _userid = value; }
            get { return _userid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Mailaddress
        {
            set { _mailaddress = value; }
            get { return _mailaddress; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Username
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            set { _password = value; }
            get { return _password; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SMTPServer
        {
            set { _smtpserver = value; }
            get { return _smtpserver; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int SMTPPort
        {
            set { _smtpport = value; }
            get { return _smtpport; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool SMTPSSL
        {
            set { _smtpssl = value; }
            get { return _smtpssl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string POPServer
        {
            set { _popserver = value; }
            get { return _popserver; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int POPPort
        {
            set { _popport = value; }
            get { return _popport; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool POPSSL
        {
            set { _popssl = value; }
            get { return _popssl; }
        }
        #endregion Model

    }
}
