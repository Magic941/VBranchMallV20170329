<%@ Page Title="主题回复表" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="GroupTopicReply.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.GroupTopicReply.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
<script type="text/javascript" >
    $(function () {
        var SelectedCss = "active";
        var NotSelectedCss = "normal";
        var status = $.getUrlParam("status");
        var topicid = $.getUrlParam("topicid");
        if (status == null) {
            status = -1;
        }
        $("a:[href^='GroupTopicReply.aspx?status=" + status + "']").parents("li").removeClass(NotSelectedCss);
        $("a:[href^='GroupTopicReply.aspx?status=" + status + "']").parents("li").addClass(SelectedCss);
        $("td:contains('已审核')").css("color", "#006400");
        $("td:contains('未通过')").css("color", "red");
    })
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title"  style=" margin-bottom:25px">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                         <asp:Literal ID="Literal1" runat="server" Text="主题回复表" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以新增、修改、删除 <asp:Literal ID="Literal3" runat="server" Text="主题回复表" />
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
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                    OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
            </td>
        </tr>
    </table>
    <!--Search end-->
    <br />
    <div class="newslist">
        <div class="newsicon">
            <ul>
     <%--           <li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx">添加</a> <b>|</b> </li>
                <li style="background: url(/admin/images/delete.gif) no-repeat"><a href="javascript:;">删除</a><b>|</b></li>--%>
            </ul>
        </div>
    </div>

    <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="normal"><a href="GroupTopicReply.aspx?status=-1&topicid=<%=topicid %>" style="padding-top: 5px;">
                        <asp:Literal ID="Literal7" runat="server" Text="全部"></asp:Literal></a></li>
                    <li class="normal"><a href="GroupTopicReply.aspx?status=0&topicid=<%=topicid %>">
                        <asp:Literal ID="Literal8" runat="server" Text="未审核"></asp:Literal></a></li>
                    <li class="normal"><a href="GroupTopicReply.aspx?status=1&topicid=<%=topicid %>">
                        <asp:Literal ID="Literal9" runat="server" Text="已审核"></asp:Literal></a></li>
                    <li class="normal"><a href="GroupTopicReply.aspx?status=2&topicid=<%=topicid %>">
                        <asp:Literal ID="Literal11" runat="server" Text="未通过"></asp:Literal></a></li>
                </ul>
            </div>
        </div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
        ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
        Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
        CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="ReplyID">
        <columns>  
        	<asp:BoundField DataField="Description" HeaderText="内容" SortExpression="Description" ItemStyle-HorizontalAlign="Left" />  
            <asp:BoundField DataField="Title" HeaderText="对应的主题" SortExpression="Title" ItemStyle-HorizontalAlign="Left"  /> 
		<asp:BoundField DataField="GroupName" HeaderText="小组" SortExpression="GroupName" ItemStyle-HorizontalAlign="Left"  /> 
		<asp:BoundField DataField="ReplyNickName" HeaderText="发表者" SortExpression="ReplyNickName" ItemStyle-HorizontalAlign="Center"  /> 
		
	
	
	 <asp:TemplateField HeaderText="状态" SortExpression="Status" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%# GetStatus(Eval("Status"))%>
                    </ItemTemplate>
                </asp:TemplateField>
		<asp:BoundField DataField="CreatedDate" HeaderText="发表的日期" SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Center"  /> 
                           
                            
                            <asp:HyperLinkField Visible="false" HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50" DataNavigateUrlFields="TopicID" DataNavigateUrlFormatString="/Group/TopicReply?id={0}"
                                Text="<%$ Resources:Site, btnDetailText %>"  ItemStyle-HorizontalAlign="Center" Target="_blank" />
                            <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ControlStyle-Width="50" DataNavigateUrlFields="ReplyID" DataNavigateUrlFormatString="Modify.aspx?id={0}"
                                Text="<%$ Resources:Site, btnEditText %>"  ItemStyle-HorizontalAlign="Center" Visible="false" />
                            <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"   Visible="false"  ItemStyle-HorizontalAlign="Center" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                         Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </columns>
        <footerstyle height="25px" horizontalalign="Right" />
        <HeaderStyle Height="35px" />
        <pagerstyle height="25px" horizontalalign="Right" />
        <sorttip ascimg="~/Images/up.JPG" descimg="~/Images/down.JPG" />
            <HeaderStyle Height="35px" />

        <rowstyle height="40px" />
        <sortdirectionstr>DESC</sortdirectionstr>
    </cc1:GridViewEx>
    <table border="0" cellpadding="0" cellspacing="1" style="width:400px; height: 100%;">
        <tr>
            <td style="width: 1px;">
            </td>
            <td>
                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"  class="adminsubmit" OnClick="btnDelete_Click" OnClientClick="return confirm('您确定要这样做吗')" />
            </td>
            <td>
              <asp:Button ID="btnChecked" runat="server" Text="批量审核"  class="adminsubmit" OnClick="btnCheck_Click" OnClientClick="return confirm('您确定要这样做吗')"/>
            </td>
            <td>
              <asp:Button ID="btnCheckedUnpass" runat="server" Text="批量拒绝"  class="adminsubmit" OnClick="btnCheckedUnPass_Click"  OnClientClick="return confirm('您确定要这样做吗')"/>
            </td>
             <td>
              <asp:Button ID="btnForbidSpeak" runat="server" Text="批量发表者禁言"  class="adminsubmit" OnClick="btnForbidSpeak_Click" />
            </td>
              <td>
              <asp:Button ID="BtnBack" runat="server" Text="返回小组列表"  class="adminsubmit" OnClick="btnBack_Click" />
            </td>
            
        </tr>
    </table></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
