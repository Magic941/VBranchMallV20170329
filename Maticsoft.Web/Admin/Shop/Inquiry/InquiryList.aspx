<%@ Page Title="询价单管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="InquiryList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Inquiry.InquiryList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Grv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "720", height: "640", overlayClose: false });
            $(".delCou").css("width", "80px");
        });
    </script>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="用户询价单管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以进行编辑、查看用户询价单记录" />
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
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:Label ID="Label1" runat="server">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblKeyword%>" />:</asp:Label><asp:TextBox
                            ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox><asp:Button ID="Button1"
                                runat="server" Text="<%$ Resources:Site, btnSearchText %>" OnClick="btnSearch_Click"
                                class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li runat="server" id="liAdd"></li>
                </ul>
            </div>
        </div>
        <Grv:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="InquiryId" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3"
            BorderWidth="1px" ShowCheckAll="false">
            <Columns>
                  <asp:TemplateField HeaderText="用户名" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Left" >
                    <ItemTemplate>
                        <%#Eval("UserName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户邮箱" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Left" >
                    <ItemTemplate>
                        <%#Eval("Email")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="手机" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("CellPhone")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="电话" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("Telephone")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="公司" ItemStyle-Width="240" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("Company")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="区域" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#GetRegionName(Eval("RegionId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="详细地址"  ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="280">
                    <ItemTemplate>
                        <%#Eval("Address")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="总价" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Amount", "￥{0:N2}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建时间" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("CreatedDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="处理人" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Left"
                   >
                    <ItemTemplate>
                        <%#GetUserName(Eval("UpdatedUserId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="处理时间" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("UpdatedDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GeStatusName(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="确定删除吗？"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="50">
                    <ItemTemplate>
                    <a  href='ShowInquiry.aspx?id=<%#Eval("InquiryId")%>' class="iframe">查看</a>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </Grv:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
