using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.Scene
{
    public partial class SceneList : PageBaseAdmin
    {
        private Maticsoft.WeChat.BLL.Core.Scene sceneBll = new Maticsoft.WeChat.BLL.Core.Scene();
        private Maticsoft.WeChat.BLL.Core.SceneDetail detailBll = new Maticsoft.WeChat.BLL.Core.SceneDetail();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindData();
            }
        }


        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        #region gridView


        public void BindData()
        {
            int count= sceneBll.GetRecordCount("");
            this.AspNetPager1.RecordCount =count;
            hfDataCount.Value = count.ToString();
            DataListProduct.DataSource = sceneBll.GetListByPage("", "CreateTime desc ", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            DataListProduct.DataBind();
        }


        public override void VerifyRenderingInServerForm(Control control)
        {

        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }
        protected void DataListProduct_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "delete")
            {
                if (e.CommandArgument != null)
                {
                    int sceneId = Globals.SafeInt(e.CommandArgument.ToString(), 0);
                    int count = detailBll.GetCount(sceneId);
                    if (count > 0)
                    {
                        MessageBox.ShowFailTip(this, "该场景已经被应用，请不要删除");
                        return;
                    }
                    if (sceneBll.Delete(sceneId))
                    {
                        MessageBox.ShowSuccessTip(this, Resources.Site.TooltipDelOK);
                    }
                }
                BindData();
            }
        }
        #endregion

    }
}