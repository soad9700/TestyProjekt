using DAL.Models;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using NUnit.Framework;
using QuickApp.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Moq;
using Microsoft.Extensions.Options;
using System;
using Microsoft.Extensions.Logging;

namespace QuickApp.Tests.Authorization
{
    public class ProfileServiceTests
    {
        private ProfileService profileService;
        private UserManagerWrapper _userManager;
        private Mock<IUserClaimsPrincipalFactory<ApplicationUser>> _claimsFactoryMock;

        [SetUp]
        public void SetUp()
        {
            _userManager = new UserManagerWrapper(new ApplicationUser(), null, null, null, null, null, null, null, null, null);
            _claimsFactoryMock = new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>();
        }

        [Test]
        public async Task GetProfileDataAsync_GivenArguments_ReturnsExpectedResult()
        {
            // Arrange
            var profileService = new ProfileService(null, _claimsFactoryMock.Object);
            var identity = new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                    new Claim("sub", "1")
                })
            };
            var subject = new ClaimsPrincipal(identity);
            var context = new ProfileDataRequestContext();
            context.Subject = subject;

            _claimsFactoryMock
                .Setup(m => m.CreateAsync(
                    It.IsAny<ApplicationUser>()))
                .ReturnsAsync(subject);

            // Act
            await profileService.GetProfileDataAsync(context);

            // Assert
            Assert.AreEqual(context.IssuedClaims.Count, 1);
        }

        [Test]
        public void Calculate_withNumberSmallerThan0_ThrowsArgumentNullException()
        {
            // Arrange

            // Act

            // Assert
        }
    }

    public class UserManagerWrapper : UserManager<ApplicationUser>
    {
        private ApplicationUser _user;

        public UserManagerWrapper(ApplicationUser user,
            IUserStore<ApplicationUser> store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher,
            IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services, 
            ILogger<UserManager<ApplicationUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            _user = user;
        }

        public override Task<ApplicationUser> FindByIdAsync(string userId)
    => Task.FromResult(_user);

    }

}

