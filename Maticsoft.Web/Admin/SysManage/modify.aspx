<%@ Page Title="<%$ Resources:SysManage, ptMenuModify%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="modify.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.modify" %>
    <%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
            $(".MLSet").colorbox({ iframe: true, width: "500", height: "280", overlayClose: false, href: "../MLShow.aspx?f=TreeText&k=<%=id%>" });
        });
    </script>
    <script language="javascript">
        function imgchang() {
            if (document.Form1.imgsel.selectedIndex != 0) {
                document.Form1.imgview.src =  document.Form1.imgsel.options[document.Form1.imgsel.selectedIndex].value;
                document.Form1.hideimgurl.value = document.Form1.imgsel.options[document.Form1.imgsel.selectedIndex].value;
            }
            else {
                document.Form1.imgview.src = '../Images/MenuImg/folder16.gif';
                document.Form1.hideimgurl.value = 'Images/MenuImg/folder16.gif';
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SysManage,ptMenuModify%>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:SysManage,lblModifyBackendMenu%>" />
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
                                ID:
                            </td>
                            <td height="25">
                                <asp:Label ID="lblID" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:SysManage, fieldNodeName%>" />:
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtTreeText" runat="server" Width="200px" MaxLength="20"></asp:TextBox>
                                <span id="btML" class="popbtn">
                                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:SysManage, lblAddMultiLang%>" Visible="false"/>
                                    <a class="MLSet" style="cursor:hand">[增加多语言数据]</a>
                                    </span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage, fieldParent%>" />:
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="listTarget" runat="server" Width="200px">
                                    <asp:ListItem Value="0" Selected="True">Root</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:Site, lblOrder%>" />:
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtOrderid" runat="server" MaxLength="5" Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:Site, lblPageUrl%>" />:
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtUrl" runat="server" Width="300px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:Site, lblIcon%>" />:
                            </td>
                            <td style="height: 3px" height="3">
                                <select id="imgsel" onchange="imgchang()" runat="server" name="imgsel">
                                    <option selected></option>
                                </select>
                                <img id="imgview" src="../Images/MenuImg/folder16.gif" border="0" runat="server" />
                                <input id="hideimgurl" style="width: 24px; height: 22px" type="hidden" size="1" runat="server"
                                       name="hideimgurl" value="Images/MenuImg/folder16.gif" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:SysManage, fieldNodeType%>" />:
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:DropDownList ID="drpTreeType" runat="server">
                                    <asp:ListItem Selected="True" Text="<%$ Resources:SysManage, dropBackendSystem%>" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:SysManage, dropBackendEnterprise%>" Value="1" ></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:SysManage, dropBackendAgent%>" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:SysManage, dropBackendUser%>" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="<%$ Resources:SysManage,dropBackendSupplier %>" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:CheckBox ID="chkEnable" Text="<%$ Resources:SysManage, fieldNodeEnable%>" Checked="True" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:SysManage, fieldPermission%>" />:
                            </td>
                            <td height="25">
                                <uc1:UCDroplistPermission ID="UCDroplistPermission1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:SysManage, fieldDescription%>" />:
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDescription" runat="server" Width="300px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td  height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" 
                                    Text="<%$ Resources:Site, btnCancleText %>"  class="adminsubmit_short" 
                                    onclick="btnCancle_Click">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
