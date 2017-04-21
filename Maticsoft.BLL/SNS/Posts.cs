/**
* Posts.cs
*
* 功 能： N/A
* 类 名： Posts
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/9/12 20:14:47   N/A    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.BLL.Members;
using Maticsoft.Common;
using Maticsoft.Model.SNS;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.SNS;
using System.Linq;
using System.Text.RegularExpressions;
using Maticsoft.TaoBao;
using Maticsoft.TaoBao.Request;
using Maticsoft.TaoBao.Response;
namespace Maticsoft.BLL.SNS
{
    /// <summary>
    /// 分享动态
    /// </summary>
    public partial class Posts
    {
        private readonly IPosts dal = DASNS.CreatePosts();
        public Posts()
        { }
        #region  BasicMethod



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.Model.SNS.Posts model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.Model.SNS.Posts model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int PostID)
        {

            return dal.Delete(PostID);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string PostIDlist)
        {
            return dal.DeleteList(Common.Globals.SafeLongFilter(PostIDlist,0) );
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.Model.SNS.Posts GetModel(int PostID)
        {

            return dal.GetModel(PostID);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.Model.SNS.Posts GetModelByCache(int PostID)
        {

            string CacheKey = "PostsModel-" + PostID;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(PostID);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.Model.SNS.Posts)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Posts> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.Model.SNS.Posts> DataTableToList(DataTable dt)
        {
            List<Maticsoft.Model.SNS.Posts> modelList = new List<Maticsoft.Model.SNS.Posts>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.Model.SNS.Posts model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

        #region  ExtensionMethod

        /// <summary>
        /// 个人中心的动态
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <param name="StartIndex">开始位置</param>
        /// <param name="EndIndex">结束位置</param>
        /// <param name="Type">对应动态的类型</param>
        /// <returns></returns>
        public List<ViewModel.SNS.Posts> GetPostByType(int UserId, int StartIndex, int EndIndex, Maticsoft.Model.SNS.EnumHelper.PostType Type, int PostId,bool includeProduct=true)
        {
            List<Maticsoft.Model.SNS.Posts> Posts = new List<Model.SNS.Posts>();
            List<Maticsoft.Model.SNS.Posts> OrigPosts = new List<Model.SNS.Posts>();
            Dictionary<int, Maticsoft.Model.SNS.Posts> OrigPostsDic = new Dictionary<int, Model.SNS.Posts>();
            List<ViewModel.SNS.Posts> PostResult = new List<ViewModel.SNS.Posts>();
            string QueryCondition = "";
            string OrigCondition = "";
            ///分页的到对应类型的动态（不包含被转发的情况）
            switch (Type)
            {
                case EnumHelper.PostType.All:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + "";
                    break;
                case EnumHelper.PostType.Fellow:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId in (SELECT  PassiveUserID  FROM  SNS_UserShip WHERE  ActiveUserID=" + UserId + " UNION SELECT " + UserId + ")";
                    break;
                case EnumHelper.PostType.User:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId=" + UserId + "";
                    break;
                case EnumHelper.PostType.OnePost:
                    QueryCondition = " PostID=" + PostId + "";
                    break;
                case EnumHelper.PostType.ReferMe:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and PostID in (SELECT  TagetID FROM SNS_ReferUsers  WHERE  ReferUserID=" + UserId + " and Type=0)";
                    break;
                case EnumHelper.PostType.EachOther:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId in ( SELECT PassiveUserID  FROM  SNS_UserShip WHERE  ActiveUserID=" + UserId + " and Type=1)";
                    break;
                case EnumHelper.PostType.Photo:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId=" + UserId + " and Type=" + (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Photo + "";
                    break;
                case EnumHelper.PostType.Product:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId=" + UserId + " and Type=" + (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product + "";
                    break;
                case EnumHelper.PostType.Video:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId=" + UserId + " and VideoUrl is not null";
                    break;
                case EnumHelper.PostType.Blog:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId=" + UserId + " and Type=" + (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Blog;
                    break;
                default:
                    QueryCondition = "Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + "";
                    break;
            }
            if (!includeProduct)
            {
                QueryCondition += " and Type<>" + (int) Maticsoft.Model.SNS.EnumHelper.PostContentType.Product;
            }
            Posts = DataTableToList(GetListByPage(QueryCondition, "PostId Desc", StartIndex, EndIndex).Tables[0]);
            // 如果是转发的情况，得到全部被转发动态的id
            var OrigIdList = (from item in Posts where item.OriginalID != 0 select item.OriginalID).Distinct().ToArray();
            string OrigIdString = string.Join(",", OrigIdList);
            ///查询被转发的动态
            if (!string.IsNullOrEmpty(OrigIdString))
            {
                OrigCondition = "PostId in(" + OrigIdString + ")";
                OrigPosts = GetModelList(OrigCondition);
            }
            //放到一个键值对当中（取值好取）
            foreach (Maticsoft.Model.SNS.Posts Post in OrigPosts)
            {
                OrigPostsDic.Add(Post.PostID, Post);
            }
            ///循环得到动态和被转发的动态
            foreach (Maticsoft.Model.SNS.Posts Post in Posts)
            {
                ViewModel.SNS.Posts Po = new ViewModel.SNS.Posts();
                Post.Description = ViewModel.ViewModelBase.RegexNickName(Post.Description);
                Po.Post = Post;
                if (OrigPostsDic.ContainsKey(Post.OriginalID))
                {
                    Po.OrigPost = OrigPostsDic[Post.OriginalID];
                    Po.OrigPost.Description = ViewModel.ViewModelBase.RegexNickName(Po.OrigPost.Description);
                }
                PostResult.Add(Po);
            }
            if (Type == EnumHelper.PostType.ReferMe)
            {
                Maticsoft.BLL.SNS.ReferUsers ReferBll = new ReferUsers();
                ReferBll.UpdateReferStateToRead(UserId, (int)Maticsoft.Model.SNS.EnumHelper.ReferType.Post);
            }
            return PostResult;
        }
        /// <summary>
        /// 重构成分页或相应的动态
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="StartIndex"></param>
        /// <param name="EndIndex"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public List<ViewModel.SNS.Posts> GetPostByType(int UserId, int StartIndex, int EndIndex, Maticsoft.Model.SNS.EnumHelper.PostType Type,bool  includeProduct=true)
        {
            return GetPostByType(UserId, StartIndex, EndIndex, Type, 0, includeProduct);
        }
        /// <summary> 
        /// 获取一条转发动态的的数据（重构）
        /// </summary>
        /// <param name="PostId"></param>
        /// <returns></returns>
        public List<ViewModel.SNS.Posts> GetForPostByPostId(int PostId,bool includeProduct)
        {
            return GetPostByType(0, 0, 1, Maticsoft.Model.SNS.EnumHelper.PostType.OnePost, PostId, includeProduct);
        }

        /// <summary>
        ///发回相应的类型的数据个数，用于分页用
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="PostType"></param>
        /// <returns></returns>
        public int GetCountByPostType(int UserId, Maticsoft.Model.SNS.EnumHelper.PostType PostType, bool includeProduct)
        {
            string QueryCondition = "";
            int Count;
            switch (PostType)
            {
                case EnumHelper.PostType.All:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + "";
                    break;
                case EnumHelper.PostType.Fellow:
                    QueryCondition = "Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId in ( SELECT  PassiveUserID  FROM  SNS_UserShip WHERE  ActiveUserID=" + UserId + " UNION SELECT " + UserId + ")";
                    break;
                case EnumHelper.PostType.User:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId=" + UserId + "";
                    break;
                case EnumHelper.PostType.ReferMe:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and PostID in (SELECT  TagetID FROM SNS_ReferUsers  WHERE  ReferUserID=" + UserId + " and Type=0)";
                    break;
                case EnumHelper.PostType.EachOther:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId in (SELECT PassiveUserID  FROM SNS_UserShip WHERE  ActiveUserID=" + UserId + " and Type=1)";
                    break;
                case EnumHelper.PostType.Photo:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId=" + UserId + " and Type=" + (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Photo + "";
                    break;
                case EnumHelper.PostType.Product:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId=" + UserId + " and Type=" + (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product + "";
                    break;
                case EnumHelper.PostType.Video:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId=" + UserId + " and VideoUrl is not null";
                    break;
                case EnumHelper.PostType.Blog:
                    QueryCondition = " Status=" + (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyChecked + " and CreatedUserId=" + UserId + " and Type=" + (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Blog ;
                    break;
                default:
                    QueryCondition = "";
                    break;
            }
            if (!includeProduct)
            {
                QueryCondition += " and Type<>" + (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product;
            }
            Count = GetRecordCount(QueryCondition);
            return Count;

        }
        /// <summary>
        /// 转发微博，提到某人功能的实现
        /// </summary>
        /// <param name="PostContent">转发时附加的内容</param>
        /// <param name="OrigType">原始动态的类型，是微博还是照片还是商品</param>
        /// <param name="Origid">原始动态的id</param>
        /// <param name="ForWardid">直接被转发者id</param>
        /// <param name="OrigUserId">原始动态创建者id</param>
        /// <param name="OrigNickName">原始创建者的昵称</param>
        /// <param name="CurrentUserID">当前用户的id</param>
        /// <param name="CurrentNickName">当前用户的昵称</param>
        /// <param name="UserIp">当前用户的ip</param>
        /// <returns></returns>
        public int PostForWard(string PostContent, int Origid, int ForWardid, int OrigUserId, string OrigNickName, int CurrentUserID, string CurrentNickName, string UserIp)
        {
            Maticsoft.BLL.Members.Users UserBll = new Members.Users();
            Maticsoft.BLL.SNS.ReferUsers ReferBll = new ReferUsers();
            Maticsoft.Model.SNS.ReferUsers ReferModel = new Model.SNS.ReferUsers();
            Maticsoft.Model.SNS.Posts PostModel = new Model.SNS.Posts();
            PostModel.CreatedDate = DateTime.Now;
            PostModel.Description = PostContent;
            PostModel.CreatedNickName = CurrentNickName;
            PostModel.CreatedUserID = CurrentUserID;
            PostModel.ForwardedID = ForWardid;
            PostModel.HasReferUsers = PostContent.Contains('@') ? true : false;
            PostModel.OriginalID = (Origid == 0 ? ForWardid : Origid);
            PostModel.UserIP = UserIp;
            PostModel.Status = (int)Maticsoft.Model.SNS.EnumHelper.Status.Enabled;

            ///增加相应的转发动态，同时直接被转发动态id对应的转发数量+1，原始的动态id对应的转发数量也+1，同时也要考虑相等的情况，则表明你是原始动态的直接转发者
            int PostID = AddForwardPost(PostModel);
            //转发时附加内容里提到某人功能的实现
            #region 通过正则匹配提到某人
            //MatchCollection matches = Regex.Matches(PostContent, @"@(.*?).\s");
            //foreach (Match item in matches)
            //{
            //    string NickName = item.Groups[1].Value;
            //    if ((ReferUserID = UserBll.GetUserIdByNickName(NickName)) > 0)
            //    {
            //        ReferModel.CreatedDate = DateTime.Now;
            //        ReferModel.IsRead = false;
            //        ReferModel.ReferUserID = ReferUserID;
            //        ReferModel.ReferNickName = NickName;
            //        ReferModel.Type = (int)Maticsoft.Model.SNS.EnumHelper.ReferType.Post;
            //        ReferModel.TagetID = PostID;
            //        ReferBll.Add(ReferModel);
            //    }
            //} 
            ReferBll.AddEx(PostContent, (int)Maticsoft.Model.SNS.EnumHelper.ReferType.Post, PostID);
            #endregion
            //下面是给@原动态作者的通知记录
            ReferModel.CreatedDate = DateTime.Now;
            ReferModel.IsRead = false;
            ReferModel.ReferUserID = OrigUserId;
            ReferModel.ReferNickName = OrigNickName;
            ReferModel.Type = (int)Maticsoft.Model.SNS.EnumHelper.ReferType.Post;
            ReferModel.TagetID = PostID;
            ReferBll.Add(ReferModel);
            return PostID;
        }

        /// <summary>
        /// 更新转发的次数
        /// </summary>
        /// <param name="StrWhere"></param>
        /// <returns></returns>
        public int UpdateForwardCount(string StrWhere)
        {
            return dal.UpdateForwardCount(StrWhere);
        }

        public int AddForwardPost(Maticsoft.Model.SNS.Posts model)
        {
            int PostID = 0;
            Maticsoft.BLL.SNS.ReferUsers referBll = new ReferUsers();
            //进行敏感字过滤
            if (Maticsoft.BLL.Settings.FilterWords.ContainsModWords(model.Description))
            {
                model.Status = (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
            }
            else
            {
                model.Description = Maticsoft.BLL.Settings.FilterWords.ReplaceWords(model.Description);
            }
            PostID = dal.AddForwardPost(model);
            ///如果内容中有提到某人则进行记录
          
            referBll.AddEx(model.Description, (int)Maticsoft.Model.SNS.EnumHelper.ReferType.Post, PostID, model.CreatedNickName);
            return PostID;

        }

        public Maticsoft.Model.SNS.Posts AddPost(Maticsoft.Model.SNS.Posts Post, int AblumId, long Pid, int PhotoCateId, string PhotoAddress = "", string MapLng = "", string MapLat = "", bool CreatePost = true)
        {

            Maticsoft.Model.SNS.Products PModel = new Model.SNS.Products();
            Maticsoft.BLL.SNS.Tags tagBll = new Tags();
            if (Post.Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product)
            {
                //AblumDetailType = (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product;
                ITopClient client = BLL.SNS.TaoBaoConfig.GetTopClient();
                Maticsoft.BLL.SNS.CategorySource CateBll = new Maticsoft.BLL.SNS.CategorySource();
                Maticsoft.TaoBao.Domain.Item Item = new Maticsoft.TaoBao.Domain.Item();
             
                    ItemGetRequest reqEx = new ItemGetRequest();
                    reqEx.Fields = "num_iid,title,price,num_iid,title,cid,nick,desc,price,item_img.url,click_url,shop_click_url,num,props_name,detail_url,pic_url";
                    reqEx.NumIid = Pid;
                    ItemGetResponse responseEx = client.Execute(reqEx);
                    Item = responseEx.Item;
                    PModel.ProductUrl = Item.DetailUrl;

                var cateModel = CateBll.GetModel(3, Convert.ToInt32(Item.Cid));
                PModel.CategoryID = cateModel != null ? cateModel.SnsCategoryId : 0;
                PModel.NormalImageUrl = Item.PicUrl;
                PModel.ThumbImageUrl = Item.PicUrl + "_300x300.jpg";
                PModel.Price = Common.Globals.SafeDecimal(Item.Price, 0);
                PModel.ProductID = Pid;
                PModel.ProductName = Item.Title;
                Post.ProductName = PModel.ProductName;
                PModel.ProductSourceID = (int)Maticsoft.Model.SNS.EnumHelper.WebSiteType.TaoBao;
                PModel.CreatedDate = DateTime.Now;
                PModel.CreatedNickName = Post.CreatedNickName;
                PModel.CreateUserID = Post.CreatedUserID;
               
                #region 进行默认的审核处理
                //管理员可以设置上传的商品默认为已审核状态，如果未定义，则为未审核
                string Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_product");
                if (!string.IsNullOrEmpty(Status))
                {
                    PModel.Status = Status == "0" ? (int)Model.SNS.EnumHelper.ProductStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.ProductStatus.UnChecked;
                    Post.Status = Status == "0" ? (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
                }
                else
                {
                    PModel.Status = (int)Model.SNS.EnumHelper.ProductStatus.AlreadyChecked;
                    Post.Status = (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked;
                }
                //进行敏感字过滤
                if (Maticsoft.BLL.Settings.FilterWords.ContainsModWords(Post.Description))
                {
                    PModel.Status = (int)Model.SNS.EnumHelper.ProductStatus.UnChecked;
                    Post.Status = (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
                }
                else
                {
                    Post.Description = Maticsoft.BLL.Settings.FilterWords.ReplaceWords(Post.Description);
                }
              
                PModel.ShareDescription = Post.Description;
                //未分类商品不再为未审核状态, 和后台设置统一
                //if (!PModel.CategoryID.HasValue || PModel.CategoryID <= 0)
                //{
                //    PModel.Status = (int)Model.SNS.EnumHelper.ProductStatus.UnChecked;
                //    Post.Status = (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
                //}
                // 最后的tags是 成色:全新|钱包款式:长款钱包|里料材质:合成革 这种形式      
                #region 获取属性
                ItemGetRequest reqPro = new ItemGetRequest();
                reqPro.Fields = "props_name";
                reqPro.NumIid = Pid;
                ItemGetResponse response2 = client.Execute(reqPro);
                string Prop = response2.Item.PropsName;
                PModel.Tags = tagBll.GetTagStr(Prop);
                Post.ProductName = PModel.ProductName;
                #endregion

                //if (!string.IsNullOrEmpty(Post.Description))
                //{
                //    Post.Description = Post.Description + "</br><a target='_blank' style='color: #FF7CAE' href='{ProductUrl} '>" + Post.ProductName + "</a>";
                //}
                //else
                //{
                //    Post.Description = "<a target='_blank' style='color: #FF7CAE' href='{ProductUrl} '>" + Post.ProductName + "</a>";
                //}
            }
            else if (Post.Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Normal)
            {
                string Status;
                if (!string.IsNullOrEmpty(Post.ImageUrl))
                {
                    Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_photo");
                    if (!string.IsNullOrEmpty(Status))
                    {
                        Post.Status = Status == "0" ? (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
                    }
                    else
                    {
                        Post.Status = (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked;
                    }
                }
                else if (!string.IsNullOrEmpty(Post.VideoUrl) && Post.VideoUrl.Length > 5)
                {
                    Post.Type = (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Video;
                    Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_video");
                    if (!string.IsNullOrEmpty(Status))
                    {
                        Post.Status = Status == "0" ? (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
                    }
                    else
                    {
                        Post.Status = (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked;
                    }
                }
                else if (!string.IsNullOrEmpty(Post.AudioUrl))
                {
                    Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_audio");
                    if (!string.IsNullOrEmpty(Status))
                    {
                        Post.Status = Status == "0" ? (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
                    }
                    else
                    {
                        Post.Status = (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked;
                    }
                }
                else
                {
                    Status = BLL.SysManage.ConfigSystem.GetValueByCache("chk_check_word");
                    if (!string.IsNullOrEmpty(Status))
                    {
                        Post.Status = Status == "0" ? (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
                    }
                    else
                    {
                        Post.Status = (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked;
                    }

                }

            }
            else if (Post.Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Photo)
            {
                string Status;
                if (!string.IsNullOrEmpty(Post.ImageUrl))
                {
                    Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_picture");
                    if (!string.IsNullOrEmpty(Status))
                    {
                        Post.Status = Status == "0" ? (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
                    }
                    else
                    {
                        Post.Status = (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked;
                    }
                }
            }
                #endregion
            string RecommandState = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductAndPhotoRecommandState");
            int RecommandStateInt = RecommandState != null ? Common.Globals.SafeInt(RecommandState, 0) : 0;
            //进行敏感字过滤
            if (Maticsoft.BLL.Settings.FilterWords.ContainsModWords(Post.Description))
            {
                Post.Status = (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
            }
            else
            {
                Post.Description = Maticsoft.BLL.Settings.FilterWords.ReplaceWords(Post.Description);
            }
            Maticsoft.Model.SNS.Posts Result = dal.AddPost(Post, AblumId, Pid, PhotoCateId, PModel, RecommandStateInt, PhotoAddress, MapLng, MapLat, CreatePost);
            #region @某人的实现
            SNS.ReferUsers ReferBll = new ReferUsers();
            if (Result != null)
            {
                ReferBll.AddEx(Post.Description, Maticsoft.Model.SNS.EnumHelper.ReferType.Post, Result.PostID);
            }
            #endregion
            return Result;
        }

        //添加博客 动态
        public Maticsoft.Model.SNS.Posts AddBlogPost(Maticsoft.Model.SNS.Posts Post, Maticsoft.Model.SNS.UserBlog blogModel, bool CreatePost = true)
        {
            Post.Type = (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Blog;
              string  Status = BLL.SysManage.ConfigSystem.GetValueByCache("chk_check_word");
                    if (!string.IsNullOrEmpty(Status))
                    {
                        Post.Status = Status == "0" ? (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
                    }
                    else
                    {
                        Post.Status = (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked;
                    }

                    //Post.Description = "<a target='_blank' style='color: #FF7CAE' href='{BlogUrl} '>" + Post.Description + "</a>";

            //进行敏感字过滤
            if (Maticsoft.BLL.Settings.FilterWords.ContainsModWords(Post.Description))
            {
                Post.Status = (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
            }
            else
            {
                Post.Description = Maticsoft.BLL.Settings.FilterWords.ReplaceWords(Post.Description);
            }
            Maticsoft.Model.SNS.Posts Result = dal.AddBlogPost(Post, blogModel, CreatePost);
            #region @某人的实现
            SNS.ReferUsers ReferBll = new ReferUsers();
            if (Result != null)
            {
                ReferBll.AddEx(Post.Description, Maticsoft.Model.SNS.EnumHelper.ReferType.Post, Result.PostID);
            }
            #endregion
            return Result;
        }

        //添加一般动态
        public Maticsoft.Model.SNS.Posts AddNormalPost(Maticsoft.Model.SNS.Posts Post)
        {
            Post.Type = (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Normal;
            string Status = BLL.SysManage.ConfigSystem.GetValueByCache("chk_check_word");
            if (!string.IsNullOrEmpty(Status))
            {
                Post.Status = Status == "0" ? (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
            }
            else
            {
                Post.Status = (int)Model.SNS.EnumHelper.PostStatus.AlreadyChecked;
            }
            //进行敏感字过滤
            if (Maticsoft.BLL.Settings.FilterWords.ContainsModWords(Post.Description))
            {
                Post.Status = (int)Model.SNS.EnumHelper.PostStatus.UnChecked;
            }
            else
            {
                Post.Description = Maticsoft.BLL.Settings.FilterWords.ReplaceWords(Post.Description);
            }
            int postId=dal.Add(Post);
            Post.PostID = postId;
            Maticsoft.Model.SNS.Posts Result = Post;
            #region @某人的实现
            SNS.ReferUsers ReferBll = new ReferUsers();
            if (Result != null)
            {
                ReferBll.AddEx(Post.Description, Maticsoft.Model.SNS.EnumHelper.ReferType.Post, Result.PostID);
            }
            #endregion
            return Result;
        }

       //添加商品动态

        public Maticsoft.Model.SNS.Posts AddProductPost(Maticsoft.Model.SNS.Products PModel, int AblumId, bool CreatePost=true)
        {
           
                PModel.ProductSourceID = (int)Maticsoft.Model.SNS.EnumHelper.WebSiteType.None;
                PModel.CreatedDate = DateTime.Now;
                #region 进行默认的审核处理
                //管理员可以设置上传的商品默认为已审核状态，如果未定义，则为未审核
                string Status = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_check_product");
                if (!string.IsNullOrEmpty(Status))
                {
                    PModel.Status = Status == "0" ? (int)Model.SNS.EnumHelper.ProductStatus.AlreadyChecked : (int)Model.SNS.EnumHelper.ProductStatus.UnChecked;
                }
                else
                {
                    PModel.Status = (int)Model.SNS.EnumHelper.ProductStatus.AlreadyChecked;
                }
                //进行敏感字过滤
                if (Maticsoft.BLL.Settings.FilterWords.ContainsModWords(PModel.ShareDescription))
                {
                    PModel.Status = (int)Model.SNS.EnumHelper.ProductStatus.UnChecked;
                }
                else
                {
                    PModel.ShareDescription = Maticsoft.BLL.Settings.FilterWords.ReplaceWords(PModel.ShareDescription);
                }
        
                #endregion
            string RecommandState = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ProductAndPhotoRecommandState");
            int RecommandStateInt = RecommandState != null ? Common.Globals.SafeInt(RecommandState, 0) : 0;
            //进行敏感字过滤

            Maticsoft.Model.SNS.Posts Result = dal.AddProductPost(PModel, AblumId,CreatePost);
            #region @某人的实现
            SNS.ReferUsers ReferBll = new ReferUsers();
            if (Result != null)
            {
                ReferBll.AddEx(Result.Description, Maticsoft.Model.SNS.EnumHelper.ReferType.Post, Result.PostID);
            }
            #endregion
            return Result;
        }
        public bool UpdateToDel(int PostID)
        {
            return dal.UpdateToDel(PostID);
        }

        /// <summary>
        /// 根据类型获取动态的前n条
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Posts> GetTopPost(int top, int Type)
        {
            return DataTableToList(dal.GetListByPage("Type=" + Type + "", "CreatedDate Desc", 0, top).Tables[0]);

        }
        /// <summary>
        /// 获取动态的前n条
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Posts> GetScrollPost(int top, Maticsoft.Model.SNS.EnumHelper.PostContentType PostType)
        {
            DataSet ds=new DataSet();
            if (PostType == Maticsoft.Model.SNS.EnumHelper.PostContentType.None)
            {
                ds = GetList(top, " ", "CreatedDate Desc");
            }
            else
            {
                ds = GetList(top, "Type=" + (int)PostType," CreatedDate Desc");
            }
            return DataTableToList(ds.Tables[0]);

        }


        #region 删除单个评论信息
        /// <summary>
        /// 删除单个评论信息
        /// </summary>
        /// <param name="PostID"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool DeleteEx(int PostID, bool IsSendMess = false, int SendUserID = 1)
        {
            Maticsoft.Model.SNS.Posts post = GetModelByCache(PostID);
            bool isSuccess= dal.DeleteEx(PostID);
            if (isSuccess && IsSendMess)
            {
                Maticsoft.BLL.Members.SiteMessage siteBll = new SiteMessage();
                siteBll.AddMessageByUser(SendUserID, post.CreatedUserID, "动态删除",
                                            "您分享的动态涉嫌非法内容，管理员已删除！ 如有疑问，请联系网站管理员");
            }
            return isSuccess;
        }


        public bool DeleteListEx(string PostIDs)
        {
            if (!string.IsNullOrEmpty(PostIDs))
            {
                string[] items = PostIDs.Split(',');
                foreach (string item in items)
                {
                    if (!dal.DeleteEx(Maticsoft.Common.Globals.SafeInt(item, 0)))
                    {
                        return false;

                    }
                }

            }
            return true;
        }
        #endregion
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetSearchList(string keywords, int type)
        {
            //不查询状态为3的动态
            string strWhere = string.Format(" STATUS<>{0}", (int)Maticsoft.Model.SNS.EnumHelper.PostStatus.AlreadyDel);
            if (type > -1)
            {
                strWhere += string.Format(" AND Type={0} ", type);
            }
            if (keywords.Length > 0)
            {
                strWhere += string.Format(" AND Description like '%{0}%'", keywords);
            }
            strWhere += " ORDER BY PostID DESC";
            return dal.GetList(strWhere);
        }

        public DataSet GetVideoSearchList(string KeyWord)
        {
            string Sql = "Type=0 and VideoUrl is not null and VideoUrl <>''";
            if (!string.IsNullOrWhiteSpace(KeyWord))
            {
                Sql += "and Description like '%" + KeyWord + "%'";
            }
            return GetList(Sql);

        }

        public bool UpdateStatusList(string PostIds, int Status)
        {
            return dal.UpdateStatusList(PostIds, Status);
        }
        /// <summary>
        /// 删除一般动态
        /// </summary>
        public bool DeleteListByNormalPost(string PostIDs, bool IsSendMess = false, int SendUserID = 1)
        {
            List<int> UserIdList = GetPostUserIds(PostIDs);
    bool IsSuccess =dal.DeleteListByNormalPost(PostIDs);
            if (IsSuccess  && IsSendMess)
            {
                Maticsoft.BLL.Members.SiteMessage siteBll = new SiteMessage();
                foreach (var userId in UserIdList)
                {
                    siteBll.AddMessageByUser(SendUserID, userId, "动态删除",
                                         "您分享的动态涉嫌非法内容，管理员已删除！ 如有疑问，请联系网站管理员");
                }

            }
            return IsSuccess;
        }

        public List<Maticsoft.Model.SNS.Posts> GetVideoList(int uid, int top)
        {
            string strWhere = string.Format(" type=3 and Status=1");
            if (uid > 0)
            {
                strWhere = strWhere + " and CreatedUserID=" + uid;
            }
            DataSet ds = dal.GetList(top, strWhere, "CreatedDate desc");
            return DataTableToList(ds.Tables[0]);
        }


        public List<Maticsoft.Model.SNS.Posts> GetVideoListByPageCache(int uid, int startIndex, int endIndex)
        {
            string CacheKey = "GetVideoListByPageCache" + uid + startIndex + endIndex;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = GetVideoListByPage(uid, startIndex, endIndex);
                    if (objModel != null)
                    {
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.Model.SNS.Posts>)objModel;
        }

        public List<Maticsoft.Model.SNS.Posts> GetVideoListByPage(int uid, int startIndex, int endIndex)
        {
            string strWhere = string.Format(" type=3 and Status=1");
            if (uid > 0)
            {
                strWhere = strWhere + " and CreatedUserID=" + uid;
            }
            DataSet ds = dal.GetListByPage(strWhere, "CreatedDate desc", startIndex, endIndex);

            return DataTableToList(ds.Tables[0]);
        }


        public bool UpdateFavCount(int postId)
        {
            return dal.UpdateFavCount(postId);
        }

        public bool UpdateCommentCount(int postId)
        {
            return dal.UpdateCommentCount(postId);
        }

        /// <summary>
        /// 获取照片用户ID
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public List<int> GetPostUserIds(string ids)
        {
            DataSet ds = dal.GetPostUserIds(ids);
            List<int> UserIdList = new List<int>();
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int n = 0; n < ds.Tables[0].Rows.Count; n++)
                {
                    if (ds.Tables[0].Rows[n]["CreatedUserID"] != null && ds.Tables[0].Rows[n]["CreatedUserID"].ToString() != "")
                    {
                        UserIdList.Add(int.Parse(ds.Tables[0].Rows[n]["CreatedUserID"].ToString()));
                    }
                }
            }
            return UserIdList;
        }
        /// <summary>
        /// 分页获取音频数据
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.SNS.Posts> GetAudioListByPage(int uid, int startIndex, int endIndex)
        {
            string strWhere = string.Format(" Status=1 and AudioUrl IS NOT NULL AND AudioUrl <>'' ");
            if (uid > 0)
            {
                strWhere = strWhere + " and CreatedUserID=" + uid;
            }
            DataSet ds = dal.GetListByPage(strWhere, "CreatedDate desc", startIndex, endIndex);

            return DataTableToList(ds.Tables[0]);
        }
        #endregion  ExtensionMethod
    }

}

