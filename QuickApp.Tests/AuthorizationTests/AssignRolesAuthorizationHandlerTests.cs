using Microsoft.AspNetCore.Authorization;
using Moq;
using NUnit.Framework;
using QuickApp.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuickApp.Tests.AuthorizationTests
{
    public class AssignRolesAuthorizationHandlerTests
    {
        private AssignRolesAuthorizationHandler SUT;
        private string[] _newRoles;
        private string[] _currentRoles;
        private Mock<ClaimsPrincipal> _contextUser;

        [SetUp]
        public void Setup()
        {
            SUT = new AssignRolesAuthorizationHandler();
            _newRoles = new string[] { "admin", "user" };
            _currentRoles = new string[] { "superadmin" };
            _contextUser = new Mock<ClaimsPrincipal>();
        }

        [Test]
        public void GetIsRolesChanged_WhenArgumentsNotNull_ShouldComputeCorrectlyAndReturnTrue()
        {
            var result = SUT.GetIsRolesChanged(_newRoles, _currentRoles);

            Assert.IsTrue(result);
        }

        [Test]
        public void GetIsRolesChanged_WhenNewRolesNull_ShouldComputeCorrectlyAndReturnTrue()
        {
            var result = SUT.GetIsRolesChanged(null, _currentRoles);

            Assert.IsTrue(result);
        }

        [Test]
        public void GetIsRolesChanged_WhenCurrentRolesNull_ShouldComputeCorrectlyAndReturnTrue()
        {
            var result = SUT.GetIsRolesChanged(_newRoles, null);

            Assert.IsTrue(result);
        }

        [Test]
        public void GetIsUserInAllAddedRoles_WhenArgumentsCorrect_ShouldComputeCorrectlyAndReturnTrue()
        {
            _contextUser.Setup(m => m.IsInRole(It.IsAny<string>())).Returns(true);
            var result = SUT.GetIsUserInAllAddedRoles(_contextUser.Object, _newRoles, _currentRoles);

            Assert.IsTrue(result);
        }

        [Test]
        public void GetIsUserInAllAddedRoles_WhenNewRolesNull_ShouldComputeCorrectlyAndReturnTrue()
        {
            _contextUser.Setup(m => m.IsInRole(It.IsAny<string>())).Returns(true);
            var result = SUT.GetIsUserInAllAddedRoles(_contextUser.Object, null, _currentRoles);

            Assert.IsTrue(result);
        }

        [Test]
        public void GetIsUserInAllAddedRoles_WhenCurrentRolesNull_ShouldComputeCorrectlyAndReturnTrue()
        {
            _contextUser.Setup(m => m.IsInRole(It.IsAny<string>())).Returns(true);
            var result = SUT.GetIsUserInAllAddedRoles(_contextUser.Object, _newRoles, null);

            Assert.IsTrue(result);
        }

        // Test failing due to NullReferenceException that is not handled in tested method
        [Test]
        public void GetIsUserInAllAddedRoles_WhenContextUserNull_ShouldComputeCorrectlyAndReturnTrue()
        {
            _contextUser.Setup(m => m.IsInRole(It.IsAny<string>())).Returns(true);
            var result = SUT.GetIsUserInAllAddedRoles(null, _newRoles, _currentRoles);

            Assert.IsTrue(result);
        }
    }
}
