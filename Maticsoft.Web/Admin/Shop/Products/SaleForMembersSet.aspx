<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="SaleForMembersSet.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.SaleForMembersSet" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/jquery.scrollTo-min.js" type="text/javascript"></script>
    <script src="/admin/js/jquery/SaleForMemberSet.helper.js" type="text/javascript"></script>
    <link href="/Admin/js/select2-3.4.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-3.4.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script src="../../js/My97DatePicker/WdatePicker.js"></script>
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/swfobject.js"></script>
    <script type="text/javascript" src="/Admin/js/jquery.uploadify/uploadify-v2.1.4/jquery.uploadify.v2.1.4.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='lnkDelete']").hide();
            $("#uploadify").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/UploadNormalImg.aspx',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.jpg',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'auto': true,
                'width': 76,
                'height': 25,
                'multi': true,
                'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {
                    if (response.split('|')[0] == "1") {
                        $("[id$='imgAd']").attr("src", response.split('|')[1].format(''));
                        $("[id$='hfFileUrl']").val(response.split('|')[1]);
                        $("[id$='HiddenField_ISModifyImage']").val("True");
                    } else {
                        alert("图片上传失败！");
                    }
                }
            });
        });
    </script>
    <style>
        .mask {
            margin: 0;
            padding: 0;
            border: solid 5px red;
            width: 100%;
            height: 100%;
            background: #333;
            opacity: 0.6;
            filter: alpha(opacity=60);
            z-index: 100;
            position: fixed;
            top: 0;
            left: 0;
            display: block;
        }

        .borderImage {
            width: 81px;
            height: 81px;
            border: 1px solid #CCC;
            text-align: center;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            resizeImg('.borderImage', 80, 80);
        });
    </script>
    <style type="text/css">
        .borderImage {
            width: 81px;
            height: 81px;
            border: 1px solid #CCC;
            text-align: center;
        }
    </style>

    <script type="text/javascript">
        var checkDate = function () {
            if ($("#ctl00_ContentPlaceHolder1_StartDate").val() == "" || $("#ctl00_ContentPlaceHolder1_EndDate").val() == "") {
                alert("活动日期区间必填");
                return false;
            }
        }
        $(function () {
            $(".select2").select2({ placeholder: "请选择", width: '240px' });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="background: white; width: 100%" id="relatedProc">
        <div class="advanceSearchArea clearfix">
            <!--预留显示高级搜索项区域-->
        </div>
        <div class="toptitle">
            <h3 class="title_height" style="margin-bottom: 5px"></h3>
        </div>
        <div class="Goodsgifts">
            <div class="left">
                <h1>
                    <asp:Literal ID="litDesc" runat="server"></asp:Literal></h1>
                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
                    <ul>
                        <li>
                            <asp:Literal runat="server" ID="Literal4" Text="操作类型" />：
                            <abbr class="formselect">
                                <asp:DropDownList CssClass="select2" Width="50px" ID="ddlOperation" runat="server" OnSelectedIndexChanged="ddlOperation_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="1">请选择</asp:ListItem>
                                    <asp:ListItem Value="2">克隆</asp:ListItem>
                                    <asp:ListItem Value="3">添加</asp:ListItem>
                                </asp:DropDownList>
                            </abbr>
                        </li>
                        <li>
                            <asp:Literal runat="server" ID="LitProductCategories" Text="分类" />：
                            <abbr class="formselect">
                                <asp:DropDownList CssClass="select2" ID="drpProductCategory" runat="server">
                                </asp:DropDownList>
                            </abbr>
                        </li>
                        <li>
                            <asp:Literal runat="server" ID="Literal1" Text="商家" />：
                            <abbr class="formselect">
                                <asp:DropDownList CssClass="select2" ID="ddlSupplier" runat="server">
                                </asp:DropDownList>
                            </abbr>
                        </li>
                        <li>
                            <asp:Literal runat="server" ID="LitProductName" Text="商品名称" />：
                            <asp:TextBox ID="txtProductName" runat="server" />
                        </li>
                        <li style="display: none;">
                            <asp:Literal runat="server" ID="litProductNum" Text="货号" />：
                            <asp:TextBox ID="txtProductNum" runat="server" />
                        </li>
                        <li id="temp" runat="server" style="width:250px;">
                            
                        </li>
                         <li id="Clone_Date" runat="server">
                            活动时间：
                            <abbr class="formselect">
                                <asp:TextBox ID="S_StartDate" Onclick="WdatePicker()"  runat="server"></asp:TextBox> ~ <asp:TextBox ID="S_EndDate" Onclick="WdatePicker()" runat="server"></asp:TextBox> 
                            </abbr>
                        </li>
                        <li>
                             <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="查询" CssClass="adminsubmit_short" />
                        </li>
                        
                    </ul>
                </asp:Panel>
                <div class="content">
                    <div class="youhuiproductlist searchproductslist">
                        <asp:HiddenField ID="hfCurrentAllData" runat="server" />
                        <asp:DataList runat="server" ID="dlstSearchProducts" Width="96%" DataKeyField="ProductId" RepeatLayout="Table" OnItemDataBound="dlstSearchProducts_ItemDataBound" OnSelectedIndexChanged="dlstSearchProducts_SelectedIndexChanged">
                            <ItemTemplate>
                                <table width="100%" border="0" cellspacing="0" class="conlisttd" skuid="<%# Eval("ProductId") %>">
                                    <tr>
                                        <td width="14%" rowspan="3" class="img">
                                            <div class="borderImage">
                                                <a href="/Product/Detail/<%# Eval("ProductId") %>" target="_blank">
                                                    <img width="80px" height="80px" id="ThumbnailUrl40" src="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(), "T158X158_") %>" /></a>
                                            </div>
                                        </td>
                                        <td height="27" colspan="4" class="br_none"><span class="Name">
                                            <a href='/Product/Detail/<%# Eval("ProductId") %>' target="_blank"><%# Eval("ProductName") %></a>
                                        </span></td>
                                    </tr>
                                    <tr>
                                        <td width="29%" height="28" valign="top"><span class="colorC">最低价：<%# Eval("LowestSalePrice", "{0:n2}")%></span></td>
                                        <td width="11%" align="right" valign="top">&nbsp;</td>
                                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                                        <td width="15%" valign="top"><a href="javascript:void(0);"><span runat="server" id="lbtnAdd" class="submit_add">添加</span></a></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="r">
                        <div style="display: none;">
                            <asp:Button runat="server" ID="btnAddSearch" CssClass="adminsubmit" Text="全部添加" />
                        </div>
                        <div class="pagination">
                            <webdiyer:AspNetPager runat="server" ID="anpSearchProducts" CssClass="anpager" CurrentPageButtonClass="cpb"
                                OnPageChanged="AspNetPager_PageChanged" PageSize="15" FirstPageText="<%$Resources:Site,FirstPage %>"
                                LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                                PrevPageText="<%$Resources:Site,GVTextPrevious %>" ShowPageIndexBox="Never" NumericButtonCount="5">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
            <div class="right" style="position:relative">
                <h1>已添加的商品</h1>
                
                <ul  style="float:left;">
                        <li>
                            <asp:Literal runat="server" ID="Literal2" Text="活动时间" />：
                            <abbr class="formselect">
                                <asp:TextBox ID="StartDate" Onclick="WdatePicker()"  runat="server"></asp:TextBox> ~ <asp:TextBox ID="EndDate" Onclick="WdatePicker()" runat="server"></asp:TextBox> 
                            </abbr>
                        </li>
                        <li id="Add_TotalLimitQu" runat="server">
                            <asp:Literal runat="server" ID="Literal6" Text="总限购数量" />：
                            <abbr class="formselect">
                                <asp:TextBox ID="TotalLimitQu"   runat="server"></asp:TextBox>
                            </abbr>
                        </li>
                        <li id="Add_AccountLimitQu" runat="server">
                            <asp:Literal runat="server" ID="Literal7" Text="账号限购数量" />：
                            <abbr class="formselect">
                                <asp:TextBox ID="AccountLimitQu"   runat="server"></asp:TextBox>
                            </abbr>
                        </li>
                        <li >
                            <asp:Button ID="btnSave" runat="server" Text="保存" CssClass="adminsubmit_short" OnClientClick="return checkDate();" OnClick="btnSave_Click" />
                        </li>
                    </ul>
                <%--<div style="float:left; margin-bottom:10px;" id="Add_UploadImg" runat="server">
                            <asp:Literal runat="server" ID="Literal8" Text="商品图片" />：
                                            <asp:HiddenField ID="hfFileUrl" runat="server" />
                    <input type="file" style="float:left" name="uploadify" id="uploadify" /><br />
                                            <div style="float:left" id="fileQueue">
                                            </div>
                                            
                </div>--%>
                <div class="content">
                    <div class="youhuiproductlist addedproductslist">
                        <asp:HiddenField ID="hfSelectedData" runat="server" ClientIDMode="Static" Value=""  />
                        <asp:DataList runat="server" ID="dlstAddedProducts" Width="96%" DataKeyField="ProductId" RepeatLayout="Table" OnItemDataBound="dlstAddedProducts_ItemDataBound">
                            <ItemTemplate>
                                <table width="100%" border="0" cellspacing="0" class="conlisttd" skuid="<%# Eval("ProductId") %>">
                                    <tr>
                                        <td width="14%" rowspan="3" class="img">

                                            <div class="borderImage">
                                                <img id="Img1" width="80px" height="80px" src="<%# @Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbnailUrl1").ToString(), "T158X158_") %>" />
                                            </div>
                                        </td>
                                        <td height="27" colspan="5" class="br_none"><span class="Name">
                                            <a href='/Product/Detail/<%# Eval("ProductId") %>' target="_blank"><%# Eval("ProductName") %></a>
                                        </span></td>
                                    </tr>
                                    <tr>
                                        <td width="29%" height="28" valign="top"><span class="colorC">最低价：<%# Eval("LowestSalePrice", "{0:n2}")%></span></td>
                                        <td width="11%" align="right" valign="top">&nbsp;</td>
                                        <td width="14%" align="left" valign="top" class="a_none">&nbsp;</td>
                                        <td width="15%" valign="top"><a href="javascript:void(0);"><span runat="server" id="lbtnDel"><span class="submit_del" skuid="<%# Eval("ProductId") %>">删除</span></span></a></td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                    <div class="r">
                        <div>
                            &nbsp;
                        </div>
                        <div class="pagination">
                            <webdiyer:AspNetPager runat="server" ID="anpAddedProducts" CssClass="anpager" CurrentPageButtonClass="cpb"
                                OnPageChanged="AspNetPager_PageChanged" PageSize="15" FirstPageText="<%$Resources:Site,FirstPage %>"
                                LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                                PrevPageText="<%$Resources:Site,GVTextPrevious %>" ShowPageIndexBox="Never" NumericButtonCount="5">
                            </webdiyer:AspNetPager>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <input type="hidden" id="type" value="<%=SelectType %>"
    </div>
    
</asp:Content>