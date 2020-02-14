using System;
namespace Model
{
    /// <summary>
    ///ChangeRecord表实体模型
    /// 
    /// 创建时间:2020-02-03 10:20:56
    /// </summary>
    [Serializable]
    public partial class ChangeRecordModel : BasePModel, IBaseModel
    {
        public ChangeRecordModel() { }

        private string _orderId;
        /// <summary>
        /// 
        /// </summary>
        public string orderId
        {
            set { _orderId = value; }
            get { return _orderId; }
        }

        private int _orderType;
        /// <summary>
        /// 
        /// </summary>
        public int orderType
        {
            set { _orderType = value; }
            get { return _orderType; }
        }

        private string _payType;
        /// <summary>
        /// 
        /// </summary>
        public string payType
        {
            set { _payType = value; }
            get { return _payType; }
        }

        private string _payObject;
        /// <summary>
        /// 
        /// </summary>
        public string payObject
        {
            set { _payObject = value; }
            get { return _payObject; }
        }

        private string _beforeAmount;
        /// <summary>
        /// 
        /// </summary>
        public string beforeAmount
        {
            set { _beforeAmount = value; }
            get { return _beforeAmount; }
        }

        private string _afterAmount;
        /// <summary>
        /// 
        /// </summary>
        public string afterAmount
        {
            set { _afterAmount = value; }
            get { return _afterAmount; }
        }

        private string _amount;
        /// <summary>
        /// 
        /// </summary>
        public string amount
        {
            set { _amount = value; }
            get { return _amount; }
        }

        private string _remark;
        /// <summary>
        /// 
        /// </summary>
        public string remark
        {
            set { _remark = value; }
            get { return _remark; }
        }

    }
}
