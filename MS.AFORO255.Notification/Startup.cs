using Aforo255.Cross.Event.Src;
using Aforo255.Cross.Event.Src.Bus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MS.AFORO255.Deposit.Messages.Events;
using MS.AFORO255.Notification.Messages.EventHandlers;
using MS.AFORO255.Notification.Repositories;

namespace MS.AFORO255.Notification
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
            services.AddDbContext<ContextDatabase>(
              opt =>
              {
                  opt.UseMySQL(Configuration["mariadb:cn"]);
              });

            /*Start - RabbitMQ*/
            services.AddMediatR(typeof(Startup));
            services.AddRabbitMQ();

            services.AddTransient<NotificationEventHandler>();
            services.AddTransient<IEventHandler<NotificationCreatedEvent>, NotificationEventHandler>();
            /*End - RabbitMQ*/

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
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
            eventBus.Subscribe<NotificationCreatedEvent, NotificationEventHandler>();
        }
    }
}
