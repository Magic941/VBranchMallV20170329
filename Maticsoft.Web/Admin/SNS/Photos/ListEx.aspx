<%@ Page Title="<%$Resources:CMSPhoto,ptList %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="ListEx.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Photos.ListEx" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="uc1" TagName="PhotoClassDropList" Src="~/Controls/PhotoClassDropList.ascx" %>
<%@ Register Src="~/Controls/SNSPhotoCateDropList.ascx" TagName="SNSPhotoCateDropList"
    TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Scripts/jquery-ui-1.8.11.min.js" type="text/javascript"></script>
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
//            $("[id$='from']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });
//            $("[id$='to']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:"+new Date().getFullYear()) });

        })
    </script>
    <script type="text/javascript">
        $(function () {
            $("#ctl00_ContentPlaceHolder1_txtKeyword").attr("placeholder", "搜索图片名或发布者昵称");
            $("#ctl00_ContentPlaceHolder1_txtKeyword").focus(function() {
                $(this).val("");
            });
            $("[id$='txtPhotoName']").hide();
            $(".group2").colorbox({ rel: 'group2', transition: "fade", width: "780", height: "630" });
            $(".iframe").colorbox({ iframe: true, width: "780", height: "630", overlayclose: false });
        });
        function ShowEdit(controls) {
            $(controls).hide();
            $(controls).next().show();
            $(controls).next().focus();
        }

        function EditRecomend(controls) {
            var photoID = $(controls).attr("PhotoID");
            var recomend = $(controls).attr("value");
            if (recomend == 2) {
                recomend = 0;
            }
            else {
                recomend = 2;
            }

            $.ajax({
                url: "/SNSPhotos.aspx",
                type: 'post',
                data: { Action: "EditRecomend", PhotoID: photoID, Recomend: recomend },
                dataType: 'json',
                success: function (resultData) {
                    switch (resultData.STATUS) {
                        case "OK":
                            if (recomend == 2) {
                                $(controls).attr("value", 2);
                                $(controls).text("取消频道首页");
                                $(controls).prev().text("推荐首页");
                            }
                            else {
                                $(controls).attr("value", 0);
                                $(controls).text("推荐频道首页");
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

        function EditRecomendIndex(controls) {
            var photoID = $(controls).attr("PhotoID");
            var recomend = $(controls).attr("value");
            if (recomend == 1) {
                recomend = 0;
            }
            else {
                recomend = 1;
            }

            $.ajax({
                url: "/SNSPhotos.aspx",
                type: 'post',
                data: { Action: "EditRecomend", PhotoID: photoID, Recomend: recomend },
                dataType: 'json',
                success: function (resultData) {
                    switch (resultData.STATUS) {
                        case "OK":
                            if (recomend == 1) {
                                $(controls).attr("value", 1);
                                $(controls).text("取消首页");
                                $(controls).next().text("推荐频道首页");
                            }
                            else {
                                $(controls).attr("value", 0);
                                $(controls).text("推荐首页");
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
            var photoID = $(controls).attr("PhotoID");
            var status = $(controls).attr("value");
            if (status == 1) {
                status = 0;
            }
            else {
                status = 1;
            }

            $.ajax({
                url: "/SNSPhotos.aspx",
                type: 'post',
                data: { Action: "EditStatus", PhotoID: photoID, Status: status },
                dataType: 'json',
                success: function (resultData) {
                    switch (resultData.STATUS) {
                        case "OK":
                            if (status == 1) {
                                $(controls).attr("value", 1);
                                $(controls).text("撤消审核");
                            }
                            else {
                                $(controls).attr("value", 0);
                                $(controls).text("通过审核");
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

        $(function() { $(".AddTags").colorbox({ iframe: true, width: "600", height: "630", overlayClose: false }); });
    </script>
    <style type="text/css">
        .search
        {
            float:left;
            background-color:#ffffff;
            height:35px;
            }
        .borderkuang td{bgcolor:"#FFFFFF"}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="txtPhoto" runat="server" Text="图片管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal8" runat="server" Text="您可以对图片进行删除，审核，推荐，批量删除等操作" />
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
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30"class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td   id=""   >
                    <asp:Literal ID="txtCateParent" runat="server" Text="图片分类" />
                </td>
                <td  height="35"   colspan="3"  >
                    <Maticsoft:SNSPhotoCateDropList ID="PhotoCate" runat="server" IsNull="true" />
                     <asp:Literal ID="txtPhotoCate" runat="server" Text="未分类图片" Visible="False"/>
                </td>
              
                <td  class="newstitlebody" id="Td1"  style="width:120px; ">
                  <span style=" float:right">状态：</span>
                    
                </td>
                <td  height="35"   style="width:100px">
                    <asp:DropDownList ID="dropState" runat="server">
                        <asp:ListItem Value="-1" Selected="True" Text="请选择"></asp:ListItem>
                        <asp:ListItem Value="0" Text="未审核"></asp:ListItem>
                        <asp:ListItem Value="1" Text="已审核"></asp:ListItem>
                        <asp:ListItem Value="2" Text="审核不通过"></asp:ListItem>
                            <asp:ListItem Value="3" Text="分类未明确"></asp:ListItem>
                    </asp:DropDownList>
                </td>
             
                 <td  height="35"   style="width:100px; float:left">
                    <asp:CheckBox ID="chkRecomend" runat="server" Text="只首页推荐" />
                    </td>
                    <td  style="width:100px;float:left">
                     <asp:CheckBox ID="chkRecomend2" runat="server" Text="只频道推荐" />
                </td>
                <td style=" width:50px"></td>
                
           </tr>
            <tr>
             <td width="1%" height="30">
               
                </td>
                <td  id="Td2"  style="width:50px">
                    <asp:Literal ID="Literal4" runat="server" Text="时间：&nbsp;&nbsp;" />
                </td>
                <td  height="35" class="newstitlebody"  style="width:160px">
                    <input type="text" id="from" name="from" style="width: 120px" /><asp:HiddenField ID="txtFrom"
                        runat="server" />
                </td>
                <td  style="width:40px">
                    <asp:Literal ID="Literal6" runat="server" Text="--" />
                </td>
                <td  height="35" style="width:130px">
                    <input type="text" id="to" name="to" style="width: 120px" /><asp:HiddenField ID="txtTo"
                        runat="server" />
                </td>
               <td style="text-align:right">  <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：</td>
                <td  height="35" class="newstitlebody" style="width:150px">
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                
                </td>
                 <td style="float: left" height="35" bgcolor="#FFFFFF" class="newstitlebody" colspan="4">
                  <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" class="adminsubmit_short">
                    </asp:Button>
                </td>
            </tr>
        </table>
        <script>
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
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul class="list">
                    <li>
                        <input id="Checkbox1" type="checkbox" onclick='$("#ctl00_ContentPlaceHolder1_DataListPhoto").find(":checkbox").attr("checked", $(this).attr("checked")=="checked");' />
                        <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,CheckAll %>" /></li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server">
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click">
                        <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></asp:LinkButton><b>|</b></li>
                    <li style="float: right; width: auto;">
                        <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left"
                            OnSelectedIndexChanged="radlState_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="1" Text="大图" Selected="True"></asp:ListItem><asp:ListItem Value="0" Text="列表"></asp:ListItem></asp:RadioButtonList></li></ul></div></div><div class="nTab4" style="display: none;">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="normal"><a href="ListEx.aspx?type=0" style="padding-top: 5px;">
                        <asp:Literal ID="Literal1" runat="server" Text="晒货列表"></asp:Literal></a></li><li class="normal"><a href="ListEx.aspx?type=1">
                        <asp:Literal ID="Literal3" runat="server" Text="搭配列表"></asp:Literal></a></li></ul><ul id="Ul1" style="float: right; width: 200px">
                    <li style="width: 300px"></li>
                </ul>
            </div>
            <div class="TabTitle">
            </div>
        </div>
        <table style="width: 100%; margin-left: 10; text-align: center" cellpadding="5" cellspacing="5"
            class="border">
            <tr>
                <td style="text-align: center">
                    <div id="gallery">
                        <asp:DataList ID="DataListPhoto" RepeatColumns="5" RepeatDirection="Horizontal" HorizontalAlign="Center"
                            runat="server" OnItemCommand="DataListPhoto_ItemCommand" OnItemDataBound="DataListPhoto_ItemDataBound">
                            <ItemTemplate>
                                <table cellpadding="2" cellspacing="8">
                                    <tr>
                                        <td style="border: 1px solid #ecf4d3; text-align: center">
                                             <a href="<%=Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.SNS)%>Photo/Detail/<%#Eval("PhotoID")%>" target="_blank">
                                               <img src='<%# Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbImageUrl").ToString(), "T116x170_") %>' style="width: 180px; height:240px" />
                                            </a>
                                            <br />
                                            <asp:CheckBox ID="ckPhoto" runat="server" />
                                            <asp:HiddenField runat="server" ID="hfPhotoId" Value='<%#Eval("PhotoID") %>' />
                                            <asp:Label ID="Label1" runat="server" Text='<%#GetCategoryName(Eval("CategoryId")) %>'></asp:Label>&nbsp;&nbsp; &nbsp;&nbsp; <asp:Label ID="Label2" runat="server" Text='<%#Eval("CreatedDate") %>'></asp:Label><br />[<a style="color: #0063dc; text-decoration: none;" photoid='<%#Eval("PhotoID") %>'
                                                value='<%#Eval("IsRecomend") %>' onclick='EditRecomendIndex(this)'> <%# GetRecomendIndex(Eval("IsRecomend"))%></a>] [<a style="color: #0063dc; text-decoration: none;"
                                                    photoid='<%#Eval("PhotoID") %>' value='<%#Eval("IsRecomend") %>' onclick='EditRecomend(this)'> <%# GetIsRecomend(Eval("IsRecomend"))%></a>] <br />[<a style="color: #0063dc; text-decoration: none;" photoid='<%#Eval("PhotoID") %>'
                                                value='<%#Eval("Status") %>' onclick='EditStatus(this)'> <%# GetStatus(Eval("Status"))%></a>]
                                                <span id="spanbtnDel" runat="server"> [<asp:LinkButton ID="lbtnDel" runat="server"
                                                    Style="color: #0063dc;" CommandName="delete" CommandArgument='<%#Eval("PhotoID") %>'
                                                    OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">
                                                    <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,btnDeleteText %>" /></asp:LinkButton>]
                                                    </span>
                                            [<a style="color: #0063dc; text-decoration: none;" class="AddTags" href="/Admin/SNS/AddTags/AddTags.aspx?Type=Photo&id=<%#Eval("PhotoID") %>">标签</a>] </td></tr></table></ItemTemplate></asp:DataList></div></td></tr><tr>
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
                <td style="float: left;" height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <Maticsoft:SNSPhotoCateDropList ID="PhotoCategory" runat="server" IsNull="true" />
                </td>
                <td align="left" style="float: left; ">
                    <asp:Button ID="btnMove" runat="server" Text="批量归类" class="adminsubmit" OnClick="btnMove_Click" />
                </td>
                <td style="float: left">
                    <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
                <td style="float: left">
                    <asp:Button ID="btnRecomend" runat="server" Text="推荐到首页" class="adminsubmit" OnClick="btnRecomend_Click" />
                </td>
                <td style="float: left">
                    <asp:Button ID="btnRecomend2" runat="server" Text="推荐到频道" class="adminsubmit" OnClick="btnRecomend2_Click" />
                </td>
                <td style="float: left">
                    <asp:Button ID="Button2" runat="server" Text="取消推荐" class="adminsubmit" OnClick="btnNoRecomend_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
