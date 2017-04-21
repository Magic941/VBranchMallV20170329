<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
   CodeBehind="Reply.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductQA.Reply" Title="疑问解答" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="疑问解答" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以对这提问进行回答和审核 " />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                提问内容 ：
                            </td>
                            <td height="25">
                                 <asp:Label ID="lbQuestion" runat="server" ></asp:Label>
                                 </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                提问者 ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lbUserName" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                提问时间 ：
                            </td>
                            <td height="25">
                                 <asp:Label ID="lbCreatedDate" runat="server" ></asp:Label>
                            </td>
                        </tr>
                           <tr>
                            <td class="td_class" style=" vertical-align:top">
                                回复内容：
                            </td>
                            <td > 
                                <asp:TextBox ID="tReply" runat="server" Width="371px" TextMode="MultiLine" Rows="5" ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  Display="Dynamic" ControlToValidate="tReply" ErrorMessage="回复内容不能为空！"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                          <tr>
                            <td class="td_class">
                                是否通过 ：
                            </td>
                            <td height="25">
                                <asp:RadioButton ID="raTrue" runat="server"  Text="是"  GroupName="State"  Checked="true"/>
                                <asp:RadioButton ID="raFalse" runat="server" Text="否" GroupName="State"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>


