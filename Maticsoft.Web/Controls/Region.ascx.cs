using System;
using System.Data;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Controls
{
    public partial class Region : System.Web.UI.UserControl
    {
        BLL.Ms.Regions bll = new BLL.Ms.Regions();

        #region 属性
        public bool ProvinceVisible
        {
            set
            {
                ddlProvince.Visible = value;
            }
        }
        public bool CityVisible
        {
            set
            {
                ddlCity.Visible = value;
            }
        }
        public bool AreaVisible
        {
            set
            {
                ddlArea.Visible = value;
            }
        }
        public bool ProvinceEnabled
        {
            set
            {
                ddlProvince.Enabled = value;
            }
        }
        public bool CityEnabled
        {
            set
            {
                ddlCity.Enabled = value;
            }
        }
        public bool AreaEnabled
        {
            set
            {
                ddlArea.Enabled = value;
            }
        }


        public int Province_iID
        {
            set
            {
                if (ddlProvince.Items.Count > 0)
                {
                    ddlProvince.SelectedValue = value.ToString();
                }
            }
            get
            {
                return (ddlProvince.SelectedItem != null) && (ddlProvince.SelectedValue.Length > 0)
                           ? Convert.ToInt32(ddlProvince.SelectedValue)
                           : -1;
            }
        }
        public int City_iID
        {
            set
            {
                Model.Ms.Regions model = bll.GetModel(value);
                if (model != null)
                {
                    BindPrivoces();
                    ddlProvince.SelectedValue = model.ParentId.ToString();
                    if (model.ParentId > 0)
                    {
                        BindCity(model.ParentId.Value);
                        ddlCity.SelectedValue = value.ToString();
                    }

                }
            }
            get
            {
                return (ddlCity.SelectedItem != null) && (ddlCity.SelectedValue.Length > 0)
                           ? Convert.ToInt32(ddlCity.SelectedValue)
                           : -1;
            }
        }
        public int Area_iID
        {

            get
            {
                return (ddlArea.SelectedItem != null) && (ddlArea.SelectedValue.Length > 0)
                           ? Convert.ToInt32(ddlArea.SelectedValue)
                           : City_iID;
            }
            set
            {
                Model.Ms.Regions ma = bll.GetModelByCache(value);
                if (ma == null || ma.ParentId == null) return;
                Model.Ms.Regions mc = bll.GetModelByCache(ma.ParentId.Value);
                if (mc != null)
                {
                    BindPrivoces();
                    ddlProvince.SelectedValue = mc.ParentId.ToString();

                    if (mc.ParentId > 0)
                    {
                        BindCity(mc.ParentId.Value);
                        this.ddlCity.SelectedValue = mc.RegionId.ToString();
                    }
                    if (mc.RegionId > 0)
                    {
                        BindArea(mc.RegionId);
                        ddlArea.SelectedValue = value.ToString();
                    }
                }
            }
        }

        public int Region_iID
        {
            get
            {
                if ((ddlArea.SelectedItem != null) && (ddlArea.SelectedValue.Length > 0))
                {
                    int area = Globals.SafeInt(ddlArea.SelectedValue, 0);
                    if (area > 0) return area;
                }
                if ((ddlCity.SelectedItem != null) && (ddlCity.SelectedValue.Length > 0))
                {
                    int city = Globals.SafeInt(ddlCity.SelectedValue, 0);
                    if (city > 0) return city;
                }
                return Province_iID;
            }
            set
            {
                Model.Ms.Regions ma = bll.GetModelByCache(value);
                if (ma == null) return;
                switch (ma.Depth)
                {
                    case 1:
                        BindPrivoces();
                        if (ddlProvince.Items.Count > 0)
                        {
                            int ParentId = Convert.ToInt32(ma.RegionId);
                            BindCity(ParentId);
                        }
                        if (ddlCity.Items.Count > 0)
                        {
                            int City_iID = Convert.ToInt32(ddlCity.Items[0].Value);
                            BindArea(City_iID);
                        }
                        ddlProvince.SelectedValue = ma.RegionId.ToString();
                        return;
                    case 2:
                        BindPrivoces();
                        if (ddlProvince.Items.Count > 0)
                        {
                            ddlProvince.SelectedValue = ma.ParentId.Value.ToString();
                            BindCity(ma.ParentId.Value);
                        }
                        if (ddlCity.Items.Count > 0)
                        {
                            BindArea(ma.RegionId);
                        }
                        ddlCity.SelectedValue = ma.RegionId.ToString();
                        break;
                    case 3:
                        Model.Ms.Regions mb = bll.GetModelByCache(ma.ParentId.Value);
                        if (mb == null || !mb.ParentId.HasValue) return;
                        BindPrivoces();
                        if (ddlProvince.Items.Count > 0)
                        {
                            ddlProvince.SelectedValue = mb.ParentId.Value.ToString();
                            BindCity(mb.ParentId.Value);
                        }
                        if (ddlCity.Items.Count > 0)
                        {
                            ddlCity.SelectedValue = mb.RegionId.ToString();
                            BindArea(mb.RegionId);
                        }
                        ddlArea.SelectedValue = ma.RegionId.ToString();
                        break;
                    default:
                        break;
                }
            }
        }

        private bool _visibleall = false;
        /// <summary>
        /// 显示所有
        /// </summary>
        public bool VisibleAll
        {
            set
            {
                _visibleall = value;
            }
        }

        private string _visiblealltext = "全部";
        /// <summary>
        /// 显示所有文字
        /// </summary>
        public string VisibleAllText
        {
            set
            {
                _visiblealltext = value;
            }
        }


        private bool _autobinddata = true;
        /// <summary>
        /// 是否自动绑定数据（当对控件设置数据时，请用false）
        /// </summary>
        public bool AutoBindData
        {
            set
            {
                _autobinddata = value;
            }
        }

        /// <summary>
        /// 是否刷新（默认true）
        /// </summary>
        public bool AutoPostBackArea
        {
            set
            {
                ddlArea.AutoPostBack = value;
            }
        }


        #endregion


        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (_autobinddata)
                {
                    if (Area_iID != -1)
                    {
                        GetCityByArea(Area_iID);
                        GetProvinceByCity(City_iID);

                    }
                    else
                    {

                        BindPrivoces();
                        if (ddlProvince.Items.Count > 0)
                        {
                            int ParentId = Convert.ToInt32(ddlProvince.Items[0].Value);
                            BindCity(ParentId);
                        }
                        if (ddlCity.Items.Count > 0)
                        {
                            int City_iID = Convert.ToInt32(ddlCity.Items[0].Value);
                            BindArea(City_iID);
                        }

                    }
                }
            }
        }

        #region AutoBindData
        protected void BindPrivoces()
        {

            try
            {
                DataSet ds = bll.GetPrivoces();
                ddlProvince.DataSource = ds;
                ddlProvince.DataTextField = "RegionName";
                ddlProvince.DataValueField = "RegionId";
                ddlProvince.DataBind();
                if (_visibleall)
                {
                    this.ddlProvince.Items.Insert(0, new ListItem(_visiblealltext, "0"));
                    this.ddlProvince.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                Model.SysManage.ErrorLog model = new Model.SysManage.ErrorLog();
                model.Loginfo = ex.Message;
                model.StackTrace = ex.StackTrace;
                model.Url = Request.Url.AbsoluteUri;
                BLL.SysManage.ErrorLog.Add(model);
            }

        }

        protected void ddlProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlProvince.SelectedItem != null)
            {
                ddlCity.Items.Clear();
                ddlArea.Items.Clear();

                int ParentId = Convert.ToInt32(ddlProvince.SelectedValue);
                BindCity(ParentId);
                ddlCity_SelectedIndexChanged(null, null);
                //droplistCompany_SelectedIndexChanged(null, null);
            }
        }
        private void BindCity(int ParentId)
        {
            try
            {
                ddlCity.DataSource = bll.GetDistrictByParentId(ParentId);
                ddlCity.DataTextField = "RegionName";
                ddlCity.DataValueField = "RegionId";
                ddlCity.DataBind();
                if (_visibleall)
                {
                    this.ddlCity.Items.Insert(0, new ListItem(_visiblealltext, "0"));
                    this.ddlCity.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                Model.SysManage.ErrorLog model = new Model.SysManage.ErrorLog();
                model.Loginfo = ex.Message;
                model.StackTrace = ex.StackTrace;
                model.Url = Request.Url.AbsoluteUri;
                BLL.SysManage.ErrorLog.Add(model);
            }
        }

        private void GetCityByArea(int id)
        {
            Model.Ms.Regions model = bll.GetModel(id);
            if (model != null)
            {
                City_iID = Convert.ToInt32(model.ParentId);
            }
        }
        private void GetProvinceByCity(int id)
        {
            Model.Ms.Regions model = bll.GetModel(id);
            if (model != null)
            {
                Province_iID = Convert.ToInt32(model.ParentId);
            }
        }
        protected void ddlCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlCity.SelectedItem != null)
            {
                ddlArea.Items.Clear();

                int City_iID = Convert.ToInt32(ddlCity.SelectedValue);
                BindArea(City_iID);

                ddlArea_SelectedIndexChanged(null, null);
            }
        }
        private void BindArea(int City_iID)
        {
            try
            {
                ddlArea.DataSource = bll.GetDistrictByParentId(City_iID);
                ddlArea.DataTextField = "RegionName";
                ddlArea.DataValueField = "RegionId";
                ddlArea.DataBind();
                if (_visibleall)
                {
                    this.ddlArea.Items.Insert(0, new ListItem(_visiblealltext, "0"));
                    this.ddlArea.SelectedValue = "0";
                }
            }
            catch (Exception ex)
            {
                Model.SysManage.ErrorLog model = new Model.SysManage.ErrorLog();
                model.Loginfo = ex.Message;
                model.StackTrace = ex.StackTrace;
                model.Url = Request.Url.AbsoluteUri;
                BLL.SysManage.ErrorLog.Add(model);
            }
        }

        protected void ddlArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.DeptSelectedIndexChanged != null)
                this.DeptSelectedIndexChanged(this, e);

        }

        #endregion

        #region 事件

        //定义委托， onselectedindexchanged="ddlArea_SelectedIndexChanged"
        public delegate void userEventArea(object sender, EventArgs arg);
        public event userEventArea DeptSelectedIndexChanged;

        #endregion




    }
}