using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.SNS;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.Products
{
    public partial class GetTaoList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 596; } } //SNS_淘宝商品采集页

        Maticsoft.BLL.SNS.Products bll = new BLL.SNS.Products();
        Maticsoft.BLL.SNS.UserAlbums albumBll = new UserAlbums();
        protected string UploadPath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Maticsoft.BLL.Members.Users userBll = new BLL.Members.Users();
                int userId = userBll.GetDefaultUserId();
                this.txtUserId.Text = userId.ToString();

                List<ViewModel.SNS.AlbumIndex> AlbumList = albumBll.GetListByUserId(userId);
                this.ddlAlbumList.DataSource = AlbumList;

                this.ddlAlbumList.DataTextField = "AlbumName";
                this.ddlAlbumList.DataValueField = "AlbumID";
                this.ddlAlbumList.DataBind();
                this.ddlAlbumList.Items.Insert(0, new ListItem("--请选择--", ""));

                // BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
        }


        #region gridView

        public override void VerifyRenderingInServerForm(Control control)
        {
        }


        #endregion 物理删除文件

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            if (Session["ProductList"] != null)
            {
                int pageIndex = this.AspNetPager1.CurrentPageIndex;
                int pageSize = this.AspNetPager1.PageSize;
                List<TaoBao.Domain.Product> ProductList = Session["ProductList"] as List<TaoBao.Domain.Product>;
                DataListProduct.DataSource = ProductList.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                DataListProduct.DataBind();
            }
        }
        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < DataListProduct.Items.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)DataListProduct.Items[i].FindControl("ckProduct");
                HiddenField hfPhotoId = (HiddenField)DataListProduct.Items[i].FindControl("hfProduct");
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (hfPhotoId.Value != null)
                    {
                        idlist += hfPhotoId.Value + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        #region 按钮处理

        protected void btnImport_Click(object sender, EventArgs e)
        {
            int TaoCateId = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            if (String.IsNullOrWhiteSpace(this.txtUserId.Text) || !Common.PageValidate.IsNumber(this.txtUserId.Text))
            {
                MessageBox.ShowFailTip(this, "请输入正确的用户ID");
                return;
            }
            int userId = Common.Globals.SafeInt(this.txtUserId.Text, 0);
            Maticsoft.BLL.Members.Users userBll = new BLL.Members.Users();
            Maticsoft.Model.Members.Users user = userBll.GetModel(userId);
            if (user == null)
            {
                MessageBox.ShowFailTip(this, "该用户ID不存在");
                return;
            }
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "选择您要导入的商品");
                return;
            }
            int albumId = Common.Globals.SafeInt(ddlAlbumList.SelectedValue, 0);
            if (albumId == 0)
            {
                MessageBox.ShowFailTip(this, "请选择需要导入数据的专辑");
                return;
            }
            var arryId = idlist.Split(',');
            if (Session["ProductList"] != null)
            {
                int pageIndex = this.AspNetPager1.CurrentPageIndex;
                int pageSize = this.AspNetPager1.PageSize;
                List<TaoBao.Domain.Product> ProductList = Session["ProductList"] as List<TaoBao.Domain.Product>;
                int count = 0;//bll.ImportData(user.UserID, albumId, TaoCateId, ProductList.Where(c => arryId.Contains(c.ProductId.ToString())).ToList());
                ProductList.RemoveAll(c => arryId.Contains(c.ProductId.ToString()));
                this.AspNetPager1.RecordCount = ProductList.Count;
                if ((pageIndex - 1) * pageSize >= ProductList.Count)
                {
                    this.AspNetPager1.CurrentPageIndex = pageIndex - 1;
                    DataListProduct.DataSource = ProductList.Skip((pageIndex - 2) * pageSize).Take(pageSize);
                }
                else
                {
                    DataListProduct.DataSource = ProductList.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                DataListProduct.DataBind();
                Session["ProductList"] = ProductList;
                MessageBox.ShowSuccessTip(this, "成功导入【" + count + "】条数据");
            }

        }

        protected void btnImportAll_Click(object sender, EventArgs e)
        {
            int TaoCateId = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            if (String.IsNullOrWhiteSpace(this.txtUserId.Text) || !Common.PageValidate.IsNumber(this.txtUserId.Text))
            {
                MessageBox.ShowFailTip(this, "请输入正确的用户ID");
                return;
            }
            int albumId = Common.Globals.SafeInt(ddlAlbumList.SelectedValue, 0);
            if (albumId == 0)
            {
                MessageBox.ShowFailTip(this, "请选择需要导入数据的专辑");
                return;
            }
            int userId = Common.Globals.SafeInt(this.txtUserId.Text, 0);

            if (Session["ProductList"] != null)
            {
                List<TaoBao.Domain.Product> list = Session["ProductList"] as List<TaoBao.Domain.Product>;
                int count = 0; //bll.ImportData(userId, albumId, TaoCateId, list);
                this.AspNetPager1.RecordCount = 0;
                DataListProduct.DataSource = null;
                DataListProduct.DataBind();
                Session["ProductList"] = null;
                MessageBox.ShowSuccessTip(this, "成功导入【" + count + "】条数据");
            }

        }
        #endregion



        protected void btnGetData_Click(object sender, EventArgs e)
        {
            int pageSize = Common.Globals.SafeInt(this.TopPageSize.Text, 20);
            int pageNo = Common.Globals.SafeInt(this.TopPageNo.Text, 1);
            this.AspNetPager1.PageSize = pageSize;

            int TaoCateId = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            if (TaoCateId == 0)
            {
                MessageBox.ShowFailTip(this, "请选择淘宝分类");
                return;
            }
            string keyword = this.TopKeyWord.Text;
            int typeId = Common.Globals.SafeInt(ddlMarketType.SelectedValue, 0);
            List<TaoBao.Domain.Product> ProductList = bll.GetTaoDataList(TaoCateId, keyword, pageNo, pageSize, typeId);
            if (ProductList != null && ProductList.Count > 0)
            {
                this.AspNetPager1.RecordCount = ProductList.Count;
                DataListProduct.DataSource = ProductList.Take(pageSize);
                DataListProduct.DataBind();
                Session["ProductList"] = ProductList;
            }
            else
            {
                MessageBox.ShowFailTip(this, "获取数据失败，请检查淘宝客设置是否正确，并确保申请的淘宝Key具有获取淘宝客数据权限。");
                return;
            }
        }

        protected void Text_Change(object sender, System.EventArgs e)
        {
            int userId = Common.Globals.SafeInt(this.txtUserId.Text, 0);
            List<ViewModel.SNS.AlbumIndex> AlbumList = albumBll.GetListByUserId(userId);
            this.ddlAlbumList.DataSource = AlbumList;

            this.ddlAlbumList.DataTextField = "AlbumName";
            this.ddlAlbumList.DataValueField = "AlbumID";
            this.ddlAlbumList.DataBind();
            this.ddlAlbumList.Items.Insert(0, new ListItem("--请选择--", ""));
        }

    }
}