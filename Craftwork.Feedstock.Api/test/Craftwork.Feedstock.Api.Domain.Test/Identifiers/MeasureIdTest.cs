using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Identifiers
{
    public class MeasureIdTest
    {
        [Fact]
        public void Should_Create_MeasureId_Whithout_Id()
        {
            var measureId = MeasureId.New();
            Assert.True(Guid.TryParse(measureId.Value.ToString(), out _));
            Assert.NotEqual(Guid.Empty, measureId.Value);
        }

        [Fact]
        public void Should_Not_Create_MeasureId_When_Id_Is_Empty()
        {
            Assert.Throws<Exception>(() => MeasureId.New(Guid.Empty));
        }

        [Fact]
        public void Should_Create_MeasureId_Whith_Id()
        {
            var id = Guid.NewGuid();
            var measureId = MeasureId.New(id);

            Assert.True(Guid.TryParse(measureId.Value.ToString(), out var _));
            Assert.NotEqual(Guid.Empty, measureId.Value);
            Assert.Equal(id, measureId.Value);
        }

        [Fact]
        public void Should_Return_Empty()
        {
            var measureId = MeasureId.Empty;

            Assert.True(Guid.TryParse(measureId.Value.ToString(), out var _));
            Assert.Equal(Guid.Empty, measureId.Value);
        }
    }
}
