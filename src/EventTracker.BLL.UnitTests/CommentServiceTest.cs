﻿using EventTracker.BLL.Services;
using EventTracker.DAL.Entities;
using ExceptionHandling.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EventTracker.BLL.UnitTests
{
    public class CommentServiceTest : BaseServiceTest
    {
        [Fact]
        public async Task CreateCommentAsync_SuccessfullyCreatesWithValidInput()
        {
            var sut = new CommentService(unitOfWork.Object);

            unitOfWork.Setup(uof => uof.Comments.CreateAsync(It.IsAny<Comment>())).Verifiable();

            await sut.CreateCommentAsync(new Comment());

            unitOfWork.Verify(mock => mock.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task EditCommentAsync_SuccessfullyCreatesWithValidInput()
        {
            var sut = new CommentService(unitOfWork.Object);
            var comment = new Comment();

            unitOfWork.Setup(uof => uof.Comments.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(comment);

            await sut.EditCommentAsync(new Comment(), new Guid());

            unitOfWork.Verify(mock => mock.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task EditCommentAsync_ThrowsItemDoesNotExistException()
        {
            var sut = new CommentService(unitOfWork.Object);

            Comment comment = null;

            unitOfWork.Setup(uof => uof.Comments.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(comment);

            await Assert.ThrowsAsync<ItemDoesNotExistException>(async () => await sut
                .EditCommentAsync(new Comment(), new Guid()));
        }

        [Fact]
        public async Task DeleteCommentAsync_SuccessfullyCreatesWithValidInput()
        {
            var sut = new CommentService(unitOfWork.Object);
            var comment = new Comment();

            unitOfWork.Setup(uof => uof.Comments.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(comment);

            await sut.DeleteCommentAsync(new Guid());

            unitOfWork.Verify(mock => mock.SaveAsync(), Times.Once);
        }

        [Fact]
        public async Task DeleteCommentAsync_ThrowsItemDoesNotExistException()
        {
            var sut = new CommentService(unitOfWork.Object);

            Comment comment = null;

            unitOfWork.Setup(uof => uof.Comments.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(comment);

            await Assert.ThrowsAsync<ItemDoesNotExistException>(async () => await sut
                .DeleteCommentAsync(new Guid()));
        }

        [Fact]
        public async Task GetAllCommentsAsync_SuccessfullyReturnsCollection()
        {
            var sut = new CommentService(unitOfWork.Object);
            var comments = new List<Comment>();

            unitOfWork.Setup(uof => uof.Comments.GetAllAsync()).ReturnsAsync(comments);

            var result = await sut.GetAllCommentsAsync();

            Assert.Equal(result, comments);
        }

        [Fact]
        public async Task GetCommentByIdAsync_SuccessfullyReturnsEvent()
        {
            var sut = new CommentService(unitOfWork.Object);
            var comment = new Comment();

            unitOfWork.Setup(uof => uof.Comments.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(comment);

            var result = await sut.GetCommentByIdAsync(new Guid());

            Assert.Equal(result, comment);
        }

        [Fact]
        public async Task GetEventByIdAsync_ThrowsItemDoesNotExistException()
        {
            var sut = new CommentService(unitOfWork.Object);

            Comment comment = null;

            unitOfWork.Setup(uof => uof.Comments.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(comment);

            await Assert.ThrowsAsync<ItemDoesNotExistException>(async () => await sut.GetCommentByIdAsync(new Guid()));
        }
    }
}