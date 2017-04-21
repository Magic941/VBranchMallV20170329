/**
* GetAdJs.cs
*
* 功 能： [N/A]
* 类 名： GetAdJs.cs
*
* Ver    变更日期                             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012年6月10日 19:40:41   孙鹏   创建
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.AdvertisePosition
{
    public partial class GetAdJs : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 369; } } //网站管理_广告管理_获取广告代码页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strAdId = Request.Params["id"];
                if (!string.IsNullOrWhiteSpace(strAdId)&&!string.IsNullOrWhiteSpace(Request.Params["k"]))
                {
                    int id = Globals.SafeInt(strAdId, -0);
                    if (new BLL.Settings.AdvertisePosition().Exists(id))
                    {
                        string WebHost = Request.Url.Authority;
                        this.txtJs.Text = string.Format("<script src=\"http://{0}/Scripts/Maticsoftpic.js\" type=\"text/javascript\"> MaticSoft.SomeApp.scriptArgs = 'http://{0}&c={1}&a=0'; </script>  ", WebHost, strAdId);
                    }
                    else
                    {
                        this.txtJs.Text = "广告位不存在，请重试！";
                    }
                }
                else
                {
                    int id = Globals.SafeInt(strAdId, -0);
                    if (new BLL.Settings.AdvertisePosition().Exists(id))
                    {
                        string WebHost = Request.Url.Authority;
                        this.txtJs.Text = string.Format("<script src=\"/Scripts/Maticsoftpic.js\" type=\"text/javascript\"> MaticSoft.SomeApp.scriptArgs = '&c={0}&a=0'; </script>  ",  strAdId);
                    }
                    else
                    {
                        this.txtJs.Text = "广告位不存在，请重试！";
                    }
                }
            }
        }
    }
}
