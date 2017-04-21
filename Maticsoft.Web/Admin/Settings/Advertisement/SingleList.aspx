<%@ Page Title="AD_Advertisement" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="SingleList.aspx.cs" Inherits="Maticsoft.Web.Admin.Advertisement.SingleList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link href="/Scripts/msgbox/css/msgbox.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/msgbox/js/msgbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            var Pid = $.getUrlParam("id");
            $(".iframe").attr("href", "add.aspx?id=" + Pid);
            $(".btnUpdate").click(function () {
                var self = $(this);
                var AdvertisementId = self.attr("AdvertisementId");
                var value = self.prev().val();
                if (isNaN(parseInt(value)) || parseInt(value) <= 0) {
                    ShowFailTip('请填写正确的顺序值');
                    return;
                }
                $.ajax({
                    url: ("SingleList.aspx?timestamp={0}").format(new Date().getTime()),
                    type: 'POST', dataType: 'json', timeout: 10000,
                    data: { Action: "UpdateSeqNum", Callback: "true", AdvId: AdvertisementId, UpdateValue: value },
                    success: function (resultData) {
                        if (resultData.STATUS == "SUCCESS") {
                            ShowSuccessTip('保存成功');
                            self.prev().val(value);
                        }
                        else {
                            ShowFailTip('服务器繁忙，请稍候再试！');
                        }
                    }
                });
            });
        });

        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="广告内容管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="管理广告位中要显示的广告内容" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="width: 500px; padding-left: 0px;">【<asp:Literal ID="litTitle" runat="server"></asp:Literal>】的所有广告内容
                    </li>
                </ul>
            </div>
        </div>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server">
                        <a class="iframe">添加</a> <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 70px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">批量删除</a><b>|</b></li>
                </ul>
            </div>
        </div>
        <Maticsoft:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="False" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="AdvertisementId">
            <Columns>
               <asp:TemplateField HeaderText="显示顺序" ItemStyle-Width="80" ItemStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("Sequence")%>' Width="30px"></asp:TextBox>
                                 <a href="javascript:;" AdvertisementId='<%#Eval("AdvertisementId") %>' class="btnUpdate">保存</a>
                            </ItemTemplate>
                        </asp:TemplateField>
                <asp:TemplateField HeaderText="广告内容" ItemStyle-HorizontalAlign="Center" SortExpression="AdvertisementId"
                    ItemStyle-Width="50">
                    <ItemTemplate>
                        <asp:Literal ID="litContent" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="广告名称" ItemStyle-HorizontalAlign="Left" SortExpression="AdvertisementName">
                    <ItemTemplate>
                        <%#Eval("AdvertisementName") %>
                        <asp:HiddenField ID="hfShowType" runat="server" Value='<%#Eval("ContentType") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="显示类型" ItemStyle-HorizontalAlign="Center" SortExpression="ContentType">
                    <ItemTemplate>
                        <%#ConvertContentType(Eval("ContentType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Impressions" HeaderText="显示频率" SortExpression="Impressions"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:BoundField DataField="CreatedDate" HeaderText="添加时间" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center" Visible="false" />
                <asp:TemplateField HeaderText="广告状态" ItemStyle-HorizontalAlign="Center" SortExpression="State">
                    <ItemTemplate>
                        <asp:Literal ID="litState" runat="server"></asp:Literal>
                    </ItemTemplate>
                </asp:TemplateField>
              
                <asp:TemplateField HeaderText="广告主" ItemStyle-HorizontalAlign="Center" SortExpression="EnterpriseID">
                    <ItemTemplate>
                        <%#GetEnName(Eval("EnterpriseID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="详细信息" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a class="modify" href='Show.aspx?id=<%#Eval("AdvertisementId") %>&Adid=<%=ADPositionId %>'>
                            详细信息</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="编辑" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <a class="modify" href='Modify.aspx?id=<%#Eval("AdvertisementId") %>&Adid=<%=ADPositionId %>'>
                            编辑</a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Site, btnDeleteText %>" SortExpression="AdvertisementId"
                    Visible="false" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </Maticsoft:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" OnClientClick='return confirm($(this).attr("ConfirmText"))'
                        ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
