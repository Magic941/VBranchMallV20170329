<%@ Page Title="积分限制管理" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="AddLimit.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.Points.AddLimit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="添加积分限制" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行添加积分限制操作" />
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
                                <asp:Literal ID="Literal3" runat="server" Text="限制名称" />：
                            </td>
                            <td height="25" >
                                <asp:TextBox ID="tName" TabIndex="2" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="tName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="周期频率" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tCycle" TabIndex="3" runat="server" Width="120px" MaxLength="20"
                                    Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator1" Display="Dynamic" Type="Integer" ControlToValidate="tCycle"
                                    MinimumValue="1" MaximumValue="100" runat="server" ErrorMessage="数字在1-100之间"></asp:RangeValidator>
                                 &nbsp;&nbsp;  &nbsp;&nbsp; <asp:Literal ID="Literal5" runat="server" Text="周期单位" />：
                                <asp:DropDownList ID="DropCycleUnit" runat="server">
                                    <asp:ListItem Value="day">日</asp:ListItem>
                                    <asp:ListItem Value="month">月</asp:ListItem>
                                    <asp:ListItem Value="year">年</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="次数限制" />：
                            </td>
                            <td height="25" >
                                <asp:TextBox ID="tMaxTimes" TabIndex="3" runat="server" Width="120px" MaxLength="20"
                                    Text="1"></asp:TextBox>
                                <asp:RangeValidator ID="RangeValidator2" Type="Integer" Display="Dynamic" ControlToValidate="tMaxTimes"
                                    MinimumValue="1" MaximumValue="100" runat="server" ErrorMessage="数字在1-100之间"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25" >
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
