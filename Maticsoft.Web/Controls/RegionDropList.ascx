<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RegionDropList.ascx.cs" Inherits="Maticsoft.Web.Controls.RegionDropList" %>
    <script type="text/javascript">
        //城市------------------------------
        function cityResult() {
            var city = document.getElementById('<%= ddlProvince.ClientID %>');
            AjaxMethod.GetCityList(city.value, get_city_Result_CallBack);
        }

        function get_city_Result_CallBack(response) {
            if (response.value != null) {
           
                document.all('<%= ddlCity.ClientID %>').length = 0;
                var ds = response.value;
                if (ds != null && typeof (ds) == "object" && ds.Tables != null) {

                    document.all('<%= ddlCity.ClientID %>').options.add(new Option("请选择", "-1"));
                    document.all('<%= ddlArea.ClientID %>').length = 0;
                    document.all('<%= ddlArea.ClientID %>').options.add(new Option("请选择", "-1"));
                    for (var i = 0; i < ds.Tables[0].Rows.length; i++) {
                        var name = ds.Tables[0].Rows[i].RegionName;
                        var id = ds.Tables[0].Rows[i].RegionId;
                        document.all('<%= ddlCity.ClientID %>').options.add(new Option(name, id));
                    }
                }

            }
            else {
                document.all('<%= ddlCity.ClientID %>').length = 0;
            }
            return
        }
        //市区----------------------------------------
        function areaResult() {
            var area = document.getElementById('<%= ddlCity.ClientID %>');
            AjaxMethod.GetAreaList(area.value, get_area_Result_CallBack);
        }
        function get_area_Result_CallBack(response) {
            if (response.value != null) {
                document.all('<%= ddlArea.ClientID %>').length = 0;
                var ds = response.value;
                if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
                    document.all('<%= ddlArea.ClientID %>').options.add(new Option("请选择", "-1"));
                    for (var i = 0; i < ds.Tables[0].Rows.length; i++) {
                        var name = ds.Tables[0].Rows[i].RegionName;
                        var id = ds.Tables[0].Rows[i].RegionId;
                        document.all('<%= ddlArea.ClientID %>').options.add(new Option(name, id));
                    }
                }

            }
            else {
                document.all('<%= ddlArea.ClientID %>').length = 0;
            }
            return;
        }

        function uploadImg() {
            alert("未知异常！");
        }
    </script>
<span class="Radselect">
    <asp:DropDownList ID="ddlProvince" runat="server">
        <asp:ListItem Value="">请选择</asp:ListItem>
    </asp:DropDownList>
</span>
<span class="TRstyle">省</span>
    <span class="Radselect">
        <asp:DropDownList ID="ddlCity" runat="server">
            <asp:ListItem Value="">请选择</asp:ListItem>
        </asp:DropDownList>
</span>
<span class="TRstyle">市</span>
<span class="Radselect">
    <asp:DropDownList ID="ddlArea" runat="server">
        <asp:ListItem Value="">请选择</asp:ListItem>
    </asp:DropDownList>
</span>
<span class="TRstyle">区</span>