using System;
using System.Collections;
using System.Text;
using Maticsoft.BLL.Ms;
using Maticsoft.BLL.SysManage;
using Maticsoft.Common;
using Maticsoft.Model.SysManage;
using ConfigSystem = Maticsoft.BLL.SNS.ConfigSystem;
using System.Collections.Generic;
using System.Linq;

namespace Maticsoft.Web.Admin.SNS.Setting
{
    public partial class StoreConfig : PageBaseAdmin
    {
        protected override int Act_PageLoad { get { return 112; } } //运营管理_是否显示API接口设置页面
        protected new int Act_UpdateData = 113;    //运营管理_是否显示API接口_编辑接口信息

        protected void Page_Load(object sender, EventArgs e)
        {
            //是否有编辑信息的权限
            if (!UserPrincipal.HasPermissionID(GetPermidByActID(Act_UpdateData)) && GetPermidByActID(Act_UpdateData) != -1)
            {
                btnSave.Visible = false;
            }
            if (!IsPostBack)
            {
                BoundData();
            }
        }



        private void BoundData()
        {

            if (BLL.SysManage.ConfigSystem.GetValueByCache("SNS_ImageStoreWay") == "1")
            {
                rdtWeb.Checked = true;
            }
            else
            {
                rdtLocal.Checked = true;
            }
            this.txtOperaterName.Text = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_YouPaiYunOperaterName");
            this.txtOperaterPassword.Text = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_YouPaiOperaterPassword");
            this.txtSpaceName.Text = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_YouPaiSpaceName");
            this.txtPhotoDomain.Text = BLL.SysManage.ConfigSystem.GetValueByCache("SNS_YouPaiPhotoDomain");

        List<Maticsoft.Model.Ms.ThumbnailSize> thumbList= Maticsoft.BLL.Ms.ThumbnailSize.GetThumSizeList(Maticsoft.Model.Ms.EnumHelper.AreaType.SNS);
       
            if (thumbList != null && thumbList.Count > 0)
            {
                int i = 0;
                foreach (var thumbnailSize in thumbList)
                {
                    if (i == 0)
                    {
                        this.thumbList.Value = thumbnailSize.ThumName + "&" + thumbnailSize.CloudSizeName;
                    }
                    else
                    {
                        this.thumbList.Value = this.thumbList.Value + "," + thumbnailSize.ThumName + "&" + thumbnailSize.CloudSizeName;
                    }
                    i++;
                }
               
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                UpdataData("SNS_ImageStoreWay", rdtWeb.Checked ? "1" : "0", "图片存储的方式[1:网上存储 0：本地存储]");
                UpdataData("SNS_YouPaiYunOperaterName", txtOperaterName.Text.Trim(), "网络又拍云存储照片【操作者名称】");
                if (!String.IsNullOrWhiteSpace(txtOperaterPassword.Text))
                {
                    UpdataData("SNS_YouPaiOperaterPassword", txtOperaterPassword.Text.Trim(), "网络又拍云存储照片【操作者密码】");
                }
                UpdataData("SNS_YouPaiSpaceName", txtSpaceName.Text.Trim(), "网络又拍云存储照片【空间名称】");

                string photoDomain = txtPhotoDomain.Text.Trim();
                if (this.rdtWeb.Checked && !photoDomain.StartsWith("http"))
                {
                    MessageBox.ShowFailTip(this, "请填写以“Http”开头网络又拍云存储地址!");
                }
                UpdataData("SNS_YouPaiPhotoDomain", txtPhotoDomain.Text.Trim(), "网络又拍云存储照片域名");

                //Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.System);//清除网站设置的缓存文件
                //Cache.Remove("ConfigSystemHashList_" + ApplicationKeyType.Shop);//清除网站设置的缓存文件

                //获取缩略图尺寸列表
                string thumbText= this.thumbList.Value;
                Maticsoft.BLL.Ms.ThumbnailSize thumbBll=new ThumbnailSize();
                var thumbList = thumbText.Split(',');
                foreach (var thumb in thumbList)
                {
                    Maticsoft.Model.Ms.ThumbnailSize thumbModel = thumbBll.GetModel(thumb.Split('&')[0].Trim());
                    if (thumbModel!=null&&thumb.Split('&').Length>=2)
                    {
                        thumbModel.CloudSizeName = thumb.Split('&')[1];
                        thumbBll.Update(thumbModel);
                    }
                }

                #region 清除缓存，需优化
                IDictionaryEnumerator de = Cache.GetEnumerator();
                ArrayList list = new ArrayList();
                while (de.MoveNext())
                {
                    list.Add(de.Key.ToString());
                }
                foreach (string key in list)
                {
                    Cache.Remove(key);
                } 
                #endregion
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipUpdateOK, "StoreConfig.aspx");

            }
            catch (Exception)
            {
                Maticsoft.Common.MessageBox.ShowSuccessTip(this, Resources.Site.TooltipTryAgainLater, "StoreConfig.aspx");
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            BoundData();
        }

         public bool UpdataData(string Key,string Value,string Description)
        {
             try
             {
                 if (BLL.SysManage.ConfigSystem.Exists(Key))
                 {
                     BLL.SysManage.ConfigSystem.Update(Key, Value,
                                                       ApplicationKeyType.OpenAPI);
                 }
                 else
                 {
                     BLL.SysManage.ConfigSystem.Add(Key, Value,
                                                   Description, ApplicationKeyType.OpenAPI);
                 }
                 return true;
             }
             catch 
             {

                 return false;
             }

           
        }
    }
}