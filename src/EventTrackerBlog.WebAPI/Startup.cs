using EventTrackerBlog.Application.Commands.Articles;
using EventTrackerBlog.Application.Commands.Comments;
using EventTrackerBlog.Application.Handlers.Articles;
using EventTrackerBlog.Application.Handlers.Comments;
using EventTrackerBlog.Application.Queries.Articles;
using EventTrackerBlog.Application.Queries.Comments;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using EventTrackerBlog.Domain.Entities;
using EventTrackerBlog.Domain.Seed;
using EventTrackerBlog.WebAPI.Middleware;
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

            services.AddAutoMapper(typeof(Startup));
            //dbcontext and sqlserver
            services.AddDbContext<BlogDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));

            services.AddDbContext<BlogDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:Default"]));

            //register handlers and queries/commands
            services.AddMediatR(typeof(Startup));

            services.AddScoped<IRequestHandler<GetAllCommentsQuery, IEnumerable<Comment>>, GetAllCommentsHandler>();
            services.AddScoped<IRequestHandler<GetCommentByIdQuery, Comment>, GetCommentByIdHandler>();
            services.AddScoped<IRequestHandler<CreateCommentCommand, CommentResponseModel>, CreateCommentHandler>();
            services.AddScoped<IRequestHandler<EditCommentCommand, CommentEditResponseModel>, EditCommentHandler>();
            services.AddScoped<IRequestHandler<DeleteCommentCommand, Guid>, DeleteCommentHandler>();
           
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
