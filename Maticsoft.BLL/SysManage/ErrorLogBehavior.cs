using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Maticsoft.BLL.SysManage
{
    /// <summary>
    /// 错误日志AOP wusg 20140713
    /// </summary>
    public class ErrorLogBehavior : IInterceptionBehavior
    {
        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public bool WillExecute
        {
            get { return true; }
        }

        public IMethodReturn Invoke(
          IMethodInvocation input,
          GetNextInterceptionBehaviorDelegate getNext)
        {

            var action = getNext();
            try
            {
                // Perform the operation
                var methodReturn = action.Invoke(input, getNext);

                // Grab the output
                var result = methodReturn.ReturnValue;
                return methodReturn;
            }
            catch (Exception e)
            {
                ErrorLog.Add(new Model.SysManage.ErrorLog("方法名：" + action.Method.Name, e.Message, ""));
                throw new Exception("方法名：[" + action.Method.Name + "]执行异常,内容为" + e.Message);
            }

        }
    }
}
