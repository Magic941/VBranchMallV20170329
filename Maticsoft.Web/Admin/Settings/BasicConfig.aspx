<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" 
CodeBehind="BasicConfig.aspx.cs" Inherits="Maticsoft.Web.Admin.Settings.BasicConfig" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
    <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal10" runat="server" Text="<%$ Resources:SysManage,ptBasicConfig%>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:SysManage,lblBasicConfig%>"/>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
           
            <tr>
                <td class="td_class">
                    <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage,lblForegroundLanguage%>" />
                    ：
                             
                </td>
                <td >
                    <asp:DropDownList ID="ForeLanguage" runat="server">
                        <asp:ListItem Value="Chinese"></asp:ListItem>
                        <asp:ListItem Value="English"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage,lblTimeZoneInformation %>" />
                    ：
                </td>
                <td>
                    <asp:DropDownList ID="TimeZone" runat="server">
                        <asp:ListItem Value="(GMT +08:00)北京，香港"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    <asp:Literal ID="Literal3" runat="server" Text="<%$ Resources:SysManage,lblTimeFormat %>" />
                    ：
                </td>
                <td>
                    <asp:DropDownList ID="TimeFormat" runat="server">
                        <asp:ListItem Value="24小时制"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    <asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:SysManage,lblDateFormat %>" />
                    ：
                </td>
                <td>
                    <asp:DropDownList ID="DateFormat" runat="server">
                        <asp:ListItem Value="Month-Day-Year(01-01-1900)"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Shop,ImageSizes %>" />
                    ：
                </td>

                <td>
                    <asp:TextBox ID="ImageSizes" runat="server" Width="400" Height="30" ></asp:TextBox>
                    (KB)
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Shop,ThumbImageWidth %>" />
                    ：
                </td>

                <td>
                    <asp:TextBox ID="ThumbImgWidth" runat="server" Width="400" Height="30" ></asp:TextBox>
                    (Pixel)
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:Shop,ThumbImageHeight %>" />
                    ：
                </td>

                <td>
                    <asp:TextBox ID="ThumbImgHeight" runat="server" Width="400" Height="30" ></asp:TextBox>
                    (Pixel)
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Shop,NormalImageWidth %>" />
                    ：
                </td>

                <td>
                    <asp:TextBox ID="NormalImgWidth" runat="server" Width="400" Height="30" ></asp:TextBox>
                    (Pixel)
                </td>
            </tr>
            <tr>
                <td class="td_class">
                    <asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:Shop,NormalImageHeight %>" />
                    ：
                </td>

                <td>
                    <asp:TextBox ID="NormalImgHeight" runat="server" Width="400" Height="30" ></asp:TextBox>
                    (Pixel)
                </td>
            </tr>
            <tr>
                <td class="td_class">
                </td>
                <td height="25">
                    <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                        class="adminsubmit_short" OnClick="btnSave_Click" ></asp:Button>
                    <asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancelText%>" 
                        class="adminsubmit_short" OnClick="btnReset_Click"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
