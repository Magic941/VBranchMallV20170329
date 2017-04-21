<%@ Page Title="<%$ Resources:Site, ptUserAdmin %>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true"  CodeBehind="CategoryList.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Gift.CategoryList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        $(function () {
            $(".iframe").colorbox({ iframe: true, width: "450", height: "325", overlayClose: false });
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
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="礼品分类管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以添加、编辑、删除礼品分类信息" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title end -->
        <!--Add  -->
        <!--Add end -->
        <!--Search -->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang" style="display:none;">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px;width:auto;" id="liAdd" runat="server"><a href="AddCategory.aspx" title="添加礼品分类">  添加</a> <b>|</b> </li>
                    <li style="background: url(/admin/images/jia.gif) no-repeat  5px -1px;width:auto;"><span id="openAll"><a style="cursor: pointer;text-decoration:none;line-height:normal;"> 全部展开</a></span><b>|</b></li>
                    <li style="background: url(/admin/images/jian.gif) no-repeat  5px -1px;width:auto;"> <span id="closeAll"> <a style="cursor: pointer;text-decoration:none;line-height:normal;"> 全部收缩</a></span><b>|</b></li>
                </ul>
            </div>
        </div>
            <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="false" ShowHeader="true"
            DataKeyNames="CategoryId" CssClass="GridViewStyle"  RowStyle-CssClass="grdrow"
            HeaderStyle-CssClass="GridViewHeaderStyle" ShowFooter="false" SelectedRowStyle-BackColor="#FBFBF4"
            OnRowDataBound="gridView_RowDataBound" OnRowCommand="gridView_RowCommand" OnRowDeleting="gridView_RowDeleting"
            CellPadding="3" BorderWidth="1px" BackColor="White" Width="100%">
            <Columns>
                <asp:TemplateField HeaderText="分类名称" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="30%">
                    <ItemTemplate>
                        <span id="spShowImage" runat="server" parentid='<%# Eval("ParentCategoryId") %>'>
                            <img src="/admin/images/jian.gif" width="24" height="24" alt="" />
                        </span>
                            <asp:Label ID="lblName" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="描述" ItemStyle-Width="10%"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Description")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="排序" ItemStyle-Width="5%"
                    ItemStyle-HorizontalAlign="Center" Visible=false>
                    <ItemTemplate>
                        <asp:ImageButton ID="imgDesc" runat="server" ImageUrl="/admin/images/desc.png" CommandName="Fall" Width="16" Height="16" />
                        <asp:ImageButton ID="imgAsc" runat="server" ImageUrl="/admin/images/asc.png" CommandName="Rise" Width="16" Height="16"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$ Resources:CMSVideo, Operation %>" ItemStyle-Width="15%"
                    ItemStyle-HorizontalAlign="Center">
                    <ItemStyle />
                    <ItemTemplate>
                   
                        &nbsp;&nbsp;
                      <span id="lbtnModify" runat="server"> <a href="UpdateCategory.aspx?categoryId=<%#Eval("CategoryID") %>" style="color:Blue">编辑</a>    &nbsp;&nbsp;</span>  
                    
                        <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
                           OnClientClick="return confirm('删除分类会删除该分类下所有子分类\n，确定要删除选择的分类吗？')" Text="<%$ Resources:Site, btnDeleteText %>" ForeColor="Blue"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
              <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <RowStyle Height="25px" />
        </asp:GridView>
    <div class="newslist_title">
        <div class="shou" style="background-color:#FFFFFF">
            
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
