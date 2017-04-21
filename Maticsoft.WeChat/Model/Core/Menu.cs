/**
* Menu.cs
*
* 功 能： N/A
* 类 名： Menu
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/9/17 12:25:28   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;

namespace Maticsoft.WeChat.Model.Core
{
	/// <summary>
	/// Menu:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Menu
	{
		public Menu()
		{}
        #region Model
        private int _menuid;
        private string _openid;
        private int _parentid;
        private string _name;
        private string _type;
        private int _sequence;
        private string _menukey;
        private string _menuurl;
        private int _status;
        private DateTime _createdate;
        private string _remark;
        private bool _haschildren = false;
        /// <summary>
        /// 菜单ID
        /// </summary>
        public int MenuId
        {
            set { _menuid = value; }
            get { return _menuid; }
        }
        /// <summary>
        ///  微信开发者ID
        /// </summary>
        public string OpenId
        {
            set { _openid = value; }
            get { return _openid; }
        }
        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentId
        {
            set { _parentid = value; }
            get { return _parentid; }
        }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 菜单类型  “click”,"view"
        /// </summary>
        public string Type
        {
            set { _type = value; }
            get { return _type; }
        }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sequence
        {
            set { _sequence = value; }
            get { return _sequence; }
        }
        /// <summary>
        /// 菜单 Key 值
        /// </summary>
        public string MenuKey
        {
            set { _menukey = value; }
            get { return _menukey; }
        }
        /// <summary>
        /// 菜单Url
        /// </summary>
        public string MenuUrl
        {
            set { _menuurl = value; }
            get { return _menuurl; }
        }
        /// <summary>
        /// 状态 0：不启用 1：启用
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate
        {
            set { _createdate = value; }
            get { return _createdate; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 是否有子集
        /// </summary>
        public bool HasChildren
        {
            set { _haschildren = value; }
            get { return _haschildren; }
        }
        #endregion Model

	}

    /// <summary>
    /// 
    /// </summary>
    public partial class Button
    {
        public string  name;
    }

    /// <summary>
    ///  事件菜单
    /// </summary>
    public class ClickBtn : Button
    {
        /// <summary>
        /// 
        /// </summary>
        public string type="click";
        public string key;
    }

    /// <summary>
    /// View菜单
    /// </summary>
    public class ViewBtn : Button
    {
        public string type="view";
        public string url;
    }

    /// <summary>
    /// 多级菜单
    /// </summary>
    public class MultiBtn : Button
    {
        public List<Button> sub_button=new List<Button>();
    }

    public class JsonMenu
    {
        public List<Button> button=new List<Button>();
    }


}

