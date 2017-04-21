<%@ Page Title="淘宝数据采集" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ProductDatas.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Products.ProductDatas" %>

<%@ Register Src="~/Controls/TaoBaoCategoryDropList.ascx" TagName="TaoBaoCategoryDropList"
    TagPrefix="Maticsoft" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="Maticsoft" TagName="SNSCategoryDropList" Src="~/Controls/SNSCategoryDropList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <style type="text/css">
        .PostPerson
        {
            width: 100px;
        }
        .PostDate
        {
            width: 100px;
        }
        .TopShortTd
        {
            width: 100px;
            text-align: right;
        }
        .TopTd
        {
            width: 250px;
            text-align: left;
        }
        .style2
        {
            width: 250px;
        }
        .style3
        {
            width: 75px;
        }
        .style4
        {
            width: 93px;
        }
        .style5
        {
            width: 93px;
            text-align: right;
        }
        .anpager
        {
            float: right;
        }
        .uploadExcel
        {
            width: 235px;
        }
        .style6
        {
            width: 150px;
            text-align: right;
            padding-bottom: 10px;
            padding-top: 10px;
            height: 1px;
        }
        .style7
        {
            width: 120px;
            height: 1px;
        }
        .style9
        {
            width: 100px;
            text-align: right;
            padding-bottom: 10px;
            padding-top: 10px;
            height: 1px;
        }
        .style10
        {
            height: 1px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("#ctl00_ContentPlaceHolder1_AspNetPager1").css("float", "right");
            $(".iframe").colorbox({ iframe: true, width: "600", height: "650", overlayClose: false });
            $(".icolor").colorbox({ iframe: true, width: "500", height: "330", overlayClose: false });
            $("#ctl00_ContentPlaceHolder1_txtUserId").attr("placeholder", "请填写会员编号");

            //  $("#ctl00_ContentPlaceHolder1_")
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="txtProduct" runat="server" Text="商品数据采集" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal8" runat="server" Text="您可以根据选择的条件批量采集商品到本站。" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr style="padding-top: 5px;">
                <td bgcolor="#FFFFFF" class="newstitle">
                    导入商品数据
                </td>
            </tr>
            <tr>
                <td class="style6">
                    默认会员编号：
                </td>
                <td class="style7">
                    <asp:TextBox ID="txtUserId2" runat="server" Style="width: 80px" OnTextChanged="Text2_Change"
                        AutoPostBack="True"></asp:TextBox>
                </td>
                <td width="200px" class="style10">
                    会员专辑：<asp:DropDownList ID="ddlAlbumList2" runat="server" Width="120px">
                    </asp:DropDownList>
                </td>
                <td class="style9">
                    Excel数据文件：
                </td>
                <td  class="style10">
                    <asp:FileUpload ID="uploadExcel" runat="server" CssClass="uploadExcel" />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="uploadExcel"
                        runat="server" ErrorMessage="请选择正确的格式" ValidationExpression="^.+(xls|xlsx)$"></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
                <td class="td_class" >
                    对应站内商品分类：
                </td>
                <td  colspan="2">
                    <Maticsoft:SNSCategoryDropList ID="SNSCategoryDropList" runat="server" IsNull="true" />
                </td>
              
            </tr>
            <tr>
                  <td class="td_class" height="30">
                </td>
                  <td class="td_class"  style="text-align: left" >
                    <asp:CheckBox ID="chkExcelRepeat" runat="server" Checked="True" />筛选重复数据
                </td>
            </tr>
            <tr>
                <td class="td_class" height="30">
                </td>
                <td height="25">
                    <asp:Button ID="btnUpload" runat="server" Text="开始上传" CssClass="adminsubmit" OnClick="btnImportExcel_Click" />
                </td>
            </tr>
            <tr>
                <td class="td_class" height="30">
                </td>
                <td height="25" colspan="4" style="color: gray;" rowspan="2">
                    提示： 导入的Excel格式直接支持淘宝联盟数据，其它平台数据要保持列的名称是如下标题，顺序任意，即可实现导入<br />
                    列标题规范：主图片，宝贝标题，商品单价，单品链接
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr style="padding-top: 5px;">
                <td bgcolor="#FFFFFF" class="newstitle">
                    采集淘宝客商品数据
                </td>
            </tr>
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
                <td class="style2">
                    <asp:TextBox ID="TopKeyWord" runat="server"></asp:TextBox>
                </td>
                <td style="text-align: right" class="style3">
                    <asp:Literal ID="Literal13" runat="server" Text="地区：" />
                </td>
                <td>
                    <asp:TextBox ID="TopArea" runat="server"></asp:TextBox>
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
                    <asp:Literal ID="Literal1" runat="server" Text="商品佣金比率" />：
                </td>
                <td class="style2">
                    <asp:TextBox ID="TopStartRate" runat="server" Style="width: 30px"></asp:TextBox><span
                        style="font-size: 16px; vertical-align: middle">-</span>
                    <asp:TextBox ID="TopEndRate" runat="server" Style="width: 30px"></asp:TextBox><span
                        style="font-size: 16px; vertical-align: middle">%</span> &nbsp;<span style="color: Gray">（留空即默认）</span>
                </td>
                <td class="style5">
                    排序方式：
                </td>
                <td class="style2">
                    <asp:DropDownList ID="TopSort" runat="server">
                        <asp:ListItem Value="price_desc" Text="默认价格从高到低"></asp:ListItem>
                        <asp:ListItem Value="price_asc" Text="价格从低到高"></asp:ListItem>
                        <asp:ListItem Value="credit_desc" Text="信用等级从高到低"></asp:ListItem>
                        <asp:ListItem Value="commissionRate_desc" Text="佣金比率从高到低"></asp:ListItem>
                        <asp:ListItem Value="commissionRate_asc" Text="佣金比率从低到高"></asp:ListItem>
                        <asp:ListItem Value="commissionNum_desc" Text="成交量成高到低"></asp:ListItem>
                        <asp:ListItem Value="commissionNum_asc" Text="成交量从低到高"></asp:ListItem>
                        <asp:ListItem Value="commissionVolume_desc" Text="总支出佣金从高到低"></asp:ListItem>
                        <asp:ListItem Value="commissionVolume_asc" Text="总支出佣金从低到高"></asp:ListItem>
                        <asp:ListItem Value="delistTime_desc" Text="商品下架时间从高到低"></asp:ListItem>
                        <asp:ListItem Value="delistTime_asc" Text="商品下架时间从低到高"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="style5">
                    <asp:Literal ID="Literal2" runat="server" Text="30天推广量" />：
                </td>
                <td class="style2">
                    <asp:TextBox ID="TopStartNum" runat="server" Style="width: 40px"></asp:TextBox><span
                        style="font-size: 16px; vertical-align: middle">-</span>
                    <asp:TextBox ID="TopEndNum" runat="server" Style="width: 40px"></asp:TextBox>&nbsp;<span
                        style="color: Gray; vertical-align: middle">（留空即默认）</span>
                </td>
                <td class="style5">
                    店铺信誉：
                </td>
                <td class="style2">
                    <asp:DropDownList ID="TopStartCredit" runat="server">
                        <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                        <asp:ListItem Value="1heart" Text="一心"></asp:ListItem>
                        <asp:ListItem Value="2heart " Text="两心"></asp:ListItem>
                        <asp:ListItem Value="3heart" Text="三心"></asp:ListItem>
                        <asp:ListItem Value="4heart" Text="四心"></asp:ListItem>
                        <asp:ListItem Value="5heart" Text="五心"></asp:ListItem>
                        <asp:ListItem Value="1diamond" Text="一钻"></asp:ListItem>
                        <asp:ListItem Value="2diamond" Text="两钻"></asp:ListItem>
                        <asp:ListItem Value="3diamond" Text="三钻"></asp:ListItem>
                        <asp:ListItem Value="4diamond" Text="四钻"></asp:ListItem>
                        <asp:ListItem Value="5diamond" Text="五钻"></asp:ListItem>
                        <asp:ListItem Value="1crown" Text="一冠"></asp:ListItem>
                        <asp:ListItem Value="2crown" Text="两冠"></asp:ListItem>
                        <asp:ListItem Value="3crown" Text="三冠"></asp:ListItem>
                        <asp:ListItem Value="4crown" Text="四冠"></asp:ListItem>
                        <asp:ListItem Value="5crown" Text="五冠"></asp:ListItem>
                        <asp:ListItem Value="1goldencrown" Text="一黄冠"></asp:ListItem>
                        <asp:ListItem Value="2goldencrown" Text="二黄冠"></asp:ListItem>
                        <asp:ListItem Value="3goldencrown" Text="三黄冠"></asp:ListItem>
                        <asp:ListItem Value="4goldencrown" Text="四黄冠"></asp:ListItem>
                        <asp:ListItem Value="5goldencrown" Text="五黄冠"></asp:ListItem>
                    </asp:DropDownList>
                    -
                    <asp:DropDownList ID="TopEndCredit" runat="server">
                        <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                        <asp:ListItem Value="1heart" Text="一心"></asp:ListItem>
                        <asp:ListItem Value="2heart " Text="两心"></asp:ListItem>
                        <asp:ListItem Value="3heart" Text="三心"></asp:ListItem>
                        <asp:ListItem Value="4heart" Text="四心"></asp:ListItem>
                        <asp:ListItem Value="5heart" Text="五心"></asp:ListItem>
                        <asp:ListItem Value="1diamond" Text="一钻"></asp:ListItem>
                        <asp:ListItem Value="2diamond" Text="两钻"></asp:ListItem>
                        <asp:ListItem Value="3diamond" Text="三钻"></asp:ListItem>
                        <asp:ListItem Value="4diamond" Text="四钻"></asp:ListItem>
                        <asp:ListItem Value="5diamond" Text="五钻"></asp:ListItem>
                        <asp:ListItem Value="1crown" Text="一冠"></asp:ListItem>
                        <asp:ListItem Value="2crown" Text="两冠"></asp:ListItem>
                        <asp:ListItem Value="3crown" Text="三冠"></asp:ListItem>
                        <asp:ListItem Value="4crown" Text="四冠"></asp:ListItem>
                        <asp:ListItem Value="5crown" Text="五冠"></asp:ListItem>
                        <asp:ListItem Value="1goldencrown" Text="一黄冠"></asp:ListItem>
                        <asp:ListItem Value="2goldencrown" Text="二黄冠"></asp:ListItem>
                        <asp:ListItem Value="3goldencrown" Text="三黄冠"></asp:ListItem>
                        <asp:ListItem Value="4goldencrown" Text="四黄冠"></asp:ListItem>
                        <asp:ListItem Value="5goldencrown" Text="五黄冠"></asp:ListItem>
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
                    <li style="width: 100px">
                        <asp:CheckBox ID="chkRepeat" runat="server" Checked="True" />筛选重复数据 </li>
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
                                            <a class="iframe" href='<%#Eval("PicUrl") %>' title='<%#Eval("Title") %>'>
                                                <img src='<%#Eval("PicUrl") %>_200x200.jpg' style="width: 180px;" />
                                            </a>
                                            <br />
                                            <asp:CheckBox ID="ckProduct" runat="server" />
                                            <asp:HiddenField runat="server" ID="hfProduct" Value='<%#Eval("NumIid") %>' />
                                            <a href="<%#Eval("ClickUrl") %>" target="_blank" title='<%#Eval("Title") %>'>
                                                <asp:Label ID="lblImageName" runat="server" Text='<%#Maticsoft.Common.StringPlus.SubString(Eval("Title"),"...",20,true) %>'></asp:Label></a><br />
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
