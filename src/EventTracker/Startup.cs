using EventTracker.BLL.Interfaces;
using EventTracker.BLL.Services;
using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using EventTracker.DAL.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker
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
            //gateway part
            var audienceConfig = Configuration.GetSection("JWTConfig");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };
            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "EventsKey";
            })
             .AddJwtBearer("EventsKey", x =>
             {
                 x.RequireHttpsMetadata = false;
                 x.TokenValidationParameters = tokenValidationParameters;
             });


            //end of gateway part

            //dbcontext
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));
            //DI
            services.AddTransient<IEventService, EventService>();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventTracker", Version = "v1" });
            });

            services.AddTransient<IEventService, EventService>();
            services.AddTransient<ICommentService, CommentService>();

            services.AddTransient<IEventRepository, EventRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EventTracker v1"));
            }

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
