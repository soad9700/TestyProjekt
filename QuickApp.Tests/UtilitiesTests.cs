using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using Microsoft.Extensions.Logging;
using IdentityModel;
using Moq;
using NUnit.Framework;
using QuickApp.Helpers;

namespace QuickApp.Tests
{
    [TestFixture]
    public class UtilitiesTests
    {
        private ClaimsPrincipal _claimsPrincipal;
        private Mock<ILoggerFactory> _loggerFactory;
        
        [SetUp]
        public void SetUp()
        {
            _claimsPrincipal = new ClaimsPrincipal(ClaimsIdentities());
            _loggerFactory = new Mock<ILoggerFactory>();
        }

        [Test]
        public void GetUserId_WithClaimPrincipalUser_ReturnsCorrectUserId()
        {
            string expectedResult = IdentityMock().First(x => x.Type == JwtClaimTypes.Subject).Value.Trim();
            
            string result = Utilities.GetUserId(_claimsPrincipal);
            
            Assert.AreEqual(expectedResult, result);
        }
        
        [Test]
        public void GetRoles_WithClaimPrincipalIdentity_ReturnsCorrectRoles()
        {
            var expectedResult = IdentityMock().First(x => x.Type == JwtClaimTypes.Role).Value.Trim();
            
            string[] result = Utilities.GetRoles(_claimsPrincipal);

            Assert.AreEqual(expectedResult, string.Concat(result));
        }

        [Test]
        public void GetRoles_WithEmptyClaimsPrincipalIdentity_ReturnsEmptyCollection()
        {
            var claimsEmptyIdentities = new List<ClaimsIdentity>{};
            var identity = new ClaimsPrincipal(claimsEmptyIdentities);
            
            string[] result = Utilities.GetRoles(identity);
            
            Assert.IsEmpty(result);
        }

        [Test]
        public void CreateLogger_WhenLoggerFactoryIsNull_ShouldReturnsInvalidOperationException()
        {
            Assert.That(()=> Utilities.CreateLogger<string>(), Throws.InvalidOperationException);
        }
        
        [Test]
        public void CreateLogger_WithInvokedConfigureLogger_ShouldReturnsILoggerInstance()
        {
            Utilities.ConfigureLogger(_loggerFactory.Object);
            ILogger logger = Utilities.CreateLogger<string>();

            bool result = logger != null;
            
            Assert.IsTrue(result);
        }

        [Test]
        public void QuickLog_WhenInvoke_ShouldCreateDirectoryWithProvidedFilename()
        {
            var currentDirectory = Environment.CurrentDirectory;
            var filename = Path.GetFullPath(Path.Combine(currentDirectory, @"..\..\..\"));
            var logText = "testLog";
            
            Utilities.QuickLog(logText, filename);
            
            Assert.IsTrue(File.Exists(filename));
        }

        [Test]
        public void QuickLog_WhenInvokeWithWhiteSpaceFileName_ShouldReturnArgumentException()
        {
            var emptyFileName = " ";
            
            Assert.That(() => Utilities.QuickLog("test", emptyFileName), Throws.ArgumentException);
        }

        private static IEnumerable<Claim> IdentityMock()
            =>  new List<Claim>() {
                new Claim(ClaimTypes.Name, "user@test.com"),
                new Claim(JwtClaimTypes.Subject, "D52ED074-4F50-492B-B742-33162C31542F"),
                new Claim(JwtClaimTypes.Role, "TestRole")
            }.AsEnumerable();
        
        private static IEnumerable<ClaimsIdentity> ClaimsIdentities()
            => new List<ClaimsIdentity>()
            {
                new ClaimsIdentity(new List<Claim>(){
                    new Claim(ClaimTypes.Name, "user@test.com"),
                    new Claim(JwtClaimTypes.Subject, "D52ED074-4F50-492B-B742-33162C31542F"),
                    new Claim(JwtClaimTypes.Role, "TestRole")
                })
            };
    }
}