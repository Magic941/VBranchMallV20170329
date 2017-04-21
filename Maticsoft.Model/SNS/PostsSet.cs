using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.SNS
{
    /// <summary>
    /// 分享设置
    /// </summary>
   public class PostsSet
   {
       public PostsSet()
        { }
        #region Model
        private bool _narmal_pricture=true;
        private bool _normal_video = true;
        private bool _normal_audio = true;
        private bool _pricture = true;
        private bool _product=true;
        private bool _posttype_all =true;
        private bool _posttype_fellow=true;
        private bool _posttype_user=true;
        private bool _posttype_eachother=true;
        private bool _posttype_referme=true;
        private bool _blog = true;
        private bool _customproduct = true;
        private bool _taoproduct = true;

        /// <summary>
        /// 一般微博图片上传图片是否支持
        /// </summary>
        public bool _Narmal_Pricture
        {
            set { _narmal_pricture = value; }
            get { return _narmal_pricture; }
        }
        /// <summary>
        /// 一般微博图片上传视频是否支持
        /// </summary>
        public bool _Narmal_Video
        {
            set { _normal_video = value; }
            get { return _normal_video; }
        }
        /// <summary>
        /// 一般微博图片上传音乐是否支持
        /// </summary>
        public bool _Narmal_Audio
        {
            set { _normal_audio = value; }
            get { return _normal_audio; }
        }
        /// <summary>
        ///是否有上传图片的模块（和微博评级）
        /// </summary>
        public bool _Picture
        {
            set { _pricture = value; }
            get { return _pricture; }
        }
        /// <summary>
        ///是否有上传商品的模块（和微博评级）
        /// </summary>
        public bool _Product
        {
            set { _product = value; }
            get { return _product; }
        }

        public bool CustomProduct
        {
            set { _customproduct = value; }
            get { return _customproduct; }
        }

        public bool TaoProduct
        {
            set { _taoproduct = value; }
            get { return _taoproduct; }
        }
        /// <summary>
        ///是否显示全部动态
        /// </summary>
        public bool _PostType_All
        {
            set { _posttype_all = value; }
            get { return _posttype_all; }
        }
        /// <summary>
        ///是否显示我关注的
        /// </summary>
        public bool _PostType_Fellow
        {
            set { _posttype_fellow = value; }
            get { return _posttype_fellow; }
        }
        /// <summary>
        ///是否显示我发表的
        /// </summary>
        public bool _PostType_User
        {
            set { _posttype_user = value; }
            get { return _posttype_user; }
        }
        /// <summary>
        ///是否显示互相关注的
        /// </summary>
        public bool _PostType_EachOther
        {
            set { _posttype_eachother = value; }
            get { return _posttype_eachother; }
        }
        /// <summary>
        ///是否显示提到（@）我的
        /// </summary>
        public bool _PostType_ReferMe
        {
            set { _posttype_referme = value; }
            get { return _posttype_referme; }
        }

        /// <summary>
        ///是否显示长微博
        /// </summary>
        public bool _Blog
        {
            set { _blog = value; }
            get { return _blog; }
        }
        #endregion Model

    }
}
