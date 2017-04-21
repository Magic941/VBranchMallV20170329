<%@ Page Title="缩略图尺寸管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
   CodeBehind="UpdateThumSize.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.ThumbnailSize.UpdateThumSize" %>
     <%@ Register TagPrefix="cc1" Namespace="Maticsoft.Web.Controls" Assembly="Maticsoft.Web" %>          

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="编辑缩略图尺寸" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行缩略图尺寸编辑操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="类型" />：
                            </td>
                            <td height="25">
                               <cc1:ConfigAreaList ID="ddlType" runat="server"  AutoPostBack="True"  OnSelectedIndexChanged="ddlType_Change"  />
                               模板名称：
                               <asp:DropDownList ID="ddlTheme" runat="server">
                              </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="尺寸名称" />：
                            </td>
                            <td height="25">
                       <asp:Literal ID="tName" runat="server" Text="" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="宽度" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tWidth" TabIndex="2" runat="server" Width="250px" MaxLength="20"
                                    Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator2" Display="Dynamic" Type="Integer" ControlToValidate="tWidth"
                                    MinimumValue="1" MaximumValue="10000" runat="server" ErrorMessage="数字在1-10000之间"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="高度" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tHeight" TabIndex="3" runat="server" Width="250px" MaxLength="20"
                                    Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" Type="Integer" ControlToValidate="tHeight"
                                    MinimumValue="1" MaximumValue="10000" runat="server" ErrorMessage="数字在1-10000之间"></asp:RangeValidator>
                            </td>
                        </tr>
                                <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal51" runat="server" Text="裁剪模式" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlThumMode" runat="server">
                                    <asp:ListItem Value="0">Auto自动缩放</asp:ListItem>
                                    <asp:ListItem Value="1">指定高宽裁减（不变形）</asp:ListItem>
                                    <asp:ListItem Value="2">指定高，宽按比例</asp:ListItem>
                                    <asp:ListItem Value="3">指定高宽缩放（可能变形）</asp:ListItem>
                                    <asp:ListItem Value="4">指定宽，高按比例</asp:ListItem>
                                    <%--   <asp:ListItem Value="3">Tao</asp:ListItem>
                                    <asp:ListItem Value="4">COM</asp:ListItem>--%>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="display: none">
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="是否加水印" />：
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkWatermark" runat="server" />是
                            </td>
                            
                        </tr>

                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="对应云存储尺寸名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tCloudSizeName"  runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="描述" />：
                            </td>
                            <td height="50">
                                <asp:TextBox ID="tDesc" runat="server" Width="250px" TextMode="MultiLine" Text=""></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit_short"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

