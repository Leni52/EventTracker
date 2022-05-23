using EventTrackerBlog.Application.Features.Articles.Commands;
using EventTrackerBlog.Application.Features.Articles.Queries;
using EventTrackerBlog.Application.Features.Comments.Commands;
using EventTrackerBlog.Application.Features.Comments.Queries;
using EventTrackerBlog.Application.Handlers.Articles;
using EventTrackerBlog.Application.Handlers.Comments;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Articles.Request;
using EventTrackerBlog.Domain.DTO.Articles.Response;
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

            services.AddScoped<IRequestHandler<GetAllCommentsQuery, IEnumerable<CommentResponseModel>>, GetAllCommentsHandler>();
            services.AddScoped<IRequestHandler<GetCommentsByArticleQuery, IEnumerable<CommentResponseModel>>, GetCommentsByArticleHandler>();
            services.AddScoped<IRequestHandler<GetCommentByIdQuery, CommentResponseModel>, GetCommentByIdHandler>();
            services.AddScoped<IRequestHandler<CreateCommentCommand, CommentResponseModel>, CreateCommentHandler>();
            services.AddScoped<IRequestHandler<EditCommentCommand, CommentEditResponseModel>, EditCommentHandler>();
            services.AddScoped<IRequestHandler<DeleteCommentCommand, Guid>, DeleteCommentHandler>();

            services.AddScoped<IRequestHandler<GetAllArticlesQuery, IEnumerable<ArticleResponseModel>>, GetAllArticlesHandler>();
            services.AddScoped<IRequestHandler<GetArticleByIdQuery, ArticleResponseModel>, GetArticleByIdHandler>();
            services.AddScoped<IRequestHandler<CreateArticleCommand, ArticleRequestModel>, CreateArticleCommandHandler>();
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
