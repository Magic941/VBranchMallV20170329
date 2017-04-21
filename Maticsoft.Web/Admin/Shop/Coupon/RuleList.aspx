<%@ Page Title="优惠券规则管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="RuleList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Coupon.RuleList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Grv" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "680", height: "480", overlayClose: false });
            $(".delCou").css("width", "80px");
        });
    </script>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="优惠券活动记录" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以进行查看优惠券活动记录" />
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
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site, lblKeyword%>" />&nbsp;&nbsp;</asp:Label><asp:TextBox
                            ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>&nbsp;&nbsp; 
                    使用类型： <asp:DropDownList ID="ddlUseType" CssClass="select2" runat="server">
                                    <asp:ListItem Value="-1">全部</asp:ListItem><asp:ListItem Value="0">B2C商城优惠券</asp:ListItem><asp:ListItem Value="1">微信商城优惠券</asp:ListItem></asp:DropDownList>&nbsp;&nbsp;

                    发放类型： <asp:DropDownList ID="ddlAutoType" CssClass="select2" runat="server">
                                    <asp:ListItem Value="-1">全部</asp:ListItem><asp:ListItem Value="0">手动发放</asp:ListItem><asp:ListItem Value="1">自动发放</asp:ListItem></asp:DropDownList>&nbsp;&nbsp;
                    &nbsp;&nbsp; <asp:Button ID="Button1"
                                runat="server" Text="<%$ Resources:Site, btnSearchText %>" OnClick="btnSearch_Click"
                                class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <Grv:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="RuleId" ShowExportExcel="False" ShowExportWord="False" 
            OnRowCommand="gridView_RowCommand"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="false">
            <Columns>
                <asp:BoundField DataField="Name" HeaderText="活动名称" SortExpression="Name" ItemStyle-HorizontalAlign="Left" />
                
                <asp:TemplateField HeaderText="优惠券分类" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <%#GetClassName(Eval("ClassId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商家名称" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center"  Visible="False">
                    <ItemTemplate>
                        <%#GetSupplierName(Eval("SupplierId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="前缀" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("PreName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="面值" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("CouponPrice", "￥{0:N2}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="最低消费金额" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("LimitPrice", "￥{0:N2}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="数量" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("SendCount")%>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="积分" ItemStyle-Width="50" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("NeedPoint")%>
                    </ItemTemplate>
                </asp:TemplateField>
                   <asp:TemplateField HeaderText="使用类型" ItemStyle-Width="100" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("UseType").ToString().Trim()=="1"?"微信商城优惠券":"B2C商城优惠券"%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="发放类型" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("AutoState").ToString().Trim()=="1"?"自动发放":"手动发放"%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品分类" ItemStyle-Width="120" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <%#GetCategoryName(Eval("CategoryId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="创建者" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center"  Visible="False">
                    <ItemTemplate>
                        <%#GetUserName(Eval("CreateUserId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="使用时间" ItemStyle-Width="150" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("StartDate","{0:yyyy-MM-dd}")%>  至 <%#Eval("EndDate", "{0:yyyy-MM-dd}")%></ItemTemplate></asp:TemplateField><asp:TemplateField HeaderText="创建时间" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("CreateDate", "{0:yyyy-MM-dd}")%>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="状态" ItemStyle-Width="40" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GeStatusName(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="删除" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="50" >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="确定删除吗？"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton></ItemTemplate></asp:TemplateField><asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    ItemStyle-Width="120" >
                    <ItemTemplate>
                             <asp:LinkButton ID="LinkButton2" runat="server"  CssClass="delCou"  CommandName="DeleteCoupon" CommandArgument='<%#Eval("RuleId")%>'
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="确定删除该活动下的优惠券吗？"
                            Text="删除优惠券" >
                             </asp:LinkButton><a href="UpdateRule.aspx?id=<%#Eval("RuleId")%>" class="iframe">增发</a></ItemTemplate></asp:TemplateField></Columns><FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr></Grv:GridViewEx></div></asp:Content><asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
