#region ----------------备注----------------

// Author:CK 
// FileName:DapperDataAccess.cs 
// Create Date:2017-09-05
// Create Time:11:15 

#endregion

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using DapperExtensions;

namespace SpiderDataAccess.DapperDataAccess
{
    public class DapperDataAccess<T> where T : class
    {
        private readonly string _mysqlConnection = ConfigurationManager.ConnectionStrings.ToString();

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="action">执行委托</param>
        protected void OpenConnection(Action<IDbConnection> action)
        {
            using (var conn = new SqlConnection(_mysqlConnection))
            {
                conn.Open();
                action(conn);
            }
        } 

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>返回ID</returns>
        public virtual int Insert(T model)
        {
            int result = 0;
            OpenConnection(conn => result = conn.Insert(model));
            return result;
        } 
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <param name="transaction">事务</param>
        protected virtual bool Delete(T model, IDbTransaction transaction=null)
        {
            bool result = false;
            OpenConnection(conn =>
            {
                result = conn.Delete(model, transaction);
            });
            return result;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体对象</param> 
        public virtual bool Update(T model)
        {
            bool result = false;
            OpenConnection(connection => result = connection.Update(model));
            return result;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        public virtual T FindBy(object id)
        {
            T result = null;
            OpenConnection(connction => result = connction.Get<T>(id));
            return result;
        } 

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回列表</returns>
        public virtual IList<T> QueryList(Expression<Func<T, object>> expression,object parameters)
        {
            IList<T> list = null;
            OpenConnection(connction =>
            {
                var predicate = Predicates.Field<T>(expression,Operator.Eq, parameters);
                list  = connction.GetList<T>(predicate).ToList(); 
            });
            return list;
        }
    }
}