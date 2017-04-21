<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConfigSystem.aspx.cs" Inherits="Maticsoft.Web.Admin.SysManage.ConfigSystem"
    Title="<%$Resources:SysManage,ptConfigSystem%>" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<%@ Register Src="/Controls/copyright.ascx" TagName="copyright" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
        <asp:Literal ID="Literal1" runat="server" Text="<%$Resources:SysManage,ptConfigSystem %>" />
    </title>
    <link href="/admin/css/Guide.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/index.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/MasterPage<% =Session["Style"] %>.css" type="text/css" rel="stylesheet" />
    <link href="/admin/css/xtree.css" type="text/css" rel="stylesheet" />
    <link href="../css/admin.css" rel="stylesheet" type="text/css" />
    <script src="/admin/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <link type="text/css" href="/admin/js/msgbox/css/msgbox.css" rel="stylesheet" charset="utf-8" />
    <script type="text/javascript" src="/admin/js/msgbox/script/msgbox.js" charset="utf-8"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div class="newslistabout">
            <div class="newslist_title">
                <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                    <tr>
                        <td bgcolor="#FFFFFF" class="newstitle">
                            <asp:Literal ID="Literal3" runat="server" Text="<%$Resources:SysManage,ptConfigSystem %>" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFFFFF" class="newstitlebody">
                            <asp:Literal ID="Literal4" runat="server" Text="<%$Resources:SysManage,lblConfigSystem %>" />
                        </td>
                    </tr>
                </table>
            </div>
            <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border" id="TableAdd" runat="server">
                <tr>
                    <td style="width: 80px" align="center" class="tdbg">
                        <b>
                            <asp:Literal ID="Literal5" runat="server" Text="<%$Resources:SysManage,ptConfigurationAdd %>" />：</b>
                    </td>
                    <td class="tdbg">
                        <table  cellpadding="2" cellspacing="1" >
                            <tr>
                                <td style=" width:50px; text-align:right">
                                Type：
                                </td>
                                <td> 
                                <asp:DropDownList ID="dropConfigType" runat="server"> </asp:DropDownList>
                                <asp:TextBox ID="txtTypeName" runat="server" class="inputtext" Width="100px"></asp:TextBox>
                                <asp:Button ID="btnSaveType" runat="server" Text="<%$Resources:Site,btnSaveText%>" class="adminsubmit_short"
                            OnClick="btnSaveType_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td style=" text-align:right">
                                KeyName：
                                </td>
                                <td><asp:TextBox ID="txtKeyName" runat="server" class="inputtext" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                 <td style=" text-align:right">Value：
                                </td>
                                <td>
                                <asp:TextBox ID="txtValue" runat="server" class="inputtext" Width="200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                 <td style=" text-align:right"><asp:Literal ID="Literal6" runat="server" Text="<%$Resources:SysManage,fieldDescription %>" />：
                                </td>
                                <td>
                                <asp:TextBox ID="txtDescription" runat="server" class="inputtext" Width="400px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                <asp:Button ID="btnSave" runat="server" Text="<%$Resources:Site,btnSaveText%>" class="adminsubmit_short"
                            OnClick="btnSave_Click" />
                        <asp:Label ID="lblToolTip" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                                              
                    </td>
                    <td class="tdbg">
                    </td>
                </tr>
                <tr>
                    <td style="width: 80px" align="left" class="tdbg">
                    </td>
                    <td class="tdbg">                        
                    </td>
                    <td class="tdbg">
                    </td>
                </tr>
            </table>
            <br />
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                        <img src="/Admin/Images/icon-1.gif" width="19" height="19" />
                    </td>
                    <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="<%$Resources:SysManage,lblSearchCriteria%>" />：
                        <asp:DropDownList ID="dropConfigTypeSearch" runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtKeyWord" runat="server" class="inputtext"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="<%$Resources:Site,btnSearchText%>"
                            class="adminsubmit_short" OnClick="btnSearch_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btnRestartApp" runat="server" Text="重启网站" class="adminsubmit"
                            OnClick="btnRestartApp_Click" />
                    </td>
                </tr>
            </table>
            <br />
            <div class="newslist">
                <div class="newsicon">
                    <ul>
                        <%--<li style="background: url(/images/icon8.gif) no-repeat 5px 3px"><a href="add.aspx">
                            <asp:Literal ID="Literal8" runat="server" Text="<%$ Resources:Site,lblAdd%>" /></a>
                            <b>|</b> </li>--%>
                        <%--<li style="background: url(/admin/images/delete.gif) no-repeat ;width:60px;"><a href="javascript:;" onclick="GetDeleteM()"><asp:Literal ID="Literal9" runat="server" Text="<%$ Resources:Site,btnDeleteListText%>"/></a><b>|</b></li>--%>
                    
                    </ul>
                </div>
            </div>
            <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
                ShowToolBar="True" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
                OnRowDataBound="gridView_RowDataBound" OnRowDeleting="gridView_RowDeleting" OnRowCancelingEdit="gridView_RowCancelingEdit"
                OnRowEditing="gridView_RowEditing" OnRowUpdating="gridView_RowUpdating" UnExportedColumnNames="Modify"
                Width="100%" PageSize="20" DataKeyNames="ID" ShowExportExcel="False" ShowExportWord="False"
                ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" ShowCheckAll="False">
                <Columns>
                    <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="true" ItemStyle-Width="80px" />
                    <asp:BoundField DataField="KeyName" HeaderText="KeyName" ItemStyle-Width="150px" />
                    <asp:BoundField DataField="Value" HeaderText="Value" ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="300px"/>
                    <asp:BoundField DataField="Description" HeaderText="<%$ Resources:SysManage,fieldDescription%>"
                        ItemStyle-HorizontalAlign="Left"  ItemStyle-Width="300px" />
                    <asp:TemplateField HeaderText="<%$ Resources:Site, lblOperation %>" ShowHeader="False"
                        ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="300px">
                        <EditItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update"
                                Text="<%$ Resources:Site, btnUpdateText %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel"
                                Text="<%$ Resources:Site, btnCancleText %>"></asp:LinkButton>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False" CommandName="Edit"
                                Text="<%$ Resources:Site, btnEditText %>"></asp:LinkButton>
                            <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Delete"
                                Text="<%$ Resources:Site, btnDeleteText  %>" OnClientClick="return confirm($(this).attr('ConfirmText'))" ConfirmText="<%$Resources:Site,TooltipDelConfirm %>" ></asp:LinkButton>
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
    </div>
    <uc1:copyright ID="Copyright1" runat="server" />
    </form>
</body>
</html>
