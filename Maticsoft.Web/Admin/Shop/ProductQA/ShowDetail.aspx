<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
   CodeBehind="ShowDetail.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductQA.ShowDetail"  Title="疑问解答"%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="问题详细" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class" style=" vertical-align:top">
                                提问内容 ：
                            </td>
                            <td height="25">
                                 <asp:Label ID="lbQuestion" runat="server"  Width="760px"></asp:Label>
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
                                  <asp:Label ID="lbReply" runat="server" Width="760px"></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td class="td_class" >
                                回复者：
                            </td>
                            <td > 
                                  <asp:Label ID="lbReplyName" runat="server" ></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td class="td_class" >
                                回复时间：
                            </td>
                            <td > 
                                  <asp:Label ID="lbReplyDate" runat="server" ></asp:Label>
                            </td>
                        </tr>
                          <tr>
                            <td class="td_class">
                                状态 ：
                            </td>
                            <td height="25">
                              <asp:Label ID="lbState" runat="server" ></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="返回"
                                    class="adminsubmit_short" OnClick="btnReturn_Click"></asp:Button>
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



