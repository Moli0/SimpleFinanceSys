using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public static class Helper
    {
        /// <summary>
        /// 取得分页的数据
        /// </summary>
        /// <param name="option"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataSet GetPaginList(Model.PaginModel option,string table) {
            if (option.NowPage == 0) {
                option.NowPage = option.PageIndex;
            }
            StringBuilder sql = new StringBuilder();
            if (string.IsNullOrEmpty(option.sortStr)) { option.sortStr = "Id desc"; }  //默认排序为插入时间
            string sqlWhere = " (1=1) ";
            sql.Append(string.Format(" select * from ( "));
            sql.Append(string.Format(" select ROW_NUMBER() OVER(ORDER BY {0}) AS pos,* from ( ", option.sortStr));
            sql.Append(string.Format(" select * FROM ( "));
            sql.Append(string.Format(" select * from {0} as x0 ",table));
            sql.Append(string.Format(" ) AS x1 "));
            if (option.hs != null){  //遍历查询条件
                foreach (DictionaryEntry a in option.hs){
                    sqlWhere += string.Format(" and {0} = '{1}' ", a.Key, a.Value);
                }
            }
            sql.Append(string.Format(" ) as x2 where {0} ", sqlWhere));
            sql.Append(string.Format(" ) as x3 where pos between {0} and {1} ", (option.NowPage - 1) * option.PageSize + 1, option.NowPage * option.PageSize));
            return ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 取得分页的数据
        /// </summary>
        /// <param name="option"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataSet GetPaginList(Model.PaginModel option, string table,Hashtable hsWhere)
        {
            if (option.NowPage == 0)
            {
                option.NowPage = option.PageIndex;
            }
            StringBuilder sql = new StringBuilder();
            if (string.IsNullOrEmpty(option.sortStr)) { option.sortStr = "Id desc"; }  //默认排序为插入时间
            string sqlWhere = " (1=1) ";
            sql.Append(string.Format(" select * from ( "));
            sql.Append(string.Format(" select ROW_NUMBER() OVER(ORDER BY {0}) AS pos,* from ( ", option.sortStr));
            sql.Append(string.Format(" select * FROM ( "));
            sql.Append(string.Format(" select * from {0} as x0 ", table));
            sql.Append(string.Format(" ) AS x1 "));
            if (option.hs != null)
            {  //遍历查询条件
                foreach (DictionaryEntry a in option.hs)
                {
                    sqlWhere += string.Format(" and {0} = '{1}' ", a.Key, a.Value);
                }
            }
            if (hsWhere != null) {
                foreach (IDictionaryEnumerator a in hsWhere) {
                    if (a.Key != null) {
                        if (!string.IsNullOrEmpty(a.Key.ToString())) {
                            sqlWhere += string.Format(" and {0} = '{1}' ", a.Key, a.Value);
                        }
                    }
                }
            }
            sql.Append(string.Format(" ) as x2 where {sqlWhere} "));
            sql.Append(string.Format(" ) as x3 where pos between {0} and {1} ", (option.NowPage - 1) * option.PageSize + 1, option.NowPage * option.PageSize));
            return ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 取得分页的数据
        /// </summary>
        /// <param name="option"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static DataSet GetPaginList(Model.PaginModel option, string table, string where)
        {
            if (option.NowPage == 0)
            {
                option.NowPage = option.PageIndex;
            }
            StringBuilder sql = new StringBuilder();
            if (string.IsNullOrEmpty(option.sortStr)) { option.sortStr = "Id desc"; }  //默认排序为插入时间
            string sqlWhere = " (1=1) ";
            sql.Append(string.Format(" select * from ( "));
            sql.Append(string.Format(" select ROW_NUMBER() OVER(ORDER BY {0}) AS pos,* from ( ", option.sortStr));
            sql.Append(string.Format(" select * FROM ( "));
            sql.Append(string.Format(" select * from {0} as x0 ", table));
            sql.Append(string.Format(" ) AS x1 "));
            if (option.hs != null)
            {  //遍历查询条件
                foreach (DictionaryEntry a in option.hs)
                {
                    sqlWhere += string.Format(" and {0} = '{1}' ", a.Key, a.Value);
                }
            }
            if (!string.IsNullOrEmpty(where)) {
                sqlWhere += string.Format(" {0} ", where);
            }
            sql.Append(string.Format(" ) as x2 where {0} ",sqlWhere));
            sql.Append(string.Format(" ) as x3 where pos between {0} and {1} ", (option.NowPage - 1) * option.PageSize + 1, option.NowPage * option.PageSize));
            return ExecuteSql(sql.ToString());
        }

        /// <summary>
        /// 执行SQL返回DataSet
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static DataSet ExecuteSql(string sql) {
            using (var conn = new ConnectionCode().GetConnection()) {
                SqlCommand comm = new SqlCommand(sql,conn);
                SqlDataAdapter sda = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                sda.Fill(ds);
                return ds;
            }
        }

        /// <summary>
        /// 根据条件取得数据行数
        /// </summary>
        /// <param name="hs"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static int GetCount(Hashtable hs, string table) {
            StringBuilder sql = new StringBuilder();
            sql.Append(string.Format(" select count(*) from ( {0} ) as x0 ",table));
            string sqlWhere = " (1=1) ";
            if (hs != null)
            {  //遍历查询条件
                foreach (DictionaryEntry a in hs)
                {
                    sqlWhere += string.Format(" and {0} = '{1}' ", a.Key, a.Value);
                }
            }
            sql.Append(string.Format(" where {0} ", sqlWhere));
            int count = GetCount(sql.ToString());
            return count;
        }

        /// <summary>
        /// 根据Sql取得数据行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static int GetCount(string sql) {
            using (var conn = new ConnectionCode().GetConnection()) {
                SqlCommand comm = new SqlCommand(sql, conn);
                string res = comm.ExecuteScalar().ToString();
                int count = 0;
                if (!string.IsNullOrEmpty(res)) {
                    count = Convert.ToInt32(res);
                }
                return count;
            }
        }
    }
}
