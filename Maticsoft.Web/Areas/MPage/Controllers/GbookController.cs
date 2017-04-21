using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class GbookController : MPageControllerBase
    {
        //
        // GET: /Mobile/WapGbook/

        #region 在线留言
        //在线留言
        public ActionResult Index(string viewName = "Index",int top=6)
        {
            BLL.Members.Guestbook gbookBll = new BLL.Members.Guestbook();
            List<Model.Members.Guestbook> listgbook = gbookBll.GetModelList(top);
            return View(viewName, listgbook);  
        }
        /// <summary>
        /// 提交留言
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult AjaxGbook(FormCollection Fm)
        {
            string title = Common.InjectionFilter.Filter(Fm["Title"]);
            string cont = Common.InjectionFilter.Filter(Fm["Content"]);
            string email = Common.InjectionFilter.Filter(Fm["Email"]);
            if (!String.IsNullOrWhiteSpace(title) && !String.IsNullOrWhiteSpace(cont) && !String.IsNullOrWhiteSpace(email))
            {
                BLL.Members.Guestbook guestBll = new BLL.Members.Guestbook();
                Model.Members.Guestbook gbookModel = new Model.Members.Guestbook();
                gbookModel.CreatedDate = DateTime.Now;
                gbookModel.CreatorEmail = email;
                gbookModel.Description = cont;
                gbookModel.Title = title;
                gbookModel.Status = 0;
                gbookModel.ToUserID = -1;
                gbookModel.ParentID = 0;
                gbookModel.CreateNickName = email;
                return guestBll.Add(gbookModel) > 0 ? Content("true") : Content("false");
            }
            return Content("false");
        }
        #endregion

    }
}
