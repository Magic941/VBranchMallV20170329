<%@ Page Title="Shop_ProductConsultations" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Consultations.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "600", height: "420", overlayClose: false });
        });
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        } 
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="商品咨询信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        管理客户提交的商品咨询，查看客户提出的问题并进行回复
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody" style="padding-top: 5px;">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Value="">请选择</asp:ListItem>
                        <asp:ListItem Value="0">未回复</asp:ListItem>
                        <asp:ListItem Value="1">已回复</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <%--<li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx"> 添加</a> <b>|</b> </li>--%>
                    <li runat="server" id="liDel"  ></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ConsultationId">
            <Columns>
                    <asp:TemplateField ControlStyle-Width="50" HeaderText="咨询商品" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="360px">
                    <ItemTemplate>
                        <%# GetProductName(Eval("ProductId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="咨询人" SortExpression="UserName" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ConsultationText" HeaderText="咨询内容" SortExpression="ConsultationText"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreatedDate" HeaderText="咨询时间" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="是否回复" ItemStyle-HorizontalAlign="Center"
                    SortExpression="IsReply">
                    <ItemTemplate>
                        <%# (Boolean.Parse(Eval("IsReply").ToString())) ? "已回复" : "未回复"%>
                    </ItemTemplate>
                </asp:TemplateField>
                         <asp:TemplateField ControlStyle-Width="50" HeaderText="状态" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <%# (Maticsoft.Common.Globals.SafeInt(Eval("Status").ToString(),0)==1) ? "已审核" : "未审核"%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    SortExpression="ConsultationId">
                    <ItemTemplate>
                        <a class='iframeshow' href="Show.aspx?id=<%#Eval("ConsultationId") %>">详细信息</a>
                        &nbsp;&nbsp; 
                        <asp:Literal ID="Literal3" runat="server"></asp:Literal>&nbsp;
                        <asp:HiddenField ID="hfCid" runat="server"  Value='<%#Eval("ConsultationId") %>' />
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                       OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" class="adminsubmit" OnClick="btnDelete_Click" />
                            <asp:Button ID="btnStatus" runat="server" Text="批量审核"
                     class="adminsubmit" OnClick="btnStatus_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>