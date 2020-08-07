using Craftwork.Feedstock.Api.Domain.Entities.Identifiers;
using System;
using Xunit;

namespace Craftwork.Feedstock.Api.Domain.Test.Identifiers
{
    public class TenantIdTest
    {
        [Fact]
        public void Should_Create_NewTenantId_Whithout_Id()
        {
            var tenantId = TenantId.New();

            Assert.True(Guid.TryParse(tenantId.Value.ToString(), out var _));
            Assert.NotEqual(Guid.Empty, tenantId.Value);
        }

        [Fact]
        public void Should_Not_Create_TenantId_When_Id_Is_Empty()
        {
            Assert.Throws<Exception>(() => TenantId.New(Guid.Empty));
        }

        [Fact]
        public void Should_Create_NewTenantId_Whith_Id()
        {
            var id = Guid.NewGuid();
            var tenantId = TenantId.New(id);

            Assert.True(Guid.TryParse(tenantId.Value.ToString(), out var _));
            Assert.NotEqual(Guid.Empty, tenantId.Value);
            Assert.Equal(id, tenantId.Value);
        }

        [Fact]
        public void Should_Return_Empty()
        {
            var tenantId = TenantId.Empty;

            Assert.True(Guid.TryParse(tenantId.Value.ToString(), out var _));
            Assert.Equal(Guid.Empty, tenantId.Value);
        }
    }
}
