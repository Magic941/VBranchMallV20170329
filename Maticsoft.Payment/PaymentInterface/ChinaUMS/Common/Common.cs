using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GL.Payment.Common
{
    /// <summary>
    /// 公共方法操作类
    /// 创建用户：shiyuankao
    /// 创建时间：2014-08-06
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            var reader = new StreamReader(filePath, Encoding.Default);
            try
            {
                return reader.ReadToEnd();
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                reader.Close();
            }
        }
    }
}
