using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Json;

namespace Maticsoft.Web.Areas.MShop.Controllers
{
    public class ActivityController : MShopControllerBase
    {
        //
        // GET: /MShop/Activity/
        private Maticsoft.WeChat.BLL.Activity.ActivityInfo infoBll = new WeChat.BLL.Activity.ActivityInfo();
        public ActionResult Index()
        {
            return View();
        }

        #region 刮刮卡
        /// <summary>
        /// 刮刮卡
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Scratch(int id)
        {
            //获取活动信息
            Maticsoft.WeChat.Model.Activity.ActivityInfo infoModel = infoBll.GetActivity(id,0);
            Maticsoft.WeChat.BLL.Activity.ActivityCode codeBll = new WeChat.BLL.Activity.ActivityCode();
            ViewBag.HasChange = codeBll.HasChance(id, UserOpen);
            return View(infoModel);
        }
        #endregion 

        #region 大转盘
        public ActionResult BigWheel(int id)
        {
            //获取活动信息
            Maticsoft.WeChat.Model.Activity.ActivityInfo infoModel = infoBll.GetActivity(id,1);
            Maticsoft.WeChat.BLL.Activity.ActivityCode codeBll = new WeChat.BLL.Activity.ActivityCode();
            ViewBag.HasChange = codeBll.HasChance(id, UserOpen);
            return View(infoModel);
        }
        #endregion 
        /// <summary>
        /// 查看是否有机会获取
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult HasChange(FormCollection collection)
        {
            Maticsoft.WeChat.BLL.Activity.ActivityCode codeBll = new WeChat.BLL.Activity.ActivityCode();
            int activityId = Common.Globals.SafeInt(collection["ActivityId"], 0);
            if (activityId == 0)
            {
                return Content("False");
            }
            bool hasChange = codeBll.HasChance(activityId, UserOpen);
            return hasChange ? Content("True") : Content("False");
        }
        /// <summary>
        /// 获取奖品码
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetSNCode(FormCollection collection)
        {
            Maticsoft.WeChat.BLL.Activity.ActivityCode codeBll = new WeChat.BLL.Activity.ActivityCode();
            int activityId = Common.Globals.SafeInt(collection["ActivityId"], 0);
            JsonObject json = new JsonObject();

            if (activityId == 0 || String.IsNullOrWhiteSpace(UserOpen))
            {
                json.Put("STATUS", "False");
                return Content(json.ToString());
            }
            json.Put("STATUS", "True");
            Maticsoft.WeChat.Model.Activity.ActivityCode codeModel = codeBll.GetAwardCode(activityId, UserOpen);

            if (codeModel == null)
            {
                json.Put("Data", "NoData");
                return Content(json.ToString());
            }
            else
            {
                json.Put("Data", codeModel.AwardName);
                json.Put("SNCode", codeModel.CodeName);
                return Content(json.ToString());
            }
        }
        
        /// <summary>
        /// 获取活动礼品
        /// </summary>
        /// <param name="id"></param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public PartialViewResult AwardList(int id, string viewName = "_AwardList")
        {
            Maticsoft.WeChat.BLL.Activity.ActivityAward awardBll = new WeChat.BLL.Activity.ActivityAward();
            List<Maticsoft.WeChat.Model.Activity.ActivityAward> list = awardBll.GetAwardList(id);
            return PartialView(viewName, list);
        }
    }
}
