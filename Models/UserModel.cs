using System;
using System.Text.Json.Serialization;
using Pertiv_be_with_dotnet.Enums;

namespace Pertiv_be_with_dotnet.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public UserRole Role { get; set; }

        public bool Is_Deleted { get; set; } = false;
    }

   
}
