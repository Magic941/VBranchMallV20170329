using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Maticsoft.WeChat.Model.Core
{
    //微信请求信息Model
    public class RequestMsg
    {
        #region Model
        private long _usermsgid;
        private string _openid;
        private string _username;
        private string _msgtype;
        private DateTime _createtime;
        private string _description;
        private string _location_x;
        private string _location_y;
        private int _scale;
        private string _picurl;
        private string _title;
        private string _url;
        private string _event;
        private string _eventkey;
        /// <summary>
        /// 
        /// </summary>
        public long UserMsgId
        {
            set { _usermsgid = value; }
            get { return _usermsgid; }
        }
        /// <summary>
        /// 开发者帐号 （微信那边获取的）
        /// </summary>
        public string OpenId
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// 微信用户  OpenID
        /// </summary>
        public string UserName
        {
            set { _username = value; }
            get { return _username; }
        }
        /// <summary>
        /// 消息类型  （地理位置:location,文本消息:text,消息类型:image，链接信息：link，事件信息：event）
        /// </summary>
        public string MsgType
        {
            set { _msgtype = value; }
            get { return _msgtype; }
        }
        /// <summary>
        /// 消息时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Location_X
        {
            set { _location_x = value; }
            get { return _location_x; }
        }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Location_Y
        {
            set { _location_y = value; }
            get { return _location_y; }
        }
        /// <summary>
        /// 地图缩放大小
        /// </summary>
        public int Scale
        {
            set { _scale = value; }
            get { return _scale; }
        }
        /// <summary>
        /// 图片链接地址（图片消息才会使用）
        /// </summary>
        public string PicUrl
        {
            set { _picurl = value; }
            get { return _picurl; }
        }
        /// <summary>
        /// 消息标题（链接消息才会使用）
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 消息链接 （链接消息才会使用）
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)、CLICK(自定义菜单点击事件)
        /// </summary>
        public string Event
        {
            set { _event = value; }
            get { return _event; }
        }
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey
        {
            set { _eventkey = value; }
            get { return _eventkey; }
        }
        #endregion Model

        #region 扩展属性
        public decimal Precision;
        #endregion 
    }
}