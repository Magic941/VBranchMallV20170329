using System;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace Maticsoft.Web.Handlers
{
    public class WeChatReturnHandler : IHttpHandler, IRequiresSessionState
    {
        #region IHttpHandler 成员

        public bool IsReusable
        {
            get { return false; }
        }
        public void ProcessRequest(HttpContext context)
        {
            string code = context.Request.Params["code"];
            //微信授权
            if (!String.IsNullOrWhiteSpace(code))
            {
                string state = Common.Globals.UrlDecode(context.Request.Params["state"]);
                string openId = state.Substring(0, state.IndexOf("|"));
                string returnUrl = state.Substring(state.IndexOf("|") + 1);
                string appId = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppId", openId);
                string appSercet = Maticsoft.WeChat.BLL.Core.Config.GetValueByCache("WeChat_AppSercet", openId);
                if (!String.IsNullOrWhiteSpace(appId) && !String.IsNullOrWhiteSpace(appSercet))
                {
                    //网页授权
                    string userOpenId = Maticsoft.WeChat.BLL.Core.Utils.GetUserOpenId(appId, appSercet, code);
                    // 处理ReturnUrl
                    #region 把用户的OpenID存进Cookies
                    context.Response.Cookies["WeChat_UserName"].Value = userOpenId;
                    context.Response.Cookies["WeChat_UserName"].Expires = DateTime.Now.AddMonths(1);
                    #endregion
                    context.Session["WeChat_UserName"] = userOpenId;
                    context.Session["WeChat_OpenId"] = openId;

                    #region 记录用户浏览页面
                    Maticsoft.WeChat.BLL.Core.OPLog.AddViewLog(userOpenId, openId, returnUrl);
                    #endregion
                    Redirect(context, returnUrl);
                }
            }
            else
            {
                string mpKey = context.Request.QueryString["mp"];
                string returnUrl = Common.Globals.UrlDecode(context.Request.QueryString["returnUrl"]);
                bool isRepeat = Common.Globals.SafeBool(context.Request.QueryString["rep"], true);
                if (!string.IsNullOrWhiteSpace(mpKey))
                {
                    string baseUrl = "http://" + Common.Globals.DomainFullName + "/wcreturn.aspx?returnUrl={0}&mp={1}&rep={2}";
                    string link = String.Format(baseUrl, Common.Globals.UrlEncode(returnUrl), mpKey, isRepeat);
                    //解码
                    string mpKey_D = Maticsoft.Common.DEncrypt.DESEncrypt.Decrypt(mpKey);
                    if (String.IsNullOrWhiteSpace(mpKey_D))
                    {
                        Redirect(context, returnUrl);
                        return;
                    }
                    var arrrKey = mpKey_D.Split('|');
                    if (arrrKey.Count() < 2 || String.IsNullOrWhiteSpace(arrrKey[0]) || String.IsNullOrWhiteSpace(arrrKey[1]))
                    {
                        Redirect(context, returnUrl);
                        return;
                    }
                    #region 记录用户浏览页面
                    Maticsoft.WeChat.BLL.Core.OPLog.AddViewLog(arrrKey[0], arrrKey[1], returnUrl);
                    #endregion
                    //允许重复访问
                    if (isRepeat)
                    {
                        #region 把用户的OpenID存进Cookies
                        context.Response.Cookies["WeChat_UserName"].Value = arrrKey[0];
                        context.Response.Cookies["WeChat_UserName"].Expires = DateTime.Now.AddMonths(1);
                        #endregion
                        context.Session["WeChat_UserName"] = arrrKey[0];
                        context.Session["WeChat_OpenId"] = arrrKey[1];
                        Redirect(context, returnUrl);
                        return;
                    }
                    if (Maticsoft.WeChat.BLL.Core.LinkLog.ExistsEx(link))
                    {
                        context.Session["WeChat_UserName"] = arrrKey[0];
                        context.Session["WeChat_OpenId"] = arrrKey[1];

                        #region 把用户的OpenID存进Cookies
                        context.Response.Cookies["WeChat_UserName"].Value = arrrKey[0];
                        context.Response.Cookies["WeChat_UserName"].Expires = DateTime.Now.AddMonths(1);
                        #endregion

                        Maticsoft.WeChat.BLL.Core.LinkLog.DeleteEx(link);
                        Redirect(context, returnUrl);
                        return;
                    }
                }
            }
            Redirect(context, 
                "/COM/WeChat/FailLink");
        }

        private void Redirect(HttpContext context, string url)
        {
            context.Response.Clear();
            context.Response.Write(
                string.Format("<script type=\"text/javascript\">window.location.replace('{0}');</script>", url));
            context.Response.End();
        }

        #endregion
    }
}