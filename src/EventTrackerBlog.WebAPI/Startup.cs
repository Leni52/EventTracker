using EventTrackerBlog.BLL.Commands.Articles;
using EventTrackerBlog.BLL.Handlers.Articles;
using EventTrackerBlog.BLL.Handlers.Comments;
using EventTrackerBlog.BLL.Queries.Articles;
using EventTrackerBlog.BLL.Queries.Comments;
using EventTrackerBlog.DAL.Data;
using EventTrackerBlog.DAL.Entities;
using EventTrackerBlog.DAL.Seed;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using EventTrackerBlog.BLL.Commands.Comments;
using EventTrackerBlog.DAL.DTO.Comments.Response;

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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EventTrackerBlog.WebAPI", Version = "v1" });
            });

            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));


            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:Default"]));
            services.AddMediatR(typeof(Startup));
           
            //register handlers and queries/commands

            services.AddScoped<IRequestHandler<GetAllCommentsQuery, IEnumerable<Comment>>, GetAllCommentsHandler>();
            services.AddScoped<IRequestHandler<GetCommentByIdQuery, Comment>, GetCommentByIdHandler>();
            services.AddScoped<IRequestHandler<CreateCommentCommand, CommentResponseModel>, CreateCommentHandler>();
            
           
            services.AddScoped<IRequestHandler<GetAllArticlesQuery, IEnumerable<Article>>, GetAllArticlesHandler>();
            services.AddScoped<IRequestHandler<GetArticleByIdQuery, Article>, GetArticleByIdHandler>();
            services.AddScoped<IRequestHandler<CreateArticleCommand, Article>, CreateArticleCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteArticleByIdCommand, Guid>, DeleteArticleByIdCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateArticleCommand, Guid>, UpdateArticleCommandHandler>();
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
