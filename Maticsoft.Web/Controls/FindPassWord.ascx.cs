using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Controls
{
/********************************************************************************

        ** 作者： Rock

        ** 创始时间：2012年4月12日 18:59:46

         ** 功能描述：找回密码功能 需要url传参：uid和email
     
        ** 修改人：

        ** 修改时间：

        **修改描述：

*********************************************************************************/
    public partial class FindPassWord : System.Web.UI.UserControl
    {
        Model.SysManage.VerifyMail vmmodel = new Model.SysManage.VerifyMail();
        BLL.SysManage.VerifyMail vmbll = new BLL.SysManage.VerifyMail();

        private string strWebSiteTitle;
        /// <summary>
        /// 网站名
        /// </summary>
        public string StrWebSiteTitle
        {
            get { return strWebSiteTitle; }
            set { strWebSiteTitle = value; }

        }
        private string _errorUrl;
        /// <summary>
        /// 发送失败，跳转的URl
        /// </summary>
        public string ErrorUrl
        {
            get { return _errorUrl; }
            set { _errorUrl = value; }
        }

        private string _skipUrl;
        /// <summary>
        /// 发送成功，跳转的URL地址
        /// </summary>
        public string SkipUrl
        {
            get { return _skipUrl; }
            set { _skipUrl = value; }
        }


        protected void btnSendEmail_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Request.Params["uid"]) && !string.IsNullOrWhiteSpace(Request.Params["email"]))
            {
                string emailStr = Request.Params["email"];//获取URl传过来的邮箱
                string uidStr = Request.Params["uid"];//获取URl传过来的用户名
                string keyvalue = Guid.NewGuid().ToString().Replace("-", "");//生成验证码
                vmmodel.UserName = uidStr;
                vmmodel.KeyValue = keyvalue;
                vmmodel.CreatedDate = DateTime.Now;
                vmmodel.Status = 0;
                vmmodel.ValidityType = 1;
                if (vmbll.Add(vmmodel))
                {
                    try
                    {
                        string connstr = BLL.SysManage.ConfigSystem.GetValueByCache("GetPwdUrl"); //配置路径
                        string str = connstr + "?keyvalue=" + keyvalue; //校验参数
                        string content = "亲爱的【" + StrWebSiteTitle + "】用户，请您在七天内点击（或复制到浏览器地址栏）以下连接进行密码重置: <a href=" + str + ">【" + str + "】</a>";//发送内容
                        Maticsoft.Common.MailSender.Send(emailStr, StrWebSiteTitle + "找回密码", content);
                        Response.Redirect(SkipUrl);
                    }
                    catch (Exception ex)
                    {
                        Maticsoft.Common.MessageBox.ShowAndRedirect(Page, ex.Message, ErrorUrl);
                    }
                }
            }
        }
    }
}