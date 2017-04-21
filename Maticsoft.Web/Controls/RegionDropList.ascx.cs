using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Controls
{
    public partial class RegionDropList : System.Web.UI.UserControl
    {
        AjaxMethod ajax = new AjaxMethod();
        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(AjaxMethod));
            if (RegionId.HasValue)
            {
                BindReg(RegionId.Value);
            }
            else
            {
                InitRegion();
            }
        }

        private int? _regionId;
        /// <summary>
        /// 地区ID
        /// </summary>
        public int? RegionId
        {
            get { return _regionId; }
            set { _regionId = value; }
        }

        /// <summary>
        /// 初始化绑定数据
        /// </summary>
        private void InitRegion()
        {
            this.ddlProvince.DataSource = ajax.GetPovinceList();
            this.ddlProvince.DataTextField = "RegionName";
            this.ddlProvince.DataValueField = "RegionId";
            this.ddlProvince.DataBind();
            this.ddlProvince.Items.Insert(0, new ListItem("请选择", ""));
            this.ddlProvince.Attributes.Add("onChange", "cityResult();");
            this.ddlCity.Attributes.Add("onChange", "areaResult();");
        }

        /// <summary>
        /// 根据已有的RegionID获取对应的地区名称
        /// </summary>
        /// <param name="id"></param>
        private void BindReg(int id)
        {
            int SecparentId = int.Parse(ajax.GetParentId(id).Rows[0]["ParentId"].ToString());
            int parentId = int.Parse(ajax.GetParentId(SecparentId).Rows[0]["ParentId"].ToString());

            this.ddlProvince.DataSource = ajax.GetPovinceList();
            this.ddlProvince.DataTextField = "RegionName";
            this.ddlProvince.DataValueField = "RegionId";
            this.ddlProvince.DataBind();
            ddlProvince.SelectedValue = id.ToString();
            this.ddlProvince.Attributes.Add("onChange", "cityResult();");
            this.ddlCity.Attributes.Add("onChange", "areaResult();");

            if (ddlProvince.SelectedValue != "")
            {
                this.ddlCity.DataSource = ajax.GetCityList(parentId);
                this.ddlCity.DataTextField = "RegionName";
                this.ddlCity.DataValueField = "RegionId";
                this.ddlCity.DataBind();
                ddlCity.SelectedValue = SecparentId.ToString();
            }
            if (ddlCity.SelectedValue != "")
            {
                this.ddlArea.DataSource = ajax.GetAreaList(SecparentId);
                this.ddlArea.DataTextField = "RegionName";
                this.ddlArea.DataValueField = "RegionId";
                this.ddlArea.DataBind();
                ddlArea.SelectedValue = id.ToString();
            }
        }
    }
}