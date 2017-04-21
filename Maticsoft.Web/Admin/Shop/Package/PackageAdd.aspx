<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="PackageAdd.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Package.PackageAdd" Title="增加包装" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="包装增加" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以根据您的需求添加相应的包装" />
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
                               名称 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtName" runat="server" Width="222px" MaxLength="50"></asp:TextBox>
                            </td>
                        </tr>
                        
                          <tr>
                            <td class="td_class">
                                所属类别 ：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="ddlCategory" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        
                        
                        <tr>
                            <td class="td_class">
                                描述 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDescription" runat="server" Width="223px" MaxLength="50" 
                                    Height="68px"></asp:TextBox>
                            </td>
                        </tr>
                        
                        
                        
                        <tr>
                            <td class="td_class">
                                 包装图 ：
                            </td>
                            <td height="25">
                                <asp:FileUpload ID="uploadPhoto" runat="server" Width="235px" />
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="uploadPhoto" runat="server" ErrorMessage="请选择正确的格式" ValidationExpression="^(([a-zA-Z]:)|(\\{2}\w+)\$?)(\\(\w[\w].*))(.jpg|.JPG|.gif|.GIF|.jpeg|.JPEG|.bmp|.BMP|.png|.PNG)$"></asp:RegularExpressionValidator>
                      
                            </td>
                        </tr>

                        <tr>
                            <td class="td_class" valign="top">
                                备注 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtRemark" runat="server" Width="219px" TextMode="MultiLine" 
                                    Height="59px"></asp:TextBox>
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
