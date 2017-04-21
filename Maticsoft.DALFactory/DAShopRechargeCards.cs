using System;
using System.Reflection;
using System.Configuration;
using Maticsoft.Common;
using Maticsoft.IDAL.Shop;
namespace Maticsoft.DALFactory
{
	/// <summary>
	/// 抽象工厂模式创建DAL。
	/// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口) 
	/// DataCache类在导出代码的文件夹里
	/// <appSettings> 
	/// <add key="DAL" value="Maticsoft.SQLServerDAL.Shop" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
	/// </appSettings> 
	/// </summary>
    public sealed class DAShopRechargeCards : DataAccessBase//<t>
	{
		
		/// <summary>
		/// 创建数据层接口
		/// </summary>
		//public static t Create(string ClassName)
		//{
			//string ClassNamespace = AssemblyPath +"."+ ClassName;
			//object objType = CreateObject(AssemblyPath, ClassNamespace);
			//return (t)objType;
		//}
		/// <summary>
		/// 创建RechargeCards数据层接口。
		/// </summary>
		public static Maticsoft.IDAL.Shop.IRechargeCards CreateRechargeCards()
		{

			string ClassNamespace = AssemblyPath +".Shop.RechargeCards";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (Maticsoft.IDAL.Shop.IRechargeCards)objType;
		}

	}
}
