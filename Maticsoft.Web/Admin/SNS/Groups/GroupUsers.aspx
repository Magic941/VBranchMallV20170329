<%@ Page Title="小组人员表" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="GroupUsers.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.GroupUsers.List" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script type="text/javascript">
    $(function () {
        $(".recommand").click(function () {
            var userid = $(this).attr("userid");
            var groupid = $(this).attr("groupid");
            var ObjectThis = $(this);
            var recommand = 0;
            if ($(this).text() == "推荐位优秀成员") {
                recommand = 1;
            }
            $.ajax({
                url: "/SNSPGroupUser.aspx",
                type: 'post',
                data: { Action: "RecommandUser", UserID: userid, GroupID: groupid, recommand: recommand },
                dataType: 'json',
                success: function (resultData) {
                    switch (resultData.STATUS) {
                        case "OK":
                            if (recommand == 1) {
                                ObjectThis.text("取消推荐优秀成员");
                            } else {
                                ObjectThis.text("推荐位优秀成员");
                            }
                            break;
                        default:
                            break;
                    }
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    alert(xmlHttpRequest.responseText);
                }
            });

        })

        $(".SetAdmin").click(function () {
            var userid = $(this).attr("userid");
            var groupid = $(this).attr("groupid");
            var ObjectThis = $(this);
            var Role = 0;
            if ($(this).text() == "设为管理员") {
                Role = 1;
            }
            $.ajax({
                url: "/SNSPGroupUser.aspx",
                type: 'post',
                data: { Action: "SetAdmin", UserID: userid, GroupID:groupid,Role:Role},
                dataType: 'json',
                success: function (resultData) {
                    switch (resultData.STATUS) {
                        case "OK":
                            if (Role == 1) {
                                ObjectThis.text("取消管理员职位");
                            } else {
                                ObjectThis.text("设为管理员");
                            }
                            break;
                        default:
                            break;
                    }
                },
                error: function (xmlHttpRequest, textStatus, errorThrown) {
                    alert(xmlHttpRequest.responseText);
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
                        <asp:Literal ID="Literal1" runat="server" Text="小组人员表" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您对此小组的人员进行管理
                        <asp:Literal ID="Literal3" runat="server" Text="小组人员表" />
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
                        OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                   <%-- <li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx">
                        添加</a> <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat"><a href="javascript:;">删除</a><b>|</b></li>--%>
                </ul>
            </div>
        </div>
     
        <table style="width: 100%;" cellpadding="5" cellspacing="5" class="border" style="margin-left: 10;">
            <tr>
                <td>
                    <div id="gallery">
                        <asp:DataList ID="DataListUser" RepeatColumns="5" RepeatDirection="Horizontal" HorizontalAlign="Left"
                            runat="server" OnItemCommand="DataListUser_ItemCommand" OnItemDataBound="DataListUser_ItemDataBound" >
                            <ItemTemplate>
                                <table cellpadding="2" cellspacing="8">
                                    <tr>
                                        <td style="border: 1px solid #ecf4d3; text-align: center">
                                            <a class="group2" title='<%#Eval("NickName") %>'>
                                                <img src="/Upload/User/Gravatar/<%#Eval("UserID") %>.jpg" style="width: 90px;
                                                    height: 100px" />
                                            </a>
                                            <br />
                                              <asp:Label ID="lblImageName" runat="server" Text='<%#Eval("NickName") %>' onclick="ShowEdit(this)"></asp:Label>    <asp:CheckBox ID="ckUser" runat="server" />
                                              <br />
                                            <br />
                                            <asp:HiddenField runat="server" ID="hfUserId" Value='<%#Eval("UserID") %>'  />
                                           
                                            <a href="javascript:void(0)" class="recommand" userid='<%#Eval("UserID") %>' groupid='<%#Eval("GroupID") %>' style="color: #0063dc; text-decoration: none;"><%#(bool)Eval("IsRecommend") ? "取消推荐优秀成员" : "推荐位优秀成员"%></a>
                                           <asp:LinkButton ID="lbtnDel" runat="server"  CommandArgument='<%#Eval("UserID")+","+Eval("GroupID")%> ' Style="color: #0063dc;" CommandName="delete" OnClientClick='return confirm($(this).attr("ConfirmText"))'
                                                ConfirmText="您确定要删除此小组成员吗">
                                                <asp:Literal ID="Literal11" runat="server" Text="删除" /></asp:LinkButton><br />
                                                <a href="javascript:void(0)" class="SetAdmin" userid='<%#Eval("UserID") %>' groupid='<%#Eval("GroupID") %>' style="color: #0063dc; text-decoration: none;"><%#(int)Eval("Role")==1 ? "取消管理员职位" :"设为管理员"%></a></td></tr></table></ItemTemplate></asp:DataList></div></td></tr><tr>
                <td>
                    <webdiyer:AspNetPager runat="server" ID="AspNetPager1" CssClass="anpager" CurrentPageButtonClass="cpb"
                        OnPageChanged="AspNetPager1_PageChanged" PageSize="10" FirstPageText="<%$Resources:Site,FirstPage %>"
                        LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                        PrevPageText="<%$Resources:Site,GVTextPrevious %>">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>" CommandArgument="<%=groupid%>"
                        class="adminsubmit" OnClick="btnDelete_Click"  OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"/>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
