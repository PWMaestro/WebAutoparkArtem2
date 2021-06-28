using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data.Common;


namespace DataAccessLayer.Repositories
{
    public abstract class RepositoryBase : IDisposable, IAsyncDisposable
    {
        protected readonly DbConnection connection;

        protected RepositoryBase(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public void Dispose() => connection?.Dispose();

        public ValueTask DisposeAsync() => (ValueTask)connection?.DisposeAsync();
    }
}