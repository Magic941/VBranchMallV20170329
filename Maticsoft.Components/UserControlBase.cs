using System;
using System.Collections;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;

namespace Maticsoft.Web
{
    public abstract class UserControlBase : System.Web.UI.UserControl
    {
        #region 权限控制


        //子类通过 new 重写该值        
        protected int Act_ShowInvalid = 2; //查看失效数据        


        /// <summary>
        ///  权限角色验证对象
        /// </summary>
        public AccountsPrincipal UserPrincipal
        {
            get
            {
#if false
                if (Context.User.Identity.IsAuthenticated)
                {
                    return new AccountsPrincipal(Context.User.Identity.Name);
                }
                else
                {
                    return null;
                }
#else
                if (!Context.User.Identity.IsAuthenticated) return null;
                if (Session[Globals.SESSIONKEY_ADMIN] == null) return null;

                return new AccountsPrincipal(
                    ((Maticsoft.Accounts.Bus.User)
                        Session[Globals.SESSIONKEY_ADMIN]).UserName);
#endif

            }
        }

        //private Maticsoft.Accounts.Bus.User currentUser;
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public Maticsoft.Accounts.Bus.User CurrentUser
        {
            //DONE: UserControlBase类, 仅供admin后台使用 Session[Globals.SESSIONKEY_ADMIN]
            get
            {
#if false
                if (Session[Globals.SESSIONKEY_ADMIN] == null)
                {
                    if (UserPrincipal != null)
                    {
                        return new Maticsoft.Accounts.Bus.User(UserPrincipal);
                    }
                    else
                    {
                        return new User();
                    }

                }
                else
                {
                    return (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_ADMIN];

                } 
#else
                if (Session[Globals.SESSIONKEY_ADMIN] == null) return new User();
                return (Maticsoft.Accounts.Bus.User)Session[Globals.SESSIONKEY_ADMIN];
#endif
            }
        }


        #endregion


        #region 公共方法

        /// <summary>
        /// 根据功能行为编号得到所属权限编号
        /// </summary>
        /// <returns></returns>
        public int GetPermidByActID(int ActionID)
        {
            Actions bllAction = new Actions();
            Hashtable ActHashtab = bllAction.GetHashListByCache();
            object obj = ActHashtab[ActionID.ToString()];
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                return -1;
            }
        }

        #endregion

    }
}