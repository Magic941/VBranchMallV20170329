using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Members;
using Maticsoft.Common;
using System.Web.Mvc;
using Maticsoft.Web.Components.Setting.CMS;
using Maticsoft.Json;

namespace Maticsoft.Web.CMS.Content
{
    public partial class Add : PageBaseAdmin
    {
        private string uploadFolder = BLL.SysManage.ConfigSystem.GetValueByCache("UploadFolder");
        private Maticsoft.BLL.CMS.Content bll = new Maticsoft.BLL.CMS.Content();
        private Maticsoft.BLL.CMS.ContentClass bllContentClass = new Maticsoft.BLL.CMS.ContentClass();
        public string strClassID = string.Empty;
        protected override int Act_PageLoad { get { return 228; } } //CMS_内容管理_添加页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) &&
     (this.Request.Form["Callback"] == "true"))
                {
                    this.Controls.Clear();
                    this.DoCallback();
                }
                BindTree();
                if (ClassID > 0)
                {
                    strClassID = hfClassID.Value = "?classid=" + ClassID;
                    ddlType.SelectedValue = ClassID.ToString();
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                    return;
                }
                txtOrders.Text = (bll.GetMaxSeq() + 1).ToString();
            }
        }

        public int ClassID
        {
            get
            {
                int id = 0;
                string strid = Request.Params["classid"];
                if (!string.IsNullOrWhiteSpace(strid) && PageValidate.IsNumber(strid))
                {
                    id = int.Parse(strid);
                }
                return id;
            }
        }

        #region 保存

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int ClassID = Globals.SafeInt(ddlType.SelectedValue, 0);
            if (ClassID <= 0)
            {
                MessageBox.ShowFailTip(this, Resources.CMS.ContentErrorAddClass);
                return;
            }
            if (String.IsNullOrWhiteSpace(this.txtSeoUrl.Text))
            {
                MessageBox.ShowFailTip(this, "请填写SeoURL地址!");
                return;
            }
            Maticsoft.Model.CMS.ContentClass modelContentClass = bllContentClass.GetModel(ClassID);
            if (modelContentClass != null)
            {
                if (!modelContentClass.AllowAddContent)
                {
                    MessageBox.ShowFailTip(this, Resources.CMS.ContentErrorAddContent);
                    return;
                }
                if (2 == modelContentClass.ClassModel)
                {
                    if (bll.ExistsByClassID(ClassID))
                    {
                        MessageBox.ShowFailTip(this, Resources.CMS.ContentErrorAddMoreContent);
                        return;
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(this.txtTitle.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, Resources.CMS.TitleErrorAddContent);
                return;
            }

            //不允许添加同名标题的文章，避免重复添加。
            if (bll.ExistTitle(txtTitle.Text.Trim()))
            {
                MessageBox.ShowFailTip(this, Resources.CMS.ContentTooltipTitleExist);
                return;
            }

            Maticsoft.Model.CMS.Content model = new Maticsoft.Model.CMS.Content();
            model.Title = Globals.HtmlEncode(this.txtTitle.Text);

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
            model.CreatedUserID = CurrentUser.UserID;
            model.LastEditDate = model.CreatedDate = DateTime.Now;
            model.LastEditUserID = CurrentUser.UserID;
            model.PvCount = 0;
            model.State = Globals.SafeInt(radlState.SelectedValue, 0);
            model.ClassID = ClassID;
            model.Keywords = Globals.HtmlEncode(txtKeywords.Text);
            model.Sequence = Globals.SafeInt(txtOrders.Text, 0);
            model.Description = txtContent.Text;

            model.SeoUrl = this.txtSeoUrl.Text;

            //待上传的图片名称
            string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));

            //上传图片正式地址
            string ImageFile = string.Format("/Upload/CMS/Article/{0}/", DateTime.Now.ToString("yyyyMM"));

            string ThumbImageFile = string.Format("/Upload/CMS/ArticleThumbs/{0}/", DateTime.Now.ToString("yyyyMM"));

            //上传附件正式地址
            string attachmentFile = string.Format("/Upload/CMS/Files/{0}/", DateTime.Now.ToString("yyyyMM"));
            if (!string.IsNullOrWhiteSpace(HiddenField_ICOPath.Value))
            {
                string imageUrl = Maticsoft.Web.Components.FileHelper.MoveImage(HiddenField_ICOPath.Value, ImageFile, ThumbImageFile, Maticsoft.Model.Ms.EnumHelper.AreaType.CMS);

                model.ImageUrl = imageUrl.Split('|')[0];
                model.ThumbImageUrl = imageUrl.Split('|')[1];
            }

            string fileName = string.Empty;
            if (!string.IsNullOrWhiteSpace(hfs_Attachment.Value))
            {
                string attachmenUrl = string.Format(hfs_Attachment.Value, "");
                fileName = attachmenUrl.Replace(tempFile, "");
                model.Attachment = attachmenUrl.Replace(tempFile, attachmentFile);
            }

            model.TotalComment = 0;
            model.TotalSupport = 0;
            model.TotalFav = 0;
            model.TotalShare = 0;
            int articleId = bll.Add(model);
            if (0 < articleId)
            {
                
                if (!string.IsNullOrWhiteSpace(hfs_Attachment.Value))
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
                    string saveUrl = PageSetting.GetCMSUrl(articleId);
                    if (area == "CMS")
                    {
                        requestUrl = "/Article/Details/" + articleId;
                    }
                    else
                    {
                        requestUrl = "/CMS/Article/Details/" + articleId;
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

                #region  同步微博

                string mediaIDs = "";
                mediaIDs = this.chkSina.Checked ? "3" : "";
                if (chkQQ.Checked)
                {
                    mediaIDs = mediaIDs + (String.IsNullOrWhiteSpace(mediaIDs) ? "13" : ",13");
                }
                Maticsoft.BLL.Members.UserBind bindBll = new UserBind();
                string cmsUrl = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("WeiBo_CMS_Url");
                string url = "http://" + Common.Globals.DomainFullName + String.Format(cmsUrl, articleId);
                bindBll.SendWeiBo(-1, mediaIDs, model.Title, url, model.ImageUrl);
                #endregion

                if (!string.IsNullOrWhiteSpace(ddlType.SelectedValue))
                {
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "List.aspx?classid=" + ddlType.SelectedValue);
                }
                else
                {
                    MessageBox.ShowSuccessTip(this, Resources.Site.TooltipSaveOK, "List.aspx?type=0");
                }
            }
        }

        #endregion 保存

        #region 绑定菜单树

        private void BindTree()
        {
            this.ddlType.Items.Clear();

            Maticsoft.BLL.CMS.ContentClass contentclass = new Maticsoft.BLL.CMS.ContentClass();
            DataSet ds = bllContentClass.GetTreeList("");
            if (!DataSetTools.DataSetIsNull(ds))
            {
                DataTable dt = ds.Tables[0];
                this.ddlType.Items.Clear();

                //加载树
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    DataRow[] drs = dt.Select("ParentID= " + 0);
                    foreach (DataRow r in drs)
                    {
                        if (!Globals.SafeBool(Globals.SafeString(r["AllowAddContent"], ""), false))
                        {
                            continue;
                        }
                        string nodeid = Globals.SafeString(r["ClassID"], "0");
                        string text = Globals.SafeString(r["ClassName"], "0");
                        string parentid = Globals.SafeString(r["ParentID"], "0");

                        //string permissionid = r["PermissionID"].ToString();
                        text = "╋" + text;
                        this.ddlType.Items.Add(new ListItem(text, nodeid));
                        int sonparentid = int.Parse(nodeid);
                        string blank = "├";

                        BindNode(sonparentid, dt, blank);
                    }
                }
            }
            this.ddlType.DataBind();
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
                this.ddlType.Items.Add(new ListItem(text, nodeid));
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnCancle_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(hfClassID.Value))
            {
                Response.Redirect("List.aspx" + hfClassID.Value);
            }
            else
            {
                Response.Redirect("List.aspx?type=0");
            }
        }

        #endregion 取消操作

        #region AjaxCallback
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "GetName":
                    writeText = GetName();
                    break;
            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string GetName()
        {
            JsonObject json = new JsonObject();
            string name = "";
            if (!String.IsNullOrWhiteSpace(Request.Form["Name"]))
            {
                name = Common.PinyinHelper.GetPinyin(Request.Form["Name"].Trim());
            }

            json.Put("STATUS", "OK");
            json.Put("DATA", name);
            return json.ToString();
        }

        #endregion
    }
}