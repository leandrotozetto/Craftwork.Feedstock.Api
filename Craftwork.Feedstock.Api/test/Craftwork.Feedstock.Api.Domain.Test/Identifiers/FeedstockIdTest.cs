using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Identifiers
{
    public class FeedstockIdTest
    {
        [Fact]
        public void Should_Create_FeedstockId_Whithout_Id()
        {
            var feedstockId = FeedstockId.New();

            Assert.True(Guid.TryParse(feedstockId.Value.ToString(), out var _));
            Assert.NotEqual(Guid.Empty, feedstockId.Value);
        }

        [Fact]
        public void Should_Not_Create_FeedstockIdd_When_Id_Is_Empty()
        {
            Assert.Throws<Exception>(() => FeedstockId.New(Guid.Empty));
        }

        [Fact]
        public void Should_Create_FeedstockId_Whith_Id()
        {
            var id = Guid.NewGuid();
            var feedstockId = FeedstockId.New(id);

            Assert.True(Guid.TryParse(feedstockId.Value.ToString(), out var _));
            Assert.NotEqual(Guid.Empty, feedstockId.Value);
            Assert.Equal(id, feedstockId.Value);
        }

        [Fact]
        public void Should_Return_Empty()
        {
            var feedstockId = FeedstockId.Empty;

            Assert.True(Guid.TryParse(feedstockId.Value.ToString(), out var _));
            Assert.Equal(Guid.Empty, feedstockId.Value);
        }
    }
}
