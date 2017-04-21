using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.SysManage
{
    public class ErrorLog
    {
        public ErrorLog()
        {            
        }

        public ErrorLog(string url, string loginfo, string stackTrace)
        {
            Url = url;
            Loginfo=loginfo;
            StackTrace=stackTrace;            
        }

        private int _id;
        private DateTime _optime=DateTime.Now;
        private string _url;
        private string _loginfo;
        private string _stacktrace;
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
        public DateTime OPTime
        {
            set { _optime = value; }
            get { return _optime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Loginfo
        {
            set { _loginfo = value; }
            get { return _loginfo; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string StackTrace
        {
            set { _stacktrace = value; }
            get { return _stacktrace; }
        }
    }
}
