using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Webdiyer.WebControls.Mvc;
using System.Text;
using Maticsoft.Json;


namespace Maticsoft.Web.Areas.Supplier.Controllers
{
    public class MemberController : SupplierControllerBase
    {
        Maticsoft.BLL.Members.PointsRule RuleBll = new BLL.Members.PointsRule();
        Maticsoft.BLL.Members.PointsLimit limitBll = new BLL.Members.PointsLimit();
        Maticsoft.BLL.Members.PointsAction actionBll = new BLL.Members.PointsAction();
        Maticsoft.BLL.Members.UserRank rankBll = new BLL.Members.UserRank();
        public ActionResult PointsRule()
        {
            return View();
        }
        public PartialViewResult LoadRule(int pageIndex = 1, string viewName = "_PointRuleList", string txtKeyWord = "")
        {
            int pageSize = 10;
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            int endIndex = pageIndex * pageSize;
            StringBuilder strWhere = new StringBuilder();
            if (!String.IsNullOrEmpty(txtKeyWord))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("Name like '%{0}%'", txtKeyWord);
            }
            int totalCount = RuleBll.GetRecordCount(strWhere.ToString());
            List<Maticsoft.Model.Members.PointsRule> list = RuleBll.GetListByPageExt(strWhere.ToString(), "", startIndex, endIndex);
            List<Maticsoft.ViewModel.Member.PointsRuleExt> ruleList = new List<ViewModel.Member.PointsRuleExt>();
            Maticsoft.ViewModel.Member.PointsRuleExt ruleModel = null;
            foreach (var item in list)
            {
                ruleModel = new ViewModel.Member.PointsRuleExt();
                ruleModel.RuleId = item.RuleId;
                ruleModel.ActionId = item.ActionId;
                ruleModel.ActionName = GetActionName(Maticsoft.Common.Globals.SafeInt(item.ActionId, 0));
                ruleModel.LimitID = item.LimitID;
                ruleModel.LimitName = GetLimitName(item.LimitID);
                ruleModel.Name = item.Name;
                ruleModel.Score = item.Score;
                ruleModel.Description = item.Description;
                ruleModel.TargetId = item.TargetId;
                ruleModel.TargetType = item.TargetType;
                ruleList.Add(ruleModel);
            }
            if (null == ruleList)
            {
                return PartialView(viewName);
            }
            PagedList<Maticsoft.ViewModel.Member.PointsRuleExt> pagedList = new PagedList<ViewModel.Member.PointsRuleExt>(ruleList, pageIndex, pageSize, totalCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView(viewName, pagedList);
            }
            return PartialView(viewName, pagedList);
        }
        #region 条件限制
        public string GetLimitName(int limitId)
        {
            if (limitId == -1)
            {
                return "无限制";
            }
            else
            {
                Maticsoft.Model.Members.PointsLimit limitModel = limitBll.GetModel(limitId);
                if (limitModel != null)
                {
                    return limitModel.Name;
                }
                else
                {
                    return "无限制";
                }
            }
        }
        #endregion

        #region 规则码
        public string GetActionName(int ActionId)
        {
            Maticsoft.BLL.Members.PointsAction actionBLL = new BLL.Members.PointsAction();
            Maticsoft.Model.Members.PointsAction actionModel = actionBLL.GetModel(ActionId);
            if (actionModel != null)
            {
                return actionModel.Name;
            }
            else
            {
                return "";
            }
        }
        #endregion

        #region 添加积分规则
        public ActionResult AddRule(string viewName = "AddRule")
        {
            return View(viewName);
        }
        [HttpPost]
        public ActionResult AddRule(string viewName = "AddRule", int ActionName = -1, string tName = "", int Score = -1, int LimitName = 1, string Remark = "")
        {
            JsonObject json = new JsonObject();
            if (RuleBll.ExistsActionId(ActionName))
            {
                json.Put("Result", "Action");
                return Json(json);
            }
            else
            {
                Maticsoft.Model.Members.PointsRule RuleModel = new Model.Members.PointsRule();
                RuleModel.ActionId = ActionName;
                RuleModel.LimitID = LimitName;
                RuleModel.Name = tName;
                RuleModel.Score = Score;
                RuleModel.Description = Remark;
                RuleModel.TargetId = SupplierId;
                RuleModel.TargetType = 0;
                if (RuleBll.Add(RuleModel) > 0)
                {
                    json.Put("Result", "OK");
                    return Json(json);
                }
                else
                {
                    json.Put("Result", "ERROR");
                    return Json(json);
                }
            }
        }
        #endregion

        #region 编辑积分规则
        public ActionResult UpdateRule(string viewName = "UpdateRule", int id = 0)
        {
            Maticsoft.Model.Members.PointsRule RuleModel = RuleBll.GetModel(id);
            if (RuleModel == null)
            {
                return View("PointsRule");
            }
            return View(viewName, RuleModel);
        }

        [HttpPost]
        public ActionResult UpdateRule(string viewName = "UpdateRule", int ruleID = 0, int ActionName = -1, string tName = "", int Score = -1, int LimitName = -1, string Remark = "")
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.PointsRule RuleModel = RuleBll.GetModel(ruleID);
            Maticsoft.Model.Members.PointsRule model = new Model.Members.PointsRule();
            model.RuleId = ruleID;
            model.ActionId = ActionName;
            model.LimitID = LimitName;
            model.Name = tName;
            model.Score = Score;
            model.Description = Remark;
            model.TargetId = SupplierId;
            model.TargetType = 0;
            if (RuleBll.Update(model))
            {
                json.Put("Result", "OK");
                return Json(json);
            }
            else
            {
                json.Put("Result", "ERROR");
                return Json(json);
            }
        }
        #endregion

        #region 删除积分规则
        public ActionResult DeleteMember(int ruleID)
        {
            JsonObject json = new JsonObject();
            if (RuleBll.Delete(ruleID))
            {
                json.Put("Result", "OK");
                return Json(json);
            }
            else
            {
                json.Put("Result", "ERROR");
                return Json(json);
            }
        }
        #endregion

        #region 积分限制管理
        public ActionResult PointsLimit(int pageIndex = 1, string viewName = "PointsLimit")
        {
            int pageSize = 10;
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            int endIndex = pageIndex * pageSize;
            int totalCount = limitBll.GetRecordCount("");
            List<Maticsoft.Model.Members.PointsLimit> list = limitBll.GetListByPageExt("", "", startIndex, endIndex);
            if (null == list)
            {
                return PartialView(viewName);
            }
            PagedList<Maticsoft.Model.Members.PointsLimit> pagedList = new PagedList<Maticsoft.Model.Members.PointsLimit>(list, pageIndex, pageSize, totalCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView(viewName, pagedList);
            }
            return PartialView(viewName, pagedList);
        }
        #endregion

        #region 编辑积分限制
        public ActionResult UpdateLimit(string viewName = "UpdateLimit", int id = 0)
        {
            Maticsoft.Model.Members.PointsLimit LimitModel = limitBll.GetModel(id);
            if (LimitModel == null)
            {
                return View("PointsLimit");
            }
            return View(viewName, LimitModel);
        }

        [HttpPost]
        public ActionResult UpdateLimit(string viewName = "UpdateLimit",int limitID = 0, string limitName = "", int Cycle = -1, string CycleUnit = "", int MaxTimes = -1)
        {
            JsonObject json = new JsonObject();
            Maticsoft.Model.Members.PointsLimit LimitModel = limitBll.GetModel(limitID);
            if (limitBll.Exists(limitName,limitID))
            {
                json.Put("Result", "Action");
                return Json(json);
            }
            else
            {
                LimitModel.Name = limitName;
                LimitModel.Cycle = Cycle;
                LimitModel.CycleUnit = CycleUnit;
                LimitModel.MaxTimes = MaxTimes;
                if (limitBll.Update(LimitModel))
                {
                    json.Put("Result", "OK");
                    return Json(json);
                }
                else
                {
                    json.Put("Result", "ERROR");
                    return Json(json);
                }
            }
        }
        #endregion

        #region 添加积分限制
        public ActionResult AddLimit(string viewName = "AddLimit")
        {
            return View(viewName);
        }
      
        [HttpPost]
        public ActionResult AddLimit(string viewName = "AddLimit", string limitName = "", int Cycle = 0, string cycleUnit = "day", int maxTime = 0)
        {
            JsonObject json = new JsonObject();
            limitName = limitName.Trim();
            if (limitBll.ExistsName(limitName))
            {
                json.Put("Result", "Action");
                return Json(json);
            }
            else
            {
                Maticsoft.Model.Members.PointsLimit limitModel = new Model.Members.PointsLimit();
                limitModel.Name = limitName;
                limitModel.Cycle = Cycle;
                limitModel.CycleUnit = cycleUnit;
                limitModel.MaxTimes = maxTime;
                limitModel.TargetId = SupplierId;
                limitModel.TargetType = 0;
                if (limitBll.Add(limitModel) > 0)
                {
                    json.Put("Result", "OK");
                    return Json(json);
                }
                else
                {
                    json.Put("Result", "ERROR");
                    return Json(json);
                }
            }
        }
        #endregion

        #region 删除积分限制
        public ActionResult DeleteLimit(int LimitID)
        {
            JsonObject json = new JsonObject();
            if (limitBll.DeleteEX(LimitID))
            {
                json.Put("Result", "OK");
                return Json(json);
            }
            else
            {
                json.Put("Result", "ERROR");
                return Json(json);
            }
        }
        #endregion

        public ActionResult RankList()
        {
            return View();
        }
        public PartialViewResult LoadRank(string viewName = "_PointRankList", int pageIndex = 1, string txtKeyWord = "")
        {
            int pageSize = 10;
            int startIndex = pageIndex > 1 ? (pageIndex - 1) * pageSize + 1 : 0;
            int endIndex = pageIndex * pageSize;
            StringBuilder strWhere = new StringBuilder();
            if (!String.IsNullOrEmpty(txtKeyWord))
            {
                if (strWhere.Length > 1)
                {
                    strWhere.Append(" and  ");
                }
                strWhere.AppendFormat("Name like '%{0}%'", txtKeyWord);
            }
            int totalCount = rankBll.GetRecordCount(strWhere.ToString());
            List<Maticsoft.Model.Members.UserRank> list =rankBll.GetListByPageExt(strWhere.ToString(), "", startIndex, endIndex);

            if (null == list)
            {
                return PartialView(viewName);
            }
            PagedList<Maticsoft.Model.Members.UserRank> pagedList = new PagedList<Maticsoft.Model.Members.UserRank>(list, pageIndex, pageSize, totalCount);
            if (Request.IsAjaxRequest())
            {
                return PartialView(viewName, pagedList);
            }
            return PartialView(viewName, pagedList);
        }
    }
}