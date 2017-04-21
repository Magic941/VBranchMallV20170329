<%@ Page Title="地区分组管理" Language="C#" MasterPageFile="~/Admin/Basic.Master"  CodeBehind="AreaList.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.Regions.AreaList" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function() {
              $(".iframe").colorbox({ iframe: true, width: "650", height: "400", overlayClose: false });
        })
    </script>      
 </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="地区分组管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以对网站地区分组进行添加，编辑，删除等操作" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">
                        <asp:HiddenField ID="txtAreaId" runat="server" />
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="地区分组名称" />：
                            </td>
                            <td height="25">
                                <asp:TextBox ID="tName" TabIndex="1" runat="server" Width="250px" MaxLength="20"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="<%$ Resources:Site, ErrorNotNull%>"
                                    Display="Dynamic" ControlToValidate="tName"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnSave" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short"></asp:Button>&nbsp;&nbsp;
                                     <asp:Button ID="btnCancel" runat="server" Text="取消"
                                    OnClick="btnCancel_Click" class="adminsubmit_short"></asp:Button>
                            </td>
                        </tr>
                   
                    </table>
                </td>
            </tr>
        </table>
        <br/>
<%--        <div class="newslist">
            <div class="newsicon">
                <ul>
                    <li></li>
                 </ul>
            </div>
        </div>--%>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="false" DataKeyNames="AreaId"
            Style="float: left;" ShowGridLine="true" ShowHeaderStyle="true" OnRowCommand="gridView_RowCommand">
            <Columns>
                    <asp:TemplateField HeaderText="地区分组名称" ItemStyle-HorizontalAlign="Left" >
                    <ItemTemplate>
                        <%#Eval("Name")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                         <a class='iframe' href="SetRegionAreas.aspx?areaid=<%#Eval("AreaId") %> " >
                            <asp:Literal ID="Literal11" runat="server" Text="设置地区" /></a>
                            &nbsp;&nbsp;
                           <asp:LinkButton ID="linkModify" runat="server" CausesValidation="False" CommandName="OnUpdate"
                            Text="编辑" CommandArgument='<%#Eval("AreaId") %>'> </asp:LinkButton>
                             &nbsp;&nbsp;
                        <asp:LinkButton ID="linkDel" runat="server" CausesValidation="False" CommandName="Delete"
                            OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"
                            Text="<%$ Resources:Site, btnDeleteText %>"> </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr>
        </cc1:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>

