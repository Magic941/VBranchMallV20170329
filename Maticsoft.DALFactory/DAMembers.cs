/**
* Comment.cs
*
* 功 能： [N/A]
* 类 名： Comment
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2012/5/23 15:36:00  Administrator    初版
*
* Copyright (c) 2012 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/

namespace Maticsoft.DALFactory
{
    public class DAMembers : DataAccessBase
    {
       
        /// <summary>
        /// 创建Guestbook数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IGuestbook CreateGuestbook()
        {
            string ClassNamespace = AssemblyPath + ".Guestbook.Guestbook";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IGuestbook)objType;
        }


        /// <summary>
        /// 创建EntryForm数据层接口。报名表
        /// </summary>
        public static Maticsoft.IDAL.Ms.IEntryForm CreateEntryForm()
        {
            string ClassNamespace = AssemblyPath + ".Ms.EntryForm";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.IEntryForm)objType;
        }

        /// <summary>
        /// 创建ConfigSystem数据层接口
        /// </summary>
        public static Maticsoft.IDAL.IMailConfig CreateMailConfig()
        {
            string ClassNamespace = AssemblyPath + ".MailConfig";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.IMailConfig)objType;
        }

        /// <summary>
        /// 创建SiteMessage数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.ISiteMessage CreateSiteMessage()
        {
            string ClassNamespace = AssemblyPath + ".Members.SiteMessage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.ISiteMessage)objType;
        }

        /// <summary>
        /// 创建SiteMessageLog数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.ISiteMessageLog CreateSiteMessageLog()
        {
            string ClassNamespace = AssemblyPath + ".Members.SiteMessageLog";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.ISiteMessageLog)objType;
        }

        /// <summary>
        /// 创建PointsDetail数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IPointsDetail CreatePointsDetail()
        {
            string ClassNamespace = AssemblyPath + ".Members.PointsDetail";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IPointsDetail)objType;
        }

        /// <summary>
        /// 创建PointsLimit数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IPointsLimit CreatePointsLimit()
        {
            string ClassNamespace = AssemblyPath + ".Members.PointsLimit";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IPointsLimit)objType;
        }

        /// <summary>
        /// 创建PointsRule数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IPointsRule CreatePointsRule()
        {
            string ClassNamespace = AssemblyPath + ".Members.PointsRule";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IPointsRule)objType;
        }

         /// <summary>
        /// 创建PointsAction数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IPointsAction CreatePointsAction()
        {
            string ClassNamespace = AssemblyPath + ".Members.PointsAction";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IPointsAction)objType;
        }
        

        /// <summary>
        /// 创建UsersApprove数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IUsersApprove CreateUsersApprove()
        {
            string ClassNamespace = AssemblyPath + ".Members.UsersApprove";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IUsersApprove)objType;
        }

        /// <summary>
        /// 创建Feedback数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IFeedback CreateFeedback()
        {
            string ClassNamespace = AssemblyPath + ".Members.Feedback";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IFeedback)objType;
        }
        /// <summary>
        /// 创建Feedback数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IFeedbackType CreateFeedbackType()
        {
            string ClassNamespace = AssemblyPath + ".Members.FeedbackType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IFeedbackType)objType;
        }


        /// <summary>
        /// 创建UsersExp数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IUserBind CreateUserBind()
        {
            string ClassNamespace = AssemblyPath + ".Members.UserBind";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IUserBind)objType;
        }

        /// <summary>
        /// 创建UsersExp数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IUsersExp CreateUsersExp()
        {
            string ClassNamespace = AssemblyPath + ".Members.UsersExp";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IUsersExp)objType;
        }

        /// <summary>
        /// 创建Users数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IUsers CreateUsers()
        {
            string ClassNamespace = AssemblyPath + ".Members.Users";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IUsers)objType;
        }

        /// <summary>
        /// 创建UserRank数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IUserRank CreateUserRank()
        {
            string ClassNamespace = AssemblyPath + ".Members.UserRank";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IUserRank)objType;
        }
        /// <summary>
        /// 创建UserInvite数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IUserInvite CreateUserInvite()
        {
            string ClassNamespace = AssemblyPath + ".Members.UserInvite";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IUserInvite)objType;
        }

           /// <summary>
        /// 创建UserCard数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Members.IUserCard CreateUserCard()
        {
            string ClassNamespace = AssemblyPath + ".Members.UserCard";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Members.IUserCard)objType;
        }
        
    }
}