<%@ Page Title="举报表" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Report.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Report.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $("span:contains('未处理')").css("color", "red");
            $("span:contains('举报属实已删除')").css("color", "#006400");
            $("span:contains('虚假举报已忽略')").css("color", "#006400");
            $("span:contains('举报内容核实中')").css("color", "#006400");
            $(".showjs").colorbox({ iframe: true, width: "720", height: "480", overlayClose: false });
        })
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
                        <asp:Literal ID="Literal1" runat="server" Text="举报管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以对举报信息进行管理" />
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
                <td style="width: 200px">
                    <asp:Literal ID="Literal5" runat="server" Text="关键字：" />
                    <asp:TextBox ID="txtKeyWord" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td style="width: 200px">
                    <asp:Literal ID="Literal4" runat="server" Text="举报人：" />
                    <asp:TextBox ID="txtName" runat="server" Width="100px"></asp:TextBox>
                </td>
                <td height="35" style="width: 100px">
                    检举内容类型：
                </td>
                <td height="35" style="width: 80px">
                    <asp:DropDownList ID="rdoType" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="0">动态</asp:ListItem>
                        <asp:ListItem Value="1">图片 </asp:ListItem>
                        <asp:ListItem Value="2">商品</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td height="35" style="width: 60px">
                    状态：
                </td>
                <td height="35" style="width: 80px">
                    <asp:DropDownList ID="rdostatus" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="-1" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="1">已处理</asp:ListItem>
                        <asp:ListItem Value="2">未处理</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td height="35" style="width: 100px">
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
                <td>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server"><a href="javascript:;" onclick="GetDeleteM()">
                            <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></a><b>|</b></li>
                 
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            OnRowCommand="gridView_RowCommand" Width="100%" PageSize="10" ShowExportExcel="False"
            ShowExportWord="False" ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px"
            ShowCheckAll="true" DataKeyNames="ID">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="编号" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40" />
                <asp:BoundField DataField="TargetID" HeaderText="编号" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="40" Visible="false" />
                <asp:BoundField DataField="CreatedUserID" HeaderText="检举人编号" SortExpression="CreatedUserID"
                    ItemStyle-HorizontalAlign="Left" Visible="false" />
                <asp:BoundField DataField="CreatedNickName" HeaderText="检举人" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="80" />
                <asp:BoundField DataField="TypeName" HeaderText="检举类型" ItemStyle-HorizontalAlign="Left"
                    ItemStyle-Width="80" />
                <asp:TemplateField HeaderText="类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                        <%# GetTargetType(Eval("TargetType"))%><asp:HiddenField ID="HiddenField_UserId" runat="server"
                            Value='<%#Eval("CreatedUserID") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="查看" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80">
                    <ItemTemplate>
                    <asp:HiddenField ID="HiddenField_PostId" runat="server"  Value='<%#Eval("TargetID") %>' />
                    <asp:HiddenField ID="HiddenField_ID" runat="server"  Value='<%#Eval("ID") %>' />
                    <asp:HiddenField ID="HiddenField_TagTypeId" runat="server"  Value='<%#Eval("TargetType") %>' />
                    <a class="showjs" href='Show.aspx?id=<%#Eval("ID")%>' >查看</a>
                  <%--      <asp:Literal ID="litShow" runat="server"></asp:Literal>--%>
                       <%-- <asp:LinkButton ID="lbtnDelPost" runat="server" CausesValidation="False" CommandName="DeleteByType"
                            CommandArgument='<%#Eval("TargetType")+","+Eval("TargetID")+","+Eval("ID")%>'
                            Text="删除"></asp:LinkButton>
                        <asp:LinkButton ID="lbtnID" runat="server" CausesValidation="False" CommandName="Status"
                            CommandArgument='<%#Eval("ID")+","+Eval("Status")%>' Style="color: #0063dc;"><span><%#Eval("Status").ToString() == "0" ? "处理" : "已处理"%></span></asp:LinkButton>--%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="处理状态" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="120">
                    <ItemTemplate>
                        <span><%#GetStatus(Eval("Status"))%></span>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Description" HeaderText="内容" ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="CreatedDate" HeaderText="举报时间" SortExpression="CreatedDate"
                    ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120" />
                <asp:TemplateField ItemStyle-Width="80" HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnDel" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
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
        <table border="0" cellpadding="0" cellspacing="1" style="width: 200px; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"/>
                </td>
                <td style="display:none;">
                    <asp:Button ID="btnAlreadyDone" runat="server" Text="批量处理" class="adminsubmit" OnClick="btnAlreadyDone_Click" />
                </td>
                <td><asp:Button ID="btnReportTrue" runat="server" Text="举报属实" class="adminsubmit"  ToolTip="举报属实，删除发布的内容"
                        onclick="btnReportTrue_Click"/></td>
                <td> <asp:Button ID="btnReportFalse" runat="server" Text="虚假举报" class="adminsubmit" ToolTip="忽略用户虚假举报"
                        onclick="btnReportFalse_Click" /></td>
                <td><asp:Button ID="btnReportUnKnow" runat="server" Text="核实中" class="adminsubmit"  ToolTip="举报内容待核实"
                        onclick="btnReportUnKnow_Click"  /></td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>