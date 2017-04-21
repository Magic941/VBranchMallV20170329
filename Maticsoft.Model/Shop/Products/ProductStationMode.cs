/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：ProductStationModes.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:28
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
    /// ProductStationModes:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class ProductStationMode
    {
        public ProductStationMode()
        { }
        #region Model
        private int _stationid;
        private long _productid;
        private int _displaysequence = 0;
        private int _type;
        private int _floor;
        public int Sort { get; set; }
        private int _GoodTypeID;

        /// <summary>
        /// 商品分类编号
        /// </summary>
        public int GoodTypeID
        {
            get { return _GoodTypeID; }
            set { _GoodTypeID = value; }
        }

        /// <summary>
        /// 楼层
        /// </summary>
        public int Floor
        {
            get { return _floor; }
            set { _floor = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int StationId
        {
            set { _stationid = value; }
            get { return _stationid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public long ProductId
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int DisplaySequence
        {
            set { _displaysequence = value; }
            get { return _displaysequence; }
        }
        /// <summary>
        /// 0:推荐 1:热卖 2:特价 3:最新
        /// </summary>
        public int Type
        {
            set { _type = value; }
            get { return _type; }
        }
        #endregion Model


    }
}

