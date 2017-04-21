/**
* IPageSetting.cs
*
* 功 能： 页面设置接口
* 类 名： IPageSetting
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
namespace Maticsoft.Components.Setting
{
    /// <summary>
    /// 页面设置接口
    /// </summary>
    public interface IPageSetting
    {
        /// <summary>
        /// 页面Title
        /// </summary>
        /// <remarks>赋值时将直接修改DB</remarks>
        string Title { get; set; }

        /// <summary>
        /// 页面Keywords
        /// </summary>
        /// <remarks>赋值时将直接修改DB</remarks>
        string Keywords { get; set; }

        /// <summary>
        /// 页面Description
        /// </summary>
        /// <remarks>赋值时将直接修改DB</remarks>
        string Description { get; set; }

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
        void Replace(params string[][] values);

        /// <summary>
        /// 替换器
        /// 替换标题中指定的动态内容
        /// </summary>
        /// <param name="values">替换键值对象</param>
        /// <remarks>标题替换值长度截取为30字符</remarks>
        /// <returns>替换结果</returns>
        string ReplaceTitle(params string[][] values);

        /// <summary>
        /// 替换器
        /// 替换关键字中指定的动态内容
        /// </summary>
        /// <param name="values">替换键值对象</param>
        /// <returns>替换结果</returns>
        string ReplaceKeywords(params string[][] values);

        /// <summary>
        /// 替换器
        /// 替换说明中指定的动态内容
        /// </summary>
        /// <param name="values">替换键值对象</param>
        /// <remarks>说明替换值长度截取为140字符</remarks>
        /// <returns>替换结果</returns>
        string ReplaceDescription(params string[][] values);
    }
}
