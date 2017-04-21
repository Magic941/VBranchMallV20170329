namespace Maticsoft.DALFactory
{
    public sealed class DAAppt : DataAccessBase
    {
        /// <summary>
        /// 创建Services数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Appt.IServices CreateServices()
        {
            string ClassNamespace = AssemblyPath + ".Appt.Services";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Appt.IServices)objType;
        }
        /// <summary>
        /// 创建Reservation数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Appt.IReservation CreateReservation()
        {
            string ClassNamespace = AssemblyPath + ".Appt.Reservation";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Appt.IReservation)objType;
        }
    }
}