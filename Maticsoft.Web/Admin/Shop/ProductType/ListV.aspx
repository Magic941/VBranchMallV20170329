<%@ Page Title="Shop_AttributeValues" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="ListV.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductType.ListV" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        var Tid;
        var AttId;
        var a;
        $(document).ready(function () {
            Tid = $.getUrlParam("tid");
            AttId = $.getUrlParam("ed");
            a = $.getUrlParam("a");
            $("#aAdd").attr("href", "addValue.aspx?tid=" + Tid + "&ed=" + AttId + "&m=1&a="+a);
            $("#aAdd").colorbox({ iframe: true, width: "700", height: "370", overlayClose: false });
        });
        function EditValue(vId) {

            $("#ValueM" + vId).attr("href", "addValue.aspx?tid=" + Tid + "&ed=" + AttId + "&v=" + vId + "&m=1&a=" + a);
            $("#ValueM" + vId).colorbox({ iframe: true, width: "700", height: "370", overlayClose: false });
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
                        编辑【<asp:Literal ID="Literal1" runat="server" Text=" " />】扩展属性值
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text=" 您可以编辑扩展属性值 " />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->        
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li   style="background: url(/images/icon8.gif) no-repeat 5px 3px;width:100%" id="liAdd" runat="server"><a id="aAdd"> 添加属性值</a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"  OnRowCommand="gridView_RowCommand"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="100" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="ValueId">
            <Columns>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="属性值" ItemStyle-HorizontalAlign="Left" SortExpression="ValueId">
                    <ItemTemplate>
                        <%#Eval("ValueStr")%>
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField  ItemStyle-Width="10%" HeaderText="排序" ItemStyle-HorizontalAlign="Center"
                            SortExpression="DisplaySequence">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDesc" runat="server" ImageUrl="/admin/images/desc.png" CommandName="Fall" Width="16" Height="16"  title="向下"/>
                                <asp:ImageButton ID="imgAsc" runat="server" ImageUrl="/admin/images/asc.png" CommandName="Rise"  Width="16" Height="16" title="向上"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center" SortExpression="ValueId">
                    <ItemTemplate>
                     <span id="lbtnModify"  runat="server" > <a id='ValueM<%#Eval("ValueId") %>' onclick='EditValue(<%#Eval("ValueId") %>)'>修改</a> / </span>
                        <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
                          OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
                    <asp:Button ID="btnBack" runat="server" Text="返回" title="返回扩展属性列表"
                        class="adminsubmit_short" onclick="btnBack_Click" />
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  class="adminsubmit" OnClick="btnDelete_Click" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
