using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.Settings;
using Maticsoft.BLL.Shop.Supplier;
using Maticsoft.Common;
using Maticsoft.Components.Setting;
using Maticsoft.Model.Shop.Order;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Model.SysManage;
using Maticsoft.ViewModel.Shop;
using Maticsoft.Web.Components.Setting.Shop;
using Webdiyer.WebControls.Mvc;
using Maticsoft.Accounts.Bus;
using Maticsoft.Json;
using Maticsoft.BLL.SysManage;



namespace Maticsoft.Web.Areas.Supplier.Controllers
{
    public class HomeController : SupplierControllerBase
    {
        //
        // GET: /Shop/Home/
        BLL.Shop.Products.SKUInfo SUKManage = new BLL.Shop.Products.SKUInfo();
        BLL.Members.UsersExp uBll = new BLL.Members.UsersExp();
        Model.Members.UsersExpModel uModel = new Model.Members.UsersExpModel();
        Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new SupplierInfo();
        private Maticsoft.BLL.SysManage.SysTree sm = new Maticsoft.BLL.SysManage.SysTree();
        Maticsoft.BLL.Shop.Supplier.SupplierAD supplierAdBll=new SupplierAD();
        Maticsoft.BLL.Shop.Supplier.SupplierMenus bllSupMenu=new SupplierMenus();
        SupplierConfig supplierConfig = new SupplierConfig();
        public ActionResult Index()
        {
            return View();
        }
        #region 修改资料
        public ActionResult UserModify()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
                Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);
                if (null != model)
                {
                    return View(model);
                }
            }
            return RedirectToAction("Login", "Account");//去登录
        }
        [HttpPost]
        public ActionResult UserModify(string txtName, string txtTrueName, string txtEmail)
        {
            AccountsPrincipal user = new AccountsPrincipal(txtName.Trim());
            Maticsoft.Accounts.Bus.User currentUser = new Maticsoft.Accounts.Bus.User(user);
            currentUser.UserName = txtName;
            currentUser.TrueName = txtTrueName.Trim();
            currentUser.Email = txtEmail.Trim();
            JsonObject json = new JsonObject();
            if (currentUser.Update())
            {
                json.Put("Result", "OK");
                return Json(json);
            }
            else
            {
                json.Put("Result", "NO");
                return Json(json);
            }
        }
        #endregion

        #region 修改密码
        public ActionResult UserPass()
        {
            Maticsoft.Accounts.Bus.User currentUser = this.CurrentUser;
            return View(currentUser);
        }

        [HttpPost]
        public ActionResult UserPass(string oldPassword, string newPassword, string confirmPassword)
        {
            if (!HttpContext.User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");//去登录
            SiteIdentity SID = new SiteIdentity(User.Identity.Name);
            JsonObject json = new JsonObject();
            if (SID.TestPassword(oldPassword) == 0)
            {
                json.Put("Result", "Error");
                return Json(json);
            }
            else
            {
                if (newPassword.Trim() != confirmPassword.Trim())
                {
                    json.Put("Result", "ConfirmError");
                    return Json(json);
                }
                else
                {
                    Maticsoft.Accounts.Bus.User currentUser = CurrentUser;
                    currentUser.Password = AccountsPrincipal.EncryptPassword(newPassword); if (!currentUser.Update())
                    {
                        json.Put("Result", "NO");
                        return Json(json);
                    }
                    else
                    {
                        json.Put("Result", "OK");
                        return Json(json);
                    }

                }
            }

        }
        #endregion

        #region 用户信息
        public ActionResult UserInfo()
        {
            //Maticsoft.Accounts.Bus.User currentUser = this.CurrentUser;
            BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
            Model.Members.UsersExpModel model = userEXBll.GetUsersModel(CurrentUser.UserID);

            ViewBag.userIP = Request.UserHostAddress;
            return View(model);
        }
        #endregion

        #region 供货统计
        public ActionResult Supply(string viewName = "Supply")
        {
            BLL.Shop.Supplier.SupplierInfo bll = new BLL.Shop.Supplier.SupplierInfo();
            DataSet ds = bll.GetStatisticsSupply(SupplierId);

            //剩余
            int? remainQuantity = ds.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? remainPrice = ds.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!remainQuantity.HasValue) remainQuantity = 0;
            if (!remainPrice.HasValue) remainPrice = 0;
            ViewBag.RemainQuantity = remainQuantity.Value.ToString();
            ViewBag.RemainPrice = remainPrice.Value.ToString("C2");

            //已售
            int? soldQuantity = ds.Tables[0].Rows[1].Field<int?>("ToalQuantity");
            decimal? soldPrice = ds.Tables[0].Rows[1].Field<decimal?>("ToalPrice");
            if (!soldQuantity.HasValue) soldQuantity = 0;
            if (!soldPrice.HasValue) soldPrice = 0;
            ViewBag.SoldQuantity = soldQuantity.Value.ToString();
            ViewBag.SoldPrice = soldPrice.Value.ToString("C2");

            ViewBag.ToalQuantity = (remainQuantity + soldQuantity).Value.ToString();
            ViewBag.ToalPrice = (remainPrice + soldPrice).Value.ToString("C2");
            return View(viewName);
        }
        #endregion

        #region 销量/业绩走势图
        public int SalesType
        {
            get
            {
                return Globals.SafeInt(Request.QueryString["SalesType"], 0);
            }
        }

        public ActionResult SalesLine(int selectYear = 2013, string viewName = "SalesLine")
        {
            string pageTitle;
            switch (SalesType)
            {
                case 2:
                    pageTitle = "业绩";
                    break;
                default:
                    pageTitle = "销量";
                    break;
            }
            ViewBag.pageTitel = pageTitle;
            BLL.Shop.Supplier.SupplierInfo bll = new BLL.Shop.Supplier.SupplierInfo();
            DataSet ds = bll.GetStatisticsSales(SupplierId, Globals.SafeInt(selectYear, DateTime.Now.Year));

            string str = CreateXmlStr(ds, SalesType, string.Format("{0}走势图", pageTitle));
            ViewBag.litChart = FusionCharts.RenderChart("/FusionCharts/Line.swf", "", str, "FusionChartsLine", "900", "500", false, true);
            return View(viewName);
        }


        private string CreateXmlStr(DataSet ds, int type, string litTitle)
        {
            string chartTitle = litTitle;
            string chartConfig = string.Empty;
            switch (type)
            {
                case 2:
                    chartConfig = " yAxisName='金额（元）' numberPrefix ='￥' ";
                    break;
                default:
                    break;
            }

            if (DataSetTools.DataSetIsNull(ds)) return "";

            StringBuilder xmlData = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
            {
                xmlData.AppendFormat(
                    "<?xml version='1.0' encoding='utf-8' ?><chart caption='{0}' xAxisName='月份' " + chartConfig + " showValues='1' showhovercap='0'  formatNumberScale='0' showBorder='0' palette='2' animation='1'  showPercentInToolTip='0'> ",
                    chartTitle);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    xmlData.AppendFormat("<set label='{0}' ", ds.Tables[0].Rows[i].Field<int>("Mon"));
                    switch (type)
                    {
                        case 2:
                            xmlData.AppendFormat(" value='{0}' />", ds.Tables[0].Rows[i].Field<decimal>("ToalPrice").ToString("F"));
                            break;
                        default:
                            xmlData.AppendFormat(" value='{0}' />", ds.Tables[0].Rows[i].Field<int>("ToalQuantity"));
                            break;
                    }
                }
                xmlData.Append("</chart>");
            }
            return xmlData.ToString();

        }
        #endregion


        #region 用户统计
        public ActionResult UserCount(FormCollection fm,string viewName = "UserCount")
        {
            ViewBag.StartDate = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime startDate = Globals.SafeDateTime(fm["StartDate"], DateTime.Now.AddYears(-1));
            DateTime endDate = Globals.SafeDateTime(fm["EndDate"], DateTime.Now);
            startDate = startDate.Date;
            endDate = endDate.Date.AddDays(1).AddSeconds(-1);
            Model.Shop.Order.StatisticMode mode = (Model.Shop.Order.StatisticMode)Globals.SafeInt(fm["rdoMode"], 0);
            if (mode.ToString() == "Day")
            {
                TimeSpan time = endDate - startDate;
                if (time.Days > 31)
                {
                    startDate = endDate.AddDays(-31);

                }
            }
            Maticsoft.BLL.Members.Users user = new BLL.Members.Users();
            DataSet ds = user.GetUserCount(mode, startDate, endDate);
            string userInfo = "user";
            string str = CreateXml(ds, mode, userInfo);
            ViewBag.litChart = FusionCharts.RenderChart("/FusionCharts/Line.swf", "", str, "FusionChartsLine", "900", "500", false, true);
            ViewBag.Mode = mode.ToString();
            return View(viewName);
        }
        #endregion

        #region 订单统计
        public ActionResult Order(FormCollection fm,string viewName ="Order")
        {
            ViewBag.StartDate = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd");
            ViewBag.EndDate = DateTime.Now.ToString("yyyy-MM-dd");
            DateTime startDate = Globals.SafeDateTime(fm["StartDate"], DateTime.Now.AddYears(-1));
            DateTime endDate = Globals.SafeDateTime(fm["EndDate"], DateTime.Now);
            startDate = startDate.Date;
            endDate = endDate.Date.AddDays(1).AddSeconds(-1);

            //未支付
            DataSet dsNotPay = BLL.Shop.Order.OrderManage.Stat4OrderStatus(0, startDate, endDate);
            int? notPayQuantity = dsNotPay.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? notPayPrice = dsNotPay.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!notPayQuantity.HasValue) notPayQuantity = 0;
            if (!notPayPrice.HasValue) notPayPrice = 0;
            ViewBag.lblNotPayQuantity = notPayQuantity.Value.ToString();
            ViewBag.lblNotPayPrice = notPayPrice.Value.ToString("C2");

            //已支付 进行中
            DataSet dsPayment = BLL.Shop.Order.OrderManage.Stat4OrderStatus(1,startDate, endDate);
            int? paymentQuantity = dsPayment.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? paymentPrice = dsPayment.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!paymentQuantity.HasValue) paymentQuantity = 0;
            if (!paymentPrice.HasValue) paymentPrice = 0;
            ViewBag.lblPaymentQuantity = paymentQuantity.Value.ToString();
            ViewBag.lblPaymentPrice = paymentPrice.Value.ToString("C2");

            //已完成
            DataSet dsComplete = BLL.Shop.Order.OrderManage.Stat4OrderStatus(2, startDate, endDate);
            int? completeQuantity = dsComplete.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? completePrice = dsComplete.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!completeQuantity.HasValue) completeQuantity = 0;
            if (!completePrice.HasValue) completePrice = 0;
            ViewBag.lblCompleteQuantity = completeQuantity.Value.ToString();
            ViewBag.lblCompletePrice = completePrice.Value.ToString("C2");
            return View(viewName);
        }
        #endregion


        #region 商品销量排行
        public ActionResult ProductSales(string StartDate="",string EndDate="", int pageIndex = 1, int selectMode = 0, string viewName = "ProductSales")
        {
            ViewBag.StartDate = StartDate==""?DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd"):StartDate;
            ViewBag.EndDate = EndDate == "" ? DateTime.Now.ToString("yyyy-MM-dd"):EndDate;
            DateTime startDate = Globals.SafeDateTime(StartDate, DateTime.Now.AddYears(-1));
            DateTime endDate = Globals.SafeDateTime(EndDate, DateTime.Now);
            startDate = startDate.Date;
            endDate = endDate.Date.AddDays(1).AddSeconds(-1);
            Model.Shop.Order.StatisticMode mode = (Model.Shop.Order.StatisticMode)selectMode;
            if (mode.ToString() == "Day")
            {
                TimeSpan time = endDate - startDate;
                if (time.Days > 31)
                {
                    startDate = endDate.AddDays(-31);

                }
            }
            DataSet ds = BLL.Shop.Order.OrderManage.ProductSales(mode, startDate, endDate,SupplierId);
            string productInfo = "product";
            string str = CreateXml(ds, mode,productInfo);
            ViewBag.litChart = FusionCharts.RenderChart("/FusionCharts/Line.swf", "", str, "FusionChartsLine", "900", "500", false, true);
            ViewBag.Mode = mode.ToString();
           

            int pageSize = 10;
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            int endIndex = pageIndex * pageSize;
            int totalCount = Maticsoft.BLL.Shop.Order.OrderManage.GetRecordCount(mode, startDate, endDate, mode.ToString(), startIndex, endIndex,SupplierId);
            List <Maticsoft.ViewModel.Order.OrderInfoExPage> list =Maticsoft.BLL.Shop.Order.OrderManage.GetListByPageEx(mode, startDate, endDate,mode.ToString(), startIndex, endIndex,SupplierId);
            if (0 == list.Count)
            {
                return View(viewName);
            }
            PagedList<Maticsoft.ViewModel.Order.OrderInfoExPage> pagedList = new PagedList<ViewModel.Order.OrderInfoExPage>(list, pageIndex, pageSize, totalCount);
            foreach(Maticsoft.ViewModel.Order.OrderInfoExPage item in pagedList)
            {
                DataSet SUKsList = SUKManage.GetList(" ProductId= "+item.Product.ToString());
                string str_SUK = string.Empty;
                foreach (DataRow row in SUKsList.Tables[0].Rows)
                {
                    str_SUK += row["SKU"].ToString()+"/";
                }
                str_SUK = str_SUK.Substring(0, str_SUK.Length - 1);
                item.SKUs = str_SUK;
            }
            
            
            //SUKManage.GetSKUItemsByProductId(SupplierId);
            if (Request.IsAjaxRequest())
            {
                return PartialView(viewName, pagedList);
            }
            return View(viewName, pagedList);

        }
        #endregion

        #region 走势图
        private string CreateXml(DataSet ds, Model.Shop.Order.StatisticMode mode,string str)
        {
            string chartTitle = "用户统计";
            string chartConfig = string.Empty;
            if (DataSetTools.DataSetIsNull(ds)) return "";

            string desc, format;
            switch (mode)
            {
                case Model.Shop.Order.StatisticMode.Day:
                    desc = "天";
                    format = "yyyy-MM-dd";
                    break;
                case Model.Shop.Order.StatisticMode.Month:
                    desc = "月份";
                    format = "yyyy-MM";
                    break;
                case Model.Shop.Order.StatisticMode.Year:
                    desc = "年份";
                    format = "yyyy";
                    break;
                default:
                    throw new ArgumentOutOfRangeException("mode");
            }

            StringBuilder xmlData = new StringBuilder();
            if (ds.Tables[0].Rows.Count > 0)
            {
                xmlData.AppendFormat(
                   "<?xml version='1.0' encoding='utf-8' ?><chart caption='{0}' xAxisName='" + desc + "' " + chartConfig + " showValues='1' showhovercap='0'  formatNumberScale='0' showBorder='0' palette='2' animation='1'  showPercentInToolTip='0' labelDisplay='Rotate' slantLabels='1' > ",
                   chartTitle);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    xmlData.AppendFormat("<set label='{0}' ", ds.Tables[0].Rows[i].Field<DateTime>("GeneratedDate").ToString(format));
                    switch (str)
                    {
                        case "user":
                            xmlData.AppendFormat(" value='{0}' />", String.IsNullOrEmpty((ds.Tables[0].Rows[i]["Users"].ToString())) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["Users"]));
                            break;
                        case "product":
                            xmlData.AppendFormat(" value='{0}' />", String.IsNullOrEmpty((ds.Tables[0].Rows[i]["ToalQuantity"].ToString())) ? 0 : Convert.ToInt32(ds.Tables[0].Rows[i]["ToalQuantity"]));
                            break;
                    }
                    
                }
                xmlData.Append("</chart>");
            }
            return xmlData.ToString();
        }
        #endregion

        #region 编辑商家
        public ActionResult Modify(string viewName = "Modify")
        {
            Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
            Maticsoft.Model.Shop.Supplier.SupplierInfo model = bll.GetModel(SupplierId);
            if (model.RegisteredCapital.HasValue)
            {
                ViewBag.RegisteredCapital = model.RegisteredCapital.ToString();
            }
            if (model.RegionId.HasValue)
            {
                ViewBag.SelectID = model.RegionId.Value;
            }
            if (model.EstablishedCity.HasValue)
            {
                ViewBag.RegionCity = model.EstablishedCity.Value;
            } if (model.EstablishedDate.HasValue)
            {
                ViewBag.EstablishedDate = model.EstablishedDate.Value.ToString("yyyy-MM-dd");
            }
            return View(viewName, model);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Modify(Maticsoft.Model.Shop.Supplier.SupplierInfo model, string viewName = "Show")
        {
            Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
            if (bll.Exists(model.Name, this.SupplierId))
            {
                return Content("该商家名称已经被注册，请更换商家名称再操作！");
            }
            try
            {
                Maticsoft.Model.Shop.Supplier.SupplierInfo oldModel = bll.GetModel(SupplierId);
                if (null != oldModel)
                {
                    oldModel.AgentId = model.AgentId;
                    oldModel.Name = model.Name;
                    oldModel.Introduction = model.Introduction;
                    oldModel.RegisteredCapital = Globals.SafeInt(model.RegisteredCapital, 0);
                    oldModel.TelPhone = model.TelPhone;
                    oldModel.CellPhone = model.CellPhone;
                    oldModel.ContactMail = model.ContactMail;
                    oldModel.RegionId = model.RegionId;
                    oldModel.Address = model.Address;
                    oldModel.Remark = model.Remark;
                    oldModel.Contact = model.Contact;
                    string EstablishedDate = model.EstablishedDate.ToString();
                    if (PageValidate.IsDateTime(EstablishedDate))
                    {
                        oldModel.EstablishedDate = Globals.SafeDateTime(EstablishedDate, DateTime.Now);
                    }
                    else
                    {
                        oldModel.EstablishedDate = null;
                    }
                    oldModel.EstablishedCity = model.EstablishedCity;
                    oldModel.Fax = model.Fax;
                    oldModel.PostCode = model.PostCode;
                    oldModel.HomePage = model.HomePage;
                    oldModel.ArtiPerson = model.ArtiPerson;
                    oldModel.Rank = Globals.SafeInt(model.Rank, 0);
                    oldModel.CategoryId = Globals.SafeInt(model.CategoryId, 0);
                    oldModel.CompanyType = Globals.SafeInt(model.CompanyType, 0);
                    oldModel.BusinessLicense = model.BusinessLicense;
                    oldModel.TaxNumber = model.TaxNumber;
                    oldModel.AccountBank = model.AccountBank;
                    oldModel.AccountInfo = model.AccountInfo;
                    oldModel.ServicePhone = model.ServicePhone;
                    oldModel.QQ = model.QQ;
                    oldModel.MSN = model.MSN;
                    oldModel.Status = Globals.SafeInt(model.Status, 0);

                    oldModel.UpdatedDate = DateTime.Now;
                    oldModel.UpdatedUserId = CurrentUser.UserID;
                    oldModel.Balance = Globals.SafeDecimal(model.Balance, 0);
                    oldModel.AgentId = Globals.SafeInt(model.AgentId, 0);
                    oldModel.Slogan = model.Slogan;
                    oldModel.Discount = model.Discount;
                    JsonObject json = new JsonObject();
                    if (bll.Update(oldModel))
                    {
                        DataCache.DeleteCache("SuppliersModel-" + SupplierId);//清除缓存
                        json.Put("Result", "OK");
                        return Json(json);
                    }
                    else
                    {
                        json.Put("Result", "NO");
                        return Json(json);
                    }
                }
            }
            catch
            {
                return Content("Error");
            }
            return View(viewName);
        }
        #endregion

        #region 商家信息
        public ActionResult Show()
        {
            Maticsoft.BLL.Shop.Supplier.SupplierInfo bll = new Maticsoft.BLL.Shop.Supplier.SupplierInfo();
            Maticsoft.Model.Shop.Supplier.SupplierInfo model = bll.GetModel(SupplierId);
            ViewBag.EnteRank = GetSuppRank(model.Rank);//商家等级
            ViewBag.EnteClassName = GetEnteClassName(model.CategoryId);//商家分类
            if (model.CompanyType.HasValue)
            {
                ViewBag.CompanyType = GetCompanyType(model.CompanyType);
            }
            if (model.RegisteredCapital.HasValue)
            {
                ViewBag.RegisteredCapital = model.RegisteredCapital.ToString();
            }
            if (model.EstablishedDate.HasValue)
            {
                ViewBag.EstablishedDate = model.EstablishedDate.Value.ToString("yyyy-MM-dd");
            }
            if (model.UpdatedDate.HasValue)
            {
                ViewBag.UpdatedDate = model.UpdatedDate.ToString();
            }
            if (model.RegionId.HasValue)
            {
                ViewBag.SelectID = model.RegionId.Value;
            }
            if (model.EstablishedCity.HasValue)
            {
                ViewBag.RegionCity = model.EstablishedCity.Value;
            }
            ViewBag.Status = GetStatus(model.Status);// 0未审核  1正常  2冻结   3删除
            Maticsoft.Accounts.Bus.User userbll = new Maticsoft.Accounts.Bus.User();
            if (model.UpdatedUserId.HasValue)
            {
                ViewBag.UpdatedUserId = userbll.GetUserNameByCache(model.UpdatedUserId.Value);
            }
            return View(model);
        }
        #endregion

        #region 获取商家分类名称
        /// <summary>
        /// 获取商家分类名称
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetEnteClassName(object target)
        {
            //合资、独资、国有、私营、全民所有制、集体所有制、股份制、有限责任制
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "合资";
                        break;
                    case "2":
                        str = "独资";
                        break;
                    case "3":
                        str = "国有";
                        break;
                    case "4":
                        str = "私营";
                        break;
                    case "5":
                        str = "全民所有制";
                        break;
                    case "6":
                        str = "集体所有制";
                        break;
                    case "7":
                        str = "股份制";
                        break;
                    case "8":
                        str = "有限责任制";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取商家性质
        /// <summary>
        /// 获取商家性质
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetCompanyType(object target)
        {
            //0:个体工商; 1:私营独资商家; 2:国营商家。
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "个体工商";
                        break;
                    case "2":
                        str = "私营独资商家";
                        break;
                    case "3":
                        str = "国营商家";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取商家审核状态
        /// <summary>
        /// 获取商家审核状态
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetStatus(object target)
        {
            //0:未审核; 1:正常;2:冻结;3:删除
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "0":
                        str = "未审核";
                        break;
                    case "1":
                        str = "正常";
                        break;
                    case "2":
                        str = "冻结";
                        break;
                    case "3":
                        str = "删除";
                        break;
                    default:
                        break;
                }
            }
            return str;
        }
        #endregion

        #region 获取商家等级
        /// <summary>
        /// 获取商家等级
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public string GetSuppRank(object target)
        {
            string str = string.Empty;
            if (!StringPlus.IsNullOrEmpty(target))
            {
                switch (target.ToString())
                {
                    case "1":
                        str = "一星级";
                        break;
                    case "2":
                        str = "二星级";
                        break;
                    case "3":
                        str = "三星级";
                        break;
                    case "4":
                        str = "四星级";
                        break;
                    case "5":
                        str = "五星级";
                        break;
                    default:
                        str = "无";
                        break;
                }
            }
            return str;
        }
        #endregion


        #region 首页
        public ActionResult Main(string viewName = "Main")
        {
            ViewBag.userName = string.IsNullOrWhiteSpace(CurrentUser.TrueName) ? CurrentUser.UserName : CurrentUser.TrueName;
            uModel = uBll.GetUsersExpModel(CurrentUser.UserID);
            if (uModel != null)
            {
                ViewBag.lastLoginDate = uModel.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                ViewBag.lastLoginDate = CurrentUser.User_dateCreate.ToString("yyyy-MM-dd HH:mm:ss");
            }
            System.Data.DataSet ds = bll.GetStatisticsSupply(SupplierId);
            //剩余
            int? remainQuantity = ds.Tables[0].Rows[0].Field<int?>("ToalQuantity");
            decimal? remainPrice = ds.Tables[0].Rows[0].Field<decimal?>("ToalPrice");
            if (!remainQuantity.HasValue) remainQuantity = 0;
            if (!remainPrice.HasValue) remainPrice = 0;
            ViewBag.remainQuantity = remainQuantity.Value.ToString();
            ViewBag.remainPrice = remainPrice.Value.ToString("C2");

            //已售
            int? soldQuantity = ds.Tables[0].Rows[1].Field<int?>("ToalQuantity");
            decimal? soldPrice = ds.Tables[0].Rows[1].Field<decimal?>("ToalPrice");
            if (!soldQuantity.HasValue) soldQuantity = 0;
            if (!soldPrice.HasValue) soldPrice = 0;
            ViewBag.soldQuantity = soldQuantity.Value.ToString();
            ViewBag.SoldPrice = soldPrice.Value.ToString("C2");

            ViewBag.ToalQuantity = (remainQuantity + soldQuantity).Value.ToString();
            ViewBag.ToalPrice = (remainPrice + soldPrice).Value.ToString("C2");
            BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.System);
            ViewBag.webSiteName = WebSiteSet.WebName;
            return View(viewName);
        }
        #endregion

        #region 头部
        public PartialViewResult Header(string viewName = "_Header")
        {
            return PartialView(viewName);
        }
        #endregion

        #region 导航
        public PartialViewResult Top(string viewName = "_Top")
        {
            ViewBag.UserName = string.IsNullOrWhiteSpace(CurrentUser.TrueName) ? CurrentUser.UserName : CurrentUser.TrueName;
            //0:admin后台 1:企业后台  2:代理商后台 3:用户后台 4商家后台
            List<Maticsoft.Model.SysManage.SysNode> AllNodeList = sm.GetTreeListByTypeCache(4, true);
            List<Maticsoft.Model.SysManage.SysNode> FirstList = AllNodeList.Where(c => c.ParentID == 0).ToList();
            List<Maticsoft.Model.SysManage.SysNode> NodeList = new List<SysNode>();
            foreach (var item in FirstList)
            {
                //判断权限
                if ((item.PermissionID == -1) || (UserPrincipal.HasPermissionID(item.PermissionID)))
                {
                    NodeList.Add(item);
                }
            }
            return PartialView(viewName, NodeList);
        }
        #endregion

        #region 左侧导航

        public PartialViewResult LeftMenu(int id, string viewName = "_LeftMenu")
        {
            List<Maticsoft.Model.SysManage.SysNode> AllNodeList = sm.GetTreeListByTypeCache(4, true);
            Maticsoft.Model.SysManage.SysNode nodeModel = AllNodeList.FirstOrDefault(c => c.NodeID == id);
            ViewBag.NodeName = nodeModel == null ? "" : nodeModel.TreeText;
            List<Maticsoft.Model.SysManage.SysNode> NodeList = AllNodeList.Where(c => c.ParentID == id).ToList();
            return PartialView(viewName, NodeList);
        }

        #endregion

        #region
        public PartialViewResult Swich(string viewName = "_Swich")
        {
            return PartialView(viewName);
        }
        #endregion

        #region 店铺管理设置
        #region 展示店铺信息
        public ActionResult ShopInfo(string viewName = "ShopInfoModify")
        {
            Model.Shop.Supplier.SupplierInfo supplierInfo = bll.GetModelByCache(SupplierId);
            string mobileCount = SupplierConfig.GetValueByCache(SupplierId, "MoblieIndexProdCount");
           // ViewBag.mobileStroeLogo = SupplierConfig.GetValueByCache(SupplierId, "MoblieStoreLogo");//MoblieStoreLogo
            //ViewBag.shopSearchStoreLogo = SupplierConfig.GetValueByCache(SupplierId, "ShopSearchStoreLogo");
            ViewBag.HasShopArea = MvcApplication.HasArea(AreaRoute.Shop);//是否包含pc版
            ViewBag.HasMShopArea = MvcApplication.HasArea(AreaRoute.MShop);//是否包含手机版
            ViewBag.mobileCount = mobileCount;
            return View(viewName, supplierInfo);
        }
        #endregion

        #region 关闭店铺
        public string CloseShop()
        {
            if (bll.CloseShop(SupplierId))
            {
                DataCache.DeleteCache("SuppliersModel-" + SupplierId);//清除缓存
                return "ok";
            }
            else
            {
                return "error";
            }
        }
        #endregion

        #region 编辑店铺信息
        [ValidateInput(false)]
        public string EditShop()
        {
            string shopName = Request.Form["shopName"];
            string txtDes = Request.Form["textDes"];
            string txtproductCount = Request.Form["txtProductCount"];
            string txtMobileCount = Request.Form["txtMobileCount"];
            string logoUrl = Request.Form["logoUrl"];
            string logoUrlSearch = Request.Form["logoUrlSearch"];
            string logoUrlSquare = Request.Form["logoUrlm"];
            string logoUrlSale = Request.Form["logoUrlSale"];
            string qq=Request.Form["qq"];
            string servicePhone = Request.Form["servicePhone"];
            bool bgType = Convert.ToBoolean(Request.Form["BGType"]);
            string background = Request.Form["Background"];
            string bgImg = Request.Form["BGImg"];
                    
            if (bll.ExistsShopName(shopName, SupplierId))
            {
                //MessageBox.ShowFailTip(this, "该店铺名称已经被注册，请更换店铺名称再操作！");
                return "exit";
            }
            Model.Shop.Supplier.SupplierInfo model = bll.GetModel(SupplierId);
            int IndexProdTop = Globals.SafeInt(txtproductCount, 0);
           
            if (null != model)
            {
                #region 移动文件
                // 移动文件
                string logoImagepath = "/Upload/Supplier/Logo/";
                string logoDirPath = Server.MapPath(logoImagepath);
                if (!Directory.Exists(logoDirPath))
                {
                    //不存在则自动创建文件夹
                    Directory.CreateDirectory(logoDirPath);
                }
                MoveFileToFTP(logoUrlSearch, logoImagepath, string.Format("{0}_T180X60.png", SupplierId));
                MoveFileToFTP(logoUrl, logoImagepath, string.Format("{0}_T980X68.png", SupplierId));
                MoveFileToFTP(logoUrlSquare, logoImagepath, string.Format("{0}_T200X200.png", SupplierId));
                MoveFileToFTP(logoUrlSale, logoImagepath, string.Format("{0}_T200X400.png", SupplierId));
                MoveFileToFTP(bgImg, logoImagepath, string.Format("{0}_T200X600.png", SupplierId));
                #endregion
                model.ShopName = shopName;
                model.IndexContent = txtDes;
                model.IndexProdTop = IndexProdTop;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedUserId = CurrentUser.UserID;
                model.StoreStatus = 0;
                model.QQ = qq;
                model.ServicePhone = servicePhone;
                model.BGType = bgType;
                model.Background = background;
                if (bll.Update(model))
                { 
                    //将手机端显示数量存入config表中
                    #region 将手机端显示数量存入config表中
                    supplierConfig.Modify(SupplierId, "MoblieIndexProdCount", txtMobileCount, 1,"手机端首页显示数量");
                   
                    DataCache.DeleteCache("SuppliersModel-" + SupplierId);//清除缓存
                    return "ok"; 
                    #endregion
                   // return "error"; //MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "ShopModify.aspx");
                }
                else
                {
                    return "error"; //MessageBox.ShowFailTip(this, Resources.Site.TooltipUpdateError);
                }

            }
            return "unKnow";
        }

        private void MoveFile(string oldFilePath, string newFilePath, string newFileName)
        {
            if (String.IsNullOrWhiteSpace(oldFilePath)) return;

            string oldFileMP = Server.MapPath(oldFilePath);
            string newFileMP = Server.MapPath(newFilePath + newFileName);

            if (System.IO.File.Exists(oldFileMP))
            {
                if (System.IO.File.Exists(newFileMP))
                {
                    System.IO.File.Delete(newFileMP);
                }
                System.IO.File.Move(oldFileMP, newFileMP);
            }
        }

        private void MoveFileToFTP(string filepath, string newFilePath, string fileName)
        {
            if (string.IsNullOrWhiteSpace(filepath))
            {
                return;
            }

            MoveFile(filepath, newFilePath, fileName);

            if (System.IO.File.Exists(Server.MapPath(newFilePath + "/" + fileName)))
            {

                FileManager.MoveImageForFTP(newFilePath + "/" + fileName, newFilePath);
            }

        }
        #endregion 
        #endregion

        #region 供应商菜单管理
        #region 菜单首页
        public ViewResult SupplierMenu()
        {
            return View();
        }
        #endregion

        #region 获取菜单列表
        public PartialViewResult LoadMenu(int pageIndex = 1, string viewName = "_MenuList")
        {
            Maticsoft.BLL.Shop.Supplier.SupplierMenus supplierMenusBll = new SupplierMenus();
            int pageSize = 10;
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            int endIndex = pageIndex * pageSize;
            string strwhere = string.Format("SupplierId={0}", SupplierId);
            int totalCount = supplierMenusBll.GetRecordCount(strwhere);
            if (totalCount < 1)
            {
                return PartialView(viewName);
            }
            List<Maticsoft.Model.Shop.Supplier.SupplierMenus> supMenuList = supplierMenusBll.GetListByPageEx(strwhere, "Sequence", startIndex, endIndex);
            PagedList<Maticsoft.Model.Shop.Supplier.SupplierMenus> pagedList = new PagedList<Model.Shop.Supplier.SupplierMenus>(supMenuList, pageIndex, pageSize, totalCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView(viewName, pagedList);
            }
            return PartialView(viewName, pagedList);
        }
        #endregion

        #region 添加菜单Get
        public ViewResult AddMenu()
        {
            return View();
        }
        #endregion

        #region 加载分类列表
        public JsonResult LoadProductCategory(string navType)
        {
            BLL.Shop.Supplier.SupplierCategories suppCate = new BLL.Shop.Supplier.SupplierCategories();
            List<Maticsoft.Model.Shop.Supplier.SupplierCategories> list =
                        suppCate.GetModelList(string.Format(" ParentCategoryId=0  and SupplierId={0} ", SupplierId));
            List<string> categoryList = new List<string>();
            foreach (var item in list)
            {
                categoryList.Add(item.CategoryId + ":" + item.Name);
            }
            return Json(categoryList);

        }
        #endregion

        #region 添加和编辑提交
        public ContentResult SubmitMenu()
        {
            string menuName = Request.Form["menuName"];
            string navUrl = Request.Form["navUrl"];
            string sequence = Request.Form["sequence"];
            string openType = Request.Form["openType"];
            string ck = Request.Form["checked"];
            string type = Request.Form["submitType"];
            string urlType = Request.Form["urlType"];
            Maticsoft.Model.Shop.Supplier.SupplierMenus model = new Maticsoft.Model.Shop.Supplier.SupplierMenus();
            Maticsoft.BLL.Shop.Supplier.SupplierMenus bllMenu = new SupplierMenus();
            model.IsUsed = ck == "checked";
            model.MenuName = menuName;
            model.NavURL = navUrl;
            model.Sequence = Maticsoft.Common.Globals.SafeInt(sequence, 1);
            model.Target = Maticsoft.Common.Globals.SafeInt(openType, 1);
            model.MenuType = 1;
            model.URLType = Common.Globals.SafeInt(urlType, 0);
            model.SupplierId = SupplierId;
            if (type == "Add")
            {
                return Content(bllMenu.Add(model) > 0 ? "ok" : "error");
            }
            if (type == "Edit")
            {
                string modelId = Request.Form["menuid"];
                int id = Maticsoft.Common.Globals.SafeInt(modelId, 0);
                model.MenuId = id;
                return Content(bllMenu.Update(model) ? "ok" : "error");
            }
            return Content("no");

        }
        #endregion

        #region 菜单删除
        public ActionResult DeleteMenus()
        {
            string idlist = Request.Form["idList"];
            bool delete = bllSupMenu.DeleteList(idlist.TrimEnd(','));
            if (delete)
            {
                return Content("ok");
            }
            return Content("no");
        }

        public ActionResult DeleteAMenu()
        {
            string menuId = Request.Form["menuId"];
            int id = Maticsoft.Common.Globals.SafeInt(menuId, 0);
            bool delete = bllSupMenu.Delete(id);
            if (delete)
            {
                return Content("ok");
            }
            return Content("no");
        }
        #endregion

        #region 菜单编辑
        public ViewResult EditMenu(int id, string viewName = "EditMenu")
        {
            Maticsoft.Model.Shop.Supplier.SupplierMenus supplierMenus = bllSupMenu.GetModel(id);
            return View(viewName, supplierMenus);
        }
        #endregion

        #region 菜单搜索
        public PartialViewResult SearchMenu(int pageIndex = 1, string viewName = "_MenuList", string keyword = "")
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat("SupplierId={1} And  MenuName like '%{0}%'", keyword,SupplierId);
            int pageSize = 10;
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            int endIndex = pageIndex * pageSize;
            int totalCount = bllSupMenu.GetRecordCount(strWhere.ToString());
            ViewBag.keyword = keyword;
            List<Maticsoft.Model.Shop.Supplier.SupplierMenus> supMenuList = bllSupMenu.GetListByPageEx(strWhere.ToString(), "Sequence", startIndex, endIndex);
            if (null == supMenuList)
            {
                return PartialView(viewName);
            }
            PagedList<Maticsoft.Model.Shop.Supplier.SupplierMenus> pagedList = new PagedList<Model.Shop.Supplier.SupplierMenus>(supMenuList, pageIndex, pageSize, totalCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView(viewName, pagedList);
            }
            return PartialView(viewName, pagedList);

        }
        #endregion
        #endregion

        #region 供应商广告管理

        #region 广告列表
        public ActionResult AdsSetting(int pageIndex = 1, int pageSize = 10, string viewName = "AdsSetting", string keyword = "", string ajaxViewName = "_AdsList")
        {
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            int endIndex = pageIndex * pageSize;
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" SupplierId = {0} ", SupplierId);
            if (keyword.Trim() != "")
            {
                strWhere.AppendFormat(" and  Name like '%{0}%'", keyword.Trim());
            }
            int totalCount = supplierAdBll.GetRecordCount(strWhere.ToString());
            List<Maticsoft.Model.Shop.Supplier.SupplierAD> supplierAdsList =
                supplierAdBll.GetListByPageEx(strWhere.ToString(), "PositionId", startIndex, endIndex);
            if (null == supplierAdsList)
            {
                return PartialView(viewName);
            }
            PagedList<Maticsoft.Model.Shop.Supplier.SupplierAD> supplierAdsPaged =
                new PagedList<Model.Shop.Supplier.SupplierAD>(supplierAdsList, pageIndex, pageSize, totalCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView(ajaxViewName, supplierAdsPaged);
            }
            return View(viewName, supplierAdsPaged);
        }
        #endregion

        #region 添加广告
        public ViewResult AddAds()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAds(Maticsoft.Model.Shop.Supplier.SupplierAD supplierAdModel)
        {
            Maticsoft.Model.Shop.Supplier.SupplierAD modelTemp = new Model.Shop.Supplier.SupplierAD();
            modelTemp.PositionId = supplierAdModel.PositionId;
            modelTemp.Name = supplierAdModel.Name;
            modelTemp.NavigateUrl = supplierAdModel.NavigateUrl;
            modelTemp.Status = supplierAdModel.Status;
            modelTemp.Sequence = 1; //supplierAdModel.Sequence;
            modelTemp.CreatedDate = DateTime.Now;
            modelTemp.CreatedUserID = CurrentUser.UserID;
            modelTemp.SupplierId = SupplierId;
            #region 移动文件
            string oldimageurl = String.Format(supplierAdModel.FileUrl, "");
            //创建文件夹  移动文件
            string path = string.Format("/Upload/Supplier/{0}/AD/", SupplierId);
            string mapPath = Request.MapPath(path);
            string filename = oldimageurl.Substring(oldimageurl.LastIndexOf("/") + 1);
            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }
            if (System.IO.File.Exists(Server.MapPath(oldimageurl)))
            {
                //System.IO.File.Move(Request.MapPath(oldimageurl), mapPath + filename);
                FileManager.MoveImageForFTP(oldimageurl, path);
            }
            #endregion
            modelTemp.FileUrl = path + filename;
            if (supplierAdBll.Add(modelTemp) > 0)
            {
                return Content("ok");
            }
            return Content("no");
        }

        #endregion

        #region 删除广告
        public ActionResult DeleteAds(string idlist)
        {
            string deleteIdList = idlist.TrimEnd(',');
            bool deleteOk = supplierAdBll.DeleteList(deleteIdList);
            if (deleteOk)
            {
                return Content("ok");
            }
            return Content("no");
        }

        public ActionResult DeleteaAd(int AdId)
        {
            if (supplierAdBll.Delete(AdId))
            {
                return Content("ok");
            }
            return Content("no");
        }
        #endregion

        #region 广告详情
        public ViewResult AdsDetail(int id, string viewName = "AdsDetail")
        {
            Maticsoft.Model.Shop.Supplier.SupplierAD adModel = supplierAdBll.GetModelByCache(id);
            string formViewName = Request.QueryString["viewName"];
            if (!string.IsNullOrWhiteSpace(formViewName))
            {
                return View(formViewName, adModel);
            }
            return View(viewName, adModel);
        }
        #endregion

        #region 广告编辑提交
        public ActionResult EditSubmit(Maticsoft.Model.Shop.Supplier.SupplierAD supplierAd)
        {
            Maticsoft.Model.Shop.Supplier.SupplierAD modelTemp = new Model.Shop.Supplier.SupplierAD();
            modelTemp.PositionId = supplierAd.PositionId;
            modelTemp.Name = supplierAd.Name;
            modelTemp.NavigateUrl = supplierAd.NavigateUrl;
            modelTemp.Status = supplierAd.Status;
            modelTemp.Sequence = supplierAd.Sequence;
            modelTemp.CreatedDate = DateTime.Now;
            modelTemp.CreatedUserID = CurrentUser.UserID;
            modelTemp.SupplierId = supplierAd.SupplierId;
            modelTemp.AdvertisementId = supplierAd.AdvertisementId;
            #region 移动文件
            string oldimageurl = String.Format(supplierAd.FileUrl, "");
            //创建文件夹  移动文件
            string path = string.Format("/Upload/Supplier/{0}/AD/", SupplierId);
            string mapPath = Request.MapPath(path);
            string filename = oldimageurl.Substring(oldimageurl.LastIndexOf("/") + 1);
            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }
            if (System.IO.File.Exists(Server.MapPath(oldimageurl)))
            {
                //System.IO.File.Move(Request.MapPath(oldimageurl), mapPath + filename);
                FileManager.MoveImageForFTP(oldimageurl, path);
            }
            #endregion
            modelTemp.FileUrl = path + filename;
            if (supplierAdBll.Update(modelTemp))
            {
                return Content("ok");
            }
            return Content("no");
        }
        #endregion
        #endregion

        #region 模板管理

        #region 模板列表
        public ActionResult ThemeSetting(string viewName = "ThemeSetting")
        {
            string areaName = "MobileSP"; //Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("MainArea");
            List<Maticsoft.Model.Ms.Theme> themeList = Maticsoft.Web.Components.FileHelper.GetThemes(areaName);
            //Maticsoft.BLL.Shop.Supplier.SupplierConfig supplierConfig = new SupplierConfig();
            string name = SupplierConfig.GetValueByCache(SupplierId, "ThemeCurrent");
            //获取当前主模板
            //string name = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ThemeCurrent");
            foreach (var item in themeList)
            {
                if (item.Name == name)
                {
                    item.IsCurrent = true;
                }
            }
            return View(viewName, themeList);
        }
        #endregion

        #region 模板修改
        public ActionResult SetCurrentTheme(string name)
        {
            Maticsoft.BLL.Shop.Supplier.SupplierConfig supplierConfig=new SupplierConfig();
            Maticsoft.Model.Shop.Supplier.SupplierConfig supplierConfigModel;
            supplierConfigModel=new Model.Shop.Supplier.SupplierConfig();
                supplierConfigModel.KeyName = "ThemeCurrent";
                supplierConfigModel.KeyType = 1;
                supplierConfigModel.SupplierId = SupplierId;
                supplierConfigModel.Value = name;
                supplierConfigModel.Description = "当前模板名称";
            bool isSuccess = supplierConfig.Modify(supplierConfigModel);
            if (isSuccess)
            {
                DataCache.SetCache("ThemeCurrent", name);
                System.Web.HttpRuntime.UnloadAppDomain();   //重启网站
                return Content("ok");
            }
            return Content("no");
        }
        #endregion
        #endregion

        #region 邮件模版配置

        #endregion


    }
}
