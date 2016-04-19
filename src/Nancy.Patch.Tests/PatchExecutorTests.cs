using System.Linq;
using Nancy.Patch.Tests.Mocks;
using NUnit.Framework;

namespace Nancy.Patch.Tests
{
    [TestFixture]
    public class PatchExecutorTests
    {
        [Test]
        public void Patch_Should_Update_Target_Properties()
        {
            var from = new TestModel
            {
                Id = 2,
                Name = "New Test",
                Description = "Updated description",
                ShortDescription = "Updated short description"
            };
            var to = new TestModel
            {
                Id = 1,
                Name = "Test",
                Description = "This is a test",
                ShortDescription = "Short description"
            };
            var propertiesToMerge = new[]
            {
                "id",
                "name",
                "description",
                "shortdescription"
            };

            var result = new PatchExecutor().Patch(from, to, propertiesToMerge);

            Assert.IsTrue(result);
            Assert.AreEqual(from.Id, to.Id);
            Assert.AreEqual(from.Name, to.Name);
            Assert.AreEqual(from.Description, to.Description);
            Assert.AreEqual(from.ShortDescription, to.ShortDescription);
        }

        [Test]
        public void Patch_Should_Ignore_Properties_Not_In_PropertiesToMerge()
        {
            var from = new TestModel
            {
                Id = 2,
                Name = "New Test",
                Description = "Updated description",
                ShortDescription = "Updated short description"
            };
            var to = new TestModel
            {
                Id = 1,
                Name = "Test",
                Description = "This is a test",
                ShortDescription = "Short description"
            };
            var propertiesToMerge = new[]
            {
                "id",
                "name"
            };

            var result = new PatchExecutor().Patch(from, to, propertiesToMerge);

            Assert.IsTrue(result);
            Assert.AreEqual(from.Id, to.Id);
            Assert.AreEqual(from.Name, to.Name);
            Assert.AreNotEqual(from.Description, to.Description);
            Assert.AreNotEqual(from.ShortDescription, to.ShortDescription);
        }

        [Test]
        public void Patch_Should_Not_Update_If_No_PropertiesToMerge()
        {
            var from = new TestModel
            {
                Id = 2,
                Name = "New Test",
                Description = "Updated description",
                ShortDescription = "Updated short description"
            };
            var to = new TestModel
            {
                Id = 1,
                Name = "Test",
                Description = "This is a test",
                ShortDescription = "Short description"
            };
            var propertiesToMerge = Enumerable.Empty<string>();

            var result = new PatchExecutor().Patch(from, to, propertiesToMerge);

            Assert.IsTrue(result);
            Assert.AreNotEqual(from.Id, to.Id);
            Assert.AreNotEqual(from.Name, to.Name);
            Assert.AreNotEqual(from.Description, to.Description);
            Assert.AreNotEqual(from.ShortDescription, to.ShortDescription);
        }
    }
}
