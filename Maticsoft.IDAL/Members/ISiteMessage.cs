using System;
using System.Data;
namespace Maticsoft.IDAL.Members
{
	/// <summary>
	/// 接口层SiteMessage
	/// </summary>
	public interface ISiteMessage
	{
		#region  成员方法
		/// <summary>
		/// 得到最大ID
		/// </summary>
		int GetMaxId();
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(int ID);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(Maticsoft.Model.Members.SiteMessage model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(Maticsoft.Model.Members.SiteMessage model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int ID);
		bool DeleteList(string IDlist );
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		Maticsoft.Model.Members.SiteMessage GetModel(int ID);
		/// <summary>
		/// 获得数据列表
		/// </summary>
		DataSet GetList(string strWhere);
		/// <summary>
		/// 获得前几行数据
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);
		int GetRecordCount(string strWhere);
		DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		/// <summary>
		/// 根据分页获得数据列表
		/// </summary>
		//DataSet GetList(int PageSize,int PageIndex,string strWhere);
      
		#endregion  成员方法
        #region 后加的

        /// <summary>
        /// 得到管理员发送消息的数量
        /// </summary>
        int GetAdminSendMsgCount(int AdminID);

        /// <summary>
        /// 管理员发送的列表
        /// </summary>
        DataSet GetAdminSendList(int AdminID);
        /// <summary>
        /// 管理员发送的列表
        /// </summary>
        DataSet GetAdminSendList(int AdminID, string KeyWord);
        /// <summary>
        /// 管理员发送的列表的分页
        /// </summary>
        DataSet GetAdminSendListByPage(int AdminID, int StartIndex, int EndIndex);
        /// <summary>
        /// 用户收件箱的全部数量，包括已读信息和未读信息
        /// </summary>
        int GetAllReceiveMsgCount(int RecevieID, int AdminID);

	    /// <summary>
	    /// 得到全部接收到的站内信的数量，包括未读的和已读的(包括系统消息)
	    /// </summary>
	    /// <param name="RecevieID">接收者的ID</param>
	    /// <param name="AdminID">管理员ID</param>
	    /// <returns></returns>
	    int GetAllReceiveMsgCount(int RecevieID);
        /// <summary>
        /// 用户接收到的全部站内信（包括全部的已读和未读的信息）
        /// </summary>
        DataSet GetAllReceiveMsgList(int RecevieID, int AdminID);
        /// <summary>
        /// 用户接收到的全部站内信分页（包括全部的已读和未读的信息）
        /// </summary>
        DataSet GetAllReceiveMsgListByPage(int RecevieID, int AdminID, int StartIndex, int EndIndex);

	    /// <summary>
	    /// 用户接收到的全部站内信分页（包括全部的已读和未读的信息 也包括系统消息）
	    /// </summary>
	    /// <param name="RecevierID">接受者id</param>
	    /// <param name="StartIndex">开始的index</param>
	    /// <param name="EndIndex">结束的index</param>
	    /// <returns></returns>
	    DataSet GetAllReceiveMsgListByPage(int RecevierID, int StartIndex, int EndIndex);
       
        /// <summary>
        /// 用户发送的全部站内信
        /// </summary>
        DataSet GetAllSendMsgListByPage(int SenderID, int StartIndex, int EndIndex);
        /// <summary>
        /// 用户得到系统消息的数量
        /// </summary>
        int GetAllSystemMsgCount(int ReceiverID, int AdminId, string UserType);
        /// <summary>
        /// 用户得到系统消息列表
        /// </summary>
        DataSet GetAllSystemMsgList(int ReceiverID, int AdminId, string UserType);
        /// <summary>
        /// 用户得到系统消息列表分页
        /// </summary>
        DataSet GetAllSystemMsgListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex);
        /// <summary>
        /// 得到已读的信息列表
        /// </summary>
        DataSet GetReceiveMsgAlreadyReadList(int ReceiverID, int AdminId);
        /// <summary>
        /// 得到已读的信息的列表分页情况
        /// </summary>
        DataSet GetReceiveMsgAlreadyReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex);
        /// <summary>
        /// 已读信息的数量
        /// </summary>
        int GetReceiveMsgAreadyReadCount(int ReceiverID, int AdminId);
        /// <summary>
        /// 未读信息的数量
        /// </summary>
        int GetReceiveMsgNotReadCount(int ReceiverID, int AdminId);
        /// <summary>
        /// 未读信息的列表
        /// </summary>
        DataSet GetReceiveMsgNotReadList(int ReceiverID, int AdminId);
        /// <summary>
        /// 未读信息的列表分页
        /// </summary>
        DataSet GetReceiveMsgNotReadListByPage(int ReceiverID, int AdminId, int StartIndex, int EndIndex);
        /// <summary>
        /// 发送消息的总数
        /// </summary>
        int GetSendMsgCount(int SenderID);

        /// <summary>
        /// 得到系统消息已读的数量
        /// </summary>
        int GetSystemMsgAlreadyReadCount(int ReceiverID, int AdminId, string UserType);
        /// <summary>
        /// 得到系统消息已读的列表
        /// </summary>
        DataSet GetSystemMsgAlreadyReadList(int ReceiverID, int AdminId, string UserType);
        /// <summary>
        /// 得到未读消息的个数
        /// </summary>
        int GetSystemMsgNotReadCount(int ReceiverID, int AdminId, string UserType);
        /// <summary>
        /// 得到未读消息的的列表
        /// </summary>
        DataSet GetSystemMsgNotReadList(int ReceiverID, int AdminId, string UserType);
        /// <summary>
        /// 得到未读消息的的列表分页
        /// </summary>
        DataSet GetSystemMsgNotReadListByPage(int ReceiverID, int AdminId, string UserType, int StartIndex, int EndIndex);
        /// <summary>
        /// 设置系统消息的某条为删除状态（管理员操作）
        /// </summary>
        int SetAdminMsgToDelById(int ID, int AdminID);
        /// <summary>
        /// 设置某短信息的状态为已读
        /// </summary>
        int SetReceiveMsgAlreadyRead(int ID);
        /// <summary>
        /// 设置收到短信息的状态为删除
        /// </summary>
        int SetReceiveMsgToDelById(int ID);
        /// <summary>
        /// 设置发出短信息的状态为删除
        /// </summary>
        int SetSendMsgToDelById(int ID);
        /// <summary>
        /// 设置某条系统消息为已读状态
        /// </summary>
        int SetSystemMsgStateToAlreadyRead(int ID, int ReceiverID, string UserType);
        /// <summary>
        /// 设置某条系统消息为删除状态
        /// </summary>
        int SetSystemMsgStateToDel(int ID, int ReceiverID, string UserType);
        /// <summary>
        /// 得到全部已读系统消息的列表分页
        /// </summary>
        DataSet GetSystemMsgAlreadyReadListByPage(int ReceiverID, int AdminId, string UserType,int StartIndex,int EndIndex);
        /// <summary>
        /// 得到全部已发送列表
        /// </summary>
        DataSet GetAllSendMsgList(int SenderID); 
        #endregion
	} 
}
