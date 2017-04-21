/**  版本信息模板在安装目录下，可自行修改。
* Shop_Card.cs
*
* 功 能： N/A
* 类 名： Shop_Card
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2014/6/28 16:15:20   lcy    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海真好邻电子商务有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
namespace Maticsoft.Model
{
    /// <summary>
    /// 营销人员，通过手机号和密码登录后获得该对象
    /// </summary>
    [Serializable]
    public class SalesPersonCardsReportModel
    {
        /// <summary>
        ///营销员名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Mobel { get; set; }

        public string Address { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public int SalesId { get; set; }

        /// <summary>
        /// 营销员级别编号
        /// </summary>
        public int LevelId { get; set; }

        /// <summary>
        /// 级别名称
        /// </summary>
        public string LevelName { get; set; }

        public int TotalCardsCount { get; set; }

        public int ActivateCardsCount { get; set; }

        public int NoActivateCardsCount { get; set; }

    }
}

