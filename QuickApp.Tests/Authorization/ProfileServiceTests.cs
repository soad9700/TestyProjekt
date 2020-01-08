using DAL.Core.Interfaces;
using DAL.Models;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using QuickApp.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuickApp.Tests.Authorization
{
    public class ProfileServiceTests
    {
        private Mock<IUserManager> _userManagerMock;
        private Mock<IUserClaimsPrincipalFactory<ApplicationUser>> _userClaimFactoryMock;
        private ApplicationUser _user;


        [SetUp]
        public void Setup()
        {
            _userManagerMock = new Mock<IUserManager>();
            _userClaimFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        }

        [TestCase("Tester", "Test", "Config", 4)]
        [TestCase(null, "Test", "Config", 3)]
        [TestCase(null, null, "Config", 2)]
        [TestCase(null, null, null, 1)]
        public async Task GetProfileDataAsync_withGivenArgument_ReturnExpectedResult_TestSuite(string jobTitle, string fullName, string configuration, int expectedValue)
        {
            // Arrange
            _user = new ApplicationUser()
            {
                JobTitle = jobTitle,
                FullName = fullName,
                Configuration = configuration
            };

            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);

            var identity = new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                    new Claim("sub", "1")
                })
            };
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _userClaimFactoryMock.Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>())).ReturnsAsync(claimsPrincipal);

            var context = new ProfileDataRequestContext();
            context.Subject = claimsPrincipal;
            context.RequestedClaimTypes = new List<string>() { "sub" };

            var profileService = new ProfileService(_userManagerMock.Object, _userClaimFactoryMock.Object);

            // Act
            await profileService.GetProfileDataAsync(context);

            // Assert
            Assert.AreEqual(expectedValue, context.IssuedClaims.Count);
        }


        [TestCase(true, true)]
        [TestCase(false, false)]
        public async Task IsActiveAsync_withUserIsEnabledProp_ReturnExpectedValue_TestSuite(bool isEnabled, bool expectedValue)
        {
            // Arrange
            _user = new ApplicationUser()
            {
                IsEnabled = isEnabled
            };

            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);

            var identity = new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                    new Claim("sub", "1")
                })
            };
            var claimsPrincipal = new ClaimsPrincipal(identity);


            var context = new IsActiveContext(claimsPrincipal, new Client(), "caller");
            context.Subject = claimsPrincipal;

            var profileService = new ProfileService(_userManagerMock.Object, _userClaimFactoryMock.Object);

            // Act
            await profileService.IsActiveAsync(context);

            // Assert
            Assert.AreEqual(expectedValue, context.IsActive);
        }

        public async Task IsActiveAsync_withUserNullArgument_ReturnIsActivePropFalse()
        {
            // Arrange
            _user = null;

            _userManagerMock.Setup(x => x.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(_user);

            var identity = new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                    new Claim("sub", "1")
                })
            };
            var claimsPrincipal = new ClaimsPrincipal(identity);


            var context = new IsActiveContext(claimsPrincipal, new Client(), "caller");
            context.Subject = claimsPrincipal;

            var profileService = new ProfileService(_userManagerMock.Object, _userClaimFactoryMock.Object);

            // Act
            await profileService.IsActiveAsync(context);

            // Assert
            Assert.IsFalse(context.IsActive);
        }

    }
}
