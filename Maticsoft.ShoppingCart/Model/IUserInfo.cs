/**
* IUserInfo.cs
*
* 功 能： 用户信息接口
* 类 名： IUserInfo
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/5/20 15:29:31  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

namespace Maticsoft.ShoppingCart.Model
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    public interface IUserInfo
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        int UserId { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        string Email { get; set; }

        /// <summary>
        /// 名称 - 用户名/真实姓名
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// 座机
        /// </summary>
        string TelPhone { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        string CellPhone { get; set; }
    }
}
