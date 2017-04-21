<%@ Page Title="模版管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
  CodeBehind="MShopList.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.Themes.MShopList" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <link href="/Admin/../Content/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/tab.js" type="text/javascript"> </script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"> </script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"> </script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"> </script>
    <script type="text/javascript">
        $(function () {
            $("[id$='txtPhotoName']").hide();

            $(".themeList").each(function () {
                var iscurrend = $(this).find("[id$='hidIsCurrent']").val();
                $(this).find(".lblCurrent").hide();
                if (iscurrend == "True") {
                    $(this).find("[id$='linkstart']").hide();
                    $(this).find(".lblCurrent").show();
                    $(this).css({ 'border': '3px solid rgb(108, 171, 231)' });
                }
            });
        });
    </script>
    <style type="text/css">
        .search
        {
            background-color: #ffffff;
            float: left;
            height: 35px;
        }
        
        .borderkuang td
        {
            bgcolor: "#FFFFFF";
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="txtPhoto" runat="server" Text="微商城模板管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal8" runat="server" Text=" 您可以根据自身的喜好设置微商城当前模板" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <!--Add end -->
        <!--Search -->
        <br />
        <table style="text-align: center; width: 100%;" cellpadding="5" cellspacing="5" class="border">
            <tr>
                <td style="text-align: left">
                    <asp:DataList ID="DataListPhoto" RepeatColumns="4" RepeatDirection="Horizontal" 
                        runat="server" OnItemCommand="DataListPhoto_ItemCommand">
                        <ItemTemplate>
                            <table cellpadding="2" cellspacing="8" style="margin-left: 20px" class="themeList">
                                <tr>
                                    <td style="border: 1px solid #ecf4d3; text-align: left; padding: 10px">
                                        <img src='<%#Eval("PreviewPhotoSrc") %>' style="width: 180px; height: 200px" title="<%#Eval("Description") %>" />
                                        <br />
                                        <asp:CheckBox ID="ckPhoto" runat="server" Visible="False" />
                                        <asp:HiddenField runat="server" ID="hfPhotoId" Value='<%#Eval("ID") %>' />
                                        <span>名称：<%#Eval("Name") %></span><br />
                                   <%--     <span>作者：<%#Eval("Author") %></span><br />
                                        <span>语言：<%#Eval("Language") %></span><br />--%>
                                        <br />
                                        <asp:LinkButton ID="linkstart" runat="server" Style="color: #0063dc;" CommandName="start"
                                            CommandArgument='<%#Eval("Name") %>'>
                                            <asp:Literal ID="btnStart" runat="server" Text="设为当前主题" /></asp:LinkButton>
                                             <span class="lblCurrent"  style='font-weight: bold'>当前主题</span>
                                        <asp:HiddenField runat="server" ID="hidIsCurrent" Value='<%#Eval("IsCurrent") %>' />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
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
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
