using AutoMapper;
using EventTracker.BLL.Exceptions;
using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using EventTracker.DAL.Entities;
using EventTracker.DTO.EventModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Net;
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
            return await _unitOfWork.Events.GetByIdAsync(eventId);
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
                throw new ItemIsAlreadyUsedException(HttpStatusCode.Conflict, "Name is already in use.");
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
                throw new ItemDoesNotExistException(HttpStatusCode.NotFound, "Event doesn't exist.");
            }

            bool checkName = await _unitOfWork.Events.CheckIfNameExistsEdit(eventRequest.Name, eventToEdit.Name);
            if (checkName)
            {
                throw new ItemIsAlreadyUsedException(HttpStatusCode.Conflict,"Name is already in use.");
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
                throw new ItemDoesNotExistException(HttpStatusCode.NotFound, "Event doesn't exist.");
            }

            _unitOfWork.Events.Delete(eventToDelete);
            await _unitOfWork.SaveAsync();
        }
    }
}
