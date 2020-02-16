using System;
namespace Model
{
    /// <summary>
	///LoanRecored表实体模型
	/// 作者:[Lin Moli]
	/// 创建时间:2020-02-16 14:21:34
	/// </summary>
    [Serializable]
    public partial class LoanRecoredModel : BasePModel, IBaseModel
    {
        public LoanRecoredModel() { }

        private int _recordType;
        /// <summary>
        /// 
        /// </summary>
        public int recordType
        {
            set { _recordType = value; }
            get { return _recordType; }
        }

        private string _orderId;
        /// <summary>
        /// 
        /// </summary>
        public string orderId
        {
            set { _orderId = value; }
            get { return _orderId; }
        }

        private string _topOrderId;
        /// <summary>
        /// 
        /// </summary>
        public string topOrderId
        {
            set { _topOrderId = value; }
            get { return _topOrderId; }
        }

        private string _title;
        /// <summary>
        /// 
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }

        private string _orderObject;
        /// <summary>
        /// 
        /// </summary>
        public string orderObject
        {
            set { _orderObject = value; }
            get { return _orderObject; }
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

        private string _interestRateType;
        /// <summary>
        /// 
        /// </summary>
        public string interestRateType
        {
            set { _interestRateType = value; }
            get { return _interestRateType; }
        }

        private string _interestRate;
        /// <summary>
        /// 
        /// </summary>
        public string interestRate
        {
            set { _interestRate = value; }
            get { return _interestRate; }
        }

        private DateTime? _endTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime? endTime
        {
            set { _endTime = value; }
            get { return _endTime; }
        }

        private string _nowAmount;
        /// <summary>
        /// 
        /// </summary>
        public string nowAmount
        {
            set { _nowAmount = value; }
            get { return _nowAmount; }
        }

        private int _isFinish;
        /// <summary>
        /// 
        /// </summary>
        public int isFinish
        {
            set { _isFinish = value; }
            get { return _isFinish; }
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
