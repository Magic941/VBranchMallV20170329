/**
* ProdCompareModel.cs
*
* 功 能： [N/A]
* 类 名： ProdCompareModel
*
* Ver    变更日期             负责人  变更内容
* ───────────────────────────────────
* V0.01  2013/11/26 10:40:20  Rock    初版
*
* Copyright (c) 2013 Maticsoft Corporation. All rights reserved.
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：动软卓越（北京）科技有限公司　　　　　　　　　　　　　　│
*└──────────────────────────────────┘
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maticsoft.ViewModel.Shop
{
    public class ProdCompareModel
    {
        // public List<string> ValueStr { get; set;  }
        //  public Dictionary<long, string[]> Data { get; set; }
        public long ProductId { get; set; }
        public long AttributeId { get; set; }
        public int ValueId { get; set; }
        public string ValueStr { get; set; }
    }
}
