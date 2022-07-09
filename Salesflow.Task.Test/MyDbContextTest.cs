using Microsoft.EntityFrameworkCore;
using Salesflow.Task.One;

namespace Salesflow.Task.Test
{
    [TestClass]
    public class MyDbContextTest
    {
        private MyDbContext _context;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<MyDbContext>()
                .UseInMemoryDatabase($"{nameof(MyDbContextTest)}_{DateTime.Now.ToFileTimeUtc()}")
                .EnableSensitiveDataLogging()
                .Options;

            _context = new MyDbContext(options);
        }

        [TestMethod]
        public void CanSaveAndReadAccount()
        {
            _context.Accounts.Add(new Account { Password = "test1", Username = "name1" });
            _context.Accounts.Add(new Account { Password = "test2", Username = "name2" });

            _context.SaveChanges();

            var account = _context.Accounts.Find((long)2);
            Assert.IsNotNull(account);
            Assert.AreEqual(account.Password, "test2");
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}