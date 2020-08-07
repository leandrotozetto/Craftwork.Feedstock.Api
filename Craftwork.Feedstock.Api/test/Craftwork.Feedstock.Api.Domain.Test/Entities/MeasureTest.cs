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
    public class MeasureTest
    {
        private readonly string _measureName = "centimeter";

        private readonly Status _status = StatusEnum.Enable;

        private readonly TenantId _tenantId = TenantId.New();

        private readonly MeasureId _measureId = MeasureId.New();

        private readonly DateTime _creationDate = DateTime.Now;

        private readonly DateTime _updateDate = DateTime.Now.AddHours(1);

        private readonly Measure _measure;

        public MeasureTest()
        {
            _measureName = "centimeter";
            _status = StatusEnum.Enable;
            _tenantId = TenantId.New();
            _measureId = MeasureId.New();
            _creationDate = DateTime.Now;
            _updateDate = DateTime.Now.AddHours(1);
            _measure = Measure.New(_measureName, _status, _tenantId);
        }

        [Fact]
        public void Shoul_Create_New_Measure()
        {
            var measure = Measure.New(_measureName, _status, _tenantId);

            Assert.NotNull(measure);
            Assert.Equal(_measureName, measure.Name);
            Assert.Equal(_status, measure.Status);
            Assert.NotEqual(DateTime.MinValue, measure.CreationDate);
            Assert.Null(measure.UpdateDate);
            Assert.False(measure.MeasureId.Equals(MeasureId.Empty));
            Assert.True(measure.TenantId.Equals(_tenantId));
        }

        [Fact]
        public void Shoul_Create_New_Measure_With_All_Information()
        {
            var measure = Measure.New(_measureId, _measureName, _status, _creationDate, _updateDate, _tenantId);

            Assert.NotNull(measure);
            Assert.Equal(_measureName, measure.Name);
            Assert.Equal(_status, measure.Status);
            Assert.Equal(_creationDate, measure.CreationDate);
            Assert.Equal(_updateDate, measure.UpdateDate);
            Assert.True(measure.MeasureId.Equals(_measureId));
            Assert.True(measure.TenantId.Equals(_tenantId));
        }
        [Fact]
        public void Shoul_Not_Create_New_Measure_With_Name_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() => Measure.New("", _status, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_measure.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Shoul_Not_Create_New_Measure_With_Name_Is_Null()
        {
            var domainException = Assert.Throws<DomainException>(() => Measure.New(null, _status, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_measure.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Shoul_Not_Create_New_Measure_With_Name_Is_EmptySpace()
        {
            var domainException = Assert.Throws<DomainException>(() => Measure.New(" ", _status, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_measure.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Change_Name()
        {
            var measure = Measure.New(_measureName, _status, _tenantId);
            var newMeasure = "meter";

            measure.ChangeName(newMeasure);

            Assert.NotNull(measure);
            Assert.Equal(newMeasure, measure.Name);
            Assert.NotNull(measure.UpdateDate);
        }

        [Fact]
        public void Should_Not_Change_Name()
        {
            var measure = Measure.New(_measureName, _status, _tenantId);

            measure.ChangeName(_measureName);

            Assert.NotNull(measure);
            Assert.Null(measure.UpdateDate);
        }

        [Fact]
        public void Should_Change_Status()
        {
            var measure = Measure.New(_measureName, _status, _tenantId);
            var newStatus = StatusEnum.Disable;

            measure.ChangeStatus(newStatus);

            Assert.NotNull(measure);
            Assert.Equal(newStatus, measure.Status);
            Assert.NotNull(measure.UpdateDate);
        }

        [Fact]
        public void Should_Not_Change_Status()
        {
            var measure = Measure.New(_measureName, _status, _tenantId);

            measure.ChangeStatus(_status);

            Assert.NotNull(measure);
            Assert.Null(measure.UpdateDate);
        }

        [Fact]
        public void Should_Be_Valid()
        {
            var measure = Measure.New(_measureName, _status, _tenantId);

            Assert.NotNull(measure);
            Assert.True(measure.Isvalid());
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
                Measure.New(name, _status, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_measure.Name));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.MaxLength(fieldName, 200), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_TenantId_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Measure.New(_measureId, _measureName, _status, _creationDate, _updateDate, TenantId.Empty));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_measure.TenantId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_TenantId_Is_Nully()
        {
            var domainException = Assert.Throws<DomainException>(() =>
                Measure.New(_measureId, _measureName, _status, _creationDate, _updateDate, null));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_measure.TenantId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.NotNull(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_MeasureId_Is_Empty()
        {
            var domainException = Assert.Throws<DomainException>(() =>
               Measure.New(MeasureId.Empty, _measureName, _status, _creationDate, _updateDate, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_measure.MeasureId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.Required(fieldName), domainError.Messages.First());
        }

        [Fact]
        public void Should_Not_Be_Valid_When_MeasureId_Is_Null()
        {
            var domainException = Assert.Throws<DomainException>(() =>
               Measure.New(null, _measureName, _status, _creationDate, _updateDate, _tenantId));
            var domainError = domainException.Errors.First();

            var fieldName = ResourcesReader.Field(nameof(_measure.MeasureId));

            Assert.Equal(fieldName, domainError.Property);
            Assert.Equal(ValidationMessage.NotNull(fieldName), domainError.Messages.First());
        }
    }
}
