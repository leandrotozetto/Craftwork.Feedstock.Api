﻿using Craftwork.Feedstock.Api.Domain.Core.Entities.Interfaces;
using Craftwork.Feedstock.Api.Domain.Core.Json;
using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using Microsoft.AspNetCore.Http;
using System.Buffers;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Interface
{
    /// <summary>
    /// Extensions for HttpContext.
    /// </summary>
    public static class HttpExtensions
    {
        /// <summary>
        /// Read json from HttpRequest.
        /// </summary>
        /// <typeparam name="T">Type of entity to be written</typeparam>
        /// <param name="httpContext">Context of the request.</param>
        /// <param name="webHostEnvironment">provider data from web hosting.</param>
        /// <returns></returns>
        public static async ValueTask<T> ReadFromJson<T>(this HttpContext httpContext)
        {
            var pipeReader = await httpContext.Request.BodyReader.ReadAsync();
            var buffer = pipeReader.Buffer;

            return ConvertToEntity<T>(buffer);
        }

        /// <summary>
        /// Create a SerializeOption
        /// </summary>
        /// <returns>returns new SerializeOption.</returns>
        private static JsonSerializerOptions CreateSerializerOptions()
        {
            return CustomSerializeOption.New()
                .AddConverter(new CustomJsonConverter<ColorCommandDto>())
                .Option();
        }

        /// <summary>
        /// Converts json to entity
        /// </summary>
        /// <typeparam name="T">Type.</typeparam>
        /// <param name="buffer">Byte[] containing json.</param>
        /// <returns>returns new entity</returns>
        public static T ConvertToEntity<T>(ReadOnlySequence<byte> buffer)
        {
            var utf8Reader = new Utf8JsonReader(buffer);
            var serializerOptions = CreateSerializerOptions();

            return JsonSerializer.Deserialize<T>(ref utf8Reader, serializerOptions);
        }

        /// <summary>
        /// Converts entity to json.
        /// </summary>
        /// <param name="entity">entity.</param>
        /// <returns>Returns string json.</returns>
        public static string ConvertToJson(object entity)
        {
            var serializerOptions = CreateSerializerOptions();

            return JsonSerializer.Serialize(entity, serializerOptions);
        }

        /// <summary>
        /// Reads a value from header
        /// </summary>
        /// <param name="httpContext">Context of the request.</param>
        /// <param name="hostingEnvironment">provider data from web hosting.</param>
        /// <param name="key">Key gets value returned.</param>
        /// <returns>Returns string with value.</returns>
        public static string ReadFromHeader(this HttpContext httpContext, string key)
        {
            httpContext.Request.Headers.TryGetValue(key, out var values);

            return values.FirstOrDefault();
        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        /// <param name="result">Data that will be serialized and returned.</param>
        /// <returns></returns>
        public static async Task Ok(this HttpResponse response, object result = null)
        {
            response.SetStatusCode(HttpStatusCode.OK);

            if (result != null)
            {
                var json = ConvertToJson(result);

                await response.WriteAsync(json);
            }
        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        public static HttpResponse NoContent(this HttpResponse response)
        {
            response.SetStatusCode(HttpStatusCode.NoContent);

            return response;
        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        public static async Task SuccessAsync(this HttpResponse response, object result = null)
        {
            if (!(result is IQueryDto queryDto) || result is null || (!(queryDto is null) && queryDto.IsEmpty()))
            {
                response.SetStatusCode(HttpStatusCode.NoContent);
            }
            else
            {
                await response.WriteAsync(JsonSerializer.Serialize(result,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
            }
        }

        /// <summary>
        /// Write json in response.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        /// <returns></returns>
        public static HttpResponse Unauthorized(this HttpResponse response)
        {
            response.SetStatusCode(HttpStatusCode.Unauthorized);

            return response;
        }

        /// <summary>
        /// Set ContentType (to Json) and httpStatusCode.
        /// </summary>
        /// <param name="response">HttResponse will be written the message.</param>
        /// <param name="httpStatusCode">HttpStatusCode to response</param>
        private static void SetStatusCode(this HttpResponse response, HttpStatusCode httpStatusCode)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)httpStatusCode;
        }
    }
}
