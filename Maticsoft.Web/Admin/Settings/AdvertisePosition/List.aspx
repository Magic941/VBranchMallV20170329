<%@ Page Title="AD_AdvertisePosition" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.AdvertisePosition.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "580", height: "380", overlayClose: false });
            $(".showjs").colorbox({ iframe: true, width: "500", height: "370", overlayClose: false });
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
                        <asp:Literal ID="Literal1" runat="server" Text="广告位管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以对<asp:Literal ID="Literal3" runat="server" Text="管理网站的广告位和广告内容，一个广告位可以设置多个广告" />
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
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="广告名称搜索" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
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
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server"><a class='iframe'  href="add.aspx">添加</a> <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat;width:60px;"  id="liDel" runat="server"><a href="javascript:;" onclick="GetDeleteM()">批量删除</a><b>|</b></li>
                </ul>
            </div>
        </div>
        <Maticsoft:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="AdvPositionId">
            <Columns>
                <asp:BoundField DataField="AdvPositionId" HeaderText="广告位ID" SortExpression="AdvPositionId" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AdvPositionName" HeaderText="广告位名称" SortExpression="AdvPositionName" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField  HeaderText="广告位大小"  ItemStyle-HorizontalAlign="Center" SortExpression="ShowType">
                    <ItemTemplate>
                        <%#Eval("Width").ToString() == "" ? "0" : Eval("Width").ToString()%>×<%#Eval("Height").ToString() == "" ? "0" : Eval("Height").ToString()%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="显示类型"  ItemStyle-HorizontalAlign="Center" SortExpression="ShowType">
                    <ItemTemplate>
                        <%#ConvertShowType(Eval("ShowType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedDate" HeaderText="添加时间" SortExpression="CreatedUserID"  ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField  HeaderText="添加者"  ItemStyle-HorizontalAlign="Center" SortExpression="ShowType"  Visible="false">
                    <ItemTemplate>
                        <%#Eval("CreatedUserID")%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField  HeaderText="广告代码"  ItemStyle-HorizontalAlign="Center" SortExpression="ShowType"  Visible="false">
                    <ItemTemplate>
                        <a class="showjs" href="GetAdJs.aspx?id=<%#Eval("AdvPositionId") %>&k=1" style="display: none;">获取站外广告代码</a>
                        <a class="showjs" href="GetAdJs.aspx?id=<%#Eval("AdvPositionId") %>">获取站内广告代码</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  HeaderText=""  ItemStyle-HorizontalAlign="Center" SortExpression="ShowType" >
                    <ItemTemplate>
                        <asp:Literal runat="server" ID="SetAdContent" />
                        
                       <%-- <a class='iframe' href="Show.aspx?id=<%#Eval("AdvPositionId") %>">详细信息</a>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center" SortExpression="AdvPositionId">
                    <ItemTemplate> 
                        <a class='iframe'  href="Modify.aspx?id=<%#Eval("AdvPositionId") %>">编辑</a>&nbsp;
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center" SortExpression="AdvPositionId">
                    <ItemTemplate> 
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </Maticsoft:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;"></td>
                <td></td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
