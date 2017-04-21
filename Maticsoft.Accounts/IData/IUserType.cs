/**
* Class2.cs
*
* 功 能： [N/A]
* 类 名： Class2
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/3/1 19:35:50  Ben    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System.Data;

namespace Maticsoft.Accounts.IData
{
    internal interface IUserType
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string UserType, string Description);

        /// <summary>
        /// 增加一条数据
        /// </summary>
        void Add(string UserType, string Description);

        /// <summary>
        /// 更新一条数据
        /// </summary>
        void Update(string UserType, string Description);

        /// <summary>
        /// 删除一条数据
        /// </summary>
        void Delete(string UserType);

        /// <summary>
        /// 得到类型描述
        /// </summary>
        string GetDescription(string UserType);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);
    }

}
