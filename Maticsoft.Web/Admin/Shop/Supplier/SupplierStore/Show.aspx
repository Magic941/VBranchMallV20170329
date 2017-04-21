<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Supplier.SupplierStore.Show" Title="商家详细信息" %>

<%@ Register Src="/Admin/../Controls/Region.ascx" TagName="Region" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .style1
        {
            width: 150px;
            text-align: right;
            padding-bottom: 10px;
            padding-top: 10px;
            height: 25px;
        }
        .style2
        {
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="店铺信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以查看店铺详细信息" />
                    </td>
                </tr>
            </table>
        </div>

        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="td_class">
                    店铺名称 ：
                </td>
                <td height="25">
                    <asp:Literal ID="labShopName"  Visible="true" runat="server" />&nbsp;&nbsp;
                    <asp:Literal  ID="labClostImg"  Visible="False" runat="server" Text="如需您要开店, 请填写店铺名称。"/>
                </td>
            </tr>
            <tr >
                <td class="td_class">
                    店铺招牌 ：
                </td>
                <td height="25">
                 <asp:Image ID="image1" runat="server" width="600px" Height="50px"/>
                </td>
            </tr>
            
            <tr>
                <td class="td_class" valign="top">
                    店铺自定义内容区 ：
                </td>
                <td height="25">
                    <asp:TextBox ID="txtIndexContent" runat="server" Width="600px" TextMode="MultiLine" ReadOnly="True"></asp:TextBox>
                </td>
            </tr>
             <tr>
                <td class="td_class" valign="top">
                    首页显示商品数量 ：
                </td>
                <td height="25">
                    <asp:Literal ID="txtIndexProdTop" runat="server"></asp:Literal>
                      &nbsp;&nbsp;
                </td>
            </tr>
          <tr>
                <td class="td_class" valign="top">
                    店铺状态 ：
                </td>
                <td height="25">
           <asp:Literal runat="server" ID="labShopStatus"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                </td>
                <td height="25">
                    <asp:Button ID="btnSave" runat="server" Text="返回" class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                    </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
