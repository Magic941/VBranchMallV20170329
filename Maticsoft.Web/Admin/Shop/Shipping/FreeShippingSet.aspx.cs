using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Ms;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.Shop.Shipping
{
    public partial class FreeShippingSet : PageBaseAdmin
    {
        protected readonly Regions _regionsBLL = new Regions();
        protected readonly Maticsoft.BLL.Shop.Shipping.Shop_freefreight _freeBll = new BLL.Shop.Shipping.Shop_freefreight();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //加载区域值
                List<Maticsoft.Model.Ms.Regions> regionList = _regionsBLL.GetModelList(" AreaId IS NOT NULL");
                ddl_area.DataSource = regionList;
                ddl_area.DataValueField = "RegionId";
                ddl_area.DataTextField = "RegionName";
                ddl_area.DataBind();
                ddl_area.Items.Insert(0, new ListItem("全国", "0"));
                Bind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int regionId = int.Parse(ddl_area.SelectedValue);
                decimal needed = decimal.Parse(txt_Needed.Text.Trim());
                Maticsoft.Model.Shop.Shipping.Shop_freefreight freefreight = new Model.Shop.Shipping.Shop_freefreight();
                freefreight.RegionId = regionId;
                freefreight.totalmoney = needed;
                freefreight.createdate = DateTime.Now;
                freefreight.createrid = CurrentUser.UserID;
                freefreight.FreeType = 1;
                if (_freeBll.ExistsRegion(int.Parse(freefreight.RegionId.ToString())))
                {
                    MessageBox.ShowFailTip(this, "该地区已经设置过免邮值。");
                }
                else
                {
                    if (_freeBll.Add(freefreight) > 0)
                    {
                        MessageBox.ShowFailTip(this, "添加成功。");
                        Bind();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.ShowFailTip(this, "添加失败。");
            }
        }

        protected void Bind()
        {
            List<Maticsoft.Model.Shop.Shipping.Shop_freefreight> FreeList = _freeBll.GetModelList();
            gdv_Shipping.DataSource = FreeList;
            gdv_Shipping.DataBind();
        }

        protected void btn_delete_Click(object sender, EventArgs e)
        {
            LinkButton lb = sender as LinkButton;
            if (lb.CommandArgument!="")
            {
                try
                {
                    if (_freeBll.Delete(int.Parse(lb.CommandArgument.ToString())))
                    {
                        MessageBox.ShowFailTip(this, "删除成功。");
                        Bind();
                    }
                    else {
                        MessageBox.ShowFailTip(this, "删除失败。");
                    }
                }
                catch (Exception)
                {
                    MessageBox.ShowFailTip(this, "程序出错，请联系技术人员。");
                }
            }
        }

        protected void gdv_Shipping_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变 
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            }
 
        }
    }
}