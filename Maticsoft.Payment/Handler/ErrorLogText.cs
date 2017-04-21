using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maticsoft.Payment.Handler
{
    public class ErrorLogTxt
    {
        private static object locker = new object();

        private ErrorLogTxt()
        {
        }

        private static ErrorLogTxt log = new ErrorLogTxt();
        

        /// <summary>
        /// 返回日志实例
        /// </summary>
        /// <returns></returns>
        public static ErrorLogTxt GetInstance(string logtype)
        {
            lock (locker)
            {
                //var errorLogFolder = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_ErrorFolder");


                //if (string.IsNullOrEmpty(errorLogFolder))
                //{
                    errorLogFolder = "d://ShopErrorFolder/";
                //}

                try
                {
                    if (!Directory.Exists(errorLogFolder))
                        Directory.CreateDirectory(errorLogFolder);
                }
                catch (Exception exp)
                {
                    Console.Write(exp.ToString());
                }


                string str = DateTime.Now.ToString("yyyyMMdd");
                errorLogFile = errorLogFolder + str + logtype + ".txt";

                try
                {
                    if (File.Exists(errorLogFile) == false)
                    {
                        FileStream stream = File.Create(errorLogFile,2048,FileOptions.Asynchronous);
                        stream.Close();
                    }
                }
                catch (Exception exp)
                {
                    Console.Write(exp.ToString());
                }
            }

            return log;
        }



        /// <summary>
        /// write log to file
        /// </summary>
        /// <param name="message"></param>
        public void Write(string message)
        {
            lock (locker)
            {
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(errorLogFile, true, System.Text.Encoding.Default);
                    StringBuilder sb = new StringBuilder(500);
                    sb.Append(DateTime.Now.ToString()).Append(DateTime.Now.Ticks.ToString()).Append("\r\n");
                    sb.Append(message).Append("\r\n");
                    sb.Append("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHLLLLLLLLLLLLLLLLLLLLLLLLLLLLLLL").Append("\r\n");
                    writer.Write(sb.ToString());
                }
                catch (IOException ioe)
                {
                    Console.WriteLine(ioe.ToString());
                }
                finally
                {
                    try
                    {
                        if (writer != null)
                            writer.Close();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                    }
                }
            }
        }

        private static string errorLogFolder ="";// = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_ErrorFolder");
        private static string errorLogFile;

    }
}
