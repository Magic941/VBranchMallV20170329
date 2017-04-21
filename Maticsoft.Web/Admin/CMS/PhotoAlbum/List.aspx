<%@ Page Title="<%$Resources:CMSPhoto,ptAlbumList %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.PhotoAlbum.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<%@ Register TagPrefix="webdiyer" Namespace="Wuqi.Webdiyer" Assembly="AspNetPager, Version=7.2.0.0, Culture=neutral, PublicKeyToken=fb0a0fe055d40fd4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("[id$='txtAlbumName']").hide();
            $(".iframe").colorbox({ iframe: true, width: "790", height: "560", overlayclose: false });
            $(".addiframe").colorbox({ iframe: true, width: "790", height: "520", overlayclose: true });

            resizeImg('.border', 150, 180);
        });
        function ShowEdit(controls) {
            $(controls).hide();
            $(controls).next().show();
            $(controls).next().focus();
        }

        function EditAlbumName(controls, id) {
            $.ajax({
                url: "/EditPhotoHandle.aspx",
                type: 'post', dataType: 'text', timeout: 10000,
                data: { Action: "EditAlbumName", AlbumName: $(controls).val(), AlbumID: id },
                success: function (resultData) {
                    if (resultData != "") {
                        $(controls).hide();
                        $(controls).prev().text(resultData).show();
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,ptAlbumList %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMSPhoto,lblAlbumList %>" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
         <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td width="170" height="35" bgcolor="#FFFFFF" class="newstitlebody">
                   <asp:Literal ID="Literal2" runat="server" Text="相册名称" />
                     <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        class="adminsubmit_short" OnClick="btnSearch_Click"></asp:Button>
                </td>
               
                
            </tr>
        </table>
        
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul class="list">
                    <li>
                        <input id="Checkbox1" type="checkbox" onclick='$(":checkbox").attr("checked", $(this).attr("checked")=="checked");' />
                        <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,CheckAll %>" /></li>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat=server><a class="addiframe"
                        href="add.aspx">
                        <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:Site,lblAdd %>" /></a>
                        <b>|</b> </li>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel" runat="server">
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></asp:LinkButton><b>|</b></li>
                     
                </ul>
            </div>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td>
                    <asp:DataList ID="RepeaterPhotoAlbum" runat="server" RepeatColumns="5" RepeatDirection="Horizontal"
                        HorizontalAlign="Left" class="border" OnItemCommand="RepeaterPhotoAlbum_ItemCommand" OnItemDataBound="DataListPhoto_ItemDataBound">
                        <ItemTemplate>
                            <table cellpadding="2" cellspacing="8">
                                <tr>
                                    <td style="border: 1px solid #ecf4d3; text-align: center">
                                        <a href="../Photo/List.aspx?AlbumID=<%#Eval("AlbumID") %>">
                                            <div style="background-image: url(/Admin/Images/albumbg.gif); height: 197px; width: 167px;">
                                                <img ref='<%#Eval("ThumbImageUrl").ToString() == "" ? "/Admin/Images/nophoto.png": Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbImageUrl").ToString(), "T235x1280_") %>'
                                                    style="width: 150px; height: 180px; margin-top: 5px; margin-left: -4px" />
                                            </div>
                                        </a>
                                        <br />
                                        <asp:CheckBox ID="ckAlbum" runat="server" />
                                        <asp:HiddenField runat="server" ID="hfAlbumID" Value='<%#Eval("AlbumID") %>' />
                                        <asp:Label ID="lblImageName" runat="server" onclick="ShowEdit(this)" Text='<%#Eval("AlbumName") %>'></asp:Label><asp:TextBox ID="txtAlbumName" runat="server" Width="100" Text='<%#Eval("AlbumName") %>'
                                            AlbumID='<%#Eval("AlbumID") %>' onblur='EditAlbumName(this, $(this).attr("AlbumID"))'></asp:TextBox><br />
                                        <span  runat="server"  ID="lbtnModify" > <a class='iframe' style="color: #0063dc;" href="Modify.aspx?id=<%#Eval("AlbumID") %>">
                                                <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,btnEditText %>" />
                                            </a></span>   
                                        <asp:LinkButton ID="lbtnDel" runat="server" Style="color: #0063dc;" CommandName="delete"
                                            CommandArgument='<%#Eval("AlbumID") %>' OnClientClick='return confirm($(this).attr("ConfirmText"))'
                                            ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">
                                            &nbsp;&nbsp;<asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Site,btnDeleteText %>" /></asp:LinkButton></td></tr></table></ItemTemplate></asp:DataList></td></tr><tr>
                <td>
                    <webdiyer:AspNetPager runat="server" ID="AspNetPager1" CssClass="anpager" CurrentPageButtonClass="cpb"
                        OnPageChanged="AspNetPager1_PageChanged" PageSize="10" FirstPageText="<%$Resources:Site,FirstPage %>"
                        LastPageText="<%$Resources:Site,EndPage %>" NextPageText="<%$Resources:Site,GVTextNext %>"
                        PrevPageText="<%$Resources:Site,GVTextPrevious %>">
                    </webdiyer:AspNetPager>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
