using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.SNS
{
    /// <summary>
    /// 商品查询条件对象  Pagination
    /// </summary>
    public class ProductQuery
    {
        #region Model
        private string keywords;
        private int? _productid;
        //private string _productname;
        //private string _productdescription;
        private decimal? _maxprice;
        private decimal? _minprice;
        private int? _productsourceid;
        private int? _categoryid;
        private bool _istopcategory;
        private int? _createuserid;       
        private int? _isrecomend;
        private int? _status;        
        private string _sharedescription;        
        private int? _ownerproductid;
        private string _order;
        private string _tags;
        private int _querytype;
        private string _color;

        public string Keywords
        {
            get
            {
                return this.keywords;
            }
            set
            {
                this.keywords = value;
            }
        }
        /// <summary>
        /// 产品的类型
        /// </summary>
        public int? ProductID
        {
            set { _productid = value; }
            get { return _productid; }
        }
        /// <summary>
        /// 颜色
        /// </summary>
        public string Color
        {
            set { _color = value; }
            get { return _color; }
        }
        ////
        ///// <summary>
        ///// 产品的名称
        ///// </summary>
        //public string ProductName
        //{
        //    set { _productname = value; }
        //    get { return _productname; }
        //}
        ///// <summary>
        ///// 产品的描述
        ///// </summary>
        //public string ProductDescription
        //{
        //    set { _productdescription = value; }
        //    get { return _productdescription; }
        //}
        /// <summary>
        /// 产品的价格
        /// </summary>
        public decimal? MaxPrice
        {
            set { _maxprice = value; }
            get { return _maxprice; }
        }

        public decimal? MinPrice
        {
            set { _minprice = value; }
            get { return _minprice; }
        }
        /// <summary>
        /// 商品来源id(淘宝的,还是京东的)
        /// </summary>
        public int? ProductSourceID
        {
            set { _productsourceid = value; }
            get { return _productsourceid; }
        }
        /// <summary>
        /// 产品所属的分类
        /// </summary>
        public int? CategoryID
        {
            set { _categoryid = value; }
            get { return _categoryid; }
        }
        public int QueryType
        {
            set { _querytype = value; }
            get { return _querytype; }
        }
        ///// <summary>
        ///// 被别人喜欢的数量
        ///// </summary>
        //public int? FavCount
        //{
        //    set { _favcount = value; }
        //    get { return _favcount; }
        //}
        ///// <summary>
        ///// 申请团购的数量(预留字段)
        ///// </summary>
        //public int? GroupBuyCount
        //{
        //    set { _groupbuycount = value; }
        //    get { return _groupbuycount; }
        //}
        /// <summary>
        /// 创建人
        /// </summary>
        public int? CreateUserID
        {
            set { _createuserid = value; }
            get { return _createuserid; }
        }
        
        ///// <summary>
        ///// 浏览数量
        ///// </summary>
        //public int? PVCount
        //{
        //    set { _pvcount = value; }
        //    get { return _pvcount; }
        //}
        /// <summary>
        /// 是否推荐（预留字段）
        /// </summary>
        public int? IsRecomend
        {
            set { _isrecomend = value; }
            get { return _isrecomend; }
        }
        /// <summary>
        /// 状态（预留字段）
        /// </summary>
        public int? Status
        {
            set { _status = value; }
            get { return _status; }
        }
        public string Order
        {
            set { _order = value; }
            get { return _order; }
        }
       
        ///// <summary>
        ///// 评论的数量
        ///// </summary>
        //public int? CommentCount
        //{
        //    set { _commentcount = value; }
        //    get { return _commentcount; }
        //}
        ///// <summary>
        ///// 被转发的数量
        ///// </summary>
        //public int? ForwardedCount
        //{
        //    set { _forwardedcount = value; }
        //    get { return _forwardedcount; }
        //}
        /// <summary>
        /// 分享时分享者的说明
        /// </summary>
        public string ShareDescription
        {
            set { _sharedescription = value; }
            get { return _sharedescription; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? OwnerProductId
        {
            set { _ownerproductid = value; }
            get { return _ownerproductid; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Tags
        {
            set { _tags = value; }
            get { return _tags; }
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsTopCategory
        {
            set { _istopcategory = value; }
            get { return _istopcategory; }
        }
       
        #endregion Model
    }
}
