using System;
namespace Model
{
    /// <summary>
    ///Base_Type表实体模型
    /// 
    /// 创建时间:2020-02-03 10:20:56
    /// </summary>
    [Serializable]
    public partial class Base_TypeModel : BasePModel, IBaseModel
    {
        public Base_TypeModel() { }

        private string _name;
        /// <summary>
        /// 
        /// </summary>
        public string name
        {
            set { _name = value; }
            get { return _name; }
        }

        private int _sort;
        /// <summary>
        /// 
        /// </summary>
        public int sort
        {
            set { _sort = value; }
            get { return _sort; }
        }

    }
}
