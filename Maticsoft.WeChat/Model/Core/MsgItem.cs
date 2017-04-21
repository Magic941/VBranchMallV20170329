/**
* MsgItem.cs
*
* 功 能： N/A
* 类 名： MsgItem
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/22 17:06:14   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.WeChat.Model.Core
{
	/// <summary>
	/// MsgItem:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class MsgItem
	{
		public MsgItem()
		{}
        #region Model
        private int _itemid;
        private string _openid;
        private string _title;
        private string _picurl;
        private string _url;
        private string _description;
        /// <summary>
        /// 
        /// </summary>
        public int ItemId
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string OpenId
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// 图文消息标题
        /// </summary>
        public string Title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。
        /// </summary>
        public string PicUrl
        {
            set { _picurl = value; }
            get { return _picurl; }
        }
        /// <summary>
        /// 点击图文消息跳转链接
        /// </summary>
        public string Url
        {
            set { _url = value; }
            get { return _url; }
        }
        /// <summary>
        /// 图文消息描述
        /// </summary>
        public string Description
        {
            set { _description = value; }
            get { return _description; }
        }
        #endregion Model

	}
}

