<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ProductsBatchUpload_Old.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductsBatchUpload_Old" %>

<%@ Register Src="/Admin/../Controls/ProductsBatchUploadDropList.ascx" TagName="ProductsBatchUploadDropList"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .PageTitleArea
        {
            margin: 0px;
            margin-bottom: 4px;
            padding: 8px;
            border: #D5DCE6 1px solid;
            background: #F8F8F8;
            font-size: 120%;
            font-style: normal;
            font-size: 12px;
            font-weight: normal;
            color: #404040;
            line-height: 17px;
        }
        .PageTitle
        {
            font-size: 15px;
            color: #2E6CD2;
            font-weight: bold;
        }
        
        .AdminSearchform
        {
            padding-left: 0;
            font-family: verdana, tahoma, helvetica;
            font-size: 12px;
            margin-bottom: 8px;
            padding-bottom: 8px;
            padding-top: 0px;
            overflow: hidden;
            clear: both;
        }
        
        /* button样式 */
        .inp_L1
        {
            height: 20px;
            padding: 0 3px;
            border: 1px solid #87a3c1;
            color: #174b73;
            background: url(../../../images/images/botton_newbg.gif) repeat-x;
            cursor: pointer;
            vertical-align: middle;
        }
        
        legend
        {
            font-weight: bold;
            color: #2E6CD2;
            font-size: 12px;
            margin-left: 10px;
        }
    </style>
    <script type="text/javascript">
        function Checking() {

            if ($("[id$=FileUploadProducts]").val() == "") {
                ShowFailTip("请选择文件");
                return false;
            }
            else {
                var PicExt = $("[id$=FileUploadProducts]").val().substring($("#[id$=FileUploadProducts]").val().lastIndexOf(".") + 1);
                if (PicExt != "csv") {
                    ShowFailTip("文件格式必须是.csv!");
                    return false;
                }
                else {
                    var checkbox1 = document.getElementById("<%=CheckBox1.ClientID%>").checked;
                    var checkbox2 = document.getElementById("<%=CheckBox2.ClientID%>").checked;
                    var checkbox3 = document.getElementById("<%=CheckBox3.ClientID%>").checked;
                    var checkbox4 = document.getElementById("<%=CheckBox4.ClientID%>").checked;
                    //if ($("[id$=CheckBox1]").attr("checked") != true)
                    if (checkbox1 != true && checkbox2 != true && checkbox3 != true && checkbox4 != true) {
                        ShowFailTip("请至少选择一种商品推荐方式");
                        return false;
                    }
                }
            }
        }   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


      <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="批量上传商品" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text=" 如果你想批量上传商品,可在这里通过CSV文件上传." />
                    </td>
                </tr>
            </table>
        </div>

        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="50px" height="30" >
                    第一步：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    下载批量上传CSV文件(选择使用网站CSV文件上传的用户执行)
                </td>
            </tr>
            <tr>
                <td width="50px" height="30" >
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                      <div class="adminsubmit" >
                            <a href="MaticsoftProducts.csv" ><div  style="text-align:center;padding-top:10px;">下载CSV文件</div></a></div>
                </td>
            </tr>
            <tr>
                <td width="50px" height="30" >
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                     <span style="color: red;">* csv 文件中不能用Alt+回车键来换行并且XX为必填项</span>
                </td>
            </tr>
        </table>
        
        <br />
        
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="50px" height="30" >
                    第二步：
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    填写CSV文件
                </td>
            </tr>
            <tr>
                <td width="50px" height="30" >
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                      1.请把本地电脑上的商品清晰图整理好并通过 FTP 分别上传到： 网站根目录/Storage/Original/及网站根目录/Storage/Album/
                </td>
            </tr>
            <tr>
                <td width="50px" height="30" >
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                      2.如果你导入的是网站CSV文件则打开CSV文件，在里面对应处写入上传商品的各项内容。注意商品清晰图只填写图片名称即可,例如：01.jpg
                </td>
            </tr>
        </table>
        
        <br />

        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border"> 
        <tr style="padding-top:5px;">
                <td >
                 <span style="color:Black;">   第三步：</span>&nbsp;&nbsp;开始批量操作
                </td>
            </tr>
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">上传批量CSV文件：</td>
                            <td height="25">
                                  <asp:FileUpload ID="FileUploadProducts" runat="server" Width="235px" />
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">上传到分类 ：</td>
                            <td height="25">
                                        <uc1:ProductsBatchUploadDropList ID="ProductsBatchUploadDropList" IsNull="True" runat="server" />
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">商品类型 ：</td>
                            <td height="25">
                                        <asp:DropDownList ID="dropProductTypes" runat="server" Width="235px">
                                        </asp:DropDownList>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">设置商品品牌 ：</td>
                            <td height="25">
                                        <asp:DropDownList ID="dropBrands" runat="server" Width="235px">
                                        </asp:DropDownList>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">设置商品商家 ：</td>
                            <td height="25">
                                        <asp:DropDownList ID="dropEnterprise" runat="server" Width="235px">
                                        </asp:DropDownList>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">设置商品为 ：</td>
                            <td height="25">
                                        <div runat="server" id="divCheckBox">
                                            <ul>
                                                <li style="float: left; margin-right: 20px; white-space: nowrap;">
                                                    <asp:CheckBox ID="CheckBox1" runat="server" Text="推荐商品" SkinID="0" /></li>
                                                <li style="float: left; margin-right: 20px; white-space: nowrap;">
                                                    <asp:CheckBox ID="CheckBox2" runat="server" Text="热卖商品" SkinID="1" /></li>
                                                <li style="float: left; margin-right: 20px; white-space: nowrap;">
                                                    <asp:CheckBox ID="CheckBox3" runat="server" Text="特价商品" SkinID="2" /></li>
                                                <li style="float: left; margin-right: 20px; white-space: nowrap;">
                                                    <asp:CheckBox ID="CheckBox4" runat="server" Text="最新商品" SkinID="3" /></li>
                                                <li style="float: left; margin-right: 20px; white-space: nowrap;">
                                                    <asp:CheckBox ID="CheckBoxSaleStatus" runat="server" Text="出售中的商品" /></li>
                                            </ul>
                                        </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" height="30">
                            </td>
                            <td height="25">
                                        <asp:Button ID="ButUpload" runat="server" Text="上 传" CssClass="adminsubmit_short" OnClick="ButUpload_Click"
                                            OnClientClick="return Checking();" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
