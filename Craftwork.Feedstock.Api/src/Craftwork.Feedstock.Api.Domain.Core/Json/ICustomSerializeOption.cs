using System.Text.Json;
using System.Text.Json.Serialization;

namespace Craftwork.Feedstock.Api.Domain.Core.Json
{
    public interface ICustomSerializeOption
    {
        ICustomSerializeOption AddConverter<T>(JsonConverter<T> jsonConverter);

        JsonSerializerOptions Option();
    }
}