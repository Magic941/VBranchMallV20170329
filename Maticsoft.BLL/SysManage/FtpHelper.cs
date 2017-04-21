using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
namespace Maticsoft.BLL.SysManage
{

    public class FTPHelper
    {
        private string ftpUri;  //ftp服务器地址
        private string ftpName;  //ftp账户
        private string ftpPwd;  //ftp密码
        private FtpWebRequest ftpRequest;  //请求
        private FtpWebResponse ftpResponse;  //响应

        public FTPHelper(string uri, string name, string password)
        {
            this.ftpUri = uri;
            this.ftpName = name;
            this.ftpPwd = password;
        }

        /// <summary>
        /// 连接类
        /// </summary>
        /// <param name="uri">ftp地址</param>
        private void Conn(string uri)
        {
            if (uri.IndexOf("ftp") > 0)
            {
                ftpRequest = (FtpWebRequest)WebRequest.Create(uri);
            }
            else
            {
                ftpRequest = (FtpWebRequest)WebRequest.Create(this.ftpUri +"/" + uri );
            }
           
            //登录ftp服务器，ftpName:账户名，ftpPwd:密码
            ftpRequest.Credentials = new NetworkCredential(ftpName, ftpPwd);
            ftpRequest.UseBinary = true;  //该值指定文件传输的数据类型。
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public virtual void DeleteFileName(string fileName)
        {
            string uri = ftpUri + fileName;
            Conn(uri);

            ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
            ftpResponse.Close();


        }
        /// <summary>
        /// 上传文件，使用FTPWebRequest、FTPWebResponse实例
        /// </summary>
        /// <param name="uri">ftp地址</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fileData">文件内容</param>
        /// <param name="msg">传出参数，返回传输结果</param>
        public virtual void UploadFile(string uri, string fileName, byte[] fileData, out string msg)
        {
            string URI = uri.EndsWith("/") ? uri : uri + "/";
            URI += fileName;
            //连接ftp服务器
            Conn(URI);
            ftpRequest.Method = WebRequestMethods.Ftp.UploadFile;
            ftpRequest.ContentLength = fileData.Length; //上传文件时通知服务器文件的大小

            //将文件流中的数据（byte[] fileData）写入请求流
            using (Stream ftpstream = ftpRequest.GetRequestStream())
            {
                ftpstream.Write(fileData, 0, fileData.Length);
            }

            ftpResponse = (FtpWebResponse)ftpRequest.GetResponse(); //响应
            msg = ftpResponse.StatusDescription; //响应状态
            ftpResponse.Close();

        }

        /// <summary>
        /// 上传文件，使用WebClient类
        /// </summary>
        /// <param name="uri">ftp地址</param>
        /// <param name="fileName">文件名</param>
        /// <param name="fileData">文件内容</param>
        /// <param name="msg">传出参数，输出传输结果</param>
        public virtual void UploadFileByWebClient(string uri, string fileName, byte[] fileData, out string msg)
        {
            string URI = uri.EndsWith("/") ? uri : uri + "/";
            URI += fileName;


            System.Net.WebClient client = new System.Net.WebClient();
            //登录FTP服务
            client.Credentials = new NetworkCredential(ftpName, ftpPwd);
            client.UploadData(URI, "STOR", fileData); //指定为ftp上传方式
            msg = "上传成功!";

        }

        /// <summary>
        /// 下载文件，使用FTPWebRequest、FTPWebResponse实例
        /// </summary>
        /// <param name="uri">ftp地址</param>
        /// <param name="destinationDir">目标文件存放地址</param>
        /// <param name="msg">传出参数，返回传输结果</param>
        public virtual void DownloadFile(string uri, string destinationDir, out string msg)
        {
            string fileName = Path.GetFileName(uri);
            string destinationPath = Path.Combine(destinationDir, fileName);


            //连接ftp服务器
            Conn(uri);

            using (FileStream outputStream = new FileStream(destinationPath, FileMode.OpenOrCreate))
            {
                using (ftpResponse = (FtpWebResponse)ftpRequest.GetResponse())
                {
                    //将响应流中的数据写入到文件流
                    using (Stream ftpStream = ftpResponse.GetResponseStream())
                    {
                        int bufferSize = 2048;
                        int readCount;
                        byte[] buffer = new byte[bufferSize];
                        readCount = ftpStream.Read(buffer, 0, bufferSize);
                        while (readCount > 0)
                        {
                            outputStream.Write(buffer, 0, readCount);
                            readCount = ftpStream.Read(buffer, 0, bufferSize);
                        }
                    }
                    msg = ftpResponse.StatusDescription;
                }
            }

        }

        /// <summary>
        /// 文件下载，使用WebClient类
        /// </summary>
        /// <param name="uri">ftp服务地址</param>
        /// <param name="destinationDir">存放目录</param>
        public virtual void DownloadFileByWebClient(string uri, string destinationDir)
        {

            string fileName = Path.GetFileName(uri);
            string destinationPath = Path.Combine(destinationDir, fileName);

            System.Net.WebClient client = new System.Net.WebClient();
            client.Credentials = new NetworkCredential(this.ftpName, this.ftpPwd);

            byte[] responseData = client.DownloadData(uri);

            using (FileStream fileStream = new FileStream(destinationPath, FileMode.OpenOrCreate))
            {
                fileStream.Write(responseData, 0, responseData.Length);
            }

        }

        /// <summary>
        /// 遍历文件
        /// </summary>
        public ArrayList GetListDirectoryDetails()
        {
            ArrayList fileInfo = new ArrayList();

            Conn(ftpUri);

            //获取 FTP 服务器上的文件的详细列表的 FTP LIST 协议方法
            ftpRequest.Method = WebRequestMethods.Ftp.ListDirectory;
            try
            {
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse(); //响应
            }
            catch (System.Net.WebException e)
            {

                throw new Exception(e.Message);
            }
            catch (System.InvalidOperationException e)
            {

                throw new Exception(e.Message);
            }


            using (Stream responseStream = ftpResponse.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        fileInfo.Add(line);
                        line = reader.ReadLine();
                    }
                }
            }

            return fileInfo;

        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="mstr"></param>
        /// <param name="ftpUri"></param>
        /// <returns></returns>
        public virtual bool Upload(MemoryStream mstr, string ftpUri)
        {
            FtpWebRequest ftp = null;
            if (ftpUri.IndexOf("ftp") > 0)
            {
                ftp = (FtpWebRequest)WebRequest.Create(ftpUri);
            }
            else
            {
                ftp = (FtpWebRequest)WebRequest.Create(this.ftpUri + "/" + ftpUri);
            }
            
            ftp.Credentials = new NetworkCredential(ftpName, ftpPwd);
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            using (Stream stream = ftp.GetRequestStream())
            {
                byte[] bytes = mstr.ToArray();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();
                mstr.Close();
            }
            return true;
        }

        public virtual bool Upload(byte[] mstr, string ftpUri)
        {

            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(ftpUri);
            ftp.Credentials = new NetworkCredential(ftpName, ftpPwd);
            ftp.Method = WebRequestMethods.Ftp.UploadFile;
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            using (Stream stream = ftp.GetRequestStream())
            {
                stream.Write(mstr, 0, mstr.Length);
                stream.Close();
            }
            return true;

        }

        /// <summary>
        /// 重命名文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public virtual bool RenameFileName(string fileName, string reNameFileName)
        {
            bool result = true;
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(reNameFileName);
            ftp.Credentials = new NetworkCredential(ftpName, ftpPwd);

            ftp.Method = WebRequestMethods.Ftp.Rename;
            ftp.RenameTo = fileName;
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            ftpResponse = (FtpWebResponse)ftp.GetResponse();
            Stream ftpStream = ftpResponse.GetResponseStream();
            ftpStream.Close();
            ftpResponse.Close();



            return result;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        public virtual bool DeleteFile(string fileName)
        {
            bool result = true;
            FtpWebRequest ftp = (FtpWebRequest)WebRequest.Create(fileName);
            ftp.Credentials = new NetworkCredential(ftpName, ftpPwd);

            ftp.Method = WebRequestMethods.Ftp.DeleteFile;
            ftp.UseBinary = true;
            ftp.UsePassive = true;
            ftpResponse = (FtpWebResponse)ftp.GetResponse();
            Stream ftpStream = ftpResponse.GetResponseStream();
            ftpStream.Close();
            ftpResponse.Close();


            return result;
        }

        // /创建目录
        public virtual void MakeDir(string dirName)
        {
            Conn(dirName);//连接
            ftpRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
            FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
            response.Close();

        }

        /// <summary>
        /// 是否存在该目录
        /// </summary>
        /// <param name="dirName"></param>
        /// <returns></returns>
        public virtual bool Exists(string dirName)
        {
            try
            {
                Conn(dirName);//连接
                var extentName = Path.GetExtension(dirName);
                if (extentName.Length > 0)
                {
                    ftpRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                }
                else
                {
                    ftpRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                }
                
                FtpWebResponse response = (FtpWebResponse)ftpRequest.GetResponse();
                var resutlt = response.ContentLength == 0 ? false : true;
                response.Close();
                return resutlt;
            }
            catch (WebException e)
            {

                FtpWebResponse response = (FtpWebResponse)e.Response;
                if (response.StatusCode ==
                    FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return false;
                }
            }
            return false;
        }
       
    }
}
