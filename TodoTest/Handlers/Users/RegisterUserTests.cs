using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using TodoAPI.Services.jwt;
using TodoAPI.Todo.Users;
using TodoAPI.Todo.Users.DTOs;
using TodoAPI.Todo.Users.Handlers;

namespace TodoTest.Handlers.Users
{
    public class RegisterUserTests : BaseHandlerTests
    {
        private RegisterHandler _handler;

        [SetUp]
        public void SetUp()
        {
            IConfigurationRoot? config = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                 .AddEnvironmentVariables()
                 .Build();
            _handler = new RegisterHandler(_dataContext, new JWTService(config));
        }

        // register should register
        [Test]
        public async Task AddingNewUserShouldAddNewUserToUsersTable()
        {
            // Arrange
            RegisterDTO registerDTO = new()
            {
                Email = "new@user.com",
                Name = "bob",
                Password = "123"
            };

            // Act
            await _handler.Handle(new RegisterCommand(registerDTO), new CancellationToken());
            System.Collections.Generic.List<UserEntity>? usersInDB = await _dataContext.Users.ToListAsync();
            UserEntity? registeredUser = await _dataContext.Users.SingleOrDefaultAsync(u => u.Email == registerDTO.Email);

            // Assert
            registeredUser.Should().NotBeNull();
            usersInDB.Count.Should().Be(1);
        }

        // registering user with existing email should throw BadRequestException
        [Test]
        public async Task AddingNewUserWithExistingEmailShouldThrowException()
        {
            // Arrange
            RegisterDTO registerDTO = new()
            {
                Email = "new@user.com",
                Name = "bob",
                Password = "123"
            };
            string? type = "";

            // Act
            await _handler.Handle(new RegisterCommand(registerDTO), new CancellationToken());
            try
            {
                await _handler.Handle(new RegisterCommand(registerDTO), new CancellationToken());
            }
            catch (Exception ex)
            {
                type = ex.GetType()?.FullName;
            }


            // Assert
            type.Should().Be("TodoAPI.Errors.BadRequestException");
        }
    }
}
