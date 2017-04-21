using System;
using System.Web;
using System.Web.UI.WebControls;
using Maticsoft.Accounts.Bus;
using Maticsoft.Common;
using Maticsoft.Map.BLL;
using Maticsoft.Map.Model;

namespace Maticsoft.Web.Enterprise
{
    public partial class MapManage : PageBaseEnterprise
    {
        BLL.Members.Users blluser = new BLL.Members.Users();
        Maticsoft.Map.BLL.MapInfoManage mapInfoManage = new MapInfoManage();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrentUser != null)
                {
                    hfEnID.Value = CurrentUser.DepartmentID;
                    int departmentId = Globals.SafeInt(CurrentUser.DepartmentID, 0);
                    MapInfo mapInfo = mapInfoManage.GetModelByDepartmentId(departmentId);
                    if (mapInfo == null) return;
                    hfMapId.Value = mapInfo.MapId.ToString();
                    txtPointerTitle.Text = HttpUtility.HtmlDecode(mapInfo.PointerTitle);
                    txtPointerContent.Text = Globals.HtmlDecodeForSpaceWrap(mapInfo.PointerContent);
                    txtMarkersLongitude.Text = mapInfo.MarkersLongitude;
                    txtMarkersDimension.Text = mapInfo.MarkersDimension;
                    hfMapImgUrl.Value = mapInfo.PointImg;
                }
            }
        }
    }
}