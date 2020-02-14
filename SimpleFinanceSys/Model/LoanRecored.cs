using System;
namespace Model
{
    /// <summary>
    ///LoanRecored表实体模型
    /// 
    /// 创建时间:2020-02-03 10:20:55
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

        private DateTime _endTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime endTime
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
