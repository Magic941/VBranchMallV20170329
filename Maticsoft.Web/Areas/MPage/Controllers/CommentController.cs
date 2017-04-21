using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.MPage.Controllers
{
    public class CommentController : MPageControllerBase
    {
        //
        // GET: /Mobile/WapComment/
        /// <summary>
        /// 评论
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="typeid">默认类型为文章</param>
        /// <param name="viewName"></param>
        /// <returns></returns>
        public ActionResult Index(int top = 6, int id = -1, int typeid = 3, string viewName = "Index")
        {
            ViewBag.contentid = id;
            ViewBag.typeid = typeid;
            BLL.CMS.Comment commentBll = new BLL.CMS.Comment();
            List<Model.CMS.Comment> listComment = commentBll.GetModelList(top, id, typeid);
            return View(viewName, listComment);
        }
        #region 评论
 
        /// <summary>
        /// 提交评论
        /// </summary>
        /// <returns></returns>
        public ActionResult AjaxComment(FormCollection Fm)
        {
            int id = Common.Globals.SafeInt(Fm["id"], 0);
            int typeid = Common.Globals.SafeInt(Fm["typeid"], 0);
            string cont = Common.InjectionFilter.Filter(Fm["cont"]);
            string username = Common.InjectionFilter.Filter(Fm["username"]);
            if (id > 0 && typeid > 0 && !String.IsNullOrWhiteSpace(cont) && !String.IsNullOrWhiteSpace(username))
            {
                BLL.CMS.Comment commentBll = new BLL.CMS.Comment();
                Model.CMS.Comment commentModel = new Model.CMS.Comment();
                commentModel.CreatedDate = DateTime.Now;
                commentModel.CreatedNickName = username;
                commentModel.Description = cont;
                commentModel.State = false;
                commentModel.ParentID = 0;
                commentModel.TypeID = typeid;//
                commentModel.IsRead = false;
                commentModel.ContentId = id;
                commentModel.CreatedUserID = -1;
                return commentBll.AddTran(commentModel) > 0 ? Content("true") : Content("false");
            }
            return Content("false");
        }
        #endregion
    }
}
