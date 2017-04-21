using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Threading;

using Maticsoft.Common;

namespace Maticsoft.Services
{
    public class CouponServices : IDisposable
    {

        //private static readonly string path = "HaolinShop.MSMQMessaging";
        protected MessageQueue queue;  //消息队列
        private static object locker = new object();
        private static CouponServices sc;
        private System.Messaging.MessageQueue msmq;

        public static CouponServices GetInstance()
        {
            if (sc == null)
            {
                lock (locker)
                {
                    if (sc == null)
                    {
                        sc = new CouponServices();
                    }
                }
            }
            return sc;
        }

        //public T ReceiveMessage<T>(string msmqname)
        //{
        //    try
        //    {
        //        System.Messaging.MessageQueue mq = CreatedMessageQueue(msmqname);
        //        lock (mq)
        //        {
        //            mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
        //        }
        //        //从队列中接收消息
        //        System.Messaging.Message myMessage = mq.Peek();
        //        return (T)myMessage.Body; //获取消息的内容
        //    }
        //    catch (MessageQueueException e)
        //    {
        //        throw e;
        //    }
        //    catch (InvalidCastException e)
        //    {
        //        throw e;
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //}



        public T ReceiveMessage<T>(string msmqname)
        {
            try
            {
                System.Messaging.MessageQueue mq = CreatedMessageQueue(msmqname);
                lock (mq)
                {
                    mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
                }
                //从队列中接收消息
                System.Messaging.Message myMessage = mq.Receive();
                return (T)myMessage.Body; //获取消息的内容
            }
            catch (MessageQueueException e)
            {
                ErrorLogTxt.GetInstance("读取队列异常日志").Write(e.Message);

                throw e;
            }
            catch (InvalidCastException e)
            {
                ErrorLogTxt.GetInstance("读取队列异常日志").Write(e.Message);
                throw e;
            }
            catch (Exception e)
            {
                ErrorLogTxt.GetInstance("读取队列异常日志").Write(e.Message);
                throw e;
            }
        }


        public List<T> ReceiveAllMessage<T>(string MsMqName)
        {
            System.Messaging.MessageQueue mq = CreatedMessageQueue(MsMqName);
            if (mq != null)
            {
                lock (mq)
                {
                    List<T> list = new List<T>();
                    try
                    {
                        mq.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });
                        System.Messaging.MessageEnumerator msg = mq.GetMessageEnumerator2();
                        while (msg.MoveNext())
                        {
                            Message oc = msg.Current;
                            T endity = (T)oc.Body;
                            list.Add(endity);
                        }
                        return list;
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return null;
        }

        public void SendMessageQueue<T>(string msmqname, T msg)
        {
            //连接到本地的队列

            System.Messaging.MessageQueue mq = CreatedMessageQueue(msmqname);

            Message myMessage = new Message();

            myMessage.Body = msg;

            myMessage.Formatter = new XmlMessageFormatter(new Type[] { typeof(T) });

            MessageQueueTransaction myTransaction = new MessageQueueTransaction();

            //启动事务

            // myTransaction.Begin();

            //发送消息到队列中

            mq.Send(myMessage);  //加了事务

            //提交事务

            // myTransaction.Commit();
        }

        public void SendMessageQuque<T>(List<T> list, string MsMqName)
        {

            //ErrorLogTxt.GetInstance("优惠券写入队列日志11111").Write("写入的数量为: " + list.Count);
            try
            {
                foreach (T entity in list)
                {
                    System.Messaging.MessageQueue mq = CreatedMessageQueue(MsMqName);
                    System.Messaging.Message myMessage = new System.Messaging.Message();
                    myMessage.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(T) });
                    myMessage.Body = entity;
                    myMessage.Label = DateTime.Now.ToString();
                    mq.Send(myMessage);
                }

            }
            catch (Exception ex)
            {
                Maticsoft.Common.ErrorLogTxt.GetInstance("优惠券写入队列异常").Write("异常信息为: " + ex.Message);
                throw ex;
            }
        }


        public void SendMessageQuque<T>(List<T> list, string MsMqName, string lable)
        {
            //ErrorLogTxt.GetInstance("优惠券写入队列日志2222").Write("写入的数量为: " + list.Count);
            try
            {
                foreach (T entity in list)
                {
                    System.Messaging.MessageQueue mq = CreatedMessageQueue(MsMqName);
                    System.Messaging.Message myMessage = new System.Messaging.Message();
                    myMessage.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(T) });
                    myMessage.Body = entity;
                    myMessage.Label = lable;
                    mq.Send(myMessage);
                }

            }
            catch (Exception ex)
            {
                ErrorLogTxt.GetInstance("优惠券写入队列异常").Write(ex.Message);
                throw ex;
            }
        }






        public MessageQueue CreatedMessageQueue(string msmqname)
        {
            try
            {
                if (!MessageQueue.Exists(@".\private$\" + msmqname))
                {
                    queue = MessageQueue.Create(@".\private$\" + msmqname);
                    queue.SetPermissions("Everyone", MessageQueueAccessRights.FullControl);
                }
                else
                {
                    queue = new MessageQueue(@".\private$\" + msmqname);
                }
            }
            catch (MessageQueueException e)
            {
                ErrorLogTxt.GetInstance("创建队列异常").Write(e.Message);
                throw e;
            }
            catch (Exception e)
            {
                ErrorLogTxt.GetInstance("创建队列异常").Write(e.Message);
                throw e;
            }

            return queue;
        }


        public void DeleteMsMq(List<string> list, string MsMqName)
        {
            System.Messaging.MessageQueue mq = CreatedMessageQueue(MsMqName);
            if (mq != null)
            {
                lock (mq)
                {
                    try
                    {
                        foreach (string str in list)
                        {
                            mq.ReceiveById(str);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }
        public void DeleteMsMq(string msmqID, string MsMqName)
        {
            System.Messaging.MessageQueue mq = CreatedMessageQueue(MsMqName);
            if (mq != null)
            {
                lock (mq)
                {
                    try
                    {
                        mq.ReceiveById(msmqID);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
        }


        public void Dispose()
        {
            queue.Dispose();
        }
    }
}
