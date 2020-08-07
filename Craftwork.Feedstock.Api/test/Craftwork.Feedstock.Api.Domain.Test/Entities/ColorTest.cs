using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Core.Exceptions;
using Craftwork.Feedstock.Api.Domain.Core.Resources;
using Craftwork.Feedstock.Api.Domain.Core.Validations;
using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using Craftwork.Feedstock.Api.Domain.Entities;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System;
using System.Linq;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Entities
{
    public class ColorTest
    {
        private readonly string _colorName;

        private readonly Status _status;

        private readonly TenantId _tenantId;

        private readonly ColorId _colorId;

        private readonly DateTime _creationDate;

        private readonly DateTime _updateDate;

        private readonly Color _color;

        public ColorTest()
        {
            _colorName = "blue";
            _status = StatusEnum.Enable;
            _tenantId = TenantId.New();
            _colorId = ColorId.New();
            _creationDate = DateTime.Now;
            _updateDate = DateTime.Now.AddHours(1);
            _color = Color.New(_colorName, _status, _tenantId);
        }

        [Fact]
        public void Shoul_Create_New_Color()
        {
            var color = Color.New(_colorName, _status, _tenantId);

            Assert.NotNull(color);
            Assert.Equal(_colorName, color.Name);
            Assert.Equal(_status, color.Status);
            Assert.NotEqual(DateTime.MinValue, color.CreationDate);
            Assert.Null(color.UpdateDate);
            Assert.False(color.TenantId.Equals(ColorId.Empty));
            Assert.True(color.TenantId.Equals(_tenantId));
        }

        [Fact]
        public void Shoul_Create_New_Color_With_All_Information()
        {
            var color = Color.New(_colorId, _colorName, _status, _creationDate, _updateDate, _tenantId);

            Assert.NotNull(color);
            Assert.Equal(_colorName, color.Name);
            Assert.Equal(_status, color.Status);
            Assert.Equal(_creationDate, color.CreationDate);
            Assert.Equal(_updateDate, color.UpdateDate);
            Assert.True(color.ColorId.Equals(_colorId));
            Assert.True(color.TenantId.Equals(_tenantId));
        }

        [Fact]
        public void Shoul_Not_Create_New_Color_With_Name_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() => Color.New("", _status, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_color.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Shoul_Not_Create_New_Color_With_Name_Is_Null()
        {
            var domainException = Assert.Throws<DomainException>(() => Color.New(null, _status, _tenantId));

            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_color.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Shoul_Not_Create_New_Color_With_Name_Is_EmptySpace()
        {
            var domainException = Assert.Throws<DomainException>(() => Color.New(" ", _status, _tenantId));

            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_color.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Change_Name()
        {
            var color = Color.New(_colorName, _status, _tenantId);
            var newColor = "red";

            color.ChangeName(newColor);

            Assert.NotNull(color);
            Assert.Equal(newColor, color.Name);
            Assert.NotNull(color.UpdateDate);
        }

        [Fact]
        public void Should_Not_Change_Name()
        {
            var color = Color.New(_colorName, _status, _tenantId);

            color.ChangeName(_colorName);

            Assert.NotNull(color);
            Assert.Null(color.UpdateDate);
        }

        [Fact]
        public void Should_Change_Status()
        {
            var color = Color.New(_colorName, _status, _tenantId);
            var newStatus = StatusEnum.Disable;

            color.ChangeStatus(newStatus);

            Assert.NotNull(color);
            Assert.Equal(newStatus, color.Status);
            Assert.NotNull(color.UpdateDate);
        }

        [Fact]
        public void Should_Not_Change_Status()
        {
            var color = Color.New(_colorName, _status, _tenantId);

            color.ChangeStatus(_status);

            Assert.NotNull(color);
            Assert.Null(color.UpdateDate);
        }

        [Fact]
        public void Should_Be_Valid()
        {
            var color = Color.New(_colorName, _status, _tenantId);

            Assert.NotNull(color);
            Assert.True(color.Isvalid());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_Name_Has_More_Than_200_Chars()
        {
            var name = @"01234567890123456789012345678901234567890123456789
                        01234567890123456789012345678901234567890123456789
                        01234567890123456789012345678901234567890123456789
                        01234567890123456789012345678901234567890123456789
                        0123456789";

            var color = Color.New(_colorName, _status, _tenantId);
            color.ChangeName(name);

            Assert.NotNull(color);

            var fieldName = ResourcesReader.Field(nameof(color.Name));
            var domainError = GetDomainExceptionError(color);

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.MaxLength(fieldName, 200), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_TenantId_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Color.New(_colorId, _colorName, _status, _creationDate, _updateDate, TenantId.Empty));

            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_color.TenantId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_TenantId_Is_Null()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Color.New(_colorId, _colorName, _status, _creationDate, _updateDate, null));

            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_color.TenantId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.NotNull(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_ColorId_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Color.New(ColorId.Empty, _colorName, _status, _creationDate, _updateDate, _tenantId));

            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_color.ColorId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_ColorId_Is_Null()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Color.New(null, _colorName, _status, _creationDate, _updateDate, _tenantId));

            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_color.ColorId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.NotNull(fieldName), domainError.Messages.First());
        }

        private DomainError GetDomainExceptionError(Color color)
        {
            var exception = Assert.Throws<DomainException>(() => color.Isvalid());
            var domainError = exception.Errors.First();

            return domainError;
        }
    }
}
