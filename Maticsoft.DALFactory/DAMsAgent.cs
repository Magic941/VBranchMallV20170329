
namespace Maticsoft.DALFactory
{
    /// <summary>
    /// 抽象工厂模式创建DAL。
    /// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口)  
    /// DataCache类在导出代码的文件夹里
    /// <appSettings>  
    /// <add key="DAL" value="Maticsoft.SQLServerDAL.Ms" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
    /// </appSettings> 
    /// </summary>
    public sealed class DAMsAgent : DataAccessBase
    {

        /// <summary>
        /// 创建AgentAD数据层接口。
        /// </summary>
        public static Maticsoft.IDAL.Ms.Agent.IAgentAD CreateAgentAD()
        {

            string ClassNamespace = AssemblyPath + ".Ms.Agent.AgentAD";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.Agent.IAgentAD)objType;
        }


        /// <summary>
        /// 创建AgentConfig数据层接口。代理商(店铺)配置
        /// </summary>
        public static Maticsoft.IDAL.Ms.Agent.IAgentConfig CreateAgentConfig()
        {

            string ClassNamespace = AssemblyPath + ".Ms.Agent.AgentConfig";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.Agent.IAgentConfig)objType;
        }


        /// <summary>
        /// 创建AgentMenus数据层接口。代理商(店铺)导航
        /// </summary>
        public static Maticsoft.IDAL.Ms.Agent.IAgentMenus CreateAgentMenus()
        {

            string ClassNamespace = AssemblyPath + ".Ms.Agent.AgentMenus";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.Agent.IAgentMenus)objType;
        }


        /// <summary>
        /// 创建AgentRank数据层接口。代理商(店铺)等级
        /// </summary>
        public static Maticsoft.IDAL.Ms.Agent.IAgentRank CreateAgentRank()
        {

            string ClassNamespace = AssemblyPath + ".Ms.Agent.AgentRank";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.Agent.IAgentRank)objType;
        }


        /// <summary>
        /// 创建Agents数据层接口。代理商
        /// </summary>
        public static Maticsoft.IDAL.Ms.Agent.IAgents CreateAgents()
        {

            string ClassNamespace = AssemblyPath + ".Ms.Agent.Agents";
            object objType = CreateObject(AssemblyPath, ClassNamespace);
            return (Maticsoft.IDAL.Ms.Agent.IAgents)objType;
        }
    }
}
