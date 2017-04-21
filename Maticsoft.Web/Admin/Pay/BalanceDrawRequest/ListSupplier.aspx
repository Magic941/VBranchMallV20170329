<%@ Page Title="申请提现记录" Language="C#" MasterPageFile="~/Admin/Basic.Master"
    AutoEventWireup="true" CodeBehind="ListSupplier.aspx.cs" Inherits="Maticsoft.Web.Admin.Pay.BalanceDrawRequest.ListSupplier" %>

<%@ Register Assembly="Maticsoft.Web" Namespace="Maticsoft.Web.Controls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link href="/Admin/js/colorbox/colorbox.css" rel="stylesheet" type="text/css" />
    <script src="/Admin/js/colorbox/jquery.colorbox-min.js" type="text/javascript"></script>
    <script type="text/javascript">    
        $(function() {
            $("span:contains('未审核')").css("color", "#d71345");
            $("span:contains('审核失败')").css("color", "#f47920");
            $("span:contains('审核通过')").css("color", "#006400");
            
            $(".iframe").colorbox({ iframe: true, width: "800", height: "560", overlayClose: false });
        });
        function GetDeleteM() {
            $("[id$='btnDelete']").click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!--Title -->
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                         <asp:Literal ID="Literal1" runat="server" Text="商家提现记录" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以审核 <asp:Literal ID="Literal3" runat="server" Text="商家提现记录" />
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
                <img src="../../Images/icon-1.gif" width="19" height="19" />
            </td>
            <td height="35" bgcolor="#FFFFFF" class="newstitlebody">
                状态：<asp:DropDownList ID="dropStatusSearch" runat="server">
                    <asp:ListItem Value="0">请选择</asp:ListItem>
                    <asp:ListItem Value="1">未审核</asp:ListItem>
                    <asp:ListItem Value="2">审核失败</asp:ListItem>
                    <asp:ListItem Value="3">审核通过</asp:ListItem>
                </asp:DropDownList>
                <asp:Literal ID="lblName" runat="server" Text="商家名称" />：
                <asp:TextBox ID="txtKeyword" runat="server"></asp:TextBox>
                <asp:Button ID="btnSearch" runat="server" Text="<%$ Resources:Site, btnSearchText %>"
                    OnClick="btnSearch_Click" class="adminsubmit"></asp:Button>
            </td>
        </tr>
    </table>
    <!--Search end-->
    <br />
    <div class="newslist">
        <div class="newsicon">
            <ul>       
                <li id="liDel" runat="server" style="background: url(/admin/images/delete.gif) no-repeat"><a href="javascript:;" onclick="GetDeleteM();">删除</a><b>|</b></li>
            </ul>
        </div>
    </div>
    <cc1:GridViewEx ID="gridView" runat="server" AllowPaging="True" AllowSorting="True"
        ShowToolBar="false" AutoGenerateColumns="False" OnBind="BindData" OnPageIndexChanging="gridView_PageIndexChanging"
        OnRowDataBound="gridView_RowDataBound"  UnExportedColumnNames="Modify"
        Width="100%" PageSize="10" ShowExportExcel="True" ShowExportWord="False" ExcelFileName="FileName1"
        CellPadding="3" BorderWidth="1px" ShowCheckAll="true" DataKeyNames="JournalNumber" OnRowCommand="gridView_RowCommand" >
        <columns>
        <asp:TemplateField ItemStyle-Width="140" HeaderText="请求时间" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                         <span><%# Convert.ToDateTime(Eval("RequestTime")).ToString("yyyy-MM-dd HH:mm:ss")%></span> 
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="80" HeaderText="金额" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <span>
                       ￥<%# Convert.ToDecimal(Eval("Amount")).ToString("F")%></span>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField   HeaderText="商家名称" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>
                    <span>
                        <%#Eval("Name")%></span>
                </ItemTemplate>
            </asp:TemplateField>
		<asp:BoundField DataField="BankCard" HeaderText="帐号" SortExpression="BankCard" ItemStyle-HorizontalAlign="Center"  /> 
		 <asp:TemplateField ItemStyle-Width="80" HeaderText="帐号类型" ItemStyle-HorizontalAlign="Center"   >
                    <ItemTemplate>
                         <span><%#GetCardType((int)Eval("CardTypeID"))%></span> 
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField ItemStyle-Width="60" HeaderText="状态" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>                    
                          <span > <%#GetDrawState(Eval("RequestStatus"))%></span>                   
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ItemStyle-Width="180" HeaderText="操作" ItemStyle-HorizontalAlign="Center">
                <ItemTemplate>                    
                     <asp:LinkButton Visible="false"  ID="lbtnStatusFail" runat="server" CommandArgument='<%#Eval("JournalNumber")+",2"%>'
                            Style="color: #0063dc;" CommandName="Status">审核失败</asp:LinkButton>  
                         <asp:LinkButton Visible="false"  ID="lbtnStatusSuccess" runat="server" CommandArgument='<%#Eval("JournalNumber")+",3"%>'
                            Style="color: #0063dc;" CommandName="Status">审核通过</asp:LinkButton>                 
                  <span > <a class='iframe' href="Show.aspx?id=<%#Eval("JournalNumber") %> ">
                        <asp:Literal ID="Literal11" runat="server" Text="<%$ Resources:Site, btnDetailText %>" /></a></span> 
                    <span id="btnEdit">   <a class='iframe' href="Modify.aspx?id=<%#Eval("JournalNumber") %> ">
                        <asp:Literal ID="Literal12" runat="server" Text="<%$ Resources:Site, btnEditText %>" /></a></span>
                </ItemTemplate>                
            </asp:TemplateField>
        </columns>
        <footerstyle height="25px" horizontalalign="Right" />
         <HeaderStyle Height="35px" />
        <pagerstyle height="25px" horizontalalign="Right" />
        <sorttip ascimg="~/Images/up.JPG" descimg="~/Images/down.JPG" />
        <rowstyle height="25px" />
        <sortdirectionstr>DESC</sortdirectionstr>
    </cc1:GridViewEx>
    <table border="0" cellpadding="0" cellspacing="1" style="width: 100%; height: 100%;">
        <tr>
            <td style="width: 1px;">
            </td>
            <td>
                <asp:Button ID="btnDelete" runat="server" Text="<%$ Resources:Site, btnDeleteListText %>"  class="adminsubmit" OnClick="btnDelete_Click" /> 
                
            </td>
        </tr>
    </table></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
