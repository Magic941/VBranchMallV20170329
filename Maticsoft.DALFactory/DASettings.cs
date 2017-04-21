namespace Maticsoft.DALFactory
{
    public sealed class DASettings : DataAccessBase
    {
        /// <summary>
        /// 创建Advertisement数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Settings.IAdvertisement CreateAdvertisement()
        {
            string ClassNamespace = AssemblyPath + ".Settings.Advertisement";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Settings.IAdvertisement)objType;
        }

        /// <summary>
        /// 创建AdvertisePosition数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Settings.IAdvertisePosition CreateAdvertisePosition()
        {
            string ClassNamespace = AssemblyPath + ".Settings.AdvertisePosition";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Settings.IAdvertisePosition)objType;
        }

        /// <summary>
        /// 创建AdvertisePosition数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Settings.IFilterWords CreateFilterWords()
        {
            string ClassNamespace = AssemblyPath + ".Settings.FilterWords";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Settings.IFilterWords)objType;
        }

        /// <summary>
        /// 创建FLinks数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Settings.IFriendlyLink CreateFriendlyLink()
        {
            string ClassNamespace = AssemblyPath + ".Settings.FriendlyLink";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Settings.IFriendlyLink)objType;
        }

        /// <summary>
        /// 创建MainMenus数据层接口。导航菜单数据表
        /// </summary>
        public static Maticsoft.IDAL.Settings.IMainMenus CreateMainMenus()
        {
            string ClassNamespace = AssemblyPath + ".Settings.MainMenus";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Settings.IMainMenus)objType;
        }

        /// <summary>
        /// 创建SEORelation数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Settings.ISEORelation CreateSEORelation()
        {
            string ClassNamespace = AssemblyPath + ".Settings.SEORelation";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Settings.ISEORelation)objType;
        }
    }
}