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
    public partial class UpdateSimpleSingleArticle : PageBaseAdmin
    {
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        private Maticsoft.BLL.CMS.ContentClass bllContentClass = new Maticsoft.BLL.CMS.ContentClass();

        public string strClassID = string.Empty;
        protected override int Act_PageLoad { get { return 229; } } //CMS_内容管理_编辑页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindTree();
                if (ContentID > 0)
                {
                    ShowInfo();
                }
               
            }
        }

        #region 内容编号

        /// <summary>
        /// 内容编号
        /// </summary>
        protected int ContentID
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

        #endregion 内容编号

        private void ShowInfo()
        {
            Maticsoft.Model.CMS.Content model = bll.GetModel(ContentID);

            if (null != model)
            {
                this.txtTitle.Text = model.Title;
                this.txtSummary.Text = model.Summary;
                this.txtContent.Text = model.Description;
                this.radlState.SelectedValue = model.State.ToString();
                this.imgUrl.ImageUrl = "~/images/no_photo_s.png";
                this.dropClass.SelectedValue = model.ClassID.ToString();
                if (!string.IsNullOrWhiteSpace(model.ImageUrl))
                {
                    this.HiddenField_ICOPath.Value = this.imgUrl.ImageUrl = model.ImageUrl;
                }
            
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
            int ClassID = Globals.SafeInt(dropClass.SelectedValue, 0);
            Maticsoft.Model.CMS.ContentClass modelContentClass = bllContentClass.GetModelByCache(ClassID);

            if (string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, Resources.CMS.TitleErrorAddContent);
                return;
            }

            if (modelContentClass != null && !modelContentClass.AllowAddContent)
            {
                MessageBox.ShowFailTip(this, Resources.CMS.ContentErrorAddContent);
                return;
            }
        
          
            if (ContentID > 0)
            {
                Maticsoft.Model.CMS.Content model = bll.GetModel(ContentID);
                if (null == model)
                {
                    MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent, "ListSimple.aspx");
                }
                model.Title = Globals.HtmlEncode(txtTitle.Text);

                model.Summary = Globals.HtmlEncode(txtSummary.Text);
             
             
                model.LastEditUserID = CurrentUser.UserID;
                model.LastEditDate = DateTime.Now;

                //model.PvCount = int.Parse(hfPvCount.Value);
                model.State = int.Parse(radlState.SelectedValue);
                model.ClassID = ClassID;
                model.Description = txtContent.Text;

                //待上传的图片名称
                string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));

                //上传图片正式地址
                string ImageFile = string.Format("/Upload/CMS/Article/{0}/", DateTime.Now.ToString("yyyyMM"));

                //上传附件正式地址
                string attachmentFile = string.Format("/Upload/CMS/Files/{0}/", DateTime.Now.ToString("yyyyMM"));

                //待上传的图片名称
                ArrayList imageList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(HiddenField_ISModifyImage.Value))
                {
                    string imageUrl = string.Format(HiddenField_ICOPath.Value, "");
                    imageList.Add(imageUrl.Replace(tempFile, ""));
                    model.ImageUrl = imageUrl.Replace(tempFile, ImageFile);
                }
                else
                {
                    model.ImageUrl = HiddenField_ICOPath.Value;
                }

            
                if (bll.Update(model))
                {
                  
                    if (!string.IsNullOrWhiteSpace(HiddenField_ISModifyImage.Value))
                    {
                        //将图片从临时文件夹移动到正式的文件夹下
                        Common.FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);
                    }
                  
                    //清理文章缓存
                    Maticsoft.Common.DataCache.DeleteCache("ContentModel-" + model.ContentID);
                    Maticsoft.Common.DataCache.DeleteCache("ContentModelEx-" + model.ContentID);
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK);
                }
                else
                {
                    MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
                }
            }
            else
            {
                MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent);
            }
        }

        #endregion 保存

        #region 绑定菜单树

        private void BindTree()
        {
            this.dropClass.Items.Clear();

            DataSet ds = bllContentClass.GetTreeList("");
            if (!DataSetTools.DataSetIsNull(ds))
            {
                DataTable dt = ds.Tables[0];

                //加载树
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    DataRow[] drs = dt.Select("ParentID= " + 0);
                    foreach (DataRow r in drs)
                    {
                        string nodeid = Globals.SafeString(r["ClassID"], "0");
                        string text = Globals.SafeString(r["ClassName"], "0");
                        string parentid = Globals.SafeString(r["ParentID"], "0");

                        //string permissionid = r["PermissionID"].ToString();
                        text = "╋" + text;
                        this.dropClass.Items.Add(new ListItem(text, nodeid));
                        int sonparentid = int.Parse(nodeid);
                        string blank = "├";
                        BindNode(sonparentid, dt, blank);
                    }
                }
            }
            this.dropClass.DataBind();
        }

        private void BindNode(int parentid, DataTable dt, string blank)
        {
            DataRow[] drs = dt.Select("ParentID= " + parentid);

            foreach (DataRow r in drs)
            {
                string nodeid = Globals.SafeString(r["ClassID"], "0");
                string text = Globals.SafeString(r["ClassName"], "0");

                //string permissionid = r["PermissionID"].ToString();
                text = blank + "『" + text + "』";
                this.dropClass.Items.Add(new ListItem(text, nodeid));
                int sonparentid = int.Parse(nodeid);
                string blank2 = blank + "─";
                BindNode(sonparentid, dt, blank2);
            }
        }

        #endregion 绑定菜单树

 
    }
}