#region ----------------备注----------------

// Author:CK 
// FileName:ICarDataAccess.cs 
// Create Date:2017-09-06
// Create Time:11:07 

#endregion

using System.Collections.Generic;
using System.Data;

namespace SpiderIDataAccess.IDapperDataAccess
{
    public interface ICarDataAccess<T> where T : class
    {
        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>返回ID</returns>
        int Insert(string sql, object parameters);

        /// <summary>
        ///     新增
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <returns>返回ID</returns>
        int Insert(string sql, object parameters, IDbTransaction transaction);

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        bool Delete(string sql, object parameters);

        /// <summary>
        ///     删除
        /// </summary> 
        /// <param name="id">ID</param>
        /// <returns></returns>
        bool Delete(int id); 

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        bool Update(string sql, object parameters);

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <returns></returns>
        bool Update(string sql, object parameters, IDbTransaction transaction);

        /// <summary>
        ///     查询
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <param name="transaction">事务</param>
        /// <returns>返回对象</returns>
        T FindBy(string sql, object parameters, IDbTransaction transaction);

        /// <summary>
        ///     查询列表
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="parameter">参数</param>
        /// <param name="transaction">事务</param>
        /// <returns>返回列表</returns>
        IList<T> QueryList(string sql, object parameter, IDbTransaction transaction);

    }
}