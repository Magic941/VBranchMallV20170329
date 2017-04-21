using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.CMS.Content
{
    public partial class UpdateSimArti : PageBaseAdmin
    {
        private  BLL.CMS.Content bll = new  BLL.CMS.Content();
        protected override int Act_PageLoad { get { return 229; } } //CMS_内容管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (CID > 0)
                {
                    ShowInfo();
                }
                else
                {
                    MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent);
                }

            }
        }

        #region 内容编号
        /// <summary>
        /// 栏目编号
        /// </summary>
        protected int CID
        {
            get
            {
                int id = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["classid"]))
                {
                    id = Globals.SafeInt(Request.Params["classid"], 0);
                }
                return id;
            }
        }

       

        #endregion 内容编号

        private void ShowInfo()
        {
            Model.CMS.Content model = bll.GetModelByClassIDByCache(CID);
            if (null != model)
            {
                this.txtContent.Text = model.Description;
            }
            else
            {
                MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent);
            }
        }

        #region 保存

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (CID <= 0)
            {
                MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent);
                return;
            }
            Model.CMS.Content model = bll.GetModelByClassIDByCache(CID);
            if (model == null)
            {
                MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent);
                return;
            }
            model.LastEditUserID = CurrentUser.UserID;
            model.LastEditDate = DateTime.Now;
            model.Description = txtContent.Text;
            if (bll.Update(model))
            {
                //清理文章缓存
                Maticsoft.Common.DataCache.DeleteCache("ContentModelClassID-" + CID);
                Maticsoft.Common.DataCache.DeleteCache("ContentModel-" + model.ContentID);
                Maticsoft.Common.DataCache.DeleteCache("ContentModelEx-" + model.ContentID);
                MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
            }
            else
            {
                MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
            }
        }

        #endregion 保存
    }
}