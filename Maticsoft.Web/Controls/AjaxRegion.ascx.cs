using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Controls
{
    public partial class AjaxRegion : System.Web.UI.UserControl
    {

        Maticsoft.BLL.Ms.Regions bll = new Maticsoft.BLL.Ms.Regions();

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
        /// <summary>
        /// 省份ID
        /// </summary>
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
                if ((ddlProvince.SelectedItem != null) && (ddlProvince.SelectedValue.Length > 0))
                {
                    return Convert.ToInt32(ddlProvince.SelectedValue);
                }
                else
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// 城市ID
        /// </summary>
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

                        if (value > 0)
                        {
                            BindArea(value);
                        }
                    }

                }
            }
            get
            {
                if ((ddlCity.SelectedItem != null) && (ddlCity.SelectedValue.Length > 0))
                {
                    return Convert.ToInt32(ddlCity.SelectedValue);
                }
                else
                {
                    return -1;
                }
            }
        }
        /// <summary>
        /// 地区ID
        /// </summary>
        public int Area_iID
        {

            get
            {
                if ((ddlArea.SelectedItem != null) && (ddlArea.SelectedValue.Length > 0))
                {
                    return Convert.ToInt32(ddlArea.SelectedValue);
                }
                else
                {
                    return City_iID;
                }
            }
            set
            {
                Model.Ms.Regions ma = bll.GetModelByCache(value);
                if (ma == null)
                    return;
                if (!ma.ParentId.HasValue)
                    return;
                Model.Ms.Regions mc = bll.GetModelByCache(ma.ParentId.Value);
                if (mc == null)
                    return;

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

        private string _visiblealltext = "请选择";
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

        /// <summary>
        /// 选择的城市或区域的值 - 已过时 请使用SelectedValue
        /// </summary>
        private string _hfValue;
        /// <summary>
        /// 选择的城市或区域的值 - 已过时 请使用SelectedValue
        /// </summary>
        [Obsolete]
        public string HFValue
        {
            set
            {
                _hfValue = value;
                this.HiddenField_SelectValue.Value = _hfValue;
                int count = 0;
                System.Data.DataSet ds = bll.GetParentIDs(Globals.SafeInt(_hfValue, 0), out count);
                if (count == 2)
                {
                    Area_iID = Globals.SafeInt(_hfValue, 0);
                } if (count == 1)
                {
                    City_iID = Globals.SafeInt(_hfValue, 0);
                }
            }
            get
            {
                return _hfValue;
            }
        }

        /// <summary>
        /// 选择的城市或区域的值
        /// </summary>
        public string SelectedValue
        {
            set
            {
                this.HiddenField_SelectValue.Value = value;
                int count = 0;
                System.Data.DataSet ds = bll.GetParentIDs(Globals.SafeInt(value, 0), out count);
                if (count == 2)
                {
                    Area_iID = Globals.SafeInt(value, 0);
                } if (count == 1)
                {
                    City_iID = Globals.SafeInt(value, 0);
                }
            }
            get { return this.HiddenField_SelectValue.Value; }
        }

        private string _classStyle;
        /// <summary>
        /// 样式控制
        /// </summary>
        public string ClassStyle
        {
            get { return _classStyle; }
            set
            {
                _classStyle = value;
                if (!string.IsNullOrWhiteSpace(_classStyle))
                {
                    this.ddlProvince.Attributes.Add("class", _classStyle);
                    this.ddlCity.Attributes.Add("class", _classStyle);
                    this.ddlArea.Attributes.Add("class", _classStyle);
                }
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Area_iID == -1)
                {
                    InitPrivoces();
                }
            }
            else
            {
                if (Area_iID == -1)
                {
                    InitPrivoces();
                }
            }
        }

        private void InitPrivoces()
        {
            //this.ddlProvince.DataSource = bll.GetPrivoces();
            //this.ddlProvince.DataTextField = "RegionName";
            //this.ddlProvince.DataValueField = "RegionId";
            //this.ddlProvince.DataBind();
            //this.ddlProvince.Items.Insert(0, new ListItem("请选择", "0"));
            BindPrivoces();
            this.ddlProvince.Attributes.Add("onchange", "getCitys(this);");
            this.ddlCity.Items.Clear();
            this.ddlCity.Items.Insert(0, new ListItem("请选择", "0"));
            this.ddlArea.Items.Clear();
            this.ddlArea.Items.Insert(0, new ListItem("请选择", "0"));
        }

        #region 绑定下拉数据
        private void BindArea(int City_iID)
        {
            try
            {
                ddlArea.DataSource = bll.GetDistrictByParentId(City_iID);
                ddlArea.DataTextField = "RegionName";
                ddlArea.DataValueField = "RegionId";
                ddlArea.DataBind();
                this.ddlArea.Items.Insert(0, new ListItem("请选择", "0"));
                if (_visibleall)
                {
                    this.ddlArea.Items.Insert(0, new ListItem(_visiblealltext, "0"));
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

        private void BindCity(int ParentId)
        {
            try
            {
                ddlCity.DataSource = bll.GetDistrictByParentId(ParentId);
                ddlCity.DataTextField = "RegionName";
                ddlCity.DataValueField = "RegionId";
                ddlCity.DataBind();
                this.ddlCity.Items.Insert(0, new ListItem("请选择", "0"));
                if (_visibleall)
                {
                    this.ddlCity.Items.Insert(0, new ListItem(_visiblealltext, "0"));
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

        protected void BindPrivoces()
        {
            try
            {
                ddlProvince.DataSource = bll.GetPrivoces();
                ddlProvince.DataTextField = "RegionName";
                ddlProvince.DataValueField = "RegionId";
                ddlProvince.DataBind();
                this.ddlProvince.Items.Insert(0, new ListItem("请选择", "0"));
                if (_visibleall)
                {
                    this.ddlProvince.Items.Insert(0, new ListItem(_visiblealltext, "0"));
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
        #endregion

    }
}