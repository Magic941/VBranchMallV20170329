using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Ms;
using Maticsoft.Common;
using Maticsoft.Model.Shop.ActivityManage;
using Maticsoft.BLL.Shop.ActivityManage;

namespace Maticsoft.Web.Admin.Shop.ActivityManage
{
    public partial class AddFree : PageBaseAdmin
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
                ddl_area.Items.Insert(0, new ListItem("请选择", "0"));
                Bind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int regionId = int.Parse(ddl_area.SelectedValue);
                Maticsoft.Model.Shop.Shipping.Shop_freefreight freefreight = new Model.Shop.Shipping.Shop_freefreight();
                freefreight.RegionId = regionId;
                //freefreight.createdate = DateTime.Now;
                freefreight.createrid = CurrentUser.UserID;
               // freefreight.FreeType = 1;
                if (_freeBll.ExistsRegion(int.Parse(freefreight.RegionId.ToString())))
                {
                    MessageBox.ShowFailTip(this, "该地区已经设置过免邮值。");
                }
                else
                {
                    if (_freeBll.Add(freefreight) > 0)
                    {
                        MessageBox.ShowSuccessTip(this, "添加成功。");
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

        //保存数据
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            Maticsoft.Model.Shop.Shipping.Shop_freefreight sfModel = new Model.Shop.Shipping.Shop_freefreight();
            sfModel.StartDate = DateTime.Parse(this.txtAMStartdate.Text); //开始时间
            sfModel.EndDate = DateTime.Parse(this.txtAMEnddate.Text);  //结束时间

            sfModel.Unit = Common.Globals.SafeInt(this.AMUnit.SelectedValue, 0); //规则单位 //是否包邮不用存放数据库
            sfModel.UnitValue = Convert.ToDecimal(UnitValue.Text);  // 单位数值
            sfModel.FreeType = Common.Globals.SafeInt(this.AMApplyStyles.SelectedValue, 0); //应用方式 0 单个商品，1 全场商品 ，2 单个商家
            sfModel.FStatus = Common.Globals.SafeInt(this.AMStatus.SelectedValue, 0); // 是否启用   
            sfModel.createrid = CurrentUser.UserID; //用户
            sfModel.createdate = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));  //创建时间

            string str = "";

            foreach (GridViewRow gvr in gdv_Shipping.Rows)
            {
                str += gvr.Cells[0].Text+",";
            }
            sfModel.AllRegion = str;   //获取区域ID

            

            string hfItems = this.hfItems.Value;
            if (String.IsNullOrWhiteSpace(hfItems))
            {
                MessageBox.ShowFailTip(this, "请填写活动优惠规则项");
                return;
            }
            //
            int sfId = _freeBll.Add(sfModel);
            if (sfId > 0)
            {
                MessageBox.ShowSuccessTipScript(this, "操作成功！", "window.parent.location.reload();");
            }
        }
    }
}