using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Core.Exceptions;
using Craftwork.Feedstock.Api.Domain.Core.Resources;
using Craftwork.Feedstock.Api.Domain.Core.Validations;
using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System;
using System.Linq;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Entities
{
    public class FeedstockTest
    {
        private readonly string _feedstockName;

        private readonly Status _status;

        private readonly MeasureId _measureId;

        private readonly TenantId _tenantId;

        private readonly ColorId _colorId;

        private readonly int _stock;

        private readonly FeedstockId _feedstockId;

        private readonly DateTime _creationDate;

        private readonly DateTime _updateDate;

        private readonly Domain.Entities.Feedstock _feedstock;

        public FeedstockTest()
        {
            _feedstockName = "leather";
            _status = StatusEnum.Enable;
            _measureId = MeasureId.New();
            _tenantId = TenantId.New();
            _colorId = ColorId.New();
            _stock = 10;
            _feedstockId = FeedstockId.New();
            _creationDate = DateTime.Now;
            _updateDate = DateTime.Now.AddHours(1);
            _feedstock = Domain.Entities.Feedstock.New(_feedstockName, _status, _measureId, _stock, _colorId, _tenantId);
        }

        [Fact]
        public void Shoul_Create_New_Feedstock()
        {
            var feedstock = Domain.Entities.Feedstock.New(_feedstockName, _status, _measureId, _stock, _colorId, _tenantId);

            Assert.NotNull(feedstock);
            Assert.Equal(_feedstockName, feedstock.Name);
            Assert.Equal(_status, feedstock.Status);
            Assert.Equal(_stock, feedstock.Stock);
            Assert.NotEqual(DateTime.MinValue, feedstock.CreationDate);
            Assert.Null(feedstock.UpdateDate);
            Assert.True(feedstock.MeasureId.Equals(_measureId));
            Assert.True(feedstock.TenantId.Equals(_tenantId));
            Assert.True(feedstock.ColorId.Equals(_colorId));
            Assert.False(feedstock.FeedstockId.Equals(FeedstockId.Empty));
        }

        [Fact]
        public void Shoul_Create_New_Feedstock_With_All_Information()
        {
            var feedstock = Domain.Entities.Feedstock.New(_feedstockId, _feedstockName, _status, _creationDate, _updateDate, _measureId, _stock, _colorId, _tenantId);

            Assert.NotNull(feedstock);
            Assert.Equal(_feedstockName, feedstock.Name);
            Assert.Equal(_status, feedstock.Status);
            Assert.Equal(_stock, feedstock.Stock);
            Assert.Equal(_creationDate, feedstock.CreationDate);
            Assert.Equal(_updateDate, feedstock.UpdateDate);
            Assert.True(feedstock.MeasureId.Equals(_measureId));
            Assert.True(feedstock.TenantId.Equals(_tenantId));
            Assert.True(feedstock.ColorId.Equals(_colorId));
            Assert.True(feedstock.FeedstockId.Equals(_feedstockId));
        }

        [Fact]
        public void Shoul_Not_Create_New_Feedstock_With_Name_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() => Domain.Entities.Feedstock.New("", _status, _measureId, _stock, _colorId, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_feedstock.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Shoul_Not_Create_New_Feedstock_With_Name_Is_Null()
        {
            var domainException = Assert.Throws<DomainException>(() => Domain.Entities.Feedstock.New(null, _status, _measureId, _stock, _colorId, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_feedstock.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Shoul_Not_Create_New_Feedstock_With_Name_Is_EmptySpace()
        {
            var domainException = Assert.Throws<DomainException>(() => Domain.Entities.Feedstock.New(" ", _status, _measureId, _stock, _colorId, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_feedstock.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Change_Name()
        {
            var feedstock = Domain.Entities.Feedstock.New(_feedstockName, _status, _measureId, _stock, _colorId, _tenantId);
            var newFeedstock = "cotton";

            feedstock.ChangeName(newFeedstock);

            Assert.NotNull(feedstock);
            Assert.Equal(newFeedstock, feedstock.Name);
            Assert.NotNull(feedstock.UpdateDate);
        }

        [Fact]
        public void Should_Not_Change_Name()
        {
            var feedstock = Domain.Entities.Feedstock.New(_feedstockName, _status, _measureId, _stock, _colorId, _tenantId);

            feedstock.ChangeName(_feedstockName);

            Assert.NotNull(feedstock);
            Assert.Null(feedstock.UpdateDate);
        }

        [Fact]
        public void Should_Change_Status()
        {
            var feedstock = Domain.Entities.Feedstock.New(_feedstockName, _status, _measureId, _stock, _colorId, _tenantId);
            var newStatus = StatusEnum.Disable;

            feedstock.ChangeStatus(newStatus);

            Assert.NotNull(feedstock);
            Assert.Equal(newStatus, feedstock.Status);
            Assert.NotNull(feedstock.UpdateDate);
        }

        [Fact]
        public void Should_Not_Change_Status()
        {
            var feedstock = Domain.Entities.Feedstock.New(_feedstockName, _status, _measureId, _stock, _colorId, _tenantId);

            feedstock.ChangeStatus(_status);

            Assert.NotNull(feedstock);
            Assert.Null(feedstock.UpdateDate);
        }

        [Fact]
        public void Should_Be_Valid()
        {
            var feedstock = Domain.Entities.Feedstock.New(_feedstockName, _status, _measureId, _stock, _colorId, _tenantId);

            Assert.NotNull(feedstock);
            Assert.True(feedstock.Isvalid());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_Name_Has_More_Than_200_Chars()
        {
            var name = @"01234567890123456789012345678901234567890123456789
                        01234567890123456789012345678901234567890123456789
                        01234567890123456789012345678901234567890123456789
                        01234567890123456789012345678901234567890123456789
                        0123456789";
            var domainException = Assert.Throws<DomainException>(() =>
                Domain.Entities.Feedstock.New(name, _status, _measureId, _stock, _colorId, _tenantId));
            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_feedstock.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.MaxLength(fieldName, 200), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_TenantId_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Domain.Entities.Feedstock.New(_feedstockId, _feedstockName, _status, _creationDate, _updateDate, _measureId, _stock, _colorId, TenantId.Empty));
            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_feedstock.TenantId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_TenantId_Is_Null()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Domain.Entities.Feedstock.New(_feedstockId, _feedstockName, _status, _creationDate, _updateDate, _measureId, _stock, null, null));
            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_feedstock.TenantId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.NotNull(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_ColorId_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Domain.Entities.Feedstock.New(_feedstockId, _feedstockName, _status, _creationDate, _updateDate, _measureId, _stock, ColorId.Empty, _tenantId));
            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_feedstock.ColorId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_ColorId_Is_Null()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Domain.Entities.Feedstock.New(_feedstockId, _feedstockName, _status, _creationDate, _updateDate, _measureId, _stock, null, _tenantId));
            var domainError = domainException.Errors.First();
            var fieldName = ResourcesReader.Field(nameof(_feedstock.ColorId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.NotNull(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_FeedstockId_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Domain.Entities.Feedstock.New(FeedstockId.Empty, _feedstockName, _status, _creationDate, _updateDate, _measureId, _stock, _colorId, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_feedstock.FeedstockId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_FeedstockId_Is_Null()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Domain.Entities.Feedstock.New(null, _feedstockName, _status, _creationDate, _updateDate, _measureId, _stock, _colorId, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_feedstock.FeedstockId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.NotNull(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_MeasureId_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Domain.Entities.Feedstock.New(_feedstockId, _feedstockName, _status, _creationDate, _updateDate, MeasureId.Empty, _stock, _colorId, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_feedstock.MeasureId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_MeasureId_Is_Null()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Domain.Entities.Feedstock.New(_feedstockId, _feedstockName, _status, _creationDate, _updateDate, null, _stock, _colorId, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_feedstock.MeasureId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.NotNull(fieldName), domainError.Messages.First());
        }
    }
}
