using Craftwork.Feedstock.Api.Domain.Dtos.Color;
using Craftwork.Feedstock.Api.Domain.Interfaces.Applications;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Threading.Tasks;

namespace Craftwork.Feedstock.Api.Interface.Apis
{
    /// <summary>
    /// Provides endpoints for color.
    /// </summary>
    public class ColorApi
    {
        /// <summary>
        /// Orchestrator of the color's operations.
        /// </summary>
        private readonly IColorApplication _colorApplication;

        /// <summary>
        /// Creates a instance of ColorApi <see cref="ColorApi"/>.
        /// </summary>
        /// <param name="app">Provide of the mechanisms to configure an application's request</param>
        public ColorApi(IApplicationBuilder app, IColorApplication colorApplication)
        {
            Config(app);
            _colorApplication = colorApplication;
        }

        /// <summary>
        /// Configures methods of the color api.
        /// </summary>
        /// <param name="app">Provide of the mechanisms to configure an application's request</param>
        public void Config(IApplicationBuilder _app) =>
            _app.UseCors("CorsPolicy")
            .UseRouter(x =>
            {
                ///Method get a color.
                x.MapGet("colors/{id}", GetByIdAsync);

                //Method for list colors.
                x.MapGet("colors/{filter}/{orderby}/{page:int}/{qtyperpage:int}", GetList);

                ///Method for create color.
                x.MapPost("colors", Create);

                ///Method for update color.
                x.MapPut("colors/{id}", Update);

                ///Method for update color.
                x.MapDelete("colors/{id}", Delete);

                //_app.UseExceptionMiddleware();
            });

        private Func<HttpRequest, HttpResponse, RouteData, Task> GetByIdAsync => async (request, response, routeData) =>
        {
            var id = routeData.GetValue<Guid>("id");
            var color = await _colorApplication.GetAsync(id);

            await response.SuccessAsync(color);
        };

        private Func<HttpRequest, HttpResponse, RouteData, Task> GetList => async (request, response, routeData) =>
        {
            var filterValue = routeData.GetValue<string>("filter");
            var filter = filterValue == "*" ? null : filterValue;
            var orderby = routeData.GetValue<string>("orderby");
            var page = routeData.GetValue<int>("page");
            var qtyperpage = routeData.GetValue<int>("qtyperpage");
            //var pagination = await _colorApplication.ListAsync(filter, orderby, page, qtyperpage);

            // await response.WriteJson(colors);
            //await response.Ok(pagination);
        };

        private Func<HttpRequest, HttpResponse, RouteData, Task> Create => async (request, response, routeData) =>
        {
            var dto = await request.HttpContext.ReadFromJson<ColorCommandDto>();

            //await _colorApplication.InsertAsync(dto);
            response.NoContent();
        };

        private Func<HttpRequest, HttpResponse, RouteData, Task> Update => async (request, response, routeData) =>
        {
            var id = routeData.GetValue<Guid>("id");
            //var dto = await request.HttpContext.ReadFromJson<ColorDto>(_webHostEnvironment);
            //https://docs.microsoft.com/pt-br/ef/core/saving/disconnected-entities
            //http://blog.maskalik.com/entity-framework/2013/12/23/entity-framework-updating-database-from-detached-objects/
            //await _colorApplication.UpdateAsync(id, dto);
            response.NoContent();
        };

        private Func<HttpRequest, HttpResponse, RouteData, Task> Delete => async (request, response, routeData) =>
        {
            var id = routeData.GetValue<Guid>("id");

            await _colorApplication.DeleteAsync(id);
            response.NoContent();
        };
    }

    /// <summary>
    /// Provides that extensions's method for ColorApi
    /// </summary>
    public static class ColorApiExtensions
    {
        /// <summary>
        /// Registers a ColorApi.
        /// </summary>
        /// <param name="app">Provide of the mechanisms to configure an application's request</param>
        public static void UseColorApi(this IApplicationBuilder app)
        {
            var colorApplication = app.ApplicationServices.GetService(typeof(IColorApplication)) as IColorApplication;

            new ColorApi(app, colorApplication);
        }
    }
}
