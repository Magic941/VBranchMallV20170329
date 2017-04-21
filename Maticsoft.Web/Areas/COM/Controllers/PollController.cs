using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Maticsoft.Json;

namespace Maticsoft.Web.Areas.COM.Controllers
{
    public class PollController : Maticsoft.Web.Controllers.ControllerBase
    {
        private BLL.Poll.Forms formManage = new BLL.Poll.Forms();
        private BLL.Poll.Topics topicManage = new BLL.Poll.Topics();
        private BLL.Poll.Options optionManage = new BLL.Poll.Options();

        //
        //GET:http://localhost:8050/com/poll/poll?fid=3

        public ActionResult Poll(int? fid)
        {
            if (fid.HasValue)
            {
                Model.Poll.PollActionHelper model = new Model.Poll.PollActionHelper();
                Model.Poll.Forms formModel = formManage.GetModel(fid.Value);
                ViewBag.FID = fid.Value;
                if (formModel != null)
                {
                    model.FormsHelper = formModel;
                    List<Model.Poll.Topics> list = topicManage.GetModelList(string.Format("FormID={0}", fid.Value));
                    if (list != null && list.Count > 0)
                    {
                        model.TopicsHelper = list;
                    }
                    if (model.TopicsHelper == null || model.FormsHelper == null)
                    {
                        return View();
                    }
                    else
                    {
                        return View(model);
                    }
                }
                else
                {
                    return View("error");
                }
            }
            else
            {
                return View("error");
            }
        }

        [HttpPost]
        public void SubmitPoll(FormCollection collection)
        {
            JsonObject json = new JsonObject();

            if (Request.Cookies["vote" + collection["FID"]] != null)
            {
                json.Accumulate("STATUS", "805");//ERROR
                json.Accumulate("DATA", "您已经投过票，请不要重复投票！");
            }
            else
            {
                Maticsoft.Model.Poll.UserPoll modelup = new Model.Poll.UserPoll();
                Maticsoft.BLL.Poll.UserPoll bllup = new Maticsoft.BLL.Poll.UserPoll();

                modelup.UserID = Common.Globals.SafeInt(collection["UID"], 0);
                modelup.UserIP = Request.UserHostAddress;

                string userPoll = collection["Option"];
                if (!string.IsNullOrWhiteSpace(userPoll))
                {
                    string[] userPollResults = userPoll.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    int index = 0;
                    foreach (string item in userPollResults)
                    {
                        string[] option = item.Split(new char[] { '_' }, StringSplitOptions.RemoveEmptyEntries);
                        modelup.TopicID = Common.Globals.SafeInt(option[0], -1);
                        modelup.OptionID = Common.Globals.SafeInt(option[1], -1);
                        bllup.Add(modelup);
                        index++;
                    }
                    if (index == userPollResults.Length)
                    {
                        HttpCookie httpCookie = new HttpCookie("vote" + collection["FID"]);
                        httpCookie.Values.Add("voteid", collection["FID"]);
                        httpCookie.Expires = DateTime.Now.AddHours(240);
                        Response.Cookies.Add(httpCookie);
                        json.Accumulate("STATUS", "800");//成功
                    }
                    else
                    {
                        json.Accumulate("STATUS", "804");//ERROR
                    }
                }
                else
                {
                    json.Accumulate("STATUS", "804");//ERROR
                }
            }
            Response.Write(json.ToString());
        }
    }
}