using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Maticsoft.TaoBao;

namespace Maticsoft.IDAL.SNS
{
  public  interface ITaoBaoConfig
    {
      ITopClient GetTopClient();
    }
}
