<%@ Page Title="社区分类管理" Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Basic.Master"
    CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Categories.List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/drop/client.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/js/drop/default.css" rel="stylesheet" type="text/css" />
    <link href="/Admin/js/drop/print.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jquery-1.8.1.min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <script src="/Admin/js/drop/jquery-ui-1.7.2.custom.min.js" type="text/javascript"></script>
    <script src="/Admin/js/jquery/SNSCateDrag.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="txtType" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle" style="float: left">
                        <asp:Literal ID="Literal1" runat="server" Text="" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody" style="float: left;border-bottom: 0px;">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以对分类信息进行添加，编辑，删除操作" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="newslist">
                <div class="newsicon">
                    <ul>
                        <li style="background: url(/images/icon8.gif) no-repeat 5px 3px; width: 50px;" id="liAdd"
                            runat="server"><a href="add.aspx?type=<%=Type %>" title="添加新的网站分类">添加</a> <b>|</b>
                        </li>
                      
                    </ul>
                </div>
            </div>
        <div >
            <ul id="sitemap" style="padding: 3px;">
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
