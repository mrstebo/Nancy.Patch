using System;
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
                ShortDescription = "Updated short description",
                CreatedAt = new DateTime(2015, 1, 2)
            };
            var to = new TestModel
            {
                Id = 1,
                Name = "Test",
                Description = "This is a test",
                ShortDescription = "Short description",
                CreatedAt = new DateTime(2015, 1, 1)
            };
            var propertiesToMerge = new[]
            {
                "id",
                "name",
                "description",
                "shortdescription",
                "createdat"
            };

            var result = new PatchExecutor().Patch(from, to, propertiesToMerge);

            Assert.IsTrue(result);
            Assert.AreEqual(from.Id, to.Id);
            Assert.AreEqual(from.Name, to.Name);
            Assert.AreEqual(from.Description, to.Description);
            Assert.AreEqual(from.ShortDescription, to.ShortDescription);
            Assert.AreEqual(from.CreatedAt, to.CreatedAt);
        }

        [Test]
        public void Patch_Should_Ignore_Properties_Not_In_PropertiesToMerge()
        {
            var from = new TestModel
            {
                Id = 2,
                Name = "New Test",
                Description = "Updated description",
                ShortDescription = "Updated short description",
                CreatedAt = new DateTime(2015, 1, 2)
            };
            var to = new TestModel
            {
                Id = 1,
                Name = "Test",
                Description = "This is a test",
                ShortDescription = "Short description",
                CreatedAt = new DateTime(2015, 1, 1)
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
            Assert.AreNotEqual(from.CreatedAt, to.CreatedAt);
        }

        [Test]
        public void Patch_Should_Not_Update_If_No_PropertiesToMerge()
        {
            var from = new TestModel
            {
                Id = 2,
                Name = "New Test",
                Description = "Updated description",
                ShortDescription = "Updated short description",
                CreatedAt = new DateTime(2015, 1, 2)
            };
            var to = new TestModel
            {
                Id = 1,
                Name = "Test",
                Description = "This is a test",
                ShortDescription = "Short description",
                CreatedAt = new DateTime(2015, 1, 1)
            };
            var propertiesToMerge = Enumerable.Empty<string>();

            var result = new PatchExecutor().Patch(from, to, propertiesToMerge);

            Assert.IsTrue(result);
            Assert.AreNotEqual(from.Id, to.Id);
            Assert.AreNotEqual(from.Name, to.Name);
            Assert.AreNotEqual(from.Description, to.Description);
            Assert.AreNotEqual(from.ShortDescription, to.ShortDescription);
            Assert.AreNotEqual(from.CreatedAt, to.CreatedAt);
        }

        [Test]
        public void Patch_Should_Allow_Nullifying_Existing_Properties()
        {
            var from = new TestModel
            {
                Name = string.Empty
            };
            var to = new TestModel
            {
                Name = "Test"
            };
            var propertiesToMerge = new[]
            {
                "name"
            };

            var result = new PatchExecutor().Patch(from, to, propertiesToMerge);

            Assert.IsTrue(result);
            Assert.AreEqual(from.Name, to.Name);
        }

	    [Test]
	    public void Patch_Given_Property_Not_In_Object_Should_Return_False_With_Message()
	    {
			var from = new TestModel
			{
				Name = "Original"
			};
			var to = new TestModel()
			{
				Name = "To"
			};
			var propertiesToMerge = new[]
            {
                "name",
                "iamnothere"
            };

			var result = new PatchExecutor().Patch(from, to, propertiesToMerge);

			Assert.IsFalse(result);
			Assert.AreEqual("Could not find writable property: iamnothere", result.Message);
	    }

	    [Test]
	    public void Patch_Given_Read_Only_Property_Should_Return_False_With_Message()
	    {
			var from = new TestModel
			{
				Name = "Original"
			};
			var to = new TestModel()
			{
				Name = "To"
			};
			var propertiesToMerge = new[]
            {
                "name",
                "readonlyname"
            };

			var result = new PatchExecutor().Patch(from, to, propertiesToMerge);

		    Assert.IsFalse(result);
			Assert.AreEqual("Could not find writable property: readonlyname", result.Message);
	    }
    }
}
