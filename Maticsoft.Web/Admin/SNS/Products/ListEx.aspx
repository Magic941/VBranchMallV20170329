<%@ Page Title="商品分享" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ListEx.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Products.ListEx" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Src="~/Controls/SNSCategoryDropList.ascx" TagName="SNSCategoryDropList"
    TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="/Admin/../Content/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("#ctl00_ContentPlaceHolder1_AspNetPager1").css("float", "right");
            $(".iframe").colorbox({ iframe: true, width: "600", height: "650", overlayClose: false });
            $(".icolor").colorbox({ iframe: true, width: "500", height: "330", overlayClose: false });
        })
        $(function () {
            var type = $("#ctl00_ContentPlaceHolder1_tType").val();
            $("#ctl00_ContentPlaceHolder1_txtKeyword").attr("placeholder", "搜索商品名或发布者昵称");
            $("#ctl00_ContentPlaceHolder1_txtKeyword").focus(function () {
                $(this).val("");
            })
            if (type == 1) {
                $.ajax({
                    url: "/SNSCategories.aspx",
                    type: 'post',
                    data: { Action: "GetSNSProductNodes" },
                    dataType: 'json',
                    success: function (resultData) {
                        switch (resultData.STATUS) {
                            case "OK":
                                $(".ProductCate").each(function () {
                                    var _self = this;
                                    //循环添加第一级select
                                    for (var n = 0; n < resultData.DATA.length; n++) {
                                        //循环添加子集
                                        $(_self).append("    <option value=\"" + resultData.DATA[n]["ClassID"] + "\">" + resultData.DATA[n]["ClassName"] + "</option>");
                                    }
                                });
                                break;
                            default:
                                break;
                        }
                    },
                    error: function (xmlHttpRequest, textStatus, errorThrown) {
                        alert(xmlHttpRequest.responseText);
                    }
                });
            }
            else {
                $(".ProductCate").hide();
            }

            //        $(".ProductCate").bind("blur", function () {
            //            alert($(this).val());
            //        });
        });
        function SetCategory(controls, id) {

            var cateId = $(controls).val(); //去掉字符串左右两边的空字符
            if (cateId > 0) {
                $.ajax({
                    url: "/SNSCategories.aspx",
                    type: 'post',
                    data: { Action: "SetCategory", ProductId: id, CategoryId: cateId },
                    dataType: 'json',
                    success: function (resultData) {
                        if (resultData.STATUS == "OK") {
                            location.reload();
                        }
                        else {
                            alert("操作失败！");
                        }
                    }
                });
            }
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("[id$='txtPhotoName']").hide();
            $(".group2").colorbox({ rel: 'group2', transition: "fade" });
        });
        function ShowEdit(controls) {
            $(controls).hide();
            $(controls).next().show();
            $(controls).next().focus();
        }

        function EditPhotoName(controls, id) {
            $.ajax({
                url: "/EditPhotoHandle.aspx",
                type: 'post', dataType: 'text', timeout: 10000,
                data: { Action: "EditPhotoName", PhotoName: $(controls).val(), PhotoId: id },
                success: function (resultData) {
                    if (resultData != "") {
                        $(controls).hide();
                        $(controls).prev().text(resultData).show();
                    }
                }
            });
        }
        function EditRecomend(controls) {
            var productId = $(controls).attr("ProductId");
            var recomend = $(controls).attr("value");
            if (recomend == 1) {
                recomend = 0;
            }
            else {
                recomend = 1;
            }

            $.ajax({
                url: "/SNSProduct.aspx",
                type: 'post',
                data: { Action: "EditRecomend", ProductId: productId, Recomend: recomend },
                dataType: 'json',
                success: function (resultData) {
                    switch (resultData.STATUS) {
                        case "OK":
                            if (recomend == 1) {
                                $(controls).attr("value", 1);
                                $(controls).text("取消推荐");
                            }
                            else {
                                $(controls).attr("value", 0);
                                $(controls).text("推荐到首页");

                            }
                            break;
                        default:
                            break;
                    }
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    alert(xmlHttpRequest.responseText);
                }
            });
        }

        function EditStatus(controls) {
            var productId = $(controls).attr("ProductId");
            var status = $(controls).attr("value");
            if (status == 2) {
                return;
            }
            if (status == 1) {
                status = 0;
            }
            else {
                status = 1;
            }

            $.ajax({
                url: "/SNSProduct.aspx",
                type: 'post',
                data: { Action: "EditStatus", ProductId: productId, Status: status },
                dataType: 'json',
                success: function (resultData) {
                    switch (resultData.STATUS) {
                        case "OK":
                            if (status == 1) {
                                $(controls).attr("value", 1);
                                $(controls).text("取消审核");
                            }
                            else {
                                $(controls).attr("value", 0);
                                $(controls).text("审核通过");
                            }
                            break;
                        default:
                            break;
                    }
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    alert(xmlHttpRequest.responseText);
                }
            });
        }
    </script>
    <script type="text/javascript">
        $(function () {
            $("#from").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#to").datepicker("option", "minDate", selectedDate);
                    $("#ctl00_ContentPlaceHolder1_txtFrom").val($("#from").val());
                }
            });
            $("#to").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#from").datepicker("option", "maxDate", selectedDate);
                    $("#ctl00_ContentPlaceHolder1_txtTo").val($(this).val());
                }
            });
        });
    </script>
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="txtProduct" runat="server" Text="已分类商品管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal8" runat="server" Text="您可以对商品进行删除，对应分类，批量删除等操作" />
                    </td>
                </tr>
            </table>
        </div>
        <%--        <table width="100%" border="0" cellspacing="0" cellpadding="3" class="borderkuang">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle" colspan="4">
                    <asp:Literal ID="Literal15" runat="server" Text="  批量获取淘宝数据" />
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
                    <asp:Literal ID="Literal14" runat="server" Text="页码" />：
                </td>
                <td class="style2">
                    <asp:TextBox ID="TopPageNo" runat="server" Style="width: 80px">1</asp:TextBox>&nbsp;
                </td>
                   <td style="text-align: right" class="style3">
                    <asp:Literal ID="Literal16" runat="server" Text="每页数目：" />
                </td>
                <td>
                    <asp:TextBox ID="TopPageSize" runat="server">100</asp:TextBox>
                    <asp:Button ID="btnGetData" runat="server" Text="获取" OnClick="btnGetData_Click" class="adminsubmit_short">
                    </asp:Button>
                </td>
              
                    
            </tr>
        </table>
        <br />--%>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="3" class="borderkuang">
            <tr class="newstitlebody">
                <td width="20px">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td style="width: 60px; text-align: right;">
                    <asp:Literal ID="txtCateParent" runat="server" Text="商品分类：" />
                </td>
                <td style="width: 250px">
                    <Maticsoft:SNSCategoryDropList ID="SNSCate" runat="server" IsNull="true" />
                     <asp:Literal ID="noneCategory" runat="server" Text="未分类" Visible="False"/>
                </td>
                <td style="width: 50px; text-align: right;">
                    <asp:Literal ID="Literal1" runat="server" Text="状态：" />
                </td>
                <td>
                    <asp:DropDownList ID="dropState" runat="server">
                        <asp:ListItem Value="-1" Selected="True" Text="请选择"></asp:ListItem>
                        <asp:ListItem Value="0" Text="未审核"></asp:ListItem>
                        <asp:ListItem Value="1" Text="已审核"></asp:ListItem>
                        <asp:ListItem Value="2" Text="下架"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:CheckBox ID="chkRecomend" runat="server" Text="只首页推荐" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td style="text-align: right;" id="Td2">
                    <asp:Literal ID="Literal3" runat="server" Text="时间：" />
                </td>
                <td>
                    <input type="text" id="from" name="from" style="width: 90px" /><asp:HiddenField ID="txtFrom"
                        runat="server" />
                    --
                    <input type="text" id="to" name="to" style="width: 90px" /><asp:HiddenField ID="txtTo"
                        runat="server" />
                </td>
                <td style="text-align: right;">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
                </td>
                <td>
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" class="adminsubmit_short">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul class="list">
                    <li>
                        <input id="Checkbox1" type="checkbox" onclick='$("#ctl00_ContentPlaceHolder1_DataListProduct").find(":checkbox").attr("checked", $(this).attr("checked")=="checked");' />
                        <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,CheckAll %>" /></li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server">
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click">
                            <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></asp:LinkButton><b>|</b></li>
                    <li style="float: right; width: auto;">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal"
                            align="left" OnSelectedIndexChanged="radlState_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="1" Text="大图" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="0" Text="列表"></asp:ListItem>
                        </asp:RadioButtonList>
                    </li>
                </ul>
            </div>
        </div>
        <div class="nTab4" style="display: none;">
            <div class="TabTitle">
                <ul id="Ul2">
                    <li class="normal"><a href="ListEx.aspx?type=0" style="padding-top: 5px;">
                        <asp:Literal ID="Literal4" runat="server" Text="已分类商品"></asp:Literal></a></li><li
                            class="normal"><a href="ListEx.aspx?type=1">
                                <asp:Literal ID="Literal5" runat="server" Text="未分类商品"></asp:Literal></a></li></ul>
                <ul id="Ul3" style="float: right; width: 200px">
                    <li style="width: 300px"></li>
                </ul>
            </div>
            <div class="TabTitle">
            </div>
        </div>
        <table style="width: 100%;" cellpadding="5" cellspacing="5" class="border" style="margin-left: 10px;">
            <tr>
                <td>
                    <div id="Div1">
                        <asp:DataList ID="DataListProduct" RepeatColumns="5" RepeatDirection="Horizontal"
                            HorizontalAlign="Left" runat="server" OnItemCommand="DataListProduct_ItemCommand">
                            <ItemTemplate>
                                <table cellpadding="2" cellspacing="8">
                                    <tr>
                                        <td style="border: 1px solid #ecf4d3; text-align: center">
                                            <a class="group2" href='<%#Eval("NormalImageUrl") %>' title='<%#Eval("ProductName") %>'>
                                                <img src='<%#Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbImageUrl").ToString(), "T116x170_")%>' style="width: 180px;" />
                                            </a>
                                            <br />
                                            <asp:CheckBox ID="ckProduct" runat="server" />
                                            <asp:HiddenField runat="server" ID="hfProduct" Value='<%#Eval("ProductID") %>' />
                                            <a target="_blank" style="color: #0063dc; text-decoration: none;" href="<%=BasePath %>Product/Detail/<%#Eval("ProductID") %>">
                                                <%#Maticsoft.Common.StringPlus.SubString(Eval("ProductName"),"...",20,true) %></a><br />
                                            <asp:Label ID="Label1" runat="server" Text='<%#GetCategoryName(Eval("CategoryID")) %>'></asp:Label><br />
                                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("CreatedDate") %>'></asp:Label><br />
                                            [<a style="color: #0063dc; text-decoration: none;" productid='<%#Eval("ProductID") %>'
                                                value='<%#Eval("IsRecomend") %>' onclick='EditRecomend(this)'>
                                                <%# GetIsRecomend(Eval("IsRecomend"))%></a>] 
                                                [<a style="color: #0063dc; text-decoration: none;"
                                                    productid='<%#Eval("ProductID") %>' value='<%#Eval("Status") %>' onclick='EditStatus(this)'>
                                                    <%# GetStatus(Eval("Status"))%></a>] 
                                                    [<asp:LinkButton ID="lbtnDel" runat="server"
                                                        Style="color: #0063dc;" CommandName="delete" CommandArgument='<%#Eval("ProductID") %>'
                                                        OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">
                                                        <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,btnDeleteText %>" /></asp:LinkButton>]
                                            [<a style="color: #0063dc; text-decoration: none;" class="iframe" href="/Admin/SNS/AddTags/AddTags.aspx?Type=Product&id=<%#Eval("ProductID") %>">标签</a>]
                                            [<a style="color: #0063dc; text-decoration: none;" class="icolor" href="AddColor.aspx?id=<%#Eval("ProductID") %>">颜色</a>]
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
                <td style="float: left" height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <Maticsoft:SNSCategoryDropList ID="SNSCate2" runat="server" IsNull="true" />
                </td>
                <td align="left" style="float: left">
                    <asp:Button ID="btnMove" runat="server" Text="转移分类" class="adminsubmit" OnClick="btnMove_Click" />
                </td>
                <td style="float: left">
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))'
                        ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" />
                </td>
                <td style="float: left;padding-top: 4px" >
                    <asp:DropDownList ID="ddAction" runat="server" OnSelectedIndexChanged="ddAction_Changed" AutoPostBack="True">
                        <asp:ListItem Value="" Text="请选择操作..."></asp:ListItem>
                        <asp:ListItem Value="1" Text="推荐到首页"></asp:ListItem>
                        <asp:ListItem Value="2" Text="取消推荐"></asp:ListItem>
                        <asp:ListItem Value="3" Text="批量下架"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
