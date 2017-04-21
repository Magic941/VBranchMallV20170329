/**
* CodeGen.cs
*
* 功 能： [N/A]
* 类 名： CodeGen
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/3/10 10:12:38  Rock    初版
*
* Copyright (c) 2014 Maticsoft Corporation. All rights reserved.
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

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class CodeGen : PageBaseAdmin
    {
        Maticsoft.BLL.SysManage.TaskQueue taskBll = new BLL.SysManage.TaskQueue();
        protected void Page_Load(object sender, EventArgs e)
        {
            //产品缩略图生成
            this.txtTaskCount.Value = taskBll.GetRecordCount(" type=" + (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopProductCode).ToString();
            this.txtTaskReCount.Text = taskBll.GetRecordCount(" type=" + (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopProductCode + " and Status=0").ToString();
            Maticsoft.Model.SysManage.TaskQueue taskModel = taskBll.GetLastModel((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopProductCode);

            if (taskModel != null)
            {
                this.txtTaskDate.Text = taskModel.RunDate.Value.ToString("yyyy-MM-dd");
                this.txtTaskId.Text = (taskModel.ID + 1).ToString();
            }
            else
            {
                this.txtTaskDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.txtTaskId.Text = "1";
            }
        }
        public int Type
        {
            get
            {
                return (int)Model.SysManage.EnumHelper.TaskQueueType.ShopProductCode;
            }
        }
    }
}