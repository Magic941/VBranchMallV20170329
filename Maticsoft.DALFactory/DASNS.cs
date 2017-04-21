namespace Maticsoft.DALFactory
{
    public sealed class DASNS : DataAccessBase
    {
        /// <summary>
        /// 创建AlbumType数据层接口。专辑类型
        /// </summary>
        public static Maticsoft.IDAL.SNS.IAlbumType CreateAlbumType()
        {
            string ClassNamespace = AssemblyPath + ".SNS.AlbumType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IAlbumType)objType;
        }

        /// <summary>
        /// 创建Categories数据层接口。商品类型表
        /// </summary>
        public static Maticsoft.IDAL.SNS.ICategories CreateCategories()
        {
            string ClassNamespace = AssemblyPath + ".SNS.Categories";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.ICategories)objType;
        }

        /// <summary>
        /// 创建淘宝数据分类数据层接口。商品类型表
        /// </summary>
        public static Maticsoft.IDAL.SNS.ICategorySource CreateCategorySource()
        {
            string ClassNamespace = AssemblyPath + ".SNS.CategorySource";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.ICategorySource)objType;
        }

        /// <summary>
        /// 创建Comments数据层接口。评论表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IComments CreateComments()
        {
            string ClassNamespace = AssemblyPath + ".SNS.Comments";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IComments)objType;
        }

        /// <summary>
        /// 创建FellowTopics数据层接口。用户关注的话题表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IFellowTopics CreateFellowTopics()
        {
            string ClassNamespace = AssemblyPath + ".SNS.FellowTopics";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IFellowTopics)objType;
        }

        /// <summary>
        /// 创建Groups数据层接口。小组表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IGroups CreateGroups()
        {
            string ClassNamespace = AssemblyPath + ".SNS.Groups";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IGroups)objType;
        }

        /// <summary>
        /// 创建GroupTopicFav数据层接口。小组主题收藏表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IGroupTopicFav CreateGroupTopicFav()
        {
            string ClassNamespace = AssemblyPath + ".SNS.GroupTopicFav";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IGroupTopicFav)objType;
        }

        /// <summary>
        /// 创建GroupTopicReply数据层接口。主题回复表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IGroupTopicReply CreateGroupTopicReply()
        {
            string ClassNamespace = AssemblyPath + ".SNS.GroupTopicReply";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IGroupTopicReply)objType;
        }

        /// <summary>
        /// 创建GroupTopics数据层接口。小组话题表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IGroupTopics CreateGroupTopics()
        {
            string ClassNamespace = AssemblyPath + ".SNS.GroupTopics";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IGroupTopics)objType;
        }

        /// <summary>
        /// 创建GroupUsers数据层接口。小组人员表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IGroupUsers CreateGroupUsers()
        {
            string ClassNamespace = AssemblyPath + ".SNS.GroupUsers";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IGroupUsers)objType;
        }

        /// <summary>
        /// 创建GuestBook数据层接口。留言表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IGuestBook CreateGuestBook()
        {
            string ClassNamespace = AssemblyPath + ".SNS.GuestBook";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IGuestBook)objType;
        }

        /// <summary>
        /// 创建SearchWordTop数据层接口。热搜此
        /// </summary>
        public static Maticsoft.IDAL.SNS.ISearchWordTop CreateSearchWordTop()
        {
            string ClassNamespace = AssemblyPath + ".SNS.SearchWordTop";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.ISearchWordTop)objType;
        }

        /// <summary>
        /// 创建OnLineShopPhoto数据层接口。网购实拍表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IOnLineShopPhoto CreateOnLineShopPhoto()
        {
            string ClassNamespace = AssemblyPath + ".SNS.OnLineShopPhoto";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IOnLineShopPhoto)objType;
        }

        /// <summary>
        /// 创建Photos数据层接口。照片表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IPhotos CreatePhotos()
        {
            string ClassNamespace = AssemblyPath + ".SNS.Photos";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IPhotos)objType;
        }

        /// <summary>
        /// 创建Posts数据层接口。动态表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IPosts CreatePosts()
        {
            string ClassNamespace = AssemblyPath + ".SNS.Posts";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IPosts)objType;
        }

        /// <summary>
        /// 创建PostsTopics数据层接口。动态话题表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IPostsTopics CreatePostsTopics()
        {
            string ClassNamespace = AssemblyPath + ".SNS.PostsTopics";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IPostsTopics)objType;
        }

        /// <summary>
        /// 创建Products数据层接口。商品表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IProducts CreateProducts()
        {
            string ClassNamespace = AssemblyPath + ".SNS.Products";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IProducts)objType;
        }

        /// <summary>
        /// 创建ProductSources数据层接口。商品来源表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IProductSources CreateProductSources()
        {
            string ClassNamespace = AssemblyPath + ".SNS.ProductSources";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IProductSources)objType;
        }

        /// <summary>
        /// 创建ReferUsers数据层接口。提到某人的记录表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IReferUsers CreateReferUsers()
        {
            string ClassNamespace = AssemblyPath + ".SNS.ReferUsers";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IReferUsers)objType;
        }

        /// <summary>
        /// 创建Report数据层接口。举报表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IReport CreateReport()
        {
            string ClassNamespace = AssemblyPath + ".SNS.Report";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IReport)objType;
        }

        /// <summary>
        /// 创建ReportType数据层接口。举报类型表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IReportType CreateReportType()
        {
            string ClassNamespace = AssemblyPath + ".SNS.ReportType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IReportType)objType;
        }

        /// <summary>
        /// 创建HotWords数据层接口。搜素关键字表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IHotWords CreateHotWords()
        {
            string ClassNamespace = AssemblyPath + ".SNS.HotWords";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IHotWords)objType;
        }

        /// <summary>
        /// 创建SearchWordLog数据层接口。搜索日志表
        /// </summary>
        public static Maticsoft.IDAL.SNS.ISearchWordLog CreateSearchWordLog()
        {
            string ClassNamespace = AssemblyPath + ".SNS.SearchWordLog";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.ISearchWordLog)objType;
        }

        /// <summary>
        /// 创建Star数据层接口。达人表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IStar CreateStar()
        {
            string ClassNamespace = AssemblyPath + ".SNS.Star";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IStar)objType;
        }

        /// <summary>
        /// 创建StarRank数据层接口。达人排行表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IStarRank CreateStarRank()
        {
            string ClassNamespace = AssemblyPath + ".SNS.StarRank";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IStarRank)objType;
        }

        /// <summary>
        /// 创建StarType数据层接口。达人类型表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IStarType CreateStarType()
        {
            string ClassNamespace = AssemblyPath + ".SNS.StarType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IStarType)objType;
        }

        /// <summary>
        /// 创建Tags数据层接口。具体标签表
        /// </summary>
        public static Maticsoft.IDAL.SNS.ITags CreateTags()
        {
            string ClassNamespace = AssemblyPath + ".SNS.Tags";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.ITags)objType;
        }

        /// <summary>
        /// 创建TagType数据层接口。标签的类型 如 材质
        /// </summary>
        public static Maticsoft.IDAL.SNS.ITagType CreateTagType()
        {
            string ClassNamespace = AssemblyPath + ".SNS.TagType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.ITagType)objType;
        }

        /// <summary>
        /// 创建UserAlbumDetail数据层接口。用户专辑详情表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IUserAlbumDetail CreateUserAlbumDetail()
        {
            string ClassNamespace = AssemblyPath + ".SNS.UserAlbumDetail";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IUserAlbumDetail)objType;
        }

        /// <summary>
        /// 创建UserAlbums数据层接口。用户专辑表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IUserAlbums CreateUserAlbums()
        {
            string ClassNamespace = AssemblyPath + ".SNS.UserAlbums";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IUserAlbums)objType;
        }

        /// <summary>
        /// 创建UserAlbumsType数据层接口。用户专辑类型
        /// </summary>
        public static Maticsoft.IDAL.SNS.IUserAlbumsType CreateUserAlbumsType()
        {
            string ClassNamespace = AssemblyPath + ".SNS.UserAlbumsType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IUserAlbumsType)objType;
        }

        /// <summary>
        /// 创建UserFavAlbum数据层接口。用户关注专辑表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IUserFavAlbum CreateUserFavAlbum()
        {
            string ClassNamespace = AssemblyPath + ".SNS.UserFavAlbum";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IUserFavAlbum)objType;
        }

        /// <summary>
        /// 创建UserFavourite数据层接口。用户的喜欢表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IUserFavourite CreateUserFavourite()
        {
            string ClassNamespace = AssemblyPath + ".SNS.UserFavourite";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IUserFavourite)objType;
        }

        /// <summary>
        /// 创建UserShip数据层接口。用户的喜欢表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IUserShip CreateUserShip()
        {
            string ClassNamespace = AssemblyPath + ".SNS.UserShip";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IUserShip)objType;
        }

        /// <summary>
        /// 创建UserShipCategories数据层接口。用户的喜欢表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IUserShipCategories CreateUserShipCategories()
        {
            string ClassNamespace = AssemblyPath + ".SNS.UserShipCategories";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IUserShipCategories)objType;
        }

        /// <summary>
        /// 创建Videos数据层接口。照片表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IVideos CreateVideos()
        {
            string ClassNamespace = AssemblyPath + ".SNS.Videos";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IVideos)objType;
        }

        /// <summary>
        /// 创建VisiteLogs数据层接口。访问日志表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IVisiteLogs CreateVisiteLogs()
        {
            string ClassNamespace = AssemblyPath + ".SNS.VisiteLogs";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IVisiteLogs)objType;
        }


        /// <summary>
        /// 创建GroupTags数据层接口。小组标签表
        /// </summary>
        public static Maticsoft.IDAL.SNS.IGroupTags CreateGroupTags()
        {
            string ClassNamespace = AssemblyPath + ".SNS.GroupTags";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IGroupTags)objType;
        }

        /// <summary>
        /// 创建PhotoTags数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.SNS.IPhotoTags CreatePhotoTags()
        {
            string ClassNamespace = AssemblyPath + ".SNS.PhotoTags";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IPhotoTags)objType;
        }

        /// <summary>
        /// 创建GradeConfig数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.SNS.IGradeConfig CreateGradeConfig()
        {
            string ClassNamespace = AssemblyPath + ".SNS.GradeConfig";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IGradeConfig)objType;
        }

        /// <summary>
        /// 创建GradeConfig数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.SNS.IUserBlog CreateUserBlog()
        {
            string ClassNamespace = AssemblyPath + ".SNS.UserBlog";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SNS.IUserBlog)objType;
        }
    }
}