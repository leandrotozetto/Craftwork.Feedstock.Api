using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Validations.Argument;
using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Interfaces.Applications;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Domain.Mappers;
using Craftwork.Feedstock.Api.Domain.Requests.Commands.Color;
using MediatR;
using System;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Application
{
    /// <summary>
    /// Contains related actions with colors in application layer.
    /// </summary>
    public class ColorApplication : IColorApplication
    {
        private readonly IColorRepository _colorRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Creates a new instance of ColorApplication <see cref="ColorApplication"/>.
        /// </summary>
        /// <param name="colorRepositorio"></param>
        public ColorApplication(IColorRepository colorRepositorio, IMediator mediator)
        {
            _colorRepository = colorRepositorio;
            _mediator = mediator;
        }

        ///// <summary>
        ///// Creates a new color.
        ///// </summary>
        ///// <param name="colorDto">Color <see cref="ColorDto"/> to be created.</param>
        ///// <returns>Returns quantity of entities affected.</returns>
        //public async Task InsertAsync(ColorCommandDto colorDto)
        //{
        //    Ensure.That(colorDto, "color").EntityIsNotNull();

        //    var entity = colorDto.ToEntity();

        //   // await _colorService.ValidateAsync(entity);

        //    entity.CreationDate = DateTime.UtcNow;

        //    await _colorRepository.InsertAsync(entity);
        //}

        ///// <summary>
        ///// Updates a color.
        ///// </summary>
        ///// <param name="id">Color's id will be update.</param>
        ///// <param name="colorDto">Color <see cref="ColorDto"/> to be updated.</param>
        ///// <returns>Returns quantity of entities affected.</returns>
        //public async Task UpdateAsync(Guid id, ColorDto colorDto)
        //{
        //    Ensure.That(id, nameof(id)).IdNotEmpty();
        //    Ensure.That(colorDto, "color").EntityIsNotNull();

        //    var current = await _colorRepository.GetAsync(id);

        //    Ensure.That(current, nameof(id)).EntityExists();

        //    var color = colorDto.ToEntity();

        //    color.Id = id;

        //    await _colorService.ValidateAsync(color);

        //    current.LastModificationDate = DateTime.UtcNow;
        //    current.Name = color.Name;
        //    current.Status = color.Status;

        //    await _colorRepository.UpdateAsync(current);
        //    //await _unityOfWork.CommitAsync();
        //}

        ///// <summary>
        ///// Get all color.
        ///// </summary>
        ///// <param name="filter">Filter to define color to be returned.</param>
        ///// <param name="orderBy">Columns name to order colors.</param>
        ///// <param name="page">Current page.</param>
        ///// <param name="qtyPerPage">Quantity of registers per page.</param>
        ///// <returns>Returns a pagination list of colors <see cref="IPagination{ColorDto}"/>.</returns>
        //public async ValueTask<IPagination<ColorDto>> ListAsync(string filter = null, string orderBy = null, int page = 0, int qtyPerPage = 0)
        //{
        //    var pagination = await _colorRepository.ListAsync(filter, orderBy, page, qtyPerPage);

        //    return pagination.ToDto();
        //}

        /// <summary>
        /// Get color by id.
        /// </summary>
        /// <param name="colorId">Color id.</param>
        /// <returns>Returns color found.</returns>
        public async Task<ColorQueryDto> GetAsync(Guid colorId)
        {
            var color = await _colorRepository.GetAsync(ColorId.New(colorId));

            return ColorMapper.Map(color);
        }

        /// <summary>
        /// Removes a color.
        /// </summary>
        /// <param name="id">ColorId that will be updated..</param>
        /// <returns>Returns quantity of entities affected.</returns>
        public async Task<IResponse> DeleteAsync(Guid id)
        {
            try
            {
                var notEmpty = Ensure.That(id, nameof(id)).IdNotEmpty();

                if (!notEmpty.IsValid)
                {
                    return Response.ValidationError(notEmpty.Error).Result;
                }

                var request = DeleteColorCommandRequest.New(id);

                var result = await _mediator.Send(request);

                //if (await _colorRepository.HasUserAssociated(id))
                //{
                //    throw new DomainException("Color", DomainMessage.EntityHasRelationship("perfil", new string[] { "usuários" }));
                //}

                //var color = await _colorRepository.GetAsync(id);

                //Ensure.That(color, nameof(id)).EntityExists();

                //await _colorRepository.DeleteAsync(color.Id);
                //await _unityOfWork.CommitAsync();

                return result ? Response.Success() : Response.Error();
            }
            catch
            {
                return Response.Error();
            }
        }

        private bool disposedValue = false;

        /// <summary>
        /// Dispose managed resource.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                   // _colorRepository.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose managed resource.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
