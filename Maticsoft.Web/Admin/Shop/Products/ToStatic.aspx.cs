using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Maticsoft.Common;
using System.Collections;
using Maticsoft.Web.Components.Setting.CMS;
using Maticsoft.Model.SysManage;


namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class ToStatic : System.Web.UI.Page
    {
        Maticsoft.BLL.SysManage.TaskQueue taskBll = new BLL.SysManage.TaskQueue();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindTree();
                this.txtTaskCount.Value = taskBll.GetRecordCount(" type=" + (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopProduct).ToString();

                this.txtTaskReCount.Text = taskBll.GetRecordCount(" type=" + (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopProduct + " and Status=0").ToString();
                Maticsoft.Model.SysManage.TaskQueue taskModel = taskBll.GetLastModel((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.ShopProduct);
                if (taskModel != null)
                {
                    this.txtTaskDate.Text = taskModel.RunDate.Value.ToString("yyyy-MM-dd");
                    this.txtTaskId.Text = (taskModel.ID + 1).ToString();
                }
                string value = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ShopProductStatic");
                this.radlStatus.SelectedValue = value;
                this.txtIsStatic.Value = value;
                this.txtShopRoot.Text = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ShopStaticRoot");
                this.ddlClassUrl.SelectedValue = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_Static_TypeRule");
                this.ddlShopUrl.SelectedValue = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_Static_NameRule");
            }
        }
        #region 绑定菜单树
        private void BindTree()
        {
            this.dropParentID.Items.Clear();
            this.dropParentID.Items.Add(new ListItem(Resources.Site.All, ""));
            Maticsoft.BLL.Shop.Products.CategoryInfo bllCategoryInfo = new BLL.Shop.Products.CategoryInfo();
            DataSet ds = bllCategoryInfo.GetList("");
            if (!DataSetTools.DataSetIsNull(ds))
            {
                DataTable dt = ds.Tables[0];
                if (!DataTableTools.DataTableIsNull(dt))
                {
                    DataRow[] dr = dt.Select("ParentCategoryId= " + 0);
                    foreach (DataRow item in dr)
                    {
                        string nodeid = item["CategoryId"].ToString();
                        string text = item["Name"].ToString();
                        string parentid = item["ParentCategoryId"].ToString();
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
            DataRow[] dr = dt.Select("ParentCategoryId= " + parentid);
            foreach (DataRow item in dr)
            {
                string nodeid = item["CategoryId"].ToString();
                string text = item["Name"].ToString();
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
            if (Maticsoft.BLL.SysManage.ConfigSystem.Exists("ShopProductStatic"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("ShopProductStatic", value, "0表示不启动静态化，1表示启用静态化，2表示启用伪静态");
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("ShopProductStatic", value, "0表示不启动静态化，1表示启用静态化，2表示启用伪静态");
            }
            Response.Redirect("ToStatic.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
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
            string[] keywords = new string[] { "/cms/", "/sns/", "/shop/", "/area/", "/tao/", "/com/", "/handlers/", "/content/", "/upload/", "/uploadfolder/","/Admin/","/bin/","/css/","/obj/" };
            string root = this.txtShopRoot.Text.Trim();
            if (root.Trim().ToString().Length == 0)
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "请填写存放目录文件夹");
                return;
            }
            if (PageSetting.IsHasKeyword(root, keywords))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "填的静态化根目录中含有网站关键字，请重新填写");
                return;
            }
            if (!BLL.SysManage.ConfigSystem.Exists("ShopStaticRoot"))
            {
                BLL.SysManage.ConfigSystem.Add("ShopStaticRoot", root, "商品静态化根目录地址，为空就默认为根目录下", Model.SysManage.ApplicationKeyType.Shop);
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("ShopStaticRoot", root, "商品静态化根目录地址，为空就默认为根目录下");
            }
            if (!Maticsoft.BLL.SysManage.ConfigSystem.Exists("Shop_Static_TypeRule"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("Shop_Static_TypeRule", this.ddlClassUrl.SelectedValue, "商品静态化类别URL规则,0:表示使用ID,1:表示使用分类名称拼音,2表示使用自定义优化", Model.SysManage.ApplicationKeyType.Shop);
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("Shop_Static_TypeRule", this.ddlClassUrl.SelectedValue, "商品静态化类别URL规则,0:表示使用ID,1:表示使用分类名称拼音,2表示使用自定义优化");
            }
            if (!Maticsoft.BLL.SysManage.ConfigSystem.Exists("Shop_Static_NameRule"))
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Add("Shop_Static_NameRule", this.ddlShopUrl.SelectedValue, "商品静态化商品文件名URL规则，0表示使用ID,1:表示使用商品标题拼音，2表示使用自定义优化", ApplicationKeyType.Shop);
            }
            else
            {
                Maticsoft.BLL.SysManage.ConfigSystem.Update("Shop_Static_NameRule", this.ddlShopUrl.SelectedValue, "商品静态化商品文件名URL规则，0表示使用ID,1:表示使用商品标题拼音，2表示使用自定义优化");
            }
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "设置成功", "ToStatic.aspx");
        }
    }
}