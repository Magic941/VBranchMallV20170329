<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="Approval.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.UserAttendance.Approval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
     <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="考勤批复" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以进行批复考勤操作" />
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
                                <asp:Literal ID="Literal3" runat="server" Text="考勤编号" />：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlAttID" runat="server"/>
                            </td>
                        </tr>
                       <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="用户名" />：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlUserName" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="真实姓名" />：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlTrueName" runat="server"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="考勤日期" />：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlAttDate" runat="server"/>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="考勤评分" />：
                            </td>
                            <td class="25">
                               <asp:TextBox ID="txtScore" runat="server" class="admininput_1"></asp:TextBox>
                               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                               ErrorMessage="请输入正整数" ControlToValidate="txtScore" 
                                    ValidationExpression="^[0-9]*$"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                       <tr style="display:none">
                            <td class="td_class">
                                 <asp:Literal ID="Literal8" runat="server" Text="状态"/>：
                            </td>
                            <td class="25">
                                <asp:DropDownList ID="drop_Status" runat="server">
                                <asp:ListItem Value="-1" Text="--请选择--" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="0" Text="无效"></asp:ListItem>
                                <asp:ListItem Value="1" Text="有效"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                       </tr>
                        <tr>
                            <td class="td_class">
                                 <asp:Literal ID="Literal9" runat="server" Text="批复状态"/>：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlRevStatus" runat="server" />
                            </td>
                       </tr>
                    
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="批复内容" />：
                            </td>
                            <td class="25">
                               <asp:TextBox ID="txtRevDescription" runat="server" class="admininput_1" TextMode="MultiLine" Width="196px" Height="80px"></asp:TextBox>
                            </td>
                        </tr>
                     
                         <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="备注" />：
                            </td>
                            <td class="25">
                               <asp:TextBox ID="txtRemark" runat="server" class="admininput_1" TextMode="MultiLine" Width="196px" Height="80px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="批复"
                                    OnClick="btnSave_Click" class="adminsubmit_short" TabIndex="8"></asp:Button>
                                <asp:Button ID="btnReturn" runat="server" Text="返回"
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
