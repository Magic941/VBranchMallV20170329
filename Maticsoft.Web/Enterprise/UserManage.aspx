<%@ Page Title="" Language="C#" MasterPageFile="~/Enterprise/Basic.Master" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="Maticsoft.Web.Enterprise.UserManage" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:content id="Content2" contentplaceholderid="ContentPlaceHolder1" runat="server">
    <!--是否是编辑-->
    <asp:hiddenfield id="hfIsEidt" runat="server" />
    <asp:hiddenfield id="hfUid" runat="server" />
    <div class="newslistabout">
        <div class="admintitle">
            <div class="sj" style="margin-right: 20px;">
                <img src="/images/icon6.gif" width="21" height="28" /></div>
            <strong id="TitleText">员工管理</strong>
        </div>
    </div>
    <!--添加信息-->
        <div class="newsadd_title" style="margin-bottom: 30px;">
        <div class="broder_b_o">
           <asp:label id="lblMsg" runat="server" forecolor="Red"></asp:label></div>
           <table style="width:100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                用户名：
                            </td>
                            <td>
                                <asp:textbox id="txtUserName" runat="server" class="member_input">
                    </asp:textbox>
                    <span>2-16个字符，用于商户登陆使用，建议使用字母和字符组合。</span> 
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                密码：
                            </td>
                            <td>
                                <asp:textbox id="txtPassword" runat="server" class="member_input" textmode="Password">
                    </asp:textbox>
                    <span>请输入6-16位字符和数字的组合。</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                姓名：
                            </td>
                            <td height="25">
                                <asp:textbox id="txtTrueName" runat="server" class="member_input">
                    </asp:textbox>
                    <span>请输入员工姓名</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                电话：
                            </td>
                            <td>
                                <asp:textbox id="txtPhone" runat="server" class="member_input">
                    </asp:textbox>
                    <span>请输入用户的电话信息。</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                电子邮箱：
                            </td>
                            <td style="height: 3px" height="3">
                                <asp:textbox id="txtEmail" runat="server" class="member_input">
                    </asp:textbox>
                    <span>请输入用户的电子邮箱。</span>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                员工状态：
                            </td>
                            <td height="25">
                                <asp:radiobutton id="rdoActive" runat="server" groupname="active" checked="true" />正常
                    <asp:radiobutton id="rdoNoActive" runat="server" groupname="active" />冻结 
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:button id="btnSave" runat="server" text="保存" class="adminsubmit" onclick="btnSave_Click" />
                <asp:button id="btnCancel" runat="server" text="取消修改" class="adminsubmit" onclick="btnCancel_Click" visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
           
    </div>
    <!--添加信息end-->
    <!-- 表格信息 -->
        <div class="newsadd_title">
            <div class="newslisttitle w749">
                <table width="100%" border="0" cellspacing="0" cellpadding="5" class="show">
                    <tr>
                        <td  width="10%" bgcolor="#FFFFFF">
                            用户名
                        </td>
                        <td  width="10%"  bgcolor="#FFFFFF">
                            姓名
                        </td>
                        <td  width="10%" bgcolor="#FFFFFF">
                            电话
                        </td>
                        <td  width="10%" bgcolor="#FFFFFF">
                            邮箱
                        </td>
                        <td  width="10%" bgcolor="#FFFFFF">
                            添加日期
                        </td>
                        <td  width="10%" bgcolor="#FFFFFF">
                            操作
                        </td>
                    </tr>
                    <asp:repeater id="Repeater1" runat="server" onitemcommand="Repeater1_ItemCommand">
                        <itemtemplate>
                        <tr>
                <td><%# Eval("UserName")%></td>
            <td><%# Eval("TrueName")%></td>
            <td><%# Eval("Phone")%></td>
            <td><%# Eval("Email")%></td>
            <td><%#DataBinder.Eval(Container.DataItem, "User_dateCreate", "{0:yyyy-MM-dd}")%></td>
            <td><asp:HiddenField ID="HiddenField_ID" Value='<%# Eval("UserID")%>' runat="server" />
            <asp:LinkButton  ID="btnEdit"  align="right" Text="编辑" style="color: #1317FC;"
                            CommandArgument="edit" CommandName='<%# Eval("UserID")%>' runat="server"></asp:LinkButton>
            <asp:LinkButton  ID="btnDel" Text="删除"  align="right" CommandArgument="delete" style="color: #1317FC;"
                            OnClientClick="return confirm('您确认删除吗?')"  runat="server" ></asp:LinkButton>
                        </td>
                    </tr>
            </itemtemplate>
                    </asp:repeater>
                </table>
            </div>
            <div class="abc">
                <table width="100%" border="0" cellspacing="0" cellpadding="5" class="news_123">
                    <tr>
                        <td>
                            <webdiyer:AspNetPager ID="AspNetPager1" runat="server" HorizontalAlign="Center" OnPageChanged="AspNetPager1_PageChanged" PageSize="12" Width="100%" FirstPageText="首页" NextPageText="下一页" PrevPageText="上一页" LastPageText="尾页" UrlPaging="True">
                            </webdiyer:AspNetPager>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    <!-- 表格信息end -->
</asp:content>
