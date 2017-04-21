using System;
using System.Web;
using Maticsoft.Common;
using Maticsoft.Map.BLL;
using Maticsoft.Map.Model;

namespace Maticsoft.Web.Admin.Ms.Enterprise
{
    public partial class MapManage : PageBaseAdmin
    {
        Maticsoft.Map.BLL.MapInfoManage mapInfoManage = new MapInfoManage();
        protected override int Act_PageLoad { get { return 319; } } //企业管理_地图管理页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.QueryString["DepartmentId"]))
                {
                    int departmentId = Globals.SafeInt(Request.QueryString["DepartmentId"], -1);
                    hfEnID.Value = departmentId.ToString();
                    MapInfo mapInfo = mapInfoManage.GetModelByDepartmentId(departmentId);
                    if (mapInfo == null)
                    {
                        Maticsoft.BLL.Ms.Enterprise bll = new Maticsoft.BLL.Ms.Enterprise();
                        Maticsoft.Model.Ms.Enterprise model = bll.GetModel(departmentId);
                        if (model == null) return;
                        if (model.RegionID.HasValue)
                        {
                            BLL.Ms.Regions bllRegions = new BLL.Ms.Regions();
                            txtCity.Value = bllRegions.GetRegionNameByRID(model.RegionID.Value);
                        }
                    }
                    else
                    {
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
}