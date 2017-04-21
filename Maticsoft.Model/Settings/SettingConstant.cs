/**
* SettingConstant.cs
*
* 功 能： 系统参数常量类 用于存储Key和初始值
* 类 名： SettingConstant
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/7/2 11:51:39    Ben    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Drawing;

namespace Maticsoft.Model.Settings
{
    public static class SettingConstant
    {
        /// <summary>
        /// 商品缩略图大小
        /// </summary>
        /// <remarks>SA_Config_System-Key</remarks>
        public const string PRODUCT_THUMB_SIZE_KEY = "ProductThumbImageSize";
        /// <summary>
        /// 商品中尺寸图大小
        /// </summary>
        /// <remarks>SA_Config_System-Key</remarks>
        public const string PRODUCT_NORMAL_SIZE_KEY = "ProductNormalImageSize";
        /// <summary>
        /// 商品缩略图 默认尺寸
        /// </summary>
        /// <remarks>初始化 or 参数异常使用</remarks>
        public static readonly Size ProductThumbSize = new Size(127, 127);
        /// <summary>
        /// 商品中尺寸图 默认尺寸
        /// </summary>
        /// <remarks>初始化 or 参数异常使用</remarks>
        public static readonly Size ProductNormalSize = new Size(300, 300);
    }
}
