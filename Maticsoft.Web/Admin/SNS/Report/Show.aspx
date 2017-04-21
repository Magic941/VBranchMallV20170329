<%@ Page Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true"
    CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Report.Show" Title="显示页" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="动态详细信息" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以查看被举报的动态详细信息" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" border="0">
                        <tr>
                            <td class="td_classshow">
                                举报类型：
                            </td>
                            <td>
                                <asp:Label ID="lblType" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                举报描述 ：
                            </td>
                            <td height="40">
                                <asp:Label ID="lblDesc" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                                举报内容 ：
                            </td>
                            <td height="40">
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td class="td_classshow">
                            </td>
                            <td height="120">
                               <asp:Image ID="lblImage" runat="server"  Height="120px" Visible="false"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_classshow">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnReportTrue" runat="server" Text="举报属实" class="adminsubmit" ToolTip="举报属实，删除发布的内容"
                                    OnClick="btnReportTrue_Click" />
                       
                                <asp:Button ID="btnReportFalse" runat="server" Text="虚假举报" class="adminsubmit" ToolTip="忽略用户虚假举报"
                                    OnClick="btnReportFalse_Click" />
                      
                                <asp:Button ID="btnReportUnKnow" runat="server" Text="核实中" class="adminsubmit" ToolTip="举报内容待核实"
                                    OnClick="btnReportUnKnow_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
