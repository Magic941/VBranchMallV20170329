<%@ Page Title="淘宝数据采集" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="GetTaoList.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.TaoData.GetTaoList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
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
            $("[id$='btnAuthorize']").click(function () {
                var appId = $("[id$='hfTaoBaoAppkey']").val();
                location.href = "http://container.open.taobao.com/container?appkey=" + appId + "&encode=utf-8";
            });
        })
    </script>
    <style type="text/css">
        .style4
        {
            text-align: right;
            width: 120px;
        }
        .style2
        {
            width: 320px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hfTaoBaoAppkey" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="txtProduct" runat="server" Text="淘宝店铺数据同步" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal8" runat="server" Text="你可以将自己淘宝店铺的商品同步到本站" />
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="3" class="borderkuang">
            
            
            
            <tr>
                <td colspan="3" style="text-align: center">
                       <asp:Button ID="btnAuthorize" runat="server" Text="登录店铺" class="adminsubmit_short"
                        OnClientClick="return false"></asp:Button>
                    <asp:Button ID="btnCancel" runat="server" Text="取消授权" OnClick="btnCancel_Click" class="adminsubmit"
                        Visible="False"></asp:Button>
                </td>
            </tr>
        </table>
          <br />
        <table width="100%" border="0" cellspacing="0" cellpadding="3" class="borderkuang">
            <tr>
                <td style="text-align: right" class="style4">
                    <asp:Literal ID="Literal12" runat="server" Text="关键字：" />
                </td>
                <td colspan="3">
                    <asp:TextBox ID="TopKeyWord" runat="server" Width="360px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="style4">
                    <asp:Literal ID="Literal14" runat="server" Text="每次采集数量" />：
                </td>
                <td class="style2">
                    <asp:TextBox ID="TopPageSize" runat="server" Style="width: 80px">20</asp:TextBox>&nbsp;
                    <span style="color: Gray">（数值太大采集速度会变慢）</span>
                </td>
                <td style="text-align: right" class="style4">
                    采集页数：
                </td>
                <td>
                    <asp:TextBox ID="TopPageNo" runat="server" Style="width: 80px">10</asp:TextBox>&nbsp;
                    <span style="color: Gray">（最大不能超过10页）</span>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="style4">
                    <asp:Literal ID="Literal2" runat="server" Text="会员折扣" />：
                </td>
                <td class="style2">
                    <asp:DropDownList ID="ddlDiscount" runat="server">
                        <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                        <asp:ListItem Value="true" Text="是"></asp:ListItem>
                        <asp:ListItem Value="false" Text="否"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: right" class="style4">
                    橱窗推荐：
                </td>
                <td>
                    <asp:DropDownList ID="ddlShowcase" runat="server">
                        <asp:ListItem Value="" Text="请选择"></asp:ListItem>
                        <asp:ListItem Value="true" Text="是"></asp:ListItem>
                        <asp:ListItem Value="false" Text="否"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="display: none">
                <td class="style5">
                    <asp:Literal ID="Literal1" runat="server" Text="起始的修改时间" />：
                </td>
                <td class="style2">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td class="style5">
                    结束的修改时间：
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-align: center">
              
                    <asp:Button ID="btnGetData" runat="server" Text="同步商品" OnClick="btnGetData_Click" class="adminsubmit_short"
                        Visible="False"></asp:Button>
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
                    <li style="float: left;  width: 100px">
                      <asp:CheckBox ID="chkRepeat" runat="server" Checked="True"/> 去除重复数据</li>
                    
                    <li style="float: left; margin-top: -4px; width: 100px">
                        <asp:Button ID="btnMove" runat="server" Text="导入选择商品" class="adminsubmit" OnClick="btnImport_Click" /></li>
                    <li style="float: left; margin-top: -4px;">
                        <asp:Button ID="btnMove2" runat="server" Text="全部导入" class="adminsubmit" OnClick="btnImportAll_Click" /></li>
                </ul>
            </div>
        </div>
        <table style="width: 100%;" cellpadding="5" cellspacing="5" class="border"  runat="server" id="tableDataList" visible="false">
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
                                            <asp:Label ID="lblImageName" runat="server" Text='<%#Maticsoft.Common.StringPlus.SubString(Eval("Title"),"...",20,true) %>'></asp:Label><br />
                                            价格：<%#Eval("Price")%>&nbsp;&nbsp;&nbsp;数量：<%#Eval("Num")%></td>
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
