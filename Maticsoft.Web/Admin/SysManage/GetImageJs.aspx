<%@ Page Title="<%$ Resources:SysManage,ptClearCache%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"  CodeBehind="GetImageJs.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.GetImageJs" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="生成网站图片采集插件" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以把图片采集的插件设置成您自己网站的图片采集工具" />
                    </td>
                </tr>
            </table>
        </div>
            <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
                <tr>
                    <td style="width: 120px" align="right" class="tdbg">
                        <b><asp:Literal ID="Literal3" runat="server" Text="生成采集工具" />：</b>
                    </td>
                    <td class="tdbg" align="left" width="150px">                    
                    <asp:Button ID="btnSetCollection" runat="server" Text="生成采集工具" OnClick="btnSetCollection_Click"
                    class="adminsubmit" />   
                    </td>      
                    <td>   <span><a href="/Home/CollectionJS"  style=" font-size:16px"  target="_blank">预览</a></span>                  </td>              
                </tr>
            </table>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

