using EventTrackerBlog.Data.Data;
using EventTrackerBlog.Data.Seed;
using EventTrackerBlog.WebAPI.Filters;
using ExceptionHandling.Handler;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using static EventTrackerBlog.Domain.Features.Articles.Commands.CreateArticle;

namespace EventTrackerBlog.WebAPI
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
            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventTrackerBlog.WebAPI", Version = "v1" });
            });
            //filter
            services.AddScoped<LogActionFilter>();

            services.AddAutoMapper(typeof(Startup));
            //dbcontext and sqlserver
            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));

            //register handlers and queries/commands
            services.AddMediatR(typeof(CreateArticleHandler));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            BlogDbSeeder.PrepPopulation(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventTrackerBlog.WebAPI v1"));
            }
            app.UseExceptionHandler(new ExceptionHandlerConfig().CustomOptions);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
