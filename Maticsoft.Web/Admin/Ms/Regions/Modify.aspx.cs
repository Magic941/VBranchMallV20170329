/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
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
using Maticsoft.Common;
using System.Data;
using System.Web.UI.WebControls;
namespace Maticsoft.Web.Ms.Regions
{
    public partial class Modify : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 327; } } //省市区域管理_编辑页
        Maticsoft.BLL.Ms.Regions bll = new Maticsoft.BLL.Ms.Regions();
        protected void Page_Load(object sender, EventArgs e)
        {
            Regions1.ProvinceVisible = true;
            Regions1.CityVisible = true;
            Regions1.AreaVisible = true;
            Regions1.VisibleAll = true;
            if (!Page.IsPostBack)
            {
                if (RegionId>0)
                {
                    ShowInfo(RegionId);
                }
            }
        }

        public int RegionId
        {
            get
            {
                int regionId = -1;
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    regionId = Convert.ToInt32(Request.Params["id"]);
                }
                return regionId;
            }
        }

        private void ShowInfo(int RegionId)
        {
            Maticsoft.Model.Ms.Regions model = bll.GetModel(RegionId);
            if (model.Depth == 3)
            {
                this.Regions1.Area_iID = model.RegionId;
                Regions1.AreaVisible = false;
            }
            else if (model.Depth == 2)
            {
                this.Regions1.City_iID = model.RegionId;
                Regions1.AreaVisible = false;
                Regions1.CityVisible = false;
            }
            else
            {
                //this.Regions1.Province_iID = model.RegionId;
            }
            this.txtRegionName.Text = model.RegionName;
            this.txtSpellShort.Text = model.SpellShort;
            this.txtDisplaySequence.Text = model.DisplaySequence.ToString();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            this.btnSave.Enabled = false;
            this.btnCancle.Enabled = false;
            if (string.IsNullOrEmpty(this.txtRegionName.Text.Trim()))
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowFailTip(this, "地区名称不能为空！");
                return;
            }
            Maticsoft.Model.Ms.Regions model = bll.GetModel(RegionId);

            model.RegionName = this.txtRegionName.Text;

            model.SpellShort = this.txtSpellShort.Text;

            model.DisplaySequence = Globals.SafeInt(this.txtDisplaySequence.Text, 1);

            if (bll.Update(model))
            {
                this.btnSave.Enabled = false;
                this.btnCancle.Enabled = false;
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, "保存成功！", "Modify.aspx");
            }
            else
            {
                this.btnSave.Enabled = true;
                this.btnCancle.Enabled = true;
                MessageBox.ShowSuccessTip(this, "编辑失败！");
            }
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }
    }
}
