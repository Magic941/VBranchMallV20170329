<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.CMS.ClassType.Add" Title="<%$Resources:CMS,ClassptCmsClass%>"
    ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {
//            $("[id$='btnSave']").click(function () {
//                $.ajax({
//                    url: "CMSContent.aspx",
//                    type: 'post', dataType: 'text', timeout: 10000, async: false,
//                    data: { action: "Add", ClassTypeName: $("[id$='txtClassTypeName']").val() },
//                    success: function (resultData) {
//                        if (resultData == "SUCCESS") {
//                            parent.location.reload();
//                            window.close();
//                        }
//                        else {
//                            alert("<%=Resources.CMS.ClassErrorAddFailed%>");
//                            return false;
//                        }
//                    }
//                });
//            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,ClassptCmsClass%>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                       <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMS,ClasslblCmsClass%>" />
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
                               <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,ClasslblClassName%>" />£º
                            </td>
                            <td>
                                <asp:TextBox ID="txtClassTypeName" runat="server" Width="350px" class="addinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" Text="<%$ Resources:Site, btnSaveText %>"
                                     class="adminsubmit_short" ></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClientClick="javascript:parent.$.fancybox.close();" OnClick="btnCancle_Click" class="adminsubmit_short" ></asp:Button><%--OnClientClick="javascript:parent.$.fancybox.close();"--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
