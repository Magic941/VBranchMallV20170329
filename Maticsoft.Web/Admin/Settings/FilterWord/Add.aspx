<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Add.aspx.cs" Inherits="Maticsoft.Web.Admin.Settings.FilterWord.Add"
    Title="增加页" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="批量添加" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        每行一组过滤词语，不良词语和替换词语之间使用“=”进行分割;
如果只是想将某个词语直接替换成 **,则只输入词语即可;
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        如需禁止发布包含某个词语的文字，请将其对应的过滤动作设置为{BANNED}即可；如需当用户发布包含某个词语的文字时，自动标记为需要人工审核，
                       <br/> 而不直接显示或替换过滤，请将其对应的过滤动作设置为{MOD}即可 。如果需要自动替换成替换词，请将其对应的过滤动作设置为{REPLACE}即可
                            <br/><span style="color:Red;">设置 '{BANNED}' , '{MOD}','{REPLACE}' 请务必使用大写字母！<br/>例如：共产党={BANNED}</span>
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class" valign="top">
                                <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SiteSetting,lblFilterWordList %>" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="txtWords" TextMode="MultiLine" runat="server" Width="500px" Height="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    class="adminsubmit_short" OnClick="btnSave_Click"></asp:Button>
                                <asp:Button ID="btnCancle" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    class="adminsubmit_short" OnClick="btnCancle_Click"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <br />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>