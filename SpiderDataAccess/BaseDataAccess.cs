#region ----------------备注----------------
// Author:TQ 
// FileName:BaseDataAccess.cs 
// Create Date:2017-06-13
// Create Time:17:22 
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace SpiderDataAccess
{
    public class BaseDataAccess<T> where T:class
    {
        private readonly ISession _session = NHibernateHelper.OpenSession();

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>返回ID</returns>
        public virtual int Insert(T model)
        {
            var id = 0;
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    id = (int) _session.Save(model);
                    _session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return id;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="model">实体对象</param>
        public virtual void Delete(T model)
        { 
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    _session.Delete(model);
                    _session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体对象</param>
        public virtual void Update(T model)
        {
            using (var transaction=_session.BeginTransaction())
            {
                try
                {
                    _session.Update(model);
                    _session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>返回对象</returns>
        public virtual T FindBy(object id)
        {
            T result = null;
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    result=_session.Get<T>(id);
                    _session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return result;
        }

        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>返回列表</returns>
        public virtual IList<T> QueryList(Func<T, bool> expression)
        {
            IList<T> list;
            using (var transaction = _session.BeginTransaction())
            {
                try
                {
                    list = expression == null ? _session.Query<T>().ToList() : _session.Query<T>().Where(expression).ToList(); 
                    _session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return list;
        }

        /// <summary>  
        /// 批量保存更新实体类  
        /// </summary>  
        /// <param name="list"></param>  
        /// <returns></returns>  
        public virtual bool SaveOrUpdateList(IList<T> list)
        {
            var result = true;
            using (var tx = _session.BeginTransaction())
            {
                try
                {
                    foreach (var T in list)
                        _session.Update(T);
                    _session.Flush();
                    tx.Commit(); 
                }
                catch (HibernateException ex)
                {
                    tx.Rollback();
                    result = false;
                    throw ex; 
                }
            } 
            return result;
        }
    }
}