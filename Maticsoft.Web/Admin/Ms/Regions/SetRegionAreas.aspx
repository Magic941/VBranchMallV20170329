<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/BasicNoFoot.Master" AutoEventWireup="true" CodeBehind="SetRegionAreas.aspx.cs" Inherits="Maticsoft.Web.Admin.Ms.Regions.SetRegionAreas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="/Scripts/select2/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/select2/select2.min.js" type="text/javascript"></script>
    <script src="/Scripts/json2.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id$="dropRegion"]').attr('multiple', 'true');
           
            //回发
            if (IsPostBack()) {
                $('[id$="dropRegion"]').val(eval('[' + $('[id$="hidRegionValue"]').val() + ']')).select2(); //回发后值下拉框中的值保持不变
            } else {//首次加载
                $('[id$="dropRegion"]').val(eval('[' + $('[id$="hidRegionIDsLoad"]').val() + ']')).select2(); //加载数据库中的值
            }
            if ($('[id$="hidColse"]').val() == "12") {
                setTimeout(function () {
                    javascript: parent.$.colorbox.close();
                }, 1000);
            }
        });
        
        //IspostBack
        function IsPostBack() {
            var IsPostBack = "<%=IsPostBack%>";
            if (IsPostBack =="True") {
                return true;
            } else {
                return false;
            }
        }
        
        function SubmitselectValue() { //确定按钮触发
            $('[id$="hidRegionValue"]').val($('[id$="dropRegion"]').select2("val"));   
        }
    </script>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:HiddenField ID="hidColse" runat="server" /> <!--如果有值 就直接关闭窗口-->
         <asp:HiddenField ID="hidRegionIDsLoad" runat="server" />
         <asp:HiddenField ID="hidRegionValue" runat="server" />
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="设置地区" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        <asp:Literal ID="Literal3" runat="server" Text="您可以设置地区" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td class="td_class" valign="top">
                                设置地区：
                            </td>
                            <td height="25">
                                <p style="text-align:left;  ">
       
         <asp:DropDownList ID="dropRegion" runat="server"    Width="350px" > </asp:DropDownList>
         </p>
                            </td>
                        </tr>
                        <tr>
                            <td class="td_class" height="30">
                            </td>
                            <td height="25">
                            <asp:Button  ID="SaveBut" runat="server" Text="保存"  class="adminsubmit_short"   OnClientClick="SubmitselectValue();" onclick="SaveBut_Click"/>
       
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    
    
    
    
    
    

   
      <%--  <input type="button" id="SaveBut1" value="保存" onclick="SubmitselectValue();"/>--%>
      
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
