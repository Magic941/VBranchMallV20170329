
using System.Web.Mvc;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Areas.SNS.Controllers
{
    /// <summary>
    /// SNS网站前台基类
    /// </summary>
    [SNSError]
    public class SNSControllerBase : Maticsoft.Web.Controllers.ControllerBase
    {
        public int FallDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallDataSize", ApplicationKeyType.SNS), 20);
        public int PostDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_PostDataSize", ApplicationKeyType.SNS), 15);
        public int CommentDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_CommentDataSize", ApplicationKeyType.SNS), 5);
        public int FallInitDataSize = Common.Globals.SafeInt(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_FallInitDataSize", ApplicationKeyType.SNS), 5);

        private readonly BLL.SNS.TaoBaoConfig _taoBaoConfig = new BLL.SNS.TaoBaoConfig(ApplicationKeyType.OpenAPI);
     
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (!filterContext.IsChildAction)
            {
                ViewBag.TaoBaoAppkey = _taoBaoConfig.TaoBaoAppkey;
            }
            base.OnResultExecuting(filterContext);
        }
    }
}
