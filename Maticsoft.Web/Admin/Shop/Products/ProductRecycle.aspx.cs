using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Text;
using Maticsoft.Json;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Products
{
    public partial class ProductRecycle : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 476; } } //Shop_回收站管理_列表页
        protected new int Act_AddData = 477;    //Shop_回收站管理_还原数据
        protected new int Act_DelData =478;    //Shop_回收站管理_删除数据
       
        Maticsoft.BLL.Shop.Products.ProductInfo bll = new BLL.Shop.Products.ProductInfo();
        Maticsoft.BLL.SysManage.TaskQueue taskBll = new BLL.SysManage.TaskQueue();
     
        public static List<Maticsoft.Model.SysManage.TaskQueue> TaskList;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liRevertAll.Visible = false;
                    liRevert.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    liDel.Visible = false;
                    liDelAll.Visible = false;
                }
               
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                  
                }
                BindData();
                BindSupplier();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 供应商
        /// </summary>
        private void BindSupplier()
        {
            Maticsoft.BLL.Shop.Supplier.SupplierInfo infoBll = new BLL.Shop.Supplier.SupplierInfo();
            DataSet ds = infoBll.GetList("  Status = 1 ");

            if (!DataSetTools.DataSetIsNull(ds))
            {
                this.ddlSupplier.DataSource = ds;
                this.ddlSupplier.DataTextField = "Name";
                this.ddlSupplier.DataValueField = "SupplierId";
                this.ddlSupplier.DataBind();
            }

            this.ddlSupplier.Items.Insert(0, new ListItem("平　台", "-1"));
            this.ddlSupplier.Items.Insert(0, new ListItem("全　部", string.Empty));
            this.ddlSupplier.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            ddlSupplier.SelectedIndex = 0;
        }
        #region gridView

        public void BindData()
        {
            string pName = InjectionFilter.SqlFilter(txtKeyword.Text.Trim());
            StringBuilder strWhere = new StringBuilder();
            strWhere.Append(" SaleStatus=2");
            int supplierId = Globals.SafeInt(this.ddlSupplier.SelectedValue, 0);
            if (supplierId > 0)
            {
                strWhere.AppendFormat(" and SupplierId = {0} ", supplierId);
            }
            if (!string.IsNullOrWhiteSpace(pName))
            {
                strWhere.AppendFormat(" and ProductName like '%{0}%'", pName);
            }

            this.AspNetPager1.RecordCount = bll.GetRecordCount(" SaleStatus=2");
  
            DataSet ds= bll.GetListByPage(strWhere.ToString(), "AddedDate desc", this.AspNetPager1.StartRecordIndex, this.AspNetPager1.EndRecordIndex);
            if (DataSetTools.DataSetIsNull(ds))
            {
                tableDataList.Visible = false;
            }
            else
            {
                tableDataList.Visible = true;
            }
            DataListProduct.DataSource = ds;
            DataListProduct.DataBind();

            //绑定DropDownList
            //BindDropList();
        }

        //private void BindDropList()
        //{
        //    //一级分类
        //    Maticsoft.BLL.Shop.Products.CategoryInfo bllc = new BLL.Shop.Products.CategoryInfo();
        //    DataSet dsc = bllc.GetList(" Depth = 1");

        //    if (!DataSetTools.DataSetIsNull(dsc))
        //    {
        //        this.dropCategories.DataSource = dsc;
        //        this.dropCategories.DataTextField = "Name";
        //        this.dropCategories.DataValueField = "CategoryId";
        //        this.dropCategories.DataBind();
        //    }
        //    this.dropCategories.Items.Insert(0, new ListItem(Resources.Site.PleaseSelect, "0"));
        //}

        public override void VerifyRenderingInServerForm(Control control)
        {
        }

        protected void DataList_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
            {
                HtmlGenericControl btnRevert = (HtmlGenericControl)e.Item.FindControl("btnRevert");
                btnRevert.Visible = false;
            }
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
            {
                HtmlGenericControl btnDel = (HtmlGenericControl)e.Item.FindControl("btnDel");
                btnDel.Visible = false;
            }
        }


        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void DataList_RowCommand(object source, DataListCommandEventArgs e)
        {
           
            //还原商品
            if (e.CommandName == "Revert")
            {
                long productId = Convert.ToInt64(e.CommandArgument);
                if (bll.UpdateStatus(productId, 0))
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "商品还原成功");
                    BindData();
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "商品还原失败");
                }
            }
            if (e.CommandName == "Delete")
            {
                string ID = e.CommandArgument.ToString();
                if (!String.IsNullOrWhiteSpace(ID) && Common.PageValidate.IsDecimal(ID))
                {
                    int result = 0;
                    DataSet ds = bll.DeleteProducts(ID, out result);
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        PhysicalFileInfo(ds.Tables[0]);
                    }
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "商品删除成功");
                }
               BindData();
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


        #region 批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {

            string idlist = GetSelIDlist();
            if (!string.IsNullOrWhiteSpace(idlist))
            {
                int result = 0;
                DataSet ds = bll.DeleteProducts(idlist, out result);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    PhysicalFileInfo(ds.Tables[0]);
                }
            }
            BindData();
        }
        #endregion
        #region 物理删除文件

        private void PhysicalFileInfo(DataTable dt)
        {
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                for (int n = 0; n < rowsCount; n++)
                {
                    if (dt.Rows[n]["ImageURL"] != null && dt.Rows[n]["ImageURL"].ToString() != "" && dt.Rows[n]["ImageURL"].ToString() != "/Content/themes/base/Shop/images/none.png")
                    {
                        DeletePhysicalFile(dt.Rows[n]["ImageURL"].ToString());
                    }
                    if (dt.Rows[n]["ThumbnailUrl1"] != null && dt.Rows[n]["ThumbnailUrl1"].ToString() != "" && dt.Rows[n]["ThumbnailUrl1"].ToString() != "/Content/themes/base/Shop/images/none.png")
                    {
                        DeletePhysicalFile(dt.Rows[n]["ThumbnailUrl1"].ToString());
                    }
                    if (dt.Rows[n]["ThumbnailUrl2"] != null && dt.Rows[n]["ThumbnailUrl2"].ToString() != "" && dt.Rows[n]["ThumbnailUrl1"].ToString() != "/Content/themes/base/Shop/images/none.png")
                    {
                        DeletePhysicalFile(dt.Rows[n]["ThumbnailUrl2"].ToString());
                    }
                }
            }
        }

        /// <summary>
        /// 删除物理文件
        /// </summary>
        private void DeletePhysicalFile(string path)
        {
            Maticsoft.Web.Components.FileHelper.DeleteFile(Maticsoft.Model.Ms.EnumHelper.AreaType.Shop, path);
        }

        #endregion 物理删除文件


        #region 批量还原
        protected void btnRevert_Click(object sender, EventArgs e)
        {
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0) return;
            bll.UpdateList(idlist, (int)Maticsoft.Model.Shop.Products.ProductSaleStatus.InStock);
            Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量还原成功");
            BindData();
        }
        #endregion

        
        #region 还原所有项目

        protected void btnRevertAll_Click(object sender, EventArgs e)
        {
            if (bll.RevertAll())
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "还原所有商品成功");
                BindData();
            }
            else
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "还原所有商品失败");
            }
        }
        #endregion
        #endregion
        #region AjaxCallback

        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "DeleteAll":
                    writeText = DeleteAll();
                    break;
                case "DeleteProduct":
                    DeleteProduct();
                    break;
                case "DeleteTask":
                    DeleteTask();
                    break;
                    
            }
            this.Response.Write(writeText);
            this.Response.End();
        }


        #region 清空回收站
        private string DeleteAll()
        {
            JsonObject json = new JsonObject();
            StringBuilder strWhere = new StringBuilder();

            List<Maticsoft.Model.Shop.Products.ProductInfo> list = bll.GetModelList(" SaleStatus=2");
        
            #region 循环清空回收站
            //先清空任务列表
            taskBll.DeleteTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.RecyleProduct);

            TaskList = new List<Model.SysManage.TaskQueue>();
            if (list != null && list.Count > 0)
            {
                Maticsoft.Model.SysManage.TaskQueue taskModel = null;
                int i = 1;
                foreach (Maticsoft.Model.Shop.Products.ProductInfo item  in list)
                {
                    taskModel = new Model.SysManage.TaskQueue();
                    taskModel.ID = i;
                    taskModel.TaskId =(int) item.ProductId;
                    taskModel.Status = 0;
                    taskModel.Type = (int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.RecyleProduct;
                    if (taskBll.Add(taskModel))
                    {
                        TaskList.Add(taskModel);
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            #endregion
            json.Put("STATUS", "SUCCESS");
            json.Put("DATA", list.Count);
            return json.ToString();
        }

        private void DeleteProduct()
        {
            int TaskId = Globals.SafeInt(Request.Form["TaskId"], 0);
            Maticsoft.Model.SysManage.TaskQueue item = TaskList.FirstOrDefault(c => c.ID == TaskId);
            if (item != null)
            {
                int result = 0;
                DataSet ds = bll.DeleteProducts(item.TaskId.ToString(), out result);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    PhysicalFileInfo(ds.Tables[0]);
                }
            }
        }

        private void DeleteTask()
        {
            taskBll.DeleteTask((int)Maticsoft.Model.SysManage.EnumHelper.TaskQueueType.RecyleProduct);
        }
        #endregion

        #endregion AjaxCallback

    }
}