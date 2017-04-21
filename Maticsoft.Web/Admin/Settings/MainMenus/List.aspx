<%@ Page Title="<%$Resources:CMS,WMptList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Settings.MainMenus.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }

        function GetAdd() {
            window.location = 'add.aspx';
        }

        
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <!--Title -->
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,WMptList%>"/>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,WMlblList%>"/>
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
                    <img src="/admin/images/list.gif"  />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                                <asp:Literal ID="Literal9" runat="server" Text="类型" />：
                                  <cc1:ConfigAreaList ID="ddlType" runat="server"   VisibleAll="True"  />
                            <asp:Literal ID="Literal2" runat="server" Text="关键字" />：
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search end-->
        <br />
        <!--Toolbar -->
        <div class="newslist">
            <div class="newsicon">
                <ul>
                  <li style="height:35px;"  >
                 <asp:Button ID="btnAddtwo" runat="server" Text="添加" class="adminsubmit_short"  OnClientClick="window.location='add.aspx';return false;"/> 
                      </li>
                    <li  style="height:35px;">
                <asp:Button ID="btnDeletetwo" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  class="adminsubmit" OnClick="btnDelete_Click" />
                      </li> 
                </ul>
            </div>
        </div>
        <!--Toolbar end -->
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" ShowExportExcel="False" ShowExportWord="False" ExcelFileName="FileName1"
            CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="MenuID">
            <Columns>
                <asp:TemplateField HeaderText="区域" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#GetAreaName(Eval("NavArea"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Site,lblShowID %>" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Sequence")%>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:CMS,WMenuName %>" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("MenuName")%>
                    </ItemTemplate>
                </asp:TemplateField>

                           <asp:TemplateField HeaderText="URL类型" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#GetURLType(Eval("URLType"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                                <asp:TemplateField HeaderText="对应模板" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("NavTheme")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="URL值" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%#Eval("NavURL")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:CMS,WMSystemMenu %>" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("MenuType").ToString()=="0"?Resources.CMS.WMSystem:Resources.CMS.WMCustom%>                        
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:CMS,WMlblTarget %>" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("Target").ToString()=="0"?Resources.CMS.WMlblSelf:Resources.CMS.WMlblBlank%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:CMS,WMIsAvailable %>" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Eval("IsUsed").ToString()=="True"?Resources.CMS.WMAvailable:Resources.CMS.WMNotAvailable%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="<%$Resources:Site,lblOperation %>" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                      <span id="spanmodify" runat="server">  <a href="Modify.aspx?id=<%#Eval("MenuID") %>">
                            <asp:Literal ID="Literal7" runat="server" Text="<%$Resources:Site,btnEditText %>"/></a></span>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                           OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" Text="<%$ Resources:Site, btnDeleteText %>"></asp:LinkButton>
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
        <!--Toolbar -->
        <div class="newslist">
            <div class="newsicon">
                <ul  >
                 <li style="height:35px;"  >
                 <asp:Button ID="butAdd" runat="server" Text="添加" class="adminsubmit_short"  OnClientClick="window.location='add.aspx';return false;"/> 
                      </li>
                    <li  style="height:35px;">
                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"
                      OnClientClick='return confirm($(this).attr("ConfirmText"))' ConfirmText="<%$Resources:Site,TooltipDelConfirm %>"  class="adminsubmit" OnClick="btnDelete_Click" />
                      </li> 
                </ul>
            </div>
        </div>
        <!--Toolbar end -->
        
        <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
