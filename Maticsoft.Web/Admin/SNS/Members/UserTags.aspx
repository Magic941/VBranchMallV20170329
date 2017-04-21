<%@ Page Language="C#"  MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="UserTags.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Members.UserTags" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/css/tags.css" rel="stylesheet" type="text/css" />
    <script>
        $(function () {

            $(".deltags").click(function () {
                var id = $(this).attr("data-id");
                window.location.href = "UserTags.aspx?id="+id+"";
            });
          
           
        })
       
    </script>
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="用户标签" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                      您可以进行添加、删除用户标签信息操作
                    </td>
                </tr>
            </table>
        </div>
    <div>
             
        <div class="m_cj" style="background-color: white; padding-left:35px ">
            <div class="right_text">
                推荐用户标签</div>
            <asp:TextBox ID="txtTags" runat="server"></asp:TextBox>
            <asp:Button ID="btnAdd" runat="server" Text="添加"  CssClass="adminsubmit_short" 
                onclick="btnAdd_Click"/>
                                                                                <asp:Repeater runat="server" id="rptTags"  OnItemDataBound="rptTags_ItemDataBound"   >
                                                                                    <HeaderTemplate>
                                                                                        <ul class="cen_yy" style=" width: auto">
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <li class="colord7 classcolor" style="">
                                                                                            <a href="javascript:void(0)"  data-id="<%# DataBinder.Eval(Container.DataItem, "TagID") %>" data-tag="<%# DataBinder.Eval(Container.DataItem, "TagName") %>" style="color: #0078B6; background-position: initial; background-repeat: initial;"><%# DataBinder.Eval(Container.DataItem, "TagName") %>
                                                                                           <span id="lbtnDel" runat="server"> <span class="deltags" style="cursor: pointer" data-id="<%# DataBinder.Eval(Container.DataItem, "TagID") %>">×</span></span></a  >
                                                                                        </li>
                                                                                    </ItemTemplate>
                                                                                    <FooterTemplate>
                                                                                    </ul>
                                                                                    </FooterTemplate>
                                                                                </asp:Repeater>
                                                                                <div style="clear: both">
                                                                                </div>
                     
                                                                          

                                                                                <div style="clear: both">
                                                                                </div>
        </div>

         </div>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

