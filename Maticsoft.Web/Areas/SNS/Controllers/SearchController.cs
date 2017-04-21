using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdiyer.WebControls.Mvc;
using Maticsoft.Web.Components.Setting.SNS;
namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class SearchController : SNSControllerBase
    {
        private int _basePageSize = 6;
        private int _baseAlbumPageSize = 16;
        private int _waterfallAlbumSize = 16;
        private int _waterfallSize = 30;
        private int _waterfallDetailCount = 1;

        public SearchController()
        {
            this._basePageSize = FallInitDataSize;
            this._waterfallSize = FallDataSize;
            this._baseAlbumPageSize = FallInitDataSize;
            this._waterfallAlbumSize = FallDataSize;
        }

        #region 搜索前加入日志
        public void LogKeyWord(string q)
        {
            Maticsoft.BLL.SNS.SearchWordLog LogBll = new BLL.SNS.SearchWordLog();
            Maticsoft.Model.SNS.SearchWordLog LogModel = new Model.SNS.SearchWordLog();
            if (!string.IsNullOrWhiteSpace(q))
            {
                LogModel.CreatedDate = DateTime.Now;
                if (currentUser != null)
                {
                    LogModel.CreatedNickName = currentUser.NickName;
                    LogModel.CreatedUserId = currentUser.UserID;

                }
                LogModel.SearchWord = q;    
                LogBll.Add(LogModel);
            }
        
        }
        
        #endregion


        #region 搜索商品

        public ActionResult Product(int pageIndex=1)
        {
            ViewBag.Title = "商品搜索";
            string keyword = String.IsNullOrWhiteSpace(Request.Params["keyword"]) ? "" : Server.UrlDecode(Request.Params["keyword"]);
            string type = String.IsNullOrWhiteSpace(Request.Params["type"]) ? "hot" : Request.Params["type"];

            keyword = Common.InjectionFilter.Filter(keyword);
            LogKeyWord(keyword);
            Maticsoft.ViewModel.SNS.ProductCategory Model = new ViewModel.SNS.ProductCategory();
            Maticsoft.BLL.SNS.Products ProductBll=new BLL.SNS.Products();
            Maticsoft.Model.SNS.ProductQuery Query = new Model.SNS.ProductQuery();
            Query.Order = type;
            ViewBag.sequence = type;
            Query.Keywords = keyword;
          
            Query.CategoryID = -1;
            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;
            ///测试代码
            //重置页面索引
     
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = 0;
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = pageIndex * pageSize;
            //#region json
            //string dataParam = "{";
            //foreach (var item in Request.RequestContext.RouteData.Values)
            //{
            //    dataParam += item.Key + ":'" + item.Value + "',";

            //}
            //dataParam = dataParam.TrimEnd(',') + "}";
            //ViewBag.DataParam = dataParam;
            //#endregion
            //获取总条数
            Query.QueryType = (int)Maticsoft.Model.SNS.EnumHelper.QueryType.Count;
            toalCount = ProductBll.GetProductCount(Query);
            if (toalCount < 1) return View(Model);   //NO DATA
            //分页获取数据
            Query.QueryType = (int)Maticsoft.Model.SNS.EnumHelper.QueryType.List;
            Model.ProductPagedList = ProductBll.GetProductByPage(Query, startIndex, endIndex).ToPagedList(
                                                    pageIndex,
                                                    pageSize,
                                                    toalCount);
            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            if (Model.ProductPagedList != null && Model.ProductPagedList.Count > 0)
            {
                foreach (var item in Model.ProductPagedList)
                {
                    if (IsStatic != "true")
                    {
                        item.StaticUrl = ViewBag.BasePath + "Product/Detail/" + item.ProductID;
                    }
                    else
                    {
                       item.StaticUrl = (String.IsNullOrWhiteSpace(item.StaticUrl) ? ViewBag.BasePath + "Product/Detail/" + item.ProductID : item.StaticUrl);
                    }
                }
            }
            #endregion

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView("ProductList", Model);
            return View(Model);
        } 
       
        #region 瀑布流用到的
        /// <summary>
        /// 瀑布流需要的方法
        /// </summary>
        /// <param name="albumID"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult WaterfallProductListData(string keyword, string type, int startIndex)
        {
            keyword = Common.InjectionFilter.Filter(keyword);
            ViewModel.SNS.ProductCategory Model = new ViewModel.SNS.ProductCategory();
            Maticsoft.Model.SNS.ProductQuery Query = new Maticsoft.Model.SNS.ProductQuery();
            Maticsoft.BLL.SNS.Products ProductBll = new BLL.SNS.Products();
            Query.Order = type;
            Query.Keywords = keyword;
            Query.CategoryID = -1;
            int pageSize = _basePageSize + _waterfallDetailCount;
            ViewBag.BasePageSize = _basePageSize;
            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDetailCount - 1 : _waterfallDetailCount;
            //获取总条数
            int toalCount = ProductBll.GetProductCount(Query);
            if (toalCount < 1) return new EmptyResult();   //NO DATA
            //分页获取数据
            Model.ProductListWaterfall = ProductBll.GetProductByPage(Query, startIndex, endIndex);
            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            if (Model.ProductListWaterfall != null && Model.ProductListWaterfall.Count > 0)
            {
                foreach (var item in Model.ProductListWaterfall)
                {
                    if (IsStatic != "true")
                    {
                        item.StaticUrl = ViewBag.BasePath + "Product/Detail/" + item.ProductID;
                    }
                    else
                    {
                       item.StaticUrl = (String.IsNullOrWhiteSpace(item.StaticUrl) ? ViewBag.BasePath + "Product/Detail/" + item.ProductID : item.StaticUrl);
                    }
                }
            }
            #endregion
            return View("ProductListWaterfall", Model);
        }
        
        #endregion
 #endregion


        #region 搜索用户
        public new  ActionResult  User(int? page)
        {
            ViewBag.Title = "用户搜索";
            string keyword = String.IsNullOrWhiteSpace(Request.Params["keyword"]) ? "" : Server.UrlDecode(Request.Params["keyword"]);
            keyword = Common.InjectionFilter.Filter(keyword);
            Maticsoft.BLL.SNS.UserShip bllUserShip = new Maticsoft.BLL.SNS.UserShip();
            Maticsoft.BLL.Members.UsersExp userexBll = new BLL.Members.UsersExp();
            //重置页面索引
            page = page.HasValue && page.Value > 1 ? page.Value : 1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = page.Value > 1 ? (page.Value - 1) * pagesize + 1 : 0;
            //计算分页结束索引
            int endIndex = page.Value * pagesize;
            //总记录数
            int toalcount = userexBll.GetUserCountByKeyWord(keyword);
            PagedList<Maticsoft.Model.Members.UsersExpModel> FansList = null;
            List<Maticsoft.Model.Members.UsersExpModel> list = userexBll.GetUserListByKeyWord(keyword, "", startIndex, endIndex);
            if (currentUser != null)
            {
                //var UserIdIdList = (from item in list select item.UserID).ToArray();
                //string UserIdIdString = string.Join(",", UserIdIdList);
                List<Maticsoft.Model.SNS.UserShip> shipList = bllUserShip.GetModelList("ActiveUserID = " + currentUser.UserID + "");
                if (shipList!=null&&shipList.Count > 0)
                {
                    list.ForEach(item => item.IsFellow = UserIsFellow(item.UserID, shipList));
                }
            }
            if (list != null && list.Count > 0)
            {
                FansList = new PagedList<Maticsoft.Model.Members.UsersExpModel>(list, page ?? 1, pagesize, toalcount);
            }
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/Search/UserList.cshtml", FansList);
            return View(CurrentThemeViewPath + "/Search/User.cshtml", FansList);

        }

        public bool UserIsFellow(int UserId, List<Maticsoft.Model.SNS.UserShip> shipList)
        {
            if (shipList.Count(item => item.PassiveUserID == UserId) > 0)
            {

                return true;
            }
            return false;
        } 
        #endregion


        #region 搜索专辑
        public ActionResult Albums(int pageIndex = 1)
        {
            ViewBag.Title = "专辑搜索";
            string keyword = String.IsNullOrWhiteSpace(Request.Params["keyword"]) ? "" : Server.UrlDecode(Request.Params["keyword"]);
            string type = String.IsNullOrWhiteSpace(Request.Params["type"]) ? "hot" : Request.Params["type"];
            keyword = Common.InjectionFilter.Filter(keyword);
            LogKeyWord(keyword);
            int pageSize = _baseAlbumPageSize + _waterfallAlbumSize;
            ViewBag.BasePageSize = _baseAlbumPageSize;
            ViewBag.sequence = type;
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex > 1 ? startIndex + _baseAlbumPageSize - 1 : _baseAlbumPageSize;
            int toalCount = 0;

            BLL.SNS.UserAlbums bllAlbums = new BLL.SNS.UserAlbums();

            //获取总条数
            toalCount = bllAlbums.GetCountByKeyWard(keyword);

            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            int ajaxEndIndex = pageIndex * pageSize;
            ViewBag.CurrentPageAjaxEndIndex = ajaxEndIndex > toalCount ? toalCount : ajaxEndIndex;

            if (toalCount < 1) return View("Album");   //NO DATA
 
 
            PagedList<ViewModel.SNS.AlbumIndex> models = bllAlbums.GetListByKeyWord(keyword, type,
                                                startIndex, endIndex, UserAlbumDetailType).ToPagedList(
                                                    pageIndex ,
                                                    pageSize,
                                                    toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView("AlbumList", models);
            return View("Album",models);

        }

        [HttpPost]
        public ActionResult WaterfallAlbumListData(string keyword, string type, int startIndex)
        {
            keyword = Common.InjectionFilter.Filter(keyword);
            int pageSize = _baseAlbumPageSize + _waterfallAlbumSize;
            ViewBag.BasePageSize = _baseAlbumPageSize;

            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDetailCount - 1 : _waterfallDetailCount;
            int toalCount = 0;

            BLL.SNS.UserAlbums bllAlbums = new BLL.SNS.UserAlbums();

            //获取总条数
            toalCount = bllAlbums.GetCountByKeyWard(keyword);
            if (toalCount < 1) return new EmptyResult();   //NO DATA
         
            //分页获取数据
            List<ViewModel.SNS.AlbumIndex> models = bllAlbums.GetListByKeyWord(
                keyword, type, startIndex, endIndex, UserAlbumDetailType);

            return View("AlbumListWaterfall", models);

        }
        #endregion


        #region 搜索小组
        public ActionResult Topics(string keyword, string type, int? pageIndex)
        {
            ViewBag.Title = "帖子搜索";
            keyword = Common.InjectionFilter.Filter(keyword);
            Maticsoft.BLL.SNS.Groups Gbll = new BLL.SNS.Groups();
            Maticsoft.BLL.SNS.GroupTopics Tbll = new BLL.SNS.GroupTopics();
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pagesize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value * pagesize;
            //总记录数
            int toalcount = 0;
            toalcount = Tbll.GetCountByKeyWord(keyword);
            PagedList<Maticsoft.Model.SNS.GroupTopics> TopicList = new PagedList<Maticsoft.Model.SNS.GroupTopics>(
                Tbll.SearchTopicByKeyWord(startIndex, endIndex, keyword, 0, type)
                , pageIndex ?? 1, pagesize, toalcount);
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/Search/TopicList.cshtml", TopicList);
            ViewBag.sequence = type == "newreply" ? "newreply" : "newpost";
            return View(CurrentThemeViewPath + "/Search/Topic.cshtml", TopicList);
        }

        //public ActionResult Groups(string q)
        //{
        //    if (!string.IsNullOrEmpty(q))
        //    {
        //        Maticsoft.BLL.SNS.Groups bll = new BLL.SNS.Groups();
        //        List<Maticsoft.Model.SNS.Groups> list = new List<Model.SNS.Groups>();
        //        list = bll.GetGroupListByKeyWord(q);
        //        if (list.Count > 0)
        //        {
        //            return View(list);
        //        }
        //    }
        //    return new EmptyResult();
        //}
        public ActionResult Groups( int? pageIndex)
        {
            ViewBag.Title = "小组搜索";
            string keyword = String.IsNullOrWhiteSpace(Request.Params["keyword"]) ? "" : Server.UrlDecode(Request.Params["keyword"]);
            string type = String.IsNullOrWhiteSpace(Request.Params["type"]) ? "hot" : Request.Params["type"];
            int Rec = String.IsNullOrWhiteSpace(Request.Params["Rec"]) ?-1 : Common.Globals.SafeInt(Request.Params["Rec"],-1);
            keyword = Common.InjectionFilter.Filter(keyword);
            Maticsoft.BLL.SNS.Groups Gbll = new BLL.SNS.Groups();
            Maticsoft.ViewModel.SNS.GroupSearch Model = new ViewModel.SNS.GroupSearch();
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //页大小
            int pagesize = 10;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pagesize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value * pagesize;
            //总记录数
            int toalcount = 0;
            toalcount = Gbll.GetCountByKeyWord(keyword, Rec);
            PagedList<Maticsoft.Model.SNS.Groups> GroupList = new PagedList<Maticsoft.Model.SNS.Groups>(
                Gbll.GetGroupListByKeyWord(startIndex, endIndex, type, keyword, Rec)
                , pageIndex ?? 1, pagesize, toalcount);
            Model.SearchList = GroupList;
            Model.RecommandList = Gbll.GetGroupListByRecommendType(3, Maticsoft.Model.SNS.EnumHelper.GroupRecommend.Index);
            Model.HotList = Gbll.GetHotGroupList(3);
            if (Request.IsAjaxRequest())
                return PartialView(CurrentThemeViewPath + "/Search/GroupList.cshtml", Model);
            return View(CurrentThemeViewPath + "/Search/Groups.cshtml", Model);
        }
        #endregion


        #region 搜索图片

        public ActionResult Photo(int? pageIndex)
        {
            ViewBag.Title = "图片搜索";
            string keyword = String.IsNullOrWhiteSpace(Request.Params["keyword"]) ? "" : Server.UrlDecode(Request.Params["keyword"]);
            string type = String.IsNullOrWhiteSpace(Request.Params["type"]) ? "hot" : Request.Params["type"];
            keyword = Common.InjectionFilter.Filter(keyword);
            LogKeyWord(keyword);
            ViewModel.SNS.PhotoList photoList = new ViewModel.SNS.PhotoList();
            Maticsoft.BLL.SNS.Photos bllPhotos = new BLL.SNS.Photos();
            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;

            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = 0;

            //获取总条数
            toalCount = bllPhotos.GetSearchCountByQ(keyword);
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            int ajaxEndIndex = pageIndex.Value * pageSize;
            ViewBag.CurrentPageAjaxEndIndex = ajaxEndIndex > toalCount ? toalCount : ajaxEndIndex;
            //#region json
            //string dataParam = "{";
            //foreach (var item in Request.RequestContext.RouteData.Values)
            //{
            //    dataParam += item.Key + ":'" + item.Value + "',";

            //}
            //dataParam = dataParam.TrimEnd(',') + "}";
            //ViewBag.DataParam = dataParam;
            //#endregion
            if (toalCount < 1) return View(photoList);   //NO DATA
            ViewBag.sequence = type;
            //分页获取数据
            photoList.PhotoPagedList = bllPhotos.GetListByKeyWord(
                keyword, type, startIndex, endIndex).ToPagedList(
                                                    pageIndex ?? 1,
                                                    pageSize,
                                                    toalCount);
            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            if (photoList.PhotoPagedList != null && photoList.PhotoPagedList.Count > 0)
            {
                foreach (var item in photoList.PhotoPagedList)
                {
                    if (IsStatic != "true")
                    {
                        item.StaticUrl = ViewBag.BasePath + "Photo/Detail/" + item.TargetId;
                    }
                    else
                    {
                      item.StaticUrl = (String.IsNullOrWhiteSpace(item.StaticUrl) ? ViewBag.BasePath + "Photo/Detail/" + item.TargetId : item.StaticUrl);
                    }
                }
            }
            #endregion
            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView("PhotoList", photoList);
            return View(photoList);
        }

        #region 瀑布流用到的
        /// <summary>
        /// 瀑布流需要的方法
        /// </summary>
        /// <param name="albumID"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>

        [HttpPost]
        public ActionResult WaterfallPhotoListData(string keyword, string type, int startIndex)
        {
            keyword = Common.InjectionFilter.Filter(keyword);
            Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
             ViewModel.SNS.PhotoList photoList = new ViewModel.SNS.PhotoList();
            int pageSize = _basePageSize + _waterfallDetailCount;
            ViewBag.BasePageSize = _basePageSize;
            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDetailCount - 1 : _waterfallDetailCount;
            //获取总条数
            int toalCount = photoBll.GetSearchCountByQ(keyword);
            if (toalCount < 1) return new EmptyResult();   //NO DATA
            //分页获取数据
            photoList.PhotoListWaterfall = photoBll.GetListByKeyWord(keyword, type, startIndex, endIndex);
            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            if (photoList.PhotoListWaterfall != null && photoList.PhotoListWaterfall.Count > 0)
            {
                foreach (var item in photoList.PhotoListWaterfall)
                {
                    if (IsStatic != "true")
                    {
                        item.StaticUrl = ViewBag.BasePath + "Photo/Detail/" + item.TargetId;
                    }
                    else
                    {
                      item.StaticUrl = (String.IsNullOrWhiteSpace(item.StaticUrl) ? ViewBag.BasePath + "Photo/Detail/" + item.TargetId : item.StaticUrl);
                    }
                }
            }
            #endregion
            return View(CurrentThemeViewPath + "/Photo/PhotoListWaterfall.cshtml", photoList);
        }

        #endregion
        #endregion
        //public ActionResult GetProductAssociation
    }
}
