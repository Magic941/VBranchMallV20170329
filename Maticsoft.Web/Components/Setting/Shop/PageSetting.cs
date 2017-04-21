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

using System;
using System.Collections.Generic;
using Maticsoft.BLL.SysManage;
using Maticsoft.Components.Setting;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Components.Setting.Shop
{
    public class PageSetting : IPageSetting
    {
        #region 动态变量替换
        /// <summary>
        /// 站点名称
        /// </summary>
        public const string RKEY_HOSTNAME = "{hostname}";
        /// <summary>
        /// 当前产品/图片/分类的名称 用于列表/详细页
        /// </summary>
        public const string RKEY_CNAME = "{cname}";
        /// <summary>
        /// 当前标题 用于帖子详细页面
        /// </summary>
        public const string RKEY_CTNAME = "{ctname}";
        /// <summary>
        /// 当前标签 用于列表/详细页面
        /// </summary>
        public const string RKEY_CTAG = "{ctag}";
        /// <summary>
        /// 当前描述 用于列表/详细页面
        /// </summary>
        public const string RKEY_CDES = "{cdes}";
        /// <summary>
        /// 当前的主键ID
        /// </summary>
        public const string RKEY_CID = "{cid}";
        /// <summary>
        /// 当前的分类的路径  形式如 父类名/子类名/子子类名
        /// </summary>
        public const string RKEY_CATEPATH = "{catepath}";

        public static string GetHostName(ApplicationKeyType applicationType = ApplicationKeyType.System)
        {
            return ConfigSystem.GetValueByCache(WebSiteSet.WEB_NAME, applicationType);
        }

        #endregion

        #region 设置Key
        protected const string KeyRule = "{0}_{1}_{2}";
        //protected const string KeyUrlRule = "{0}_{1}_{2}_{3}";

        protected const string BaseKeyTitle = "Title";
        protected const string BaseKeyKeywords = "Keywords";
        protected const string BaseKeyDescription = "Description";

        protected const string BaseKeyUrl = "KeyUrl";
        protected const string BaseKeyAlt = "KeyAlt";
        protected const string BaseKeyImageTitle = "ImageTitle";

        public readonly string KeyTitle;
        public readonly string KeyKeywords;
        public readonly string KeyDescription;
        public readonly string KeyUrl;

        public readonly string KeyAlt;
        public readonly string KeyImageTitle;
        #endregion

        #region 构造
        /// <summary>
        /// 构造页面配置
        /// </summary>
        /// <param name="pageName">页面名称</param>
        /// <param name="applicationType">所在模块</param>
        public PageSetting(string pageName, ApplicationKeyType applicationType = ApplicationKeyType.System, string type = "Base")
        {
            _applicationType = applicationType;
            //重新初始化Key 根据当前ApplicationKeyType 追加前缀
            //switch (type)
            //{
            //    case ""
            //}
            if (type == "Base")
            {
                KeyTitle = string.Format(KeyRule, _applicationType, pageName, BaseKeyTitle);
                KeyKeywords = string.Format(KeyRule, _applicationType, pageName, BaseKeyKeywords);
                KeyDescription = string.Format(KeyRule, _applicationType, pageName, BaseKeyDescription);
                //根据新Key获取对应值, 如为空读取默认值
                _title = ConfigSystem.GetValueByCache(KeyTitle, _applicationType);
                if (string.IsNullOrWhiteSpace(_title))
                    _title = ConfigSystem.GetValueByCache(BaseKeyTitle, ApplicationKeyType.System);

                _keywords = ConfigSystem.GetValueByCache(KeyKeywords, _applicationType);
                if (string.IsNullOrWhiteSpace(_keywords))
                    _keywords = ConfigSystem.GetValueByCache(BaseKeyKeywords, ApplicationKeyType.System);

                _description = ConfigSystem.GetValueByCache(KeyDescription, _applicationType);
                if (string.IsNullOrWhiteSpace(_description))
                    _description = ConfigSystem.GetValueByCache(BaseKeyDescription, ApplicationKeyType.System);

                _isimage = false;
            }
            if (type == "Url")
            {
                KeyTitle = string.Format(KeyRule, _applicationType, pageName, BaseKeyTitle);
                KeyKeywords = string.Format(KeyRule, _applicationType, pageName, BaseKeyKeywords);
                KeyDescription = string.Format(KeyRule, _applicationType, pageName, BaseKeyDescription);
                KeyUrl = string.Format(KeyRule, _applicationType, pageName, BaseKeyUrl);
                //根据新Key获取对应值, 如为空读取默认值
                _title = ConfigSystem.GetValueByCache(KeyTitle, _applicationType);
                if (string.IsNullOrWhiteSpace(_title))
                    _title = ConfigSystem.GetValueByCache(BaseKeyTitle, ApplicationKeyType.System);

                _url = ConfigSystem.GetValueByCache(KeyUrl, _applicationType);
                if (string.IsNullOrWhiteSpace(_url))
                    _url = ConfigSystem.GetValueByCache(BaseKeyUrl, ApplicationKeyType.System);

                _keywords = ConfigSystem.GetValueByCache(KeyKeywords, _applicationType);
                if (string.IsNullOrWhiteSpace(_keywords))
                    _keywords = ConfigSystem.GetValueByCache(BaseKeyKeywords, ApplicationKeyType.System);

                _description = ConfigSystem.GetValueByCache(KeyDescription, _applicationType);
                if (string.IsNullOrWhiteSpace(_description))
                    _description = ConfigSystem.GetValueByCache(BaseKeyDescription, ApplicationKeyType.System);

                _isimage = false;
            }
            if (type == "Image")
            {
                _isimage = true;
                KeyAlt = string.Format(KeyRule, _applicationType, pageName, BaseKeyAlt);
                KeyImageTitle = string.Format(KeyRule, _applicationType, pageName, BaseKeyImageTitle);

                _alt = ConfigSystem.GetValueByCache(KeyAlt, _applicationType);
                if (string.IsNullOrWhiteSpace(_alt))
                    _alt = ConfigSystem.GetValueByCache(BaseKeyAlt, ApplicationKeyType.System);

                _imagetitle = ConfigSystem.GetValueByCache(KeyImageTitle, _applicationType);
                if (string.IsNullOrWhiteSpace(_imagetitle))
                    _imagetitle = ConfigSystem.GetValueByCache(BaseKeyImageTitle, ApplicationKeyType.System);
            }
        }

        public PageSetting()
        { }

        #endregion

        #region 属性
        protected ApplicationKeyType _applicationType;
        protected string _title;
        protected string _description;
        protected string _keywords;

        protected string _url;
        //图片
        protected string _alt;
        protected string _imagetitle;

        protected bool _isimage;

        /// <summary>
        /// 页面Title
        /// </summary>
        /// <remarks>赋值时将直接修改DB</remarks>
        public virtual string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                ConfigSystem.Modify(KeyTitle, value, KeyTitle, _applicationType);
            }
        }

        /// <summary>
        /// 页面Keywords
        /// </summary>
        /// <remarks>赋值时将直接修改DB</remarks>
        public virtual string Keywords
        {
            get { return _keywords; }
            set
            {
                _keywords = value;
                ConfigSystem.Modify(KeyKeywords, value, KeyKeywords, _applicationType);
            }
        }

        /// <summary>
        /// 页面Description
        /// </summary>
        /// <remarks>赋值时将直接修改DB</remarks>
        public virtual string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                ConfigSystem.Modify(KeyDescription, value, KeyDescription, _applicationType);
            }
        }

        //..

        /// <summary>
        /// 页面URL地址
        /// </summary>
        public virtual string Url
        {
            get { return _url; }
            set
            {
                _url = value;
                ConfigSystem.Modify(KeyUrl, value, KeyUrl, _applicationType);
            }
        }

        /// <summary>
        ///图片Alt地址
        /// </summary>
        public virtual string Alt
        {
            get { return _alt; }
            set
            {
                _alt = value;
                ConfigSystem.Modify(KeyAlt, value, KeyAlt, _applicationType);
            }
        }

        /// <summary>
        ///图片Title地址
        /// </summary>
        public virtual string ImageTitle
        {
            get { return _imagetitle; }
            set
            {
                _imagetitle = value;
                ConfigSystem.Modify(KeyImageTitle, value, KeyImageTitle, _applicationType);
            }
        }
        /// <summary>
        /// 是否是图片SEO
        /// </summary>
        public virtual bool IsImage
        {
            get { return _isimage; }
            set
            {
                _isimage = value;
            }
        }
        #endregion

        #region 方法

        /// <summary>
        /// 替换器
        /// 替换设置中指定的动态内容
        /// </summary>
        /// <param name="values">替换键值对象</param>
        /// <remarks>
        /// 标题替换值长度截取为30字符
        /// 说明替换值长度截取为140字符
        /// </remarks>
        /// <returns>替换结果</returns>
        public void Replace(params string[][] values)
        {
            if (values == null || values.Length < 1) return;
            _title = ReplaceTitle(values);
            _keywords = ReplaceKeywords(values);
            _description = ReplaceDescription(values);
        }

        /// <summary>
        /// 替换器
        /// 替换标题中指定的动态内容
        /// </summary>
        /// <param name="values">替换键值对象</param>
        /// <remarks>标题替换值长度截取为30字符</remarks>
        /// <returns>替换结果</returns>
        public string ReplaceTitle(params string[][] values)
        {
            if (values == null || values.Length < 1) return _title;
            string tmp = _title;
            foreach (string[] keyValue in values)
            {
                if (keyValue.Length != 2) continue;
                tmp = tmp.Replace(
                    keyValue[0], //Key
                    Maticsoft.Common.StringPlus.SubString(
                        Maticsoft.Common.Globals.HtmlDecode(
                            keyValue[1]), 30, "..") //Value
                    );
            }
            return tmp;
        }


        /// <summary>
        /// 替换器
        /// 替换URL中指定的动态内容
        /// </summary>
        /// <param name="values">替换键值对象</param>
        /// <remarks>标题替换值长度截取为30字符</remarks>
        /// <returns>替换结果</returns>
        public string ReplaceURL(params string[][] values)
        {
            if (values == null || values.Length < 1) return _url;
            string tmp = _url;
            foreach (string[] keyValue in values)
            {
                if (keyValue.Length != 2) continue;
                tmp = tmp.Replace(
                    keyValue[0], //Key
                        Maticsoft.Common.Globals.HtmlDecode(
                            keyValue[1]) //Value
                    );
            }
            return tmp;
        }

        /// <summary>
        /// 替换器
        /// 替换ImageAlt中指定的动态内容
        /// </summary>
        /// <param name="values">替换键值对象</param>
        /// <remarks>标题替换值长度截取为30字符</remarks>
        /// <returns>替换结果</returns>
        public string ReplaceAlt(params string[][] values)
        {
            if (values == null || values.Length < 1) return _alt;
            string tmp = _alt;
            foreach (string[] keyValue in values)
            {
                if (keyValue.Length != 2) continue;
                tmp = tmp.Replace(
                    keyValue[0], //Key
                        Maticsoft.Common.Globals.HtmlDecode(
                            keyValue[1]) //Value
                    );
            }
            return tmp;
        }


        /// <summary>
        /// 替换器
        /// 替换ImageAlt中指定的动态内容
        /// </summary>
        /// <param name="values">替换键值对象</param>
        /// <remarks>标题替换值长度截取为30字符</remarks>
        /// <returns>替换结果</returns>
        public string ReplaceImageTitle(params string[][] values)
        {
            if (values == null || values.Length < 1) return _imagetitle;
            string tmp = _imagetitle;
            foreach (string[] keyValue in values)
            {
                if (keyValue.Length != 2) continue;
                tmp = tmp.Replace(
                    keyValue[0], //Key
                        Maticsoft.Common.Globals.HtmlDecode(
                            keyValue[1]) //Value
                    );
            }
            return tmp;
        }
        #region 明确写法 - KeyValuePair实例内容所使用的字符较多 暂不使用
        ///// <summary>
        ///// 替换器
        ///// 替换标题中指定的动态内容
        ///// </summary>
        ///// <param name="values">替换键值对象</param>
        ///// <remarks>标题替换值长度截取为30字符</remarks>
        ///// <returns>替换结果</returns>
        //public string ReplaceTitle(params KeyValuePair<string, string>[] values)
        //{
        //    if (values == null || values.Length < 1) return _title;
        //    string tmp = null;
        //    foreach (KeyValuePair<string, string> keyValue in values)
        //    {
        //        tmp = _title.Replace(
        //            keyValue.Key, //Key
        //            Maticsoft.Common.StringPlus.SubString(
        //                Maticsoft.Common.Globals.HtmlDecode(
        //                    keyValue.Value), 30, "..") //Value
        //            );
        //    }
        //    return tmp;
        //} 
        #endregion

        /// <summary>
        /// 替换器
        /// 替换关键字中指定的动态内容
        /// </summary>
        /// <param name="values">替换键值对象</param>
        /// <returns>替换结果</returns>
        public string ReplaceKeywords(params string[][] values)
        {
            if (values == null || values.Length < 1) return _keywords;
            string tmp = _keywords;
            foreach (string[] keyValue in values)
            {
                if (keyValue.Length != 2) continue;
                tmp = tmp.Replace(
                    keyValue[0], //Key
                    Maticsoft.Common.StringPlus.SubString(
                        Maticsoft.Common.Globals.HtmlDecode(
                            keyValue[1]), 30, "..") //Value
                    );
            }
            return tmp;
        }

        /// <summary>
        /// 替换器
        /// 替换说明中指定的动态内容
        /// </summary>
        /// <param name="values">替换键值对象</param>
        /// <remarks>说明替换值长度截取为140字符</remarks>
        /// <returns>替换结果</returns>
        public string ReplaceDescription(params string[][] values)
        {
            if (values == null || values.Length < 1) return _description;
            string tmp = _description;
            foreach (string[] keyValue in values)
            {
                if (keyValue.Length != 2) continue;
                tmp = tmp.Replace(
                    keyValue[0], //Key
                    Maticsoft.Common.StringPlus.SubString(
                        Maticsoft.Common.Globals.HtmlDecode(
                            keyValue[1]), 140, "..") //Value
                    );
            }
            return tmp;
        }

        #endregion

        #region 静态方法 页面读取调用
        /// <summary>
        /// 替换指定的动态内容
        /// </summary>
        /// <param name="target">替换文本</param>
        /// <returns>替换结果</returns>
        private static string ReplaceHostName(string target)
        {
            return target.Replace(RKEY_HOSTNAME, GetHostName());
        }

        #region 获取单参数

        public static string GetTitle(string pageName, ApplicationKeyType applicationType = ApplicationKeyType.System)
        {
            string title = ConfigSystem.GetValueByCache(
                string.Format(KeyRule, applicationType, pageName, BaseKeyTitle),
                applicationType);
            if (string.IsNullOrWhiteSpace(title))
                title = ConfigSystem.GetValueByCache(BaseKeyTitle, ApplicationKeyType.System);
            if (title.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                return ReplaceHostName(title);
            return title;
        }

        public static string GetKeywords(string pageName, ApplicationKeyType applicationType = ApplicationKeyType.System)
        {
            string keywords = ConfigSystem.GetValueByCache(
                string.Format(KeyRule, applicationType, pageName, BaseKeyKeywords),
                applicationType);
            if (string.IsNullOrWhiteSpace(keywords))
                keywords = ConfigSystem.GetValueByCache(BaseKeyKeywords, ApplicationKeyType.System);
            if (keywords.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                return ReplaceHostName(keywords);
            return keywords;
        }

        public static string GetDescription(string pageName, ApplicationKeyType applicationType = ApplicationKeyType.System)
        {
            string description = ConfigSystem.GetValueByCache(
                string.Format(KeyRule, applicationType, pageName, BaseKeyDescription),
                applicationType);
            if (string.IsNullOrWhiteSpace(description))
                description = ConfigSystem.GetValueByCache(BaseKeyDescription, ApplicationKeyType.System);
            if (description.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                return ReplaceHostName(description);
            return description;
        }
        #endregion

        /// <summary>
        /// 获取页面设置参数
        /// </summary>
        /// <param name="pageName">页面名称 (admin后台定义)</param>
        /// <param name="applicationType">模块名称</param>
        /// <returns>设置内容</returns>
        public static PageSetting GetPageSetting(string pageName, ApplicationKeyType applicationType = ApplicationKeyType.System)
        {
            PageSetting pageSetting = new PageSetting(pageName, applicationType);
            #region 加载默认值 - BaseKeyTitle
            // 根据新Key获取对应值, 如为空读取默认值

            //pageSetting._title = ConfigSystem.GetValueByCache(pageSetting.KeyTitle, pageSetting._applicationType);
            //if (string.IsNullOrWhiteSpace(pageSetting._title))
            //    pageSetting._title = ConfigSystem.GetValueByCache(BaseKeyTitle, ApplicationKeyType.System);

            //pageSetting._keywords = ConfigSystem.GetValueByCache(pageSetting.KeyKeywords, _applicationType);
            //if (string.IsNullOrWhiteSpace(pageSetting._keywords))
            //    pageSetting._keywords = ConfigSystem.GetValueByCache(BaseKeyKeywords, ApplicationKeyType.System);

            //_description = ConfigSystem.GetValueByCache(KeyDescription, _applicationType);
            //if (string.IsNullOrWhiteSpace(_description))
            //    _description = ConfigSystem.GetValueByCache(BaseKeyDescription, ApplicationKeyType.System); 
            #endregion
            if (pageSetting._title.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._title = ReplaceHostName(pageSetting._title);
            if (pageSetting._keywords.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._keywords = ReplaceHostName(pageSetting._keywords);
            if (pageSetting._description.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._description = ReplaceHostName(pageSetting._description);
            return pageSetting;
        }

        /// <summary>
        /// 获取产品类别页面设置参数
        /// </summary>
        /// <param name="pageName">页面名称 (admin后台定义)</param>
        /// <param name="applicationType">模块名称</param>
        /// <returns>设置内容</returns>
        public static PageSetting GetCategorySetting(Maticsoft.Model.Shop.Products.CategoryInfo cateModel, string pageName = "Category", ApplicationKeyType applicationType = ApplicationKeyType.Shop)
        {
            if (cateModel != null)
            {
                PageSetting pageSetting = new PageSetting(pageName, applicationType, "Url");
                // 当前产品分类的产品详细页面设置
                if (!String.IsNullOrWhiteSpace(cateModel.Meta_Title))
                {
                    pageSetting._title = cateModel.Meta_Title;
                }
                else
                {
                    if (pageSetting._title.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                        pageSetting._title = ReplaceHostName(pageSetting._title);
                }

                if (!String.IsNullOrWhiteSpace(cateModel.Meta_Keywords))
                {
                        pageSetting._keywords = cateModel.Meta_Keywords;
                }
                else
                {
                    if (pageSetting._keywords.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                        pageSetting._keywords = ReplaceHostName(pageSetting._keywords);
                }

                if (!String.IsNullOrWhiteSpace(cateModel.Meta_Description))
                {
                        pageSetting._description = cateModel.Meta_Description;
                }
                else
                {
                    if (pageSetting._description.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                        pageSetting._description = ReplaceHostName(pageSetting._description);
                }
                if (!String.IsNullOrWhiteSpace(cateModel.SeoUrl))
                {
                    if (cateModel.SeoUrl.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                        pageSetting._url = ReplaceHostName(cateModel.SeoUrl);
                }
                else
                {
                    if (pageSetting._url.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                        pageSetting._url = ReplaceHostName(pageSetting._url);
                }

                //当前产品分类（类别）
                //全局的SEO设置 （仅初始化使用）
                pageSetting.Replace(
                    new[] { PageSetting.RKEY_CNAME, cateModel.Name }, //分类名称
                    new[] { PageSetting.RKEY_CID, cateModel.CategoryId.ToString() }); //分类ID 
                return pageSetting;
            }
            else
            {
                return GetPageSetting("Home", Model.SysManage.ApplicationKeyType.Shop);
            }
        }

        #region 产品详细SEO

        /// <summary>
        /// 获取产品详细页面设置参数
        /// </summary>
        /// <param name="pageName">页面名称 (admin后台定义)</param>
        /// <param name="applicationType">模块名称</param>
        /// <returns>设置内容</returns>
        public static PageSetting GetProductSetting(Maticsoft.Model.Shop.Products.ProductInfo productInfo, string pageName = "Product", ApplicationKeyType applicationType = ApplicationKeyType.Shop)
        {
            PageSetting pageSetting = new PageSetting(pageName, applicationType);
            Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new BLL.Shop.Products.CategoryInfo();
            Maticsoft.BLL.Shop.Products.ProductCategories ProductCateBll = new BLL.Shop.Products.ProductCategories();
            List<Maticsoft.Model.Shop.Products.ProductCategories> CateList = ProductCateBll.GetModelList(" ProductId=" + productInfo.ProductId);
            string CateListNames = "";
            string CateNames = "";
            if (CateList != null && CateList.Count > 0)
            {
                int i = 0;
                foreach (var item in CateList)
                {
                    string cateInfo = cateBll.GetNamePathByPath(item.CategoryPath).Replace("/", ",");
                    CateListNames = i == 0 ? cateInfo : (CateListNames + "," + cateInfo);
                    i++;
                }
            }

            if (CateList != null && CateList.Count > 0)
            {
                int i = 0;
                foreach (var item in CateList)
                {
                    var cateInfo = cateBll.GetModelByCache(item.CategoryId);
                    if (cateInfo != null)
                    {
                        CateNames = i == 0 ? cateInfo.Name : (CateNames + "," + cateInfo.Name);
                    }
                    i++;
                }
            }
            // 当前产品（单个产品设置）。。Meta_Title
            if (!String.IsNullOrWhiteSpace(productInfo.Meta_Title))
            {
                pageSetting._title = productInfo.Meta_Title;
            }

            else
            {
                pageSetting._title = ConfigSystem.GetValueByCache(pageSetting.KeyTitle, pageSetting._applicationType);
                if (string.IsNullOrWhiteSpace(pageSetting._title))
                    pageSetting._title = ConfigSystem.GetValueByCache(BaseKeyTitle, ApplicationKeyType.System);
            }
            //Meta_Keywords
            if (!String.IsNullOrWhiteSpace(productInfo.Meta_Keywords))
            {
                pageSetting._keywords = productInfo.Meta_Keywords;
            }
            else
            {
                pageSetting._keywords = ConfigSystem.GetValueByCache(pageSetting.KeyKeywords, pageSetting._applicationType);
                if (string.IsNullOrWhiteSpace(pageSetting._keywords))
                    pageSetting._keywords = ConfigSystem.GetValueByCache(BaseKeyKeywords, ApplicationKeyType.System);

            }
            //Meta_Description
            if (!String.IsNullOrWhiteSpace(productInfo.Meta_Description))
            {
                pageSetting._description = productInfo.Meta_Description;

            }
            else
            {
                pageSetting._description = ConfigSystem.GetValueByCache(pageSetting.KeyDescription, pageSetting._applicationType);
                if (string.IsNullOrWhiteSpace(pageSetting._description))
                    pageSetting._description = ConfigSystem.GetValueByCache(BaseKeyDescription, ApplicationKeyType.System);
            }

            if (pageSetting._title.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._title = ReplaceHostName(pageSetting._title);
            if (pageSetting._keywords.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._keywords = ReplaceHostName(pageSetting._keywords);
            if (pageSetting._description.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                pageSetting._description = ReplaceHostName(pageSetting._description);

            BLL.SysManage.WebSiteSet WebSiteSet = new BLL.SysManage.WebSiteSet(ApplicationKeyType.System);

            //产品品牌值
            BLL.Shop.Products.BrandInfo brandManage = new BLL.Shop.Products.BrandInfo();
            Model.Shop.Products.BrandInfo brandModel = brandManage.GetModelByCache(productInfo.BrandId);
            string brands = brandModel == null ? "" : brandModel.BrandName;

            //产品属性集合
            //BLL.Shop.Products.ProductAttribute attributeManage = new BLL.Shop.Products.ProductAttribute();
            //List<Maticsoft.Model.Shop.Products.ProductAttribute> attributesList = attributeManage.ProductAttributesList(productInfo.ProductId);
            //string attributes = (attributesList != null && attributesList.Count > 0) ? String.Join(",", attributesList.Select(c => c.ValueStr)) : "";
            pageSetting.Replace(
         new[] { PageSetting.RKEY_CNAME, productInfo.ProductName },        //产品名称
         new[] { RKEY_CID, productInfo.ProductCode }, //产品编号
                //       new[] { PageSetting.RKEY_COMPANY, WebSiteSet.Company_Name }, //
                //new[] { PageSetting.RKEY_CATENAME, CateNames },//分类名称
             new[] { "{catelistname}", CateListNames }, //分类名称集合
             new[] { "{brands}", brands }//产品品牌
                //new[] { "{attributes}", attributes  }
               ); //产品属性
            return pageSetting;
        }
        #endregion

        /// <summary>
        /// 获取产品路径
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="productInfo"></param>
        /// <param name="applicationType"></param>
        /// <returns></returns>
        public static string GetProductUrl(Maticsoft.Model.Shop.Products.ProductInfo productInfo, string pageName = "Product", ApplicationKeyType applicationType = ApplicationKeyType.Shop)
        {
            PageSetting pageSetting = new PageSetting();
            Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new BLL.Shop.Products.CategoryInfo();
            Maticsoft.Model.Shop.Products.CategoryInfo CategoryInfo = cateBll.GetModelExCache(productInfo.CategoryId);
            string seoUrl = "";
            if (!String.IsNullOrWhiteSpace(productInfo.SeoUrl))
            {
                if (productInfo.SeoUrl.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                    seoUrl = ReplaceHostName(productInfo.SeoUrl);
            }
            else if (!String.IsNullOrWhiteSpace(CategoryInfo.SeoUrl))
            {
                if (CategoryInfo.SeoUrl.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                    seoUrl = ReplaceHostName(CategoryInfo.SeoUrl);
            }
            else
            {
                seoUrl = ConfigSystem.GetValueByCache(
               string.Format(KeyRule, applicationType, pageName, BaseKeyUrl),
               applicationType);
                if (string.IsNullOrWhiteSpace(seoUrl))
                    seoUrl = ConfigSystem.GetValueByCache(BaseKeyDescription, ApplicationKeyType.System);
                if (seoUrl.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
                    return ReplaceHostName(seoUrl);
            }
            pageSetting._url = seoUrl;
            return pageSetting.ReplaceURL(
              new[] { PageSetting.RKEY_CNAME, productInfo.ProductName },        //产品名称
              new[] { PageSetting.RKEY_CID, productInfo.ProductId.ToString() }, //产品ID
              new[] { PageSetting.RKEY_CATEPATH, CategoryInfo.NamePath }); //分类NamePath

        }
       
        /// <summary>
        /// 获取产品路径
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="productInfo"></param>
        /// <param name="applicationType"></param>
        /// <returns></returns>
        public static string GetProductUrl(long productId, string pageName = "Product", ApplicationKeyType applicationType = ApplicationKeyType.Shop)
        {
            Maticsoft.BLL.Shop.Products.ProductInfo productBll = new BLL.Shop.Products.ProductInfo();
            Maticsoft.Model.Shop.Products.ProductInfo ProductInfo = productBll.GetModelByCache(productId);
            if (ProductInfo != null)
            {
                return GetProductUrl(ProductInfo);
            }
            else
                return "";

        }

        /// <summary>
        /// 获取文章路径
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="productInfo"></param>
        /// <param name="applicationType"></param>
        /// <returns></returns>
        //public static string GetCMSUrl(int NewId, string pageName = "CMS", ApplicationKeyType applicationType = ApplicationKeyType.Shop)
        //{
        //    Maticsoft.BLL.CMS.Content contentBll = new BLL.CMS.Content();
        //    Maticsoft.Model.CMS.Content contentModel = contentBll.GetModelByCache(NewId);
        //   if (contentModel != null)
        //    {
        //        PageSetting pageSetting = new PageSetting();
        //        Maticsoft.BLL.CMS.ContentClass cateBll = new BLL.CMS.ContentClass();
        //      Maticsoft.Model.CMS.ContentClass ClassInfo = cateBll.GetModelExCache(contentModel.ClassID);
        //        string seoUrl = "";
        //        if (!String.IsNullOrWhiteSpace(contentModel.SeoUrl))
        //        {
        //            if (contentModel.SeoUrl.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
        //                seoUrl = ReplaceHostName(contentModel.SeoUrl);
        //        }
        //        else if (!String.IsNullOrWhiteSpace(ClassInfo.SeoUrl))
        //        {
        //            if (ClassInfo.SeoUrl.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
        //                seoUrl = ReplaceHostName(ClassInfo.SeoUrl);
        //        }
        //        else
        //        {
        //            seoUrl = ConfigSystem.GetValueByCache(
        //           string.Format(KeyRule, applicationType, pageName, BaseKeyUrl),
        //           applicationType);
        //            if (string.IsNullOrWhiteSpace(seoUrl))
        //                seoUrl = ConfigSystem.GetValueByCache(BaseKeyDescription, ApplicationKeyType.System);
        //            if (seoUrl.IndexOf(RKEY_HOSTNAME, System.StringComparison.Ordinal) > -1)
        //                return ReplaceHostName(seoUrl);
        //        }
        //        pageSetting._url = seoUrl;
        //        return pageSetting.ReplaceURL(
        //          new[] { PageSetting.RKEY_CNAME, contentModel.Title },        //产品名称
        //          new[] { PageSetting.RKEY_CID, contentModel.ContentID.ToString() }, //产品ID
        //          new[] { PageSetting.RKEY_CATEPATH, ClassInfo.NamePath }); //分类NamePath

        //    }
        //    else
        //        return "";

        //}
        #endregion

        /// <summary>
        /// 获取商品路径
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static string GetProStaticUrl(int productID)
        {
            //Maticsoft.BLL.Shop.Products.ProductInfo proBll = new BLL.Shop.Products.ProductInfo();
            //Maticsoft.Model.Shop.Products.ProductInfo proModel = proBll.GetModelByCache(id);
            //Maticsoft.BLL.Shop.Products.ProductCategories cateBll = new BLL.Shop.Products.ProductCategories();
            //if (proModel != null)
            //{
            //    string proUrl = proBll.GetProductUrl(proModel) + ".html";
            //    string root = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ShopStaticRoot");
            //    root = root.LastIndexOf("/") > -1 ? root : (root + "/");
            //    string url = root + cateBll.GetClassUrl(proModel.ProductId) + "/" + proUrl;
            //    return url.Replace("--", "-").ToLower();
            //    return "";
            //}
            //else
            //{
            //    return "";
            //}

            Maticsoft.BLL.Shop.Products.ProductInfo proBll = new BLL.Shop.Products.ProductInfo();
            Maticsoft.Model.Shop.Products.ProductInfo proModel = proBll.GetModelByCache(productID);
            Maticsoft.BLL.Shop.Products.CategoryInfo cateBll = new BLL.Shop.Products.CategoryInfo();
            Maticsoft.BLL.Shop.Products.ProductCategories pcBll = new BLL.Shop.Products.ProductCategories ();


            if (proModel != null)
            {
                string proUrl = proBll.GetProductUrl(proModel) + ".html";
                string root = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ShopStaticRoot");
                root = root.LastIndexOf("/") > -1 ? root : (root + "/");
                #region 获取类别ID
                Maticsoft.Model.Shop.Products.ProductCategories cate = pcBll.GetModel(productID);
                #endregion
                string url = "";
                if (cate != null)
                {
               
                     url = root + cateBll.GetClassUrl(int.Parse(cate.CategoryId.ToString())) + "/" + proUrl;
                }
                return url.Replace("--", "-").ToLower();
                //return "";
            }
            else
            {
                return "";
            }
            
        }
    }
}
