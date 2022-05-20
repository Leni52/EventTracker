using AutoMapper;
using EventTracker.BLL.Exceptions;
using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Contracts;
using EventTracker.DAL.Entities;
using EventTracker.DTO.EventModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EventTracker.BLL.Services
{
    public class EventService : IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        public EventService(IUnitOfWork unitOfWork, IMapper mapper, INotificationService notificationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _unitOfWork.Events.GetAllAsync();
        }

        public async Task<Event> GetEventByIdAsync(Guid eventId)
        {
            var eventFromDb = await _unitOfWork.Events.GetByIdAsync(eventId);
            if(eventFromDb==null)
            {
                throw new ItemDoesNotExistException();
            }
            return eventFromDb;
        }

        public async Task<IEnumerable<Comment>> GetAllCommentsFromEvent(Guid eventId)
        {
            Event commentedEvent = await GetEventByIdAsync(eventId);
            return commentedEvent.Comments;
        }

        public async Task CreateEventAsync(EventRequestModel eventRequest)
        {
            bool EventExists = await _unitOfWork.Events.CheckIfNameExistsCreate(eventRequest.Name);
            if (EventExists)
            {
                throw new ItemIsAlreadyUsedException();
            }

            var eventToCreate = _mapper.Map<Event>(eventRequest);
            eventToCreate.CreatedAt = DateTime.Now;
            eventToCreate.LastModifiedAt = DateTime.Now;

            await _unitOfWork.Events.CreateAsync(eventToCreate);
            await _unitOfWork.SaveAsync();
            _notificationService.SendNotificationAsync(eventToCreate);
        }

        public async Task EditEventAsync(EventRequestModel eventRequest, Guid eventId)
        {
            var eventToEdit = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (eventToEdit == null)
            {
                throw new ItemDoesNotExistException();
            }

            bool checkName = await _unitOfWork.Events.CheckIfNameExistsEdit(eventRequest.Name, eventToEdit.Name);
            if (checkName)
            {
                throw new ItemIsAlreadyUsedException();
            }

            eventToEdit = _mapper.Map<Event>(eventRequest);
            eventToEdit.LastModifiedAt = DateTime.Now;

            _unitOfWork.Events.Update(eventToEdit);
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
        }

        public async Task SignUpRegularUser(Guid eventId, ClaimsPrincipal claimsPrincipal)
        {
            var userId = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            var externalUser = await _unitOfWork.ExternalUsers.GetByExternalId(userId);
            var eventData = await _unitOfWork.Events.GetByIdAsync(eventId);

            if (eventData == null)
            {
                throw new Exception("Event doesn't exist.");
            }

            eventData.Users.Add(externalUser);
            await _unitOfWork.SaveAsync();
        }
    }
}
