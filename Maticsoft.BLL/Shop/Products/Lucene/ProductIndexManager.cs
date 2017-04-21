
using Lucene.Net.Analysis;
using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Analysis.Standard;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Maticsoft.BLL.Shop.Products.Lucene;
using Maticsoft.BLL.SysManage;
using Maticsoft.Model.Shop.Products;
using Maticsoft.Model.Shop.Products.Lucene;
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
    /// 负责创建索引、接收改变商品信息并修改相应的索引、自动生成索引多线程
    /// </summary>
    public class ProductIndexManager
    {
        public static readonly ProductIndexManager productIndex = new ProductIndexManager();

        //分词类型，2=盘古二元分词或1=一元分词
        public static readonly int analyzerType = 2;//PanGuAnalyzer
        /// <summary>
        /// 索引存储位置
        /// </summary>
        private static string indexPath
        {
            get
            {

                var folder = BLL.SysManage.ConfigSystem.GetValueByCache("Shop_ProductIndex");
                if (string.IsNullOrEmpty(folder))
                {
                    folder = "c://HaoLinShopProductIndex/";
                }

                return folder;
            }
        }
        private ProductIndexManager()
        {
        }
        //请求队列 解决索引目录同时操作的并发问题
        private Queue<ProductInfo> productQueue = new Queue<ProductInfo>();
        /// <summary>
        /// 新增商品时 添加邢增索引请求至队列
        /// </summary>
        /// <param name="books"></param>
        public void Add(ProductInfo product)
        {

            product.ProductIndexType = IndexType.Insert;

            productQueue.Enqueue(product);
        }
        /// <summary>
        /// 删除Books表信息时 添加删除索引请求至队列
        /// </summary>
        /// <param name="bid"></param>
        public void Del(long productid)
        {
            var product = new ProductInfo();
            product.ProductIndexType = IndexType.Delete;
            product.ProductId = productid;
            productQueue.Enqueue(product);
        }

        /// <summary>
        /// 索引修改
        /// </summary>
        /// <param name="books"></param>
        public void Mod(ProductInfo product)
        {
            product.ProductIndexType = IndexType.Modify;
            productQueue.Enqueue(product);
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
                    CRUDIndex();
                }
                else
                {
                    Thread.Sleep(3000);
                }
            }
        }

        /// <summary>
        ///  从队列中创建索引,远程的修改需求全部通过接口发到队列中，然后对索引进行变动
        /// </summary>
        private void CRUDIndex()
        {
            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NativeFSLockFactory());
            //索引目录是否存在
            bool isExist = IndexReader.IndexExists(directory);
            if (isExist)
            {
                if (IndexWriter.IsLocked(directory))
                {
                    IndexWriter.Unlock(directory);
                }
            }
            Analyzer analyzer = new StandardAnalyzer();
            if (analyzerType == 2)
                analyzer = new PanGuAnalyzer();
            //创建索引写器
            IndexWriter writer = new IndexWriter(directory, analyzer, !isExist, IndexWriter.MaxFieldLength.UNLIMITED);

            while (productQueue.Count > 0)
            {
                try
                {
                    Document document = new Document();
                    var product = productQueue.Dequeue();
                    if (product.ProductIndexType == IndexType.Insert)
                    {
                        document = GenerateProductDocument(product);
                        writer.AddDocument(document);
                    }
                    if (product.ProductIndexType == IndexType.Delete)
                    {
                        writer.DeleteDocuments(new Term(ProductIndexField.PRODUCTID, product.ProductId.ToString()));
                    }
                    if (product.ProductIndexType == IndexType.Modify)
                    {
                        //先删除 再新增
                        writer.DeleteDocuments(new Term(ProductIndexField.PRODUCTID, product.ProductId.ToString()));
                        document = GenerateProductDocument(product);

                        writer.AddDocument(document);
                    }
                }
                catch (Exception e2)
                {

                    ErrorLogTxt.GetInstance("商品索引生成").Write("索引生成报错!");
                }
            }
            writer.Close();
            directory.Close();
        }


        /// <summary>
        /// 据关键字来搜索商品，商城内部全搜索
        /// 可以按搜索最佳匹配度Sort.RELEVANCE、销量、价格及范围、上架时间来排列数据
        /// </summary>
        /// <param name="keyWords">关键字，多关键字用|分隔</param>
        /// <param name="sortMode">排序方式：以按搜索最佳匹配度、销量、价格及范围、上架时间来排列数据</param>
        /// <param name="priceRange">价格范围 例如： 0-99 </param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页尺寸</param>
        /// <returns></returns>
        public ProductSearchResult SearchInMall(string keyWords, ProductIndexEnum.EnumSearchSortType sortMode, string priceRange, int? pageIndex = 1, int? pageSize = 30)
        {
            ProductSearchResult result = new ProductSearchResult();
            result.PageIndex = (int)pageIndex;
            result.PageSize = (int)pageSize;

            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            //搜索条件
            var splitKeyWords1 = SplitContent.SplitWords(keyWords);
            var splitKeyWords = splitKeyWords1.ToList();
            List<string> searchKeyWords = new List<string>();
            searchKeyWords.Add(ProductIndexField.PRODUCTNAME);
            searchKeyWords.Add(ProductIndexField.PRODUCTCODE);
            searchKeyWords.Add(ProductIndexField.PRODUCTDESCLONG);
            searchKeyWords.Add(ProductIndexField.SEOTITLE);
            searchKeyWords.Add(ProductIndexField.SEODESCRIPTION);
            searchKeyWords.Add(ProductIndexField.SUBHEAD);
            searchKeyWords.Add(ProductIndexField.BRANDNAME);
            searchKeyWords.Add(ProductIndexField.ATTRIBUTENAME);
            searchKeyWords.Add(ProductIndexField.KEYWORDS);
            searchKeyWords.Add(ProductIndexField.PRODUCTTAGS);
            searchKeyWords.Add(ProductIndexField.PRODUCTCATEGORYTAGS);
            //searchKeyWords.Add(ProductIndexField.ALLCATEGORYNAMES);

            BooleanQuery queryMain = new BooleanQuery();


            var aryPricerange = priceRange.Split(new char[] { '-' });
            decimal lower = 0;
            decimal upper = 0;
            ///价格范围过滤
            if (priceRange != "0-0" && aryPricerange.Length == 2)
            {
                lower = Convert.ToDecimal(aryPricerange[0]);
                upper = Convert.ToDecimal(aryPricerange[1]);
                //BooleanQuery queryMustPrice = new BooleanQuery();
                //var rangeQuery = new RangeQuery(new Term(ProductIndexField.CURRENTPRICE, lower), new Term(ProductIndexField.CURRENTPRICE, upper), true);

                //queryMustPrice.Add(rangeQuery, BooleanClause.Occur.MUST);
                //queryMain.Add(queryMustPrice, BooleanClause.Occur.MUST);
            }

            BooleanQuery queryAnd = new BooleanQuery();
           

            foreach (string field in searchKeyWords)
            {
                foreach (string keyword in splitKeyWords)
                { 
                    Analyzer analyzer = new StandardAnalyzer();
                    if (analyzerType == 2)
                        analyzer = new PanGuAnalyzer();
                    //var query = new TermQuery(new Term(field, keyword)); PanGuAnalyzer
                    //queryAnd.Add(query, BooleanClause.Occur.SHOULD);//这里设置 条件为Or关系
                    var query = new QueryParser(field, analyzer);
                    queryAnd.Add(query.Parse(keyword), BooleanClause.Occur.SHOULD);
                }
            }

            queryMain.Add(queryAnd, BooleanClause.Occur.MUST);

            //定义排序
            Sort sortobj = null;

            SortField sf = null;

            switch (sortMode)
            {
                case ProductIndexEnum.EnumSearchSortType.Default:
                    sortobj = Sort.RELEVANCE;
                    break;
                case ProductIndexEnum.EnumSearchSortType.SaleCountUp:
                    sf = new SortField(ProductIndexField.SALEQTY, 6);
                    sortobj = new Sort(sf);
                    break;
                case ProductIndexEnum.EnumSearchSortType.SaleCountDown:
                    sf = new SortField(ProductIndexField.SALEQTY, 6, true);
                    sortobj = new Sort(sf);
                    break;
                case ProductIndexEnum.EnumSearchSortType.PriceUp:
                    sf = new SortField(ProductIndexField.CURRENTPRICE, 5);
                    sortobj = new Sort(sf);
                    break;
                case ProductIndexEnum.EnumSearchSortType.PriceDown:
                    sf = new SortField(ProductIndexField.CURRENTPRICE, 5, true);
                    sortobj = new Sort(sf);
                    break;
                case ProductIndexEnum.EnumSearchSortType.AddedDateUp:
                    sf = new SortField(ProductIndexField.LAUNCHTIME, 2);
                    sortobj = new Sort(sf);
                    break;
                case ProductIndexEnum.EnumSearchSortType.AddedDateDown:
                    sf = new SortField(ProductIndexField.LAUNCHTIME, 2, true);
                    sortobj = new Sort(sf);
                    break;
                default:
                    sortobj = Sort.RELEVANCE;
                    break;
            }

            // TopScoreDocCollector collector = TopScoreDocCollector.create(100000, true);


            //TopFieldDocs Search(Query query, Filter filter, int n, Sort sort);
            var resultDocs = searcher.Search(queryMain, sortobj);

            // searcher.Search(queryOr, null, collector);//根据query查询条件进行查询，查询结果放入collector容器

            int start = ((int)pageIndex - 1) * (int)pageSize;
            int count = (int)pageSize;

            //ScoreDoc[] docs = resultDocs.scoreDocs;  //. .TopDocs(start, count).scoreDocs;
            //result.SearchCount = resultDocs.totalHits;


            //展示数据实体对象集合
            List<ProductInfoForProductIndex> productResult = new List<ProductInfoForProductIndex>();
            for (int i = 0; i < resultDocs.Length(); i++)
            {
                //int docId = resultDocs.Doc(i);
                Document doc = resultDocs.Doc(i); //searcher.Doc(docId);//根据文档id来获得文档对象Document

                var p = GenerateProduct(doc);
                if (productResult.FirstOrDefault(a => a.ProductId == p.ProductId) == null)
                {
                    if (priceRange != "0-0" && aryPricerange.Length == 2)
                    {
                        if (lower <= p.LowestSalePrice && p.LowestSalePrice <= upper)
                        {
                            productResult.Add(p);
                        }
                    }
                    else
                    {
                        if (p.LowestSalePrice > 0)
                            productResult.Add(p);
                    }
                }
            }
            result.SearchCount = productResult.Count();
            result.ProductsResult = productResult.Skip(start).Take(count).ToList();

            searcher.Close();

            return result;
        }


        /// <summary>
        /// 在分类中搜索,
        ///可以搜索指定分类下所有的商品，结合关键字，该地方未完成实现，默认实现展示该分类下所有商品。
        ///分类有一级分类、二级分类、三级分类
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="keyWords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ProductSearchResult SearchInCategory(int categoryId, string keyWords = "", int pageIndex = 1, int pageSize = 30)
        {
            ProductSearchResult result = new ProductSearchResult();
            result.PageIndex = (int)pageIndex;
            result.PageSize = (int)pageSize;

            var directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            //搜索条件

            var splitKeyWords1 = SplitContent.SplitWords(keyWords);
            var splitKeyWords = splitKeyWords1.ToList();
            List<string> searchFields = new List<string>();
            searchFields.Add(ProductIndexField.PRODUCTNAME);
            searchFields.Add(ProductIndexField.PRODUCTCODE);
            searchFields.Add(ProductIndexField.PRODUCTDESCLONG);
            searchFields.Add(ProductIndexField.SEOTITLE);
            searchFields.Add(ProductIndexField.SEODESCRIPTION);
            searchFields.Add(ProductIndexField.SUBHEAD);
            searchFields.Add(ProductIndexField.BRANDNAME);
            searchFields.Add(ProductIndexField.ATTRIBUTENAME);
            searchFields.Add(ProductIndexField.KEYWORDS);
            searchFields.Add(ProductIndexField.PRODUCTTAGS);
            searchFields.Add(ProductIndexField.ALLCATEGORYNAMES);

            BooleanQuery queryMain = new BooleanQuery();

            if (categoryId > 0)
            {
                var bqueryAnd = new BooleanQuery();
                var termquery = new TermQuery(new Term(ProductIndexField.CATEGORYID, categoryId.ToString()));
                bqueryAnd.Add(termquery, BooleanClause.Occur.MUST);
                queryMain.Add(bqueryAnd, BooleanClause.Occur.MUST);
            }

            var bQuery = new BooleanQuery();

            //对文字域的处理
            foreach (string field in searchFields)
            {
                // QueryParser queryParser = new QueryParser(field, new PanGuAnalyzer(true));
                foreach (string keyword in splitKeyWords)
                {
                    var query = new QueryParser(field, new StandardAnalyzer(true));
                    bQuery.Add(query.Parse(keyword), BooleanClause.Occur.SHOULD);
                }
            }

            queryMain.Add(bQuery, BooleanClause.Occur.MUST);
            TopScoreDocCollector collector = TopScoreDocCollector.create(100000, true);
            searcher.Search(queryMain, null, collector);

            ScoreDoc[] docs = collector.TopDocs((result.PageIndex - 1) * result.PageSize, result.PageSize).scoreDocs;
            result.SearchCount = collector.GetTotalHits();

            List<ProductInfoForProductIndex> productResult = new List<ProductInfoForProductIndex>();
            for (int i = 0; i < docs.Length; i++)
            {
                int docId = docs[i].doc;
                Document doc = searcher.Doc(docId);

                var p = GenerateProduct(doc);
                //搜索关键字高亮显示 使用盘古提供高亮插件
                // p.ProductName = SplitContent.HightLight(keyWords, p.ProductName);

                if (productResult.FirstOrDefault(a => a.ProductId == p.ProductId) == null)
                    productResult.Add(p);
            }
            result.ProductsResult = productResult;

            return result;
        }
        /// <summary>
        /// 在店铺内搜索 
        /// 店铺号和关键字之间是与关系，关键字之间是或关系
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="keyWords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ProductSearchResult SearchInStore(int storeId, string keyWords = "", int pageIndex = 1, int pageSize = 30)
        {
            ProductSearchResult result = new ProductSearchResult();
            result.PageIndex = (int)pageIndex;
            result.PageSize = (int)pageSize;


            FSDirectory directory = FSDirectory.Open(new DirectoryInfo(indexPath), new NoLockFactory());
            IndexReader reader = IndexReader.Open(directory, true);
            IndexSearcher searcher = new IndexSearcher(reader);
            //搜索条件
            // PhraseQuery query = new PhraseQuery();
            var splitKeyWords1 = SplitContent.SplitWords(keyWords);
            var splitKeyWords = splitKeyWords1.ToList();
            List<string> searchFields = new List<string>();
            searchFields.Add(ProductIndexField.PRODUCTNAME);
            searchFields.Add(ProductIndexField.PRODUCTCODE);
            searchFields.Add(ProductIndexField.PRODUCTDESCLONG);
            searchFields.Add(ProductIndexField.SEOTITLE);
            searchFields.Add(ProductIndexField.SEODESCRIPTION);
            searchFields.Add(ProductIndexField.SUBHEAD);
            searchFields.Add(ProductIndexField.BRANDNAME);
            searchFields.Add(ProductIndexField.ATTRIBUTENAME);
            searchFields.Add(ProductIndexField.KEYWORDS);
            searchFields.Add(ProductIndexField.PRODUCTTAGS);
            searchFields.Add(ProductIndexField.ALLCATEGORYNAMES);

            BooleanQuery queryMain = new BooleanQuery();


            // MultiFieldQueryParser parser = new MultiFieldQueryParser(searchFields.ToArray(), new PanGuAnalyzer());
            //每个词都在多域中查询
            //foreach (string keyword in splitKeyWords)
            //{
            //queryMain.Add(parser.Parse(keyWords), BooleanClause.Occur.SHOULD);
            // }
            if (storeId > 0)
            {
                var bqueryAnd = new BooleanQuery();
                var termquery = new TermQuery(new Term(ProductIndexField.SUPPLIERID, storeId.ToString()));
                bqueryAnd.Add(termquery, BooleanClause.Occur.MUST);
                // var queryAnd = new TermQuery(new Term(ProductIndexField.SUPPLIERID, storeId.ToString()));
                queryMain.Add(bqueryAnd, BooleanClause.Occur.MUST);
            }

            //if (storeId > 0)
            //{
            //    var queryMust = new QueryParser(ProductIndexField.SUPPLIERID, new PanGuAnalyzer(true));
            //    queryOr.Add(queryMust.Parse(storeId.ToString()), BooleanClause.Occur.MUST);
            //}

            var clauses = new List<BooleanClause.Occur>();

            //foreach (string field in searchFields)
            //{
            //    clauses.Add(BooleanClause.Occur.SHOULD);
            //}
            //clauses.Add(BooleanClause.Occur.MUST);

            var bQuery = new BooleanQuery();

            //对文字域的处理
            foreach (string field in searchFields)
            {
                // QueryParser queryParser = new QueryParser(field, new PanGuAnalyzer(true));
                foreach (string keyword in splitKeyWords)
                {
                    //var query = new TermQuery(new Term(field, keyword));
                    var query = new QueryParser(field, new PanGuAnalyzer(true));
                    bQuery.Add(query.Parse(keyword), BooleanClause.Occur.SHOULD);
                }
            }
            queryMain.Add(bQuery, BooleanClause.Occur.MUST);

            //public static Query Parse(string[] queries, string[] fields, BooleanClause.Occur[] flags, Analyzer analyzer);
            //searchFields.Add(ProductIndexField.SUPPLIERID);
            // splitKeyWords.Add(storeId.ToString());
            //MultiFieldQueryParser.Parse(splitKeyWords.ToArray(),searchFields.ToArray(), clauses.ToArray(), new PanGuAnalyzer(true));

            TopScoreDocCollector collector = TopScoreDocCollector.create(100000, true);
            searcher.Search(queryMain, null, collector);//根据query查询条件进行查询，查询结果放入collector容器

            //TopDocs 指定0到GetTotalHits() 即所有查询结果中的文档 如果TopDocs(20,10)则意味着获取第20-30之间文档内容 达到分页的效果
            ScoreDoc[] docs = collector.TopDocs((result.PageIndex - 1) * result.PageSize, result.PageSize).scoreDocs;
            result.SearchCount = collector.GetTotalHits();
            //展示数据实体对象集合
            List<ProductInfoForProductIndex> productResult = new List<ProductInfoForProductIndex>();
            for (int i = 0; i < docs.Length; i++)
            {
                int docId = docs[i].doc;//得到查询结果文档的id（Lucene内部分配的id）
                Document doc = searcher.Doc(docId);//根据文档id来获得文档对象Document


                var p = GenerateProduct(doc);
                //搜索关键字高亮显示 使用盘古提供高亮插件
                // p.ProductName = SplitContent.HightLight(keyWords, p.ProductName);

                if (productResult.FirstOrDefault(a => a.ProductId == p.ProductId) == null)
                    productResult.Add(p);
            }

            result.ProductsResult = productResult;

            return result;
        }


        /// <summary>
        /// 在团购中搜索 
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="keyWords"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public ProductSearchResult SearchInGroupBuy(int groupId, string keyWords, int pageIndex = 1, int pageSize = 30)
        {
            ProductSearchResult result = new ProductSearchResult();
            result.PageIndex = (int)pageIndex;
            result.PageSize = (int)pageSize;

            return null;
        }


        /// <summary>
        /// 从数据库开始加载商品生成所有索引，时间相对较长
        /// </summary>
        /// <returns></returns>
        public bool CreateProductIndexFromDataBase()
        {
            Maticsoft.BLL.Shop.Products.ProductInfo p = new Shop.Products.ProductInfo();
            var dataset = p.GetListALL("SaleStatus=1");
            if (dataset != null)
            {
                var allproducts = p.ProductDataTableToListAll(dataset.Tables[0]);
                foreach (var p2 in allproducts)
                {
                    this.Add(p2);
                }
                return true;
            }
            return false;

        }

        /// <summary>
        /// 文档转成商品信息
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public ProductInfoForProductIndex GenerateProduct(Document doc)
        {

            ProductInfoForProductIndex p = new ProductInfoForProductIndex();
            p.ProductName = doc.Get(ProductIndexField.PRODUCTNAME);
            p.Description = doc.Get(ProductIndexField.PRODUCTDESCLONG);
            long productid;
            long.TryParse(doc.Get(ProductIndexField.PRODUCTID), out productid);
            p.ProductId = productid;
            p.BrandId = Convert.ToInt32(doc.Get(ProductIndexField.BRANDID));
            p.BrandName = doc.Get(ProductIndexField.BRANDNAME);

            p.CategoryId = Convert.ToInt32(doc.Get(ProductIndexField.CATEGORYID));
            p.CategoryName = doc.Get(ProductIndexField.CATEGORYNAME);
            p.CategoryPath = doc.Get(ProductIndexField.CATEGORYPATH);

            p.MarketPrice = Convert.ToDecimal(doc.Get(ProductIndexField.MARKETPRICE));
            p.LowestSalePrice = Convert.ToDecimal(doc.Get(ProductIndexField.CURRENTPRICE));

            p.AddedDate = Convert.ToDateTime(doc.Get(ProductIndexField.LAUNCHTIME));

            p.SaleCounts = Convert.ToInt32(doc.Get(ProductIndexField.SALEQTY));

            p.Tags = doc.Get(ProductIndexField.PRODUCTTAGS);
            p.Subhead = doc.Get(ProductIndexField.SUBHEAD);

            p.ProductName = doc.Get(ProductIndexField.PRODUCTNAME);
            p.SupplierName = doc.Get(ProductIndexField.SUPPLIERNAME);
            p.SupplierId = Convert.ToInt32(doc.Get(ProductIndexField.SUPPLIERID));

            p.ProductCode = doc.Get(ProductIndexField.PRODUCTCODE);

            p.Meta_Keywords = doc.Get(ProductIndexField.KEYWORDS);

            p.Meta_Title = doc.Get(ProductIndexField.SEOTITLE);

            p.Meta_Description = doc.Get(ProductIndexField.SEODESCRIPTION);
            p.CategoryName = doc.Get(ProductIndexField.CATEGORYNAME);
            p.CategoryId = Convert.ToInt32(doc.Get(ProductIndexField.CATEGORYID));
            p.CategoryPath = doc.Get(ProductIndexField.CATEGORYPATH);
            p.Description = doc.Get(ProductIndexField.PRODUCTDESCLONG);
            p.ImageUrl = doc.Get(ProductIndexField.IMAGEURL);
            p.ThumbnailUrl1 = doc.Get(ProductIndexField.THUMBNAILURL);

            return p;
        }



        /// <summary>
        /// 产生商品的document
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="product"></param>
        public Document GenerateProductDocument(ProductInfo product)
        {
            Document document = new Document();
            document.Add(new Field(ProductIndexField.PRODUCTNAME, GetFieldValue(product.ProductName), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(ProductIndexField.CURRENTPRICE, GetFieldValue(product.LowestSalePrice), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field(ProductIndexField.MARKETPRICE, GetFieldValue(product.MarketPrice), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field(ProductIndexField.BRANDID, GetFieldValue(product.BrandId), Field.Store.YES, Field.Index.NOT_ANALYZED));

            document.Add(new Field(ProductIndexField.CATEGORYID, GetFieldValue(product.CategoryId), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field(ProductIndexField.SUPPLIERID, GetFieldValue(product.SupplierId.ToString()), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field(ProductIndexField.SALEQTY, product.SaleCounts.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field(ProductIndexField.LAUNCHTIME, product.AddedDate.ToString("yyyy-MM-dd HH:mm:ss"), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            document.Add(new Field(ProductIndexField.CATEGORYNAME, GetFieldValue(product.CategoryName), Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field(ProductIndexField.BRANDNAME, GetFieldValue(product.BrandName), Field.Store.YES, Field.Index.ANALYZED));

            //暂时不写，上线后再写
            //document.Add(new Field(ProductIndexField.COMMENTCOUNT, product., Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            //document.Add(new Field(ProductIndexField.COMMENTSCORE, product.Name, Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field(ProductIndexField.PRODUCTTAGS, GetFieldValue(product.Tags), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(ProductIndexField.PRODUCTCATEGORYTAGS, GetFieldValue(product.CTags), Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field(ProductIndexField.ATTRIBUTENAME, GetFieldValue(product.AttributeInfoNames), Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field(ProductIndexField.PRODUCTID, GetFieldValue(product.ProductId), Field.Store.YES, Field.Index.NOT_ANALYZED));

            document.Add(new Field(ProductIndexField.SUBHEAD, GetFieldValue(product.Subhead), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(ProductIndexField.SUPPLIERNAME, GetFieldValue(product.SupplierName), Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field(ProductIndexField.PRODUCTCODE, GetFieldValue(product.ProductCode), Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field(ProductIndexField.SEOKEYWORD, GetFieldValue(product.Meta_Keywords), Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field(ProductIndexField.SEOTITLE, GetFieldValue(product.Meta_Title), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(ProductIndexField.SEODESCRIPTION, GetFieldValue(product.Meta_Description), Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(ProductIndexField.PRODUCTDESCLONG, GetFieldValue(product.Description), Field.Store.NO, Field.Index.ANALYZED));
            document.Add(new Field(ProductIndexField.SUPPPARENTCATEGORYID, GetFieldValue(product.SuppParentCategoryId), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field(ProductIndexField.SUPPCATEGORYNAME, GetFieldValue(product.SupplierName), Field.Store.YES, Field.Index.ANALYZED));

            document.Add(new Field(ProductIndexField.IMAGEURL, GetFieldValue(product.ImageUrl), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));
            document.Add(new Field(ProductIndexField.THUMBNAILURL, GetFieldValue(product.ThumbnailUrl1), Field.Store.YES, Field.Index.NOT_ANALYZED_NO_NORMS));

            //document.Add(new Field(ProductIndexField.ALLCATEGORYNAMES, GetFieldValue(product.ALLCategoryNames), Field.Store.YES, Field.Index.ANALYZED));

            return document;
        }

        private string GetFieldValue(object value)
        {
            if (value == null)
            {
                return "";

            }
            return value.ToString();
        }


    }
}
