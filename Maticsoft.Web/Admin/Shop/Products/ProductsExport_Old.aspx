<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ProductsExport_Old.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.ProductsExport" %>

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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="导出商品" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text=" 将指定的商品导出并保存至本地文件" />
                    </td>
                </tr>
            </table>
        </div>

        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border"> 
            </tr>
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">选择商品范围：</td>
                            <td height="25">
                                <asp:DropDownList ID="dropSaleStatus" runat="server" Width="235px">
                                    <asp:ListItem Value="1">出售中的商品</asp:ListItem>
                                    <asp:ListItem Value="2">仓库里的商品</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">商品名称 ：</td>
                            <td height="25">
                                     <asp:TextBox ID="tbProdName" runat="server" Width="230"></asp:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">商品分类 ：</td>
                            <td height="25">
                                <asp:DropDownList ID="dropCategory" runat="server" Width="230">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">货号 ：</td>
                            <td height="25">
                                    <asp:TextBox ID="tbSKU" runat="server" Width="230"></asp:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">品牌分类 ：</td>
                            <td height="25">
                                <asp:DropDownList ID="dropBrands" runat="server" Width="235px">
                                </asp:DropDownList> 
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" height="30">
                            </td>
                            <td height="25">
                            <asp:Button ID="ButExport" runat="server" Text="导出商品" CssClass="adminsubmit" 
                            OnClick="ButExport_Click" />                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
