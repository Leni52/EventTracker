using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Contracts;
using EventTracker.DAL.Entities;
using ExceptionHandling.Exceptions;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventTracker.BLL.Services
{
    using static Common.NotificationMessages;
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notificationService;

        public EventService(IUnitOfWork unitOfWork, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _unitOfWork.Events.GetAllAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            var eventToReturn = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (eventToReturn == null)
            {
                throw new ItemDoesNotExistException();
            }

            return eventToReturn;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsFromEvent(Guid eventId)
        {
            Event commentedEvent = await GetEventByIdAsync(eventId);
            return commentedEvent.Comments;
        }

        public async Task CreateEventAsync(Event eventToCreate)
        {
            bool eventExists = await _unitOfWork.Events.CheckIfNameExistsCreate(eventToCreate.Name);
            if (eventExists)
            {
                throw new ItemIsAlreadyUsedException();
            }

            eventToCreate.CreatedAt = DateTime.Now;
            eventToCreate.LastModifiedAt = DateTime.Now;

            await _unitOfWork.Events.CreateAsync(eventToCreate);
            await _unitOfWork.SaveAsync();
        }

        public async Task EditEventAsync(Event editedEvent, Guid eventId)
        {
            var eventToEdit = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (eventToEdit == null)
            {
                throw new ItemDoesNotExistException();
            }

            bool nameExists = await _unitOfWork.Events.CheckIfNameExistsEdit(editedEvent.Name, eventToEdit.Name);
            if (nameExists)
            {
                throw new ItemIsAlreadyUsedException();
            }

            eventToEdit.Name = editedEvent.Name;
            eventToEdit.Description = editedEvent.Description;
            eventToEdit.Category = editedEvent.Category;
            eventToEdit.Location = editedEvent.Location;
            eventToEdit.StartDate = editedEvent.StartDate;
            eventToEdit.EndDate = editedEvent.EndDate;
            eventToEdit.LastModifiedAt = DateTime.Now;

            _unitOfWork.Events.Edit(eventToEdit);
            await _unitOfWork.SaveAsync();
        }

        public async Task DeleteEventAsync(Guid eventId)
        {
            var eventToDelete = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (eventToDelete == null)
            {
                throw new ItemDoesNotExistException();
            }

            _unitOfWork.Events.Delete(eventToDelete);
            await _unitOfWork.SaveAsync();

            var subject = string.Format(DeletedEventSubject, eventToDelete.Name);
            var body = string.Format(DeletedEventBody, eventToDelete.Name, eventToDelete.Location, eventToDelete.StartDate, eventToDelete.EndDate);
            //  _notificationService.SendNotificationAsync(eventToDelete, subject, body);
        }

        public async Task SignUpRegularUserAsync(Guid eventId, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var externalUser = await _unitOfWork.ExternalUsers.GetByExternalId(userId);
            var eventData = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventData == null)
            {
                throw new ItemDoesNotExistException();
            }

            _unitOfWork.Events.AddUserToEvent(eventData, externalUser);
            await _unitOfWork.SaveAsync();

            var subject = string.Format(SubscribedToEventSubject, eventData.Name);
            var body = string.Format(SubscribedToEventBody, eventData.Name, eventData.Location, eventData.StartDate,
                eventData.EndDate);
            _notificationService.SendNotificationAsync(eventData, subject, body);
        }

        public async Task SignOutRegularUserAsync(Guid eventId, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var externalUser = await _unitOfWork.ExternalUsers.GetByExternalId(userId);
            var eventData = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventData == null)
            {
                throw new ItemDoesNotExistException();
            }

            if (!_unitOfWork.Events.CheckIfUserIsInEvent(eventData, externalUser))
            {
                throw new InvalidSubscriberException();
            }

            _unitOfWork.Events.RemoveUserFromEvent(eventData, externalUser);
            await _unitOfWork.SaveAsync();

            var subject = string.Format(SubscribedToEventSubject, eventData.Name);
            var body = string.Format(SubscribedToEventBody, eventData.Name, eventData.Location, eventData.StartDate,
                eventData.EndDate);
            _notificationService.SendNotificationAsync(eventData, subject, body);
        }
    }
}