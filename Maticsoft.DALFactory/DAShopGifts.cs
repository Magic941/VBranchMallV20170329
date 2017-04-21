using System;
using System.Reflection;
using System.Configuration;
using Maticsoft.IDAL.Members;
namespace Maticsoft.DALFactory
{
	/// <summary>
	/// 抽象工厂模式创建DAL。
	/// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口) 
	/// DataCache类在导出代码的文件夹里
	/// <appSettings> 
	/// <add key="DAL" value="Maticsoft.SQLServerDAL.Members" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
	/// </appSettings> 
	/// </summary>
	public sealed class DAShopGifts:DataAccessBase
	{
	
		/// <summary>
		/// 创建Gifts数据层接口。
		/// </summary>
		public static Maticsoft.IDAL.Shop.Gift.IGifts CreateGifts()
		{

			string ClassNamespace = AssemblyPath +".Shop.Gift.Gifts";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (Maticsoft.IDAL.Shop.Gift.IGifts)objType;
		}


		/// <summary>
		/// 创建GiftsCategory数据层接口。
		/// </summary>
		public static Maticsoft.IDAL.Shop.Gift.IGiftsCategory CreateGiftsCategory()
		{

			string ClassNamespace = AssemblyPath +".Shop.Gift.GiftsCategory";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (Maticsoft.IDAL.Shop.Gift.IGiftsCategory)objType;
		}


		/// <summary>
		/// 创建ExchangeDetail数据层接口。
		/// </summary>
		public static Maticsoft.IDAL.Shop.Gift.IExchangeDetail CreateExchangeDetail()
		{

			string ClassNamespace = AssemblyPath +".Shop.Gift.ExchangeDetail";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (Maticsoft.IDAL.Shop.Gift.IExchangeDetail)objType;
		}

}
}