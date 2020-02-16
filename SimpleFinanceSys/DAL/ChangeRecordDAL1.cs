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

        /// <summary>
        /// 取得天、月、年、历史的收入与支出金额
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataSet GetBoxTotalModel(string userid) {
            string sql = string.Format(@"select * from (
	select 
	sum((case when orderType = 0 then amount else 0 end )) dayInAmount,
	sum((case when orderType = 1 then amount else 0 end )) dayOutAmount
	from (
		select CONVERT(VARCHAR(100),create_time,23) create_date,orderType,Sum(amount) amount
		 from ChangeRecord where (orderType = 0 or orderType = 1) and CONVERT(VARCHAR(100),create_time,23) = CONVERT(VARCHAR(100),GETDATE(),23) and create_user = '{0}'
		 group by CONVERT(VARCHAR(100),create_time,23),orderType
	 ) as x1
 ) t1
 left join (
	select 
	sum((case when orderType = 0 then amount else 0 end )) monthInAmount,
	sum((case when orderType = 1 then amount else 0 end )) monthOutAmount
	from (
		select LEFT(CONVERT(VARCHAR(100),create_time,23),7) create_date,orderType,Sum(amount) amount
		 from ChangeRecord where (orderType = 0 or orderType = 1) and LEFT(CONVERT(VARCHAR(100),create_time,23),7) = LEFT(CONVERT(VARCHAR(100),GETDATE(),23),7) and create_user = '{0}'
		 group by LEFT(CONVERT(VARCHAR(100),create_time,23),7),orderType
	 ) as x1
  ) t2 on 1=1
 left join (
	select 
	sum((case when orderType = 0 then amount else 0 end )) yearInAmount,
	sum((case when orderType = 1 then amount else 0 end )) yearOutAmount
	from (
		select LEFT(CONVERT(VARCHAR(100),create_time,23),4) create_date,orderType,Sum(amount) amount
		 from ChangeRecord where (orderType = 0 or orderType = 1) and LEFT(CONVERT(VARCHAR(100),create_time,23),4) = LEFT(CONVERT(VARCHAR(100),GETDATE(),23),4) and create_user = '{0}'
		 group by LEFT(CONVERT(VARCHAR(100),create_time,23),4),orderType
	 ) as x1
  ) t3 on 1=1
 left join (
	select 
	sum((case when orderType = 0 then amount else 0 end )) allInAmount,
	sum((case when orderType = 1 then amount else 0 end )) allOutAmount
	from (
		select orderType,Sum(amount) amount
		 from ChangeRecord where (orderType = 0 or orderType = 1)  and create_user = '{0}'
		 group by orderType
	 ) as x1
  ) t4 on 1=1", userid);
            return Helper.ExecuteSql(sql);
        }

        /// <summary>
        /// 按天取得上周收入与支出合计
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataSet GetTotalForDay(string userid) {
            string sql = string.Format(@"	select 
	create_date,
	sum((case when orderType = 0 then amount else 0 end )) dayInAmount,
	sum((case when orderType = 1 then amount else 0 end )) dayOutAmount
	from (
		select CONVERT(VARCHAR(100),create_time,23) create_date,orderType,Sum(amount) amount
		 from ChangeRecord where (orderType = 0 or orderType = 1) and CONVERT(VARCHAR(100),create_time,23)  between CONVERT(VARCHAR(100),DATEADD(wk, DATEDIFF(wk,0,DATEADD(day,-7,getdate())), -1),23) and CONVERT(VARCHAR(100),DATEADD(wk, DATEDIFF(wk,0,DATEADD(day,-7,getdate())), 5),23) and create_user = '{0}'
		 group by CONVERT(VARCHAR(100),create_time,23),orderType
	 ) as x1 group by create_date", userid);
            return Helper.ExecuteSql(sql);
        }

        /// <summary>
        /// 取得存款与负债总计
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataSet GetTotal(string userid) {
            string sql = string.Format(@"select * from (
select (sum((case when recordType = 0 then amount else 0 end))-sum((case when recordType = 2 then amount else 0 end))) DepositTotal from LoanRecored where (recordType = 0 or recordType = 2) and create_user = '{0}') t1 left join 
(select (sum((case when recordType = 1 then amount else 0 end))-sum((case when recordType = 3 then amount else 0 end))) loanTotal from LoanRecored where (recordType = 1 or recordType = 3) and create_user = '{0}') t2 on 1=1", userid);
            return Helper.ExecuteSql(sql);
        }

        /// <summary>
        /// 取得今日的最大金额，计算公式：昨日余额+今日收入
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public DataSet GetMaxMoney(string userid) {
            string sql = string.Format(@"select sum(amount) amount from (
select afterAmount amount from ChangeRecord where 
create_time = (select max(create_time) from ChangeRecord where Convert(varchar(10),create_time,23) < Convert(varchar(10),GETDATE(),23) and create_user = '{0}' ) and create_user = '{0}'
union all
select SUM(amount) amount from ChangeRecord where Convert(Varchar(10),create_time,23) = Convert(varchar(10),GETDATE(),23)  and orderType = 0 and create_user = '{0}'
) t1", userid);
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

