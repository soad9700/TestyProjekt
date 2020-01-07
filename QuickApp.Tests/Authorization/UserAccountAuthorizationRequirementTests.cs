using NUnit.Framework;
using QuickApp.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace QuickApp.Tests.Authorization
{
    public class ViewUserAuthorizationHandlerTests
    {
        [Test]
        public void GetIsSameUser_withNullTargetUserId_ReturnFalse()
        {
            // Arrange
            var viewUserAuthorizationHandler = new ViewUserAuthorizationHandler();

            // Act
            var result = viewUserAuthorizationHandler.GetIsSameUser(new ClaimsPrincipal(), null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetIsSameUser_withEmptyTargetUserId_ReturnFalse()
        {
            // Arrange
            var viewUserAuthorizationHandler = new ViewUserAuthorizationHandler();

            // Act
            var result = viewUserAuthorizationHandler.GetIsSameUser(new ClaimsPrincipal(), "");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetIsSameUser_withNullClaimsPrincipal_ShouldThrowArgumentNullException()
        {
            // Arrange
            var viewUserAuthorizationHandler = new ViewUserAuthorizationHandler();

            // Assert
            Assert.Throws<NullReferenceException>(() => viewUserAuthorizationHandler.GetIsSameUser(null, "Ancds"));
        }

        [Test]
        public void GetIsSameUser_withClaimsPrincipalContainIdentityWithSameTargetUserIdAsGiven_ReturnTrue()
        {
            // Arrange
            const string targetUserId = "1";
            var viewUserAuthorizationHandler = new ViewUserAuthorizationHandler();
            var identity = new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                    new Claim("sub", targetUserId)
                })
            };
            var claimPrincipal = new ClaimsPrincipal(identity);

            // Act
            var result = viewUserAuthorizationHandler.GetIsSameUser(claimPrincipal, targetUserId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GetIsSameUser_withClaimsPrincipalDoesNotContainIdentityWithSameTargetUserIdAsGiven_ReturnFalse()
        {
            // Arrange
            var viewUserAuthorizationHandler = new ViewUserAuthorizationHandler();
            var identity = new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                    new Claim("sub", "1")
                })
            };
            var claimPrincipal = new ClaimsPrincipal(identity);

            // Act
            var result = viewUserAuthorizationHandler.GetIsSameUser(claimPrincipal, "2");

            // Assert
            Assert.IsFalse(result);
        }

    }



    public class ManageUserAuthorizationHandlerTests 
    {
        [Test]
        public void GetIsSameUser_withNullTargetUserId_ReturnFalse()
        {
            // Arrange
            var viewUserAuthorizationHandler = new ViewUserAuthorizationHandler();

            // Act
            var result = viewUserAuthorizationHandler.GetIsSameUser(new ClaimsPrincipal(), null);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetIsSameUser_withEmptyTargetUserId_ReturnFalse()
        {
            // Arrange
            var viewUserAuthorizationHandler = new ViewUserAuthorizationHandler();

            // Act
            var result = viewUserAuthorizationHandler.GetIsSameUser(new ClaimsPrincipal(), "");

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetIsSameUser_withNullClaimsPrincipal_ShouldThrowArgumentNullException()
        {
            // Arrange
            var viewUserAuthorizationHandler = new ViewUserAuthorizationHandler();

            // Assert
            Assert.Throws<NullReferenceException>(() => viewUserAuthorizationHandler.GetIsSameUser(null, "Ancds"));
        }

        [Test]
        public void GetIsSameUser_withClaimsPrincipalContainIdentityWithSameTargetUserIdAsGiven_ReturnTrue()
        {
            // Arrange
            const string targetUserId = "1";
            var viewUserAuthorizationHandler = new ViewUserAuthorizationHandler();
            var identity = new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                    new Claim("sub", targetUserId)
                })
            };
            var claimPrincipal = new ClaimsPrincipal(identity);

            // Act
            var result = viewUserAuthorizationHandler.GetIsSameUser(claimPrincipal, targetUserId);

            // Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GetIsSameUser_withClaimsPrincipalDoesNotContainIdentityWithSameTargetUserIdAsGiven_ReturnFalse()
        {
            // Arrange
            var viewUserAuthorizationHandler = new ViewUserAuthorizationHandler();
            var identity = new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                    new Claim("sub", "1")
                })
            };
            var claimPrincipal = new ClaimsPrincipal(identity);

            // Act
            var result = viewUserAuthorizationHandler.GetIsSameUser(claimPrincipal, "2");

            // Assert
            Assert.IsFalse(result);
        }   
    }

}
