<%@ Page Title="活动管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="GiftsList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ActivityManage.GiftsList" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            //$(".SetProduct").each(function () { 
            //var test = document.getElementById("ctl00_ContentPlaceHolder1_gridView_ctl06_lbAmApplyStyles").innerHTML;
            //debugger
            //var test = document.getElementsByName("lbAstyle");


            $(".iframe").colorbox({ iframe: true, width: "840", height: "460", overlayClose: false });
            $(".SetProduct").colorbox({ iframe: true, width: "1080", height: "800", overlayClose: false });
            $(".btnStatus").each(function () {
                var ruleId = $(this).attr("AMId");
                var AMStatus = parseInt($(this).attr("AMStatus"));
                if (AMStatus == 0) {
                    $(this).css("color", "green");
                }
                else {
                    $(this).css("color", "red");
                }
            })

            $(".btnStatus").click(function () {
                var AMId = $(this).attr("AMId");
                var AMStatus = parseInt($(this).attr("AMStatus"));
                AMStatus = AMStatus == 0 ? 1 : 0;
                var self = $(this);
                $.ajax({
                    url: ("AMList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "UpdateStatus", Callback: "true", AMId: AMId, AMStatus: AMStatus },
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            ShowSuccessTip("操作成功");
                            self.attr("AMStatus", AMStatus);
                            if (AMStatus == 0) {
                                self.text("启用");
                                self.css("color", "green");
                            }
                            else {
                                self.text("关闭");
                                self.css("color", "red");
                            }
                        }
                        else {
                            alert("系统忙请稍后再试！");
                        }
                    }
                });
            })
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
                        <asp:Literal ID="Literal1" runat="server" Text="活动列表" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        可以设置活动规则和商品。
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="../../Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="活动名称搜索" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    &nbsp;开启状态：
                    <asp:DropDownList ID="ddlStatus" runat="server">
                        <asp:ListItem Selected="True" Text="请选择"></asp:ListItem>
                        <asp:ListItem Text="未开启"></asp:ListItem>
                        <asp:ListItem Text="进行中"></asp:ListItem>
                        <asp:ListItem Text="已过期"></asp:ListItem>
                    </asp:DropDownList>
                     &nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li id="liAdd" runat="server" style="background: url(/images/icon8.gif) no-repeat 5px 3px">
                        <a class="iframe" href="AddGifts.aspx">添加</a> <b>|</b> </li>
                    <li id="liDel" runat="server" style="background: url(/admin/images/delete.gif) no-repeat;
                        display: none"><a href="#">删除</a><b>|</b></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="AMId"
            Style="float: left;" ShowGridLine="true" ShowHeaderStyle="true">
            <Columns>
                <asp:TemplateField HeaderText="活动名称" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("AMName")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="应用方式" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbAmApplyStyles" Name ="lbAstyle" runat="server" Text='<%# GetRuleMode(Eval("AMApplyStyles")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="活动类型" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lbType" Name ="lbType" runat="server" Text='<%# GetAMType(Eval("AMType")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AMStartDate" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="AMEndDate" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                       <a class="btnStatus" AMId='<%# Eval("AMId") %>'  AMStatus='<%# Eval("AMStatus") %>'> <%# GetStatus(Eval("AMStatus")) %> </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="AMCreateDate" HeaderText="创建时间" DataFormatString="{0:yyyy-MM-dd HH:mm:ss}"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="创建者" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# GetUserName(Eval("AMUserId")) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%--<a class="SetProduct" <%#int.Parse(Eval("AMApplyStyles").ToString())==1?"disabled='true'":"" %> href='<%#int.Parse(Eval("AMApplyStyles").ToString())==1?"javascript:":"SetProductAM.aspx?id=<%# Eval(\"AMId\")" %>'>设置活动商家 </a>--%>
                        <a class="SetProduct" <%#int.Parse(Eval("AMApplyStyles").ToString())!=2?"disabled='false'":"" %> href='ProductGifts.aspx?id=<%# Eval("AMId") %>'>设置商品 </a>
                        <a class="SetProduct" <%#int.Parse(Eval("AMApplyStyles").ToString())==2?"disabled='true'":"" %> href='SupplierGifts.aspx?id=<%# Eval("AMId") %>'>设置商铺 </a>
                        &nbsp; <span id="lbtnModify" runat="server"><a class="iframe" href='EditGifts.aspx?id=<%# Eval("AMId") %>'>
                            修改</a>&nbsp;</span>
                        <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" BackColor="#FFF" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
        <script type="text/javascript">
            $(function () {


            })


        </script>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td style="display: none">
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

