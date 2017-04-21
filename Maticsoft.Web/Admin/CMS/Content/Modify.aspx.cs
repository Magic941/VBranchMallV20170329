using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.Common;
using Maticsoft.Web.Components.Setting.CMS;

namespace Maticsoft.Web.CMS.Content
{
    public partial class Modify : PageBaseAdmin
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
                else
                {
                    MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent, "List.aspx");
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
                this.lblContentID.Text = model.ContentID.ToString();
                this.txtTitle.Text = Globals.HtmlDecode(model.Title);
                this.txtSubTitle.Text = Globals.HtmlDecode(model.SubTitle);
                this.txtBeFrom.Text = model.BeFrom;
                this.txtSummary.Text = Globals.HtmlDecode(model.Summary);
                this.txtContent.Text = model.Description;
                this.txtLinkUrl.Text = Globals.HtmlDecode(model.LinkUrl);
                this.hfPvCount.Value = model.PvCount.ToString();
                this.radlState.SelectedValue = model.State.ToString();
                this.dropClass.SelectedValue = model.ClassID.ToString();
                this.txtKeywords.Text = Globals.HtmlDecode(model.Keywords);
                this.txtOrders.Text = model.Sequence.ToString();
                this.chkIsRecomend.Checked = model.IsRecomend;
                this.chkIsHot.Checked = model.IsHot;
                this.chkIsColor.Checked = model.IsColor;
                this.chkIsTop.Checked = model.IsTop;
                this.txtRemary.Text = Globals.HtmlDecode(model.Remary);
                this.lblUser.Text = model.CreatedUserID.ToString();
                this.imgUrl.ImageUrl = "~/images/no_photo_s.png";
                this.txtSeoUrl.Text = model.SeoUrl;

                this.txtMeta_Title.Text = model.Meta_Title;
                this.txtMeta_Keywords.Text = model.Meta_Keywords;
                this.txtMeta_Description.Text = model.Meta_Description;

                if (!string.IsNullOrWhiteSpace(model.ImageUrl))
                {
                    this.HiddenField_ICOPath.Value = this.imgUrl.ImageUrl = model.ImageUrl;
                }

                if (!string.IsNullOrWhiteSpace(model.Attachment))
                {
                    this.hfs_Attachment.Value = model.Attachment;
                    this.HiddenField_OldAttachPath.Value = model.Attachment;
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
            if (!PageValidate.IsNumber(lblContentID.Text))
            {
                MessageBox.ShowFailTip(this, Resources.CMS.ContentErrorContentID);
                return;
            }
            if (String.IsNullOrWhiteSpace(this.txtSeoUrl.Text))
            {
                MessageBox.ShowFailTip(this, "请填写SeoURL地址!");
                return;
            }
            if (ContentID > 0)
            {
                Maticsoft.Model.CMS.Content model = bll.GetModel(ContentID);
                if (null == model)
                {
                    MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent, "List.aspx");
                }
                model.Title = Globals.HtmlEncode(txtTitle.Text);
                model.Attachment = this.hfs_Attachment.Value;

                #region IsCheck

                model.IsRecomend = chkIsRecomend.Checked;
                model.IsHot = chkIsHot.Checked;
                model.IsColor = chkIsColor.Checked;
                model.IsTop = chkIsTop.Checked;

                #endregion IsCheck

                #region SEO 优化
                model.Meta_Title = this.txtMeta_Title.Text;
                model.Meta_Keywords = this.txtMeta_Keywords.Text;
                model.Meta_Description = this.txtMeta_Description.Text;
                #endregion

                if (!string.IsNullOrWhiteSpace(txtSubTitle.Text))
                {
                    model.SubTitle = Globals.HtmlEncode(txtSubTitle.Text);
                }
                else
                {
                    model.SubTitle = Globals.HtmlEncode(this.txtTitle.Text);
                }
                model.Summary = Globals.HtmlEncode(txtSummary.Text);
                model.LinkUrl = Globals.HtmlEncode(txtLinkUrl.Text);
                model.Remary = Globals.HtmlEncode(txtRemary.Text);
                if (txtBeFrom.Text.Length > 0)
                {
                    model.BeFrom = txtBeFrom.Text;
                }
                else
                {
                    BLL.SysManage.WebSiteSet webSiteSet = new BLL.SysManage.WebSiteSet(Model.SysManage.ApplicationKeyType.CMS);
                    model.BeFrom = webSiteSet.WebName;
                }
                model.LastEditUserID = CurrentUser.UserID;
                model.LastEditDate = DateTime.Now;

                //model.PvCount = int.Parse(hfPvCount.Value);
                model.State = int.Parse(radlState.SelectedValue);
                model.ClassID = ClassID;
                model.Keywords = Globals.HtmlEncode(txtKeywords.Text);
                model.Sequence = int.Parse(txtOrders.Text);
                model.Description = txtContent.Text;
                model.SeoUrl = txtSeoUrl.Text;

                //待上传的图片名称
                string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));

                //上传图片正式地址
                string ImageFile = string.Format("/Upload/CMS/Article/{0}/", DateTime.Now.ToString("yyyyMM"));

                //上传附件正式地址
                string attachmentFile = string.Format("/Upload/CMS/Files/{0}/", DateTime.Now.ToString("yyyyMM"));

                string ThumbImageFile = string.Format("/Upload/CMS/ArticleThumbs/{0}/", DateTime.Now.ToString("yyyyMM"));

                //待上传的图片名称
                ArrayList imageList = new ArrayList();
                if (!string.IsNullOrWhiteSpace(HiddenField_ISModifyImage.Value))
                {
                    string imageUrl = Maticsoft.Web.Components.FileHelper.MoveImage(HiddenField_ICOPath.Value, ImageFile, ThumbImageFile, Maticsoft.Model.Ms.EnumHelper.AreaType.CMS);
                    model.ImageUrl = imageUrl.Split('|')[0];
                    model.ThumbImageUrl = imageUrl.Split('|')[1];
                }
                else
                {
                    model.ImageUrl = HiddenField_ICOPath.Value;
                }

                string fileName = string.Empty;
                if (!string.IsNullOrWhiteSpace(HiddenField_IsModifyAttachment.Value))
                {
                    string attachmenUrl = string.Format(hfs_Attachment.Value, "");
                    fileName = attachmenUrl.Replace(tempFile, "");
                    model.Attachment = attachmenUrl.Replace(tempFile, attachmentFile);
                }
                else
                {
                    model.Attachment = hfs_Attachment.Value;
                }
                if (bll.Update(model))
                {
                    if (!string.IsNullOrWhiteSpace(this.HiddenField_IsDeleteAttachment.Value))
                    {
                        //删除附件
                        Common.FileManage.DeleteFile(Server.MapPath(this.HiddenField_OldAttachPath.Value));
                    }
                    if (!string.IsNullOrWhiteSpace(HiddenField_ISModifyImage.Value))
                    {
                        //将图片从临时文件夹移动到正式的文件夹下
                        Common.FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(ImageFile), imageList);
                    }
                    if (!string.IsNullOrWhiteSpace(HiddenField_IsModifyAttachment.Value))
                    {
                        //将附件从临时文件夹移动到正式的文件夹下
                        Common.FileManage.MoveFile(Server.MapPath(tempFile), Server.MapPath(attachmentFile), fileName);
                    }

                    #region 文章静态化
                    bool isStatic = this.chkStatic.Checked;
                    if (isStatic)
                    {
                        string area = BLL.SysManage.ConfigSystem.GetValueByCache("MainArea");
                        string requestUrl = "";//静态化请求地址
                        string saveUrl = PageSetting.GetCMSUrl(ContentID);
                        if (area == "CMS")
                        {
                            requestUrl = "/Article/Details/" + ContentID;
                        }
                        else
                        {
                            requestUrl = "/CMS/Article/Details/" + ContentID;
                        }
                        if (!String.IsNullOrWhiteSpace(requestUrl) && !String.IsNullOrWhiteSpace(saveUrl))
                        {
                            Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(requestUrl, saveUrl);
                        }
                    }
                    #endregion
                    #region 首页静态化
                    bool IndexStatic = this.chkIndex.Checked;
                    if (IndexStatic)
                    {
                        string area = BLL.SysManage.ConfigSystem.GetValueByCache("MainArea");
                        string requestUrl = "";//静态化请求地址
                        string saveUrl = "/index.html";
                        if (area == "CMS")
                        {
                            requestUrl = "/Home/Index?type=1";
                        }
                        else
                        {
                            requestUrl = "/CMS/Home/Index?type=1";
                        }
                        if (!String.IsNullOrWhiteSpace(requestUrl) && !String.IsNullOrWhiteSpace(saveUrl))
                        {
                            Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(requestUrl, saveUrl);
                        }
                    }
                    #endregion


                    //清理文章缓存
                    Maticsoft.Common.DataCache.DeleteCache("ContentModel-" + model.ContentID);
                    Maticsoft.Common.DataCache.DeleteCache("ContentModelEx-" + model.ContentID);
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "List.aspx?type=0");
                }
                else
                {
                    MessageBox.ShowFailTip(this, Resources.Site.TooltipSaveError);
                }
            }
            else
            {
                MessageBox.ShowServerBusyTip(this, Resources.CMS.ContentErrorNoContent, "List.aspx");
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

        #region 取消操作

        /// <summary>
        /// 取消操作
        /// </summary>
        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("List.aspx?type=0");
        }

        #endregion 取消操作
    }
}