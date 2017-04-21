using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

public class AjaxMethod
{
    Maticsoft.BLL.Ms.Regions bll = new Maticsoft.BLL.Ms.Regions();
    
    #region 地区DropDownlist
    public DataSet GetPovinceList()
    {
        DataSet dsProvice = bll.GetProvinces();
        return dsProvice;
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
    public DataSet GetCityList(int povinceid)
    {
        DataSet dsCity = bll.GetCitys(povinceid);
        return dsCity;
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
    public DataSet GetAreaList(int povinceid)
    {
        DataSet dsCity = bll.GetCitys(povinceid);
        return dsCity;
    }

    [Ajax.AjaxMethod(Ajax.HttpSessionStateRequirement.Read)]
    public DataTable GetParentId(int id)
    {
        DataTable dtPar = bll.GetParentId(id);
        return dtPar;
    }
    #endregion
}