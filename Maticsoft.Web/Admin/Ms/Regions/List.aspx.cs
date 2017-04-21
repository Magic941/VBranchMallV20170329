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
using System.Data;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Ms;

namespace Maticsoft.Web.Ms.Regions
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        protected override int Act_PageLoad { get { return 325; } } //省市区域管理_列表页
        protected new int Act_UpdateData = 328;    //省市区域管理_编辑数据
        protected new int Act_DelData = 329;    //省市区域管理_删除数据

        private Maticsoft.BLL.Ms.Regions Regbll = new Maticsoft.BLL.Ms.Regions();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                gridView.BorderColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_bordercolorlight"].ToString());
                gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[Session["Style"].ToString() + "xtable_titlebgcolor"].ToString());

                Regions1.ProvinceVisible = true;
                Regions1.CityVisible = false;
                Regions1.AreaVisible = false;
                Regions1.VisibleAll = true;
                //
            } BindData();
        }
        
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            gridView.DataBind();
        }

        #region gridView

        public void BindData()
        {
            
            gridView.DataSource = Regbll.GetList(Regions1.Province_iID);
            gridView.DataBind();
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridView.PageIndex = e.NewPageIndex;
            BindData();
        }

        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            e.Row.Attributes.Add("style", "background:#FFF");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlGenericControl linkbtnModify = (HtmlGenericControl)e.Row.FindControl("linkModify");
                LinkButton linkbtnDel = (LinkButton)e.Row.FindControl("LinkButton1");
                LinkButton LinkRegionRec = (LinkButton)e.Row.FindControl("LinkRegionRec");
                linkbtnDel.Attributes.Add("onclick", "return confirm(\"删除该数据会影响整站与地区相关的数据，确定要删除吗？\")");
                int num = (int)DataBinder.Eval(e.Row.DataItem, "Depth");
                string str = DataBinder.Eval(e.Row.DataItem, "RegionName").ToString();
                e.Row.Cells[0].CssClass = "productcag" + num.ToString();
                if (1 != num)
                {
                    System.Web.UI.HtmlControls.HtmlGenericControl control = e.Row.FindControl("spShowImage") as System.Web.UI.HtmlControls.HtmlGenericControl;
                    control.Visible = false;
                }
                if (num == 2)
                {
                    LinkRegionRec.Visible = true;
                }
                if (num == 3)
                {
                    linkbtnDel.Visible = true;
                }
                Label label = e.Row.FindControl("lblRegionName") as Label;

                label.Text = str;


                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_DelData)) && GetPermidByActID(Act_DelData) != -1)
                {
                    linkbtnDel.Visible = false;

                }
              
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
                {
                    linkbtnModify.Visible = false;

                }
               
            }
        }

        protected void gridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int ID = (int)gridView.DataKeys[e.RowIndex].Value;
            Regbll.Delete(ID);
            BindData();
        }


        protected void gridView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "RegionRec")
            {
                int regionId = Common.Globals.SafeInt(e.CommandArgument.ToString(),0);
                Maticsoft.BLL.Ms.RegionRec recBll=new RegionRec();
               int recId= recBll.AddEx(regionId, 0);
                if (recId > 0)
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "推荐成功！");
                }
                else
                {
                    Maticsoft.Common.MessageBox.ShowSuccessTip(this, "推荐失败！");
                }
            }
        }
        #endregion

    }
}
