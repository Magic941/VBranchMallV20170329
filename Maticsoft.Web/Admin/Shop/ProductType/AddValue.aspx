<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="AddValue.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductType.AddValue"
    Title="商品类型管理" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <script type="text/javascript">
        var midValue;
        $(document).ready(function () {
            midValue = $.getUrlParam("m");
            if (midValue) {
                $("#oneValue").show();
                $("#anyValue").hide();
            } else {
                $("#oneValue").hide();
                $("#anyValue").show();
            }
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                       添加【<asp:Literal ID="Literal2" runat="server" Text="" />】扩展属性值
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="商品类型是一系列属性的组合，可以用来向客户展示某些商品具有的特有的属性。" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                      
                        <tr id="attributeValue">
                            <td class="td_class">
                                属性值 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAttributeValue" runat="server" Width="372px" onkeydown="javascript:this.value=this.value.replace('，',',')"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr id="anyValue">
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <label class="msgNormal" style="width: 200px">
                                    <asp:Literal ID="Literal1" runat="server" Text="扩展属性的值，多个属性值可用“，”号隔开，每个值最多15个字符！" /></label>
                            </td>
                        </tr>

                        
                        <tr id="oneValue">
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <label class="msgNormal" style="width: 200px">
                                    <asp:Literal ID="Literal4" runat="server" Text="扩展属性的值，字符数最多15个字符。" /></label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 15px;">
                            </td>
                            <td height="6">
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="保存" title="保存当前属性值" class="adminsubmit_short"  OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short"  OnClientClick="javascript:parent.$.colorbox.close();" title="返回列表页"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
