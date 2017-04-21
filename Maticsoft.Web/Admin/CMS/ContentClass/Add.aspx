<%@ Page Title="<%$Resources:CMS,CCptAdd%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.CMS.ContentClass.Add" %>

<%@ Register TagPrefix="Maticsoft" Namespace="Maticsoft.Web.Validator" Assembly="Maticsoft.Web.Validator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="/admin/js/validate/pagevalidator.css" type="text/css" />
    <script type="text/javascript" src="/admin/js/validate/pagevalidator.js"></script>
    <link href="/admin/js/jquery.uploadify/uploadify-v3.1/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/admin/js/jquery.uploadify/uploadify-v3.1/jquery.uploadify-3.1.min.js"
        type="text/javascript"></script>
    <link href="/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.css" rel="stylesheet"
        type="text/css" />
    <script src="/admin/js/jquery.uploadify/uploadify-v2.1.0/jquery.uploadify.v2.1.0.min.js"
        type="text/javascript"></script>
    <!--jQueryUploadify Start-->
    <script type="text/javascript">
        $(document).ready(function () {
            $("#uploadify").uploadify({
                'uploader': '/admin/js/jquery.uploadify/uploadify-v2.1.0/uploadify.swf',
                'script': '/CMSContent.aspx?action=uploadico',
                'cancelImg': '/admin/js/jquery.uploadify/uploadify-v2.1.0/cancel.png',
                'buttonImg': '/admin/images/uploadfile.png',
                'folder': 'UploadFile',
                'queueID': 'fileQueue',
                'auto': true,
                'multi': true,
                'fileExt': '*.jpg;*.gif;*.png;*.bmp',
                'fileDesc': 'Image Files (.JPG, .GIF, .PNG)',
                'queueSizeLimit': 1,
                'sizeLimit': 1024 * 1024 * 10,
                'onInit': function () {
                },

                'onSelect': function (e, queueID, fileObj) {
                },
                'onComplete': function (event, queueId, fileObj, response, data) {
                    var jsonData = eval("(" + response.split('|')[1] + ")");
                    switch (jsonData.Status) {
                        case "OK":
                            //将获取的值放在隐藏隐藏域中，供后台调用
                            $("#imgURL").attr("src", jsonData.SavePath);
                            $("[id$='HiddenField_ICOPath']").val(jsonData.SavePath);
                            break;
                        case "Failed":
                            alert(jsonData.ErrorMessage);
                            break;
                    }
                }
            });
        });
    </script>
    <!--jQueryUploadify End-->
    <!--Select2 Start-->
    <link href="/Admin/js/select2-2.1/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/select2-2.1/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () { $("[id$='dropParentID']").select2(); });
    </script>
    <!--Select2 End-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,CCptAdd %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:CMS,CClblAdd %>" />
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
                                <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,CClassType %>" />：
                            </td>
                            <td>
                                <asp:DropDownList ID="dropClassTypeID" runat="server" Width="371px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMS,CContentModel %>" />：
                            </td>
                            <td>
                                <asp:RadioButtonList ID="radlClassModel" runat="server" RepeatDirection="Horizontal"
                                    align="left">
                                    <asp:ListItem Value="3" Text="<%$Resources:CMS,CCArticleList %>" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="<%$Resources:CMS,CCSingleArticle %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:CMS,ContentlblClassName %>" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtClassName" runat="server" Width="371px" MaxLength="25"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <div id="txtClassNameTip" runat="server">
                                </div>
                                <Maticsoft:ValidateTarget ID="ValidateTargetName" runat="server" Description="栏目名称不能为空，长度限制在10个字符以内！"
                                    OkMessage="输入正确！" ControlToValidate="txtClassName" ContainerId="ValidatorContainer">
                                    <Validators>
                                        <Maticsoft:InputStringClientValidator ErrorMessage="请输入栏目名称，长度限制在10个字符以内！" LowerBound="1"
                                            UpperBound="10" />
                                    </Validators>
                                </Maticsoft:ValidateTarget>
                                <br />
                            </td>
                        </tr>
                        
                        <%--<tr> 
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <div id="txt" runat="server">
                                </div>
                                <Maticsoft:ValidateTarget ID="ValidateTarget1" runat="server" Description="栏目名称不能为空，长度限制在10个字符以内！"
                                    OkMessage="输入正确！" ControlToValidate="txtClassName" ContainerId="ValidatorContainer">
                                    <Validators>
                                        <Maticsoft:InputStringClientValidator ErrorMessage="请输入栏目名称，长度限制在10个字符以内！" LowerBound="1"
                                            UpperBound="10" />
                                    </Validators>
                                </Maticsoft:ValidateTarget>
                                <br />
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:CMS,CCFieldDescription %>" />：
                            </td>
                            <td>
                                <asp:TextBox ID="txtDescription" runat="server" Width="371px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" style="margin-top: 10px;">
                                <%--<asp:Label ID="lblImageUrl" runat="server" Text="缩略图 ："></asp:Label>--%>
                                <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,lblThumbnails %>" />:
                            </td>
                            <td height="25">
                                <asp:HiddenField ID="HiddenField_ICOPath" runat="server" />
                                <div id="fileQueue">
                                </div>
                                <input type="file" name="uploadify" id="uploadify" />
                                <div class="msgNormal" style="margin-left: 100px; position: relative; margin-top: -32px;">
                                    <asp:Literal ID="Literal15" runat="server" Text="请将图片大小控制在200K以内！"></asp:Literal>
                                </div>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <img id="imgURL" style="width: 120px; height: 120px;" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:CMS,CCTheParent %>" />：
                            </td>
                            <td height="25">
                                <asp:DropDownList ID="dropParentID" runat="server" Width="371px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:CMS,ContentKeywordList %>" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtKeywords" runat="server" Width="365px" MaxLength="25"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <div class="msgNormal">
                                    <asp:Literal ID="lblHelpSecret" runat="server" Text="请输入关键字以逗号分割，长度限制在25个字符以内！"></asp:Literal>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal16" runat="server" Text="静态目录" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtIndexChar" runat="server" Width="371px" MaxLength="25"></asp:TextBox>
                             <%--  <asp:CheckBox ID="chkIndexChar" runat="server" Text="拼音" />--%>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="<%$Resources:CMS,CCTemplateName %>" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtPageModelName" runat="server" Width="371px" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="display: none;">
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:CMS,CCOrder %>" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtOrders" runat="server" Width="30px" Text="1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="<%$Resources:Site,ErrorNotNull %>"
                                    ControlToValidate="txtOrders" Display="Dynamic"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOrders"
                                    ErrorMessage="<%$Resources:CMS,ContentErrorInputInt %>" ValidationExpression="^[+-]?\d+$"
                                    Display="Dynamic"></asp:RegularExpressionValidator>
                                <div style="margin-top: -24px; margin-left: 165px;">
                                    <asp:Literal ID="Literal12" runat="server" Text="<%$Resources:CMS,CColumnIndex %>" />：<asp:TextBox
                                        ID="txtClassIndex" runat="server" Width="30px" Text="0" MaxLength="25"></asp:TextBox></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal13" runat="server" Text="<%$Resources:Site,State %>" />：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left">
                                    <asp:ListItem Value="0" Text="<%$Resources:Site,Approved %>" Selected="True"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="<%$Resources:Site,Draft %>"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="<%$Resources:Site,PendingReview %>"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:CheckBox ID="chkAllowSubclass" runat="server" Text="<%$Resources:CMS,CCAddSubclass %>"
                                    Checked="true" />
                                <div style="margin-top: -22px; margin-left: 120px;">
                                    <asp:CheckBox ID="chkAllowAddContent" runat="server" Text="<%$Resources:CMS,CCAddContent %>"
                                        Checked="true" /></div>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="<%$Resources:Site,remark %>" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtRemark" runat="server" Width="371px" TextMode="MultiLine" MaxLength="100"
                                    class="addinput"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" OnClientClick="return PageIsValid();" class="adminsubmit_short">
                                </asp:Button>
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$Resources:Site,btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <Maticsoft:ValidatorContainer runat="server" ID="ValidatorContainer" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
