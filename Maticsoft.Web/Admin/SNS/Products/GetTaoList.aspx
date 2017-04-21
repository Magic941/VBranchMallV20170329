<%@ Page Title="淘宝数据采集" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="GetTaoList.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Products.GetTaoList" %>

<%@ Register Src="~/Controls/TaoBaoCategoryDropList.ascx" TagName="TaoBaoCategoryDropList"
    TagPrefix="Maticsoft" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="Maticsoft" TagName="SNSCategoryDropList" Src="~/Controls/SNSCategoryDropList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#ctl00_ContentPlaceHolder1_AspNetPager1").css("float", "right");
            $(".iframe").colorbox({ iframe: true, width: "600", height: "650", overlayClose: false });
            $(".icolor").colorbox({ iframe: true, width: "500", height: "330", overlayClose: false });
            $("#ctl00_ContentPlaceHolder1_txtUserId").attr("placeholder", "请填写会员编号");
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="txtProduct" runat="server" Text="淘宝普通商品数据采集" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal8" runat="server" Text="您可以根据选择的条件批量采集淘宝普通商品到本站。" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="3" class="borderkuang">
            <tr>
                <td style="text-align: right" class="style4">
                    <asp:Literal ID="Literal6" runat="server" Text="淘宝商品分类：" />
                </td>
                <td colspan="3">
                    <Maticsoft:TaoBaoCategoryDropList ID="TaoBaoCate" runat="server" IsNull="true" />
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="style4">
                    <asp:Literal ID="Literal12" runat="server" Text="关键字：" />
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TopKeyWord" runat="server" Width="360px"></asp:TextBox>
                </td>
            
            </tr>
            <tr>
                <td class="style5">
                    <asp:Literal ID="Literal14" runat="server" Text="每次采集数量" />：
                </td>
                <td class="style2">
                    <asp:TextBox ID="TopPageSize" runat="server" Style="width: 80px">20</asp:TextBox>&nbsp;
                    <span style="color: Gray">（数值太大采集速度会变慢）</span>
                </td>
                <td class="style5">
                    采集页数：
                </td>
                <td class="style2">
                    <asp:TextBox ID="TopPageNo" runat="server" Style="width: 80px">10</asp:TextBox>&nbsp;
                    <span style="color: Gray">（最大不能超过10页）</span>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Literal ID="Literal2" runat="server" Text="垂直市场类型" />：
                </td>
                <td class="style2">
                 <asp:DropDownList ID="ddlMarketType" runat="server">
                        <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                        <asp:ListItem Value="3" Text="3C垂直市场"></asp:ListItem>
                          <asp:ListItem Value="4" Text="鞋城垂直市场"></asp:ListItem>
                          <asp:ListItem Value="8" Text="网游垂直市场"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="style5" >
                    市场类型：
                </td>
                <td class="style2">
                    <asp:DropDownList ID="ddlMarket" runat="server">
                        <asp:ListItem Value="1" Text="请选择"></asp:ListItem>
                        <asp:ListItem Value="1" Text="C2C市场"></asp:ListItem>
                          <asp:ListItem Value="2" Text="B2C市场"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: center">
                    <asp:Button ID="btnGetData" runat="server" Text="采集" OnClick="btnGetData_Click" class="adminsubmit_short">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <!--Title end -->
        <!--Search -->
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul class="list" style="vertical-align: middle; padding-top: 8px; line-height: 20px;
                    height: 30px; background: white url(none);">
                    <li>
                        <input id="Checkbox1" type="checkbox" onclick='$(":checkbox").attr("checked", $(this).attr("checked")=="checked");' />
                        <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,CheckAll %>" /></li>
                    <li style="width: 180px">默认会员编号：<asp:TextBox ID="txtUserId" runat="server" Style="width: 80px"
                        OnTextChanged="Text_Change" AutoPostBack="True"></asp:TextBox>
                    </li>
                    <li style="width: 180px">会员专辑：<asp:DropDownList ID="ddlAlbumList" runat="server"
                        Width="120px">
                    </asp:DropDownList>
                    </li>
                    <li style="float: left; margin-top: -4px; width: 100px">
                        <asp:Button ID="btnMove" runat="server" Text="导入选择商品" class="adminsubmit" OnClick="btnImport_Click" /></li>
                    <li style="float: left; margin-top: -4px;">
                        <asp:Button ID="btnMove2" runat="server" Text="全部导入" class="adminsubmit" OnClick="btnImportAll_Click" /></li>
                </ul>
            </div>
        </div>
        <table style="width: 100%;" cellpadding="5" cellspacing="5" class="border" style="margin-left: 10px;">
            <tr>
                <td>
                    <div id="Div1">
                        <asp:DataList ID="DataListProduct" RepeatColumns="5" RepeatDirection="Horizontal"
                            HorizontalAlign="Left" runat="server">
                            <ItemTemplate>
                                <table cellpadding="2" cellspacing="8">
                                    <tr>
                                        <td style="border: 1px solid #ecf4d3; text-align: center">
                                            <a class="iframe" href='<%#Eval("PicUrl") %>' title='<%#Eval("Name") %>'>
                                                <img src='<%#Eval("PicUrl") %>_200x200.jpg' style="width: 180px;" />
                                            </a>
                                            <br />
                                            <asp:CheckBox ID="ckProduct" runat="server" />
                                            <asp:HiddenField runat="server" ID="hfProduct" Value='<%#Eval("ProductId") %>' />
                                                <asp:Label ID="lblImageName" runat="server" Text='<%#Maticsoft.Common.StringPlus.SubString(Eval("Name"),"...",20,true) %>'></asp:Label><br />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList></div>
                </td>
            </tr>
            <tr>
                <td>
                    <webdiyer:AspNetPager runat="server" ID="AspNetPager1" CssClass="anpager" CurrentPageButtonClass="cpb"
                        OnPageChanged="AspNetPager1_PageChanged" PageSize="15" FirstPageText="<%$Resources:Site,FirstPage %>"
                        LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                        PrevPageText="<%$Resources:Site,GVTextPrevious %>">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td align="left" style="float: left">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

