<%@ Page Language="C#" Title="询价单详情" MasterPageFile="~/Admin/BasicNoFoot.Master"
    AutoEventWireup="true" CodeBehind="ShowInquiry.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Inquiry.ShowInquiry" %>

<%@ Register TagPrefix="cc1" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/tab.js" type="text/javascript"></script>
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" />
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <style type="text/css">
        .td_class
        {
            width: 80px;
            border-right: 1px solid #DBE2E7;
            border-left: 1px solid #fff;
            border-bottom: 1px solid #ddd;
            border-top: 1px solid #fff;
            padding-bottom: 0;
            padding-top: 0;
            height: 36px
        }
        .td_content
        {
            border-right: 1px solid #DBE2E7;
            border-left: 1px solid #fff;
            border-bottom: 1px solid #ddd;
            border-top: 1px solid #fff;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hfOrderMainStatus" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="lblTitle" runat="server" Text="正在查看订单详细信息" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);"><a href="javascript:;">咨询内容</a></li>
                    <li class="normal" onclick="nTabs(this,1);"><a href="javascript:;">基本信息</a></li>
                    <li class="normal" onclick="nTabs(this,2);"><a href="javascript:;">商品清单</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content0">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="newstitle" style="color: #666">
                            <span style="font-size: 16px; padding-left: 20px">咨询内容</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="float: left; padding-left: 30px">
                            <asp:TextBox ID="txtLeaveMsg" runat="server" TextMode="MultiLine" Width="580px" Rows="5"
                                ReadOnly="True"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="newstitle" style="color: #666">
                            <span style="font-size: 16px; padding-left: 20px">回复内容</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="float: left; padding-left: 30px">
                            <asp:TextBox ID="txtReplyMsg" runat="server" TextMode="MultiLine" Width="580px" Rows="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="newstitle" style="color: #666">
                            <span style="font-size: 16px; padding-left: 20px">总费用</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="float: left; padding-left: 30px">
                            <asp:TextBox ID="txtAmount" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td height="25" colspan="2" align="center">
                            <asp:Button ID="Button2" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
            <%--基本信息--%>
            <div id="myTab1_Content1" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td style="vertical-align: top;">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                                <tr>
                                    <td colspan="2" class="newstitle" bgcolor="#FFFFFF">
                                        <span style="font-size: 16px; padding-left: 20px">用户信息</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal2" runat="server" Text=" 用户名" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal6" runat="server" Text=" 邮箱地址" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr style="display: none">
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal15" runat="server" Text=" 手机" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblCellPhone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal8" runat="server" Text=" 电话" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblTelephone" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal11" runat="server" Text=" 详细地址" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal1" runat="server" Text="  公司名称" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblCompany" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal3" runat="server" Text=" 创建时间" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal7" runat="server" Text=" 处理状态" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Label ID="Literal4" runat="server" Text=" 处理人" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblUpdateUser" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class" style="background-color: #E2E8EB">
                                        <asp:Literal ID="Literal5" runat="server" Text=" 处理时间" />：
                                    </td>
                                    <td height="25" class="td_content">
                                        <asp:Label ID="lblUpdateDate" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <%--     商品清单--%>
            <div id="myTab1_Content2" class="none4">
                <table style="width: 100%; border-bottom: none; border-top: none; float: left;" cellpadding="2"
                    cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
                                ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
                                OnRowDataBound="gridView_RowDataBound" Width="100%" PageSize="10" ShowExportExcel="False"
                                ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
                                ShowCheckAll="False" DataKeyNames="ItemId" Style="float: left;">
                                <Columns>
                                    <asp:TemplateField ControlStyle-Width="60" HeaderText="商品图片" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <a href="javascript:;" target="_blank">
                                                <asp:Image ID="Image1" runat="server" Width="60px" Height="60px" ImageAlign="Middle"
                                                    ImageUrl='<%# Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailsUrl").ToString(), "T128X130_")%>' />
                                            </a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="商品名称" ItemStyle-HorizontalAlign="left">
                                        <ItemTemplate>
                                            <%#Eval("Name")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50" HeaderText="货号" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <%#Eval("SKU")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50" HeaderText="购买数量" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <%# Eval("Quantity")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ControlStyle-Width="50" HeaderText="商品单价" ItemStyle-HorizontalAlign="Center"
                                        ItemStyle-Width="50">
                                        <ItemTemplate>
                                            <span class="txtprice">
                                                <%# Maticsoft.Common.Globals.SafeDecimal(Eval("AdjustedPrice").ToString(),0).ToString("F")%></span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle Height="25px" HorizontalAlign="Right" />
                                <HeaderStyle Height="35px" />
                                <PagerStyle Height="25px" HorizontalAlign="Right" />
                                <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
                                <RowStyle Height="25px" />
                                <SortDirectionStr>DESC</SortDirectionStr>
                            </cc1:GridViewEx>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
