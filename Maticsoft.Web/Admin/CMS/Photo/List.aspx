<%@ Page Title="<%$Resources:CMSPhoto,ptList %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.CMS.Photo.List" %>

<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<%@ Register TagPrefix="uc1" TagName="PhotoClassDropList" Src="~/Controls/PhotoClassDropList.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery/maticsoft.img.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("[id$='txtPhotoName']").hide();
            $(".group2").colorbox({ rel: 'group2', transition: "fade" });
            $(".iframe").colorbox({ iframe: true, width: "780", height: "630", overlayclose: false });
            resizeImg('.border', 180, 200);
        });
        function ShowEdit(controls) {
            $(controls).hide();
            $(controls).next().show();
            $(controls).next().focus();
        }

        function EditPhotoName(controls, id) {
            $.ajax({
                url: "/EditPhotoHandle.aspx",
                type: 'post', dataType: 'text', timeout: 10000,
                data: { Action: "EditPhotoName", PhotoName: $(controls).val(), PhotoId: id },
                success: function (resultData) {
                    if (resultData != "") {
                        $(controls).hide();
                        $(controls).prev().text(resultData).show();
                    }
                }
            });
        }
        function EditCover(controls, id) {
            $.ajax({
                url: "/EditPhotoHandle.aspx",
                type: 'post', dataType: 'text', timeout: 5000, async: false,
                data: { Action: "EditCover", AlbumId: $("[id$='dorpPhotoAlbum']").find(":selected").val(), PhotoId: id },
                success: function (resultData) {
                    if (resultData == "Success") {
                        $("[title='<%= Resources.CMSPhoto.lblFrontCover%>']").attr("title", "<%= Resources.CMSPhoto.lblSetToCover%>").text("<%= Resources.CMSPhoto.lblSetToCover%>");
                        $(controls).attr("title", "<%= Resources.CMSPhoto.lblFrontCover%>").text("<%= Resources.CMSPhoto.lblFrontCover%>");
                        clickautohide(4, "<%= Resources.CMSPhoto.TooltipSetToCoverSuccess%>", 2000);
                    }
                    else {
                        clickautohide(5, "<%= Resources.CMSPhoto.TooltipSetToCoverFail%>", 2000);
                    }
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMSPhoto,ptList %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMSPhoto,lblList %>" />
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
                   <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:CMSPhoto,lblAlbum %>" />：
                    <asp:DropDownList ID="dorpPhotoAlbum" runat="server">
                        <asp:ListItem Value="0" Text="<%$Resources:Site,PleaseSelect %>"></asp:ListItem>
                    </asp:DropDownList>
                     <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:CMSPhoto, lblAlbumType%>" />：
                </td>
                <td width="70" style="padding-left:0px;">
                       <uc1:PhotoClassDropList ID="ddlPhotoClass" runat="server" IsNull="True" />
                      
                </td>
                <td>   <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, lblKeyword%>" />：<asp:TextBox
                        ID="txtKeyWord" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" CssClass="adminsubmit_short" runat="server" Text="<%$ Resources:Site, lblSearch%>"
                        OnClick="btnSearch_Click" /></td>
            </tr>
        </table>
       
        <!--Search end-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul class="list">
                    <li style="width: auto;">
                        <input id="Checkbox1" type="checkbox" onclick='$(":checkbox").attr("checked", $(this).attr("checked")=="checked");' /><label
                            for="Checkbox1" style="color: #333; font: 12px '宋体';">全选</label><b>|</b>
                    </li>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server">
                        <a class="various" href='add.aspx?AlbumID=<%=this.dorpPhotoAlbum.SelectedValue %>'>
                            <asp:Literal ID="Literal8" runat="server" Text="<%$Resources:Site,lblAdd %>" /></a>
                        <b>|</b> </li>
                    <%-- <li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a   href="add.aspx">添加</a> <b>|</b> </li>--%>
                    <li style="background: url(/admin/images/delete.gif) no-repeat; width: 60px;" id="liDel"
                        runat="server">
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick='return confirm($(this).attr("ConfirmText"))'
                            ConfirmText="<%$Resources:Site,TooltipDelConfirm %>">
                            <asp:Literal ID="Literal9" runat="server" Text="<%$Resources:Site,btnDeleteListText %>" /></asp:LinkButton><b>|</b></li></ul></div></div><table style="width: 100%; margin-left: 10;" cellpadding="5" cellspacing="5" class="border">
            <tr>
                <td>
                    <div id="gallery">
                        <asp:DataList ID="DataListPhoto" RepeatColumns="5" RepeatDirection="Horizontal" HorizontalAlign="Left"
                            runat="server" OnItemCommand="DataListPhoto_ItemCommand" OnItemDataBound="DataListPhoto_ItemDataBound" >
                            
                            <ItemTemplate>
                                <table cellpadding="2" cellspacing="8">
                                    <tr>
                                        <td style="border: 1px solid #ecf4d3; text-align: center">
                                            <a class="group2" href='<%#Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbImageUrl").ToString(), "T235x1280_")%>' title='<%#Eval("PhotoName") %>'>
                                                <img ref='<%#Maticsoft.Web.Components.FileHelper.GeThumbImage(Eval("ThumbImageUrl").ToString(), "T235x1280_")%>' style="width: <%#Unit.Parse(strThumbImageWidth) %>;
                                                    height: <%#Unit.Parse(strThumbImageHeight) %>" />
                                            </a>
                                            <br />
                                            <asp:CheckBox ID="ckPhoto" runat="server" />
                                            <asp:HiddenField runat="server" ID="hfPhotoId" Value='<%#Eval("PhotoID") %>' />
                                            <asp:Label ID="lblImageName" runat="server" Text='<%#Eval("PhotoName") %>' onclick="ShowEdit(this)"></asp:Label>
                                            <asp:TextBox  ID="txtPhotoName" runat="server" Text='<%#Eval("PhotoName") %>' PhoneId='<%#Eval("PhotoID") %>'
                                                onblur='EditPhotoName(this, $(this).attr("PhoneId"))'></asp:TextBox><br /><a style="color: #0063dc; text-decoration: none;" phoneid='<%#Eval("PhotoID") %>'  onclick='EditCover(this,$(this).attr("PhoneId"))' title='<%# GetPhotoCover(Eval("CoverPhoto"), Eval("PhotoID"))%>'><%# GetPhotoCover(Eval("CoverPhoto"), Eval("PhotoID"))%></a><span  runat="server"  ID="lbtnModify"><a  class='iframe'  style="color: #0063dc; 
                                                    margin-left: 30px; margin-right: 30px;" href="Modify.aspx?id=<%#Eval("PhotoID") %>"><asp:Literal ID="Literal3" runat="server" Text="<%$Resources:Site,btnEditText%>" />
                                                </a></span>
                                              
                                            <asp:LinkButton ID="lbtnDel" runat="server" Style="color: #0063dc;" CommandName="delete"
                                                CommandArgument='<%#Eval("PhotoID") %>' OnClientClick='return confirm($(this).attr("ConfirmText"))'
                                                ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Visible="True">
                                                <asp:Literal ID="Literal11" runat="server" Text="<%$Resources:Site,btnDeleteText %>" /></asp:LinkButton></td></tr></table></ItemTemplate></asp:DataList></div></td></tr><tr>
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
