<%@ Page Language="C#" MasterPageFile="~/Admin/Basic.Master" AutoEventWireup="true"
    CodeBehind="Modify.aspx.cs" Inherits="Maticsoft.Web.Admin.SNS.Photos.Modify"
    Title="修改页" %>

<%@ Register Src="~/Controls/UCDroplistPermission.ascx" TagName="UCDroplistPermission"
    TagPrefix="uc2" %>
    <%@ Register src="~/Controls/SNSPhotoCateDropList.ascx" tagname="PhotoCategoryDropList" tagprefix="Maticsoft" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="/Scripts/jquery/jquery.autosize-min.js" type="text/javascript"></script>
    <script src="/Scripts/jquery/maticsoft.jquery.dynatextarea.js" type="text/javascript"></script>
    <link href="/Scripts/select2/select2.css" rel="stylesheet" type="text/css" />
    <script src="/Scripts/select2/select2.min.js" type="text/javascript"></script>
    <script type="text/javascript">
//        $(function () {
//            jQuery.validator.unobtrusive.parse();
//        });

        $(function () {
            $("#ctl00_ContentPlaceHolder1_ddlTags").attr("multiple", "true");
            //            $("#ctl00_ContentPlaceHolder1_ddlTags").val([]);
            //$("#ctl00_ContentPlaceHolder1_ddlTags").val([1,2]).select2();
            $("#ctl00_ContentPlaceHolder1_ddlTags").val([<%=TagsValue%>]).select2({
                placeholder: "请选择标签",
                allowClear: true
            });
            $("#ctl00_ContentPlaceHolder1_Button1").click(function () {
                $("#ctl00_ContentPlaceHolder1_HidTags").val($("#ctl00_ContentPlaceHolder1_ddlTags").val());
            });
        });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="newslistabout">
        <div class="newslist_title">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="borderkuang">
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitle">
                        <asp:Literal ID="Literal2" runat="server" Text="照片表编辑" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#FFFFFF" class="newstitlebody">
                        您可以编辑<asp:Literal ID="Literal3" runat="server" Text="照片表" />
                    </td>
                </tr>
            </table>
        </div>
        <table style="width: 100%;" cellpadding="2" cellspacing="1" class="border">
            <tr>
                <td class="tdbg">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        
                        
                        <tr>
                            <td height="35" width="30%" align="right">
                                正常的图片地址 ：
                            </td>
                            <td height="35" width="*" align="left">
                                <asp:Image ID="txtImage" runat="server"  />
                            </td>
                        </tr>
                        <tr>
                            <td height="35" width="30%" align="right">
                                用户名称 ：
                            </td>
                            <td height="35" width="*" align="left">
                                <asp:Label ID="txtCreatedNickName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                            <tr>
                            <td height="35" width="30%" align="right">
                                喜欢的次数 ：
                            </td>
                            <td height="35" width="*" align="left">
                                    <asp:Label ID="txtFavouriteCount" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="35" width="30%" align="right">
                                评论的次数 ：
                            </td>
                            <td height="35" width="*" align="left">
                                    <asp:Label ID="txtCommentCount" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="35" width="30%" align="right">
                                描述 ：
                            </td>
                            <td height="35" width="*" align="left">
                                <asp:TextBox ID="txtDescription" runat="server" Width="200px" TextMode=MultiLine Rows=5></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td height="35" width="30%" align="right">
                                状态：
                            </td>
                            <td height="35" width="*" align="left">
                               <asp:RadioButtonList ID="radlState" runat="server" RepeatDirection="Horizontal" align="left">
                                                <asp:ListItem Value="0" Text="未审核" ></asp:ListItem>
                                                <asp:ListItem Value="1" Text="已审核" Selected="True"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="审核未通过"></asp:ListItem>
                                            </asp:RadioButtonList>
                            </td>
                        </tr>
                        
                        <%--<tr>
                            <td height="35" width="30%" align="right">
                                创建时间 ：
                            </td>
                            <td height="35" width="*" align="left">
                                <asp:TextBox ID="txtCreatedDate" runat="server" Width="70px" onfocus="setday(this)"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr style=" display:none">
                            <td height="35" width="30%" align="right">
                                分类 ：
                            </td>
                            <td height="35" width="*" align="left">
                                 <Maticsoft:PhotoCategoryDropList ID="PhotoCategory" runat="server" IsNull="true" />
                            </td>
                        </tr>
                        <tr>
                            <td height="35" width="30%" align="right">
                                是否推荐 ：
                            </td>
                            <td height="35" width="*" align="left">
                                  <asp:RadioButtonList ID="rabRecomend" runat="server" RepeatDirection="Horizontal" align="left">
                                                <asp:ListItem Value="0" Text="不推荐" ></asp:ListItem>
                                                <asp:ListItem Value="1" Text="推荐到首页" ></asp:ListItem>  
                                                <asp:ListItem Value="2" Text="推荐到频道首页" Selected="True"></asp:ListItem>
                                 </asp:RadioButtonList>
                            </td>
                        </tr>
             
                           
                        <tr style=" margin-top:10px">
                            <td height="35" width="30%" align="right">
                                标签 ：
                            </td>
                            <td height="35" width="*" align="left">
                                <asp:DropDownList ID="ddlTags" runat="server" CssClass="populate" Width="400px">
                                </asp:DropDownList>
                
                                <asp:HiddenField ID="HidTags" 
                                    runat="server" />
                                <%--<select multiple="" name="e9" id="e9" style="width: 300px; display: none;" class="populate">
                                    <option value="AZ">Arizona</option>
                                    <option value="CO">Colorado</option>
                                    <option value="ID">Idaho</option>
                                    <option value="MT">Montana</option>
                                    <option value="NE">Nebraska</option>
                                    <option value="NM">New Mexico</option>
                                    <option value="ND">North Dakota</option>
                                    <option value="UT">Utah</option>
                                    <option value="WY">Wyoming</option>
                                </select>--%>
                            </td>
                        </tr>
                        <%--<tr>
                            <td height="35" width="30%" align="right">
                                标签 ：
                            </td>
                            <td height="35" width="*" align="left">
                                <asp:TextBox ID="txtTags" runat="server" Width="200px"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td class="td_class">
                            </td>
                            <td height="50">
                                <asp:Button ID="Button1" runat="server" Text="<%$ Resources:Site, btnSaveText %>"
                                    OnClick="btnSave_Click" class="adminsubmit_short" OnClientClick="return PageIsValid();">
                                </asp:Button>
                                <asp:Button ID="Button2" runat="server" Text="<%$ Resources:Site, btnCancleText %>"
                                    OnClick="btnCancle_Click" class="adminsubmit_short" ValidationGroup="A"></asp:Button>
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
