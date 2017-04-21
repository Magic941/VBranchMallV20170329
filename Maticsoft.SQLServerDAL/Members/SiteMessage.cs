using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using Maticsoft.IDAL.Members;
using Maticsoft.DBUtility;
namespace Maticsoft.SQLServerDAL.Members
{
	/// <summary>
	/// 数据访问类:SiteMessage
	/// </summary>
	public partial class SiteMessage:ISiteMessage
	{
		public SiteMessage()
		{}
		#region  Method

		/// <summary>
		/// 得到最大ID
		/// </summary>
		public int GetMaxId()
		{
		return DbHelperSQL.GetMaxID("ID", "SA_SiteMessage"); 
		}

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int ID)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(" select count(1) from SA_SiteMessage");
			strSql.Append("  where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}


		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Maticsoft.Model.Members.SiteMessage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(" insert into SA_SiteMessage(");
			strSql.Append(" SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2)");
			strSql.Append("  values (");
			strSql.Append(" @SenderID,@ReceiverID,@Title,@Content,@MsgType,@SendTime,@ReadTime,@ReceiverIsRead,@SenderIsDel,@ReaderIsDel,@Ext1,@Ext2)");
			strSql.Append(" ;select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@SenderID", SqlDbType.Int,4),
					new SqlParameter("@ReceiverID",SqlDbType.Int,4),
					new SqlParameter("@Title", SqlDbType.NVarChar,300),
					new SqlParameter("@Content", SqlDbType.NVarChar),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
					new SqlParameter("@ReadTime", SqlDbType.DateTime),
					new SqlParameter("@ReceiverIsRead", SqlDbType.Bit,1),
					new SqlParameter("@SenderIsDel", SqlDbType.Bit,1),
					new SqlParameter("@ReaderIsDel", SqlDbType.Bit,1),
					new SqlParameter("@Ext1", SqlDbType.NVarChar,300),
					new SqlParameter("@Ext2", SqlDbType.NVarChar,300)
                                        };
				
			parameters[0].Value = model.SenderID;
			parameters[1].Value = model.ReceiverID;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.Content;
			parameters[4].Value = model.MsgType;
			parameters[5].Value = model.SendTime;
			parameters[6].Value = model.ReadTime;
			parameters[7].Value = model.ReceiverIsRead;
			parameters[8].Value = model.SenderIsDel;
			parameters[9].Value = model.ReaderIsDel;
			parameters[10].Value = model.Ext1;
			parameters[11].Value = model.Ext2;
	
			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Maticsoft.Model.Members.SiteMessage model)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(" update SA_SiteMessage set ");
			strSql.Append(" SenderID=@SenderID,");
			strSql.Append(" ReceiverID=@ReceiverID,");
			strSql.Append(" Title=@Title,");
			strSql.Append(" Content=@Content,");
			strSql.Append(" MsgType=@MsgType,");
			strSql.Append(" SendTime=@SendTime,");
			strSql.Append(" ReadTime=@ReadTime,");
			strSql.Append(" ReceiverIsRead=@ReceiverIsRead,");
			strSql.Append(" SenderIsDel=@SenderIsDel,");
			strSql.Append(" ReaderIsDel=@ReaderIsDel,");
			strSql.Append(" Ext1=@Ext1,");
			strSql.Append(" Ext2=@Ext2,");
			
			strSql.Append("  where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@SenderID", SqlDbType.Int,4),
					new SqlParameter("@ReceiverID", SqlDbType.NChar,10),
					new SqlParameter("@Title", SqlDbType.NVarChar,300),
					new SqlParameter("@Content", SqlDbType.NVarChar),
					new SqlParameter("@MsgType", SqlDbType.NVarChar,50),
					new SqlParameter("@SendTime", SqlDbType.DateTime),
					new SqlParameter("@ReadTime", SqlDbType.DateTime),
					new SqlParameter("@ReceiverIsRead", SqlDbType.Bit,1),
					new SqlParameter("@SenderIsDel", SqlDbType.Bit,1),
					new SqlParameter("@ReaderIsDel", SqlDbType.Bit,1),
					new SqlParameter("@Ext1", SqlDbType.NVarChar,300),
					new SqlParameter("@Ext2", SqlDbType.NVarChar,300),
			
					new SqlParameter("@ID", SqlDbType.Int,4)};
			parameters[0].Value = model.SenderID;
			parameters[1].Value = model.ReceiverID;
			parameters[2].Value = model.Title;
			parameters[3].Value = model.Content;
			parameters[4].Value = model.MsgType;
			parameters[5].Value = model.SendTime;
			parameters[6].Value = model.ReadTime;
			parameters[7].Value = model.ReceiverIsRead;
			parameters[8].Value = model.SenderIsDel;
			parameters[9].Value = model.ReaderIsDel;
			parameters[10].Value = model.Ext1;
			parameters[11].Value = model.Ext2;
	
			parameters[12].Value = model.ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append(" delete from SA_SiteMessage ");
			strSql.Append("  where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		/// <summary>
		/// 批量删除数据
		/// </summary>
		public bool DeleteList(string IDlist )
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(" delete from SA_SiteMessage ");
			strSql.Append("  where ID in ("+IDlist + ")  ");
			int rows=DbHelperSQL.ExecuteSql(strSql.ToString());
			if (rows > 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}


		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Maticsoft.Model.Members.SiteMessage GetModel(int ID)
		{
			
			StringBuilder strSql=new StringBuilder();
			strSql.Append(" select  top 1 ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2 from SA_SiteMessage ");
			strSql.Append("  where ID=@ID");
			SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int,4)
			};
			parameters[0].Value = ID;

			Maticsoft.Model.Members.SiteMessage model=new Maticsoft.Model.Members.SiteMessage();
			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				if(ds.Tables[0].Rows[0]["ID"]!=null && ds.Tables[0].Rows[0]["ID"].ToString()!="")
				{
					model.ID=int.Parse(ds.Tables[0].Rows[0]["ID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["SenderID"]!=null && ds.Tables[0].Rows[0]["SenderID"].ToString()!="")
				{
					model.SenderID=int.Parse(ds.Tables[0].Rows[0]["SenderID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReceiverID"]!=null && ds.Tables[0].Rows[0]["ReceiverID"].ToString()!="")
				{
                    model.ReceiverID = int.Parse(ds.Tables[0].Rows[0]["ReceiverID"].ToString());
				}
				if(ds.Tables[0].Rows[0]["Title"]!=null && ds.Tables[0].Rows[0]["Title"].ToString()!="")
				{
					model.Title=ds.Tables[0].Rows[0]["Title"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Content"]!=null && ds.Tables[0].Rows[0]["Content"].ToString()!="")
				{
					model.Content=ds.Tables[0].Rows[0]["Content"].ToString();
				}
				if(ds.Tables[0].Rows[0]["MsgType"]!=null && ds.Tables[0].Rows[0]["MsgType"].ToString()!="")
				{
					model.MsgType=ds.Tables[0].Rows[0]["MsgType"].ToString();
				}
				if(ds.Tables[0].Rows[0]["SendTime"]!=null && ds.Tables[0].Rows[0]["SendTime"].ToString()!="")
				{
					model.SendTime=DateTime.Parse(ds.Tables[0].Rows[0]["SendTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReadTime"]!=null && ds.Tables[0].Rows[0]["ReadTime"].ToString()!="")
				{
					model.ReadTime=DateTime.Parse(ds.Tables[0].Rows[0]["ReadTime"].ToString());
				}
				if(ds.Tables[0].Rows[0]["ReceiverIsRead"]!=null && ds.Tables[0].Rows[0]["ReceiverIsRead"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["ReceiverIsRead"].ToString()=="1")||(ds.Tables[0].Rows[0]["ReceiverIsRead"].ToString().ToLower()=="true"))
					{
						model.ReceiverIsRead=true;
					}
					else
					{
						model.ReceiverIsRead=false;
					}
				}
				if(ds.Tables[0].Rows[0]["SenderIsDel"]!=null && ds.Tables[0].Rows[0]["SenderIsDel"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["SenderIsDel"].ToString()=="1")||(ds.Tables[0].Rows[0]["SenderIsDel"].ToString().ToLower()=="true"))
					{
						model.SenderIsDel=true;
					}
					else
					{
						model.SenderIsDel=false;
					}
				}
				if(ds.Tables[0].Rows[0]["ReaderIsDel"]!=null && ds.Tables[0].Rows[0]["ReaderIsDel"].ToString()!="")
				{
					if((ds.Tables[0].Rows[0]["ReaderIsDel"].ToString()=="1")||(ds.Tables[0].Rows[0]["ReaderIsDel"].ToString().ToLower()=="true"))
					{
						model.ReaderIsDel=true;
					}
					else
					{
						model.ReaderIsDel=false;
					}
				}
				if(ds.Tables[0].Rows[0]["Ext1"]!=null && ds.Tables[0].Rows[0]["Ext1"].ToString()!="")
				{
					model.Ext1=ds.Tables[0].Rows[0]["Ext1"].ToString();
				}
				if(ds.Tables[0].Rows[0]["Ext2"]!=null && ds.Tables[0].Rows[0]["Ext2"].ToString()!="")
				{
					model.Ext2=ds.Tables[0].Rows[0]["Ext2"].ToString();
				}
				return model;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(" select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2");
			strSql.Append("  FROM SA_SiteMessage ");
			if(strWhere.Trim()!="")
			{
				strSql.Append("  where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(" select ");
			if(Top>0)
			{
				strSql.Append("  top "+Top.ToString());
			}
			strSql.Append("  ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,SenderUserName,ReceiverUserName ");
			strSql.Append("  FROM SA_SiteMessage ");
			if(strWhere.Trim()!="")
			{
				strSql.Append("  where "+strWhere);
			}
			strSql.Append("  order by " + filedOrder);
			return DbHelperSQL.Query(strSql.ToString());
		}

		/// <summary>
		/// 获取记录总数
		/// </summary>
		public int GetRecordCount(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(" select count(1) FROM SA_SiteMessage ");
			if(strWhere.Trim()!="")
			{
				strSql.Append("  where "+strWhere);
			}
			object obj = DbHelperSQL.GetSingle(strSql.ToString());
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
		}
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append(" SELECT * FROM ( ");
			strSql.Append("  SELECT ROW_NUMBER() OVER (");
			if (!string.IsNullOrEmpty(orderby.Trim()))
			{
				strSql.Append(" order by T." + orderby );
			}
			else
			{
				strSql.Append(" order by T.ID desc");
			}
			strSql.Append(" )AS Row, T.*  from SA_SiteMessage T ");
			if (!string.IsNullOrEmpty(strWhere.Trim()))
			{
				strSql.Append("  WHERE " + strWhere);
			}
			strSql.Append("  ) TT");
			strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
			return DbHelperSQL.Query(strSql.ToString());
		}

    

		/*
		/// <summary>
		/// 分页获取数据列表
		/// </summary>
		public DataSet GetList(int PageSize,int PageIndex,string strWhere)
		{
			SqlParameter[] parameters = {
					new SqlParameter("@tblName", SqlDbType.NVarChar, 255),
					new SqlParameter("@fldName", SqlDbType.NVarChar, 255),
					new SqlParameter("@PageSize", SqlDbType.Int),
					new SqlParameter("@PageIndex", SqlDbType.Int),
					new SqlParameter("@IsReCount", SqlDbType.Bit),
					new SqlParameter("@OrderType", SqlDbType.Bit),
					new SqlParameter("@strWhere", SqlDbType.NVarChar,1000),
					};
			parameters[0].Value = "SA_SiteMessage";
			parameters[1].Value = "ID";
			parameters[2].Value = PageSize;
			parameters[3].Value = PageIndex;
			parameters[4].Value = 0;
			parameters[5].Value = 0;
			parameters[6].Value = strWhere;	
			return DbHelperSQL.RunProcedure("UP_GetRecordByPage",parameters,"ds");
		}*/

		#endregion  Method
        #region 两个辅助方法
        /// 辅助类 进行分页使用
        /// </summary>
        /// <param name="Order"></param>
        /// <param name="OriginalSql"></param>
        /// <param name="StartIndex"></param>
        /// <param name="EndIndex"></param>
        /// <returns></returns>
        public string GetListToPageSQl(string Order, string OriginalSql, int StartIndex, int EndIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  SELECT * FROM (SELECT * , ROW_NUMBER() OVER (ORDER BY " + Order + ") AS row  FROM ( ");
            strSql.Append(OriginalSql);
            strSql.Append("  ) AS MatiTemp1 ) AS MatiTemp2 WHERE  row BETWEEN " + StartIndex + " AND " + EndIndex + "");
            return strSql.ToString();
        }
        public string GetListToPageSQl(string Order, string OriginalSql)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("  SELECT * FROM (SELECT * , ROW_NUMBER() OVER (ORDER BY " + Order + ") AS row  FROM ( ");
            strSql.Append(OriginalSql);
            strSql.Append("  ) AS MatiTemp1 ) AS MatiTemp2 WHERE  row BETWEEN @StartIndex AND @EndIndex");
            return strSql.ToString();

        }
        public int GetCountSql(string CountSql)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(CountSql);
            object obj = DbHelperSQL.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        public int GetCountSql(string CountSql,SqlParameter[] parameter)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(CountSql);
            object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameter);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        #endregion
        #region 站内信代码
        /// <summary>
        
     /// <summary>
     /// 管理员发送系统消息的数量
     /// </summary>
     /// <param name="AdminID">管理员的id</param>
     /// <returns></returns>
        public int GetAdminSendMsgCount(int AdminID)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append(" select COUNT(1) from SA_SiteMessage where SenderIsDel='False' and SenderID=@SenderID");
            SqlParameter[] parameters = {
					new SqlParameter("@SenderID", SqlDbType.Int,4)};
            parameters[0].Value =AdminID;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
			if (obj == null)
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(obj);
			}
        } 
      
        #endregion

        /// <summary>
        /// 管理员发送系统消息
        /// </summary>
        /// <param name="AdminID">管理员id</param>
        /// <returns></returns>
      
        public DataSet GetAdminSendList(int AdminID)
        {
            StringBuilder strSql=new StringBuilder();
            strSql.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as ReceiverUserName");
            strSql.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.ReceiverID=U.UserID WHERE  SenderIsDel='False' and SenderID=@SenderID");
            SqlParameter[] parameters = {
					new SqlParameter("@SenderID", SqlDbType.Int,4)};
            parameters[0].Value = AdminID;
			return DbHelperSQL.Query(strSql.ToString(),parameters);
        }


        /// <summary>
        /// 管理员发送系统消息
        /// </summary>
        /// <param name="AdminID">管理员id</param>
        /// <returns></returns>

        public DataSet GetAdminSendList(int AdminID,string KeyWord)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as ReceiverUserName");
            strSql.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.ReceiverID=U.UserID WHERE  SenderIsDel='False' and SenderID=@SenderID");

            if (!string.IsNullOrEmpty(KeyWord))
            {
                strSql.Append(" and Content like '%" + KeyWord + "%' ");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@SenderID", SqlDbType.Int,4)};
          
            parameters[0].Value = AdminID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 管理员发送系统消息的分页
        /// </summary>
        /// <param name="AdminID"></param>
        /// <param name="StartIndex"></param>
        /// <param name="EndIndex"></param>
        /// <returns></returns>
        public DataSet GetAdminSendListByPage(int AdminID, int StartIndex, int EndIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Select ID, SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as ReceiverUserName");
            strSql.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.ReceiverID=U.UserID WHERE  SenderIsDel='False' and SenderID=@SenderID");
            SqlParameter[] parameters = {
					new SqlParameter("@SenderID", SqlDbType.Int,4),
                    new SqlParameter("@StartIndex", SqlDbType.Int,4),
                    new SqlParameter("@EndIndex", SqlDbType.Int,4)};
            parameters[0].Value = AdminID;
            parameters[1].Value = StartIndex;
            parameters[2].Value = EndIndex;
            return DbHelperSQL.Query(GetListToPageSQl("SendTime Desc",strSql.ToString()),parameters);
           
        }
        /// <summary>
        /// 得到全部接收到的站内信的数量，包括未读的和已读的
        /// </summary>
        /// <param name="RecevieID">接收者的ID</param>
        /// <param name="AdminID">管理员ID</param>
        /// <returns></returns>
        public int GetAllReceiveMsgCount(int RecevieID, int AdminID)
        {
            string CountSql = "SELECT COUNT(1) FROM SA_SiteMessage WHERE  ReceiverID =@ReceiverID AND  ReaderIsDel = 'False' AND SenderID <>@SenderID";
            SqlParameter[] parameters = {
					new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                     new SqlParameter("@SenderID", SqlDbType.Int,4)                   };
            parameters[0].Value = RecevieID;
            parameters[1].Value = AdminID;
            return  GetCountSql(CountSql,parameters);
        }
        /// <summary>
        /// 得到全部接收到的站内信的数量，包括未读的和已读的(包括系统消息)
        /// </summary>
        /// <param name="RecevieID">接收者的ID</param>
        /// <param name="AdminID">管理员ID</param>
        /// <returns></returns>
        public int GetAllReceiveMsgCount(int RecevieID )
        {
            string CountSql = "SELECT COUNT(1) FROM SA_SiteMessage WHERE  ReceiverID =@ReceiverID AND  ReaderIsDel = 'False' ";
            SqlParameter[] parameters = {
					new SqlParameter("@ReceiverID", SqlDbType.Int,4)         };
            parameters[0].Value = RecevieID;
            return GetCountSql(CountSql, parameters);
        }
        /// <summary>
        /// 用户接收到的全部站内信（包括全部的已读和未读的信息）
        /// </summary>
        /// <param name="RecevieID">接收者ID</param>
        /// <param name="AdminID">后台管理员的ID</param>
        /// <returns></returns>
       public DataSet GetAllReceiveMsgList(int RecevierID, int AdminID)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as SenderUserName");
           strSql.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.SenderID=U.UserID WHERE  where ReceiverID=@RecevierID and ReaderIsDel='False'and SenderID<>@AdminID");
           SqlParameter[] parameters = {
					new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                     new SqlParameter("@SenderID", SqlDbType.Int,4)                   };
           parameters[0].Value = RecevierID;
           parameters[1].Value = AdminID;
           return DbHelperSQL.Query(strSql.ToString());
     
       }

      /// <summary>
       /// 用户接收到的全部站内信分页（包括全部的已读和未读的信息）
      /// </summary>
      /// <param name="RecevierID">接受者id</param>
      /// <param name="AdminID">后台用户id</param>
      /// <param name="StartIndex">开始的index</param>
      /// <param name="EndIndex">结束的index</param>
      /// <returns></returns>
       public DataSet GetAllReceiveMsgListByPage(int RecevierID, int AdminID, int StartIndex, int EndIndex)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as SenderUserName");
           strSql.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.SenderID=U.UserID WHERE ReceiverID=@ReceiverID  and ReaderIsDel='False'and SenderID<>@AdminId");

           SqlParameter[] parameters = {
					new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                     new SqlParameter("@AdminId", SqlDbType.Int,4),
                    new SqlParameter("@StartIndex", SqlDbType.Int,4),
                    new SqlParameter("@EndIndex", SqlDbType.Int,4)};
           parameters[0].Value = RecevierID;
           parameters[1].Value = AdminID;
           parameters[2].Value = StartIndex;
           parameters[3].Value = EndIndex;
           return DbHelperSQL.Query(GetListToPageSQl("SendTime Desc",strSql.ToString()),parameters);

       }
       /// <summary>
       /// 用户接收到的全部站内信分页（包括全部的已读和未读的信息 也包括系统消息）
       /// </summary>
       /// <param name="RecevierID">接受者id</param>
       /// <param name="StartIndex">开始的index</param>
       /// <param name="EndIndex">结束的index</param>
       /// <returns></returns>
       public DataSet GetAllReceiveMsgListByPage(int RecevierID, int StartIndex, int EndIndex)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as SenderUserName");
           strSql.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.SenderID=U.UserID WHERE ReceiverID=@ReceiverID  and ReaderIsDel='False'");

           SqlParameter[] parameters = {
					new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                    new SqlParameter("@StartIndex", SqlDbType.Int,4),
                    new SqlParameter("@EndIndex", SqlDbType.Int,4)};
           parameters[0].Value = RecevierID;
           parameters[1].Value = StartIndex;
           parameters[2].Value = EndIndex;
           return DbHelperSQL.Query(GetListToPageSQl("SendTime Desc", strSql.ToString()), parameters);

       }

       /// <summary>
       /// 用户发送的全部站内信
       /// </summary>
       /// <param name="RecevieID">接收者ID</param>
       /// <param name="AdminID">后台管理员的ID</param>
       /// <returns></returns>
       public DataSet GetAllSendMsgList(int SenderID)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as  ReceiverUserName");
           strSql.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.ReceiverID=U.UserID WHERE  SenderID=@SenderID and SenderIsDel='False'");
           SqlParameter[] parameters = {
                     new SqlParameter("@SenderID", SqlDbType.Int,4)                   };
           parameters[0].Value = SenderID;
           return DbHelperSQL.Query(strSql.ToString());

       }
        /// <summary>
       ///  用户发送的全部站内信分页
        /// </summary>
        /// <param name="SenderID">发送者ID</param>
        /// <param name="StartIndex">开始index</param>
        /// <param name="EndIndex">结束index</param>
        /// <returns></returns>
       public DataSet GetAllSendMsgListByPage(int SenderID, int StartIndex, int EndIndex)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as  ReceiverUserName");
           strSql.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.ReceiverID=U.UserID WHERE SenderID=@SenderID and SenderIsDel='False'");
           SqlParameter[] parameters = {
                     new SqlParameter("@SenderID", SqlDbType.Int,4),
                      new SqlParameter("@StartIndex", SqlDbType.Int,4),
                        new SqlParameter("@EndIndex", SqlDbType.Int,4)
                                       };
           parameters[0].Value = SenderID;
           parameters[1].Value = StartIndex;
           parameters[2].Value = EndIndex;
           return DbHelperSQL.Query(GetListToPageSQl("SendTime Desc",strSql.ToString()),parameters);

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
           StringBuilder strSql=new StringBuilder();
           strSql.Append( "SELECT COUNT(1) FROM SA_SiteMessage where ");
           strSql.Append(" ( ReceiverID =@ReceiverID AND SenderID =@AdminId AND ReaderIsDel = 'False' and SendTime >= ( SELECT  User_dateCreate FROM     Accounts_Users WHERE UserID=@ReceiverID))"); //第一种情况是管理员单点给某个用户发
           strSql.Append(" Or");///下面是群发的情况(在记录表中没有删除的情况就成立条件)
           strSql.Append(" ( ID  IN ( SELECT MessageID FROM SA_SiteMessageLog WHERE MessageState=1 AND ReceiverID =@ReceiverID AND MsgType =@UserType AND SenderID = @AdminId) AND MsgType =@UserType AND SenderID =@AdminId and SendTime >= (SELECT  User_dateCreate FROM     Accounts_Users WHERE UserID=@ReceiverID) )");
           SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                      new SqlParameter("@AdminId", SqlDbType.Int,4),
                        new SqlParameter("@UserType", SqlDbType.NVarChar)
                                       };
           parameters[0].Value =ReceiverID;
           parameters[1].Value = AdminId;
           parameters[2].Value = UserType;
           return GetCountSql(strSql.ToString(),parameters);
       } 

        /// <summary>
       /// 得到全部的系统消息的列表，包括已读的和未读的（店对面）
       /// </summary>
       /// <param name="RecevieID">接受者的ID</param>
       /// <param name="AdminID">管理员ID</param>
      /// <param name="UserType">用户的类型</param>
       /// <returns></returns>
       public  DataSet GetAllSystemMsgList(int ReceiverID, int AdminId, string UserType)
       {
           StringBuilder strSql=new StringBuilder();
           strSql.Append( "SELECT * FROM SA_SiteMessage where ");
           strSql.Append(" ( ReceiverID = " + ReceiverID + " AND SenderID =@AdminId  AND ReaderIsDel = 'False' and SendTime >= (SELECT  User_dateCreate FROM     Accounts_Users WHERE UserID=@ReceiverID))"); //第一种情况是管理员单点给某个用户发
           strSql.Append(" Or");///下面是群发的情况(在记录表中没有删除的情况就成立条件)
           strSql.Append(" ( ID  IN ( SELECT MessageID FROM SA_SiteMessageLog WHERE MessageState=1 AND ReceiverID =@ReceiverID AND MsgType =@UserType AND SenderID =@AdminId) AND MsgType =@UserType AND SenderID = @AdminId  and SendTime >= (SELECT  User_dateCreate FROM     Accounts_Users WHERE UserID=@ReceiverID) ) ");
           SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                      new SqlParameter("@AdminId", SqlDbType.Int,4),
                        new SqlParameter("@UserType", SqlDbType.NVarChar)
                                       };
           parameters[0].Value = ReceiverID;
           parameters[1].Value = AdminId;
           parameters[2].Value = UserType;
           return DbHelperSQL.Query(strSql.ToString(),parameters);
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
        public  DataSet GetAllSystemMsgListByPage(int ReceiverID, int AdminId, string UserType,int StartIndex,int EndIndex)
       {
           StringBuilder strSql = new StringBuilder();
           strSql.Append(" SELECT * FROM SA_SiteMessage where ");
           strSql.Append(" ( ReceiverID = " + ReceiverID + " AND SenderID =@AdminId AND ReaderIsDel = 'False' and SendTime >= (SELECT  User_dateCreate FROM     Accounts_Users WHERE UserID=@ReceiverID))"); //第一种情况是管理员单点给某个用户发
           strSql.Append(" Or");///下面是群发的情况(在记录表中没有删除的情况就成立条件)
           strSql.Append(" ( ID  IN ( SELECT MessageID FROM SA_SiteMessageLog WHERE MessageState=1 AND ReceiverID =@ReceiverID AND MsgType =@UserType AND SenderID =@AdminId) AND MsgType =@UserType AND SenderID =@AdminId and SendTime >= (SELECT  User_dateCreate FROM     Accounts_Users WHERE UserID=@ReceiverID))");
           SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                      new SqlParameter("@AdminId", SqlDbType.Int,4),
                        new SqlParameter("@UserType", SqlDbType.NVarChar),
                         new SqlParameter("@StartIndex", SqlDbType.Int,4),
                          new SqlParameter("@EndIndex", SqlDbType.Int,4),
                                       };
           parameters[0].Value = ReceiverID;
           parameters[1].Value = AdminId;
           parameters[2].Value = UserType;
           parameters[3].Value = StartIndex;
           parameters[4].Value = EndIndex;
            return DbHelperSQL.Query(GetListToPageSQl("SendTime Desc", strSql.ToString()),parameters);
       }
        /// <summary>
        /// 得到已读信息的列表（点对点）
        /// </summary>
        /// <param name="ReceiverID">收信人的id</param>
        /// <param name="AdminId">管理员的id</param>
        /// <returns></returns>
        public DataSet GetReceiveMsgAlreadyReadList(int ReceiverID, int AdminId)
        {
             StringBuilder strSql = new StringBuilder();
             strSql.Append(" SELECT * FROM SA_SiteMessage where ");
             strSql.Append("  ReceiverID=@ReceiverID and ReaderIsDel='False'and ReceiverIsRead='True'and SenderID<>@AdminId");
             SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                      new SqlParameter("@AdminId", SqlDbType.Int,4),
                                       };
             parameters[0].Value = ReceiverID;
             parameters[1].Value = AdminId;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }
        /// <summary>
        /// 已读信息的列表分页情况（点对点）
        /// </summary>
        /// <param name="ReceiverID">接受用户id</param>
        /// <param name="AdminId">管理员id</param>
        /// <param name="StartIndex">开始的index</param>
        /// <param name="EndIndex">结束的index</param>
        /// <returns></returns>
        public DataSet GetReceiveMsgAlreadyReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" SELECT * FROM SA_SiteMessage where ");
            strSql.Append("  ReceiverID=@ReceiverID and ReaderIsDel='False'and ReceiverIsRead='True'and SenderID<>@AdminId");

            SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                      new SqlParameter("@AdminId", SqlDbType.Int,4),
                         new SqlParameter("@StartIndex", SqlDbType.Int,4),
                          new SqlParameter("@EndIndex", SqlDbType.Int,4),
                                       };
            parameters[0].Value = ReceiverID;
            parameters[1].Value = AdminId;
            parameters[2].Value = StartIndex;
            parameters[3].Value = EndIndex;
            return DbHelperSQL.Query(GetListToPageSQl("SendTime Desc",strSql.ToString()),parameters);
        }
        /// <summary>
        /// 得到用户已读信息的个数（点对点）
        /// </summary>
        /// <param name="ReceiverID">接受者id</param>
        /// <param name="AdminId">后台管理员id</param>
        /// <returns></returns>
        public int GetReceiveMsgAreadyReadCount(int ReceiverID, int AdminId)
        { 
          string strsql="select count(1) from SA_SiteMessage where ReceiverID=@ReceiverID and ReaderIsDel='False'and ReceiverIsRead='True'and SenderID<>@AdminId";
          SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                      new SqlParameter("@AdminId", SqlDbType.Int,4)
                                       };
          parameters[0].Value = ReceiverID;
          parameters[1].Value = AdminId;
          return GetCountSql(strsql,parameters);
        
        }
        /// <summary>
        ///未读信息的个数（点对点）
        /// </summary>
        /// <param name="ReceiverID"></param>
        /// <param name="AdminId"></param>
        /// <returns></returns>
        public int GetReceiveMsgNotReadCount(int ReceiverID, int AdminId)
        {
            string strsql = "select count(1) from SA_SiteMessage where ReceiverID=@ReceiverID and ReaderIsDel='False'and ReceiverIsRead='False'and SenderID<>@AdminId";
            SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                      new SqlParameter("@AdminId", SqlDbType.Int,4),
                                       };
            parameters[0].Value = ReceiverID;
            parameters[1].Value = AdminId;
            return GetCountSql(strsql,parameters);
        
        }
        /// <summary>
        /// 未读信息的列表（点对点）
        /// </summary>
        /// <param name="ReceiverID">接受者id</param>
        /// <param name="AdminId">后台管理员id</param>
        /// <returns></returns>
        public DataSet GetReceiveMsgNotReadList(int ReceiverID, int AdminId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as SenderUserName");
            strSql.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.SenderID=U.UserID WHERE ");
            strSql.Append("  ReceiverID=@ReceiverID  and ReaderIsDel='False'and ReceiverIsRead='False'and SenderID<>@AdminId");
            SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                      new SqlParameter("@AdminId", SqlDbType.Int,4),
                                       };
            parameters[0].Value = ReceiverID;
            parameters[1].Value = AdminId;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        
        } 
        /// <summary>
        /// 未读信息的列表分页（点对点）
        /// </summary>
        /// <param name="ReceiverID">接受者id</param>
        /// <param name="AdminId">后台管理员id</param>
       /// <param name="StartIndex">开始的index</param>
        /// <param name="EndIndex">结束的index</param>
        /// <returns></returns>
        public DataSet GetReceiveMsgNotReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" Select ID,SenderID,ReceiverID,Title,Content,MsgType,SendTime,ReadTime,ReceiverIsRead,SenderIsDel,ReaderIsDel,Ext1,Ext2,U.NickName as SenderUserName");
            strSql.Append(" FROM SA_SiteMessage S LEFT JOIN Accounts_Users U ON S.SenderID=U.UserID WHERE ");
            strSql.Append("  ReceiverID=@ReceiverID and ReaderIsDel='False'and ReceiverIsRead='False'and SenderID<>@AdminId");
            SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                      new SqlParameter("@AdminId", SqlDbType.Int,4),
                       new SqlParameter("@StartIndex", SqlDbType.Int,4),
                        new SqlParameter("@EndIndex", SqlDbType.Int,4),
                                       };
            parameters[0].Value = ReceiverID;
            parameters[1].Value = AdminId;
            parameters[2].Value = StartIndex;
            parameters[3].Value =EndIndex;
            return DbHelperSQL.Query(GetListToPageSQl("SendTime Desc",strSql.ToString()),parameters);
        
        }
        /// <summary>
        /// 得到发送消息的数量（点对点）
        /// </summary>
        /// <param name="SenderID">发送者ID</param>
        /// <returns></returns>
        public int GetSendMsgCount(int SenderID)
        {
            string strsql = "select count(1) from SA_SiteMessage where SenderID=@SenderID and SenderIsDel='False'";
            SqlParameter[] parameters = {
                     new SqlParameter("@SenderID", SqlDbType.Int,4)
                                       };
            parameters[0].Value = SenderID;
            return GetCountSql(strsql,parameters);
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(1) from SA_SiteMessage where");
            strSql.Append(" (ReceiverID =@ReceiverID AND SenderID =@AdminId  AND ReceiverIsRead = 'True' AND ReaderIsDel='False')");
            strSql.Append(" Or");
            strSql.Append(" (ID  IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID =@ReceiverID and MessageState='0' AND MsgType=@UserType))");
            SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                       new SqlParameter("@AdminId", SqlDbType.Int,4),
                        new SqlParameter("@UserType", SqlDbType.NVarChar)

                                       };
            parameters[0].Value = ReceiverID;
            parameters[1].Value =AdminId;
            parameters[2].Value = UserType;
            return GetCountSql(strSql.ToString(),parameters);
        }
        /// <summary>
        /// 得到已读系统信息的列表
        /// </summary>
        /// <param name="ReceiverID">发送者ID</param>
        /// <param name="AdminId">管理员ID</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public DataSet GetSystemMsgAlreadyReadList(int ReceiverID, int AdminId, string UserType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from SA_SiteMessage where");
            strSql.Append(" (ReceiverID = @ReceiverID AND SenderID =@AdminId  AND ReceiverIsRead = 'True' AND ReaderIsDel='False')");
            strSql.Append(" Or");
            strSql.Append(" (ID  IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID =@ReceiverID and MessageState='0' AND MsgType=@UserType))");
            SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                       new SqlParameter("@AdminId", SqlDbType.Int,4),
                        new SqlParameter("@UserType", SqlDbType.NVarChar)

                                       };
            parameters[0].Value = ReceiverID;
            parameters[1].Value = AdminId;
            parameters[2].Value = UserType;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
          
        }  
        /// <summary>
        /// 得到已读系统信息的列表分页(点对面)
        /// </summary>
        /// <param name="ReceiverID">发送者ID</param>
        /// <param name="AdminId">管理员ID</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
        public DataSet GetSystemMsgAlreadyReadListByPage(int ReceiverID, int AdminId, string UserType,int StartIndex,int EndIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from SA_SiteMessage where");
            strSql.Append(" (ReceiverID = @ReceiverID AND SenderID =@AdminId AND ReceiverIsRead = 'True' AND ReaderIsDel='False')");
            strSql.Append(" Or");
            strSql.Append(" (ID  IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID = " + ReceiverID + " and MessageState='0' AND MsgType=@UserType))");
            SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                       new SqlParameter("@AdminId", SqlDbType.Int,4),
                        new SqlParameter("@UserType", SqlDbType.NVarChar),
                          new SqlParameter("@StartIndex", SqlDbType.Int,4),
                            new SqlParameter("@EndIndex", SqlDbType.Int,4)

                                       };
            parameters[0].Value = ReceiverID;
            parameters[1].Value = AdminId;
            parameters[2].Value = UserType;
            parameters[3].Value = StartIndex;
            parameters[4].Value = EndIndex;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
          
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select count(1) from SA_SiteMessage where MsgType=@UserType  ");
            strSql.Append("and ((ReceiverID = @ReceiverID AND SenderID = @AdminId AND ReceiverIsRead = 'False' AND ReaderIsDel='False')");
            strSql.Append(" Or");
            strSql.Append(" (ID Not IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID =@ReceiverID AND MsgType=@UserType)))");
            strSql.Append(" AND SendTime> (SELECT User_dateCreate FROM Accounts_Users WHERE UserID=@ReceiverID)");
            SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                       new SqlParameter("@AdminId", SqlDbType.Int,4),
                        new SqlParameter("@UserType", SqlDbType.NVarChar)

                                       };
            parameters[0].Value = ReceiverID;
            parameters[1].Value = AdminId;
            parameters[2].Value = UserType;
            return GetCountSql(strSql.ToString(),parameters);
        
        }
        /// <summary>
        /// 未读系统消息列表（点对面）
        /// </summary>
        /// <param name="ReceiverID">接受者ID</param>
        /// <param name="AdminId">管理员ID</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
       public  DataSet GetSystemMsgNotReadList(int ReceiverID, int AdminId, string UserType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from SA_SiteMessage where MsgType=@UserType and ");
            strSql.Append(" ((ReceiverID =@ReceiverID AND SenderID =@AdminId AND ReceiverIsRead = 'False' AND ReaderIsDel='False')");
            strSql.Append(" Or");
            strSql.Append(" (ID Not IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID =@ReceiverID AND MsgType=@UserType)))");
            SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                       new SqlParameter("@AdminId", SqlDbType.Int,4),
                        new SqlParameter("@UserType", SqlDbType.NVarChar)
                                       };
            parameters[0].Value = ReceiverID;
            parameters[1].Value = AdminId;
            parameters[2].Value = UserType;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        
        }
        /// <summary>
        /// 未读系统消息列表分页（点对面）
        /// </summary>
        /// <param name="ReceiverID">接受者ID</param>
        /// <param name="AdminId">管理员ID</param>
        /// <param name="UserType">用户类型</param>
        /// <returns></returns>
      public  DataSet GetSystemMsgNotReadListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select * from SA_SiteMessage where MsgType=@UserType and ");
            strSql.Append(" ((ReceiverID =@ReceiverID AND SenderID =@AdminId  AND ReceiverIsRead = 'False' AND ReaderIsDel='False')");
            strSql.Append(" Or");
            strSql.Append(" (ID Not IN ( SELECT MessageID FROM  SA_SiteMessageLog WHERE ReceiverID =@ReceiverID AND MsgType=@UserType)))");
            SqlParameter[] parameters = {
                     new SqlParameter("@ReceiverID", SqlDbType.Int,4),
                       new SqlParameter("@AdminId", SqlDbType.Int,4),
                        new SqlParameter("@UserType", SqlDbType.NVarChar),
                          new SqlParameter("@StartIndex", SqlDbType.Int,4),
                       new SqlParameter("@EndIndex", SqlDbType.Int,4),

                                       };
            parameters[0].Value = ReceiverID;
            parameters[1].Value = AdminId;
            parameters[2].Value = UserType;
            parameters[3].Value = StartIndex;
            parameters[4].Value = EndIndex;
            return DbHelperSQL.Query(GetListToPageSQl("SendTime Desc",strSql.ToString()),parameters);
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
            SqlParameter[] parameters = {
                     new SqlParameter("@ID", SqlDbType.Int,4),
                       new SqlParameter("@AdminId", SqlDbType.Int,4)
                                       };
            parameters[0].Value = ID;
            parameters[1].Value = AdminID;
            return DbHelperSQL.ExecuteSql("update SA_SiteMessage set SenderIsDel='True' where SenderID=@AdminID and ID=@ID",parameters);
           
        }
        /// <summary>
        /// 设置某短信息的状态为已读
        /// </summary>
        /// <param name="ID">id</param>
        /// <param name="AdminID">管理员id</param>
        /// <returns></returns>
        public int SetReceiveMsgAlreadyRead(int ID)
        {
            SqlParameter[] parameters = {
                     new SqlParameter("@ID", SqlDbType.Int,4)  
                                       };
            parameters[0].Value = ID;
            return DbHelperSQL.ExecuteSql("update SA_SiteMessage  set ReceiverIsRead='True' where ID=@ID",parameters);
        
        }

        /// <summary>
        /// 设置收到短信息的状态为删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int SetReceiveMsgToDelById(int ID)
        {
            SqlParameter[] parameters = {
                     new SqlParameter("@ID", SqlDbType.Int,4)  
                                       };
            parameters[0].Value = ID;
            return DbHelperSQL.ExecuteSql("update SA_SiteMessage  set  ReaderIsDel='True' where ID=@ID",parameters);
        
        } 
        /// <summary>
        /// 设置发出短信息的状态为删除
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int SetSendMsgToDelById(int ID)
        {
            SqlParameter[] parameters = {
                     new SqlParameter("@ID", SqlDbType.Int,4)  
                                       };
            parameters[0].Value = ID;
            return DbHelperSQL.ExecuteSql("update SA_SiteMessage set SenderIsDel='True' where ID=@ID",parameters);
        
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
            int rows;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int),
                    	new SqlParameter("@ReceiverID", SqlDbType.Int),
                        new SqlParameter("@UserType", SqlDbType.NVarChar),
			};
            parameters[0].Value = ID;
            parameters[1].Value = ReceiverID;
            parameters[2].Value = UserType;
            return DbHelperSQL.RunProcedure("Sp_MsgBox_SetSystemMsgStateToAlreadyRead", parameters, out rows);
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
            int rows;
            SqlParameter[] parameters = {
					new SqlParameter("@ID", SqlDbType.Int),
                    	new SqlParameter("@ReceiverID", SqlDbType.Int),
                        new SqlParameter("@UserType", SqlDbType.NVarChar),
			};
            parameters[0].Value = ID;
            parameters[1].Value = ReceiverID;
            parameters[2].Value = UserType;
            return DbHelperSQL.RunProcedure("Sp_MsgBox_SetSystemMsgStateToDel", parameters, out rows);
        }
    }
}

