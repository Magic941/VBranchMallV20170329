<%@ Page Title="淘宝商品分类" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="TaoBaoCateList.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.TaoBaoCate.TaoBaoCateList" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="Gre" %>
<%@ Register Src="~/Controls/TaoBaoCategoryDropList.ascx" TagName="TaoBaoCategoryDropList"
    TagPrefix="Maticsoft" %>
<%@ Register Src="~/Controls/SNSCategoryDropList.ascx" TagName="SNSCategoryDropList"
    TagPrefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <script src="/Scripts/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link href="/admin/css/gridviewstyle.css" rel="stylesheet" type="text/css" />
            <link href="/Scripts/jBox/Skins/Blue/jbox.css" rel="stylesheet" type="text/css" />
        <script src="/Scripts/jBox/jquery.jBox-2.3.min.js" type="text/javascript"></script>
        <script src="/Scripts/jBox/i18n/jquery.jBox-zh-CN.js" type="text/javascript"></script>
     <script type="text/javascript">
         $(function () {

             $('[id$=btnUpdateTaoBaoCate]').click(function() {
                 $.jBox.tip("正在更新淘宝分类数据，由于数据量大,请您耐心等待......", 'loading');
             });

             //            $('#btnUpdateTaoBaoCate').click(function () {
             //                    $.jBox.tip("正在更新淘宝分类数据，由于数据量大,请您耐心等待......", 'loading');
             //                         $.ajax({
             //                    url: ("TaoBaoCateList.aspx?timestamp={0}").format(new Date().getTime()),
             //                    type: 'POST',
             //                    timeout:10000000,
             //                    dataType: 'json',
             //                    data: {
             //                        action: "GetTaoBaoCateList",
             //                        Callback:true
             //                    },
             //                    success: function(result) {
             //                        if (result.STATUS == "SUCCESS") {
             //                            var addCount = result.DATA.split("|")[0];
             //                            var updateCount=result.DATA.split("|")[1];
             //                            alert("更新淘宝分类成功，新增了【" + addCount + "】条数据，更新了【" + updateCount + "】条数据");
             //                        }
             //                    },
             //                });
             //                });
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
                        <asp:Literal ID="Literal1" runat="server" Text="社区淘宝分类关系管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="设置淘宝商品的分类与本站商品分类的对应关系。" />
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
                <td width="70px" height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal11" runat="server" Text="淘宝分类：" />
                </td>
                <td style="float: left" height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <Maticsoft:TaoBaoCategoryDropList ID="TaoBaoCate" runat="server" IsNull="true" />
                </td>
                <td style="float: left" height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    &nbsp;&nbsp;<asp:Literal ID="Literal2" runat="server" Text="<%$Resources:Site,lblKeyword%>" />：
                    <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                    <asp:Button ID="btnTaoBaoShow" runat="server" Text="搜索" OnClick="btnTaoBaoShow_Click"
                        class="adminsubmit_short"></asp:Button>
                            <asp:Button ID="btnUpdateTaoBaoCate" runat="server" Text="更新淘宝分类" OnClick="btnUpdateTaoBaoCate_Click"
                        class="adminsubmit" Visible=false></asp:Button><span style="color: gray;padding-left: 18px; display:none">建议您晚上网站访问量比较少的情况执行这个操作</span>
                   <input type="button" value="更新淘宝分类"  class="adminsubmit" style="display:none"/>
                </td>
            </tr>
        </table>
        <br />
        <!--Search end-->
        
        <Gre:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="CategoryId"
            Style="float: left;">
            <Columns>
                <asp:TemplateField HeaderText="分类名称" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="200">
                    <ItemTemplate>
                        <%#Eval("Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="描述" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("Description")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="对应本站分类" ItemStyle-Width="30%" ItemStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <%#GetSNSCateName(Eval("SnsCategoryId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </Gre:GridViewEx>
        
        <div class="newslist_title">
            <div class="shou" style="background-color: #FFFFFF">
            </div>
        </div>
        <br/>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border" id="tabRelation"
            name="Relation">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class">
                                对应本站商品分类 ：
                            </td>
                            <td height="25">
                                <Maticsoft:SNSCategoryDropList ID="SNSCategoryDropList" runat="server" IsNull="true" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                是否更新淘宝下属子类 ：
                            </td>
                            <td height="25">
                                <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left">
                                    <asp:ListItem Value="true" Text="是"></asp:ListItem>
                                    <asp:ListItem Value="false" Text="否" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnBatch" runat="server" Text="批量更新" class="adminsubmit" OnClick="btnBatch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>