namespace Maticsoft.DALFactory
{
    /// <summary>
    /// 抽象工厂模式创建DAL。
    /// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口) 
    /// DataCache类在导出代码的文件夹里
    /// <appSettings> 
    /// <add key="DAL" value="Maticsoft.SQLServerDAL.JLT" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
    /// </appSettings> 
    /// </summary>
    public sealed class DAJLT : DataAccessBase
    {
        /// <summary>
        /// 创建ToDo数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.JLT.IToDoInfo CreateToDoInfo()
        {
            string ClassNamespace = AssemblyPath + ".JLT.ToDoInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.JLT.IToDoInfo)objType;
        }
       

        public static Maticsoft.IDAL.JLT.IReports CreateReports()
        {	
            string classNamespace = AssemblyPath + ".JLT.Reports";
            object objType = CreateObject(AssemblyPath, classNamespace);
            return (Maticsoft.IDAL.JLT.IReports)objType;
        }


        /// <summary>
        /// 创建UserAttendance数据层接口。考勤信息表
        /// </summary>
        public static Maticsoft.IDAL.JLT.IUserAttendance CreateUserAttendance()
        {
            string ClassNamespace = AssemblyPath + ".JLT.UserAttendance";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.JLT.IUserAttendance)objType;
        }
        
        /// <summary>
        /// 创建AttendanceType数据层接口。考勤类型
        /// </summary>
        public static Maticsoft.IDAL.JLT.IAttendanceType CreateAttendanceType()
        {

            string ClassNamespace = AssemblyPath + ".JLT.AttendanceType";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.JLT.IAttendanceType)objType;
        }
    }
}
