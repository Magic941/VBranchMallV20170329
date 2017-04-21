/**
* PageSettingBase.cs
*
* 功 能： 页面设置基类
* 类 名： PageSettingBase
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/16 15:49:53  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

using Maticsoft.BLL.SysManage;
using Maticsoft.Components.Setting;
using Maticsoft.Model.SysManage;

namespace Maticsoft.Web.Components.Setting
{
    public abstract class PageSettingBase : IPageSetting
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
        /// 当前描述 用于列表/详细页面
        /// </summary>
        public const string RKEY_CATEID = "{cateid}";
        /// <summary>
        /// 当前描述 用于列表/详细页面
        /// </summary>
        public const string RKEY_CID = "{cid}";
        public static string GetHostName(ApplicationKeyType applicationType = ApplicationKeyType.System)
        {
            return ConfigSystem.GetValueByCache(WebSiteSet.WEB_NAME, applicationType);
        }
        #endregion

        #region 设置Key
        protected const string KeyRule = "{0}_{1}_{2}";

        protected const string BaseKeyTitle = "Title";
        protected const string BaseKeyKeywords = "Keywords";
        protected const string BaseKeyDescription = "Description";

        public readonly string KeyTitle;
        public readonly string KeyKeywords;
        public readonly string KeyDescription;
        #endregion

        #region 属性
        protected ApplicationKeyType _applicationType;
        protected string _title;
        protected string _description;
        protected string _keywords;

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
                ConfigSystem.Modify(KeyKeywords, value, KeyTitle, _applicationType);
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
                ConfigSystem.Modify(KeyDescription, value, KeyTitle, _applicationType);
            }
        }
        #endregion

        #region 构造
        /// <summary>
        /// 构造页面配置
        /// </summary>
        /// <param name="pageName">页面名称</param>
        /// <param name="applicationType">所在模块</param>
        public PageSettingBase(string pageName, ApplicationKeyType applicationType = ApplicationKeyType.System)
        {
            _applicationType = applicationType;

            //重新初始化Key 根据当前ApplicationKeyType 追加前缀
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
        }
        #endregion

        #region 实例方法

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
        protected static string ReplaceHostName(string target)
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

        #endregion
    }
}
