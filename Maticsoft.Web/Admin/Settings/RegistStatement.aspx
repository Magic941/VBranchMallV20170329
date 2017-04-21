<%@ Page Title="<%$ Resources:SysManage,ptRegistStatement%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="RegistStatement.aspx.cs" Inherits="Maticsoft.Web.Admin.Settings.RegistStatement" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:SysManage,ptRegistStatement%>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:SysManage,lblRegistStatement%>"/>
                    </td>
                </tr>
            </table>
        </div>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
        <tr>
            <td class="td_class" style="vertical-align: top; height: 50px;">                
            </td>
            <td  >
                <asp:TextBox ID="txtContent" runat="server" Width="80%" TextMode="MultiLine" Text=""></asp:TextBox>
            </td>
        </tr>
       
       <tr>
        <td class="td_class">
        </td>
        <td height="25">
            <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                class="adminsubmit_short" OnClick="btnSave_Click" ></asp:Button>
            <asp:Button ID="btnReset" runat="server" CausesValidation="false" Text="<%$ Resources:Site, btnCancelText%>" 
                class="adminsubmit_short" OnClick="btnReset_Click"></asp:Button>
        </td>
    </tr>
    </table>
    
  </div>
  <script type="text/javascript">
      var editor = new baidu.editor.ui.Editor({//实例化编辑器
          iframeCssUrl: 'ueditor/themes/default/iframe.css', toolbars: [

            ['fullscreen',  '|',
                'bold', 'italic', 'underline', 'strikethrough', '|', 'forecolor', 'backcolor', '|',
                  'justifyleft', 'justifycenter', 'justifyright', 'justifyjustify', 'insertorderedlist', 'insertunorderedlist', '|', 'indent', '|',
                  'imagenone', 'imageleft', 'imageright',
                'imagecenter', '|', 'insertimage', '|', 'link', 'unlink']
                 ],
          initialContent: '',
          minFrameHeight: 500,
          pasteplain: false
         , wordCount: false
          , elementPathEnabled: false
 , autoClearinitialContent: false, imagePath: "/Upload/RTF/", imageManagerPath: "/"
      });
      editor.render('<%=txtContent.ClientID %>'); //将编译器渲染到容器
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
