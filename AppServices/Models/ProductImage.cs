using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppServices.Models
{
    public  class ProductImage
    {
        public ProductImage()
        { }
        #region Model
        private int _productimageid;
        private long _productid;
        private string _imageurl;
        private string _thumbnailurl1;
        private string _thumbnailurl2;
        private string _thumbnailurl3;
        private string _thumbnailurl4;
        private string _thumbnailurl5;
        private string _thumbnailurl6;
        private string _thumbnailurl7;
        private string _thumbnailurl8;
        /// <summary>
        /// 
        /// </summary>
        public int ProductImageId
        {
            set { _productimageid = value; }
            get { return _productimageid; }
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
        public string ImageUrl
        {
            set { _imageurl = value; }
            get { return _imageurl; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl1
        {
            set { _thumbnailurl1 = value; }
            get { return _thumbnailurl1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl2
        {
            set { _thumbnailurl2 = value; }
            get { return _thumbnailurl2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl3
        {
            set { _thumbnailurl3 = value; }
            get { return _thumbnailurl3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl4
        {
            set { _thumbnailurl4 = value; }
            get { return _thumbnailurl4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl5
        {
            set { _thumbnailurl5 = value; }
            get { return _thumbnailurl5; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl6
        {
            set { _thumbnailurl6 = value; }
            get { return _thumbnailurl6; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl7
        {
            set { _thumbnailurl7 = value; }
            get { return _thumbnailurl7; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl8
        {
            set { _thumbnailurl8 = value; }
            get { return _thumbnailurl8; }
        }
        #endregion Model

    }
}