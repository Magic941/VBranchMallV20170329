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
    public partial class AddSpec : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 512; } } //Shop_规格值_添加页
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
        /// 属性值ID
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
        /// 是否是图片,0文字，1图片
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
                if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)))
                {
                    MessageBox.ShowAndBack(this, "您没有权限");
                    return;
                }
                if (ProductTypeId == 0 || AttributeId == 0)
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
                //if (ValueId > 0)
                //{
                //    ShowValueInfo();
                //}
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
                this.txtAttributeValue.Text = model.ValueStr;
            }
        }

        #region SaveAttributeValues
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (IsImg == 0)
            {
                if (string.IsNullOrWhiteSpace(this.txtAttributeValue.Text.Trim()))
                {
                    Maticsoft.Common.MessageBox.ShowFailTip(this, "扩展属性的值，多个属性值可用“，”号隔开，每个值最多15个字符！");
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
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

            Model.Shop.Products.AttributeValue attModel = new Model.Shop.Products.AttributeValue();
            //添加多个个属性值
            int resultCount = 0;
            if (IsImg == 0)
            {
                //添加多个属性值
                string AttributeValue = this.txtAttributeValue.Text.Trim();

                string[] strArray = AttributeValue.Replace("\r\n", "\n").Replace("\n", ",").Replace("，", ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                string strValList = "'" + string.Join("','", strArray) + "'";
                if (ValueBll.Exists(string.Format(" AttributeId={0} and ValueStr in ( {1} )", AttributeId, strValList)))//是否已存在
                {
                    MessageBox.ShowFailTip(this, "规格值已存在!");
                    return;
                }
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (strArray[i].Length <= 100)
                    {
                        string str2 = strArray[i];
                        if (!string.IsNullOrWhiteSpace(str2))
                        {
                            attModel.AttributeId = AttributeId;
                            attModel.ValueStr = str2;
                            resultCount = ValueBll.AttributeValueManage(attModel, Model.Shop.Products.DataProviderAction.Create) ? resultCount + 1 : 0;
                        }
                    }
                }
            }
            else if (IsImg == 1)
            {
                //将图片移动到FTP
                if (!string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
                {
                    FileManager.MoveImageForFTP(this.hfFileUrl.Value, "/UploadFolder/Images/ContentImgFile/");
                }

                //添加单个属性值
                attModel.ImageUrl = this.hfFileUrl.Value;
                attModel.AttributeId = AttributeId;
                attModel.ValueStr = this.txtImgTitle.Text;
                if (ValueBll.Exists(AttributeId, this.txtImgTitle.Text.Trim()))//是否已存在
                {
                    MessageBox.ShowFailTip(this, "规格值已存在!");
                    return;
                }
                resultCount = ValueBll.AttributeValueManage(attModel, Model.Shop.Products.DataProviderAction.Create) ? resultCount + 1 : 0;
            }
            else
            {
                //修改属性值
            }

            if (resultCount > 0 && AddOrModify > 0 && MidValue ==1)
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='ListSpec.aspx?tid=" + ProductTypeId + "&ed=" + AttributeId + "&img=" + IsImg + "&a=" + AddOrModify + "'");
            }
            else if (resultCount > 0 && AddOrModify > 0 && MidValue == 2)
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='Step3.aspx?tid=" + ProductTypeId + "'");
            }
            else if (resultCount > 0  && MidValue == 1)
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='ListSpec.aspx?tid=" + ProductTypeId + "&ed=" + AttributeId + "&img=" + IsImg + "'");
            }
            else if (resultCount > 0)
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='Modify3.aspx?tid=" + ProductTypeId + "'");
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
