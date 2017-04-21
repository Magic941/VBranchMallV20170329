using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.WeChat.Scene
{
    public partial class UpdateScene : PageBaseAdmin
    {
        Maticsoft.WeChat.BLL.Core.Scene sceneBll = new Maticsoft.WeChat.BLL.Core.Scene();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Maticsoft.WeChat.Model.Core.Scene actionModel = sceneBll.GetModel(SceneId);
                if (actionModel != null)
                {
                    this.tName.Text = actionModel.Name;
                    this.tDesc.Text = actionModel.Remark;
                }
                
            }
        }

        #region 编号

        /// <summary>
        /// 编号
        /// </summary>
        protected int SceneId
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
            Maticsoft.WeChat.Model.Core.Scene sceneModel = sceneBll.GetModel(SceneId);
            sceneModel.Name = this.tName.Text;
            sceneModel.Remark = this.tDesc.Text;
            if (sceneBll.Update(sceneModel))
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