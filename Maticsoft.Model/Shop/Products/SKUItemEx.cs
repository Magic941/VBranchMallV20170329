/*----------------------------------------------------------------
// Copyright (C) 2012 动软卓越 版权所有。
//
// 文件名：SKUItems.cs
// 文件功能描述：
// 
// 创建标识： [Ben]  2012/06/11 20:36:32
// 修改标识：
// 修改描述：
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
namespace Maticsoft.Model.Shop.Products
{
    public partial class SKUItem
    {
        private string _valueStr;
        /// <summary>
        /// SKU自定义文本
        /// </summary>
        public string ValueStr
        {
            get { return _valueStr; }
            set { _valueStr = value; }
        }

        private string _imageUrl;
        /// <summary>
        /// SKU自定义图片
        /// </summary>
        public string ImageUrl
        {
            get { return _imageUrl; }
            set { _imageUrl = value; }
        }

        private long _productId;

        public long ProductId
        {
            get { return _productId; }
            set { _productId = value; }
        }

        private long _specId;

        public long SpecId
        {
            get { return _specId; }
            set { _specId = value; }
        }


        #region Shop_Attribute
        private string _attributename;
        private int _abDisplaysequence;
        private int _usagemode;
        private bool _useattributeimage;

        /// <summary>
        /// 属性名
        /// </summary>
        public string AttributeName
        {
            set { _attributename = value; }
            get { return _attributename; }
        }
        /// <summary>
        /// 属性排序
        /// </summary>
        public int AB_DisplaySequence
        {
            set { _abDisplaysequence = value; }
            get { return _abDisplaysequence; }
        }
        /// <summary>
        /// 0:单选 1:多选 2:自定义填写 3:规格
        /// </summary>
        public int UsageMode
        {
            set { _usagemode = value; }
            get { return _usagemode; }
        }
        /// <summary>
        /// 属性值是否是图片
        /// </summary>
        public bool UseAttributeImage
        {
            set { _useattributeimage = value; }
            get { return _useattributeimage; }
        }
        private bool _userDefinedPic;
        /// <summary>
        /// 是否允许SKU自定义图片
        /// </summary>
        public bool UserDefinedPic
        {
            get { return _userDefinedPic; }
            set { _userDefinedPic = value; }
        }
        #endregion Attributename


        #region Shop_AttributeValues
        private int _AV_displaysequence;
        private string _AV_valuestr;
        private string _AV_imageurl;
        /// <summary>
        /// 属性值排序
        /// </summary>
        public int AV_DisplaySequence
        {
            set { _AV_displaysequence = value; }
            get { return _AV_displaysequence; }
        }
        /// <summary>
        /// 属性值_文本
        /// </summary>
        public string AV_ValueStr
        {
            set { _AV_valuestr = value; }
            get { return _AV_valuestr; }
        }
        /// <summary>
        /// 属性值_图片
        /// </summary>
        public string AV_ImageUrl
        {
            set { _AV_imageurl = value; }
            get { return _AV_imageurl; }
        }
        #endregion Shop_AttributeValues
    }
}

