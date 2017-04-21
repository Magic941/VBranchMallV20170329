/**  版本信息模板在安装目录下，可自行修改。
* Scene.cs
*
* 功 能： N/A
* 类 名： Scene
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/2/20 12:32:06   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.WeChat.Model.Core
{
	/// <summary>
	/// Scene:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Scene
	{
		public Scene()
		{}
        #region Model
        private int _sceneid;
        private string _openid;
        private string _name;
        private DateTime _createtime;
        private string _remark;
        private string _imageurl;
        /// <summary>
        /// 场景ID
        /// </summary>
        public int SceneId
        {
            set { _sceneid = value; }
            get { return _sceneid; }
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
        /// 场景名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createtime = value; }
            get { return _createtime; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        #endregion Model

	}
}

