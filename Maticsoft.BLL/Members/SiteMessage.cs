using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using Maticsoft.Model.Members;
using Maticsoft.DALFactory;
using Maticsoft.IDAL.Members;
using Webdiyer.WebControls.Mvc;
namespace Maticsoft.BLL.Members
{
	/// <summary>
	/// 站内信
	/// </summary>
	public partial class SiteMessage
	{
        private readonly ISiteMessage dal = DAMembers.CreateSiteMessage();
		public SiteMessage()
		{}
		#region  Method

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
		public bool Exists(int ID)
		{
			return dal.Exists(ID);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(Maticsoft.Model.Members.SiteMessage model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Members.SiteMessage model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			return dal.Delete(ID);
		}
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			return dal.DeleteList(Common.Globals.SafeLongFilter(IDlist ,0) );
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Members.SiteMessage GetModel(int ID)
		{
			
			return dal.GetModel(ID);
		}

		/// <summary>
		/// 得到一个对象实体，从缓存中
		/// </summary>
		public Maticsoft.Model.Members.SiteMessage GetModelByCache(int ID)
		{
			
			string CacheKey = "SiteMessageModel-" + ID;
			object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);
			if (objModel == null)
			{
				try
				{
					objModel = dal.GetModel(ID);
					if (objModel != null)
					{
                        int ModelCache = Globals.SafeInt(BLL.SysManage.ConfigSystem.GetValueByCache("ModelCache"), 30);
						Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);
					}
				}
				catch{}
			}
			return (Maticsoft.Model.Members.SiteMessage)objModel;
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Members.SiteMessage> GetModelList(string strWhere)
		{
			DataSet ds = dal.GetList(strWhere);
			return DataTableToList(ds.Tables[0]);
		}
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public List<Maticsoft.Model.Members.SiteMessage> DataTableToList(DataTable dt)
		{
			List<Maticsoft.Model.Members.SiteMessage> modelList = new List<Maticsoft.Model.Members.SiteMessage>();
			int rowsCount = dt.Rows.Count;
			if (rowsCount > 0)
			{
				Maticsoft.Model.Members.SiteMessage model;
				for (int n = 0; n < rowsCount; n++)
				{
					model = new Maticsoft.Model.Members.SiteMessage();
					if(dt.Rows[n]["ID"]!=null && dt.Rows[n]["ID"].ToString()!="")
					{
						model.ID=int.Parse(dt.Rows[n]["ID"].ToString());
					}
					if(dt.Rows[n]["SenderID"]!=null && dt.Rows[n]["SenderID"].ToString()!="")
					{
						model.SenderID=int.Parse(dt.Rows[n]["SenderID"].ToString());
					}
					if(dt.Rows[n]["ReceiverID"]!=null && dt.Rows[n]["ReceiverID"].ToString()!="")
					{
                        model.ReceiverID = int.Parse(dt.Rows[n]["ReceiverID"].ToString());
					}
					if(dt.Rows[n]["Title"]!=null && dt.Rows[n]["Title"].ToString()!="")
					{
					model.Title=dt.Rows[n]["Title"].ToString();
					}
					if(dt.Rows[n]["Content"]!=null && dt.Rows[n]["Content"].ToString()!="")
					{
					model.Content=dt.Rows[n]["Content"].ToString();
					}
					if(dt.Rows[n]["MsgType"]!=null && dt.Rows[n]["MsgType"].ToString()!="")
					{
					model.MsgType=dt.Rows[n]["MsgType"].ToString();
					}
					if(dt.Rows[n]["SendTime"]!=null && dt.Rows[n]["SendTime"].ToString()!="")
					{
						model.SendTime=DateTime.Parse(dt.Rows[n]["SendTime"].ToString());
					}
					if(dt.Rows[n]["ReadTime"]!=null && dt.Rows[n]["ReadTime"].ToString()!="")
					{
						model.ReadTime=DateTime.Parse(dt.Rows[n]["ReadTime"].ToString());
					}
					if(dt.Rows[n]["ReceiverIsRead"]!=null && dt.Rows[n]["ReceiverIsRead"].ToString()!="")
					{
						if((dt.Rows[n]["ReceiverIsRead"].ToString()=="1")||(dt.Rows[n]["ReceiverIsRead"].ToString().ToLower()=="true"))
						{
						model.ReceiverIsRead=true;
						}
						else
						{
							model.ReceiverIsRead=false;
						}
					}
					if(dt.Rows[n]["SenderIsDel"]!=null && dt.Rows[n]["SenderIsDel"].ToString()!="")
					{
						if((dt.Rows[n]["SenderIsDel"].ToString()=="1")||(dt.Rows[n]["SenderIsDel"].ToString().ToLower()=="true"))
						{
						model.SenderIsDel=true;
						}
						else
						{
							model.SenderIsDel=false;
						}
					}
					if(dt.Rows[n]["ReaderIsDel"]!=null && dt.Rows[n]["ReaderIsDel"].ToString()!="")
					{
						if((dt.Rows[n]["ReaderIsDel"].ToString()=="1")||(dt.Rows[n]["ReaderIsDel"].ToString().ToLower()=="true"))
						{
						model.ReaderIsDel=true;
						}
						else
						{
							model.ReaderIsDel=false;
						}
					}
					if(dt.Rows[n]["Ext1"]!=null && dt.Rows[n]["Ext1"].ToString()!="")
					{
					model.Ext1=dt.Rows[n]["Ext1"].ToString();
					}
					if(dt.Rows[n]["Ext2"]!=null && dt.Rows[n]["Ext2"].ToString()!="")
					{
					model.Ext2=dt.Rows[n]["Ext2"].ToString();
					}
                    if (dt.Columns.Contains("SenderUserName")&& dt.Rows[n]["SenderUserName"] != null && dt.Rows[n]["SenderUserName"].ToString() != "")
					{
					model.SenderUserName=dt.Rows[n]["SenderUserName"].ToString();
					}
                    if (dt.Columns.Contains("ReceiverUserName") && dt.Rows[n]["ReceiverUserName"] != null && dt.Rows[n]["ReceiverUserName"].ToString() != "")
					{
					model.ReceiverUserName=dt.Rows[n]["ReceiverUserName"].ToString();
					}
					modelList.Add(model);
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
			return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		//public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		//{
			//return dal.GetList(PageSize,PageIndex,strWhere);
		//}

		#endregion  Method

        #region 站内信要用的方法

        /// <summary>
        /// 后台管理员发送信息的数量
        /// </summary>
        /// <param name="AdminID">管理员的ID</param>
        /// <returns></returns>
        public int GetAdminSendMsgCount(int AdminID)
        {
            return dal.GetAdminSendMsgCount(AdminID);
        }

        /// <summary>
        /// 管理员发送的全部信息的列表
        /// </summary>
        /// <param name="AdminID">管理员的ID</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetAdminSendList(int AdminID)
        {
            return DataTableToList(dal.GetAdminSendList(AdminID).Tables[0]);
        }

        /// <summary>
        /// 管理员发送的全部信息的列表
        /// </summary>
        /// <param name="AdminID">管理员的ID</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetAdminSendList(int AdminID,string KeyWord)
        {
            return DataTableToList(dal.GetAdminSendList(AdminID,KeyWord).Tables[0]);
        }
        /// <summary>
        /// 管理员发送系统消息的分页
        /// </summary>
        /// <param name="AdminID">管理员ID</param>
        /// <param name="StartIndex">索引的index</param>
        /// <param name="EndIndex"></param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetAdminSendListByPage(int AdminID, int StartIndex, int EndIndex)
        {
            return DataTableToList(dal.GetAdminSendListByPage(AdminID,StartIndex,EndIndex).Tables[0]);
           
        }
        /// <summary>
        /// 得到全部接收到的站内信的数量，包括未读的和已读的
        /// </summary>
        /// <param name="RecevieID">接受者的ID</param>
        /// <param name="AdminID">管理员ID</param>
        /// <returns></returns>
        public int GetAllReceiveMsgCount(int RecevieID, int AdminID)
        {
            return dal.GetAllReceiveMsgCount(RecevieID, AdminID);
        }

	    /// <summary>
	    /// 得到全部接收到的站内信的数量，包括未读的和已读的(包括系统消息)
	    /// </summary>
	    /// <param name="RecevieID">接收者的ID</param>
	    /// <param name="AdminID">管理员ID</param>
	    /// <returns></returns>
	    public int GetAllReceiveMsgCount(int RecevieID)
	    {
	        return dal.GetAllReceiveMsgCount(RecevieID);
	    }

	    /// <summary>
        /// 用户接收到的全部站内信（包括全部的已读和未读的信息）
        /// </summary>
        /// <param name="RecevieID">接收者ID</param>
        /// <param name="AdminID">后台管理员的ID</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetAllReceiveMsgList(int RecevierID, int AdminID)
       {
           return DataTableToList(dal.GetAllReceiveMsgList(RecevierID, AdminID).Tables[0]);
     
       }

      /// <summary>
       /// 用户接收到的全部站内信分页（包括全部的已读和未读的信息）
      /// </summary>
      /// <param name="RecevierID">接受者id</param>
      /// <param name="AdminID">后台用户id</param>
      /// <param name="StartIndex">开始的index</param>
      /// <param name="EndIndex">结束的index</param>
      /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetAllReceiveMsgListByPage(int RecevierID, int AdminID, int StartIndex, int EndIndex)
       {
           return DataTableToList(dal.GetAllReceiveMsgListByPage(RecevierID, AdminID,StartIndex,EndIndex).Tables[0]);

       }

	    /// <summary>
	    /// 用户接收到的全部站内信分页（包括全部的已读和未读的信息 也包括系统消息）
	    /// </summary>
	    /// <param name="RecevierID">接受者id</param>
	    /// <param name="StartIndex">开始的index</param>
	    /// <param name="EndIndex">结束的index</param>
	    /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetAllReceiveMsgListByPage(int RecevierID, int StartIndex, int EndIndex)
	    {
            return DataTableToList(dal.GetAllReceiveMsgListByPage(RecevierID, StartIndex, EndIndex).Tables[0]);
	    }

	    /// <summary>
       /// 用户发送的全部站内信
       /// </summary>
       /// <param name="RecevieID">接收者ID</param>
       /// <param name="AdminID">后台管理员的ID</param>
       /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetAllSendMsgList(int SenderID)
       {
           return DataTableToList(dal.GetAllSendMsgList(SenderID).Tables[0]);

       }
        /// <summary>
       ///  用户发送的全部站内信分页
        /// </summary>
        /// <param name="SenderID">发送者ID</param>
        /// <param name="StartIndex">开始index</param>
        /// <param name="EndIndex">结束index</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetAllSendMsgListByPage(int SenderID, int StartIndex, int EndIndex)
       {
           return DataTableToList(dal.GetAllSendMsgListByPage(SenderID,StartIndex,EndIndex).Tables[0]);

       }
       /// <summary>
       /// 得到全部的系统消息的个数，包括已读的和未读的（点对面）
       /// </summary>
       /// <param name="RecevieID">接受者的ID</param>
       /// <param name="AdminID">管理员ID</param>
       /// <param name="UserType">用户的类型</param>
       /// <returns>系统的个数</returns>
       public int GetAllSystemMsgCount(int ReceiverID, int AdminId, string UserType)
       {
           return dal.GetAllSystemMsgCount(ReceiverID, AdminId, UserType);
       } 

        /// <summary>
       /// 得到全部的系统消息的列表，包括已读的和未读的（店对面）
       /// </summary>
       /// <param name="RecevieID">接受者的ID</param>
       /// <param name="AdminID">管理员ID</param>
      /// <param name="UserType">用户的类型</param>
       /// <returns></returns>
       public List<Maticsoft.Model.Members.SiteMessage> GetAllSystemMsgList(int ReceiverID, int AdminId, string UserType)
       {
           return DataTableToList(dal.GetAllSystemMsgList(ReceiverID,AdminId,UserType).Tables[0]);
       }

        /// <summary>
        /// 分页得到系统消息(点对面)
        /// </summary>
        /// <param name="ReceiverID">用户id</param>
        /// <param name="AdminId">管理员id</param>
        /// <param name="UserType"></param>
        /// <param name="StartIndex"></param>
        /// <param name="EndIndex"></param>
        /// <returns></returns>
       public List<Maticsoft.Model.Members.SiteMessage> GetAllSystemMsgListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex)
       {
           return DataTableToList(dal.GetAllSystemMsgListByPage(ReceiverID, AdminId, UserType,StartIndex,EndIndex).Tables[0]);
       }
        /// <summary>
        /// 得到已读信息的列表（点对点）
        /// </summary>
        /// <param name="ReceiverID">收信人的id</param>
        /// <param name="AdminId">管理员的id</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetReceiveMsgAlreadyReadList(int ReceiverID, int AdminId)
        {
            return DataTableToList(dal.GetReceiveMsgAlreadyReadList(ReceiverID, AdminId).Tables[0]); 
        
        }
        /// <summary>
        /// 已读信息的列表分页情况（点对点）
        /// </summary>
        /// <param name="ReceiverID">接受用户id</param>
        /// <param name="AdminId">管理员id</param>
        /// <param name="StartIndex">开始的index</param>
        /// <param name="EndIndex">结束的index</param>
        /// <returns></returns>
       public List<Maticsoft.Model.Members.SiteMessage> GetReceiveMsgAlreadyReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex)
        {
            return DataTableToList(dal.GetReceiveMsgAlreadyReadListByPage(ReceiverID, AdminId,StartIndex,EndIndex).Tables[0]); 
        }
        /// <summary>
        /// 得到用户已读信息的个数（点对点）
        /// </summary>
        /// <param name="ReceiverID">接受者id</param>
        /// <param name="AdminId">后台管理员id</param>
        /// <returns></returns>
        public int GetReceiveMsgAreadyReadCount(int ReceiverID, int AdminId)
        {
            return dal.GetReceiveMsgAreadyReadCount(ReceiverID, AdminId);
        }
        /// <summary>
        ///未读信息的个数（点对点）
        /// </summary>
        /// <param name="ReceiverID">发送者ID</param>
        /// <param name="AdminId">管理员ID</param>
        /// <returns></returns>
        public int GetReceiveMsgNotReadCount(int ReceiverID, int AdminId)
        {
            return dal.GetReceiveMsgNotReadCount(ReceiverID, AdminId);
        
        }
        /// <summary>
        /// 未读信息的列表（点对点）
        /// </summary>
        /// <param name="ReceiverID">接受者id</param>
        /// <param name="AdminId">后台管理员id</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetReceiveMsgNotReadList(int ReceiverID, int AdminId)
        {
            return DataTableToList(dal.GetReceiveMsgNotReadList(ReceiverID, AdminId).Tables[0]); 
        
        } 
        /// <summary>
        /// 未读信息的列表分页（点对点）
        /// </summary>
        /// <param name="ReceiverID">接受者id</param>
        /// <param name="AdminId">后台管理员id</param>
       /// <param name="StartIndex">开始的index</param>
        /// <param name="EndIndex">结束的index</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetReceiveMsgNotReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex)
        {
            return DataTableToList(dal.GetReceiveMsgNotReadListByPage(ReceiverID, AdminId,StartIndex,EndIndex).Tables[0]); 
        
        }
        /// <summary>
        /// 得到发送消息的数量（点对点）
        /// </summary>
        /// <param name="SenderID">发送者ID</param>
        /// <returns></returns>
        public int GetSendMsgCount(int SenderID)
        {
            return dal.GetSendMsgCount(SenderID);
        } 
        /// <summary>
            /// 已读系统消息的个数(点对面)
        /// </summary>
        /// <param name="ReceiverID">接受者ID</param>
        /// <param name="AdminId">管理员ID</param>
        /// <param name="UserType">用户的类型</param>
        /// <returns></returns>
        public  int GetSystemMsgAlreadyReadCount(int ReceiverID, int AdminId, string UserType)
        {
            return dal.GetSystemMsgAlreadyReadCount(ReceiverID, AdminId, UserType);
        }
        /// <summary>
        /// 得到已读系统信息的列表
        /// </summary>
        /// <param name="ReceiverID">发送者ID</param>
        /// <param name="AdminId">管理员ID</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetSystemMsgAlreadyReadList(int ReceiverID, int AdminId, string UserType)
        {
            return DataTableToList(dal.GetSystemMsgAlreadyReadList(ReceiverID, AdminId,UserType).Tables[0]); 
          
        }  
        /// <summary>
        /// 得到已读系统信息的列表分页(点对面)
        /// </summary>
        /// <param name="ReceiverID">发送者ID</param>
        /// <param name="AdminId">管理员ID</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetSystemMsgAlreadyReadListByPage(int ReceiverID, int AdminId, string UserType,int StartIndex,int EndIndex)
        {
            return DataTableToList(dal.GetSystemMsgAlreadyReadListByPage(ReceiverID, AdminId, UserType,StartIndex,EndIndex).Tables[0]); 
        }
        /// <summary>
        /// 得到未读系统信息的数量（点对面）
        /// </summary>
        /// <param name="ReceiverID">接受者的id</param>
        /// <param name="AdminId">后台管理员的id</param>
        /// <param name="UserType">用户的类型</param>
        /// <returns></returns>
        public int GetSystemMsgNotReadCount(int ReceiverID, int AdminId, string UserType)
        {
            return dal.GetSystemMsgNotReadCount(ReceiverID,AdminId,UserType);
        
        }
        /// <summary>
        /// 未读系统消息列表（点对面）
        /// </summary>
        /// <param name="ReceiverID">接受者ID</param>
        /// <param name="AdminId">管理员ID</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetSystemMsgNotReadList(int ReceiverID, int AdminId, string UserType)
        {
            return DataTableToList(dal.GetSystemMsgNotReadList(ReceiverID,AdminId,UserType).Tables[0]);
        
        }
        /// <summary>
        /// 未读系统消息列表分页（点对面）
        /// </summary>
        /// <param name="ReceiverID">接受者ID</param>
        /// <param name="AdminId">管理员ID</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public List<Maticsoft.Model.Members.SiteMessage> GetSystemMsgNotReadListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex)
        {
            return DataTableToList(dal.GetSystemMsgNotReadListByPage(ReceiverID, AdminId, UserType,StartIndex,EndIndex).Tables[0]);
        }
        /// <summary>
        ///   设置系统消息的某条为删除状态（管理员操作）
        /// </summary>
        /// </summary>
        /// <param name="ID">此条系统消息的id</param>
        /// <param name="AdminID">管理员id</param>
        /// <returns></returns>
        public int SetAdminMsgToDelById(int ID, int AdminID)
        {
            return dal.SetAdminMsgToDelById(ID, AdminID);
           
        }
        /// <summary>
        /// 设置某短信息的状态为已读
        /// </summary>
        /// <param name="ID">id</param>
        /// <param name="AdminID">管理员id</param>
        /// <returns></returns>
        public int SetReceiveMsgAlreadyRead(int ID)
        {
            return dal.SetReceiveMsgAlreadyRead(ID);
        
        }

        /// <summary>
        /// 设置收到短信息的状态为删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int SetReceiveMsgToDelById(int ID)
        {
            return dal.SetReceiveMsgToDelById(ID);
        
        } 
        /// <summary>
        /// 发送消息的方法（点对点）
        /// </summary>
        /// <param name="SendID">发送者ID</param>
        /// <param name="ReceiverID">接收者ID</param>
        /// <param name="Title">标题</param>
        /// <param name="Content">内容</param>
        /// <returns>增加消息的ID</returns>
        public int AddMessageByUser(int SendID,int ReceiverID,string Title, string Content)
        {
            Maticsoft.Model.Members.SiteMessage MessageModel = new Model.Members.SiteMessage();
            MessageModel.Content = Content;
            MessageModel.Title=Title;
            MessageModel.ReceiverID = ReceiverID;
            MessageModel.SenderID = SendID;
            MessageModel.SendTime = DateTime.Now;
            MessageModel.ReceiverIsRead = false;
            MessageModel.SenderIsDel = false;
            MessageModel.ReaderIsDel = false;
            return  dal.Add(MessageModel);
        
        
        }
        /// <summary>
        /// 设置发出短信息的状态为删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int SetSendMsgToDelById(int ID)
        {
            return dal.SetSendMsgToDelById(ID);
        
        }
        /// <summary>
       ///  设置某条系统消息为已读状态
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="ReceiverID">接收者id</param>
        /// <param name="AdminId">管理员id</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public int SetSystemMsgStateToAlreadyRead(int ID, int ReceiverID,  string UserType)
        {
            return dal.SetSystemMsgStateToAlreadyRead(ID,ReceiverID,UserType);
        }
        /// <summary>
        ///  设置某条系统消息为删除状态
        /// </summary>
        /// <param name="ID">ID</param>
        /// <param name="ReceiverID">接收者id</param>
        /// <param name="AdminId">管理员id</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public int SetSystemMsgStateToDel(int ID, int ReceiverID, string UserType)
        {
            return dal.SetSystemMsgStateToDel(ID, ReceiverID, UserType);
        } 
	#endregion

        #region 计算所以的辅助方法
        public int GetStartPageIndex(int PageSize, int PageIndex)
        {
            return PageSize * (PageIndex - 1) + 1;

        }

        public int GetEndPageIndex(int PageSize, int PageIndex)
        {

            return PageSize * PageIndex;

        } 
        #endregion

        #region mvc下实现分页

        /// <summary>
        /// 非管理员信息
        /// QHQ
        /// 得到用户的所有收到的信息
        /// 得到收件箱的全部内容（不是系统消息）实现了杨涛的分页mvc下的分页
        /// </summary>
        /// <param name="ReceiverID">接收者ID</param>
        /// <param name="AdminID">管理员ID</param>
        /// <param name="PageSize">每页显示的条数</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public PagedList<Model.Members.SiteMessage> GetAllReceiveMsgListByMvcPage(int ReceiverID,int AdminID,int PageSize,int PageIndex)
        {
            List<Maticsoft.Model.Members.SiteMessage> list = GetAllReceiveMsgListByPage(ReceiverID, AdminID, GetStartPageIndex(PageSize, PageIndex), GetEndPageIndex(PageSize, PageIndex));
            PagedList<Model.Members.SiteMessage> PageList = new PagedList<Model.Members.SiteMessage>(list, PageIndex, PageSize, GetAllReceiveMsgCount(ReceiverID, AdminID));
            return PageList;
        }
        /// <summary>
        /// 用户接收到的全部站内信分页（包括全部的已读和未读的信息 也包括系统消息）
        /// </summary>
        /// <param name="RecevierID">接受者id</param>
        /// <param name="StartIndex">开始的index</param>
        /// <param name="EndIndex">结束的index</param>
        /// <returns></returns>
        public PagedList<Model.Members.SiteMessage> GetAllReceiveMsgListByMvcPage(int ReceiverID, int PageSize, int PageIndex)
        {
            List<Maticsoft.Model.Members.SiteMessage> list = GetAllReceiveMsgListByPage(ReceiverID, GetStartPageIndex(PageSize, PageIndex), GetEndPageIndex(PageSize, PageIndex));
            PagedList<Model.Members.SiteMessage> PageList = new PagedList<Model.Members.SiteMessage>(list, PageIndex, PageSize, GetAllReceiveMsgCount(ReceiverID));
            return PageList;
        }

      /// <summary>
      /// QHQ
      /// 得到用户发出去的所有信息
      /// 得到所有发件箱的内容实现了杨涛的分页mvc下的分页
      /// </summary>
      /// <param name="SenderID">发送者ID</param>
      /// <param name="PageSize">每页显示的条数</param>
      /// <param name="PageIndex">当前页码</param>
      /// <returns></returns>
        public PagedList<Model.Members.SiteMessage> GetAllSendMsgListByMvcPage(int SenderID, int PageSize, int PageIndex)
        {
            PagedList<Model.Members.SiteMessage> PageList = new PagedList<Model.Members.SiteMessage>(GetAllSendMsgListByPage(SenderID,
                GetStartPageIndex(PageSize, PageIndex), GetEndPageIndex(PageSize, PageIndex)), PageIndex, PageSize, GetSendMsgCount(SenderID));
            return PageList;
        }

        /// <summary>
        /// 收件箱未读的内容（除系统消息）实现了杨涛的分页mvc下的分页
        /// </summary>
        /// <param name="ReceiverID">收件人id</param>
        /// <param name="AdminID">管理员id</param>
        /// <param name="PageSize">每页显示多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public PagedList<Model.Members.SiteMessage> GetAllReceiveMsgNotReadListByMvcPage(int ReceiverID, int AdminID, int PageSize, int PageIndex)
        {
            PagedList<Model.Members.SiteMessage> PageList = new PagedList<Model.Members.SiteMessage>(GetReceiveMsgNotReadListByPage(ReceiverID,AdminID,
                GetStartPageIndex(PageSize, PageIndex), GetEndPageIndex(PageSize, PageIndex)), PageIndex, GetReceiveMsgNotReadCount(ReceiverID,AdminID));
            return PageList;
        }

        /// <summary>
        /// 收件箱未读的内容（除系统消息）实现了杨涛的分页mvc下的分页
        /// </summary>
        /// <param name="ReceiverID">收件人id</param>
        /// <param name="AdminID">管理员id</param>
        /// <param name="PageSize">每页显示多少条</param>
        /// <param name="PageIndex">当前页码</param>
        /// <returns></returns>
        public PagedList<Model.Members.SiteMessage> GetAllReceiveMsgAlReadReadyListByMvcPage(int ReceiverID, int AdminID, int PageSize, int PageIndex)
        {
            PagedList<Model.Members.SiteMessage> PageList = new PagedList<Model.Members.SiteMessage>(GetReceiveMsgAlreadyReadListByPage(ReceiverID, AdminID,
                GetStartPageIndex(PageSize, PageIndex), GetEndPageIndex(PageSize, PageIndex)), PageIndex, GetReceiveMsgAreadyReadCount(ReceiverID, AdminID));
            return PageList;
        }

      /// <summary>
      /// QHQ
      /// 获取系统
      /// /得到系统消息的列表表实现了杨涛的分页mvc下的分页
      /// </summary>
      /// <param name="ReceiverID">收件人id</param>
      /// <param name="AdminID">管理员id</param>
      /// <param name="UserType">用户的类型</param>
      /// <param name="PageSize">每页显示多少条</param>
      /// <param name="PageIndex">当前的页码</param>
      /// <returns></returns>
        public PagedList<Model.Members.SiteMessage> GetSystemMsgNotReadListByMvcPage(int ReceiverID, int AdminID, string UserType, int PageSize, int PageIndex)
        {
            PagedList<Model.Members.SiteMessage> PageList = new PagedList<Model.Members.SiteMessage>(GetSystemMsgNotReadListByPage(ReceiverID, AdminID,UserType,
                GetStartPageIndex(PageSize, PageIndex), GetEndPageIndex(PageSize, PageIndex)), PageIndex, GetSystemMsgNotReadCount(ReceiverID, AdminID,UserType));
            return PageList;
        }
        /// <summary>
        /// /得到已读系统消息的列表表实现了杨涛的分页mvc下的分页
        /// </summary>
        /// <param name="ReceiverID">收件人id</param>
        /// <param name="AdminID">管理员id</param>
        /// <param name="UserType">用户的类型</param>
        /// <param name="PageSize">每页显示多少条</param>
        /// <param name="PageIndex">当前的页码</param>
        /// <returns></returns>
        public PagedList<Model.Members.SiteMessage> GetSystemMsgAlreadyReadListByMvcPage(int ReceiverID, int AdminID, string UserType, int PageSize, int PageIndex)
        {
            PagedList<Model.Members.SiteMessage> PageList = new PagedList<Model.Members.SiteMessage>(GetSystemMsgAlreadyReadListByPage(ReceiverID, AdminID, UserType, GetStartPageIndex(PageSize, PageIndex), GetEndPageIndex(PageSize, PageIndex)), PageIndex, GetSystemMsgAlreadyReadCount(ReceiverID, AdminID, UserType));
            return PageList;
        }
        /// <summary>
        /// /得到全部系统消息的列表表实现了杨涛的分页mvc下的分页
        /// </summary>
        /// <param name="ReceiverID">收件人id</param>
        /// <param name="AdminID">管理员id</param>
        /// <param name="UserType">用户的类型</param>
        /// <param name="PageSize">每页显示多少条</param>
        /// <param name="PageIndex">当前的页码</param>
        /// <returns></returns>
        public PagedList<Model.Members.SiteMessage> GetAllSystemMsgListByMvcPage(int ReceiverID, int AdminID, string UserType, int PageSize, int PageIndex)
        {
             //List<Maticsoft.Model.Members.SiteMessage> list=
            int counts = GetAllSystemMsgCount(ReceiverID, AdminID, UserType);
            List<Model.Members.SiteMessage> list=  GetAllSystemMsgListByPage(ReceiverID, AdminID, UserType, GetStartPageIndex(PageSize, PageIndex),
                                      GetEndPageIndex(PageSize, PageIndex));
            PagedList<Model.Members.SiteMessage> PageList = new PagedList<Model.Members.SiteMessage>(list , PageIndex,PageSize, counts);
            return PageList;
            // PagedList<Model.Members.SiteMessage> PageList1 = new PagedList<Model.Members.SiteMessage>()
        }
	#endregion
	}
}

