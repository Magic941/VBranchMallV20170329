/**
* ProductCategories.cs
*
* 功 能： N/A
* 类 名： ProductCategories
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年12月14日 11:36:03   Rock    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;

namespace Maticsoft.Model.Shop.Products
{
    /// <summary>
    /// 产品类别关联
    /// </summary>
    [Serializable]
    public partial class ProductCategories
    {
        public ProductCategories()
        { }

        #region Model

        private int _categoryid;
        private long _productid;
        private string _categoryPath;

        /// <summary>
        /// 分类ID
        /// </summary>
        public int CategoryId
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }

        /// <summary>
        /// 产品ID
        /// </summary>
        public long ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }

        /// <summary>
        /// 分类Path
        /// </summary>
        public string CategoryPath
        {
            set { _categoryPath = value; }
            get { return _categoryPath; }
        }

        #endregion Model
    }
}