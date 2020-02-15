using Dapper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DAL
{
     /// <summary>
    /// 数据表[ChangeRecord]操作类
    /// </summary>
    public partial class ChangeRecordDAL : BaseDal
	{
        public DataSet GetYearForBill(string cond) {
            if (string.IsNullOrEmpty(cond)) {
                cond = " 1=1 ";
            }
            string sql = string.Format(@"select Left(Convert(varchar(100),create_time,120),4) create_year 
    from changerecord where {0} group by Left(Convert(varchar(100),create_time,120),4)", cond);
            return Helper.ExecuteSql(sql);
        }
        private bool ItemIsNull(object data) {
            if (data == null)
            {
                return true;
            }
            else {
                if (string.IsNullOrWhiteSpace(data.ToString()))
                {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
	}
}

