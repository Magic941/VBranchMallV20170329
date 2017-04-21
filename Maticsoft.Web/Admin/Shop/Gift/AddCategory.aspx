<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
   CodeBehind="AddCategory.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Gift.AddCategory" Title="添加礼品分类" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<%@ Register src="~/Controls/GiftCategoryDropList.ascx" tagname="GiftCategoryDropList" tagprefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="添加礼品分类" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="为不同类型的礼品创建不同的分类，方便您管理也方便顾客浏览 " />
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
                                分类名称 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" runat="server" Width="371px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  Display="Dynamic" ControlToValidate="txtName" ErrorMessage="分类名称不能为空！"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                父分类 ：
                            </td>
                            <td height="25">
                                <Maticsoft:GiftCategoryDropList ID="DropParentId" runat="server"  IsNull="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                分类描述 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDescription" runat="server" Width="371px" Height=" 80px"></asp:TextBox>
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

