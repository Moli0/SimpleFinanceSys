﻿using Dapper;
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
    /// 数据表[Base_Object]操作类
    /// </summary>
    public partial class Base_ObjectDAL : BaseDal
	{
	    /// <summary>
		///构造函数
		/// </summary>
	    public Base_ObjectDAL(){}

		/// <summary>
		///添加一条数据(返)插入的数据的id）
		/// </summary>
		public string Add(Model.Base_ObjectModel model)
		{
		    string sqlStr = " insert into [Base_Object](id,create_time,create_user,last_time,last_user,state,name,explain,sort) values(@id,@create_time,@create_user,@last_time,@last_user,@state,@name,@explain,@sort);select @@id ";
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<string>(sqlStr,model).SingleOrDefault();
			}
		}

		/// <summary>
		///一次添加多条数据(返)插入的行数）
		/// </summary>
		public int Adds(List<Model.Base_ObjectModel> model)
		{
		    string sqlStr = " insert into [Base_Object](id,create_time,create_user,last_time,last_user,state,name,explain,sort) ";
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
		public bool Update(Model.Base_ObjectModel model)
		{
		    string sqlStr = " update [Base_Object] set create_user=@create_user,last_time=@last_time,last_user=@last_user,state=@state,name=@name,explain=@explain,sort=@sort where id = @id";
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
		    string sqlStr = " update [Base_Object] set "+ field +" where (1=2) ";
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
		    string sqlStr = " delete [Base_Object] where id = @id";
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
		    string sqlStr = " delete [Base_Object] where (1=2) ";
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
		public Model.Base_ObjectModel GetModel(int id)
		{
		    string sqlStr = " select top 1 * from [Base_Object] where id = @id ";
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<Model.Base_ObjectModel>(sqlStr,new{ id = id }).SingleOrDefault();
			}
		}

		/// <summary>
		///根据条件取得数据模型(返)一个model）
		/// </summary>
		public Model.Base_ObjectModel GetModel(string cond)
		{
		    string sqlStr = " select top 1 * from [Base_Object] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<Model.Base_ObjectModel>(sqlStr).SingleOrDefault();
			}
		}

		/// <summary>
		///按条件取得模型列表(返)一个List<model>）
		/// </summary>
		public List<Model.Base_ObjectModel> GetListModel(string cond)
		{
		    string sqlStr = " select * from [Base_Object] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<Model.Base_ObjectModel>(sqlStr).AsList();
			}
		}

		/// <summary>
		///按条件取一个字段的值(返)一个string）
		/// </summary>
		public string GetOneField(string field,string cond)
		{
			string sqlStr = " select top 1 "+ field +" from [Base_Object] where state not in (-2) ";
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
		public Model.Base_ObjectModel GetFields(string fields,string cond)
		{
		    string sqlStr = " select top 1 "+ fields +" from [Base_Object] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<Model.Base_ObjectModel >(sqlStr).SingleOrDefault();
			}
		}

		/// <summary>
		///按条件取一个字段的列表(返)一个List<string>）
		/// </summary>
		public List<string> GetListField(string field,string cond)
		{
		    string sqlStr = " select "+ field +" from [Base_Object] where state not in (-2) ";
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
		public List<Model.Base_ObjectModel> GetListFields(string fields,string cond)
		{
		    string sqlStr = " select "+ fields +" from [Base_Object] where state not in (-2) ";
			if (!string.IsNullOrEmpty(cond))
            {
                sqlStr += " and " + cond;
            }
			using(var connection = new ConnectionCode().GetConnection())
			{
			    return connection.Query<Model.Base_ObjectModel>(sqlStr).AsList();
			}
		}

		/// <summary>
		/// 取所有数据的数量
		/// </summary>
		public int GetCount()
		{
			string sqlStr = " select count(*) from [Base_Object] ";
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
			string sqlStr = " select count(*) from [Base_Object] where state not in (-2) ";
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
            ds = Helper.GetPaginList(option,"[Base_Object]");
            return ds;
        }
	}
}
