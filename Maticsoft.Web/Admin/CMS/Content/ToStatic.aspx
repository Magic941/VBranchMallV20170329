<%@ Page Title="<%$Resources:CMS,ContentptList %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="ToStatic.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Content.ToStatic" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>
    <script src="/Scripts/jquery.cookie.js" type="text/javascript"></script>
    <script src="/admin/js/CMS/httptostatic.js" type="text/javascript"></script>
    <style type="text/css">
        .style1
        {
            width: 32px;
        }
        .style2
        {
            width: 72px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <asp:HiddenField ID="txtTaskCount" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtIsStatic" runat="server"></asp:HiddenField>
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal3" runat="server" Text="CMS文章静态管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="设置CMS是否启用静态化，并根据条件进行静态页面生成。" />
                    </td>
                </tr>
            </table>
        </div>
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3"
            class="borderkuang" id="Table1">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    开启静态化：
                </td>
                <td>
                    <asp:RadioButtonList ID="radlStatus" runat="server" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="radlStatus_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="true">是</asp:ListItem>
                        <asp:ListItem Value="false" Selected="True">否</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3"
            class="borderkuang" id="tabIndex">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                </td>
                <td>
                    <asp:Button ID="btnIndex" runat="server" Text="首页静态生成" class="adminsubmit" OnClick="btnIndex_Click" />
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3"
            class="borderkuang" id="tabRuleSet">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td colspan="3">
                    <h3>
                        静态路径规则设置</h3>
                </td>
            </tr>
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    存放目录
                </td>
                <td style="width: 240px">
                    <asp:TextBox ID="txtCMSRoot" runat="server" Text="/"></asp:TextBox>
                </td>
                  <td>
                    <span style="color: gray">设置文章静态文件的 存放目录，请以"/"开始和结束，如：/html/</span>
                </td>
            </tr>
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    栏目文件夹
                </td>
                <td >
                    <asp:DropDownList ID="ddlClassUrl" runat="server" Width="200px">
                        <asp:ListItem Value="0">栏目ID</asp:ListItem>
                        <asp:ListItem Value="1">栏目名称拼音</asp:ListItem>
                        <asp:ListItem Value="2">自定义</asp:ListItem>
                    </asp:DropDownList>
                </td>
                   <td>
                    <span style="color: gray">选择自定义时，如果该栏目的自定义值为空，则默认使用栏目ID</span>
                </td>
            </tr>
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                    文章文件名
                </td>
                <td>
                    <asp:DropDownList ID="ddlArticleUrl" runat="server" Width="200px" >
                        <asp:ListItem Value="0">文章ID</asp:ListItem>
                        <asp:ListItem Value="1">文章标题拼音</asp:ListItem>
                        <asp:ListItem Value="2">自定义</asp:ListItem>
                    </asp:DropDownList>
                </td>
                   <td>
                  <span style="color: gray">选择自定义时，如果该文章的自定义值为空，则默认使用文章ID</span>
                </td>
            </tr>
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td style="width: 80px">
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" Text="保存" class="adminsubmit_short" OnClick="btnRuleSet_Click" />
                </td>
            </tr>
        </table>
             <br />
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3"
            class="borderkuang" id="tabTask">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td colspan="2">
                    <h3>
                        文章静态生成</h3>
                </td>
            </tr>
            <tr>
                <td style="width: 8px;">
                </td>
                <td style="width: 80px">
                    文章栏目：
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="dropParentID" runat="server" Width="200px" name="Cid">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 8px;">
                </td>
                <td>
                    添加时间：
                </td>
                <td style="text-align: left">
                    <input type="text" id="from" name="from" style="width: 90px" /><asp:HiddenField ID="txtFrom"
                        runat="server" />
                    --
                    <input type="text" id="to" name="to" style="width: 90px" /><asp:HiddenField ID="txtTo"
                        runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 8px;">
                </td>
                <td>
                </td>
                <td style="text-align: left">
                    <%--  <asp:Button ID="btnToStatic" runat="server" Text="静态化"  OnClick="btnToStatic_Click"/>--%>
                    <input id="btnToStatic" type="button" value="生成静态页面" class="adminsubmit" />
                </td>
            </tr>
            <tr style="height: 60px">
                <td colspan="3" id="probar" style="display: none;">
                    <div id="progressbar" style="width: 560px;">
                    </div>
                    <div style="width: 560px; text-align: center">
                        <span id="txtCount"></span>/共<span id="txtTotalCount"></span></div>
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellspacing="1" style="width: 100%; height: 100%; display: none;"
            cellpadding="3" class="borderkuang" id="txtRemain">
            <tr>
                <td>
                </td>
                <td colspan="2">
                    <div>
                        上次任务断点时间：<asp:Literal ID="txtTaskDate" runat="server"></asp:Literal>
                        &nbsp;&nbsp;任务ID：<asp:Literal ID="txtTaskId" runat="server"></asp:Literal>
                        &nbsp;&nbsp;剩余条数：<asp:Literal ID="txtTaskReCount" runat="server"></asp:Literal></div>
                    <div style="padding-left: 160px">
                        <input id="btnContinue" type="button" value="继续任务" class="adminsubmit" />
                        &nbsp;&nbsp;
                        <input id="btnRemove" type="button" value="清除任务" class="adminsubmit" /></div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
