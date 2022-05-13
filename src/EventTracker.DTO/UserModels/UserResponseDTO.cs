using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTracker.DTO.UserModels
{
    public class UserResponseDTO
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UserName { get; set; }
    }
}
