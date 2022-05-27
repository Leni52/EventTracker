using EventTracker.BLL.Exceptions;
using EventTracker.BLL.Services;
using EventTracker.DAL.Entities;
using EventTracker.DTO.EventModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EventTracker.BLL.UnitTests
{
    public class EventServiceTest : BaseServiceTest
    {
        [Fact]
        public async Task CreateEventAsync_SuccessfullyCreatesWithValidInput()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);

            unitOfWork.Setup(uof => uof.Events.CheckIfNameExistsCreate(It.IsAny<string>())).ReturnsAsync(false);

            await sut.CreateEventAsync(new Event());

            unitOfWork.Verify(mock => mock.Events.CreateAsync(It.IsAny<Event>()), Times.Once);
        }

        [Fact]
        public async Task CreateEventAsync_ThrowsNameInUseException()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);

            unitOfWork.Setup(uof => uof.Events.CheckIfNameExistsCreate(It.IsAny<string>())).ReturnsAsync(true);

            await Assert.ThrowsAsync<ItemIsAlreadyUsedException>(async () => await sut.CreateEventAsync(new Event()));
        }

        [Fact]
        public async Task EditEventAsync_SuccessfullyEditsWithValidInput()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);
            var testEvent = new Event();

            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testEvent);
            unitOfWork.Setup(uof => uof.Events.CheckIfNameExistsEdit(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            await sut.EditEventAsync(new Event(), new Guid());

            unitOfWork.Verify(mock => mock.Events.Edit(It.IsAny<Event>()), Times.Once);
        }

        [Fact]
        public async Task EditEventAsync_ThrowsItemDoesNotExistException()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);

            Event testEvent = null;

            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testEvent);

            await Assert.ThrowsAsync<ItemDoesNotExistException>(async () => await sut.EditEventAsync(new Event(), new Guid()));
        }

        [Fact]
        public async Task EditEventAsync_ThrowsItemAlreadyUsedException()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);
            var testEvent = new Event();

            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testEvent);
            unitOfWork.Setup(uof => uof.Events.CheckIfNameExistsEdit(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            await Assert.ThrowsAsync<ItemIsAlreadyUsedException>(async () => await sut.EditEventAsync(new Event(), new Guid()));
        }

        [Fact]
        public async Task DeleteEventAsync_SuccessfullyDeletesWithValidInput()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);
            var testEvent = new Event();

            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testEvent);

            await sut.DeleteEventAsync(new Guid());

            unitOfWork.Verify(mock => mock.Events.Delete(It.IsAny<Event>()), Times.Once);
        }

        [Fact]
        public async Task DeleteEventAsync_ThrowsItemDoesNotExistException()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);

            Event testEvent = null;

            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testEvent);

            await Assert.ThrowsAsync<ItemDoesNotExistException>(async () => await sut.DeleteEventAsync(new Guid()));
        }

        [Fact]
        public async Task GetEventByIdAsync_SuccessfullyReturnsEvent()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);
            var testEvent = new Event();

            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testEvent);

            var result = await sut.GetEventByIdAsync(new Guid());

            Assert.Equal(result, testEvent);
        }

        [Fact]
        public async Task GetEventByIdAsync_ThrowsItemDoesNotExistException()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);

            Event testEvent = null;

            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testEvent);

            await Assert.ThrowsAsync<ItemDoesNotExistException>(async () => await sut.GetEventByIdAsync(new Guid()));
        }

        [Fact]
        public async Task GetAllEventsAsync_SuccessfullyReturnsCollection()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);
            var events = new List<Event>();

            unitOfWork.Setup(uof => uof.Events.GetAllAsync()).ReturnsAsync(events);

            var result = await sut.GetAllEventsAsync();

            Assert.Equal(result, events);
        }

        [Fact]
        public async Task SignUpRegularUserAsync_SuccessfullValidInput()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);
            var externalUser = new ExternalUser();
            var testEvent = new Event();

            unitOfWork.Setup(uof => uof.ExternalUsers.GetByExternalId(It.IsAny<string>())).ReturnsAsync(externalUser);
            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testEvent);

            await sut.SignUpRegularUserAsync(new Guid(), new System.Security.Claims.ClaimsPrincipal());

            notificationService.Verify(mock => mock
                .SendNotificationAsync(It.IsAny<Event>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task SignUpRegularUserAsync_ThrowsItemDoesNotExistException()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);
            var externalUser = new ExternalUser();
            Event nullEvent = null;

            unitOfWork.Setup(uof => uof.ExternalUsers.GetByExternalId(It.IsAny<string>())).ReturnsAsync(externalUser);
            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(nullEvent);

            await Assert.ThrowsAsync<ItemDoesNotExistException>(async () => await sut
                .SignUpRegularUserAsync(new Guid(), new System.Security.Claims.ClaimsPrincipal()));
        }

        [Fact]
        public async Task SignOutRegularUserAsync_SuccessfullValidInput()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);
            var externalUser = new ExternalUser();
            var testEvent = new Event();

            unitOfWork.Setup(uof => uof.ExternalUsers.GetByExternalId(It.IsAny<string>())).ReturnsAsync(externalUser);
            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testEvent);
            unitOfWork.Setup(uof => uof.Events.CheckIfUserIsInEvent(It.IsAny<Event>(), It.IsAny<ExternalUser>())).Returns(true);

            await sut.SignOutRegularUserAsync(new Guid(), new System.Security.Claims.ClaimsPrincipal());

            notificationService.Verify(mock => mock
                .SendNotificationAsync(It.IsAny<Event>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task SignOutRegularUserAsync_ThrowsItemDoesNotExistException()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);
            var externalUser = new ExternalUser();
            Event nullEvent = null;

            unitOfWork.Setup(uof => uof.ExternalUsers.GetByExternalId(It.IsAny<string>())).ReturnsAsync(externalUser);
            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(nullEvent);

            await Assert.ThrowsAsync<ItemDoesNotExistException>(async () => await sut
                .SignOutRegularUserAsync(new Guid(), new System.Security.Claims.ClaimsPrincipal()));
        }

        [Fact]
        public async Task SignOutRegularUserAsync_ThrowsInvalidSubscriberException()
        {
            var sut = new EventService(unitOfWork.Object, notificationService.Object);
            var externalUser = new ExternalUser();
            var testEvent = new Event();

            unitOfWork.Setup(uof => uof.ExternalUsers.GetByExternalId(It.IsAny<string>())).ReturnsAsync(externalUser);
            unitOfWork.Setup(uof => uof.Events.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(testEvent);
            unitOfWork.Setup(uof => uof.Events.CheckIfUserIsInEvent(It.IsAny<Event>(), It.IsAny<ExternalUser>())).Returns(false);

            await Assert.ThrowsAsync<InvalidSubscriberException>(async () => await sut
                .SignOutRegularUserAsync(new Guid(), new System.Security.Claims.ClaimsPrincipal()));
        }
    }
}