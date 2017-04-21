<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="HotKeyword.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.HotKeyword" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/select2-3.4.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-3.4.1/select2.min.js" type="text/javascript" charset="utf-8"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $(".select2").select2({ placeholder: "请选择", width: '240px' });

            $(".iframe").attr("href", "AddHotKeyword.aspx");
            $(".iframe").colorbox({ iframe: true, width: "550", height: "380", overlayClose: false });

            //            $(".iframeUpdate").attr("href", "AddHotKeyword.aspx");
            $(".iframeUpdate").colorbox({ iframe: true, width: "550", height: "380", overlayClose: false });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="热门关键词管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="设置热门关键字，便于用户搜索最热商品" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class" style="text-align: left;padding-left:15px;">
                                 商品主类：<asp:DropDownList CssClass="select2" ID="dropCategories" runat="server" Width="231px">
                                </asp:DropDownList>
                                   关键词名称：<asp:TextBox ID="tbKeyWord" runat="server"  Width="320px" ></asp:TextBox>
                                  <asp:Button ID="btnAdd" runat="server" Text="添 加" class="adminsubmit_short" OnClick="btnAdd_Click">  </asp:Button>
                            </td>
                            
                        </tr>
                      
                        
                    </table>
                </td>
            </tr>
        </table>
        <br/>
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
                    <asp:Literal ID="Literal2" runat="server" Text="搜索关键字" />：
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
                    <li style="width: 1px; padding-left: 0px"></li>
                    <li id="liDel" runat="server" style="margin-top: -6px; width: 100px; padding-left: 0px">
                        <asp:Button ID="Button1" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" OnClick="btnDelete_Click"
                             Text="批量删除" class="adminsubmit"  />
                    </li>
                  
                </ul>
            </div>
             
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="Id" ShowToolBar="True">
            <Columns>
                <asp:BoundField DataField="Keywords" HeaderText="关键字" SortExpression="Keywords" ItemStyle-HorizontalAlign="Left" />
                <asp:TemplateField HeaderText="所属分类" SortExpression="Name" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("Name").ToString().Length == 0 ? "无分类" : Eval("Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编辑" SortExpression="Name" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a class="iframeUpdate" href="AddHotKeyword.aspx?id=<%#Eval("id") %>">编辑</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="200" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    Visible="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                           OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
