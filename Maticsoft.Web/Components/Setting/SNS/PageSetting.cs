/**
* PageSetting.cs
*
* 功 能： 页面设置访问类
* 类 名： PageSetting
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/11/15 10:32:31  Ben    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using Maticsoft.BLL.SNS;
using Maticsoft.BLL.SysManage;
using Maticsoft.Components.Setting;
using Maticsoft.Model.SysManage;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Maticsoft.Web.Components.Setting.SNS
{
    public class PageSetting : PageSettingBase
    {
        #region 构造
        /// <summary>
        /// 构造页面配置
        /// </summary>
        /// <param name="pageName">页面名称</param>
        /// <param name="applicationType">所在模块</param>
        public PageSetting(string pageName, ApplicationKeyType applicationType = ApplicationKeyType.System)
            : base(pageName, applicationType) { }
        #endregion

        #region 静态方法
        /// <summary>
        /// 获取页面设置参数
        /// </summary>
        /// <param name="pageName">页面名称 (admin后台定义)</param>
        /// <param name="applicationType">模块名称</param>
        /// <returns>设置内容</returns>
        public static IPageSetting GetPageSetting(string pageName, ApplicationKeyType applicationType = ApplicationKeyType.System)
        {
            PageSetting pageSetting = new PageSetting(pageName, applicationType);
            if (pageSetting._title.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._title = ReplaceHostName(pageSetting._title);

            if (pageSetting._keywords.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._keywords = ReplaceHostName(pageSetting._keywords);
            if (pageSetting._description.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._description = ReplaceHostName(pageSetting._description);
            return pageSetting;
        }

        public static IPageSetting GetPhotoListSetting(int cid,
                                                       ApplicationKeyType applicationType = ApplicationKeyType.System)
        {
            Maticsoft.BLL.SNS.Categories cateBll=new Categories();
            Maticsoft.Model.SNS.Categories cateModel = cateBll.GetModelByCache(cid);
            PageSetting pageSetting = new PageSetting("PhotoList", applicationType);
            if (pageSetting._title.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
            {
                pageSetting._title = ReplaceHostName(pageSetting._title);
            }
            if (cateModel!=null&&!String.IsNullOrWhiteSpace(cateModel.Meta_Title))
            {
                pageSetting._title = ReplaceHostName(cateModel.Meta_Title);
            }
            if (pageSetting._keywords.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
            {
                pageSetting._keywords = ReplaceHostName(pageSetting._keywords);
            }
            if (cateModel != null && !String.IsNullOrWhiteSpace(cateModel.Meta_Keywords))
            {
                pageSetting._keywords = ReplaceHostName(cateModel.Meta_Keywords);
            }
            if (pageSetting._description.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
            {
                pageSetting._description = ReplaceHostName(pageSetting._description);
            }
            if (cateModel != null && !String.IsNullOrWhiteSpace(cateModel.Meta_Description))
            {
                pageSetting._description = ReplaceHostName(cateModel.Meta_Description);
            }
            return pageSetting;
        }

        #endregion


        #region 获取静态化路径
        /// <summary>
        /// 获取商品路径
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="productInfo"></param>
        /// <param name="applicationType"></param>
        /// <returns></returns>
        public static string GetProductUrl(long productId)
        {
            Maticsoft.BLL.SNS.Products productBll = new BLL.SNS.Products();
            Maticsoft.Model.SNS.Products ProductInfo = productBll.GetModel(productId);
            if (ProductInfo != null)
            {
                return GetProductUrl(ProductInfo);
            }
            else
                return "";
        }

        public static string GetProductUrl(Maticsoft.Model.SNS.Products productInfo)
        {

            Maticsoft.BLL.SNS.Categories cateBll = new BLL.SNS.Categories();
            string Root = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ProductStaticRoot"); //获取商品静态的根目录
            Root = Root.LastIndexOf("/") > -1 ? Root : (Root + "/");
            int cateId = productInfo.CategoryID.HasValue ? productInfo.CategoryID.Value : -1;
            return (Root + cateBll.GetUrlByIdCache(cateId) + "/" + productInfo.ProductID + ".html").Replace("--", "-").ToLower();
        }

        /// <summary>
        /// 获取图片路径
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        public static string GetPhotoUrl(int photoId)
        {
            Maticsoft.BLL.SNS.Photos photoBll = new BLL.SNS.Photos();
            Maticsoft.Model.SNS.Photos PhotosInfo = photoBll.GetModel(photoId);
            if (PhotosInfo != null)
            {
                return GetPhotoUrl(PhotosInfo);
            }
            else
                return "";
        }

        public static string GetPhotoUrl(Maticsoft.Model.SNS.Photos PhotosInfo)
        {
            if (!String.IsNullOrWhiteSpace(PhotosInfo.StaticUrl))
            {
                return PhotosInfo.StaticUrl;
            }
            Maticsoft.BLL.SNS.UserAlbumDetail albumDetailBll = new BLL.SNS.UserAlbumDetail();
            Maticsoft.BLL.SNS.UserAlbums albumBll = new BLL.SNS.UserAlbums();
            string Root = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("PhotoStaticRoot"); //获取商品静态的根目录
            Root = Root.LastIndexOf("/") > -1 ? Root : (Root + "/");

            //现在专辑与图片没有用到多对多关系，暂时不用考虑
            List<Maticsoft.Model.SNS.UserAlbumDetail> DetailList = albumDetailBll.GetModelList("Type=0 and TargetID=" + PhotosInfo.PhotoID);
            if (DetailList != null && DetailList.Count > 0)
            {
                Maticsoft.Model.SNS.UserAlbums UserAlbums = albumBll.GetModelByCache(DetailList.FirstOrDefault().AlbumID);
                if (UserAlbums != null)
                {
                    return (Root + Common.PinyinHelper.GetPinyin(UserAlbums.AlbumName) + "/" + PhotosInfo.PhotoID + ".html").Replace("--", "-").ToLower();
                }
            }


            return "";
        }

        /// <summary>
        /// 获取产品详细页面设置参数
        /// </summary>
        /// <param name="pageName">页面名称 (admin后台定义)</param>
        /// <param name="applicationType">模块名称</param>
        /// <returns>设置内容</returns>
        public static PageSetting GetBlogDetailSetting(Maticsoft.Model.SNS.UserBlog userblog, string pageName = "BlogDetail", ApplicationKeyType applicationType = ApplicationKeyType.SNS)
        {
            if (userblog == null) userblog = new Model.SNS.UserBlog();
            PageSetting pageSetting = new PageSetting(pageName, applicationType);
            // 当前产品（单个产品设置）。。Meta_Title
            if (!String.IsNullOrWhiteSpace(userblog.Meta_Title))
            {
                pageSetting._title = userblog.Meta_Title;
            }
            else
            {
                pageSetting._title = Maticsoft.BLL.SNS.ConfigSystem.GetValueByCache(pageSetting.KeyTitle, pageSetting._applicationType);
                if (string.IsNullOrWhiteSpace(pageSetting._title))
                    pageSetting._title = Maticsoft.BLL.SNS.ConfigSystem.GetValueByCache(BaseKeyTitle, ApplicationKeyType.System);
            }
            //Meta_Keywords
            if (!String.IsNullOrWhiteSpace(userblog.Meta_Keywords))
            {
                pageSetting._keywords = userblog.Meta_Keywords;
            }
            else
            {
                pageSetting._keywords = Maticsoft.BLL.SNS. ConfigSystem.GetValueByCache(pageSetting.KeyKeywords, pageSetting._applicationType);
                if (string.IsNullOrWhiteSpace(pageSetting._keywords))
                    pageSetting._keywords = Maticsoft.BLL.SNS.ConfigSystem.GetValueByCache(BaseKeyKeywords, ApplicationKeyType.System);

            }
            //Meta_Description
            if (!String.IsNullOrWhiteSpace(userblog.Meta_Description))
            {
                pageSetting._description = userblog.Meta_Description;

            }
            else
            {
                pageSetting._description = Maticsoft.BLL.SNS.ConfigSystem.GetValueByCache(pageSetting.KeyDescription, pageSetting._applicationType);
                if (string.IsNullOrWhiteSpace(pageSetting._description))
                    pageSetting._description = Maticsoft.BLL.SNS.ConfigSystem.GetValueByCache(BaseKeyDescription, ApplicationKeyType.System);
            }
            if (pageSetting._title.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._title = ReplaceHostName(pageSetting._title);
            if (pageSetting._keywords.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._keywords = ReplaceHostName(pageSetting._keywords);
            if (pageSetting._description.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._description = ReplaceHostName(pageSetting._description);
            pageSetting.Replace(
                        new[] { PageSetting.RKEY_CNAME, userblog.Title },   //博客标题
                        new[] { PageSetting.RKEY_CID, userblog.BlogID.ToString() } );   //博客ID
            return pageSetting;
        }
        #endregion
    }
}
