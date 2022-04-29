using EventTracker.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EventTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class EventsController : ControllerBase
    {
        private static IEventService _eventService;

        public EventsController(IEventService eventService) : base()
        {
            _eventService = eventService;
        }
    }
}
