<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="SendEmail.aspx.cs" Inherits="Maticsoft.Web.Admin.Accounts.SendEmail"
    Title="<%$ Resources:SysManage,ptGroupEmail%>" ValidateRequest="false" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Service/jquery.autocomplete.css" rel="stylesheet" type="text/css" />
    <script src="../Service/jquery.autocomplete.js" type="text/javascript"></script>
    <link href="../js/msgbox/css/msgbox.css" rel="stylesheet" type="text/css" />
    <script src="../js/msgbox/script/msgbox.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ctl00_ContentPlaceHolder1_txtKeyword").autocompleteEnte("/Admin/Service/GetData.asmx/GetUserName", {
                httpMethod: "POST",                                            //ʹ��POST����WebService
                dataType: 'json',                                                     //������������ΪXML
                minchar: 0,                                                           //��С��Ӧ�ַ�����
                selectFirst: false,                                                   //Ĭ�ϲ�ѡ�е�1��
                max: 12,                                                              //�б��������Ŀ
                minChars: 1,                                                        //�Զ���ɼ���֮ǰ�������С�ַ�
                width: 150,                                                         //��ʾ�Ŀ�ȣ��������
                scrollHeight: 300,                                               //��ʾ�ĸ߶ȣ������ʾ������
                matchContains: true,                                          //����ƥ�䣬����data����������ݣ��Ƿ�ֻҪ�����ı���������ݾ���ʾ
                autoFill: false,                                                     //�Զ����
                //��ʽ��ѡ��,����WebService���ص�������JSON��ʽ,����Ҫת��HTML��TABLE��ʽ��ʾ
                formatItem: function (result, i, max) {
                    //���ﷵ�ص�resultΪһ���ѷ�װ�õ�JSON����
                    //iΪ�ڼ������ݣ���1��ʼ��maxΪ������������
                    //���ز�����Ҳ����ֻ��һ������result
                    var item = "<table id='auto" + i + "'><tr><td align='left'>" + result.name + "</td></tr></table>";
                    return item;
                }
            });

        });
    </script>
    <script type="text/javascript">
        window.UEDITOR_HOME_URL = "/ueditor/";
    </script>
    <script src="/ueditor/editor_config.js" type="text/javascript"></script>
    <script src="/ueditor/editor_all_min.js" type="text/javascript"></script>
    <link href="/ueditor/themes/default/ueditor.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage,lblGroupMail %>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="�������Լ������ʼ����ݣ������ʼ����͸����ϲ�ѯ���������л�Ա�򵥸��û�"/>
                    </td>
                </tr>
            </table>
        </div>
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage,lblSendTo %>" />��
                    <asp:RadioButton ID="RadioButton1" runat="server"  GroupName="A" 
                        Checked="true"/><asp:Literal ID="Literal4" runat="server" Text="<%$ Resources:SysManage,lblSendMailToUsers %>"/>��
                    
                    <asp:DropDownList ID="DropUserType" runat="server" class="dropSelect">
                        <asp:ListItem Value="" Selected="True" Text="<%$ Resources:SysManage,ListItemAll%>"></asp:ListItem>
                        <asp:ListItem Value="AA" Text="<%$ Resources:Site,fielDescriptionAA %>"></asp:ListItem>
                        <asp:ListItem Value="UU" Text="<%$ Resources:Site,fielDescriptionUU %>"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="A"/><asp:Literal ID="Literal5"
                        runat="server" Text="<%$Resources:SysManage,lblSendMailToUser %>"/>��                    
                    <asp:TextBox ID="txtKeyword" runat="server" Width="150px" class="admininput"></asp:TextBox>
                    ���������û�����
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:SysManage,lblMessageSubject %>"/>��
                            </td>
                            <td>
                                <asp:TextBox ID="txtTitle" runat="server" Width="496px" MaxLength="50"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ErrorMessage="*" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class"  style="vertical-align: top;">
                                <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:SysManage,lblMessageContent %>"/>��
                            </td>
                            <td>
                                <asp:TextBox ID="txtContent" runat="server" Width="500px" TextMode="MultiLine" Height="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnNext" runat="server" Text="<%$ Resources:SysManage,btnNextText %>" class="adminsubmit" OnClick="btnNext_Click">
                                </asp:Button>
                                <asp:Button ID="btnTestSend" runat="server" Text="<%$ Resources:SysManage,btnTestSend %>" class="adminsubmit" OnClick="btnTestSend_Click" ValidationGroup="EmailTest">
                                </asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <br />
    </div>    
    <script type="text/javascript">
        var editor = new baidu.editor.ui.Editor({//ʵ�����༭��
            iframeCssUrl: 'ueditor/themes/default/iframe.css', toolbars: [

            ['fullscreen', 'source', '|', 'undo', 'redo', '|',
                'bold', 'italic', 'underline', '|', 'forecolor', 'backcolor', '|','fontfamily', 'fontsize', '|',
                'justifyleft', 'justifycenter', 'justifyright','|', 'removeformat', '|', 'pasteplain', '|',  'link', 'unlink', '|']
                 ],
            initialContent: '',autoHeightEnabled:false,
            minFrameHeight: 230,
            pasteplain: false
         , wordCount: false
          , elementPathEnabled: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
        });
        editor.render('ctl00_ContentPlaceHolder1_txtContent'); //����������Ⱦ������
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
