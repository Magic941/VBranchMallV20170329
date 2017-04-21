/**
* Command.cs
*
* 功 能： N/A
* 类 名： Command
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/7/29 15:35:22   N/A    初版
*
* Copyright (c) 2012-2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Data;
using System.Collections.Generic;
using System.Text;
using Maticsoft.Common;
using Maticsoft.WeChat.Model.Core;
using Maticsoft.WeChat.IDAL.Core;
using System.Linq;

namespace Maticsoft.WeChat.BLL.Core
{
	/// <summary>
	/// Command
	/// </summary>
	public partial class Command
	{
		private readonly ICommand dal = Maticsoft.DBUtility.PubConstant.IsSQLServer ? (ICommand)new Maticsoft.WeChat.SQLServerDAL.Core.Command() : null;//暂时预留
		public Command()
		{}
        #region  BasicMethod

        /// <summary>
        /// 得到最大ID
        /// </summary>
        public int GetMaxId()
        {
            return dal.GetMaxId();
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int CommandId)
        {
            return dal.Exists(CommandId);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Maticsoft.WeChat.Model.Core.Command model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Maticsoft.WeChat.Model.Core.Command model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int CommandId)
        {

            return dal.Delete(CommandId);
        }
        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool DeleteList(string CommandIdlist)
        {
            return dal.DeleteList(CommandIdlist);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Maticsoft.WeChat.Model.Core.Command GetModel(int CommandId)
        {

            return dal.GetModel(CommandId);
        }

        /// <summary>
        /// 得到一个对象实体，从缓存中
        /// </summary>
        public Maticsoft.WeChat.Model.Core.Command GetModelByCache(int CommandId)
        {

            string CacheKey = "CommandModel-" + CommandId;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = dal.GetModel(CommandId);
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.Command)objModel;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.Command> GetModelList(string strWhere)
        {
            DataSet ds = dal.GetList(strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Maticsoft.WeChat.Model.Core.Command> DataTableToList(DataTable dt)
        {
            List<Maticsoft.WeChat.Model.Core.Command> modelList = new List<Maticsoft.WeChat.Model.Core.Command>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Maticsoft.WeChat.Model.Core.Command model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = dal.DataRowToModel(dt.Rows[n]);
                    if (model != null)
                    {
                        modelList.Add(model);
                    }
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetAllList()
        {
            return GetList("");
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        //public DataSet GetList(int PageSize,int PageIndex,string strWhere)
        //{
        //return dal.GetList(PageSize,PageIndex,strWhere);
        //}

        #endregion  BasicMethod

	    #region  ExtensionMethod

        /// <summary>
        /// 获取指令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static  string GetComName(Maticsoft.WeChat.Model.Core.Command command, string value)
        {
            string name = "";
            switch (command.ParseType)
            {
                    //按长度处理
                case 0:
                    name = value.Substring(0, command.ParseLength);
                    break;
                case 1:
                    name = value.Split(Convert.ToChar(command.ParseChar))[0];
                    break;
            }
            return name;
        }
        /// <summary>
        /// 获取关键字
        /// </summary>
        /// <param name="command"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetKeyWord(Maticsoft.WeChat.Model.Core.Command command, string value)
        {
            string name = "";
            if (command != null && command.CommandId > 0 && !String.IsNullOrWhiteSpace(value))
            {
                switch (command.ParseType)
                {
                    //按长度处理
                    case 0:
                        name = value.Substring(command.ParseLength);
                        break;
                    case 1:
                        name = value.Split(Convert.ToChar(command.ParseChar))[1];
                        break;
                }
            }
       
            return name;
        }
	    /// <summary>
        /// 匹配指令
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Maticsoft.WeChat.Model.Core.Command MatchCommand(Maticsoft.WeChat.Model.Core.RequestMsg msgModel)
	    {
            //如果不是文本消息，就不需要走指令处理
	        if (msgModel.MsgType != "text")
	        {
	            return null;
	        }
	        string CacheKey = "MatchCommand-" + msgModel.Description + msgModel.MsgType;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    //获取所有指令
                     List<Maticsoft.WeChat.Model.Core.Command> AllCommand = Maticsoft.WeChat.BLL.Core.Command.GetAllCommand(msgModel.OpenId);
                    if (AllCommand != null && AllCommand.Count > 0)
                    {
                        AllCommand = AllCommand.OrderBy(c => c.Sequence).ToList();
                        foreach (var command in AllCommand)
                        {
                            if (msgModel.Description.ToLower().StartsWith(command.Name.ToLower()))
                            {
                                objModel = command;
                                break;
                            }
                        }
                    }
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (Maticsoft.WeChat.Model.Core.Command)objModel;
	    }

        /// <summary>
        /// 获取最大顺序值
        /// </summary>
        /// <returns></returns>
	    public int GetSequence(string openId)
	    {
            return dal.GetSequence(openId);
	    }
        /// <summary>
        /// 获取所有可用的指令
        /// </summary>
        /// <returns></returns>
	    public static List<Maticsoft.WeChat.Model.Core.Command> GetAllCommand(string openId)
	    {
            string CacheKey = "GetAllCommand"+openId ;
            object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    Maticsoft.WeChat.BLL.Core.Command commandBll = new Command();
                    objModel = commandBll.GetModelList(" Status=1 and OpenId='" + openId+"'");
                    if (objModel != null)
                    {
                        int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt("ModelCache");
                        Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
                    }
                }
                catch { }
            }
            return (List<Maticsoft.WeChat.Model.Core.Command>)objModel;
	    }

        public static string GetCommandStr(string openId)
	    {
	        List<Maticsoft.WeChat.Model.Core.Command> commandList = GetAllCommand(openId);
            StringBuilder commandStr = new StringBuilder();
            if (commandList != null && commandList.Count>0)
	        {
	            foreach (var command in commandList)
	            {
	                commandStr.Append(command.Name + "  " + command.Remark).Append("\n");
	            }
	        }
	        return commandStr.ToString();
	    }


        public List<Maticsoft.WeChat.Model.Core.Command> GetCommandList(string openId, int status, string keyword, int startIndex, int endIndex)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" OpenId='{0}'", openId);
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strWhere.AppendFormat(" and  Name like '%{0}%'", keyword);
            }
            if (status != -1)
            {
                strWhere.AppendFormat(" and  Status ={0}", status);
            }
            DataSet ds = GetListByPage(strWhere.ToString(), "", startIndex, endIndex);
            return DataTableToList(ds.Tables[0]);
        }

        public int GetCount(string openId, int status, string keyword)
        {
            StringBuilder strWhere = new StringBuilder();
            strWhere.AppendFormat(" OpenId='{0}'", openId);
            if (!String.IsNullOrWhiteSpace(keyword))
            {
                strWhere.AppendFormat(" and  Name like '%{0}%'", keyword);
            }
            if (status != -1)
            {
                strWhere.AppendFormat(" and  Status ={0}", status);
            }

            return GetRecordCount(strWhere.ToString());
        }

	    #endregion  ExtensionMethod
	}
}

