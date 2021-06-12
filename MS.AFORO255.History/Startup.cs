using Aforo255.Cross.Event.Src;
using Aforo255.Cross.Event.Src.Bus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MS.AFORO255.History.Messages.EventHandlers;
using MS.AFORO255.History.Messages.Events;
using MS.AFORO255.History.Repositories;
using MS.AFORO255.History.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MS.AFORO255.History
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MS.AFORO255.History", Version = "v1" });
            //});

            services.Configure<Mongosettings>(opt =>
            {
                opt.Connection = Configuration.GetSection("mongo:cn").Value;
                opt.DatabaseName = Configuration.GetSection("mongo:database").Value;
            });
            services.AddScoped<IHistoryService, HistoryService>();
            services.AddScoped<IMongoBookDBContext, MongoBookDBContext>();

            /*Start - RabbitMQ*/
            //services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(Startup).GetTypeInfo().Assembly);
            services.AddRabbitMQ();

            services.AddTransient<TransactionEventHandler>();
            services.AddTransient<IEventHandler<TransactionCreatedEvent>, TransactionEventHandler>();
            /*End - RabbitMQ*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseSwagger();
                //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MS.AFORO255.History v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            ConfigureEventBus(app);
        }

        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<TransactionCreatedEvent, TransactionEventHandler>();
        }

    }
}
