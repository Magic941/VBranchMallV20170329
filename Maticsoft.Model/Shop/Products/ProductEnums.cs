/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。 
//
// 文件名：DataProviderAction.cs
// 文件功能描述：修改、新增、删除 枚举
// 
// 创建标识：2012年6月12日 13:38:22 孙鹏
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/


namespace Maticsoft.Model.Shop.Products
{
    public enum DataProviderAction
    {
        None = -1,
        Create = 0,
        Update = 1,
        Delete = 2
    }

    /// <summary>
    /// -1:未审核 0:下架  1:上架 2:已删除
    /// </summary>
    public enum ProductSaleStatus
    {
        //DONE: 商品状态更正为 0:下架(仓库中)  1:上架 2:已删除 <BEN ADD 2012-06-29>
        UnCheck = -1,
        InStock = 0,
        OnSale = 1,
        Deleted = 2
    }


    /// <summary>
    /// 0:正常有分类  -1:无分类
    /// </summary>
    public enum ProductCategoryStatus
    {
        None = -1,
        Normal = 0
    }

    /// <summary>
    ///  0:单选  1：多选   2：自定义输入  3：规格
    /// </summary>
    public enum ProductAttributeModel
    {
        None = -1,
        One = 0,
        Any = 1,
        Input = 2,
        Specification = 3
    }

    /// <summary>
    /// 查询方式 0 ：属性 1：规格
    /// </summary>
    public enum SearchType
    {
        None = -1,
        ExtAttribute = 0,
        Specification = 1
    }

    /// <summary>
    /// 排序方式 0向下 1：向上
    /// </summary>
    public enum SwapSequenceIndex
    {
        None = -1,
        Down = 0,
        Up = 1
    }

    /// <summary>
    /// 商品状态 0：推荐 1热卖 2特价 3：最新,4:首页推荐,6：分享商品
    /// </summary>
    public enum ProductRecType
    {
        None = -1,
        /// <summary>
        /// 推荐
        /// </summary>
        Recommend = 0,
        /// <summary>
        /// 热卖（国产精品）
        /// </summary>
        Hot = 1,
        /// <summary>
        /// 特价（进口精品）
        /// </summary>
        Cheap = 2,
        /// <summary>
        /// 最新
        /// </summary>
        Latest = 3,
        /// <summary>
        /// 首页推荐
        /// </summary>
        IndexRec = 4,
        /// <summary>
        /// 首页楼层推荐
        /// </summary>
        IndexFloor = 5,
        /// <summary>
        /// 分享商品
        /// </summary>
        Share = 6
    }

 
}
