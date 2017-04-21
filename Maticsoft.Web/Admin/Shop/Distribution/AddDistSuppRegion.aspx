
<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
  CodeBehind="AddDistSuppRegion.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Distribution.AddDistSuppRegion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript" charset="utf-8"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("[id$='ddlSupplier']").select2(); });
    </script>
</asp:Content>
<%@ Register Src="/Controls/Region.ascx" TagName="Region" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="分销商地区管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal6" runat="server" Text="您可以进行添加分销商地区操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="分销商名称" />：
                            </td>
                            <td height="25">
                              <asp:DropDownList ID="ddlSupplier" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="分销地区" />：
                            </td>
                            <td height="25">
                              <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <contenttemplate>
                                    <uc1:Region ID="RegionID" runat="server"  VisibleAll ="true"  VisibleAllText="--请选择--" />
                                    </contenttemplate>
                    </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>

