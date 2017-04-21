<%@ Page Title="微信用户分组管理" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="GroupList.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Group.GroupList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $(".iframe").colorbox({ iframe: true, width: "480", height: "372", overlayClose: false });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信用户分组管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="微信用户分组管理" />
                    </td>
                </tr>
            </table>
        </div>
        
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="分组名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tName"  runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="分组描述" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tDesc" runat="server" Width="250px" MaxLength="50" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        
        <br />
        <div class="newslist" >
            <div class="newsicon">
                <ul>
                    <li runat="server"></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" CellPadding="3"
            BorderWidth="1px" ShowCheckAll="true" DataKeyNames="GroupId">
            <Columns>
                <asp:TemplateField HeaderText="分组名称" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%# Eval("GroupName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="备注" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%#Eval("Remark")%>
                        </ItemTemplate>
                </asp:TemplateField>
                         <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                    <a href='UpdateGroup.aspx?id=<%# Eval("GroupId")%>' class="iframe"> 编辑</a>
                        </ItemTemplate>
                          </asp:TemplateField>
            </Columns>
              <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td align="left">
                    <asp:CheckBox ID="chkIsCover" runat="server" />是否覆盖
                     <asp:Button ID="btnGenerate"  runat="server" Text="获取分组" class="adminsubmit" OnClick="btnGetGroup_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

