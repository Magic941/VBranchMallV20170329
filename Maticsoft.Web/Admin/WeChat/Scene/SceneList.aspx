<%@ Page Title="微信渠道推广场景管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="SceneList.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Scene.SceneList" %>
    <%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "480", height: "372", overlayClose: false });
            var value = $("[id$='hfDataCount']").val();
            if (value == "0") {
                $("#dataList").hide();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信渠道推广场景管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以设置不同微信渠道推广场景操作" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat; width: 50px;" id="liAdd"
                        runat="server"><a href="AddScene.aspx" title="添加新的渠道推广" class="iframe">添加</a> <b>|</b>
                    </li>
                </ul>
            </div>
        </div>
        <asp:HiddenField ID="hfDataCount" runat="server" />
        <table id="dataList" style="width: 100%;" cellpadding="5" cellspacing="5" class="border" style="margin-left: 10px;">
            <tr>
                <td>
                        <asp:DataList ID="DataListProduct" RepeatColumns="5" RepeatDirection="Horizontal"
                            HorizontalAlign="Left" runat="server" OnItemCommand="DataListProduct_ItemCommand">
                            <ItemTemplate>
                                <table cellpadding="2" cellspacing="8">
                                    <tr>
                                        <td style="border: 1px solid #ecf4d3; text-align: center">
                                            <img src='<%#String.IsNullOrWhiteSpace(Eval("ImageUrl").ToString())?" /Upload/WeChat/QRImage/QRImage_"+ Eval("SceneId")+".jpg":Eval("ImageUrl") %>' style="width: 200px;" />
                                            <br />
                                            <%# Eval("Name")%><br />
                                            [<a href='UpdateScene.aspx?id=<%# Eval("SceneId")%>' class="iframe"> 编辑</a>] [<asp:LinkButton
                                                ID="lbtnDel" runat="server" Style="color: #0063dc;" CommandName="delete" CommandArgument='<%#Eval("SceneId") %>'
                                                OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,btnDeleteText %>" /></asp:LinkButton>]
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                </td>
            </tr>
            <tr>
                <td>
                    <webdiyer:aspnetpager runat="server" id="AspNetPager1" cssclass="anpager" currentpagebuttonclass="cpb"
                        onpagechanged="AspNetPager1_PageChanged" pagesize="15" firstpagetext="<%$Resources:Site,FirstPage %>"
                        lastpagetext="<%$Resources:Site,EndPage %>" nextpagetext="<%$Resources:Site,GVTextNext %>"
                        prevpagetext="<%$Resources:Site,GVTextPrevious %>">
                    </webdiyer:aspnetpager>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
