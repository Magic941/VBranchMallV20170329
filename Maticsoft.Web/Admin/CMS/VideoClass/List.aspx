<%@ Page Title="<%$ Resources:CMSVideo, ptVideoClassList %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.CMS.VideoClass.List" %>

<%@ Register src="/Admin/../Controls/VideoClassDropList.ascx" tagname="VideoClassDropList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //Add
            $(".Add").colorbox({ iframe: true, width: "800", height: "420", overlayClose: false });
            //Modify
            $(".Modify").colorbox({ iframe: true, width: "800", height: "360", overlayClose: false });
            //Show
            $(".Show").colorbox({ iframe: true, width: "800", height: "360", overlayClose: false });
        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_gridView tr").each(function (index, domEle) {
                if (index != 0) {
                    var optionTag = $(this).html();
                    if (optionTag.indexOf("parentid=\"0\"") < 0) {
                        $(domEle).hide();
                        $(".productcag1 span img").attr("src", "/admin/images/jia.gif");
                    }
                }
            })
        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <!--Title -->
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="ltlList" runat="server" Text="<%$ Resources:CMSVideo, ptVideoClassList %>"></asp:Literal>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="ltlTip" runat="server" Text="<%$ Resources:CMSVideo, ptVideoClassListTip %>"></asp:Literal>
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
                <td width="80px" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/admin/Images/icon-1.gif" width="19" height="19" alt="" />
               <b>
               <asp:Literal ID="ltlCategory" runat="server" Text="<%$ Resources:CMSVideo, ltlCategory%>" />：</b>
                   
                </td>
                <td  style=" text-align:left; width:200px;">
                    <uc1:VideoClassDropList ID="VideoClassDropList1" runat="server" IsNull="true" />
                </td>
                <td   style=" text-align:left" >
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short" />
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/icon8.gif) no-repeat 5px 3px" ID="liAdd" runat="server" ><a class="Add"
                        href="add.aspx" >
                        <asp:Literal ID="ltlAdd" runat="server" Text="<%$ Resources:Site, ltlAdd %>"></asp:Literal></a>
                        <b>|</b> </li>
                
                        
                    <li  id="openAll" style="background: url(/admin/images/jia.gif) no-repeat;width: 60px;"><a style="cursor: pointer;"><asp:Literal
                    ID="Literal6" runat="server" Text="<%$Resources:CMS,CCExpandAll%>" /></a><b>|</b></li>
                    <li  id="closeAll" style="background: url(/admin/images/jian.gif) no-repeat;width: 60px;"><a style="cursor: pointer;"><asp:Literal
                    ID="Literal11" runat="server" Text="<%$Resources:CMS,CCollapseAll %>" /></a><b>|</b></li>
                </ul>
            </div>
        </div>
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false" ShowHeader="true"
            DataKeyNames="VideoClassID" CssClass="GridViewStyle" GridLines="none" RowStyle-CssClass="grdrow"
            HeaderStyle-CssClass="GridViewHeaderStyle" ShowFooter="false" SelectedRowStyle-BackColor="#FBFBF4"
            OnRowDataBound="gridView_RowDataBound" OnRowCommand="gridView_RowCommand" OnRowDeleting="gridView_RowDeleting"
            CellPadding="3" BorderWidth="1px" BackColor="White">
            <Columns>
                <asp:TemplateField HeaderText="<%$ Resources:CMSVideo, CategoryName %>" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <span id="spShowImage" runat="server" parentid='<%# Eval("ParentID") %>'>
                            <img src="/admin/images/jian.gif" width="24" height="24" alt="" />
                        </span><a href="/admin/cms/video/list.aspx?VideoClassID=<%# Eval("VideoClassID") %>">
                            <asp:Label ID="lblVideoClassName" runat="server" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CMSVideo, Sort %>" ItemStyle-Width="80px"
                    ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDesc" runat="server" ImageUrl="/admin/images/desc.png" CommandName="Fall"   Width="16" Height="16" />
                        <asp:ImageButton ID="imgAsc" runat="server" ImageUrl="/admin/images/asc.png" CommandName="Rise"   Width="16" Height="16" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:Site,btnDetailText %>" ItemStyle-Width="200px"
                    ItemStyle-HorizontalAlign="center">
                    <ItemStyle />
                    <ItemTemplate>
                        <asp:HyperLink CssClass="Show" ID="HyperLink3" runat="server" Text="<%$ Resources:Site, btnDetailText %>"
                            NavigateUrl='<%#Eval("VideoClassId", "Show.aspx?id={0}")%>' ForeColor="Black"></asp:HyperLink>   
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="<%$ Resources:Site,btnEditText %>" ItemStyle-Width="200px"
                    ItemStyle-HorizontalAlign="center">
                    <ItemStyle />
                    <ItemTemplate>
                        <asp:HyperLink CssClass="Modify" ID="lkEdit" runat="server" Text="<%$ Resources:Site, btnEditText %>"
                            NavigateUrl='<%#Eval("VideoClassId", "Modify.aspx?id={0}")%>' ForeColor="Black"></asp:HyperLink>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:Site, btnDeleteText %>" ItemStyle-Width="200px"
                    ItemStyle-HorizontalAlign="center">
                    <ItemStyle />
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                          OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  Text="<%$ Resources:Site, btnDeleteText %>" ForeColor="Black"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
                <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <RowStyle Height="25px" />
        </asp:GridView>
    </div>
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //全部隐藏
            $("#closeAll").bind("click", function () {
                $("#ctl00_ContentPlaceHolder1_gridView tr").each(function (index, domEle) {
                    if (index != 0) {
                        var optionTag = $(this).html();
                        if (optionTag.indexOf("parentid=\"0\"") < 0) {
                            $(domEle).hide();
                            $(".productcag1 span img").attr("src", "/admin/images/jia.gif");
                        }
                    }
                })
            });
            //全部展开
            $("#openAll").bind("click", function () {
                $("#ctl00_ContentPlaceHolder1_gridView tr").each(function (index, domEle) {
                    if (index != 0) {
                        $(domEle).show();
                        $(".productcag1 span img").attr("src", "/admin/images/jian.gif");
                    }
                })
            });
            $(".productcag1 span img").each(function (index, imgObj) {
                $(imgObj).click(function () {
                    if ($(imgObj).attr("src") == "/admin/images/jian.gif") {
                        var currentTrNode = $(imgObj).parents("tr");
                        currentTrNode = currentTrNode.next();
                        var optionHTML;
                        while (true) {
                            optionHTML = currentTrNode.html();
                            if (typeof (optionHTML) != "string") { break; }
                            if (optionHTML.indexOf("parentid=\"0\"") < 0) {
                                currentTrNode.hide();
                                currentTrNode = currentTrNode.next();
                            }
                            else { break; }
                        }
                        //把img src设加可开打状态
                        $(imgObj).attr("src", "/admin/images/jia.gif");
                    }
                    else {
                        var currentTrNode = $(imgObj).parents("tr");
                        currentTrNode = currentTrNode.next();
                        var optionHTML;
                        while (true) {
                            optionHTML = currentTrNode.html();
                            if (typeof (optionHTML) != "string") { break; }
                            if (optionHTML.indexOf("parentid=\"0\"") < 0) {
                                currentTrNode.show();
                                currentTrNode = currentTrNode.next();
                            }
                            else { break; }
                        }
                        $(imgObj).attr("src", "/admin/images/jian.gif");
                    }
                })
            })
        })
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
