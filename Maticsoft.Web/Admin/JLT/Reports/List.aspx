<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="List.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.Reports.List" %>
<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
<script src="/Admin/js/jqueryui/jquery-ui-1.8.19.custom.min.js"></script>
    <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $(".imageinfo").colorbox({ rel: 'imageinfo', transition: "fade" });
      
        //绑定日期控件
        var today = new Date();
        var year = today.getFullYear();
        var month = today.getMonth();
        var day = today.getDate();
        $("[id$='txtDate']").prop("readonly", true).datepicker({
            numberOfMonths: 1, //显示月份数量
            onClose: function () {
                $(this).css("color", "#000");
            }
        }).focus(function () { $(this).val(''); });
    });

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
    <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="简报管理" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal4" runat="server" Text="您可以查看简报" />
                    </td>
                </tr>
            </table>
    </div>     
    <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
            <tr>
                <td width="1%" height="30" bgcolor="#FFFFFF" class="newstitlebody">
                    <img src="../../Images/icon-1.gif" width="19" height="19" />
                </td>
                <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                    
                    <asp:Literal ID="Literal2" runat="server" Text="<%$ Resources:Site, lblSearch%>" />：
                  简报类型：<asp:DropDownList ID="ddType" runat="server">
                           <asp:ListItem Value="">全部</asp:ListItem>
                           <asp:ListItem Value="0">文字</asp:ListItem>
                           <asp:ListItem Value="1">图片</asp:ListItem>
                           <asp:ListItem Value="2">语音</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Literal ID="Literal7" runat="server" Text="用户名" />：
                    <asp:TextBox ID="txtUserName" runat="server" class="admininput_1"></asp:TextBox>
                    <asp:Literal ID="Literal9" runat="server" Text="提交日期" />：
                    <asp:TextBox ID="txtDate" runat="server" class="admininput_1" ></asp:TextBox>
                    <asp:Label ID="Label1" runat="server"> 
                    <asp:Literal ID="Literal3" runat="server" Text="简报内容" />:</asp:Label>
                    <asp:TextBox ID="txtKeyword" runat="server" class="admininput_1"></asp:TextBox>&nbsp 
                    <asp:Button ID="btnSearch" runat="server" 
                                Text="<%$ Resources:Site, btnSearchText %>" 
                                OnClick="btnSearch_Click"
                                class="adminsubmit_short">
                    </asp:Button>
                </td>

            </tr>
    </table>
    <br />
    <div class="newslist">
        <div class="newsicon">
            <ul>
                <%--<li style="background: url(/images/icon8.gif) no-repeat 5px 3px" id="liAdd" runat="server"><a href="add.aspx">
                    <asp:Literal ID="Literal5" runat="server" Text="<%$ Resources:Site, lblAdd%>" /></a>
                    <b>|</b> </li>--%><li style="background: url(/admin/images/list.gif) no-repeat"><a href="list.aspx">
                    <asp:Literal ID="Literal6" runat="server" Text="<%$ Resources:Site, lblScan%>" /></a><b>|</b></li> <%--<li style="background: url(/admin/images/reload.png) no-repeat"><a href="#">
                    <asp:Literal ID="Literal7" runat="server" Text="返回" /></a><b>|</b></li>--%></ul></div></div><cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
            ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
            OnRowDataBound="gridView_RowDataBound" 
        OnRowDeleting="gridView_RowDeleting" UnExportedColumnNames="Modify"
            Width="100%" PageSize="10" DataKeyNames="ID" ShowExportExcel="True" ShowExportWord="False"
            ExcelFileName="FileName1" CellPadding="3" BorderWidth="1px" 
        ShowCheckAll="true">
            <Columns>
                <asp:TemplateField HeaderText="编号" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="40px">
                    <ItemTemplate>
                            <%#Eval("ID")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="简报内容" ItemStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                            <%#Eval("Content")%>
                            <%# ImageSource(Eval("FileNames"), Eval("FileDataPath"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="提交日期" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180px">
                    <ItemTemplate>
                            <%# Eval("CreatedDate")%>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="用户名" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="100px">
                    <ItemTemplate>
                            <%#GetUserName( Eval("UserId"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="简报类型" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                    <ItemTemplate>
                            <%#GetType( Eval("Type"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="附件信息" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <%# FileSource(Eval("FileNames"), Eval("FileDataPath"))%>
                    </ItemTemplate>
                </asp:TemplateField>
                
                <asp:HyperLinkField HeaderText="详细" ItemStyle-Width="50px"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Show.aspx?id={0}"
                    Text="详细" ItemStyle-HorizontalAlign="Center" />
                <asp:HyperLinkField HeaderText="<%$ Resources:Site, btnEditText %>" ItemStyle-Width="50px"
                    DataNavigateUrlFields="ID" DataNavigateUrlFormatString="Modify.aspx?id={0}"
                    Text="<%$ Resources:Site, btnEditText %>" ItemStyle-HorizontalAlign="Center" Visible="False"/>
                <asp:TemplateField ItemStyle-Width="50px" HeaderText="<%$ Resources:Site, btnDeleteText %>"
                    ItemStyle-HorizontalAlign="Center" Visible="False">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete"
                            Text="<%$ Resources:Site, btnDeleteText %>">
                        </asp:LinkButton></ItemTemplate></asp:TemplateField></Columns><FooterStyle Height="25px" HorizontalAlign="Right" />
            <HeaderStyle Height="35px" />
            <PagerStyle Height="25px" HorizontalAlign="Right" />
            <SortTip AscImg="~/Images/up.JPG" DescImg="~/Images/down.JPG" />
            <RowStyle Height="25px" />
            <SortDirectionStr>DESC</SortDirectionStr></cc1:gridviewex>
</div>
    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
            <tr>
                <td height="10px;">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="width: 1px;">
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" Text="批量删除"
                        class="adminsubmit" OnClick="btnDelete_Click" />
                 </td>
            </tr>
        </table>
   </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
