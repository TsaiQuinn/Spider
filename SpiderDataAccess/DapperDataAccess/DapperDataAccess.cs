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
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;
using SpiderIDataAccess.IDapperDataAccess;

namespace SpiderDataAccess.DapperDataAccess
{
    public class DapperDataAccess<T> : ICarDataAccess<T> where T : class
    {
        private readonly string _mysqlConnection = ConfigurationManager.ConnectionStrings["MysqlConnectStr"].ToString();

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回ID</returns>
        public virtual int Insert(string sql, object parameters)
        {
            return Insert(sql, parameters, null);
        }

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <returns>返回ID</returns>
        public virtual int Insert(string sql, object parameters, IDbTransaction transaction)
        {
            var result = 0;
            OpenConnection(conn => result = conn.Execute(sql, parameters, transaction));
            return result;
        }

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual bool Delete(string sql, object parameters)
        {
            var result = false;
            OpenConnection(conn =>
            {
                result = conn.Execute(sql, parameters) > 0;
            });
            return result;
        }

        /// <summary>
        ///     删除
        /// </summary> 
        /// <param name="id">ID</param>
        /// <returns></returns>
        public virtual bool Delete(int id)
        {
            throw new NotImplementedException();
        } 

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        public virtual bool Update(string sql, object parameters, IDbTransaction transaction)
        {
            var result = false;
            OpenConnection(connection => { result = connection.Execute(sql, parameters, transaction) > 0; });
            return result;
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public virtual bool Update(string sql, object parameters)
        {
            return Update(sql, parameters, null);
        }

        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <returns>返回对象</returns>
        public virtual T FindBy(string sql, object parameters, IDbTransaction transaction)
        {
            T result = null;
            OpenConnection(connction => result = connction.QuerySingleOrDefault<T>(sql, parameters, transaction));
            return result;
        }

        /// <summary>
        ///     查询列表
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameter">参数</param>
        /// <param name="transaction">事务</param>
        /// <returns>返回列表</returns>
        public IList<T> QueryList(string sql, object parameter, IDbTransaction transaction)
        {
            IList<T> list = null;
            OpenConnection(connection => { list = connection.Query<T>(sql, parameter, transaction).ToList(); });
            return list;
        }

        /// <summary>
        ///     打开连接
        /// </summary>
        /// <param name="action">执行委托</param>
        protected void OpenConnection(Action<IDbConnection> action)
        {
            using (var conn = new MySqlConnection(_mysqlConnection))
            {
                conn.Open();
                action(conn);
            }
        }
    }
}