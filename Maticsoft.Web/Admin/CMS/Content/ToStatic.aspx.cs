
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using Maticsoft.Web.Admin.SysManage;
using Maticsoft.BLL.SysManage;
using System.Collections;
using Maticsoft.Web.Components.Setting.CMS;

namespace Maticsoft.Web.Admin.CMS.Content
{
    public partial class ToStatic : PageBaseAdmin
    {
        Maticsoft.BLL.CMS.Content bll = new BLL.CMS.Content();
        Maticsoft.BLL.SysManage.TaskQueue taskBll = new BLL.SysManage.TaskQueue();
        protected override int Act_PageLoad { get { return 234; } } //CMS_文章静态化页
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindTree();
                this.txtTaskCount.Value = taskBll.GetRecordCount(" type=" + (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.Article).ToString();
                this.txtTaskReCount.Text = taskBll.GetRecordCount(" type=" + (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.Article + " and Status=0").ToString();
                Maticsoft.Model.SysManage.TaskQueue taskModel = taskBll.GetLastModel((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.Article);
                if (taskModel != null)
                {
                    this.txtTaskDate.Text = taskModel.RunDate.Value.ToString("yyyy-MM-dd");
                    this.txtTaskId.Text = (taskModel.ID + 1).ToString();
                }
               string value= Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ArticleIsStatic");
               this.radlStatus.SelectedValue = value;
               this.txtIsStatic.Value = value;
               this.txtCMSRoot.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ArticleStaticRoot");
               this.ddlClassUrl.SelectedValue = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("CMS_Static_ClassRule");
               this.ddlArticleUrl.SelectedValue = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("CMS_Static_ContentRule");
            }
        }

        #region 绑定菜单树
        private void BindTree()
        {
            this.dropParentID.Items.Clear();
            this.dropParentID.Items.Add(new ListItem(Resources.Site.All, ""));

            Maticsoft.BLL.CMS.ContentClass bllContentClass = new Maticsoft.BLL.CMS.ContentClass();
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
                        string nodeid = r["ClassID"].ToString();
                        string text = r["ClassName"].ToString();
                        string parentid = r["ParentID"].ToString();
                        //string permissionid = r["PermissionID"].ToString();
                        text = "╋" + text;
                        this.dropParentID.Items.Add(new ListItem(text, nodeid));
                        int sonparentid = int.Parse(nodeid);
                        string blank = "├";
                        BindNode(sonparentid, dt, blank);
                    }
                }
            }
            this.dropParentID.DataBind();

        }
        private void BindNode(int parentid, DataTable dt, string blank)
        {
            DataRow[] drs = dt.Select("ParentID= " + parentid);

            foreach (DataRow r in drs)
            {
                string nodeid = r["ClassID"].ToString();
                string text = r["ClassName"].ToString();
                //string permissionid = r["PermissionID"].ToString();
                text = blank + "『" + text + "』";
                this.dropParentID.Items.Add(new ListItem(text, nodeid));
                int sonparentid = int.Parse(nodeid);
                string blank2 = blank + "─";
                BindNode(sonparentid, dt, blank2);
            }
        }


        #endregion

        protected void radlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            IDictionaryEnumerator de = HttpContext.Current.Cache.GetEnumerator();
            ArrayList listCache = new ArrayList();
            while (de.MoveNext())
            {
                listCache.Add(de.Key.ToString());
            }
            foreach (string key in listCache)
            {
                HttpContext.Current.Cache.Remove(key);
            }
            string value = this.radlStatus.SelectedValue;
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("ArticleIsStatic"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("ArticleIsStatic", value, "CMS 文章是否静态化，true表示静态化，false表示不需要静态化");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("ArticleIsStatic", value, "CMS 文章是否静态化，true表示静态化，false表示不需要静态化");
            }
            Response.Redirect("ToStatic.aspx");
        }

        protected void btnIndex_Click(object sender, EventArgs e)
        {
            string requestUrl = "/Home/Index?type=1";
            if (Maticsoft.BLL.CMS.GenerateHtml.HttpToStatic(requestUrl, "/index.html"))
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "首页静态生成成功", "ToStatic.aspx");
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "首页静态生成失败，请重试", "ToStatic.aspx");
            }
        }


        protected void btnRuleSet_Click(object sender, EventArgs e)
        {
            //清空一下缓存
            IDictionaryEnumerator de = Cache.GetEnumerator();
            ArrayList listCache = new ArrayList();
            while (de.MoveNext())
            {
                listCache.Add(de.Key.ToString());
            }
            foreach (string key in listCache)
            {
                Cache.Remove(key);
            }
            string[] keywords = new string[]
                {"/cms/", "/sns/", "/shop/", "/area/", "/tao/", "/com/", "/handlers/", "/content/", "/upload/", "/uploadfolder/"};
            string root = this.txtCMSRoot.Text.Trim();
            if (PageSetting.IsHasKeyword(root, keywords))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "填的静态化根目录中含有网站关键字，请重新填写");
                return;
            }
            if (!BLL.SysManage.ConfigSystem.Exists("ArticleStaticRoot"))
           {
               BLL.SysManage.ConfigSystem.Add("ArticleStaticRoot", root, "文章静态化根目录地址，为空就默认为根目录下", ApplicationKeyType.CMS);
           }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("ArticleStaticRoot", root, "文章静态化根目录地址，为空就默认为根目录下");
            }
            if (!Maticsoft.BLL.SysManage.ConfigSystem.Exists("CMS_Static_ClassRule"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("CMS_Static_ClassRule", this.ddlClassUrl.SelectedValue, "文章静态化栏目URL规则，0：表示使用ID，1：表示使用栏目名称拼音，2：表示使用自定义优化", ApplicationKeyType.CMS);
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("CMS_Static_ClassRule", this.ddlClassUrl.SelectedValue, "文章静态化栏目URL规则，0：表示使用ID，1：表示使用栏目名称拼音，2：表示使用自定义优化");
            }
            if (!Maticsoft.BLL.SysManage.ConfigSystem.Exists("CMS_Static_ContentRule"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("CMS_Static_ContentRule", this.ddlArticleUrl.SelectedValue, "文章静态化内容URL规则，0：表示使用ID，1：表示使用文章名称拼音，2：表示使用自定义优化", ApplicationKeyType.CMS);
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("CMS_Static_ContentRule", this.ddlArticleUrl.SelectedValue, "文章静态化内容URL规则，0：表示使用ID，1：表示使用文章名称拼音，2：表示使用自定义优化");
            }
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "设置成功", "ToStatic.aspx");
        }
        
    }
}
