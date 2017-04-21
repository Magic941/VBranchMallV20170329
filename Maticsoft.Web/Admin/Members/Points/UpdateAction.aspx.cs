using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Members.Points
{
    public partial class UpdateAction : PageBaseAdmin
    {
        private Maticsoft.BLL.Members.PointsAction actionBll = new BLL.Members.PointsAction();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Maticsoft.Model.Members.PointsAction actionModel = actionBll.GetModel(ActionId);
                if (actionModel != null)
                {
                    this.tName.Text = actionModel.Name;
                }
                else
                {
                    Response.Redirect("ActionList.aspx");
                }
            }
        }

        #region 编号

        /// <summary>
        /// 编号
        /// </summary>
        protected int ActionId
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["id"]))
                {
                    id = Globals.SafeInt(Request.Params["id"], 0);
                }
                return id;
            }
        }

        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Maticsoft.Model.Members.PointsAction actionModel = actionBll.GetModel(ActionId);
            actionModel.Name = this.tName.Text;
            if (actionBll.Update(actionModel))
            {
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "操作失败！");
            }

        }
    }
}