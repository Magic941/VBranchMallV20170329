/**
* NotifyEventHandler.cs
*
* 功 能： 回调/通知委托
* 类 名： NotifyEventHandler
*
* Ver   变更日期    部门      担当者 变更内容
* ─────────────────────────────────
* V0.01 2012/01/13  研发部    姚远   初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌─────────────────────────────────┐
*│ 此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露． │
*│ 版权所有：动软卓越（北京）科技有限公司                           │
*└─────────────────────────────────┘
*/
using Maticsoft.Payment.Core;

namespace Maticsoft.Payment.Handler
{
    public delegate void NotifyEventHandler(NotifyQuery sender);
}

