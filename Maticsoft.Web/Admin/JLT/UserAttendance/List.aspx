<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.UserAttendance.List" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {

        //绑定日期控件
        var today = new Date();
        var year = today.getFullYear();
        var month = today.getMonth();
        var day = today.getDate();
        $.datepicker.setDefaults($.datepicker.regional['zh-CN']);
        $("[id$='txtDate']").prop("readonly", true).datepicker({
            numberOfMonths: 1, //显示月份数量
            onClose: function () {
                $(this).css("color", "#000");
            }
        }).focus(function () { $(this).val(''); });
    });
    function GetDeleteM() {
        $("[id$='btnDelete']").click();
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
    <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="考勤管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以查看、批复考勤" />
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
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:Literal ID="Literal8" runat="server" Text="考勤类别" />：
                    <asp:DropDownList ID="DropTypeName" runat="server" class="dropSelect">
                    </asp:DropDownList>
                    <asp:Literal ID="Literal7" runat="server" Text="用户名" />：
                    <asp:TextBox ID="txtUserName" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Literal ID="Literal3" runat="server" Text="真实姓名" />:
                    <asp:TextBox ID="txtTrueName" runat="server" class="admininput_1"></asp:TextBox>
                    <%--<asp:Literal ID="Literal10" runat="server" Text="状态" />：
                    <asp:DropDownList ID="DropStatus" runat="server" class="dropSelect">
                    </asp:DropDownList>--%>
                    <asp:Literal ID="Literal9" runat="server" Text="创建日期" />：
                    <asp:TextBox ID="txtDate" runat="server" class="admininput_1" ></asp:TextBox>
                    <asp:Literal ID="Literal11" runat="server" Text="批复状态" />：
                    <asp:DropDownList ID="DropRevStatus" runat="server" class="dropSelect">
                    </asp:DropDownList>&nbsp 
                    <asp:Button ID="btnSearch" runat="server" 
                                Text="<%$ Resources:Site, btnSearchText %>" 
                                OnClick="btnSearch_Click"
                                class="adminsubmit_short">
                    </asp:Button>
                </td>

            </tr>
    </table>
    <br />
    <div class="newslist">
        <div class="newsicon">
            <ul>
                    <li style="background: url(/admin/images/list.gif) no-repeat"><a href="list.aspx">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, lblScan%>" /></a>
                    </li>
            </ul>
        </div>
    </div>
            <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="ID" ShowExportExcel="True" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="5" BorderWidth="1px" 
            ShowCheckAll="true">
            <Columns>
                <asp:TemplateField HeaderText="考勤编号" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                            <%# Eval("ID")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户名" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                            <%# Eval("UserName")%>
                    </ItemTemplate>
                </asp:TemplateField>
               
                <asp:TemplateField HeaderText="真实姓名" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%# GetTrueName( Eval("UserID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="考勤类型" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#GetTypeName(Eval("TypeID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="考勤时间" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%# Eval("CreatedDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#GetStatus( Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField HeaderText="批复状态" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#GetRevStatus( Eval("ReviewedStatus"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField HeaderText="考勤评分" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%# Eval("Score")%>
                    </ItemTemplate>
                </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="批复人" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%#GetUserName( Eval("ReviewedUserID"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="批复时间" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                            <%# Eval("ReviewedDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:HyperLinkField HeaderText="详细" ItemStyle-Width="50px"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Show.aspx?id={0}"
                    Text="详细" ItemStyle-HorizontalAlign="Center" />
                <asp:HyperLinkField HeaderText="批复" ItemStyle-Width="50px"
                DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Approval.aspx?id={0}"
                Text="批复" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="<%$Resources:Site,btnDeleteText%>"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm%>"
                            Text="<%$Resources:Site,btnDeleteText%>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns><FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr></cc1:GridViewEx></div><table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
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
                    <%--<asp:Literal ID="ltlToUser" runat="server" Text="状态："></asp:Literal>
                    <asp:DropDownList ID="drop_Status" runat="server">
                    <asp:ListItem Value="-1" Text="--请选择--" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="0" Text="无效"></asp:ListItem>
                    <asp:ListItem Value="1" Text="有效"></asp:ListItem></asp:DropDownList>&nbsp;&nbsp; --%>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                        class="adminsubmit" OnClick="btnDelete_Click" />
                    &nbsp;&nbsp;
                    <asp:Literal ID="Literal12" runat="server" Text="批复内容："></asp:Literal>
                    <asp:TextBox ID="txtRevDescription" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Literal ID="Literal10" runat="server" Text="备注："></asp:Literal>
                    <asp:TextBox ID="txtRemark" runat="server" class="admininput_1"></asp:TextBox>&nbsp;&nbsp; 
                  <%--  <asp:Literal ID="Literal13" runat="server" Text="考勤评分" />： 
                    <asp:TextBox ID="txtScore" runat="server" class="admininput_1" >
                    </asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                        ErrorMessage="请输入正整数" ControlToValidate="txtScore" 
                        ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>--%>
                    <asp:Button ID="btnBatch" runat="server" Text="批量批复" class="adminsubmit" OnClick="btnBatch_Click" />
                 </td>
            </tr>
        </table>
   </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
