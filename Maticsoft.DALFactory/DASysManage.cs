namespace Maticsoft.DALFactory
{
    public sealed class DASysManage : DataAccessBase
    {
        #region SysManage

        /// <summary>
        /// 创建ConfigSystem数据层接口
        /// </summary>
        public static Maticsoft.IDAL.SysManage.IConfigSystem CreateConfigSystem()
        {
            string ClassNamespace = AssemblyPath + ".SysManage.ConfigSystem";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SysManage.IConfigSystem)objType;
        }
        /// <summary>
        /// 创建ConfigType数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.SysManage.IConfigType CreateConfigType()
        {

            string ClassNamespace = AssemblyPath + ".SysManage.ConfigType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SysManage.IConfigType)objType;
        }


        /// <summary>
        /// 创建SysTree数据层接口
        /// </summary>
        public static Maticsoft.IDAL.SysManage.ISysTree CreateSysTree()
        {
            string ClassNamespace = AssemblyPath + ".SysManage.SysTree";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SysManage.ISysTree)objType;
        }

        /// <summary>
        /// 创建ErrorLog数据层接口
        /// </summary>
        public static Maticsoft.IDAL.SysManage.IErrorLog CreateErrorLog()
        {
            string ClassNamespace = AssemblyPath + ".SysManage.ErrorLog";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SysManage.IErrorLog)objType;
        }

        /// <summary>
        /// 创建MultiLanguage数据层接口
        /// </summary>
        public static Maticsoft.IDAL.SysManage.IMultiLanguage CreateMultiLanguage()
        {
            string ClassNamespace = AssemblyPath + ".SysManage.MultiLanguage";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SysManage.IMultiLanguage)objType;
        }

        /// <summary>
        /// 创建TreeFavorite数据层接口
        /// </summary>
        public static Maticsoft.IDAL.SysManage.ITreeFavorite CreateTreeFavorite()
        {
            string ClassNamespace = AssemblyPath + ".SysManage.TreeFavorite";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SysManage.ITreeFavorite)objType;
        }

        /// <summary>
        /// 创建UserLog数据层接口
        /// </summary>
        public static Maticsoft.IDAL.SysManage.IUserLog CreateUserLog()
        {
            string ClassNamespace = AssemblyPath + ".SysManage.UserLog";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SysManage.IUserLog)objType;
        }

        #endregion SysManage



        /// <summary>
        /// 创建EmailQueue数据层接口。
        /// </summary>
        //public static Maticsoft.IDAL.IEmailQueue CreateEmailQueue()
        //{
        //    string ClassNamespace = AssemblyPath + ".SysManage.EmailQueue";
        //    object objType = CreateObject(AssemblyPath, ClassNamespace);
        //    return (Maticsoft.IDAL.IEmailQueue)objType;
        //}

        /// <summary>
        /// 创建EmailQueue数据层接口。
        /// </summary>
        //public static Maticsoft.IDAL.IEmailQueueProvider CreateEmailQueueProvider()
        //{
        //    string ClassNamespace = AssemblyPath + ".SysManage.EmailQueueProvider";
        //    object objType = CreateObject(AssemblyPath, ClassNamespace);
        //    return (Maticsoft.IDAL.IEmailQueueProvider)objType;
        //}

        /// <summary>
        /// 创建VerifyMail数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.SysManage.IVerifyMail CreateVerifyMail()
        {
            string ClassNamespace = AssemblyPath + ".SysManage.VerifyMail";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SysManage.IVerifyMail)objType;
        }

        /// <summary>
        /// 创建TaskQueue数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.SysManage.ITaskQueue CreateTaskQueue()
        {
            string ClassNamespace = AssemblyPath + ".SysManage.TaskQueue";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SysManage.ITaskQueue)objType;
        }

        /// <summary>
        /// 创建ConfigArea数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.SysManage.IConfigArea CreateConfigArea()
        {
            string ClassNamespace = AssemblyPath + ".SysManage.ConfigArea";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.SysManage.IConfigArea)objType;
        }
    }
}