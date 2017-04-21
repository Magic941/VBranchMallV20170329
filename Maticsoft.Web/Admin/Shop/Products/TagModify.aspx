<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Admin/Basic.Master" CodeBehind="TagModify.aspx.cs" Inherits="Maticsoft.Web.Admin.Shop.Products.TagModify" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/maticsoft.jquery.min.js" type="text/javascript"></script>
    <link href="/Scripts/jqueryui/jquery-ui-1.8.19.custom.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/jqueryui/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="/admin/js/jqueryui/JqueryDataPicker4CN.js" type="text/javascript"></script>

    <script type="text/javascript">
        function closefrm(flag) {
            if (flag == 0) {
                alert('商品标签保存失败');
                return false;
            }
            else {
                alert('商品标签保存成功');
                parent.closeWindown();
            }
        

        }
    </script>
    <style type="text/css">
        .style1
        {
            width: 32px;
        }
        .style2
        {
            width: 72px;
        }
    </style>
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:HiddenField ID="hidPid" runat="server" />
    <div class="newslistabout">

      
        <br />
        <table border="0" cellspacing="1" style="width: 100%; height: 100%;" cellpadding="3"
            class="borderkuang" id="tabTask">
            <tr>
                <td height="18px;" style="width: 80px">
                </td>
                <td colspan="2">
                    <h3>
                        商品标签生成</h3>
                </td>
            </tr>
            <tr>
                <td style="width: 8px;">
                </td>
                <td style="width: 80px">
                    分类栏目：
                </td>
                <td style="text-align: left">
                    <asp:DropDownList ID="dropParentID" runat="server" Width="200px" name="Cid">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 8px;">
                </td>
                <td>
                    标签描述：
                </td>
                <td style="text-align: left">
                 
                    <asp:TextBox ID="txttag" runat="server" style="width: 600px"  ></asp:TextBox>
                    注：标签之间用','分割开
                </td>
            </tr>
             <tr>
                <td style="width: 8px;">
                </td>
                <td style="width: 80px">
                    操作类型：
                </td>
                <td style="text-align: left">
                   <asp:RadioButtonList ID="isedittype" runat="server" RepeatDirection="Horizontal" >
                       <asp:ListItem Text="按商品勾选更新标签" Value="0" ></asp:ListItem>
                       <asp:ListItem Text="按商品分类更新标签" Value="1" ></asp:ListItem>
                   </asp:RadioButtonList>
                </td>
            </tr>
             <tr>
                <td style="width: 8px;">
                </td>
                <td style="width: 80px">
                    更新方式：
                </td>
                <td style="text-align: left">
                   <asp:RadioButtonList ID="rbltag" runat="server" RepeatDirection="Horizontal" >
                       <asp:ListItem Text="标签覆盖" Value="0"  ></asp:ListItem>
                       <asp:ListItem Text="标签追加" Value="1" Selected="True" ></asp:ListItem>
                   </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td style="width: 8px;">
                </td>
                <td>
                </td>
                <td style="text-align: left">
                 
                    <asp:Button ID="Button1" runat="server" Text=" 保  存 "  OnClick="btnSave_Click"/>
                
                </td>
            </tr>
            <tr style="height: 60px">
                <td colspan="3" id="probar" style="display: none;">
                    <div id="progressbar" style="width: 560px;">
                    </div>
                    <div style="width: 560px; text-align: center">
                        <span id="txtCount"></span>/共<span id="txtTotalCount"></span></div>
                </td>
            </tr>
        </table>
        <br />
       
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceCheckright" runat="server">
</asp:Content>
