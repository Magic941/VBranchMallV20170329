<%@ Page Title="Shop_ProductTypes" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="Step3.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductType.Step3" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/jquery/AttributeManage.js" type="text/javascript"></script>
    <script type="text/javascript">
        var Tid;
        $(document).ready(function () {
            Tid = $.getUrlParam("tid");
            $(".iframe").attr("href", "Step3_1.aspx?tid=" + Tid);
            $(".iframe").colorbox({ iframe: true, width: "700", height: "380", overlayClose: false });
        });
        function addspec(ed, isImg) {
            $("#ValueM" + ed).attr("href", "AddSpec.aspx?tid=" + Tid + "&a=1&m=2&ed=" + ed + isImg);
            $("#ValueM" + ed).colorbox({ iframe: true, width: "700", height: "360", overlayClose: false });
        }
        function getUrl(ed, isImg) {
            $("#showValue" + ed).attr("href", "listSpec.aspx?tid=" + Tid + "&a=1&ed=" + ed + isImg);
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
                        <asp:Literal ID="Literal1" runat="server" Text="添加新规格" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="添加供客户可选的规格,如服装类型商品的颜色、尺码。" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/step3.jpg) no-repeat; width: 100%"></li>
                </ul>
            </div>
        </div>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px;width:100%"><a class="iframe">添加新规格</a> <b>|</b> </li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"  OnRowCommand="gridView_RowCommand"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="AttributeId">
            <Columns>
                <asp:TemplateField ItemStyle-Width="5%" HeaderText="规格类型" ItemStyle-HorizontalAlign="Center"
                    SortExpression="UseAttributeImage">
                    <ItemTemplate>
                    <%#Eval("UseAttributeImage").ToString()=="True"?"[图片]":"[文字]"%>  
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="10%" HeaderText="规格名称" ItemStyle-HorizontalAlign="Left"
                    SortExpression="AttributeName">
                    <ItemTemplate>
                    <%#Eval("AttributeName")%> 
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:TemplateField  ItemStyle-Width="50%" HeaderText="规格值" ItemStyle-HorizontalAlign="Center"
                            SortExpression="AttributeId">
                            <ItemTemplate>
                                <asp:Literal ID="litAttValue" runat="server"></asp:Literal>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField  ItemStyle-Width="10%" HeaderText="排序" ItemStyle-HorizontalAlign="Center"
                            SortExpression="DisplaySequence">
                            <ItemTemplate>
                                <asp:ImageButton ID="imgDesc" runat="server" ImageUrl="/admin/images/desc.png" CommandName="Fall" Width="16" Height="16"  title="向下"/>
                                <asp:ImageButton ID="imgAsc" runat="server" ImageUrl="/admin/images/asc.png" CommandName="Rise"  Width="16" Height="16" title="向上"/>
                            </ItemTemplate>
                        </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="15%" HeaderText="操作" ItemStyle-HorizontalAlign="Center"
                    SortExpression="AttributeId">
                    <ItemTemplate>
                        <a id="ValueM<%#Eval("AttributeId") %>" onclick="addspec(<%#Eval("AttributeId") %>,<%#Eval("UseAttributeImage").ToString()=="True"?"'&img=1'":"'&img=0'"%>)"> 添加规格值</a> / <a id="showValue<%#Eval("AttributeId") %>" onclick="getUrl(<%#Eval("AttributeId") %>,<%#Eval("UseAttributeImage").ToString()=="True"?"'&img=1'":"'&img=0'"%>)">  编辑</a> / 
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
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
                <td style="height: 6px;">
                </td>
                <td height="6">
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td  align="center">
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  class="adminsubmit" OnClick="btnDelete_Click" Visible="false" />
                    <asp:Button ID="btnSave" runat="server" Text="完成" title="返回类型列表" 
                        class="adminsubmit_short" onclick="btnSave_Click" ></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
