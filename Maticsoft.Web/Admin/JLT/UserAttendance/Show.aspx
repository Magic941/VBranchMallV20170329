<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true" CodeBehind="Show.aspx.cs" Inherits="Maticsoft.Web.Admin.JLT.UserAttendance.Show" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="newslistabout">
     <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal1" runat="server" Text="查看考勤详细" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal2" runat="server" Text="您可以查看考勤详细内容" />
                    </td>
                </tr>
            </table>
    </div>  
    <br />
     <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="3" width="100%" border="0">

                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal3" runat="server" Text="考勤编号" />：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlAttID" runat="server"/>
                            </td>
                        </tr>
                        <tr style="display:none">
                            <td class="td_class">
                                <asp:Literal ID="Literal12" runat="server" Text="用户编号" />：
                            </td>
                            <td height="25">
                                <asp:Literal ID="ltlUserID" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal4" runat="server" Text="用户名" />：
                            </td>
                            <td height="25">
                                <asp:Literal ID="ltlUserName" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal5" runat="server" Text="真实姓名" />：
                            </td>
                            <td class="25">
                                <asp:Literal ID="ltlTrueName" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal6" runat="server" Text="经度" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlLongitude" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal7" runat="server" Text="纬度" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlLatitude" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal8" runat="server" Text="距离" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlKiloMeters" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal9" runat="server" Text="考勤类型" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlTypeName" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal10" runat="server" Text="创建日期" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlCreatedDate" runat="server"  />
                            </td>
                        </tr>
                         <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal11" runat="server" Text="考勤日期" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlAttDate" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal14" runat="server" Text="考勤描述" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlDescription" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal16" runat="server" Text="图片路径" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlImagePath" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal18" runat="server" Text="考勤评分" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlScore" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal20" runat="server" Text="状态" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlStatus" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal22" runat="server" Text="批复人" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlRevUserName" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal24" runat="server" Text="批复内容" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlRevDescription" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal26" runat="server" Text="批复时间" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlRevDate" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal28" runat="server" Text="批复状态" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlRevStatus" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                                <asp:Literal ID="Literal30" runat="server" Text="备注" />：
                            </td>
                            <td class="25">
                               <asp:Literal ID="ltlRemark" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnReturn" runat="server" CausesValidation="false" Text="返回"
                                    OnClick="btnReturn_Click" class="adminsubmit_short" TabIndex="9"></asp:Button>
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
