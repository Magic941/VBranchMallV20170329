<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.ToDoInfo.Show" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
     <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="查看待办" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以查看待办详细内容" />
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
                                <asp:Literal ID="Literal3" runat="server" Text="待办编号" />：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlToDoID" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="执行人" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlToUserName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="创建人" />：
                            </td>
                            <td height="25">
                                <asp:Literal ID="ltlUserName" runat="server"  />
                            </td>
                        </tr>
                         
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="标题" />：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlTitle" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="内容" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlContent" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="发送类型" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlToType" runat="server" />
                            </td>
                        </tr>
                       
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="创建时间" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlCreatedDate" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                附件信息 ：
                            </td>
                            <td height="25">
                                <asp:Literal ID="litFile" runat="server"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="状态" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlStatus" runat="server"  />
                            </td>
                        </tr>
                         <tr style="display:none">
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="备注" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlRemark" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnReturn" runat="server" CausesValidation="false" Text="返回"
                                    OnClick="btnReturn_Click" class="adminsubmit_short" TabIndex="9"></asp:Button>
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
