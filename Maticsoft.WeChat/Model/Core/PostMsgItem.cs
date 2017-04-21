/**
* PostMsgItem.cs
*
* 功 能： N/A
* 类 名： PostMsgItem
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/29 18:14:12   N/A    初版
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
	/// PostMsgItem:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class PostMsgItem
	{
		public PostMsgItem()
		{}
        #region Model
        private int _postmsgid;
        private int _itemid;
        private int _type = 0;
        /// <summary>
        /// 回复信息ID
        /// </summary>
        public int PostMsgId
        {
            set { _postmsgid = value; }
            get { return _postmsgid; }
        }
        /// <summary>
        /// 图文信息ID
        /// </summary>
        public int ItemId
        {
            set { _itemid = value; }
            get { return _itemid; }
        }
        /// <summary>
        /// 消息项类型 0:自动回复 1：客服回复 2:任务推送
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        #endregion Model

	}
}

