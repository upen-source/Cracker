using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Data.Contracts;
using Data.Extensions;

namespace Data
{
    public abstract class Repository<TEntity> : IRepository<TEntity>, IDisposable
    {
        protected DbConnection DbConnection { get; }

        protected Repository(DbConnection connection)
        {
            DbConnection = connection;
        }

        private DbCommand CreateCommand()
        {
            return DbConnection.CreateCommand();
        }

        public abstract void Add(TEntity entity);
        public abstract IList<TEntity> GetAll();

        protected void Insert(string query, IEnumerable<object> values)
        {
            Execute($"INSERT {query.TrimStart()}", values);
        }

        protected void Update(string query, IEnumerable<object> values)
        {
            Execute($"UPDATE {query.TrimStart()}", values);
        }

        protected void Delete(string query, IEnumerable<object> values)
        {
            Execute($"DELETE {query.TrimStart()}", values);
        }

        private void Execute(string query, IEnumerable<object> values)
        {
            DbConnection.Open();
            using DbCommand command = CreateCommand();
            command.MapPlaceHolders(values);
            command.CommandText = query;
            command.ExecuteNonQuery();
            DbConnection.Close();
        }

        protected IList<TEntity> Select(string query, IEnumerable<object>? values = null, Func<IDataRecord, TEntity>? mapMethod = null)
        {
            DbConnection.Open();
            using DbCommand command = CreateCommand();
            command.CommandText = $"SELECT {query.TrimStart()}";
            command.MapPlaceHolders(values);
            IList<TEntity> entities = command.ToList(mapMethod ?? DefaultMap);
            DbConnection.Close();
            return entities;
        }

        protected abstract TEntity DefaultMap(IDataRecord @record);

        public void Dispose()
        {
            DbConnection.Close();
            GC.SuppressFinalize(this);
        }
    }
}
