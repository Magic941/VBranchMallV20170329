using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Maticsoft.Json;
using Maticsoft.BLL.Members;
using Maticsoft.Model.SNS;
using Maticsoft.ViewModel.SNS;
using Webdiyer.WebControls.Mvc;
using Maticsoft.Common;
using Maticsoft.Components.Filters;
using Maticsoft.Common.Video;
using Maticsoft.Web.Components.Setting.SNS;
using System.Data;
using AlbumType = Maticsoft.BLL.SNS.AlbumType;
using EnumHelper = Maticsoft.Model.Ms.EnumHelper;
using GroupTopicReply = Maticsoft.BLL.SNS.GroupTopicReply;
using GroupTopics = Maticsoft.BLL.SNS.GroupTopics;
using Posts = Maticsoft.BLL.SNS.Posts;
using Products = Maticsoft.BLL.SNS.Products;
using UserAlbumsType = Maticsoft.BLL.SNS.UserAlbumsType;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    [TokenAuthorize]
    public class ProfileController : UsersProfileControllerBase
    {
        Maticsoft.BLL.Members.UsersExp UserBll = new BLL.Members.UsersExp();

        #region 初始分页和瀑布流数据
        public ProfileController()
        {
            this._PostPageSize = base.PostDataSize;
            this.FavAllPageSize = base.FallDataSize;
            this.FavBasePageSize = base.FallInitDataSize;

        }
        #endregion

        #region 重写userid和user对象



        public override bool LoadUserInfo(int UserID)
        {
            if (currentUser != null)
            {
                this.UserID = currentUser.UserID;
                this.DefaultPostType = Model.SNS.EnumHelper.PostType.Fellow;
                this.UserModel = UserBll.GetUsersExpModel(this.UserID);
                this.IsCurrentUser = true;
                this.NickName = UserModel != null ? UserModel.NickName : "";
                this.Activity = currentUser.Activity;
                return true;
            }
            return false;

        }
        #endregion

        #region 添加动态

        /// <summary>
        /// 添加动态的时候要用到的方法
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>

        public ActionResult AjaxPostAdd(FormCollection Fm)
        {
            if (currentUser == null)
            {
                return Content("NoLogin");
            }
            if (currentUser.UserType == "AA")
            {
                return Content("AA");
            }
            bool IsStatic = Common.Globals.SafeBool(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic"), false);
            string Type = Fm["Type"];
            string ImageUrl = Fm["ImageUrl"];
            string ShareDes = Common.InjectionFilter.Filter(Fm["ShareDes"]);
            string VideoUrl = Fm["VideoUrl"];
            string PostExUrl = Fm["PostExUrl"];
            string AudioUrl = Fm["AudioUrl"];
            string ProductName = Fm["ProductName"];
            int PhotoCateId = Common.Globals.SafeInt(Fm["PhotoCateId"], 0);
            int AblumId = Common.Globals.SafeInt(Fm["AblumId"], 0);
            long Pid = Common.Globals.SafeLong(Fm["Pid"], 0);

            string PhotoAdress = Fm["Address"];
            string MapLng = Fm["MapLng"];
            string MapLat = Fm["MapLat"];

            PostsModel.Description = ShareDes;
            PostsModel.CreatedDate = DateTime.Now;
            PostsModel.CreatedNickName = currentUser.NickName;
            PostsModel.CreatedUserID = currentUser.UserID;
            PostsModel.ImageUrl = ImageUrl;
            PostsModel.UserIP = Request.UserHostAddress;
            PostsModel.AudioUrl = AudioUrl;
            if (!String.IsNullOrWhiteSpace(PostsModel.ImageUrl) && Type != "Product")
            {
                //移动图片
                string savePath = "/Upload/SNS/Images/Photos/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string saveThumbsPath = "/Upload/SNS/Images/PhotosThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                PostsModel.ImageUrl = Maticsoft.BLL.SNS.Photos.MoveImage(PostsModel.ImageUrl, savePath, saveThumbsPath);
            }
            if (Type == "Product")
            {
                PostsModel.Type = (int)Model.SNS.EnumHelper.PostContentType.Product;
                Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
                if (productBll.Exsit(ProductName, currentUser.UserID))
                {
                    return Content("ProductRepeat");
                }
            }
            else if (Type == "Photo")
            {
                PostsModel.Type = (int)Model.SNS.EnumHelper.PostContentType.Photo;
            }
            else
            {
                PostsModel.Type = (int)Model.SNS.EnumHelper.PostContentType.Normal;
                if (!string.IsNullOrEmpty(VideoUrl) && !string.IsNullOrEmpty(PostExUrl))
                {
                    PostsModel.VideoUrl = VideoUrl;
                    PostsModel.PostExUrl = PostExUrl;
                }
            }
            PostsModel.Status = (int)Model.SNS.EnumHelper.Status.Enabled;
            PostsModel = PostsBll.AddPost(PostsModel, AblumId, Pid, PhotoCateId, PhotoAdress, MapLng, MapLat);
            //积分动作
            PostsModel.Description = ViewModel.ViewModelBase.RegexNickName(PostsModel.Description);
            Maticsoft.ViewModel.SNS.Posts PostsViewModel = new ViewModel.SNS.Posts();
            if (!string.IsNullOrEmpty(PostsModel.ImageUrl)  && Type != "Product")
            {
                PostsModel.ImageUrl = PostsModel.ImageUrl.Split('|')[0];
            }
            //if (PostsModel.Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product)
            //{
            //    #region 替换商品静态地址
            //    Maticsoft.BLL.SNS.Products productBll = new Products();
            //    Maticsoft.Model.SNS.Products productModel = productBll.GetModelByCache(PostsModel.TargetId);
            //    if (IsStatic && productModel != null)
            //    {
            //        PostsModel.Description = PostsModel.Description.Replace("{ProductUrl}", (String.IsNullOrWhiteSpace(productModel.StaticUrl) ? ViewBag.BasePath + "Product/Detail/" + PostsModel.TargetId : productModel.StaticUrl));
            //    }
            //    else
            //    {
            //        PostsModel.Description = PostsModel.Description.Replace("{ProductUrl}", (ViewBag.BasePath + "Product/Detail/" + PostsModel.TargetId));
            //    }

            //    #endregion
            //}
            

            PostsViewModel.Post = PostsModel;

            list.Add(PostsViewModel);
            return PartialView(CurrentThemeViewPath + "/UserProfile/LoadPostData.cshtml", list);
        }
        #endregion

        #region 发布长微博
        public ActionResult AjaxAddBlog(FormCollection Fm)
        {
            if (currentUser == null)
            {
                return Content("NoLogin");
            }
            if (currentUser.UserType == "AA")
            {
                return Content("AA");
            }
            string BlogTitle = Common.InjectionFilter.Filter(Fm["Title"]);
            string BlogContent = Fm["Des"];
            PostsModel.Description = BlogTitle;
            PostsModel.CreatedDate = DateTime.Now;
            PostsModel.CreatedNickName = currentUser.NickName;
            PostsModel.CreatedUserID = currentUser.UserID;
            PostsModel.UserIP = Request.UserHostAddress;

            Maticsoft.Model.SNS.UserBlog blogModel=new UserBlog();
            blogModel.Title = BlogTitle;
            blogModel.Description = BlogContent;
            blogModel.UserID = currentUser.UserID;
            blogModel.UserName = currentUser.NickName;
            blogModel.CreatedDate = DateTime.Now;
        
            PostsModel.Status = (int)Model.SNS.EnumHelper.Status.Enabled;

            PostsModel = PostsBll.AddBlogPost(PostsModel, blogModel);
            PostsModel.Description = ViewModel.ViewModelBase.RegexNickName(PostsModel.Description);
            Maticsoft.ViewModel.SNS.Posts PostsViewModel = new ViewModel.SNS.Posts();
            //替换占位符｛BlogUrl｝
            //PostsModel.Description = PostsModel.Description.Replace("{BlogUrl}", (ViewBag.BasePath + "Blog/BlogDetail/" + PostsModel.TargetId));
            PostsViewModel.Post = PostsModel;

            list.Add(PostsViewModel);
            return PartialView(CurrentThemeViewPath + "/UserProfile/LoadPostData.cshtml", list);
        }
        #endregion

        #region 添加积分

        public ActionResult AjaxAddPoint(string Type)
        {
            int Pointer = 0;
            var pointBll = new PointsDetail();
            if (Type == "Product")
            {
                Pointer = pointBll.AddPoints(9, currentUser.UserID, "发布商品");
            }
            else if (Type == "Photo")
            {
                Pointer = pointBll.AddPoints(5, currentUser.UserID, "分享照片");
            }
            else
            {
                Pointer = pointBll.AddPoints(4, currentUser.UserID, "发布动态");
            }
            return Content(Pointer.ToString());
        }
        #endregion

        #region 得到商品的信息
        /// <summary>
        /// 得到商品的信息
        /// </summary>
        /// <param name="ProductLink"></param>
        /// <returns></returns>
        public ActionResult AjaxGetProductInfo(string ProductLink)
        {
            Maticsoft.BLL.SNS.Products ProductBll = new BLL.SNS.Products();
            string ProductId = Maticsoft.ViewModel.ViewModelBase.RegexProductId(ProductLink);
            if (ProductId == "No")
            {
                return Content("No");
            }
            string Result = ProductBll.GetProductImageUrl(Maticsoft.Common.Globals.SafeLong(ProductId, 0));
            if (Result == "No")
            {
                return Content("No");
            }
            return Content(ProductId + "|" + Result);

        }
        #endregion

        #region 获取的专辑

        /// <summary>
        /// 获取某人的专辑
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxGetMyMyAblum()
        {
            Maticsoft.BLL.SNS.UserAlbums AlbumsBll = new BLL.SNS.UserAlbums();
            List<Maticsoft.Model.SNS.UserAlbums> listAblums = AlbumsBll.GetUserAblumsByUserID(currentUser.UserID);
            return Content(jss.Serialize(listAblums));

        }
        #endregion

        #region 分享图片
        public ActionResult AjaxGetUploadPhotoResult()
        {
            Maticsoft.BLL.SNS.UserAlbums AlbumsBll = new BLL.SNS.UserAlbums();
            List<Maticsoft.Model.SNS.UserAlbums> listAblums = AlbumsBll.GetUserAblumsByUserID(currentUser.UserID);
            Maticsoft.ViewModel.SNS.PhotoAlbum photoAlbum=new PhotoAlbum();
            List<Maticsoft.Model.SNS.Categories> PhotoCateList= Maticsoft.BLL.SNS.Categories.GetAllList(1);

            photoAlbum.UserAlbums = listAblums;
            photoAlbum.PhotoCateList = PhotoCateList.Where(c => c.Depth == 1).ToList();
            ViewBag.ImageUrl = Request["image"];
            ViewBag.ImageData = Request["data"];
            //获取图片分类

            return PartialView(CurrentThemeViewPath + "/Partial/_UploadPhotoResultLayOut.cshtml", photoAlbum);

        }
        #endregion

        #region 分享商品
        [HttpPost]
        public ActionResult AjaxProductPost(FormCollection Fm)
        {
            if (currentUser == null)
            {
                return Content("NoLogin");
            }
            if (currentUser.UserType == "AA")
            {
                return Content("AA");
            }
            string ImageUrl = Fm["ImageUrl"];
            string ShareDes = Common.InjectionFilter.Filter(Fm["ShareDes"]);
            string ProductName = Fm["ProductName"];
            int PhotoCateId = Common.Globals.SafeInt(Fm["PhotoCateId"], 0);
            int AblumId = Common.Globals.SafeInt(Fm["AblumId"], 0);
            decimal ProductPrice = Common.Globals.SafeDecimal(Fm["ProductPrice"], 0);
            string ProductUrl = Fm["ProductUrl"];

            Maticsoft.Model.SNS.Products productModel = new Maticsoft.Model.SNS.Products();

            productModel.ShareDescription = ShareDes;
            productModel.CreatedDate = DateTime.Now;
            productModel.CreatedNickName = currentUser.NickName;
            productModel.CreateUserID = currentUser.UserID;

            productModel.CategoryID = PhotoCateId;
            productModel.ProductUrl = ProductUrl;
            productModel.Price = ProductPrice;
            productModel.ProductName = ProductName;
                //移动图片
                string savePath = "/Upload/SNS/Images/Product/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                string saveThumbsPath = "/Upload/SNS/Images/ProductThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
                 ImageUrl= Maticsoft.BLL.SNS.Photos.MoveImage(ImageUrl, savePath, saveThumbsPath);

                productModel.ThumbImageUrl =ImageUrl.Split('|')[1] ;
                productModel.NormalImageUrl=ImageUrl.Split('|')[0] ;
           
                PostsModel.Type = (int)Model.SNS.EnumHelper.PostContentType.Product;
                Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
                if (productBll.ExsitUrl(ProductUrl, currentUser.UserID))
                {
                    return Content("ProductRepeat");
                }
                PostsModel = PostsBll.AddProductPost(productModel, AblumId);
            if (PostsModel == null)
            {
                return RedirectToAction("Index", "Error");
            }
            PostsModel.Description = ViewModel.ViewModelBase.RegexNickName(PostsModel.Description);
            Maticsoft.ViewModel.SNS.Posts PostsViewModel = new ViewModel.SNS.Posts();

            PostsViewModel.Post = PostsModel;
            list.Add(PostsViewModel);
            return PartialView(CurrentThemeViewPath + "/UserProfile/LoadPostData.cshtml", list);
        }
        public ActionResult AjaxProductResult()
        {
            Maticsoft.BLL.SNS.UserAlbums AlbumsBll = new BLL.SNS.UserAlbums();
            List<Maticsoft.Model.SNS.UserAlbums> listAblums = AlbumsBll.GetUserAblumsByUserID(currentUser.UserID);
            Maticsoft.ViewModel.SNS.ProductAlbum productAlbum = new ProductAlbum();
            List<Maticsoft.Model.SNS.Categories> ProductCateList = Maticsoft.BLL.SNS.Categories.GetAllList(0);

            productAlbum.UserAlbums = listAblums;
            productAlbum.ProductCateList = ProductCateList.Where(c => c.Depth == 1).ToList();
            ViewBag.ImageUrl = Request["image"];
            ViewBag.ImageData = Request["data"];
            //获取图片分类

            return PartialView(CurrentThemeViewPath + "/Partial/_ProductResult.cshtml", productAlbum);

        }
        #endregion

        #region 增加专辑
        /// <summary>
        /// 增加专辑
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddAlbum(FormCollection Fm)
        {
            if (currentUser == null)
            {
                return Content("NoLogin");
            }
            if (CurrentUser.UserType == "AA")
            {
                return Content("AA");
            }
            string AblumName = Fm["AlbumName"];
            int TypeId = Common.Globals.SafeInt(Fm["Type"], 0);
            Maticsoft.BLL.SNS.UserAlbums AlbumsBll = new BLL.SNS.UserAlbums();
            Maticsoft.Model.SNS.UserAlbums AlbumModel = new Model.SNS.UserAlbums();
            AlbumModel.AlbumName = AblumName;
            AlbumModel.CreatedDate = DateTime.Now;
            AlbumModel.CreatedNickName = currentUser.NickName;
            AlbumModel.CreatedUserID = currentUser.UserID;
            if ((AlbumModel.AlbumID = AlbumsBll.AddEx(AlbumModel, TypeId)) > 0)
            {
                Maticsoft.BLL.SNS.UserAlbumsType utBll = new Maticsoft.BLL.SNS.UserAlbumsType();
                Model.SNS.UserAlbumsType uatModel = new Model.SNS.UserAlbumsType();
                uatModel.AlbumsID = AlbumModel.AlbumID;
                uatModel.AlbumsUserID = AlbumModel.CreatedUserID;
                uatModel.TypeID = TypeId;
                if (utBll.Add(uatModel))
                {
                    return Content(AlbumModel.AlbumID.ToString());
                }
            }
            return Content("No");
        }
        #endregion

        #region 获取专辑类型

        /// <summary>
        /// 获取专辑的类型
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxGetAlbumsType()
        {
            Maticsoft.BLL.SNS.AlbumType AlbumTypeBll = new BLL.SNS.AlbumType();
            List<Maticsoft.Model.SNS.AlbumType> AlbumTypeList = AlbumTypeBll.GetModelListByCache(Model.SNS.EnumHelper.Status.Enabled);
            return Content(jss.Serialize(AlbumTypeList));
        }

        #endregion

        #region 编辑专辑
        public ActionResult AlbumEdit(int AlbumId)
        {
            ViewBag.Title = "编辑专辑";
            Maticsoft.BLL.SNS.UserAlbums AlbumBll = new BLL.SNS.UserAlbums();
            Maticsoft.BLL.SNS.UserAlbumsType typeBll=new UserAlbumsType();
            Maticsoft.BLL.SNS.AlbumType albumTypeBll=new AlbumType();
            Maticsoft.Model.SNS.UserAlbums AlbumModel = AlbumBll.GetModel(AlbumId);

            List<Maticsoft.Model.SNS.AlbumType> list = albumTypeBll.GetModelList("Status=1");
            var dropList = new List<SelectListItem>();
            dropList.Add(new SelectListItem { Value = "0", Text = "请选择" });
            if (list != null && list.Count > 0)
            {
                foreach (var item in list)
                {
                    dropList.Add(new SelectListItem { Value = item.ID.ToString(), Text = item.TypeName });
                }
                ViewBag.TypeList = dropList;
            }
            if (AlbumModel != null)
            {
               Maticsoft.Model.SNS.UserAlbumsType typeModel= typeBll.GetModelByUserId(AlbumModel.AlbumID, currentUser.UserID);
                AlbumModel.TypeId = 0;
                if (typeModel != null)
                {
                    AlbumModel.TypeId = typeModel.TypeID;
                }
                return View(AlbumModel);
            }
            return new EmptyResult();

        }
        [HttpPost]
        public ActionResult AlbumEdit(FormCollection fm)
        {
            ViewBag.Title = "编辑专辑";
            Maticsoft.Model.SNS.UserAlbums AlbumModel = new Model.SNS.UserAlbums();
            Maticsoft.BLL.SNS.UserAlbums AlbumBll = new BLL.SNS.UserAlbums();
            AlbumModel.AlbumName = fm["AlbumName"];
            AlbumModel.Description = fm["Description"];
            AlbumModel.AlbumID = Common.Globals.SafeInt(fm["AlbumID"], 0);
            if (AlbumBll.UpdateEx(AlbumModel))
            {
                int typeId=Common.Globals.SafeInt(fm["TypeId"], 0);
                Maticsoft.BLL.SNS.UserAlbumsType typeBll = new UserAlbumsType();
                Maticsoft.Model.SNS.UserAlbumsType typeModel = typeBll.GetModelByUserId(AlbumModel.AlbumID, currentUser.UserID);
                if (typeModel == null)
                {
                    typeModel = new Maticsoft.Model.SNS.UserAlbumsType();
                    typeModel.TypeID = typeId;
                    typeModel.AlbumsID = AlbumModel.AlbumID;
                    typeModel.AlbumsUserID = currentUser.UserID;
                    typeBll.AddEx(typeModel);
                }
                else
                {
                    typeModel.TypeID = typeId;
                    typeBll.UpdateType(typeModel);
                }
                return RedirectToAction("Albums");
            }
            return RedirectToAction("AlbumEdit", new { AlbumId = AlbumModel.AlbumID });
        }
        #endregion

        #region 删除专辑
        public ActionResult AjaxDelAlbum(int AlbumId)
        {
            Maticsoft.BLL.SNS.UserAlbums AlbumBll = new BLL.SNS.UserAlbums();
            if (AlbumBll.DeleteEx(AlbumId, currentUser.UserID))
            {
                return Content("True");
            }

            return Content("False");

        }
        #endregion

        #region 是否关注某用户
        public ActionResult AjaxCheckIsFellow(int UserId)
        {
            Maticsoft.BLL.SNS.UserShip shipBll = new BLL.SNS.UserShip();
            if (shipBll.Exists(currentUser.UserID, UserId))
            {
                return Content("True");
            }
            return Content("False");
        }
        #endregion

        #region 得到当前用户的id
        public ActionResult AjaxGetUserId()
        {
            if (currentUser != null)
            {
                return Content(currentUser.UserID.ToString());

            }
            return Content("0");

        }
        #endregion

        #region 添加关注
        /// <summary>
        /// 添加关注
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxFellowUser(int UserId)
        {
            Maticsoft.BLL.SNS.UserShip shipBll = new BLL.SNS.UserShip();
            //自己不能关注自己
            if (UserId == currentUser.UserID)
            {
                return Content("Self");
            }
            if (shipBll.AddAttention(CurrentUser.UserID, UserId))
            {
                return Content("True");
            }
            return Content("False");

        }
        #endregion

        #region 取消关注
        /// <summary>
        /// 取消关注
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AjaxUnFellowUser(int UserId)
        {
            Maticsoft.BLL.SNS.UserShip shipBll = new BLL.SNS.UserShip();
            if (shipBll.CancelAttention(CurrentUser.UserID, UserId))
            {
                return Content("True");
            }
            return Content("False");
        }
        #endregion

        #region 删除喜欢
        public ActionResult AjaxDelFav(int TargetId, string Type)
        {
            Maticsoft.BLL.SNS.UserFavourite ufBll = new BLL.SNS.UserFavourite();
            if (ufBll.DeleteEx(currentUser.UserID, TargetId, Type == "Product" ? 1 : 0))
            {
                return Content("True");
            }
            return Content("False");
        }
        #endregion

        #region 添加喜欢

        public ActionResult AjaxAddFavourite(FormCollection Fm)
        {
            //假如是视频喜欢 （暂时没有加收藏表数据）
            int TargetID=  Common.Globals.SafeInt(Fm["TargetId"], 0);
            if (Fm["Type"] == "Video")
            {
                Maticsoft.BLL.SNS.Posts postBll=new Posts();
                if (postBll.UpdateFavCount(TargetID))
                {
                    return Content("Ok");
                }
                else
                {
                    return Content("No");
                }
            }
            Maticsoft.Model.SNS.UserFavourite UserFavModel = new Model.SNS.UserFavourite();
            Maticsoft.BLL.SNS.UserFavourite UserFavBll = new BLL.SNS.UserFavourite();
            int TopicId = Common.Globals.SafeInt(Fm["TopicId"], 0);
            int ReplyId = Common.Globals.SafeInt(Fm["ReplyId"], 0);
            UserFavModel.Type = (Fm["Type"] == "Product" ? (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product : (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo);
            UserFavModel.TargetID = Common.Globals.SafeInt(Fm["TargetId"], 0);
            UserFavModel.CreatedDate = DateTime.Now;
            UserFavModel.CreatedUserID = currentUser.UserID;
            UserFavModel.CreatedNickName = currentUser.NickName;
            if (UserFavBll.Exists(currentUser.UserID, UserFavModel.Type, UserFavModel.TargetID))
            {
                return Content("Repeat");
            }
            if (UserFavBll.AddEx(UserFavModel, TopicId, ReplyId))
            {
                return Content("Ok");
            }
            return Content("No");
        }
        #endregion

        #region 加入专辑

        public ActionResult AjaxAddToAlbum(FormCollection Fm)
        {
            Maticsoft.Model.SNS.UserAlbumDetail AlumsDetailModel = new Model.SNS.UserAlbumDetail();
            Maticsoft.BLL.SNS.UserAlbumDetail AlumsDetailBll = new Maticsoft.BLL.SNS.UserAlbumDetail();
            int TargetId = Common.Globals.SafeInt(Fm["TargetId"], 0);
            int Type = Fm["Type"] == "Product" ? (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product : (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Photo;
            int AlbumId = Common.Globals.SafeInt(Fm["AlbumId"], 0);
            if (AlbumId > 0)
            {
                string Des = Fm["Des"];
                AlumsDetailModel.TargetID = TargetId;
                AlumsDetailModel.Type = Type;
                AlumsDetailModel.Description = Des;
                AlumsDetailModel.AlbumUserId = currentUser.UserID;
                AlumsDetailModel.AlbumID = AlbumId;
                if (AlumsDetailBll.Exists(AlbumId, TargetId, Type))
                {
                    return Content("Repeat");
                }
                if (AlumsDetailBll.AddEx(AlumsDetailModel))
                {
                    return Content("Ok");
                }
            }
            return Content("No");

        }
        #endregion

        #region 删除动态
        /// <summary>
        /// 删除动态
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxDelPost(FormCollection Fm)
        {
            int postId = Common.Globals.SafeInt(Fm["PostId"], 0);

            Maticsoft.Model.SNS.Posts PostModel = PostsBll.GetModel(postId);
            Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
            Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
            int result = 0;
            DataSet ds = new DataSet();
            switch (PostModel.Type)
            {
                case 0:
                    //一般动态
                    if (PostsBll.DeleteEx(postId))
                    {
                        if (!String.IsNullOrWhiteSpace(PostModel.ImageUrl) &&
                 !PostModel.ImageUrl.StartsWith("http://"))
                        {
                            DeletePhysicalFile(PostModel.ImageUrl);
                        }
                    }
                    break;
                case 1:
                    //删除单个图片类型动态
                    ds = photoBll.DeleteListEx(PostModel.TargetId.ToString(), out result);
                    if (ds != null)
                    {
                        PhysicalFileInfo(ds.Tables[0]);
                    }
                    break;
                case 2:
                    //删除单个商品类型动态
                    ds = productBll.DeleteListEx(PostModel.TargetId.ToString(), out result);
                    if (ds != null)
                    {
                        PhysicalFileInfo(ds.Tables[0]);
                    }
                    break;
                case 4:
                    //删除长微博
                    Maticsoft.BLL.SNS.UserBlog userBlog=new BLL.SNS.UserBlog();
                    userBlog.DeleteEx(PostModel.TargetId);
                    break;
                default:
                    //一般动态
                    if (PostsBll.DeleteEx(postId))
                    {
                        if (!String.IsNullOrWhiteSpace(PostModel.ImageUrl) &&
                 !PostModel.ImageUrl.StartsWith("http://"))
                        {
                            DeletePhysicalFile(PostModel.ImageUrl);
                        }
                    }
                    break;

            }
            return Content(postId.ToString());
        }
        #endregion

        #region 添加对商品或图片的评论
        public ActionResult AjaxAddProductComment(FormCollection Fm)
        {
            Maticsoft.Model.SNS.Comments ComModel = new Model.SNS.Comments();
            Maticsoft.BLL.SNS.Comments ComBll = new BLL.SNS.Comments();
            int TargetId = Common.Globals.SafeInt(Fm["TargetId"], 0);
            int Type = Fm["Type"] == "Product" ? (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product : (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo;
            int CommentId = 0;
            string Des = Common.InjectionFilter.Filter(Fm["Des"]);
            ComModel.CreatedDate = DateTime.Now;
            ComModel.CreatedNickName = currentUser.NickName;
            ComModel.CreatedUserID = currentUser.UserID;
            ComModel.Description = Des;
            ComModel.HasReferUser = Des.Contains('@') ? true : false;
            ComModel.IsRead = false;
            ComModel.ReplyCount = 0;
            ComModel.TargetId = TargetId;
            ComModel.Type = Type;
            ComModel.UserIP = Request.UserHostAddress;
            if ((CommentId = ComBll.AddEx(ComModel)) > 0)
            {
                ComModel.CommentID = CommentId;
                ComModel.Description = ViewModel.ViewModelBase.RegexNickName(ComModel.Description);
                List<Maticsoft.Model.SNS.Comments> list = new List<Maticsoft.Model.SNS.Comments>();
                //是否含有审核词
                if (!Maticsoft.BLL.Settings.FilterWords.ContainsModWords(ComModel.Description))
                {
                    list.Add(ComModel);
                }
                return PartialView(CurrentThemeViewPath + "/Partial/TargetListComment.cshtml", list.AsEnumerable());
            }
            return Content("No");
        }
        #endregion

        #region 转发动态
        /// <summary>
        /// 转发动态方法
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxPostForward(FormCollection Fm)
        {
            int ResultPostId = 0;
            string PostContent = Common.InjectionFilter.Filter(Fm["content"]);
            PostContent = ViewModel.ViewModelBase.ReplaceFace(PostContent);
            int OrigPostId = Common.Globals.SafeInt(Fm["origid"], 0);
            #region 主要用于提示作用用
            int OrigUserId = Common.Globals.SafeInt(Fm["origuserid"], 0);
            string OrigNickname = Fm["orignickname"];
            #endregion
            int Forwardid = Common.Globals.SafeInt(Fm["forwardid"], 0);
            if (OrigPostId == 0 || OrigUserId == 0)
            {
                return Content("No");
            }
            PostsModel.CreatedDate = DateTime.Now;
            PostsModel.CreatedNickName = currentUser.NickName;
            PostsModel.CreatedUserID = currentUser.UserID;
            PostsModel.Description = PostContent;
            PostsModel.ForwardedID = Forwardid;
            PostsModel.HasReferUsers = PostContent.Contains("@") ? true : false;
            PostsModel.OriginalID = OrigPostId;
            PostsModel.Type = (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Normal;
            ResultPostId = PostsBll.AddForwardPost(PostsModel);
            list = PostsBll.GetForPostByPostId(ResultPostId,IncludeProduct);

            #region 对原动态的用户进行@提到提醒
            Maticsoft.BLL.SNS.ReferUsers referBll = new Maticsoft.BLL.SNS.ReferUsers();
            Maticsoft.Model.SNS.ReferUsers referModel = new Model.SNS.ReferUsers();
            referModel.CreatedDate = DateTime.Now;
            referModel.IsRead = false;
            referModel.ReferUserID = OrigUserId;
            referModel.ReferNickName = OrigNickname;
            referModel.Type = (int)Maticsoft.Model.SNS.EnumHelper.ReferType.Post;
            referModel.TagetID = ResultPostId;
            referBll.Add(referModel);
            #endregion

            return PartialView(CurrentThemeViewPath + "/UserProfile/LoadPostData.cshtml", list);

        }
        #endregion

        #region 增加对动态的评论
        /// <summary>
        /// 无刷新评论的的方法
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddComment(FormCollection Fm)
        {
            Maticsoft.BLL.SNS.Comments ComBll = new BLL.SNS.Comments();
            Maticsoft.Model.SNS.Comments ComModel = new Model.SNS.Comments();
            int PostId = Common.Globals.SafeInt(Fm["PostId"], 0);
            List<Maticsoft.Model.SNS.Comments> list = new List<Model.SNS.Comments>();
            int CommentId = 0;
            string Des = Common.InjectionFilter.Filter(Fm["Des"]);
            Des = ViewModel.ViewModelBase.ReplaceFace(Des);
            if (PostId > 0)
            {
                PostsModel = PostsBll.GetModel(PostId);
                ComModel.CreatedDate = DateTime.Now.AddMinutes(1);
                ComModel.CreatedNickName = currentUser.NickName;
                ComModel.CreatedUserID = currentUser.UserID;
                ComModel.Description = Des;
                ComModel.HasReferUser = Des.Contains('@') ? true : false;
                ComModel.IsRead = false;
                ComModel.ReplyCount = 0;
                ComModel.TargetId = PostsModel.TargetId > 0 ? PostsModel.TargetId : PostsModel.PostID;
                if (PostsModel.Type.Value == 3)
                {
                    PostsModel.Type = 0;
                }
                ComModel.Type = PostsModel.Type.Value;
                ComModel.UserIP = Request.UserHostAddress;
                if ((CommentId = ComBll.AddEx(ComModel)) > 0)
                {
                    ComModel.CommentID = CommentId;
                    ComModel.Description = ViewModel.ViewModelBase.RegexNickName(ComModel.Description);
                    //是否含有审核词
                    if (!Maticsoft.BLL.Settings.FilterWords.ContainsModWords(ComModel.Description))
                    {
                        list.Add(ComModel);
                    }
                    ViewBag.PostId = PostId;
                    return View(CurrentThemeViewPath + "/UserProfile/postCommentList.cshtml", list);
                }

            }
            return Content("No");
        }
        #endregion

        #region 创建专辑界面
        public ActionResult AjaxCreateAlbums()
        {
            Maticsoft.BLL.SNS.AlbumType Bll = new BLL.SNS.AlbumType();
            List<Maticsoft.Model.SNS.AlbumType> list = Bll.GetModelList("");
            return View(CurrentThemeViewPath + "/UserProfile/AjaxCreateAlbums.cshtml", list);

        }
        #endregion

        #region 删除专辑中的图片
        public ActionResult AjaxDelAlbumDetail(int TargetId, string Type, int AlbumId)
        {
            Maticsoft.BLL.SNS.UserAlbumDetail uadBll = new BLL.SNS.UserAlbumDetail();
            if (uadBll.DeleteEx(AlbumId, TargetId, Type == "Product" ? 1 : 0))
            {
                return Content("True");
            }
            return Content("False");
        }
        #endregion

        #region 删除预览图的物理文件
        public ActionResult AjaxDelYulanTu(FormCollection Fm)
        {
            string ImageUrl = Fm["ImageUrl"];
            if (!string.IsNullOrEmpty(ImageUrl))
            {
                //假如是云存储
                if (ImageUrl.StartsWith("http://"))
                {
                    return Content("True");
                }
                //首先删除临时文件夹原图
                if (System.IO.File.Exists(Server.MapPath(String.Format(ImageUrl,""))))
                {
                    System.IO.File.Delete(Server.MapPath(String.Format(ImageUrl, "")));
                }
             //然后循环删除缩略图
                List<Maticsoft.Model.Ms.ThumbnailSize> ThumbSizeList =
                       Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS);
                if (ThumbSizeList != null && ThumbSizeList.Count > 0)
                {
                    foreach (var thumbSize in ThumbSizeList)
                    {
                        if (System.IO.File.Exists(Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName))))
                        {
                            System.IO.File.Delete(Server.MapPath(String.Format(ImageUrl, thumbSize.ThumName)));
                        }
                    }
                }
                return Content("True");
            }

            return Content("No");
        }
        #endregion

        #region 发表主题
        public ActionResult AJaxCreateTopic(FormCollection fm)
        {
            string Title = Common.InjectionFilter.Filter(fm["Title"] != null ? fm["Title"].ToString() : "");
            string Des = fm["Des"] != null ? fm["Des"].ToString() : "";
            long Pid = Common.Globals.SafeLong(fm["Pid"], 0);
            string ImageUrl = "";
            //商品和图片只能发一个
            if (Pid == 0)
            {
                ImageUrl = fm["ImageUrl"] != null ? fm["ImageUrl"].ToString() : "";
            }
            int GroupId = Common.Globals.SafeInt(fm["GroupId"], 0);
            if (string.IsNullOrEmpty(Title) || string.IsNullOrEmpty(Des))
            {
                return Content("No");
            }
            Maticsoft.BLL.SNS.GroupTopics bllTopic = new BLL.SNS.GroupTopics();
            Maticsoft.Model.SNS.GroupTopics modelTopic = new Model.SNS.GroupTopics();
            modelTopic.Title = Title;
            modelTopic.Description = Des;
            //移动图片
            string savePath = "/Upload/SNS/Images/Group/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            string saveThumbsPath = "/Upload/SNS/Images/GroupThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            modelTopic.ImageUrl = Maticsoft.BLL.SNS.Photos.MoveImage(ImageUrl, savePath, saveThumbsPath);
            modelTopic.GroupID = GroupId;
            modelTopic.GroupName = "";
            modelTopic.CreatedDate = DateTime.Now;
            modelTopic.CreatedUserID = currentUser.UserID;
            modelTopic.CreatedNickName = currentUser.NickName;
            if ((modelTopic.TopicID = bllTopic.AddEx(modelTopic, Pid)) > 0)
            {
                return Content(modelTopic.TopicID.ToString());
            }
            //SNS连续发帖冻结帐号
            else if (modelTopic.TopicID == -2)
            {
                //已冻结帐号, 强制用户退出
                System.Web.Security.FormsAuthentication.SignOut();
                Session.Remove(Globals.SESSIONKEY_USER);
                Session.Clear();
                Session.Abandon();
            }
            return Content("No");
        }
        #endregion

        #region 回应主题
        public ActionResult AJaxCreateTopicReply(FormCollection fm)
        {
            string Des = Common.InjectionFilter.Filter(fm["Des"] != null ? fm["Des"].ToString() : "");
            long Pid = Common.Globals.SafeLong(fm["Pid"], 0);
            int GroupId = Common.Globals.SafeInt(fm["GroupId"], 0);
            int TopicId = Common.Globals.SafeInt(fm["TopicId"], 0);
            string ImageUrl = "";
            //商品和图片只能发一个
            if (Pid == 0)
            {
                ImageUrl = fm["ImageUrl"] != null ? fm["ImageUrl"].ToString() : "";
            }
            if (string.IsNullOrEmpty(Des))
            {
                return Content("No");
            }
            Maticsoft.BLL.SNS.GroupTopicReply bllTopicReply = new BLL.SNS.GroupTopicReply();
            Maticsoft.Model.SNS.GroupTopicReply modelTopicReply = new Model.SNS.GroupTopicReply();
            modelTopicReply.Description = Des;
            //移动图片
            string savePath = "/Upload/SNS/Images/Group/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            string saveThumbsPath = "/Upload/SNS/Images/GroupThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            modelTopicReply.PhotoUrl = Maticsoft.BLL.SNS.Photos.MoveImage(ImageUrl, savePath, saveThumbsPath);
            modelTopicReply.GroupID = GroupId;
            modelTopicReply.CreatedDate = DateTime.Now;
            modelTopicReply.ReplyUserID = currentUser.UserID;
            modelTopicReply.ReplyNickName = currentUser.NickName;
            modelTopicReply.Status = 1;
            modelTopicReply.TopicID = TopicId;
            if ((modelTopicReply.ReplyID = bllTopicReply.AddEx(modelTopicReply, Pid)) > 0)
            {
                Maticsoft.BLL.SNS.GroupTopicReply bllReply = new BLL.SNS.GroupTopicReply();
                List<Maticsoft.Model.SNS.GroupTopicReply> list = new List<Model.SNS.GroupTopicReply>();
                modelTopicReply = bllReply.GetModel(modelTopicReply.ReplyID);
                list.Add(modelTopicReply);
                return PartialView(CurrentThemeViewPath + "/Group/TopicReplyResult.cshtml", list);
            }
            return Content("No");
        }
        #endregion

        #region 对回应进行回应
        public ActionResult AJaxCreateReply(FormCollection fm)
        {
            string Des = Common.InjectionFilter.Filter(fm["Des"]);
            int ReplyId = Common.Globals.SafeInt(fm["ReplyId"], 0);
            int UserId = currentUser.UserID;
            string NickName = currentUser.NickName;
            Maticsoft.BLL.SNS.GroupTopicReply bllReply = new BLL.SNS.GroupTopicReply();
            Des = Maticsoft.ViewModel.ViewModelBase.ReplaceFace(Des);
            int NewReplyId = bllReply.ForwardReply(ReplyId, Des, UserId, NickName);
            List<Maticsoft.Model.SNS.GroupTopicReply> list = new List<Model.SNS.GroupTopicReply>();
            Maticsoft.Model.SNS.GroupTopicReply replyModel = new Model.SNS.GroupTopicReply();
            replyModel = bllReply.GetModel(NewReplyId);
            list.Add(replyModel);
            return PartialView(CurrentThemeViewPath + "/Group/TopicReplyResult.cshtml", list);
        }
        #endregion

        #region 对主题进行操作
        public ActionResult AjaxTopicOperation(FormCollection fm)
        {
            int TopicId = Common.Globals.SafeInt(fm["TopicId"], 0);
            int Type = Common.Globals.SafeInt(fm["Type"], 0);
            Maticsoft.BLL.SNS.GroupTopics bllTopic = new BLL.SNS.GroupTopics();
            Maticsoft.Model.SNS.GroupTopics modelTopic = new Model.SNS.GroupTopics();
            modelTopic = bllTopic.GetModel(TopicId);
            if (Type < 2)
            {
                if (bllTopic.UpdateRecommand(TopicId, Type))
                {
                    return Content("Ok");
                }
                return Content("No");
            }
            else if (Type == 2)
            {
                modelTopic.IsRecomend = 0;
                if (bllTopic.DeleteEx(TopicId))
                {
                    return Content("Ok");
                }
                return Content("No");
            }
            return null;
        }

        #endregion

        #region 收藏主题
        public ActionResult AjaxAddTopicToFav(int TopicId)
        {
            Maticsoft.Model.SNS.GroupTopicFav Model = new Model.SNS.GroupTopicFav();
            Maticsoft.BLL.SNS.GroupTopicFav Bll = new BLL.SNS.GroupTopicFav();
            if (Bll.Exists(TopicId, currentUser.UserID))
            {
                return Content("Repeate");
            }
            Model.CreatedDate = DateTime.Now;
            Model.CreatedUserID = currentUser.UserID;
            Model.TopicID = TopicId;
            if (Bll.AddEx(Model))
            {
                return Content("Yes");
            }
            return Content("No");
        }
        #endregion

        #region 检测用户是加入小组
        public ActionResult AJaxCheckUserIsJoinGroup(FormCollection fm)
        {
            Maticsoft.Model.SNS.GroupUsers model = new Model.SNS.GroupUsers();
            Maticsoft.BLL.SNS.GroupUsers bll = new BLL.SNS.GroupUsers();
            int GroupId = Common.Globals.SafeInt(fm["GroupId"], 0);
            if (bll.Exists(GroupId, currentUser.UserID))
            {
                return Content("joined");
            }
            return Content("No");

        }
        #endregion

        #region  加入小组
        public ActionResult AjaxJoinGroup(FormCollection fm)
        {
            Maticsoft.Model.SNS.GroupUsers model = new Model.SNS.GroupUsers();
            Maticsoft.BLL.SNS.GroupUsers bll = new BLL.SNS.GroupUsers();
            int GroupId = Common.Globals.SafeInt(fm["GroupId"], 0);
            if (bll.Exists(GroupId, currentUser.UserID))
            {
                return Content("joined");
            }
            model.GroupID = GroupId;
            model.JoinTime = DateTime.Now;
            model.NickName = currentUser.NickName;
            model.UserID = currentUser.UserID;
            model.Status = 1;
            if (bll.AddEx(model))
            {

                return Content("Yes");
            }


            return Content("No");
        }
        #endregion

        #region 同步到微博

        /// <summary>
        /// 添加动态的时候要用到的方法
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>

        public void AjaxPostWeibo(FormCollection Fm)
        {
            Maticsoft.BLL.Members.UserBind userBind = new BLL.Members.UserBind();
            Maticsoft.BLL.SNS.Posts postBll = new BLL.SNS.Posts();
            Maticsoft.BLL.SNS.GroupTopics topicBll = new GroupTopics();
            Maticsoft.BLL.SNS.GroupTopicReply replyBll = new GroupTopicReply();
            if (currentUser.UserType == "AA" || String.IsNullOrWhiteSpace(Fm["MediaIds"]))
            {
                return;
            }
            string MediaIds = Fm["MediaIds"];
            string ShareDes = Fm["ShareDes"];
            string imageurl = Fm["ImageUrl"];
            string VideoRawUrl = Fm["VideoRawUrl"];//暂时不实现同步音频
            string url = Fm["Url"];   //自主开发的微博接口不需要URL
            string type = Fm["Type"];
            int TargetID = Common.Globals.SafeInt(Fm["TargetID"], 0);
            int PostID = Common.Globals.SafeInt(Fm["PostID"], 0);
            int TopicID = Common.Globals.SafeInt(Fm["TopicID"], 0);
            int AlumbID = Common.Globals.SafeInt(Fm["AlumbID"], 0);
            int ReplyId = Common.Globals.SafeInt(Fm["ReplyId"], 0);
            if (AlumbID > 0)
            {
                url = "http://" + Common.Globals.DomainFullName + "/Album/Details?AlbumID=" + AlumbID + "";
            }
            #region 动态的情况
            if (PostID > 0)
            {
                Maticsoft.Model.SNS.Posts postModel = postBll.GetModel(PostID);
                if (postModel != null)
                {
                    if (postModel.Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Photo)
                    {
                        type = "Photo";
                        TargetID = postModel.TargetId;
                        if (string.IsNullOrEmpty(ShareDes))
                        {
                            ShareDes = "分享图片";
                        }

                    }
                    else if (postModel.Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product)
                    {
                        type = "Product";
                        TargetID = postModel.TargetId;
                        if (string.IsNullOrEmpty(ShareDes))
                        {
                            ShareDes = postModel.ProductName;
                        }
                    }
                    else if (postModel.Type == (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Normal)
                    {
                        type = "Weibo";
                        ShareDes = string.IsNullOrEmpty(postModel.Description) ? "分享图片" : postModel.Description;
                    }
                    imageurl = postModel.ImageUrl.Replace("{0}", "T116x170_");
                }
            }
            #endregion
            if (TopicID > 0)
            {
               url = "http://" + Common.Globals.DomainFullName + "/Group/TopicReply?id=" + TopicID + "";
                Maticsoft.Model.SNS.GroupTopics topicmodel = topicBll.GetModel(TopicID);
                imageurl = topicmodel != null ? topicmodel.ImageUrl : "";
            }
            if (ReplyId > 0)
            {
               url = "http://" + Common.Globals.DomainFullName + "/Group/TopicReply?id=" + TopicID + "";
                Maticsoft.Model.SNS.GroupTopicReply replymodel = replyBll.GetModel(ReplyId);
                imageurl = replymodel != null ? replymodel.PhotoUrl : "";
            }
            if (!string.IsNullOrEmpty(imageurl) && imageurl.Split('|').Length > 2 && type != "Product")
            {
                imageurl = "http://" + Common.Globals.DomainFullName + imageurl.Split('|')[0];
            }
            if (type == "Weibo")
            {
                url = "http://" + Common.Globals.DomainFullName + "/User/Posts/" + currentUser.UserID + "";

            }
            else if (type == "Photo" || type == "Product")
            {
                url = "http://" + Common.Globals.DomainFullName + "/Detail/" + type + "/" + TargetID + "";
            }
            try
            {

                userBind.SendWeiBo(currentUser.UserID, MediaIds, ShareDes,url, imageurl);
            }
            catch (Exception ex)
            {
                Response.Write(ex);
            }
        }
        #endregion

        #region 举报页面
        public ActionResult AjaxCreateReport()
        {
            Maticsoft.BLL.SNS.ReportType Bll = new BLL.SNS.ReportType();
            List<Maticsoft.Model.SNS.ReportType> list = Bll.GetModelList("");
            return View(CurrentThemeViewPath + "/UserProfile/AjaxCreateReport.cshtml", list);

        }
        #endregion

        #region 增加举报
        /// <summary>
        /// 增加举报
        /// </summary>
        /// <param name="Fm"></param>
        /// <returns></returns>
        public ActionResult AjaxAddReport(FormCollection Fm)
        {
            string reportDes = Common.InjectionFilter.Filter(Fm["Description"]);
            int TypeId = Common.Globals.SafeInt(Fm["Type"], 0);
            int TargetId = Common.Globals.SafeInt(Fm["TargetId"], 0);
            Maticsoft.BLL.SNS.Report reportBll = new BLL.SNS.Report();
            Maticsoft.Model.SNS.Report model = new Model.SNS.Report();
            PostsModel = PostsBll.GetModel(TargetId);
            if (PostsModel != null)
            {
                model.Description = reportDes;
                model.CreatedDate = DateTime.Now;
                model.CreatedNickName = currentUser.NickName;
                model.CreatedUserID = currentUser.UserID;
                model.ReportTypeID = TypeId;
                model.TargetID = TargetId;
                if (PostsModel.TargetId > 0)
                {
                    model.TargetID = PostsModel.TargetId;
                }
                model.Status = 0;
                model.TargetType = PostsModel.Type.Value;
                if (reportBll.Add(model) > 0)
                {
                    return Content("Ok");
                }
            }
            return Content("No");
        }
        #endregion

        #region 发布视频方法


        #region 检测视频地址是否正确

        public string GetVideoUrl(string url)
        {
            int videotype = GetType(url);
            if (videotype == 1)
            {
                YouKuInfo info = VideoHelper.GetYouKuInfo(url);
                if (null != info)
                {
                    string VideoUrl = "http://player.youku.com/player.php/sid/" + info.VidEncoded + "/v.swf";
                    string ImageUrl = info.Logo;
                    return VideoUrl + "," + ImageUrl;
                }
            }
            if (videotype == 2)
            {
                Ku6Info info = VideoHelper.GetKu6Info(url);
                if (null != info)
                {
                    string VideoUrl = info.flash;
                    string ImageUrl = info.coverurl;
                    return VideoUrl + "," + ImageUrl;
                }
            }
            return "";
        }

        /// <summary>
        /// 更新用户域名
        /// </summary>
        [HttpPost]
        [ValidateInput(false)]
        public void CheckVideoUrl(string VideoUrl)
        {
            //如果是正确的，那就是视频的原始地址。
            string url = VideoUrl;
            JsonObject json = new JsonObject();

            if (string.IsNullOrWhiteSpace(url))
            {
                //视频地址不能为空！
                json.Accumulate("STATUS", "NotNull");
            }
            else
            {
                int videotype = GetType(url);
                if (videotype == 0)
                {
                    //粘贴的视频地址错误！
                    json.Accumulate("STATUS", "Error");
                }
                else
                {
                    if (videotype == 1)
                    {
                        YouKuInfo info = VideoHelper.GetYouKuInfo(url);
                        if (null != info)
                        {
                            json.Accumulate("STATUS", "Succ");
                            //视频播放地址
                            json.Accumulate("VideoUrl", string.Format("http://player.youku.com/player.php/sid/{0}/v.swf", info.VidEncoded));
                            //视频图片
                            json.Accumulate("ImageUrl", info.Logo);
                            //视频标题
                            json.Accumulate("VideoTitle", info.Title);
                        }
                        else
                        {
                            //粘贴的视频地址错误！
                            json.Accumulate("STATUS", "Error");
                        }
                    }

                    if (videotype == 2)
                    {
                        Ku6Info info = VideoHelper.GetKu6Info(url);
                        if (null != info)
                        {
                            json.Accumulate("STATUS", "Succ");

                            //视频播放地址
                            json.Accumulate("VideoUrl", info.flash);

                            //视频图片
                            json.Accumulate("ImageUrl", info.coverurl);
                            //视频标题
                            json.Accumulate("VideoTitle", info.title);
                        }
                        else
                        {
                            //粘贴的视频地址错误！
                            json.Accumulate("STATUS", "Error");
                        }
                    }
                }
            }
            Response.Write(json.ToString());
            return;
        }

        #endregion 检测视频地址是否正确

        #region 获取视频类型

        /// <summary>
        /// 获取视频类型
        /// </summary>
        public int GetType(string url)
        {
            int type = 0;
            if (VideoHelper.IsYouKuVideoUrl(url))
            {
                type = 1;//优库视频地址
            }
            if (VideoHelper.IsKu6VideoUrl(url))
            {
                type = 2;//酷6视频
            }
            return type;
        }

        #endregion 获取视频类型

        #endregion 检测视频方法

        #region 物理删除文件
        private void PhysicalFileInfo(DataTable dt)
        {
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                for (int n = 0; n < rowsCount; n++)
                {
                    if (dt.Rows[n]["TargetImageURL"] != null && dt.Rows[n]["TargetImageURL"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["TargetImageURL"].ToString());
                    }
                    if (dt.Rows[n]["ThumbImageUrl"] != null && dt.Rows[n]["ThumbImageUrl"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["ThumbImageUrl"].ToString());
                    }
                    if (dt.Rows[n]["NormalImageUrl"] != null && dt.Rows[n]["NormalImageUrl"].ToString() != "")
                    {
                        DeletePhysicalFile(dt.Rows[n]["NormalImageUrl"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 删除物理文件
        /// </summary>
        private void DeletePhysicalFile(string path)
        {
            Maticsoft.Web.Components.FileHelper.DeleteFile(EnumHelper.AreaType.SNS, path);
        }
        #endregion


        #region 获取用户ID

        public ActionResult GetUserId()
        {
            if (currentUser == null)
            {
                return Content("0");
            }
            return Content(currentUser.UserID.ToString());
        }
        #endregion

        
    }
}
