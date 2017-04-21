using System;
using System.Collections.Generic;
namespace Maticsoft.Model.SNS
{
    /// <summary>
    /// 分享内容
    /// </summary>
    public class PostContent
    {
        public long TargetId { get; set; }
        public string TargetName { get; set; }
        public string TargetDescription { get; set; }
        public int TargetType { get; set; }
        public int CommentCount { get; set; }
        public int FavouriteCount { get; set; }
        public string ThumbImageUrl { get; set; }
        public decimal Price { get; set; }
        public string TopCommentsId { set; get; }

        public string StaticUrl = "";//静态化路径

        public List<Maticsoft.Model.SNS.Comments> CommentList = new List<Comments>();

        /// <summary>
        /// 数据类型
        /// 1:数据来自Photo表
        /// 2:数据来自产品表
        /// </summary>
        public int Type { get; set; }

        public static PostContent CreateInstance<T>(T target) where T : class, new()
        {
            PostContent postContent = new PostContent();
            #region 照片对象转换
            if (target is Photos)
            {
                Photos photo = target as Photos;
                postContent.TargetId = photo.PhotoID;
                postContent.TargetName = photo.PhotoName;
                postContent.TargetDescription = photo.Description;
                postContent.CommentCount = photo.CommentCount;
                postContent.FavouriteCount = photo.FavouriteCount;
                postContent.ThumbImageUrl = photo.ThumbImageUrl;
                postContent.Type = (int)EnumHelper.PostContentType.Photo;
                postContent.TargetType = photo.Type;
                postContent.TopCommentsId = photo.TopCommentsId;
            }
            #endregion

            #region 商品对象转换
            else if (target is Products)
            {
                Products product = target as Products;
                postContent.TargetId = product.ProductID;
                postContent.TargetName = product.ProductName;
                postContent.TargetDescription = product.ShareDescription;   //商品说明目前使用分享说明
                postContent.CommentCount = product.CommentCount;
                postContent.FavouriteCount = product.FavouriteCount;
                postContent.ThumbImageUrl = product.ThumbImageUrl;
                postContent.Type = (int)EnumHelper.PostContentType.Product;
                postContent.TopCommentsId = product.TopCommentsId;

                
                //postContent.TargetType = product.Type;
            }
            #endregion

            #region 视频对象转换 预留 暂不使用
            //else if (target is Videos)
            //{
            //    Videos video = target as Videos;
            //    postContent.TargetId = video.VideoID;
            //    postContent.TargetName = video.VideoName;
            //    postContent.CommentCount = video.CommentCount;
            //    postContent.FavouriteCount = video.FavouriteCount;
            //    postContent.ThumbImageUrl = video.ThumbImageUrl;
            //    postContent.Type = (int)EnumHelper.PostContentType.None;
            //}
            #endregion
            //不支持的类型
            else
            {
                throw new NotSupportedException();
            }
            return postContent;
        }
    }
}
