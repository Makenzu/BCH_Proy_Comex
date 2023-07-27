using BCH.Comex.Common.Tracing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;

namespace BCH.Comex.Data.DAL
{
    public class GenericRepository<TEntity, TContext> where TEntity : class where TContext : DbContext
    {
        private const string BCHComexDataBaseTraceSource = "BCHComexDataBaseTraceSource";
        
        protected TContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(TContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();

            ConfigurarLog();
        }

        /// <summary>
        /// Configura el log de las consultas a la base de datos
        /// </summary>
        /// <param name="context"></param>
        private void ConfigurarLog()
        {
            TraceSource ts = new TraceSource(BCHComexDataBaseTraceSource);
            var tracer = new Tracer(ts);
            this.context.Database.Log = s =>
            {
                tracer.TraceVerbose(s);
            };
        }

        public virtual IList<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }



        public virtual IList<TEntity> Get1(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        } 


        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual IList<TEntity> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public List<T> EjecutarSP<T>(string sp, params string[] parameters)
        {
            using (var tracer = new Tracer("EjecutarSP", BCHComexDataBaseTraceSource))
            {
                tracer.AddToContext("SP", sp);

                try
                {
                    //SE USA SqlParameter para evitar SQL INJECTION, sino hay un context.Database.SqlQuery que soporta parametros como string[]
                    List<SqlParameter> listaParametros = new List<SqlParameter>();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        sp += " @param" + i;
                        if (i < parameters.Length - 1)
                        {
                            sp += ", ";
                        }
                        listaParametros.Add(new SqlParameter("param" + i, parameters[i] == null ? string.Empty : parameters[i]));

                        tracer.AddToContext("param" + i, parameters[i] == null ? string.Empty : parameters[i]);
                    }
                    var res = context.Database.SqlQuery<T>(sp, listaParametros.ToArray());
                    return res.ToList<T>();
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta en EjecutarSP", e);
                    throw;
                }
            }
        }

        public List<T> EjecutarSP<T>(string sp, List<SqlParameter> listaOutput  , params string[] parameters)
        {
            using (var tracer = new Tracer("EjecutarSP", BCHComexDataBaseTraceSource))
            {
                tracer.AddToContext("SP", sp);

                try
                {
                    //SE USA SqlParameter para evitar SQL INJECTION, sino hay un context.Database.SqlQuery que soporta parametros como string[]
                    List<SqlParameter> listaParametros = new List<SqlParameter>();
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        sp += " @param" + i;
                        //if (i < parameters.Length - 1)
                        //{
                        sp += ", ";
                        //}
                        listaParametros.Add(new SqlParameter("param" + i, parameters[i] == null ? string.Empty : parameters[i]));

                        tracer.AddToContext("param" + i, parameters[i] == null ? string.Empty : parameters[i]);
                    }

                    int contadorParam = parameters.Length;

                    for (int i = 0; i < listaOutput.Count(); i++)
                    {
                        sp += " @" + listaOutput[i].ParameterName;
                        if (i < listaOutput.Count() - 1)
                        {
                            sp += ", ";
                        }
                        listaParametros.Add(listaOutput[i]);
                    }

                    var res = context.Database.SqlQuery<T>(sp, listaParametros.ToArray());
                    return res.ToList<T>();
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta en EjecutarSP", e);
                    throw;
                }
            }
        }

        public void ReadQuerySP(Action<DbDataReader> method, string sp, params string[] parameters)
        {
            using (var tracer = new Tracer("ReadQuerySP", BCHComexDataBaseTraceSource))
            {
                tracer.AddToContext("SP", sp);
                try
                {
                    //SE USA SqlParameter para evitar SQL INJECTION, sino hay un context.Database.SqlQuery que soporta parametros como string[]
                    using (var command = context.Database.Connection.CreateCommand())
                    {
                        context.Database.Connection.Open();
                        command.CommandText = sp;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCommandBuilder.DeriveParameters((SqlCommand)command);
                        for (int i = 1; i < command.Parameters.Count; i++)
                        {
                            if (parameters[i - 1] == null)
                            {
                                command.Parameters[i].Value = DBNull.Value;
                            }
                            else
                            {
                                command.Parameters[i].Value = parameters[i - 1];
                            }

                            tracer.AddToContext("param" + i, parameters[i - 1]);
                        }
                        using (var reader = command.ExecuteReader())
                        {
                            method(reader);
                        }
                        context.Database.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta en ReadQuerySP", ex);
                    throw;
                }
                finally
                {
                    context.Database.Connection.Close();
                }
            }
        }

        /// <summary>
        /// Lee 2 resultsets
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="sp"></param>
        /// <param name="result1"></param>
        /// <param name="result2"></param>
        /// <param name="paramList"></param>
        public void ReadQueryMultipleResult<T1, T2>(string sp, out List<T1> result1, out List<T2> result2, params string[] paramList)
        {
            using (var tracer = new Tracer("ReadQueryMultipleResult", BCHComexDataBaseTraceSource))
            {
                tracer.AddToContext("SP", sp);

                try
                {
                    //SE USA SqlParameter para evitar SQL INJECTION, sino hay un context.Database.SqlQuery que soporta parametros como string[]
                    using (var command = context.Database.Connection.CreateCommand())
                    {
                        context.Database.Connection.Open();
                        command.CommandText = sp;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCommandBuilder.DeriveParameters((SqlCommand)command);
                        for (int i = 1; i < command.Parameters.Count; i++)
                        {
                            command.Parameters[i].Value = paramList[i - 1];

                            tracer.AddToContext("param" + i, paramList[i - 1]);
                        }

                        using (var reader = command.ExecuteReader())
                        {
                            result1 = ((IObjectContextAdapter)context)
                                .ObjectContext
                                .Translate<T1>(reader).ToList();

                            reader.NextResult();

                            result2 = ((IObjectContextAdapter)context)
                                .ObjectContext
                                .Translate<T2>(reader).ToList();
                            //method(reader);
                        }
                        context.Database.Connection.Close();
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta en ReadQueryMultipleResult", ex);
                    throw;
                }
                finally
                {
                    context.Database.Connection.Close();
                }
            }
        }


        public EntityState GetEntityState(TEntity instance)
        {
            if (instance != null)
            {
                return context.Entry<TEntity>(instance).State;
            }
            else return EntityState.Unchanged;
        }

        public void ReadQuerySPTransaction(Action<DbDataReader> method, string sp, params string[] parameters)
        {
            using (var tracer = new Tracer("ReadQuerySPTransaction", BCHComexDataBaseTraceSource))
            {
                tracer.AddToContext("SP", sp);
                try
                {
                    //SE USA SqlParameter para evitar SQL INJECTION, sino hay un context.Database.SqlQuery que soporta parametros como string[]
                    using (var command = context.Database.Connection.CreateCommand())
                    {
                        command.CommandText = sp;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.Transaction = context.Database.CurrentTransaction.UnderlyingTransaction;

                        SqlCommandBuilder.DeriveParameters((SqlCommand)command);
                        for (int i = 1; i < command.Parameters.Count; i++)
                        {
                            command.Parameters[i].Value = parameters[i - 1];

                            tracer.AddToContext("param" + i, parameters[i - 1]);
                        }
                        using (var reader = command.ExecuteReader())
                        {
                            method(reader);
                        }
                    }
                }
                catch (Exception ex)
                {
                    tracer.TraceException("Alerta en ReadQuerySPTransaction", ex);
                    throw;
                }
            }
        }

        public int EjecutarSPConRetornoSinTransaccion(string sp, string prepend, List<string> inputParameters, List<SqlParameter> outputParameters = null)
         {
             return EjecutarSPConRetorno(sp, prepend, TransactionalBehavior.DoNotEnsureTransaction, inputParameters, outputParameters);
        }

         public int EjecutarSPConRetorno(string sp, string prepend, params string[] parameters)
         {
             List<string> listaParams = (parameters != null ? parameters.ToList() : null);
             return EjecutarSPConRetorno(sp, prepend, TransactionalBehavior.EnsureTransaction, listaParams);
         }

         
        private int EjecutarSPConRetorno(string sp,string prepend, TransactionalBehavior behavior, List<string> inputParameters, List<SqlParameter> outputParameters = null)
        {
            using (var tracer = new Tracer("EjecutarSPConRetorno", BCHComexDataBaseTraceSource))
            {
                tracer.AddToContext("SP", sp);
                tracer.AddToContext("prepend", prepend);

                try
                {
                    //SE USA SqlParameter para evitar SQL INJECTION, sino hay un context.Database.SqlQuery que soporta parametros como string[]
                    List<SqlParameter> listaParametros = new List<SqlParameter>();
                    if (inputParameters != null)
                    {
                        for (int i = 0; i < inputParameters.Count; i++)
                        {
                            sp += " @param" + i;
                            if (i < inputParameters.Count - 1)
                            {
                                sp += ", ";
                            }
                            listaParametros.Add(new SqlParameter("param" + i, inputParameters[i] == null ? string.Empty : inputParameters[i]));

                            tracer.AddToContext("param" + i, inputParameters[i] == null ? string.Empty : inputParameters[i]);
                        }
                    }

                    if (outputParameters != null && outputParameters.Count > 0)
                    {
                        sp += ", ";

                        for (int i = 0; i < outputParameters.Count; i++)
                        {
                            SqlParameter paramOut = outputParameters[i];
                            sp += " @" + paramOut.ParameterName + " OUTPUT";
                            if (i < outputParameters.Count - 1)
                            {
                                sp += ", ";
                            }
    
                            listaParametros.Add(paramOut);
                        }
                    }
                    
                    var returnCode = new SqlParameter();
                    returnCode.ParameterName = "@ReturnCode";
                    returnCode.SqlDbType = SqlDbType.Int;
                    returnCode.Direction = ParameterDirection.Output;
                    sp = prepend + " exec @ReturnCode = " + sp;

                    listaParametros.Add(returnCode);

                    //var data = context.Database.SqlQuery<int>(sp,  listaParametros.ToArray());
                    var data2 = context.Database.ExecuteSqlCommand(behavior, sp, listaParametros.ToArray());
                    return (int)returnCode.Value;
                }
                catch (Exception e)
                {
                    tracer.TraceException("Alerta en EjecutarSPConRetorno", e);
                    throw;
                }
            }
        }
    }
}
