/**
* Show.cs
*
* 功 能： [N/A]
* 类 名： Show.cs
*
* Ver                   变更日期             负责人      变更内容
* ───────────────────────────────────
* V0.01  2012年5月31日 14:33:10  孙鹏             创建
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Web.UI;

namespace Maticsoft.Web.Admin.AdvertisePosition
{
    public partial class Show : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 368; } } //网站管理_广告管理_详细页
        public string strid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.Params["id"] != null && Request.Params["id"].Trim() != "")
                {
                    strid = Request.Params["id"];
                    int AdvPositionId = (Convert.ToInt32(strid));
                    ShowInfo(AdvPositionId);
                }
            }
        }

        private void ShowInfo(int AdvPositionId)
        {
            Maticsoft.BLL.Settings.AdvertisePosition bll = new Maticsoft.BLL.Settings.AdvertisePosition();
            Maticsoft.Model.Settings.AdvertisePosition model = bll.GetModel(AdvPositionId);
            BLL.Members.Users userInfo = new BLL.Members.Users();
            this.lblAdvPositionId.Text = model.AdvPositionId.ToString();
            this.lblAdvPositionName.Text = model.AdvPositionName;
            this.lblShowType.Text = ConvertShowType(model.ShowType);
            switch (model.ShowType)
            {
                case 0:
                case 2:
                case 3:
                    this.horizontalClass.Visible = false;
                    this.codeClass.Visible = false;
                    break;
                case 1:
                    this.codeClass.Visible = false;
                    break;
                case 4:
                    this.verticalClass.Visible = false;
                    this.horizontalClass.Visible = false;
                    break;
                default:
                    break;
            }
            this.lblRepeatColumns.Text = model.RepeatColumns.ToString();
            this.lblWidth.Text = model.Width.ToString();
            this.lblHeight.Text = model.Height.ToString();
            this.lblAdvHtml.Text = Common.Globals.HtmlEncode(model.AdvHtml);
            this.chkIsOne.Checked = model.IsOne;
            if (!model.IsOne)
            {
                timeintervalClass.Visible = false;
            }
            this.lblTimeInterval.Text = model.TimeInterval.ToString();
            this.lblCreatedDate.Text = model.CreatedDate.ToString();
            Maticsoft.Accounts.Bus.User userBll = new Maticsoft.Accounts.Bus.User();
            this.lblCreatedUserID.Text = userBll.GetUserNameByCache(model.CreatedUserID.Value);
        }

        public void btnCancle_Click(object sender, EventArgs e)
        {
            Response.Redirect("list.aspx");
        }

        protected string ConvertShowType(object obj)
        {
            if (obj != null)
            {
                switch (obj.ToString())
                {
                    case "0":
                        return "纵向平铺";
                    case "1":
                        return "横向平铺";
                    case "2":
                        return "层叠显示";
                    case "3":
                        return "交替显示";
                    case "4":
                        return "自定义广告位";
                    default:
                        return "未定义的显示类型";
                }
            }
            else
            {
                return "未定义的显示类型";
            }
        }
    }
}
