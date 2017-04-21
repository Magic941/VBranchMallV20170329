/**
* Command.cs
*
* 功 能： N/A
* 类 名： Command
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/29 15:35:22   N/A    初版
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
	/// Command:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class Command
	{
		public Command()
		{}
        #region Model
        private int _commandid;
        private string _openid;
        private int _actionid = -1;
        private int _targetid = -1;
        private string _name;
        private int _sequence;
        private int _status = 1;
        private int _parsetype;
        private int _parselength;
        private string _parsechar;
        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public int CommandId
        {
            set { _commandid = value; }
            get { return _commandid; }
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
        /// 对应行为ID
        /// </summary>
        public int ActionId
        {
            set { _actionid = value; }
            get { return _actionid; }
        }
        /// <summary>
        /// 指定目标ID，如查询CMS文章的某栏目下文章，就目标ID 就是该栏目ID
        /// </summary>
        public int TargetId
        {
            set { _targetid = value; }
            get { return _targetid; }
        }
        /// <summary>
        /// 指令名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
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
        /// 状态 0：未启用 1：启用
        /// </summary>
        public int Status
        {
            set { _status = value; }
            get { return _status; }
        }
        /// <summary>
        /// 指令解析类型 0：按长度  1：按关键字 例如（# @ 等）
        /// </summary>
        public int ParseType
        {
            set { _parsetype = value; }
            get { return _parsetype; }
        }
        /// <summary>
        /// 解析长度
        /// </summary>
        public int ParseLength
        {
            set { _parselength = value; }
            get { return _parselength; }
        }
        /// <summary>
        /// 解析特殊字符  例如 @  #  等
        /// </summary>
        public string ParseChar
        {
            set { _parsechar = value; }
            get { return _parsechar; }
        }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark
        {
            set { _remark = value; }
            get { return _remark; }
        }
        #endregion Model

	}
}

