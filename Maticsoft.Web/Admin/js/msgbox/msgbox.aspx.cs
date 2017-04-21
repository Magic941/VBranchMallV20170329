/**
* msgbox.cs
*
* 功 能： 提示信息
* 类 名： msgbox
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/25 12:08:43  蒋海滨    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Maticsoft.Common;

namespace Maticsoft.Web.Admin.js.msgbox
{
    public partial class msgbox : PageBaseAdmin
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Maticsoft.Common.MessageBox.ShowServerBusyTip(this, "服务器繁忙，请稍后再试。");
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "设置成功！");
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            Maticsoft.Common.MessageBox.ShowFailTip(this, "数据拉取失败！");
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            Maticsoft.Common.MessageBox.ShowLoadingTip(this, "正在加载中，请稍后...");
        }
    }
}