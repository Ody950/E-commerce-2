
using E_commerce_2.Data;
using E_commerce_2.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace E_commerceTestData.Mocks
{
    public abstract class Mock : IDisposable
    {
        private readonly SqliteConnection _connection;
        protected readonly TheMarketDBContext _db;
        public Mock()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            var options = new DbContextOptionsBuilder<TheMarketDBContext>()
                .UseSqlite(_connection)
                .Options;
            _db = new TheMarketDBContext(options);
            _db.Database.EnsureCreated();
        }
       

        // Other methods for creating and saving test data
        public void Dispose()
        {
            _db?.Dispose();
            _connection?.Dispose();
        }
    }
}