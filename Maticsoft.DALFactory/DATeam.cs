namespace Maticsoft.DALFactory
{
    public sealed class DATeam: DataAccessBase
    {
        /// <summary>
        /// 创建Team数据层接口
        /// </summary>
        public static Maticsoft.IDAL.Team.ISalesInfo CreateSalesInfo()
        {
            string ClassNamespace = AssemblyPath + ".Team.SalesInfo";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Team.ISalesInfo)objType;
        }

        /// <summary>
        /// 创建TeamB2CComm数据层接口
        /// </summary>
        public static Maticsoft.IDAL.Team.ITeamB2CComm CreateTeamB2CComm()
        {
            string ClassNamespace = AssemblyPath + ".Team.TeamB2CComm";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Team.ITeamB2CComm)objType;
        }
    }
}
