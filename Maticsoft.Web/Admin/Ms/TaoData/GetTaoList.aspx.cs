using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.SysManage;
using Maticsoft.Common;
using Maticsoft.Web.Components;

namespace Maticsoft.Web.Admin.Ms.TaoData
{
    public partial class GetTaoList : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 596; } } //SNS_淘宝商品采集页

        Maticsoft.BLL.Shop.Products.ProductInfo infoBll=new ProductInfo();
        protected  string savePath = string.Format("/Upload/Shop/Images/Product/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        private string skuImageFile = string.Format("/Upload/Shop/Images/ProductsSkuImages/{0}/", DateTime.Now.ToString("yyyyMMdd"));
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                this.hfTaoBaoAppkey.Value = ConfigSystem.GetValueByCache("OpenAPI_Shop_TaoBaoAppkey");
               if (Session["TaoBao_Session_Key"] == null || String.IsNullOrWhiteSpace(Session["TaoBao_Session_Key"].ToString()))
                {
                    this.btnGetData.Visible = false;
                    this.btnAuthorize.Visible = true;
                    btnCancel.Visible = false;
                }
                else
                {
                    this.btnGetData.Visible = true;
                    this.btnAuthorize.Visible = false;
                    btnCancel.Visible = true;
                }
                // BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
        }


        #region gridView

        public override void VerifyRenderingInServerForm(Control control)
        {
        }


        #endregion 物理删除文件

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            if (Session["ProductList"] != null)
            {
                int pageIndex = this.AspNetPager1.CurrentPageIndex;
                int pageSize = this.AspNetPager1.PageSize;
                List<TaoBao.Domain.Product> ProductList = Session["ProductList"] as List<TaoBao.Domain.Product>;
                List<TaoBao.Domain.Product> list= ProductList.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                if (list == null || list.Count == 0)
                {
                    tableDataList.Visible = false;
                }
                else
                {
                    tableDataList.Visible = true;
                }
                DataListProduct.DataSource = list;
                DataListProduct.DataBind();
            }
        }
        private string GetSelIDlist()
        {
            string idlist = "";
            bool BxsChkd = false;
            for (int i = 0; i < DataListProduct.Items.Count; i++)
            {
                CheckBox ChkBxItem = (CheckBox)DataListProduct.Items[i].FindControl("ckProduct");
                HiddenField hfPhotoId = (HiddenField)DataListProduct.Items[i].FindControl("hfProduct");
                if (ChkBxItem != null && ChkBxItem.Checked)
                {
                    BxsChkd = true;
                    if (hfPhotoId.Value != null)
                    {
                        idlist += hfPhotoId.Value + ",";
                    }
                }
            }
            if (BxsChkd)
            {
                idlist = idlist.Substring(0, idlist.LastIndexOf(","));
            }
            return idlist;
        }

        #region 按钮处理

        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Server.MapPath(savePath)))
            {
                Directory.CreateDirectory(Server.MapPath(savePath));
            }
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "选择您要导入的商品");
                return;
            }
          
            var arryId = idlist.Split(',');
            if (Session["ProductList"] != null)
            {
                int pageIndex = this.AspNetPager1.CurrentPageIndex;
                int pageSize = this.AspNetPager1.PageSize;
                List<TaoBao.Domain.Item> ProductList = Session["ProductList"] as List<TaoBao.Domain.Item>;

                int count = 0;
                List<TaoBao.Domain.Item> SelectList =
                    ProductList.Where(c => arryId.Contains(c.NumIid.ToString())).ToList();
            
             
                foreach (var item in SelectList)
                {
                    if (chkRepeat.Checked)
                    {
                        if (!infoBll.Exists(item.NumIid.ToString()))
                        {
                            if (AddProduct(item))
                            {
                                count++;
                            }
                        }
                    }
                    else
                    {
                        if (AddProduct(item))
                        {
                            count++;
                        }
                    }

                }

                ProductList.RemoveAll(c => arryId.Contains(c.ProductId.ToString()));
                this.AspNetPager1.RecordCount = ProductList.Count;
                if ((pageIndex - 1) * pageSize >= ProductList.Count)
                {
                    this.AspNetPager1.CurrentPageIndex = pageIndex - 1;
                    DataListProduct.DataSource = ProductList.Skip((pageIndex - 2) * pageSize).Take(pageSize);
                }
                else
                {
                    DataListProduct.DataSource = ProductList.Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                DataListProduct.DataBind();
                Session["ProductList"] = ProductList;
                MessageBox.ShowSuccessTip(this, "成功导入【" + count + "】条数据");
            }

        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["TaoBao_Session_Key"] = null;
            btnCancel.Visible = false;
            btnAuthorize.Visible = true;
            btnGetData.Visible = false;
        }

        protected void btnImportAll_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Server.MapPath(savePath)))
            {
                Directory.CreateDirectory(Server.MapPath(savePath));
            }
         
            if (Session["ProductList"] != null)
            {
                List<TaoBao.Domain.Item> list = Session["ProductList"] as List<TaoBao.Domain.Item>;
                int count = 0; //bll.ImportData(userId, albumId, TaoCateId, list);
                foreach (var item in list)
                {
                    if (chkRepeat.Checked)
                    {
                        if (!infoBll.Exists(item.NumIid.ToString()))
                        {
                            if (AddProduct(item))
                            {
                                count++;
                            }
                        }
                    }
                    else
                    {
                        if (AddProduct(item))
                        {
                            count++;
                        }
                    }

                }
                this.AspNetPager1.RecordCount = 0;
                DataListProduct.DataSource = null;
                DataListProduct.DataBind();
                Session["ProductList"] = null;
                MessageBox.ShowSuccessTip(this, "成功导入【" + count + "】条数据");
            }
        }
        #endregion


        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnGetData_Click(object sender, EventArgs e)
        {
            int pageSize = Common.Globals.SafeInt(this.TopPageSize.Text, 20);
            int pageNo = Common.Globals.SafeInt(this.TopPageNo.Text, 1);
            this.AspNetPager1.PageSize = pageSize;
            string sessionKey = "";
            if (Session["TaoBao_Session_Key"] == null || String.IsNullOrWhiteSpace(Session["TaoBao_Session_Key"].ToString()))
            {
               MessageBox.ShowFailTip(this,"请先进行用户授权");
                return;
            }
            if (pageSize > 20)
            {
                MessageBox.ShowFailTip(this, "每次采集数量请不要超过20");
                return;
            }
            sessionKey = Session["TaoBao_Session_Key"].ToString();
            int TaoCateId = 0;
            string keyword = this.TopKeyWord.Text;
            string hasDiscount = ddlDiscount.SelectedValue;
            string hasShowcase = ddlShowcase.SelectedValue;
            List<TaoBao.Domain.Item> ProductList = infoBll.GetTaoListByUser(sessionKey, TaoCateId, keyword, pageNo, pageSize, hasDiscount, hasShowcase);
            if (ProductList != null && ProductList.Count > 0)
            {
                this.AspNetPager1.RecordCount = ProductList.Count;
                DataListProduct.DataSource = ProductList.Take(pageSize);
                DataListProduct.DataBind();
                Session["ProductList"] = ProductList;
            }
            else
            {
                MessageBox.ShowFailTip(this, "获取数据失败，请确保该授权用户有出售中的淘宝商品。");
                return;
            }
        }

        /// <summary>
        /// 形成时间戳，组成图片名字
        /// </summary>
        /// <returns></returns>
        public string CreateIDCode()
        {
            DateTime Time1 = DateTime.Now.ToUniversalTime();
            DateTime Time2 = Convert.ToDateTime("1970-01-01");
            TimeSpan span = Time1 - Time2;   //span就是两个日期之间的差额   
            string t = span.TotalMilliseconds.ToString("0");
            return t;
        }
        /// <summary>
        /// 生成缩略图
        /// </summary>
        /// <param name="imgname"></param>
        /// <param name="thumbImagePath"></param>
        public void MakeThumbnail(string imgname, out string thumbImagePath)
        {
            string saveThumbsPath = "/Upload/Shop/Images/ProductThumbs/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            if (!Directory.Exists(Server.MapPath(saveThumbsPath)))
            {
                Directory.CreateDirectory(Server.MapPath(saveThumbsPath));
            }
            
            List<Maticsoft.Model.Ms.ThumbnailSize> thumSizeList = Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Model.Ms.EnumHelper.AreaType.Shop);
            bool isAddWater = Maticsoft.BLL.SysManage.ConfigSystem.GetBoolValueByCache("System_ThumbImage_AddWater");
            //原图水印保存地址
            if (isAddWater)
            {
               string  imagePath = savePath + "W_";
                //生成临时原图水印图
                FileHelper.MakeWater(Server.MapPath(savePath + imgname), Server.MapPath(imagePath + imgname));
            }
            if (thumSizeList != null && thumSizeList.Count > 0)
            {
                foreach (var thumbnailSize in thumSizeList)
                {
                    ImageTools.MakeThumbnail(Server.MapPath(savePath + imgname), Server.MapPath(saveThumbsPath + thumbnailSize.ThumName + imgname), 
                        thumbnailSize.ThumWidth, thumbnailSize.ThumHeight, GetThumMode(thumbnailSize.ThumMode));
                }
            }
            thumbImagePath = saveThumbsPath + "{0}" + imgname;
        }
        /// <summary>
        /// 获取裁剪模式
        /// </summary>
        /// <param name="ThumMode"></param>
        /// <returns></returns>
        protected MakeThumbnailMode GetThumMode(int ThumMode)
        {
            MakeThumbnailMode mode = MakeThumbnailMode.None;
            switch (ThumMode)
            {
                case 0:
                    mode = MakeThumbnailMode.Auto;
                    break;
                case 1:
                    mode = MakeThumbnailMode.Cut;
                    break;
                case 2:
                    mode = MakeThumbnailMode.H;
                    break;
                case 3:
                    mode = MakeThumbnailMode.HW;
                    break;
                case 4:
                    mode = MakeThumbnailMode.W;
                    break;
                default:
                    mode = MakeThumbnailMode.Auto;
                    break;
            }
            return mode;
        }
        /// <summary>
        /// 添加商品
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool AddProduct(TaoBao.Domain.Item item)
        {
            string ImageUrl = "";
            string imagename = CreateIDCode() + ".jpg";
            //下载原图片，生成缩略图
            using (System.Net.WebClient mywebclient = new System.Net.WebClient())
            {
                ImageUrl = savePath + imagename; ;
                mywebclient.DownloadFile(item.PicUrl, Server.MapPath(ImageUrl));
            }
            string skuBaseJson = "{ 'SKU':'" + item.NumIid + DateTime.Now.ToString("mmssffff") + "', 'SalePrice': '" + item.Price + "','CostPrice':'" + item.Price +
                      "','Stock':'" + item.Num + "','AlertStock':'0','Weight':'0','Upselling':'1','SKUItems':[]}";
       
            string ThumbImageUrl = "";
            MakeThumbnail(imagename, out ThumbImageUrl);
            //获取商品的描述和 市场价 

            Model.Shop.Products.ProductInfo productInfo = new Model.Shop.Products.ProductInfo();
            productInfo.HasSKU = false;
            productInfo.ImageUrl = ImageUrl;
            productInfo.ThumbnailUrl1 = ThumbImageUrl;
            productInfo.LowestSalePrice = Common.Globals.SafeDecimal(item.Price,0);
            productInfo.MarketPrice = Common.Globals.SafeDecimal(item.Price, 0);
            productInfo.SaleStatus = 1;
            productInfo.AddedDate = DateTime.Now;
            productInfo.ProductName = item.Title;
            productInfo.ProductCode = item.NumIid.ToString();
            productInfo.CategoryId = -1;
            productInfo.Description = item.Desc;
            //SKU
            List<Model.Shop.Products.SKUInfo> skuList  = GetSKUInfo4Json(LitJson.JsonMapper.ToObject(skuBaseJson));
            productInfo.SkuInfos = skuList;
            productInfo.SupplierId = -1;
            productInfo.Product_Categories=new string[]{};
            long ProductId = 0;
          return  BLL.Shop.Products.ProductManage.AddProduct(productInfo, out ProductId);
        }

        private List<Model.Shop.Products.SKUInfo> GetSKUInfo4Json(LitJson.JsonData jsonData)
        {
            List<Model.Shop.Products.SKUInfo> list = new List<Model.Shop.Products.SKUInfo>();
            if (jsonData.IsArray && jsonData.Count > 0)
            {
                //开启SKU时
                foreach (LitJson.JsonData item in jsonData)
                {
                    list.Add(GetSKUInfo4Obj(item));
                }
            }
            else if (jsonData.IsObject)
            {
                //关闭SKU时
                list.Add(GetSKUInfo4Obj(jsonData));
            }
            return list;
        }
        private Model.Shop.Products.SKUInfo GetSKUInfo4Obj(LitJson.JsonData jsonData)
        {

                  ArrayList skuImageList = new ArrayList();
         ArrayList salePriceList = new ArrayList();

            Model.Shop.Products.SKUInfo skuInfo = null;
            if (!jsonData.IsObject) return null;

            skuInfo = new Model.Shop.Products.SKUInfo();

            //Base Info
            skuInfo.SKU = jsonData["SKU"].ToString();

            //CostPrice 允许为空
            string tmpCostPrice = jsonData["CostPrice"].ToString();
            if (!string.IsNullOrWhiteSpace(tmpCostPrice))
            {
                skuInfo.CostPrice = Globals.SafeDecimal(tmpCostPrice, decimal.MinusOne);
            }
            skuInfo.SalePrice = Globals.SafeDecimal(jsonData["SalePrice"].ToString(), decimal.MinusOne);
            salePriceList.Add(skuInfo.SalePrice);
            skuInfo.Stock = Globals.SafeInt(jsonData["Stock"].ToString(), -1);
            skuInfo.AlertStock = Globals.SafeInt(jsonData["AlertStock"].ToString(), -1);
            skuInfo.Weight = Globals.SafeInt(jsonData["Weight"].ToString(), -1);
            skuInfo.Upselling = Globals.SafeBool(jsonData["Upselling"].ToString(), false);

            //SKU Info
            if (jsonData["SKUItems"].IsArray && jsonData["SKUItems"].Count > 0)
            {
                foreach (LitJson.JsonData item in jsonData["SKUItems"])
                {
                    string skuImagepath = string.Empty;
                    if (!string.IsNullOrWhiteSpace(item["ImageUrl"].ToString()))
                    {
                        skuImagepath = item["ImageUrl"].ToString().Replace(savePath, skuImageFile);
                        string BaseImage = item["ImageUrl"].ToString().Replace(savePath, "");
                        if (!skuImageList.Contains(String.Format(BaseImage, "T32X32_")))
                        {
                            skuImageList.Add(String.Format(BaseImage, "T32X32_"));
                            skuImageList.Add(String.Format(BaseImage, "T130X130_"));
                            skuImageList.Add(String.Format(BaseImage, "T300X390_"));
                        }
                    }
                    skuInfo.SkuItems.Add(
                            new Model.Shop.Products.SKUItem
                            {
                                AttributeId = Globals.SafeLong(item["AttributeId"].ToString(), -1),
                                ValueId = Globals.SafeLong(item["ValueId"].ToString(), -1),
                                ImageUrl = skuImagepath,
                                ValueStr = item["ValueStr"].ToString()
                            }
                        );
                }
            }
            return skuInfo;
        }
    }
}