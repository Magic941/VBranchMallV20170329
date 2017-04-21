

<%@ Page Title="邮件模板管理" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true"   CodeBehind="UpdateTemplet.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.EmailTemplet.UpdateTemplet" %>

    
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                         <asp:Literal ID="Literal1" runat="server" Text="编辑邮件模板" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                         <asp:Literal ID="Literal6" runat="server" Text="您可以进行编辑邮件模板操作" />
                    </td>
                </tr>
            </table>
        </div>
                <div id="codediv" style="display: none; top: 707px;
            height: 100px; line-height: 32px; margin-top: -16px; overflow: hidden; padding: 10px 25px;
            position: absolute; left: 605px; width: 480px;">
            <p style=" color:Gray; font-size:14px">
               提示： 该类型模板可用标签代码。
            </p>
            <ul id="seocodes" style="width: 100%; height:100px">
                <li style="float: left; margin-right: 5px;"><a 
                    href="javascript:void(0)">{subject}</a> <span class="pipe">|</span> <a 
                        href="javascript:;">{forum}</a> </li>
            </ul>
        </div>
        <script type="text/javascript">
            $(function () {
                var type = $("#ctl00_ContentPlaceHolder1_ddlType").val();
                getcodetext(document.getElementById("ctl00_ContentPlaceHolder1_ddlType"), type);
                $("#ctl00_ContentPlaceHolder1_ddlType").change(function () {
                    var type2 = $("#ctl00_ContentPlaceHolder1_ddlType").val();
                    getcodetext(this, type2);
                });
            })
        </script>
        <script  type="text/javascript">
            var codediv = $('#codediv').get(0);
            var codetypes = new Array(), codenames = new Array();
            //基础
            codetypes['Base'] = 'Domain,CreatedDate';
            codenames['Base_Domain'] = '站点域名';
            codenames['Base_CreatedDate'] = '创建时间';

            //注册
            codetypes['Register'] = 'Domain,CreatedDate,UserName,SecretKey';
            codenames['Register_Domain'] = '站点域名';
            codenames['Register_CreatedDate'] = '创建时间';
            codenames['Register_UserName'] = '用户名';
            codenames['Register_SecretKey'] = '注册激活码';

            //找回密码
            codetypes['FindPwd'] = 'Domain,CreatedDate,UserName,SecretKey';
            codenames['FindPwd_Domain'] = '站点域名';
            codenames['FindPwd_CreatedDate'] = '创建时间';
            codenames['FindPwd_UserName'] = '用户名';
            codenames['FindPwd_SecretKey'] = '激活码';

            //意见与反馈
            codetypes['Feedback'] = 'Domain,CreatedDate,UserName,Question,ReplyResult,QuestionDate';
            codenames['Feedback_Domain'] = '站点域名';
            codenames['Feedback_CreatedDate'] = '创建时间';
            codenames['Feedback_UserName'] = '用户名';
            codenames['Feedback_Question'] = '用户反馈的问题';
            codenames['Feedback_ReplyResult'] = '回复结果';
            codenames['Feedback_QuestionDate'] = '用户反馈时间';

            //留言
            codetypes['GuestBook'] = 'Domain,CreatedDate,UserName,Description,ReplyResult,QuestionDate';
            codenames['GuestBook_Domain'] = '站点域名';
            codenames['GuestBook_CreatedDate'] = '创建时间';
            codenames['GuestBook_UserName'] = '用户名';
            codenames['GuestBook_Description'] = '用户留言内容';
            codenames['GuestBook_ReplyResult'] = '回复结果';
            codenames['GuestBook_QuestionDate'] = '用户留言时间';

            $(function () {
                //$('.TabContent').unbind('mouseover').bind('mouseover', function () { codediv.style.display = 'none'; });
            });
            function getcodetext(obj, ctype) {
                var top_offset = obj.offsetTop;
                var codecontent = '';
                var targetid = obj.id;
                while ((obj = obj.offsetParent).tagName != 'BODY') {
                    top_offset += obj.offsetTop;
                }
                if (!codetypes[ctype]) {
                    return true;
                }
                types = codetypes[ctype].split(',');
                for (var i = 0; i < types.length; i++) {
                    if (codecontent != '') {
                        codecontent += '&nbsp;&nbsp;';
                    }
                    codecontent += '<li style="float: left;margin-right: 5px;"><a  href="javascript:;" title="' + codenames[ctype + '_' + types[i]] + '">{' + types[i] + '}</a></li>';
                }
                $('#seocodes').get(0).innerHTML = codecontent;
                codediv.style.top = top_offset + 'px';
                codediv.style.display = '';
             //   _attachEvent($('#myTab1').get(0), 'mouseover', function () { codediv.style.display = 'none'; });
            }
            function _attachEvent(obj, evt, func, eventobj) {
                eventobj = !eventobj ? obj : eventobj;
                if (obj.addEventListener) {
                    obj.addEventListener(evt, func, false);
                } else if (eventobj.attachEvent) {
                    obj.attachEvent('on' + evt, func);
                }
            } function isUndefined(variable) {
                return typeof variable == 'undefined' ? true : false;
            }
            function insertContent(target, text) {
                var obj = $("#" + target).get(0);
                selection = document.selection;
                checkFocus(target);
                if (!isUndefined(obj.selectionStart)) {
                    var opn = obj.selectionStart + 0;
                    obj.value = obj.value.substr(0, obj.selectionStart) + text + obj.value.substr(obj.selectionEnd);
                } else if (selection && selection.createRange) {
                    var sel = selection.createRange();
                    sel.text = text;
                    sel.moveStart('character', -strlen(text));
                } else {
                    obj.value += text;
                }
            }
            function checkFocus(target) {
                var obj = $("#" + target).get(0);
                if (!obj.hasfocus) {
                    obj.focus();
                }
            }
        </script>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="模板类型" />：
                            </td>
                         <td height="25">
                                <asp:DropDownList ID="ddlType" runat="server" Enabled="false">
                                    <asp:ListItem Value="Base">请选择模板类型</asp:ListItem>
                                    <asp:ListItem Value="Register">注册激活邮件</asp:ListItem>
                                    <asp:ListItem Value="FindPwd">找回密码邮件</asp:ListItem>
                                    <asp:ListItem Value="Feedback">意见与反馈邮件</asp:ListItem>
                                    <asp:ListItem Value="GuestBook">留言反馈邮件</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal2" runat="server" Text="模板主题" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtSubject" TabIndex="2" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="模板描述" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtDescription" TabIndex="2" runat="server" Width="360px" MaxLength="20" TextMode="MultiLine" Rows="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="模板内容" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtBody" TabIndex="3" runat="server" Width="600px" Height="1000px" TextMode="MultiLine"  ></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit"></asp:Button>
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
     <%--<script type="text/javascript">
         var editor = new baidu.editor.ui.Editor({//实例化编辑器
             iframeCssUrl: 'ueditor/themes/default/iframe.css', toolbars: [

            ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                'bold', 'italic', 'underline', 'strikethrough', '|', 'forecolor', 'backcolor', '|',
                             'superscript', 'subscript', '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|', 'indent', '|', 'removeformat', 'formatmatch', 'autotypeset', '|', 'pasteplain', '|', 'customstyle',
                'paragraph', '|', 'rowspacingtop', 'rowspacingbottom', 'lineheight', '|', 'fontfamily', 'fontsize', '|', 'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|', 'insertimage', 'insertvideo', 'map', 'horizontal', '|',
                'link', 'unlink', '|', 'inserttable', 'deletetable', 'insertparagraphbeforetable', 'insertrow', 'deleterow', 'insertcol', 'deletecol', 'mergecells', 'mergeright', 'mergedown', 'splittocells', 'splittorows', 'splittocols']
                 ],
             initialContent: '',
             minFrameHeight: 420,
             autoHeightEnabled: false,
             pasteplain: false,
         wordCount: false
          , elementPathEnabled: false
         });
         editor.render('ctl00_ContentPlaceHolder1_txtBody'); //将编译器渲染到容器
    </script>--%>
</asp:Content>
 
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>







