using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class ImageReGen : PageBaseAdmin
    {
        Maticsoft.BLL.SysManage.TaskQueue taskBll = new BLL.SysManage.TaskQueue();
        protected void Page_Load(object sender, EventArgs e)
        {
               //产品缩略图生成
                this.txtTaskCount.Value = taskBll.GetRecordCount(" type="+(int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopImageReGen).ToString();
                this.txtTaskReCount.Text = taskBll.GetRecordCount(" type="+(int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopImageReGen+" and Status=0").ToString();
                Maticsoft.Model.SysManage.TaskQueue taskModel = taskBll.GetLastModel((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopImageReGen);

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
    }
}