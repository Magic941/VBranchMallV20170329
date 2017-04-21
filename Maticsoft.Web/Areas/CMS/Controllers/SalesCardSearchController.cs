using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Json;
using Maticsoft.Common;
using Maticsoft.Components.Filters;
using Maticsoft.Web.Components.Setting.CMS;
using Webdiyer.WebControls.Mvc;
using Maticsoft.BLL.Shop.Card;
using Maticsoft.BLL;
using System.Text;
using Maticsoft.Model;
using Newtonsoft.Json;
using Maticsoft.Web.Models;
using Maticsoft.Services;


namespace Maticsoft.Web.Areas.CMS.Controllers
{
    /// <summary>
    /// 卡激活
    /// </summary>
    public class SalesCardsSearchController : CMSControllerBase
    {

        /// <summary>
        /// 激活起始页页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        { 
                        
            return View();
        }

        /// <summary>
        /// 营销员登录,并保留本地的Session,并切换到报表页面
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public ActionResult Login(string userName,string pwd)
        {
            SalesActivatedCardSearchLogic sc = new SalesActivatedCardSearchLogic();
            var salesperson  = sc.GetSalesPerson(userName, pwd);
            if (salesperson != null)
            {
                Session["SalesPerson"] = salesperson;
                return Json(new { IsSuccess = true, Message = "登录成功!" });
            }
            else
            {
                return Json(new { IsSuccess = false, Message = "账号或密码不正确!" });
            }       
        }

        public ActionResult LogOut()
        {
            Session["SalesPerson"] = null;

            return Redirect("/SalesCardsSearch/Index");
        }


        public ActionResult ActivatedCardReport()
        {
            SalesActivatedCardSearchLogic sc = new SalesActivatedCardSearchLogic();

            var salesperson = Session["SalesPerson"];

            if (salesperson != null)
            {
                ViewBag.LoginStatus = true;
                var salesperson2 = (SalesPersonModel)salesperson;
                var r = sc.GetSalesPersonCardsReport(salesperson2.Mobile, salesperson2.SalesPersonPwd);
                return View(r);
            }
            else
            {
                ViewBag.LoginStatus = false;
            }
            return View();
        }

        public ActionResult ActivatedCardSearch(string searchCardId,string searchCardNo,string searchInsureNo,int? searchInsureType,int? currentPageIndex)
        {
            if (searchInsureType == null)
            {
                searchInsureType = 0;
            }
            if (currentPageIndex == null)
            {
                currentPageIndex = 1;
            }

            ViewBag.searchCardId = searchCardId;
            ViewBag.searchCardNo = searchCardNo;
            ViewBag.searchInsureNo = searchInsureNo;
            ViewBag.searchInsureType = searchInsureType;
            ViewBag.currentPageIndex = currentPageIndex;

            SalesActivatedCardSearchLogic sc = new SalesActivatedCardSearchLogic();

            var salesperson = Session["SalesPerson"];

            if (salesperson != null)
            {
                ViewBag.LoginStatus = true;
                var salesperson2 = (SalesPersonModel)salesperson;
                int TotalRows;
                var r = sc.GetSalesActivatedCardInfo(salesperson2.Mobile, salesperson2.SalesPersonPwd,searchCardNo, searchCardId,searchInsureNo,(int)searchInsureType, (int)currentPageIndex,out TotalRows);
                PagedList<SalesPersonCardsSearchModel> lists = new PagedList<SalesPersonCardsSearchModel>(r, (int)currentPageIndex, 10, TotalRows);

                return View(lists);
                
            }
            else
            {
                ViewBag.LoginStatus = false;
            }
            
            return View();
        }
    }
}