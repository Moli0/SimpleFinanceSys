using System;
namespace Model
{
    /// <summary>
    ///UserInfo表实体模型
    /// 
    /// 创建时间:2020-02-03 10:20:57
    /// </summary>
    [Serializable]
    public partial class UserInfoModel : BasePModel, IBaseModel
    {
        public UserInfoModel() { }

        private string _username;
        /// <summary>
        /// 
        /// </summary>
        public string username
        {
            set { _username = value; }
            get { return _username; }
        }

        private string _userid;
        /// <summary>
        /// 
        /// </summary>
        public string userid
        {
            set { _userid = value; }
            get { return _userid; }
        }

        private string _email;
        /// <summary>
        /// 
        /// </summary>
        public string email
        {
            set { _email = value; }
            get { return _email; }
        }

        private string _pwd;
        /// <summary>
        /// 
        /// </summary>
        public string pwd
        {
            set { _pwd = value; }
            get { return _pwd; }
        }

        private string _headUrl;
        /// <summary>
        /// 
        /// </summary>
        public string headUrl
        {
            set { _headUrl = value; }
            get { return _headUrl; }
        }

        private string _dayMaxPay;
        /// <summary>
        /// 
        /// </summary>
        public string dayMaxPay
        {
            set { _dayMaxPay = value; }
            get { return _dayMaxPay; }
        }

        private string _monthMaxPay;
        /// <summary>
        /// 
        /// </summary>
        public string monthMaxPay
        {
            set { _monthMaxPay = value; }
            get { return _monthMaxPay; }
        }

        private DateTime _lastSignTime;
        /// <summary>
        /// 
        /// </summary>
        public DateTime lastSignTime
        {
            set { _lastSignTime = value; }
            get { return _lastSignTime; }
        }

        private int _tipsState;
        /// <summary>
        /// 
        /// </summary>
        public int tipsState
        {
            set { _tipsState = value; }
            get { return _tipsState; }
        }

        private string _baseAmount;
        /// <summary>
        /// 
        /// </summary>
        public string baseAmount
        {
            set { _baseAmount = value; }
            get { return _baseAmount; }
        }

        private string _nowMoney;
        /// <summary>
        /// 
        /// </summary>
        public string nowMoney
        {
            set { _nowMoney = value; }
            get { return _nowMoney; }
        }

    }
}
