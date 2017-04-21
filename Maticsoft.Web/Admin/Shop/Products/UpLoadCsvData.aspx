<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/BasicNoFoot.Master" CodeBehind="UpLoadCsvData.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.UpLoadCsvData" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

      <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="商品CSV" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="点击上传CSV文件" />
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
                              <asp:FileUpload ID="uploadCsv" runat="server" />

                              &nbsp;

                              <asp:Button ID="btnUpload" runat="server" Text="上传CSV" CssClass="adminsubmit" 
                                    onclick="btnUpload_Click" />
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
