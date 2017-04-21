using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Model.SysManage;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.SNS;
using Webdiyer.WebControls.Mvc;
using Maticsoft.Json;
using System.Web.Script.Serialization;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class ProductController : SNSControllerBase
    {
        #region 全局变量
        protected JavaScriptSerializer jss = new JavaScriptSerializer();
        Maticsoft.BLL.SNS.Categories CateBll = new BLL.SNS.Categories();
        Maticsoft.BLL.SNS.Products ProductBll = new BLL.SNS.Products();
        Maticsoft.BLL.SNS.Photos PhotoBll = new BLL.SNS.Photos();
        Maticsoft.BLL.SNS.TagType TagTypeBll = new BLL.SNS.TagType();
        Maticsoft.BLL.SNS.SearchWordTop SearchBll = new BLL.SNS.SearchWordTop();
        Maticsoft.BLL.SNS.Comments ComBll = new BLL.SNS.Comments();
        private int _basePageSize = 6;
        private int _waterfallSize = 32;
        private int _waterfallDataCount = 1;
        private int commentPagesize = 3;
        #endregion

        public ProductController()
        {
            this._basePageSize = FallInitDataSize;
            this._waterfallSize = FallDataSize;
            this.commentPagesize = CommentDataSize;
        }

        #region 逛宝贝和商品页面的过滤页
        public ActionResult Index(string cname, int topcid, int cid, int minprice, int maxprice, string sequence, string color, int? pageIndex)
        {
            Maticsoft.ViewModel.SNS.ProductCategory Model = CateBll.GetCateListByParentId(topcid);
            Maticsoft.Model.SNS.ProductQuery Query = new Model.SNS.ProductQuery();
            Model.TagsList = TagTypeBll.GetTagListByCid(topcid);
            Model.CurrentSequence = sequence;
            Model.CurrentMinPrice = minprice;
            Model.CurrentMaxPrice = maxprice;
            Model.CurrentCateName = cname;
            //下面表示通过逛宝贝过来的，其相应的类别都是默认的0
            #region 有两种情况 一种是逛宝贝，一种是通过详细的衣服或鞋子类别过来的
            if (topcid == 0 && cid == 0)
            {
                Model.KeyWordList = SearchBll.GetRecommadKeyWordList();
                Maticsoft.Model.SNS.Categories CateModel = CateBll.GetModel(cname);
                Model.CurrentCateName = "社区热搜";
                if (CateModel != null)
                {
                    Query.CategoryID = CateModel.CategoryId;
                }
                else
                {
                    Query.Keywords = (cname != "all" ? cname : null);

                }

            }
            else
            {
                //点击具体的类别
                if (cid > 0)
                {
                    Query.CategoryID = cid;
                }
                else
                {
                    //点击类别下标签
                    Query.CategoryID = topcid;
                    Query.Tags = cname;
                }
            }
            #endregion
            Query.IsTopCategory = (cid == topcid);
            Query.MaxPrice = maxprice;
            Query.MinPrice = minprice;
            Query.Order = sequence;
            Query.Color = color;
            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;
            //重置页面索引
            pageIndex = pageIndex.HasValue && pageIndex.Value > 1 ? pageIndex.Value : 1;
            //计算分页起始索引
            int startIndex = pageIndex.Value > 1 ? (pageIndex.Value - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex.Value > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = 0;
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            ViewBag.CurrentPageAjaxEndIndex = pageIndex * pageSize;

            #region json
            string dataParam = "{";
            foreach (var item in Request.RequestContext.RouteData.Values)
            {
                dataParam += item.Key + ":'" + item.Value + "',";

            }
            dataParam = dataParam.TrimEnd(',') + "}";
            ViewBag.DataParam = dataParam;
            #endregion
            //获取总条数
            Query.QueryType = (int)Maticsoft.Model.SNS.EnumHelper.QueryType.Count;
            toalCount = ProductBll.GetProductCount(Query);

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("ProductList", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, Model.CurrentCateName });   //商品分类名称
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            if (toalCount < 1) return View(Model);   //NO DATA

            //分页获取数据
            Query.QueryType = (int)Maticsoft.Model.SNS.EnumHelper.QueryType.List;
            Model.ProductPagedList = ProductBll.GetProductByPage(Query, startIndex, endIndex).ToPagedList(
                                                    pageIndex ?? 1,
                                                    pageSize,
                                                    toalCount);
            #region 加载评论
            var ProductIdList = (from item in Model.ProductPagedList where item.CommentCount > 0 && !string.IsNullOrEmpty(item.TopCommentsId) select item.TopCommentsId).Distinct().ToArray();
            string CommentsIdString = string.Join(",", ProductIdList).TrimEnd(',');
            Maticsoft.BLL.SNS.Comments comBll = new BLL.SNS.Comments();
            Model.CommentList = comBll.GetCommentByIds(CommentsIdString, (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product);
            #endregion

            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (var item in Model.ProductPagedList)
            {
                if (IsStatic != "true")
                {
                    item.StaticUrl = ViewBag.BasePath + "Product/Detail/" + item.ProductID;
                }
                else
                {
                    item.StaticUrl = (String.IsNullOrWhiteSpace(item.StaticUrl) ? ViewBag.BasePath + "Product/Detail/" + item.ProductID : item.StaticUrl);  // PageSetting.GetProductUrl(item);
                }
            }
            #endregion

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView("ProductList", Model);

            
            return View(Model);
        }
        #endregion

        #region 瀑布流用到的

        /// <summary>
        /// 瀑布流需要的方法
        /// </summary>
        /// <param name="albumID"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>

        [HttpPost]
        [OutputCache(VaryByParam = "*", Duration = SNSAreaRegistration.OutputCacheDuration)]
        public ActionResult WaterfallProductListData(string cname, int topcid, int cid, int minprice, int maxprice, string sequence, string color, int startIndex)
        {
            ViewModel.SNS.ProductCategory Model = new ViewModel.SNS.ProductCategory();
            Maticsoft.Model.SNS.ProductQuery Query = new Maticsoft.Model.SNS.ProductQuery();
            #region 有两种情况 一种是逛宝贝，一种是通过详细的衣服或鞋子类别过来的
            if (topcid == 0 && cid == 0)
            {
                Model.KeyWordList = SearchBll.GetRecommadKeyWordList();
                Maticsoft.Model.SNS.Categories CateModel = CateBll.GetModel(cname);
                Model.CurrentCateName = "社区热搜";
                if (CateModel != null)
                {
                    Query.CategoryID = CateModel.CategoryId;
                }
                else
                {
                    Query.Keywords = (cname != "all" ? cname : null);
                }

            }
            else
            {
                //点击具体的类别
                if (cid > 0)
                {
                    Query.CategoryID = cid;
                }
                else
                {
                    //点击类别下标签
                    Query.CategoryID = topcid;
                    Query.Tags = cname;
                }
            }
            #endregion
            Query.MaxPrice = maxprice;
            Query.MinPrice = minprice;
            Query.Order = sequence;
            Query.IsTopCategory = (cid == topcid);
            Query.Color = color;
            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;

            //重置分页起始索引
            startIndex = startIndex > 1 ? startIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = startIndex > 1 ? startIndex + _waterfallDataCount - 1 : _waterfallDataCount;
            //获取总条数
            int toalCount = ProductBll.GetProductCount(Query);
            if (toalCount < 1) return new EmptyResult();   //NO DATA

            //分页获取数据
            Model.ProductListWaterfall = ProductBll.GetProductByPage(Query, startIndex, endIndex);
            #region 加载评论
            var ProductIdList = (from item in Model.ProductListWaterfall where item.CommentCount > 0 && !string.IsNullOrEmpty(item.TopCommentsId) select item.TopCommentsId).Distinct().ToArray();
            string CommentsIdString = string.Join(",", ProductIdList).TrimEnd(',');
            Maticsoft.BLL.SNS.Comments comBll = new BLL.SNS.Comments();
            Model.CommentList = comBll.GetCommentByIds(CommentsIdString, (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product);
            #endregion

            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
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
            #endregion

            return View("ProductListWaterfall", Model);

        }

        #endregion

        #region  商品或图片的详细页
        /// <summary>
        ///商品或图片的详细页
        /// </summary>
        /// <param name="type"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public ActionResult Detail(int pid)
        {
            if (!ProductBll.Exists(pid))
            {
                return RedirectToAction("Index", "Error");
            }
            Maticsoft.ViewModel.SNS.TargetDetail DetailModel = new ViewModel.SNS.TargetDetail();
            Maticsoft.Model.SNS.ProductQuery Query = new Model.SNS.ProductQuery();
            Maticsoft.BLL.SNS.Tags tagsBll = new BLL.SNS.Tags();
            int TypeInt = (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product;
            int CommentType = (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product;
            DetailModel = ProductBll.GetTargetAssiationInfo(pid);
            #region 可能喜欢的
            //DetailModel.RecommandProduct = ProductBll.GetProductListByCid(DetailModel.Product.CategoryID.Value);
            Query.IsTopCategory = true;
            Query.CategoryID = CateBll.GetTopCidByChildCid(DetailModel.Product.CategoryID.HasValue ? DetailModel.Product.CategoryID.Value : 0);
            Query.Order = "hot";
            DetailModel.RecommandProduct = ProductBll.GetProductByPage(Query, 1, 16);
            #endregion

            #region 静态化路径
            string IsStatic = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNSIsStatic");
            foreach (var item in DetailModel.RecommandProduct)
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
            #endregion

            DetailModel.ProductTagList = tagsBll.GetHotTags(16);
            Maticsoft.BLL.SNS.UserFavourite ufBll = new BLL.SNS.UserFavourite();
            DetailModel.FavCount = ufBll.GetFavCountByTargetId(pid, TypeInt);
            DetailModel.FavUserList = ufBll.GetFavUserByTargetId(pid, TypeInt, 24);
            DetailModel.Commentcount = ComBll.GetCommentCount(CommentType, pid);
            DetailModel.CommentPageSize = commentPagesize;
            var ProductIdList = (from item in DetailModel.RecommandProduct where item.CommentCount > 0 && !string.IsNullOrEmpty(item.TopCommentsId) select item.TopCommentsId).Distinct().ToArray();
            string CommentsIdString = string.Join(",", ProductIdList).TrimEnd(',');
            Maticsoft.BLL.SNS.Comments comBll = new BLL.SNS.Comments();
            DetailModel.CommentList = comBll.GetCommentByIds(CommentsIdString, (int)Maticsoft.Model.SNS.EnumHelper.PostContentType.Product);

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("ProductDetail", ApplicationKeyType.SNS);
            pageSetting.Replace(
                new[] { PageSetting.RKEY_CNAME, DetailModel.Targetname },   //商品名称
                new[] { PageSetting.RKEY_CTAG, DetailModel.Tags });         //商品标签
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion

            #region 淘点金代码
           ViewBag.TaoCode=BLL.SysManage.ConfigSystem.GetValueByCache("OpenAPI_SNS_TaoBaoCode", ApplicationKeyType.OpenAPI); 
            #endregion

            return View(DetailModel);
        }
        #endregion


        #region  商品详细的跳转中间页

        public ActionResult R(int id)
        {
            string url = ProductBll.GetProductUrlByCache(id);
            if (String.IsNullOrWhiteSpace(url))
            {
                return RedirectToAction("Detail", new { pid = id });
            }
            return Redirect(url);
        }

        #endregion

        //#region 添加评论
        ///// <summary>
        /////添加评论
        ///// </summary>
        ///// <param name="Fm"></param>
        ///// <returns></returns>
        //public ActionResult AjaxAddComment(FormCollection Fm)
        //{
        //    if (currentUser == null)
        //    {
        //        return null;
        //    }
        //    Maticsoft.Model.SNS.Comments ComModel = new Model.SNS.Comments();
        //    int TargetId = Common.Globals.SafeInt(Fm["TargetId"], 0);
        //    int Type = Fm["Type"] == "Product" ? (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product : (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo;
        //    int CommentId = 0;
        //    string Des = ViewModel.ViewModelBase.ReplaceFace(Fm["Des"]);
        //    ComModel.CreatedDate = DateTime.Now;
        //    ComModel.CreatedNickName = currentUser.NickName;
        //    ComModel.CreatedUserID = currentUser.UserID;
        //    ComModel.Description = Des;
        //    ComModel.HasReferUser = Des.Contains('@') ? true : false;
        //    ComModel.IsRead = false;
        //    ComModel.ReplyCount = 0;
        //    ComModel.TargetId = TargetId;
        //    ComModel.Type = Type;
        //    ComModel.UserIP = Request.UserHostAddress;
        //    if ((CommentId = ComBll.AddEx(ComModel)) > 0)
        //    {
        //        ComModel.CommentID = CommentId;
        //        // ComModel.Description = ViewModel.ViewModelBase.RegexNickName(ComModel.Description);
        //        List<Maticsoft.Model.SNS.Comments> list = new List<Model.SNS.Comments>();
        //        list.Add(ComModel);
        //        return PartialView("ProductComment", list);

        //    }
        //    return Content("No");

        //}
        //#endregion

        //#region 获取评论
        //public ActionResult AjaxGetCommentsByType(string type, int pid, int? PageIndex)
        //{
        //    int StartIndex = Maticsoft.ViewModel.ViewModelBase.GetStartPageIndex(commentPagesize, PageIndex.Value);
        //    int EndIndex = Maticsoft.ViewModel.ViewModelBase.GetEndPageIndex(commentPagesize, PageIndex.Value);
        //    int CommentType = type == "Product" ? (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product : (int)Maticsoft.Model.SNS.EnumHelper.CommentType.Photo;
        //    List<Maticsoft.Model.SNS.Comments> CommentList = ComBll.GetCommentByPage(CommentType, pid, StartIndex, EndIndex);
        //    return PartialView("ProductComment", CommentList);

        //} 
        //#endregion

        #region 对商品点击数进行计数
        /// <summary>
        /// 统计点击到淘宝的数量
        /// </summary>
        /// <param name="fc"></param>
        /// <returns></returns>
        public ActionResult AjaxUpdateClickCount(FormCollection fc)
        {
            int productId = Common.Globals.SafeInt(fc["ProductId"], 0);
            ProductBll.UpdateClickCount(productId);
            return new EmptyResult();
        }

        #endregion

        #region Ajax 方法
        public ActionResult GetCount(FormCollection Fm)
        {
            if (String.IsNullOrWhiteSpace(Fm["ProductId"]))
            {
                return Content("No");
            }
            else
            {
                int ProductId = Common.Globals.SafeInt(Fm["ProductId"], 0);
                Maticsoft.BLL.SNS.Comments commentBll = new BLL.SNS.Comments();
                Maticsoft.BLL.SNS.UserFavourite ufBll = new BLL.SNS.UserFavourite();

                int favcount = ufBll.GetFavCountByTargetId(ProductId, (int)Maticsoft.Model.SNS.EnumHelper.ImageType.Product);
                int commmentcount = commentBll.GetCommentCount((int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product, ProductId);
                var model = ProductBll.GetTargetAssiationInfo(ProductId);
                return Content(model.Favouritecount + "|" + model.PvCount + "|" + commmentcount + "|" + favcount);
            }
        }

        public ActionResult GetListCounts(FormCollection Fm)
        {
            if (String.IsNullOrWhiteSpace(Fm["ProductIds"]))
            {
                return Content("No");
            }
            else
            {
                string ProductIds = Fm["ProductIds"];
                var ProductId_Arry = ProductIds.Split(',');
                Maticsoft.BLL.SNS.Comments commentBll = new BLL.SNS.Comments();
                Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
                string result = "";
                int i = 0;
                foreach (var item in ProductId_Arry)
                {
                    int ProductId = Common.Globals.SafeInt(item, 0);
                    var model = ProductBll.GetTargetAssiationInfo(ProductId);
                    int commmentcount = commentBll.GetCommentCount((int)Maticsoft.Model.SNS.EnumHelper.CommentType.Product, ProductId);
                    if (i == 0)
                    {
                        result = ProductId + "," + model.Favouritecount + "," + commmentcount;
                    }
                    else
                    {
                        result = result + "|" + ProductId + "," + model.Favouritecount + "," + commmentcount;
                    }
                    i++;
                }
                return Content(result);
            }
        }

        /// <summary>
        /// 获取商品是否开启本地跳转
        /// </summary>
        /// <returns></returns>
        public ActionResult GetProductIsR()
        {
            bool IsOpen = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("SNS_Product_OpenRedirect");
            return Content(IsOpen.ToString());
        }

        #endregion
    }
}
