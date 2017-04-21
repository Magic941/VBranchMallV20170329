using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SysManage
{
    public partial class GetImageJs : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 211; } } 
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSetCollection_Click(object sender, System.EventArgs e)
        {
            if (Maticsoft.BLL.CMS.GenerateHtml.GenImageJs())
            {
                MessageBox.ShowSuccessTip(this, "图片采集工具生成成功");
            }
            else
            {
                MessageBox.ShowFailTip(this, "图片采集工具生成失败");
            }
          
        }
    }
}