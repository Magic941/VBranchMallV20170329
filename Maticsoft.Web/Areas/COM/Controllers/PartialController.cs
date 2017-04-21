using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.COM.Controllers
{
    public class PartialController : Maticsoft.Web.Controllers.ControllerBase
    {
        //
        // GET: /COM/Partial/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PollOptions(int? topicId, bool isCheckBox)
        {
            if (topicId.HasValue)
            {
                BLL.Poll.Options manage = new BLL.Poll.Options();
                List<Model.Poll.Options> list = manage.GetModelList(string.Format("TopicID={0}", topicId.Value));
                if (list != null && list.Count > 0)
                {
                    ViewBag.IsCheckBox = isCheckBox;
                    return View(list);
                }
                else
                {
                    return View();//OR 错误页
                }
            }
            else
            {
                return View();//OR 错误页
            }
        }
    }
}
