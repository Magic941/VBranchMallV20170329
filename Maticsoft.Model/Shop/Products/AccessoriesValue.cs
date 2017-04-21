/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：AccessoriesValues.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:21
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
	/// AccessoriesValues:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
    [Serializable]
	public partial class AccessoriesValue
	{
		public AccessoriesValue()
		{}
        #region Model
        private int _accessoriesvalueid;
        private int _accessoriesid;
        private string _sku;
        /// <summary>
        /// 
        /// </summary>
        public int AccessoriesValueId
        {
            set { _accessoriesvalueid = value; }
            get { return _accessoriesvalueid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int AccessoriesId
        {
            set { _accessoriesid = value; }
            get { return _accessoriesid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SKU
        {
            set { _sku = value; }
            get { return _sku; }
        }
        #endregion Model

	}
}

