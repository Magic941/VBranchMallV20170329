/**
* List.cs
*
* 功 能： [N/A]
* 类 名： List.cs
*
* Ver    变更日期      负责人  变更内容
* ───────────────────────────────────
* V0.01
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Web.Components;

namespace Maticsoft.Web.Admin.Ms.Themes
{
    public partial class List : PageBaseAdmin
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
            string areaName = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("MainArea");
            List<Maticsoft.Model.Ms.Theme> themeList = Maticsoft.Web.Components.FileHelper.GetThemes(areaName);
            //获取当前主模板
            string name = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ThemeCurrent");
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
                    Maticsoft.BLL.SysManage.ConfigSystem.Modify("ThemeCurrent", name, "当前主模板的名称");
                    DataCache.SetCache("ThemeCurrent", name);
                    System.Web.HttpRuntime.UnloadAppDomain();   //重启网站
                    string url;
                    switch (MvcApplication.MainAreaRoute)
                    {
                        case AreaRoute.MPageSP:
                        case AreaRoute.MPage:
                        case AreaRoute.CMS:
                            url = "/Admin/CMS/Photo/PhotoReGen.aspx";
                            break;
                        case AreaRoute.MShop:
                        case AreaRoute.MobileSP:
                        case AreaRoute.MobileAG:
                        case AreaRoute.Shop:
                            url = "/Admin/Shop/Products/ImageReGen.aspx";
                            break;
                        case AreaRoute.SNS:
                            url = "/Admin/SNS/Photos/ImageReGen.aspx";
                            break;
                        default:
                            url = string.Empty;
                            break;
                    }
                    if (string.IsNullOrWhiteSpace(url))
                    {
                        MessageBox.ShowSuccessTip(this, "模版切换成功!", "List.aspx");
                    }
                    else
                    {
                        MessageBox.ShowSuccessTip(this, "模版切换成功, 请重新生成网站缩略图, 即将为您跳转..", url);
                    }
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