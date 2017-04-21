<%@ Page Title="商品表" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Products.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/SNSCategoryDropList.ascx" TagName="SNSCategoryDropList"
    TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="/Admin/../Content/themes/base/jquery.ui.all.css" rel="stylesheet" type="text/css" />
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

             })


    </script>
    <script type="text/javascript">
         $(document).ready(function () {

                });
    </script>
    <script type="text/javascript">
        $(function () {
            var type = $("#ctl00_ContentPlaceHolder1_tType").val();
            $("#ctl00_ContentPlaceHolder1_txtKeyword").attr("placeholder", "搜索商品名或发布者昵称");
            $("#ctl00_ContentPlaceHolder1_txtKeyword").focus(function() {
                $(this).val("");
            });
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="tType" runat="server" />
    <!--Title -->
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
                    <asp:Literal ID="Literal5" runat="server" Text="您可以对商品进行删除，对应分类，批量删除等操作" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr class="newstitlebody"  bgcolor="#FFFFFF">
                <td width="1%" height="30" >
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td  style=" max-width:40px" >
                    <asp:Literal ID="txtCateParent" runat="server" Text="商品分类" />
                </td>
                <td  height="35"  style="max-width:150px">
                    <Maticsoft:SNSCategoryDropList ID="SNSCate" runat="server" IsNull="true" />
                </td >
                    <td  style="width:30px" id="Td1">
                    <asp:Literal ID="Literal1" runat="server" Text="状态" />
                </td>
                <td height="35"   style=" width:70px">
                    <asp:DropDownList ID="dropState" runat="server">
                       <asp:ListItem Value="-1" Selected="True" Text="请选择"></asp:ListItem>
                           <asp:ListItem Value="0"  Text="未审核"></asp:ListItem>
                             <asp:ListItem Value="1" Text="已审核"></asp:ListItem>
                               <asp:ListItem Value="2" Text="已下架"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                
                    <td  style=" width:50px"  id="Td2" class="newstitlebody"  bgcolor="#FFFFFF">
                    <asp:Literal ID="Literal3" runat="server" Text="  时间：" />
                </td>
                <td  height="35"   style=" width:80px">
               <input type="text" id="from" name="from" style=" width:80px"/><asp:HiddenField ID="txtFrom" runat="server" />
                </td>
                
                    <td  id="Td3"  style="width:20px">
                    <asp:Literal ID="Literal6" runat="server" Text="---" />
                </td>
                <td  height="35"   style=" width:80px">
                <input type="text" id="to" name="to" style=" width:80px"/><asp:HiddenField ID="txtTo" runat="server" />
                </td>
                 <td height="35"  style=" width:100px" class="newstitlebody"  bgcolor="#FFFFFF" >
                    <asp:CheckBox ID="chkRecomend" runat="server" Text="只首页推荐" />
                </td>
                
                <td height="35" >
                    &nbsp;&nbsp;<asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="搜索" OnClick="btnSearch_Click" class="adminsubmit_short">
                    </asp:Button>
                </td>
            </tr>
        </table>
   
          <script type="text/javascript">
              $(function () {
                  $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
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
                <ul>
                   
                    <li style="float: right;width: auto;"><asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" 
                            align="left" OnSelectedIndexChanged="radlState_SelectedIndexChanged"   AutoPostBack="true" >
                               <asp:ListItem Value="1" Text="大图" ></asp:ListItem>
                                                <asp:ListItem Value="0" Text="列表" Selected="True"></asp:ListItem>
                                             
                                            </asp:RadioButtonList></li>
                </ul>
            </div>
        </div>
        <div class="nTab4" style="display: none;">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="normal"><a href="List.aspx?type=0 " style="padding-top: 5px;">
                        <asp:Literal ID="Literal7" runat="server" Text="已分类商品"></asp:Literal></a></li>
                    <li class="normal"><a href="List.aspx?type=1">
                        <asp:Literal ID="Literal8" runat="server" Text="未分类商品"></asp:Literal></a></li>
                </ul>

                  <ul id="Ul1" style=" float:right ; width:200px" >
                    <li  style=" width:300px" >
                      
                        </li>
                </ul>
            </div>
            <div class="TabTitle">
              
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ProductID">
            <Columns>
                <asp:TemplateField  ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Image ID="Image1" runat="server" Width="100px" Height="100px" ImageAlign="Middle"
                            ImageUrl='<%#Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbImageUrl").ToString(), "T116x170_")%>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品名称" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <a href="<%=BasePath %>Product/Detail/<%#Eval("ProductID")%>" target="_blank">
                            <%#Eval("ProductName")%></a></br>
                                   <span style=" color:Gray">  <%#Eval("CreatedNickName")%>
                        &nbsp;&nbsp; &nbsp;&nbsp;<%#Eval("CreatedDate")%></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品分类" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#GetCategoryName(Eval("CategoryID"))%>
                        <select id="txt<%# Eval("ProductID")%>" style="width: 120px;" onblur='SetCategory(this, <%# Eval("ProductID")%>)'
                            class="ProductCate">
                            <option value="0">请选择</option>
                        </select>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" >
                    <ItemStyle />
                    <ItemTemplate>
           
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" ForeColor="Blue" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
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
                <td style="float: left" height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <Maticsoft:SNSCategoryDropList ID="SNSCate2" runat="server" IsNull="true" />
                </td>
                <td align="left" style="float: left">
                    <asp:Button ID="btnMove" runat="server" Text="转移分类" class="adminsubmit" OnClick="btnMove_Click" />
                </td>
                <td style="float: left">
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"/>
                </td>
                   <td style="float: left">
                    <asp:Button ID="btnDeleteAll" runat="server" Text="全部删除"
                        class="adminsubmit" OnClick="btnDeleteAll_Click"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="你确定要一键删除检索全部商品结果数据？"/>
                </td>
                   <td style="float: left;padding-top: 4px">
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
