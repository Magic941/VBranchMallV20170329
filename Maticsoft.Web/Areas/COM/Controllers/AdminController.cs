using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.COM.Controllers
{
    public class AdminController : COMControllerBaseAdmin
    {

       private    Maticsoft.BLL.Members.Users userBll = new BLL.Members.Users();
       private Maticsoft.BLL.Members.UserCard cardBll = new BLL.Members.UserCard();
       private Maticsoft.BLL.Shop.Coupon.CouponInfo infoBll = new BLL.Shop.Coupon.CouponInfo();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 优惠券兑换
        /// </summary>
        /// <returns></returns>
        public ActionResult CouponEx()
        {
            int Act_PageLoad = 695;
            ViewBag.HasPerm = true;
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_PageLoad)))
            {
                ViewBag.HasPerm = false;
            }
            return View();
        }


        public PartialViewResult UserInfo(int UserId = 0, string UserCard="",string Coupon="", string ViewName = "_UserInfo")
        {
            if (UserId == 0 && !String.IsNullOrWhiteSpace(UserCard))
            {
                Maticsoft.Model.Members.UserCard cardModel = cardBll.GetModel(Common.InjectionFilter.SqlFilter(UserCard));
                UserId = cardModel == null ? 0 : cardModel.UserId;
            }
            if (UserId == 0 && !String.IsNullOrWhiteSpace(Coupon))
            {
                Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = infoBll.GetModel(Common.InjectionFilter.SqlFilter(Coupon));
                UserId = infoModel == null ? 0 : infoModel.UserId;
            }
            Maticsoft.Model.Members.Users userModel = userBll.GetModel(UserId);
             return PartialView(ViewName, userModel);
        }

        public PartialViewResult UserCard(string UserCard, string ViewName = "_UserInfo")
        {
            Maticsoft.Model.Members.UserCard cardModel = cardBll.GetModel(UserCard);
            Maticsoft.Model.Members.Users userModel = null;
            userModel = cardModel == null ? null : userBll.GetModel(cardModel.UserId);
            return PartialView(ViewName, userModel);
        }

        public PartialViewResult CouponList(int UserId = 0, string UserCard = "", string ViewName = "_CouponList")
        {
            if(UserId==0)
            {
            Maticsoft.Model.Members.UserCard cardModel = cardBll.GetModel(UserCard);
            UserId = cardModel == null ? 0 :  cardModel.UserId;
           }
            if (UserId == 0)
            {
                return PartialView(ViewName, null);
            }
            List<Maticsoft.Model.Shop.Coupon.CouponInfo> infoList = infoBll.GetCouponList(UserId, 1, true);
            return PartialView(ViewName, infoList);
        }

        public PartialViewResult GetCoupon(string Coupon = "", string ViewName = "_CouponDetail")
        {
            Maticsoft.Model.Shop.Coupon.CouponInfo infoModel = infoBll.GetModel(Common.InjectionFilter.SqlFilter(Coupon));
            return PartialView(ViewName, infoModel);
        }

        public ActionResult UseCoupon(FormCollection Fm)
        {
            string Coupon = Fm["Coupon"];
            if (String.IsNullOrWhiteSpace(Coupon))
            {
                return Content("0");
            }
            //是否过期
            if (!infoBll.IsEffect(Coupon))
            {
                return Content("2");
            }
            if (infoBll.UseCoupon(Coupon))
            {
                return Content("1");
            }
            return Content("3");
           
        }
     
        

    }
}
