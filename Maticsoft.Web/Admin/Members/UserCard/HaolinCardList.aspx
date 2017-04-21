<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="HaolinCardList.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.UserCard.HaolinCardList" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %> 
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/Admin/js/Shop/poplayer/js/tipswindown.js"></script>
    <link href="/Admin/js/Shop/poplayer/js/css.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">

    function showTipsWindown(id) {
        tipsWindown("健康卡修改", "iframe:" + id, "600", "500", "true", "", "true", id);
    }
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        健康卡管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                     您可以对健康卡会员卡进行 删除等操作
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />
                    姓名：
                    <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    用户编号：
                    <asp:TextBox ID="txtUserNo" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    卡号：
                    <asp:TextBox ID="txtCardNo" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    投保人姓名：
                    <asp:TextBox ID="txtInsureName" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    投保人身份证号：
                    <asp:TextBox ID="txtInsureID" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>" OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                   
                </ul>
            </div>
        </div>
        <cc1:gridviewex ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="20" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="CardNo">
            <Columns>
                <asp:BoundField DataField="CardNo" HeaderText="健康卡号"    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="120" HeaderText="用户编号" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("UserId")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120" HeaderText="姓名" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#Eval("Name")%><%#!string.IsNullOrWhiteSpace(Eval("NameOne").ToString())?"、"+Eval("NameOne"):""%><%#!string.IsNullOrWhiteSpace(Eval("NameTwo").ToString())?"、"+Eval("NameTwo"):""%>
                    </ItemTemplate>
                </asp:TemplateField>

                    <asp:TemplateField ControlStyle-Width="120" HeaderText="邮箱" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#Eval("Email")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120" HeaderText="销售员" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#Eval("BackPerson")%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ControlStyle-Width="120" HeaderText="手机" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#Eval("Moble")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="身份证号" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#Eval("CardId")%><%#!string.IsNullOrWhiteSpace(Eval("NameOneCardId").ToString())?"、"+Eval("NameOneCardId"):""%><%#!string.IsNullOrWhiteSpace(Eval("NameTwoCardId").ToString())?"、"+Eval("NameTwoCardId"):""%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="120" HeaderText="激活时间" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#Eval("ActiveDate","{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ControlStyle-Width="120" HeaderText="地址" 
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#Eval("Address")%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a href="#" style="white-space: nowrap;" onclick="showTipsWindown('/admin/Members/UserCard/HaolinCardEdit.aspx?ID=<%#Eval("CardNo")%>')" >编辑</a>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:gridviewex>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;display:none;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"   class="adminsubmit" />
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
