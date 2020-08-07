using Craftwork.Feedstock.Api.Domain.Core.Resources;
using Craftwork.Feedstock.Api.Domain.Core.Validations;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using Craftwork.Feedstock.Api.Domain.Core.ValuesObjects;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Interfaces;
using System;

namespace Craftwork.Feedstock.Api.Domain.Entities
{
    public class Feedstock : IEntity
    {
        /// <summary>
        /// Id.
        /// </summary>
        public FeedstockId FeedstockId { get; private set; }

        /// <summary>
        /// Name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Id of color.
        /// </summary>
        public ColorId ColorId { get; private set; }

        /// <summary>
        /// Id of measure.
        /// </summary>
        public MeasureId MeasureId { get; private set; }

        /// <summary>
        /// Value of stock
        /// </summary>
        public int Stock { get; private set; }

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
        private Feedstock() { }

        /// <summary>
        /// This constructor contains all fields of Measure <see cref="Measure"/>.
        /// </summary>
        /// <param name="id">Id of measure.</param>
        /// <param name="name">Name of measure.</param>
        /// <param name="status">Status of register.</param>
        /// <param name="creationDate">Date of create.</param>
        /// <param name="updateDate">Date of Update.</param>
        /// <param name="measureId">Measure Id.</param>
        /// <param name="stock">Quantity in stock.</param>
        /// <param name="colorId">Id of color.</param>
        /// <param name="tenantId">Cliente Id.</param>
        /// <returns>Returns new instance of Measure <see cref="Feedstock"/></returns>
        public static Feedstock New(
            FeedstockId feedstockId,
            string name,
            Status status,
            DateTime creationDate,
            DateTime? updateDate,
            MeasureId measureId,
            int stock,
            ColorId colorId,
            TenantId tenantId)
        {
            //Ensure.That(feedstockId, ResourcesReader.Field(nameof(feedstockId))).ArgumentIsNotNull();
            //Ensure.That(colorId, ResourcesReader.Field(nameof(colorId))).ArgumentIsNotNull();
            //Ensure.That(tenantId, ResourcesReader.Field(nameof(tenantId))).ArgumentIsNotNull();
            //Ensure.That(measureId, ResourcesReader.Field(nameof(measureId))).ArgumentIsNotNull();

            var feedstock =  new Feedstock
            {
                FeedstockId = feedstockId,
                Name = name,
                Status = status,
                CreationDate = creationDate,
                UpdateDate = updateDate,
                MeasureId = measureId,
                Stock = stock,
                ColorId = colorId,
                TenantId = tenantId
            };

            feedstock.Isvalid();

            return feedstock;
        }


        /// <summary>
        /// This constructor contains some fields of Measure <see cref="Feedstock"/>.
        /// </summary>
        /// <param name="name">Name of measure.</param>
        /// <param name="status">Status of register.</param>
        /// <param name="measureId">Measure Id.</param>
        /// <param name="stock">Quantity in stock.</param>
        /// <param name="colorId">Id of color.</param>
        /// <param name="tenantId">Cliente Id.</param>
        /// <returns>Returns new instance of Measure <see cref="Feedstock"/></returns>
        public static Feedstock New(
            string name,
            Status status,
            MeasureId measureId,
            int stock,
            ColorId colorId,
            TenantId tenantId)
        {
            var feedstock =  new Feedstock
            {
                Name = name,
                Status = status,
                CreationDate = DateTime.Now,
                UpdateDate = null,
                MeasureId = measureId,
                Stock = stock,
                ColorId = colorId,
                TenantId = tenantId,
                FeedstockId = FeedstockId.New()
            };

            feedstock.Isvalid();

            return feedstock;
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
            var validator = Validator<Feedstock>.New();

            validator.RuleFor(x => x.Name, ResourcesReader.Field(nameof(Name)))
                .Required()
                .MaxLength(200);

            validator.RuleFor(x => x.MeasureId, ResourcesReader.Field(nameof(MeasureId)))
                .NotNull();

            validator.RuleFor(x => x.MeasureId.Value, ResourcesReader.Field(nameof(MeasureId)))
                .Required()
                .When(x => x.MeasureId != null);

            validator.RuleFor(x => x.TenantId, ResourcesReader.Field(nameof(TenantId)))
                .NotNull();

            validator.RuleFor(x => x.TenantId.Value, ResourcesReader.Field(nameof(TenantId)))
                .Required()
                .When(x=> x.TenantId != null);

            validator.RuleFor(x => x.FeedstockId, ResourcesReader.Field(nameof(FeedstockId)))
                .NotNull();

            validator.RuleFor(x => x.FeedstockId.Value, ResourcesReader.Field(nameof(FeedstockId)))
                .Required()
                .When(x => x.FeedstockId != null);

            validator.RuleFor(x => x.ColorId, ResourcesReader.Field(nameof(ColorId)))
                .NotNull();

            validator.RuleFor(x => x.ColorId.Value, ResourcesReader.Field(nameof(ColorId)))
                .Required()
                .When(x => x.ColorId != null);

            return validator.Validate(this).IsValid;
        }
    }
}
