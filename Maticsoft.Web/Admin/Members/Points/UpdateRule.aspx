<%@ Page Title="积分规则管理" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="UpdateRule.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.Points.UpdateRule" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                         <asp:Literal ID="Literal1" runat="server" Text="修改积分规则" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                         <asp:Literal ID="Literal6" runat="server" Text="您可以进行修改积分规则操作" />
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
                                <asp:Literal ID="Literal2" runat="server" Text="对应操作" />：
                            </td>
                            <td height="25">
                              <asp:DropDownList ID="ddlAction" runat="server" Enabled="false">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="规则名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tName" TabIndex="2" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="tName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="影响积分数" />：
                            </td>
                            <td height="25">
                               
                                <asp:TextBox ID="tScore" TabIndex="3" runat="server" Width="250px" MaxLength="20" Text="0"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" Type="Integer" Display="Dynamic" ControlToValidate="tScore" MinimumValue="0" MaximumValue="1000" runat="server" ErrorMessage="数字在0-1000之间"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                        
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="限制条件" />：
                            </td>
                            <td height="25">
                             <asp:DropDownList ID="DropLimitName" runat="server">
                            </asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="规则描述" />：
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
                                    OnClick="btnSave_Click" class="adminsubmit"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit"></asp:Button>
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


