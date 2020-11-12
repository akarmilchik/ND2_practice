using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TicketsResale.Context;

namespace TicketsResale.Test
{
    public class ConnectionFactory : IDisposable
    {
        private bool disposedValue = false;

        public StoreContext CreateContextForSQLite()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();

            var option = new DbContextOptionsBuilder<StoreContext>().UseSqlite(connection).Options;

            var context = new StoreContext(option);

            if (context != null)
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }

            return context;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
