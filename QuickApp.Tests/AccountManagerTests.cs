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
        public void GetUserByIdAsync_WithCorrectUserId_ShouldReturnUserInstance()
        {
            
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
        public void GetUserByIdAsync_WithNotExistingUserId_ShouldReturnFalse()
        {
            
        }
        
        [Test]
        public void GGetUserByUserNameAsync_WithNotExistingUserId_ShouldReturnFalse()
        {
            
        }
        
        [Test]
        public void GetUserByUserNameAsync_WithNotExistingUserId_ShouldReturnFalse()
        {
            
        }
        
        [Test]
        public void GetUserByEmailAsync_WithNotExistingUserId_ShouldReturnFalse()
        {
            
        }
    }
}