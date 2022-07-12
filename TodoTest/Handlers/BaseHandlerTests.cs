using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Threading.Tasks;
using TodoAPI.Data;
using TodoAPI.Helpers;

namespace TodoTest.Handlers
{
    public class BaseHandlerTests
    {
        protected DataContext _dataContext;
        protected IMapper _mapper;

        [OneTimeSetUp]
        public void SetupDb()
        {
            DbContextOptions<DataContext>? options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dataContext = new DataContext(options);
            _dataContext.Database.EnsureCreated();

            MapperConfiguration? mapFactory = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfiles());
            });
            _mapper = mapFactory.CreateMapper();
        }

        [TearDown]
        public async Task ClearData()
        {
            _dataContext.Users.RemoveRange(await _dataContext.Users.ToListAsync());
            await _dataContext.SaveChangesAsync();
        }

    }
}
