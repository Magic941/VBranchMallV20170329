using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.Categories
{
    public partial class Relation : PageBaseAdmin
    {

        Maticsoft.BLL.SNS.CategorySource bll = new BLL.SNS.CategorySource();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(Request.Params["id"].Trim()))
                {
                    this.SNSCate.SelectedValue = Request.Params["id"];
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int SNSCategoryId = Globals.SafeInt(this.SNSCate.SelectedValue, 0);
            int TaoBaoCategoryId = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
           bool IsLoop = Globals.SafeBool(radlState.SelectedValue, false);
           if (bll.UpdateSNSCate(TaoBaoCategoryId, SNSCategoryId, IsLoop))
            {
                MessageBox.ShowSuccessTip(this, "商品对应成功");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "商品对应类别成功!", this);
            }
            else
            {
                MessageBox.ShowFailTip(this, "商品对应失败！");
                LogHelp.AddUserLog(CurrentUser.UserName, CurrentUser.UserType, "商品对应类别失败!", this);
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("CateList.aspx");
        }
    }
}
