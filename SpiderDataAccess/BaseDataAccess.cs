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
    public class BaseDataAccess<T> where T : class
    { 

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="model">实体对象</param>
        /// <returns>返回ID</returns>
        public virtual int Insert(T model)
        {
            var id = 0;
            using(ISession session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    id = (int) session.Save(model);
                    session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
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
            using (ISession session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    session.Delete(model);
                    session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="model">实体对象</param>
        public virtual bool Update(T model)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    session.Update(model);
                    session.Flush();
                    transaction.Commit();
                    return true;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
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
            using (ISession session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    result = session.Get<T>(id);
                    session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
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
            using (ISession session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                try
                {
                    list = expression == null
                        ? session.Query<T>().ToList()
                        : session.Query<T>().Where(expression).ToList();
                    session.Flush();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    session.Close();
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
            using (ISession session = NHibernateHelper.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                try
                {
                    foreach (var T in list)
                        session.Update(T);
                    session.Flush();
                    tx.Commit();
                }
                catch (HibernateException ex)
                {
                    tx.Rollback();
                    result = false;
                    throw ex;
                }
                finally
                {
                    session.Close();
                }
            }
            return result;
        }
    }
}