<%@ Page Title="<%$Resources:CMS,ClassptList%>" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.CMS.ClassType.List" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".iframe").colorbox({ iframe: true, width: "400", height: "280", overlayClose: false });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title Start-->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:CMS,ClassptList %>" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:CMS,ClasslblList %>" />
                    </td>
                </tr>
            </table>
        </div>
        <!--Title End -->
        <!--Search Start-->
        <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    <asp:Literal ID="Literal7" runat="server" Text="<%$ Resources:Site, lblSearch%>" />£º
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                        OnClick="btnSearch_Click" class="adminsubmit_short"></asp:Button>
                </td>
            </tr>
        </table>
        <!--Search End-->
        <br />
        <div class="newslist">
            <div class="newsicon">
                <ul class="list">
                    <li style="background: url(/images/icon8.gif) no-repeat 5px 3px;display:none;"><a class='iframe'  href="EditInfo.aspx">  <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Site,lblAdd %>" /></a>
                        <b>|</b> </li>
                     <li style="background: url(/images/icon8.gif) no-repeat 5px 3px;"  ><a id="liAdd" runat="server" href="add.aspx">Ìí¼Ó</a> <b>|</b> </li>
                    <%--<li style="background: url(/admin/images/delete.gif) no-repeat ;width:60px;"><a href="javascript:;" onclick="GetDeleteM()"><asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,btnDeleteListText %>"/></a><b>|</b></li>--%>
                   
                </ul>
            </div>
        </div>
        <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDeleting="gridView_RowDeleting"
            OnRowDataBound="gridView_RowDataBound" UnExportedColumnNames="Modify" Width="100%"
            PageSize="15" DataKeyNames="ClassTypeID" ShowExportExcel="False" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="false" >
            <Columns>
                <asp:BoundField DataField="ClassTypeName" HeaderText="<%$Resources:CMS,ClasslblClassName %>"
                    SortExpression="ClassTypeName" ItemStyle-HorizontalAlign="Left"  />
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$Resources:Site,lblOperation %>"
                    ItemStyle-HorizontalAlign="Center" SortExpression="ClassTypeID" ItemStyle-Width="120px" >
                    <ItemTemplate>
                        <a class='iframe' href="EditInfo.aspx?id=<%#Eval("ClassTypeID") %>">
                            <asp:Literal ID="Literal6" runat="server" Text="<%$Resources:Site,btnEditText%>" /></a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ControlStyle-Width="50" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="120px"  >
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>" OnClientClick="return confirm($(this).attr('ConfirmText'))"
                            ConfirmText="<%$Resources:Site,TooltipDelConfirm%>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
           <FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
        </cc1:GridViewEx>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
