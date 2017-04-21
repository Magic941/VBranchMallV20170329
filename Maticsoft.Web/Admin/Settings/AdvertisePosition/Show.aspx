<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.AdvertisePosition.Show"
    Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="广告位信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以查看广告位信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_classshow"> 广告位编号 ： </td>
                            <td height="25">
                                <asp:Label ID="lblAdvPositionId" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow"> 广告位名称 ： </td>
                            <td height="25">
                                <asp:Label ID="lblAdvPositionName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_classshow">  显示类型：  </td>
                            <td height="25">
                                <asp:Label ID="lblShowType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="horizontalClass" runat="server">
                            <td class="td_classshow">  行显示个数 ： </td>
                            <td height="25">
                                <asp:Label ID="lblRepeatColumns" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr  id="verticalClass"  runat="server">
                            <td class="td_classshow">广告位大小 ：</td>
                            <td height="25">
                                <asp:Label ID="lblWidth" runat="server"></asp:Label><b style="vertical-align:middle">×</b><asp:Label ID="lblHeight" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr  id="codeClass" runat="server">
                            <td class="td_classshow">
                                广告位内容 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblAdvHtml" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <asp:Label ID="lblIsOne" runat="server"></asp:Label>
                                <asp:CheckBox ID="chkIsOne" Text="循环显示" runat="server" Enabled="false" />
                                <div style="margin-top: -22px; margin-left: 157px;" id="timeintervalClass" runat="server">
                                    <span style="vertical-align: middle">循环间隔：</span>
                                    <asp:Label ID="lblTimeInterval" runat="server"></asp:Label></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow"> 添加时间 ： </td>
                            <td height="25">
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">  添加人 ：</td>
                            <td height="25">
                                <asp:Label ID="lblCreatedUserID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"  OnClientClick="javascript:parent.$.colorbox.close();" ></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
