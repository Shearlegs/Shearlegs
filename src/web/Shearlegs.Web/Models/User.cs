using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shearlegs.Web.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime CreateDate { get; set; }

        public string Password { get; set; }

        public string LastLoginString => LastLoginDate == default ? "Never" : LastLoginDate.ToString();

        public User MakeCopy()
        {
            return new User()
            {
                Id = Id,
                Name = Name,
                Role = Role,
                LastLoginDate = LastLoginDate,
                CreateDate = CreateDate,
                Password = Password
            };
        }
    }
}
