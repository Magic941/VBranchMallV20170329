/**
* Add.cs
*
* 功 能： [N/A]
* 类 名： Add.cs
*
* Ver        变更日期                           负责人          变更内容
* ───────────────────────────────────
* V0.01   2012年6月13日 20:46:27    Rock            创建
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using Maticsoft.Common;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Maticsoft.BLL.SysManage;

namespace Maticsoft.Web.Admin.Shop.ProductType
{
    public partial class ModifySpec : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 513; } } //Shop_规格值_编辑页
        Maticsoft.BLL.Shop.Products.AttributeInfo bll = new Maticsoft.BLL.Shop.Products.AttributeInfo();
        BLL.Shop.Products.AttributeValue ValueBll = new BLL.Shop.Products.AttributeValue();

        #region GetUrlParameters
        /// <summary>
        /// 类别ID
        /// </summary>
        private int ProductTypeId
        {
            get
            {
                int producrTypeId = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["tid"]))
                {
                    producrTypeId = Globals.SafeInt(Request.Params["tid"], 0);
                }
                return producrTypeId;
            }
        }

        /// <summary>
        /// 属性ID
        /// </summary>
        private long AttributeId
        {
            get
            {
                long action = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["ed"]))
                {
                    action = Globals.SafeInt(Request.Params["ed"], 0);
                }
                return action;
            }
        }

        /// <summary>
        /// 判断添加一个属性值还是多个属性值
        /// </summary>
        private int MidValue
        {
            get
            {
                int midvalue = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["m"]))
                {
                    midvalue = Globals.SafeInt(Request.Params["m"], 0);
                }
                return midvalue;
            }
        }

        /// <summary>
        /// 规格值ID
        /// </summary>
        private long ValueId
        {
            get
            {
                long valueid = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["v"]))
                {
                    valueid = Globals.SafeInt(Request.Params["v"], 0);
                }
                return valueid;
            }
        }

        /// <summary>
        /// 是否是图片
        /// </summary>
        private int IsImg
        {
            get
            {
                int isimg = -1;
                if (!string.IsNullOrWhiteSpace(Request.Params["img"]))
                {
                    isimg = Globals.SafeInt(Request.Params["img"], -1);
                }
                return isimg;
            }
        }

        private int AddOrModify
        {
            get
            {
                int res = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["a"]))
                {
                    res = Globals.SafeInt(Request.Params["a"], 0);
                }
                return res;
            }
        }
        #endregion

        #region PageLoading
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ProductTypeId == 0 || AttributeId == 0 || ValueId == 0)
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "参数错误，正在返回商品类型列表页...", "list.aspx");
                    return;
                }
                if (IsImg < 0)
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "参数错误，正在返回商品类型列表页...", "list.aspx");
                    return;
                }
                ShowTitleInfo();
                if (ValueId > 0)
                {
                    ShowValueInfo();
                }
            }
        }
        #endregion

        #region TitleMessage
        private void ShowTitleInfo()
        {
            Model.Shop.Products.AttributeInfo model = bll.GetModel(AttributeId);
            if (model != null)
            {
                this.Literal2.Text = model.AttributeName;
            }
            if (IsImg > 0)
            {
                this.attributeText.Visible = false;
                this.TextMsg.Visible = false;
            }
            else
            {
                this.imgInfo.Visible = false;
                this.linDelete.Visible = false;
                this.UseAttributeImage.Visible = false;
                this.imgTitle.Visible = false;
                this.imgMsg.Visible = false;
            }
        }
        #endregion


        private void ShowValueInfo()
        {
            Model.Shop.Products.AttributeValue model = ValueBll.GetModel(ValueId);
            if (model != null)
            {
                if (IsImg > 0)
                {
                    this.hfFileUrl.Value = model.ImageUrl;
                    this.imgInfo.ImageUrl = Maticsoft.Web.Components.FileHelper.GetImageUrl(model.ImageUrl);
                    this.txtImgTitle.Text = model.ValueStr;
                }
                else
                {
                    this.txtAttributeValue.Text = model.ValueStr;
                }
            }
        }

        #region SaveAttributeValues
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsImg == 0)
            {
                if (string.IsNullOrWhiteSpace(this.txtAttributeValue.Text.Trim()))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "请将规格值控制在1到15个字符之间！");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(this.hfFileUrl.Value.Trim()))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择要上传的图片，仅接受jpg、gif、png、格式的图片!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(this.txtImgTitle.Text.Trim()))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "请填写图片描述信息！");
                    return;
                }
            }
            Model.Shop.Products.AttributeValue attVaModel = ValueBll.GetModel(ValueId);
            if (IsImg == 1)
            {
                //将图片移动到FTP
                if (!string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
                {
                    FileManager.MoveImageForFTP(this.hfFileUrl.Value, "/UploadFolder/Images/ContentImgFile/");
                }

                attVaModel.ValueStr = this.txtImgTitle.Text;
                attVaModel.ImageUrl = this.hfFileUrl.Value;
            }
            else
            {
                attVaModel.ValueStr = this.txtAttributeValue.Text;
            }
            if (ValueBll.Exists(AttributeId, attVaModel.ValueStr, ValueId))//是否已存在
            {
                MessageBox.ShowFailTip(this, "规格值已存在!");
                return;
            }
            if (ValueBll.Update(attVaModel))
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='ListSpec.aspx?tid=" + ProductTypeId + "&ed=" + AttributeId + "&img=" + IsImg + "&a=" + AddOrModify + "'");
            }
            else
            {
                this.btnSave.Enabled = false;
                Maticsoft.Common.MessageBox.ShowFailTip(this, "保存失败！");
            }
        }
        #endregion
    }
}
