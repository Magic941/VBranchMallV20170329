<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.Reports.Modify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
     <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="编辑简报" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以进行编辑简报办操作" />
                    </td>
                </tr>
            </table>
    </div>  
    <br />
     <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="用户名" />：
                            </td>
                            <td height="25">
                                <asp:Literal ID="ltlUserName" runat="server" />
                            </td>
                        </tr>
    
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="内容" />：
                            </td>
                            <td class="25">
                                <asp:TextBox ID="txtContent" TabIndex="3" runat="server" Width="196px" TextMode="MultiLine"
                                    ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="类型" />：
                            </td>
                            <td class="25">
                                <asp:DropDownList ID="dropType" runat="server" class="dropSelect" 
                                    TabIndex="6">
                                    <asp:ListItem Text="文字" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="图片" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="声音" Value="2" Selected="True" ></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="文件数据" />：
                            </td>
                            <td height="25">
                                 <asp:FileUpload ID="upFilePath" runat="server" Width="200px" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="备注" />：
                            </td>
                            <td class="25">
                                 <asp:TextBox ID="txtRemark" TabIndex="3" runat="server" Width="196px" TextMode="MultiLine"
                                    ></asp:TextBox>
                            </td>
                        </tr>
                        <asp:HiddenField ID="hdCreatedDate" runat="server" />
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                     class="adminsubmit_short" TabIndex="8" onclick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" TabIndex="9" onclick="btnCancle_Click"></asp:Button>
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
