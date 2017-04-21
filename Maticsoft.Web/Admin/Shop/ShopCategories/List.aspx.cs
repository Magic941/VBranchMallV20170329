/**
* List.cs
*
* 功 能： [N/A]
* 类 名： List.cs
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using Maticsoft.Json;
using Maticsoft.Model.Shop.Products;

namespace Maticsoft.Web.Admin.Shop.ShopCategories
{
    public partial class List : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 524; } } //shop_商品分类管理_列表页
        protected new int Act_AddData = 525;    //shop_商品分类管理_添加数据
        protected new int Act_UpdateData = 526;    //hop_商品分类管理_编辑数据
        protected new int Act_DelData = 527;    //shop_商品分类管理_删除数据
        //int Act_ShowInvalid = -1; //查看失效数据行为

        private Maticsoft.BLL.Shop.Products.CategoryInfo bll = new Maticsoft.BLL.Shop.Products.CategoryInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.Request.Form["Callback"]) && (this.Request.Form["Callback"] == "true"))
            {
                this.Controls.Clear();
                this.DoCallback();
            }
            if (!Page.IsPostBack)
            {
                if (Session["Style"] != null && Session["Style"].ToString() != "")
                {
                    string style = Session["Style"] + "xtable_bordercolorlight";
                    if (Application[style] != null && Application[style].ToString() != "")
                    {
                        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                    }
                }

                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                {
                    liAdd.Visible = false;
                }
                List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList =
             Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();
                var RootList = cateList.Where(c => c.Depth == 1).OrderBy(c => c.DisplaySequence).ToList();
                this.ddCateList.DataSource = RootList;
                this.ddCateList.DataTextField = "Name";
                this.ddCateList.DataValueField = "CategoryId";
                this.ddCateList.DataBind();
                this.ddCateList.Items.Insert(0, new ListItem("全部", "0"));
                BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }
        /// <summary>
        /// 批量修改分类顺序值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpdateSeq_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                int cateId = Common.Globals.SafeInt(gridView.DataKeys[i].Value.ToString(), 0);
                var item = (TextBox)this.gridView.Rows[i].FindControl("TextBox1");
                int seq = Common.Globals.SafeInt(item.Text, 0);
                if (cateId > 0 && seq > 0)
                {
                    bll.UpdateSeqByCid(seq, cateId);
                }
            }
            //清空缓存
            Cache.Remove("GetAllCateList-CateList");
        }


        #region gridView

        public void BindData()
        {
            #region

            //if (!Context.User.Identity.IsAuthenticated)
            //{
            //    return;
            //}
            //AccountsPrincipal user = new AccountsPrincipal(Context.User.Identity.Name);
            //if (user.HasPermissionID(PermId_Modify))
            //{
            //    gridView.Columns[6].Visible = true;
            //}
            //if (user.HasPermissionID(PermId_Delete))
            //{
            //    gridView.Columns[7].Visible = true;
            //}

            #endregion gridView

            List<Maticsoft.Model.Shop.Products.CategoryInfo> cateList =
                Maticsoft.BLL.Shop.Products.CategoryInfo.GetAllCateList();


            //对商品数据进行排序
            List<Maticsoft.Model.Shop.Products.CategoryInfo> orderList = new List<CategoryInfo>();
            int cateId = Common.Globals.SafeInt(ddCateList.SelectedValue, 0);
            var RootList = new List<CategoryInfo>();
            if (cateId == 0)
            {
                RootList = cateList.Where(c => c.Depth == 1).OrderBy(c => c.DisplaySequence).ToList();
            }
            else
            {
                RootList = cateList.Where(c => c.CategoryId == cateId).ToList();
            }

            foreach (var item in RootList)
            {
                orderList = CateOrder(item, cateList, orderList);
            }
            //ds = bll.GetList(strWhere.ToString(), UserPrincipal.PermissionsID, UserPrincipal.PermissionsID.Contains(GetPermidByActID(Act_ShowInvalid)));
            gridView.DataSource = orderList;
            gridView.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            gridView.DataBind();
        }
        private List<Maticsoft.Model.Shop.Products.CategoryInfo> CateOrder(Maticsoft.Model.Shop.Products.CategoryInfo model, List<Maticsoft.Model.Shop.Products.CategoryInfo> AllCateList, List<Maticsoft.Model.Shop.Products.CategoryInfo> orderList)
        {
            orderList.Add(model);
            if (model.HasChildren)
            {
                var list = AllCateList.Where(c => c.ParentCategoryId == model.CategoryId).OrderBy(c => c.DisplaySequence);
                foreach (var item in list)
                {
                    CateOrder(item, AllCateList, orderList);
                }
            }
            else
            {
                return orderList;
            }
            return orderList;
        }

        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = ((GridViewRow)((Control)e.CommandSource).NamingContainer).RowIndex;
            int categoryId = (int)this.gridView.DataKeys[rowIndex].Value;
            if (e.CommandName == "Fall")
            {
                bll.SwapCategorySequence(categoryId, Model.Shop.Products.SwapSequenceIndex.Up);
            }
            if (e.CommandName == "Rise")
            {
                bll.SwapCategorySequence(categoryId, Model.Shop.Products.SwapSequenceIndex.Down);
            }
            if (e.CommandName == "UpdateSeq")
            {
                var item = (TextBox)this.gridView.Rows[rowIndex].FindControl("TextBox1");
                int seq = Common.Globals.SafeInt(item.Text, 0);
                bll.UpdateSeqByCid(seq, categoryId);
            }
            //清空缓存
            Cache.Remove("GetAllCateList-CateList");
            BindData();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    LinkButton delbtn = (LinkButton)e.Row.FindControl("linkDel");
                    delbtn.Visible = false;
                }
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    HtmlGenericControl updatebtn = (HtmlGenericControl)e.Row.FindControl("lbtnModify");
                    updatebtn.Visible = false;
                }

                int num = (int)DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "Name").ToString();
                e.Row.Cells[0].CssClass = "productcag" + num.ToString();
                if (num != 1)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl control = e.Row.FindControl("spShowImage") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    control.Visible = false;
                }
                Label label = e.Row.FindControl("lblName") as Label;
                label.Text = str;

                //object obj1 = DataBinder.Eval(e.Row.DataItem, "Levels");
                //if ((obj1 != null) && ((obj1.ToString() != "")))
                //{
                //    e.Row.Cells[4].Text = obj1.ToString() == "0" ? "Private" : "Shared";
                //}
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
          
            BindData();

        }

        private void PhysicalFileInfo(DataTable dt)
        {
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                for (int n = 0; n < rowsCount; n++)
                {
                    if (dt.Rows[n]["ImageUrl"] != null && dt.Rows[n]["ImageUrl"].ToString() != "" &&
                        dt.Rows[n]["ImageUrl"].ToString() != "/Content/themes/base/Shop/images/none.png")
                    {
                        DeletePhysicalFile(dt.Rows[n]["ImageUrl"].ToString());
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

        private bool RegURL(string path)
        {
            Regex regex = new Regex("^[a-zA-z]+://(//w+(-//w+)*)(//.(//w+(-//w+)*))*(//?//S*)?$");
            Match match = regex.Match(path);
            return match.Success;
        }

        #endregion
        #region Ajax方法
        private void DoCallback()
        {
            string action = this.Request.Form["Action"];
            this.Response.Clear();
            this.Response.ContentType = "application/json";
            string writeText = string.Empty;

            switch (action)
            {
                case "UpdateSeqNum":

                    writeText = UpdateSeqNum();
                    break;
                case "Delete":
                    writeText = Delete();
                    break;
                default:
                    writeText = UpdateSeqNum();
                    break;

            }
            this.Response.Write(writeText);
            this.Response.End();
        }

        private string UpdateSeqNum()
        {
            JsonObject json = new JsonObject();
            int CategoryId = Common.Globals.SafeInt(this.Request.Form["CategoryId"], 0);
            int UpdateValue = Common.Globals.SafeInt(this.Request.Form["UpdateValue"], 0);
            if (CategoryId == 0 || UpdateValue == 0)
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {

                if (bll.UpdateSeqByCid(UpdateValue, CategoryId))
                {
                    Cache.Remove("GetAllCateList-CateList");
                    json.Put("STATUS", "SUCCESS");
                }
                else
                {
                    json.Put("STATUS", "FAILED");
                }
            }
            return json.ToString();
        }

        private string Delete()
        {
            JsonObject json = new JsonObject();
            int CategoryId = Common.Globals.SafeInt(this.Request.Form["CategoryId"], 0);
            int result;
            if (CategoryId == 0 )
            {
                json.Put("STATUS", "FAILED");
            }
            else
            {
                Maticsoft.BLL.Shop.Products.ProductCategories productCateBll = new BLL.Shop.Products.ProductCategories();
                int count = productCateBll.GetCount(CategoryId);
                if (count > 0)
                {
                    json.Put("STATUS", "NO");
                    return json.ToString();
                }
                DataSet ds = bll.DeleteCategory(CategoryId, out result);
                if (ds != null)
                {
                    //物理删除文件
                    PhysicalFileInfo(ds.Tables[0]);
                }
           
                //清空缓存
                Cache.Remove("GetAllCateList-CateList");
                json.Put("STATUS", "SUCCESS");
              
            }
            return json.ToString();
        }
        #endregion
    }
}