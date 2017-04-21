<%@ Page Title="照片表" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Photos.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="~/Controls/SNSPhotoCateDropList.ascx" TagName="SNSPhotoCateDropList"
    TagPrefix="Maticsoft" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
        <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
  <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
     <script type="text/javascript">
         $(function () {
             $("#ctl00_ContentPlaceHolder1_txtKeyword").attr("placeholder", "搜索图片名或发布者昵称");
             $("#ctl00_ContentPlaceHolder1_txtKeyword").focus(function () {
                 $(this).val("");
             });
             $(function() {
                 $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
//                 $("[id$='from']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });
//                 $("[id$='to']").prop("readonly", true).datepicker({ dateFormat: "yy-mm-dd", yearRange: ("1949:" + new Date().getFullYear()) });
             });
         })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
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
                    <asp:Literal ID="Literal1" runat="server" Text="您可以对图片进行删除，审核，推荐，批量删除等操作" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
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
                    <asp:Literal ID="Literal4" runat="server" Text="时间:&nbsp;&nbsp;" />
                </td>
                <td  height="35" class="newstitlebody"  style="width:160px">
                    <input type="text" id="from" name="from" style="width: 120px"  /><asp:HiddenField ID="txtFrom"
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
                <ul>
                    <li style="float: right; width: auto;">
                        <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left"
                            OnSelectedIndexChanged="radlState_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="1" Text="大图"></asp:ListItem>
                            <asp:ListItem Value="0" Text="列表" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </li>
                </ul>
            </div>
        </div>
        <div class="nTab4" style="display: none;">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="normal"><a href="List.aspx?type=0 " style="padding-top: 5px;">
                        <asp:Literal ID="Literal7" runat="server" Text="晒货列表"></asp:Literal></a></li>
                    <li class="normal"><a href="List.aspx?type=1">
                        <asp:Literal ID="Literal8" runat="server" Text="搭配列表"></asp:Literal></a></li>
                </ul>
                <ul id="Ul1" style="float: right; width: 200px">
                    <li style="width: 300px"></li>
                </ul>
            </div>
            <div class="TabTitle">
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="PhotoID">
            <Columns>
                <asp:TemplateField ItemStyle-Width="100">
                    <ItemTemplate>
                        <a href="<%=Maticsoft.Components.MvcApplication.GetCurrentRoutePath(Maticsoft.Web.AreaRoute.SNS)%>Photo/Detail/<%#Eval("PhotoID")%>" target="_blank">
                            <asp:Image ID="Image1" runat="server" Width="100px" Height="100px" ImageAlign="Middle"
                                ImageUrl='<%#Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbImageUrl").ToString(), "T116x170_")%>' />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="商品分类" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetCategoryName(Eval("CategoryID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="CreatedNickName" HeaderText="创建人姓名" SortExpression="CreatedNickName"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="CreatedDate" HeaderText="上传的日期" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="操作" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center">
                    <ItemStyle />
                    <ItemTemplate>
                        &nbsp;&nbsp;<span runat="server" id="lbtnModify"> <a href="Modify.aspx?id=<%#Eval("PhotoID") %>&type=<%=Type %>" style="color: Blue">
                            编辑</a> &nbsp;&nbsp;</span>
                        <%--<a href="Show.aspx?id=<%#Eval("PhotoID") %>&type=<%=Type %>" style="color: Blue">详细</a>--%>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
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
                <td style="float: left; display:none" height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <Maticsoft:SNSPhotoCateDropList ID="PhotoCategory" runat="server" IsNull="true" />
                </td>
                <td align="left" style="float: left; display:none">
                    <asp:Button ID="btnMove" runat="server" Text="转移分类" class="adminsubmit" OnClick="btnMove_Click" />
                </td>
                <td style="float: left">
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" />
                </td>

                    <td style="float: left">
                    <asp:Button ID="btnRecomend" runat="server" Text="推荐到首页"
                        class="adminsubmit" OnClick="btnRecomend_Click" />
                </td>
                 <td style="float: left">
                    <asp:Button ID="btnRecomend2" runat="server" Text="推荐到频道"
                        class="adminsubmit" OnClick="btnRecomend2_Click" />
                </td>
                    <td style="float: left">
                    <asp:Button ID="Button2" runat="server" Text="取消推荐"
                        class="adminsubmit" OnClick="btnNoRecomend_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
