<%@ Page Title="<%$Resources:SysManage,ptFeedbackListShow%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="ShowDetail.aspx.cs" Inherits="Maticsoft.Web.Admin.Members.FeedBack.ShowDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SysManage,ptFeedbackListShow%>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对客户提交的意见反馈进行解决回复和查看。"/>
                    </td>
                     <td bgcolor="#FFFFFF" class="newstitlebody" style=" text-align:center; width:80px">
                     <a href="FeedbackList.aspx">返回</a>
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
                               <asp:Literal ID="Literal3" runat="server" Text="反馈类型"/>：
                            </td>
                            <td height="25">
                                <asp:Label ID="lbltypeName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                              <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,fieldFeedback_cUserName%>"/>：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,fieldEmail%>"/>：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblEmail" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal12" runat="server" Text="用户性别"/>：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblSex" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,fieldTelphone%>"/>：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblPhone" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,fieldCompany%>"/>：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCompany" runat="server"></asp:Label>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Sysmanage,fieldFeedback_cContent%>"/>：
                            </td>
                             <td width="560">
                                <asp:TextBox ID="lbltxtContent" runat="server" ReadOnly="true" TextMode="MultiLine"
                                    Rows="8" Width="560px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                              <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Sysmanage,fieldFeedback_dateCreate%>"/>：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblCreatedDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                IP ：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblUserIP" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:Sysmanage,fieldFeedback_bSolved%>"/>：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblIsSolved" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                               <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Sysmanage,fieldFeedback_cResult%>"/>：
                            </td>
                         <td height="25">
                                <asp:TextBox ID="txtResult" runat="server" Rows="8" TextMode="MultiLine"
                                    Width="560px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style=" display:none">
                            <td class="td_class">
                               <asp:Literal ID="Literal13" runat="server" Text="是否公开"/>：
                            </td>
                            <td height="25">
                                <asp:Label ID="lblStatus" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSolve" runat="server" Text="<%$Resources:Site,lblSolve%>" OnClick="btnSolve_Click" Width="57px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

