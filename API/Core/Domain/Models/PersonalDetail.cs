using System;

namespace API.Core.Domain.Models
{
    public class PersonalDetail
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }
    }
}