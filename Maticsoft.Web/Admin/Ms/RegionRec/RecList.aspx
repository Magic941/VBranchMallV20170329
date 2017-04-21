<%@ Page Title="热门地区推荐" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="RecList.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.RegionRec.RecList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        var html = "<li class='colord7 classcolor' recId={0}><a href='javascript:void(0)' style='color: #0078B6'  data-tag='{1}'>{1} <span class='deltags' style='cursor: pointer'>×</span></a></li>";
        $.ajax({
            url: ("RecList.aspx?timestamp={0}").format(new Date().getTime()),
            type: 'POST', dataType: 'json', timeout: 10000,
            data: { Action: "GetRecList", Callback: "true" },
            success: function (resultData) {
                if (resultData.STATUS == "SUCCESS") {
                    var arry = resultData.DATA.split("|");
                    if (arry == "")
                        return;
                    $.each(arry, function (i, n) {
                        var tag = html.format(n.split(",")[0], n.split(",")[1]);
                        $("#txtRecList").append(tag);
                    });
                }
                else {
                    alert("系统忙请稍后再试！");
                }
            }
        });
        $("#txtRecList").find("li").die("click").live("click", function () {
            var _self = $(this);
            var recId = $(this).attr("recId");
            $.ajax({
                url: ("RecList.aspx?timestamp={0}").format(new Date().getTime()),
                type: 'POST', dataType: 'json', timeout: 10000,
                data: { Action: "Delete", Callback: "true", RecId: recId },
                success: function (resultData) {
                    if (resultData.STATUS == "SUCCESS") {
                        _self.hide();
                    }
                    else {
                        alert("系统忙请稍后再试！");
                    }
                }
            });
        });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="热门地区推荐管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以设置热门城市" />
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
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:DropDownList ID="ddRegion" runat="server" OnSelectedIndexChanged="ddRegion_Change"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table >
        <!--Search end-->
        <br />
        <div class="newslist" style="display: none">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px; display: none"
                        id="liDel" runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
                   
                </ul>
            </div>
        </div>
        
        <br/>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"  >
        <tr>
        <td width="400" style=" vertical-align:top">
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="RegionId"
            Style="float: left;"  OnRowCommand="gridView_RowCommand">
            <Columns>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$Resources:Site,lblTitle%>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# Eval("RegionName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="操作" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkRegionRec" runat="server" CausesValidation="False" CommandName="RegionRec"
                            CommandArgument='<%# Eval("RegionId")%>' Text="设为推荐城市" ForeColor="Blue"></asp:LinkButton>
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
        </td>
        <td>
        <div class="borderkuang" style="height: 400px;">
            <div class="right_text">
                已推荐的城市</div>
            <ul class="cen_yy" id="txtRecList">
                <div style="clear: both">
                </div>
            </ul>
            <div style="clear: both">
            </div>
        </div>
        </td>
        </tr>
        </table>
        
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
