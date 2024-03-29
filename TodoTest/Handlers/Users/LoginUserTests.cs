﻿using System;
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
    public class LoginUserTests : BaseHandlerTests
    {
        private LoginHandler _handler;
        private static string _email = "admin@admin.com";
        private static string _name = "admin";
        private static string _password = "admin@admin.com";

        [SetUp]
        public async Task SetUp()
        {
            IConfigurationRoot? config = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                 .AddEnvironmentVariables()
                 .Build();
            _handler = new LoginHandler(_dataContext, _mapper, new JWTService(config));
            await new RegisterHandler(_dataContext, new JWTService(config))
                .Handle(
                    new RegisterCommand
                    (
                        new RegisterDTO
                        {
                            Email = _email,
                            Name = _name,
                            Password = _password
                        }
                    ),
                    new CancellationToken()
                );
        }

        // login with unexisting user email should throw TodoAPI.Errors.Exceptions.BadRequestException
        [Test]
        public async Task LoginWithUnexistingEmailThrowsBadRequest()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Email = "emil@babazade.com",
                Password = _password
            };
            string? type = "";

            // Act
            try
            {
                await _handler.Handle(new LoginCommand(loginDTO), new CancellationToken());
            }
            catch (Exception ex)
            {
                type = ex.GetType()?.FullName;
            }


            // Assert
            type.Should().Be("TodoAPI.Errors.BadRequestException");
        }

        // login with incorrect password should throw TodoAPI.Errors.Exceptions.BadRequestException
        [Test]
        public async Task LoginWithIncorrectPasswordThrowsBadRequestException()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Email = _email,
                Password = "123"
            };
            string? type = "";

            // Act
            try
            {
                await _handler.Handle(new LoginCommand(loginDTO), new CancellationToken());
            }
            catch (Exception ex)
            {
                type = ex.GetType()?.FullName;
            }


            // Assert
            type.Should().Be("TodoAPI.Errors.BadRequestException");
        }

        // login with correct password and email should return id, name, email, and token
        [Test]
        public async Task LoginWithCorrectCredsShouldLogin()
        {
            // Arrange
            var loginDTO = new LoginDTO
            {
                Email = _email,
                Password = _password
            };

            // Act
            UserDTO res = await _handler.Handle(new LoginCommand(loginDTO), new CancellationToken());
            UserEntity user = await _dataContext.Users.SingleOrDefaultAsync(u => u.Email == _email);

            // Assert
            user.Should().NotBe(null);
            res.Email.Should().Be(_email);
            res.Id.Should().Be(user.Id);
            res.Token.Should().NotBe(null);
        }
    }
}
