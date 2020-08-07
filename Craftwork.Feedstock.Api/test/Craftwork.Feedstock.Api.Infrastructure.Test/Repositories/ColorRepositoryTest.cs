using Craftwork.Feedstock.Api.Domain.Core.Entities;
using Craftwork.Feedstock.Api.Domain.Core.Enum;
using Craftwork.Feedstock.Api.Domain.Core.Exceptions;
using Craftwork.Feedstock.Api.Domain.Entities;
using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using Craftwork.Feedstock.Api.Domain.Interfaces.Repositories;
using Craftwork.Feedstock.Api.Infrastructure.Repositories;
using Moq;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Repositories
{
    public class ColorRepositoryTest
    {
        private readonly ColorRepository _colorRepository;
        private readonly ColorId _newColorId = ColorId.New();
        private readonly Color _color = Color.New("blue", StatusEnum.Enable, TenantId.New());
        private bool _update;
        private bool _insert;
        private bool _get;
        private bool _list;
        private bool _delete;

        public ColorRepositoryTest()
        {
            _colorRepository = new ColorRepository(CreateRepository());

            _update = false;
            _insert = false;
            _get = false;
            _list = false;
        }

        private IRepository<Color> CreateRepository()
        {
            var repository = new Mock<IRepository<Color>>();

            repository.Setup(x => x.InsertAsync(It.IsAny<Color>()))
                .Callback((Color color) =>
                {
                    if (color is null)
                    {
                        throw new DomainException(Enumerable.Empty<DomainError>());
                    }

                    _insert = true;
                });

            repository.Setup(x => x.UpdateAsync(It.IsAny<Color>()))
                .Callback((Color color) =>
                {
                    if (color is null)
                    {
                        throw new DomainException(Enumerable.Empty<DomainError>());
                    }

                    _update = true;
                });

            repository.Setup(x => x.Delete(It.IsAny<Color>()))
                .Callback((Color color) =>
                {
                    if (color is null)
                    {
                        throw new DomainException(Enumerable.Empty<DomainError>());
                    }

                    _delete = true;
                });

            repository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Color, bool>>>()))
                .ReturnsAsync((Expression<Func<Color, bool>> filter) =>
                {
                    if (filter is null)
                    {
                        throw new DomainException(Enumerable.Empty<DomainError>());
                    }

                    _get = true;

                    return Color.New("Name", StatusEnum.Enable, TenantId.New());
                });

            repository.Setup(x => x.ListAsync(It.IsAny<Expression<Func<Color, bool>>>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns((Expression<Func<Color, bool>> filter, string orderBy, int page, int qtyPerPage) =>
                {
                    if (filter is null)
                    {
                        throw new DomainException(Enumerable.Empty<DomainError>());
                    }

                    _list = true;

                    var func = filter.Compile();
                    var result = func.Invoke(_color);

                    if (result)
                    {
                        return Task.FromResult(Pagination<Color>.New(new Color[] { _color }, 1, 1, 1));
                    }

                    return Task.FromResult(Pagination<Color>.Empty);
                });

            return repository.Object;
        }

        [Fact]
        public async Task Should_Create_Color()
        {
            var color = Color.New("blue", StatusEnum.Enable, TenantId.New());

            await _colorRepository.InsertAsync(color);

            Assert.True(_insert);
        }

        [Fact]
        public void Should_Not_Create_Color_When_Color_Is_Null()
        {
            Assert.ThrowsAsync<DomainException>(() => _colorRepository.InsertAsync(null));
            Assert.False(_insert);
        }

        [Fact]
        public async Task Should_Update_Color()
        {
            var color = Color.New("blue", StatusEnum.Enable, TenantId.New());

            await _colorRepository.UpdateAsync(color);

            Assert.True(_update);
        }

        [Fact]
        public void Should_Not_Update_Color_When_Color_Is_Null()
        {
            Assert.ThrowsAsync<DomainException>(() => _colorRepository.UpdateAsync(null));
            Assert.False(_update);
        }

        [Fact]
        public async Task Should_Get_Color_By_ColorId()
        {
            await _colorRepository.GetAsync(_newColorId);

            Assert.True(_get);
        }

        [Fact]
        public void Should_Not_Get_Color_By_ColorId_When_ColorId_Is_Null()
        {
            Assert.False(_get);
            Assert.ThrowsAsync<DomainException>(() => _colorRepository.GetAsync(null));
        }

        [Fact]
        public async Task Should_Get_List_Color()
        {
            var color = Color.New("blue", StatusEnum.Enable, TenantId.New());

            var pagination = await _colorRepository.ListAsync(x => x.ColorId != ColorId.Empty);

            Assert.Single(pagination.Entities);
            Assert.True(_list);
        }

        [Fact]
        public async Task Should_Delete_Color()
        {
            var colorid = ColorId.New();

            await _colorRepository.DeleteAsync(colorid);

            Assert.True(_delete);
        }
    }
}