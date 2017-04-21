/**  版本信息模板在安装目录下，可自行修改。
* Shop_freefreight.cs
*
* 功 能： N/A
* 类 名： Shop_freefreight
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/10/22 16:32:56   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model.shop.Freight
{

    /// <summary>
    /// Shop_ProductsFreight:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class Shop_ProductsFreight
    {
        public Shop_ProductsFreight()
        { }
        #region Model
        private string _productcode;
        private string _sku;
        private decimal? _freight;
        private int? _modeid = 0;
        private string _eidtor;
        private DateTime? _addtime = DateTime.Now;
        private DateTime? _updatetime = DateTime.Now;


        public long ProductID { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        public string ProductCode
        {
            set { _productcode = value; }
            get { return _productcode; }
        }
        /// <summary>
        /// 商品规格编号
        /// </summary>
        public string SKU
        {
            set { _sku = value; }
            get { return _sku; }
        }
        /// <summary>
        /// 运费（-1表示不适用独立运费）
        /// </summary>
        public decimal? Freight
        {
            set { _freight = value; }
            get { return _freight; }
        }
        /// <summary>
        /// 配送方式编号（0表示未设置配送方式）
        /// </summary>
        public int? ModeId
        {
            set { _modeid = value; }
            get { return _modeid; }
        }
        /// <summary>
        /// 编辑者
        /// </summary>
        public string Eidtor
        {
            set { _eidtor = value; }
            get { return _eidtor; }
        }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime? AddTime
        {
            set { _addtime = value; }
            get { return _addtime; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime
        {
            set { _updatetime = value; }
            get { return _updatetime; }
        }
        #endregion Model

    }
}

