using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.BLL.Members;
using Webdiyer.WebControls.Mvc;

namespace Maticsoft.Web.Areas.COM.Controllers
{
    public class UserCenterController : COMControllerBaseUser
    {
        //
        // GET: /COM/UserCenter/
        #region 成员变量

        private Maticsoft.Accounts.Bus.User userBusManage = new Maticsoft.Accounts.Bus.User();
        private BLL.Members.Users userManage = new BLL.Members.Users();
        private BLL.Members.UsersExp userEXBll = new BLL.Members.UsersExp();
        private BLL.Members.PointsDetail detailBll = new BLL.Members.PointsDetail();
        #endregion
     

        #region 用户签到
        public ActionResult SignPoint(int pageIndex = 1)
        {
            ViewBag.CanSign = true;
            string isEnable = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PointEnable");
            if (isEnable != "true")
            {
                ViewBag.CanSign = false;
            }
            PointsRule ruleBll = new PointsRule();
            Model.Members.PointsRule ruleModel = ruleBll.GetModel(10, CurrentUser.UserID);
            if (ruleModel == null)
            {
                ViewBag.CanSign = false;
            }
            if (detailBll.isLimit(ruleModel, CurrentUser.UserID))
            {
                ViewBag.CanSign = false;
            }

            //首页用户数据
            Maticsoft.Model.Members.UsersExpModel userEXModel = userEXBll.GetUsersModel(CurrentUser.UserID);
            if (userEXModel != null)
            {
                ViewBag.Points = userEXModel.Points.HasValue ? userEXModel.Points : 0;
                ViewBag.NickName = userEXModel.NickName;
            }
            int _pageSize = 8;
            //计算分页起始索引
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * _pageSize + 1 : 0;
            //计算分页结束索引
            int endIndex = pageIndex * _pageSize;
            int toalCount = 0;
            //获取总条数
            toalCount = detailBll.GetSignCount(CurrentUser.UserID);
            if (toalCount < 1)
            {
                return View();//NO DATA
            }
            List<Maticsoft.Model.Members.PointsDetail> detailList = detailBll.GetSignListByPage(CurrentUser.UserID, "", startIndex, endIndex);
            if (detailList != null && detailList.Count > 0)
            {
                foreach (var item in detailList)
                {
                    item.RuleName = GetRuleName(item.RuleId);
                }
            }
            PagedList<Maticsoft.Model.Members.PointsDetail> lists = new PagedList<Maticsoft.Model.Members.PointsDetail>(detailList, pageIndex, _pageSize, toalCount);
            return View(lists);
        }

        public ActionResult AjaxSign()
        {
            string isEnable = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PointEnable");
            if (isEnable != "true")
            {
                return Content("Enable");
            }
            PointsRule ruleBll = new PointsRule();
            Model.Members.PointsRule ruleModel = ruleBll.GetModel(10, CurrentUser.UserID);
            if (ruleModel == null)
            {
                return Content("NoRule");
            }
            if (detailBll.isLimit(ruleModel, CurrentUser.UserID))
            {
                return Content("Limit");
            }
            int points = detailBll.AddPoints(10, CurrentUser.UserID, "签到加积分");
            return Content(points.ToString());
        }

        public string GetRuleName(int RuleId)
        {
            Maticsoft.BLL.Members.PointsRule ruleBll = new BLL.Members.PointsRule();
            return ruleBll.GetRuleName(RuleId);
        }
        #endregion 
    }
}
