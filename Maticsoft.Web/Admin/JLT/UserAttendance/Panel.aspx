<%@ Page Title="<%$Resources:CMS,ContentptList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Panel.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.UserAttendance.Panel" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function openMap(sender) {
            $.colorbox({ href: $(sender).attr("ref"), iframe: true, width: "800", height: "680", overlayClose: false });
        }
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
    
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="组织结构查看" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="可根据组织结构查看员工" />
                    </td>
                </tr>
            </table>
        </div>

    <table width="100%">
        <tr>
            <td valign="top" style="width: 10%; font-size: 14px;">
                <div style="border-style: solid; border-width: 1px; border-color: #5082bd; text-align: center;">
                    <iframe src="Left.aspx" name="LeftTree" frameborder="0" scrolling="no" allowtransparency="true"
                        style="overflow: hidden; height: 750px; width:200px;"></iframe>
                </div>
            </td>
            <td style="width: 75%; font-size: 14px;">
                <iframe src="UserInfo.aspx<%= (CurrentUser.UserType != "AA" ?"?did="+ CurrentUser.DepartmentID : "") %>" name="List" frameborder="0" scrolling="yes" id="mainFrame" name="mainFrame" 
                    allowtransparency="true" style="overflow-y: scroll; height: 750px; width: 100%;">
                </iframe>
            </td>
        </tr>
    </table>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
