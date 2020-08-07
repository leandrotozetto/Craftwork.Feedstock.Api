using Craftwork.Feedstock.Api.Domain.Core.Resources;
using Craftwork.Feedstock.Api.Domain.Core.Validations;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Interfaces;
using System;

namespace Craftwork.Feedstock.Api.Domain.Entities
{
    /// <summary>
    /// Represents the measure of any material. 
    /// </summary>
    public class Measure : IEntity
    {
        /// <summary>
        /// Id.
        /// </summary>
        public MeasureId MeasureId { get; private set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Status.
        /// </summary>        
        public Status Status { get; private set; }

        /// <summary>
        /// Creation date
        /// </summary>
        public DateTime CreationDate { get; private set; }

        /// <summary>
        /// Update date.
        /// </summary>
        public DateTime? UpdateDate { get; private set; }

        /// <summary>
        /// Cliente Id.
        /// </summary>
        public TenantId TenantId { get; private set; }

        /// <summary>
        ///Default Constructor
        /// </summary>
        private Measure() { }

        /// <summary>
        /// This constructor contains all fields of Measure <see cref="Measure"/>.
        /// </summary>
        /// <param name="measureId">Id of measure.</param>
        /// <param name="name">Name of measure.</param>
        /// <param name="status">Status of register.</param>
        /// <param name="creationDate">Date of create.</param>
        /// <param name="updateDate">Date of Update.</param>
        /// <param name="tenantId">Cliente Id.</param>
        /// <returns>Returns new instance of Measure <see cref="Measure"/></returns>
        public static Measure New(
            MeasureId measureId,
            string name,
            Status status,
            DateTime creationDate,
            DateTime? updateDate,
            TenantId tenantId)
        {
            var measure = new Measure
            {
                MeasureId = measureId,
                Name = name,
                Status = status,
                CreationDate = creationDate,
                UpdateDate = updateDate,
                TenantId = tenantId
            };

            measure.Isvalid();

            return measure;
        }

        /// <summary>
        /// This constructor contains some fields of Measure <see cref="Measure"/>.
        /// </summary>
        /// <param name="name">Name of measure.</param>
        /// <param name="status">Status of register.</param>
        /// <param name="tenantId">Cliente Id.</param>
        /// <returns>Returns new instance of Measure <see cref="Measure"/></returns>
        public static Measure New(
            string name,
            Status status,
            TenantId tenantId)
        {
            var measure = new Measure
            {
                Name = name,
                Status = status,
                CreationDate = DateTime.Now,
                UpdateDate = null,
                TenantId = tenantId,
                MeasureId = MeasureId.New()
            };

            measure.Isvalid();

            return measure;
        }

        /// <summary>
        /// Changes the name property value .
        /// </summary>
        /// <param name="name">New value.</param>
        public void ChangeName(string name)
        {
            Ensure.That(name, nameof(name)).IsNullOrWhiteSpace();

            if (!string.Equals(name, Name))
            {
                Name = name;
                UpdateDate = DateTime.Now;
            }
        }

        /// <summary>
        /// Chages the status property value.
        /// </summary>
        /// <param name="status">New value.</param>
        public void ChangeStatus(Status status)
        {
            if (!status.Equals(Status))
            {
                Status = status;
                UpdateDate = DateTime.Now;
            }
        }

        /// <summary>
        /// Checks if the object is valid.
        /// </summary>
        /// <returns>Returns boolean value to express the status.</returns>
        public bool Isvalid()
        {
            var validator = Validator<Measure>.New();

            validator.RuleFor(x => x.Name, ResourcesReader.Field(nameof(Name)))
                .Required()
                .MaxLength(200);

            validator.RuleFor(x => x.TenantId, ResourcesReader.Field(nameof(TenantId)))
                .NotNull();

            validator.RuleFor(x => x.TenantId.Value, ResourcesReader.Field(nameof(TenantId)))
                .Required()
                .When(x => x.TenantId != null);

            validator.RuleFor(x => x.MeasureId, ResourcesReader.Field(nameof(MeasureId)))
                .NotNull();

            validator.RuleFor(x => x.MeasureId.Value, ResourcesReader.Field(nameof(MeasureId)))
                .Required()
                .When(x => x.MeasureId != null);

            return validator.Validate(this).IsValid;
        }
    }
}
