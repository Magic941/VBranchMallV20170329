
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Maticsoft.BLL.Shop.Products.Lucene;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Model.Shop.Products.Lucene;
using Maticsoft.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Maticsoft.BLL.Products.Lucene
{
    /// <summary>
    /// 本地索引队列器，主要任务是和远程进行沟通索引新增、删除、更新
    /// </summary>
    public class ProductIndexManagerLocal
    {
        public static readonly ProductIndexManagerLocal productIndex = new ProductIndexManagerLocal();
        public static ProductSearchAPI service;
        private ProductIndexManagerLocal()
        {
            service = new ProductSearchAPI(Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("ProductSearchServerIP"));
        }
        //请求队列 解决索引目录同时操作的并发问题
        private Queue<ProductIndexLocalData> productQueue = new Queue<ProductIndexLocalData>();
        /// <summary>
        /// 新增商品时 添加邢增索引请求至队列
        /// </summary>
        /// <param name="books"></param>
        public void Add(long productId)
        {
            var p = new ProductIndexLocalData();
            p.ProductIndexActionType = ProductIndexEnum.EnumProductIndexAction.Add;
            p.ProductId = productId;
            productQueue.Enqueue(p);
        }
        /// <summary>
        /// 删除一个索引，商品下架时、删除时
        /// </summary>
        /// <param name="bid"></param>
        public void Del(long productId)
        {
            var p = new ProductIndexLocalData();
            p.ProductIndexActionType = ProductIndexEnum.EnumProductIndexAction.Delete;
            p.ProductId = productId;
            productQueue.Enqueue(p);
        }

        /// <summary>
        /// 索引修改
        /// </summary>
        /// <param name="books"></param>
        public void Mod(long productId)
        {
            var p = new ProductIndexLocalData();
            p.ProductIndexActionType = ProductIndexEnum.EnumProductIndexAction.Update;
            p.ProductId = productId;
            productQueue.Enqueue(p);
        }

        public void StartNewThread()
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(QueueToIndex));
        }

        /// <summary>
        /// /定义一个线程 将队列中的数据取出来 插入索引库中
        /// </summary>
        /// <param name="para"></param>
        private void QueueToIndex(object para)
        {
            while (true)
            {
                if (productQueue.Count > 0)
                {
                    NotifyUpdate();
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }

        /// <summary>
        /// 通知远程更新
        /// </summary>
        private void NotifyUpdate()
        {
            while (productQueue.Count > 0)
            {
                var p = productQueue.Dequeue();
                try
                {
                    service.UpdateProductIndex(p.ProductId, p.ProductIndexActionType);
                }
                catch (Exception e) {

                    Maticsoft.BLL.SysManage.ErrorLogTxt.GetInstance("商品索引更新通知").Write("发生错误，原因为"+e.Message);
                }
            }
        }
    }
}
