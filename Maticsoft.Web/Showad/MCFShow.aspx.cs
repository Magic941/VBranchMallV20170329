/**
* ADShow.cs
*
* 功 能： [N/A]
* 类 名： ADShow
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/6/5 12:12:35  Rock    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Maticsoft.Common;

namespace Maticsoft.Web.Showad
{
    public partial class MCFShow : System.Web.UI.Page
    {

        #region 字段
        public string strADContentHtml = "";//广告效果代码	
        public string strADID = "";//广告ADID
        public string strStyle = "";
        public string strDirection = "";
        public string strAutoStart = "";
        public string strW = "";
        public string strH = "";
        #endregion

        #region 参数解释
        /***********----页面传入参数定义--*************/
        //c ： 代表：CallID  广告编号
        //t : 广告效果类型
        /*********************************************/
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                //Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
                if (!string.IsNullOrWhiteSpace(Request.Params["c"]))
                {
                    #region 解析URL传递过来的参数
                    //网站主编码
                    if (!string.IsNullOrWhiteSpace(Request.Params["c"]))
                    {
                        strADID = Request.Params["c"];
                    }
                    //广告效果类型
                    int AdType = 0;
                    if (!string.IsNullOrWhiteSpace(Request.Params["t"]))
                    {
                        AdType = Globals.SafeInt(Request.Params["t"], 0);
                    }
                    if (!string.IsNullOrWhiteSpace(Request.Params["a"]))
                    {
                        strAutoStart = Request.Params["a"];
                    }

                    ShowAD(strADID, AdType);
                    #endregion
                }
            }
        }


        public int? ADForProject
        {
            get
            {
                int adForProject = 0;
                if (!string.IsNullOrWhiteSpace(Request.Params["p"]))
                {
                    adForProject = Common.Globals.SafeInt(Request.Params["p"], 0);
                }
                if (adForProject == 0)
                {
                    return null;
                }
                else
                {
                    return adForProject;
                }
            }
        }

        #region 根据广告编号显示相应的广告 ShowAD
        private void ShowAD(string CallID, int AdTypeid)
        {
            Maticsoft.BLL.Settings.AdvertisePosition bllPosition = new BLL.Settings.AdvertisePosition();
            Model.Settings.AdvertisePosition model = bllPosition.GetModel(int.Parse(CallID));
            BLL.Settings.Advertisement bll = new BLL.Settings.Advertisement();

            string strADContent = "";
            //int ExistCount = ;
            if (model != null)
            {
                strADContent = CreateAd(AdTypeid, model, bll, strADContent, model.ShowType.Value);
            }
            else
            {
                //广告不存在
                Response.Write("广告不存在。");
            }
        }

        private string CreateAd(int AdTypeid, Model.Settings.AdvertisePosition model, BLL.Settings.Advertisement bll, string strADContent, int showType)
        {
            List<int> list = bll.GetContentType(model.AdvPositionId);
            if (list != null)
            {
                int ContentType = -1;
                while (true)
                {
                    Random rdNum = new Random();
                    int type = rdNum.Next(0, 4);
                    if (list.Contains(type))
                    {
                        ContentType = type;
                        break;
                    }
                }

                //优先自定义广告代码
                if (model.ShowType.HasValue && model.ShowType.Value == 4 &&
                    !string.IsNullOrWhiteSpace(model.AdvHtml))
                {
                    ContentType = 3; //自定义广告代码
                }

                #region 根据广告样式显示广告
                if (ContentType == 0)//文字广告
                {
                    #region 文字广告
                    //创建文字广告的脚本
                    strADContent = bll.CreateTextTag(model.AdvPositionId, ContentType);
                    string modelPage = "adtext.htm";
                    string ADhtmltext = ReadHtml(Server.MapPath(modelPage));
                    ADhtmltext = ADhtmltext.Replace("<%=tabWidth%>", model.Width.ToString());
                    ADhtmltext = ADhtmltext.Replace("<%=tabHeight%>", model.Height.ToString());
                    ADhtmltext = ADhtmltext.Replace("<%=strADContent%>", strADContent);
                    strADContentHtml = ADhtmltext.ToString();
                    #endregion
                }
                else if (ContentType == 1)// 图片广告
                {
                    #region 图片广告
                    string modelPage = string.Empty;
                    string strScript = "";
                    string strADpicContent = "";
                    switch (showType)
                    {
                        case 0://纵向平铺
                            modelPage = "adshow.htm";
                            strADpicContent = bll.CreatePicTag(model.AdvPositionId, ContentType, true, null, null);
                            break;
                        case 1://横向平铺 
                            modelPage = "adUdshow.htm";
                            strADpicContent = bll.CreatePicTag(model.AdvPositionId, ContentType, true, model.RepeatColumns, null);
                            break;
                        case 2://层叠显示
                            if (ADForProject.HasValue)
                            {
                                //SNS首页广告
                                if (ADForProject.Value == 1)
                                {
                                    modelPage = "SNSadpic.htm";
                                    strADpicContent = bll.CreatePicTag(model.AdvPositionId, ContentType, false, null, null);
                                }
                                //SNS专辑页面banner广告
                                if (ADForProject.Value == 2)
                                {
                                    modelPage = "SNSAlbumadpic.htm";
                                    strADpicContent = bll.CreatePicTag(model.AdvPositionId, ContentType, false, null, ADForProject.Value);
                                }
                                //图分享的首页广告位图片
                                if (ADForProject.Value == 3)
                                {
                                    modelPage = "TfxIndexAdPic.htm";
                                    strADpicContent = bll.CreatePicTag(model.AdvPositionId, ContentType, false, null, ADForProject.Value);
                                }
                                //淘淘乐后期改版首页广告
                                if (ADForProject.Value == 4)
                                {
                                    modelPage = "TaoLeAdpic.htm";
                                    strADpicContent = bll.CreatePicTag(model.AdvPositionId, ContentType, false, null, null);
                                }
                            }
                            else
                            {
                                strScript = "<script src=\"moveleft.js\" type=\"text/javascript\"></script>";
                                modelPage = "adshow.htm";
                                strADpicContent = bll.CreatePicTag(model.AdvPositionId, ContentType, true, null, null);
                            }
                            break;
                        case 3://交替显示
                            if (bll.IsExist(model.AdvPositionId, ContentType) > 1)
                            {
                                modelPage = "adpic.htm";
                            }
                            else
                            {
                                modelPage = "adsingle.htm";
                            }
                            strADpicContent = bll.CreatePicTag(model.AdvPositionId, ContentType, false, null, null);
                            break;
                        case 5://自定义广告代码
                            strADContent = bll.CreateCodeTag(model.AdvPositionId, ContentType);
                            modelPage = "adcode.htm";
                            break;
                        default:
                            break;
                    }
                    string ADhtmltext = ReadHtml(Server.MapPath(modelPage));
                    System.Data.DataSet ds = bll.GetTransitionImg(model.AdvPositionId, ContentType, model.RepeatColumns);
                    int AdvertisementCount = 1;
                    if (ds != null)
                    {
                        AdvertisementCount = ds.Tables[0].Rows.Count;
                    }
                    //创建图片广告的脚本
                    if (showType == 1)
                    {
                        ADhtmltext = ADhtmltext.Replace("<%=tabWidth%>", model.Width.ToString());
                        strW = model.Width.ToString();
                        ADhtmltext = ADhtmltext.Replace("<%=tabHeight%>", (model.Height * AdvertisementCount + 2).ToString());
                        strH = (model.Height * AdvertisementCount + 2).ToString();
                    }
                    else if (showType == 0)
                    {
                        ADhtmltext = ADhtmltext.Replace("<%=tabWidth%>", (model.Width * AdvertisementCount + 2).ToString());
                        strW = (model.Width * AdvertisementCount + 2).ToString();
                        ADhtmltext = ADhtmltext.Replace("<%=tabHeight%>", model.Height.ToString());
                        strH = model.Height.ToString();
                    }
                    else
                    {
                        strW = model.Width.ToString();
                        ADhtmltext = ADhtmltext.Replace("<%=tabWidth%>", model.Width.ToString());
                        ADhtmltext = ADhtmltext.Replace("<%=tabHeight%>", model.Height.ToString());
                        strH = model.Height.ToString();
                    }

                    if (strAutoStart.Equals("0"))
                    {
                        ADhtmltext = ADhtmltext.Replace("<%=tabStyle %>", "style=\"display:none;\"");
                    }
                    else
                    {
                        ADhtmltext = ADhtmltext.Replace("<%=tabStyle %>", "");
                    }
                    ADhtmltext = ADhtmltext.Replace("<%=strADContent%>", strADpicContent);
                    ADhtmltext = ADhtmltext.Replace("<%=tabScript %>", strScript);
                    strADContentHtml = ADhtmltext;
                    #endregion
                }
                else if (ContentType == 2)// Flash广告
                {
                    #region Flash广告
                    string modelPage = string.Empty;
                    if (bll.IsExist(model.AdvPositionId, ContentType) > 1)
                    {
                        modelPage = "adpic.htm";
                    }
                    else
                    {
                        modelPage = "adsingle.htm";
                    }
                    string ADhtmltext = ReadHtml(Server.MapPath(modelPage));
                    //创建图片广告的脚本
                    string strADpicContent = bll.CreateFlashTag(model.AdvPositionId, ContentType);
                    ADhtmltext = ADhtmltext.Replace("<%=tabWidth%>", model.Width.ToString());
                    ADhtmltext = ADhtmltext.Replace("<%=tabHeight%>", model.Height.ToString());

                    if (strAutoStart.Equals("0"))
                    {
                        ADhtmltext = ADhtmltext.Replace("<%=tabStyle %>", "style=\"display:none;\"");
                    }
                    else
                    {
                        ADhtmltext = ADhtmltext.Replace("<%=tabStyle %>", "");
                    }
                    ADhtmltext = ADhtmltext.Replace("<%=strADContent%>", strADpicContent);
                    strADContentHtml = ADhtmltext.ToString();
                    #endregion
                }
                else if (ContentType == 3)//自定义广告
                {
                    #region 自定义广告代码
                    strW = model.Width.Value.ToString();
                    strH = model.Height.Value.ToString();
                    strADContentHtml = Globals.HtmlDecode(bll.GetDefindCode(model.AdvPositionId));
                    #endregion
                }
                #endregion
            }
            else
            {
                Response.Write("广告不存在。");
            }
            return strADContent;
        }

        #region 读取广告模版
        /// <summary>
        /// 读取广告模版
        /// </summary>
        private string ReadHtml(string strPath)
        {
            StreamReader objReader = new StreamReader(strPath);
            string sLine = "";
            ArrayList arrText = new ArrayList();

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arrText.Add(sLine);
            }
            objReader.Close();
            return string.Join(" ", (string[])arrText.ToArray(typeof(string)));
        }
        #endregion

        #endregion
    }
}