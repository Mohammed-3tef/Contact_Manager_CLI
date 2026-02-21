using System;

namespace Contact_Manager_CLI.Models
{
    public class Contact
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }

        public Contact()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }
    }
}
