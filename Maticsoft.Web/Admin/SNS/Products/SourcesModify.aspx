<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="SourcesModify.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.ProductSources.Modify" Title="修改页" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="商品来源表编辑" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以编辑<asp:Literal ID="Literal3" runat="server" Text="商品来源表" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    
<table cellSpacing="0" cellPadding="0" width="100%" border="0">
	<tr>
	<td height="25" width="30%" align="right">
		流水id
	：</td>
	<td height="25" width="*" align="left">
		<asp:label id="lblID" runat="server"></asp:label>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		商品来源网站的名称
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtWebSiteName" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		商品来源网站的url
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtWebSiteUrl" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		网站的log,在单品也链接到此
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtWebSiteLogo" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		采集时商品类别匹配的正则表达式
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtCategoryTags" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		采集时商品价格匹配的正则表达式
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtPriceTags" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		采集时图片匹配的正则表达式
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtImagesTag" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
	<tr>
	<td height="25" width="30%" align="right">
		状态
	：</td>
	<td height="25" width="*" align="left">
		<asp:TextBox id="txtStatus" runat="server" Width="200px"></asp:TextBox>
	</td></tr>
</table>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
