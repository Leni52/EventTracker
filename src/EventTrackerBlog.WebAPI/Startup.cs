using EventTrackerBlog.Application.Features.Articles.Commands;
using EventTrackerBlog.Application.Features.Articles.Queries;
using EventTrackerBlog.Application.Features.Comments.Commands;
using EventTrackerBlog.Application.Features.Comments.Queries;
using EventTrackerBlog.Domain.Data;
using EventTrackerBlog.Domain.DTO.Articles.Request;
using EventTrackerBlog.Domain.DTO.Articles.Response;
using EventTrackerBlog.Domain.DTO.Comments.Response;
using EventTrackerBlog.Domain.Seed;
using ExceptionHandling.Handler;
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
using static EventTrackerBlog.Application.Features.Articles.Commands.CreateArticle;
using static EventTrackerBlog.Application.Features.Articles.Commands.DeleteArticleById;
using static EventTrackerBlog.Application.Features.Articles.Commands.UpdateArticle;
using static EventTrackerBlog.Application.Features.Articles.Queries.GetAllArticles;
using static EventTrackerBlog.Application.Features.Articles.Queries.GetArticleById;
using static EventTrackerBlog.Application.Features.Comments.Commands.CreateComment;
using static EventTrackerBlog.Application.Features.Comments.Commands.DeleteComment;
using static EventTrackerBlog.Application.Features.Comments.Commands.EditComment;
using static EventTrackerBlog.Application.Features.Comments.Queries.GetAllComments;
using static EventTrackerBlog.Application.Features.Comments.Queries.GetCommentById;
using static EventTrackerBlog.Application.Features.Comments.Queries.GetCommentsByArticle;

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

            services.AddScoped<IRequestHandler<GetAllComments, IEnumerable<CommentResponseModel>>, GetAllCommentsHandler>();
            services.AddScoped<IRequestHandler<GetCommentsByArticle, IEnumerable<CommentResponseModel>>, GetCommentsByArticleHandler>();
            services.AddScoped<IRequestHandler<GetCommentById, CommentResponseModel>, GetCommentByIdHandler>();
            services.AddScoped<IRequestHandler<CreateComment, CommentResponseModel>, CreateCommentHandler>();
            services.AddScoped<IRequestHandler<EditComment, CommentResponseModel>, EditCommentHandler>();
            services.AddScoped<IRequestHandler<DeleteComment, Guid>, DeleteCommentHandler>();

            services.AddScoped<IRequestHandler<GetAllArticles, IEnumerable<ArticleResponseModel>>, GetAllArticlesHandler>();
            services.AddScoped<IRequestHandler<GetArticleById, ArticleResponseModel>, GetArticleByIdHandler>();
            services.AddScoped<IRequestHandler<CreateArticle, ArticleRequestModel>, CreateArticleHandler>();
            services.AddScoped<IRequestHandler<DeleteArticleById, Guid>, DeleteArticleByIdHandler>();
            services.AddScoped<IRequestHandler<UpdateArticle, ArticleResponseModel>, UpdateArticleHandler>();

            services.AddMediatR(typeof(Startup));
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
