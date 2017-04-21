/**
* List.cs
*
* 功 能： N/A
* 类 名： List
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01						   N/A    初版
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
using System.Web.UI.WebControls;
using Maticsoft.Common;

namespace Maticsoft.Web.Admin.SNS.Categories
{
    public partial class List : PageBaseAdmin
    {
        //int Act_ShowInvalid = -1; //查看失效数据行为
        private Maticsoft.BLL.SNS.Categories bll = new BLL.SNS.Categories();

        protected override int Act_PageLoad { get { return 121; } } //运营管理_是否显示社区商品分类管理页面

        protected new int Act_DelData = 124;    //运营管理_商品分享分类_删除分类
        protected new int Act_UpdateData = 123;    //运营管理_商品分享分类_编辑分类
        protected new int Act_AddData = 122;    //运营管理_商品分享分类_添加分类

        public int Type = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Type = type;
                Session["CategoryType"] = type;
                if (type == 1)
                {
                    this.Literal1.Text = "图片分享分类管理";
                   // this.Literal3.Text = "您可以添加、编辑、删除社区图片品分类信息";
                    // Act_PageLoad=130;//SNS_是否显示社区商品分类管理页面
                  
                    Act_DelData = 133;    //SNS_图片分享分类_删除分类
                    Act_UpdateData = 132;    //SNS_图片分享分类_编辑分类
                    Act_AddData = 131;    //SNS_图片分享分类_添加分类
                
                }
                else
                {
                    this.Literal1.Text = "商品分享分类管理";
                   // this.Literal3.Text = "您可以添加、编辑、删除社区商品分类信息";

                    // Act_PageLoad=121;//SNS_是否显示社区商品分类管理页面
                    Act_DelData = 124;    //SNS_商品分享分类_删除分类
                    Act_UpdateData = 123;    //SNS_商品分享分类_编辑分类
                    Act_AddData = 122;    //SNS_商品分享分类_添加分类
                   
                }
                this.txtType.Value = type.ToString();
                //if (Session["Style"] != null && Session["Style"].ToString() != "")
                //{
                //    string style = Session["Style"] + "xtable_bordercolorlight";
                //    if (Application[style] != null && Application[style].ToString() != "")
                //    {
                //        gridView.BorderColor = ColorTranslator.FromHtml(Application[style].ToString());
                //        gridView.HeaderStyle.BackColor = ColorTranslator.FromHtml(Application[style].ToString());
                //    }
                //}
                //if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_AddData)) && GetPermidByActID(Act_AddData) != -1)
                //{
                //    LiAdd.Visible = false;
                //}

                //BindData();
            }
        }

     

        //区分是否为商品分类还是图片分类
        public int type
        {
            get
            {
                int _type = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["type"]))
                {
                    _type = Globals.SafeInt(Request.Params["type"], 0);
                }
                return _type;
            }
        }

        
    }
}