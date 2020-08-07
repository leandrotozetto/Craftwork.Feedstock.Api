using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Identifiers
{
    public class ColorIdTest
    {
        [Fact]
        public void Should_Create_ColorId_Whithout_Id()
        {
            var colorId = ColorId.New();

            Assert.True(Guid.TryParse(colorId.Value.ToString(), out var _));
            Assert.NotEqual(Guid.Empty, colorId.Value);
        }

        [Fact]
        public void Should_Not_Create_ColorId_When_Id_Is_Empty()
        {
            Assert.Throws<Exception>(() => ColorId.New(Guid.Empty));
        }

        [Fact]
        public void Should_Create_ColorId_Whith_Id()
        {
            var id = Guid.NewGuid();
            var colorId = ColorId.New(id);

            Assert.True(Guid.TryParse(colorId.Value.ToString(), out var _));
            Assert.NotEqual(Guid.Empty, colorId.Value);
            Assert.Equal(id, colorId.Value);
        }


        [Fact]
        public void Should_Return_Empty()
        {
            var colorId = ColorId.Empty;

            Assert.True(Guid.TryParse(colorId.Value.ToString(), out var _));
            Assert.Equal(Guid.Empty, colorId.Value);
        }
    }
}
