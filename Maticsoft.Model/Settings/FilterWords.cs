/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：FilterWords.cs
// 文件功能描述：
// 
// 创建标识： [Name]  2012/08/24 11:00:36
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.Settings
{
    /// <summary>
    /// FilterWords:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class FilterWords
    {
        public FilterWords()
        {}
        #region Model
        private int _filterid;
        private string _wordpattern;
        private int _actiontype;
        private string _repalceword;
        /// <summary>
        /// ID
        /// </summary>
        public int FilterId
        {
            set { _filterid = value; }
            get { return _filterid; }
        }
        /// <summary>
        /// 过滤词
        /// </summary>
        public string WordPattern
        {
            set { _wordpattern = value; }
            get { return _wordpattern; }
        }
        /// <summary>
        /// 过滤类型 0：禁用关键词 1：审核关键词 2：替换关键词
        /// </summary>
        public int ActionType
        {
            set { _actiontype = value; }
            get { return _actiontype; }
        }
        /// <summary>
        /// 替换词
        /// </summary>
        public string RepalceWord
        {
            set { _repalceword = value; }
            get { return _repalceword; }
        }
        #endregion Model

    }
}

