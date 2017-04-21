<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Step2_1.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.ProductType.Step2_1"
    Title="商品类型管理" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("[id$='rbInput']").click(function () {
                $("#attributeValue").hide();
                $("[id$='txtAttributeValue']").val("")
                $("#attributeValueTip").hide();
            });
            $("[id$='rbOne']").click(function () {
                $("#attributeValue").show();
                $("[id$='txtAttributeValue']").val("")
                $("#attributeValueTip").show();
            });
            $("[id$='rbAny']").click(function () {
                $("#attributeValue").show();
                $("[id$='txtAttributeValue']").val("")
                $("#attributeValueTip").show();
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="添加扩展属性" />
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
                        <tr>
                            <td class="td_class">
                                属性名称 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAttributeName" runat="server" Width="372px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <div id="txtAttributeNameTip" runat="server">
                                </div>
                                <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server"  OkMessage="输入正确！"  Description="属性名称不能为空，长度限制在1-15个字符之间！"
                                    ControlToValidate="txtAttributeName" ContainerId="ValidatorContainer">
                                    <Validators>
                                        <Maticsoft:InputStringClientValidator ErrorMessage="属性名称不能为空，长度限制在1-15个字符之间！" LowerBound="1"
                                            UpperBound="15" />
                                    </Validators>
                                </Maticsoft:ValidateTarget>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="vertical-align: top;">
                                展示方式 ：
                            </td>
                            <td height="25">
                                <asp:RadioButton ID="rbOne" runat="server" GroupName="ShowType" Checked="true" />单选
                                <asp:RadioButton ID="rbAny" runat="server" GroupName="ShowType" />多选
                                <asp:RadioButton ID="rbInput" runat="server" GroupName="ShowType" />直接填写
                            </td>
                        </tr>
                        <tr id="attributeValue">
                            <td class="td_class">
                                属性值 ：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtAttributeValue" runat="server" Width="372px" onkeydown="javascript:this.value=this.value.replace('，',',')"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id='attributeValueTip'>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <label class="msgNormal" style="width: 200px">
                                    <asp:Literal ID="Literal4" runat="server" Text="扩展属性的值，多个属性值可用“，”号隔开，每个值最多15个字符！" /></label>
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
                                <asp:Button ID="btnSave" runat="server" Text="保存" title="保存当前属性值" class="adminsubmit_short"
                                    OnClientClick="return PageIsValid();" OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short"  OnClientClick="javascript:parent.$.colorbox.close();" title="返回列表页" Visible="false"></asp:Button>
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
