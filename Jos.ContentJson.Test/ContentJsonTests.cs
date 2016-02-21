using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Jos.ContentJson.Extensions;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Jos.ContentJson.Test
{
    [TestFixture]
    public class ContentJsonTests
    {
        #region GetJsonKey
        [Test]
        public void GetJsonKey_WithJsonPropertyAttribute()
        {
            var property = new Mock<PropertyInfo>();
            var propertyName = "jsonPropertyJsonKey";
            var jsonPropertyAttribute = new JsonPropertyAttribute {PropertyName = propertyName};

            TypeDescriptor.AddAttributes(property.Object, jsonPropertyAttribute);

            var jsonKey = property.Object.GetJsonKey();

            Assert.AreEqual(propertyName, jsonKey);
        }

        [Test]
        public void GetJsonKey_NoJsonPropertyAttribute()
        {
            var property = new Mock<PropertyInfo>();
            var propertyName = "Josef";
            property.SetupGet(x => x.Name).Returns(propertyName);

            var jsonKey = property.Object.GetJsonKey();

            Assert.AreEqual(propertyName.ToLower(), jsonKey);
        }
        #endregion

        #region PropertyShouldBeIncluded
        [Test]
        public void PropertyShouldBeIncluded_WithJsonIgnoreAttribute()
        {
            var property = new Mock<PropertyInfo>();
            var jsonIgnoreAttribute = new JsonIgnoreAttribute();

            TypeDescriptor.AddAttributes(property.Object, jsonIgnoreAttribute);

            var shouldBeIncluded = property.Object.PropertyShouldBeIncluded();

            Assert.False(shouldBeIncluded);
        }

        [Test]
        public void PropertyShouldBeIncluded_WithDisplayAttribute()
        {
            var property = new Mock<PropertyInfo>();
            var displayAttribute = new DisplayAttribute();

            TypeDescriptor.AddAttributes(property.Object, displayAttribute);

            var shouldBeIncluded = property.Object.PropertyShouldBeIncluded();

            Assert.True(shouldBeIncluded);
        }

        [Test]
        public void PropertyShouldBeIncluded_WithDisplayAttribute_AndJsonIgnoreAttribute()
        {
            var property = new Mock<PropertyInfo>();
            var displayAttribute = new DisplayAttribute();
            var jsonIgnoreAttribute = new JsonIgnoreAttribute();

            TypeDescriptor.AddAttributes(property.Object, displayAttribute);
            TypeDescriptor.AddAttributes(property.Object, jsonIgnoreAttribute);

            var shouldBeIncluded = property.Object.PropertyShouldBeIncluded();

            Assert.False(shouldBeIncluded);
        }

        [Test]
        public void PropertyShouldBeIncluded_WithJsonPropertyAttribute()
        {
            var property = new Mock<PropertyInfo>();
            var jsonPropertyAttribute = new JsonPropertyAttribute();

            TypeDescriptor.AddAttributes(property.Object, jsonPropertyAttribute);

            var shouldBeIncluded = property.Object.PropertyShouldBeIncluded();

            Assert.True(shouldBeIncluded);
        }

        [Test]
        public void PropertyShouldBeIncluded_WithJsonPropertyAttribute_AndJsonIgnoreAttribute()
        {
            var property = new Mock<PropertyInfo>();
            var jsonPropertyAttribute = new JsonPropertyAttribute();
            var jsonIgnoreAttribute = new JsonIgnoreAttribute();

            TypeDescriptor.AddAttributes(property.Object, jsonPropertyAttribute);
            TypeDescriptor.AddAttributes(property.Object, jsonIgnoreAttribute);

            var shouldBeIncluded = property.Object.PropertyShouldBeIncluded();

            Assert.False(shouldBeIncluded);
        }
        #endregion

        #region LowerCaseFirstsLetter
        [Test]
        public void LowerCaseFirstLetter_WithValue()
        {
            var exampleString = "JosefHeterJag";
            var expectedOutput = "josefHeterJag";
            var result = exampleString.LowerCaseFirstLetter();

            Assert.That(result.First() == 'j');
            Assert.That(result == expectedOutput);

            exampleString = "J";
            result = exampleString.LowerCaseFirstLetter();
            Assert.That(result.First() == 'j');
            Assert.That(result == "j");
        }

        [Test]
        public void LowerCaseFirstLetter_NoValue()
        {
            string exampleString = string.Empty;
            var result = exampleString.LowerCaseFirstLetter();

            Assert.AreEqual(result, "missingPropertyName");

            exampleString = null;
            result = exampleString.LowerCaseFirstLetter();

            Assert.AreEqual(result, "missingPropertyName");
        }

        #endregion
    }
}
