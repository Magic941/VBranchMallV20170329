<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Admin/BasicNoFoot.Master" CodeBehind="DownCsvPattern.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.DownCsvPattern" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="商品CSV模版" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您下载商品CSV模版" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        
                        
                        <tr>
                            <td class="td_class" height="30">
                            </td>
                            <td height="25">
                                <asp:Button ID="btnDownLoad" runat="server" Text="下载Csv文件" class="adminsubmit" 
                                    onclick="btnDownLoad_Click"></asp:Button>
                                    
                                   （填写好数据后） <asp:Button ID="Button1" runat="server" Text="下一步" class="adminsubmit" 
                                    onclick="btnNext_Click"></asp:Button>
                            </td>
                             <td height="25">
                              
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
