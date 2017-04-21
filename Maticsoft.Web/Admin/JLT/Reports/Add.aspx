<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.Reports.Add" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
     <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="新增简报" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以进行新增简报办操作" />
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
                                <asp:Literal ID="Literal4" runat="server" Text="标题" />：
                            </td>
                            <td class="25">
                                <asp:TextBox ID="txtTitle" TabIndex="2" runat="server" Width="400" MaxLength="20"
                                    ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="内容" />：
                            </td>
                            <td class="25">
                                <asp:TextBox ID="txtContent" TabIndex="3" runat="server" Width="395" Height="250" TextMode="MultiLine"
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
                                    <asp:ListItem Text="文字" Value="0" Selected="True" ></asp:ListItem>
                                    <asp:ListItem Text="图片" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="声音" Value="2"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_class">
                                附件：
                            </td>
                            <td class="25">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                <asp:FileUpload ID="FileUpload2" runat="server" /><br/>
                                <asp:FileUpload ID="FileUpload3" runat="server" />
                                <asp:FileUpload ID="FileUpload4" runat="server" /><br/>
                                <asp:FileUpload ID="FileUpload5" runat="server" />
                                <asp:FileUpload ID="FileUpload6" runat="server" /><br/>
                                <asp:FileUpload ID="FileUpload7" runat="server" />
                                <asp:FileUpload ID="FileUpload8" runat="server" /><br/>
                                <asp:FileUpload ID="FileUpload9" runat="server" />
                                <asp:FileUpload ID="FileUpload10" runat="server" />
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="备注" />：
                            </td>
                            <td class="25">
                                 <asp:TextBox ID="txtRemark" TabIndex="3" runat="server" Width="395" Height="250" TextMode="MultiLine"
                                    ></asp:TextBox>
                            </td>
                        </tr>
                        

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
