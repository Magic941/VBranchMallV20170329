using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using Maticsoft.Common;

namespace Maticsoft.Web.Controls
{
    public partial class PCC : System.Web.UI.UserControl
    {
        Maticsoft.BLL.Ms.Regions bll = new Maticsoft.BLL.Ms.Regions();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetProvinces();
                if (PageValidate.IsNumber(this.ddlProvince.SelectedValue))
                {
                    GetCitys(int.Parse(this.ddlProvince.SelectedValue));
                }
                if (PageValidate.IsNumber(this.ddlCity.SelectedValue))
                {
                    GetCountry(int.Parse(this.ddlCity.SelectedValue));
                }
            }
        }

        /// <summary>
        /// 获取省份数据
        /// </summary>
        public void GetProvinces()
        {
            DataSet ds = bll.GetProvinces();
            this.ddlProvince.DataSource = ds.Tables[0];
            this.ddlProvince.DataTextField = "RegionName";
            this.ddlProvince.DataValueField = "RegionID";
            this.ddlProvince.DataBind();
        }

        /// <summary>
        /// 获取城市数据
        /// </summary>
        /// <param name="parentID"></param>
        public void GetCitys(int parentID)
        {
            DataSet ds = bll.GetCitys(parentID);
            this.ddlCity.DataSource = ds.Tables[0];
            this.ddlCity.DataTextField = "RegionName";
            this.ddlCity.DataValueField = "RegionID";
            this.ddlCity.DataBind();
        }

        /// <summary>
        /// 获取县城数据
        /// </summary>
        /// <param name="parentID"></param>
        public void GetCountry(int parentID)
        {
            DataSet ds = bll.GetCitys(parentID);
            this.ddlCountry.DataSource = ds.Tables[0];
            this.ddlCountry.DataTextField = "RegionName";
            this.ddlCountry.DataValueField = "RegionID";
            this.ddlCountry.DataBind();
        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageValidate.IsNumber(this.ddlProvince.SelectedValue))
            {
                GetCitys(int.Parse(this.ddlProvince.SelectedValue));
            }
            if (PageValidate.IsNumber(this.ddlCity.SelectedValue))
            {
                GetCountry(int.Parse(this.ddlCity.SelectedValue));
            }
        }

        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (PageValidate.IsNumber(this.ddlCity.SelectedValue))
            {
                GetCountry(int.Parse(this.ddlCity.SelectedValue));
            }
        }
    }
}