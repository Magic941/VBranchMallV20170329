using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.Model.CMS
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 内容审核状态枚举
        /// </summary>
        /// <remarks>状态》0:审核;1:草稿;2:未审核。</remarks>
        public enum ContentStateType
        {
            None = -1,
            /// <summary>
            /// 
            /// </summary>
            Approve = 0,
            /// <summary>
            /// 
            /// </summary>
            Draft = 1,
            /// <summary>
            /// 
            /// </summary>
            Unaudited = 2
        }
        /// <summary>
        /// 文章推荐类型
        /// </summary>
        public enum  ContentRec
        {
            None = -1,
            /// <summary>
            /// 推荐
            /// </summary>
            Recomend = 0,
            /// <summary>
            /// 热门
            /// </summary>
            Hot = 1,
            /// <summary>
            /// 醒目
            /// </summary>
            Color = 2,
            /// <summary>
            /// 置顶
            /// </summary>
            Top = 3
        }
        /// <summary>
        /// 文章推荐类型
        /// </summary>
        public enum CommentType
        {
            None = -1,
            /// <summary>
            /// 推荐
            /// </summary>
            Photo =1,
            /// <summary>
            /// 热门
            /// </summary>
            Video = 2,
            /// <summary>
            /// 醒目
            /// </summary>
            Content = 3
        
        }
    }
}
