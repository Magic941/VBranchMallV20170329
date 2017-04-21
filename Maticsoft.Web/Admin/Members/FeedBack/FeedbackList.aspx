<%@ Page Title="<%$ Resources:SysManage,ptFeedbackList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="FeedbackList.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.FeedBack.FeedbackList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link href="/Admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
    <script src="/Admin/js/tab.js" type="text/javascript"></script>
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
    <script type="text/javascript" >
        $(function () {
            var SelectedCss = "active";
            var NotSelectedCss = "normal";
            var type = $.getUrlParam("type");
            if (status == null) {
                status = 0;
            }
            $("a:[href='FeedbackList.aspx?type=" + type + "']").parents("li").removeClass(NotSelectedCss);
            $("a:[href='FeedbackList.aspx?type=" + type + "']").parents("li").addClass(SelectedCss);
            $("td:contains('未解决')").css("color", "red");
            $("td:contains('已解决')").css("color", "#006400");
        })
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage,lblFeedbackList%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage,lblCustomerFeedbackManage%>" />
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
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                       &nbsp;&nbsp;
                              <asp:DropDownList ID="DropType" runat="server">
                    </asp:DropDownList>
                       &nbsp;&nbsp;
                       <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
             
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <%-- <li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx"><asp:Literal ID="Literal4" runat="server" Text=""<%$Resources:Site,lblAdd%>/></a> <b>|</b> </li>--%>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;"  id="liDel"
                        runat="server"><a
                        href="javascript:;" onclick="GetDeleteM()">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:Site,btnDeleteListText%>" /></a><b>|</b></li>
                </ul>
            </div>
        </div>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                      <li class="normal"><a href="FeedbackList.aspx?type=0">
                        <asp:Literal ID="Literal8" runat="server" Text="未解决"></asp:Literal></a></li>
                       <li class="normal"><a href="FeedbackList.aspx?type=1" style="padding-top: 5px;">
                        <asp:Literal ID="Literal7" runat="server" Text="已解决"></asp:Literal></a></li>
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="10" ShowExportExcel="false" ShowExportWord="False" CellPadding="3"
            BorderWidth="1px" ShowCheckAll="true" DataKeyNames="FeedbackId">
            <Columns>
                <asp:BoundField DataField="FeedbackId" HeaderText="<%$Resources:Site,fieldFeedback_iID %>" SortExpression="FeedbackId"
                    ItemStyle-HorizontalAlign="Center" />
                       <asp:TemplateField HeaderText="反馈类型" ItemStyle-HorizontalAlign="center" SortExpression="TypeId">
                    <ItemTemplate>
                        <%# GetTypeName(Eval("TypeId"))%></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="UserName" HeaderText="<%$Resources:Site,fieldFeedback_cUserName %>" SortExpression="UserName"
                    ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField DataField="UserEmail" HeaderText="<%$Resources:Site,fieldEmail%>" SortExpression="UserEmail"
                    ItemStyle-HorizontalAlign="Center" />
                           <asp:BoundField DataField="UserSex" HeaderText="用户性别" SortExpression="UserSex"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="Phone" HeaderText="<%$Resources:Site,fieldTelphone%>" SortExpression="Phone"
                    ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="UserCompany" HeaderText="<%$Resources:Site,fieldCompany%>" SortExpression="UserCompany"
                    ItemStyle-HorizontalAlign="Center" />
           
                <asp:BoundField DataField="Description" HeaderText="反馈内容" SortExpression="Description"
                    ItemStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="CreatedDate" HeaderText="<%$Resources:Sysmanage,fieldFeedback_dateCreate%>"
                 SortExpression="CreatedDate" ItemStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="UserIP" HeaderText="IP"
                 SortExpression="UserIP" ItemStyle-HorizontalAlign="Center" />
                <asp:TemplateField HeaderText="<%$Resources:Sysmanage,fieldFeedback_bSolved%>" ItemStyle-HorizontalAlign="center">
                    <ItemTemplate>
                        <%# GetboolText(Eval("IsSolved").ToString())%></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Result" HeaderText="<%$Resources:Sysmanage,fieldFeedback_cResult%>" SortExpression="Result"
                    ItemStyle-HorizontalAlign="left" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnDetailText %>" ControlStyle-Width="50"
                    DataNavigateUrlFields="FeedbackId" DataNavigateUrlFormatString="ShowDetail.aspx?id={0}"
                    Text="<%$ Resources:Site, btnDetailText %>" ItemStyle-HorizontalAlign="Center" />
            </Columns>
           <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;"></td>
                <td></td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  class="adminsubmit" OnClick="btnDelete_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

