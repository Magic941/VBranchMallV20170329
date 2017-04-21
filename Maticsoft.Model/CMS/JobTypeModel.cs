using System;

namespace Maticsoft.Model.CMS
{
	/// <summary>
    /// { "n": "农牧业", "c": "L1GS11", "pC": "0", "iCC": "JGS", "id": 2 }
	/// </summary>
	[Serializable]
	public partial class JobTypeModel
	{
        //public JobTypeModel()
        //{}
		#region Model
		


        /// <summary>
        /// 喜欢数
        /// </summary>
        public int id
        {
            get;
            set;
        }

        public string n
        {
            get;
            set;
        }

        public string c
        {
            get;
            set;
        }

        public string pC
        {
            get;
            set;
        }

        public string iCC
        {
            get;
            set;
        }

		#endregion Model

	}
}

