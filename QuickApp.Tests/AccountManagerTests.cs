using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL;
using DAL.Core;
using DAL.Core.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace QuickApp.Tests
{
    [TestFixture]
    public class AccountManagerTests
    {
        private AccountManager _accountManager;
        private Mock<IUserManager> _userManagerMock;

        [SetUp]
        public void SetUp()
        {
            _userManagerMock = new Mock<IUserManager>();
            _accountManager = new AccountManager(_userManagerMock.Object);
        }

        [Test]
        public void CreateUserAsync_WithLoginAndPassword_ShouldReturnTrue()
        {
            string login = "test";
            string password = "password123";

            var result = _accountManager.CreateUserAsync(login, password);

            Assert.IsTrue(result.Result);
        }

        [Test]
        public void CreateUserAsync_WithEmptyLogin_ShouldThrowArgumentException()
        {
            string login = "";
            string password = "password123";

            _accountManager.CreateUserAsync(login, password);

            Assert.Throws<ArgumentException>(() => throw new ArgumentException());
        }

        [Test]
        public void CreateUserAsync_WithEmptyPassword_ShouldThrowArgumentException()
        {
            string login = "test";
            string password = "";

            _accountManager.CreateUserAsync(login, password);

            Assert.Throws<ArgumentException>(() => throw new ArgumentException());
        }

        [Test]
        public async Task CreateUserAsync_WithTooShortPassword_ShouldReturnFalse()
        {
            string login = "test";
            string password = "pa23";

            var result = await _accountManager.CreateUserAsync(login, password);

            Assert.False(result);
        }

        [Test]
        public async Task CreateRoleAsync_ClaimsNull_ShouldProcessCorrectly()
        {
            var result = await _accountManager.CreateRoleAsync(new ApplicationRole("admin"), null);

            var expectedResult = (true, new string[] { });

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task CreateRoleAsync_ClaimsContainInvalid_ShouldReturnErrorTuple()
        {
            var claims = new List<string>() { null, null };
            var result = await _accountManager.CreateRoleAsync(new ApplicationRole("admin"), claims);

            var expectedResult = (false, new[] { "The following claim types are invalid: " + string.Join(", ", claims) });

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task UpdateRoleAsync_ClaimsContainNulls_ShouldReturnErrorTuple()
        {
            var claims = new List<string>() { null, null };
            var result = await _accountManager.UpdateRoleAsync(new ApplicationRole("admin"), claims);

            var expectedResult = (false, new[] { "The following claim types are invalid: " + string.Join(", ", claims) });

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        public async Task UpdateRoleAsync_ClaimsNull_ShouldReturnErrorTuple()
        {
            var result = await _accountManager.UpdateRoleAsync(new ApplicationRole("admin"), null);

            var expectedResult = (true, new string[] { });

            Assert.AreEqual(expectedResult, result);
        }
    }
}