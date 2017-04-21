
<%@ Page Title="<%$ Resources:SysManage,ptFeedbackList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="ItemList.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.MsgItem.ItemList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("[id$='ddUser']").select2(); });
    </script>
    <script type="text/javascript">
        $(function () {
            $(".iframe").colorbox({ iframe: true, width: "780", height: "630", overlayclose: false });
            $(".imageUrl").each(function () {
                var src = $(this).attr("src");
                if (src != "") {
                    $(this).show();
                }
            });
            $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
            $("#ctl00_ContentPlaceHolder1_txtFrom").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtTo").datepicker("option", "minDate", selectedDate);
                }
            });
            $("#ctl00_ContentPlaceHolder1_txtTo").prop("readonly", true).datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                onClose: function (selectedDate) {
                    $("#ctl00_ContentPlaceHolder1_txtFrom").datepicker("option", "maxDate", selectedDate);
                    $("#ctl00_ContentPlaceHolder1_txtTo").val($(this).val());
                }
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="微信素材管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="lblTitle" runat="server" Text="微信素材管理" />
                    </td>
                </tr>
            </table>
        </div>
        
                
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="true" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" CellPadding="3"
            BorderWidth="1px" ShowCheckAll="True" DataKeyNames="ItemId">
            <Columns>
                <asp:TemplateField HeaderText="图片" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="60px">
                    <ItemTemplate> 
                        <a href="<%#String.Format(Eval("PicUrl").ToString(),"N_")%>" class="iframe">
                    
                            <img src="<%#String.Format(Eval("PicUrl").ToString(),"T_")%>" alt="" width="60" height="60" class="imageUrl"  /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="标题" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <%# Eval("Title")%></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="内容" ItemStyle-HorizontalAlign="Left"  >
                    <ItemTemplate>
                        <%# Eval("Description")%></ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="操作" ItemStyle-HorizontalAlign="center" ItemStyle-Width="120px">
                    <ItemTemplate>
                        <a href='UpdateItem.aspx?id=<%# Eval("ItemId")%>'  >编辑</a>
                          <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm%>"
                            Text="<%$Resources:Site,btnDeleteText%>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
               <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
    
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

