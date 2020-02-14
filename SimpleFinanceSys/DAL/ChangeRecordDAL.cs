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
	    /// <summary>
		///构造函数
		/// </summary>
	    public ChangeRecordDAL(){}

		/// <summary>
		///添加一条数据(返)插入的数据的id）
		/// </summary>
		public string Add(Model.ChangeRecordModel model)
		{
            string sqlStr = " insert into [ChangeRecord](id,create_time,create_user,last_time,last_user,state,orderId,orderType,payType,payObject,beforeAmount,afterAmount,amount,remark) values(@id,@create_time,@create_user,@last_time,@last_user,@state,@orderId,@orderType,@payType,@payObject,@beforeAmount,@afterAmount,@amount,@remark);select @@id ";
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<string>(sqlStr,model).SingleOrDefault();
			}
		}

		/// <summary>
		///一次添加多条数据(返)插入的行数）
		/// </summary>
		public int Adds(List<Model.ChangeRecordModel> model)
		{
            string sqlStr = " insert into [ChangeRecord](id,create_time,create_user,last_time,last_user,state,orderId,orderType,payType,@payObject,beforeAmount,afterAmount,amount,remark) ";
			for(int i = 0;i<model.Count;i++)
			{
			    sqlStr += " select ";
				foreach(var a in model[i].GetType().GetProperties())
				{
					if (false){
						continue;
					}
					sqlStr += " '" + a.GetValue(model[i]) + "',";
				}
				sqlStr = sqlStr.Substring(0,sqlStr.Length-1);
			    if(i<model.Count-1)
				{
				    sqlStr += " union all ";
				}
			}
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Execute(sqlStr);
			}
		}

		/// <summary>
		///根据id修改一条数据(返)修改结果）
		/// </summary>
		public bool Update(Model.ChangeRecordModel model)
		{
            string sqlStr = " update [ChangeRecord] set create_user=@create_user,last_time=@last_time,last_user=@last_user,state=@state,orderId=@orderId,orderType=@orderType,payType=@payType,payObject=@payObject,beforeAmount=@beforeAmount,afterAmount=@afterAmount,amount=@amount,remark=@remark where id = @id";
            using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Execute(sqlStr,model)>0;
			}
		}

		/// <summary>
		///根据条件修改数据(返)受影响的行数）
		/// </summary>
		public int Update(string field,string cond)
		{
		    string sqlStr = " update [ChangeRecord] set "+ field +" where (1=2) ";
            if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " or " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Execute(sqlStr);
			}
		}

		/// <summary>
		///根据id删除一条数据(返)修改结果）
		/// </summary>
		public bool Delete(int id)
		{
		    string sqlStr = " delete [ChangeRecord] where id = @id";
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Execute(sqlStr,new{ id = id })>0;
			}
		}

		/// <summary>
		///根据条件删除数据(返)受影响的行数）
		/// </summary>
		public int Delete(string cond)
		{
		    string sqlStr = " delete [ChangeRecord] where (1=2) ";
            if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " or " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Execute(sqlStr);
			}
		}

		/// <summary>
		///根据id取得数据模型(返)一个model）
		/// </summary>
		public Model.ChangeRecordModel GetModel(int id)
		{
		    string sqlStr = " select top 1 * from [ChangeRecord] where id = @id ";
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<Model.ChangeRecordModel>(sqlStr,new{ id = id }).SingleOrDefault();
			}
		}

		/// <summary>
		///根据条件取得数据模型(返)一个model）
		/// </summary>
		public Model.ChangeRecordModel GetModel(string cond)
		{
		    string sqlStr = " select top 1 * from [ChangeRecord] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<Model.ChangeRecordModel>(sqlStr).SingleOrDefault();
			}
		}

		/// <summary>
		///按条件取得模型列表(返)一个List<model>）
		/// </summary>
		public List<Model.ChangeRecordModel> GetListModel(string cond)
		{
		    string sqlStr = " select * from [ChangeRecord] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<Model.ChangeRecordModel>(sqlStr).AsList();
			}
		}

		/// <summary>
		///按条件取一个字段的值(返)一个string）
		/// </summary>
		public string GetOneField(string field,string cond)
		{
			string sqlStr = " select top 1 "+ field +" from [ChangeRecord] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<string>(sqlStr).SingleOrDefault();
			}
		}

		/// <summary>
		///按条件取多个字段的值(返)一个model）
		/// </summary>
		public Model.ChangeRecordModel GetFields(string fields,string cond)
		{
		    string sqlStr = " select top 1 "+ fields +" from [ChangeRecord] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<Model.ChangeRecordModel >(sqlStr).SingleOrDefault();
			}
		}

		/// <summary>
		///按条件取一个字段的列表(返)一个List<string>）
		/// </summary>
		public List<string> GetListField(string field,string cond)
		{
		    string sqlStr = " select "+ field +" from [ChangeRecord] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<string>(sqlStr).AsList();
			}
		}

		/// <summary>
		///按条件取多个字段的列表(返)一个List<model>）
		/// </summary>
		public List<Model.ChangeRecordModel> GetListFields(string fields,string cond)
		{
		    string sqlStr = " select "+ fields +" from [ChangeRecord] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<Model.ChangeRecordModel>(sqlStr).AsList();
			}
		}

		/// <summary>
		/// 取所有数据的数量
		/// </summary>
		public int GetCount()
		{
			string sqlStr = " select count(*) from [ChangeRecord] ";
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<int>(sqlStr).SingleOrDefault();
			}
		}

		/// <summary>
		/// 按条件取所有数据的数量
		/// </summary>
		public int GetCount(string cond)
		{
			string sqlStr = " select count(*) from [ChangeRecord] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<int>(sqlStr).SingleOrDefault();
			}
		}

		/// <summary>
        /// 根据参数分页取数据
        /// </summary>
        /// <param name="option">参数模型</param>
        /// <returns>一个DataSet对象</returns>
        public DataSet GetPageList(Model.PaginModel option)
        {
            DataSet ds = new DataSet();
            ds = Helper.GetPaginList(option,"[ChangeRecord]");
            return ds;
        }
	}
}

