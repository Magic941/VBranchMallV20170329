using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.DALFactory
{
    public sealed class DAPoll : DataAccessBase
    {

        /// <summary>
        /// 创建Forms数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Poll.IForms CreateForms()
        {

            string ClassNamespace = AssemblyPath + ".Poll.Forms";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Poll.IForms)objType;
        }

        /// <summary>
        /// 创建Options数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Poll.IOptions CreateOptions()
        {

            string ClassNamespace = AssemblyPath + ".Poll.Options";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Poll.IOptions)objType;
        }


        /// <summary>
        /// 创建Reply数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Poll.IReply CreateReply()
        {

            string ClassNamespace = AssemblyPath + ".Poll.Reply";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Poll.IReply)objType;
        }


        /// <summary>
        /// 创建UserPoll数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Poll.IUserPoll CreateUserPoll()
        {

            string ClassNamespace = AssemblyPath + ".Poll.UserPoll";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Poll.IUserPoll)objType;
        }


        /// <summary>
        /// 创建Topics数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Poll.ITopics CreateTopics()
        {

            string ClassNamespace = AssemblyPath + ".Poll.Topics";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Poll.ITopics)objType;
        }


        /// <summary>
        /// 创建Users数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Poll.IPollUsers CreatePollUsers()
        {

            string ClassNamespace = AssemblyPath + ".Poll.PollUsers";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Poll.IPollUsers)objType;
        }
    }
}
