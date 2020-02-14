using System;
namespace Model
{
    /// <summary>
	///Base_Object表实体模型
	/// 作者:[Lin Moli]
	/// 创建时间:2020-02-14 19:11:02
	/// </summary>
    [Serializable]
	public partial class Base_ObjectModel : BasePModel, IBaseModel
    {
		public Base_ObjectModel(){}
		
		private string _name ;
		/// <summary>
		/// 
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		
		private string _explain ;
		/// <summary>
		/// 
		/// </summary>
		public string explain
		{
			set{ _explain=value;}
			get{return _explain;}
		}
		
		private int _sort ;
		/// <summary>
		/// 
		/// </summary>
		public int sort
		{
			set{ _sort=value;}
			get{return _sort;}
		}

	}
}
