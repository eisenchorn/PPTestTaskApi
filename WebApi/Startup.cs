using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using WebApi.Exceptions;
using WebApi.Models;
using WebApi.Services;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton(InitOrders());
            services.AddSingleton(ImitPostamats());
            services.AddScoped<OrderService>();
            services.AddScoped<PostamatService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("some", new OpenApiInfo {Title = "OrdersAPI", Version = "v1"});
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/some/swagger.json", "OrdersAPI v1"); });
            app.UseRouting();

            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                try
                {
                    await next.Invoke();
                }
                catch (NotFoundException e)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.NotFound;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync($@"""{e.Message}""");
                    await context.Response.Body.FlushAsync();
                }
                catch (BadRequestException e)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.BadRequest;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync($@"""{e.Message}""");
                    await context.Response.Body.FlushAsync();
                }
                catch (ForbiddenException e)
                {
                    context.Response.StatusCode = (int) HttpStatusCode.Forbidden;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync($@"""{e.Message}""");
                    await context.Response.Body.FlushAsync();
                }
            });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private static List<Order> InitOrders()
        {
            return new List<Order>
            {
                new Order
                {
                    Id = 1,
                    Status = OrderStatusCode.Registred,
                    Amount = 10,
                    RecipientPhone = "88005553535",
                    RecipientName = "John Smith",
                    PostamatId = "Msk01"
                },
                new Order
                {
                    Id = 2,
                    Status = OrderStatusCode.AcceptedByWarehouse,
                    Amount = 10.5m,
                    RecipientPhone = "88005553535",
                    RecipientName = "John Smith",
                    PostamatId = "Msk01"
                },
                new Order
                {
                    Id = 3,
                    Status = OrderStatusCode.AcceptedByCourier,
                    Amount = 10,
                    RecipientPhone = "88005553535",
                    RecipientName = "John Smith",
                    PostamatId = "Msk01"
                },
                new Order
                {
                    Id = 4,
                    Status = OrderStatusCode.DeliveredToPostamat,
                    Amount = 10,
                    RecipientPhone = "88005553535",
                    RecipientName = "John Smith",
                    PostamatId = "Msk01"
                },
                new Order
                {
                    Id = 5,
                    Status = OrderStatusCode.DeliveredToRecipient,
                    Amount = 10,
                    RecipientPhone = "88005553535",
                    RecipientName = "John Smith",
                    PostamatId = "Msk01"
                },
                new Order
                {
                    Id = 13,
                    Status = OrderStatusCode.Registred,
                    Amount = 10,
                    RecipientPhone = "88005553535",
                    RecipientName = "John Smith",
                    PostamatId = "Msk01"
                }
            };
        }

        private static List<Postamat> ImitPostamats()
        {
            return new List<Postamat>
            {
                new Postamat
                {
                    Address = "Moscow Kremlin",
                    Id = "Msk01",
                    IsAlive = true
                },
                new Postamat
                {
                    Address = "Moscow VDNH",
                    Id = "Msk02",
                    IsAlive = true
                },
                new Postamat
                {
                    Address = "Moscow Lefortovo",
                    Id = "Msk03",
                    IsAlive = false
                },
                new Postamat
                {
                    Address = "Saint Petersburg Nevsky Prospect",
                    Id = "Spb01nvsk",
                    IsAlive = true
                },
                new Postamat
                {
                    Address = "Petergof",
                    Id = "Spb02",
                    IsAlive = false
                }
            };
        }
    }
}