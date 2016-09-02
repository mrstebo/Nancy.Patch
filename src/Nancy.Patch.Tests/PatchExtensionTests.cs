using System;
using Nancy.Patch.Tests.Mocks;
using Nancy.Responses.Negotiation;
using Nancy.Testing;
using NUnit.Framework;

namespace Nancy.Patch.Tests
{
    [TestFixture]
    public class PatchExtensionTests
    {
        private static readonly Func<TestModel> BaseModel = () => new TestModel
        {
            Name = "Test Model",
            Description = "Description",
            ShortDescription = "Short Description",
            CreatedAt = new DateTime(2015, 1, 1)
        };

        private Browser _browser;
            
        [SetUp]
        public void SetUp()
        {
            _browser = new Browser(config => config.Module<MockModule>());
        }

        [TearDown]
        public void TearDown()
        {
            _browser = null;
        }

        [Test]
        public void ShouldReturn_OkStatusCode()
        {
            var response = _browser.Patch("/", with =>
            {
                with.HttpRequest();
                with.Body("{\"name\": \"My New Name\", \"description\": \"This is a description\", \"createdAt\": \"2015-01-01T00:00:00\"}");
                with.Header("Content-Type", "application/json");
            });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void ShouldSet_Properties()
        {
            var response = _browser.Patch("/", with =>
            {
                with.HttpRequest();
                with.Body("{\"name\": \"My New Name\", \"description\": \"This is a description\", \"createdAt\": \"2015-01-01T00:00:00\"}");
                with.Header("Content-Type", "application/json");
            });
            var result = response.Body.DeserializeJson<TestModel>();

            Assert.AreEqual("My New Name", result.Name);
            Assert.AreEqual("This is a description", result.Description);
            Assert.AreEqual(new DateTime(2015, 1, 1, 0, 0, 0), result.CreatedAt);
        }

        [Test]
        public void With_EmptyDateTimeProperty_ShouldReturn_OkStatusCode()
        {
            var response = _browser.Patch("/", with =>
            {
                with.HttpRequest();
                with.Body("{\"createdAt\": \"\"}");
                with.Header("Content-Type", "application/json");
            });

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [Test]
        public void With_EmptyDateTimeProperty_ShouldSet_DateTimePropertyToDefaultValue()
        {
            var response = _browser.Patch("/", with =>
            {
                with.HttpRequest();
                with.Body("{\"createdAt\": \"\"}");
                with.Header("Content-Type", "application/json");
            });
            var result = response.Body.DeserializeJson<TestModel>();

            Assert.AreEqual(default(DateTime), result.CreatedAt);
        }

        [Test]
        public void ShouldIgnore_JsonIgnoreProperties()
        {
            var response = _browser.Patch("/", with =>
            {
                with.HttpRequest();
                with.Body(
                    "{\"id\": \"1\", \"description\":\"This is a description\", \"modelname\": \"Hello World\"}");
                with.Header("Content-Type", "application/json");
            });
            var result = response.Body.DeserializeJson<TestModel>();

            Assert.AreEqual(1, result.Id);
            Assert.AreEqual("This is a description", result.Description);
            Assert.AreEqual("TestModelWithJsonIgnore", result.ModelName);
        }
        
        private class MockModule : NancyModule
        {
            public MockModule()
                : base("/")
            {
                Patch["/"] = _ =>
                {
                    var model = BaseModel();

                    if (!this.Patch(model))
                    {
                        return Negotiate
                            .WithAllowedMediaRange(new MediaRange("application/json"))
                            .WithStatusCode(HttpStatusCode.UnprocessableEntity);
                    }
                    return Negotiate
                        .WithModel(model)
                        .WithAllowedMediaRange(new MediaRange("application/json"))
                        .WithStatusCode(HttpStatusCode.OK);
                };
            }
        }
    }
}
