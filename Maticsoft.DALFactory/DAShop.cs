namespace Maticsoft.DALFactory
{
    public sealed class DAShop: DataAccessBase
    {
        /// <summary>
        /// 创建Favorite数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.IFavorite CreateFavorite()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Favorite";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.IFavorite)objType;
        }

        /// <summary>
        /// 创建Shippers数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Shop.IShippers CreateShippers()
        {

            string ClassNamespace = AssemblyPath + ".Shop.Shippers";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Shop.IShippers)objType;
        }
    }
}