using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Serialization;
using Maticsoft.Json;
using Maticsoft.Json.Conversion;


namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class SurveyController : MPageControllerBase
    {
        //
        // GET: /Mobile/WapSurvey/

        #region 问卷调查
        //在线调查 
        /// <summary>
        ///问卷的题目 
        /// </summary>
        /// <param name="fid">问卷的ID</param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult Index(int fid = 0, string viewName = "Index")
        {
            BLL.Poll.Topics topicManage = new BLL.Poll.Topics();
            List<Model.Poll.Topics> list = topicManage.GetModelList(-1, fid);
            ViewBag.FormID = fid;
            return View(viewName, list);
        }

        /// <summary>
        /// 题目的答案列表
        /// </summary>
        /// <param name="qnumber">第几题</param>
        /// <param name="type">题目的类型 单选 多选  反馈</param>
        /// <param name="topicid">题目的编号</param>
        /// <param name="viewName">viewName</param>
        /// <returns></returns>
        public PartialViewResult Options(int qnumber, int type, int topicid = -1, string viewName = "_Options")
        {
            ViewBag.type = type;
            ViewBag.topicid = topicid;
            ViewBag.qnumber = qnumber;
            BLL.Poll.Options optionBll = new BLL.Poll.Options();
            List<Model.Poll.Options> list = optionBll.GetModelList(string.Format(" TopicID={0}", topicid));
            return PartialView(viewName, list);
        }
        [HttpPost]
        public ActionResult SubmitPoll(FormCollection fm) //题目的ID和答案的ID
        {
            string data = fm["TopicIDjson"];
            if (Request.Cookies["votetopic"] != null)
                return Content("isnotnull");//ERROR  "您已经投过票，请不要重复投票！
            if (String.IsNullOrWhiteSpace(data))
            {
                return Content("false");
            }
            BLL.Poll.PollUsers pollUserBll = new BLL.Poll.PollUsers();
            Model.Poll.PollUsers polluserModel = new Model.Poll.PollUsers();
            int userid= pollUserBll.Add(polluserModel);//创建投票用户
            if (userid < 0)
            {
                return Content("false");
            }
            Model.Poll.UserPoll modelup = new Model.Poll.UserPoll();
            BLL.Poll.UserPoll bllup = new BLL.Poll.UserPoll();
            modelup.UserIP = Request.UserHostAddress;
            modelup.UserID = userid;
            JsonArray jsonArray = JsonConvert.Import<JsonArray>(data);
            foreach (JsonObject jsonObject in jsonArray)
            {
                int topicid = Common.Globals.SafeInt(jsonObject["topicid"].ToString(), 0); 
                string topicvlaue = jsonObject["topicvlaue"].ToString();
                int type = Common.Globals.SafeInt(jsonObject["type"].ToString(), -1);
                modelup.TopicID = topicid;  //TopicID; //问题的ID
                switch (type)
                {
                    case 0://单选
                        modelup.OptionID = Common.Globals.SafeInt(topicvlaue, -1); // Option; //答案
                        bllup.Add(modelup);
                        break;
                    case 1://多选
                        modelup.OptionIDList = topicvlaue; // Option; //答案
                        if (!String.IsNullOrWhiteSpace(topicvlaue))
                        {
                            bool i = bllup.Add2(modelup);
                        }
                        break;
                    case 2://反馈  待补充  暂时只支持单选、多选
                        break;
                    default:
                        break;
                }
            }
            HttpCookie httpCookie = new HttpCookie("votetopic");
            httpCookie.Values.Add("voteid", "votetopic");
            httpCookie.Expires = DateTime.Now.AddHours(240);
            Response.Cookies.Add(httpCookie);
            return Content("true"); //成功   
        }

        /// <summary>
        /// 结果
        /// </summary>
        /// <param name="fid">题目的ID</param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult Result(int fid = 1, string viewName = "Result")
        {
            BLL.Poll.Topics topicManage = new BLL.Poll.Topics();
            List<Model.Poll.Topics> list = topicManage.GetModelList(-1, fid);
            return View(viewName, list);
        }

        public ActionResult ResultOptions(int tid, string viewName = "_ResultOptions")
        {
            BLL.Poll.Options blloption = new BLL.Poll.Options();
            List<Model.Poll.Options> list = blloption.GetCountList(" TopicID=" + tid);
            return View(viewName, list);
        }
        #endregion

    }
}
