using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

using Maticsoft.BLL.Shop.Products;
using Maticsoft.BLL.Shop.PromoteSales;
using ProductImage = Maticsoft.Model.Shop.Products.ProductImage;
using Maticsoft.BLL.SysManage;
using Maticsoft.Model.Settings;
using System.Collections;

using System.Data.SqlTypes;
using System.Data;
using Maticsoft.Common;
namespace Maticsoft.Web.Admin.Shop.PromoteSales
{
    public partial class AddWeiXinGroupBuy : PageBaseAdmin
    {
        private string tempFile = string.Format("/Upload/Temp/{0}/", DateTime.Now.ToString("yyyyMMdd"));

        private Maticsoft.BLL.Shop.Products.CategoryInfo categoryInfoBll = new CategoryInfo();
        private Maticsoft.BLL.Shop.Products.ProductInfo productInfoBll = new ProductInfo();
        private Maticsoft.BLL.Shop.PromoteSales.WeiXinGroupBuy WeiXinbuyBll = new WeiXinGroupBuy();
        Maticsoft.BLL.Shop.Products.GoodsType GoodTypeBll = new BLL.Shop.Products.GoodsType();
        private const string DROPDOWNLIST_GOODSTYPE_PREFIX = "dllGoodsTypeID";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Size thumbSize = Maticsoft.Common.StringPlus.SplitToSize(
                    BLL.SysManage.ConfigSystem.GetValueByCache(SettingConstant.PRODUCT_NORMAL_SIZE_KEY),
                    '|', SettingConstant.ProductThumbSize.Width, SettingConstant.ProductThumbSize.Height);

                //商品分类
                this.ddlCateList.DataSource = categoryInfoBll.GetList("Depth=1");
                ddlCateList.DataTextField = "Name";
                ddlCateList.DataValueField = "CategoryId";
                ddlCateList.DataBind();
                ddlCateList.Items.Insert(0, new ListItem("请选择", "0"));
                this.txtSequence.Text = (WeiXinbuyBll.MaxSequence() + 1).ToString();

                DataSet ds1 = GoodTypeBll.GetGoodsActiveTypeList("");
                this.DdlGoodsActiveType.Items.Clear();
                DdlGoodsActiveType.Items.Add(new ListItem("请选择", "0"));
                if (!Maticsoft.Common.DataSetTools.DataSetIsNull(ds1))
                {
                    this.DdlGoodsActiveType.DataSource = ds1;
                    this.DdlGoodsActiveType.DataTextField = "Name";
                    this.DdlGoodsActiveType.DataValueField = "ID";
                    this.DdlGoodsActiveType.DataBind();
                }
                

                //BindShopGoodType();
                SetShopGoodsType(string.Empty);

                DateTime dt = DateTime.Now; //当前时间  
                DateTime startWeek = DateTime.Now.AddDays(7);//  默认七天后的日期

                this.txtStartDate.Text = dt.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtEndDate.Text = startWeek.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            // 检验是有有选择微信商品分类
            if (0 > GetGoodsTypeValue())
            {
                Response.Write("<script type='text/javascript'>alert('请选择微信商品分类!');</script>'");
                return;
            }

            Maticsoft.Model.Shop.PromoteSales.WeiXinGroupBuy WeiXinbuyModel = new Model.Shop.PromoteSales.WeiXinGroupBuy();
            long productId = Common.Globals.SafeLong(this.ddlProduct.SelectedValue, 0);
            int groupCount = Common.Globals.SafeInt(this.txtGroupCount.Text, 0);
            int maxCount = Common.Globals.SafeInt(this.txtMaxCount.Text, 0);
            int promotionLimitQu = Common.Globals.SafeInt(this.txtPromotionLimitQu.Text, 1);
            #region

            if (productId == 0)
            {
                Common.MessageBox.ShowFailTip(this, "请选择微信营销商品！");
                return;
            }
            decimal price = Common.Globals.SafeDecimal(txtPrice.Text, -1);
            if (price == -1)
            {
                Common.MessageBox.ShowFailTip(this, "请填写商品价格");
                return;
            }
            if (String.IsNullOrWhiteSpace(txtEndDate.Text))
            {
                Common.MessageBox.ShowFailTip(this, "请选择活动结束时间");
                return;
            }
            if (maxCount < groupCount)
            {
                Common.MessageBox.ShowFailTip(this, "限购总数量必须大于微信营销满足数量");
                return;
            }
            if (promotionLimitQu > groupCount)
            {
                Common.MessageBox.ShowFailTip(this, "限单笔购数量必须小于微信营销满足数量");
                return;
            }
            //判重
            if (WeiXinbuyBll.IsExists(productId))
            {
                Common.MessageBox.ShowFailTip(this, "该商品已加入微信营销活动");
                return;
            }
            int? selectedRegionId = Common.Globals.SafeInt(ajaxRegion.SelectedValue, 217);

            if (ddlCateList.SelectedValue == "0")
            {
                Common.MessageBox.ShowFailTip(this, "请选择商品分类");
                return;
            }
            #endregion
            string categoryId = ddlCateList.SelectedValue;
            string path = ddlCateList2.SelectedValue;
            BLL.Shop.Products.ProductInfo bllProductInfo = new ProductInfo();
            Maticsoft.Model.Shop.Products.ProductInfo productModel = bllProductInfo.GetModelByCache(productId);
            WeiXinbuyModel.Description = this.txtDesc.Text;
            WeiXinbuyModel.EndDate = Common.Globals.SafeDateTime(txtEndDate.Text, DateTime.Now);
            WeiXinbuyModel.Price = Common.Globals.SafeDecimal(price, 0);
            WeiXinbuyModel.ProductId = productId;
            WeiXinbuyModel.GroupBase = Common.Globals.SafeInt(txtGroupBase.Text, 0);
            WeiXinbuyModel.Sequence = Common.Globals.SafeInt(txtSequence.Text, 0);
            WeiXinbuyModel.Status = chkStatus.Checked ? 1 : 0;
            WeiXinbuyModel.PromotionType = chkPromotionType.Checked ? 1 : 0;
            WeiXinbuyModel.FinePrice = Common.Globals.SafeDecimal(this.txtFinePrice.Text, 0);
            WeiXinbuyModel.RegionId = selectedRegionId.Value;
            WeiXinbuyModel.GoodsActiveType =int.Parse(this.DdlGoodsActiveType.SelectedValue);
            WeiXinbuyModel.GroupCount = groupCount;
            WeiXinbuyModel.MaxCount = maxCount;
            WeiXinbuyModel.StartDate = Common.Globals.SafeDateTime(txtStartDate.Text, DateTime.MinValue);
            WeiXinbuyModel.CategoryId = Common.Globals.SafeInt(categoryId, 0);
            WeiXinbuyModel.PromotionLimitQu = promotionLimitQu;
            int intGoodsTypeID = Common.Globals.SafeInt(this.dllGoodsTypeID2.SelectedValue, 0);
            intGoodsTypeID = GetGoodsTypeValue();
            WeiXinbuyModel.GoodsTypeID = intGoodsTypeID;
            WeiXinbuyModel.FloorID = Common.Globals.SafeInt(ddlIsIndex.SelectedValue, 0);
            WeiXinbuyModel.IsIndex = chkIsIndexP.Checked ? 1 : 0;//是否是首页推荐
            WeiXinbuyModel.LeastbuyNum = Common.Globals.SafeInt(this.txtLeastbuyNum.Text, 1);

            //待上传的图片名称
            string savePath = string.Format("/Upload/Shop/Images/Product/GroupBy/{0}/", DateTime.Now.ToString("yyyyMMdd"));

            ArrayList imageList = new ArrayList();

            //if (string.IsNullOrWhiteSpace(this.hfFileUrl.Value))
            //{
            //    Maticsoft.Common.MessageBox.ShowFailTip(this, "请选择要上传的图片！");
            //    return;
            //}
            #region
            if (!string.IsNullOrWhiteSpace(HiddenField_ISModifyImage.Value))
            {
                string imageUrl = string.Format(hfFileUrl.Value, "");
                imageList.Add(imageUrl.Replace(tempFile, ""));
                WeiXinbuyModel.GroupBuyImage = imageUrl.Replace(tempFile, savePath);
            }
            else
            {
                WeiXinbuyModel.GroupBuyImage = this.hfFileUrl.Value;
            }

            if (imageList.Count > 0)
            {
                foreach (var file in imageList)
                {
                    Maticsoft.BLL.SysManage.FileManager.MoveImageForFTP(tempFile + file, savePath);
                }
            }

            if (path == "0")
            {
                WeiXinbuyModel.CategoryPath = categoryId;
            }
            else
            {
                WeiXinbuyModel.CategoryPath = categoryId + "|" + path;
            }
            if (null != productModel)
            {
                WeiXinbuyModel.ProductName = productModel.ProductName;
                WeiXinbuyModel.ProductCategory = ddlCateList.SelectedItem.Text;
            }
            #endregion

            //判断当前这条数据是否存在
            if (WeiXinbuyModel.IsIndex == 1)
            {
                DataSet ds = WeiXinbuyBll.GetFloor_IsDisplay(WeiXinbuyModel.FloorID, WeiXinbuyModel.Sequence, 1, 1, productId);
                if (ds != null)
                {
                    WeiXinbuyBll.Update_status(WeiXinbuyModel.FloorID, WeiXinbuyModel.Sequence, 1, 1);
                }

            }
            int result = WeiXinbuyBll.Add(WeiXinbuyModel);
            if (result > 0)
            {
                Common.MessageBox.ShowSuccessTip(this, "操作成功", "WeiXinGroupBuyList.aspx");
            }
            else
            {
                Common.MessageBox.ShowFailTip(this, "操作失败");
            }
        }

        protected void ddlCateList_Changed(object sender, EventArgs e)
        {
            int categoryId = Common.Globals.SafeInt(ddlCateList.SelectedValue, 0);
            if (categoryId == 0)
            {
                ddlCateList2.Visible = false;
                return;
            }

            //绑定二级分类
            this.ddlCateList2.DataSource = categoryInfoBll.GetList("ParentCategoryId=" + categoryId);
            ddlCateList2.DataTextField = "Name";
            ddlCateList2.DataValueField = "CategoryId";
            ddlCateList2.DataBind();
            ddlCateList2.Items.Insert(0, new ListItem("请选择", "0"));
            ddlCateList2.Visible = true;

            ddlProduct.DataSource = productInfoBll.GetProductsByCid(categoryId);
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
        }

        protected void ddlCateList2_Changed(object sender, EventArgs e)
        {
            int categoryId = Common.Globals.SafeInt(ddlCateList2.SelectedValue, 0);
            if (categoryId == 0)
            {
                categoryId = Common.Globals.SafeInt(ddlCateList.SelectedValue, 0);
            }

            ddlProduct.DataSource = productInfoBll.GetProductsByCid(categoryId);
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductId";
            ddlProduct.DataBind();
        }

        /// <summary>
        /// 绑定商品分类
        /// </summary>
        private void BindShopGoodType()
        {
            DataSet ds = GoodTypeBll.GetList("PID = 0");
            this.dllGoodsTypeID0.Items.Clear();
            if (!Maticsoft.Common.DataSetTools.DataSetIsNull(ds))
            {
                this.dllGoodsTypeID0.DataSource = ds;
                this.dllGoodsTypeID0.DataTextField = "GoodTypeName";
                this.dllGoodsTypeID0.DataValueField = "GoodTypeID";
                this.dllGoodsTypeID0.DataBind();
            }
            dllGoodsTypeID0.SelectedIndex = 0;

          

            
        }

        //protected void dllGoodsTypeID0_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int intGoodsType = Common.Globals.SafeInt(this.dllGoodsTypeID0.SelectedValue, 0);
        //    BindShopChildGoodType(intGoodsType);
        //}

        #region 微信商品分类相关方法

        /// <summary>
        /// 根据类型路径绑定分类,比如 2|25|46
        /// </summary>
        /// <param name="typePath"></param>
        /// <param name="selectedValue"></param>
        private void SetShopGoodsType(string typePath)
        {
            InitAllShopGoodsTypes();
            if (string.IsNullOrWhiteSpace(typePath))
            {
                ShopGoodsTypeBindData(this.dllGoodsTypeID0, 0, -1);
                return;
            }
            IList<int> lstTypes = new List<int>();
            if (typePath.IndexOf("|") >= 0)
            {
                typePath.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList().ForEach(item => { lstTypes.Add(Globals.SafeInt(item, 0)); });
            }
            else
            {
                lstTypes.Add(Globals.SafeInt(typePath, 0));
            }
            if (null != lstTypes && lstTypes.Count() > 0)
            {
                DropDownList ddl = null;
                for (int i = 0; i < lstTypes.Count(); i++)
                {
                    string ddlCtlId = DROPDOWNLIST_GOODSTYPE_PREFIX + i;
                    int parentId = 0 == i ? 0 : Globals.SafeInt(lstTypes.ElementAtOrDefault(i - 1), 0);
                    System.Web.UI.WebControls.DropDownList control = this.Master.FindControl("ContentPlaceHolder1").FindControl(ddlCtlId) as System.Web.UI.WebControls.DropDownList;
                    if (null == control) continue;
                    ShopGoodsTypeBindData(control, parentId, lstTypes.ElementAtOrDefault(i));
                }
            }
        }

        /// <summary>
        /// 初始化所有以"dllGoodsTypeID"开头,以数字结尾的下拉控件,比如:dllGoodsTypeID0, dllGoodsTypeID1, dllGoodsTypeID2...
        /// </summary>
        private void InitAllShopGoodsTypes()
        {
            IEnumerable<DropDownList> ddlCtls = GetAllDropDownListCtl();
            if (null != ddlCtls && ddlCtls.Count() > 0) ddlCtls.ToList().ForEach(ctl => { InitShopGoodsType(ctl); });
        }

        /// <summary>
        /// 初始化指定的下拉控件
        /// </summary>
        /// <param name="ddCtlID"></param>
        private void InitShopGoodsType(System.Web.UI.WebControls.DropDownList ddlCtl)
        {
            ddlCtl.Items.Clear();
            ddlCtl.Items.Add(new ListItem("请选择", "-1"));
            ddlCtl.SelectedIndex = 0;
            ddlCtl.Visible = false;
        }

        /// <summary>
        /// 重置指定分类所有子类下拉列表控件
        /// </summary>
        /// <param name="ddlCtl"></param>
        private void ResetDescendant(DropDownList ddlCtl)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"\D");
            int sourceCtlIndex = Common.Globals.SafeInt(reg.Replace(ddlCtl.ID, string.Empty), -1);
            if (sourceCtlIndex < 0) return;
            IEnumerable<DropDownList> ddlCtls = GetAllDropDownListCtl();
            if (null != ddlCtls && ddlCtls.Count() > 0)
            {
                ddlCtls.ToList().ForEach(ctl =>
                {
                    if (Common.Globals.SafeInt(reg.Replace(ctl.ID, string.Empty), -1) == sourceCtlIndex + 1)
                    {
                        ResetDrowDwonList(ctl, Globals.SafeInt(ddlCtl.SelectedValue, -1), -1, true);
                    }
                    if (Common.Globals.SafeInt(reg.Replace(ctl.ID, string.Empty), -1) > sourceCtlIndex + 1)
                    {
                        InitShopGoodsType(ctl);
                    }
                });
            }
        }

        /// <summary>
        /// 获取所有微信下拉列表控件
        /// </summary>
        /// <returns></returns>
        private IEnumerable<DropDownList> GetAllDropDownListCtl()
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(DROPDOWNLIST_GOODSTYPE_PREFIX + @"\d+");
            return this.Master.FindControl("ContentPlaceHolder1").Controls.Cast<System.Web.UI.Control>().Where(ctl => ctl.GetType().ToString().Equals("System.Web.UI.WebControls.DropDownList") && reg.IsMatch(ctl.ID)).Cast<System.Web.UI.WebControls.DropDownList>();
        }

        /// <summary>
        /// 下拉列表控件绑定数据(联动)
        /// </summary>
        /// <param name="ddlCtl"></param>
        /// <param name="parentTypeId"></param>
        /// <param name="selectedValue"></param>
        private void ShopGoodsTypeBindData(System.Web.UI.WebControls.DropDownList ddlCtl, int parentTypeId, int selectedValue)
        {
            int sourceCtlIndex = Common.Globals.SafeInt(new System.Text.RegularExpressions.Regex(@"\D").Replace(ddlCtl.ID, string.Empty), -1);
            if (sourceCtlIndex < 0) return;
            if (null == ddlCtl || parentTypeId < 0) return;
            // 当前下拉列表控件设置数据源
            ResetDrowDwonList(ddlCtl, parentTypeId, selectedValue, true);
            // 设置下一级分类下拉列表控件
            int targetCtlIndex = sourceCtlIndex + 1;
            string targetCtlId = new System.Text.RegularExpressions.Regex(@"\d").Replace(ddlCtl.ID, string.Empty) + targetCtlIndex;
            System.Web.UI.WebControls.DropDownList targetCtl = this.Master.FindControl("ContentPlaceHolder1").FindControl(targetCtlId) as System.Web.UI.WebControls.DropDownList;
            if (null == targetCtl) return;
            ResetDrowDwonList(targetCtl, Globals.SafeInt(ddlCtl.SelectedValue, -1), -1, true);
        }

        /// <summary>
        /// 重置下拉控件
        /// </summary>
        /// <param name="ddlCtl"></param>
        /// <param name="parentTypeId"></param>
        /// <param name="selectedValue"></param>
        /// <param name="visible"></param>
        private void ResetDrowDwonList(System.Web.UI.WebControls.DropDownList ddlCtl, int parentTypeId, int selectedValue, bool visible)
        {
            if (null == ddlCtl) return;
            InitShopGoodsType(ddlCtl);
            if (parentTypeId < 0) return;
            DataSet ds = GoodTypeBll.GetList("PID = " + parentTypeId);
            if (!Maticsoft.Common.DataSetTools.DataSetIsNull(ds))
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    ddlCtl.Items.Add(new ListItem(row["GoodTypeName"].ToString(), row["GoodTypeID"].ToString()));
                }
                if (null != ddlCtl.Items.FindByValue(selectedValue.ToString()))
                {
                    ddlCtl.SelectedValue = selectedValue.ToString();
                }
                else
                {
                    ddlCtl.SelectedIndex = 0;
                }
                ddlCtl.Visible = visible;
            }
        }


        #region 微信商品分类下拉列表选择项改变事件处理
        protected void dllGoodsTypeID0_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetDescendant(this.dllGoodsTypeID0);
        }

        protected void dllGoodsTypeID1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetDescendant(this.dllGoodsTypeID1);
        }

        protected void dllGoodsTypeID2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetDescendant(this.dllGoodsTypeID2);
        }

        //protected void dllGoodsTypeID3_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    ResetDescendant(this.dllGoodsTypeID3);
        //}
        #endregion

        /// <summary>
        /// 获取选择微信商品分类的值(如果多级分类,则选中最后一级分类的值)
        /// </summary>
        /// <returns></returns>
        private int GetGoodsTypeValue()
        {
            int ret = -1;
            IEnumerable<DropDownList> ctls = GetAllDropDownListCtl();
            if (null != ctls && ctls.Count() > 0)
            {
                return Globals.SafeInt(ctls.Where(ctl => ctl.Visible).LastOrDefault().SelectedValue, -1);
            }
            return ret;
        }

        #endregion
    }
}