using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    public class UserController : UsersProfileControllerBase
    {
        Maticsoft.BLL.Members.UsersExp UserExBll = new BLL.Members.UsersExp();

        //
        // GET: /SNS/UserProfile/

        public UserController()
        {
            this.FavBasePageSize = FallInitDataSize;
            this.FavAllPageSize = FallDataSize;
            this._PostPageSize = PostDataSize;
        }

        public ActionResult Index()
        {
            return View();
        }
       
         #region 重载用户信息
        public override bool LoadUserInfo(int UserID)
        {
            if ((this.UserModel = UserExBll.GetUsersExpModel(UserID)) != null)
            {
                this.UserID = UserID;
                this.DefaultPostType = Model.SNS.EnumHelper.PostType.User;
                this.IsCurrentUser = false;
                #region 后期需要优化这部分，用户扩展表中取不到NickName
                Maticsoft.BLL.Members.Users UserBll = new BLL.Members.Users();
                Maticsoft.Model.Members.Users model = new Model.Members.Users();
                model = UserBll.GetModel(UserID);
                this.NickName = model != null ? model.NickName : "";
                this.Activity =model != null ? model.Activity.Value:false;
                #endregion
                return true;
            }
            return false;
        }
    } 
        #endregion
}
