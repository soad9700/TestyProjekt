using DAL.Core.Interfaces;
using DAL.Models;
using IdentityServer4.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Claims;

namespace QuickApp.Tests.Authorization
{
    public class ProfileServiceTests
    {
        private Mock<IUserManager> _userManagerMock;
        private Mock<IUserClaimsPrincipalFactory<ApplicationUser>> _userClaimFactoryMock;


        [SetUp]
        private void Setup()
        {
            _userManagerMock = new Mock<IUserManager>();
            _userClaimFactoryMock= new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        
        }
        [Test]
        public void GetIsSameUser_withNullTargetUserId_ReturnFalse()
        {
            // Arrange
            var identity = new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                    new Claim("sub", "1")
                })
            };
            var claimPrincipal = new ClaimsPrincipal(identity);

            var profileService = new ProfileService(_userManagerMock.Object, _userClaimFactoryMock.Object);
            // Act

            // Assert
        }
    }
}
