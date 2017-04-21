

namespace Maticsoft.Web.Areas.CMS.Controllers
{
    /// <summary>
    /// CMS网站前台基类
    /// </summary>
    public class CMSControllerBase : Maticsoft.Web.Controllers.ControllerBase
    {        
        public CMSControllerBase()
        {
            ViewBag.BaiduShareUserId =
                Common.Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("BaiduShareUserId"), 0);
            
                
        }
    }
}
