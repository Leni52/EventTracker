using AutoMapper;
using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Contracts;
using Moq;
using System;
using Xunit;

namespace EventTracker.BLL.UnitTests
{
    public class BaseServiceTest
    {
        public BaseServiceTest()
        {
            unitOfWork = new Mock<IUnitOfWork>();
            autoMapper = new Mock<IMapper>();
            notificationService = new Mock<INotificationService>();
            eventService = new Mock<IEventService>();
            commentService = new Mock<ICommentService>();
        }

        public Mock<IUnitOfWork> unitOfWork { get; set; }
        public Mock<IMapper> autoMapper { get; set; }
        public Mock<INotificationService> notificationService { get; set; }
        public Mock<IEventService> eventService { get; set; }
        public Mock<ICommentService> commentService { get; set; }
    }
}