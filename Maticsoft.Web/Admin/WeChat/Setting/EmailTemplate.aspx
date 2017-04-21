<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="EmailTemplate.aspx.cs" Inherits="Maticsoft.Web.Admin.WeChat.Setting.EmailTemplate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="/admin/css/tab.css" rel="stylesheet" type="text/css" charset="utf-8" />
<script src="/admin/js/tab.js" type="text/javascript"></script>
<script type="text/javascript">
    window.UEDITOR_HOME_URL = "/ueditor/";
</script>
<script src="/ueditor/editor_config.js" type="text/javascript"></script>
<script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
<link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
<link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var codediv = $('#codediv').get(0);
    var codetypes = new Array(), codenames = new Array();
    //基础
    codetypes['Base'] = 'Domain,CreatedDate,UserName,Description';
    codenames['Base_Domain'] = '站点域名';
    codenames['Base_CreatedDate'] = '创建时间';
    codenames['Base_CreatedDate'] = '创建时间';
    codenames['Base_UserName'] = '用户帐号';
    codenames['Base_Description'] = '消息内容';


    $(function () {
        var obj = $(".nTab4").get(0);
        getcodetext(obj, "Base");
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
        _attachEvent($('#myTab1').get(0), 'mouseover', function () { codediv.style.display = 'none'; });
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
    <div class="newslist_title">
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td bgcolor="#FFFFFF" class="newstitle">
                    微信邮件模版设置管理
                </td>
            </tr>
            <tr>
                <td bgcolor="#FFFFFF" class="newstitlebody">
                    设置微信邮件模版的内容。
                </td>
            </tr>
        </table>
    </div>
  <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang"
            style="margin-bottom: 20px">
            <tr>
            
                <td width="100" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <span class="newstitle">邮件通知</span>
                </td>
            </tr>
            <tr>
            <td class="td_class">
                                  
                                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:CheckBox ID="chkNoMsg" runat="server" Text="系统默认消息邮件" />
                    <asp:CheckBox ID="chkSubscribe" runat="server" Text="用户关注消息邮件" />
                </td>
            </tr>
               <tr>
                <td class="td_class">
                                    接收邮件邮箱
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWCEmail" runat="server"></asp:TextBox>
                                    该邮箱用来接收默认消息邮件和用户关注消息邮件通知
                                </td>
            </tr>
        </table>
        <div class="nTab4">
            <div class="TabTitle">
                <ul id="myTab1">
                    <li class="active" onclick="nTabs(this,0);" id="tab0"><a href="javascript:;">关注时邮件</a></li>
                    <li class="normal" onclick="nTabs(this,1);" id="tab1"><a href="javascript:;">默认消息邮件</a></li>
                </ul>
            </div>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content0">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" 
                                style="margin-bottom: 20px">
                                <tr>
                                    <td class="td_class">
                                        模板主题：
                                    </td>
                                    <td height="35" bgcolor="#FFFFFF"  >
                                        <asp:TextBox ID="SubMsgTitle" runat="server"  Width="240"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        模板内容：
                                    </td>
                                    <td height="25">
                                        <asp:TextBox ID="SubMsgDesc" runat="server"  TextMode="MultiLine" Rows="8" Width="600"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="TabContent">
            <div id="myTab1_Content1" class="none4">
                <table style="width: 100%; border-top: none; border-bottom: none; padding-top: 10px"
                    cellpadding="2" cellspacing="1" class="border">
                    <tr>
                        <td class="tdbg">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0" 
                                style="margin-bottom: 20px">
                                <tr>
                                    <td class="td_class">
                                        模板主题：
                                    </td>
                                    <td height="25">
                                        <asp:textbox runat="server"  id="NoMsgTitle" style="width:240px"></asp:textbox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="td_class">
                                        模板内容：
                                    </td>
                                 
                                    <td height="25">
                                       <asp:TextBox ID="NoMsgDesc" runat="server" TextMode="MultiLine" Rows="8" Width="600"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="codediv" style="top: 400px; height: 100px; line-height: 32px; margin-top: -16px;
            overflow: hidden; padding: 10px 25px; left: 780px; width: 420px; position: absolute;">
            <p style="color: Gray; font-size: 14px">
                提示： 该类型模板可用标签代码。
            </p>
            <ul id="seocodes" style="width: 100%; height: 100px">
                <li style="float: left; margin-right: 5px;"><a href="javascript:void(0)">{subject}</a>
                    <span class="pipe">|</span> <a href="javascript:;">{forum}</a> </li>
            </ul>
        </div>
        <table style="width: 100%; border-top: none; float: left; padding-top: 20px; padding-bottom: 20px"
            cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                    </td>
                            <td height="25">
                            <asp:Button ID="btnSave" runat="server" Text="全部保存" class="adminsubmit" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({//实例化编辑器
            iframeCssUrl: '/ueditor/themes/default/iframe.css', toolbars: [
              ['fullscreen',
                'bold', 'underline', 'strikethrough', '|', 'removeformat', '|', 'forecolor', 'backcolor',
                '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|',
                'insertimage', 'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|', 'link', 'unlink', '|']
                 ],
            initialContent: '',
            minFrameHeight: 220,
            initialFrameHeight: 220,
            pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor.render($('[id$=SubMsgDesc]').get(0)); //将编译器渲染到容器

        var editor2 = new baidu.editor.ui.Editor({//实例化编辑器
            iframeCssUrl: '/ueditor/themes/default/iframe.css', toolbars: [
                ['fullscreen',
                'bold', 'underline', 'strikethrough', '|', 'removeformat', '|', 'forecolor', 'backcolor',
                '|', 'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|',
                'insertimage', 'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|', 'link', 'unlink', '|']
                 ],
            initialContent: '',
            minFrameHeight: 220,
            initialFrameHeight: 220,
            pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor2.render($('[id$=NoMsgDesc]').get(0)); //将编译器渲染到容器

        $(function () {
            $("#baidu_editor_1").css("height", "220px");
        });
    </script>
</div>
</asp:Content>

