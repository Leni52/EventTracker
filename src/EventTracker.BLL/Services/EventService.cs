﻿using AutoMapper;
using EventTracker.BLL.Interfaces;
using EventTracker.DAL.Contracts;
using EventTracker.DAL.Data;
using EventTracker.DAL.Entities;
using EventTracker.DTO.EventModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventTracker.BLL.Services
{
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
                throw new Exception("Event does not exist.");
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
                throw new Exception("Name is already in use.");
            }

            eventToCreate.CreatedAt = DateTime.Now;
            eventToCreate.LastModifiedAt = DateTime.Now;

            await _unitOfWork.Events.CreateAsync(eventToCreate);
            await _unitOfWork.SaveAsync();
            //_notificationService.SendNotificationAsync(eventToCreate);
        }

        public async Task EditEventAsync(Event editedEvent, Guid eventId)
        {
            var eventToEdit = await _unitOfWork.Events.GetByIdAsync(eventId);
            if (eventToEdit == null)
            {
                throw new Exception("Event doesn't exist.");
            }

            bool nameExists = await _unitOfWork.Events.CheckIfNameExistsEdit(editedEvent.Name, eventToEdit.Name);
            if (nameExists)
            {
                throw new Exception("Name is already in use.");
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
                throw new Exception("Event doesn't exist.");
            }

            _unitOfWork.Events.Delete(eventToDelete);
            await _unitOfWork.SaveAsync();
        }
    }
}
