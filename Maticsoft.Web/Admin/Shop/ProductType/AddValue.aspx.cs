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
namespace Maticsoft.Web.Admin.Shop.ProductType
{
    public partial class AddValue : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 503; } } //Shop_扩展属性值_添加编辑页
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
            if (string.IsNullOrWhiteSpace(this.txtAttributeValue.Text.Trim()))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "扩展属性的值，多个属性值可用“，”号隔开，每个值最多15个字符！");
                return;
            }
            if (ValueId > 0 && string.IsNullOrWhiteSpace(this.txtAttributeValue.Text.Trim()) && this.txtAttributeValue.Text.Trim().Contains(","))
            {
                Maticsoft.Common.MessageBox.ShowFailTip(this, "属性值必须小于15个字符，不能为空，并且不能包含逗号！");
                return;
            }

            string AttributeValue = this.txtAttributeValue.Text.Trim();

            Model.Shop.Products.AttributeValue attModel = new Model.Shop.Products.AttributeValue();
            //添加多个个属性值
            int resultCount = 0;
            if (MidValue == 0 && ValueId==0)
            {
                //添加多个属性值
                string[] strArray = AttributeValue.Replace("\r\n", "\n").Replace("\n", ",").Replace("，", ",").Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                string strValList = "'"+string.Join("','", strArray)+"'";
                if (ValueBll.Exists(string.Format(" AttributeId={0} and ValueStr in ( {1} )", AttributeId, strValList)))//是否已存在
                {
                    MessageBox.ShowFailTip(this, "属性值已存在!");
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
            else if (MidValue > 0 && ValueId == 0)
            {
                if (ValueBll.Exists(AttributeId, AttributeValue))//是否已存在
                {
                    MessageBox.ShowFailTip(this,"属性值已存在!");
                    return;
                }
                //添加单个属性值
                attModel.AttributeId = AttributeId;
                attModel.ValueStr = AttributeValue;
                resultCount = ValueBll.AttributeValueManage(attModel, Model.Shop.Products.DataProviderAction.Create) ? resultCount + 1 : 0;
            }
            else
            {
                if (ValueBll.Exists( AttributeId,AttributeValue,ValueId))//是否已存在
                {
                    MessageBox.ShowFailTip(this, "属性值已存在!");
                    return;
                }
                //修改属性值
                attModel = ValueBll.GetModel(ValueId);
                attModel.ValueStr = AttributeValue;
                if (ValueBll.Update(attModel))
                {
                    resultCount++;
                }
            }

            if (resultCount > 0 && AddOrModify > 0 && MidValue > 0)
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='listV.aspx?tid=" + ProductTypeId + "&ed=" + AttributeId + "&a=" + AddOrModify + "'");
            }else if(resultCount > 0 && AddOrModify > 0)
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='Step2.aspx?tid=" + ProductTypeId + "'");
            }
            else   if (resultCount > 0 && MidValue == 0)
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='Modify2.aspx?tid=" + ProductTypeId + "&ed=" + AttributeId + "'");
            }
            else if (resultCount > 0 && MidValue > 0)
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='listV.aspx?tid=" + ProductTypeId + "&ed=" + AttributeId + "'");
            }
            else if (resultCount > 0 && ValueId > 0)
            {
                Maticsoft.Common.MessageBox.ResponseScript(this, "parent.location.href='listV.aspx?tid=" + ProductTypeId + "&ed=" + AttributeId + "'");
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
