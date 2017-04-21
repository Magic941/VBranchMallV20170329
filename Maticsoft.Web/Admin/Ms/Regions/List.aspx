<%@ Page  Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Ms.Regions.List" %>

<%@ Register src="/Controls/Region.ascx" tagname="Region" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                       省市区域管理
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以编辑、删除省市区域信息
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
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"  Visible="false"></asp:TextBox>
                    <uc1:Region ID="Regions1" runat="server" />
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short" Visible="false"></asp:Button>
                </td>
            </tr>
        </table>
        <br />
        <!--Search end-->
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/admin/images/icon8.gif) no-repeat 5px 3px; width: auto; display: none">
                        <a href="add.aspx" >添加</a> <b>|</b> </li>
                  
                    <li style="background: url(/admin/images/jia.gif) no-repeat  5px -1px; width: auto;">
                        <span id="openAll"><a style="cursor: pointer; text-decoration: none; line-height: normal;">
                            全部展开</a></span><b>|</b></li>
                    <li style="background: url(/admin/images/jian.gif) no-repeat  5px -1px; width: auto;">
                        <span id="closeAll"><a style="cursor: pointer; text-decoration: none; line-height: normal;">
                            全部收缩</a></span><b>|</b></li>
                </ul>
            </div>
        </div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel runat="server" ID="UpdatePanel1">
            <ContentTemplate>
                    <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false" ShowHeader="true"
        DataKeyNames="RegionId" CssClass="GridViewStyle" GridLines="none" RowStyle-CssClass="grdrow"
        HeaderStyle-CssClass="GridViewHeaderStyle" ShowFooter="false" SelectedRowStyle-BackColor="#FBFBF4"
        OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" CellPadding="0" OnRowCommand="gridView_RowCommand"
        BorderWidth="1px" BackColor="White">
        <Columns>
            <asp:TemplateField HeaderText="地区名称" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="150px">
                <ItemTemplate>
                    <span id="spShowImage" runat="server" parentid='<%# Eval("parentid") %>'>
                        <img src="/Admin/Images/jian.gif"  width="24" height="24" />
                    </span>
                    <asp:Label ID="lblRegionName" runat="server" />
                    <asp:Label ID="lblRegId" runat="server" Text='<%#Eval("RegionId") %>' Visible="false"></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
             
            <asp:TemplateField HeaderText="操作" ItemStyle-Width="150px" >
                <ItemTemplate>
                <span id="linkModify" runat="server"> <a href="Modify.aspx?Id=<%# Eval("RegionId")%>"  style="color:Blue;" >编辑</a> 
                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                                    Text="<%$ Resources:Site, btnDeleteText %>" ForeColor="Blue" Visible="false"></asp:LinkButton>
               </span>    </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="" ItemStyle-Width="150px" >
                <ItemTemplate>
                        <asp:LinkButton ID="LinkRegionRec" runat="server" CausesValidation="False" CommandName="RegionRec" CommandArgument='<%# Eval("RegionId")%>'
                                    Text="设置热门城市" ForeColor="Blue" Visible="false"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <FooterStyle Height="25px" HorizontalAlign="Right" />
        <HeaderStyle Height="35px" />
        <PagerStyle Height="25px" HorizontalAlign="Right" />
        <RowStyle Height="25px" />
    </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="newslist_title">
            <div class="shou" style="background-color: #FFFFFF">
            </div>
        </div>
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
                });
            });
            //全部展开
            $("#openAll").bind("click", function () {
                $("#ctl00_ContentPlaceHolder1_gridView tr").each(function (index, domEle) {
                    if (index != 0) {
                        $(domEle).show();
                        $(".productcag1 span img").attr("src", "/admin/images/jian.gif");
                    }
                });
            });
            $(".productcag1 span img").each(function(index, imgObj) {
                $(imgObj).click(function() {
                    if ($(imgObj).attr("src") == "/admin/images/jian.gif") {
                        var currentTrNode = $(imgObj).parents("tr");
                        currentTrNode = currentTrNode.next();
                        var optionHTML;
                        while (true) {
                            optionHTML = currentTrNode.html();
                            if (typeof(optionHTML) != "string") {
                                break;
                            }
                            if (optionHTML.indexOf("parentid=\"0\"") < 0) {
                                currentTrNode.hide();
                                currentTrNode = currentTrNode.next();
                            } else {
                                break;
                            }
                        }
                        //把img src设加可开打状态
                        $(imgObj).attr("src", "/admin/images/jia.gif");
                    } else {
                        var currentTrNode = $(imgObj).parents("tr");
                        currentTrNode = currentTrNode.next();
                        var optionHTML;
                        while (true) {
                            optionHTML = currentTrNode.html();
                            if (typeof(optionHTML) != "string") {
                                break;
                            }
                            if (optionHTML.indexOf("parentid=\"0\"") < 0) {
                                currentTrNode.show();
                                currentTrNode = currentTrNode.next();
                            } else {
                                break;
                            }
                        }
                        $(imgObj).attr("src", "/admin/images/jian.gif");
                    }
                });
            });
        })
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>