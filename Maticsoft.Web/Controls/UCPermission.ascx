<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCPermission.ascx.cs" Inherits="Maticsoft.Web.Controls.UCPermission" %>

<link media="screen" rel="stylesheet" href="/admin/js/colorbox/example1/colorbox.css" />
        <script src="/admin/js/colorbox/jquery.min.js"></script>
        <script src="/admin/js/colorbox/jquery.colorbox.js"></script>
        <script>
            $(document).ready(function () {
                $(".example8").colorbox({ width: "500px", height: "45%", inline: true, href: "#inline_example1" });
                $("#colorbox").appendTo('form');
            });
        </script>
        <script type="text/javascript">
            function save() {
                var item = $("input[name='ctl00$ContentPlaceHolder1$UCDroplistPermission1$radbtnlistPermission']:checked").val();
                //var item = $("table[id^=ctl] input[type=radio]:checked").val();
                //alert(item);
                document.getElementById("ctl00_ContentPlaceHolder1_UCDroplistPermission1_HiddenFieldPermID").value = item;
                $("inline_example1").colorbox.close();
            }
            function closed() {
                $("inline_example1").colorbox.close();
            }
        </script>
         
        <div style='display: none'>                        
            <div id='inline_example1' style='padding:10px; background: #fff; text-align:left'>               
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <b>权限类别：</b><asp:DropDownList ID="droplistPermCategories" runat="server" AutoPostBack="true" >
                </asp:DropDownList>                               
                <br /><b>权限列表：</b><br />
                <asp:RadioButtonList ID="radbtnlistPermission" runat="server" RepeatColumns="3" RepeatDirection="Horizontal" >
                </asp:RadioButtonList>
                </ContentTemplate>            
            </asp:UpdatePanel> 
            
            <br/>   
                <input type="submit" value="确定" onclick="save()" />
                <input type="submit" value="关闭" onclick="closed()" />
            </div>
        </div>
<asp:Label ID="lblPermName" runat="server" Text=""></asp:Label>&nbsp;<a class="example8" style=" color:Blue" href="#">[设置数据权限]</a>
                            <asp:HiddenField ID="HiddenFieldPermID" runat="server" />