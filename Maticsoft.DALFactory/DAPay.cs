using System;
using System.Reflection;
using System.Configuration;
using Maticsoft.IDAL.Pay;
namespace Maticsoft.DALFactory
{
	/// <summary>
	/// 抽象工厂模式创建DAL。
	/// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口) 
	/// DataCache类在导出代码的文件夹里
	/// <appSettings> 
	/// <add key="DAL" value="Maticsoft.SQLServerDAL.Pay" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
	/// </appSettings> 
	/// </summary>
    public sealed class DAPay : DataAccessBase//<t>
	{
	 
		/// <summary>
		/// 创建RechargeRequest数据层接口。
		/// </summary>
		public static Maticsoft.IDAL.Pay.IRechargeRequest CreateRechargeRequest()
		{

			string ClassNamespace = AssemblyPath +".Pay.RechargeRequest";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (Maticsoft.IDAL.Pay.IRechargeRequest)objType;
		}


		/// <summary>
		/// 创建BalanceDetails数据层接口。
		/// </summary>
		public static Maticsoft.IDAL.Pay.IBalanceDetails CreateBalanceDetails()
		{

			string ClassNamespace = AssemblyPath +".Pay.BalanceDetails";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (Maticsoft.IDAL.Pay.IBalanceDetails)objType;
		}
        /// <summary>
        /// 创建BalanceDrawRequest数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Pay.IBalanceDrawRequest CreateBalanceDrawRequest()
        {

            string ClassNamespace = AssemblyPath + ".Pay.BalanceDrawRequest";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Pay.IBalanceDrawRequest)objType;
        }
}
}