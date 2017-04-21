<%@ Page Title="Shop_ProductTypes" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="Modify2.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductType.Modify2" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/tab.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery/AttributeManage.js" type="text/javascript"></script>
    <script type="text/javascript">
        function getQueryString(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
            var r = window.location.search.substr(1).match(reg);
            if (r != null) return unescape(r[2]); return null;
        }

        var Tid;
        $(document).ready(function () {
            Tid = getQueryString("tid");
            $("#btnAddAtt").attr("href", "Step2_1.aspx?tid=" + Tid + "&ed=-1");
            $("#btnAddAtt").colorbox({ iframe: true, width: "700", height: "450", overlayClose: false });
            $("#m1").attr("href", "Modify1.aspx?tid=" + Tid);
            $("#m2").attr("href", "Modify2.aspx?tid=" + Tid);
            $("#m3").attr("href", "Modify3.aspx?tid=" + Tid);
        });
        function addValue(ed) {
            $("#ValueM" + ed).attr("href", "addValue.aspx?tid=" + Tid + "&ed=" + ed);
            $("#ValueM" + ed).colorbox({ iframe: true, width: "700", height: "370", overlayClose: false }); 
        }
        function getUrl(ed) {
            $("#showValue" + ed).attr("href", "listV.aspx?tid=" + Tid + "&ed=" + ed);
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
                        <asp:Literal ID="Literal1" runat="server" Text="添加扩展属性" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="商品类型是一系列属性的组合，可以用来向客户展示某些商品具有的特有的属性。" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="normal"><a id="m1">基本设置</a></li>
                    <li class="active"><a id="m2">扩展属性</a></li>
                    <li class="normal"><a id="m3">规格</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content0">
                <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True" CssClass="border"
                    ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
                    OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify" OnRowCommand="gridView_RowCommand"
                    Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
                    CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="AttributeId" style="border-top:none;">
                    <Columns>
                        <asp:BoundField DataField="AttributeName" HeaderText="属性名称" SortExpression="AttributeName"
                            ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="10%" />
                            
                        <asp:TemplateField  ItemStyle-Width="5%" HeaderText="支持多选" ItemStyle-HorizontalAlign="Center"
                            SortExpression="UsageMode">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgChose" runat="server" ImageUrl="/admin/images/accept.png" CommandName="ChoseAny" Width="16" Height="16" />
                                <asp:ImageButton ID="imgNochose" runat="server" ImageUrl="/admin/images/dele.png" CommandName="ChoseOne" Width="16" Height="16" />

                            </ItemTemplate>
                        </asp:TemplateField>                            
                        <asp:TemplateField  ItemStyle-Width="50%" HeaderText="属性值" ItemStyle-HorizontalAlign="Center"
                            SortExpression="AttributeId">
                            <ItemTemplate>
                                <asp:Literal ID="litAttValue" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  ItemStyle-Width="10%" HeaderText="排序" ItemStyle-HorizontalAlign="Center"
                            SortExpression="DisplaySequence" Visible="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDesc" runat="server" ImageUrl="/admin/images/desc.png" CommandName="Fall" Width="16" Height="16"  title="向下"/>
                                <asp:ImageButton ID="imgAsc" runat="server" ImageUrl="/admin/images/asc.png" CommandName="Rise"  Width="16" Height="16" title="向上"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-Width="15%"  HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                            SortExpression="AttributeId">
                            <ItemTemplate>
                               <span id="lbtnAdd" runat="server"> <a id="ValueM<%#Eval("AttributeId") %>" onclick="addValue(<%#Eval("AttributeId") %>)">添加属性值</a> / </span>
                               <span id="lbtnModify" runat="server"> <a id="showValue<%#Eval("AttributeId") %>" onclick="getUrl(<%#Eval("AttributeId") %>)"> 编辑</a> /</span>
                                <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
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
                <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;float: left;">
                    <tr>
                        <td style="height: 6px;">
                        </td>
                        <td height="6">
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 1px;">
                        </td>
                        <td  runat="server" id="tdbtnaddatt">
                        <input type="button" id="btnAddAtt" value="添加扩展属性" class="adminsubmit"/>
                            <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                                class="adminsubmit" OnClick="btnDelete_Click" Visible="false" />
                            <asp:Button ID="btnSave" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" Text="下一步" title="下一步，添加规格" class="adminsubmit_short"  OnClick="btnSave_Click" Visible="false">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
