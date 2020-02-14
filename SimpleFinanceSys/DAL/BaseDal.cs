using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace DAL
{
    public class BaseDal
    {
        public int InsertListSql<T>(List<T> list, string tbName) where T : class
        {
            if (list == null || list.Count == 0) { return 0; }
            string columns = "";
            string valuesStr = "";
            foreach (var a in list[0].GetType().GetProperties())
            {
                if (a.Name == "create_dateStr" || a.Name == "create_timeStr")
                {
                    continue;
                }
                columns += a.Name + ",";
            }
            columns = columns.Substring(0, columns.Length - 1);
            for (int i = 0; i < list.Count; i++)
            {
                string sqlStr = "";
                sqlStr += @"
    select ";
                foreach (var a in list[i].GetType().GetProperties())
                {
                    if (a.Name == "create_dateStr" || a.Name == "create_timeStr")
                    {
                        continue;
                    }
                    if (a.GetValue(list[i], null) == null || string.IsNullOrEmpty(a.GetValue(list[i], null).ToString()))
                    {
                        sqlStr += "NULL,";
                    }
                    else
                    {
                        sqlStr += " '" + a.GetValue(list[i], null).ToString() + "',";
                    }
                }
                sqlStr = sqlStr.Substring(0, sqlStr.Length - 1);
                if (i < list.Count - 1)
                {
                    sqlStr += " union all ";
                }
                valuesStr += sqlStr;
            }
            string sql = string.Format(" insert into [{0}]({1}) {2} ", tbName, columns, valuesStr);
            using(var connection = new ConnectionCode().GetConnection()){
                return connection.Execute(sql);
            }
        }

        public int InsertSql<T>(T model, string tbName) where T : Model.BaseModeol {

            string columns = "";
            string valuesStr = "";
            foreach (var a in model.GetType().GetProperties())
            {
                if (a.Name == "create_dateStr" || a.Name == "create_timeStr") {
                    continue;
                }
                if (a.GetValue(model, null) == null || string.IsNullOrEmpty(a.GetValue(model, null).ToString()))
                {
                    continue;
                }
                columns += a.Name + ",";
            }
            columns = columns.Substring(0, columns.Length - 1);
            string sqlStr = "";
            sqlStr += @"
    select ";
            foreach (var a in model.GetType().GetProperties())
            {
                if (a.Name == "create_dateStr" || a.Name == "create_timeStr")
                {
                    continue;
                }
                if (a.GetValue(model, null) == null || string.IsNullOrEmpty(a.GetValue(model, null).ToString()))
                {
                    continue;
                }
                else
                {
                    sqlStr += " '" + a.GetValue(model, null).ToString() + "',";
                }
            }
            sqlStr = sqlStr.Substring(0, sqlStr.Length - 1);
            valuesStr += sqlStr;
            string sql = string.Format(" insert into [{0}]({1}) {2} ", tbName, columns, valuesStr);
            using (var connection = new ConnectionCode().GetConnection())
            {
                return connection.Execute(sql);
            }
        }

        public int InsertSql<T>(T model) where T : Model.BaseModeol {
            string tbname = model.GetType().Name;
            return InsertSql<T>(model, tbname);
        }

        public int Update<T>(T model, string tbName) where T : Model.BaseModeol {
            string sql = "";
            string setSql = "";
            foreach (var a in model.GetType().GetProperties())
            {
                if (a.Name == "create_dateStr" || a.Name == "create_timeStr")
                {
                    continue;
                }
                if (a.GetValue(model, null) == null || string.IsNullOrEmpty(a.GetValue(model, null).ToString()))
                {
                }
                else
                {
                    setSql += string.Format(" {0} = '{1}', ", a.Name, a.GetValue(model, null).ToString());
                }
            }
            if (!string.IsNullOrEmpty(setSql)) {
                setSql = setSql.Trim();
                setSql = setSql.Substring(0, setSql.Length - 1);
                sql = string.Format("update [{0}] set {1} where id = '{2}' ", tbName, setSql, model.id);
            }
            if (!string.IsNullOrEmpty(sql))
            {
                using (var connection = new ConnectionCode().GetConnection())
                {
                    return connection.Execute(sql);
                }
            }
            else {
                return 0;
            }
        }

        public int Update<T>(T model) where T : Model.BaseModeol
        {
            string tbname = model.GetType().Name;
            return Update<T>(model, tbname);
        }

        public int Delete(string id,string tbName){

            string sql = string.Format("delete [{0}] where id = '{1}' ", tbName, id);
            using (var conn = new ConnectionCode().GetConnection()) {
                return conn.Execute(sql);
            }
        }

        public int Delete<T>(T model) where T : Model.BaseModeol
        {
            string tbName = model.GetType().Name;
            string id = model.id;
            return Delete(id, tbName);
        }

        public T GetDataForType<T>(string id, string tbName) where T : Model.BaseModeol {
            string sql = string.Format("select * from [{0}] where id = '{1}'", tbName, id);
            using (var conn = new ConnectionCode().GetConnection()) {
                return conn.Query<T>(sql).SingleOrDefault();
            }
        }
    }
}
