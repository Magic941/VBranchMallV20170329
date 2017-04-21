using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Maticsoft.BLL.Shop.Coupon;
using System.IO;
using System.Data;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Maticsoft.BLL.Shop.Card;
using Maticsoft.Services;

namespace Maticsoft.Web.Admin.Shop.Coupon
{
    public partial class ManualCoupon : PageBaseAdmin
    {
        private Maticsoft.BLL.Shop.Coupon.CouponRule ruleBll = new CouponRule();
        APIHelper AHelper = new APIHelper(System.Configuration.ConfigurationManager.AppSettings["CardURL"]);
        protected string UploadPath = "/Upload/Temp/" + DateTime.Now.ToString("yyyyMMdd") + "/";
        public readonly Maticsoft.BLL.Shop.Coupon.CouponClass _couponBLL = new Maticsoft.BLL.Shop.Coupon.CouponClass();
        protected Maticsoft.BLL.Shop_CouponRuleExt cop = new Maticsoft.BLL.Shop_CouponRuleExt();
        protected Maticsoft.BLL.Shop_CardUserInfo scui = new Maticsoft.BLL.Shop_CardUserInfo();
        protected Maticsoft.BLL.Shop.Coupon.CouponInfo couInfo = new Maticsoft.BLL.Shop.Coupon.CouponInfo();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //绑定优惠券类别
                List<Maticsoft.Model.Shop.Coupon.CouponClass> couponList = _couponBLL.GetModelList("");
                if (couponList.Count > 0)
                {
                    ddlClass.DataSource = couponList;
                    ddlClass.DataValueField = "ClassId";
                    ddlClass.DataTextField = "Name";
                    ddlClass.DataBind();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string strBatch = txtBatch.Text;
            lblMsg.Text = "";
            DataSet ds = couInfo.GetList("Batch='" + strBatch + "'");

            if (ds.Tables[0].Rows.Count > 0)
            {
                Common.MessageBox.ShowFailTip(this, "批号在数据库中已经存在");
                return;
            }

            string FileName = uploadExcel.PostedFile.FileName;
            string ErrorMsg = "出现异常，请检查您的数据格式";
            int Count = 0;
            int CountError = 0;
            if (!uploadExcel.HasFile)
            {
                lblMsg.Text = "请上传文件";
                Common.MessageBox.ShowFailTip(this, "请上传文件");
                return;
            }

            if (!Directory.Exists(HttpContext.Current.Server.MapPath(UploadPath)))
            {
                Directory.CreateDirectory(HttpContext.Current.Server.MapPath(UploadPath));
            }
            uploadExcel.PostedFile.SaveAs(Server.MapPath(UploadPath) + FileName);

            if (ExcelToManualCoupon(UploadPath, FileName, Convert.ToInt32(ddlClass.SelectedValue), out ErrorMsg, ref Count, ref CountError, strBatch))
            {
                Common.MessageBox.ShowSuccessTip(this, "成功发放" + Count + "条优惠券数据", "CouponList.aspx");
            }
            else
            {
                lblMsg.Text = ErrorMsg;
                //Common.MessageBox.ShowSuccessTip(this, "发放失败,信息:" + ErrorMsg + "提示：请检查卡及会员及相关文件");
            }
        }

        public bool ExcelToManualCoupon(string Path, string FileName, int CouClassId, out string ErrorMsg, ref int Count, ref int CountError, string Batch)
        {
            ErrorMsg = "";
            string sheetName = Maticsoft.BLL.SysManage.ConfigSystem.GetValueByCache("Shop_CouponExcel_SheetName");
            if (String.IsNullOrWhiteSpace(sheetName))
            {
                sheetName = "Sheet1";
            }
            DataSet ExcelData = new DataSet();
            using (FileStream fs = new FileStream(Server.MapPath(Path + FileName), FileMode.OpenOrCreate))
            {
                try
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
                    if (!table.Columns.Contains("营销人员"))
                    {
                        ErrorMsg = "上传Excel文件内容的数据格式不正确";
                        ErrorLogTxt.GetInstance("优惠券日志").Write("1"+ ErrorMsg);
                        return false;
                    }

                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        DataRow dataRow = table.NewRow();

                        for (int j = row.FirstCellNum; j < cellCount; j++)
                        {
                            if (row.GetCell(j) != null)
                            {
                                if (row.GetCell(j).CellType == CellType.NUMERIC && DateUtil.IsCellDateFormatted(row.GetCell(j)))
                                {
                                    dataRow[j] = row.GetCell(j).DateCellValue;
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString();
                                }
                            }

                        }
                        table.Rows.Add(dataRow);
                    }

                    UserCardLogic uc = new UserCardLogic();

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        string isActiveStr = uc.CheckCardActive(table.Rows[i][1].ToString());

                        if (isActiveStr == "1")
                        {
                            Count = Count + 1;
                        }
                        else
                        {
                            CountError = CountError + 1;
                            ErrorMsg = ErrorMsg + "," + table.Rows[i][1].ToString();
                        }
                    }

                    if (CountError > 0)
                    {
                        ErrorMsg = "有" + CountError.ToString() + "张未激活或无效卡,卡号为：" + ErrorMsg;
                        ErrorLogTxt.GetInstance("优惠券日志").Write("2" + ErrorMsg);
                        return false;
                    }

                    bool isSet = false;
                    
                    Maticsoft.BLL.Members.Users us = new Maticsoft.BLL.Members.Users();

                    int shopCardInfoNum = 0;
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        //cop.SetUserCoupon(userID, item.CouponCount, item.ClassID)
                        DataSet ds = scui.GetList("CardNo='" + table.Rows[i][1].ToString() + "'");
                        string UserNo = "";
                        if (ds.Tables[0].Rows.Count == 0)
                        {
                            ErrorMsg = ErrorMsg + "," + table.Rows[i][1].ToString();
                            ErrorLogTxt.GetInstance("优惠券日志").Write("ShopCardInfo 没找到：" + ErrorMsg);
                            shopCardInfoNum = shopCardInfoNum + 1;
                            //return false;
                        }
                    }

                    if (shopCardInfoNum > 0)
                    {
                        return false;
                    }

                    DataSet copDs = couInfo.GetList("userid=0 and UsedDate is null and ClassId=" + CouClassId.ToString());

                    if (copDs.Tables[0].Rows.Count < table.Rows.Count)
                    {
                        ErrorMsg = "你所选的券，不够用，差" + (table.Rows.Count - copDs.Tables[0].Rows.Count).ToString() + "张";
                        return false;
                    }

                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        //cop.SetUserCoupon(userID, item.CouponCount, item.ClassID)
                        DataSet ds = scui.GetList("CardNo='" + table.Rows[i][1].ToString() + "'");
                        string UserNo = "";
                        UserNo = ds.Tables[0].Rows[0]["UserId"].ToString();
                        int usId = us.GetUserIdByUserName(UserNo);
                        try
                        {
                            isSet = cop.SetManualCoupon(usId, 1, CouClassId, table.Rows[i][1].ToString(), Batch);
                            if (!isSet)
                            {
                                ErrorMsg = "有" + i.ToString() + "张券被绑定生效，绑定卡号：" + table.Rows[i][1].ToString() + "时，遇到券不在有效期内，券绑定被中止";
                                ErrorLogTxt.GetInstance("优惠券日志").Write("绑定该券时出错" + ErrorMsg);
                                return false;
                            }
                        }
                        catch (Exception e)
                        {
                            ErrorMsg = "梆定此卡时出现异常，卡号为：" + table.Rows[i][1].ToString() +"，绑定被中止。异常详细："+ e.ToString();
                        }

                    }
                    
                    ErrorMsg = "";
                    return true;
                }
                catch (Exception e)
                {
                    ErrorMsg = "未知问题" + e.ToString();
                    ErrorLogTxt.GetInstance("优惠券日志").Write("5" + e.ToString());
                    return false;
                }
            }
        }
    }
}