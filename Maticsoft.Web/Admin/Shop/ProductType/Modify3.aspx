<%@ Page Title="Shop_ProductTypes" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="Modify3.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductType.Modify3" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Maticsoft" %>
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
            $("#btnAddSpec").attr("href", "Step3_1.aspx?tid=" + Tid + "&ed=-1");
            $("#btnAddSpec").colorbox({ iframe: true, width: "650", height: "375", overlayClose: false });
            $("#m1").attr("href", "Modify1.aspx?tid=" + Tid);
            $("#m2").attr("href", "Modify2.aspx?tid=" + Tid);
            $("#m3").attr("href", "Modify3.aspx?tid=" + Tid);
        });
        function addspec(ed, isImg) {
            $("#ValueM" + ed).attr("href", "AddSpec.aspx?tid=" + Tid + "&ed=" + ed + isImg);
            $("#ValueM" + ed).colorbox({ iframe: true, width: "700", height: "360", overlayClose: false });
        }
        function getUrl(ed, isImg) {
            $("#showValue" + ed).attr("href", "listSpec.aspx?tid=" + Tid + "&ed=" + ed + isImg);
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
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="normal"><a id="m1">基本设置</a></li>
                    <li class="normal"><a id="m2">扩展属性</a></li>
                    <li class="active"><a id="m3">规格</a></li>
                </ul>
            </div>
        </div>
        <maticsoft:gridviewex id="gridView" runat="server" allowpaging="True" allowsorting="True" CssClass="border"
            ShowToolBar="True" autogeneratecolumns="False" onbind="BindData" onpageindexchanging="gridView_PageIndexChanging"
            onrowcommand="gridView_RowCommand" onrowdatabound="gridView_RowDataBound" onrowdeleting="gridView_RowDeleting"
            unexportedcolumnnames="Modify" width="100%" pagesize="10" ShowExportExcel="False"
            showexportword="False" excelfilename="FileName1" CellPadding="3" BorderWidth="1px"
            showcheckall="false" datakeynames="AttributeId">
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
                      <span id="lbtnAdd" runat="server">  <a id="ValueM<%#Eval("AttributeId") %>" onclick="addspec(<%#Eval("AttributeId") %>,<%#Eval("UseAttributeImage").ToString()=="True"?"'&img=1'":"'&img=0'"%>)"> 添加规格值</a> /</span>
                      <span id="lbtnModify" runat="server">   <a id="showValue<%#Eval("AttributeId") %>" onclick="getUrl(<%#Eval("AttributeId") %>,<%#Eval("UseAttributeImage").ToString()=="True"?"'&img=1'":"'&img=0'"%>)">  编辑</a> / </span>
                        
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
        </maticsoft:gridviewex>
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
                <td>
                    <asp:Button ID="btnDelete" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" Visible="false" />
                   <span id="spanbtnAddSpec"  runat="server"> <input type="button" id="btnAddSpec" value="添加新规格" class="adminsubmit" /></span>
                    <asp:Button ID="btnSave" runat="server" Text="完成" title="返回类型列表" class="adminsubmit_short"
                        OnClick="btnSave_Click" Visible="false"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
