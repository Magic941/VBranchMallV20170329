
namespace Maticsoft.Model.SNS
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取的动态的类型
        /// </summary>
        /// <remarks>类型》0:全站的动态;1:好友的动态;2:自己的动态。</remarks>
        public enum PostType
        {
            None = -1,

            /// <summary>
            /// 全站动态
            /// </summary>
            All = 0,

            /// <summary>
            /// 关注
            /// </summary>
            Fellow = 1,

            /// <summary>
            /// 自身动态
            /// </summary>
            User = 2,

            /// <summary>
            /// 一条数据
            /// </summary>
            OnePost = 3,

            /// <summary>
            /// 提到我的
            /// </summary>
            ReferMe = 4,

            /// <summary>
            /// 互相关注
            /// </summary>
            EachOther = 5,

            /// <summary>
            /// 照片
            /// </summary>
            Photo = 6,

            /// <summary>
            /// 商品
            /// </summary>
            Product = 7,

            /// <summary>
            /// 视频
            /// </summary>
            Video = 8,
                 /// <summary>
            /// 长微博
            /// </summary>
            Blog = 9
        }

        /// <summary>
        /// 获取的动态的类型
        /// </summary>
        /// <remarks>类型》0:一般动态;1:图片动态;2:商品动态。</remarks>
        public enum PostContentType
        {
            None = -1,

            /// <summary>
            /// 一般动态
            /// </summary>
            Normal = 0,

            /// <summary>
            /// 图片动态
            /// </summary>
            Photo = 1,

            /// <summary>
            /// 商品动态
            /// </summary>
            Product = 2,
            /// <summary>
            /// 视频动态
            /// </summary>
            Video=3,
              /// <summary>
            /// 长微博
            /// </summary>
            Blog=4

        }


       


        /// <summary>
        /// 小组主题的操作
        /// </summary>
        /// <remarks>类型》0:推荐;1:取消推荐;2:删除。</remarks>
        public enum TopicOparationType
        {
            None = -1,

         
            /// <summary>
            /// 取消推荐
            /// </summary>
            CancellRecommend =0,
            /// <summary>
            /// 推荐
            /// </summary>
            Recommend = 1,

            /// <summary>
            /// 删除主题
            /// </summary>
            Delete = 2
        }
        /// <summary>
        /// 小组推荐
        /// </summary>
        /// <remarks>类型》0:不推荐;1:推荐。</remarks>
        public enum TopicRecommendType
        {
            None = -1,

            /// <summary>
            /// 不推荐
            /// </summary>
            NoneRecommend = 0,

            /// <summary>
            /// 
            /// </summary>
            Recommend = 1
        }
        /// <summary>
        /// 小组状态
        /// </summary>
        /// <remarks>状态：0 ：未审核 1：已审核 2：审核未通过</remarks>
        public enum GroupStatus
        {
            none = -1,

            /// <summary>
            /// 未审核
            /// </summary>
            UnCheck = 0,

            /// <summary>
            /// 已审核
            /// </summary>
            Checked = 1,

            /// <summary>
            /// 审核未通过
            /// </summary>
            CheckedUnPass = 2
        }
        /// <summary>
        /// 小组的推荐类型
        /// </summary>
        /// <remarks>状态：0 ：不推荐 1：推荐到小组首页 2：推荐为精选小组</remarks>
        public enum GroupRecommend
        {
            /// <summary>
            /// 不推荐
            /// </summary>
            None = 0,

            /// <summary>
            /// 推荐到小组首页
            /// </summary>
            Index = 1,

            /// <summary>
            /// 推荐为精选小组
            /// </summary>
            Selective = 2
        }

        /// <summary>
        /// 小组用户状态
        /// </summary>
        /// <remarks>状态：0 ：未审核 1：已审核 3：禁言</remarks>
        public enum GroupUserStatus
        {
            none = -1,

            /// <summary>
            /// 未审核
            /// </summary>
            UnCheck = 0,

            /// <summary>
            /// 已审核
            /// </summary>
            Checked = 1,

            /// <summary>
            /// 禁言
            /// </summary>
            ForbidSpeak = 2
        }
        /// <summary>
        /// 小组主题和回复状态
        /// </summary>
        /// <remarks>状态：0 ：未审核 1：已审核 2：审核未通过</remarks>
        public enum TopicStatus
        {
            none = -1,

            /// <summary>
            /// 未审核
            /// </summary>
            UnCheck = 0,

            /// <summary>
            /// 已审核
            /// </summary>
            Checked = 1,

            /// <summary>
            /// 审核未通过
            /// </summary>
            CheckedUnPass = 2
        } 
        /// <summary>
        /// 小组成员角色
        /// </summary>
        /// <remarks>0 为普通小组成员, 1为管理员, 2.超级管理员(组长)</remarks>
        public enum GroupUserRole
        {
            none = -1,

            /// <summary>
            /// 普通小组成员
            /// </summary>
            Normal = 0,

            /// <summary>
            /// 管理员
            /// </summary>
            Admin = 1,

            /// <summary>
            /// 超级管理员（小组长）
            /// </summary>
            Leader = 2
        }
  
        /// <summary>
        /// 获取的喜欢的类型
        /// </summary>
        /// <remarks>类型0:图片;1:商品</remarks>
        public enum FavoriteType
        {
            None = -1,

            /// <summary>
            /// 图片
            /// </summary>
            Photo = 0,

            /// <summary>
            /// 商品
            /// </summary>
            Product = 1
        }
        /// <summary>
        /// 获取的动态的类型
        /// </summary>
        /// <remarks>类型0:全站的动态;1:好友的动态;2:自己的动态。</remarks>
        public enum ImageType
        {
            None = -1,

            /// <summary>
            /// 照片类型
            /// </summary>
            Photo = 0,

            /// <summary>
            /// 商品类型
            /// </summary>
            Product = 1
        }

        /// <summary>
        /// 状态 0:未审核 1：已审核  2：审核未通过 3：分类未明确 4：分类已明确
        /// </summary>
        /// <remarks></remarks>
        public enum ProductStatus
        {
            None = -1,

            /// <summary>
            /// 未审核
            /// </summary>
            UnChecked = 0,

            /// <summary>
            /// 已审核
            /// </summary>
            AlreadyChecked = 1,

            /// <summary>
            /// 审核未通过
            /// </summary>
            CheckedPass = 2,

            /// <summary>
            /// 分类未明确
            /// </summary>
            CategoryUnDefined = 3,

            /// <summary>
            /// 分类已明确
            /// </summary>
            CategoryDefined = 4
        }

        /// <summary>
        /// 状态 0:不可用 1：可用
        /// </summary>
        /// <remarks></remarks>
        public enum Status
        {
            None = -1,

            /// <summary>
            /// 不可用
            /// </summary>
            Disabled = 0,

            /// <summary>
            /// 可用
            /// </summary>
            Enabled = 1
        }

        /// <summary>
        /// 举报内容的类型(0:动态或者1:图片或者2:商品)
        /// </summary>
        public enum TargetType
        {
            None = -1,
            /// <summary>
            /// 动态
            /// </summary>
            Normal = 0,

            /// <summary>
            /// 图片
            /// </summary>
            Photo = 1,

            /// <summary>
            /// 商品
            /// </summary>
            Product = 2,
        }

        /// <summary>
        /// 我的小组 0：小组动态1：我发表的:2：我回应:3：我收藏的
        /// </summary>
        public enum UserGroupType
        {
            None = -1,
            /// <summary>
            /// 我加入的小组的动态
            /// </summary>
            UserGroup = 0,
            /// <summary>
            /// 我发表的
            /// </summary>
            UserPostTopic = 1,
            /// <summary>
            /// 我回应的
            /// </summary>
            UserReply = 2,
            /// <summary>
            /// 我收藏的
            /// </summary>
            UserFav = 3,
        }

        /// <summary>
        /// 状态 0:一般 1：互相关注
        /// </summary>
        /// <remarks></remarks>
        public enum FansType
        {
            None = -1,

            /// <summary>
            /// 一般
            /// </summary>
            Normal = 0,

            /// <summary>
            /// 互相关注
            /// </summary>
            EachOher = 1
        }
        /// <summary>
        /// 状态0:晒货 1:搭配,2网购实拍3.群组分享的
        /// </summary>
        /// <remarks></remarks>
        public enum PhotoType
        {
            None = -1,

            /// <summary>
            /// 晒货
            /// </summary>
            ShareGoods = 0,

            /// <summary>
            /// 搭配
            /// </summary>
            Collocation = 1,
            /// <summary>
            /// 网购实拍
            /// </summary>
            NetBuyPhoto=2,
            /// <summary>
            /// 群组分享的
            /// </summary>
            Group = 3
        }

        /// <summary>
        /// 提到某人类型 0:动态 1：评论
        /// </summary>
        /// <remarks></remarks>
        public enum ReferType
        {
            None = -1,

            /// <summary>
            /// 动态类型
            /// </summary>
            Post = 0,

            /// <summary>
            /// 可用
            /// </summary>
            Comment = 1
        }
        /// <summary>
        /// 推荐类型 0:不推荐 1：推荐到首页 2：推荐到频道首页
        /// </summary>
        /// <remarks></remarks>
        public enum RecommendType
        {
            /// <summary>
            /// 不推荐
            /// </summary>
            None = 0,

            /// <summary>
            /// 推荐到首页
            /// </summary>
            Home = 1,

            /// <summary>
            /// 推荐到频道首页
            /// </summary>
            Channel = 2
        }

        /// <summary>
        /// 状态 0:未审核 1：已审核  2：审核未通过 3：分类未明确 4：分类已明确
        /// </summary>
        /// <remarks></remarks>
        public enum PhotoStatus
        {
            None = -1,

            /// <summary>
            /// 未审核
            /// </summary>
            UnChecked = 0,

            /// <summary>
            /// 已审核
            /// </summary>
            AlreadyChecked = 1,
            /// <summary>
            /// 审核未通过
            /// </summary>
            CheckedPass = 2,

            /// <summary>
            /// 分类未明确
            /// </summary>
            CategoryUnDefined = 3,

            /// <summary>
            /// 分类已明确
            /// </summary>
            CategoryDefined = 4
        }
        /// <summary>
        /// 状态 0:未审核 1：已审核  2：审核未通过 3.已删除
        /// </summary>
        /// <remarks></remarks>
        public enum PostStatus
        {
            None = -1,
            /// <summary>
            /// 未审核
            /// </summary>
            UnChecked = 0,
            /// <summary>
            /// 已审核
            /// </summary>
            AlreadyChecked = 1,
            /// <summary>
            /// 审核未通过
            /// </summary>
            CheckedUnPass = 2,
            /// <summary>
            /// 已删除
            /// </summary>
            AlreadyDel = 3
        }

        /// <summary>
        /// 评论目标的类型 0：是一般动态的评论 1：照片的评论 2 是商品的评论 3. 专辑的评论
        /// </summary>
        public enum CommentType
        {
            None = -1,

            /// <summary>
            /// 一般动态
            /// </summary>
            Normal = 0,

            /// <summary>
            /// 照片
            /// </summary>
            Photo = 1,

            /// <summary>
            /// 商品
            /// </summary>
            Product = 2,
            /// <summary>
            /// 专辑
            /// </summary>
            Album = 3,
              /// <summary>
            /// 博客
            /// </summary>
            Blog = 4
        }

        /// <summary>
        /// 网站对于的id 3：淘宝网 4：京东 5，当当
        /// </summary>
        public enum WebSiteType
        {
            None = -1,

            /// <summary>
            /// 淘宝
            /// </summary>
            TaoBao = 3,

            /// <summary>
            /// 京东
            /// </summary>
            JingDong = 4,

            /// <summary>
            /// 当当
            /// </summary>
            DangDang = 5
        }

        /// <summary>
        /// 查询的类型 0 数量 1.列表
        /// </summary>
        public enum QueryType
        {
            None = -1,

            /// <summary>
            /// 数量
            /// </summary>
            Count = 0,

            /// <summary>
            /// 列表
            /// </summary>
            List = 1
        }

        /// <summary>
        /// 提示信息的类型 0 系统消息 1.私信 2.@提到 3.评论
        /// </summary>
        public enum MsgType
        {
            None = -1,

            /// <summary>
            /// 系统消息
            /// </summary>
            System = 0,

            /// <summary>
            /// 私信
            /// </summary>
            Private = 1,
            /// <summary>
            /// @提到
            /// </summary>
            Refer = 2,
            /// <summary>
            /// 评论
            /// </summary>
            Comment =3,
        }
        ///// <summary>
        ///// 网站对于的id 3：淘宝网 4：京东 5，当当
        ///// </summary>
        //public enum SequenceType
        //{
        //    None = -1,
        //    /// <summary>
        //    /// 淘宝
        //    /// </summary>
        //    taobao = 3,
        //    /// <summary>
        //    /// 照片
        //    /// </summary>
        //    jingdong = 4,
        //    /// <summary>
        //    /// 商品
        //    /// </summary>
        //    dangdang = 5
        //}


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
        /// 排序方式 0向下 1：向上
        /// </summary>
        public enum NavigationType
        {
            Default = 0,
            TufenXiang = 1
        }
        /// <summary>
        /// 排序方式 0向下 1：向上
        /// </summary>
        public enum FLinkType
        {
            Image = 0,
            Text = 1
        }
    }
}
