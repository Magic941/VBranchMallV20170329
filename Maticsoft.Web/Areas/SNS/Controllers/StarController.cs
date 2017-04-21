using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.Components.Setting;
using Maticsoft.Web.Components.Setting.SNS;
using Webdiyer.WebControls.Mvc;
using System.IO;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class StarController : SNSControllerBase
    {
        //
        // GET: /SNS/Star/
        Maticsoft.BLL.SNS.StarType starTypeBll = new BLL.SNS.StarType();
        Maticsoft.BLL.SNS.Star starBll = new BLL.SNS.Star();
        Maticsoft.BLL.SNS.StarRank starRankBll = new BLL.SNS.StarRank();

        private int _basePageSize = 6;
        private int _waterfallSize = 30;
        private int _waterfallDetailCount = 1;

        public ActionResult Pioneer(int pageIndex = 1, int StarType = 0)
        {
            ViewBag.Title = "达人排行";
            BLL.SNS.StarType bllType = new BLL.SNS.StarType();

            ViewModel.SNS.Stars models = new ViewModel.SNS.Stars();
            if (StarType == 0)
            {
                ViewBag.Type = "Index";
                ViewBag.TypeName = "综合推荐";
                models.HotStarList = starRankBll.HotStarList();
            }
            else
            {
                Model.SNS.StarType starType = bllType.GetModel(StarType);
                ViewBag.TypeName = starType.TypeName;
            }

            #region SEO 优化设置
            IPageSetting pageSetting = PageSetting.GetPageSetting("Star", ApplicationKeyType.SNS);
            ViewBag.Title = pageSetting.Title;
            ViewBag.Keywords = pageSetting.Keywords;
            ViewBag.Description = pageSetting.Description;
            #endregion
            models.StarNewList = starBll.GetStarNewList(StarType);//新晋达人
            models.StarRankList = starRankBll.GetStarRankList(StarType);//该类型下的达人总排行
            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;
            //重置页面索引
            pageIndex = pageIndex > 1 ? pageIndex : 1;
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex > 1 ? startIndex + _basePageSize - 1 : _basePageSize;
            int toalCount = 0;
            //获取总条数
            toalCount = starBll.GetCountByType(StarType);
            //瀑布流Index
            ViewBag.CurrentPageAjaxStartIndex = endIndex;
            int ajaxEndIndex = pageIndex * pageSize;
            ViewBag.CurrentPageAjaxEndIndex = ajaxEndIndex > toalCount ? toalCount : ajaxEndIndex;
            int CurrentUserId = currentUser != null ? currentUser.UserID : 0;
            if (toalCount < 1 && StarType != 0)
                return RedirectToAction("Pioneer");//NO DATA

            models.StarPagedList = starBll.GetListForPage(StarType, "",
                                                 startIndex, endIndex, CurrentUserId).ToPagedList(
                                                    pageIndex,
                                                    pageSize,
                                                    toalCount);

            //检测Ajax请求, 进行无刷新分页
            if (Request.IsAjaxRequest())
                return PartialView("StarList", models);

            

            return View(models);
        }

        public ActionResult Index()
        {
            //明星达人
            //晒货达人
            return View();
        }
        /// <summary>
        /// 申请达人
        /// </summary>
        /// <returns></returns>
        public ActionResult Apply()
        {
            ViewBag.Title = "申请达人";
            if (CurrentUser != null)
            {
                Maticsoft.ViewModel.SNS.Star model = new ViewModel.SNS.Star();
                List<Maticsoft.Model.SNS.StarType> list = starTypeBll.GetModelList("");
                var dropList = new List<SelectListItem>();
                dropList.Add(new SelectListItem { Value = "0", Text = "请选择" });
                if (list != null && list.Count > 0)
                {
                    foreach (Maticsoft.Model.SNS.StarType item in list)
                    {
                        dropList.Add(new SelectListItem { Value = item.TypeID.ToString(), Text = item.TypeName });
                    }
                }
                model.DropList = dropList;
                return View(model);
            }
            else
            {
                return RedirectToAction("Pioneer", "Star");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Apply(ViewModel.SNS.Star model)
        {
            ViewBag.Title = "申请达人";
            if (CurrentUser != null)
            {
                if (starBll.Exists(currentUser.UserID, model.StarModel.TypeID))
                {
                    ViewBag.Type = "Exists";
                    return View(model);
                }
                else
                {
                    model.StarModel.UserID = CurrentUser.UserID;
                    model.StarModel.Status = 0;
                    model.StarModel.NickName = CurrentUser.NickName;
                    model.StarModel.CreatedDate = DateTime.Now;
                    if (!model.StarModel.UserGravatar.Contains("http://"))
                    {
                        FileInfo file = new FileInfo(model.StarModel.UserGravatar);
                        //string ext = file.Name;
                        string StarImagePath = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("StarImagePath");
                        if (String.IsNullOrEmpty(StarImagePath))
                        {
                            StarImagePath = "/Upload/SNS/Images/Star/";
                        }
                        var imageUrl = StarImagePath + file.Name;
                        if (!System.IO.Directory.Exists(StarImagePath))
                        {
                            System.IO.Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(StarImagePath));

                        }
                        if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(model.StarModel.UserGravatar)))
                        {
                            System.IO.File.Move(System.Web.HttpContext.Current.Server.MapPath(model.StarModel.UserGravatar), System.Web.HttpContext.Current.Server.MapPath(imageUrl));
                        }
                        model.StarModel.UserGravatar = imageUrl;
                    }
                 
                    if (starBll.Add(model.StarModel) > 1)
                    {
                        ViewBag.Type = "Success";
                        return View(model);
                    }
                }
            }
            return View(model);
        }
        /// <summary>
        /// 达人列表页 （瀑布流）
        /// </summary>
        /// <param name="AlbumType"></param>
        /// <param name="startIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult WaterfallStarListData(int StartIndex, int StarType = 0)
        {
            int pageSize = _basePageSize + _waterfallSize;
            ViewBag.BasePageSize = _basePageSize;

            //重置分页起始索引
            StartIndex = StartIndex > 1 ? StartIndex + 1 : 0;
            //计算分页结束索引
            int endIndex = StartIndex > 1 ? StartIndex + _waterfallDetailCount - 1 : _waterfallDetailCount;
            int toalCount = 0;

            //获取总条数
            toalCount = starBll.GetCountByType(StarType);
            int CurrentUserId = currentUser != null ? currentUser.UserID : 0;
            if (toalCount < 1) return new EmptyResult();   //NO DATA
            ViewModel.SNS.Stars models = new ViewModel.SNS.Stars();
            //分页获取数据
            models.StarList = starBll.GetListForPage(StarType, "", StartIndex, endIndex, CurrentUserId);

            return View("StarListWaterfall", models);

        }

        #region 达人类型
        public ActionResult StarType()
        {
            Maticsoft.BLL.SNS.StarType typeBll = new BLL.SNS.StarType();
            ViewBag.StarType = Request.Params["StarType"];
            List<Maticsoft.Model.SNS.StarType> list = typeBll.GetModelList("");
            if (list != null && list.Count > 0)
            {
                return View("_StarType", list);
            }
            return View("_StarType");
        }

        #endregion
    }
}
