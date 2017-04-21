/**
* ImageTransitionEffects.cs
*
* 功 能： [N/A]
* 类 名： ImageTransitionEffects
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/1 18:55:41  Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using Maticsoft.Common;

namespace Maticsoft.Web.Controls
{
    public partial class ImageTransitionEffects : System.Web.UI.UserControl
    {
        public string strWidth = "0";//广告展示的宽度
        public string strHeight = "0";//广告展示的高度
        BLL.Settings.Advertisement bll = new BLL.Settings.Advertisement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BoundData(int.Parse(AdPositionID));
            }
        }

        private void BoundData(int PositionID)
        {
            int cType = Globals.SafeInt(ContentType, -1);

            DataSet ds = bll.GetTransitionImg(PositionID, cType, null);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                strWidth = ds.Tables[0].Rows[0]["Width"].ToString();
                strHeight = ds.Tables[0].Rows[0]["Height"].ToString();
                _direction = ds.Tables[0].Rows[0]["ShowType"].ToString();

                switch (int.Parse(ContentType))
                {
                    case 0://文字
                        this.rp_TextShow.DataSource = ds;
                        this.rp_TextShow.DataBind();
                        break;
                    case 1://图片
                        this.rpTransitionEffects.DataSource = ds;
                        this.rpTransitionEffects.DataBind();
                        break;
                    case 2://Flash
                        this.rp_FlashShow.DataSource = ds;
                        this.rp_FlashShow.DataBind();
                        break;
                    case 3://Html代码
                        this.rp_HtmlCode.DataSource = ds;
                        this.rp_HtmlCode.DataBind();
                        break;
                    default:
                        break;
                }
            }
        }

        private string _adPositionID;
        /// <summary>
        /// 广告位ID
        /// </summary>
        public string AdPositionID
        {
            get { return _adPositionID; }
            set { _adPositionID = value; }
        }

        private string _contentType;
        /// <summary>
        /// 内容类型: 0 文字，1 图片，2 flash，3 HTML代码
        /// </summary>
        public string ContentType
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(_contentType))
                    return _contentType;
                else return "-1";
            }
            set { _contentType = value; }
        }

        private string _direction;
        /// <summary>
        /// 可选属性：滚动方向(默认为0向上滚动) 值:0上 1下 2左 3右 -1上下交替 4左右交替  
        /// </summary>
        public string Direction
        {
            get
            {
                switch (_direction)
                {
                    case "0":
                        return "0";
                    case "1":
                        return "1";
                    case "2":
                        return "4";
                    case "3":
                        return "-1";
                    case "4":
                        return "2";
                    default:
                        return _direction;
                }
            }
            set { _direction = value; }
        }
    }
}