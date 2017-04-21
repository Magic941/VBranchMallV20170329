/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：HotKeywords.cs
// 文件功能描述：
// 
// 创建标识： [cc]  2012/06/18 15:46:48
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
namespace Maticsoft.Model.Shop.Products
{
    /// <summary>
    /// HotKeywords:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class HotKeyword
    {
        public HotKeyword()
        { }
        #region Model
        private int _id;
        private string _keywords;
        private int? _categoryid;
        /// <summary>
        /// 
        /// </summary>
        public int Id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Keywords
        {
            set { _keywords = value; }
            get { return _keywords; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        #endregion Model

    }
}

