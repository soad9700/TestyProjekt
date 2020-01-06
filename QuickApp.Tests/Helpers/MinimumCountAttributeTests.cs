using NUnit.Framework;
using System.Collections.Generic;
using QuickApp.Helpers;

namespace QuickApp.Tests.Helpers
{
    public class MinimumCountAttributeTests
    {
        private MinimumCountAttribute SUT;
        
        
        [SetUp]
        public void Setup()
        {
            SUT = new MinimumCountAttribute();
        }

        [Test]
        public void IsValid_WhenArgumentNullAndRequiredTrue_ShouldReturnFalse()
        {
            var result = SUT.IsValid(null);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void IsValid_WhenArgumentNullAndRequiredFalse_ShouldReturnTrue()
        {
            SUT = new MinimumCountAttribute(1, false);
            var result = SUT.IsValid(null);
            Assert.AreEqual(true, result);
        }
        
        [Test]
        public void IsValid_WhenArgumentNotNull_ShouldReturnTrue()
        {
            var testICollection = new List<string>(){"test1", "test2", "test3", "test4", "test5"};

            var result = SUT.IsValid(testICollection);
            Assert.AreEqual(true, result);
        }
        
        [Test]
        public void IsValid_WhenArgumentNotNullAndMinCountBiggerThanListCount_ShouldReturnFalse()
        {
            var testICollection = new List<string>(){"test1", "test2", "test3", "test4", "test5"};
            
            SUT = new MinimumCountAttribute(10);

            var result = SUT.IsValid(testICollection);
            Assert.AreEqual(false, result);
        }
        
        [Test]
        public void IsValid_WhenArgumentNotICollection_ShouldReturnFalse()
        {
            var testObject = new object();
            
            var result = SUT.IsValid(testObject);
            Assert.AreEqual(false, result);
        }

        [Test]
        public void FormatErrorMessage_WhenArgumentValid_ShouldReturnStringErrorMessageWithArgumentIncluded()
        {
            var testName = "testName";

            var result = SUT.FormatErrorMessage(testName);
            
            Assert.That(result.Contains(testName));
        }
        
        [Test]
        public void FormatErrorMessage_WhenArgumentNull_ShouldReturnStringErrorMessageWithNoArgumentIncluded()
        {
            var emptyNameErrorMessageLength = 29;

            var result = SUT.FormatErrorMessage(null);
            
            Assert.That(result.Length == emptyNameErrorMessageLength);
        }
        
    }
}
