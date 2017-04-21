using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Admin.Ms.Themes
{
    public partial class MShopList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 334; } } //设置_模版管理页

        protected new int Act_DelData = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }

        #region DataList

        public void BindData()
        {
            //获取该主区域 下的所有模板
            List<Maticsoft.Model.Ms.Theme> themeList = Maticsoft.Web.Components.FileHelper.GetThemes("MShop");
            //获取当前主模板
            string name = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("MShop_Theme");
            foreach (var item in themeList)
            {
                if (item.Name == name)
                {
                    item.IsCurrent = true;
                }
            }
            DataListPhoto.DataSource = themeList;
            DataListPhoto.DataBind();
        }

        protected void DataListPhoto_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "start")
            {
                if (e.CommandArgument != null)
                {
                    string name = e.CommandArgument.ToString();
                    //写
                    Maticsoft.BLL.SysManage.ConfigSystem.Modify("MShop_Theme", name, "微商城模板的名称");
                    Cache.Remove("ConfigSystemHashList");    //清除网站设置的缓存文件
                    MessageBox.ShowSuccessTip(this, "启用成功", "MShopList.aspx");
                }
            }
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        #endregion

    }
}