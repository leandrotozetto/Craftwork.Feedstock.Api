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
    /// Represents the color of any material. 
    /// </summary>
    public class Color : IEntity
    {
        /// <summary>
        /// Id.
        /// </summary>
        public ColorId ColorId { get; private set; }

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

        private Color() { }

        /// <summary>
        /// This constructor contains all fields of Color <see cref="Color"/>.
        /// </summary>
        /// <param name="id">Id of color.</param>
        /// <param name="name">Name of color.</param>
        /// <param name="status">Status of register.</param>
        /// <param name="creationDate">Date of create.</param>
        /// <param name="updateDate">Date of Update.</param>
        /// <param name="tenantId">Cliente Id.</param>
        /// <returns>Returns new instance of Color <see cref="Color"/></returns>
        public static Color New(
            ColorId colorId,
            string name,
            Status status,
            DateTime creationDate,
            DateTime? updateDate,
            TenantId tenantId)
        {
            var color = new Color
            {
                ColorId = colorId,
                Name = name,
                Status = status,
                CreationDate = creationDate,
                UpdateDate = updateDate,
                TenantId = tenantId
            };

            color.Isvalid();

            return color;
        }

        /// <summary>
        /// This constructor contains some fields of Color <see cref="Color"/>.
        /// </summary>
        /// <param name="name">Name of color.</param>
        /// <param name="status">Status of register.</param>
        /// <returns>Returns new instance of Color <see cref="Color"/></returns>
        public static Color New(
            string name,
            Status status,
            TenantId tenantId)
        {
            var color = new Color
            {
                ColorId = ColorId.New(),
                Name = name,
                Status = status,
                CreationDate = DateTime.Now,
                TenantId = tenantId
            };

            color.Isvalid();

            return color;
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
            var validator = Validator<Color>.New();

            validator.RuleFor(x => x.ColorId, ResourcesReader.Field(nameof(ColorId)))
                .NotNull();

            validator.RuleFor(x => x.ColorId.Value, ResourcesReader.Field(nameof(ColorId)))
                .Required()
                .When(x => x.ColorId != null);

            validator.RuleFor(x => x.Name, ResourcesReader.Field(nameof(Name)))
                .Required()
                .MaxLength(200);

            validator.RuleFor(x => x.TenantId, ResourcesReader.Field(nameof(TenantId)))
                .NotNull();

            validator.RuleFor(x => x.TenantId.Value, ResourcesReader.Field(nameof(TenantId)))
                .Required()
                .When(x => x.TenantId != null);

            return validator.Validate(this).IsValid;
        }
    }
}
