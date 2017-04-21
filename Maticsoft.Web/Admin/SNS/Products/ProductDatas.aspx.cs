using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.SNS;
using Maticsoft.Common;
using System.Text.RegularExpressions;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Maticsoft.Web.Admin.SNS.Products
{
    public partial class ProductDatas : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 596; } } //SNS_淘宝商品采集页

        Maticsoft.BLL.SNS.Products bll = new BLL.SNS.Products();
        Maticsoft.BLL.SNS.UserAlbums albumBll = new UserAlbums();
        protected string UploadPath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Maticsoft.BLL.Members.Users userBll = new BLL.Members.Users();
                int userId = userBll.GetDefaultUserId();
                this.txtUserId.Text = userId.ToString();
                this.txtUserId2.Text = userId.ToString();

                List<ViewModel.SNS.AlbumIndex> AlbumList = albumBll.GetListByUserId(userId);
                this.ddlAlbumList.DataSource = AlbumList;

                this.ddlAlbumList.DataTextField = "AlbumName";
                this.ddlAlbumList.DataValueField = "AlbumID";
                this.ddlAlbumList.DataBind();
                this.ddlAlbumList.Items.Insert(0, new ListItem("--请选择--", ""));

                this.ddlAlbumList2.DataSource = AlbumList;

                this.ddlAlbumList2.DataTextField = "AlbumName";
                this.ddlAlbumList2.DataValueField = "AlbumID";
                this.ddlAlbumList2.DataBind();
                this.ddlAlbumList2.Items.Insert(0, new ListItem("--请选择--", ""));

                // BindData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //BindData();
        }

        //批量转移
        //protected void btnMove_Click(object sender, EventArgs e)
        //{
        //    string idlist = GetSelIDlist();
        //    if (idlist.Trim().Length == 0) return;
        //    int SNSPhotoCateId = Globals.SafeInt(this.PhotoCategory.SelectedValue, 0);
        //    if (!bll.UpdateCateList(idlist, SNSPhotoCateId))
        //    {
        //        Maticsoft.Common.MessageBox.ShowSuccessTip(this, "批量转移分类失败");
        //    }
        //    gridView.OnBind();
        //}

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
                List<TaoBao.Domain.TbkItem> ProductList = Session["ProductList"] as List<TaoBao.Domain.TbkItem>;
                DataListProduct.DataSource = ProductList.Skip((pageIndex - 1) * pageSize).Take(pageSize);
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
            int TaoCateId = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            if (String.IsNullOrWhiteSpace(this.txtUserId.Text) || !Common.PageValidate.IsNumber(this.txtUserId.Text))
            {
                MessageBox.ShowFailTip(this, "请输入正确的用户ID");
                return;
            }
            int userId = Common.Globals.SafeInt(this.txtUserId.Text, 0);
            Maticsoft.BLL.Members.Users userBll = new BLL.Members.Users();
            Maticsoft.Model.Members.Users user = userBll.GetModel(userId);
            if (user == null)
            {
                MessageBox.ShowFailTip(this, "该用户ID不存在");
                return;
            }
            string idlist = GetSelIDlist();
            if (idlist.Trim().Length == 0)
            {
                MessageBox.ShowFailTip(this, "选择您要导入的商品");
                return;
            }
            int albumId = Common.Globals.SafeInt(ddlAlbumList.SelectedValue, 0);
            if (albumId == 0)
            {
                MessageBox.ShowFailTip(this, "请选择需要导入数据的专辑");
                return;
            }
            var arryId = idlist.Split(',');
            bool IsReRepeat = this.chkRepeat.Checked;
            if (Session["ProductList"] != null)
            {
                int pageIndex = this.AspNetPager1.CurrentPageIndex;
                int pageSize = this.AspNetPager1.PageSize;
                List<TaoBao.Domain.TbkItem> ProductList = Session["ProductList"] as List<TaoBao.Domain.TbkItem>;
                int count = bll.ImportData(user.UserID, albumId, TaoCateId, ProductList.Where(c => arryId.Contains(c.NumIid.ToString())).ToList(), IsReRepeat);
                ProductList.RemoveAll(c => arryId.Contains(c.NumIid.ToString()));
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

        #region Execel 数据导入
        /// <summary>
        /// Execel 数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImportExcel_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(this.txtUserId2.Text) || !Common.PageValidate.IsNumber(this.txtUserId2.Text))
            {
                MessageBox.ShowFailTip(this, "请输入正确的用户ID");
                return;
            }
            int userId = Common.Globals.SafeInt(this.txtUserId2.Text, 0);
            Maticsoft.BLL.Members.Users userBll = new BLL.Members.Users();
            Maticsoft.Model.Members.Users user = userBll.GetModel(userId);
            if (user == null)
            {
                MessageBox.ShowFailTip(this, "该用户ID不存在");
                return;
            }
            int albumId = Common.Globals.SafeInt(ddlAlbumList2.SelectedValue, 0);
            if (albumId == 0)
            {
                MessageBox.ShowFailTip(this, "请选择需要导入数据的专辑");
                return;
            }
            int categoryId = Common.Globals.SafeInt(SNSCategoryDropList.SelectedValue, 0);
            string FileName = uploadExcel.PostedFile.FileName;
            string ErrorMsg = "出现异常，请检查您的数据格式";
            int Count = 0;
            if (!uploadExcel.HasFile)
            {
                Common.MessageBox.ShowSuccessTip(this, "请上传文件");
                return;
            }
            if (!Directory.Exists(HttpContext.Current.Server.MapPath(UploadPath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(UploadPath));
            }
            uploadExcel.PostedFile.SaveAs(Server.MapPath(UploadPath) + FileName);
            bool Repeat = chkExcelRepeat.Checked;
            if (ExcelImport(UploadPath, FileName, userId, albumId, categoryId, Repeat, out ErrorMsg, ref Count))
            {
                Common.MessageBox.ShowSuccessTip(this, "成功插入" + Count + "条数据");
            }
            else
            {
                Common.MessageBox.ShowSuccessTip(this, "插入失败,信息:" + ErrorMsg + "提示：检查您填写数据的数据格式");
            }

        }
        /// <summary>
        /// Excel数据导入
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="FileName"></param>
        /// <param name="userid"></param>
        /// <param name="albumid"></param>
        /// <param name="categoryId"></param>
        /// <param name="ErrorMsg"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public bool ExcelImport(string Path, string FileName, int userid, int albumid, int categoryId, bool Repeat, out string ErrorMsg, ref int Count)
        {
            string sheetName = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("SNS_TaoBaoExcel_SheetName");
            if (String.IsNullOrWhiteSpace(sheetName))
            {
                sheetName = "Page1";
            }
            DataSet ExcelData = new DataSet();
            using (FileStream fs = new FileStream(Server.MapPath(Path + FileName), FileMode.OpenOrCreate))
            {
                //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
                HSSFWorkbook workbook = new HSSFWorkbook(fs);

                //获取excel的第一个sheet
                ISheet sheet = workbook.GetSheet(sheetName);

                DataTable table = new DataTable();
                //获取sheet的首行
                IRow headerRow = sheet.GetRow(0);

                //一行最后一个方格的编号 即总的列数
                int cellCount = headerRow.LastCellNum;

                for (int i = headerRow.FirstCellNum; i < cellCount; i++)
                {
                    DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                    table.Columns.Add(column);
                }
                //最后一列的标号  即总的行数
                //int rowCount = sheet.LastRowNum;
                for (int i = (sheet.FirstRowNum + 1); i <=sheet.LastRowNum; i++)
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                            dataRow[j] = row.GetCell(j).ToString();
                    }
                    table.Rows.Add(dataRow);
                }
                ExcelData.Tables.Add(table);
                try
                {
                    Count = bll.ImportExcelData(userid, albumid, categoryId, ExcelData.Tables[0], Repeat);
                    ErrorMsg = "";
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorMsg = ex.Message;
                    return false;
                }
            }
        }
        #endregion


        protected void btnImportAll_Click(object sender, EventArgs e)
        {
            int TaoCateId = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            if (String.IsNullOrWhiteSpace(this.txtUserId.Text) || !Common.PageValidate.IsNumber(this.txtUserId.Text))
            {
                MessageBox.ShowFailTip(this, "请输入正确的用户ID");
                return;
            }
            int albumId = Common.Globals.SafeInt(ddlAlbumList.SelectedValue, 0);
            if (albumId == 0)
            {
                MessageBox.ShowFailTip(this, "请选择需要导入数据的专辑");
                return;
            }
            int userId = Common.Globals.SafeInt(this.txtUserId.Text, 0);
            bool IsReRepeat = this.chkRepeat.Checked;
            if (Session["ProductList"] != null)
            {
                List<TaoBao.Domain.TbkItem> list = Session["ProductList"] as List<TaoBao.Domain.TbkItem>;
                int count = bll.ImportData(userId, albumId, TaoCateId, list, IsReRepeat);
                this.AspNetPager1.RecordCount = 0;
                DataListProduct.DataSource = null;
                DataListProduct.DataBind();
                Session["ProductList"] = null;
                MessageBox.ShowSuccessTip(this, "成功导入【" + count + "】条数据");
            }

        }
        #endregion
        protected void btnGetData_Click(object sender, EventArgs e)
        {
            int pageSize = Common.Globals.SafeInt(this.TopPageSize.Text, 20);
            int pageNo = Common.Globals.SafeInt(this.TopPageNo.Text, 1);
            this.AspNetPager1.PageSize = pageSize;

            int TaoCateId = Globals.SafeInt(this.TaoBaoCate.SelectedValue, 0);
            if (TaoCateId == 0)
            {
                MessageBox.ShowFailTip(this, "请选择淘宝分类");
                return;
            }
            string keyword = this.TopKeyWord.Text;
            string area = this.TopArea.Text;
            //MessageBox.ShowServerBusyTip(this, "正在获取数据，请稍候。。。");
            string sort = this.TopSort.SelectedValue;
            int startRate = (int)Common.Globals.SafeDecimal(this.TopStartRate.Text, 0) * 100;
            int endRate = (int)Common.Globals.SafeDecimal(this.TopEndRate.Text, 0) * 100;
            string startCredit = this.TopStartCredit.Text;
            string endCredit = this.TopEndCredit.Text;
            int startNum = Common.Globals.SafeInt(this.TopStartNum.Text, 0);
            int endNum = Common.Globals.SafeInt(this.TopEndNum.Text, 0);
            List<TaoBao.Domain.TbkItem> ProductList = bll.GetProductDates(TaoCateId, keyword, area, pageNo, pageSize, sort, startRate, endRate, startCredit, endCredit, startNum, endNum);
            if (ProductList != null && ProductList.Count > 0)
            {
                this.AspNetPager1.RecordCount = ProductList.Count;
                DataListProduct.DataSource = ProductList.Take(pageSize);
                DataListProduct.DataBind();
                Session["ProductList"] = ProductList;
            }
            else
            {
                MessageBox.ShowFailTip(this, "获取数据失败，请检查淘宝客设置是否正确，并确保申请的淘宝Key具有获取淘宝客数据权限。");
                return;
            }
        }

        protected void Text_Change(object sender, System.EventArgs e)
        {
            int userId = Common.Globals.SafeInt(this.txtUserId.Text, 0);
            List<ViewModel.SNS.AlbumIndex> AlbumList = albumBll.GetListByUserId(userId);
            this.ddlAlbumList.DataSource = AlbumList;

            this.ddlAlbumList.DataTextField = "AlbumName";
            this.ddlAlbumList.DataValueField = "AlbumID";
            this.ddlAlbumList.DataBind();
            this.ddlAlbumList.Items.Insert(0, new ListItem("--请选择--", ""));
        }
        protected void Text2_Change(object sender, System.EventArgs e)
        {
            int userId = Common.Globals.SafeInt(this.txtUserId2.Text, 0);
            List<ViewModel.SNS.AlbumIndex> AlbumList = albumBll.GetListByUserId(userId);

            this.ddlAlbumList2.DataSource = AlbumList;

            this.ddlAlbumList2.DataTextField = "AlbumName";
            this.ddlAlbumList2.DataValueField = "AlbumID";
            this.ddlAlbumList2.DataBind();
            this.ddlAlbumList2.Items.Insert(0, new ListItem("--请选择--", ""));
        }

    }
}