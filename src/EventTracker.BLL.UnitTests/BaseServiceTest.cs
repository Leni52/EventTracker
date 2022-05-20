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
            eventService = new Mock<IEventService>();
            commentService = new Mock<ICommentService>();
        }

        public Mock<IUnitOfWork> unitOfWork { get; set; }
        public Mock<IEventService> eventService { get; set; }
        public Mock<ICommentService> commentService { get; set; }
    }
}
